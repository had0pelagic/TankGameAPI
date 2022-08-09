using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using TankGameAPI.Models;
using TankGameAPI.Models.Tank;
using TankGameAPI.Utils;
using TankGameAPI.Utils.Messages;
using TankGameDomain;
using TankGameInfrastructure;

namespace TankGameAPI.Services
{
    public class TankService : ITankService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public TankService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a tank with random coordinates
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="InvalidClientException"></exception>
        public async Task<string> CreateTank(CreateTankModel model)
        {
            var field = await _context.Fields.FirstOrDefaultAsync() ?? throw new InvalidClientException(Messages.Field.NotFound);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username) ?? throw new InvalidClientException(Messages.User.NotFound);
            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Name);

            if (tank != null)
            {
                throw new InvalidClientException(Messages.Tank.Exists);
            }

            var tankCoordinates = await GetTankCoordinates();
            int[] rotation = { 0, 90, 180, 270 };

            tank = _mapper.Map<Tank>(model);
            tank.Owner = user;
            tank.XPosition = tankCoordinates.Item1;
            tank.YPosition = tankCoordinates.Item2;
            tank.Rotation = rotation[new Random().Next(rotation.Length)];

            await _context.Tanks.AddAsync(tank);
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} created at X: {tank.XPosition} Y: {tank.YPosition}";
        }

        /// <summary>
        /// Moves tank left by 1 pixel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="InvalidClientException"></exception>
        public async Task<string> MoveTankLeft(MoveTankModel model)
        {
            var field = await _context.Fields.Include(x => x.Obstacles).FirstOrDefaultAsync() ?? throw new InvalidClientException(Messages.Field.NotFound);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username) ?? throw new InvalidClientException(Messages.User.NotFound);
            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name) ?? throw new InvalidClientException(Messages.Tank.NotFound);

            if (field.LeftBorder > tank.XPosition - 1)
            {
                throw new InvalidClientException(Messages.Field.OutOfBorder);
            }

            foreach (var obstacle in field.Obstacles)
            {
                if (tank.YPosition == obstacle.YPosition && tank.XPosition - 1 == obstacle.XPosition)
                {
                    throw new InvalidClientException(Messages.Tank.Collision);
                }
            }

            tank.XPosition--;

            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }

        /// <summary>
        /// Moves tank right by 1 pixel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="InvalidClientException"></exception>
        public async Task<string> MoveTankRight(MoveTankModel model)
        {
            var field = await _context.Fields.Include(x => x.Obstacles).FirstOrDefaultAsync() ?? throw new InvalidClientException(Messages.Field.NotFound);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username) ?? throw new InvalidClientException(Messages.User.NotFound);
            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name) ?? throw new InvalidClientException(Messages.Tank.NotFound);

            if (field.RightBorder <= tank.XPosition + 1)
            {
                throw new InvalidClientException(Messages.Field.OutOfBorder);
            }

            foreach (var obstacle in field.Obstacles)
            {
                if (tank.YPosition == obstacle.YPosition && tank.XPosition + 1 == obstacle.XPosition)
                {
                    throw new InvalidClientException(Messages.Tank.Collision);
                }
            }

            tank.XPosition++;

            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }

        /// <summary>
        /// Moves tank up by 1 pixel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="InvalidClientException"></exception>
        public async Task<string> MoveTankUp(MoveTankModel model)
        {
            var field = await _context.Fields.Include(x => x.Obstacles).FirstOrDefaultAsync() ?? throw new InvalidClientException(Messages.Field.NotFound);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username) ?? throw new InvalidClientException(Messages.User.NotFound);
            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name) ?? throw new InvalidClientException(Messages.Tank.NotFound);

            if (field.TopBorder > tank.YPosition - 1)
            {
                throw new InvalidClientException(Messages.Field.OutOfBorder);
            }

            foreach (var obstacle in field.Obstacles)
            {
                if (tank.XPosition == obstacle.XPosition && tank.YPosition - 1 == obstacle.YPosition)
                {
                    throw new InvalidClientException(Messages.Tank.Collision);
                }
            }

            tank.YPosition--;

            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }

        /// <summary>
        /// Moves tank down by 1 pixel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="InvalidClientException"></exception>
        public async Task<string> MoveTankDown(MoveTankModel model)
        {
            var field = await _context.Fields.Include(x => x.Obstacles).FirstOrDefaultAsync() ?? throw new InvalidClientException(Messages.Field.NotFound);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username) ?? throw new InvalidClientException(Messages.User.NotFound);
            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name) ?? throw new InvalidClientException(Messages.Tank.NotFound);

            if (field.BottomBorder <= tank.YPosition + 1)
            {
                throw new InvalidClientException(Messages.Field.OutOfBorder);
            }

            foreach (var obstacle in field.Obstacles)
            {
                if (tank.XPosition == obstacle.XPosition && tank.YPosition + 1 == obstacle.YPosition)
                {
                    throw new InvalidClientException(Messages.Tank.Collision);
                }
            }

            tank.YPosition++;

            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }

        /// <summary>
        /// Rotates tank left by 90 degrees
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="InvalidClientException"></exception>
        public async Task<string> RotateTankLeft(MoveTankModel model)
        {
            var field = await _context.Fields.FirstOrDefaultAsync() ?? throw new InvalidClientException(Messages.Field.NotFound);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username) ?? throw new InvalidClientException(Messages.User.NotFound);
            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name) ?? throw new InvalidClientException(Messages.Tank.NotFound);

            _ = tank.Rotation == 0 ? tank.Rotation = 270 : tank.Rotation -= 90;

            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} Rotation: {tank.Rotation}";
        }

        /// <summary>
        /// Rotates tank right by 90 degrees
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="InvalidClientException"></exception>
        public async Task<string> RotateTankRight(MoveTankModel model)
        {
            var field = await _context.Fields.FirstOrDefaultAsync() ?? throw new InvalidClientException(Messages.Field.NotFound);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username) ?? throw new InvalidClientException(Messages.User.NotFound);
            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name) ?? throw new InvalidClientException(Messages.Tank.NotFound);

            _ = tank.Rotation == 270 ? tank.Rotation = 0 : tank.Rotation += 90;

            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} Rotation: {tank.Rotation}";
        }

        /// <summary>
        /// Signal to make a tank shoot
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="InvalidClientException"></exception>
        public async Task<TankAttackModel> Attack(MoveTankModel model)
        {
            var field = await _context.Fields.FirstOrDefaultAsync() ?? throw new InvalidClientException(Messages.Field.NotFound);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username) ?? throw new InvalidClientException(Messages.User.NotFound);
            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name) ?? throw new InvalidClientException(Messages.Tank.NotFound);

            var tanks = await _context.Tanks
                .Include(x => x.Owner)
                .ToListAsync();

            HandleAttack(tank, tanks);

            await _context.SaveChangesAsync();

            return _mapper.Map<TankAttackModel>(tank);
        }

        /// <summary>
        /// Returns valid coordinates for to tank spawn in
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidClientException"></exception>
        private async Task<Tuple<int, int>> GetTankCoordinates()
        {
            var field = await _context.Fields.Include(x => x.Obstacles).FirstOrDefaultAsync() ?? throw new InvalidClientException(Messages.Field.NotFound);
            var tanks = await _context.Tanks.ToListAsync();

            var tanksCoordinates = tanks.Select(x => new CoordinatesModel()
            {
                XPosition = x.XPosition,
                YPosition = x.YPosition
            });
            var obstacleCoordinates = field.Obstacles.Select(x => new CoordinatesModel()
            {
                XPosition = x.XPosition,
                YPosition = x.YPosition
            });
            var obstacles = tanksCoordinates.Concat(obstacleCoordinates);

            while (true)
            {
                var x = new Random().Next(field.Width);
                var y = new Random().Next(field.Height);

                var isObstacleInCurrentPosition = obstacles.FirstOrDefault(o => o.XPosition == x && o.YPosition == y) != null;
                if (IsTrapped(x, y, field.Width, field.Height, obstacles) || isObstacleInCurrentPosition)
                {
                    continue;
                }

                return new Tuple<int, int>(x, y);
            }
        }

        /// <summary>
        /// Checks if a tank is trapped with given random coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="obstacles"></param>
        /// <returns></returns>
        private bool IsTrapped(int x, int y, int width, int height, IEnumerable<CoordinatesModel> obstacles)
        {
            var upObstacle = obstacles.FirstOrDefault(o => o.XPosition == x && o.YPosition == y - 1) != null;
            if (upObstacle || y - 1 == -1)
            {
                upObstacle = true;
            }
            else
            {
                upObstacle = false;
            }

            var downObstacle = obstacles.FirstOrDefault(o => o.XPosition == x && o.YPosition == y + 1) != null;
            if (downObstacle || y + 1 == height)
            {
                downObstacle = true;
            }
            else
            {
                downObstacle = false;
            }

            var rightObstacle = obstacles.FirstOrDefault(o => o.XPosition == x + 1 && o.YPosition == y) != null;
            if (rightObstacle || x + 1 == width)
            {
                rightObstacle = true;
            }
            else
            {
                rightObstacle = false;
            }

            var leftObstacle = obstacles.FirstOrDefault(o => o.XPosition == x - 1 && o.YPosition == y) != null;
            if (leftObstacle || x - 1 == -1)
            {
                leftObstacle = true;
            }
            else
            {
                leftObstacle = false;
            }

            return upObstacle && downObstacle && rightObstacle && leftObstacle;
        }

        /// <summary>
        /// Handles attack while checking if anyone is in front of the projectile
        /// </summary>
        /// <param name="tank"></param>
        /// <param name="tanks"></param>
        private void HandleAttack(Tank tank, List<Tank> tanks)
        {
            foreach (var enemy in tanks)
            {
                if (tank.Name == enemy.Name)
                {
                    continue;
                }

                var isEnemyAbove = tank.Rotation == 0 && tank.YPosition > enemy.YPosition && tank.XPosition == enemy.XPosition;
                var isEnemyBelow = tank.Rotation == 180 && tank.YPosition < enemy.YPosition && tank.XPosition == enemy.XPosition;
                var isEnemyRight = tank.Rotation == 90 && tank.XPosition < enemy.XPosition && tank.YPosition == enemy.YPosition;
                var isEnemyLeft = tank.Rotation == 270 && tank.XPosition > enemy.XPosition && tank.YPosition == enemy.YPosition;

                if (isEnemyAbove || isEnemyBelow || isEnemyLeft || isEnemyRight)
                {
                    _context.Tanks.Remove(enemy);
                    _context.Entry(enemy).State = EntityState.Deleted;
                    _context.Users.Remove(enemy.Owner);
                    _context.Entry(enemy.Owner).State = EntityState.Deleted;
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
