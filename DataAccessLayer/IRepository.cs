namespace DataAccessLayer
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Возвращает список всех сущностей
        /// </summary>
        /// <returns>Список сущностей</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Возвращает сущность по её Id
        /// </summary>
        /// <param name="id">Id сущности</param>
        /// <returns>Сущность по Id</returns>
        T GetItem(int id);

        /// <summary>
        /// Добавляет сущность в БД
        /// </summary>
        /// <param name="obj"></param>
        void Create(T obj);

        /// <summary>
        /// Обновляет атрибуты сущности в БД
        /// </summary>
        /// <param name="obj"></param>
        void Update(T obj);

        /// <summary>
        /// Удаляет сущность из БД.
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
