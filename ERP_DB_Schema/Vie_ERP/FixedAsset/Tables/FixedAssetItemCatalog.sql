CREATE TABLE [FixedAsset].[FixedAssetItemCatalog] (
    [Id]                                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                                         VARCHAR (20)  NOT NULL,
    [Description]                                  VARCHAR (MAX) NOT NULL,
    [Active]                                       BIT           NOT NULL,
    [IncomeAccountPayableConceptId]                INT           NULL,
    [IncomeAccountId]                              INT           NOT NULL,
    [IncomeLeasingAccountId]                       INT           NULL,
    [DebitLoanAccountId]                           INT           NULL,
    [CreditLoanAccountId]                          INT           NULL,
    [DepreciationAccountId]                        INT           NULL,
    [DebitValorizationAccountId]                   INT           NULL,
    [CreditValorizationAccountId]                  INT           NULL,
    [DebitDevaluationAccountId]                    INT           NULL,
    [CreditDevaluationAccountId]                   INT           NULL,
    [NetIncomeAccountId]                           INT           NULL,
    [LossMainAccountId]                            INT           NULL,
    [ReplacementCreditMainAccountId]               INT           NULL,
    [WarehouseAssetsMainAccountId]                 INT           CONSTRAINT [DF_FixedAssetItemCatalog_WarehouseAssetsMainAccountId] DEFAULT ((2619)) NULL,
    [MaintenanceAssetsMainAccountId]               INT           CONSTRAINT [DF_FixedAssetItemCatalog_MaintenanceAssetsMainAccountId] DEFAULT ((2619)) NULL,
    [DeclarantRetentionAccountPayableConceptId]    INT           NULL,
    [NotDeclarantRetentionAccountPayableConceptId] INT           NULL,
    [Status]                                       BIT           NOT NULL,
    [CreationUser]                                 VARCHAR (20)  NOT NULL,
    [CreationDate]                                 DATETIME      NOT NULL,
    [ModificationUser]                             VARCHAR (20)  NULL,
    [ModificationDate]                             DATETIME      NULL,
    [TimeStamp]                                    ROWVERSION    NOT NULL,
    [IVADeductible]                                BIT           CONSTRAINT [DF_FixedAssetItemCatalog_IVADeductible] DEFAULT ((0)) NOT NULL,
    [DepreciationLeasingAccountId]                 INT           CONSTRAINT [DF_FixedAssetItemCatalog_DepreciationLeasingAccountId] DEFAULT ((3935)) NULL,
    [LoanLeasingAccountId]                         INT           CONSTRAINT [DF_FixedAssetItemCatalog_LoanLeasingAccountId] DEFAULT ((4)) NULL,
    [CreditEquipmentPlantAcumulatedAccountId]      INT           NULL,
    [AccumulatedDeteriorationAccountId]            INT           CONSTRAINT [DF_FixedAssetItemCatalog_AccumulatedDeteriorationAccountId] DEFAULT ((4)) NULL,
    [FinancialRentingAccountId]                    INT           CONSTRAINT [DF_FixedAssetItemCatalog_FinancialRentingAccountId] DEFAULT ((0)) NULL,
    [IVAAccountId]                                 INT           NULL,
    [WithholdingTaxConceptId]                      INT           NULL,
    [WithholdingTaxAccountId]                      INT           NULL,
    [WithholdingICAConceptId]                      INT           NULL,
    [WithholdingICAAccountId]                      INT           NULL,
    [AffectBudget]                                 BIT           CONSTRAINT [DF_FixedAssetItemCatalog_AffectBudget] DEFAULT ((0)) NOT NULL,
    [BudgetId]                                     INT           NULL,
    [Classification]                               TINYINT       CONSTRAINT [DF__FixedAsse__Class__698EAC39] DEFAULT ((1)) NOT NULL,
    [HandlesDepreciationbyDistribution]            BIT           CONSTRAINT [DF_FixedAssetItemCatalog_HandlesDepreciationbyDistribution] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_FixedAssetItemCatalog__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EquipmentCatalog_MainAccounts] FOREIGN KEY ([CreditLoanAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_EquipmentCatalog_MainAccounts1] FOREIGN KEY ([DebitLoanAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_EquipmentCatalog_MainAccounts2] FOREIGN KEY ([IncomeAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_EquipmentCatalog_MainAccounts3] FOREIGN KEY ([IncomeLeasingAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalog_MainAccounts] FOREIGN KEY ([DepreciationAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalog_MainAccounts1] FOREIGN KEY ([DebitValorizationAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalog_MainAccounts2] FOREIGN KEY ([CreditValorizationAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalog_MainAccounts3] FOREIGN KEY ([DebitDevaluationAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetEquipmentCatalog_MainAccounts4] FOREIGN KEY ([CreditDevaluationAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_AccountPayableConcepts] FOREIGN KEY ([DeclarantRetentionAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_AccountPayableConcepts1] FOREIGN KEY ([NotDeclarantRetentionAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_AccountPayableConcepts2] FOREIGN KEY ([IncomeAccountPayableConceptId]) REFERENCES [Payments].[AccountPayableConcepts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts] FOREIGN KEY ([NetIncomeAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts1] FOREIGN KEY ([LossMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts10] FOREIGN KEY ([IVAAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts11] FOREIGN KEY ([WithholdingTaxAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts12] FOREIGN KEY ([WithholdingICAAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts2] FOREIGN KEY ([ReplacementCreditMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts3] FOREIGN KEY ([WarehouseAssetsMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts4] FOREIGN KEY ([MaintenanceAssetsMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts5] FOREIGN KEY ([DepreciationLeasingAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts6] FOREIGN KEY ([CreditEquipmentPlantAcumulatedAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts7] FOREIGN KEY ([LoanLeasingAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts8] FOREIGN KEY ([AccumulatedDeteriorationAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_MainAccounts9] FOREIGN KEY ([FinancialRentingAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_RetentionConcepts] FOREIGN KEY ([WithholdingTaxConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalog_RetentionConcepts1] FOREIGN KEY ([WithholdingICAConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_FixedAssetItemCatalog__Code]
    ON [FixedAsset].[FixedAssetItemCatalog]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maneja depreciación por Distribución', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'HandlesDepreciationbyDistribution';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificacion  1 - Activo Fijo  2 - Intangibles', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Classification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del presupuesto de gastos a usar en el reconocimiento de facturas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el grupo de atencion afecta presupuesto  1 - Si  0- No    Si esta como si, entonces cuando se vaya a radicar la factura este creara un reconocimiento automaticamente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'AffectBudget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable de la Retencion de ICA en Venta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingICAAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto de Retencion de ICA en Venta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingICAConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable de la Retencion de la Fuente en Venta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingTaxAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto de Retencion de la Fuente en Venta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WithholdingTaxConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable IVA en Venta', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IVAAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cuenta depreciación renting financiero', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'FinancialRentingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable para el deterioro de propiedad, planta y equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'AccumulatedDeteriorationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cuenta Crédito Deterioro Acumulado de Propiedad, Planta y Equipo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditEquipmentPlantAcumulatedAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable de depreciación para los activos comodatos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'LoanLeasingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta contable que se acredita al momento de depreciar un activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DepreciationLeasingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'En desuso por actualización.  Ahora se usará el parámetro IvaCost definido en SettingFixedAssetByLegalBook para determinar la forma en que se contabiliza y se lleva el valor del Iva en el activo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IVADeductible';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado (1 - Activo, 0 - Inactivo)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de cuentas por pargar para la retencion en la fuente,en proveedores que sean no declarantes, Este concepto debe ser de tipo Especifico y debe Manejar Retencion y No debe ser de tipo 383 y 384', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'NotDeclarantRetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de cuentas por pargar para la retencion en la fuente,en proveedores que sean Declarantes, Este concepto debe ser de tipo Especifico y debe Manejar Retencion y No debe ser de tipo 383 y 384', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DeclarantRetentionAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la cuenta contable de Activos en Mantenimiento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'MaintenanceAssetsMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la cuenta contable de Activos en Bodega', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'WarehouseAssetsMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Cuenta Credito de Reposicion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'ReplacementCreditMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Cuenta contable de Perdida del ejercicio', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'LossMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta contable de la ganancia del ejercicio o Ingresos Netos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'NetIncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Cuenta Contable Credito de la Desvalorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditDevaluationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Cuenta Contable Débito de la Desvalorización', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DebitDevaluationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Cuenta Contable Credito de la Valorizacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditValorizationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Cuenta Contable Debito de la Valorizacion', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DebitValorizationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Cuenta Crédito de Depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DepreciationAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Cuenta Crédito de Comodato', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'CreditLoanAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de Cuenta Débito de Comodato', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'DebitLoanAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cuenta de Ingreso de Leasing', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IncomeLeasingAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta de Propiedad, Planta y Equipo. Se selecciona.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de cuenta por pagar, este debe ser de tipo General', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'IncomeAccountPayableConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Active';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del catalogo de Articulos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Catálogo de Articulos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Catálogo de Articulos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalog', @level2type = N'COLUMN', @level2name = N'Id';

