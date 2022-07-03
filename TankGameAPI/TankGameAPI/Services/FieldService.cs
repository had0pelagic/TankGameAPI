using TankGameDomain;
using TankGameInfrastructure;

namespace TankGameAPI.Services
{
    public class FieldService : IFieldService
    {
        private readonly Context _context;

        public FieldService(Context context)
        {
            _context = context;
        }

        public async Task<string> CreateField(int height, int width)
        {
            if (_context.Fields.Any())
            {
                throw new Exception("Field already exists");
            }

            var field = await _context.Fields.AddAsync(new Field()
            {
                Height = height,
                Width = width,
                TopBorder = 0,
                BottomBorder = height,
                LeftBorder = 0,
                RightBorder = width
            });
            await _context.SaveChangesAsync();

            return $"Field created with height: {field.Entity.Height} width: {field.Entity.Width}";
        }
    }
}
