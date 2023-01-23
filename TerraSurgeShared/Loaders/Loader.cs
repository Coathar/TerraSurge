using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Globalization;
using System.Reflection;
using TerraSurgeShared.Models;
using static TerraSurgeShared.Loaders.Loader;

namespace TerraSurgeShared.Loaders
{
    public abstract class Loader
    {
        public interface IBase<T> : IBase
        {
            public Guid SystemGuid { get; set; }

            public abstract T ToDatabaseObject();

            public abstract void UpdateDatabaseObject(T toUpdate);

            public static abstract IBase CreateFromDatabaseObject(T databaseObject);
        }

        public abstract void Start();

        public interface IBase
        {

        }
    }

    public abstract class Loader<T, W> : Loader where T : IBase<W> where W : class, ISystemLoaded
    {
        protected AppDbContext DbContext { get; set; }

        public Loader()
        {
            DbContext = new AppDbContext();
        }

        public override void Start()
        {
            Setup();

            List<T> fileRecords = new List<T>();
            List<T> dbRecords = new List<T>();

            string friendlyName = typeof(W).Name;
            
            using(TextFieldParser csvParser = new TextFieldParser($"Loaders/Resources/{friendlyName}.csv"))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;
               
                while (!csvParser.EndOfData)
                {
                    fileRecords.Add(ProcessLine(csvParser.ReadFields()));
                }
            }

            // Get all already existing db records that match file records
            DbSet<W> targetDbSet = (DbSet<W>)DbContext.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(DbSet<W>)).First().GetValue(DbContext);
            List<W> existingRecords = targetDbSet.Where(x => fileRecords.Select(x => x.SystemGuid).Contains(x.SystemGuid)).ToList();

            if (targetDbSet == null)
            {
                throw new Exception("Error running loader " + friendlyName + " as there is no DbSet in the context.");
            }

            foreach (W dbObject in existingRecords)
            {
                T dbRecord = (T)T.CreateFromDatabaseObject(dbObject);
                T fileRecord = fileRecords.Where(x => x.SystemGuid == dbRecord.SystemGuid).First();

                if (CompareHash(fileRecord, dbRecord))
                {
                    fileRecord.UpdateDatabaseObject(dbObject);
                    targetDbSet.Update(dbObject);
                }
            }

            // Loop over remaining file records, these need to be inserted
            foreach (T fileRecord in fileRecords)
            {
                if (!existingRecords.Select(x => x.SystemGuid).Contains(fileRecord.SystemGuid))
                {
                    targetDbSet.Add(fileRecord.ToDatabaseObject());
                }
            }

            // Remove remaining records that aren't in the file.
            targetDbSet.RemoveRange(targetDbSet.Where(x => !fileRecords.Select(x => x.SystemGuid).Contains(x.SystemGuid)));
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Compares two objects and returns whether or not it needs to be updated.
        /// </summary>
        /// <param name="fileRecord"></param>
        /// <param name="dbRecord"></param>
        /// <returns>Returns true if the record neesd to be updated and false if not.</returns>
        protected abstract bool CompareHash(T fileRecord, T dbRecord);

        protected abstract T ProcessLine(string[] fileLine);

        protected virtual void Setup()
        {

        }

        protected Guid ToGuid(string input)
        {
            return new Guid(input);
        }

        protected E ToEnum<E>(string input) where E : struct
        {
            return Enum.Parse<E>(input);
        }

        protected DateTime ToDateTime(string input)
        {
            return DateTime.ParseExact(input, "yyyy/M/d HH:mm:ss", CultureInfo.InvariantCulture);
        }
    }
}
