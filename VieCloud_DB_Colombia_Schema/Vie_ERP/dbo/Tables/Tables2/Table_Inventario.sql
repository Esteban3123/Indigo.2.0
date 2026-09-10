CREATE TABLE [dbo].[Table_Inventario] (
    [ID]           INT          IDENTITY (1, 1) NOT NULL,
    [CODIGO]       VARCHAR (50) NULL,
    [CANTIDAD]     INT          NULL,
    [PRESENTACION] INT          NULL,
    CONSTRAINT [PK_Table_Inventario] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla que registra el inventario de artículos o insumos, almacenando un código identificador, la cantidad disponible y un tipo de presentación codificado como entero. Su estructura mínima sugiere que actúa como catálogo base de stock, posiblemente de medicamentos o insumos médicos, aunque sin FKs visibles que confirmen la relación con otras entidades del sistema.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Table_Inventario';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Table_Inventario';
GO
