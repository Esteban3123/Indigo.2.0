CREATE TABLE [dbo].[AGASICITE] (
    [CODAUTONU] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODAUTOCI] INT                                                                              NOT NULL,
    [CODESPECI] CHAR (3)                                                                         NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHORAIN] DATETIME                                                                         NOT NULL,
    [FECHORAFI] DATETIME                                                                         NOT NULL,
    [CODIGOCON] CHAR (6)                                                                         NOT NULL,
    [CODACTMED] CHAR (3)                                                                         NOT NULL,
    [CODTIPSOL] CHAR (1)                                                                         NOT NULL,
    [CODTIPCIT] CHAR (1)                                                                         NOT NULL,
    [CODESTCIT] CHAR (1)                                                                         NOT NULL,
    [CITAEXTRA] BIT                                                                              NOT NULL,
    [OBSERVACI] NVARCHAR (MAX)                                                                   NULL,
    [ESTENVSMS] BIT                                                                              NOT NULL,
    [CODUSUASI] CHAR (20)                                                                        NOT NULL,
    [FECREGSIS] DATETIME                                                                         NOT NULL,
    [CODUSUARI] CHAR (20)                                                                        NOT NULL,
    [FECHELIMI] DATETIME                                                                         NOT NULL,
    [CODCAUCAN] CHAR (3)                                                                         NOT NULL,
    [OBSCAUCAN] NCHAR (100)                                                                      NULL,
    [OBSCITPRE] NVARCHAR (500)                                                                   NULL,
    [FECINICIT] DATETIME                                                                         NULL,
    [CODDIAGNO] CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    CONSTRAINT [PK_AGASICITE] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGASICITE_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGASICITE_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGASICITE_AGCAUCANC] FOREIGN KEY ([CODCAUCAN]) REFERENCES [dbo].[AGCAUCANC] ([CODCAUCAN]),
    CONSTRAINT [FK_AGASICITE_AGCONSULT] FOREIGN KEY ([CODIGOCON], [CODCENATE]) REFERENCES [dbo].[AGCONSULT] ([CODIGOCON], [CODCENATE]),
    CONSTRAINT [FK_AGASICITE_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_AGASICITE_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AGASICITE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_AGASICITE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGASICITE].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGASICITE].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[AGASICITE].[CODDIAGNO]
    WITH (LABEL = 'Confidential - Health', INFORMATION_TYPE = 'Health');




GO
CREATE NONCLUSTERED INDEX [IX_AGASICITE__IPCODPACI__INC__CODACTMED__CODAUTONU__CODCAUCAN__CODESPECI__CODPROSAL__CODUSUASI__FECHORAFI__FECHORAIN__OBSCAUCAN]
    ON [dbo].[AGASICITE]([IPCODPACI] ASC)
    INCLUDE([CODAUTONU], [CODESPECI], [CODPROSAL], [FECHORAIN], [FECHORAFI], [CODACTMED], [CODUSUASI], [CODCAUCAN], [OBSCAUCAN]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico CIE-10, identificación de la enfermedad o condición clínica asociada a la cita (VARCHAR(4), referencia a INDIAGNOS, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del  diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de inicio de la cita asignada tras modificación a estado preasignada al ser eliminada (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECINICIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora de la Cita Asignada Luego de Ser Modificada al estado preasignada al ser Eliminada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECINICIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECINICIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones de la cita en estado preasignada al momento de su eliminación o cancelación (NVARCHAR(500))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'OBSCITPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion de la Cita en Estado Preasignada al ser Eliminada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'OBSCITPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'OBSCITPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación detallada del motivo de cancelación de la cita médica (NCHAR(100))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la observacion de la cusa de cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la causa de cancelación de la cita, motivo por el cual se cancela la atención (CHAR(3), referencia a AGCAUCANC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Causa de Cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora exacta en que fue eliminada o cancelada la cita médica del sistema (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECHELIMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Fecha y hora a la que se elimino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECHELIMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECHELIMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario del sistema que realizó la eliminación o cancelación de la cita médica (CHAR(20))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que elimino la Cita Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se registró la cita en el sistema, marca de creación del registro (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Fecha y hora en que se registro la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario del sistema que asignó o programó la cita médica al paciente (CHAR(20))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario que Asigno la Cita Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODUSUASI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODUSUASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano si se envió notificación por mensaje de texto SMS al paciente sobre la cita (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'ESTENVSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saber si se envio el mensaje de texto al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'ESTENVSMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'ESTENVSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de observaciones generales, notas clínicas o administrativas adicionales de la cita (NVARCHAR(MAX))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para las Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano que define si la cita es extraordinaria o fuera del horario regular (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CITAEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Es Cita Extra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CITAEXTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CITAEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del estado de la cita: 0=Asignada, 1=Cumplida, 2=Incumplida (CHAR(1))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODESTCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Estado de la Cita  0. Asignada  1. Cumplida  2. Incumplida  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODESTCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODESTCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de cita médica: 0=Primera Vez, 1=Control, 2=Postoperatorio (CHAR(1))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODTIPCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Tipo de Cita Médica.    0. Primera Vez  1. Control  2. Pos Operatorio  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODTIPCIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODTIPCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de solicitud de cita: 0=Presencial, 1=Telefónica (CHAR(1))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODTIPSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Solicitud.    0. Presencial  1. Telefónica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODTIPSOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODTIPSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la actividad médica o procedimiento asociado a la cita (CHAR(3), referencia a AGACTIMED)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Actividad Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODACTMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consultorio o sala donde se programa la cita (CHAR(6), referencia a AGCONSULT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODIGOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consultorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODIGOCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODIGOCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora final o de cierre programada de la cita médica (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora final de la Cita Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECHORAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora inicial programada de la cita médica (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora inicial de la Cita Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECHORAIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud, médico o especialista que atiende la cita (VARCHAR(20), referencia a INPROFSAL, enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente, equivalente a cédula/documento/identificación del paciente (VARCHAR(25), referencia a INPACIENT, PII enmascarado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro o institución de atención donde se programa la cita (CHAR(10), referencia a ADCENATEN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la especialidad médica o disciplina del profesional que atiende (CHAR(3), referencia a INESPECIA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODESPECI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código autonumérico de la tabla de citas original, referencia interna (INT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODAUTOCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Autonumerico de la tabla de citas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODAUTOCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODAUTOCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico único identificador del registro de cita en la tabla AGASICITE (INT, clave primaria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Citas médicas asignadas en el sistema de agendamiento. Registra cada cita con su paciente, profesional, especialidad, centro de atención, horario, estado y motivo de cancelación si aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE';
