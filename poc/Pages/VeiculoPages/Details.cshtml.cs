using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using poc.Models;

namespace poc.Pages.VeiculoPages;

public class DetailsModel : PageModel
{
    private readonly AppDbContext _context;
    public DetailsModel(AppDbContext context)
    {
        _context = context;
    }

    public Veiculo Veiculo { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var veiculo = await _context.Veiculos.FirstOrDefaultAsync(m => m.Id == id);
        if (veiculo is null)
        {
            return NotFound();
        }
        else
        {
            Veiculo = veiculo;
        }

        return Page();
    }
}
