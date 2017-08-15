using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EveryDay.Calc.Webcalc.Repository
{
    public class OperationRepository : IRepository<Operation>
    {
        public void Create(Operation obj)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\education\Calc-2\EveryDay.Calc\EveryDay.Calc.Webcalc\App_Data\calc.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //параметризованный запрос
                    string req = "INSERT INTO dbo.Operation VALUES(@Name)";
                    //открываем соединение с базой данных
                    connection.Open();
                    //создаём команду
                    SqlCommand cmd = new SqlCommand(req, connection);
                    //создаем параметр и добавляем его в коллекцию
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    //выполняем sql запрос
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
            }
        }
   

        public void Delete(long Id)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\education\Calc-2\EveryDay.Calc\EveryDay.Calc.Webcalc\App_Data\calc.mdf;Integrated Security=True";
 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //параметризованный запрос
                    string req = "DELETE FROM dbo.Operation " +
                    "WHERE id = @id";
                    //открываем соединение с базой данных
                    connection.Open();
                    //создаём команду
                    SqlCommand cmd = new SqlCommand(req, connection);
                    //создаем параметр и добавляем его в коллекцию
                    cmd.Parameters.AddWithValue("@id", Id);
                    //выполняем sql запрос
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public IEnumerable<Operation> GetAll()
        {
            var result = new List<Operation>();

            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\education\Calc-2\EveryDay.Calc\EveryDay.Calc.Webcalc\App_Data\calc.mdf;Integrated Security=True";
            string queryString ="SELECT Id, Name, Description FROM dbo.Operation";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    result.Add(ReadSingleRow(reader));
                }

                // Call Close when done reading.
                reader.Close();
            }
            return result;
        }

        private Operation ReadSingleRow(IDataRecord record)
        {
            return new Operation()
            {
                Id = record.GetInt64(0),
                Name = record.GetString(1)
            };
            //Console.WriteLine(String.Format("{0}, {1}", record[0], record[1]));
        }

        public Operation Read(long Id)
        {
            var result = new Operation();

            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\education\Calc-2\EveryDay.Calc\EveryDay.Calc.Webcalc\App_Data\calc.mdf;Integrated Security=True";
            string queryString = "SELECT Id, Name, Description FROM dbo.Operation WHERE id = @id";

            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@id", Id);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                reader.Read();

                result = ReadSingleRow(reader);

                // Call Close when done reading.
                reader.Close();
            }
            return result;
        }

        public void Update(Operation obj)
        {
            var connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\education\Calc-2\EveryDay.Calc\EveryDay.Calc.Webcalc\App_Data\calc.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    //параметризованный запрос
                    string req = "UPDATE dbo.Operation SET Name = @Name Where Id = @Id";
                    //открываем соединение с базой данных
                    connection.Open();
                    //создаём команду
                    SqlCommand cmd = new SqlCommand(req, connection);
                    //создаем параметр и добавляем его в коллекцию
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    //выполняем sql запрос
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    public class Operation
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}