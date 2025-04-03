using ExpenseTrackerAPI.Models;
using Microsoft.Data.SqlClient;

namespace ExpenseTrackerAPI.DataLayer
{
    public class ExpenseDL
    {
        private readonly string _connectionString;
        public ExpenseDL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<Expense> GetExpenses()
        {
            List<Expense> expenses = new();
            using (SqlConnection con = new(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new("Select * from Expenses", con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    expenses.Add(new Expense
                    {
                        Id = (int)reader["id"],
                        Category = reader["Category"].ToString(),
                        Amount = (decimal)reader["Amount"],
                        Date = DateTime.Now
                    });
                }
            }
            return expenses;
        }
    }
}
