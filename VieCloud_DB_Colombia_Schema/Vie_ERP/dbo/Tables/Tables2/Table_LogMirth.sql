CREATE TABLE [dbo].[Table_LogMirth] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [Mensaje] VARCHAR (250) NULL,
    [ER7]     VARCHAR (MAX) NULL,
    [Fecha]   DATETIME      CONSTRAINT [DF_Table_LogMirth_Fecha] DEFAULT ([Common].[getdate]()) NULL,
    CONSTRAINT [PK_Table_LogMirth] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de log de mensajes procesados por Mirth Connect (motor de integración HL7). Guarda los mensajes de intercambio de información clínica entre sistemas, incluyendo el contenido en formato ER7 y la fecha de procesamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Table_LogMirth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Table_LogMirth';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental del registro de log.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Table_LogMirth', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Table_LogMirth', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o resumen del mensaje de integración recibido o enviado, texto corto indicando el tipo de evento o error.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Table_LogMirth', @level2type = N'COLUMN', @level2name = N'Mensaje';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Table_LogMirth', @level2type = N'COLUMN', @level2name = N'Mensaje';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido completo del mensaje HL7 en formato ER7 (texto plano con segmentos separados por pipe), que puede incluir datos clínicos del paciente, órdenes, resultados u otros eventos de integración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Table_LogMirth', @level2type = N'COLUMN', @level2name = N'ER7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Table_LogMirth', @level2type = N'COLUMN', @level2name = N'ER7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que el mensaje fue registrado o procesado por el motor de integración Mirth.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Table_LogMirth', @level2type = N'COLUMN', @level2name = N'Fecha';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Table_LogMirth', @level2type = N'COLUMN', @level2name = N'Fecha';
