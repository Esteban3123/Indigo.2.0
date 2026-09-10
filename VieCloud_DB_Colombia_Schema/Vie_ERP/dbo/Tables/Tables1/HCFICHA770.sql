CREATE TABLE [dbo].[HCFICHA770] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [NOMMADRE]            VARCHAR (50)  NULL,
    [EDADMADRE]           VARCHAR (50)  NULL,
    [LLANTONACER]         BIT           NULL,
    [MAMANORMAL]          BIT           NULL,
    [DEJOMAMAR]           BIT           NULL,
    [QUEFECHA]            DATE          NULL,
    [HIPERTEMIA]          BIT           NULL,
    [FONTANELA]           BIT           NULL,
    [RIGIDEZNUCA]         BIT           NULL,
    [TRISMUS]             BIT           NULL,
    [CONVULSIONES]        BIT           NULL,
    [ESPASMOS]            BIT           NULL,
    [CONTRACCIONES]       BIT           NULL,
    [OPISTOTONOS]         BIT           NULL,
    [LLANTOEXCE]          BIT           NULL,
    [SEPSISUMBI]          BIT           NULL,
    [NUMEMBA]             VARCHAR (50)  NULL,
    [ASISTICONTROL]       BIT           NULL,
    [EXPLINOASIS]         VARCHAR (50)  NULL,
    [ATENMED]             BIT           NULL,
    [ATENDENF]            BIT           NULL,
    [ATENDAUX]            BIT           NULL,
    [ATENPROM]            BIT           NULL,
    [ATENOTRO]            BIT           NULL,
    [OTROQUIEN]           VARCHAR (50)  NULL,
    [NUMCONTROL]          VARCHAR (50)  NULL,
    [ULTICONTROL]         DATE          NULL,
    [LUGAREMB]            BIT           NULL,
    [QUEMUNI]             VARCHAR (50)  NULL,
    [ANTEVAC]             BIT           NULL,
    [NUMDOSIS]            VARCHAR (50)  NULL,
    [EXPLINORECIB]        VARCHAR (50)  NULL,
    [FECHTD1]             DATE          NULL,
    [FECHTD2]             DATE          NULL,
    [FECHTD3]             DATE          NULL,
    [FECHTD4]             DATE          NULL,
    [LUGARPART]           BIT           NULL,
    [QUIENATENPAR]        INT           NULL,
    [APLISUST]            BIT           NULL,
    [CUALSUST]            VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA770] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA770_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA770_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA770] NOCHECK CONSTRAINT [CK_HCFICHA770_JSON];




GO
ALTER TABLE [dbo].[HCFICHA770] NOCHECK CONSTRAINT [CK_HCFICHA770_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Payload JSON con nuevas columnas y datos extendidos; VARCHAR(MAX), validado con CHECK ISJSON().', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de ficha de notificación; NULL indica primera versión; rastreo de cambios en registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de sustancia aplicada en ombligo (factores de riesgo neonatal); texto descriptivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CUALSUST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Cuál sustancia? (FACTORES DE RIESGO)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CUALSUST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CUALSUST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿se aplicaron sustancias en muñón umbilical? (1=Sí, 0=No); factor de riesgo infeccioso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'APLISUST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Se aplicaron sustancias en el muñón umbilical? (FACTORES DE RIESGO, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'APLISUST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'APLISUST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código profesional que atendió parto (1=Médico, 2=Enfermera, 3=Auxiliar, 4=Promotora, 5=Partera Complementada, 6=Partera no Complementada, 7=Familiar, 8=Sola, 9=Otro); factor riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'QUIENATENPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Quién atendió al parto (FACTORES DE RIESGO, (1. Médico, 2. enfermera, 3. Auxiliar, 4. Promotora, 5. Partera Complementada, 6. Partera no Complementada, 7. Familiar, 8. Sola, 9. Otro))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'QUIENATENPAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'QUIENATENPAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: lugar de nacimiento (1=Casa, 2=Institución de salud); factor riesgo perinatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LUGARPART';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar del parto (FACTORES DE RIESGO, (1. Casa, 2. Institución de salud))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LUGARPART';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LUGARPART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inoculación dosis 4 vacuna TD (tétanos-difteria); antecedente vacunal madre; DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la dosis 4 (ANTECEDENTES VACUNALES, TD4)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inoculación dosis 3 vacuna TD (tétanos-difteria); antecedente vacunal madre; DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la dosis 3 (ANTECEDENTES VACUNALES, TD3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inoculación dosis 2 vacuna TD (tétanos-difteria); antecedente vacunal madre; DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la dosis 2 (ANTECEDENTES VACUNALES, TD2)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inoculación dosis 1 vacuna TD (tétanos-difteria); antecedente vacunal madre; DATE.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la dosis 1 (ANTECEDENTES VACUNALES, TD1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FECHTD1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación/explicación si madre NO recibió vacuna antitetánica; antecedente vacunal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'EXPLINORECIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si marcó no, explique porqué  no recibió la vacuna (ANTECEDENTES VACUNALES)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'EXPLINORECIB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'EXPLINORECIB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de dosis TdaP o TD recibidas por madre; antecedente vacunal prenatal; VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NUMDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de dosis TdaP ó TD (ANTECEDENTES VACUNALES)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NUMDOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NUMDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿madre tiene antecedentes de vacunación antitetánica?; antecedente vacunal clave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ANTEVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Antecedentes de vacunación antitetánica? ( ANTECEDENTES VACUNALES, (true or false))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ANTEVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ANTEVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Municipio de residencia alternativa si madre NO vivió en mismo lugar; VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'QUEMUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En caso negativo,  ¿en qué municipio? (FACTORES DE RIESGO GESTACIONAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'QUEMUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'QUEMUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿madre residió en mismo municipio durante embarazo? (1=Sí, 2=No); factor riesgo gestacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LUGAREMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿ Vivió la madre en el mismo lugar durante el embarazo? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LUGAREMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LUGAREMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha del último control prenatal realizado; DATE; factor riesgo gestacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ULTICONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Último control prenatal (FACTORES DE RIESGO GESTACIONAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ULTICONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ULTICONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de controles prenatales asistidos durante gestación; VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NUMCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de controles (FACTORES DE RIESGO GESTACIONAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NUMCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NUMCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre/identificación de otro profesional que atendió control prenatal; VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'OTROQUIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Sí marco otro quién? (FACTORES DE RIESGO GESTACIONAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'OTROQUIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'OTROQUIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿atención prenatal por otro profesional no listado? (1=Sí, 2=No); factor riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Atendido por otro? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿control prenatal atendido por promotora de salud? (1=Sí, 2=No); factor riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENPROM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Atendido por promotor (a)? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENPROM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENPROM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿control prenatal atendido por auxiliar de enfermería? (1=Sí, 2=No); factor riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENDAUX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Atendido por auxiliar? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENDAUX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENDAUX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿control prenatal atendido por enfermera? (1=Sí, 2=No); factor riesgo gestacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENDENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Atendido por enfermero (a)? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENDENF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENDENF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿control prenatal atendido por médico? (1=Sí, 2=No); factor riesgo gestacional clave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Atendido por médico? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ATENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación/razón si madre NO asistió a control prenatal; factor riesgo gestacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'EXPLINOASIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Sí marcó no, explique porqué no asistió a control prenatal (FACTORES DE RIESGO GESTACIONAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'EXPLINOASIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'EXPLINOASIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿madre asistió a control prenatal durante gestación? (1=Sí, 2=No); factor crítico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ASISTICONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Asistió a control prenatal? (FACTORES DE RIESGO GESTACIONAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ASISTICONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ASISTICONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de embarazos simultáneos o previos del mismo caso; VARCHAR(50); factor riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NUMEMBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de embarazos con el del caso (FACTORES DE RIESGO GESTACIONAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NUMEMBA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NUMEMBA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿presencia de sepsis umbilical en recién nacido? (1=Sí, 2=No); dato clínico crítico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'SEPSISUMBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Sepsis umbilical? (DATOS CLÍNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'SEPSISUMBI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'SEPSISUMBI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿llanto excesivo/anormal en recién nacido? (1=Sí, 2=No); manifestación clínica neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LLANTOEXCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Llanto excesivo? (DATOS CLÌNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LLANTOEXCE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LLANTOEXCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿postura de opistótonos (arco dorsal)? (1=Sí, 2=No); signo clínico de tetania neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'OPISTOTONOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Opistótonos (DATOS CLÌNICOS, (1. Sí, 2. No)) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'OPISTOTONOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'OPISTOTONOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿contracciones musculares involuntarias? (1=Sí, 2=No); dato clínico neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CONTRACCIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contracciones (DATOS CLÌNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CONTRACCIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CONTRACCIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿espasmos musculares presentes? (1=Sí, 2=No); manifestación clínica neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ESPASMOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Espasmos (DATOS CLÍNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ESPASMOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ESPASMOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿convulsiones/crisis convulsivas en recién nacido? (1=Sí, 2=No); dato clínico severo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Convulsiones (DATOS CLÌNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CONVULSIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿trismus (trancadera de mandíbula)? (1=Sí, 2=No); signo clínico tétanos neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'TRISMUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TRISMUS (DATOS CLÍNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'TRISMUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'TRISMUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿rigidez en nuca/cuello? (1=Sí, 2=No); signo de meningitis o tétanos neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'RIGIDEZNUCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Rigidez en la nuca (DATOS CLÍNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'RIGIDEZNUCA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'RIGIDEZNUCA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿fontanela abombada/tensa? (1=Sí, 2=No); signo clínico hipertensión intracraneal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FONTANELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fontanela abonbada (DATOS CLÍNICOS, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FONTANELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'FONTANELA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿temperatura elevada/fiebre en recién nacido? (1=Sí, 2=No); signo clínico infección.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'HIPERTEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hipertemia (INFORMACIÓN GENERAL , (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'HIPERTEMIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'HIPERTEMIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha específica de aparición de síntomas o hallazgos clínicos; DATE; información temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'QUEFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En que fecha (INFORMACIÓN GENERAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'QUEFECHA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'QUEFECHA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿recién nacido dejó de mamar? (1=Sí, 2=No); indicador de dificultad alimentaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'DEJOMAMAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Dejo de mamar? (INFORMACIÓN GENERAL , (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'DEJOMAMAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'DEJOMAMAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿recién nacido mamó normalmente al nacer? (1=Sí, 2=No); capacidad succión refleja.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'MAMANORMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Mamaba normal al nacer? (INFORMACIÓN GENERAL, (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'MAMANORMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'MAMANORMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Booleano: ¿llanto presente al nacimiento? (1=Sí, 2=No); reflejo y vitalidad neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LLANTONACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Llanto al nacer? (INFORMACIÓN GENERAL , (1. Sí, 2. No))', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LLANTONACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'LLANTONACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad de la madre al momento de parto; VARCHAR(50); dato demográfico factor riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'EDADMADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad de la madre (INFORMACIÓN GENERAL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'EDADMADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'EDADMADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo de la madre; VARCHAR(50); identificación demográfica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NOMMADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NOMMADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'NOMMADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 del diagnóstico principal (ej: tétanos neonatal); CHAR(4); referencia nosológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) a tabla HCFICHANOTIFICACION; INT; relación 1:N con ficha padre.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiene relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único/consecutivo del registro; INT IDENTITY(1,1); PRIMARY KEY.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación epidemiológica 770 para tétanos neonatal: registra los datos clínicos, antecedentes maternos, signos y síntomas del recién nacido, control prenatal, vacunación antitetánica y lugar del parto asociados a un caso notificado al sistema de vigilancia en salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA770';
