CREATE TABLE [dbo].[HCFICHA340] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CLASICOMO]           INT           NULL,
    [HIJOMAD]             BIT           NULL,
    [MASCOMPSEX]          BIT           NULL,
    [HSH]                 BIT           NULL,
    [BISEXUAL]            BIT           NULL,
    [ANTETRANSHEMO]       BIT           NULL,
    [USUHEMODIA]          BIT           NULL,
    [TRABSALUD]           BIT           NULL,
    [ACCILABORAL]         BIT           NULL,
    [TRANSPORG]           BIT           NULL,
    [PERSOINYDROG]        BIT           NULL,
    [CONVPORTHBsAg]       BIT           NULL,
    [CONTSEXHBsAg]        BIT           NULL,
    [PROCENTESTE]         BIT           NULL,
    [RECACUPUNTU]         BIT           NULL,
    [MODOTRANS]           INT           NULL,
    [DONASANGRE]          BIT           NULL,
    [MOMDIAGHB]           INT           NULL,
    [SEMGEST]             VARCHAR (50)  NULL,
    [VACUPREVHEPB]        BIT           NULL,
    [NUMDOSIS]            VARCHAR (50)  NULL,
    [FECHAULTDOS]         DATE          NULL,
    [FUENTE]              INT           NULL,
    [SIGYSINT]            BIT           NULL,
    [PRESEALGCOMPLI]      INT           NULL,
    [COINFVIH]            BIT           NULL,
    [NOMAPE]              VARCHAR (50)  NULL,
    [TIPOID]              VARCHAR (50)  NULL,
    [NUMIDENT]            VARCHAR (50)  NULL,
    [APLIVACUHEPB]        INT           NULL,
    [APLIGAMAGLO]         INT           NULL,
    [FECHATOMA]           DATE          NULL,
    [FECHARECE]           DATE          NULL,
    [MUESTRA]             INT           NULL,
    [PRUEBA]              INT           NULL,
    [AGENTE]              INT           NULL,
    [RESULTADO]           INT           NULL,
    [FECHARESUL]          DATE          NULL,
    [VALOR]               VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA340] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA340_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA340_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA340] NOCHECK CONSTRAINT [CK_HCFICHA340_JSON];


GO
ALTER TABLE [dbo].[HCFICHA340] NOCHECK CONSTRAINT [FK_HCFICHA340_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor (Datos laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Fecha de Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado:   1=Compatible   2=Reactivo   3=No Reactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Agente:   1=Hepatitis b   2=Hepatitis delta   3=Hepatitis c', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prueba:   1=HBsAg   2=Patología   3=AntiVHD   4=Anti-HBc IgM   5=Anti-HBc Totales   6=Anti VHC   7=Carga Viral   8=Pruebas Genotípicas   9=D0 Inmunoensayo   10=H6 Inmunobiot --> item eliminado desde version V01_2020-03-06     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Muestra:   1=Sangre Total   2=Tejido   3=Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Toma de Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplicación de Gamaglobulina al Recién Nacido:   1=Primeras 12 Horas   2=13 a 24 Horas   3=Más de 24 Horas   4=Sin Dato   5=No Aplicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'APLIGAMAGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplicación de la Vacuna anti Hepatitis B al Recien Nacido:   1=Primeras 12 Horas   2=13 a 24 Horas   3=Más de 24 Horas   4=Sin Dato   5=No Aplicación  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'APLIVACUHEPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'NUMIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo ID (Documento Identificación) de la Madre --> desde versión ''V01_2020-03-06'' es con una enumeración:  1 = RC    2 = TI   3 = CC    4 = CE   5 = PA   6 = MS   7 = AS   8 = PE     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'TIPOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Coinfección VIH:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'NOMAPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Coinfección VIH:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'COINFVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Presenta Alguna de las Siguientes Complicaciones:  1=Falla Hepática Fulminante   2=Cirrosis Hepática   3=Carcinoma Hepático   4=Síndrome Febril Ictérico   5=Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PRESEALGCOMPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Signos y síntomas:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'SIGYSINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fuente:  1=Carné   2=Verbal   3=Sin Dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FUENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Última Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'NUMDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Vacunación Previa con Hepatitis B:  True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'VACUPREVHEPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Semanas de Gestación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'SEMGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Momento en el que fue Diagnosticada con HB:  1=Previo a la Gestación/Consulta Preconcepcional   2=Durante la Gestación   3=En el Momento del Parto   4=Posterior al Parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MOMDIAGHB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Donasangre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'DONASANGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modo de Transmisión más Probable:   1=Materno Infantil   2=Horizontal   3=Parental/Percutánea   4=Sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MODOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recacupuntu:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'RECACUPUNTU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Procenteste:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PROCENTESTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contsexhbsag:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CONTSEXHBsAg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Convporthbsag:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CONVPORTHBsAg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Persoinydrog:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PERSOINYDROG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Transporg:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'TRANSPORG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Accilaboral:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'ACCILABORAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Trabsalud:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'TRABSALUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuhemodia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'USUHEMODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'antetranshemo:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'ANTETRANSHEMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bisexual:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'BISEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hsh:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'HSH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mascompex:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MASCOMPSEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hijomad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'HIJOMAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación del Caso:   1=Paciente con Resultado Positivo para HBsAg a Clasificar   2=Hepatitis B Aguda   3=Hepatitis B Crónica   4=Hepatitis B por Transmisión Perinatal   5=Hepatitis Coinfección B-D   6=Hepatitis C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CLASICOMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'ID';

