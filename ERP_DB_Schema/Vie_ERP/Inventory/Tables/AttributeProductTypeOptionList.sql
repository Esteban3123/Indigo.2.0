CREATE TABLE [Inventory].[AttributeProductTypeOptionList] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AttributeProductTypeId] INT           NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [Name]                   VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_AttributeProductTypeOptionList] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttributeProductTypeOptionList_AttributeProductType] FOREIGN KEY ([AttributeProductTypeId]) REFERENCES [Inventory].[AttributeProductType] ([Id])
);


GO
ALTER TABLE [Inventory].[AttributeProductTypeOptionList] NOCHECK CONSTRAINT [FK_AttributeProductTypeOptionList_AttributeProductType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la opcion de lista', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la opcion de la lista', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del atributo del tipo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'AttributeProductTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la lista de opciones de los atributos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'Id';

