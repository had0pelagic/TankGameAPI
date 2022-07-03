namespace TankGameAPI.Services
{
    public interface IFieldService
    {
        public Task<string> CreateField(int height, int width);
    }
}
