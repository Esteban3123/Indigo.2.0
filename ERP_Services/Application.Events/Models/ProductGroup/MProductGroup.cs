namespace Application.Events.Models.ProductGroup
{
    public class MProductGroup
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int GroupClass { get; set; }
        public int SubclassCode { get; set; }
        public GeneralAccount IncomeAccount { get; set; }
        public InventoryAccountPayableConcept InventoryAccountPayableConcept { get; set; }
        public InventoryAccountPayableConcept DeclarantRetentionAccountPayableConcept { get; set; }
        public NotDeclarantRetentionAccountPayableConcept NotDeclarantRetentionAccountPayableConcept { get; set; }
        public CostCenter CostCenter { get; set; }
        public GeneralAccount ReferenceInputDebitAccount { get; set; }
        public GeneralAccount ReferenceInputCreditAccount { get; set; }
        public GeneralAccount ReferenceOutputDebitAccount { get; set; }
        public GeneralAccount ReferenceOutputCreditAccount { get; set; }
        public int ExcludeFreightCosts { get; set; }
        public int ProductReplacementTime { get; set; }
        public int ProductsSourcingTime { get; set; }
        public decimal SecurityPercentage { get; set; }
        public int Status { get; set; }
        public string CreationUser { get; set; }
        public string CreationDate { get; set; }
        public string ModificationUser { get; set; }
        public string ModificationDate { get; set; }
        public GeneralAccount InventoryCostMainAccount { get; set; }
        public ReteFuenteConcept ReteFuenteConcept { get; set; }
        public GeneralAccount ConsignmentMerchandiseDebitAccount { get; set; }
        public GeneralAccount ConsignmentMerchandiseCreditAccount { get; set; }
        public GeneralAccount CounterpartCostConsignedInventory { get; set; }
        public GeneralAccount IncomeRecognitionMainAccount { get; set; }
        public GeneralAccount IVAAccount { get; set; }
        public GeneralAccount WithholdingTaxAccount { get; set; }
        public GeneralAccount WithholdingICAConcept { get; set; }
        public GeneralAccount WithholdingICAAccount { get; set; }
        public int AffectBudget { get; set; }
        public ProductGroupFunctionalUnit[] ProductGroupFunctionalUnit { get; set; }
    }
}
