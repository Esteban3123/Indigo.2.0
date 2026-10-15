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
ALTER TABLE [MixingStation].[CMCenterLineUnit] NOCHECK CONSTRAINT [FK_CMCenterLineUnit_MixingStationId];


GO
ALTER TABLE [MixingStation].[CMCenterLineUnit] NOCHECK CONSTRAINT [FK_CMCenterLineUnit_ProductionLineId];


GO
CREATE NONCLUSTERED INDEX [IX_CMCenterLineUnit_MixingStationId]
    ON [MixingStation].[CMCenterLineUnit]([MixingStationId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el registro esta o no activo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'StatusCLU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de cuarta entrega', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'FourthDeliveryTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de tercera entrega', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'ThirdDeliveryTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de segunda entrega', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'SecondDeliveryTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora de primera entrega', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'FirstDeliveryTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cada cuantas horas se hace la entrega: 1:6 horas, 2:8 horas, 3:12 horas, 4:24 horas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'EveryTimeDeliverId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de unidad funcional en el HIS', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'CodeFunctionalUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de línea de producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'ProductionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de centro de atención en el HIS', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'CodeCenterAttention';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la estación de mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'MixingStationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMCenterLineUnit', @level2type = N'COLUMN', @level2name = N'Id';

