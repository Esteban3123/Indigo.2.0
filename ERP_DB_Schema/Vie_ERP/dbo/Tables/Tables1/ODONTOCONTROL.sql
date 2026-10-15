CREATE TABLE [dbo].[ODONTOCONTROL] (
    [ID]                    INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]             CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]             CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]             VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]             CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]             NCHAR (10)                                                                       NOT NULL,
    [CODPROSAL]             CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHAREG]              DATETIME                                                                         NOT NULL,
    [ESURGENCIA]            BIT                                                                              NOT NULL,
    [OBSERVACIONESGENERA]   VARCHAR (MAX)                                                                    NULL,
    [IDRIASCUPS]            INT                                                                              NULL,
    [screenShotValoracion]  VARBINARY (MAX)                                                                  NULL,
    [screenShotTratamiento] VARBINARY (MAX)                                                                  NULL,
    CONSTRAINT [PK_ODONTOCONTROL] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ODONTOCONTROL_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ODONTOCONTROL_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ODONTOCONTROL_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ODONTOCONTROL_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ODONTOCONTROL_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_ODONTOCONTROL_RIASCUPS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID])
);


GO
ALTER TABLE [dbo].[ODONTOCONTROL] NOCHECK CONSTRAINT [FK_ODONTOCONTROL_ADCENATEN];


GO
ALTER TABLE [dbo].[ODONTOCONTROL] NOCHECK CONSTRAINT [FK_ODONTOCONTROL_ADINGRESO];


GO
ALTER TABLE [dbo].[ODONTOCONTROL] NOCHECK CONSTRAINT [FK_ODONTOCONTROL_INPACIENT];


GO
ALTER TABLE [dbo].[ODONTOCONTROL] NOCHECK CONSTRAINT [FK_ODONTOCONTROL_INPROFSAL];


GO
ALTER TABLE [dbo].[ODONTOCONTROL] NOCHECK CONSTRAINT [FK_ODONTOCONTROL_INUNIFUNC];


GO
ALTER TABLE [dbo].[ODONTOCONTROL] NOCHECK CONSTRAINT [FK_ODONTOCONTROL_RIASCUPS];


GO
CREATE NONCLUSTERED INDEX [IX_ODONTOCONTROL_1]
    ON [dbo].[ODONTOCONTROL]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la captura de pantalla tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'screenShotTratamiento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la captura de pantalla valoración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'screenShotValoracion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo en la cual me relaciona el ID de la RIASCUPS que tiene la agenda medica, si la actividad de agendamiento No tiene ó no aplica a RIAS pues guarda Dbnull.Value.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se guarda la observaciones que hizo el medico por todo el odontograma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'OBSERVACIONESGENERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿es urgencia? (0. no, 1. si)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'ESURGENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha creacion del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'FECHAREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo profesional que registra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de folio HC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'relacion con unidades funcionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de  la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'ID';

