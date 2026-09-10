CREATE TABLE [dbo].[AGTRACITA] (
    [AUTONUMER]                INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODAUTONU]                INT         NOT NULL,
    [CODCENATE]                CHAR (10)   NOT NULL,
    [CODPROORI]                CHAR (20)   NOT NULL,
    [CODPROMOD]                CHAR (20)   NOT NULL,
    [FECHINORI]                DATETIME    NOT NULL,
    [FECHFIORI]                DATETIME    NOT NULL,
    [FECHINMOD]                DATETIME    NOT NULL,
    [FECHFIMOD]                DATETIME    NOT NULL,
    [CODUSUMOD]                CHAR (20)   NOT NULL,
    [FECREGSIS]                DATETIME    NOT NULL,
    [ConsultationCode]         VARCHAR (6) NULL,
    [ModifiedConsultationCode] VARCHAR (6) NULL,
    CONSTRAINT [PK_AGTRACITA] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGTRACITA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(6) del consultorio modificado donde se reasignó la cita médica; identifica la nueva ubicación física de atención tras cambio de agenda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'ModifiedConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del consultorio al cual fue modificada la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'ModifiedConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'ModifiedConsultationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(6) del consultorio original asignado inicialmente a la cita médica; identifica la ubicación física de atención programada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'ConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del consultorio original de la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'ConsultationCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'ConsultationCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora DATETIME del registro en el sistema; marca el momento de creación del auditaje de la cita en base de datos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Registro del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) del usuario profesional de la salud que realizó la modificación de la cita; auditaje de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que Modifico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final DATETIME de la cita después de ser modificada o reasignada; hora de fin de atención actualizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECHFIMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Final de la Cita Despues de Ser Modificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECHFIMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECHFIMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial DATETIME de la cita después de ser modificada o reasignada; hora de inicio de atención actualizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECHINMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Inicial de la Cita Despues de Ser Modificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECHINMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECHINMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final DATETIME original de la cita programada antes de cualquier modificación; hora de fin de atención inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECHFIORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Final Original de la Cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECHFIORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECHFIORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial DATETIME original de la cita programada antes de cualquier modificación; hora de inicio de atención inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECHINORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y Hora Inicial Original de la Cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECHINORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'FECHINORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) del profesional de salud reasignado para atender la cita tras modificación; identifica el nuevo médico/especialista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODPROMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional con el que tiene la cita luego de ser reasignada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODPROMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODPROMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(20) del profesional de salud asignado originalmente para atender la cita; identifica el médico/especialista inicial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODPROORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional con el que tiene la cita fue asignada originalmente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODPROORI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODPROORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CHAR(10) del centro de atención o unidad funcional donde se registra la cita; FK a ADCENATEN.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico INT relación con la tabla AGCITA; vincula el auditaje a la cita médica original.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la relacion con la Cita Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico INT PRIMARY KEY de la tabla AGTRACITA; identificador único del auditaje o trazabilidad de la cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'AUTONUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'AUTONUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA', @level2type = N'COLUMN', @level2name = N'AUTONUMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de modificaciones o trazabilidad de citas agendadas. Guarda el historial de cambios realizados sobre una cita médica, incluyendo los datos originales y los datos modificados, el usuario que realizó el cambio y las fechas involucradas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGTRACITA';
