using Microsoft.Data.SqlClient;
using labdemo1.Model;

namespace labdemo1.Model
{
    public class StudentViewModel
    {

        string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=LabDb;Integrated Security=True";

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand("select * from Student", connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Student student = new Student();
                student.No = Convert.ToInt32(reader["No"]);
                student.Name = reader["Name"].ToString();
                student.Address = reader["Address"].ToString();
                student.Age = Convert.ToInt32(reader["Age"]);
                student.Email = reader["Email"].ToString();

                students.Add(student);
            }
            connection.Close();

            return students;
        }

        internal Student GetStudents(int No)
        {
            List<Student> student = GetStudents();
            Student filterInventory = (from student1 in student
                                       where student1.No == No select student1)
                                       .First();

            return filterInventory;
        }

        internal int AddStudent(Student student)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            String queryFormat = "insert into Student(Name, Address, Age, Email, IsEmailValidated) values('{0}', '{1}', '{2}', '{3}', '{4}')";

            string query = string.Format(queryFormat, student.Name, student.Address, student.Age, student.Email, false);
            SqlCommand command = new SqlCommand(query, connection);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected;
        }

        internal int UpdateStudent(Student student)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string queryFormat = "update Student set Name = '{0}', Address = '{1}', Age = {2}, Email = '{3}', IsEmailValidated='{4}' where No = {5}";
            string query = string.Format(queryFormat,
                student.Name,
                student.Address,
                student.Age,
                student.Email,
                false,
                student.No);
            SqlCommand command = new SqlCommand(query, connection);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected;
        }

        internal int RemoveStudent(int No)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string queryFormat = "delete from Student where No={0}";
            string query = string.Format(queryFormat,No);
            SqlCommand command = new SqlCommand(query, connection);
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected;
        }
    }
}
