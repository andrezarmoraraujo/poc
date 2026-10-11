using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using poc.Models;

namespace poc.Pages.VeiculoPages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public IList<Veiculo> Veiculo { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Veiculo = await _context.Veiculos.ToListAsync();
    }
}
