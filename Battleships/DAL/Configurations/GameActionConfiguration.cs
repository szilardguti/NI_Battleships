using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Battleships.DAL.Entities;
using Battleships.Model;
using Battleships.Model.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Battleships.DAL.Configurations
{
    public class GameActionConfiguration : IEntityTypeConfiguration<GameAction>
    {
        public void Configure(EntityTypeBuilder<GameAction> builder)
        {
            builder
                .Property(e => e.FirstPlayerBoardStatus)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<ICollection<Tile>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

            builder
                .Property(e => e.SecondPlayerBoardStatus)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<ICollection<Tile>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

            builder
                .Property(e => e.FirstPlayerShips)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<ICollection<ShipModel>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

            builder
                .Property(e => e.SecondPlayerShips)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<ICollection<ShipModel>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
        }
    }
}
