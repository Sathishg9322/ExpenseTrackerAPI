using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ExpensesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/expenses
        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            var expense = await _context.Expenses.ToListAsync();
            return Ok(expense);
        }


        // GET: api/expenses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null) return NotFound();
            return Ok(expense);
        }

        // POST: api/expenses
        [HttpPost]
        public async Task<IActionResult> PostExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return Ok("Record Inserted");
        }

        // PUT: api/expenses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, Expense expense)
        {
            if (id != expense.Id) return BadRequest();

            _context.Entry(expense).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok("Record Updated");
        }

        // DELETE: api/expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null) return NotFound();

            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return Ok("Record Deleted");
        }
    }
}
