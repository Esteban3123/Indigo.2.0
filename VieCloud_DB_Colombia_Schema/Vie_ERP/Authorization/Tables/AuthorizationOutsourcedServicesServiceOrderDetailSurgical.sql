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



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta de ingresos (INT, FK a MainAccounts). Especifica si el ingreso es de la entidad o de particular. Usado para contabilidad y reportes financieros. Nullable.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta de ingresos ya sea de la entidad o de particular', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, default=0). Especifica si el registro se creó desde Causación de Honorarios Médicos, indicando que solo se tendrá en cuenta en el Módulo de Liquidación de Honorarios (no en facturación general).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica que el registro se creo desde Causacion de honorarios medicos con lo cual solo se tendra en cuenta para el  Modulo de Liquidacion de Honorarios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT). Especifica si se aplicó recargo (sobretasa) al cálculo del precio de venta del servicio quirúrgico o producto.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del manual de tarifas quirúrgicas (INT, FK a RateManualDetailSurgical). Especifica de dónde se tomó el valor. Null si la liquidación es de tipo Fijo. Nullable.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Centro de Costo (INT, FK a CostCenter). Especifica el centro de costo responsable de la prestación del servicio quirúrgico para asignación de costos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Concepto de Facturación (INT, FK a BillingConcept). Especifica la categoría o concepto con el que se facturará el servicio en RIPS y factura.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de facturación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico del costo (NUMERIC 18,2). Especifica el costo unitario o total del servicio quirúrgico para análisis de rentabilidad.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del costo', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del profesional de salud ejecutor (INT, FK a ThirdParty). Especifica el ID del cirujano, anestesiólogo u otro profesional que realiza el servicio y/o administra el producto. Nullable.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del profesional de salud que realiza el servicio y/o administra el producto.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del profesional de salud (CHAR 20). Especifica el código del profesional que realiza la intervención quirúrgica o administra el producto (origen: tabla Crystal INPROFSAL). Para Qx es el código del cirujano. Nullable.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico total (NUMERIC 18). Corresponde al valor total cobrado por el producto/servicio quirúrgico facturado después de aplicar descuentos y recargos.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total cobrado por el Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de venta unitaria (NUMERIC 18). Valor del venta del servicio establecido en el manual de tarifas de contratos vigente (base para cálculo de TotalSalesPrice).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Venta del servicio establecido en el manual de tarifas de contratos', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de liquidación (NUMERIC 5,2). Especifica el porcentaje aplicado para liquidar honorarios médicos o participaciones en el servicio quirúrgico.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de liquidación.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad facturada (INT). Cantidad facturada del Servicio IPS Asociado ejecutado en la intervención quirúrgica (ej: 1 cirugía, 2 drenajes).', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Facturada (Servicio IPS Asociado)', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del servicio IPS (INT, FK a IPSService). Id del servicio IPS asociado al Servicio IPS Principal de tipo Quirúrgico (Qx). Especifica el procedimiento o componente.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IPS asociado al Servicio IPS Principal de Tipo Qx', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de orden (INT, FK a AuthorizationOutsourcedServicesServiceOrderDetail). Especifica el id del detalle de la orden de servicios tercerizada de la cual desciende este registro.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'AuthorizationOutsourcedServicesServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (INT, PK IDENTITY). Clave primaria que identifica de forma única cada detalle quirúrgico en la tabla.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle quirúrgico de los servicios tercerizados autorizados en órdenes de servicio: registra los ítems de cirugía (honorarios, tarifas, cantidades, porcentajes de liquidación y conceptos de facturación) asociados a una autorización de servicios contratados con terceros.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationOutsourcedServicesServiceOrderDetailSurgical';
