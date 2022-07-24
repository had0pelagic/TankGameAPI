using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using TankGameAPI.Models.Tank;
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
            var field = await _context.Fields.FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception(Messages.Field.NotFound);
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username);

            if (user == null)
            {
                throw new Exception(Messages.User.NotFound);
            }

            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Name);

            if (tank != null)
            {
                throw new Exception(Messages.Tank.Exists);
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
            var field = await _context.Fields
                .Include(x => x.Obstacles)
                .FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception(Messages.Field.NotFound);
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username);

            if (user == null)
            {
                throw new Exception(Messages.User.NotFound);
            }

            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name);

            if (tank == null)
            {
                throw new Exception(Messages.Tank.NotFound);
            }

            if (field.LeftBorder > tank.XPosition - 1)
            {
                throw new Exception(Messages.Field.OutOfBorder);
            }

            foreach (var obstacle in field.Obstacles)
            {
                if (tank.YPosition == obstacle.YPosition && tank.XPosition - 1 == obstacle.XPosition)
                {
                    throw new Exception(Messages.Tank.Collision);
                }
            }

            tank.XPosition--;
            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }

        public async Task<string> MoveTankRight(MoveTankModel model)
        {
            var field = await _context.Fields
                .Include(x => x.Obstacles)
                .FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception(Messages.Field.NotFound);
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username);

            if (user == null)
            {
                throw new Exception(Messages.User.NotFound);
            }

            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name);

            if (tank == null)
            {
                throw new Exception(Messages.Tank.NotFound);
            }

            if (field.RightBorder <= tank.XPosition + 1)
            {
                throw new Exception(Messages.Field.OutOfBorder);
            }

            foreach (var obstacle in field.Obstacles)
            {
                if (tank.YPosition == obstacle.YPosition && tank.XPosition + 1 == obstacle.XPosition)
                {
                    throw new Exception(Messages.Tank.Collision);
                }
            }

            tank.XPosition++;
            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }

        public async Task<string> MoveTankUp(MoveTankModel model)
        {
            var field = await _context.Fields
                .Include(x => x.Obstacles)
                .FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception(Messages.Field.NotFound);
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username);

            if (user == null)
            {
                throw new Exception(Messages.User.NotFound);
            }

            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name);

            if (tank == null)
            {
                throw new Exception(Messages.Tank.NotFound);
            }

            if (field.TopBorder > tank.YPosition - 1)
            {
                throw new Exception(Messages.Field.OutOfBorder);
            }

            foreach (var obstacle in field.Obstacles)
            {
                if (tank.XPosition == obstacle.XPosition && tank.YPosition - 1 == obstacle.YPosition)
                {
                    throw new Exception(Messages.Tank.Collision);
                }
            }

            tank.YPosition--;
            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }

        public async Task<string> MoveTankDown(MoveTankModel model)
        {
            var field = await _context.Fields
                .Include(x => x.Obstacles)
                .FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception(Messages.Field.NotFound);
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username);

            if (user == null)
            {
                throw new Exception(Messages.User.NotFound);
            }

            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name);

            if (tank == null)
            {
                throw new Exception(Messages.Tank.NotFound);
            }

            if (field.BottomBorder <= tank.YPosition + 1)
            {
                throw new Exception(Messages.Field.OutOfBorder);
            }

            foreach (var obstacle in field.Obstacles)
            {
                if (tank.XPosition == obstacle.XPosition && tank.YPosition + 1 == obstacle.YPosition)
                {
                    throw new Exception(Messages.Tank.Collision);
                }
            }

            tank.YPosition++;
            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }

        public async Task<string> RotateTankRight(MoveTankModel model)
        {
            var field = await _context.Fields.FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception(Messages.Field.NotFound);
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username);

            if (user == null)
            {
                throw new Exception(Messages.User.NotFound);
            }

            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name);

            if (tank == null)
            {
                throw new Exception(Messages.Tank.NotFound);
            }

            _ = tank.Rotation == 270 ? tank.Rotation = 0 : tank.Rotation += 90;

            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} Rotation: {tank.Rotation}";
        }

        public async Task<string> RotateTankLeft(MoveTankModel model)
        {
            var field = await _context.Fields.FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception(Messages.Field.NotFound);
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username);

            if (user == null)
            {
                throw new Exception(Messages.User.NotFound);
            }

            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name);

            if (tank == null)
            {
                throw new Exception(Messages.Tank.NotFound);
            }

            _ = tank.Rotation == 0 ? tank.Rotation = 270 : tank.Rotation -= 90;

            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} Rotation: {tank.Rotation}";
        }

        public async Task<TankAttackModel> Attack(MoveTankModel model)
        {
            var field = await _context.Fields.FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception(Messages.Field.NotFound);
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == model.Owner.Username);

            if (user == null)
            {
                throw new Exception(Messages.User.NotFound);
            }

            var tank = await _context.Tanks.FirstOrDefaultAsync(x => x.Name == model.Tank.Name);

            if (tank == null)
            {
                throw new Exception(Messages.Tank.NotFound);
            }

            var tanks = await _context.Tanks
                .Include(x => x.Owner)
                .ToListAsync();

            foreach (var i in tanks)
            {
                bool remove = false;

                if (i.Name == tank.Name)
                {
                    continue;
                }
                if (tank.Rotation == 0 && tank.YPosition > i.YPosition && tank.XPosition == i.XPosition)
                {
                    remove = true;
                }
                if (tank.Rotation == 180 && tank.YPosition < i.YPosition && tank.XPosition == i.XPosition && !remove)
                {
                    remove = true;
                }
                if (tank.Rotation == 90 && tank.XPosition < i.XPosition && tank.YPosition == i.YPosition && !remove)
                {
                    remove = true;
                }
                if (tank.Rotation == 270 && tank.XPosition > i.XPosition && tank.YPosition == i.YPosition && !remove)
                {
                    remove = true;
                }
                if (remove)
                {
                    _context.Tanks.Remove(i);
                    _context.Entry(i).State = EntityState.Deleted;
                    _context.Users.Remove(i.Owner);
                    _context.Entry(i.Owner).State = EntityState.Deleted;
                    break;
                }
                else
                {
                    continue;
                }
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<TankAttackModel>(tank);
        }
    }
}
