CREATE TABLE [InteropCost].[DistributionManpowerDetail] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionManpowerId]    INT             NOT NULL,
    [ProductionCenterId]        INT             NOT NULL,
    [HoursQuantity]             INT             NOT NULL,
    [TotalAccrued]              NUMERIC (18, 2) CONSTRAINT [DF_DistributionManpowerDetail_TotalAccrued] DEFAULT ((0)) NOT NULL,
    [TotalProvision]            NUMERIC (18, 2) CONSTRAINT [DF_DistributionManpowerDetail_TotalProvision] DEFAULT ((0)) NOT NULL,
    [TotalEmployerContribution] NUMERIC (18, 2) CONSTRAINT [DF_DistributionManpowerDetail_TotalEmployerContribution] DEFAULT ((0)) NOT NULL,
    [TotalParafiscal]           NUMERIC (18, 2) CONSTRAINT [DF_DistributionManpowerDetail_TotalParafiscal] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_DistributionManpowerDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionManpowerDetail_DistributionManpower] FOREIGN KEY ([DistributionManpowerId]) REFERENCES [InteropCost].[DistributionManpower] ([Id]),
    CONSTRAINT [FK_DistributionManpowerDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionManpowerDetail] NOCHECK CONSTRAINT [FK_DistributionManpowerDetail_DistributionManpower];


GO
ALTER TABLE [InteropCost].[DistributionManpowerDetail] NOCHECK CONSTRAINT [FK_DistributionManpowerDetail_ProductionCenter];

