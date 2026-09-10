CREATE TABLE [MixingStation].[CMCenterLineUnit] (
    [Id]                  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MixingStationId]     INT       NOT NULL,
    [CodeCenterAttention] CHAR (10) NOT NULL,
    [ProductionLineId]    INT       NOT NULL,
    [CodeFunctionalUnit]  CHAR (10) NOT NULL,
    [EveryTimeDeliverId]  TINYINT   NOT NULL,
    [FirstDeliveryTime]   DATETIME  NOT NULL,
    [SecondDeliveryTime]  DATETIME  NULL,
    [ThirdDeliveryTime]   DATETIME  NULL,
    [FourthDeliveryTime]  DATETIME  NULL,
    [StatusCLU]           BIT       CONSTRAINT [DF_CMCenterLineUnit_StatusCLU] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK__CMCenterLineUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMCenterLineUnit_MixingStationId] FOREIGN KEY ([MixingStationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_CMCenterLineUnit_ProductionLineId] FOREIGN KEY ([ProductionLineId]) REFERENCES [MixingStation].[ProductionLine] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_CMCenterLineUnit_MixingStationId]
    ON [MixingStation].[CMCenterLineUnit]([MixingStationId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de estado activo/inactivo del registro de asociación centro-línea (BIT: 1=activo, 0=inactivo)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'StatusCLU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el registro esta o no activo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'StatusCLU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'StatusCLU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de fecha y hora de la cuarta entrega programada de insumos o medicamentos en la unidad funcional (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'FourthDeliveryTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de cuarta entrega', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'FourthDeliveryTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'FourthDeliveryTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de fecha y hora de la tercera entrega programada de insumos o medicamentos en la unidad funcional (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'ThirdDeliveryTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de tercera entrega', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'ThirdDeliveryTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'ThirdDeliveryTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de fecha y hora de la segunda entrega programada de insumos o medicamentos en la unidad funcional (DATETIME, nullable)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'SecondDeliveryTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de segunda entrega', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'SecondDeliveryTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'SecondDeliveryTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de fecha y hora de la primera entrega programada de insumos o medicamentos en la unidad funcional (DATETIME)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'FirstDeliveryTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de primera entrega', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'FirstDeliveryTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'FirstDeliveryTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de entrega en horas: 1=cada 6 horas, 2=cada 8 horas, 3=cada 12 horas, 4=cada 24 horas (TINYINT)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'EveryTimeDeliverId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cada cuantas horas se hace la entrega: 1:6 horas, 2:8 horas, 3:12 horas, 4:24 horas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'EveryTimeDeliverId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'EveryTimeDeliverId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de unidad funcional/servicio en el HIS (ej: urgencia, farmacia, quirófano) - CHAR(10)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'CodeFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de unidad funcional en el HIS', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'CodeFunctionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'CodeFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de línea de producción o de preparación vinculada (FK a ProductionLine)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'ProductionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de línea de producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'ProductionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'ProductionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención o institución de salud en el HIS (ej: hospital, clínica, centro médico) - CHAR(10)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'CodeCenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de centro de atención en el HIS', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'CodeCenterAttention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'CodeCenterAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la estación de mezcla o preparación central (FK a CMConfiguration)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'MixingStationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la estación de mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'MixingStationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'MixingStationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria autoincremental de la asociación centro-línea-unidad funcional (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de la línea central de despacho por estación de mezcla: define los horarios de entrega (hasta cuatro turnos) de medicamentos preparados hacia cada unidad funcional o centro de atención, indicando con qué frecuencia se realizan los envíos y si el vínculo está activo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit';
