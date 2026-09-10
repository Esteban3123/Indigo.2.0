CREATE TABLE [dbo].[AGTRAAGEN] (
    [AUTONUMER]                INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODAUTONU]                INT         NOT NULL,
    [CODCENATE]                CHAR (10)   NOT NULL,
    [FECHINORI]                DATETIME    NOT NULL,
    [FECHFIORI]                DATETIME    NOT NULL,
    [FECHINMOD]                DATETIME    NOT NULL,
    [FECHFIMOD]                DATETIME    NOT NULL,
    [CODUSUMOD]                CHAR (20)   NOT NULL,
    [FECREGSIS]                DATETIME    NOT NULL,
    [ConsultationCode]         VARCHAR (6) NULL,
    [ModifiedConsultationCode] VARCHAR (6) NULL,
    CONSTRAINT [PK_AGTRAAGEN] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGTRAAGEN_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consultorio destino tras modificación de agenda; identifica la unidad funcional donde se reasignó la cita médica (VARCHAR 6, PII-Consulta).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'ModifiedConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del consultorio al cual fue modificada la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'ModifiedConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'ModifiedConsultationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consultorio original de la agenda médica; identificador de la unidad funcional de atención primaria (VARCHAR 6, PII-Consulta).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'ConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del consultorio original de la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'ConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'ConsultationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro en el sistema ERP; timestamp de auditoría de creación del agendamiento (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Registro del sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario profesional de salud que modificó la agenda; identificación del personal que realizó cambios (CHAR 20, PII-Usuario).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que Modifico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final de la agenda tras modificación; límite temporal actualizado de la cita médica (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHFIMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Final de la Agenda Despues de Ser Modificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHFIMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHFIMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial de la agenda tras modificación; inicio temporal actualizado de la atención o consulta (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHINMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Inicial de la Agenda Despues de Ser Modificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHINMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHINMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final original de la agenda; límite temporal inicial de la cita médica antes de cambios (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHFIORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Final Original de la Agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHFIORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHFIORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial original de la agenda; inicio temporal de la cita médica antes de modificaciones (DATETIME).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHINORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Inicial Original de la Agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHINORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'FECHINORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención; identificador de la institución/unidad funcional donde se gestiona la agenda (CHAR 10, FK→ADCENATEN, PII-Centro).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico de relación con la agenda médica; clave externa hacia tabla de programación de consultas (INT).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la relacion con la Agenda Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico primario de la tabla; identificador único de cada registro de agendamiento modificado (INT IDENTITY, PK).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'AUTONUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'AUTONUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN', @level2type = N'COLUMN', @level2name = N'AUTONUMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de rangos horarios de agendamiento médico. Guarda los bloques de tiempo originales y modificados disponibles para la programación de citas, por centro de atención y usuario que realiza el cambio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRAAGEN';
