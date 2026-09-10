CREATE TABLE [rda].[InteroperabilityMessages] (
    [Id]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [JsonPayload] NVARCHAR (MAX) NOT NULL,
    [JsonHash]    CHAR (64)      NOT NULL,
    [Status]      VARCHAR (20)   NOT NULL,
    [CreatedAt]   DATETIME2 (7)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de mensajes de interoperabilidad enviados o recibidos entre sistemas externos e Indigo Vie Cloud, almacenando el contenido completo en formato JSON junto con su estado de procesamiento.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'InteroperabilityMessages';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'InteroperabilityMessages';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del mensaje de interoperabilidad.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'InteroperabilityMessages', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'InteroperabilityMessages', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido completo del mensaje en formato JSON, con los datos clínicos o administrativos intercambiados entre sistemas.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'InteroperabilityMessages', @level2type = N'COLUMN', @level2name = N'JsonPayload';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'InteroperabilityMessages', @level2type = N'COLUMN', @level2name = N'JsonPayload';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Huella digital (hash SHA-256) del contenido JSON, usada para detectar duplicados y verificar integridad del mensaje.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'InteroperabilityMessages', @level2type = N'COLUMN', @level2name = N'JsonHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'InteroperabilityMessages', @level2type = N'COLUMN', @level2name = N'JsonHash';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual del mensaje en el flujo de interoperabilidad (por ejemplo: pendiente, procesado, error, rechazado).', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'InteroperabilityMessages', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'InteroperabilityMessages', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el mensaje fue registrado en el sistema.', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'InteroperabilityMessages', @level2type = N'COLUMN', @level2name = N'CreatedAt';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'rda', @level1type = N'TABLE', @level1name = N'InteroperabilityMessages', @level2type = N'COLUMN', @level2name = N'CreatedAt';
