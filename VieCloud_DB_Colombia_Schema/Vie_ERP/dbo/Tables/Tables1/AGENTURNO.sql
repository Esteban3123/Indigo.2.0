CREATE TABLE [dbo].[AGENTURNO] (
    [ID]         INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]  INT      NOT NULL,
    [NUMTURNO]   INT      NULL,
    [TURHORAINI] DATETIME NULL,
    [TURHORAFIN] DATETIME NULL,
    CONSTRAINT [PK_AGENTURNO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGENTURNO_AGENSALAC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de fin del turno de atención; timestamp de cierre de agenda en sala de consulta o procedimiento (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'TURHORAFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de Fin del Turno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'TURHORAFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'TURHORAFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio del turno de atención; timestamp de apertura de agenda en sala de consulta o procedimiento (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'TURHORAINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de Inicio del Turno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'TURHORAINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'TURHORAINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del turno asignado al paciente (1, 2, 3, 4, 5 o 6); orden de atención en sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'NUMTURNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del turno (1,2,3,4,5 o 6)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'NUMTURNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'NUMTURNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo de la sala o unidad funcional de atención; referencia FK a AGENSALAC para identificar centro de consulta, urgencia o procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del concecutivo de la Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) de registro de turno de agenda en sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de turnos de agendamiento: guarda los turnos asignados a cada cita o bloque de agenda, con su horario de inicio y fin. Permite controlar el orden de atención y la disponibilidad de turnos en el sistema de agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURNO';
