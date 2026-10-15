CREATE TABLE [Cost].[CostAttentionCenter] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [HealthProviderId] INT           NOT NULL,
    CONSTRAINT [PK_CostAttentionCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostAttentionCenter_HealthProvider] FOREIGN KEY ([HealthProviderId]) REFERENCES [Common].[HealthProvider] ([Id])
);


GO
ALTER TABLE [Cost].[CostAttentionCenter] NOCHECK CONSTRAINT [FK_CostAttentionCenter_HealthProvider];

