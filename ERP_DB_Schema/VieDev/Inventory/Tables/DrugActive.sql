CREATE TABLE [Inventory].[DrugActive] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [ParentDCIId] INT NOT NULL,
    [DCIId]       INT NULL,
    [ATCEntityId] INT NOT NULL,
    CONSTRAINT [PK_DrugActive] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DCI_DCI] FOREIGN KEY ([DCIId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [FK_DCI_ParentDCI] FOREIGN KEY ([ParentDCIId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [FK_DrugActive_ATCEntity] FOREIGN KEY ([ATCEntityId]) REFERENCES [Inventory].[ATCEntity] ([Id])
);


GO
ALTER TABLE [Inventory].[DrugActive] NOCHECK CONSTRAINT [FK_DCI_DCI];


GO
ALTER TABLE [Inventory].[DrugActive] NOCHECK CONSTRAINT [FK_DCI_ParentDCI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion del a entidad del ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'ATCEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion del DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'DCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificaccion Padre del DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'ParentDCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugActive', @level2type = N'COLUMN', @level2name = N'Id';

