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



GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuenta contable de ingresos (FK MainAccounts). Identifica si el ingreso pertenece a la entidad prestadora o a profesional particular. Tipo: INT, nullable. Búsqueda: cuenta de ingresos, ingresos entidad, ingresos profesional.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta de ingresos ya sea de la entidad o de particular', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT, default 0) que indica si el registro proviene de Causación de Honorarios Médicos. Cuando es 1, se procesa exclusivamente en el Módulo de Liquidación de Honorarios. Búsqueda: honorarios médicos, causación, liquidación honorarios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica que el registro se creo desde Causacion de honorarios medicos con lo cual solo se tendra en cuenta para el  Modulo de Liquidacion de Honorarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que especifica si se aplicó recargo o sobrecosto al cálculo del precio de venta del servicio quirúrgico. Búsqueda: recargo, sobrecosto, precio de venta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a RateManualDetailSurgical. Referencia el detalle del manual de tarifas de donde se extrajo el valor. NULL si la liquidación en Grupo de Atención-Tarifas es de tipo Fijo. Búsqueda: manual de tarifas, tarifa quirúrgica, contrato.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a CostCenter (Payroll). Centro de Costo o Unidad Funcional asignada al servicio quirúrgico para contabilidad. Búsqueda: centro de costo, unidad funcional, departamento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a BillingConcept. Identifica el Grupo de Servicios o concepto de facturación aplicable al detalle quirúrgico. Búsqueda: grupo de servicios, concepto de facturación, servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo de Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numeric(18,2). Valor monetario del costo directo del servicio o producto quirúrgico. Base para cálculos de margen y utilidad. Búsqueda: costo, valor costo, costo directo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a ThirdParty. Identifica el tercero (persona jurídica o natural) asociado al profesional de salud ejecutante. Búsqueda: tercero, proveedor, profesional de salud, cirujano.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero asociado a un profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Char(20), nullable. Código del Profesional de Salud que ejecuta/realiza el servicio o administra el producto (origen INPROFSAL Crystal). En Qx: código del cirujano realizador. Búsqueda: cédula profesional, código profesional, cirujano, médico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numeric(18). Valor total de venta facturado por el producto/servicio quirúrgico. Equivalente a precio unitario × cantidad facturada. Búsqueda: precio de venta, monto facturado, tarifa total.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total cobrado por el Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numeric(18). Valor de venta unitario establecido en el manual de tarifas de contratos. Base para cálculo del total de ventas. Búsqueda: tarifa, precio contratado, valor unitario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Venta del servicio establecido en el manual de tarifas de contratos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numeric(5,2). Porcentaje de liquidación aplicado al detalle quirúrgico. Define cuánto del valor se liquida. Búsqueda: porcentaje liquidación, % pago, participación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el porcentaje de liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Cantidad facturada del servicio IPS asociado. Multiplicador para el cálculo de TotalSalesPrice. Búsqueda: cantidad, unidades facturadas, cantidad servicios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Facturada (Servicio IPS Asociado)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a IPSService (Contract). Servicio IPS asociado al servicio quirúrgico principal. Identifica procedimiento, insumo o producto administrado. Búsqueda: servicio IPS, código CUPS, procedimiento quirúrgico, insumo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IPS asociado al Servicio IPS Principal de Tipo Qx', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a QuotationServiceOrderDetail. Detalle de la orden de servicios/cotización que contiene esta línea quirúrgica. Búsqueda: orden de servicios, cotización, detalle orden.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'QuotationServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'QuotationServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'QuotationServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY(1,1). Clave primaria. Identificador único autoincremental del detalle de servicio quirúrgico. Búsqueda: registro, identificador, detalle quirúrgico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle quirúrgico de las líneas de una orden de servicio en cotización: registra cada servicio IPS incluido en un procedimiento quirúrgico cotizado, con cantidades facturadas, precio de venta, porcentaje de liquidación, honorarios médicos, centro de costos y conceptos de facturación asociados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Actividad económica asociada al servicio quirúrgico, usada para clasificación tributaria y facturación electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailSurgical', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
