using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Events.Models;
using Application.Events.Models.InvoiceEntityCapitated;

namespace Application.Events.Serializers
{
    public class InvoiceCapitatedEvent : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.InvoiceEntityCapitated invoiceEntityCapitated = obj as Domain.Entities.InvoiceEntityCapitated;

            MInvoiceCapitated mInvoiceCapitated = new MInvoiceCapitated();
            MethodsInvoiceCapitated methodsInvoiceCapitated = new MethodsInvoiceCapitated();
            
            mInvoiceCapitated.Code = invoiceEntityCapitated.Code;
            mInvoiceCapitated.OperatingUnit = methodsInvoiceCapitated.GetOperatingUnit(invoiceEntityCapitated.OperatingUnitId);
            mInvoiceCapitated.DocumentDate = Convert.ToString(invoiceEntityCapitated.DocumentDate);
            mInvoiceCapitated.CareGroup = methodsInvoiceCapitated.GetCareGroup(invoiceEntityCapitated.CareGroupId);
            mInvoiceCapitated.InitialDate = invoiceEntityCapitated.InitialDate is null ? string.Empty : Convert.ToString(invoiceEntityCapitated.InitialDate);
            mInvoiceCapitated.EndDate = invoiceEntityCapitated.EndDate is null ? string.Empty : Convert.ToString(invoiceEntityCapitated.EndDate);
            mInvoiceCapitated.UserNumber = invoiceEntityCapitated.UserNumber;
            mInvoiceCapitated.UserValue = invoiceEntityCapitated.UserValue;
            mInvoiceCapitated.InvoiceCategory = methodsInvoiceCapitated.GetInvoiceCategory(invoiceEntityCapitated.InvoiceCategoryId);
            mInvoiceCapitated.InvoiceNumber = methodsInvoiceCapitated.GetInvoiceNumber(invoiceEntityCapitated.InvoiceId);
            mInvoiceCapitated.Status = invoiceEntityCapitated.Status;
            mInvoiceCapitated.DiscountPercentage = invoiceEntityCapitated.DiscountPercentage;
            mInvoiceCapitated.DiscountValue = invoiceEntityCapitated.DiscountValue;
            mInvoiceCapitated.TotalValue = invoiceEntityCapitated.TotalValue;
            mInvoiceCapitated.InvoicePeriod = invoiceEntityCapitated.InvoicePeriod;
            mInvoiceCapitated.PreviousRIPSInvoice = methodsInvoiceCapitated.GetPreviousRIPSInvoice(invoiceEntityCapitated.PreviousRIPSInvoice);
            mInvoiceCapitated.CreationUser = invoiceEntityCapitated.CreationUser;
            mInvoiceCapitated.CreationDate = Convert.ToString(invoiceEntityCapitated.CreationDate);
            mInvoiceCapitated.ModificationUser = invoiceEntityCapitated.ModificationUser;
            mInvoiceCapitated.ModificationDate = invoiceEntityCapitated.ModificationDate is null ? string.Empty : Convert.ToString(invoiceEntityCapitated.ModificationDate);
            return mInvoiceCapitated;
        }
    }
}
