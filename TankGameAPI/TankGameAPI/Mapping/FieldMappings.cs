using Mapster;
using TankGameAPI.Models.Field;
using TankGameDomain;

namespace TankGameAPI.Mapping
{
    public class FieldMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Field, FieldModel>()
                .Map(x => x.Width, y => y.Width)
                .Map(x => x.Height, y => y.Height)
                .Map(x => x.LeftBorder, y => y.LeftBorder)
                .Map(x => x.RightBorder, y => y.RightBorder)
                .Map(x => x.TopBorder, y => y.TopBorder)
                .Map(x => x.BottomBorder, y => y.BottomBorder);
            config.NewConfig<FieldModel, Field>()
               .Map(x => x.Width, y => y.Width)
               .Map(x => x.Height, y => y.Height)
               .Map(x => x.LeftBorder, y => y.LeftBorder)
               .Map(x => x.RightBorder, y => y.RightBorder)
               .Map(x => x.TopBorder, y => y.TopBorder)
               .Map(x => x.BottomBorder, y => y.BottomBorder);
        }
    }
}
