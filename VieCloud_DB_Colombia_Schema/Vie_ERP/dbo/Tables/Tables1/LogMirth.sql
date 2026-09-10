CREATE TABLE [dbo].[LogMirth] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [NumeroOrdenIndigo] INT            NOT NULL,
    [IdOrden]           INT            NOT NULL,
    [FechaRegistro]     DATETIME       NOT NULL,
    [TipoIntegracion]   TINYINT        NOT NULL,
    [TipoCanal]         TINYINT        NOT NULL,
    [MensajeError]      VARCHAR (8000) NULL,
    CONSTRAINT [PK_LogMirth] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
GRANT VIEW CHANGE TRACKING
    ON OBJECT::[dbo].[LogMirth] TO [MIRTH_LOGIN]
    AS [dbo];


GO
GRANT VIEW DEFINITION
    ON OBJECT::[dbo].[LogMirth] TO [MIRTH_LOGIN]
    AS [dbo];


GO
GRANT UPDATE
    ON OBJECT::[dbo].[LogMirth] TO [MIRTH_LOGIN]
    AS [dbo];


GO
GRANT TAKE OWNERSHIP
    ON OBJECT::[dbo].[LogMirth] TO [MIRTH_LOGIN]
    AS [dbo];


GO
GRANT SELECT
    ON OBJECT::[dbo].[LogMirth] TO [MIRTH_LOGIN]
    AS [dbo];


GO
GRANT REFERENCES
    ON OBJECT::[dbo].[LogMirth] TO [MIRTH_LOGIN]
    AS [dbo];


GO
GRANT INSERT
    ON OBJECT::[dbo].[LogMirth] TO [MIRTH_LOGIN]
    AS [dbo];


GO
GRANT DELETE
    ON OBJECT::[dbo].[LogMirth] TO [MIRTH_LOGIN]
    AS [dbo];


GO
GRANT CONTROL
    ON OBJECT::[dbo].[LogMirth] TO [MIRTH_LOGIN]
    AS [dbo];


GO
GRANT ALTER
    ON OBJECT::[dbo].[LogMirth] TO [MIRTH_LOGIN]
    AS [dbo];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de validación o error capturado durante la integración Mirth; texto descriptivo del fallo en procesamiento de orden (VARCHAR 8000, NULL si éxito)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'MensajeError';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje de validacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'MensajeError';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'MensajeError';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del flujo de integración: 1=Envío orden a tercero (laboratorio/patología/hemocomponentes), 2=Recepción resultado u orden de tercero; indica dirección del canal de comunicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'TipoCanal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Envio orden a tercero  2 - Recepcion Resultado orden de tercero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'TipoCanal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'TipoCanal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de servicio de salud integrado: 1=Integración Laboratorios (análisis clínicos), 2=Integración Patologías (anatomía patológica), 3=Integración Hemocomponentes (banco de sangre); TINYINT identificador de proveedor externo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'TipoIntegracion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - integracion Laboratorios  2 - integracion Patologias  3 - integracion Hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'TipoIntegracion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'TipoIntegracion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del evento de integración Mirth; timestamp del procesamiento de orden (DATETIME, auditoría de transacciones)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'FechaRegistro';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'FechaRegistro';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'FechaRegistro';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de la orden de servicio (examen, análisis, procedimiento) en tabla [Ordenes]; clave autoincrementable de referencia a orden clínica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'IdOrden';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Orden  (columna AUTO de Ordenes)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'IdOrden';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'IdOrden';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de orden en sistema Indigo Vie Cloud (INTERDETA); identificador externo para trazabilidad de fallo o integración con terceros; clave de correlación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'NumeroOrdenIndigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id orden de fallo - NumeroOrdenIndigo (INTERDETA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'NumeroOrdenIndigo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'NumeroOrdenIndigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo de registro de log Mirth (INT IDENTITY, clave primaria); registro único de evento de integración para auditoría y depuración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría y errores de integración con Mirth Connect. Guarda el historial de mensajes intercambiados entre Indigo y sistemas externos (laboratorio, imágenes, HL7, etc.), incluyendo órdenes procesadas, canal utilizado y los errores ocurridos durante la transmisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'LogMirth';
