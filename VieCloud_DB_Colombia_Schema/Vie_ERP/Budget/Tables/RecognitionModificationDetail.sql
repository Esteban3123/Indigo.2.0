CREATE TABLE [Budget].[RecognitionModificationDetail] (
    [Id]                        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RecognitionModificationId] INT          NOT NULL,
    [RecognitionDetailId]       INT          NOT NULL,
    [Nature]                    TINYINT      NOT NULL,
    [Value]                     NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_RecognitionModificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RecognitionModificationDetail_RecognitionDetail] FOREIGN KEY ([RecognitionDetailId]) REFERENCES [Budget].[RecognitionDetail] ([Id]),
    CONSTRAINT [FK_RecognitionModificationDetail_RecognitionModification] FOREIGN KEY ([RecognitionModificationId]) REFERENCES [Budget].[RecognitionModification] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18) del monto a modificar en el detalle del reconocimiento; importe de débito o crédito aplicado a la glosa, factura o contrato.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza del movimiento: DÉBITO (1) o CRÉDITO (2); tipo de ajuste contable en la modificación del reconocimiento presupuestario.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza (DEBITO = 1,CREDITO = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle del reconocimiento a modificar; referencia a Budget.RecognitionDetail que será ajustado por débito o crédito.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'RecognitionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del reconocimiento que se va a modificar', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'RecognitionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'RecognitionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera o registro principal de modificación del reconocimiento; vinculación a Budget.RecognitionModification que agrupa los cambios.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'RecognitionModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cabecera de la modificacion del reconocimiento', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'RecognitionModificationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'RecognitionModificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (IDENTITY) de la línea de detalle de modificación del reconocimiento; clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las modificaciones realizadas a reconocimientos presupuestales. Registra cada línea o movimiento de ajuste sobre un reconocimiento existente, indicando la naturaleza del cambio (débito o crédito) y el valor monetario afectado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'RecognitionModificationDetail';
