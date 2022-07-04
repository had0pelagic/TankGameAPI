﻿using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using TankGameAPI.Models.Field;
using TankGameAPI.Models.Tank;
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

        public async Task<FieldModel> GetField()
        {
            if (!_context.Fields.Any())
            {
                throw new Exception("Field not found");
            }

            var field = await _context.Fields.FirstOrDefaultAsync();

            if (field == null)
            {
                throw new Exception("Field not found");
            }

            var tanks = _context.Tanks
                .Include(x => x.Owner)
                .Select(x => _mapper.Map<TankInformationModel>(x))
                .ToList();

            if (tanks.Count == 0)
            {
                throw new Exception("No tanks in the field");
            }

            var fieldInformation = _mapper.Map<FieldModel>(field);
            fieldInformation.Tanks = tanks;

            return fieldInformation;
        }
    }
}
