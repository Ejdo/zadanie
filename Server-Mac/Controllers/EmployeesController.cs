using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            return await _context.Employees
                .Where(employee => employee.isActive)
                .Include(employee => employee.EmployeePositions).ThenInclude(employeePosition => employeePosition.Position)
                .ToListAsync();
        }
        
        [Route("former")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetFormerEmployees()
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            return await _context.Employees
                .Where(employee => !employee.isActive)
                .Include(employee => employee.EmployeePositions).ThenInclude(employeePosition => employeePosition.Position)
                .ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employee = await _context.Employees
                .Where(employee => employee.Id == id)
                .Include(employee => employee.EmployeePositions).ThenInclude(employeePosition => employeePosition.Position)
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> ArchiveEmployee(int id, bool isActive )
        {

            _context.Employees.Find(id).isActive = isActive;
            System.Diagnostics.Debug.WriteLine(isActive);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if(employee.EmployeePositions.Last().Position.Id != employee.PositionId)
            {
                employee.EmployeePositions.Last().endDate = DateTime.Today.ToString("yyyy/MM/dd");
                EmployeePosition newPosiiton = new EmployeePosition();
                newPosiiton.Position = _context.Positions.Find(employee.PositionId);
                newPosiiton.startDate = DateTime.Today.ToString("yyyy/MM/dd");
                employee.EmployeePositions.Add(newPosiiton);
            }

            _context.Employees.Update(employee);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
          if (_context.Employees == null)
          {
              return Problem("Entity set 'AppDbContext.Employees'  is null.");
          }
            
            EmployeePosition position = new EmployeePosition();
            position.Id = employee.Id;
            position.Position = _context.Positions.Find(employee.PositionId);
            position.startDate = DateTime.Today.ToString("yyyy/MM/dd");
            employee.EmployeePositions.Add(position);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(long id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
