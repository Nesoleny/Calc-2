using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EveryDay.Calc.Webcalc.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\education\Calc-2\EveryDay.Calc\EveryDay.Calc.Webcalc\App_Data\calc.mdf;Integrated Security=True";

        private string TableName { get; set; }

        private string SelectColumns { get; set; }

        private string Columns { get; set; }

        public BaseRepository(string tableName, IEnumerable<string> columns)
        {
            TableName = tableName;
            Columns = string.Join(", ", columns);
            SelectColumns = "Id, " + string.Join(", ", columns);
        }

        public void Create(T obj)
        {
            string queryString = string.Format("INSERT into {0} ({1}) values ({2})", TableName, Columns, WriteSingleRow(obj));
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                
                command.ExecuteNonQuery();
            }
        }

        public void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            var result = new List<T>();

            string queryString = string.Format("SELECT {1} FROM {0}", TableName, SelectColumns);

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

        public virtual T ReadSingleRow(SqlDataReader record)
        {
            return null;
        }

        public virtual string WriteSingleRow(T obj)
        {
            return null;
        }

        public T Read(long Id)
        {
            T result = null;
            string queryString = string.Format("SELECT {1} FROM {0} WHERE {0}.Id = {2}", TableName, SelectColumns, Id);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                if (reader.HasRows)
                {
                    reader.Read();

                    result = ReadSingleRow(reader);
                }
                // Call Close when done reading.
                reader.Close();
            }
            return result;
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}