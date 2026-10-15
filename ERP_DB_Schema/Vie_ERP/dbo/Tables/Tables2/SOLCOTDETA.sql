CREATE TABLE [dbo].[SOLCOTDETA] (
    [AUTO]      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOCOTI]  INT             NOT NULL,
    [PRECCOTIZ] NUMERIC (18, 2) NOT NULL,
    [PRODAUTON] INT             NOT NULL,
    [CANTICOTI] NUMERIC (5)     NOT NULL,
    [MARCPROD]  INT             NOT NULL,
    CONSTRAINT [PK_SOLCOTDETA] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [FK_SOLCOTDETA_SOLMARCA] FOREIGN KEY ([MARCPROD]) REFERENCES [dbo].[SOLMARCA] ([Autonumerico]),
    CONSTRAINT [FK_SOLCOTDETA_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON])
);


GO
ALTER TABLE [dbo].[SOLCOTDETA] NOCHECK CONSTRAINT [FK_SOLCOTDETA_SOLMARCA];


GO
ALTER TABLE [dbo].[SOLCOTDETA] NOCHECK CONSTRAINT [FK_SOLCOTDETA_SOLPRODUC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'MARCPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la cantidad del producto que cotiza el proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'CANTICOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el Autonumerico del Producto que cotiza el proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'contiene el autonumerico del proveedor que cotiza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'PRECCOTIZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'autonumerico de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'AUTO';

