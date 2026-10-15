CREATE TABLE [Cost].[CostActivityProductionCenter] (
    [Id]                     INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityId]         INT NOT NULL,
    [CostProductionCenterId] INT NOT NULL,
    CONSTRAINT [PK_CostActivityProductionCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityProductionCenter_CostActivity] FOREIGN KEY ([CostActivityId]) REFERENCES [Cost].[CostActivity] ([Id]),
    CONSTRAINT [FK_CostActivityProductionCenter_CostProductionCenter] FOREIGN KEY ([CostProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
ALTER TABLE [Cost].[CostActivityProductionCenter] NOCHECK CONSTRAINT [FK_CostActivityProductionCenter_CostActivity];


GO
ALTER TABLE [Cost].[CostActivityProductionCenter] NOCHECK CONSTRAINT [FK_CostActivityProductionCenter_CostProductionCenter];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Centro de Producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter', @level2type = N'COLUMN', @level2name = N'CostProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la Actividad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter', @level2type = N'COLUMN', @level2name = N'CostActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostActivityProductionCenter', @level2type = N'COLUMN', @level2name = N'Id';

