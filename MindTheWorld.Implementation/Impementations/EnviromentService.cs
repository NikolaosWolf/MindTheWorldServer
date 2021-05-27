﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MindTheWorld.Api.Dtos;
using MindTheWorld.Domain;
using MindTheWorld.Extras.Helpers;
using MindTheWorld.Services.Definitions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MindTheWorld.Services.Implementations
{
    public class EnviromentService : IEnviromentService
    {
        private readonly ICsvTransformer _csvTransformer;
        private readonly ITransformService _transformService;
        private readonly MindTheWorldContext _mindTheWorldContext;

        public EnviromentService(ICsvTransformer csvTransformer,
                                 ITransformService transformService,
                                 MindTheWorldContext mindTheWorldContext)
        {
            _csvTransformer = csvTransformer;
            _transformService = transformService;
            _mindTheWorldContext = mindTheWorldContext;
        }

        public async Task<IEnumerable<MaterialFootprintPerCapitum>> ImportMaterialFootprints(IFormFile inputFile)
        {
            var dataTable = await _csvTransformer.Transform(inputFile);

            _mindTheWorldContext
                .MaterialFootprintPerCapita
                .AddRange(await _transformService.ToEntities<MaterialFootprintPerCapitum, decimal?>(dataTable));

            await _mindTheWorldContext.SaveChangesAsync();

            return null;
        }

        public async Task<IEnumerable<Co2EmissionsPerPerson>> ImportCo2Emissions(IFormFile inputFile)
        {
            var dataTable = await _csvTransformer.Transform(inputFile);

            _mindTheWorldContext
                .Co2EmissionsPerPeople
                .AddRange(await _transformService.ToEntities<Co2EmissionsPerPerson, decimal?>(dataTable));

            await _mindTheWorldContext.SaveChangesAsync();

            return null;
        }

        public async Task<IEnumerable<EpidemicDeath>> ImportEpidemicDeaths(IFormFile inputFile)
        {
            var dataTable = await _csvTransformer.Transform(inputFile);

            _mindTheWorldContext
                .EpidemicDeaths
                .AddRange(await _transformService.ToEntities<EpidemicDeath, int?>(dataTable));

            await _mindTheWorldContext.SaveChangesAsync();

            return null;
        }

        public async Task<IEnumerable<RenewableWater>> ImportRenewableWaters(IFormFile inputFile)
        {
            var dataTable = await _csvTransformer.Transform(inputFile);

            _mindTheWorldContext
                .RenewableWaters
                .AddRange(await _transformService.ToEntities<RenewableWater, decimal?>(dataTable));

            await _mindTheWorldContext.SaveChangesAsync();

            return null;
        }

        public async Task<IEnumerable<WaterWithdrawlPerPerson>> ImportWaterWithdrawals(IFormFile inputFile)
        {
            var dataTable = await _csvTransformer.Transform(inputFile);

            _mindTheWorldContext
                .WaterWithdrawlPerPeople
                .AddRange(await _transformService.ToEntities<WaterWithdrawlPerPerson, decimal?>(dataTable));

            await _mindTheWorldContext.SaveChangesAsync();

            return null;
        }

        public async Task<IEnumerable<IndexDto>> GetEpidemicDeathsAsync()
        {
            var data = await _mindTheWorldContext.EpidemicDeaths.Include(d => d.Country).ToListAsync();

            var result = new List<IndexDto>();

            foreach (var item in data)
            {
                result.Add(new IndexDto
                {
                    Country = item.Country.Name,
                    Year = item.Year,
                    Value = item.Value.GetValueOrDefault()
                });
            }

            return result;
        }
    }
}
