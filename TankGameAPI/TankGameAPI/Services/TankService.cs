using Mapster;
using Microsoft.EntityFrameworkCore;
using TankGameAPI.Models.Tank;
using TankGameDomain;
using TankGameInfrastructure;

namespace TankGameAPI.Services
{
    public class TankService : ITankService
    {
        private readonly Context _context;

        public TankService(Context context)
        {
            _context = context;
        }

        public async Task<string> CreateTank(CreateTankModel model)
        {
            var field = await _context.Fields.FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception("Field not found");
            }

            var user = _context.Users.FirstOrDefault(x => x.Name == model.Name);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var tank = model.Adapt<Tank>();
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
                throw new Exception("Field not found");
            }

            var user = _context.Users.FirstOrDefault(x => x.Name == model.Owner.Username);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var tank = _context.Tanks.FirstOrDefault(x => x.Name == model.Tank.Name);

            if (tank == null)
            {
                throw new Exception("Tank not found");
            }

            if (field.LeftBorder > tank.XPosition - 1)
            {
                throw new Exception("Moving out of field is prohibited");
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
                throw new Exception("Field not found");
            }

            var user = _context.Users.FirstOrDefault(x => x.Name == model.Owner.Username);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var tank = _context.Tanks.FirstOrDefault(x => x.Name == model.Tank.Name);

            if (tank == null)
            {
                throw new Exception("Tank not found");
            }

            if (field.RightBorder < tank.XPosition + 1)
            {
                throw new Exception("Moving out of field is prohibited");
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
                throw new Exception("Field not found");
            }

            var user = _context.Users.FirstOrDefault(x => x.Name == model.Owner.Username);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var tank = _context.Tanks.FirstOrDefault(x => x.Name == model.Tank.Name);

            if (tank == null)
            {
                throw new Exception("Tank not found");
            }

            if (field.TopBorder > tank.YPosition + 1)
            {
                throw new Exception("Moving out of field is prohibited");
            }

            tank.YPosition++;
            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }

        public async Task<string> MoveTankDown(MoveTankModel model)
        {
            var field = await _context.Fields.FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception("Field not found");
            }

            var user = _context.Users.FirstOrDefault(x => x.Name == model.Owner.Username);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var tank = _context.Tanks.FirstOrDefault(x => x.Name == model.Tank.Name);

            if (tank == null)
            {
                throw new Exception("Tank not found");
            }

            if (field.BottomBorder > tank.YPosition - 1)
            {
                throw new Exception("Moving out of field is prohibited");
            }

            tank.YPosition--;
            _context.Entry(tank).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return $"Tank: {tank.Name} X: {tank.XPosition} Y: {tank.YPosition}";
        }
    }
}
