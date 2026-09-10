CREATE TABLE [Admissions].[TriageParametersEntity] (
    [id]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EntityCode]  CHAR (9)      NOT NULL,
    [State]       BIT           NOT NULL,
    [Observation] VARCHAR (300) NOT NULL,
    [AppliedIV]   BIT           NOT NULL,
    [AppliedV]    BIT           NOT NULL,
    CONSTRAINT [PK_TriageParametersEntity] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que señala si la parametrización aplica a Triage V (nivel de urgencia/prioridad 5, menor complejidad). Búsqueda: triage nivel 5, urgencia baja, parametrización triage V.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'AppliedV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametrizacion aplica a Triage V', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'AppliedV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'AppliedV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que señala si la parametrización aplica a Triage IV (nivel de urgencia/prioridad 4, complejidad moderada). Búsqueda: triage nivel 4, urgencia moderada, parametrización triage IV.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'AppliedIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Parametrizacion aplica a Triage IV', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'AppliedIV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'AppliedIV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo de texto (VARCHAR 300) con observaciones, notas o comentarios sobre la parametrización del triage en la entidad. Búsqueda: notas, comentarios, detalles, observaciones triage.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'Observation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) del estado activo/inactivo de la parametrización de triage. Búsqueda: activo, habilitado, vigente, estado parametrización.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico (CHAR 9) de la entidad/centro de atención parametrizado. FK a [INENTIDAD]. Búsqueda: código entidad, centro de salud, unidad funcional, institución, identificador entidad.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la entidad parametrizada', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'EntityCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'EntityCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity', @level2type = N'COLUMN', @level2name = N'id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de triage por entidad: define qué categorías o niveles de clasificación de urgencia (triage) aplican para cada entidad, incluyendo si se usan las escalas triage IV y V, el estado de vigencia y observaciones.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'TriageParametersEntity';
