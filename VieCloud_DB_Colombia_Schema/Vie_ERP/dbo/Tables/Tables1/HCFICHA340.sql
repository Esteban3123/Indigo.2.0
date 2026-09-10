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
ALTER TABLE [dbo].[HCFICHA340] NOCHECK CONSTRAINT [CK_HCFICHA340_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON validado; VARCHAR(MAX) con constraint ISJSON para estructuras complejas de notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del esquema de ficha de notificación; NULL indica primera versión; rastrea cambios de estructura (ej: V01_2020-03-06).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico o alfanumérico del resultado de laboratorio; dato de examen (hepatitis B, C, delta).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor (Datos laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se emite el resultado de la prueba de laboratorio; fecha de notificación al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Fecha de Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHARESUL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHARESUL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Interpretación del examen: 1=Compatible, 2=Reactivo (positivo), 3=No Reactivo (negativo); estado serológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado:   1=Compatible   2=Reactivo   3=No Reactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agente infeccioso detectado: 1=Hepatitis B, 2=Hepatitis Delta (D), 3=Hepatitis C; etiología de notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente:   1=Hepatitis b   2=Hepatitis delta   3=Hepatitis c', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'AGENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'AGENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba de laboratorio: HBsAg, Patología, AntiVHD, Anti-HBc, Anti-VHC, Carga Viral, Genotipificación, Inmunoensayo, Inmunobiot; test serológico/virológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba:   1=HBsAg   2=Patología   3=AntiVHD   4=Anti-HBc IgM   5=Anti-HBc Totales   6=Anti VHC   7=Carga Viral   8=Pruebas Genotípicas   9=D0 Inmunoensayo   10=H6 Inmunobiot --> item eliminado desde version V01_2020-03-06     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PRUEBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PRUEBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de espécimen biológico: 1=Sangre Total, 2=Tejido, 3=Suero; origen de la muestra de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muestra:   1=Sangre Total   2=Tejido   3=Suero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que el laboratorio recibe la muestra; marca de entrada a procesamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Recepción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHARECE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHARECE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se extrae o toma la muestra clínica; momento de la colección de espécimen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Toma de Examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplicación de gamaglobulina (inmunoglobulina) al recién nacido: 1=Primeras 12h, 2=13-24h, 3=Más de 24h, 4=Sin dato, 5=No aplicada; profilaxis neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'APLIGAMAGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplicación de Gamaglobulina al Recién Nacido:   1=Primeras 12 Horas   2=13 a 24 Horas   3=Más de 24 Horas   4=Sin Dato   5=No Aplicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'APLIGAMAGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'APLIGAMAGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplicación de vacuna anti-Hepatitis B al recién nacido: 1=Primeras 12h, 2=13-24h, 3=Más de 24h, 4=Sin dato, 5=No aplicada; inmunización perinatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'APLIVACUHEPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Aplicación de la Vacuna anti Hepatitis B al Recien Nacido:   1=Primeras 12 Horas   2=13 a 24 Horas   3=Más de 24 Horas   4=Sin Dato   5=No Aplicación  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'APLIVACUHEPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'APLIVACUHEPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación de la madre o paciente; cédula, documento de identidad, carné; PII_Identification_Ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'NUMIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identificación de la madre: RC=Registro Civil, TI=Tarjeta Identidad, CC=Cédula Ciudadanía, CE=Cédula Extranjería, PA=Pasaporte, MS=Migrante Especial, AS=Adulto Sin ID, PE=Permiso Especial; desde V01_2020-03-06.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'TIPOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo ID (Documento Identificación) de la Madre --> desde versión ''''V01_2020-03-06'''' es con una enumeración:  1 = RC    2 = TI   3 = CC    4 = CE   5 = PA   6 = MS   7 = AS   8 = PE     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'TIPOID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'TIPOID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre y apellido de la madre o paciente notificado; identificación nominal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'NOMAPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Coinfección VIH:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'NOMAPE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'NOMAPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de coinfección VIH en paciente con hepatitis B; True=Sí hay coinfección, False=No; importante para clasificación de riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'COINFVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Coinfección VIH:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'COINFVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'COINFVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de complicaciones hepáticas: 1=Falla Hepática Fulminante, 2=Cirrosis Hepática, 3=Carcinoma Hepático, 4=Síndrome Febril Ictérico, 5=Ninguna; gravedad clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PRESEALGCOMPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Presenta Alguna de las Siguientes Complicaciones:  1=Falla Hepática Fulminante   2=Cirrosis Hepática   3=Carcinoma Hepático   4=Síndrome Febril Ictérico   5=Ninguna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PRESEALGCOMPLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PRESEALGCOMPLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de presencia de signos y síntomas clínicos; True=Presentes, False=Ausentes; manifestaciones clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'SIGYSINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Signos y síntomas:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'SIGYSINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'SIGYSINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de información vacunal: 1=Carné/Documento, 2=Verbal/Anamnesis, 3=Sin dato; procedencia de datos de vacunación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FUENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fuente:  1=Carné   2=Verbal   3=Sin Dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FUENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FUENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de administración de la última dosis de vacuna; seguimiento de esquema vacunal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Última Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'FECHAULTDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de dosis de vacuna recibidas; seguimiento de esquema (3 dosis típico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'NUMDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'NUMDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'NUMDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de vacunación previa contra Hepatitis B: True=Sí vacunado, False=No vacunado; antecedente inmunológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'VACUPREVHEPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vacunación Previa con Hepatitis B:  True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'VACUPREVHEPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'VACUPREVHEPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas de gestación al momento del diagnóstico; edad fetal en trimestres; relevante para transmisión perinatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'SEMGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Semanas de Gestación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'SEMGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'SEMGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Momento temporal del diagnóstico de Hepatitis B: 1=Previo/Preconcepcional, 2=Durante gestación, 3=En parto, 4=Posterior al parto; cronología de infección.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MOMDIAGHB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Momento en el que fue Diagnosticada con HB:  1=Previo a la Gestación/Consulta Preconcepcional   2=Durante la Gestación   3=En el Momento del Parto   4=Posterior al Parto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MOMDIAGHB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MOMDIAGHB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si paciente es donante de sangre: True=Sí, False=No; factor epidemiológico de transmisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'DONASANGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Donasangre:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'DONASANGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'DONASANGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de transmisión más probable: 1=Materno-Infantil/Perinatal, 2=Horizontal, 3=Parental/Percutánea, 4=Sexual; mecanismo de contagio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MODOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modo de Transmisión más Probable:   1=Materno Infantil   2=Horizontal   3=Parental/Percutánea   4=Sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MODOTRANS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MODOTRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de exposición a acupuntura o procedimientos con agujas: True=Sí, False=No; factor de riesgo percutáneo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'RECACUPUNTU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recacupuntu:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'RECACUPUNTU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'RECACUPUNTU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de procedimiento con escisión/resección tisular: True=Sí, False=No; exposición quirúrgica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PROCENTESTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Procenteste:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PROCENTESTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PROCENTESTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de contacto sexual con persona HBsAg positiva: True=Sí expuesto, False=No; transmisión horizontal sexual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CONTSEXHBsAg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contsexhbsag:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CONTSEXHBsAg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CONTSEXHBsAg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de convivencia/contacto doméstico con HBsAg positivo: True=Sí, False=No; exposición domiciliar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CONVPORTHBsAg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Convporthbsag:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CONVPORTHBsAg';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CONVPORTHBsAg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si es persona que inyecta drogas: True=Sí, False=No; grupo de riesgo, vía parenteral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PERSOINYDROG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Persoinydrog:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PERSOINYDROG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'PERSOINYDROG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de exposición por trasplante de órgano: True=Sí, False=No; factor de riesgo transmisión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'TRANSPORG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Transporg:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'TRANSPORG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'TRANSPORG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de accidente laboral con exposición a sangre: True=Sí, False=No; factor ocupacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'ACCILABORAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Accilaboral:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'ACCILABORAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'ACCILABORAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si trabaja en sector salud/sanitario: True=Sí, False=No; exposición ocupacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'TRABSALUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Trabsalud:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'TRABSALUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'TRABSALUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si es usuario de hemodiálisis: True=Sí, False=No; factor de riesgo nosocomial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'USUHEMODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuhemodia:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'USUHEMODIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'USUHEMODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de antecedente de transfusión sanguínea: True=Sí recibió, False=No; factor de riesgo hemoterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'ANTETRANSHEMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'antetranshemo:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'ANTETRANSHEMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'ANTETRANSHEMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de orientación sexual bisexual: True=Sí, False=No; factor epidemiológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'BISEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Bisexual:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'BISEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'BISEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de hombre que tiene sexo con hombres: True=Sí, False=No; grupo de riesgo sexual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'HSH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hsh:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'HSH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'HSH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de múltiples compañeros sexuales: True=Sí, False=No; factor de riesgo sexual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MASCOMPSEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mascompex:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MASCOMPSEX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'MASCOMPSEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si es hijo/a de madre con Hepatitis B: True=Sí, False=No; transmisión perinatal/familiar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'HIJOMAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hijomad:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'HIJOMAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'HIJOMAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación epidemiológica del caso: 1=Positivo HBsAg sin clasificar, 2=Hepatitis B Aguda, 3=Hepatitis B Crónica, 4=Transmisión Perinatal, 5=Coinfección B-D, 6=Hepatitis C; categoría de notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CLASICOMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación del Caso:   1=Paciente con Resultado Positivo para HBsAg a Clasificar   2=Hepatitis B Aguda   3=Hepatitis B Crónica   4=Hepatitis B por Transmisión Perinatal   5=Hepatitis Coinfección B-D   6=Hepatitis C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CLASICOMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CLASICOMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 o catálogo interno; 4 caracteres; identifica la enfermedad notificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ficha de notificación madre (FK); vincula con HCFICHANOTIFICACION; llave relacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de la ficha de hepatitis B; clave primaria; IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha epidemiológica de notificación de Hepatitis B (evento 340 del SIVIGILA). Registra los factores de riesgo, antecedentes de transmisión, datos de vacunación, resultados de laboratorio y seguimiento clínico de casos notificados de Hepatitis B en pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA340';
