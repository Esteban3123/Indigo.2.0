CREATE TABLE [dbo].[AGDISPONSALA] (
    [ID]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]      CHAR (10)     NOT NULL,
    [IDSALA]         INT           NOT NULL,
    [FECHORAIN]      DATETIME      NOT NULL,
    [FECHORAFI]      DATETIME      NOT NULL,
    [CODUSUARIO]     CHAR (20)     NOT NULL,
    [FECHACREACION]  DATETIME      NOT NULL,
    [TURNOBLOQ]      BIT           NOT NULL,
    [MOSTRARWEB]     BIT           NULL,
    [CODPROSAL]      CHAR (20)     NULL,
    [CODESPECI]      CHAR (3)      NULL,
    [IndigoSyncId]   VARCHAR (36)  NULL,
    [BlockingReason] CHAR (4)      NULL,
    [Observation]    VARCHAR (500) NULL,
    CONSTRAINT [PK_AGDISPONSALA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK__AGDISPONS__CODES__32470242] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK__AGDISPONS__CODPR__3152DE09] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_AGDISPONSALA_AGENSALAC] FOREIGN KEY ([IDSALA]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC])
);


GO
ALTER TABLE [dbo].[AGDISPONSALA] NOCHECK CONSTRAINT [FK__AGDISPONS__CODPR__3152DE09];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGDISPONSALA].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_AGDISPONSALA_CODCENATE_IDSALA_FECHORAFI]
    ON [dbo].[AGDISPONSALA]([CODCENATE] ASC, [IDSALA] ASC, [FECHORAFI] ASC)
    INCLUDE([FECHORAIN]);


GO
CREATE NONCLUSTERED INDEX [IX_AGDISPONSALA_IDSALA_FECHORAFI_CODESPECI_CODPROSAL_FECHORAIN]
    ON [dbo].[AGDISPONSALA]([IDSALA] ASC, [FECHORAFI] ASC)
    INCLUDE([CODESPECI], [CODPROSAL], [FECHORAIN]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica (ej: Medicina General, Cardiología, Pediatría). CHAR(3). Referencia la especialidad vinculada a la sala de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo de le especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico, enfermero, especialista). VARCHAR PII ofuscado. Identificación única del proveedor asignado a la sala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de visibilidad de citas en portal web: 1=Mostrar citas disponibles, 0=Ocultar citas. BIT NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar citas En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bloqueo de turnos/citas médicas en la agenda: 0=Desbloqueado (activo), 1=Bloqueado (inactivo). BIT. Control de disponibilidad de consultas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'TURNOBLOQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bloque de Turno Medico  0 = False   1 = True ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'TURNOBLOQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'TURNOBLOQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro de disponibilidad de sala. DATETIME. Auditoría de cuándo se registró la disposición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'la Fecha que creo el registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del usuario que realizó la creación o modificación del registro. CHAR(20). Trazabilidad del operador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODUSUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final del período seleccionado para disponibilidad o bloqueo de la sala. DATETIME. Rango de vigencia (hasta).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial del período seleccionado para disponibilidad o bloqueo de la sala. DATETIME. Rango de vigencia (desde).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Inicial Seleccionado por Usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la sala de atención (consulta, procedimiento, urgencia). INT. Referencia a unidad funcional/espacio físico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'IDSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Sala ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'IDSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'IDSALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (clínica, hospital, consultorio). CHAR(10). Ubicación geográfica/institucional del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Centro Atencion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementado (IDENTITY) de cada registro de disponibilidad de sala en agenda. INT. Clave primaria de la tabla AGDISPONSALA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o nota descriptiva del motivo del bloqueo de agenda (ej: Mantenimiento, Capacitación, Inactividad). VARCHAR(500).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la observacion del bloqueo de la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de bloqueo de agenda (FK a HCMOANULB.CODMOTANU). CHAR(4). Clasificación de razón: ausencia profesional, festivo, cierre temporal, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'BlockingReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo del motivo de bloque de la agenda, correspondiente a la columna CODMOTANU de la tabla HCMOANULB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'BlockingReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'BlockingReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Disponibilidad y bloqueos de salas de atención o agendamiento por centro de atención. Registra los intervalos de tiempo en que una sala está disponible o bloqueada, incluyendo el profesional de la salud asignado, la especialidad y observaciones del bloqueo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de sincronización con el sistema Indigo (GUID), usado para rastrear la integración o replicación del registro entre entornos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'IndigoSyncId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGDISPONSALA', @level2type = N'COLUMN', @level2name = N'IndigoSyncId';
