CREATE TABLE [Budget].[CollectionModificationDetail] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CollectionModificationId] INT          NOT NULL,
    [CollectionDetailId]       INT          NOT NULL,
    [Nature]                   TINYINT      NOT NULL,
    [Value]                    NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CollectionModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CollectionModificationDetail_CollectionDetail] FOREIGN KEY ([CollectionDetailId]) REFERENCES [Budget].[CollectionDetail] ([Id]),
    CONSTRAINT [FK_CollectionModificationDetail_CollectionModification] FOREIGN KEY ([CollectionModificationId]) REFERENCES [Budget].[CollectionModification] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18) de la modificación al recaudo; monto que se debita o acredita en la recolección de pagos, facturación o glosa.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la modificacion', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza de la modificación (TINYINT): Débito=1 (descuento, devolución, glosa) o Crédito=2 (abono, pago, acreditación); tipo de movimiento contable.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obtiene o establece la naturaleza de la modificacion ( Debito = 1 Credito = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle del recaudo; referencia a Budget.CollectionDetail que vincula la línea específica de recolección, pago o factura modificada.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'CollectionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del recaudo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'CollectionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'CollectionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la modificación al recaudo; referencia a Budget.CollectionModification que agrupa los cambios aplicados a un cobro o ingreso.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'CollectionModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la modificacion al Recaudo', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'CollectionModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'CollectionModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de modificación de detalle de recaudo; clave primaria que rastrea cada ajuste individual.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de modificaciones aplicadas a ítems de una colección presupuestaria. Registra cada ajuste o corrección individual dentro de una modificación de cobro o recaudo, indicando el tipo de movimiento (naturaleza) y el valor modificado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'CollectionModificationDetail';
