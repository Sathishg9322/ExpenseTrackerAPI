using ExpenseTrackerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

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
                        Id = (int)reader["Id"],
                        Category = reader["Category"].ToString(),
                        Amount = (decimal)reader["Amount"],
                        Date = Convert.ToDateTime(reader["Date"].ToString())
                    });
                }
            }
            return expenses;
        }

        public List<Expense> GetExpenseById(int id)
        {
            List<Expense> expenses = new();
            using (SqlConnection con = new(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new("Select * from Expenses where Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    expenses.Add(new Expense
                    {
                        Id = (int)reader["Id"],
                        Category = reader["Category"].ToString(),
                        Amount = (decimal)reader["Amount"],
                        Date = Convert.ToDateTime(reader["Date"].ToString())
                    });
                }
                return expenses;
            }
            return null;
        }

        public void AddExpense(Expense expense)
        {
            using (SqlConnection con = new(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new("INSERT INTO Expenses (Category, Amount, Date) VALUES (@Category, @Amount, @Date)", con);
                cmd.Parameters.AddWithValue("@Category", expense.Category);
                cmd.Parameters.AddWithValue("@Amount", expense.Amount);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteExpense(int id)
        {
            using (SqlConnection con = new(_connectionString))
            {
                con.Open();
                SqlCommand cmd = new("DELETE FROM Expenses WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
