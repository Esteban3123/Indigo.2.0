CREATE TABLE [dbo].[HCANTGINI] (
    [IDETIPHIS] CHAR (9)                                                                              NOT NULL,
    [NUMEFOLIO] NCHAR (10)                                                                            NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')      NOT NULL,
    [NUMINGRES] CHAR (10)                                                                             NOT NULL,
    [CODCENATE] CHAR (10)                                                                             NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                             NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')         NOT NULL,
    [FECHISPAC] DATETIME                                                                              NOT NULL,
    [MENARQUIA] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [CICLOSPAC] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [MENSTRDUR] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [CICLOREGU] BIT                                                                                   NULL,
    [EDADVIDSE] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [GESTACION] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMPARTO]  INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMCESARE] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMABORTO] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMHIJVIV] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMMORTIN] VARCHAR (50) MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [NUMETOPIC] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMEMOLAS] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [NUMEOVITO] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [FECULTMEN] VARCHAR (50)                                                                          NULL,
    [FECULTPAR] VARCHAR (50)                                                                          NULL,
    [FECULTCIT] VARCHAR (50) MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [CODPLANIF] CHAR (2)                                                                              NULL,
    [OTROSANTE] VARCHAR (2000) MASKED WITH (FUNCTION = 'default()')                                   NULL,
    [CONTPRENA] BIT                                                                                   NULL,
    [CANTPRENA] INT                                                                                   NULL,
    [NOMSEMGES] NUMERIC (3, 1) MASKED WITH (FUNCTION = 'default()')                                   NULL,
    [INICONPRE] NUMERIC (3, 1) MASKED WITH (FUNCTION = 'default()')                                   NULL,
    [RIESOBTET] VARCHAR (200) MASKED WITH (FUNCTION = 'default()')                                    NULL,
    [RESCUAHEM] VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [RESPARORI] VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [TESTSULLI] VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [GLUCBASAL] VARCHAR (400) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)') NULL,
    [RESULVDRL] BIT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [DILUCVDRL] INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [IGGTOXOPL] BIT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [CANTTOXO]  INT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [FECULTIGG] DATETIME MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [IQMTOXOPL] CHAR (1) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')      NULL,
    [FECULTIQM] DATETIME                                                                              NULL,
    [RESULTHIV] BIT MASKED WITH (FUNCTION = 'default()')                                              NULL,
    [HEPATITIB] CHAR (1) MASKED WITH (FUNCTION = 'partial(0, "ResultLabObstetric_Ofuscado", 0)')      NULL,
    [CANTHEPAT] INT                                                                                   NULL,
    [OTROSOBST] VARCHAR (2000) MASKED WITH (FUNCTION = 'partial(0, "OtherObstetric_Ofuscado", 0)')    NULL,
    [INDAUDFOR] NUMERIC (18)                                                                          NOT NULL,
    [FECPROPAR] DATETIME                                                                              NULL,
    CONSTRAINT [PK_HCANTGINI] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCANTGINI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANTGINI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANTGINI_HCTIPPLAN] FOREIGN KEY ([CODPLANIF]) REFERENCES [dbo].[HCTIPPLAN] ([CODPLANIF]),
    CONSTRAINT [FK_HCANTGINI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANTGINI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANTGINI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCANTGINI] NOCHECK CONSTRAINT [FK_HCANTGINI_ADcenaten];


GO
ALTER TABLE [dbo].[HCANTGINI] NOCHECK CONSTRAINT [FK_HCANTGINI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCANTGINI] NOCHECK CONSTRAINT [FK_HCANTGINI_HCTIPPLAN];


GO
ALTER TABLE [dbo].[HCANTGINI] NOCHECK CONSTRAINT [FK_HCANTGINI_INPACIENT];


GO
ALTER TABLE [dbo].[HCANTGINI] NOCHECK CONSTRAINT [FK_HCANTGINI_INPROFSAL];


GO
ALTER TABLE [dbo].[HCANTGINI] NOCHECK CONSTRAINT [FK_HCANTGINI_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Probable Parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECPROPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos -  Otros Obstetricos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'OTROSOBST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Cantidad o Numero Referenci Hepatitis B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CANTHEPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Ant. Sup. Hepatitis B  1: + Positivo  2: - Negativo  3: No Tiene', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'HEPATITIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - HIV / VIH  True: Positivo  False: Negativo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESULTHIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Ultimo Examen IqM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTIQM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - IqM Toxoplasma  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Ultimo Examen IgG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTIGG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Cantidad o Numero referencia a IgG Toxoplasma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CANTTOXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - IgG Toxoplasma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IGGTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - VDRL Diluciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'DILUCVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Glucemia Basal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESULVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Test Sullivan', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'GLUCBASAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Test Sullivan', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'TESTSULLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Parcial de Orina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESPARORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Cuadro Hematico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RESCUAHEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Inicio Control Prenatal Semanas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'RIESOBTET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Semanas de gestacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'INICONPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Semanas de gestacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NOMSEMGES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Cantidad de Controles Prenatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CANTPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Obstetricos - Control Prenatal True:Si False:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CONTPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos -  Otros Antecedentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'OTROSANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Codigo del Tipo de Planificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CODPLANIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Fecha Ultima Citologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTCIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Fecha Ultimo Parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Mola', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECULTMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Numero de Embarazos Ovitos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMEOVITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Numero de Embarazos Etopicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMEMOLAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Numero de Embarazos Etopicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMETOPIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Numero de Mortinatos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMMORTIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Numero de Nacidos Vivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMHIJVIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Numero de Abortos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMABORTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Numero de Cesareas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMCESARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Numero de Partos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Numero de Gestaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'GESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad en la que Inicia Vida Sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'EDADVIDSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ciclo Regular:  True= Si  False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CICLOREGU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Duracion de la menstracion expresados en Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'MENSTRDUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Ciclos expresados en Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CICLOSPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ginecologicos - Menarquia Epresado en Años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'MENARQUIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Historia Clinica Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTGINI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';

