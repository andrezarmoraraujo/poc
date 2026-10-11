using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using poc.Models;

public class ConsumosController : Controller
{
    private readonly AppDbContext _context;

    public ConsumosController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Consumos
    public async Task<IActionResult> Index()
    {
        var consumos = await _context.Consumos
            .Include(c => c.Veiculo)
            .ToListAsync();

        return View(consumos);
    }

    // GET: Consumos/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var consumo = await _context.Consumos
            .Include(c => c.Veiculo)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (consumo == null)
            return NotFound();

        return View(consumo);
    }

    // GET: Consumos/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.VeiculoId = new SelectList(
            await _context.Veiculos.ToListAsync(),
            "Id",
            "Nome"
        );

        return View();
    }

    // POST: Consumos/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Descricao,Data,Valor,Km,Tipo,VeiculoId")] Consumo consumo)
    {
        ModelState.Remove(nameof(Consumo.Veiculo));

        if (!await _context.Veiculos.AnyAsync(v => v.Id == consumo.VeiculoId))
        {
            ModelState.AddModelError(
                nameof(Consumo.VeiculoId),
                "Selecione um veículo cadastrado."
            );
        }

        if (ModelState.IsValid)
        {
            _context.Consumos.Add(consumo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewBag.VeiculoId = new SelectList(
            await _context.Veiculos.ToListAsync(),
            "Id",
            "Nome",
            consumo.VeiculoId
        );

        return View(consumo);
    }

    // GET: Consumos/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var consumo = await _context.Consumos.FindAsync(id);

        if (consumo == null)
            return NotFound();

        ViewBag.VeiculoId = new SelectList(
            await _context.Veiculos.ToListAsync(),
            "Id",
            "Nome",
            consumo.VeiculoId
        );

        return View(consumo);
    }

    // POST: Consumos/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Descricao,Data,Valor,Km,Tipo,VeiculoId")] Consumo consumo)
    {
        if (id != consumo.Id)
            return NotFound();

        ModelState.Remove(nameof(Consumo.Veiculo));

        if (!await _context.Veiculos.AnyAsync(v => v.Id == consumo.VeiculoId))
        {
            ModelState.AddModelError(
                nameof(Consumo.VeiculoId),
                "Selecione um veículo cadastrado."
            );
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Consumos.Update(consumo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsumoExists(consumo.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.VeiculoId = new SelectList(
            await _context.Veiculos.ToListAsync(),
            "Id",
            "Nome",
            consumo.VeiculoId
        );

        return View(consumo);
    }

    // GET: Consumos/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var consumo = await _context.Consumos
            .Include(c => c.Veiculo)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (consumo == null)
            return NotFound();

        return View(consumo);
    }

    // POST: Consumos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
            return NotFound();

        var consumo = await _context.Consumos.FindAsync(id);

        if (consumo == null)
            return NotFound();

        _context.Consumos.Remove(consumo);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool ConsumoExists(int? id)
    {
        return _context.Consumos.Any(c => c.Id == id);
    }
}