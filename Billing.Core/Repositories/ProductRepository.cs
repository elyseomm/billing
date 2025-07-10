using AutoMapper;
using Billing.Core.DTO;
using Billing.Core.Models;
using Billing.Core.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Billing.Core.Repositories
{
    public class ProductRepository : BaseRepository
    {
        #region MAPEAMENTOS ENTITY / DTO

        private readonly MapperConfiguration toDTO = new MapperConfiguration(c => 
            c.CreateMap<Product, ProductDTO>(), _loggerF);

        private readonly MapperConfiguration toModel = new MapperConfiguration(c =>
               c.CreateMap<ProductDTO, Product>(), _loggerF);

        #endregion
        public ProductRepository(ILogger logger, ILoggerFactory loggerF,
            BillingContext db) : base(logger, loggerF, db)
        {
        }

        public IEnumerable<Product> GetAll() => _context.Products.AsNoTracking().AsEnumerable();

        public Product GetById(string id) {
            var rows = _context.Products.AsNoTracking();
            //var keyValue = Convert.FromBase64String(id);
            //return rows.FirstOrDefault(p => p.Id.SequenceEqual<byte>(keyValue));
            return rows.FirstOrDefault(p => p.Id.Equals(id));
        }

        public ProductDTO New(ProductDTO c)
        {            
            if (c != null)
            {
                #region Field Validation

                ValidateFields(c);

                #endregion
                                
                if (c.Active == 0) {
                    c.Active = 1;
                }

                var newTuple = ToModel(toModel, c);
                newTuple.Id = CustomConverter.NewGuid();
                _context.Products.Add(newTuple);
                _context.SaveChanges();
                var resp = new ProductDTO()
                {
                    Id = newTuple.Id,
                    ProductName = newTuple.ProductName,
                    Active = newTuple.Active
                };

                return resp;
            }

            return null;
        }
        
        public ProductDTO Update(ProductDTO c)
        {
            if (c != null)
            {
                var tuple = GetById(c.Id);
                if (tuple != null)
                {
                    if (c.ProductName != null)
                    {
                        tuple.ProductName = c.ProductName;
                    }

                    if (c.Active != null)
                    {
                        tuple.Active = c.Active.Value;
                    }

                    _context.Products.Update(tuple);
                    _context.SaveChanges();
                    var resp = new ProductDTO()
                    {
                        //Id = CustomConverter.ToHexaStr(tuple.Id),
                        Id = tuple.Id,
                        ProductName = tuple.ProductName,
                        Active = tuple.Active
                    };

                    return resp;
                }
            }

            return null;
        }

        public bool Delete(string id)
        {
            var tuple = GetById(id);
            if (tuple != null)
            {
                _context.Products.Remove(tuple);
                _context.SaveChanges();
                return true;                
            }

            return false;
        }

        private void ValidateFields(ProductDTO tuple)
        {
            if (string.IsNullOrWhiteSpace(tuple.ProductName))
                throw new Exception("Preencha o Nome do Producto. Campo requerido!");
        }

        #region DOING MAPPER THINGS !!!

        private static Product ToModel<T>(MapperConfiguration cfg, T origin)
        {
            var mapper = new Mapper(cfg);
            var newitem = mapper.Map<T, Product>(origin);
            return newitem;
        }

        private static T ToDTO<T>(MapperConfiguration cfg, Product origin)
        {
            var mapper = new Mapper(cfg);
            var newitem = mapper.Map<Product, T>(origin);
            return newitem;
        }

        #endregion
    }
}
