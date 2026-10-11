using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using poc.Models;

namespace poc.Pages.VeiculoPages;

public class DeleteModel : PageModel
{
    private readonly AppDbContext _context;

    public DeleteModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var veiculo = await _context.Veiculos.FindAsync(id);
        if (veiculo != null)
        {
            Veiculo = veiculo;
            _context.Veiculos.Remove(Veiculo);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}
