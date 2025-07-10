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
    public class InvoiceRepository : BaseRepository
    {
        public InvoiceRepository(ILogger logger, ILoggerFactory loggerF,
            BillingContext db) : base(logger, loggerF, db)
        {
        }

        public IEnumerable<Invoice> GetAll() => _context.Invoices.AsNoTracking().AsEnumerable();

        public Invoice GetById(long id) {
            var rows = _context.Invoices.AsNoTracking();
            //var keyValue = Convert.FromBase64String(id);
            //return rows.FirstOrDefault(p => p.Id.SequenceEqual<byte>(keyValue));
            return rows.FirstOrDefault(p => p.Id.Equals(id));
        }

        public InvoiceDTO New(InvoiceDTO c)
        {            
            if (c != null)
            {
                #region Field Validation

                ValidateFields(c);

                #endregion
                                
                if (c.Active == 0) {
                    c.Active = 1;
                }

                var newTuple = ToModel(c);
                _context.Invoices.Add(newTuple);
                _context.SaveChanges();

                var resp = ToDTO<InvoiceDTO>(newTuple);
                return resp;
            }

            return null;
        }
        
        public InvoiceDTO Update(InvoiceDTO c)
        {
            if (c != null)
            {
                var tuple = GetById(c.Id);
                if (tuple != null)
                {
                    if (c.CustomerId != null)
                    {
                        tuple.CustomerId = c.CustomerId;
                    }

                    if (c.Name != null)
                    {
                        tuple.Name = c.Name;
                    }

                    if (c.InvoiceNumber != null)
                    {
                        tuple.InvoiceNumber = c.InvoiceNumber;
                    }

                    if (c.CreatedAt != null)
                    {
                        tuple.CreatedAt = c.CreatedAt.Value;
                    }

                    if (c.Date != null)
                    {
                        tuple.Date = c.Date.Value;
                    }

                    if (c.DueDate != null)
                    {
                        tuple.DueDate = c.DueDate.Value;
                    }

                    if (c.InvoiceDate != null)
                    {
                        tuple.InvoiceDate = c.InvoiceDate.Value;
                    }

                    if (c.InvoiceAmount != null)
                    {
                        tuple.InvoiceAmount = c.InvoiceAmount.Value;
                    }

                    if (c.TotalAmount != null)
                    {
                        tuple.TotalAmount = c.TotalAmount.Value;
                    }

                    if (c.BillingLines != null)
                    {
                        tuple.BillingLines = c.BillingLines;
                    }

                    if (c.CurrencyCode != null)
                    {
                        tuple.CurrencyCode = c.CurrencyCode;
                    }

                    if (c.Currency != null)
                    {
                        tuple.Currency = c.Currency;
                    }

                    if (c.Active != null)
                    {
                        tuple.Active = c.Active.Value;
                    }

                    _context.Invoices.Update(tuple);
                    _context.SaveChanges();
                    var resp = ToDTO<InvoiceDTO>(tuple);

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
                _context.Invoices.Remove(tuple);
                _context.SaveChanges();
                return true;                
            }

            return false;
        }

        private void ValidateFields(InvoiceDTO tuple)
        {
            //if (string.IsNullOrWhiteSpace(tuple.Invoice))
            //    throw new Exception("Preencha o Nome do Invoiceo. Campo requerido!");
        }

        #region DOING MAPPER THINGS !!!

        public static Invoice ToModel<T>(T origin)
        {
            var mapper = new Mapper(new MapperConfiguration(c =>
               c.CreateMap<InvoiceDTO, Invoice>(), _loggerF));
            var newitem = mapper.Map<T, Invoice>(origin);
            return newitem;
        }

        public static T ToDTO<T>(Invoice origin)
        {
            var mapper = new Mapper(new MapperConfiguration(c =>
            c.CreateMap<Invoice, InvoiceDTO>(), _loggerF));
            var newitem = mapper.Map<Invoice, T>(origin);
            return newitem;
        }

        #endregion
    }
}
