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



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_BillingConcept__Code]
    ON [Billing].[BillingConcept]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal para recuperación de monto fijo. INT, FK a [GeneralLedger].[MainAccounts]. Usado en facturación de servicios de salud para registrar recuperos de cantidades fijas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'RecoveryFixedAmountMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable recuperacion monto fijo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'RecoveryFixedAmountMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'RecoveryFixedAmountMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable principal para copagos y cuotas moderadoras. INT, FK a [GeneralLedger].[MainAccounts]. Contabiliza la parte pagada por el paciente/afiliado en servicios de salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CopayMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cuenta contable copagos/cuotas moderadoras', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CopayMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CopayMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alterno parametrizado (VARCHAR 13) aplicable solo en facturación de copagos y cuotas moderadoras (ConceptType=3). Identifica el concepto en sistemas externos o de intercambio de datos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'AlternativeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código alternativo parametrizado solo si el tipo de facturación es "Facturación Copagos y Cuotas Moderadoras".', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'AlternativeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'AlternativeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del servicio principal asociado al concepto de facturación. Vincula servicios secundarios o complementarios con su servicio principal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'AssociatedMainServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio principal asociado:       va a alojar el ID del servicio principal.   ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'AssociatedMainServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'AssociatedMainServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de servicio: 0=Servicio principal, 1=Servicio secundario (BIT). Define la jerarquía del concepto en la orden de servicio o factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'TypeService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Servicio:       0. Servicio principal   1. Servicio secundario   ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'TypeService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'TypeService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario de venta (DECIMAL 18,2). Aplica cuando ConceptType=1 (Facturación Básica). Valor por defecto: 0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Price';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precio Unitario de Venta, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Price';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Price';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable para retención de ICA (INT, FK a [GeneralLedger].[MainAccounts]). Aplica en ConceptType=1 (Facturación Básica). Contabiliza impuesto de industria y comercio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingICAAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de la Retencion de ICA, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingICAAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingICAAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención de ICA (INT, FK a [GeneralLedger].[RetentionConcepts]). Define el tipo de retención de ICA. Aplica en ConceptType=1.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingICAConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto de Retencion de ICA, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingICAConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingICAConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable para retención en la fuente (INT, FK a [GeneralLedger].[MainAccounts]). Aplica en ConceptType=1. Contabiliza descuentos por impuesto retenido.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingTaxAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de la Retencion de la Fuente, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingTaxAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingTaxAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de retención en la fuente (INT, FK a [GeneralLedger].[RetentionConcepts]). Define porcentaje/tipo de retención. Aplica en ConceptType=1.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingTaxConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Concepto de Retencion de la Fuente, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingTaxConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'WithholdingTaxConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable para IVA (INT, FK a [GeneralLedger].[MainAccounts]). Aplica en ConceptType=1 (Facturación Básica). Contabiliza impuesto al valor agregado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IVAAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable IVA, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IVAAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IVAAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la configuración de IVA (INT, FK a [GeneralLedger].[GeneralLedgerIVA]). Especifica tarifa y tratamiento de IVA. Aplica en ConceptType=1.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del IVA, aplica cuando el Tipo de Concepto es 1: Facturacion Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IVAId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta NIIF para reconocimiento de ingresos pendientes por facturar (INT, FK a [GeneralLedger].[MainAccounts]). Se asigna cuando AccountingType=1 (Cuenta Única de Ingreso). Garantiza normas de reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionPendingBillingMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta NIIF Reconociemiento de Ingresos Pendientes por facturar, se asigna valor cuando el tipo de contabilización “Cuenta Única de Ingreso”', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionPendingBillingMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionPendingBillingMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal TIMESTAMP (automática) que registra el instante exacto de creación, modificación o evento crítico en el concepto de facturación. Usado para auditoría y sincronización.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de la última modificación del concepto de facturación. NULL si nunca fue modificado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación. NULL si el registro no ha sido modificado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) de creación del concepto de facturación. Campo obligatorio, registra quién y cuándo se creó.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el concepto de facturación. Campo obligatorio para auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del concepto: 1=Activo, 0=Inactivo (BIT). Determina si el concepto se puede usar en órdenes, facturas y recaudos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado   1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable de gastos de honorarios/aranceles (INT, FK a [GeneralLedger].[MainAccounts]). Aplica solo en ConceptType=2 (Facturación Servicios de Salud).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'FeesExpensesAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta de gastos de honorarios    Nota : este campo solo aplica cuando el tipo de concepto es 2 -  Facturacion Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'FeesExpensesAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'FeesExpensesAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable para descuentos (INT, FK a [GeneralLedger].[MainAccounts]). Registra deducciones, bonificaciones o ajustes negativos al concepto de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'DiscountAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta de descuento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'DiscountAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'DiscountAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable de ingresos a particulares/pacientes (INT, FK a [GeneralLedger].[MainAccounts]). Aplica solo en ConceptType=2 (Facturación Servicios de Salud).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IndividualIncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable para ingresos a particulares    Nota : este campo solo aplica cuando el tipo de concepto es 2 -  Facturacion Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IndividualIncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'IndividualIncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta contable de ingresos de la entidad prestadora (INT, FK a [GeneralLedger].[MainAccounts]). Obligatorio en ConceptType=1 y en ConceptType=2 con AccountingType=1. Registra ingresos operacionales.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'EntityIncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de ingresos de la entidad    - Este campo se solicita cuando es Facturacion Basica  - Este campo se solicita cuando es Facturacion Servicio de salud y El tipo de contabilizacion es 1 - Cuenta Unica  - Este campo se oculta cuenta es Facturacion Servicio de salud y El tipo de contabilizacion es 2 - Cuenta por Tipo de unidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'EntityIncomeAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'EntityIncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de contabilización de ingresos: 1=Cuenta Única de Ingreso, 2=Cuenta por Tipo de Unidad Funcional (TINYINT). Aplica solo en ConceptType=2 (Facturación Servicios de Salud).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'AccountingType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de contabilidad   1 - Cuenta Unica de Ingreso  2 - Cuenta por Tipo de Unidad    Nota : este campo solo aplica cuando el tipo de concepto es 2 -  Facturacion Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'AccountingType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'AccountingType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo (INT, FK a [Payroll].[CostCenter]). Se asigna cuando ObtainCostCenter=2 (Centro de Costo Específico). Aplica solo en ConceptType=2.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo, solo se llena si se obtiene el centro de costo del grupo del servicio IPS    Nota : este campo solo aplica cuando el tipo de concepto es 2 -  Facturacion Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen del centro de costo en orden de servicio: 1=Unidad Funcional del Paciente, 2=Centro de Costo Específico, 3=Centro de Costo por Sucursal y Unidad Funcional (TINYINT). Aplica solo en ConceptType=2.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ObtainCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica de donde se va a obtener el centro de costo en la orden de servicio  1 - Unidad Funcional del Paciente  2 - Centro de Costo Específico  3 - Centro de Costo por Sucursal y Unidad Funcional    Nota : este campo solo aplica cuando el tipo de concepto es 2 -  Facturacion Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ObtainCostCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ObtainCostCenter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de concepto de facturación: 1=Facturación Básica, 2=Facturación Servicios de Salud, 3=Facturación Copagos y Cuotas Moderadoras (TINYINT). Determina qué campos se requieren.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especirfica el Tipo del concepto  1 - Facturacion Basica  2 - Facturacion Servicios de Salud  3 - Facturacion copagos y cuotas moderadoras', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción del concepto de facturación (VARCHAR 100). Identifica el servicio, producto o cargo. Usado en órdenes, facturas, recibos y glosas.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del grupo contable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del concepto de facturación (VARCHAR 20). Identificador corto para búsqueda, RIPS, facturación electrónica e integración con sistemas externos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del grupo contable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK IDENTITY). Clave primaria del concepto de facturación en tabla [Billing].[BillingConcept]. Usado en FK hacia órdenes, facturas y recibos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del grupo contable', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Conceptos de facturación utilizados en el proceso de cobro y liquidación de servicios de salud. Cada concepto define el tipo de cargo, sus cuentas contables asociadas (ingresos, descuentos, IVA, retenciones de ICA e ITE), centro de costos, precio base y configuración tributaria para la generación de facturas y documentos equivalentes.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad económica asociada al concepto de facturación, usada para clasificación tributaria y reporte de retenciones (ICA, renta) según el tipo de servicio o bien facturado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConcept', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
