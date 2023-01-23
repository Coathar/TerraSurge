namespace TerraSurgeShared.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class Loader : Attribute
    {
        public Type DependsOn { get; set; }
    }
}
