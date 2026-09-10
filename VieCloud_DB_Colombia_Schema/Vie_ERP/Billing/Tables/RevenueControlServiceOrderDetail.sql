CREATE TABLE [Billing].[RevenueControlServiceOrderDetail] (
    [Id]                                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RevenueControlDetailId]              INT          NOT NULL,
    [ServiceOrderDetailId]                INT          NOT NULL,
    [RecordType]                          TINYINT      NOT NULL,
    [CUPSEntityId]                        INT          NULL,
    [IPSServiceId]                        INT          NULL,
    [ProductId]                           INT          NULL,
    [BillingConceptId]                    INT          NULL,
    [PerformsFunctionalUnitId]            INT          NOT NULL,
    [CostCenterId]                        INT          NOT NULL,
    [IncomeRecognitionMainAccountId]      INT          NOT NULL,
    [ServicesPendingBillingMainAccountId] INT          NOT NULL,
    [InvoicedQuantity]                    INT          NOT NULL,
    [RateManualSalePrice]                 NUMERIC (18) NOT NULL,
    [GrandTotalSalesPrice]                NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_RevenueControlServiceOrderDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta total del servicio, producto o procedimiento facturado. Valor numérico (NUMERIC 18) que incluye tarifa base más ajustes. Base para facturación y reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio de venta total.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario de venta según tarifa manual ingresada. Valor numérico (NUMERIC 18) del servicio, producto o procedimiento. Puede diferir de tarifa estándar o contratada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio de venta de la tarifa manual.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades facturadas del producto, servicio o procedimiento. Entero (INT) que multiplica el precio unitario. Referencia a lo cobrado en la factura o RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Referente a la cantidad facturada de un producto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) de la cuenta contable principal para servicios no facturados aún. Registro de pasivos u obligaciones pendientes de facturación en estado de negociación o glosa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServicesPendingBillingMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta principal de los servicios pendientes de facturar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServicesPendingBillingMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServicesPendingBillingMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) de la cuenta contable principal para reconocimiento de ingresos. Controla el ingreso realizado por servicios prestados, procedimientos, productos o atenciones facturados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta principal de reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) del centro de costo donde se prestó el servicio, procedimiento o atención. Agrupa gastos por unidad funcional, especialidad médica o área administrativa.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) de la unidad funcional que prestó el servicio o procedimiento. Referencia a área clínica, consultorio, laboratorio, imagenología o servicio donde se ejecutó la orden.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional del proceso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) del concepto de facturación aplicado. Clasifica el cargo como servicio profesional, procedimiento, producto, medicamento, honorario, arancel o gasto de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) del producto o insumo asociado. Nulo si es servicio puro. Puede ser medicamento, implante, dispositivo médico o bien tangible facturado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) del servicio prestado por la institución prestadora (IPS). Referencia al catálogo de servicios de salud: consulta, urgencia, hospitalización, procedimiento, examen o procedimiento especial.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IP.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) del código CUPS (Código Único de Procedimientos en Salud) registrado. Enlaza a procedimientos, servicios, atenciones o actos quirúrgicos normalizados para facturación y RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad del CUPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de registro (TINYINT): clasificador que diferencia entre servicios profesionales, procedimientos, productos, medicamentos u otros conceptos facturables en la orden de servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el tipo de registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RecordType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) del detalle de la orden de servicios o atención. Vinculación a procedimiento, examen, consulta, urgencia, hospitalización o acto médico ejecutado para el paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la órden de servicios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK INT) del detalle del control de ingresos. Rastreo de facturación, glosas, pagos, RIPS y auditoria contable del servicio prestado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del control de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY, PK) del registro de detalle. Llave primaria autoincrementable que relaciona control de ingresos con órdenes de servicios facturados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de ítems de órdenes de servicio asociados al control de ingresos en facturación. Relaciona cada línea de servicio ejecutado (procedimiento, medicamento o concepto de cobro) con su cuenta contable, unidad funcional, cantidad facturada y valor de venta, permitiendo el reconocimiento de ingresos y el seguimiento de servicios pendientes de facturar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControlServiceOrderDetail';
