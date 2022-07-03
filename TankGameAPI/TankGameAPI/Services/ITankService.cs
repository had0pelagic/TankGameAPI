using TankGameAPI.Models.Tank;

namespace TankGameAPI.Services
{
    public interface ITankService
    {
        public Task<string> CreateTank(CreateTankModel model);
        public Task<string> MoveTankLeft(MoveTankModel model);
        public Task<string> MoveTankRight(MoveTankModel model);
        public Task<string> MoveTankUp(MoveTankModel model);
        public Task<string> MoveTankDown(MoveTankModel model);
    }
}
