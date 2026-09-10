CREATE TABLE [dbo].[ADARRIURG] (
    [AUTO]      NUMERIC (18) IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES] CHAR (10)    NULL,
    [AUTOARRIB] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_ADARRIURG] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_ADARRIURG_HCARRURGC] FOREIGN KEY ([AUTOARRIB]) REFERENCES [dbo].[HCARRURGC] ([AUTOARRIB])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) que referencia el registro de arribo/llegada en urgencias de la tabla HCARRURGC. Clave foránea para vincular detalles de atención urgente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG', @level2type = N'COLUMN', @level2name = N'AUTOARRIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG', @level2type = N'COLUMN', @level2name = N'AUTOARRIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG', @level2type = N'COLUMN', @level2name = N'AUTOARRIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente a la institución. Identificador alfanumérico del episodio de atención/admisión, clave para asociar eventos de urgencia con el registro del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la tabla. Clave primaria que genera automáticamente cada registro de arribo en urgencias para garantizar unicidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de arribo o llegada de pacientes en urgencias. Vincula cada ingreso de urgencias con el evento de arribo correspondiente, permitiendo controlar el momento en que el paciente llega al servicio de urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADARRIURG';
