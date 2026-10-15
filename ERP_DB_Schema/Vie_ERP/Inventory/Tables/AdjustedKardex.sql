CREATE TABLE [Inventory].[AdjustedKardex] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [KardexId]         INT             NOT NULL,
    [QuantityPrevious] INT             NOT NULL,
    [Quantity]         INT             NOT NULL,
    [ValuePrevious]    DECIMAL (18, 2) NOT NULL,
    [Value]            DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_AdjustedKardex] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdjustedKardex_Kardex] FOREIGN KEY ([KardexId]) REFERENCES [Inventory].[Kardex] ([Id])
);


GO
ALTER TABLE [Inventory].[AdjustedKardex] NOCHECK CONSTRAINT [FK_AdjustedKardex_Kardex];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Anterior', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'ValuePrevious';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad anterior', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'QuantityPrevious';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Kardex', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'KardexId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustedKardex', @level2type = N'COLUMN', @level2name = N'Id';

