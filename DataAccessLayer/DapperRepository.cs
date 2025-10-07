using Dapper;
using Model;
using System.Data;
using System.Data.SqlClient;
using static Dapper.SqlMapper;


namespace DataAccessLayer
{
    public class DapperRepository<T> : IRepository<T>
    where T : class, IDomainObject
    {
        static string connectionString;
        IDbConnection db;


        public DapperRepository()
        {
            connectionString = "Server=(LocalDB)\\MSSQLLocalDB; Database=ShipsDB;";    
        }
        

        public IEnumerable<T> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
                return db.Query<T>("SELECT * FROM Ships").ToList();
        }

        public T GetItem(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
                return db.Query<T>("SELECT * FROM Ships WHERE Id = @Id", new { id }).FirstOrDefault();
        }

        public void Create(T item)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
                db.Execute("INSERT INTO Ships (Name, Hp, FlagColor, IsYourTurn) VALUES (@Name, @Hp, @FlagColor, @IsYourTurn)", item);
        }

        public void Update(T item)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
                db.Execute("UPDATE Ships SET Name = @Name, Hp = @Hp, FlagColor = @FlagColor, IsYourTurn = @IsYourTurn WHERE Id = @Id", item);
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
                db.Execute("DELETE FROM Ships WHERE Id = @Id", new { id });
        }
    }
}
