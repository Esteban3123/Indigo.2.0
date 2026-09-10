CREATE TABLE [dbo].[AGAGEMEDC] (
    [CODAUTONU]             INT              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FECHORAIN]             DATETIME         NOT NULL,
    [FECHORAFI]             DATETIME         NOT NULL,
    [CODPROSAL]             CHAR (20)        NULL,
    [CODCENATE]             CHAR (10)        NOT NULL,
    [TIPAGEMED]             CHAR (1)         NOT NULL,
    [CODIGOCON]             CHAR (6)         NULL,
    [CODESPECI]             CHAR (3)         NULL,
    [CODUSUASI]             CHAR (20)        NOT NULL,
    [FECREGSIS]             DATETIME         NOT NULL,
    [AGENSALAC]             INT              NULL,
    [CODIGOUBIC]            CHAR (6)         NULL,
    [TURNOEMER]             BIT              NULL,
    [TURNOBLOQ]             BIT              NULL,
    [IDAGENDA]              INT              NULL,
    [DISPONIBILIDAD]        BIT              NULL,
    [IDPADREDISPONIBILIDAD] INT              NULL,
    [JORNADA]               INT              NULL,
    [MOSTRARWEB]            BIT              NULL,
    [IdMassiveAgenda]       UNIQUEIDENTIFIER NULL,
    [ProgrammingSummary]    VARCHAR (1000)   NULL,
    [IndigoSyncId]          VARCHAR (36)     NULL,
    [BlockingReason]        CHAR (4)         NULL,
    [Observation]           VARCHAR (500)    NULL,
    CONSTRAINT [PK_AGAGEMEDC] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGAGEMEDC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGAGEMEDC_AGCONSULT] FOREIGN KEY ([CODIGOCON], [CODCENATE]) REFERENCES [dbo].[AGCONSULT] ([CODIGOCON], [CODCENATE]),
    CONSTRAINT [FK_AGAGEMEDC_AGENSALAC] FOREIGN KEY ([AGENSALAC]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC]),
    CONSTRAINT [FK_AGAGEMEDC_IDAGENDA] FOREIGN KEY ([IDAGENDA]) REFERENCES [dbo].[AGAGEMEDC] ([CODAUTONU]),
    CONSTRAINT [FK_AGAGEMEDC_IDDISPONIBILIDAD] FOREIGN KEY ([IDPADREDISPONIBILIDAD]) REFERENCES [dbo].[AGAGEMEDC] ([CODAUTONU]),
    CONSTRAINT [FK_AGAGEMEDC_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AGAGEMEDC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[AGAGEMEDC] NOCHECK CONSTRAINT [FK_AGAGEMEDC_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGAGEMEDC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [idx_IdMassiveAgenda_AGAGEMEDC]
    ON [dbo].[AGAGEMEDC]([IdMassiveAgenda] ASC);


GO
ALTER INDEX [idx_IdMassiveAgenda_AGAGEMEDC]
    ON [dbo].[AGAGEMEDC] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_AGAGEMEDC_CODPROSAL_FECHORAIN_FECHORAFI_CODAUTONU]
    ON [dbo].[AGAGEMEDC]([CODPROSAL] ASC, [FECHORAIN] ASC, [FECHORAFI] ASC)
    INCLUDE([CODAUTONU]);


GO
CREATE NONCLUSTERED INDEX [IX_ListarTurnosMedicos]
    ON [dbo].[AGAGEMEDC]([CODPROSAL] ASC)
    INCLUDE([CODIGOCON], [FECHORAFI], [FECHORAIN], [TIPAGEMED]);


GO
CREATE NONCLUSTERED INDEX [IX_AGAGEMEDC_CODPROSAL_CODCENATE_FECHORAIN_FECHORAFI_CODAUTONU_TIPAGEMED]
    ON [dbo].[AGAGEMEDC]([CODPROSAL] ASC, [CODCENATE] ASC, [FECHORAIN] ASC, [FECHORAFI] ASC)
    INCLUDE([CODAUTONU], [TIPAGEMED]);


GO
CREATE NONCLUSTERED INDEX [IX_AGAGEMEDC_CODCENATE_FECHORAIN_FECHORAFI_CODAUTONU_CODPROSAL_TIPAGEMED]
    ON [dbo].[AGAGEMEDC]([CODCENATE] ASC, [FECHORAIN] ASC, [FECHORAFI] ASC)
    INCLUDE([CODAUTONU], [CODPROSAL], [TIPAGEMED]);


GO
CREATE NONCLUSTERED INDEX [IX_AGAGEMEDC_CODCENATE_CODIGOCON_CODAUTONU_CODPROSAL_FECHORAFI_FECHORAIN]
    ON [dbo].[AGAGEMEDC]([CODCENATE] ASC, [CODIGOCON] ASC)
    INCLUDE([CODAUTONU], [CODPROSAL], [FECHORAFI], [FECHORAIN]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas sobre el bloqueo de la agenda médica, comentarios del cierre o razón adicional del bloqueo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la observacion del bloqueo de la agenda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de bloque de la agenda (FK a HCMOANULB.CODMOTANU): anulación, falta de disponibilidad, conflicto de horario, otros motivos de cierre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'BlockingReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo del motivo de bloque de la agenda, correspondiente a la columna CODMOTANU de la tabla HCMOANULB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'BlockingReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'BlockingReason';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resumen de la programación cuando la agenda se carga masivamente (PBI 16723), descripción del lote o serie de agendamientos registrados en masa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'ProgrammingSummary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para guardar el resumen de la programación cuando la agenda se graba masivamente (PBI 16723 -> Guardar desde formulario agenda en serie)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'ProgrammingSummary';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'ProgrammingSummary';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (GUID) de la agenda masiva para agrupar lotes de agendamientos cargados en serie desde formulario (PBI 16723).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IdMassiveAgenda';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para guardar el identificador de la agenda masiva (PBI 16723 -> Guardar desde formulario agenda en serie)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IdMassiveAgenda';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IdMassiveAgenda';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de visualización en portal web: 1 = agenda visible al paciente, 0 = agenda oculta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar Agenda medica En Web  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'MOSTRARWEB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Jornada laboral de la agenda: 1=Mañana, 2=Tarde, 3=Nocturna. Define horario del turno médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'JORNADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la jornada de la agenda medica :   1 - Jornada Mañana  2 - Jornada Tarde  3 - Jornada Nocturna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'JORNADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'JORNADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a AGAGEMEDC.CODAUTONU: agenda médica base que activa disponibilidad de quirófano o médico especialista para programar cirugías, relación padre-hijo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IDPADREDISPONIBILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proceso de cirugías , Id de la agenda medica base para adicionar disponibilidad a un medico y asi poder programar cirugia    Relacion con la misma tabla (AGAGEMEDC - CODAUTONU)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IDPADREDISPONIBILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IDPADREDISPONIBILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de disponibilidad para proceso quirúrgico: marca si se habilitó disponibilidad en médico o sala para programación de cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'DISPONIBILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Proceso de cirujias , adicionar disponibilidad a un medico para poder programar cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'DISPONIBILIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'DISPONIBILIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a AGAGEMEDC.CODAUTONU: relaciona disponibilidad de quirófano con agenda del profesional para cirugías (solo TIPAGEMED=0).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IDAGENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla AGAGEMEDC que relaciona la disponibilidad de la sala   con la agenda del profesional para cirugías.  Solo se guarda para Agenda medica de Quirofanos (TIPAGEMED = 0 Profesional)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IDAGENDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IDAGENDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario: 1=turno bloqueado (no se pueden asignar pacientes), 0=turno disponible. Bloqueo de agenda médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'TURNOBLOQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para determinar si el turno medico esta bloqueado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'TURNOBLOQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'TURNOBLOQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador binario: 1=turno de emergencia/urgencia, 0=turno programado rutinario. Identifica atenciones en servicio de urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'TURNOEMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para determinar si el turno es de emergencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'TURNOEMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'TURNOEMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de ubicación física donde se realiza la agenda (consultorio, sala, quirófano), FK a tabla de ubicaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODIGOUBIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la ubicación donde se realiza la agenda (consultorio o sala)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODIGOUBIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODIGOUBIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo/código de la sala de atención (quirófano, apoyo diagnóstico), FK a AGENSALAC.CODCONCEC cuando TIPAGEMED=1 o 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'AGENSALAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'consecutivo de la sala que utiliza si es de tipo Qx o AD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'AGENSALAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'AGENSALAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la agenda médica en el sistema (DATETIME), marca creación del agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Fecha y hora en que se registro la Agenda Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que asignó o creó la agenda médica en el sistema, profesional o administrador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que Asigno la Agenda Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODUSUASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica (cardiología, pediatría, etc.), FK a INESPECIA.CODESPECI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consultorio o sala donde se realiza la atención, ubicación física de la consulta, FK a AGCONSULT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODIGOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la ubicación donde se realiza la agenda (consultorio o sala)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODIGOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODIGOCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de agendamiento: 0=Profesional (consulta), 1=Programación Quirófano, 2=Apoyo Diagnóstico (laboratorio/imagen).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'TIPAGEMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Agendamiento   0:Profesional   1:Programación Quirofano 2:Apoyo Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'TIPAGEMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'TIPAGEMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención (institución, sede, clínica), FK a ADCENATEN.CODCENATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud [OBSOLETO - PBI 13603], FK a INPROFSAL.CODPROSAL. Campo heredado, no usar en desarrollos nuevos. Identificación PII ofuscada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud  -----------------------------------------    CAMPO OBSOLETO -> PBI 13603', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final del agendamiento médico (DATETIME), termina el slot de consulta/procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora final de la Agenda Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial del agendamiento médico (DATETIME), comienza el slot de consulta/procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora inicial de la Agenda Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado (PK) de la agenda médica, clave primaria de AGAGEMEDC para todas las relaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agenda médica: bloques de tiempo disponibles u ocupados de los profesionales de la salud por centro de atención. Permite gestionar la programación de citas, turnos de urgencias, bloqueos y disponibilidad para agendamiento web o masivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de sincronización con sistemas externos o procesos de integración (GUID en formato texto).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IndigoSyncId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGAGEMEDC', @level2type = N'COLUMN', @level2name = N'IndigoSyncId';
