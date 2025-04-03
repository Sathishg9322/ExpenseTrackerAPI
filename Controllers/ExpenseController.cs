using ExpenseTrackerAPI.DataLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseDL expenseDL;
        public ExpenseController(IConfiguration configuration)
        {
            expenseDL = new ExpenseDL(configuration);
        }

        public IActionResult GetDetails() 
        {
            var expenses = expenseDL.GetExpenses();
            return Ok(expenses);
        }

    }
}
