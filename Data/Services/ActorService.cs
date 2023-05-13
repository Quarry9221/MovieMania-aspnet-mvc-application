using Microsoft.EntityFrameworkCore;
using MovieMania.Data.Base;
using MovieMania.Models;

namespace MovieMania.Data.Services
{
    public class ActorService : EntityBaseRepository<Actor>, IActorService
    {
        public ActorService(AppDbContext context) : base(context) { }
    }
}
