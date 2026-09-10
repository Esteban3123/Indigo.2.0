CREATE TABLE [Billing].[RevenueRecognitionDetailTmp] (
    [Id]                                  INT          IDENTITY (1, 1) NOT NULL,
    [RevenueRecognitionId]                INT          NOT NULL,
    [RevenueControlDetailId]              INT          NOT NULL,
    [ServiceOrderDetailId]                INT          NULL,
    [RecordType]                          TINYINT      NULL,
    [CUPSEntityId]                        INT          NULL,
    [IPSServiceId]                        INT          NULL,
    [ProductId]                           INT          NULL,
    [BillingConceptId]                    INT          NULL,
    [PerformsFunctionalUnitId]            INT          NULL,
    [CostCenterId]                        INT          NULL,
    [IncomeRecognitionMainAccountId]      INT          NULL,
    [ServicesPendingBillingMainAccountId] INT          NULL,
    [InvoicedQuantity]                    INT          NOT NULL,
    [RateManualSalePrice]                 NUMERIC (18) NOT NULL,
    [GrandTotalSalesPrice]                NUMERIC (18) NOT NULL,
    [ThirdPartySalesPrice]                NUMERIC (18) NOT NULL,
    [GrandTotalDiscount]                  NUMERIC (18) NOT NULL,
    [SubTotalPatientSalesPrice]           NUMERIC (18) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal del precio de venta al paciente (NUMERIC 18). Monto parcial facturable directamente al afiliado, excluyendo terceros pagadores, en reconocimiento de ingresos temporal.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el subtotal del precio de venta al paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento total aplicado en el folio durante reconocimiento de ingresos reversado (NUMERIC 18). Suma de ajustes, promociones o bonificaciones en facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el descuento total del folio en el reconocimiento de ingreso reversado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta al tercero pagador (NUMERIC 18). Monto facturable a asegurador, EPS, IPS o entidad contratante, excluyendo paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el precio de venta al tercero.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio total de venta del folio en reconocimiento de ingresos reversado (NUMERIC 18). Sumatoria de servicios, procedimientos y productos antes de descuentos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio total de venta del folio en el reconocimiento de ingreso reversado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta de la tarifa manual (NUMERIC 18). Valor unitario establecido manualmente, independiente de CUPS o lista de precios estándar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio de venta de la tarifa manual.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad facturada de producto, servicio o procedimiento (INT). Unidades o sesiones incluidas en el reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad facturada de un producto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave contable (INT) de servicios pendientes de facturación. Cuenta puente para servicios otorgados no aún reconocidos en ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'ServicesPendingBillingMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta principal de los servicios pendientes de facturar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'ServicesPendingBillingMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'ServicesPendingBillingMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave contable (INT) de reconocimiento de ingresos. Cuenta principal de ingresos operacionales por servicios de salud prestados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta principal de reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo (INT, FK). Asocia servicio/procedimiento a unidad operativa para control presupuestal y analítica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de costo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad funcional prestadora (INT, FK). Área clínica, laboratorio, urgencia, imagenología u otra donde se ejecutó el servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional del proceso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de facturación (INT, FK). Categoría de ingreso: consulta, procedimiento, medicamento, hospitalización, glosa, ajuste, etc.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'BillingConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto (INT, FK). Artículo, medicamento, dispositivo o bien vendido en la factura.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del servicio registrado en IPS (INT, FK). Vinculación a catálogo interno de servicios de la institución prestadora.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio IP.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'IPSServiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la entidad CUPS (INT, FK). Código CUPS estandarizado para procedimiento, servicio, medicamento o examen según MINSALUD.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la entidad del CUPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de registro (TINYINT, nullable). Clasificación: 1=Servicio, 2=Producto, 3=Procedimiento, 4=Ajuste, etc., para facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'RecordType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'RecordType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'RecordType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de orden de servicios (INT, nullable, FK). Trazabilidad a la orden clínica que originó el servicio/procedimiento.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la órden de servicios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de control de ingresos (INT, FK). Enlace a auditoría y control de facturación para rastrabilidad contable.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del control de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del reconocimiento de ingresos (INT, FK). Cabecera de la transacción de reconocimiento contable reversado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'RevenueRecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'RevenueRecognitionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'RevenueRecognitionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del detalle temporal de reconocimiento de ingresos. Clave primaria para auditoría, reversiones y ajustes contables.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único de los procesos de reconocimientos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla temporal que almacena el detalle del reconocimiento de ingresos por facturación: relaciona cada línea de servicio prestado con sus valores de venta, descuentos, copagos del paciente y cuentas contables asociadas, para el proceso de reconocimiento contable de ingresos en el módulo de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetailTmp';
