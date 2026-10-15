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
CREATE NONCLUSTERED INDEX [IX_INTERLABD_CODCONCEC]
    ON [dbo].[INTERLABD]([CODCONCEC] ASC)
    INCLUDE([NUMMUESTRA], [ANALITO], [VALOR], [UNIDAD], [OBSERVACION], [DESCRIPCION], [VALORMINIMO], [VALORMAXIMO], [AUTOLABOR], [CLASIFICACION], [IDINTERCTRL], [MICROBIOLOGIA], [TIPORESULTADO]);


GO
CREATE NONCLUSTERED INDEX [IX_INTERLABD_CODCONCEC_CriticalResult]
    ON [dbo].[INTERLABD]([CODCONCEC] ASC)
    INCLUDE([CriticalResult]);


GO
CREATE NONCLUSTERED INDEX [UX_INTERLABD_CODCONCEC]
    ON [dbo].[INTERLABD]([CODCONCEC] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_INTERLABD_ANALITO_AUTOLABOR]
    ON [dbo].[INTERLABD]([ANALITO] ASC, [AUTOLABOR] ASC)
    INCLUDE([NUMMUESTRA], [UNIDAD], [VALOR], [VALORMAXIMO], [VALORMINIMO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valores fuera de los límites    Columna que me identificara si el analito esta con valor por debajo del rango mínimo o por encima del rango máximo.    Nota 1: Este valor lo puede enviar la interfaz, pero tambien lo calculamos en Indigo en caso que la interfaz no lo envien, entonces tenemos las dos oportunidades para diligenciar dicha columna.    Nota 2: Solo podrá identificarse para los resultados de tipo numérico en el cual cuente con los rango mínimo y máximo (IMPORTANTE)    La columna es creada el 08-09-2022  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'ValuesOutLimits';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Columna de "Resultado crítico" que permitirá recibir el valor registrado con este atributo desde la Interfaz de laboratorio, registrara  true ó false, este valor siempre lo enviara la interfaz de laboratorio.    Nota: Esta columna fue creada el 08-09-2022   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'CriticalResult';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor  "0": Indica que el resultado enviado en el XML es preliminar  Valor "1": Indica que el resultado enviado en el XML es final', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'TIPORESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '''Valor "0": Indica que el estudio no es de microbiología, es decir que no maneja resultados preliminares     ''Valor "1": Indica que el estudio es de microbiología, es decir que maneja resultados preliminares', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INTERLABD', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';

