CREATE TABLE [dbo].[HCFICHA550] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [SITIODEFUN]          INT           NULL,
    [CONVIVENCIA]         INT           NULL,
    [MARCOTROCUAL]        VARCHAR (50)  NULL,
    [ESCOLARIDAD]         INT           NULL,
    [REGULAFECUNDI]       INT           NULL,
    [GESTACIONES]         VARCHAR (50)  NULL,
    [PARTVAGINA]          VARCHAR (50)  NULL,
    [CESAREAS]            VARCHAR (50)  NULL,
    [MUERTOS]             VARCHAR (50)  NULL,
    [VIVOS]               VARCHAR (50)  NULL,
    [ABORTOS]             VARCHAR (50)  NULL,
    [NINGUNO]             BIT           NULL,
    [HIPERTCRON]          BIT           NULL,
    [CARDIOPATIAS]        BIT           NULL,
    [DIABETES]            BIT           NULL,
    [MOLAHIDATI]          BIT           NULL,
    [RNPRETERMINO]        BIT           NULL,
    [RNBAJOPESO]          BIT           NULL,
    [RNMASOCROMI]         BIT           NULL,
    [TRASTORMENT]         BIT           NULL,
    [OBESIDAD]            BIT           NULL,
    [DESNUTRICRON]        BIT           NULL,
    [INTERGEMENOR]        BIT           NULL,
    [ITS]                 BIT           NULL,
    [VIHSIDA]             BIT           NULL,
    [OTRASINFECC]         BIT           NULL,
    [RHNEGATIVO]          BIT           NULL,
    [TABAQUISMO]          BIT           NULL,
    [ALCOHOLIS]           BIT           NULL,
    [SUSTPSICO]           BIT           NULL,
    [DEFICISOCIO]         BIT           NULL,
    [SIFILIS]             BIT           NULL,
    [HEPATITISB]          BIT           NULL,
    [OTROFACTRIES]        BIT           NULL,
    [GINGIVITIS]          BIT           NULL,
    [PREECLAMPSIA]        BIT           NULL,
    [ECLAMPSIA]           BIT           NULL,
    [SINDROHELLP]         BIT           NULL,
    [DIABETESGEST]        BIT           NULL,
    [SEPSIS]              BIT           NULL,
    [HEMO1ERSEME]         BIT           NULL,
    [HEMO2DOSEME]         BIT           NULL,
    [HEMO3ERTRIME]        BIT           NULL,
    [DESPROPCEFALO]       BIT           NULL,
    [RETARCRECINTR]       BIT           NULL,
    [ENFEAUTOIN]          BIT           NULL,
    [MALARIA]             BIT           NULL,
    [EMBANODESEA]         BIT           NULL,
    [VIOLECONTRA]         BIT           NULL,
    [OTRACOMPLICA]        BIT           NULL,
    [GESTPRODVIOL]        BIT           NULL,
    [FETOINCOMPA]         BIT           NULL,
    [SINTODEPRES]         BIT           NULL,
    [MARCOTROFAC]         VARCHAR (50)  NULL,
    [MARCOTRACOMP]        VARCHAR (50)  NULL,
    [NOCPN]               VARCHAR (50)  NULL,
    [SEMAINICCPN]         VARCHAR (50)  NULL,
    [CONTROREALIZA]       INT           NULL,
    [NIVELATENC1]         INT           NULL,
    [NIVELATENC2]         INT           NULL,
    [REMISIOPORTU]        INT           NULL,
    [COMPLIFETORNCIE]     VARCHAR (50)  NULL,
    [MOMENOCURMUE]        INT           NULL,
    [SEMAGESTMORT]        VARCHAR (50)  NULL,
    [FECHAHORA]           DATETIME      NULL,
    [HORA]                DATETIME      NULL,
    [TIPOPARTO]           INT           NULL,
    [PARTOATENDI]         INT           NULL,
    [OTROQUIEN]           VARCHAR (50)  NULL,
    [CAUSADEFUN]          VARCHAR (50)  NULL,
    [CAUSAMUERTE]         INT           NULL,
    [DEMORA1]             VARCHAR (50)  NULL,
    [DEMORA2]             VARCHAR (50)  NULL,
    [DEMORA3]             VARCHAR (50)  NULL,
    [DEMORA4]             VARCHAR (50)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA550] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHA550_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento de campos adicionales en formato JSON (VARCHAR MAX). Documentación complementaria de la ficha de notificación de muerte materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guardan otros campos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de versión de la estructura de la ficha. Control de cambios: V:03 desde 2024-03-01, vigente desde 21/06/2024 (PBI 18049, 18148).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuarta demora en atención de muerte materna (categoría de demora). OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tercera demora en atención de muerte materna (categoría de demora). OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segunda demora en atención de muerte materna (categoría de demora). OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primera demora en atención de muerte materna (categoría de demora). OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEMORA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de causa de muerte materna (clasificación de causa raíz). OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CAUSAMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CAUSAMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CAUSAMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción causa de defunción (diagnóstico de muerte materna, causa raíz). OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CAUSADEFUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CAUSADEFUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CAUSADEFUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otro profesional que atendió el parto (cuando no es médico, enfermera o partera). VARCHAR(50). Relacionado con muerte materna sección 7.4.1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTROQUIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Otro quien (datos relacionados con la muerte materna 7.4.1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTROQUIEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTROQUIEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de profesional que atendió el parto (médico, enfermera, partera, otro). INT. Dato relacionado con muerte materna y complicaciones obstétricas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'PARTOATENDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parto atendido por (datos relacionados con la muerte materna)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'PARTOATENDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'PARTOATENDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modalidad del parto (espontáneo, inducido, instrumentado). INT. Antecedente de embarazo y complicaciones obstétricas de muerte materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo del parto (datos relacionados con la muerte materna)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'TIPOPARTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de evento (timestamp de hora). OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del evento de muerte materna o complicación obstétrica. DATETIME. Marca temporal de notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'FECHAHORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Fecha y hora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'FECHAHORA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'FECHAHORA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semana de gestación en que ocurrió la muerte (semanas de embarazo al momento del evento). OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SEMAGESTMORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SEMAGESTMORT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SEMAGESTMORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Momento de ocurrencia de muerte materna. INT: 5=Durante gestación, 6=En últimos 42 días post-parto, 7=Entre 43 días y 1 año. RIPS/Vigilancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MOMENOCURMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Momento en que ocurrio la muerte (5.Durante la gestación, 6.En los últimos 42 días posterior al parto, 7. Entre 43 días y un año antes del parto) datos relacionados con la muerte materna', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MOMENOCURMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MOMENOCURMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Complicación fetal o neonatal al momento de nacimiento. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'COMPLIFETORNCIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'COMPLIFETORNCIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'COMPLIFETORNCIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Remisión a oportunidad en atención perinatal. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'REMISIOPORTU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'REMISIOPORTU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'REMISIOPORTU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nivel de atención en que se atendió la urgencia obstétrica. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NIVELATENC2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NIVELATENC2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NIVELATENC2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nivel de atención en que se atendió la urgencia obstétrica. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NIVELATENC1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NIVELATENC1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NIVELATENC1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de controles prenatales realizados en el embarazo actual. INT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CONTROREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CONTROREALIZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CONTROREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semana de gestación al inicio de control prenatal (CPN). Antecedente de control gestacional. VARCHAR(50).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SEMAINICCPN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Semana gestacional al inicio de CPN (antecedentes prenatales del embarazo actual)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SEMAINICCPN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SEMAINICCPN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de controles prenatales realizados en embarazo actual. VARCHAR(50). Antecedente de control gestacional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NOCPN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de controles prenatales (antecedentes prenatales del embarazo actual)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NOCPN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NOCPN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marco de tratamiento de complicación obstétrica. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MARCOTRACOMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MARCOTRACOMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MARCOTRACOMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marco de tratamiento de factor de riesgo obstétrico. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MARCOTROFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MARCOTROFAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MARCOTROFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntomas de depresión (salud mental perinatal). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SINTODEPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SINTODEPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SINTODEPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Incompatibilidad fetal o anomalía congénita grave. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'FETOINCOMPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'FETOINCOMPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'FETOINCOMPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Gestación producto de violencia sexual. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'GESTPRODVIOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'GESTPRODVIOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'GESTPRODVIOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otra complicación obstétrica no clasificada. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTRACOMPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTRACOMPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTRACOMPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Violencia intrafamiliar o de pareja durante gestación. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VIOLECONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VIOLECONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VIOLECONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Embarazo no deseado. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'EMBANODESEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'EMBANODESEA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'EMBANODESEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de malaria (paludismo) en gestación. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MALARIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MALARIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MALARIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enfermedad autoinmune (LES, síndrome antifosfolípido, etc.). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ENFEAUTOIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ENFEAUTOIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ENFEAUTOIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retraso del crecimiento intrauterino (RCIU). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RETARCRECINTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RETARCRECINTR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RETARCRECINTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desproporción cefalopélvica (incompatibilidad pelvis-feto). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DESPROPCEFALO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DESPROPCEFALO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DESPROPCEFALO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemorragia del tercer trimestre de embarazo. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEMO3ERTRIME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEMO3ERTRIME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEMO3ERTRIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemorragia del segundo trimestre de embarazo. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEMO2DOSEME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEMO2DOSEME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEMO2DOSEME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hemorragia del primer trimestre de embarazo. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEMO1ERSEME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEMO1ERSEME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEMO1ERSEME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Infección severa/sepsis materna durante gestación o puerperio. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SEPSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SEPSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SEPSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diabetes gestacional (hiperglucemia en embarazo). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DIABETESGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DIABETESGEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DIABETESGEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síndrome HELLP (hemólisis, enzimas elevadas, plaquetas bajas). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SINDROHELLP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SINDROHELLP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SINDROHELLP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Eclampsia (crisis convulsiva en gestación/puerperio). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ECLAMPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ECLAMPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ECLAMPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Preeclampsia (hipertensión gestacional con proteinuria). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'PREECLAMPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'PREECLAMPSIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'PREECLAMPSIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Enfermedad bucal/gingivitis (salud oral perinatal). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'GINGIVITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'GINGIVITIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'GINGIVITIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otro factor de riesgo no clasificado en embarazo. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTROFACTRIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTROFACTRIES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTROFACTRIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de Hepatitis B. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEPATITISB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEPATITISB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HEPATITISB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de sífilis (ITS). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SIFILIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SIFILIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SIFILIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Deficiencia socioeconómica. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEFICISOCIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEFICISOCIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DEFICISOCIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consumo de sustancias psicoactivas. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SUSTPSICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SUSTPSICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SUSTPSICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de alcoholismo o consumo de alcohol. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ALCOHOLIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ALCOHOLIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ALCOHOLIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de tabaquismo o fumadora activa. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'TABAQUISMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'TABAQUISMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'TABAQUISMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor Rh negativo (antígeno de grupo sanguíneo). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RHNEGATIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RHNEGATIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RHNEGATIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otras infecciones (TB, toxoplasmosis, etc.). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTRASINFECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTRASINFECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OTRASINFECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de VIH/SIDA. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VIHSIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VIHSIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VIHSIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Infecciones de transmisión sexual (gonorrea, clamidia, etc.). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ITS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ITS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ITS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intervalo intergenésico menor a 2 años. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'INTERGEMENOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'INTERGEMENOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'INTERGEMENOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desnutrición crónica materna. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DESNUTRICRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DESNUTRICRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DESNUTRICRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obesidad pregestacional (IMC ≥ 30). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OBESIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'OBESIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Trastorno mental (depresión, psicosis, bipolaridad, etc.). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'TRASTORMENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'TRASTORMENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'TRASTORMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recién nacido con mayor oscromía (hipoxia perinatal). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RNMASOCROMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RNMASOCROMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RNMASOCROMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recién nacido de bajo peso (< 2500 g). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RNBAJOPESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RNBAJOPESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RNBAJOPESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recién nacido prematuro (< 37 semanas de gestación). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RNPRETERMINO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RNPRETERMINO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'RNPRETERMINO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mola hidatidiforme (gestación anormal). BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MOLAHIDATI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MOLAHIDATI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MOLAHIDATI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de diabetes mellitus pregestacional. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DIABETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'DIABETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de cardiopatía o enfermedad cardiovascular. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CARDIOPATIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CARDIOPATIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CARDIOPATIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hipertensión arterial crónica pregestacional. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HIPERTCRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HIPERTCRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'HIPERTCRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sin antecedentes o comorbilidades relevantes. BIT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NINGUNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NINGUNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'NINGUNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de abortos previos (espontáneos, inducidos, quirúrgicos). VARCHAR(50). Antecedente materno obstétrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ABORTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el número de abortos (Antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ABORTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ABORTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de hijos vivos previos. VARCHAR(50). Antecedente materno, paridad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el número de vivos (antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VIVOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'VIVOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de muertes fetales/neonatales previas. VARCHAR(50). Antecedente materno de pérdida perinatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MUERTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de muertos (antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MUERTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MUERTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de partos por cesárea previos. VARCHAR(50). Antecedente quirúrgico materno obstétrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CESAREAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el numero de cesareas (Antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CESAREAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CESAREAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de partos vaginales previos. VARCHAR(50). Antecedente materno obstétrico, paridad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'PARTVAGINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de partos vaginales (antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'PARTVAGINA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'PARTVAGINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de gestaciones (paridad). VARCHAR(50). Antecedente materno: G (gestaciones).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'GESTACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el número de gestaciones (antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'GESTACIONES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'GESTACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Método de regulación de la fecundidad usado (anticonceptivos). INT. Antecedente materno de planificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'REGULAFECUNDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Regulación de la fecundidad (antecedentes maternos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'REGULAFECUNDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'REGULAFECUNDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grado de escolaridad materno (primaria, secundaria, superior). INT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ESCOLARIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ESCOLARIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ESCOLARIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marco de tratamiento de factor de riesgo cualitativo. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MARCOTROCUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MARCOTROCUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'MARCOTROCUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de convivencia (con pareja, sola, familia). INT. OBSOLETO desde V:03 2024-03-01: se almacenará NULL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CONVIVENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo queda obsoleto con versionamiento V:03 2024-03-01 desde 21/06/2024 cambios pedidos en PBI 18049, 18148 en esta columna se almacenaran nulos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CONVIVENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CONVIVENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar donde ocurrió la muerte materna (hogar, clínica, calle, transporte). INT. Dato de vigilancia epidemiológica, acceso a atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SITIODEFUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio donde ocurrio la muerte (datos relacionados con la muerte materna)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SITIODEFUN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'SITIODEFUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico CIE-10 principal de la causa de muerte. CHAR(4). Clasificación internacional de enfermedades.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ficha de notificación padre (FK → HCFICHANOTIFICACION.ID). INT NOT NULL. Relación 1:1 con notificación de muerte materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (clave primaria). INT IDENTITY(1,1). Registro de ficha de muerte materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación 550 para mortalidad materna y perinatal. Registra los antecedentes obstétricos, factores de riesgo, complicaciones durante la gestación, datos del parto y causas de muerte de la madre o el recién nacido, usada para vigilancia epidemiológica y reporte al SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA550';
