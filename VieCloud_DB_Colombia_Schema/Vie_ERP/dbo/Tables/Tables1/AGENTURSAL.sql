CREATE TABLE [dbo].[AGENTURSAL] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDSALA]      INT       NOT NULL,
    [IDEQUIPOTRA] INT       NOT NULL,
    [NUMTURNO]    INT       NOT NULL,
    [FECHAINI]    DATETIME  NULL,
    [FECHAFIN]    DATETIME  NULL,
    [FECCREREG]   DATETIME  NULL,
    [USUCREREG]   CHAR (20) NULL,
    CONSTRAINT [PK_AGENTURSAL] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identificación de quien creó el registro de agenda (CHAR 20, auditoria, PII Identification_Ofuscado, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'USUCREREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'USUCREREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'USUCREREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de agenda en el sistema (DATETIME, auditoria, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'FECCREREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creación del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'FECCREREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'FECCREREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de finalización de la agenda del turno, marca cierre de disponibilidad (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'FECHAFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de fin de la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'FECHAFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'FECHAFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de la agenda del turno, marca comienzo de disponibilidad (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'FECHAINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'FECHAINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'FECHAINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del turno en la sala para identificar orden de atención (INT, rango 1-N)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'NUMTURNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número del Turno de sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'NUMTURNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'NUMTURNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del equipo de tratamiento o recurso asignado al turno (INT, FK referencial)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'IDEQUIPOTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del equipo de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'IDEQUIPOTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'IDEQUIPOTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la sala o unidad funcional de atención donde se agenda el turno (INT, FK referencial)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'IDSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'IDSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'IDSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la agenda del turno de sala, clave primaria (INT, IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la agenda del turno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de turnos asignados a salas y equipos de trabajo en el módulo de agendamiento. Controla qué turno ocupa cada sala o equipo durante un intervalo de tiempo determinado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENTURSAL';
