CREATE TABLE [MixingStation].[CMMixingProducitonLine] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Id_CMConfiguration] INT NOT NULL,
    [Id_ProductionLine]  INT NOT NULL,
    [StatePl]            BIT NULL,
    CONSTRAINT [PK_CMMixingProducitonLine] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMMixingProducitonLine_CMConfiguration] FOREIGN KEY ([Id_CMConfiguration]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_CMMixingProducitonLine_ProductionLine] FOREIGN KEY ([Id_ProductionLine]) REFERENCES [MixingStation].[ProductionLine] ([Id])
);


GO
ALTER TABLE [MixingStation].[CMMixingProducitonLine] NOCHECK CONSTRAINT [FK_CMMixingProducitonLine_CMConfiguration];


GO
ALTER TABLE [MixingStation].[CMMixingProducitonLine] NOCHECK CONSTRAINT [FK_CMMixingProducitonLine_ProductionLine];

