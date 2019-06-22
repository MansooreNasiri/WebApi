using Store.Models.Models;
using Store.Services.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Store.Services.Controllers
{
    public class ProductController : ApiController
    {
        private StoreDbContext _db = new StoreDbContext();

     
        [HttpGet]
        public IHttpActionResult GetAllProduct()
        {
            var products = _db.Product.AsNoTracking()
                .Select(p => new Products
                {
                    Id = p.Id,
                    BrandName = p.BrandName,
                    ModelNo = p.ModelNo,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    CompanyId = p.CompanyId,
                    CompanyName = p.Company.Name,
                    Description = p.Description,
                    
                }).ToList();

           
            return Ok(products);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Products))]
        [HttpGet]
        public IHttpActionResult GetProduct([FromUri]int id)
        {
            var product = _db.Product.
                Where(p => p.Id == id)
                .Select(p => new
                {
                    Id = p.Id,
                    BrandName = p.BrandName,
                    ModelNo = p.ModelNo,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    CompanyId = p.CompanyId,
                    CompanyName = p.Company.Name,
                    Description = p.Description,

                }).SingleOrDefault();

            if (product == null)
            {
                return NotFound();
            }


            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult EditProduct(int id, Products product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            _db.Entry(product).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Products))]
        [HttpPost]
        public IHttpActionResult AddProduct([FromBody]Products product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Product.Add(product);
            _db.SaveChanges();

            return Ok("محصول جدید با موفقت ثبت شد");
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Products))]
        [HttpDelete]
        public IHttpActionResult DeleteProduct([FromUri]int id)
        {
            var product = _db.Product.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _db.Product.Remove(product);
            _db.SaveChanges();

            return Ok("محصول با موفقت حذف شد");
        }



        [HttpGet]
        public IHttpActionResult GetCountries()
        {
            var country = _db.Countries.AsNoTracking()
                        .Select(c => new Country
                        {
                            Id = c.Id,
                            Name = c.Name

                        }).ToList();

            return Ok(country);
        }

        [HttpGet]
        public IHttpActionResult GetCategories()
        {

            var categories = _db.Categories.AsNoTracking()
                        .Select(c => new Category
                        {
                            Id = c.Id,
                            Name = c.Name

                        }).ToList();

            return Ok(categories);
        }

        [HttpGet]
        public IHttpActionResult GetComponies()
        {
            var companies = _db.Companies.AsNoTracking()
                       .Select(c => new Company
                       {
                           Id = c.Id,
                           Name = c.Name

                       }).ToList();

            return Ok(companies);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return _db.Product.Count(e => e.Id == id) > 0;
        }
    }
}
