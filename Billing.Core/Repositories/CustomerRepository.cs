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
    public class CustomerRepository(ILogger logger, ILoggerFactory loggerF,
        BillingContext db) : BaseRepository(logger, loggerF, db)
    {
        public IEnumerable<Customer> GetAll() => _context.Customers.AsEnumerable();

        public Customer GetById(string id) {
            var rows = _context.Customers.AsNoTracking();
            //var keyValue = Convert.FromBase64String(id);
            //return rows.FirstOrDefault(p => p.Id.SequenceEqual<byte>(keyValue));
            return rows.FirstOrDefault(p => p.Id.Equals(id));
        }

        public CustomerDTO New(CustomerDTO c)
        {            
            if (c != null)
            {
                #region Field Validation

                ValidateFields(c);

                #endregion
                                
                if (c.Active == 0)
                {
                    c.Active = 1;
                }

                var newTuple = ToModel(c);
                newTuple.Id = CustomConverter.NewGuid();
                _context.Customers.Add(newTuple);
                _context.SaveChanges();
                var resp = ToDTO<CustomerDTO>(newTuple);
                return resp;
            }

            return null;
        }
        
        public CustomerDTO Update(CustomerDTO c)
        {
            if (c != null)
            {
                //var tuple = GetById(CustomConverter.DecodeB64FromHex(c.Id));
                var tuple = GetById(c.Id);
                if (tuple != null)
                {
                    if (c.Name != null)
                    {
                        tuple.Name = c.Name;
                    }
                    if (c.Email != null)
                    {
                        tuple.Email = c.Email;
                    }
                    if (c.Address != null)
                    {
                        tuple.Address = c.Address;
                    }
                    if (c.Active != null)
                    {
                        tuple.Active = c.Active.Value;
                    }

                    _context.Customers.Update(tuple);
                    _context.SaveChanges();
                    var resp = ToDTO<CustomerDTO>(tuple);
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
                _context.Customers.Remove(tuple);
                _context.SaveChanges();
                return true;                
            }

            return false;
        }

        private static void ValidateFields(CustomerDTO tuple)
        {
            if (string.IsNullOrWhiteSpace(tuple.Name))
                throw new Exception("Preencha o Nome. Campo requerido!");

            if (string.IsNullOrWhiteSpace(tuple.Email))
                throw new Exception("Preencha o Email. Campo requerido!");
            
            if (string.IsNullOrWhiteSpace(tuple.Address))
                throw new Exception("Preencha o Fone1. Campo requerido!");
        }

        #region DOING MAPPER THINGS !!!

        public static Customer ToModel<T>(T origin)
        {
            var mapper = new Mapper(new MapperConfiguration(c =>
               c.CreateMap<CustomerDTO, Customer>(), _loggerF));
            var newitem = mapper.Map<T, Customer>(origin);
            return newitem;
        }

        public static T ToDTO<T>(Customer origin)
        {
            var mapper = new Mapper(new MapperConfiguration(c =>
                c.CreateMap<Customer, CustomerDTO>(), _loggerF));
            var newitem = mapper.Map<Customer, T>(origin);
            return newitem;
        }

        #endregion
    }
}
