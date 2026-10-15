CREATE TABLE [Billing].[QuotationServiceOrderDetailSurgical] (
    [Id]                                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [QuotationServiceOrderDetailId]          INT             NOT NULL,
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
    [OnlyMedicalFees]                        BIT             CONSTRAINT [DF_QuotationServiceOrderDetailSurgical_OnlyMedicalFees] DEFAULT ((0)) NOT NULL,
    [IncomeMainAccountId]                    INT             NULL,
    [EconomicActivityId]                     INT             NULL,
    CONSTRAINT [PK_QuotationServiceOrderDetailSurgical] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_MainAccounts] FOREIGN KEY ([IncomeMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_QuotationServiceOrderDetail] FOREIGN KEY ([QuotationServiceOrderDetailId]) REFERENCES [Billing].[QuotationServiceOrderDetail] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_RateManualDetailSurgical] FOREIGN KEY ([RateManualDetailSurgicalId]) REFERENCES [Contract].[RateManualDetailSurgical] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_ThirdParty] FOREIGN KEY ([PerformsHealthProfessionalThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Billing].[QuotationServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_BillingConcept];


GO
ALTER TABLE [Billing].[QuotationServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_CostCenter];


GO
ALTER TABLE [Billing].[QuotationServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_IPSService];


GO
ALTER TABLE [Billing].[QuotationServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_MainAccounts];


GO
ALTER TABLE [Billing].[QuotationServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_QuotationServiceOrderDetail];


GO
ALTER TABLE [Billing].[QuotationServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_RateManualDetailSurgical];


GO
ALTER TABLE [Billing].[QuotationServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetailSurgical_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta de ingresos ya sea de la entidad o de particular', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica que el registro se creo desde Causacion de honorarios medicos con lo cual solo se tendra en cuenta para el  Modulo de Liquidacion de Honorarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Grupo de Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor del costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero asociado a un profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor total cobrado por el Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Venta del servicio establecido en el manual de tarifas de contratos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el porcentaje de liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Facturada (Servicio IPS Asociado)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio IPS asociado al Servicio IPS Principal de Tipo Qx', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'QuotationServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';

