using TankGameAPI.Models.Tank;

namespace TankGameAPI.Services
{
    public interface ITankService
    {
        public Task<string> CreateTank(CreateTankModel model);
        public Task<string> MoveTankLeft(MoveTankModel model);
    }
}
