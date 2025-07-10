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
    public class InvoiceLineRepository : BaseRepository
    {
        public InvoiceLineRepository(ILogger logger, ILoggerFactory loggerF,
            BillingContext db) : base(logger, loggerF, db)
        {
        }
        public IEnumerable<InvoiceLine> GetAll() => _context.InvoiceLines.AsNoTracking().AsEnumerable();

        public InvoiceLine GetById(long id) {
            var rows = _context.InvoiceLines.AsNoTracking();
            //var keyValue = Convert.FromBase64String(id);
            //return rows.FirstOrDefault(p => p.Id.SequenceEqual<byte>(keyValue));
            return rows.FirstOrDefault(p => p.Id.Equals(id));
        }

        public IEnumerable<InvoiceLine> GetByInvoiceId(long invoiceId)
        {
            return GetAll().Where(p => p.InvoiceId.Equals(invoiceId));
        }

        public invoiceLineDTO New(invoiceLineDTO c)
        {            
            if (c != null)
            {
                #region Field Validation

                ValidateFields(c);

                #endregion

                var newTuple = ToModel(c);
                _context.InvoiceLines.Add(newTuple);
                _context.SaveChanges();
                var resp = ToDTO<invoiceLineDTO>(newTuple);
                return resp;
            }

            return null;
        }
        
        public invoiceLineDTO Update(invoiceLineDTO c)
        {
            if (c != null)
            {
                var tuple = GetById(c.Id);
                if (tuple != null)
                {
                    if (c.ProductId != null)
                    {
                        tuple.ProductId = c.ProductId;
                    }

                    if (c.Quantity != null)
                    {
                        tuple.Quantity = c.Quantity.Value;
                    }

                    if (c.UnitPrice != null)
                    {
                        tuple.UnitPrice = c.UnitPrice.Value;
                    }

                    if (c.SubTotal != null)
                    {
                        tuple.SubTotal = c.SubTotal.Value;
                    }

                    _context.InvoiceLines.Update(tuple);
                    _context.SaveChanges();
                    var resp = ToDTO<invoiceLineDTO>( tuple);
                    return resp;
                }
            }

            return null;
        }

        public bool Delete(long id)
        {
            var tuple = GetById(id);
            if (tuple != null)
            {
                _context.InvoiceLines.Remove(tuple);
                _context.SaveChanges();
                return true;                
            }

            return false;
        }

        private void ValidateFields(invoiceLineDTO tuple)
        {
            //if (string.IsNullOrWhiteSpace(tuple.InvoiceLineName))
            //    throw new Exception("Preencha o Nome do InvoiceLineo. Campo requerido!");
        }

        #region DOING MAPPER THINGS !!!

        public static InvoiceLine ToModel<T>(T origin)
        {
            var mapper = new Mapper(new MapperConfiguration(c => c.CreateMap<invoiceLineDTO, InvoiceLine>(), _loggerF));
            var newitem = mapper.Map<T, InvoiceLine>(origin);
            return newitem;
        }

        public static T ToDTO<T>(InvoiceLine origin)
        {
            var mapper = new Mapper(new MapperConfiguration(c => c.CreateMap<InvoiceLine, invoiceLineDTO>(), _loggerF));
            var newitem = mapper.Map<InvoiceLine, T>(origin);
            return newitem;
        }

        #endregion
    }
}
