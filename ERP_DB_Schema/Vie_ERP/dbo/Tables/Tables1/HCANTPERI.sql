CREATE TABLE [dbo].[HCANTPERI] (
    [IDETIPHIS]      CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]      NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]      CHAR (10)                                                                        NOT NULL,
    [CODCENATE]      CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]      CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHISPAC]      DATETIME                                                                         NOT NULL,
    [EDADMADRE]      INT                                                                              NULL,
    [EDADGESTA]      INT                                                                              NULL,
    [NUMPARIDA]      INT                                                                              NULL,
    [TIPOPARTO]      CHAR (1)                                                                         NULL,
    [CONTPRENA]      BIT                                                                              NULL,
    [CANTPRENA]      INT                                                                              NULL,
    [GESTACION]      CHAR (1)                                                                         NULL,
    [CANTGESTA]      INT                                                                              NULL,
    [IQGTOXOPL]      CHAR (1)                                                                         NULL,
    [CANTTOXO]       INT                                                                              NULL,
    [IQMTOXOPL]      CHAR (1)                                                                         NULL,
    [PRESENTAC]      CHAR (1)                                                                         NULL,
    [RESULTHIV]      CHAR (1)                                                                         NULL,
    [HEPATITIB]      CHAR (1)                                                                         NULL,
    [CANTHEPAT]      INT                                                                              NULL,
    [RESULVDRL]      CHAR (1)                                                                         NULL,
    [DILUCVDRL]      INT                                                                              NULL,
    [RUPPREMEM]      INT                                                                              NULL,
    [UNIDTIEMP]      CHAR (1)                                                                         NULL,
    [OTROSPERI]      VARCHAR (4000)                                                                   NULL,
    [IPGRUPSAP]      CHAR (2)                                                                         NULL,
    [IPRHSANGP]      CHAR (1)                                                                         NULL,
    [IPGRUPSAM]      CHAR (2)                                                                         NULL,
    [IPRHSANGM]      CHAR (1)                                                                         NULL,
    [PERICEFAL]      INT                                                                              NULL,
    [PERITORAX]      INT                                                                              NULL,
    [PERIABDOM]      INT                                                                              NULL,
    [TALLAPACI]      INT                                                                              NULL,
    [PESOPACIE]      INT                                                                              NULL,
    [BALLARDPA]      INT                                                                              NULL,
    [APGAR1PAC]      INT                                                                              NULL,
    [APGAR5PAC]      INT                                                                              NULL,
    [APGAR10PA]      INT                                                                              NULL,
    [ADAPNEONT]      CHAR (1)                                                                         NULL,
    [MECONIOPA]      BIT                                                                              NULL,
    [REANIMACI]      VARCHAR (4000)                                                                   NULL,
    [TIPALIMEN]      CHAR (1)                                                                         NULL,
    [EXAMENESE]      BIT MASKED WITH (FUNCTION = 'default()')                                         NULL,
    [DESCRIPEX]      VARCHAR (4000) MASKED WITH (FUNCTION = 'partial(0, "Description_Ofuscado", 0)')  NULL,
    [INDAUDFOR]      NUMERIC (18)                                                                     NOT NULL,
    [ChildbirthCare] INT                                                                              NULL,
    CONSTRAINT [PK_HCANTPERI] PRIMARY KEY CLUSTERED ([IDETIPHIS] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC),
    CONSTRAINT [FK_HCANTPERI_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCANTPERI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCANTPERI_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCANTPERI_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCANTPERI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCANTPERI] NOCHECK CONSTRAINT [FK_HCANTPERI_ADcenaten];


GO
ALTER TABLE [dbo].[HCANTPERI] NOCHECK CONSTRAINT [FK_HCANTPERI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCANTPERI] NOCHECK CONSTRAINT [FK_HCANTPERI_INPacient];


GO
ALTER TABLE [dbo].[HCANTPERI] NOCHECK CONSTRAINT [FK_HCANTPERI_INPROFSAL];


GO
ALTER TABLE [dbo].[HCANTPERI] NOCHECK CONSTRAINT [FK_HCANTPERI_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IDX_AntecedentesPerinatales]
    ON [dbo].[HCANTPERI]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ATENCION AL PARTO --> en esta columna se almacenara 1- intrahospitalario 2-extrahospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'ChildbirthCare';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Anomalia en el Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'DESCRIPEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Examenes Externos Adicionales  True: Normal  False: Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'EXAMENESE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Alimentacion  1: Sin Definir  2: Leche Materna  3: Artificial  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'TIPALIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reanimacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'REANIMACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Meconio  True: Positivo  False: Negativo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'MECONIOPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Adaptacion Neonatal  1: Espontanea  2: Conducida  3: Inducida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'ADAPNEONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'APGAR 10 minutos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'APGAR10PA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'APGAR 5 minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'APGAR5PAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'APGAR 1 minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'APGAR1PAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ballard', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'BALLARDPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso en Gramos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PESOPACIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla Paciente en cm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'TALLAPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perimetro Abdominal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PERIABDOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perimetro Toraxico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PERITORAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perimetro Cefalico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PERICEFAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RH Materno:  +  -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPRHSANGM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo Sanguineo Materno:  A  B  AB  O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPGRUPSAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RH Paterno:  +  -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPRHSANGP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo Sanguineo Paterno:  A  B  AB  O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPGRUPSAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otros Perinatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'OTROSPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Tiempo Ruptura Prematura de Membrana  1: Horas  2: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'UNIDTIEMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ruptura Prematura de Membrana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'RUPPREMEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diluciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'DILUCVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'VDRL  True: Positivo  False: Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'RESULVDRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad o Numero Referenci Hepatitis B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTHEPAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ant. Sup. Hepatitis B  1: + Positivo  2: - Negativo  3: No Tiene', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'HEPATITIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HIV / VIH  True: Positivo  False: Negativo  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'RESULTHIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presentacion:  1: Cefalico  2: Pelviz  3: Transverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'PRESENTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IqM Toxoplasma  1: + Positivo  2: - Negativo  3: No Tiene', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IQMTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad o Numero referencia a IqG Toxoplasma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTTOXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IqG Toxoplasma  True: Positivo  False: Falso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IQGTOXOPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad o Numero de Gestaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gestacion  1: Unico  2: Multiple', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'GESTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad de Controles Prenatales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CANTPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control Prenatal True:Si False:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CONTPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Parto:  1: Vaginal  2: Instrumentada  3: Cesarea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Paridad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'NUMPARIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'EDADGESTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad de la Madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'EDADMADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Historia Clinica Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCANTPERI', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';

