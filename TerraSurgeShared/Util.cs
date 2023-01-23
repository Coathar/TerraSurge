namespace TerraSurgeShared
{
    public static class Util
    {
        /// <summary>
        /// Creates a hash based on the given values.
        /// Adapted from https://github.com/SRombauts/cpp-algorithms/blob/master/src/algo/hash.cpp
        /// and https://social.msdn.microsoft.com/Forums/en-US/2784171b-313e-4d50-8d1f-db6db5f4e56f/rewritting-c-gethashcode-in-visual-basic-with-a-few-questions
        /// </summary>
        /// <param name="parameters">Parameters to hash together</param>
        /// <returns>A unique hash for the parameters provided.</returns>
        public static long CreateLongHashCode(params object[] parameters)
        {
            unchecked
            {
                long hash = 2166136261;

                foreach (object o in parameters)
                {
                    hash = hash * 16777619 ^ (o == null ? string.Empty.GetHashCode() : o.GetHashCode());
                }

                return hash;
            }
        }
    }
}
