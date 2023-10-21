using Application.Ports.Repositories;

namespace Infrastructure.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}
