CREATE TABLE [dbo].[AGCITAESP] (
    [CODAUTONU]   INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]   CHAR (10)                                                                        NOT NULL,
    [FECHACITA]   DATE                                                                             NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI]   CHAR (3)                                                                         NOT NULL,
    [CODACTMED]   CHAR (3)                                                                         NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [FECREGSIS]   DATETIME                                                                         NOT NULL,
    [CITAASIGN]   BIT                                                                              NOT NULL,
    [OBSERVACI]   VARCHAR (MAX)                                                                    NULL,
    [CODUSUASI]   CHAR (20)                                                                        NULL,
    [TEMP]        NCHAR (1)                                                                        NULL,
    [ESTADO]      INT                                                                              NULL,
    [IDCITA]      INT                                                                              NULL,
    [CANCELUSU]   CHAR (20)                                                                        NULL,
    [FECHCANCELA] DATETIME                                                                         NULL,
    [CODCAUCAN]   CHAR (3)                                                                         NULL,
    [OBSCAUCAN]   VARCHAR (200)                                                                    NULL,
    [CODSERIPS]   CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_AGCITAESP] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGCITAESP_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGCITAESP_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGCITAESP_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AGCITAESP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_AGCITAESP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGCITAESP].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGCITAESP].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IPCODPACI]
    ON [dbo].[AGCITAESP]([IPCODPACI] ASC);


GO
ALTER INDEX [IPCODPACI]
    ON [dbo].[AGCITAESP] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio RIPS asociado a la actividad médica (CODACTMED) seleccionado desde formulario modal de demanda insatisfecha en asignación de citas de consulta externa. VARCHAR(20), mapeo a servicios de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el codigo del servicio relacionado a la actividad (CODACTIMED) seleccionado desde el formulario modal de demanda insatisfecha en el formulario de asignacion de citas de consulta exterana ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o justificación de la cancelación de cita. Texto descriptivo del motivo de cancelación. VARCHAR(200).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observación de Cancelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la causa o motivo de cancelación de cita médica. Clasificador de razones de cancelación. CHAR(3), FK referencia a tabla de causas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Cancelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registró la cancelación de la cita. Timestamp del sistema. DATETIME, auditoría de cancelaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'FECHCANCELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Cancelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'FECHCANCELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'FECHCANCELA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del usuario que ejecutó la cancelación de la cita. CHAR(20), trazabilidad de quién cancela.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CANCELUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Usuario Cancelo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CANCELUSU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CANCELUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la cita creada. Referencia a tabla AGASICITA. INT, PK relacionada, vincula cita en espera con cita asignada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cita que se creo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado actual de la cita: 1=En Espera (pendiente asignación), 2=Asignada (confirmada con profesional), 3=Cancelada (anulada). INT, clasificador de ciclo de vida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - En Espera  2 - Asignada  3 - Cancelada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de registro temporal o provisional. NCHAR(1), marca datos no consolidados del sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'TEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Temporal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'TEMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'TEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario del sistema que asignó u originó la cita médica. CHAR(20), auditoría de asignación de citas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que Asigno la Cita Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODUSUASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales sobre la cita, notas clínicas o administrativas. VARCHAR(MAX), campo libre para contexto adicional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera booleana: True=cita en espera ya asignada a profesional, False=cita en espera sin asignar. BIT, estado de asignación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CITAASIGN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'True= si la cita en espera ya fue asignada, False= en espera por asignar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CITAASIGN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CITAASIGN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del sistema cuando se creó el registro de cita. DATETIME, auditoría de creación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha del Registro del Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (médico, odontólogo, especialista) asignado a la cita. VARCHAR(25) MASKED, PII, FK a INPROFSAL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la actividad médica (consulta, procedimiento, terapia) realizada en cita. CHAR(3), FK a AGACTIMED, cataloga tipo de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Actividad Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de especialidad médica (pediatría, cardiología, odontología, etc.). CHAR(3), FK a INESPECIA, clasificador de área clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o identificación del paciente (cédula, documento, ID temporal). VARCHAR(25) MASKED con Identification_Ofuscado, PII, FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que el paciente desea o requiere la cita médica. DATE, fecha solicitada de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'FECHACITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se Deseaba la Cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'FECHACITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'FECHACITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro o unidad funcional de atención donde se realiza la cita. CHAR(10), FK a ADCENATEN, ubicación física de la cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico único (identity) de la tabla AGCITAESP. INT, PK CLUSTERED, identificador secuencial auto-generado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de citas médicas especializadas agendadas por paciente. Contiene la programación, asignación, cancelación y estado de cada cita, incluyendo el profesional de salud, la especialidad, el servicio y las observaciones asociadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP';
