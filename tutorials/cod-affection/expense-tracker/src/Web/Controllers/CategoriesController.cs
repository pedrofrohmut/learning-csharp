using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Web.Data;
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Web.Models;

namespace ExpenseTracker.Web.Controllers;

public class CategoriesController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public CategoriesController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }


    // GET: Category
    public async Task<IActionResult> Index()
    {
        return this.dbContext.Categories != null ?
            View(await this.dbContext.Categories.ToListAsync()) :
            Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
    }


    // GET: Category/AddOrEdit
    public IActionResult AddOrEdit(int id = 0)
    {
        if (id == 0) {
            return View(new Category());
        } else {
            return View(this.dbContext.Categories?.Find(id));
        }
    }

    // POST: Category/AddOrEdit
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit([Bind("CategoryId,Title,Icon,Type")] Category category)
    {
        if (ModelState.IsValid) {
            if (category.CategoryId == 0)
            this.dbContext.Add(category);
            else
            this.dbContext.Update(category);
            await this.dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }


    // POST: Category/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (this.dbContext.Categories == null) {
            return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
        }
        var category = await this.dbContext.Categories.FindAsync(id);
        if (category != null) {
            this.dbContext.Categories.Remove(category);
        }
        await this.dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
