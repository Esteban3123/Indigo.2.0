CREATE TABLE [dbo].[HCRECEPINTER] (
    [ID]           INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCREFRECEPID] INT         NOT NULL,
    [CORREO]       NCHAR (100) NOT NULL,
    [ESTADO]       INT         NOT NULL,
    [FECHAENV]     DATETIME    NULL,
    CONSTRAINT [PK_HCRECEPINTER] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRECEPINTER_HCREFRECEP] FOREIGN KEY ([HCREFRECEPID]) REFERENCES [dbo].[HCREFRECEP] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de envío del correo electrónico (DATETIME), timestamp que registra cuándo se envió la comunicación, nulo si aún no se ha enviado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'FECHAENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de envío del correo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'FECHAENV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'FECHAENV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del envío de correo electrónico: 1 = Correo no enviado (pendiente), 2 = Correo enviado exitosamente, indicador de gestión de comunicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del envío del correo:  1 ->Correo No enviado  2 -> Correo enviado   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico, email de contacto del paciente, profesional de la salud o centro de atención (NCHAR 100), datos de identificación de contacto PII sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'CORREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electrónico - Email', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'CORREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'CORREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de referencia que enlaza con la tabla HCREFRECEP (FK), establece relación con la cabecera de recepción de comunicaciones, equivalente a ingreso o atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'HCREFRECEPID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id que relaciona el registro de la cabecera: tabla HCREFRECEP', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'HCREFRECEPID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'HCREFRECEPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la tabla HCRECEPINTER, clave primaria que identifica cada registro de comunicación por correo electrónico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de envíos de recepción de interconsultas por correo electrónico. Guarda el historial de notificaciones enviadas a destinatarios (correo) para cada recepción de interconsulta, incluyendo el estado del envío y la fecha en que se realizó.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRECEPINTER';
