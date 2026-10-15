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
ALTER TABLE [dbo].[AGASICITE] NOCHECK CONSTRAINT [FK_AGASICITE_ADCENATEN];


GO
ALTER TABLE [dbo].[AGASICITE] NOCHECK CONSTRAINT [FK_AGASICITE_AGACTIMED];


GO
ALTER TABLE [dbo].[AGASICITE] NOCHECK CONSTRAINT [FK_AGASICITE_AGCAUCANC];


GO
ALTER TABLE [dbo].[AGASICITE] NOCHECK CONSTRAINT [FK_AGASICITE_AGCONSULT];


GO
ALTER TABLE [dbo].[AGASICITE] NOCHECK CONSTRAINT [FK_AGASICITE_INDIAGNOS];


GO
ALTER TABLE [dbo].[AGASICITE] NOCHECK CONSTRAINT [FK_AGASICITE_INESPECIA];


GO
ALTER TABLE [dbo].[AGASICITE] NOCHECK CONSTRAINT [FK_AGASICITE_INPACIENT];


GO
ALTER TABLE [dbo].[AGASICITE] NOCHECK CONSTRAINT [FK_AGASICITE_INPROFSAL];


GO
CREATE NONCLUSTERED INDEX [IX_AGASICITE__IPCODPACI__INC__CODACTMED__CODAUTONU__CODCAUCAN__CODESPECI__CODPROSAL__CODUSUASI__FECHORAFI__FECHORAIN__OBSCAUCAN]
    ON [dbo].[AGASICITE]([IPCODPACI] ASC)
    INCLUDE([CODAUTONU], [CODESPECI], [CODPROSAL], [FECHORAIN], [FECHORAFI], [CODACTMED], [CODUSUASI], [CODCAUCAN], [OBSCAUCAN]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del  diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora de la Cita Asignada Luego de Ser Modificada al estado preasignada al ser Eliminada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECINICIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion de la Cita en Estado Preasignada al ser Eliminada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'OBSCITPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la observacion de la cusa de cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Causa de Cancelacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la Fecha y hora a la que se elimino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECHELIMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que elimino la Cita Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Guarda la Fecha y hora en que se registro la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que Asigno la Cita Medica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODUSUASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saber si se envio el mensaje de texto al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'ESTENVSMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para las Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Es Cita Extra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CITAEXTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Estado de la Cita  0. Asignada  1. Cumplida  2. Incumplida  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODESTCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Tipo de Cita Médica.    0. Primera Vez  1. Control  2. Pos Operatorio  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODTIPCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Solicitud.    0. Presencial  1. Telefónica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODTIPSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Actividad Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consultorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODIGOCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora final de la Cita Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECHORAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y hora inicial de la Cita Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'FECHORAIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Autonumerico de la tabla de citas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODAUTOCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGASICITE', @level2type = N'COLUMN', @level2name = N'CODAUTONU';

