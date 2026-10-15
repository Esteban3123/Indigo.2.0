CREATE TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetailSurgical] (
    [Id]                                                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationOutsourcedServicesServiceOrderDetailId] INT             NOT NULL,
    [IPSServiceId]                                        INT             NOT NULL,
    [InvoicedQuantity]                                    INT             NOT NULL,
    [LiquidationPercentage]                               NUMERIC (5, 2)  NOT NULL,
    [RateManualSalePrice]                                 NUMERIC (18)    NOT NULL,
    [TotalSalesPrice]                                     NUMERIC (18)    NOT NULL,
    [PerformsHealthProfessionalCode]                      CHAR (20)       NULL,
    [PerformsHealthProfessionalThirdPartyId]              INT             NULL,
    [CostValue]                                           NUMERIC (18, 2) NOT NULL,
    [BillingConceptId]                                    INT             NOT NULL,
    [CostCenterId]                                        INT             NOT NULL,
    [RateManualDetailSurgicalId]                          INT             NULL,
    [SurchargeApply]                                      BIT             NOT NULL,
    [OnlyMedicalFees]                                     BIT             CONSTRAINT [DF_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_OnlyMedicalFees] DEFAULT ((0)) NOT NULL,
    [IncomeMainAccountId]                                 INT             NULL,
    CONSTRAINT [PK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_AuthorizationOutsourcedServicesServiceOrderDetail] FOREIGN KEY ([AuthorizationOutsourcedServicesServiceOrderDetailId]) REFERENCES [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetail] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_IPSService] FOREIGN KEY ([IPSServiceId]) REFERENCES [Contract].[IPSService] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_MainAccounts] FOREIGN KEY ([IncomeMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_RateManualDetailSurgical] FOREIGN KEY ([RateManualDetailSurgicalId]) REFERENCES [Contract].[RateManualDetailSurgical] ([Id]),
    CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_ThirdParty] FOREIGN KEY ([PerformsHealthProfessionalThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_AuthorizationOutsourcedServicesServiceOrderDetail];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_BillingConcept];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_CostCenter];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_IPSService];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_MainAccounts];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_RateManualDetailSurgical];


GO
ALTER TABLE [Authorization].[AuthorizationOutsourcedServicesServiceOrderDetailSurgical] NOCHECK CONSTRAINT [FK_AuthorizationOutsourcedServicesServiceOrderDetailSurgical_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta de ingresos ya sea de la entidad o de particular', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica que el registro se creo desde Causacion de honorarios medicos con lo cual solo se tendra en cuenta para el  Modulo de Liquidacion de Honorarios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del concepto de facturación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor del costo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el Id del profesional de salud que realiza el servicio y/o administra el producto.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor total cobrado por el Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del Venta del servicio establecido en el manual de tarifas de contratos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el porcentaje de liquidación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Facturada (Servicio IPS Asociado)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del servicio IPS asociado al Servicio IPS Principal de Tipo Qx', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';

