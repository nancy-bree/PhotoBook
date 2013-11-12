using StructureMap;
using StructureMap.Configuration.DSL;
using PhotoBook.DAL;

namespace PhotoBook.Infrastructure
{
    public class BootStrapper
    {
        public static void ConfigureDependencies()
        {
            ObjectFactory.Initialize(x => x.AddRegistry<ControllerRegistry>());
        }

        public class ControllerRegistry : Registry
        {
            public ControllerRegistry()
            {
                For<IUnitOfWork>().Use<UnitOfWork>();
            }
        }
    }
}