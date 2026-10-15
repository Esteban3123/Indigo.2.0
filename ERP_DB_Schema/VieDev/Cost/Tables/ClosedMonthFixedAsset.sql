CREATE TABLE [Cost].[ClosedMonthFixedAsset] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ClosedMonthId]    INT             NOT NULL,
    [CostCenterId]     INT             NOT NULL,
    [FixedAssetItemId] INT             NOT NULL,
    [TotalHoursWorked] INT             NOT NULL,
    [ValueTotal]       NUMERIC (18, 2) NOT NULL,
    [AverageCost]      NUMERIC (24, 6) NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonthFixedAsset] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthFixedAsset_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Cost].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthFixedAsset_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ClosedMonthFixedAsset_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id])
);


GO
ALTER TABLE [Cost].[ClosedMonthFixedAsset] NOCHECK CONSTRAINT [FK_ClosedMonthFixedAsset_ClosedMonth];


GO
ALTER TABLE [Cost].[ClosedMonthFixedAsset] NOCHECK CONSTRAINT [FK_ClosedMonthFixedAsset_CostCenter];


GO
ALTER TABLE [Cost].[ClosedMonthFixedAsset] NOCHECK CONSTRAINT [FK_ClosedMonthFixedAsset_FixedAssetItem];

