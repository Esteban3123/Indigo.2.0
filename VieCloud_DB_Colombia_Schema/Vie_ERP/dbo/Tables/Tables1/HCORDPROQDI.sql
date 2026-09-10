CREATE TABLE [dbo].[HCORDPROQDI] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AUTOPROCED]  INT       NOT NULL,
    [CODPRODUC]   CHAR (20) NOT NULL,
    [CANTSOLICIT] INT       NOT NULL,
    [CANTENTREG]  INT       NOT NULL,
    [MATESTADO]   TINYINT   NOT NULL,
    CONSTRAINT [PK_HCORDPROQDI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDPROQDI_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la solicitud de material quirúrgico: 1=Solicitado, 2=Orden de compra generada. Tipo: TINYINT. Indica fase del suministro de osteosíntesis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'MATESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la solicitud:  1-> Solicitado   2-> Orden Compra generada para el producto.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'MATESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'MATESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades entregadas del producto de osteosíntesis. Tipo: INT. Permite auditar diferencias entre solicitado vs entregado en procedimiento quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'CANTENTREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad entregada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'CANTENTREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'CANTENTREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades solicitadas del producto de osteosíntesis para el procedimiento quirúrgico. Tipo: INT. Controla requerimiento inicial de material.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'CANTSOLICIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Solicitada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'CANTSOLICIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'CANTSOLICIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto de osteosíntesis (placas, tornillos, implantes). Tipo: CHAR(20). FK a IHLISTPRO. Identifica material quirúrgico específico usado en procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del producto de osteosintesis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código autoincremental del procedimiento quirúrgico. Tipo: INT. Referencia única al acto quirúrgico donde se consume el material de osteosíntesis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'AUTOPROCED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del autonumerico del procedimiento qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'AUTOPROCED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'AUTOPROCED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código autoincremental del registro de detalle producto-orden quirúrgica. Tipo: INT IDENTITY. Identificador único de cada línea de material solicitado/entregado en procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del autonumerico del procedimiento qx', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de productos o materiales solicitados y entregados en una orden de procedimiento quirúrgico de historia clínica. Registra qué insumos o medicamentos se pidieron y cuántos fueron efectivamente despachados para cada procedimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDPROQDI';
