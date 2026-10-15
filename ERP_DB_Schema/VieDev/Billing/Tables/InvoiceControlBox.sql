CREATE TABLE [Billing].[InvoiceControlBox] (
    [Id]           INT     IDENTITY (1, 1) NOT NULL,
    [DocumentType] TINYINT NOT NULL,
    [Consecutive]  INT     NOT NULL,
    CONSTRAINT [PK_InvoiceControlBox] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo - campo representativo para tomar un numero dependiendo del tipo de factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceControlBox', @level2type = N'COLUMN', @level2name = N'Consecutive';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipos de factura  1= Factura prestacións servicios salud   6= Factura Basica  7= Factura de Venta de Productos  93=Notas Debito/Credito', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceControlBox', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceControlBox', @level2type = N'COLUMN', @level2name = N'Id';

