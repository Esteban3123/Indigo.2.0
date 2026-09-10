CREATE TABLE [Billing].[Log_ServiceOrderDetailDistribution] (
    [Id]                        INT            NOT NULL,
    [RevenueControlDetailId]    INT            NOT NULL,
    [ServiceOrderDetailId]      INT            NOT NULL,
    [Quantity]                  INT            NOT NULL,
    [GrandTotalSalesPrice]      NUMERIC (18)   NOT NULL,
    [GrandTotalDiscount]        NUMERIC (18)   CONSTRAINT [DF_Log_ServiceOrderDetailDistribution_GrandTotalDiscount] DEFAULT ((0)) NOT NULL,
    [DistributionType]          TINYINT        NOT NULL,
    [ThirdPartySalesPrice]      NUMERIC (18)   NOT NULL,
    [ThirdPartyPercentage]      NUMERIC (5, 2) NOT NULL,
    [ApplyRecoveryFee]          TINYINT        CONSTRAINT [DF_Log_ServiceOrderDetailDistribution_ApplyRecoveryFee] DEFAULT ((1)) NOT NULL,
    [RecoveryFeeType]           TINYINT        CONSTRAINT [DF_Log_ServiceOrderDetailDistribution_RecoveryFeeType] DEFAULT ((1)) NOT NULL,
    [SubTotalPatientSalesPrice] NUMERIC (18)   NOT NULL,
    [PatientPercentage]         NUMERIC (5, 2) NOT NULL,
    [LastCaregroupId]           INT            CONSTRAINT [DF_Log_ServiceOrderDetailDistribution_LastCaregroupId] DEFAULT ((0)) NOT NULL,
    [ModificationDate]          DATETIME       CONSTRAINT [DF__Log_Servi__Modif__5BAE99E9] DEFAULT (getdate()) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del último cambio realizado en el registro de distribución (DATETIME, genera timestamp automático con getdate()).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha de la última modificación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del último grupo de atención, caregroup o equipo asistencial relacionado con la distribución (INT, FK a tabla de grupos de atención).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'LastCaregroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del último grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'LastCaregroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'LastCaregroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de participación del paciente en el costo del servicio, incluye copago y cuota moderada (NUMERIC 5,2, rango 0-100%).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'PatientPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el porcentaje aplicado el paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'PatientPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'PatientPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto subtotal a cargo del paciente, antes de aplicar descuentos generales (NUMERIC 18, en unidad monetaria).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio subtotal de venta al paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tarifa de recuperación aplicada: 1=Sin tarifa, 2=Cuota moderada, 3=Copago; clasifica cobros adicionales al paciente (TINYINT).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la tarifa de recuperación donde 1 - No tiene ninguna, 2 - Cuota Moderada, 3 - Copago.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera de aplicación de tarifa de recuperación: 0=No se cobra, 1=Disponible para cobrar, 2=Cobrado; controla estado de cobro (TINYINT).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que aplica tarifa de recuperación donde 0 - No se cobra, 1 - Disponible para cobrar, 2 - Cobrado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de participación de terceros (aseguradora, EPS, empresa) en el costo del servicio (NUMERIC 5,2, rango 0-100%).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje para los terceros.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto a facturar a terceros/aseguradoras por el servicio prestado (NUMERIC 18, en unidad monetaria).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio de venta a terceros.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de distribución de costos: clasifica cómo se reparte el monto entre paciente, terceros y entidad (TINYINT, enumerable).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de distribución.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DistributionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descuento total aplicado al detalle de la orden de servicio, reduce el valor final de venta (NUMERIC 18, valor por defecto 0).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el descuento total general.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio total de venta del detalle de la orden de servicio, antes de descuentos; suma base para facturación y RIPS (NUMERIC 18).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el precio total de venta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del servicio o item de la orden de servicio; cantidad de procedimientos, consultas o artículos (INT).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece la cantidad de los items que tiene el detalle de la órden de servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de la orden de servicio (OrdenDeAtención), vincula registro a procedimiento/servicio específico (INT, FK).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la órden de servicios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del control de ingresos (ControlDeIngresos/Facturación), vincula a seguimiento de ingresos generados (INT, FK).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del control de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del registro de distribución de detalle de orden de servicio (INT NOT NULL).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro histórico (log) de la distribución de costos y precios de cada ítem de una orden de servicio entre el tercero pagador (aseguradora, EPS, contrato) y el paciente. Almacena cómo se reparte el valor total de un servicio facturado: porcentaje a cargo del tercero, cuota de recuperación y saldo a cargo del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution';
