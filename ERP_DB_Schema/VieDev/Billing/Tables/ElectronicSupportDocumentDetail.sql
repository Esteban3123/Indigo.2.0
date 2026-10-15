CREATE TABLE [Billing].[ElectronicSupportDocumentDetail] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ElectronicSupportDocumentId] INT           NOT NULL,
    [Destination]                 TINYINT       NOT NULL,
    [CreationDate]                DATETIME      NOT NULL,
    [Status]                      BIT           NOT NULL,
    [Response]                    VARCHAR (250) NULL,
    [Comments]                    VARCHAR (250) NULL,
    [ResponseData]                VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ElectronicSupportDocumentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicSupportDocumentDetail_ElectronicSupportDocument] FOREIGN KEY ([ElectronicSupportDocumentId]) REFERENCES [Billing].[ElectronicSupportDocument] ([Id])
);


GO
ALTER TABLE [Billing].[ElectronicSupportDocumentDetail] NOCHECK CONSTRAINT [FK_ElectronicSupportDocumentDetail_ElectronicSupportDocument];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacenamos la respuesta recibida del servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentarios en la respuesta recibida', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la respuesta recibida', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Response';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del envío      0. Fallido (Si se obtiene respuesta valida del servicio no cumple los requisitos)      1. Exitoso (Si se obtiene respuesta valida del servicio cumple los requisitos)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creación del registro e intento de envío', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica el destinato del envío      0. Validacion previa a la generacion del XML      1. Envio Documento Electronico a la DIAN      2. Validacion del Documento Enviado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Destination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del documento electronico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'ElectronicSupportDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del envío del documento electronico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Id';

