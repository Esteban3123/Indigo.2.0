CREATE TABLE [Inventory].[DrugInteraction] (
    [Id]          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ParentDCIId] INT           NOT NULL,
    [DCIId]       INT           NULL,
    [RiskLevel]   TINYINT       NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    [ATCEntityId] INT           NULL,
    CONSTRAINT [PK_DrugInteraction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DrugInteraction_ATCEntity] FOREIGN KEY ([ATCEntityId]) REFERENCES [Inventory].[ATCEntity] ([Id]),
    CONSTRAINT [FK_DrugInteraction_DCI] FOREIGN KEY ([DCIId]) REFERENCES [Inventory].[DCI] ([Id]),
    CONSTRAINT [FK_DrugInteraction_DCI_Parent] FOREIGN KEY ([ParentDCIId]) REFERENCES [Inventory].[DCI] ([Id])
);


GO
ALTER TABLE [Inventory].[DrugInteraction] NOCHECK CONSTRAINT [FK_DrugInteraction_DCI];


GO
ALTER TABLE [Inventory].[DrugInteraction] NOCHECK CONSTRAINT [FK_DrugInteraction_DCI_Parent];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'id de la entidad del ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'ATCEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la interaccion con el medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el nivel de riesgo de la interaccion  1 - Graves  2 - Moderadas  3 - Leves', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'RiskLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del dci con el que interactua', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'DCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del dci padre, el cual tiene interaccion con otros', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'ParentDCIId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la interaccion del medicamento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DrugInteraction', @level2type = N'COLUMN', @level2name = N'Id';

