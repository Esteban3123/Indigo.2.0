CREATE TABLE [Billing].[ElectronicSupportDocumentDetail] (
    [Id]                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ElectronicSupportDocumentId] INT           NOT NULL,
    [Destination]                 TINYINT       NOT NULL,
    [CreationDate]                DATETIME      NOT NULL,
    [Status]                      BIT           NOT NULL,
    [Response]                    VARCHAR (250) NULL,
    [Comments]                    VARCHAR (250) NULL,
    [ResponseData]                VARCHAR (MAX) NOT NULL,
    [HttpContent]                 VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ElectronicSupportDocumentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicSupportDocumentDetail_ElectronicSupportDocument] FOREIGN KEY ([ElectronicSupportDocumentId]) REFERENCES [Billing].[ElectronicSupportDocument] ([Id])
);




GO





GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos completos de la respuesta recibida del servicio de facturación electrónica (DIAN, validación, etc.). Tipo: VARCHAR(MAX). Contiene XML, JSON o estructura de error del destinatario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacenamos la respuesta recibida del servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comentarios, notas o información adicional extraída de la respuesta recibida del servicio. Tipo: VARCHAR(250). Útil para auditoría de glosas o rechazos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios en la respuesta recibida', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de respuesta del servicio (ej: código HTTP, código DIAN, código de validación). Tipo: VARCHAR(250). Indica tipo de respuesta recibida del destinatario.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Response';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la respuesta recibida', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Response';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Response';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del envío del documento electrónico: 0=Fallido (respuesta válida del servicio pero no cumple requisitos), 1=Exitoso (respuesta válida y cumple requisitos). Tipo: BIT. Clave para tracking de factura electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del envío      0. Fallido (Si se obtiene respuesta valida del servicio no cumple los requisitos)      1. Exitoso (Si se obtiene respuesta valida del servicio cumple los requisitos)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro y del intento de envío del documento electrónico. Tipo: DATETIME. Marca temporal para auditoría de envíos a DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro e intento de envío', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destinatario del envío: 0=Validación previa (generación de XML), 1=DIAN (envío de documento electrónico), 2=Validación post-envío. Tipo: TINYINT. Identifica fase de facturación electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Destination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica el destinato del envío      0. Validacion previa a la generacion del XML      1. Envio Documento Electronico a la DIAN      2. Validacion del Documento Enviado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Destination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Destination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del documento electrónico padre (factura, nota crédito, remisión). Tipo: INT. Clave foránea a [Billing].[ElectronicSupportDocument].', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'ElectronicSupportDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento electronico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'ElectronicSupportDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'ElectronicSupportDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de envío del documento electrónico. Tipo: INT (IDENTITY). Permite rastrear cada intento de transmisión a destinatarios.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del envío del documento electronico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'Id';

GO

GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro detallado de los intentos de envío y respuestas de documentos soporte electrónicos de facturación. Guarda el historial de comunicaciones con cada destino (DIAN, aseguradora, etc.), incluyendo el estado, respuesta recibida y contenido HTTP de cada transmisión.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido completo del cuerpo HTTP enviado o recibido durante la transmisión del documento soporte electrónico, incluyendo cabeceras, payload o XML de la petición/respuesta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'HttpContent';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentDetail', @level2type = N'COLUMN', @level2name = N'HttpContent';
