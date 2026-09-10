CREATE TABLE [Billing].[ElectronicSupportDocumentAdjustmentNoteDetail] (
    [Id]                                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ElectronicSupportDocumentAdjustmentNoteId] INT           NOT NULL,
    [Destination]                               TINYINT       NOT NULL,
    [CreationDate]                              DATETIME      NOT NULL,
    [Status]                                    BIT           NOT NULL,
    [Response]                                  VARCHAR (250) NULL,
    [Comments]                                  VARCHAR (250) NULL,
    [ResponseData]                              VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_ElectronicSupportDocumentAdjustmentNoteDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ElectronicSupportDocumentAdjustmentNoteDetail_ElectronicSupportDocumentAdjustmentNote] FOREIGN KEY ([ElectronicSupportDocumentAdjustmentNoteId]) REFERENCES [Billing].[ElectronicSupportDocumentAdjustmentNote] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Payload completo de la respuesta recibida del servicio externo (DIAN, validador). VARCHAR(MAX) almacena JSON/XML de la respuesta técnica del procesamiento electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacenamos la respuesta recibida del servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'ResponseData';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas descriptivas sobre la respuesta recibida del servicio; observaciones sobre validación, errores o incidencias en el procesamiento del documento electrónico. VARCHAR(250).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Comentarios en la respuesta recibida', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Comments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de respuesta del servicio (ej: 0200, error codes DIAN). Identifica el resultado técnico del envío o validación. VARCHAR(250).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Response';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la respuesta recibida', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Response';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Response';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado binario del envío: 0=Fallido (respuesta válida pero no cumple requisitos), 1=Exitoso (respuesta válida y cumple requisitos DIAN/validación).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del envío      0. Fallido (Si se obtiene respuesta valida del servicio no cumple los requisitos)      1. Exitoso (Si se obtiene respuesta valida del servicio cumple los requisitos)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Timestamp de creación del registro e intento de envío a servicio externo. DATETIME, registra cuándo se procesó el documento electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro e intento de envío', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de destino/fase del envío: 0=Validación previa (generación XML), 1=Envío a DIAN (documento electrónico oficial), 2=Validación post-envío. TINYINT.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Destination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica el destinato del envío      0. Validacion previa a la generacion del XML      1. Envio Documento Electronico a la DIAN      2. Validacion del Documento Enviado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Destination';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Destination';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a cabecera de nota de ajuste del documento de soporte electrónico. INT, vincula detalle a nota de ajuste principal (RIPS/factura electrónica).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'ElectronicSupportDocumentAdjustmentNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del detalle de la nota de ajuste del documento de soporte electrónico.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'ElectronicSupportDocumentAdjustmentNoteId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'ElectronicSupportDocumentAdjustmentNoteId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PK del detalle de envío electrónico. INT IDENTITY, identifica cada intento/respuesta de transmisión a servicio externo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del envío del documento electronico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los intentos de envío y respuestas asociadas a notas de ajuste (notas débito/crédito) de documentos soporte electrónicos en facturación. Registra cada transmisión hacia la DIAN u otros destinatarios, incluyendo el estado del envío, la respuesta recibida y los datos técnicos de respuesta.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ElectronicSupportDocumentAdjustmentNoteDetail';
