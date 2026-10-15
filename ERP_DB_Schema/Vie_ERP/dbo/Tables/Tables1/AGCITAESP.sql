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
    CONSTRAINT [FK_AGCITAESP_AGASICITA] FOREIGN KEY ([IDCITA]) REFERENCES [dbo].[AGASICITA] ([CODAUTONU]),
    CONSTRAINT [FK_AGCITAESP_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AGCITAESP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_AGCITAESP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[AGCITAESP] NOCHECK CONSTRAINT [FK_AGCITAESP_ADCENATEN];


GO
ALTER TABLE [dbo].[AGCITAESP] NOCHECK CONSTRAINT [FK_AGCITAESP_AGACTIMED];


GO
ALTER TABLE [dbo].[AGCITAESP] NOCHECK CONSTRAINT [FK_AGCITAESP_AGASICITA];


GO
ALTER TABLE [dbo].[AGCITAESP] NOCHECK CONSTRAINT [FK_AGCITAESP_INESPECIA];


GO
ALTER TABLE [dbo].[AGCITAESP] NOCHECK CONSTRAINT [FK_AGCITAESP_INPACIENT];


GO
ALTER TABLE [dbo].[AGCITAESP] NOCHECK CONSTRAINT [FK_AGCITAESP_INPROFSAL];


GO
CREATE NONCLUSTERED INDEX [IPCODPACI]
    ON [dbo].[AGCITAESP]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del servicio relacionado a la actividad (CODACTIMED) seleccionado desde el formulario modal de demanda insatisfecha en el formulario de asignacion de citas de consulta exterana ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación de Cancelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'OBSCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Cancelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODCAUCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Cancelación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'FECHCANCELA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Usuario Cancelo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CANCELUSU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la cita que se creo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - En Espera  2 - Asignada  3 - Cancelada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Temporal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'TEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario que Asigno la Cita Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODUSUASI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'True= si la cita en espera ya fue asignada, False= en espera por asignar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CITAASIGN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Registro del Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Actividad Medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se Deseaba la Cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'FECHACITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODAUTONU';

