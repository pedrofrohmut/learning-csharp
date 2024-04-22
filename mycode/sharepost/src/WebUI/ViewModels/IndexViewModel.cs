using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shareposts.WebUI.ViewModels;

public class IndexViewModel : PageModel
{
    private readonly ILogger<IndexViewModel> logger;

    public IndexViewModel(ILogger<IndexViewModel> logger)
    {
        this.logger = logger;
    }

    public void OnGet() { }
}
