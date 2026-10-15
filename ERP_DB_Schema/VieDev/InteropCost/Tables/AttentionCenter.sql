CREATE TABLE [InteropCost].[AttentionCenter] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [HealthProviderId] INT           NOT NULL,
    CONSTRAINT [PK_AttentionCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttentionCenter_HealthProvider] FOREIGN KEY ([HealthProviderId]) REFERENCES [Common].[HealthProvider] ([Id])
);


GO
ALTER TABLE [InteropCost].[AttentionCenter] NOCHECK CONSTRAINT [FK_AttentionCenter_HealthProvider];

