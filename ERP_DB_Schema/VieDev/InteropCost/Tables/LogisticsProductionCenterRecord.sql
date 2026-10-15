CREATE TABLE [InteropCost].[LogisticsProductionCenterRecord] (
    [Id]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]               VARCHAR (20)  NOT NULL,
    [ProductionCenterId] INT           NOT NULL,
    [RecordDate]         DATETIME      NOT NULL,
    [Description]        VARCHAR (500) NULL,
    [Status]             TINYINT       NOT NULL,
    [CreationUser]       VARCHAR (20)  NOT NULL,
    [CreationDate]       DATETIME      NOT NULL,
    [ModificationUser]   VARCHAR (20)  NULL,
    [ModificationDate]   DATETIME      NULL,
    [ConfirmUser]        VARCHAR (20)  NULL,
    [ConfirmDate]        DATETIME      NULL,
    CONSTRAINT [PK_LogisticsProductionCenterRecord__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LogisticsProductionCenterRecord_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[LogisticsProductionCenterRecord] NOCHECK CONSTRAINT [FK_LogisticsProductionCenterRecord_ProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado en que se encuentra el registro:  ==========================  1 - Registrado  2 - Confirmado', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'LogisticsProductionCenterRecord', @level2type = N'COLUMN', @level2name = N'Status';

