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

