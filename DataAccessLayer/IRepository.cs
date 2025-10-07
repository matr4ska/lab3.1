namespace DataAccessLayer
{
    public interface IRepository<T>
    where T : class
    {
        IEnumerable<T> GetAll();
        T GetItem(int id);
        void Create(T obj);
        void Update(T obj);
        void Delete(int id);
    }
}
