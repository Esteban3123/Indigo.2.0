CREATE TABLE [Inventory].[AttributeProductType] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [ProductTypeId]    INT           NOT NULL,
    [DataType]         TINYINT       NOT NULL,
    [Status]           BIT           NOT NULL,
    [CreationUser]     VARCHAR (20)  CONSTRAINT [DF_AttributeProductType_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]     DATETIME      CONSTRAINT [DF_AttributeProductType_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    CONSTRAINT [PK_AttributeProductType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttributeProductType_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [Inventory].[ProductType] ([Id])
);


GO
ALTER TABLE [Inventory].[AttributeProductType] NOCHECK CONSTRAINT [FK_AttributeProductType_ProductType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de dato del atributo  1 - Fecha  2 - Numero  3 - Texto  4 - Lista de opciones', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductType', @level2type = N'COLUMN', @level2name = N'DataType';

