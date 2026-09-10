CREATE TABLE [Security].[ProductCatalog] (
    [Id]           SMALLINT      NOT NULL,
    [PlatformName] VARCHAR (100) NOT NULL,
    [SuiteName]    VARCHAR (100) NOT NULL,
    [ProductName]  VARCHAR (100) NOT NULL,
    [State]        BIT           NOT NULL,
    [Visible]      BIT           NOT NULL,
    CONSTRAINT [PK_ProductCatalog] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ProductCatalog', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Visible para mostrar en menu', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'ProductCatalog', @level2type = N'COLUMN', @level2name = N'Visible';

