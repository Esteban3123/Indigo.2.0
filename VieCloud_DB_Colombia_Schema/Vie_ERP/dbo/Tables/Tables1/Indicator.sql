CREATE TABLE [dbo].[Indicator] (
    [Id]                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                             VARCHAR (20)    NOT NULL,
    [Name]                             VARCHAR (200)   NOT NULL,
    [InstitutionalDocumentId]          INT             NOT NULL,
    [StandardAccreditation]            VARCHAR (20)    NOT NULL,
    [ObjectiveMeasureUnitId]           INT             NOT NULL,
    [ObjectiveCondition]               TINYINT         NOT NULL,
    [ObjectiveValue]                   NUMERIC (18, 2) NOT NULL,
    [ObjectiveValueTwo]                NUMERIC (18, 2) NULL,
    [Periodicity]                      TINYINT         NOT NULL,
    [GeneratingDataResponsibleId]      INT             NOT NULL,
    [CalculationAnalysisResponsibleId] INT             NOT NULL,
    [DecisionMakingResponsibleId]      INT             NOT NULL,
    [CommitteeId]                      INT             NOT NULL,
    [IndicatorFormula]                 VARCHAR (MAX)   NOT NULL,
    CONSTRAINT [PK_Indicator__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Indicator_InstitutionalDocument] FOREIGN KEY ([InstitutionalDocumentId]) REFERENCES [dbo].[InstitutionalDocument] ([Id])
);




GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_Indicator__Code]
    ON [dbo].[Indicator]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del comité donde se socializa y revisa (FK a Committee). Grupo de gobernanza encargado de recibir, discutir y tomar acciones sobre desempeño del indicador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'CommitteeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del comite donde se socializa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'CommitteeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'CommitteeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del responsable de toma de decisiones (FK a persona/rol). Directivo o gerente autorizado para actuar sobre resultados del indicador e implementar mejoras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'DecisionMakingResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del responsable de la toma de decisiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'DecisionMakingResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'DecisionMakingResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del responsable de cálculo y análisis (FK a persona/rol). Profesional responsable de procesar, calcular y analizar los datos para obtener el resultado del indicador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'CalculationAnalysisResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del responsable del calculo y analisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'CalculationAnalysisResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'CalculationAnalysisResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del responsable de generación de datos (FK a persona/rol). Usuario o departamento encargado de recolectar, capturar y validar datos brutos del indicador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'GeneratingDataResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del responsable de generar los datos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'GeneratingDataResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'GeneratingDataResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de cálculo del indicador (TINYINT: 1=Diario, 2=Semanal, 3=Quincenal, 4=Mensual, 5=Bimensual, 6=Trimensual, 7=Semestral, 8=Anual). Define cuán frecuentemente se evalúa y reporta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Periodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodicidad del indicador  1 - Diario  2 - Semanal  3 - Quincenal  4 - Mensual  5 - Bimensual  6 - Trimensual  7 - Semestral   8 - Anual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Periodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Periodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo valor de meta para rango ENTRE (NUMERIC 18,2, nullable). Límite superior cuando ObjectiveCondition=5 (ENTRE); ejemplo: meta entre 2% y 5%.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveValueTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de la meta, este solo se llena si el tipo de condicion es  ENTRE, Ejemplo Entre el 2% y el 5%', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveValueTwo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveValueTwo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico de la meta (NUMERIC 18,2). Umbral o cifra objetivo que debe alcanzarse; para ENTRE, primer límite inferior.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de la meta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Operador de condición de la meta (TINYINT: 1=<, 2=<=, 3=>, 4=>=, 5=ENTRE). Especifica el tipo de comparación para validar si el resultado cumple la meta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveCondition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la condicion para la meta   1 - <  2 - <=  3 - >  4 - >=  5 - Entre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveCondition';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveCondition';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de unidad de medida del objetivo (FK). Referencia a la unidad cuantitativa del objetivo: porcentaje (%), número absoluto, tasa, índice, días, horas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveMeasureUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de unidad de medida objetiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveMeasureUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'ObjectiveMeasureUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estándar de acreditación o cumplimiento (VARCHAR 20). Normativa, estándar de calidad o requisito de acreditación (JCI, ISO, INVIMA, MINSALUD) que cubre este indicador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'StandardAccreditation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estandar de acreditacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'StandardAccreditation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'StandardAccreditation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del documento institucional asociado (FK a InstitutionalDocument). Referencia a la política, protocolo, plan o directiva que respalda el indicador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'InstitutionalDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del documento institucional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'InstitutionalDocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'InstitutionalDocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo del indicador (VARCHAR 200). Denominación completa del indicador de desempeño, gestión, calidad o acreditación usado en reportes institucionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del indicador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico del indicador (VARCHAR 20). Identificador corto único para búsqueda y referencia rápida del indicador en reportes y auditorías.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del indicador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del indicador (INT, PK). Clave primaria que identifica de forma exclusiva cada registro de indicador de gestión o desempeño.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del indicador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicadores de calidad y gestión institucional. Registra cada indicador con su nombre, código, fórmula de cálculo, meta (valor objetivo y condición), periodicidad de medición, estándar de acreditación asociado y los responsables de generación de datos, análisis y toma de decisiones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fórmula matemática o expresión de cálculo del indicador, que define cómo se obtiene el resultado a partir de los datos fuente (numerador, denominador u otras variables).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'IndicatorFormula';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Indicator', @level2type = N'COLUMN', @level2name = N'IndicatorFormula';
