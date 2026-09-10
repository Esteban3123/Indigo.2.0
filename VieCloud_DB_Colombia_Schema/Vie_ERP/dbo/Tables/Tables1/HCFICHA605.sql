CREATE TABLE [dbo].[HCFICHA605] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [VACUNAROTAVIRUS]     INT           NULL,
    [FECHAPLIDOSIS1]      DATE          NULL,
    [FECHAPLIDOSIS2]      DATE          NULL,
    [TIENECARNET]         BIT           NULL,
    [PESONACER]           INT           NULL,
    [LECHEMATERNA]        INT           NULL,
    [TIEMPOLECHE]         INT           NULL,
    [ALIMENACTUAL]        INT           NULL,
    [FIEBRE]              INT           NULL,
    [VOMITO]              INT           NULL,
    [NUMVOMITO]           INT           NULL,
    [FECHAINIDIA]         DATE          NULL,
    [NUMDEPOSIC]          INT           NULL,
    [FECTERMDIA]          DATE          NULL,
    [HECES]               INT           NULL,
    [CUALHECES]           VARCHAR (200) NULL,
    [ESTADOINGRE]         INT           NULL,
    [GRADODESHI]          INT           NULL,
    [PESO]                INT           NULL,
    [TALLA]               INT           NULL,
    [ANTIBIOTICOANTES]    INT           NULL,
    [CUALANTIBIO]         VARCHAR (200) NULL,
    [TIPOHIDRATA]         INT           NULL,
    [COMPLICACIONEMB]     INT           NULL,
    [CUALCOMPLICA]        VARCHAR (200) NULL,
    [ANTIBIOHOSP]         INT           NULL,
    [CUALANTIBIO2]        VARCHAR (200) NULL,
    [DIASHOSPI]           INT           NULL,
    [HOSPURGENCIA]        INT           NULL,
    [HOSPPEDIA]           INT           NULL,
    [HOSPUCI]             INT           NULL,
    [FECHAEGRESO]         DATE          NULL,
    [MOTIVOEGRESO]        INT           NULL,
    [SALIDADIARREA]       INT           NULL,
    [DIAGNOEGRESO]        CHAR (4)      NULL,
    [FECHARECOL]          DATE          NULL,
    [FECHARECEP]          DATE          NULL,
    [FECHARESULT]         DATE          NULL,
    [IDROTAVIRUS]         BIT           NULL,
    [SEROTIPOG]           VARCHAR (200) NULL,
    [SEROTIPOP]           VARCHAR (200) NULL,
    [IDBACTERIAS]         BIT           NULL,
    [CUALESBACTE]         VARCHAR (200) NULL,
    [IDPARASITOS]         BIT           NULL,
    [CUALESPARA]          VARCHAR (200) NULL,
    [NINOGUARDE]          BIT           NULL,
    [CUALGUARDE]          VARCHAR (200) NULL,
    [DIARREAFAMI]         INT           NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA605] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA605_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA605_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA605] NOCHECK CONSTRAINT [CK_HCFICHA605_JSON];




GO
ALTER TABLE [dbo].[HCFICHA605] NOCHECK CONSTRAINT [CK_HCFICHA605_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos adicionales en formato JSON estructurado VARCHAR(MAX) validado ISJSON, extensión flexible de la notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nuevas columnas en formato JSON ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación de diarrea infantil; NULL indica primera versión inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 (CHAR 4) de la enfermedad diarreica aguda notificada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente epidemiológico: hay más personas con diarrea en la familia (1=sí, 2=no, 3=desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'DIARREAFAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hay más personas con diarrea en la familia? ((1. si, 2. no, 3. desconocido), otros datos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'DIARREAFAMI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'DIARREAFAMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o identificación específica de la guardería/centro infantil asistido por el niño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALGUARDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'asiste el niño a guarderia, ¿cual?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALGUARDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALGUARDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de riesgo: ¿asiste el niño a guardería/centro infantil? (1=sí, 2=no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'NINOGUARDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Asiste el niño a guardería? ((1. si, 2. no), OTROS DATOS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'NINOGUARDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'NINOGUARDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de laboratorio: especificación de parásitos identificados en heces o muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALESPARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de parásitos, ¿cuales? (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALESPARA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALESPARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de laboratorio: presencia de parásitos confirmada (BIT: true=positivo, false=negativo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'IDPARASITOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de parásitos ((true or false), datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'IDPARASITOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'IDPARASITOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de laboratorio microbiológico: especificación de bacterias patógenas aisladas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALESBACTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de bacterias, ¿cuales? (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALESBACTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALESBACTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de laboratorio: presencia de bacterias detectadas (BIT: true=positivo, false=negativo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'IDBACTERIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de bacterias ((1. si, 2. no), datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'IDBACTERIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'IDBACTERIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Caracterización molecular de laboratorio: serotipo P del rotavirus identificado en muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'SEROTIPOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Serotipo P (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'SEROTIPOP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'SEROTIPOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Caracterización molecular de laboratorio: serotipo G del rotavirus identificado en muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'SEROTIPOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Serotipo G (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'SEROTIPOG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'SEROTIPOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado de laboratorio: presencia de rotavirus confirmada (BIT: true=positivo, false=negativo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'IDROTAVIRUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de rotavirus ((true or false), datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'IDROTAVIRUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'IDROTAVIRUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resultado emitido por laboratorio de salud pública (LSP) en notificación de diarrea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHARESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recepción de muestra en laboratorio de salud pública (LSP)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHARECEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recepción (LSP, datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHARECEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHARECEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de recolección/toma de muestra clínica (heces) para análisis de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHARECOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de recolección (datos de laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHARECOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHARECOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código CIE-10 (CHAR 4) del diagnóstico documentado al momento de egreso hospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'DIAGNOEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Diagnóstico de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'DIAGNOEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'DIAGNOEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evolución al egreso: ¿paciente sale con síntomas de diarrea persistente? (1=sí, 2=no, 3=desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'SALIDADIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Se dió salida con cuadro de diarrea? ((1. si, 2. no, 3. desconocido), EVOLUCIÓN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'SALIDADIARREA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'SALIDADIARREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Causa del egreso hospitalario (1=mejoría clínica, 2=salida voluntaria del paciente, 3=fallecimiento)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'MOTIVOEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo de egreso ((1. mejoria, 2. salida voluntaria, 3. muerte), EVOLUCIÓN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'MOTIVOEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'MOTIVOEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de alta u egreso del paciente de la institución de atención en salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHAEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHAEGRESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHAEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de hospitalización específicamente en Unidad de Cuidados Intensivos (UCI) por diarrea aguda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'HOSPUCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hospitalizacion durante el tratamiento de la diarrea días (UCI)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'HOSPUCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'HOSPUCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de hospitalización específicamente en servicio de Pediatría por diarrea aguda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'HOSPPEDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hospitalizacion durante el tratamiento de la diarrea días (pediatria)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'HOSPPEDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'HOSPPEDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días de permanencia en servicio de Urgencias durante tratamiento de diarrea aguda', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'HOSPURGENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hospitalizacion durante el tratamiento de la diarrea días (urgencias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'HOSPURGENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'HOSPURGENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración total de hospitalización para manejo de enfermedad diarreica aguda (número de días)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'DIASHOSPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' Duración de hospitalización para el tratamiento de la diarrea (número de días)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'DIASHOSPI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'DIASHOSPI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del antibiótico suministrado durante la hospitalización del paciente con diarrea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALANTIBIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N' En caso de haber recibido antibiótico durante la hospitalizacion, ¿cuál? (evolución)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALANTIBIO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALANTIBIO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento antimicrobiano: ¿recibió antibiótico durante hospitalización? (1=sí, 2=no, 3=desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ANTIBIOHOSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Recibió antibiótico durante la hospitalización? ((1. Si, 2. No, 3. Desconocido), EVOLUCIÓN)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ANTIBIOHOSP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ANTIBIOHOSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especificación detallada de la complicación presentada (deshidratación grave, IRA, sepsis, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALCOMPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En caso de haber presentado complicación, ¿cuál? (evolución)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALCOMPLICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALCOMPLICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evolución: ¿presentó complicaciones durante hospitalización? (1=sí, 2=no, 3=desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'COMPLICACIONEMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Presentó alguna complicación durante la hospitalización? ((1. si, 2. no, 3. desconocido), evolución)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'COMPLICACIONEMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'COMPLICACIONEMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de terapia de rehidratación utilizada (1=vía oral/SRO, 2=intravenosa parenteral)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'TIPOHIDRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Qué tipo de hidratación recibió? ((1. hidratación via oral, 2. hidratación intravenosa), tratamiento)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'TIPOHIDRATA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'TIPOHIDRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del antibiótico administrado antes del ingreso al hospital según relato del acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALANTIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En caso de haber recibido antibiótico antes del ingreso, ¿Cuál? (tratamiento)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALANTIBIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALANTIBIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamento: ¿recibió antibiótico previo al ingreso hospitalario? (1=sí, 2=no, 3=desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ANTIBIOTICOANTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Recibió antibiótico antes de ingresar al hospital? ((1. si, 2. no, 3. desconocido), tratamiento)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ANTIBIOTICOANTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ANTIBIOTICOANTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medida antropométrica: altura/longitud del niño en centímetros al momento de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'TALLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Talla (DATOS CLINICOS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'TALLA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'TALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medida antropométrica: peso corporal del niño en gramos o kilogramos al ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'PESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso (datos clinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'PESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'PESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evaluación clínica: grado de deshidratación (1=leve-moderada, 2=grave no especificado, 3=grave, 4=desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'GRADODESHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grado de deshidratación ((1. leve moderada, 2. grave desconocido, 3. grave, 4. desconocidos), datos clinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'GRADODESHI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'GRADODESHI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado clínico general del paciente al momento de admisión (letargo, alerta, irritabilidad, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ESTADOINGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ESTADOINGRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ESTADOINGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción cualitativa adicional de características de las heces (color, olor, moco, sangre, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALHECES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Características de las heces, ¿cual?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALHECES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'CUALHECES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Datos clínicos: apariencia de las deposiciones (1=líquidas acuosas, 2=semilíquidas, 3=sanguinolientas, 4=otra)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'HECES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Características de las heces ((1. Liquidas, 2. Semiliquidas, 3. Sanguinolientas, 4. Otra), DATOS CLÍNICOS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'HECES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'HECES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de resolución/remisión de síntomas diarreicos según relato de acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECTERMDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de terminación de la diarrea (datos clinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECTERMDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECTERMDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de deposiciones en primeras 24 horas desde inicio de cuadro diarreico agudo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'NUMDEPOSIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de deposiciones en las primeras 24 horas iniciado el cuadro (datos clinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'NUMDEPOSIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'NUMDEPOSIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio de síntomas de diarrea aguda reportada por familiar o cuidador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHAINIDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de inicio de las diarrea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHAINIDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHAINIDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de episodios de vómito en primeras 24 horas desde presentación del cuadro clínico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'NUMVOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número de vomito en las primeras 24 horas iniciado el cuadro (datos clinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'NUMVOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'NUMVOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Síntoma gastrointestinal: presencia de vómito/emesis (1=sí, 2=no, 3=desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vómito ((1. si, 2. no, 3. desconocido), DATOS CLÍNICOS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'VOMITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'VOMITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Signo vital: presencia de elevación de temperatura (1=sí, 2=no, 3=desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FIEBRE ((1. si, 2. no, 3. desconocido), datos clinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FIEBRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FIEBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente nutricional actual: tipo de alimentación del niño (1=materna exclusiva, 2=artificial, 3=mixta, 4=variada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ALIMENACTUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se guarda la alimentación actual ((1. Materna exclusivamente,  2. Artificial, 3. Mixta, 4. Alimentación variada), ANTECEDENTES VACUNALES Y LACTANCIA MATERNA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ALIMENACTUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ALIMENACTUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración en meses/semanas de lactancia materna exclusiva recibida por el niño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'TIEMPOLECHE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Recibió leche materna exclusivamente, ¿cuanto tiempo? (ANTECEDENTES VACUNALES Y LACTANCIA MATERNA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'TIEMPOLECHE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'TIEMPOLECHE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Antecedente: ¿recibió leche materna exclusivamente en primeros meses? (1=sí, 2=no, 3=desconocido)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'LECHEMATERNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Recibió leche materna exclusivamente? ((1. si, 2. no, 3. desconocido), ANTECEDENTES VACUNALES Y LACTANCIA MATERNA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'LECHEMATERNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'LECHEMATERNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso al nacimiento registrado en gramos (antecedente perinatal de bajo peso)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'PESONACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso al nacer (ANTECEDENTES VACUNALES Y LACTANCIA MATERNA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'PESONACER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'PESONACER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento: ¿dispone de carnet de vacunación del niño? (1=sí, 2=no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'TIENECARNET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'¿Tiene carnet? (1. si, 2. no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'TIENECARNET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'TIENECARNET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de aplicación de segunda dosis de vacuna rotavirus según carnet/registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHAPLIDOSIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de aplicación segunda dosis (ANTECEDENTES VACUNALES Y LACTANCIA MATERNA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHAPLIDOSIS2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHAPLIDOSIS2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de aplicación de primera dosis de vacuna rotavirus según carnet/registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHAPLIDOSIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de aplicación primera dosis (ANTECEDENTES VACUNALES Y LACTANCIA MATERNA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHAPLIDOSIS1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'FECHAPLIDOSIS1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vacunación: ¿recibió vacuna antirotavirus? (1=sí, 2=no, 3=desconocido, antecedente vacunal)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'VACUNAROTAVIRUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vacuna contra rotavirus ((1. si, 2. no, 3. desconocido), ANTECEDENTES VACUNALES Y LACTANCIA MATERNA)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'VACUNAROTAVIRUS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'VACUNAROTAVIRUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) a tabla HCFICHANOTIFICACION: relación 1:1 con ficha de notificación epidemiológica padre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único consecutivo (IDENTITY PK) del registro de ficha 605 de diarrea infantil', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha de notificación epidemiológica 605 para casos de Enfermedad Diarreica Aguda (EDA) / rotavirus en niños. Registra datos clínicos, de laboratorio, hospitalización, vacunación y seguimiento del episodio diarreico notificado al sistema de vigilancia en salud pública.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA605';
