CREATE TABLE [Common].[ISO4217] (
    [Id]               INT          IDENTITY (1, 1) NOT NULL,
    [CodeAbbreviation] VARCHAR (3)  NOT NULL,
    [CurrencyName]     VARCHAR (25) NOT NULL,
    CONSTRAINT [PK_ISO4217__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la divisa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217', @level2type = N'COLUMN', @level2name = N'CurrencyName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de 3 caracteres de la divisa', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217', @level2type = N'COLUMN', @level2name = N'CodeAbbreviation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ISO4217', @level2type = N'COLUMN', @level2name = N'Id';

