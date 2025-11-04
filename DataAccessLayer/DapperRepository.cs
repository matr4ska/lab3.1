using Dapper;
using System.Data;
using System.Data.SqlClient;
using static Dapper.SqlMapper;


namespace DataAccessLayer
{
    public class DapperRepository<Ship> : IRepository<Ship> 
    {
        static string connectionString;
        IDbConnection db;

        public DapperRepository()
        {
            connectionString = "Server=(LocalDB)\\MSSQLLocalDB; Database=ShipsDB;";    
        }
        
        public IEnumerable<Ship> GetAll()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
                return db.Query<Ship>("SELECT * FROM Ships").ToList();
        }

        public Ship GetItem(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
                return db.Query<Ship>("SELECT * FROM Ships WHERE Id = @Id", new { id }).FirstOrDefault();
        }

        public void Create(Ship item)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
                db.Execute("INSERT INTO Ships (Name, Hp, FlagColor, IsYourTurn) VALUES (@Name, @Hp, @FlagColor, @IsYourTurn)", item);
        }

        public void Update(Ship item)
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
