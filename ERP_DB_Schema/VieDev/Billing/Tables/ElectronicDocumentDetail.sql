CREATE TABLE [Billing].[ElectronicDocumentDetail] (
    [Id]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ElectronicDocumentId] INT           NOT NULL,
    [Destination]          TINYINT       NOT NULL,
    [CreationDate]         DATETIME      NOT NULL,
    [Status]               BIT           NOT NULL,
    [Response]             VARCHAR (250) NULL,
    [Comments]             VARCHAR (250) NULL,
    [ResponseData]         VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ElectronicDocumentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicDocumentDetail_ElectronicDocument] FOREIGN KEY ([ElectronicDocumentId]) REFERENCES [Billing].[ElectronicDocument] ([Id])
);


GO
ALTER TABLE [Billing].[ElectronicDocumentDetail] NOCHECK CONSTRAINT [FK_ElectronicDocumentDetail_ElectronicDocument];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información de la respuesta de la DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Muestra mensajes de información que se generaron durante el proceso en cada detalle.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de respuesta de la DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Response';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado 0 - Respuesta invalida de la DIAN, 1 - Respuesta valida de la DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece la fecha de creación del documento electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Establece el destino de envio. 0 - Detalles con errores , 2 - Envio a la DIAN, 3 - Envio al Cliente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Destination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del documento electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'ElectronicDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Id';

