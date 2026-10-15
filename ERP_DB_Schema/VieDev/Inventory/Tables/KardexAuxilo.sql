CREATE TABLE [Inventory].[KardexAuxilo] (
    [Id]                    INT NOT NULL,
    [PreviousAmountProduct] INT NOT NULL,
    [CantidaReal]           INT NOT NULL,
    CONSTRAINT [PK_KardexAuxilo] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad real', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo', @level2type = N'COLUMN', @level2name = N'CantidaReal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad anterior del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo', @level2type = N'COLUMN', @level2name = N'PreviousAmountProduct';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'KardexAuxilo', @level2type = N'COLUMN', @level2name = N'Id';

