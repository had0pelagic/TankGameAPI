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

            if (field.LeftBorder > tank.XPosition - 1)
            {
                throw new Exception(Messages.Field.OutOfBorder);
            }

            tank.XPosition--;
            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }

        public async Task<string> MoveTankRight(MoveTankModel model)
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

            if (field.RightBorder <= tank.XPosition + 1)
            {
                throw new Exception(Messages.Field.OutOfBorder);
            }

            tank.XPosition++;
            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }

        public async Task<string> MoveTankUp(MoveTankModel model)
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

            if (field.TopBorder > tank.YPosition - 1)
            {
                throw new Exception(Messages.Field.OutOfBorder);
            }

            tank.YPosition--;
            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }

        public async Task<string> MoveTankDown(MoveTankModel model)
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

            if (field.BottomBorder <= tank.YPosition + 1)
            {
                throw new Exception(Messages.Field.OutOfBorder);
            }

            tank.YPosition++;
            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }
    }
}
