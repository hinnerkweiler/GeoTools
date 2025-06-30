using GeoTools.Data;
using GeoTools.Data.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace GeoTools.Services;

public class LocationLookupService
{
    private readonly GeoDbContext _db;

    public LocationLookupService(GeoDbContext db)
    {
        _db = db;
    }

    public async Task<LocationInfoDto?> LookupAsync(double lat, double lon)
    {
        var sql = """
                  WITH point AS (
                      SELECT ST_SetSRID(ST_MakePoint(@lon, @lat), 4326) AS geom
                  )
                  SELECT
                      (SELECT name FROM seanamesiho s WHERE ST_Contains(s.geom, point.geom) LIMIT 1) AS "SeaRegion",
                      (SELECT featurecla FROM marinepoly e WHERE ST_Contains(e.geom, point.geom) LIMIT 1) AS "MarinePoly",
                      (SELECT TERRITORY1 FROM archipelagicwaters m WHERE ST_Contains(m.geom, point.geom) LIMIT 1) AS "Archipelago"
                  FROM point;
                  """;

        var param = new[]
        {
            new NpgsqlParameter("lat", lat),
            new NpgsqlParameter("lon", lon)
        };

        return await _db.Set<LocationInfoDto>().FromSqlRaw(sql, param).AsNoTracking().FirstOrDefaultAsync();
    }
}