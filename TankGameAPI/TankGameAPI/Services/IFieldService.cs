using TankGameAPI.Models.Field;

namespace TankGameAPI.Services
{
    public interface IFieldService
    {
        public Task<string> CreateField(int height, int width, int obstacleCount);
        public Task<FieldModel> GetField();
    }
}
