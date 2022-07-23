using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using TankGameAPI.Models.Field;
using TankGameAPI.Models.Tank;
using TankGameAPI.Utils.Messages;
using TankGameDomain;
using TankGameInfrastructure;

namespace TankGameAPI.Services
{
    public class FieldService : IFieldService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public FieldService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> CreateField(int height, int width, int obstacleCount)
        {
            if (_context.Fields.Any())
            {
                throw new Exception(Messages.Field.AlreadyExists);
            }

            var field = new Field()
            {
                Height = height,
                Width = width,
                TopBorder = 0,
                BottomBorder = height,
                LeftBorder = 0,
                RightBorder = width,
                Obstacles = GenerateObstacles(height, width, obstacleCount)
            };

            await _context.Fields.AddAsync(field);
            await _context.SaveChangesAsync();

            return $"Field created with height: {field.Height} width: {field.Width}";
        }

        public async Task<FieldModel> GetField()
        {
            var field = await _context.Fields
                .Include(x => x.Obstacles)
                .FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception(Messages.Field.AlreadyExists);
            }

            var tanks = _context.Tanks
                .Include(x => x.Owner)
                .Select(x => _mapper.Map<TankInformationModel>(x))
                .ToList();

            if (tanks.Count == 0)
            {
                throw new Exception(Messages.Tank.NoTanks);
            }

            var fieldInformation = _mapper.Map<FieldModel>(field);
            fieldInformation.Tanks = tanks;

            return fieldInformation;
        }

        private List<Obstacle> GenerateObstacles(int height, int width, int obstacleCount)
        {
            var obstacles = new List<Obstacle>();

            while (true)
            {
                if (obstacles.Count == obstacleCount)
                {
                    break;
                }

                var randomX = new Random().Next(width);
                var randomY = new Random().Next(height);
                var obstacle = new Obstacle() { XPosition = randomX, YPosition = randomY };

                if (!obstacles.Contains(obstacle))
                {
                    obstacles.Add(obstacle);
                }
            }

            return obstacles;
        }
    }
}
