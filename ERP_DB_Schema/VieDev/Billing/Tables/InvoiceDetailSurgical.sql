CREATE TABLE [Billing].[InvoiceDetailSurgical] (
    [Id]                                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceDetailId]                        INT             NOT NULL,
    [IPSServiceId]                           INT             NOT NULL,
    [InvoicedQuantity]                       INT             NOT NULL,
    [LiquidationPercentage]                  NUMERIC (5, 2)  NOT NULL,
    [RateManualSalePrice]                    NUMERIC (18)    NOT NULL,
    [TotalSalesPrice]                        NUMERIC (18)    NOT NULL,
    [PerformsHealthProfessionalCode]         CHAR (20)       NULL,
    [PerformsHealthProfessionalThirdPartyId] INT             NULL,
    [CostValue]                              NUMERIC (18, 2) NOT NULL,
    [BillingConceptId]                       INT             NOT NULL,
    [CostCenterId]                           INT             NOT NULL,
    [RateManualDetailSurgicalId]             INT             NULL,
    [SurchargeApply]                         BIT             NOT NULL,
    [OnlyMedicalFees]                        BIT             CONSTRAINT [DF_InvoiceDetailSurgical_OnlyMedicalFees] DEFAULT ((0)) NOT NULL,
    [IncomeMainAccountId]                    INT             NOT NULL,
    [Balance]                                NUMERIC (18, 2) CONSTRAINT [DF_InvoiceDetailSurgical_Balance] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_InvoiceDetailSurgical] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CC_InvoiceDetailSurgical_Balance] CHECK ([Balance]>=(0)),
    CONSTRAINT [FK_InvoiceDetailSurgical_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_InvoiceDetailSurgical_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_InvoiceDetailSurgical_InvoiceDetail] FOREIGN KEY ([InvoiceDetailId]) REFERENCES [Billing].[InvoiceDetail] ([Id]),
    CONSTRAINT [FK_InvoiceDetailSurgical_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_InvoiceDetailSurgical_MainAccounts] FOREIGN KEY ([IncomeMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_InvoiceDetailSurgical_RateManualDetailSurgical] FOREIGN KEY ([RateManualDetailSurgicalId]) REFERENCES [Contract].[RateManualDetailSurgical] ([Id]),
    CONSTRAINT [FK_InvoiceDetailSurgical_ThirdParty] FOREIGN KEY ([PerformsHealthProfessionalThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Billing].[InvoiceDetailSurgical] NOCHECK CONSTRAINT [CC_InvoiceDetailSurgical_Balance];


GO
ALTER TABLE [Billing].[InvoiceDetailSurgical] NOCHECK CONSTRAINT [FK_InvoiceDetailSurgical_BillingConcept];


GO
ALTER TABLE [Billing].[InvoiceDetailSurgical] NOCHECK CONSTRAINT [FK_InvoiceDetailSurgical_CostCenter];


GO
ALTER TABLE [Billing].[InvoiceDetailSurgical] NOCHECK CONSTRAINT [FK_InvoiceDetailSurgical_InvoiceDetail];


GO
ALTER TABLE [Billing].[InvoiceDetailSurgical] NOCHECK CONSTRAINT [FK_InvoiceDetailSurgical_IPSService];


GO
ALTER TABLE [Billing].[InvoiceDetailSurgical] NOCHECK CONSTRAINT [FK_InvoiceDetailSurgical_MainAccounts];


GO
ALTER TABLE [Billing].[InvoiceDetailSurgical] NOCHECK CONSTRAINT [FK_InvoiceDetailSurgical_RateManualDetailSurgical];


GO
ALTER TABLE [Billing].[InvoiceDetailSurgical] NOCHECK CONSTRAINT [FK_InvoiceDetailSurgical_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_InvoiceDetailSurgical_InvoiceDetailId]
    ON [Billing].[InvoiceDetailSurgical]([InvoiceDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor total cobrado por el Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el porcentaje de liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';

