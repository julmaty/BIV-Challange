using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BIV_Challange;
using BIV_Challange.Models;
using BIV_Challange.ViewModels;

namespace BIV_Challange.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CutoffsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CutoffsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Cutoffs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CutoffListViewModel>>> GetCutoffs()
        {
            var cutoffs = _context.Cutoffs.Select(c => new CutoffListViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Type = c.Type
            }).ToList();
            return cutoffs;
        }

        // GET: api/Cutoffs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cutoff>> GetCutoff(int id)
        {
            var cutoff = _context.Cutoffs.Include(c => c.cutOffValues).Where(c => c.Id == id).FirstOrDefault();

            if (cutoff == null)
            {
                return NotFound();
            }

            return cutoff;
        }

        // PUT: api/Cutoffs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCutoff(int id, Cutoff cutoff)
        {
            if (id != cutoff.Id)
            {
                return BadRequest();
            }

            Cutoff? cutoffInDb = _context.Cutoffs.Include(c => c.cutOffValues).Where(c => c.Id == id).FirstOrDefault();
            cutoffInDb.Name = cutoff.Name;
            cutoffInDb.Type = cutoff.Type;

            _context.Entry(cutoffInDb).State = EntityState.Modified;

            var newValuesIds = cutoff.cutOffValues.Select(v => v.Id).Except(cutoffInDb.cutOffValues.Select(v => v.Id));
            var newValues = cutoff.cutOffValues.Where(c => newValuesIds.Contains(c.Id)).ToList();
            var valuesToDeleteIds = cutoffInDb.cutOffValues.Select(v => v.Id).Except(cutoff.cutOffValues.Select(v => v.Id));
            var valuesToDelete = cutoffInDb.cutOffValues.Where(c => valuesToDeleteIds.Contains(c.Id)).ToList();
            var editedValuesIds = cutoff.cutOffValues.Select(v => v.Id).Intersect(cutoffInDb.cutOffValues.Select(v => v.Id));
            var editedValues = cutoffInDb.cutOffValues.Where(c => editedValuesIds.Contains(c.Id)).ToList();
            foreach (var cutOffValue in newValues)
            {
                cutOffValue.CutoffId = cutoff.Id;
                _context.CutoffValues.Add(cutOffValue);
            }
            foreach (var cutOffValue in valuesToDelete)
            {
                _context.CutoffValues.Remove(cutOffValue);
            }
            foreach (var cutOffValue in editedValues)
            {
                var edited = cutoff.cutOffValues.Where(c => c.Id == cutOffValue.Id).First();
                cutOffValue.Number = edited.Number;
                cutOffValue.Value = edited.Value;
                _context.Entry(cutOffValue).State = EntityState.Modified;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CutoffExists(id))
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

        // POST: api/Cutoffs
        [HttpPost]
        public async Task<ActionResult<Cutoff>> PostCutoff(Cutoff cutoff)
        {
            _context.Cutoffs.Add(cutoff);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCutoff", new { id = cutoff.Id }, cutoff);
        }

        // DELETE: api/Cutoffs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCutoff(int id)
        {
            var cutoff = _context.Cutoffs.Include(c => c.cutOffValues).Where(c => c.Id == id).FirstOrDefault();

            if (cutoff == null)
            {
                return NotFound();
            }          

            _context.Cutoffs.Remove(cutoff);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CutoffExists(int id)
        {
            return _context.Cutoffs.Any(e => e.Id == id);
        }
    }
}
