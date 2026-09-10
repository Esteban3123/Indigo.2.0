CREATE TABLE [dbo].[INTERLABD] (
    [AUTO]            INT                                                                             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCONCEC]       INT                                                                             NOT NULL,
    [NUMMUESTRA]      INT                                                                             NOT NULL,
    [ANALITO]         VARCHAR (150)                                                                   NOT NULL,
    [VALOR]           VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Value_Ofuscado", 0)')        NOT NULL,
    [UNIDAD]          VARCHAR (30)                                                                    NOT NULL,
    [OBSERVACION]     VARCHAR (7000) MASKED WITH (FUNCTION = 'partial(0, "Observation_Ofuscado", 0)') NULL,
    [DESCRIPCION]     VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Description_Ofuscado", 0)')  NULL,
    [VALORMINIMO]     VARCHAR (15)                                                                    NULL,
    [VALORMAXIMO]     VARCHAR (15)                                                                    NULL,
    [AUTOLABOR]       INT                                                                             NULL,
    [CLASIFICACION]   VARCHAR (60)                                                                    NULL,
    [MICROBIOLOGIA]   BIT                                                                             NULL,
    [TIPORESULTADO]   BIT                                                                             NULL,
    [IDINTERCTRL]     INT                                                                             NULL,
    [CriticalResult]  BIT                                                                             CONSTRAINT [DF_INTERLABD_CriticalResult] DEFAULT ((0)) NULL,
    [ValuesOutLimits] BIT                                                                             CONSTRAINT [DF_INTERLABD_ValuesOutLimits] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_INTERLABD] PRIMARY KEY CLUSTERED ([AUTO] ASC, [CODCONCEC] ASC)
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INTERLABD].[VALOR]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INTERLABD].[OBSERVACION]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INTERLABD].[DESCRIPCION]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
CREATE NONCLUSTERED INDEX [Index_INTERLABD_AUTOLABOR]
    ON [dbo].[INTERLABD]([AUTOLABOR] ASC)
    INCLUDE([ANALITO], [CODCONCEC], [NUMMUESTRA], [OBSERVACION], [UNIDAD], [VALOR]);


GO
CREATE NONCLUSTERED INDEX [IX_INTERLABD_CODCONCEC]
    ON [dbo].[INTERLABD]([CODCONCEC] ASC)
    INCLUDE([NUMMUESTRA], [ANALITO], [VALOR], [UNIDAD], [OBSERVACION], [DESCRIPCION], [VALORMINIMO], [VALORMAXIMO], [AUTOLABOR], [CLASIFICACION], [IDINTERCTRL], [MICROBIOLOGIA], [TIPORESULTADO]);


GO
CREATE NONCLUSTERED INDEX [IX_INTERLABD_CODCONCEC_CriticalResult]
    ON [dbo].[INTERLABD]([CODCONCEC] ASC)
    INCLUDE([CriticalResult]);


GO
ALTER INDEX [IX_INTERLABD_CODCONCEC_CriticalResult]
    ON [dbo].[INTERLABD] DISABLE;




GO
CREATE NONCLUSTERED INDEX [UX_INTERLABD_CODCONCEC]
    ON [dbo].[INTERLABD]([CODCONCEC] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_INTERLABD_ANALITO_AUTOLABOR]
    ON [dbo].[INTERLABD]([ANALITO] ASC, [AUTOLABOR] ASC)
    INCLUDE([NUMMUESTRA], [UNIDAD], [VALOR], [VALORMAXIMO], [VALORMINIMO]);

GO

CREATE NONCLUSTERED INDEX IX_INTERLABD_Autolabor_Critico
ON dbo.INTERLABD
(
    AUTOLABOR,
    CODCONCEC
)
WHERE CriticalResult = 1;



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de valores fuera de rango: detecta si el analito está por debajo del mínimo o encima del máximo referencial. Puede provenir de interfaz de laboratorio o calcularse en Indigo. Solo aplica para resultados numéricos con rangos definidos. Creado 08-09-2022.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'ValuesOutLimits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valores fuera de los límites    Columna que me identificara si el analito esta con valor por debajo del rango mínimo o por encima del rango máximo.    Nota 1: Este valor lo puede enviar la interfaz, pero tambien lo calculamos en Indigo en caso que la interfaz no lo envien, entonces tenemos las dos oportunidades para diligenciar dicha columna.    Nota 2: Solo podrá identificarse para los resultados de tipo numérico en el cual cuente con los rango mínimo y máximo (IMPORTANTE)    La columna es creada el 08-09-2022  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'ValuesOutLimits';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'ValuesOutLimits';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) de resultado crítico recibido desde interfaz de laboratorio. Registra true/false según criticidad del hallazgo en examen. Enviado siempre por la interfaz. Creado 08-09-2022.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'CriticalResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna de "Resultado crítico" que permitirá recibir el valor registrado con este atributo desde la Interfaz de laboratorio, registrara  true ó false, este valor siempre lo enviara la interfaz de laboratorio.    Nota: Esta columna fue creada el 08-09-2022   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'CriticalResult';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'CriticalResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con tabla INTERCTRL (control de resultados preliminares de laboratorio). FK para seguimiento de estados intermedios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'IDINTERCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla INTERCTRL (aplica para los preliminares)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'IDINTERCTRL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'IDINTERCTRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de resultado: 0=preliminar (parcial), 1=final (confirmado). Indica estado de validación del resultado enviado en XML desde laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor  "0": Indica que el resultado enviado en el XML es preliminar  Valor "1": Indica que el resultado enviado en el XML es final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificador de tipo de estudio: 0=no microbiología (sin preliminares), 1=microbiología (con resultados preliminares). Define flujo de validación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'''''Valor "0": Indica que el estudio no es de microbiología, es decir que no maneja resultados preliminares     ''''Valor "1": Indica que el estudio es de microbiología, es decir que maneja resultados preliminares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación categórica del resultado del examen de laboratorio. Agrupa analitos por tipo o categoría diagnóstica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Clasificacion del Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'CLASIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico (PK) de la orden médica de laboratorio. Identifica unívocamente el pedido de examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de ordenes medicas de laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'AUTOLABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite superior referencial del rango normal para el analito. VARCHAR(15), criterio para evaluar valores anormales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'VALORMAXIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Maximo Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'VALORMAXIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'VALORMAXIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Límite inferior referencial del rango normal para el analito. VARCHAR(15), criterio para evaluar valores anormales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'VALORMINIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Minimo Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'VALORMINIMO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'VALORMINIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción referencial del resultado del analito. VARCHAR(MAX) ofuscado. Detalla interpretación clínica o notas técnicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion Resultado Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación del analito en el resultado. VARCHAR(7000) ofuscado. Comentarios clínicos, advertencias o aclaraciones adicionales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'OIbservacion Analito Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'OBSERVACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida del valor del analito (ej: mg/dL, mmol/L, células/μL). Especifica escala de referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'UNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Unidad Analito Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'UNIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'UNIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico o textual del resultado del analito. VARCHAR(MAX) ofuscado (PII). Resultado cuantitativo o cualitativo del examen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor Analito Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'VALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del analito, parámetro o sustancia medida en el examen de laboratorio (ej: glucosa, hemoglobina, colesterol). VARCHAR(150).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'ANALITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Analito del Resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'ANALITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'ANALITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número identificador de la muestra biológica asociada al análisis. Trazabilidad muestra-resultado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'NUMMUESTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo de la cabecera de resultado. FK a tabla controladora de interfaz de laboratorio. Agrupa analitos por orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo de la Cabecera del Resultado de la Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico (IDENTITY) único de la fila. PK del detalle de resultado. Identificador secuencial del registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'AutoNumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
CREATE NONCLUSTERED INDEX [IX_INTERLABD_CODCONCEC_Critical_DESC]
    ON [dbo].[INTERLABD]([CODCONCEC] ASC, [CriticalResult] DESC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de resultados de laboratorio por analito (prueba individual) dentro de una muestra. Registra los valores obtenidos, rangos de referencia, unidades, observaciones y alertas de resultados críticos o fuera de límites para cada examen de laboratorio procesado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD';
