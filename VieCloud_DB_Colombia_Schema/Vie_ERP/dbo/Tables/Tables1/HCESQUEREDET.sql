CREATE TABLE [dbo].[HCESQUEREDET] (
    [ID]           INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HCESQUERENID] INT       NOT NULL,
    [CODPRODUC]    CHAR (20) NOT NULL,
    [CANTIDAD]     INT       NOT NULL,
    CONSTRAINT [PK_HCESQUEREDET] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESQUEREDET_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades, dosis o mililitros del producto prescrito en el esquema renal (tipo INT, rango 0-2147483647)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto, medicamento o insumo renal (tipo CHAR 20, referencia a IHLISTPRO.CODPRODUC, búsquedas: fármaco, droga, solución dializado, anticoagulante)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la cabecera o encabezado del esquema renal (tipo INT, clave foránea a HCESQUEREN, vincula detalle con la prescripción renal principal)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'HCESQUERENID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la cabecera de Esquema Renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'HCESQUERENID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'HCESQUERENID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID único del detalle o línea del esquema renal (tipo INT IDENTITY, clave primaria, búsquedas: renograma, hemodiálisis, diálisis peritoneal, item prescripción renal)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del detalle esquema renal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los productos o ítems incluidos en cada esquema de requerimiento clínico, registrando la cantidad solicitada de cada producto o insumo médico asociado al esquema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESQUEREDET';
