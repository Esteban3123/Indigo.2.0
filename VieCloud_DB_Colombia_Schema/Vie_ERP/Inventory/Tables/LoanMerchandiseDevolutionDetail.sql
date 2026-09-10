CREATE TABLE [Inventory].[LoanMerchandiseDevolutionDetail] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoanMerchandiseDevolutionId] INT NOT NULL,
    [LoanMerchandiseDetaillId]    INT NOT NULL,
    [Quantity]                    INT NOT NULL,
    CONSTRAINT [PK_LoanMerchandiseDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetail_LoanMerchandiseDetail] FOREIGN KEY ([LoanMerchandiseDetaillId]) REFERENCES [Inventory].[LoanMerchandiseDetail] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetail_LoanMerchandiseDevolution] FOREIGN KEY ([LoanMerchandiseDevolutionId]) REFERENCES [Inventory].[LoanMerchandiseDevolution] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades a devolver del préstamo de mercancía. Tipo: INT. Cantidad de artículos/productos/insumos que se restituyen en esta línea de devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle del préstamo de mercancía a devolver. Referencia a [Inventory].[LoanMerchandiseDetail]. Vincula cada línea de devolución al artículo/insumo prestado originalmente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDetaillId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del prestamo de mercancia que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDetaillId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDetaillId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera/encabezado de la devolución de mercancía. Referencia a [Inventory].[LoanMerchandiseDevolution]. Agrupa múltiples líneas de devolución bajo una transacción de devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del detalle de devolución de mercancía. Tipo: INT IDENTITY. Clave primaria de la línea individual de devolución en el proceso de restitución de préstamos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las devoluciones de mercancía prestada (préstamos de inventario). Registra qué artículos y en qué cantidad fueron devueltos por cada devolución, vinculando el encabezado de la devolución con el ítem original del préstamo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetail';
