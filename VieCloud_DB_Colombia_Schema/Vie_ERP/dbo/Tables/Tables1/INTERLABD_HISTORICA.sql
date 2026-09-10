CREATE TABLE [dbo].[INTERLABD_HISTORICA] (
    [AUTO] INT NOT NULL,
    [CODCONCEC] INT NOT NULL,
    [NUMMUESTRA] INT NOT NULL,
    [ANALITO] VARCHAR (150) NOT NULL,
    [VALOR] VARCHAR (MAX) NOT NULL,
    [UNIDAD] VARCHAR (30) NOT NULL,
    [OBSERVACION] VARCHAR (7000) NULL,
    [DESCRIPCION] VARCHAR (MAX) NULL,
    [VALORMINIMO] VARCHAR (15) NULL,
    [VALORMAXIMO] VARCHAR (15) NULL,
    [AUTOLABOR] INT NULL,
    [CLASIFICACION] VARCHAR (60) NULL,
    [IDINTERCTRL] INT NULL,
    [MICROBIOLOGIA] BIT NULL,
    [TIPORESULTADO] BIT NULL,
    [CriticalResult] BIT NULL,
    [ValuesOutLimits] BIT NULL
);
GO
CREATE NONCLUSTERED INDEX [IX_INTERLABD_HISTORICA] ON [dbo].[INTERLABD_HISTORICA] ([CODCONCEC] ASC, [AUTOLABOR] ASC) INCLUDE ([CLASIFICACION]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Histórico de resultados de laboratorio clínico interfaceados desde equipos o sistemas externos. Guarda cada analito (prueba) con su valor, unidades, rangos de referencia y alertas de criticidad para los exámenes de laboratorio de los pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de resultado de laboratorio (clave autogenerada).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la conexión o concepto del examen de laboratorio al que pertenece este resultado; vincula el resultado con la orden o solicitud de examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de muestra del laboratorio; identifica la muestra biológica analizada (tubo, frasco, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del analito o prueba específica realizada, por ejemplo: glucosa, hemoglobina, creatinina, leucocitos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'ANALITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'ANALITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado o valor obtenido para el analito; puede ser numérico o descriptivo (positivo/negativo, texto de interpretación).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida del resultado del analito, por ejemplo: mg/dL, g/dL, UI/L, %.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'UNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'UNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o comentarios adicionales del laboratorio sobre el resultado, aclaraciones técnicas o notas del analista.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción extendida del resultado o del analito; puede incluir interpretación narrativa o detalles adicionales del informe.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor mínimo del rango de referencia normal para el analito (límite inferior de referencia).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'VALORMINIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'VALORMINIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor máximo del rango de referencia normal para el analito (límite superior de referencia).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'VALORMAXIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'VALORMAXIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del laboratorio o equipo analizador que generó el resultado; permite trazabilidad del instrumento de medición.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación o agrupación del analito dentro del perfil de examen, por ejemplo: hematología, química sanguínea, uroanálisis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro de control de interfaz; vincula el resultado con el proceso de integración o transmisión desde el equipo de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'IDINTERCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'IDINTERCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de si el resultado corresponde a un examen de microbiología (cultivos, antibiogramas, gram). Valor 1 = sí es microbiología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de resultado: indica si el valor es numérico o cualitativo/textual. Permite distinguir resultados cuantitativos de cualitativos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de resultado crítico o de pánico: valor fuera de los límites críticos que requiere notificación urgente al médico tratante. Valor 1 = resultado crítico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'CriticalResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'CriticalResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de que el valor está fuera de los rangos de referencia normales (alto o bajo). Valor 1 = resultado anormal fuera de límites.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'ValuesOutLimits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD_HISTORICA', @level2type = N'COLUMN', @level2name = N'ValuesOutLimits';
