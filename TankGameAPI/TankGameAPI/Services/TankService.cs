using MapsterMapper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<string> CreateTank(CreateTankModel model)
        {
            var field = await _context.Fields.FirstOrDefaultAsync() ?? throw new InvalidClientException(Messages.Field.NotFound);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username) ?? throw new InvalidClientException(Messages.User.NotFound);
            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Name);
            
            if (tank != null)
            {
                throw new InvalidClientException(Messages.Tank.Exists);
            }

            tank = _mapper.Map<Tank>(model);
            tank.Owner = user;
            tank.XPosition = Math.Abs(field.Width / 2);
            tank.YPosition = Math.Abs(field.Height / 2);

            await _context.Tanks.AddAsync(tank);
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} created at X: {tank.XPosition} Y: {tank.YPosition}";
        }

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
