using Application.Ports.Repositories;

namespace Infrastructure.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }
    }
}
