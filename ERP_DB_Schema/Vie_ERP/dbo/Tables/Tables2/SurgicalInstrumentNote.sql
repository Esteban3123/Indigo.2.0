CREATE TABLE [dbo].[SurgicalInstrumentNote] (
    [ID]        INT                                                                              IDENTITY (1, 1) NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECREGIST] DATETIME                                                                         NOT NULL,
    [TITNOTENF] CHAR (40)                                                                        NOT NULL,
    [NOTENFSUB] VARCHAR (MAX)                                                                    NULL,
    [NOTENFOBJ] VARCHAR (MAX)                                                                    NULL,
    [NOTENFANA] VARCHAR (MAX)                                                                    NULL,
    [CODNIVIMP] CHAR (2)                                                                         NULL,
    [FECREGSIS] DATETIME                                                                         CONSTRAINT [DF_SurgicalInstrumentNote_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_SurgicalInstrumentNote] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_SurgicalInstrumentNote_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_SurgicalInstrumentNote_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_SurgicalInstrumentNote_HCNIVIMPN] FOREIGN KEY ([CODNIVIMP]) REFERENCES [dbo].[HCNIVIMPN] ([CODNIVIMP]),
    CONSTRAINT [FK_SurgicalInstrumentNote_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_SurgicalInstrumentNote_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_SurgicalInstrumentNote_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[SurgicalInstrumentNote] NOCHECK CONSTRAINT [FK_SurgicalInstrumentNote_ADCENATEN];


GO
ALTER TABLE [dbo].[SurgicalInstrumentNote] NOCHECK CONSTRAINT [FK_SurgicalInstrumentNote_ADINGRESO];


GO
ALTER TABLE [dbo].[SurgicalInstrumentNote] NOCHECK CONSTRAINT [FK_SurgicalInstrumentNote_HCNIVIMPN];


GO
ALTER TABLE [dbo].[SurgicalInstrumentNote] NOCHECK CONSTRAINT [FK_SurgicalInstrumentNote_INPACIENT];


GO
ALTER TABLE [dbo].[SurgicalInstrumentNote] NOCHECK CONSTRAINT [FK_SurgicalInstrumentNote_INPROFSAL];


GO
ALTER TABLE [dbo].[SurgicalInstrumentNote] NOCHECK CONSTRAINT [FK_SurgicalInstrumentNote_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro / fecha del sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de Importancia de la Nota - Instrumentador Quirurgico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'CODNIVIMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nota Instrumentador Quirurgico -  Analisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'NOTENFANA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nota Instrumentador Quirurgico - Objetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'NOTENFOBJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nota Instrumentador Quirurgico - Subjetivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'NOTENFSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Titulo de la Nota -  Instrumentador Quirurgico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'TITNOTENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora del Registro la que ellos colocan en el formulario manualmente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'FECREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla (Autonumérico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SurgicalInstrumentNote', @level2type = N'COLUMN', @level2name = N'ID';

