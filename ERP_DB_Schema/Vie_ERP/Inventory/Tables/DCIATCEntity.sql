CREATE TABLE [Inventory].[DCIATCEntity] (
    [Id]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdDCI]       INT NOT NULL,
    [IdATCEntity] INT NOT NULL,
    CONSTRAINT [PK_DCIATCEntity] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DCIATCEntity_ATCEntity] FOREIGN KEY ([IdATCEntity]) REFERENCES [Inventory].[ATCEntity] ([Id]),
    CONSTRAINT [FK_DCIATCEntity_DCI] FOREIGN KEY ([IdDCI]) REFERENCES [Inventory].[DCI] ([Id])
);


GO
ALTER TABLE [Inventory].[DCIATCEntity] NOCHECK CONSTRAINT [FK_DCIATCEntity_ATCEntity];


GO
ALTER TABLE [Inventory].[DCIATCEntity] NOCHECK CONSTRAINT [FK_DCIATCEntity_DCI];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Entidad ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity', @level2type = N'COLUMN', @level2name = N'IdATCEntity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del DCI', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity', @level2type = N'COLUMN', @level2name = N'IdDCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DCIATCEntity', @level2type = N'COLUMN', @level2name = N'Id';

