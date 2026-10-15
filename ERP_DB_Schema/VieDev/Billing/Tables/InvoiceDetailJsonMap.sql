CREATE TABLE [Billing].[InvoiceDetailJsonMap] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [InvoiceDetailId] INT           NOT NULL,
    [ConsecutiveJson] INT           NOT NULL,
    [ServiceNameJson] VARCHAR (250) NOT NULL,
    [CreationDate]    DATETIME      NOT NULL,
    [TimeStamp]       ROWVERSION    NOT NULL,
    CONSTRAINT [PK_InvoiceDetailJsonMap] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceDetailJsonMap_InvoiceDetail] FOREIGN KEY ([InvoiceDetailId]) REFERENCES [Billing].[InvoiceDetail] ([Id])
);


GO
ALTER TABLE [Billing].[InvoiceDetailJsonMap] NOCHECK CONSTRAINT [FK_InvoiceDetailJsonMap_InvoiceDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de los servicios del Json del ministerio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'ServiceNameJson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de los servicios del Json del ministerio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'ConsecutiveJson';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del detalle de la tabla de factura ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'InvoiceDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tabla que guarda la relacion de los detalles de la factura con el Json Generado para validacion al ministerio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailJsonMap';

