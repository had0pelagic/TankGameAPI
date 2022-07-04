using Mapster;
using TankGameAPI.Models.Tank;
using TankGameDomain;

namespace TankGameAPI.Mapping
{
    public class TankMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Tank, TankInformationModel>()
                .Map(x => x.Owner, y => y.Owner)
                .Map(x => x.Name, y => y.Name)
                .Map(x => x.XPosition, y => y.XPosition)
                .Map(x => x.YPosition, y => y.YPosition);
            config.NewConfig<TankInformationModel, Tank>()
                .Map(x => x.Owner, y => y.Owner)
                .Map(x => x.Name, y => y.Name)
                .Map(x => x.XPosition, y => y.XPosition)
                .Map(x => x.YPosition, y => y.YPosition);

            config.NewConfig<Tank, CreateTankModel>()
               .Map(x => x.Owner, y => y.Owner)
               .Map(x => x.Name, y => y.Name);
            config.NewConfig<CreateTankModel, Tank>()
                .Map(x => x.Owner, y => y.Owner)
                .Map(x => x.Name, y => y.Name);
        }
    }
}
