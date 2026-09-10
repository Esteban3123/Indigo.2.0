CREATE TABLE [dbo].[INTERLAID] (
    [AUTO]          INT                                                                             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]     INT                                                                             NOT NULL,
    [NUMMUESTRA]    INT                                                                             NOT NULL,
    [ANALITO]       VARCHAR (70)                                                                    NOT NULL,
    [VALOR]         VARCHAR (16) MASKED WITH (FUNCTION = 'partial(0, "Value_Ofuscado", 0)')         NOT NULL,
    [UNIDAD]        VARCHAR (30)                                                                    NOT NULL,
    [OBSERVACION]   VARCHAR (7000) MASKED WITH (FUNCTION = 'partial(0, "Observation_Ofuscado", 0)') NULL,
    [DESCRIPCION]   VARCHAR (10) MASKED WITH (FUNCTION = 'partial(0, "Description_Ofuscado", 0)')   NULL,
    [VALORMINIMO]   VARCHAR (15)                                                                    NULL,
    [VALORMAXIMO]   VARCHAR (15)                                                                    NULL,
    [AUTOLABOR]     INT                                                                             NULL,
    [CLASIFICACION] VARCHAR (60)                                                                    NULL,
    CONSTRAINT [PK_INTERLAID] PRIMARY KEY CLUSTERED ([AUTO] ASC, [CODCONCEC] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INTERLAID].[VALOR]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INTERLAID].[OBSERVACION]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INTERLAID].[DESCRIPCION]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del resultado de laboratorio; categoría o tipo de análisis (ej: química, hematología, inmunología, uroanálisis)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificacion del Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) de la orden médica de laboratorio; FK a tabla de órdenes de examen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de ordenes medicas de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo de referencia normal o rango superior para el analito; límite superior de normalidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'VALORMAXIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Maximo Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'VALORMAXIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'VALORMAXIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor mínimo de referencia normal o rango inferior para el analito; límite inferior de normalidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'VALORMINIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Minimo Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'VALORMINIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'VALORMINIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o interpretación del resultado de referencia (normal, anormal, crítico); PII_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Resultado Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o nota clínica adicional asociada al analito y su resultado; hallazgos relevantes; PII_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'OIbservacion Analito Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida del analito (mg/dL, mmol/L, UI/L, células/mm³, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'UNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Unidad Analito Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'UNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'UNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico o resultado del analito obtenido en la prueba de laboratorio; PII_Ofuscado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Analito Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del analito o parámetro medido en el examen (glucosa, hemoglobina, colesterol, creatinina, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'ANALITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Analito del Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'ANALITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'ANALITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número identificador único de la muestra biológica en el laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo de la cabecera (encabezado) del resultado de laboratorio; referencia al lote/interfaz de resultados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo de la Cabecera del Resultado de la Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico de la fila en tabla INTERLAID; clave primaria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'AutoNumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultados de analitos de laboratorio clínico asociados a una muestra. Contiene los valores obtenidos por cada análisis (examen de laboratorio), junto con sus rangos de referencia, unidades de medida y observaciones del resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLAID';
