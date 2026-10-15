CREATE TABLE [InteropCost].[DistributionIntermediate] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]               VARCHAR (20)  NOT NULL,
    [ProductionCenterId] INT           NOT NULL,
    [Description]        VARCHAR (300) NOT NULL,
    [Year]               INT           NOT NULL,
    [Month]              INT           NOT NULL,
    [Status]             BIT           NOT NULL,
    [CreationUser]       VARCHAR (20)  NOT NULL,
    [CreationDate]       DATETIME      NOT NULL,
    [ModificationUser]   VARCHAR (20)  NULL,
    [ModificationDate]   DATETIME      NULL,
    [TimeStamp]          ROWVERSION    NOT NULL,
    CONSTRAINT [PK_DistributionIntermediate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionIntermediate_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionIntermediate] NOCHECK CONSTRAINT [FK_DistributionIntermediate_ProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del de la entidad 1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediate', @level2type = N'COLUMN', @level2name = N'Status';

