CREATE TABLE [MixingStation].[CMMixingProducitonLine] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Id_CMConfiguration] INT NOT NULL,
    [Id_ProductionLine]  INT NOT NULL,
    [StatePl]            BIT NULL,
    CONSTRAINT [PK_CMMixingProducitonLine] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMMixingProducitonLine_CMConfiguration] FOREIGN KEY ([Id_CMConfiguration]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_CMMixingProducitonLine_ProductionLine] FOREIGN KEY ([Id_ProductionLine]) REFERENCES [MixingStation].[ProductionLine] ([Id])
);


GO
ALTER TABLE [MixingStation].[CMMixingProducitonLine] NOCHECK CONSTRAINT [FK_CMMixingProducitonLine_CMConfiguration];


GO
ALTER TABLE [MixingStation].[CMMixingProducitonLine] NOCHECK CONSTRAINT [FK_CMMixingProducitonLine_ProductionLine];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del registro: 1:Activo, 0:Inactivo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'StatePl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID linea de produccón', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'Id_ProductionLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id CM Configuration', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'Id_CMConfiguration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMMixingProducitonLine', @level2type = N'COLUMN', @level2name = N'Id';

