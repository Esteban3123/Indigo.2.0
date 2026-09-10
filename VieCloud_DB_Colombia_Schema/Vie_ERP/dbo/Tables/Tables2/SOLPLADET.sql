CREATE TABLE [dbo].[SOLPLADET] (
    [PLADETAUT] INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PLANTAUTO] INT NOT NULL,
    [PRODAUTON] INT NOT NULL,
    [PLADETCAN] INT NOT NULL,
    CONSTRAINT [PK_SOLPLADET] PRIMARY KEY CLUSTERED ([PLADETAUT] ASC),
    CONSTRAINT [FK_SOLPLADET_SOLPLANTI] FOREIGN KEY ([PLANTAUTO]) REFERENCES [dbo].[SOLPLANTI] ([PLANTAUTO]),
    CONSTRAINT [FK_SOLPLADET_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto en el detalle de la plantilla. Tipo: INT. Valor numérico que representa la cantidad solicitada, prescrita o facturada del artículo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PLADETCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la cantidad de producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PLADETCAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PLADETCAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) del producto/artículo. Tipo: INT. Referencia a SOLPRODUC. Clave foránea que vincula con catálogo de productos, medicamentos, insumos o servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) de la plantilla madre o cabecera. Tipo: INT. Referencia a SOLPLANTI. Clave foránea que agrupa los detalles de una plantilla de solicitud, orden, receta o prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PLANTAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene le autonumerico de la cabecera de la plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PLANTAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PLANTAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (autonumérico) de este registro de detalle. Tipo: INT PRIMARY KEY. Clave primaria que identifica unívocamente cada línea de producto en la plantilla.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PLADETAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PLADETAUT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET', @level2type = N'COLUMN', @level2name = N'PLADETAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las solicitudes de planillas de autorización. Registra cada ítem o producto autorizado dentro de una planilla de autorización, indicando la cantidad solicitada y la cantidad cancelada por ítem.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLADET';
