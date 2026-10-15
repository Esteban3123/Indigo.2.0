CREATE TABLE [Billing].[QuotationServiceOrderDetailDistribution] (
    [Id]                            INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [QuotationServiceOrderDetailId] INT            NOT NULL,
    [Quantity]                      INT            NOT NULL,
    [GrandTotalSalesPrice]          NUMERIC (18)   NOT NULL,
    [GrandTotalDiscount]            NUMERIC (18)   CONSTRAINT [DF_QuotationServiceOrderDetailDistribution_GrandTotalDiscount] DEFAULT ((0)) NOT NULL,
    [DistributionType]              TINYINT        NOT NULL,
    [ThirdPartySalesPrice]          NUMERIC (18)   NOT NULL,
    [ThirdPartyPercentage]          NUMERIC (5, 2) NOT NULL,
    [ApplyRecoveryFee]              TINYINT        CONSTRAINT [DF_QuotationServiceOrderDetailDistribution_ApplyRecoveryFee] DEFAULT ((1)) NOT NULL,
    [RecoveryFeeType]               TINYINT        CONSTRAINT [DF_QuotationServiceOrderDetailDistribution_RecoveryFeeType] DEFAULT ((1)) NOT NULL,
    [SubTotalPatientSalesPrice]     NUMERIC (18)   NOT NULL,
    [PatientPercentage]             NUMERIC (5, 2) NOT NULL,
    [LastCaregroupId]               INT            CONSTRAINT [DF_QuotationServiceOrderDetailDistribution_LastCaregroupId] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_QuotationServiceOrderDetailDistribution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QuotationServiceOrderDetailDistribution_CareGroup] FOREIGN KEY ([LastCaregroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_QuotationServiceOrderDetailDistribution_QuotationServiceOrderDetail] FOREIGN KEY ([QuotationServiceOrderDetailId]) REFERENCES [Billing].[QuotationServiceOrderDetail] ([Id])
);


GO
ALTER TABLE [Billing].[QuotationServiceOrderDetailDistribution] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetailDistribution_CareGroup];


GO
ALTER TABLE [Billing].[QuotationServiceOrderDetailDistribution] NOCHECK CONSTRAINT [FK_QuotationServiceOrderDetailDistribution_QuotationServiceOrderDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo de atención de la última retarificación. Éste campo es usado como una bandera para las retarificaciones realizadas en el proceso de distribución. Por lo tanto no tiene relación con la tabla de grupos de atención.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'LastCaregroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al porcentaje del valor total cobrado por el Producto / Servicio a cargo del paciente por concepto de cuota de recuperacion (RecoveryFeeType)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'PatientPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor total calculado por el Producto / Servicio al paciente por concepto de cuotas de recuperacion. (RecoveryFeeType). Este valor es el calculado segun se establece en la ley, pero puede variar si al paciente se le concede un descuento sobre el valor de recuperacion.  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Cuota de Recuperacion  1. Ninguna  2. Cuota Moderadora  3. Copago  4. Bono  5. Cuota de Recuperación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'RecoveryFeeType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica 3 estados para el calculo de la cuota de recuperacion  0 - No Aplica Cuota de Recuperacion  1 - Disponible para hacer el calculo de la cuota de recuperacion  2 - Se esta aplicando cuota de recuperacion sobre el item', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ApplyRecoveryFee';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al porcentaje cobrado por el Producto / Servicio al tercero de la factura y/o folio, este valor es diferente del 100% cuando se aplica cuota de recuperacion.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartyPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor total cobrado por el Producto / Servicio al tercero de la factura y/o folio, este valor es diferente del GrandTotalSalesPrice cuando se aplica cuota de recuperacion.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Distribucion  1 - Ninguno  2 - Distribucion Normal  3 - Distribucion por Corte de Cuentas  4 - Distribucion por Unidad  5 - Distribucion NoPOS', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'DistributionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el total del descuento que se le realizo al item, Este descuento tambien se distribuye cuando el item es distribuido', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al valor total cobrado por el Producto / Servicio establecido en GrandTotalSalesPrice de ServiceOrderDetail  ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la cotización', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'QuotationServiceOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'QuotationServiceOrderDetailDistribution', @level2type = N'COLUMN', @level2name = N'Id';

