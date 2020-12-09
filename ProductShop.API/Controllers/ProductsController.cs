using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductShop.API.Data.Repositories;
using ProductShop.API.Domain;
using ProductShop.API.Dtos;
using System;
using Microsoft.AspNetCore.JsonPatch;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace ProductShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepo _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IProductRepo repository, IMapper mapper, IWebHostEnvironment environment)
        {
            _repository = repository;
            _mapper = mapper;
            _environment = environment;
        }


        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAllProducts()
        {
            var productItems = _repository.GetProducts();

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(productItems));
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public ActionResult<ProductDto> GetProductById(int id)
        {

            var productItem = _repository.GetProductById(id);

            if (productItem != null)
            {
                return Ok(_mapper.Map<ProductDto>(productItem));

            }
            return NotFound();

        }
        //POST: api/product
        [HttpPost]
        public ActionResult<ProductDto> CreateProduct([FromForm] ProductDto productCreateDto)
        {
            var file = HttpContext.Request.Form.Files[0];

            if (file.Length == 0)
                return BadRequest();

            var path = _environment.WebRootPath + "\\Uploads\\";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileExtention = (Path.GetExtension(file.FileName)).ToLower();
            string fileName = Guid.NewGuid().ToString();
            fileName = fileName + fileExtention;
            using (var stream = System.IO.File.Create(path + fileName))
            {
                file.CopyTo(stream);
                stream.Flush();
            }
            productCreateDto.Image = Path.Combine(fileName);
            var productModel = _mapper.Map<Product>(productCreateDto);
            _repository.CreateProduct(productModel);
            _repository.SaveChanges();
            var productReadDto = _mapper.Map<ProductDto>(productModel);

            return CreatedAtRoute(nameof(GetProductById), new { productReadDto.Id }, productReadDto);

        }

        //PUT: api/product/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct( int id, [FromForm] ProductUpdateDto updateProductDto)
        {
            var productInDb = _repository.GetProductById(id);
            if (productInDb == null)
            {
                return NotFound();
            }
            var file = HttpContext.Request.Form.Files[0];

            if (file.Length == 0)
                return BadRequest();

            var path = _environment.WebRootPath + "\\Uploads\\";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileExtention = (Path.GetExtension(file.FileName)).ToLower();
            string fileName = Guid.NewGuid().ToString();
            fileName = fileName + fileExtention;
            using (var stream = System.IO.File.Create(path + fileName))
            {
                file.CopyTo(stream);
                stream.Flush();
            }
            updateProductDto.Image = Path.Combine(fileName);
            _mapper.Map(updateProductDto, productInDb);
            _repository.UpdateProduct(productInDb);
            _repository.SaveChanges();

            return NoContent();

        }
        
        //Delete: api/product/{id}
        [HttpDelete("{id}")]

        public ActionResult DeleteProduct(int id)
        {
            var productInDb = _repository.GetProductById(id);
            if (productInDb == null)
            {
                return NotFound();
            }
            _repository.DeleteProduct(productInDb);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
