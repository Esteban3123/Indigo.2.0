CREATE TABLE [Contract].[MarketingUnitCups] (
    [Id]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MarketingUnitId] INT NULL,
    [CupsEntityId]    INT NULL,
    CONSTRAINT [PK_MarketingUnitCups__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MarketingUnitCups_CupsEntity] FOREIGN KEY ([CupsEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_MarketingUnitCups_MarketingUnit] FOREIGN KEY ([MarketingUnitId]) REFERENCES [Contract].[MarketingUnit] ([Id])
);


GO
ALTER TABLE [Contract].[MarketingUnitCups] NOCHECK CONSTRAINT [FK_MarketingUnitCups_CupsEntity];


GO
ALTER TABLE [Contract].[MarketingUnitCups] NOCHECK CONSTRAINT [FK_MarketingUnitCups_MarketingUnit];

