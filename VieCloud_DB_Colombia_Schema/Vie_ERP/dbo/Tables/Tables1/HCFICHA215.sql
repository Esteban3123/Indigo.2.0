CREATE TABLE [dbo].[HCFICHA215] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [NOMMAD]              VARCHAR (50)  NULL,
    [TIPID]               VARCHAR (50)  NULL,
    [NUMIDENT]            VARCHAR (50)  NULL,
    [EDAD]                VARCHAR (50)  NULL,
    [NUMEMB]              VARCHAR (50)  NULL,
    [NACVIVOS]            VARCHAR (50)  NULL,
    [ABORTOS]             VARCHAR (50)  NULL,
    [MORTINAT]            VARCHAR (50)  NULL,
    [DIAGNOS]             BIT           NULL,
    [EDADGESTDIA]         VARCHAR (50)  NULL,
    [PATCRON]             BIT           NULL,
    [CUALES]              VARCHAR (MAX) NULL,
    [EMBMULT]             BIT           NULL,
    [NATIVIVO]            INT           NULL,
    [EDADGESTNAC]         VARCHAR (50)  NULL,
    [PESO]                VARCHAR (50)  NULL,
    [PERIMCEF]            VARCHAR (50)  NULL,
    [DESC1]               VARCHAR (MAX) NULL,
    [DESC2]               VARCHAR (MAX) NULL,
    [DESC3]               VARCHAR (MAX) NULL,
    [DESC4]               VARCHAR (MAX) NULL,
    [DESC5]               VARCHAR (MAX) NULL,
    [DESC6]               VARCHAR (MAX) NULL,
    [DESC7]               VARCHAR (MAX) NULL,
    [DESC8]               VARCHAR (MAX) NULL,
    [STORCH]              BIT           NULL,
    [TSH1]                BIT           NULL,
    [TOTSUE1]             BIT           NULL,
    [LIBSUE1]             BIT           NULL,
    [TSH2]                BIT           NULL,
    [TOTSUE2]             BIT           NULL,
    [LIBSUE2]             BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA215] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA215_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA215_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA215] NOCHECK CONSTRAINT [CK_HCFICHA215_JSON];




GO
ALTER TABLE [dbo].[HCFICHA215] NOCHECK CONSTRAINT [CK_HCFICHA215_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido en formato JSON con nuevas columnas y campos adicionales de la ficha de notificación (VARCHAR MAX, validado con ISJSON)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación; valor nulo = primera versión (formato: V##_YYYY-MM-DD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'T4 Libre en Suero - Segunda medición; bit booleano (1=sí, 0=no). Indicador de prueba tiroidea TSH realizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'LIBSUE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'T4 Libre Suero 2  True = si false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'LIBSUE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'LIBSUE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'T4 Total en Suero - Segunda medición; bit booleano (1=sí, 0=no). Indicador de prueba tiroidea TSH realizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TOTSUE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'T4 Total suero 2  True = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TOTSUE2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TOTSUE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hormona Estimulante de Tiroides (TSH) - Segunda medición; bit booleano (1=sí, 0=no). Tamizaje neonatal de hipotiroidismo congénito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TSH2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TSH 2  True = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TSH2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TSH2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'T4 Libre en Suero - Primera medición; bit booleano (1=sí, 0=no). Indicador de prueba tiroidea realizada en recién nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'LIBSUE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'T4 Libre Suero  True = si false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'LIBSUE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'LIBSUE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'T4 Total en Suero - Primera medición; bit booleano (1=sí, 0=no). Indicador de prueba tiroidea realizada en recién nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TOTSUE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'T4 Total suero  True = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TOTSUE1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TOTSUE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hormona Estimulante de Tiroides (TSH) - Primera medición; bit booleano (1=sí, 0=no). Tamizaje neonatal de hipotiroidismo congénito', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TSH1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TSH  True = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TSH1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TSH1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Prueba STORCH en Recién Nacido (Sífilis, Toxoplasmosis, Rubeola, Citomegalovirus, Herpes); bit booleano (1=sí realizada, 0=no). Infecciones congénitas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'STORCH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'STORCH en Recién Nacido  true = si   false = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'STORCH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'STORCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción adicional u observaciones generales 8 de la notificación de nacimiento (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC8';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción adicional u observaciones generales 7 de la notificación de nacimiento (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción adicional u observaciones generales 6 de la notificación de nacimiento (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción adicional u observaciones generales 5 de la notificación de nacimiento (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción adicional u observaciones generales 4 de la notificación de nacimiento (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción adicional u observaciones generales 3 de la notificación de nacimiento (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción adicional u observaciones generales 2 de la notificación de nacimiento (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción adicional u observaciones generales 1 de la notificación de nacimiento (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Descripción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DESC1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Perímetro cefálico del recién nacido medido en centímetros (cms). Indicador biométrico de normalidad neurológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'PERIMCEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Perímetro Cefálico (Cms)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'PERIMCEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'PERIMCEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso corporal del recién nacido en gramos al momento del nacimiento. Parámetro vital de viabilidad neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'PESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso (Gramos) al Nacer', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'PESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'PESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional al momento del nacimiento expresada en semanas. Clasificación de prematuro/término/postérmino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EDADGESTNAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Gestacional al Momento del Nacimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EDADGESTNAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EDADGESTNAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de vitalidad del recién nacido: 1=sí nativo vivo, 2=no, 3=no ha nacido aún. Indicador de viabilidad y momento de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NATIVIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nativivo  1=si  2=no   3=No ha nacido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NATIVIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NATIVIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Embarazo múltiple (gemelos, trillizos, etc.); bit booleano (1=sí, 0=no). Pregnancia múltiple de la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EMBMULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Embarazo Múltiple   true = si   false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EMBMULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EMBMULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otras patologías o características adicionales cuando se marca como patología crónica adicional (VARCHAR MAX)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'CUALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuales  otros ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'CUALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'CUALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Patología crónica adicional o complicaciones durante el embarazo/parto; bit booleano (1=sí, 0=no). Antecedentes maternos relevantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'PATCRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Patología Crónica Adicional o Complicaciones en Embarazo  true = si   false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'PATCRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'PATCRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad gestacional al momento del diagnóstico de la condición notificada, expresada en semanas. Contexto clínico del hallazgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EDADGESTDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad Gestional al Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EDADGESTDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EDADGESTDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano de presencia de diagnóstico documentado en la ficha (1=sí, 0=no). Confirmación de patología notificada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DIAGNOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DIAGNOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'DIAGNOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de mortinatos (fetos con ≥22 semanas de gestación sin signos de vida). Antecedente obstétrico materno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'MORTINAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mortinatos(>=22)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'MORTINAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'MORTINAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de abortos previos (<22 semanas de gestación). Antecedente obstétrico materno de pérdida gestacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'ABORTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Abortos (<22 Sem)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'ABORTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'ABORTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de hijos nacidos vivos previos. Paridad materna, contador de partos exitosos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NACVIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nacidos Vivos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NACVIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NACVIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de embarazos que ha tenido la madre (gestas). Indicador de multiparidad y experiencia obstétrica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NUMEMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Embarazos Totales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NUMEMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NUMEMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad actual de la madre en años al momento de la notificación. Edad materna como factor de riesgo perinatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación de la madre (cédula, documento, DNI). Identificador único PII Identification_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NUMIDENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NUMIDENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento identificatorio de la madre. Enumeración: 1=RC (Registro Civil), 2=TI (Tarjeta Identidad), 3=CC (Cédula Ciudadanía), 4=CE (Cédula Extranjería), 5=PA (Pasaporte), 6=MS (Menor Sin ID), 7=AS (Adulto Sin ID), 8=PE (Permiso Especial), 9=CN (Carné Nacional). Desde v01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TIPID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo ID (Documento Identificación) de la Madre --> desde versión ''''V01_2020-03-06'''' es con una enumeración:  1 = RC    2 = TI   3 = CC    4 = CE   5 = PA   6 = MS   7 = AS   8 = PE   9 = CN   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TIPID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'TIPID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombres y apellidos completos de la madre. Identificación legible de la paciente gestante/puerperio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NOMMAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombres y Apellidos de la Madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NOMMAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'NOMMAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 (4 caracteres CHAR). Clasificación de enfermedad/condición notificada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la ficha de notificación padre (FK referencia HCFICHANOTIFICACION.ID). Relación con evento de notificación principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY 1,1) del registro de ficha 215. Clave primaria, PK_HCFICHA215', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación perinatal (ficha 215) de historia clínica: registra datos de la madre, el embarazo, el recién nacido y resultados de tamizajes neonatales (STORCH, TSH) asociados a una notificación epidemiológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA215';
