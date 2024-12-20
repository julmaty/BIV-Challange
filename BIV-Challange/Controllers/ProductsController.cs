﻿
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
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ProductsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductListViewModel>>> GetProducts()
        {
            var products = _context.Products.Select(p => new ProductListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.Category
                }).ToList();
            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = _context.Products.Include(p => p.CutoffsForProduct).Include(p => p.TablesForParam).Where(p => p.Id == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Models.Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var productInDb = _context.Products.Include(p => p.CutoffsForProduct).Include(p => p.TablesForParam).Where(p => p.Id == id).FirstOrDefault();
            productInDb.Name = product.Name;
            productInDb.Category = product.Category;
            productInDb.OblFields = product.OblFields;

            var user = HttpContext.User.Identity;
            if (user is not null && user.IsAuthenticated)
            {
                var userId = _context.Users.Where(u => u.Email == user.Name).Select(u => u.Id).FirstOrDefault();
                productInDb.UserUpdated = userId;
            }
            else
            {
                product.UserUpdated = "000";
            }


            _context.Entry(productInDb).State = EntityState.Modified;

            var newValuesIds = product.CutoffsForProduct.Select(v => v.Number).Except(productInDb.CutoffsForProduct.Select(v => v.Number));
            var newValues = product.CutoffsForProduct.Where(c => newValuesIds.Contains(c.Number)).ToList();
            var valuesToDeleteIds = productInDb.CutoffsForProduct.Select(v => v.Number).Except(product.CutoffsForProduct.Select(v => v.Number));
            var valuesToDelete = productInDb.CutoffsForProduct.Where(c => valuesToDeleteIds.Contains(c.Number)).ToList();
            var editedValuesIds = product.CutoffsForProduct.Select(v => v.Number).Intersect(productInDb.CutoffsForProduct.Select(v => v.Number));
            var editedValues = productInDb.CutoffsForProduct.Where(c => editedValuesIds.Contains(c.Number)).ToList();

            foreach (var cutOffValue in newValues)
            {
                cutOffValue.CutoffId = product.Id;
                _context.CutoffsForProduct.Add(cutOffValue);
            }
            foreach (var cutOffValue in valuesToDelete)
            {
                _context.CutoffsForProduct.Remove(cutOffValue);
            }
            foreach (var cutOffValue in editedValues)
            {
                var edited =product.CutoffsForProduct.Where(c => c.Number == cutOffValue.Number).First();
                cutOffValue.Number = edited.Number;
                cutOffValue.Value = edited.Value;
                cutOffValue.CutoffId = edited.CutoffId;
                _context.Entry(cutOffValue).State = EntityState.Modified;
            }

            newValuesIds = product.TablesForParam.Select(v => v.Number).Except(productInDb.TablesForParam.Select(v => v.Number));
            var newValuesTab = product.TablesForParam.Where(c => newValuesIds.Contains(c.Number)).ToList();
            valuesToDeleteIds = productInDb.TablesForParam.Select(v => v.Number).Except(product.TablesForParam.Select(v => v.Number));
            var valuesToDeleteTab = productInDb.TablesForParam.Where(c => valuesToDeleteIds.Contains(c.Number)).ToList();
            editedValuesIds = product.TablesForParam.Select(v => v.Number).Intersect(productInDb.TablesForParam.Select(v => v.Number));
            var editedValuesTab = productInDb.TablesForParam.Where(c => editedValuesIds.Contains(c.Number)).ToList();

            foreach (var table in newValuesTab)
            {
                table.ProductId = product.Id;
                _context.TablesForParams.Add(table);
            }
            foreach (var table in valuesToDeleteTab)
            {
                _context.TablesForParams.Remove(table);
            }
            foreach (var table in editedValuesTab)
            {
                var edited = product.TablesForParam.Where(c => c.Number == table.Number).First();
                table.Number = edited.Number;
                table.Value = edited.Value;
                table.CutoffForProductNumbers = edited.CutoffForProductNumbers;
                _context.Entry(table).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Models.Product>> PostProduct(Models.Product product)
        {
            var user = HttpContext.User.Identity;
            if (user is not null && user.IsAuthenticated)
            {
                var userId = _context.Users.Where(u => u.Email == user.Name).Select(u => u.Id).FirstOrDefault();
                product.UserCreated = userId;
                product.UserUpdated = userId;
            }
            else
            {
                product.UserCreated = "000";
                product.UserUpdated = "000";
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = _context.Products.Include(p => p.CutoffsForProduct).Include(p => p.TablesForParam).Where(p => p.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
