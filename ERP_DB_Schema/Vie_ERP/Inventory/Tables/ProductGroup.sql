CREATE TABLE [Inventory].[ProductGroup] (
    [Id]                                           INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                                         VARCHAR (20)   NOT NULL,
    [Name]                                         VARCHAR (100)  NOT NULL,
    [GroupClass]                                   TINYINT        NOT NULL,
    [SubclassCode]                                 TINYINT        NOT NULL,
    [IncomeAccountId]                              INT            NOT NULL,
    [InventoryAccountPayableConceptId]             INT            NOT NULL,
    [DeclarantRetentionAccountPayableConceptId]    INT            CONSTRAINT [DF_ProductGroup_DeclarantRetentionAccountPayableConceptId] DEFAULT ((73)) NOT NULL,
    [NotDeclarantRetentionAccountPayableConceptId] INT            CONSTRAINT [DF_ProductGroup_NotDeclarantRetentionAccountPayableConceptId] DEFAULT ((73)) NOT NULL,
    [CostCenterId]                                 INT            NOT NULL,
    [ReferenceInputDebitAccountId]                 INT            NOT NULL,
    [ReferenceInputCreditAccountId]                INT            NOT NULL,
    [ReferenceOutputDebitAccountId]                INT            NOT NULL,
    [ReferenceOutputCreditAccountId]               INT            NOT NULL,
    [ExcludeFreightCosts]                          BIT            NOT NULL,
    [ProductReplacementTime]                       INT            NOT NULL,
    [ProductsSourcingTime]                         INT            NOT NULL,
    [SecurityPercentage]                           NUMERIC (5, 2) NOT NULL,
    [BudgetCategoryId]                             INT            NULL,
    [Status]                                       BIT            NOT NULL,
    [CreationUser]                                 VARCHAR (20)   NOT NULL,
    [CreationDate]                                 DATETIME       NOT NULL,
    [ModificationUser]                             VARCHAR (20)   NULL,
    [ModificationDate]                             DATETIME       NULL,
    [TimeStamp]                                    ROWVERSION     NOT NULL,
    [InventoryCostMainAccountId]                   INT            NULL,
    [ReteFuenteConceptId]                          INT            NULL,
    [ConsignmentMerchandiseDebitAccountId]         INT            CONSTRAINT [DF__ProductGr__Consi__0829D8A0] DEFAULT ((2649)) NOT NULL,
    [ConsignmentMerchandiseCreditAccountId]        INT            CONSTRAINT [DF__ProductGr__Consi__091DFCD9] DEFAULT ((2649)) NOT NULL,
    [CounterpartCostConsignedInventoryId]          INT            CONSTRAINT [DF_ProductGroup_CounterpartCostConsignedInventory] DEFAULT ((2649)) NOT NULL,
    [IncomeRecognitionMainAccountId]               INT            NULL,
    [IVAAccountId]                                 INT            NULL,
    [WithholdingTaxAccountId]                      INT            NULL,
    [WithholdingICAConceptId]                      INT            NULL,
    [WithholdingICAAccountId]                      INT            NULL,
    [AffectBudget]                                 BIT            CONSTRAINT [DF_ProductGroup_AffectBudget] DEFAULT ((0)) NOT NULL,
    [BudgetId]                                     INT            NULL,
    [AccountingPackageMainAccountId]               INT            NULL,
    [FavorableDeviationMainAccountId]              INT            NULL,
    [VariationPVMainAccountId]                     INT            NULL,
    [EconomicActivityId]                           INT            NULL,
    CONSTRAINT [PK_ProductGroup__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductGroup_AccountingPackageMainAccount] FOREIGN KEY ([AccountingPackageMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_AccountPayableConcepts] FOREIGN KEY ([DeclarantRetentionAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_ProductGroup_AccountPayableConcepts1] FOREIGN KEY ([InventoryAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_ProductGroup_AccountPayableConcepts2] FOREIGN KEY ([NotDeclarantRetentionAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_ProductGroup_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_ProductGroup_Category] FOREIGN KEY ([BudgetCategoryId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_ProductGroup_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ProductGroup_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_ProductGroup_FavorableDeviationMainAccount] FOREIGN KEY ([FavorableDeviationMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts] FOREIGN KEY ([IncomeAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts1] FOREIGN KEY ([InventoryCostMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts10] FOREIGN KEY ([ConsignmentMerchandiseDebitAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts11] FOREIGN KEY ([ConsignmentMerchandiseCreditAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts12] FOREIGN KEY ([CounterpartCostConsignedInventoryId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts2] FOREIGN KEY ([IncomeRecognitionMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts3] FOREIGN KEY ([IVAAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts4] FOREIGN KEY ([WithholdingTaxAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts5] FOREIGN KEY ([WithholdingICAAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts6] FOREIGN KEY ([ReferenceInputDebitAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts7] FOREIGN KEY ([ReferenceInputCreditAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts8] FOREIGN KEY ([ReferenceOutputDebitAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_MainAccounts9] FOREIGN KEY ([ReferenceOutputCreditAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ProductGroup_RetentionConcepts] FOREIGN KEY ([ReteFuenteConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_ProductGroup_RetentionConcepts2] FOREIGN KEY ([WithholdingICAConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_ProductGroup_VariationPVMainAccount] FOREIGN KEY ([VariationPVMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_AccountingPackageMainAccount];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_AccountPayableConcepts];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_AccountPayableConcepts1];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_AccountPayableConcepts2];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_Budget];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_Category];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_CostCenter];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_FavorableDeviationMainAccount];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts1];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts10];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts11];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts12];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts2];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts3];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts4];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts5];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts6];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts7];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts8];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_MainAccounts9];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_RetentionConcepts];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_RetentionConcepts2];


GO
ALTER TABLE [Inventory].[ProductGroup] NOCHECK CONSTRAINT [FK_ProductGroup_VariationPVMainAccount];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ProductGroup__Code]
    ON [Inventory].[ProductGroup]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contabilización variación de precios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'VariationPVMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contabilización variación favorable', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'FavorableDeviationMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contabilización de paquetes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'AccountingPackageMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del presupuesto de gastos a usar en el reconocimiento de facturas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el grupo de atencion afecta presupuesto  1 - Si  0- No    Si esta como si, entonces cuando se vaya a radicar la factura este creara un reconocimiento automaticamente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable de la Retencion de ICA en Venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'WithholdingICAAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto de Retencion de ICA en Venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'WithholdingICAConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable de la Retencion de la Fuente en Venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'WithholdingTaxAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable IVA en Venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'IVAAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta para reconocimiento de ingresos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta que será la contrapartida al costo para la contabilización de la dispensación, devoluciones del inventario en consignación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CounterpartCostConsignedInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta credito para la contabilización del inventario en consignación por parte del consignatario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseCreditAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta debito para la contabilización del inventario en consignación por parte del consignatario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ConsignmentMerchandiseDebitAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto de Retencion de la Fuente en Venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReteFuenteConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable costo inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'InventoryCostMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro 1 - Activo 0- Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del  rubro de gastos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'BudgetCategoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de seguridad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'SecurityPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de abastecimiento de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ProductsSourcingTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tiempo de reposicion de productos (Dias)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ProductReplacementTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se realiza el proceso de costo a los productos (Suministros) asociados al grupo (EXCLUIR COSTO EN SUMINISTROS)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ExcludeFreightCosts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta credito de la remision de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceOutputCreditAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta debito de la remision de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceOutputDebitAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta credito de la remision de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceInputCreditAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta debito de la remision de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'ReferenceInputDebitAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de cuentas por pargar para la retencion en la fuente,en proveedores que sean no declarantes, Este concepto debe ser de tipo Especifico y debe Manejar Retencion y No debe ser de tipo 383 y 384', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'NotDeclarantRetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de cuentas por pargar para la retencion en la fuente,en proveedores que sean Declarantes, Este concepto debe ser de tipo Especifico y debe Manejar Retencion y No debe ser de tipo 383 y 384', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'DeclarantRetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de concepo de la cuenta por pagar de inventarios, Esa cuenta es usada para generar el comprobsante contable en los procesos de comprobante de entrada  y dispensacion farmaceuica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'InventoryAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable que se va afectar en los ingresos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'IncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de subatencion o subclase  1 - Insumos Hospitalarios  2 - Material Quirurgico  3 - Medicamentos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'SubclassCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la clase de productos del grupo  1 - Producto  2 - Servicio', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'GroupClass';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductGroup', @level2type = N'COLUMN', @level2name = N'Id';

