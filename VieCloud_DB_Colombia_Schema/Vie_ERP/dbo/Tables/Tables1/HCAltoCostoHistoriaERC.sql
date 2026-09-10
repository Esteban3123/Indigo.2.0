CREATE TABLE [dbo].[HCAltoCostoHistoriaERC] (
    [ID]            INT          IDENTITY (1, 1) NOT NULL,
    [ESTADO]        INT          NOT NULL,
    [CODDIAGNO]     CHAR (4)     NOT NULL,
    [IPCODPACI]     VARCHAR (25) NOT NULL,
    [NUMINGRES]     CHAR (10)    NOT NULL,
    [NUMEFOLIO]     CHAR (10)    NOT NULL,
    [CODPROSAL]     CHAR (20)    NOT NULL,
    [FECHAREGISTRO] DATETIME     NOT NULL,
    [CODCENATE]     CHAR (10)    NOT NULL,
    [UFUCODIGO]     CHAR (10)    NOT NULL,
    [16]            VARCHAR (12) NULL,
    [17]            DATE         NULL,
    [18]            INT          NULL,
    [19]            DATE         NULL,
    [19.1]          INT          NULL,
    [20]            INT          NULL,
    [21]            DATE         NULL,
    [21.1]          INT          NULL,
    [22]            INT          NULL,
    [36]            INT          NULL,
    [37]            INT          NULL,
    [38]            INT          NULL,
    [39]            INT          NULL,
    [40]            DATE         NULL,
    [41]            INT          NULL,
    [42]            VARCHAR (6)  NULL,
    [43]            INT          NULL,
    [44]            DATE         NULL,
    [45]            DATE         NULL,
    [46]            INT          NULL,
    [47]            VARCHAR (6)  NULL,
    [49]            INT          NULL,
    [50]            VARCHAR (6)  NULL,
    [51]            VARCHAR (6)  NULL,
    [52]            INT          NULL,
    [54]            INT          NULL,
    [55]            DATE         NULL,
    [56]            DATE         NULL,
    [57]            INT          NULL,
    [59]            VARCHAR (6)  NULL,
    [60]            VARCHAR (6)  NULL,
    [61]            VARCHAR (6)  NULL,
    [62]            INT          NULL,
    [62.1]          INT          NULL,
    [62.2]          INT          NULL,
    [62.3]          INT          NULL,
    [62.4]          INT          NULL,
    [62.5]          INT          NULL,
    [62.6]          INT          NULL,
    [62.7]          INT          NULL,
    [62.8]          INT          NULL,
    [62.9]          INT          NULL,
    [62.10]         INT          NULL,
    [62.11]         INT          NULL,
    [63]            DATE         NULL,
    [63.1]          NUMERIC (12) NULL,
    [64]            INT          NULL,
    [65]            NUMERIC (12) NULL,
    [66]            NUMERIC (12) NULL,
    [67]            INT          NULL,
    [69]            INT          NULL,
    [69.1]          DATE         NULL,
    [69.2]          DATE         NULL,
    [69.3]          DATE         NULL,
    [69.4]          DATE         NULL,
    [69.5]          DATE         NULL,
    [69.6]          DATE         NULL,
    [69.7]          DATE         NULL,
    [70]            INT          NULL,
    [70.1]          INT          NULL,
    [70.2]          INT          NULL,
    [70.3]          INT          NULL,
    [70.4]          INT          NULL,
    [70.5]          INT          NULL,
    [70.6]          INT          NULL,
    [70.7]          INT          NULL,
    [70.8]          NUMERIC (12) NULL,
    [70.9]          INT          NULL,
    [71]            INT          NULL,
    [72]            DATE         NULL,
    [73]            DATE         NULL,
    [74]            INT          NULL,
    [79]            INT          NULL,
    [80]            INT          NULL,
    [80.1]          DATE         NULL,
    [27]            VARCHAR (6)  NULL,
    [27.1]          DATE         NULL,
    [28]            VARCHAR (6)  NULL,
    [28.1]          DATE         NULL,
    [29]            VARCHAR (6)  NULL,
    [29.1]          DATE         NULL,
    [30]            VARCHAR (6)  NULL,
    [30.1]          DATE         NULL,
    [31]            VARCHAR (6)  NULL,
    [31.1]          DATE         NULL,
    [32]            INT          NULL,
    [32.1]          DATE         NULL,
    [33]            VARCHAR (6)  NULL,
    [33.1]          DATE         NULL,
    [34]            VARCHAR (6)  NULL,
    [34.1]          DATE         NULL,
    [23]            VARCHAR (6)  NULL,
    [24]            VARCHAR (6)  NULL,
    [25]            VARCHAR (6)  NULL,
    [26]            VARCHAR (6)  NULL,
    [35]            VARCHAR (6)  NULL,
    [48]            INT          NULL,
    [53]            INT          NULL,
    [58]            INT          NULL,
    [68]            INT          NULL,
    [75]            INT          NULL,
    [76]            INT          NULL,
    [77]            INT          NULL,
    [78]            INT          NULL,
    [81]            NUMERIC (20) NULL,
    [82]            DATE         NULL,
    CONSTRAINT [PK_HCAltoCostoHistoriaERC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último registro de Parathormona (PTH) en pg/mL, marcador de metabolismo óseo-mineral en ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'34.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha de la última parathormona PTH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'34.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'34.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parathormona (PTH) en pg/mL, hormona reguladora del calcio y fósforo sérico en pacientes con ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'34';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Parathormona PTH (pg/mL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'34';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'34';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último registro de Colesterol LDL (colesterol malo) en mg/dl, lípido aterogénico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'33.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha del último colesterol LDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'33.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'33.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Colesterol LDL (mg/dl), fracción lipídica aterogénica de riesgo cardiovascular en ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'33';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Colesterol LDL (mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'33';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'33';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último registro de Colesterol HDL (colesterol bueno) en mg/dl, lípido protector', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'32.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha del último colesterol HDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'32.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'32.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Colesterol HDL (mg/dl), fracción lipídica protectora en perfil lipídico de paciente ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'32';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Colesterol HDL (mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'32';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'32';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último registro de Colesterol total en mg/dl, suma de fracciones lipídicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'31.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Fecha del último colesterol total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'31.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'31.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Colesterol total (mg/dl), suma de HDL, LDL y triglicéridos, indicador de perfil lipídico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'31';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Colesterol total (mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'31';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'31';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último registro de Relación albuminuria/creatinuria, marcador de enfermedad renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'30.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha de la última albuminuria/creatinuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'30.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'30.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación albuminuria/creatinuria, cociente de proteína urinaria en diagnóstico y seguimiento ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'30';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Relación albuminuria/creatinuria ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'30';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'30';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último registro de Albuminuria en mg/24h, proteinuria en orina de 24 horas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'29.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha de la última albuminuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'29.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'29.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Albuminuria (mg/24h), proteína en orina de 24 horas, marcador de daño glomerular en ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'29';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Albuminuria (mg/24h)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'29';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'29';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último registro de Hemoglobina glicosilada (HbA1c) en porcentaje, control glucémico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'28.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha de última hemoglobina glicosilada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'28.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'28.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemoglobina glicosilada (%), indicador de control glucémico de 2-3 meses en diabetes ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'28';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Hemoglobina glicosilada (%)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'28';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'28';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último registro de Creatinina sérica en sangre, marcador función renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'27.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Fecha de última creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'27.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'27.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Creatinina en sangre (mg/dl), marcador principal de función renal y filtración glomerular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'27';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la creatinina en sangre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'27';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'27';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del deceso o muerte del paciente ERC, evento vital adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'80.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha de muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'80.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'80.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa de muerte del paciente ERC, diagnóstico o condición que originó el fallecimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'80';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Causa de Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'80';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'80';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Novedad o evento clínico relevante reportado en la historia de ERC del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'79';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Novedad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'79';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'79';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de trasplantes renales recibidos por el paciente, conteo histórico de injertos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'74';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Número de trasplantes renales que ha recibido el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'74';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'74';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de retorno a diálisis por pérdida definitiva del trasplante renal, rechazo crónico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'73';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha de retorno a diálisis por pérdida definitiva del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'73';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'73';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del primer rechazo agudo del injerto confirmado por biopsia en primeros 12 meses post-trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'72';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha del primer rechazo agudo del injerto (confirmado por biopsia en los primeros 12 meses posteriores al trasplante)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'72';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'72';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Episodios de rechazo agudo confirmados por biopsia en primeros 12 meses post-trasplante, complicación inmune', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'71';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'episodios de rechazo agudo confirmados por biopsia en los primeros 12 meses posteriores al trasplante, ha presentado el paciente con trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'71';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'71';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamentos inmunosupresores alternativos no incluidos en variables 70.1-70.6 o fuera de plan (3er fármaco)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'medicamentos inmunosupresores no incluidos en las variables 70.1 a 70.6 o no incluidos en el plan de beneficios (medicamento 3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamentos inmunosupresores alternativos no incluidos en variables 70.1-70.6 o fuera de plan (2do fármaco)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'recibe para el manejo del trasplante renal medicamentos inmunosupresores no incluidos en las variables 70.1 a 70.6 o no incluidos en el plan de beneficios  (medicamento 2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamentos inmunosupresores alternativos no incluidos en variables 70.1-70.6, fuera de cobertura plan', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'medicamentos inmunosupresores no incluidos en las variables 70.1 a 70.6 o no incluidos en el plan de beneficios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prednisona prescrita actualmente (última prescripción) para manejo inmunosupresor de trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actualmente (última prescripción) el paciente recibe Prednisona para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tacrolimus prescrito actualmente (última prescripción) para manejo inmunosupresor de trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actualmente (última prescripción) el paciente recibe Tacrolimus para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Micofenolato prescrito actualmente (última prescripción) para manejo inmunosupresor de trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actualmente (última prescripción) el paciente recibe Micofenolato para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciclosporina prescrita actualmente (última prescripción) para manejo inmunosupresor de trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actualmente (última prescripción) el paciente recibe Ciclosporina para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Azatioprina prescrita actualmente (última prescripción) para manejo inmunosupresor de trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actualmente (última prescripción) el paciente recibe Azatioprina para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Metilprednisolona prescrita actualmente (última prescripción) para manejo inmunosupresor de trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Actualmente (última prescripción) el paciente recibe Metilprednisolona para el manejo del trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de medicamentos inmunosupresores formulados en este último corte para manejo de trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Cuántos medicamentos inmunosupresores se formularon para el manejo en este último corte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'70';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del primer diagnóstico de cáncer en paciente con trasplante renal, complicación oncológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha del primer diagnóstico de cáncer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de complicación por herida quirúrgica en paciente trasplantado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Fecha de diagnóstico si ha presentado alguna complicación herida quirúrgica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de complicación urológica en paciente con trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha de diagnóstico si ha presentado alguna complicación urológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de complicación vascular en paciente con trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha de diagnóstico si ha presentado alguna complicación vascular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de infección por tuberculosis en paciente trasplantado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha de diagnóstico si ha presentado infección por tuberculosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de infección por hongos en paciente con trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha de diagnóstico si ha presentado infección por hongos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de infección por citomegalovirus (CMV) en paciente trasplantado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha de diagnóstico si ha presentado infección por citomegalovirus', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de complicación relacionada con trasplante renal (infección, rechazo, oncológica, vascular)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'El usuario ha presentado alguna complicación relacionada con el trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'69';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de donante del último trasplante renal (vivo relacionado, vivo no relacionado, cadáver)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'67';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Tipo de donante de último transplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'67';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'67';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de IPS o grupo de trasplante que realizó el trasplante renal, institución ejecutora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'66';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Código de la IPS o Grupo de trasplante, que realizó el trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'66';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'66';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de EPS que autorizó/financió el trasplante renal, aseguradora responsable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'65';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Código de la EPS que realizó el trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'65';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'65';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de trasplante renal recibido por el paciente (Sí/No), condición de trasplantado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'64';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda El usuario ha recibido trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'64';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'64';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de IPS donde paciente está en lista de espera para trasplante renal, entidad activa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'63.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Código de la IPS donde está en lista de espera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'63.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'63.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso a lista de espera para trasplante renal, inicio de candidatura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'63';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha de ingreso a lista de espera para la realización del trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'63';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'63';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraindicación reportada para trasplante renal en valoración nefrología (no especificada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraindicación: enfermedad pulmonar crónica reportada en valoración de nefrología para trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' reportó como contraindicación para el trasplante renal, en la valoración de nefrología, que el paciente presenta enfermedad pulmonar crónica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.10';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraindicación: enfermedad inmunológica activa últimos 3 meses reportada en valoración nefrología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'reportó como contraindicación para el trasplante renal, en la valoración de nefrología, que el paciente presenta enfermedad inmunológica activa los últimos tres meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.9';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraindicación: infección por VHC (hepatitis C) reportada en valoración de nefrología para trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se reportó infección por el VHC, como contraindicación para el trasplante renal, en la valoración de nefrología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraindicación: infección por VIH reportada en valoración de nefrología para trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se reportó infección por el VIH, como contraindicación para el trasplante renal, en la valoración de nefrología', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraindicación: enfermedad cardiaca, cerebrovascular o vascular periférica para trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'enfermedad cardiaca, cerebrovascular o vascular periférica, como contraindicación para el trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraindicación por condición no especificada reportada en valoración de nefrología para trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología que el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraindicación: esperanza de vida menor o igual a 6 meses en valoración para trasplante renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología que el paciente presenta esperanza de vida menor o igual a 6 meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraindicación: paciente NO ha manifestado deseo de trasplantarse, falta de consentimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se reportó como contraindicación para el trasplante renal, en la valoración de nefrología que el paciente NO ha manifestado su deseo de trasplantarse', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraindicación: infección crónica o activa no tratada/no controlada últimos 3 meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se reportó infección crónica o activa no tratada o no controlada hasta en los últimos tres meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraindicación: cáncer activo en últimos 12 meses reportado en valoración para trasplante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'e reportó cáncer activo en los últimos 12 meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Última valoración clínica inicial por nefrología en persona con ERC5 en diálisis, análisis trasplantabilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Última Valoración Clínica inicial por nefrología a personas ERC5 en diálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'62';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fósforo sérico (P) (mg/dl), marcador de metabolismo óseo-mineral en ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'61';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fórforo sérico (P) (mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'61';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'61';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Albúmina sérica (g/dl), proteína plasmática indicadora de nutrición y función hepática en ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'60';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Albúmina sérica (g/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'60';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'60';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemoglobina (g/dl), pigmento transportador de oxígeno, marcador de anemia en ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'59';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hemoglobina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'59';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'59';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tratamiento no dialítico para ERC estadio 5, manejo conservador sin terapia renal sustitutiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'57';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Tratamiento no Dialítico para ERC estadio 5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'57';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'57';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de infección por Hepatitis C (VHC) en paciente ERC, enfermedad viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'56';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Fecha de diagnóstico de la infección por Hepatitis C, si el usuario la ha presentado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'56';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'56';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de infección por Hepatitis B (VHB) en paciente ERC, enfermedad viral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'55';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Fecha de diagnóstico de la infección por Hepatitis B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'55';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'55';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vacuna Hepatitis B aplicada al paciente ERC, inmunización preventiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'54';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Vacuna Hepatitis B', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'54';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'54';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peritonitis infecciosa presentada por paciente en diálisis peritoneal, complicación infecciosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'52';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Peritonitis Infecciosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'52';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'52';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de horas de diálisis por sesión, tiempo de tratamiento en hemodiálisis o diálisis peritoneal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'51';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Número de horas de diálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'51';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'51';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de diálisis Kt/V (clearance-time-volume ratio) dpd, marcador de adecuación dialítica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'50';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Dosis de diálisis (Kt/V) dpd. KTV/dpd', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'50';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'50';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diálisis peritoneal (DP) recibida por paciente, modalidad de terapia renal sustitutiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'49';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Diálisis peritoneal (DP)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'49';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'49';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis de diálisis Kt/V single pool, marcador de adecuación en hemodiálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'47';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Dosis de diálisis (Kt/V) single pool', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'47';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'47';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemodiálisis (HD) recibida por paciente, modalidad principal de terapia renal sustitutiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'46';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda   Hemodiálisis (HD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'46';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'46';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso a unidad renal actual para terapia dialítica, inicio de atención en centro diálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'45';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Fecha de ingreso a la unidad renal actual que le presta el servicio de terapia dialítica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'45';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'45';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de terapia de reemplazo renal (TRR) actual en el paciente, primer día de diálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'44';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se inició la terapia de reemplazo renal que recibe el usuario en el momento de la fecha de corte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'44';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'44';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modo de inicio de primera terapia de reemplazo renal (emergencia, programada, otro)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'43';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Modo de inicio de la primera Terapia de Reemplazo Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'43';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'43';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de inicio de primera terapia de reemplazo renal (TRR), paciente dializado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'42';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  el usuario inició la primera terapia de reemplazo renal -TRR', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'42';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'42';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de programa de atención ERC (renoprotección, nefroprotección, protección renal, prediálisis)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'41';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'persona se encuentra en un programa de atención de ERC (renoprotección, nefroprotección, protección renal, prediálisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'41';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'41';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de ERC estadio 5 (falla renal terminal), momento clasificación severidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'40';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Fecha de diagnóstico de ERC estadio 5 ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'40';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'40';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estadio de ERC (1-5): clasificación de enfermedad renal crónica por filtración glomerular', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'39';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Estadio de ERC (Enfermedad Renal Crónica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'39';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'39';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de diagnóstico de ERC en cualquier estadio (1-5), presencia de enfermedad renal crónica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'38';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'usuario tiene diagnóstico de ERC en cualquier de sus estadios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'38';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'38';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de prescripción de antagonista de receptores de angiotensina II (ARA II) en ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'37';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'recibe Antagonista de los receptores de angiotensina 11', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'37';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'37';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de prescripción de inhibidor de enzima convertidora de angiotensina (IECA) en ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'36';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  El usuario recibe Inhibidor de la Enzima convertidora de angiotensina (lECA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'36';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'36';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Etiología de ERC seleccionada: causa primaria de enfermedad renal crónica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda lo selecionado Etiología de la ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo de diabetes mellitus durante período de reporte, gasto sanitario DM en ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'21.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Costo DM durante el período de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'21.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'21.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de diabetes mellitus (DM), comorbilidad frecuente en ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'21';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha de diagnóstico de la Diabetes Mellitus', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'21';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'21';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de diagnóstico confirmado de diabetes mellitus (Sí/No), comorbilidad ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'20';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  El usuario tiene diagnostico confirmado de Diabetes Mellitus', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'20';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'20';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo de hipertensión arterial durante período de reporte, gasto sanitario HTA en ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'19.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Costo HTA durante el período de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'19.1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'19.1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de diagnóstico de hipertensión arterial (HTA), comorbilidad frecuente en ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'19';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Fecha de diagnóstico de Hipertensión Arterial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'19';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'19';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de diagnóstico confirmado de hipertensión arterial (HTA) (1=Sí, 2=No)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'18';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  El usuario tiene diagnóstico confirmado de hipertensión  - HPTA        1 = Si    2= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'18';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'18';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de ingreso a programa de atención renal, inicio de seguimiento nefrológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'17';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda fecha de ingreso programa de atencion renal ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'17';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'17';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de IPS donde se hace seguimiento al usuario ERC, institución principal de atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'16';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Código de la IPS donde se hace seguimiento al usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'16';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'16';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional que atiende al paciente, área clínica responsable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de centro de atención donde se registra el histórico ERC, institución prestadora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de registro del dato en el sistema, timestamp de ingreso a base de datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha en el q se guarda el dato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que registró el histórico ERC, médico/especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigoprofesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio del registr del histórico ERC, identificador de atención/documento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso del paciente, identificador de admisión hospitalaria/ambulatoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificación del paciente (cédula/pasaporte/identificación PII), clave única', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda codigo del paciente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico (CIE-10), clasificación de diagnóstico principal del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro en sistema (activo, inactivo, etc.), flag de validez', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Gurada  el estado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo único de la tabla HCAltoCostoHistoriaERC, clave primaria autonumérica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historial clínico de pacientes con enfermedad renal crónica (ERC) clasificados como alto costo. Registra el seguimiento, evolución y datos clínicos relevantes de cada paciente con ERC por ingreso y profesional de salud tratante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador o código de condición clínica adicional relacionado con el seguimiento renal (campo auxiliar ERC, formato corto)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'23';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador o código de condición clínica complementaria en el seguimiento del paciente con ERC (campo auxiliar ERC)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'24';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'24';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o clasificación adicional de estado clínico o terapéutico del paciente con enfermedad renal crónica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'25';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'25';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código o clasificación de otra variable clínica o de seguimiento registrada en la historia ERC del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'26';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'26';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de resultado, indicador o variable clínica correspondiente a la sección 35 del formulario de seguimiento ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'35';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'35';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor entero asociado a un indicador clínico, laboratorio o estado del paciente en la sección 48 del registro ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'48';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'48';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico entero de variable clínica o de seguimiento correspondiente al campo 53 del formulario ERC (posiblemente resultado de laboratorio o escala)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'53';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'53';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor entero de indicador clínico, resultado o estado registrado en la sección 58 del seguimiento de alto costo ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'58';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'58';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor entero de variable clínica o indicador de seguimiento correspondiente al campo 68 del registro ERC de alto costo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'68';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'68';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador numérico entero de condición clínica, resultado o estado del paciente ERC registrado en el campo 75', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'75';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'75';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador numérico entero de variable clínica o de seguimiento del paciente con ERC en el campo 76', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'76';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'76';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor entero de indicador clínico o resultado asociado al seguimiento del paciente ERC en el campo 77', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'77';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'77';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor entero de variable clínica, resultado de laboratorio o indicador de estado en el campo 78 del formulario ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'78';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'78';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de alta precisión (posiblemente resultado de laboratorio, tasa de filtración glomerular u otro indicador cuantitativo) registrado en el campo 81 del seguimiento ERC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'81';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'81';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha asociada a un evento clínico, resultado o control del paciente con ERC registrado en el campo 82 del formulario de alto costo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'82';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCAltoCostoHistoriaERC', @level2type = N'COLUMN', @level2name = N'82';
