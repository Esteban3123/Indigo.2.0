using Application.Events.Models;
using Application.Events.Models.Supplier;
using System;

namespace Application.Events.Serializers
{
    public class Supplier : IDittoDocument
    {
        /// <summary>
        /// Return JSON Type : Supplier
        /// </summary>
        /// <param name="supplierEntity"></param>
        /// <returns></returns>
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.Supplier supplierEntity = obj as Domain.Entities.Supplier;

            MSupplier supplier = new MSupplier();
            MethodsSupplier methodsSupplier = new MethodsSupplier();
            supplier.ThirdParty = methodsSupplier.GetThirdParty(supplierEntity.ThirdParty);
            supplier.Code = supplierEntity.Code;
            supplier.Name = supplierEntity.Name;
            supplier.CodeCMMS = supplierEntity.CodeCMMS;
            supplier.WebSite = supplierEntity.WebSite;
            supplier.City = methodsSupplier.getCodeCity(supplierEntity.IdCity);
            supplier.PermanentRetention = Convert.ToInt16(supplierEntity.PermanentRetention);
            supplier.TimeLimitDays = supplierEntity.TimeLimitDays;
            supplier.NotIva = Convert.ToInt16(supplierEntity.NotIva);
            supplier.Declarant = Convert.ToInt16(supplierEntity.Declarant);
            supplier.IndependentEmployee = Convert.ToInt16(supplierEntity.IndependentEmployee);
            supplier.PrioritizeBankAccount = Convert.ToInt16(supplierEntity.PrioritizeBankAccount);
            supplier.TaxCategory = Convert.ToInt16(supplierEntity.TaxCategory);
            supplier.Status = Convert.ToInt16(supplierEntity.Status);
            supplier.CreationUser = supplierEntity.CreationUser;
            supplier.CreationDate = Convert.ToString(supplierEntity.CreationDate);
            supplier.ModificationUser = supplierEntity.ModificationUser;
            supplier.ModificationDate = Convert.ToString(supplierEntity.ModificationDate);
            supplier.WorkRent = Convert.ToInt16(supplierEntity.WorkRent);
            supplier.Pensions = Convert.ToInt16(supplierEntity.Pensions);
            supplier.CapitalRent = Convert.ToInt16(supplierEntity.CapitalRent);
            supplier.NotLaborRent = Convert.ToInt16(supplierEntity.NotLaborRent);
            supplier.DividendsAndParticipations = Convert.ToInt16(supplierEntity.DividendsAndParticipations);
            supplier.Manufacturer = Convert.ToInt16(supplierEntity.Manufacturer);
            supplier.Seller = Convert.ToInt16(supplierEntity.Seller);
            supplier.SelfWithholding = Convert.ToInt16(supplierEntity.SelfWithholding);
            supplier.SelfWithholdingICA = Convert.ToInt16(supplierEntity.SelfWithholdingICA);
            supplier.SupplierBankAccount = methodsSupplier.getsupplierBankAccounts(supplierEntity);
            supplier.SupplierDetailType = methodsSupplier.getSupplierType(supplierEntity);
            supplier.SuppliersDistributionLines = methodsSupplier.getSuppliersDistributionLines(supplierEntity);
            return supplier;
        }
    }
}
