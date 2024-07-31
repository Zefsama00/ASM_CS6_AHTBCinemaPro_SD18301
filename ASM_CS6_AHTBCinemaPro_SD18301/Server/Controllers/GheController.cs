// Controller cho Ghế
using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using ASM_CS6_AHTBCinemaPro_SD18301.Server.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class GheController : ControllerBase
{
    private readonly GheServer _gheServer;
    private readonly DBCinemaContext _context;

    public GheController(GheServer gheServer, DBCinemaContext context)
    {
        _gheServer = gheServer;
        _context = context;
    }

    // Lấy danh sách tất cả ghế
    [HttpGet]
    [Route("GetGhe")]
    public ActionResult<List<Ghe>> GetGhe()
    {
        var ghes = _context.Ghes.ToList();
        return Ok(ghes);
    }

    // Lấy thông tin ghế theo ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Ghe>> GetGhe(string id)
    {
        var ghe = await _context.Ghes.FindAsync(id);
        if (ghe == null)
        {
            return NotFound();
        }
        return Ok(ghe);
    }

    // Thêm một ghế mới
    [HttpPost]
    [Route("AddGhe")]
    public async Task<ActionResult<Ghe>> AddGhe(Ghe ghe)
    {
        // Ensure IdGhe is set
        if (string.IsNullOrEmpty(ghe.IdGhe))
        {
            ghe.IdGhe = new GheIdGenerator().Next(null);
        }

        // Check if the IdGhe already exists
        if (_context.Ghes.Any(e => e.IdGhe == ghe.IdGhe))
        {
            return BadRequest("ID của ghế đã tồn tại.");
        }

        _context.Ghes.Add(ghe);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetGhe), new { id = ghe.IdGhe }, ghe);
    }

    // Cập nhật thông tin ghế
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGhe(string id, Ghe ghe)
    {
        if (id != ghe.IdGhe)
        {
            return BadRequest();
        }

        _context.Entry(ghe).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GheExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // Xóa một ghế theo ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGhe(string id)
    {
        var ghe = await _context.Ghes.FindAsync(id);
        if (ghe == null)
        {
            return NotFound();
        }

        _context.Ghes.Remove(ghe);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool GheExists(string id)
    {
        return _context.Ghes.Any(e => e.IdGhe == id);
    }

    // Generator ID cho Ghế
    public class GheIdGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;

        public override string Next(EntityEntry entry)
        {
            return "GE" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }
    }

}
