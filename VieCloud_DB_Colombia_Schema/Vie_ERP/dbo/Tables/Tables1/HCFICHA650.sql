CREATE TABLE [dbo].[HCFICHA650] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [NOMBEVENTO]          VARCHAR (500) NULL,
    [CODEVENTO]           VARCHAR (10)  NULL,
    [FECHANOTIFICA]       DATETIME      NULL,
    [RAZONSOCIAL]         VARCHAR (100) NULL,
    [UPGD]                VARCHAR (50)  NULL,
    [CLASIFICACION]       TINYINT       NULL,
    [PROPIETARIO]         VARCHAR (100) NULL,
    [RESIDENCIA]          VARCHAR (100) NULL,
    [TELEFONO]            VARCHAR (50)  NULL,
    [DEPMUNCOD]           CHAR (5)      NULL,
    [ESPECIE]             TINYINT       NULL,
    [RAZA]                VARCHAR (100) NULL,
    [COLOR]               VARCHAR (100) NULL,
    [EDAD]                TINYINT       NULL,
    [UNIDAD]              TINYINT       NULL,
    [ANTECEDENTE]         TINYINT       NULL,
    [FECHAVACUNA]         DATETIME      NULL,
    [AREA]                TINYINT       NULL,
    [AGRESIVIDAD]         BIT           NULL,
    [PARALISIS]           BIT           NULL,
    [SALIVACION]          BIT           NULL,
    [APETITO]             BIT           NULL,
    [VORACIDAD]           BIT           NULL,
    [DEGLUCION]           BIT           NULL,
    [LADRIDO]             BIT           NULL,
    [MANDIBULA]           BIT           NULL,
    [ANISOCORIA]          BIT           NULL,
    [OTROSIGNO]           BIT           NULL,
    [CUALSIGNO]           VARCHAR (100) NULL,
    [FECHASINTOMA]        DATETIME      NULL,
    [TIPOMUERTE]          TINYINT       NULL,
    [FECHAMUERTE]         DATETIME      NULL,
    [INFOLABORATORIO]     BIT           NULL,
    [FECHATOMA]           DATETIME      NULL,
    [FECHAREMISION]       DATETIME      NULL,
    [PRUEBADX]            TINYINT       NULL,
    [RESULTADO]           TINYINT       NULL,
    [IDENTIFICACION]      BIT           NULL,
    [VARIANTE]            TINYINT       NULL,
    [CUALVARIANTE]        VARCHAR (100) NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA650] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA650_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA650_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA650] NOCHECK CONSTRAINT [CK_HCFICHA650_JSON];




GO
ALTER TABLE [dbo].[HCFICHA650] NOCHECK CONSTRAINT [CK_HCFICHA650_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Payload en formato JSON con datos adicionales o campos extendidos de la notificación de evento en salud animal (VARCHAR MAX, validado con ISJSON)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión del formato y estructura de la ficha de notificación epidemiológica, ej: V01_2020-03-06 (DATETIME de inicio de versión)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación --->primer versión : V01_2020-03-06', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre de otra variante identificada del agente patógeno cuando VARIANTE=0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CUALVARIANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuál otra variante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CUALVARIANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CUALVARIANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Variante del agente identificada (0=Otra, 1, 3, 4, 5, 8); clasificación de cepa o subtipo viral/patógeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'VARIANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Variante identificada:   1 - 1  3 - 3  4 - 4  5 - 5  8 - 8  0 - 0. Otra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'VARIANTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'VARIANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de si la variante fue identificada/confirmada (True=Sí, False=No; BIT, PII epidemiológico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'IDENTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación variante:   True: Sí  False: No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'IDENTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'IDENTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de prueba diagnóstica (1=Positivo, 2=Negativo, 3=Inadecuado, 4=Pendiente análisis)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado:   1- Positivo   2- Negativo   3- Inadecuado   4- Pendiente   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prueba diagnóstica realizada (1=IFD/Inmunofluorescencia, 2=Prueba biológica/cultivo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'PRUEBADX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prueba diagnóstica:   1- IFD   2- Prueba biológica   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'PRUEBADX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'PRUEBADX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de envío o remisión de la muestra al laboratorio de confirmación (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHAREMISION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha remisión de muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHAREMISION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHAREMISION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recolección/toma de muestra del animal (DATETIME, crítica para epidemiología)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha toma de muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHATOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHATOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de disponibilidad de información de laboratorio (True=Sí hay datos lab, False=No hay; BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'INFOLABORATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Información de Laboratorio:  True: Sí hay información de labotorio  False: No hay información de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'INFOLABORATORIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'INFOLABORATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de muerte del animal (DATETIME, evento crítico en vigilancia de rabia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHAMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHAMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHAMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Circunstancia de muerte (1=Espontánea, 2=Sacrificio, 3=Accidente, 4=Desconocida)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'TIPOMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de muerte:   1- Espontánea   2- Sacrificio   3- Accidente   4- Desconocida    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'TIPOMUERTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'TIPOMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de síntomas clínicos en el animal (DATETIME, referencia epidemiológica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHASINTOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de síntomas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHASINTOMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHASINTOMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otro signo o síntoma no listado en campos específicos (TEXT libre)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CUALSIGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual Otro (Signo y sintoma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CUALSIGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CUALSIGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de presencia de signos/síntomas adicionales no codificados (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'OTROSIGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro (signo y sintoma)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'OTROSIGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'OTROSIGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de anisocoria (dilatación desigual de pupilas); signo neurológico de rabia (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ANISOCORIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Anisocoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ANISOCORIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ANISOCORIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mandíbula trabada o caída; signo de parálisis facial en rabia animal (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'MANDIBULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mandibula trabada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'MANDIBULA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'MANDIBULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ladrido ronco o cambio en vocalización; signo de rabia canina (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'LADRIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ladrido ronco', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'LADRIDO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'LADRIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dificultad para deglutir (disfagia); signo compatible con rabia (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'DEGLUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Deglución dificultosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'DEGLUCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'DEGLUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comportamiento de voracidad o hambre exagerada en animal (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'VORACIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Voracidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'VORACIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'VORACIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Apetito alterado, anorexia o rechazo de alimento (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'APETITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Apetito alterado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'APETITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'APETITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Salivación excesiva o espumosa; signo clásico de rabia (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'SALIVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Salivación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'SALIVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'SALIVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parálisis de miembros posteriores; signo de rabia paralítica (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'PARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parálisis miembros posteriores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'PARALISIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'PARALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comportamiento agresivo o irritabilidad del animal; signo de rabia furiosa (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'AGRESIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agresividad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'AGRESIVIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'AGRESIVIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Área geográfica de procedencia del animal (1=Cabecera municipal, 2=Centro poblado, 3=Rural disperso)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'AREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Área de procedencia del animal:   1- Cabecera municipal   2- Centro poblado   3- Rural disperso   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'AREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'AREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de última vacunación antirrábica del animal (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHAVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vacunación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHAVACUNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHAVACUNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente de vacunación antirrábica (1=Sí vacunado, 2=No vacunado, 3=Desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ANTECEDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Antecedente de vacunación:   1- Sí  2- No  3- Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ANTECEDENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ANTECEDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida para la edad del animal (1=Años, 2=Meses)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'UNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida de edad:   1- Años   2- Meses   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'UNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'UNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Edad del animal en la unidad especificada (TINYINT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'EDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'EDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Color del pelaje o características pigmentarias visibles del animal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'COLOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Color de la cabeza del animal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'COLOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Raza o tipo de animal (ej: labrador, siames, mongrel, criolla)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RAZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Raza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RAZA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RAZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especie animal afectada (1=Perro, 2=Gato, 3=Zorro, 4=Murciélago; epidemiología de rabia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ESPECIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especie:   1- Perro   2- Gato   3- Zorro   4- Murciélago   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ESPECIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ESPECIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código DANE de departamento y municipio de residencia del propietario (CHAR 5)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del departamento y municipio de residencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'DEPMUNCOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de contacto del propietario o responsable del animal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Télefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'TELEFONO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección completa de residencia del propietario (localización de caso)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RESIDENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dirección de residencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RESIDENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RESIDENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del propietario o responsable del animal (PII, Identification_Ofuscado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'PROPIETARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombres y apellidos del propietario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'PROPIETARIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'PROPIETARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación epidemiológica del caso (1=Probable, 2=Confirmado por laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificación inicial del caso:   1 - Probable   2- Confirmado por laboratorio   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad Primaria Generadora de Datos (UPGD) que notifica el evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'UPGD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la UPGD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'UPGD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'UPGD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Razón social, nombre institucional o establecimiento de la UPGD notificante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RAZONSOCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Razón social de la Unidad primaria generadora de datos UPGD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RAZONSOCIAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'RAZONSOCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de notificación oficial del evento al sistema de vigilancia (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHANOTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHANOTIFICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'FECHANOTIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del evento epidemiológico (ej: rabia animal, evento zoonótico)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CODEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CODEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CODEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del evento notificado (ej: Rabia, Brucellosis, Leptospirosis animal)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'NOMBEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'NOMBEVENTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'NOMBEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico en formato CIE-10 veterinario (CHAR 4, ej: A82 para rabia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la ficha matriz de notificación (FK a HCFICHANOTIFICACION.ID)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental de registro (PK, INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación epidemiológica 650 para eventos de agresión por animales potencialmente transmisores de rabia (APTR). Registra los datos del animal agresor, signos clínicos observados, antecedentes de vacunación, resultados de laboratorio y clasificación del evento, según el protocolo de vigilancia del INS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA650';
