using Application.Events.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Events.Models.InvoiceEntityCapitated
{
    public class MethodsInvoiceCapitated
    {

        public OperatingUnit GetOperatingUnit(int? operatingUnitId)
        {
            if (operatingUnitId is null)
                return null;

            OperatingUnit operatingUnit = new OperatingUnit();

            using (var execute = new ExecuteCommand())
            {
                string query = "SELECT TOP 1 UnitCode,UnitName FROM Common.OperatingUnit WITH(NOLOCK) WHERE Id =  @OperatingUnitId";
                System.Data.DataTable dtOperatingUnit = execute.GeneralExecuteQuerySqlCommand(query, "OperatingUnitId", operatingUnitId);
                if (dtOperatingUnit is null || dtOperatingUnit.Rows.Count == 0) { return null; }
                operatingUnit.UnitCode = Convert.ToString(dtOperatingUnit.Rows[0]["UnitCode"]);
                operatingUnit.UnitName = Convert.ToString(dtOperatingUnit.Rows[0]["UnitName"]);
                return operatingUnit;
            }  
        }


        public CareGroup GetCareGroup(int? careGroupId)
        {
            if (careGroupId is null)
                return null;

            CareGroup careGroup = new CareGroup();

            using (var execute = new ExecuteCommand())
            {
                string query = "SELECT TOP 1 Code,Name,LiquidationType FROM Contract.CareGroup WITH(NOLOCK) WHERE Id =  @CareGroupId";
                System.Data.DataTable dtCareGroupId = execute.GeneralExecuteQuerySqlCommand(query, "CareGroupId", careGroupId);
                if (dtCareGroupId is null || dtCareGroupId.Rows.Count == 0) { return null; }
                careGroup.Code = Convert.ToString(dtCareGroupId.Rows[0]["Code"]);
                careGroup.Name = Convert.ToString(dtCareGroupId.Rows[0]["Name"]);
                careGroup.LiquidationType = Convert.ToByte(dtCareGroupId.Rows[0]["LiquidationType"]);
                return careGroup;
            }
        }

        public InvoiceCategory GetInvoiceCategory(int? invoiceCategoryId)
        {
            if (invoiceCategoryId is null)
                return null;

            InvoiceCategory invoiceCategory = new InvoiceCategory();

            using (var execute = new ExecuteCommand())
            {
                string query = "SELECT TOP 1 Code,Name FROM Billing.InvoiceCategories WITH(NOLOCK) WHERE Id =  @InvoiceCategoryId";
                System.Data.DataTable dtInvoiceCategory = execute.GeneralExecuteQuerySqlCommand(query, "InvoiceCategoryId", invoiceCategoryId);
                if (dtInvoiceCategory is null || dtInvoiceCategory.Rows.Count == 0) { return null; }
                invoiceCategory.Code = Convert.ToString(dtInvoiceCategory.Rows[0]["Code"]);
                invoiceCategory.Name = Convert.ToString(dtInvoiceCategory.Rows[0]["Name"]);
                return invoiceCategory;
            }
        }


        public string GetInvoiceNumber(int? invoiceId)
        {
            if (invoiceId is null)
                return string.Empty;

            using (var execute = new ExecuteCommand())
            {
                string query = "SELECT TOP 1 InvoiceNumber FROM Billing.Invoice WITH(NOLOCK) WHERE Id = @InvoiceId";
                System.Data.DataTable dtInvoice = execute.GeneralExecuteQuerySqlCommand(query, "InvoiceId", invoiceId);
                if (dtInvoice is null || dtInvoice.Rows.Count == 0) { return string.Empty; }
                return Convert.ToString(dtInvoice.Rows[0]["InvoiceNumber"]);
            }
        }

        public PreviousRIPSInvoice GetPreviousRIPSInvoice(int? previousRIPSInvoiceId)
        {
            if (previousRIPSInvoiceId is null)
                return null;

            PreviousRIPSInvoice previousRIPSInvoice = new PreviousRIPSInvoice();

            using (var execute = new ExecuteCommand())
            {
                string query = "SELECT TOP 1 i.InvoiceNumber,iec.InitialDate,iec.EndDate,iec.InvoicePeriod " +
                                "from Billing.InvoiceEntityCapitated iec WITH(NOLOCK) " +
                                "JOIN Billing.Invoice i WITH(NOLOCK) on iec.InvoiceId = i.Id " +
                                "where iec.Id = @PreviousRIPSInvoiceId";
                System.Data.DataTable dtPreviousRIPSInvoice = execute.GeneralExecuteQuerySqlCommand(query, "PreviousRIPSInvoiceId", previousRIPSInvoiceId);
                if (dtPreviousRIPSInvoice is null || dtPreviousRIPSInvoice.Rows.Count == 0) { return null; }
                previousRIPSInvoice.InvoiceNumber = Convert.ToString(dtPreviousRIPSInvoice.Rows[0]["InvoiceNumber"]);
                previousRIPSInvoice.InitialDate = Convert.ToString(dtPreviousRIPSInvoice.Rows[0]["InitialDate"]);
                previousRIPSInvoice.EndDate = Convert.ToString(dtPreviousRIPSInvoice.Rows[0]["EndDate"]);
                previousRIPSInvoice.InvoicePeriod = Convert.ToByte(dtPreviousRIPSInvoice.Rows[0]["InvoicePeriod"]);
                return previousRIPSInvoice;
            }
        }



    }
}
