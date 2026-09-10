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



GO



GO



GO



GO



GO



GO



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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Criterio de redondeo aplicado al TotalSalesPrice (1=Peso, 10=Decena, 100=Centena, 1000=Unidad de Mil). Heredado del manual de tarifas quirúrgico al momento del cálculo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RoundService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica como se va redondear los valores  1 -- Peso  10 -- Decena  100 -- Centena  1000 -- Unidad de Mil    Este valor se obtiene del manual de tarifa asociado, al momento de calcular el TotalSalesPrice', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RoundService';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RoundService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK MainAccounts. Cuenta contable de ingresos (entidad o particular) para registro en libro mayor; clasificación de flujo de caja de procedimientos quirúrgicos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta de ingresos ya sea de la entidad o de particular', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Bandera que indica si el registro proviene de causación de honorarios médicos; filtra inclusión en módulo de liquidación de honorarios profesionales.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica que el registro se creo desde Causacion de honorarios medicos con lo cual solo se tendra en cuenta para el  Modulo de Liquidacion de Honorarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indicador de aplicación de recargo al cálculo del precio de venta unitario del servicio quirúrgico (sobrevalor, sobreprecio).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK RateManualDetailSurgical. ID del renglón específico del contrato tarifario quirúrgico; NULL si la liquidación es de tarifa fija (no variable).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK CostCenter. Identificador del centro de costo responsable de la ejecución del procedimiento quirúrgico (unidad funcional, sala de operaciones, servicio).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK BillingConcept. ID del grupo de servicios o concepto de facturación que agrupa el procedimiento quirúrgico para liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo de Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(18,2). Valor del costo unitario o total del procedimiento/insumo quirúrgico; base para margen y análisis de rentabilidad.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK ThirdParty. ID del tercero correspondiente al profesional de salud (cirujano, anestesiólogo, auxiliar) que ejecuta el procedimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del tercero Profesional de la Salud que Realiza ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20). Código único del profesional de salud ejecutor (de tabla INPROFSAL Crystal); para Qx especifica código del cirujano principal. PII Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(18). Valor total cobrado (facturado) por el servicio/procedimiento quirúrgico al usuario o asegurador; resultado de cantidad × tarifa ± recargos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total cobrado por el Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(18). Tarifa unitaria de venta del servicio quirúrgico definida en el manual de tarifas del contrato; antes de redondeo y recargos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Venta del servicio establecido en el manual de tarifas de contratos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(5,2). Porcentaje de participación o liquidación aplicado al servicio quirúrgico (comisión, distribución de ingreso entre partes).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de liquidacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Cantidad de unidades facturadas del servicio IPS quirúrgico (procedimientos, sesiones, unidades consumidas).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Facturada (Servicio IPS Asociado)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK IPSService. ID del servicio IPS específico asociado al procedimiento quirúrgico principal (componente de la orden).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IPS asociado al Servicio IPS Principal de Tipo Qx', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK ServiceOrderDetail. ID del renglón detallado de la orden de servicios quirúrgicos que origina este registro de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, PK. Identificador único del detalle de registro quirúrgico en facturación; clave para trazabilidad de procedimiento y liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle Qx', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle quirúrgico de las órdenes de servicio en facturación. Registra los servicios quirúrgicos facturados, incluyendo cantidades, precios de venta, porcentajes de liquidación, costos, honorarios médicos y la cuenta contable de ingresos asociada a cada procedimiento quirúrgico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la actividad económica asociada al servicio quirúrgico facturado, utilizada para clasificación tributaria y reportes fiscales.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';

GO
CREATE NONCLUSTERED INDEX [IX_ServiceOrderDetailSurgical_ServiceOrderDetailId]
    ON [Billing].[ServiceOrderDetailSurgical]([ServiceOrderDetailId] ASC);
