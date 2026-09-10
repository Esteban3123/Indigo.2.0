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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca del producto cotizado (FK → SOLMARCA). Identificador de fabricante, referencia de laboratorio o casa comercial del artículo en cotización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'MARCPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'MARCPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'MARCPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad del producto en cotización (NUMERIC 5). Unidades, presentaciones o dosis que el proveedor cotiza para este renglón de solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'CANTICOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la cantidad del producto que cotiza el proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'CANTICOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'CANTICOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico del producto cotizado (FK → SOLPRODUC). Identificador único del artículo, medicamento, insumo o dispositivo médico en catálogo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el Autonumerico del Producto que cotiza el proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario de la cotización (NUMERIC 18,2). Valor en moneda del costo que el proveedor propone por unidad del producto en esta oferta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'PRECCOTIZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'contiene el autonumerico del proveedor que cotiza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'PRECCOTIZ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'PRECCOTIZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico de la cotización padre (FK → SOLCOTIZACION). Referencia a la solicitud de cotización principal que agrupa este detalle.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'autonumerico de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'AUTOCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY 1,1). Clave primaria de la tabla de detalles de cotización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las cotizaciones solicitadas: registra cada ítem o producto cotizado dentro de una solicitud de cotización, incluyendo precio, cantidad y marca.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLCOTDETA';
