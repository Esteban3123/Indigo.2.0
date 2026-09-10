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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Información completa de la respuesta de la DIAN (VARCHAR MAX). Contiene datos XML/JSON de validación, aceptación o rechazo del documento electrónico; usado para auditoría y trazabilidad de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información de la respuesta de la DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensajes informativos generados durante el proceso de envío en cada detalle (VARCHAR 250). Incluye alertas, validaciones parciales o notas del sistema sobre el estado del documento electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra mensajes de información que se generaron durante el proceso en cada detalle.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de respuesta de la DIAN (VARCHAR 250). Código numérico o alfanumérico que indica el resultado de la validación: aceptado, rechazado, con observaciones. Referencia a comunicaciones DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Response';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de respuesta de la DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Response';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Response';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de validación DIAN: 0=Respuesta inválida/rechazada de la DIAN, 1=Respuesta válida/aceptada de la DIAN (BIT). Indicador booleano de éxito en la transacción electrónica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado 0 - Respuesta invalida de la DIAN, 1 - Respuesta valida de la DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del detalle de respuesta del documento electrónico (DATETIME). Marca temporal de cuando se registró la transacción con la DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece la fecha de creación del documento electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino de envío del documento: 0=Detalles con errores (local), 2=Envío a DIAN, 3=Envío al Cliente (TINYINT). Clasifica la ruta de procesamiento y distribución del documento electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Destination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el destino de envio. 0 - Detalles con errores , 2 - Envio a la DIAN, 3 - Envio al Cliente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Destination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Destination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del documento electrónico asociado (INT FK). Referencia a [Billing].[ElectronicDocument] para trazabilidad de factura, RIPS o documento de soporte.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'ElectronicDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'ElectronicDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'ElectronicDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de detalle de documento electrónico (INT PK). Clave primaria clustered para cada transacción de respuesta DIAN.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del detalle de envíos y respuestas de documentos electrónicos de facturación (facturas electrónicas, notas crédito/débito). Guarda cada intento de transmisión a la DIAN u otros destinatarios, con su estado, respuesta y comentarios asociados.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicDocumentDetail';
