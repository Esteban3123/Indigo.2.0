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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la captura de pantalla tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'screenShotTratamiento';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la captura de pantalla valoración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'screenShotValoracion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'¿es urgencia? (0. no, 1. si)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ODONTOCONTROL', @level2type = N'COLUMN', @level2name = N'ESURGENCIA';

