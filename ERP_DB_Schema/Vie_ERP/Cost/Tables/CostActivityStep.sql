CREATE TABLE [Cost].[CostActivityStep] (
    [Id]             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityId] INT           NOT NULL,
    [Order]          INT           NOT NULL,
    [Description]    VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_CostActivityStep__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStep_CostActivity] FOREIGN KEY ([CostActivityId]) REFERENCES [Cost].[CostActivity] ([Id])
);


GO
ALTER TABLE [Cost].[CostActivityStep] NOCHECK CONSTRAINT [FK_CostActivityStep_CostActivity];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_CostActivityStep__CostActivityId__Order]
    ON [Cost].[CostActivityStep]([CostActivityId] ASC, [Order] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orden', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'Order';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la Actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'CostActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityStep', @level2type = N'COLUMN', @level2name = N'Id';

