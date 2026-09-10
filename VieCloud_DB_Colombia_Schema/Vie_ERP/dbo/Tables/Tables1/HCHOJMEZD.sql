CREATE TABLE [dbo].[HCHOJMEZD] (
    [CONSECUTI] NUMERIC (18)    NOT NULL,
    [CODPRODUC] CHAR (20)       NOT NULL,
    [CANTIUTIL] INT             NOT NULL,
    [DOSISPROD] NUMERIC (18, 2) NULL,
    [ID]        INT             IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_HCHOJMEZD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCHOJMEZD_HCHOJMEZC] FOREIGN KEY ([CONSECUTI]) REFERENCES [dbo].[HCHOJMEZC] ([CONSECUTI]),
    CONSTRAINT [FK_HCHOJMEZD_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración o dosis del producto medicamento/fármaco ordenado; expresado en unidades de medida farmacéutica (mg, ml, unidades); permite búsquedas por dosis prescrita en recetas, órdenes médicas y procedimientos. Type: NUMERIC(18,2); Nullable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concetración ó Dosis del producto ordenada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad utilizada u ordenada del producto; número de unidades, dosis o presentaciones dispensadas en la atención; clave para auditoría de consumo farmacéutico y facturación. Type: INT; requerido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'CANTIUTIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Utilizada/ordenada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'CANTIUTIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'CANTIUTIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto medicamento, insumo, material médico o fármaco; identificador único que referencia el catálogo maestro de productos (IHLISTPRO); facilita búsquedas por nombre comercial, principio activo o tipo de producto. Type: CHAR(20); FK a IHLISTPRO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo autonumérico de cabecera; número secuencial que agrupa detalles de productos en un documento de atención, orden o movimiento; enlace a registro padre en HCHOJMEZC. Type: NUMERIC(18); PK referenciado; requerido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo Autonumerico de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'CONSECUTI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'CONSECUTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de medicamentos o insumos utilizados en una hoja de enfermería o evolución clínica. Registra qué producto se usó, la cantidad aplicada y la dosis correspondiente por cada registro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro, generado automáticamente por el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJMEZD', @level2type = N'COLUMN', @level2name = N'ID';
