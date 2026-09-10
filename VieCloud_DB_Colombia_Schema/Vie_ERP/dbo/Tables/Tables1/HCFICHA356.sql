CREATE TABLE [dbo].[HCFICHA356] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [FECHAOCURRENCIA]     DATE          NULL,
    [INTENPREVIOS]        BIT           NULL,
    [NUMINTENTOS]         INT           NULL,
    [ESTADOCIVIL]         INT           NULL,
    [ESCOLARIDAD]         INT           NULL,
    [CONFLIPAREJA]        BIT           NULL,
    [PROBLEJURIDICOS]     BIT           NULL,
    [ENFERCRONICA]        BIT           NULL,
    [SUICIDIOFAMI]        BIT           NULL,
    [PROBLEECONO]         BIT           NULL,
    [MALTRAFISICO]        BIT           NULL,
    [MUERTEFAMI]          BIT           NULL,
    [PROBLELABOR]         BIT           NULL,
    [ESCOLAR]             BIT           NULL,
    [CONSUMOSPA]          BIT           NULL,
    [ANTECEFAMI]          BIT           NULL,
    [IDEASUICIDA]         BIT           NULL,
    [PLANSUICIDIO]        BIT           NULL,
    [ANTEVIOLENCIA]       BIT           NULL,
    [ABUSOALCOHOL]        BIT           NULL,
    [ANTEPSQUIATRI]       BIT           NULL,
    [AHORCAMI]            BIT           NULL,
    [ELEMCORTO]           BIT           NULL,
    [ARMAFUEGO]           BIT           NULL,
    [INMOLACION]          BIT           NULL,
    [LANZAVACIO]          BIT           NULL,
    [LANZAVEHI]           BIT           NULL,
    [LANZACUERPO]         BIT           NULL,
    [INTOXICACION]        BIT           NULL,
    [CODPRODUC]           VARCHAR (50)  NULL,
    [NOMBREPRODUC]        VARCHAR (50)  NULL,
    [VIAEXPO]             INT           NULL,
    [LUGARINTOX]          INT           NULL,
    [PSIQUIATRIA]         BIT           NULL,
    [PSICOLOGIA]          BIT           NULL,
    [TRABAJSOCIAL]        BIT           NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [CASOTRASTOR]         INT           NULL,
    [CASOINTOX]           INT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA356] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA356_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA356_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA356] NOCHECK CONSTRAINT [CK_HCFICHA356_JSON];




GO
ALTER TABLE [dbo].[HCFICHA356] NOCHECK CONSTRAINT [CK_HCFICHA356_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON (VARCHAR MAX). Nulo en versión antigua; desde V01_2020-03-06 incluye campos como PROBLEFAMILIAR (bit). Válida estructura JSON verificada en BD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON  -->  (versión antigua) = nulo   /    (versiones nuevas, desde V01_2020-03-06 ) = PROBLEFAMILIAR = bit (Problema Familiar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación de conducta suicida. Nulo = primera versión; valores posteriores indican revisiones del registro (ej: V01_2020-03-06).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de caso de intoxicación registrado en la ficha de notificación (mecanismo de suicidio por exposición a sustancias).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CASOINTOX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Caso de la intoxicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CASOINTOX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CASOINTOX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de caso de trastorno mental o psiquiátrico registrado en la notificación (clasificación de diagnóstico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CASOTRASTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Caso del trastorno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CASOTRASTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CASOTRASTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 (4 caracteres). Identifica el trastorno mental, conducta suicida o intoxicación notificados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de remisión a Trabajo Social (bit). Parte del plan de manejo en salud mental para apoyo social y económico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'TRABAJSOCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio de trabajo social (remisión a salud mental)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'TRABAJSOCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'TRABAJSOCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de remisión a Psicología (bit). Servicio de atención en salud mental para evaluación y seguimiento psicológico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PSICOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio de psicologia (remisión a salud mental)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PSICOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PSICOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de remisión a Psiquiatría (bit). Servicio especializado de salud mental para evaluación diagnóstica y farmacoterapia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PSIQUIATRIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Servicio de psiquiatria (remisión a salud mental)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PSIQUIATRIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PSIQUIATRIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar donde ocurrió la intoxicación (mecanismo de suicidio). Referencia a tabla de codificación de ubicaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LUGARINTOX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar donde se produjo la intoxicación  (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LUGARINTOX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LUGARINTOX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de exposición o administración del tóxico (mecanismo de suicidio: oral, inhalatoria, inyectada, dérmica, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'VIAEXPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Via de la exposición (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'VIAEXPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'VIAEXPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre comercial o genérico del producto/sustancia usada en intoxicación (mecanismo de suicidio). Texto descriptivo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'NOMBREPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del producto (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'NOMBREPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'NOMBREPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del producto tóxico (mecanismo de suicidio). Puede ser código interno o estándar de sustancias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del producto (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de intoxicación como mecanismo de suicidio (bit). Marca si el evento implicó exposición a sustancias tóxicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'INTOXICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Intoxicación (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'INTOXICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'INTOXICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de lanzamiento a cuerpo de agua como mecanismo de suicidio (bit). Ahogamiento intencional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LANZACUERPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salto a un cuerpo de agua (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LANZACUERPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LANZACUERPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de lanzamiento a vehículo como mecanismo de suicidio (bit). Impacto intencional con transporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LANZAVEHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salto a un vehiculo (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LANZAVEHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LANZAVEHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de salto al vacío como mecanismo de suicidio (bit). Caída intencional desde altura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LANZAVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salto al vacio (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LANZAVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'LANZAVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de inmolación (quema intencional) como mecanismo de suicidio (bit).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'INMOLACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Inmolación (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'INMOLACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'INMOLACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de arma de fuego como mecanismo de suicidio (bit). Disparo intencional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ARMAFUEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Arma de fuego (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ARMAFUEGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ARMAFUEGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de elemento cortopunzante como mecanismo de suicidio (bit). Cuchillo, vidrio, navaja u objeto similar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ELEMCORTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Elemento cortopunzante  (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ELEMCORTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ELEMCORTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de ahorcamiento o asfixia como mecanismo de suicidio (bit). Constricción de vías aéreas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'AHORCAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ahorcamiento o asfixia (Mecanismo de suicidio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'AHORCAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'AHORCAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de trastorno psiquiátrico diagnosticado previamente (bit). Depresión, esquizofrenia, trastorno bipolar, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ANTEPSQUIATRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedente de trastorno psiquiatrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ANTEPSQUIATRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ANTEPSQUIATRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de abuso o dependencia del alcohol (bit). Factor de riesgo en conducta suicida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ABUSOALCOHOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Abuso del alcohol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ABUSOALCOHOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ABUSOALCOHOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedentes de violencia o abuso previo (bit). Víctima de maltrato físico, psicológico, sexual o intrafamiliar.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ANTEVIOLENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedentes de violencia o abuso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ANTEVIOLENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ANTEVIOLENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de plan suicida organizado y estructurado (bit). Mayor letalidad que ideas sin planificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PLANSUICIDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plan organizado de suicidio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PLANSUICIDIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PLANSUICIDIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de ideas suicidas persistentes (bit). Pensamiento recurrente sobre muerte o autoeliminación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'IDEASUICIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ideas suicidas persistente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'IDEASUICIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'IDEASUICIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de conducta suicida en familia (bit). Intento o suicidio consumado en familiar cercano.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ANTECEFAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedente de conducta suicida (familiar)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ANTECEFAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ANTECEFAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de consumo de sustancias psicoactivas - SPA (bit). Drogas ilícitas, cannabis, cocaína u otras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CONSUMOSPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consumo de sustancias psicoactivas (SPA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CONSUMOSPA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CONSUMOSPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel escolar o educativo alcanzado por el paciente (int). Referencia a tabla de escolaridad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ESCOLAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel escolar alcanzado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ESCOLAR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ESCOLAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de problemas laborales actuales (bit). Desempleo, despido, conflicto laboral o insatisfacción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PROBLELABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Problemas laborales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PROBLELABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PROBLELABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de muerte reciente de familiar o persona cercana (bit). Factor estresante importante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'MUERTEFAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Muerte familiar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'MUERTEFAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'MUERTEFAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de antecedentes de maltrato físico (bit). Violencia física intrafamiliar o externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'MALTRAFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Maltrato físico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'MALTRAFISICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'MALTRAFISICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de problemas económicos o financieros (bit). Pobreza, deuda o dificultad económica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PROBLEECONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Problemas económicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PROBLEECONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PROBLEECONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de suicidio consumado en familiar o amigo cercano (bit). Factor de riesgo importante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'SUICIDIOFAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Suicidio de un familiar o amigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'SUICIDIOFAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'SUICIDIOFAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de enfermedad crónica o incurable (bit). Cáncer, VIH, dolor crónico u otra patología grave.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ENFERCRONICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Enfermedad crónica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ENFERCRONICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ENFERCRONICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de problemas jurídicos o legales (bit). Demandas, detenciones, cargos penales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PROBLEJURIDICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Problema juridico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PROBLEJURIDICOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'PROBLEJURIDICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de conflicto con pareja o expareja (bit). Separación, violencia intrafamiliar o ruptura emocional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CONFLIPAREJA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Conflicto con la pareja o expareja', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CONFLIPAREJA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'CONFLIPAREJA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de escolaridad del paciente (int). Primaria, secundaria, superior. Referencia a tabla de clasificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ESCOLARIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel de escolaridad del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ESCOLARIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ESCOLARIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado civil del paciente (int). Soltero, casado, divorciado, viudo, unión libre. Referencia a tabla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ESTADOCIVIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estádo civil del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ESTADOCIVIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ESTADOCIVIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de intentos de suicidio previos (int). Cantidad de episodios registrados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'NUMINTENTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de intentos de suicidio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'NUMINTENTOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'NUMINTENTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de intento previo de suicidio (bit). Verdadero si hay antecedente de tentativa anterior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'INTENPREVIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Intento previo de suicidio (true or false)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'INTENPREVIOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'INTENPREVIOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que ocurrió el evento de conducta suicida o intoxicación (DATE). Registro de la notificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'FECHAOCURRENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la ocurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'FECHAOCURRENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'FECHAOCURRENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ficha de notificación de conducta suicida (FK a HCFICHANOTIFICACION). Vínculo con evento principal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla HCFICHANOTIFICACION (Id)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y consecutivo del registro en HCFICHA356 (INT Identity). Clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación de intento de suicidio (ficha 356). Registra los factores de riesgo, antecedentes, método utilizado, circunstancias psicosociales y atención requerida para cada evento de conducta suicida o intoxicación asociada notificado en la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA356';
