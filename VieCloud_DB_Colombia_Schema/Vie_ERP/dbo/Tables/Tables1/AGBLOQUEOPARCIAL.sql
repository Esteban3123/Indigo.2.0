CREATE TABLE [dbo].[AGBLOQUEOPARCIAL] (
    [ID]               INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDAGENDA]         INT            NOT NULL,
    [FECHAINIBLOQUEO]  DATETIME       NOT NULL,
    [FECHAFINBLOQUEO]  DATETIME       NOT NULL,
    [ESTADO]           TINYINT        NOT NULL,
    [FECHAREGISTRO]    DATETIME       NOT NULL,
    [CODUSUARIO]       CHAR (20)      NOT NULL,
    [OBSERVACION]      VARCHAR (3000) NULL,
    [CODUSUARIODESBLO] CHAR (20)      NULL,
    [FECHADESBLO]      DATETIME       NULL,
    [BlockingReason]   INT            NULL,
    [ScheduleType]     INT            NOT NULL,
    CONSTRAINT [PK_AGBLOQUEOPARCIAL] PRIMARY KEY CLUSTERED ([ID] ASC)
);




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de bloqueo de agenda (FK a HCMOANULB.CODMOTANU); razón por la cual se bloquea parcialmente la disponibilidad horaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'BlockingReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo del motivo de bloque de la agenda, correspondiente a la columna CODMOTANU de la tabla HCMOANULB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'BlockingReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'BlockingReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de desbloqueo; momento en que se libera el bloqueo parcial de la agenda (datetime, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHADESBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de desbloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHADESBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHADESBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario que desbloquea (PII Identification_Ofuscado); quien autoriza la liberación del bloqueo parcial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'CODUSUARIODESBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que desbloquea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'CODUSUARIODESBLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'CODUSUARIODESBLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas o comentarios sobre el motivo del bloqueo parcial; razón clínica, administrativa o logística del bloqueo (varchar 3000, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo del bloqueo parcial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del usuario creador del bloqueo (PII Identification_Ofuscado); quien registra el bloqueo parcial de la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario que creo el registro de bloqueo ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de bloqueo (datetime); auditoria de cuándo se generó el bloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del bloqueo (tinyint): 1=Activo, 2=Inactivo; indica si el bloqueo está vigente o ha sido levantado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del registro  1 - bloqueo Activo  2 - bloqueo Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final del bloqueo parcial (datetime); momento en que termina el período bloqueado en la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHAFINBLOQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora final del bloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHAFINBLOQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHAFINBLOQUEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial del bloqueo parcial (datetime); momento en que comienza el período bloqueado en la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHAINIBLOQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora inicial del bloqueo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHAINIBLOQUEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'FECHAINIBLOQUEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la agenda a bloquear parcialmente (FK a tabla AGENDA); referencia a la disponibilidad horaria afectada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'IDAGENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la agenda a bloquear parcialmente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'IDAGENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'IDAGENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la transacción de bloqueo (int identity); clave primaria autoincremental', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de agenda bloqueada (tinyint): 1=Agenda ambulatoria (consulta externa), 2=Disponibilidad de sala de apoyo diagnóstico (DX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'ScheduleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el bloqueo pertenece a una agenda abulatoria o a una disponibilidad de sala de apoyo diagnostico

1 - Agenda ambulatoria
2 - Disponibilidad de sala de apoyo DX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'ScheduleType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL', @level2type = N'COLUMN', @level2name = N'ScheduleType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bloqueos parciales de agendas médicas: registra los períodos en que una agenda queda bloqueada temporalmente, incluyendo quién la bloqueó, el motivo, y si fue desbloqueada posteriormente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGBLOQUEOPARCIAL';
