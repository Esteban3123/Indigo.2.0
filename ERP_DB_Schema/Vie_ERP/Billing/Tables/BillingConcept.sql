CREATE TABLE [Billing].[BillingConcept] (
    [Id]                                           INT             IDENTITY (1, 1) NOT NULL,
    [Code]                                         VARCHAR (20)    NOT NULL,
    [Name]                                         VARCHAR (100)   NOT NULL,
    [ConceptType]                                  TINYINT         CONSTRAINT [DF_BillingConcept_ConceptType] DEFAULT ((2)) NOT NULL,
    [ObtainCostCenter]                             TINYINT         CONSTRAINT [DF_IPSServiceGroup_ObtainCostCenter] DEFAULT ((1)) NULL,
    [CostCenterId]                                 INT             NULL,
    [AccountingType]                               TINYINT         NULL,
    [EntityIncomeAccountId]                        INT             NULL,
    [IndividualIncomeAccountId]                    INT             NULL,
    [DiscountAccountId]                            INT             NULL,
    [FeesExpensesAccountId]                        INT             NULL,
    [Status]                                       BIT             NOT NULL,
    [CreationUser]                                 VARCHAR (20)    NOT NULL,
    [CreationDate]                                 DATETIME        NOT NULL,
    [ModificationUser]                             VARCHAR (20)    NULL,
    [ModificationDate]                             DATETIME        NULL,
    [TimeStamp]                                    ROWVERSION      NOT NULL,
    [IncomeRecognitionPendingBillingMainAccountId] INT             NULL,
    [IVAId]                                        INT             NULL,
    [IVAAccountId]                                 INT             NULL,
    [WithholdingTaxConceptId]                      INT             NULL,
    [WithholdingTaxAccountId]                      INT             NULL,
    [WithholdingICAConceptId]                      INT             NULL,
    [WithholdingICAAccountId]                      INT             NULL,
    [Price]                                        DECIMAL (18, 2) CONSTRAINT [DF_BillingConcept_Price] DEFAULT ((0)) NOT NULL,
    [TypeService]                                  BIT             CONSTRAINT [DF__BillingCo__TypeS__7E73EF2F] DEFAULT ((0)) NOT NULL,
    [AssociatedMainServiceId]                      INT             NULL,
    [AlternativeCode]                              VARCHAR (13)    NOT NULL,
    [CopayMainAccountId]                           INT             NULL,
    [RecoveryFixedAmountMainAccountId]             INT             NULL,
    [EconomicActivityId]                           INT             NULL,
    CONSTRAINT [PK_BillingConcept__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingConcept_CopayMainAccounts] FOREIGN KEY ([CopayMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConcept_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_BillingConcept_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_BillingConcept_GeneralLedgerIVA] FOREIGN KEY ([IVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_BillingConcept_MainAccounts] FOREIGN KEY ([IncomeRecognitionPendingBillingMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConcept_MainAccounts1] FOREIGN KEY ([IVAAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConcept_MainAccounts2] FOREIGN KEY ([WithholdingTaxAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConcept_MainAccounts3] FOREIGN KEY ([WithholdingICAAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConcept_RecoveryMainAccounts] FOREIGN KEY ([RecoveryFixedAmountMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConcept_RetentionConcepts] FOREIGN KEY ([WithholdingTaxConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id]),
    CONSTRAINT [FK_BillingConcept_RetentionConcepts1] FOREIGN KEY ([WithholdingICAConceptId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [Billing].[BillingConcept] NOCHECK CONSTRAINT [FK_BillingConcept_CopayMainAccounts];


GO
ALTER TABLE [Billing].[BillingConcept] NOCHECK CONSTRAINT [FK_BillingConcept_CostCenter];


GO
ALTER TABLE [Billing].[BillingConcept] NOCHECK CONSTRAINT [FK_BillingConcept_GeneralLedgerIVA];


GO
ALTER TABLE [Billing].[BillingConcept] NOCHECK CONSTRAINT [FK_BillingConcept_MainAccounts];


GO
ALTER TABLE [Billing].[BillingConcept] NOCHECK CONSTRAINT [FK_BillingConcept_MainAccounts1];


GO
ALTER TABLE [Billing].[BillingConcept] NOCHECK CONSTRAINT [FK_BillingConcept_MainAccounts2];


GO
ALTER TABLE [Billing].[BillingConcept] NOCHECK CONSTRAINT [FK_BillingConcept_MainAccounts3];


GO
ALTER TABLE [Billing].[BillingConcept] NOCHECK CONSTRAINT [FK_BillingConcept_RecoveryMainAccounts];


GO
ALTER TABLE [Billing].[BillingConcept] NOCHECK CONSTRAINT [FK_BillingConcept_RetentionConcepts];


GO
ALTER TABLE [Billing].[BillingConcept] NOCHECK CONSTRAINT [FK_BillingConcept_RetentionConcepts1];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_BillingConcept__Code]
    ON [Billing].[BillingConcept]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable recuperacion monto fijo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'RecoveryFixedAmountMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cuenta contable copagos/cuotas moderadoras', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CopayMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alternativo parametrizado solo si el tipo de facturación es "Facturación Copagos y Cuotas Moderadoras".', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'AlternativeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio principal asociado:       va a alojar el ID del servicio principal.   ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'AssociatedMainServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Servicio:       0. Servicio principal   1. Servicio secundario   ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'TypeService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Precio Unitario de Venta, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Price';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable de la Retencion de ICA, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingICAAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto de Retencion de ICA, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingICAConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable de la Retencion de la Fuente, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingTaxAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto de Retencion de la Fuente, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingTaxConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable IVA, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IVAAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del IVA, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta NIIF Reconociemiento de Ingresos Pendientes por facturar, se asigna valor cuando el tipo de contabilización “Cuenta Única de Ingreso”', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionPendingBillingMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado   1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta de gastos de honorarios    Nota : este campo solo aplica cuando el tipo de concepto es 2 -  Facturacion Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'FeesExpensesAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta de descuento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'DiscountAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable para ingresos a particulares    Nota : este campo solo aplica cuando el tipo de concepto es 2 -  Facturacion Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IndividualIncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable de ingresos de la entidad    - Este campo se solicita cuando es Facturacion Basica  - Este campo se solicita cuando es Facturacion Servicio de salud y El tipo de contabilizacion es 1 - Cuenta Unica  - Este campo se oculta cuenta es Facturacion Servicio de salud y El tipo de contabilizacion es 2 - Cuenta por Tipo de unidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'EntityIncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de contabilidad   1 - Cuenta Unica de Ingreso  2 - Cuenta por Tipo de Unidad    Nota : este campo solo aplica cuando el tipo de concepto es 2 -  Facturacion Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'AccountingType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de costo, solo se llena si se obtiene el centro de costo del grupo del servicio IPS    Nota : este campo solo aplica cuando el tipo de concepto es 2 -  Facturacion Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica de donde se va a obtener el centro de costo en la orden de servicio  1 - Unidad Funcional del Paciente  2 - Centro de Costo Específico  3 - Centro de Costo por Sucursal y Unidad Funcional    Nota : este campo solo aplica cuando el tipo de concepto es 2 -  Facturacion Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ObtainCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especirfica el Tipo del concepto  1 - Facturacion Basica  2 - Facturacion Servicios de Salud  3 - Facturacion copagos y cuotas moderadoras', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del grupo contable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del grupo contable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo contable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Id';

