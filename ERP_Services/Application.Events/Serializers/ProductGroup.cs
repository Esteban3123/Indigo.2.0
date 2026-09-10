using Application.Events.Models;
using Application.Events.Models.ProductGroup;
using System;

namespace Application.Events.Serializers
{
    public class ProductGroup : IDittoDocument
    {
        public object Generate(object obj, QueueParameter parameter)
        {
            Domain.Entities.ProductGroup productGroup =obj as Domain.Entities.ProductGroup;

            MProductGroup mProductGroup = new MProductGroup();
            MethodsProductGroup methodsProductGroup = new MethodsProductGroup();
            mProductGroup.Code = productGroup.Code;
            mProductGroup.Name = productGroup.Name;
            mProductGroup.GroupClass = productGroup.GroupClass;
            mProductGroup.SubclassCode = productGroup.SubclassCode;
            mProductGroup.IncomeAccount = methodsProductGroup.GetGeneralAccount(productGroup.IncomeAccountId);
            mProductGroup.InventoryAccountPayableConcept = methodsProductGroup.GetInventoryAccountPayableConcept(productGroup.InventoryAccountPayableConceptId);
            mProductGroup.DeclarantRetentionAccountPayableConcept = methodsProductGroup.GetInventoryAccountPayableConcept(productGroup.DeclarantRetentionAccountPayableConceptId);
            mProductGroup.NotDeclarantRetentionAccountPayableConcept = methodsProductGroup.GetNotDeclarantRetentionAccountPayableConcept(productGroup.NotDeclarantRetentionAccountPayableConceptId);
            mProductGroup.CostCenter = methodsProductGroup.GetCostCenter(productGroup.CostCenterId);
            mProductGroup.ReferenceInputDebitAccount = methodsProductGroup.GetGeneralAccount(productGroup.ReferenceInputDebitAccountId);
            mProductGroup.ReferenceInputCreditAccount = methodsProductGroup.GetGeneralAccount(productGroup.ReferenceInputCreditAccountId);
            mProductGroup.ReferenceOutputDebitAccount = methodsProductGroup.GetGeneralAccount(productGroup.ReferenceOutputDebitAccountId);
            mProductGroup.ReferenceOutputCreditAccount = methodsProductGroup.GetGeneralAccount(productGroup.ReferenceOutputCreditAccountId);
            mProductGroup.ExcludeFreightCosts = Convert.ToInt32(productGroup.ExcludeFreightCosts);
            mProductGroup.ProductReplacementTime = productGroup.ProductReplacementTime;
            mProductGroup.ProductsSourcingTime = productGroup.ProductsSourcingTime;
            mProductGroup.SecurityPercentage = productGroup.SecurityPercentage;
            mProductGroup.Status = Convert.ToInt16(productGroup.Status);
            mProductGroup.CreationUser = productGroup.CreationUser;
            mProductGroup.CreationDate = Convert.ToString(productGroup.CreationDate);
            mProductGroup.ModificationUser = productGroup.ModificationUser;
            mProductGroup.ModificationDate = Convert.ToString(productGroup.ModificationDate);
            mProductGroup.InventoryCostMainAccount = methodsProductGroup.GetGeneralAccount(productGroup.InventoryCostMainAccountId ?? 0);
            mProductGroup.ReteFuenteConcept = methodsProductGroup.GetReteFuenteConcept(productGroup.ReteFuenteConceptId ?? 0);
            mProductGroup.ConsignmentMerchandiseDebitAccount = methodsProductGroup.GetGeneralAccount(productGroup.ConsignmentMerchandiseDebitAccountId);
            mProductGroup.ConsignmentMerchandiseCreditAccount = methodsProductGroup.GetGeneralAccount(productGroup.ConsignmentMerchandiseCreditAccountId);
            mProductGroup.CounterpartCostConsignedInventory = methodsProductGroup.GetGeneralAccount(productGroup.CounterpartCostConsignedInventoryId);
            mProductGroup.IncomeRecognitionMainAccount = methodsProductGroup.GetGeneralAccount(productGroup.IncomeRecognitionMainAccountId ?? 0);
            mProductGroup.IVAAccount = methodsProductGroup.GetGeneralAccount(productGroup.IVAAccountId ?? 0);
            mProductGroup.WithholdingTaxAccount = methodsProductGroup.GetGeneralAccount(productGroup.WithholdingTaxAccountId ?? 0);
            mProductGroup.WithholdingICAConcept = methodsProductGroup.GetGeneralAccount(productGroup.WithholdingICAConceptId ?? 0);
            mProductGroup.WithholdingICAAccount = methodsProductGroup.GetGeneralAccount(productGroup.WithholdingICAAccountId ?? 0);
            mProductGroup.AffectBudget = Convert.ToInt16(productGroup.AffectBudget);
            mProductGroup.ProductGroupFunctionalUnit = methodsProductGroup.GetProductGroupFunctionalUnit(productGroup);
            return mProductGroup;
        }
    }
}
