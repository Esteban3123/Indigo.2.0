CREATE TABLE [dbo].[HCFICHA452] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [LACERACION]          BIT           NULL,
    [CONFUSION]           BIT           NULL,
    [QUEMADURA]           BIT           NULL,
    [CLASIGRADO]          INT           NULL,
    [EXTENSION]           INT           NULL,
    [AMPUTACION]          BIT           NULL,
    [DANIOCULAR]          BIT           NULL,
    [DANIOAUDIT]          BIT           NULL,
    [FRACTURAS]           BIT           NULL,
    [VIAAEREA]            BIT           NULL,
    [TRAUMAABD]           BIT           NULL,
    [OTRO1]               BIT           NULL,
    [OTROCUAL]            VARCHAR (50)  NULL,
    [TIPOARTEFLESI]       INT           NULL,
    [TIPOARTEPIRO]        INT           NULL,
    [CUALOTROART]         VARCHAR (50)  NULL,
    [LESIOEFECTALCO]      BIT           NULL,
    [MENOREDADSPA]        BIT           NULL,
    [LUGAREVENTO]         INT           NULL,
    [POLVORAPIRO]         INT           NULL,
    [ARTEEXPLOMAP]        INT           NULL,
    [CUAL1]               VARCHAR (50)  NULL,
    [CUAL2]               VARCHAR (50)  NULL,
    [CARA]                BIT           NULL,
    [CUELLO]              BIT           NULL,
    [MANOS]               BIT           NULL,
    [PIES]                BIT           NULL,
    [PLIEGUES]            BIT           NULL,
    [GENITALES]           BIT           NULL,
    [TRONCO]              BIT           NULL,
    [MIEMSUP]             BIT           NULL,
    [MIEMINF]             BIT           NULL,
    [DEDOSMANO]           BIT           NULL,
    [MANO]                BIT           NULL,
    [ANTEBRAZO]           BIT           NULL,
    [BRAZO]               BIT           NULL,
    [MUSLO]               BIT           NULL,
    [PIERNA]              BIT           NULL,
    [PIE]                 BIT           NULL,
    [DEDOSPIE]            BIT           NULL,
    [HUESOSCRAN]          BIT           NULL,
    [HUESOSMANO]          BIT           NULL,
    [MIEMBROSUP]          BIT           NULL,
    [REJACOSTAL]          BIT           NULL,
    [COLUMNA]             BIT           NULL,
    [CADERA]              BIT           NULL,
    [MIEMBROINF]          BIT           NULL,
    [HUESOSPIE]           BIT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA452] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA452_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA452_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA452] NOCHECK CONSTRAINT [CK_HCFICHA452_JSON];




GO
ALTER TABLE [dbo].[HCFICHA452] NOCHECK CONSTRAINT [CK_HCFICHA452_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento de nuevas columnas en formato JSON estructurado; VARCHAR(MAX), validado con CHECK isjson().', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación; NULL indica primera versión; VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de fractura en huesos del pie (sitios anatómicos fracturados); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'HUESOSPIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Sitios anatómicos fracturados  es seleccionada  HUESOS PIE   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'HUESOSPIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'HUESOSPIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de fractura en miembro inferior completo (sitios anatómicos fracturados); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MIEMBROINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Sitios anatómicos fracturados  es seleccionada  MIEMBRO INFERIOR   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MIEMBROINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MIEMBROINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de fractura en cadera (sitios anatómicos fracturados); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CADERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Sitios anatómicos fracturados  es seleccionada  CADERA  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CADERA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CADERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de fractura en columna vertebral (sitios anatómicos fracturados); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'COLUMNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Sitios anatómicos fracturados  es seleccionada  COLOMNA  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'COLUMNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'COLUMNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de fractura en reja costal, caja torácica (sitios anatómicos fracturados); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'REJACOSTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Sitios anatómicos fracturados  es seleccionada  Reja Costal  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'REJACOSTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'REJACOSTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de fractura en miembro superior completo (sitios anatómicos fracturados); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MIEMBROSUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Sitios anatómicos fracturados  es seleccionada  MIEMBRO SUPERIOR   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MIEMBROSUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MIEMBROSUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de fractura en huesos de mano (sitios anatómicos fracturados); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'HUESOSMANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Sitios anatómicos fracturados  es seleccionada  HUESOS MANO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'HUESOSMANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'HUESOSMANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de fractura en huesos de cráneo (sitios anatómicos fracturados); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'HUESOSCRAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si  Sitios anatómicos fracturados  es seleccionada  HUESOS CRANEO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'HUESOSCRAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'HUESOSCRAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de lesión/quemadura en dedos del pie (sitios anatómicos comprometidos); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'DEDOSPIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Sitios anatómicos comprometidos  es seleccionada  DEDOS  DEL PIE   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'DEDOSPIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'DEDOSPIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de lesión/quemadura en pie (sitios anatómicos comprometidos); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'PIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Sitios anatómicos comprometidos  es seleccionada  PIE  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'PIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'PIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de lesión/quemadura en pierna (sitios anatómicos comprometidos); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'PIERNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Sitios anatómicos comprometidos  es seleccionada  PIERNA   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'PIERNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'PIERNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de lesión/quemadura en muslo (sitios anatómicos comprometidos); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MUSLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Sitios anatómicos comprometidos  es seleccionada  MUZLO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MUSLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MUSLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de lesión/quemadura en brazo (sitios anatómicos comprometidos); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'BRAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Sitios anatómicos comprometidos  es seleccionada  BRAZO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'BRAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'BRAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de lesión/quemadura en antebrazo (sitios anatómicos comprometidos); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'ANTEBRAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Sitios anatómicos comprometidos  es seleccionada  ANTEBRAZO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'ANTEBRAZO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'ANTEBRAZO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de lesión/quemadura en mano (sitios anatómicos comprometidos); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Sitios anatómicos comprometidos  es seleccionada   MANO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de lesión/quemadura en dedos de mano (sitios anatómicos comprometidos); 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'DEDOSMANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si Sitios anatómicos comprometidos  es seleccionada  DEDOS MANOS   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'DEDOSMANO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'DEDOSMANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de quemadura en miembro inferior; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MIEMINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si en caso de queaduras  es seleccionada  MIEMBRO INFERIOR   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MIEMINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MIEMINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de quemadura en miembro superior; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MIEMSUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si en caso de queaduras  es seleccionada  MIEMBRO SUPERIOR   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MIEMSUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MIEMSUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de quemadura en tronco; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'TRONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si en caso de queaduras  es seleccionada  TRONCO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'TRONCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'TRONCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de quemadura en genitales; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'GENITALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si en caso de queaduras  es seleccionada  GENITALES   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'GENITALES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'GENITALES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de quemadura en pliegues corporales; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'PLIEGUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si en caso de queaduras  es seleccionada  PLIEGUES  true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'PLIEGUES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'PLIEGUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de quemadura en pies; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'PIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si en caso de queaduras  es seleccionada  PIES   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'PIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'PIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de quemadura en manos; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MANOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si en caso de queaduras  es seleccionada  MANOS   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MANOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MANOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de quemadura en cuello; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CUELLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si en caso de queaduras  es seleccionada  CUELLO   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CUELLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CUELLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de quemadura en cara, rostro; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda si en caso de queaduras  es seleccionada  CARA   true = si    false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo descriptivo adicional VARCHAR(50); dato complementario no clasificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CUAL2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda cual2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CUAL2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CUAL2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo descriptivo adicional VARCHAR(50); dato complementario no clasificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CUAL1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda cual1   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CUAL1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CUAL1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación INT de artefactos explosivos, minas antipersonal (MAP), municiones: 1=Tránsito, 2=Contacto, 3=Actividades de Desminado, 4=Actividades de Desminado, 5=Otro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'ARTEEXPLOMAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda  Artefactos explosivos, minas antipersonal (MAP), municiones sin explosionar   1  = transito  2 = contacto  3  =   Actividades de Desminado  4 =  Actividades de Desminado  5  = Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'ARTEEXPLOMAP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'ARTEEXPLOMAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación INT de actividad con pólvora pirotécnica: 1=Almacenamiento, 2=Transporte, 3=Fabricación, 4=Manipulación, 5=Venta, 6=Observador, 7=Otro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'POLVORAPIRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Actividad en que presentó el evento  (Polvora Pirotécnica)   1 =Almacenamiento   2 = transporte  3 = Fabricación   4 =Manipulación  5 =  Venta  6 = Observador  7= otro  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'POLVORAPIRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'POLVORAPIRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación INT de lugar del evento: 1=Vivienda, 2=Vía pública, 3=Parque público, 4=Lugar de trabajo, 5=Zona rural, 6=Sin dato, 7=Otro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'LUGAREVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el lugar del evento 1 =vivienda    2 = via publica   3 =parque publico  4 = lugar de trabajo  5=zona rural    6= sin dato  7=  otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'LUGAREVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'LUGAREVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: ¿Acompañante/adulto de menor estaba bajo efectos de alcohol o SPA?; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MENOREDADSPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Si es menor de edad, el adulto o acompañante se encontraba bajo efectos de alcohol o (SPA)?  1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MENOREDADSPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'MENOREDADSPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT: ¿Lesionado estaba bajo efectos de alcohol u otras sustancias?; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'LESIOEFECTALCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'giuarda la  Lesionado estaba bajo efectos de alcohol  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'LESIOEFECTALCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'LESIOEFECTALCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción VARCHAR(50) de otro artefacto pirotécnico específico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CUALOTROART';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda ¿Cuál otro artefacto pirotécnico?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CUALOTROART';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CUALOTROART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación INT de artefacto pirotécnico: 1=Cohetes, 2=Globos, 3=Pitos, 4=Totes, 5=Volcanes, 6=Voladores, 7=Luces de Bengala, 8=Juegos para Exhibición/Eventos, 9=Sin dato, 10=Otro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'TIPOARTEPIRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guiarda  Artefacto pirotécnico  1 = Cohetes  2 = Globos  3 =pitos   4 =totes  5 =volcanes   6= voladores  7 =Luces de Bengala   8 = Juegos Pirotécnicos para Exhibición y Eventos  9 = sin dato  10 = otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'TIPOARTEPIRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'TIPOARTEPIRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación INT de tipo de artefacto que produjo lesión: 1=Artefacto Pirotécnico, 2=Mina Antipersonal, 3=Municiones sin Explosionar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'TIPOARTEFLESI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Tipo de artefacto que produjo la lesión   1 = Artefacto Pirotécnico  2 =Mina Antipersonal    3 =  Municiones sin Explosionar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'TIPOARTEFLESI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'TIPOARTEFLESI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción VARCHAR(50) de otra lesión/evento no clasificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'OTROCUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Otro ¿Cuál? ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'OTROCUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'OTROCUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de presencia de otra lesión no especificada; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'OTRO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda otro  1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'OTRO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'OTRO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de trauma abdominal, lesión interna del abdomen; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'TRAUMAABD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  Trauma abdominal   1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'TRAUMAABD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'TRAUMAABD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de compromiso de vía aérea, afectación respiratoria; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'VIAAEREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Vía aérea  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'VIAAEREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'VIAAEREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de presencia de fracturas óseas; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'FRACTURAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Fracturas  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'FRACTURAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'FRACTURAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de daño auditivo, sordera, hipoacusia; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'DANIOAUDIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el  Daño auditivo   1 = Di  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'DANIOAUDIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'DANIOAUDIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de daño ocular, ceguera, pérdida de visión; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'DANIOCULAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el  Daño ocular  1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'DANIOCULAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'DANIOCULAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de amputación, pérdida de extremidad; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'AMPUTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda amputacion   1 = Si   0= No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'AMPUTACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'AMPUTACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación INT de extensión de quemadura: 1=Menor o igual 5%, 2=De 6% a 14%, 3=Mayor o igual 15%.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'EXTENSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda extension  1 = Menor o Igual al 5%  2  =Del 6% al 14%  3 = Mayor o igual al 15%', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'EXTENSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'EXTENSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación INT de grado de quemadura: 1=Primer grado, 2=Segundo grado, 3=Tercer grado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CLASIGRADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Clasificación grado  1 = primer grado  2 = segundo grado   3 = tercer grado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CLASIGRADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CLASIGRADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de presencia de quemadura, lesión térmica; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'QUEMADURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la  QUEMADURA 1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'QUEMADURA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'QUEMADURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de confusión mental, alteración del estado de conciencia; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CONFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la   CONFUSION 1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CONFUSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CONFUSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT de laceración, herida abierta, desgarro; 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'LACERACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la lanceracion  1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'LACERACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'LACERACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CHAR(4), clasificación CIE-10 o similar; PII potencial según contexto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda  codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador INT de ficha de notificación; FK a HCFICHANOTIFICACION(ID); vinculación obligatoria a evento de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador secuencial INT IDENTITY(1,1); clave primaria, consecutivo único de registro de lesión/evento traumático.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle clínico de lesiones registradas en la ficha de notificación de accidentes por pólvora, quemaduras y explosivos (formulario 452). Guarda el tipo de lesión, las zonas del cuerpo afectadas, el agente causante, el lugar del evento y factores asociados como alcohol o menores de edad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA452';
