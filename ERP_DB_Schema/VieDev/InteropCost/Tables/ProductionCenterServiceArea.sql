CREATE TABLE [InteropCost].[ProductionCenterServiceArea] (
    [Id]                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionCenterId] INT          NOT NULL,
    [ServiceAreaId]      INT          NOT NULL,
    [ServiceAreaCode]    VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_ProductionCenterServiceArea] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionCenterServiceArea_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[ProductionCenterServiceArea] NOCHECK CONSTRAINT [FK_ProductionCenterServiceArea_ProductionCenter];

