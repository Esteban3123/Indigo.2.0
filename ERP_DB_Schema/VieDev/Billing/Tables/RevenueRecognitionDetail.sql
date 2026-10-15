CREATE TABLE [Billing].[RevenueRecognitionDetail] (
    [Id]                                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RevenueRecognitionId]                INT             NOT NULL,
    [RevenueControlDetailId]              INT             NOT NULL,
    [ServiceOrderDetailId]                INT             NULL,
    [RecordType]                          TINYINT         NULL,
    [CUPSEntityId]                        INT             NULL,
    [IPSServiceId]                        INT             NULL,
    [ProductId]                           INT             NULL,
    [BillingConceptId]                    INT             NULL,
    [PerformsFunctionalUnitId]            INT             NULL,
    [CostCenterId]                        INT             NULL,
    [IncomeRecognitionMainAccountId]      INT             NULL,
    [ServicesPendingBillingMainAccountId] INT             NULL,
    [InvoicedQuantity]                    INT             CONSTRAINT [DF__RevenueRe__Invoi__283AAE32] DEFAULT ((0)) NOT NULL,
    [RateManualSalePrice]                 NUMERIC (20, 2) CONSTRAINT [DF__RevenueRe__RateM__292ED26B] DEFAULT ((0)) NOT NULL,
    [GrandTotalSalesPrice]                NUMERIC (20, 2) CONSTRAINT [DF__RevenueRe__Grand__2A22F6A4] DEFAULT ((0)) NOT NULL,
    [ThirdPartySalesPrice]                NUMERIC (20, 2) CONSTRAINT [DF__RevenueRe__Third__2B171ADD] DEFAULT ((0)) NOT NULL,
    [GrandTotalDiscount]                  NUMERIC (20, 2) CONSTRAINT [DF__RevenueRe__Grand__2C0B3F16] DEFAULT ((0)) NOT NULL,
    [SubTotalPatientSalesPrice]           NUMERIC (20, 2) CONSTRAINT [DF__RevenueRe__SubTo__2CFF634F] DEFAULT ((0)) NOT NULL,
    [AdmissionNumber]                     VARCHAR (10)    NULL,
    CONSTRAINT [PK_RevenueRecognitionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RevenueRecognitionDetail_RevenueRecognition] FOREIGN KEY ([RevenueRecognitionId]) REFERENCES [Billing].[RevenueRecognition] ([Id])
);


GO
ALTER TABLE [Billing].[RevenueRecognitionDetail] NOCHECK CONSTRAINT [FK_RevenueRecognitionDetail_RevenueRecognition];


GO
CREATE NONCLUSTERED INDEX [UX_RevenueRecognitionDetail_RevenueControlDetailId]
    ON [Billing].[RevenueRecognitionDetail]([RevenueControlDetailId] ASC)
    INCLUDE([RevenueRecognitionId]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el precio subtotal de venta al paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'SubTotalPatientSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el descuento total general.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el precio de venta a terceros.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartySalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el precio total de venta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'GrandTotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el precio de venta de la tarifa manual.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'RateManualSalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica la cantidad facturada de un producto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'InvoicedQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta principal de los servicios pendientes de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'ServicesPendingBillingMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta principal de reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del centro de costo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la unidad funcional del proceso.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'PerformsFunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del concepto de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del producto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del servicio IP.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'IPSServiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la entidad del CUPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el tipo de registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'RecordType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle de la órden de servicio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueRecognitionDetail', @level2type = N'COLUMN', @level2name = N'ServiceOrderDetailId';

