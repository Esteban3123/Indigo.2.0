CREATE TABLE [Billing].[ServiceOrderDetailSurgical] (
    [Id]                                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ServiceOrderDetailId]                   INT             NOT NULL,
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
    [OnlyMedicalFees]                        BIT             CONSTRAINT [DF_ServiceOrderDetailSurgical_OnlyMedicalFees] DEFAULT ((0)) NOT NULL,
    [IncomeMainAccountId]                    INT             NOT NULL,
    [RoundService]                           INT             CONSTRAINT [DF_ServiceOrderDetailSurgical_RoundService] DEFAULT ((1)) NOT NULL,
    [EconomicActivityId]                     INT             NULL,
    CONSTRAINT [PK_ServiceOrderDetailSurgical__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ServiceOrderDetailSurgical_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetailSurgical_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetailSurgical_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetailSurgical_IPSServiceGroup] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetailSurgical_MainAccounts] FOREIGN KEY ([IncomeMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetailSurgical_RateManualDetail] FOREIGN KEY ([RateManualDetailSurgicalId]) REFERENCES [Contract].[RateManualDetailSurgical] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetailSurgical_ServiceOrderDetail1] FOREIGN KEY ([ServiceOrderDetailId]) REFERENCES [Billing].[ServiceOrderDetail] ([Id]),
    CONSTRAINT [FK_ServiceOrderDetailSurgical_ThirdParty] FOREIGN KEY ([PerformsHealthProfessionalThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Billing].[ServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_ServiceOrderDetailSurgical_CostCenter];


GO
ALTER TABLE [Billing].[ServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_ServiceOrderDetailSurgical_IPSService];


GO
ALTER TABLE [Billing].[ServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_ServiceOrderDetailSurgical_IPSServiceGroup];


GO
ALTER TABLE [Billing].[ServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_ServiceOrderDetailSurgical_MainAccounts];


GO
ALTER TABLE [Billing].[ServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_ServiceOrderDetailSurgical_RateManualDetail];


GO
ALTER TABLE [Billing].[ServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_ServiceOrderDetailSurgical_ServiceOrderDetail1];


GO
ALTER TABLE [Billing].[ServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_ServiceOrderDetailSurgical_ThirdParty];


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetailSurgical__OnlyMedicalFees__INC__ServiceOrderDetailId__TotalSalesPrice]
    ON [Billing].[ServiceOrderDetailSurgical]([OnlyMedicalFees] ASC)
    INCLUDE([ServiceOrderDetailId], [TotalSalesPrice]);


GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetailSurgical__ServiceOrderDetailId]
    ON [Billing].[ServiceOrderDetailSurgical]([ServiceOrderDetailId] ASC);


GO
-- Índice filtrado optimizado para VReportInvoiceDetail y consultas similares
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetailSurgical_ForInvoiceReport]
    ON [Billing].[ServiceOrderDetailSurgical] ([ServiceOrderDetailId] ASC, [Id] ASC)
    INCLUDE ([IPSServiceId], [InvoicedQuantity], [TotalSalesPrice])
    WHERE [TotalSalesPrice] > 0 AND [OnlyMedicalFees] = 0;


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica como se va redondear los valores  1 -- Peso  10 -- Decena  100 -- Centena  1000 -- Unidad de Mil    Este valor se obtiene del manual de tarifa asociado, al momento de calcular el TotalSalesPrice', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RoundService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta de ingresos ya sea de la entidad o de particular', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica que el registro se creo desde Causacion de honorarios medicos con lo cual solo se tendra en cuenta para el  Modulo de Liquidacion de Honorarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Grupo de Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor del costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del tercero Profesional de la Salud que Realiza ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor total cobrado por el Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Venta del servicio establecido en el manual de tarifas de contratos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de liquidacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Facturada (Servicio IPS Asociado)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio IPS asociado al Servicio IPS Principal de Tipo Qx', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle Qx', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';

