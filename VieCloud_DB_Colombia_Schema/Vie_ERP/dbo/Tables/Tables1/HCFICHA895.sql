CREATE TABLE [dbo].[HCFICHA895] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [COMPLITIPONEUR]      BIT           NULL,
    [FECHAINICIOSIND]     DATE          NULL,
    [TIPOCOMPLNEUR]       VARCHAR (50)  NULL,
    [DESPLAULTI]          BIT           NULL,
    [MUNIDEPA]            VARCHAR (50)  NULL,
    [FECHAULTMENS]        DATE          NULL,
    [REALPRIMECOGES]      BIT           NULL,
    [FECHAPRIMECOG]       DATE          NULL,
    [EDADGESTPRIMEC]      VARCHAR (50)  NULL,
    [ENCUSEGUEAPB]        INT           NULL,
    [GESTTERMEMB]         BIT           NULL,
    [FECHTERMEMB]         DATE          NULL,
    [CONDIFINAL]          INT           NULL,
    [PERIMCEFA]           VARCHAR (50)  NULL,
    [REALNECROCLI]        BIT           NULL,
    [DEFECONGE]           BIT           NULL,
    [TOMOMUESSUER]        BIT           NULL,
    [TOMOMUESCORD]        BIT           NULL,
    [EXANTEMA]            BIT           NULL,
    [FIEBRE]              BIT           NULL,
    [HIPEREMIA]           BIT           NULL,
    [ARTRALGIAS]          BIT           NULL,
    [MIALGIAS]            BIT           NULL,
    [CEFALEA]             BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA895] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA895_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA895_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA895] NOCHECK CONSTRAINT [CK_HCFICHA895_JSON];




GO
ALTER TABLE [dbo].[HCFICHA895] NOCHECK CONSTRAINT [CK_HCFICHA895_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON (VARCHAR MAX, validado con ISJSON). Desde versión V01_2020-03-06: CODIGOCIE10 (4 alfanuméricos), CODPAIS_DESPLAZAMIENTO (3 alfanuméricos), CODDEPARTAMENTO_DESPLAZAMIENTO (2 alfanuméricos), CODMUNICIPIO_DESPLAZAMIENTO (3 alfanuméricos). Búsqueda: código CIE, ubicación geográfica, desplazamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON     Desde versión "V01_2020-03-06" -->  - CODIGOCIE10 : 4 caracteres alfanumericos   - CODPAIS_DESPLAZAMIENTO : 3 caracteres alfanumericos   - CODDEPARTAMENTO_DESPLAZAMIENTO : 2 caracteres alfanumericos   - CODMUNICIPIO_DESPLAZAMIENTO : 3 caracteres alfanumericos    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación epidemiológica. NULL = primera versión; especifica cambios estructurales desde V01_2020-03-06. Búsqueda: versionado, historial, notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de presencia de cefalea (dolor de cabeza) en signos y síntomas. Búsqueda: síntoma neurológico, dolor cabeza, notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Seleccione los signos y sintomas que haya presentado  Cefalea  1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'CEFALEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'CEFALEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de presencia de mialgia (dolor muscular) en signos y síntomas. Búsqueda: síntoma, dolor muscular, infección.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'MIALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Seleccione los signos y sintomas que haya presentado  Mialgias   1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'MIALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'MIALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de presencia de artralgia (dolor articular) en signos y síntomas. Búsqueda: síntoma, articulación, dolor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Seleccione los signos y sintomas que haya presentado  Artralgias   1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'ARTRALGIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de hiperemia conjuntival (enrojecimiento, conjuntivitis) en signos y síntomas. Búsqueda: conjuntivitis, ojo rojo, síntoma ocular.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'HIPEREMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Seleccione los signos y sintomas que haya presentado  Hiperemia conjuntival (Conjuntivitis)   1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'HIPEREMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'HIPEREMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de presencia de fiebre en signos y síntomas. Búsqueda: síntoma principal, temperatura elevada, infección.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Seleccione los signos y sintomas que haya presentado     Fiebre   1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de presencia de exantema/rash (erupción cutánea) en signos y síntomas. Búsqueda: síntoma dermatológico, erupción piel.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'EXANTEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Seleccione los signos y sintomas que haya presentado  Exantema (Rash)   1 = true   0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'EXANTEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'EXANTEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de toma de muestra de cordón umbilical para análisis perinatal. Búsqueda: muestra biológica, cordón, neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'TOMOMUESCORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  ¿Se tomó muestra de cordón umbilical?   1 = Si     0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'TOMOMUESCORD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'TOMOMUESCORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de toma de muestra de suero (sangre) para análisis serológico. Búsqueda: muestra biológica, suero, laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'TOMOMUESSUER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  ¿Se tomó muestra de suero?   1 = Si   0 =  No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'TOMOMUESSUER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'TOMOMUESSUER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de presencia de defecto congénito en recién nacido. Búsqueda: anomalía congénita, malformación, neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'DEFECONGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Defecto congénito  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'DEFECONGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'DEFECONGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de realización de necropsia clínica post-mortem. Búsqueda: autopsia, muerte perinatal, patología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'REALNECROCLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  ¿Se realizó necropsia clínica?   1 = Si    0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'REALNECROCLI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'REALNECROCLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perímetro cefálico en centímetros (VARCHAR 50, valor numérico). Búsqueda: medida antropométrica, cabeza, recién nacido, microcefalia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'PERIMCEFA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Perímetro cefálico (Cms)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'PERIMCEFA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'PERIMCEFA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condición final del embarazo (INT: 1=Aborto, 2=Muerte perinatal, 3=Nacido vivo). Búsqueda: resultado gestacional, outcome, egreso gestante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'CONDIFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Condición final  1 =Aborto     2 =Muerte perinatal     3 =Nacido vivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'CONDIFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'CONDIFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) de terminación del embarazo (parto, aborto o muerte). Búsqueda: fecha egreso obstétrico, parto, gestación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHTERMEMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Fecha de terminación del embarazo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHTERMEMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHTERMEMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de terminación del embarazo por gestante. Búsqueda: egreso gestación, cierre embarazo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'GESTTERMEMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  ¿La gestante terminó el embarazo?   1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'GESTTERMEMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'GESTTERMEMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de seguimiento por EAPB (INT: 1=Sí, 2=No, 3=Pendiente). Búsqueda: asegurador, continuidad atención, vigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'ENCUSEGUEAPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  ¿Se encuentra en seguimiento por la EAPB?   1=Si     2 =No     3 =Pendiente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'ENCUSEGUEAPB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'ENCUSEGUEAPB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional (semanas) en primera ecografía obstétrica (VARCHAR 50). Búsqueda: edad gestacional, ecografía, seguimiento prenatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'EDADGESTPRIMEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Edad gestacional de la primera ecografía obstétrica ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'EDADGESTPRIMEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'EDADGESTPRIMEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) de realización de la primera ecografía obstétrica. Búsqueda: ultrasonido obstétrico, control prenatal, imagen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHAPRIMECOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha de la primera ecografía obstétrica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHAPRIMECOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHAPRIMECOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de realización de primera ecografía a gestante. Búsqueda: control prenatal, ultrasonido, seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'REALPRIMECOGES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  ¿Se le realizó la primera ecografía a la gestante?   1 = Si       0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'REALPRIMECOGES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'REALPRIMECOGES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) de última menstruación (FUM), base para cálculo de edad gestacional. Búsqueda: FUM, ciclo menstrual, gestación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHAULTMENS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha de la última menstruación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHAULTMENS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHAULTMENS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Municipio/departamento/país de desplazamiento (VARCHAR 50). Búsqueda: ubicación geográfica, migración, epidemiología territorial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'MUNIDEPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Municipio/Departamento/País de desplazamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'MUNIDEPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'MUNIDEPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de desplazamientos en últimos 30 días. Búsqueda: movilidad, factor de riesgo, exposición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'DESPLAULTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ¿Desplazamientos en los últimos 30 días?     1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'DESPLAULTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'DESPLAULTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de complicación neurológica (VARCHAR 50, valores descriptivos). Búsqueda: síndrome neurológico, complicación SNC, diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLNEUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Tipo de complicación neurológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLNEUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'TIPOCOMPLNEUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha (DATE) de inicio del síndrome/síntomatología neurológica. Búsqueda: onset neurológico, cronología síntomas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHAINICIOSIND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha de inicio del sindrome neurológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHAINICIOSIND';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'FECHAINICIOSIND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 1=Sí, 0=No) de presencia de complicaciones neurológicas. Búsqueda: complicación SNC, afectación neurológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'COMPLITIPONEUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  ¿Tiene complicaciones de tipo neurológico?  1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'COMPLITIPONEUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'COMPLITIPONEUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico (CHAR 4, ej. CIE-10) del evento notificado. Búsqueda: diagnóstico, código enfermedad, clasificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de ficha de notificación epidemiológica padre (FK → HCFICHANOTIFICACION). Búsqueda: relación notificación, eventos vinculados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro en tabla HCFICHA895. Búsqueda: clave primaria, consecutivo ficha.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación epidemiológica 895 (Zika / arbovirus con complicaciones neurológicas y obstétricas). Registra los datos clínicos, síntomas, complicaciones neurológicas, seguimiento gestacional y condición final del caso notificado, asociado a una ficha de notificación y un diagnóstico CIE-10.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA895';
