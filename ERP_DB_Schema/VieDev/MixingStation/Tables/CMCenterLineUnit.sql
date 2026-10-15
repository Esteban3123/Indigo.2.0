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

