CREATE TABLE [dbo].[HCHEMODIA] (
    [CONSECUTI] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [REGREGIST] DATETIME                                                                         NOT NULL,
    [NUMSESSIO] CHAR (20)                                                                        NOT NULL,
    [NUMMAQUIN] CHAR (20)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [TIEMSESIO] INT                                                                              NOT NULL,
    [DIALIZADO] CHAR (10)                                                                        NOT NULL,
    [CANTBOMBA] CHAR (10)                                                                        NOT NULL,
    [ELVALORUF] CHAR (10)                                                                        NOT NULL,
    [NUMAGUJAS] INT                                                                              NOT NULL,
    [CHEPARINA] CHAR (10)                                                                        NOT NULL,
    [SELCATETE] CHAR (10)                                                                        NOT NULL,
    [SELFAVFAV] CHAR (10)                                                                        NOT NULL,
    [VALENFERM] CHAR (400)                                                                       NOT NULL,
    [PESOPSECO] CHAR (10)                                                                        NOT NULL,
    [PESOPREDI] CHAR (10)                                                                        NOT NULL,
    [PESPOSDIA] CHAR (10)                                                                        NOT NULL,
    [FECREGSIS] DATETIME                                                                         CONSTRAINT [DF_HCHEMODIA_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_HCHEMODIA] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCHEMODIA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCHEMODIA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCHEMODIA_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCHEMODIA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCHEMODIA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCHEMODIA] NOCHECK CONSTRAINT [FK_HCHEMODIA_ADCENATEN];


GO
ALTER TABLE [dbo].[HCHEMODIA] NOCHECK CONSTRAINT [FK_HCHEMODIA_ADINGRESO];


GO
ALTER TABLE [dbo].[HCHEMODIA] NOCHECK CONSTRAINT [FK_HCHEMODIA_INPACIENT];


GO
ALTER TABLE [dbo].[HCHEMODIA] NOCHECK CONSTRAINT [FK_HCHEMODIA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCHEMODIA] NOCHECK CONSTRAINT [FK_HCHEMODIA_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro en el Sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso Pos-Dialisis (ml)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'PESPOSDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso Predialisis (kg)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'PESOPREDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso Seco (kg)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'PESOPSECO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valoracion de Enfermeria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'VALENFERM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FAV:  1. FAV  2. INJERTO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'SELFAVFAV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cateter:  1.Perm YUG  2.Trans. YUG  3.Trans. SBC  4.Trans. FM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'SELCATETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Heparina (UI)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CHEPARINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agujas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMAGUJAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'UF (ml)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'ELVALORUF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'bomba (ml/min)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CANTBOMBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dializador (fl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'DIALIZADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Horas de la Sesion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'TIEMSESIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la Maquina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMMAQUIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de la Session', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'NUMSESSIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'REGREGIST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHEMODIA', @level2type = N'COLUMN', @level2name = N'CONSECUTI';

