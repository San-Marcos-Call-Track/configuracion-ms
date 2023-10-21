namespace Application.Ports.Repositories
{
    public interface IBaseRepository
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
