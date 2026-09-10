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
ALTER TABLE [Billing].[InvoiceDetailSurgical] NOCHECK CONSTRAINT [CC_InvoiceDetailSurgical_Balance];


GO



GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_InvoiceDetailSurgical_InvoiceDetailId]
    ON [Billing].[InvoiceDetailSurgical]([InvoiceDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente del detalle quirúrgico (NUMERIC 18,2, DEFAULT=0, CHECK≥0). Se afecta por notas de cartera tipo preauditoría. Rastrea deuda o crédito.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al saldo del detalle. Este campo es afectado por notas de cartera de tipo preauditoría', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cuenta de ingresos (GeneralLedger.MainAccounts). Define si ingreso es de entidad o de particulares. Clase contable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta de ingresos ya sea de la entidad o de particular', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'IncomeMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT, DEFAULT=0). TRUE=registro originado en Causación de Honorarios Médicos; solo se contabiliza en módulo de Liquidación de Honorarios. FALSE=procesamiento normal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica que el registro se creo desde Causacion de honorarios medicos con lo cual solo se tendra en cuenta para el  Modulo de Liquidacion de Honorarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'OnlyMedicalFees';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT). TRUE=se aplicó recargo/sobreprecio al cálculo del valor de venta. FALSE=sin recargo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si se le aplico recargo al calculo del precio de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'SurchargeApply';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle específico en manual de tarifas quirúrgicas (Contract.RateManualDetailSurgical). NULL si tarifa es Fija. Trazabilidad de tarifa aplicada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del detalle del manual de tarifas de donde se tomo el valor.  Si el tipo de liquidacion en el Grupo de Atencion - Tarifas es Fijo este campo es null', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualDetailSurgicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del centro de costo responsable (Payroll.CostCenter). Unidad funcional que reporta gastos del procedimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Centro de Costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del concepto/grupo de servicios de facturación (Billing.BillingConcept). Clasifica el tipo de servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Grupo de Servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del costo asociado al servicio. Costo operativo o de producción del procedimiento quirúrgico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor del costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'CostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del tercero/profesional de salud ejecutor (Common.ThirdParty). Vincula al responsable del procedimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tercero Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador (CHAR 20, PII) del profesional de salud que realiza/administra el servicio quirúrgico. Para cirugías: código del cirujano. Procede de tabla Crystal INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Codigo del Profesional de la Salud que Realiza el Servicio y/o Administra el Producto.   Estos datos se sacan de la tabla de Crystal INPROFSAL  Para Qx Se especifica el codigo del profesional cirujano', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'PerformsHealthProfessionalCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total cobrado por el producto/servicio quirúrgico = RateManualSalePrice × InvoicedQuantity. Monto final de venta facturado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al valor total cobrado por el Producto / Servicio  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor unitario de venta del servicio según manual de tarifas vigente en contrato. Base para cálculo de precio total.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Venta del servicio establecido en el manual de tarifas de contratos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de liquidación aplicado (0.00-100.00). Define la proporción de pago según contrato o tarifa pactada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el porcentaje de liquidación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'LiquidationPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad facturada del servicio/procedimiento IPS. Número de unidades cobradas por la atención quirúrgica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Facturada (Servicio IPS Asociado)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del servicio IPS asociado, procedimiento quirúrgico o complementario. Referencia a Contract.IPSService.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IPS asociado al Servicio IPS Principal de Tipo Qx', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia (FK) al detalle de factura padre (Billing.InvoiceDetail). Vincula cada registro quirúrgico a su factura principal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id relacion detalle de factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la tabla InvoiceDetailSurgical. Clave primaria para rastreo de detalles quirúrgicos facturados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle quirúrgico de los ítems facturados en una cuenta de cobro: registra los servicios de cirugía incluidos en una factura, con cantidades, precios de venta, porcentajes de liquidación, honorarios médicos, costos, centro de costo y cuenta contable de ingreso asociada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailSurgical';
