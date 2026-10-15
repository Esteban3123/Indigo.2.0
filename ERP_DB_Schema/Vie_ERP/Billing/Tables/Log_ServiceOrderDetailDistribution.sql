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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la fecha de la última modificación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del último grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'LastCaregroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el porcentaje aplicado el paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'PatientPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el precio subtotal de venta al paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la tarifa de recuperación donde 1 - No tiene ninguna, 2 - Cuota Moderada, 3 - Copago.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo que aplica tarifa de recuperación donde 0 - No se cobra, 1 - Disponible para cobrar, 2 - Cobrado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el porcentaje para los terceros.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el precio de venta a terceros.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el tipo de distribución.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el descuento total general.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el precio total de venta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece la cantidad de los items que tiene el detalle de la órden de servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle de la órden de servicios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle del control de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RevenueControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'Log_ServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Id';

