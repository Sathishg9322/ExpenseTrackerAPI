using ExpenseTrackerAPI.DataLayer;
using ExpenseTrackerAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/Expense")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseDL expenseDL;
        public ExpenseController(IConfiguration configuration)
        {
            expenseDL = new ExpenseDL(configuration);
        }

        [HttpGet]
        public IActionResult GetDetails() 
        {
            var expenses = expenseDL.GetExpenses();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetailsById(int id)
        {
            var expenses = expenseDL.GetExpenseById(id);
            return Ok(expenses);
        }


        [HttpPost]
        public IActionResult CreateNew(Expense expense)
        {
            expenseDL.AddExpense(expense);
            return Ok("Data inserted");
        }

        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            expenseDL.DeleteExpense(id);
            return Ok("Data deleted");
        }
    }
}
