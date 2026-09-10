CREATE TABLE [dbo].[PRHCEXPRES] (
    [ID]         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC] INT           NOT NULL,
    [EXPRESION]  VARCHAR (MAX) NOT NULL,
    [NOMRIESGO]  VARCHAR (500) NOT NULL,
    [DESRIESGO]  VARCHAR (MAX) NOT NULL,
    [PLANRIESGO] VARCHAR (MAX) NOT NULL,
    [FECHREGIS]  DATETIME      NOT NULL,
    [AGRPAQUETE] BIT           NULL,
    CONSTRAINT [PK_PRHCEXPRES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PRHCEXPRES_MODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID])
);


GO
ALTER TABLE [dbo].[PRHCEXPRES] NOCHECK CONSTRAINT [FK_PRHCEXPRES_MODELOHC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Agrupación en paquete (BIT, nullable). Indicador booleano (SI=1/NO=0) que señala si el riesgo se agrupa dentro de un paquete de riesgos relacionados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'AGRPAQUETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'True = SI  False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'AGRPAQUETE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'AGRPAQUETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro (DATETIME). Marca temporal de cuándo se creó o registró la regla de expresión de riesgo en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda le fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'FECHREGIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'FECHREGIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plan de manejo/intervención del riesgo (VARCHAR MAX). Protocolo, recomendaciones o acciones clínicas a seguir cuando se detecta/confirma el riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'PLANRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el plan de riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'PLANRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'PLANRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada del riesgo (VARCHAR MAX). Explicación clínica, impacto y características del riesgo identificado en la atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'DESRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la descripción del riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'DESRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'DESRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del riesgo identificado (VARCHAR 500). Etiqueta o denominación del riesgo clínico (ej: ''''Hipertensión severa'''', ''''Riesgo de caída'''') evaluado por la expresión.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'NOMRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el nombre del riesgo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'NOMRIESGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'NOMRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresión lógica/técnica a validar (VARCHAR MAX). Fórmula, condición o regla que evalúa factores de riesgo en el paciente/atención; usada en alertas clínicas y validaciones de HC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la Expresion a validar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'EXPRESION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'EXPRESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del modelo de historia clínica (INT, FK→PRMODELOHC.ID). Referencia a la plantilla o estructura de HC a la que pertenece la regla de riesgo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id modelo de historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK). Consecutivo secuencial de la tabla PRHCEXPRES para registros de expresiones de riesgo en historias clínicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Expresiones de riesgo clínico asociadas a modelos de historia clínica. Registra las reglas o condiciones que identifican un riesgo en salud del paciente, junto con su descripción, nombre del riesgo y el plan de manejo recomendado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PRHCEXPRES';
