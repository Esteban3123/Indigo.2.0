CREATE TABLE [Payments].[FactoringPreparationDetail] (
    [Id]                     INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFactoringPreparation] INT NOT NULL,
    [IdAccountPayable]       INT NOT NULL,
    CONSTRAINT [PK_FactoringPreparationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FactoringPreparationDetail_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_FactoringPreparationDetail_FactoringPreparation] FOREIGN KEY ([IdFactoringPreparation]) REFERENCES [Payments].[FactoringPreparation] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por pagar (FK). Referencia a Payments.AccountPayable. Vincula la obligación financiera pendiente de pago al detalle de factoring. Tipo: INT, clave foránea.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail', @level2type = N'COLUMN', @level2name = N'IdAccountPayable';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la preparación de factoring (FK). Referencia a Payments.FactoringPreparation. Agrupa múltiples cuentas por pagar en un lote de factoring para gestión y descuento. Tipo: INT, clave foránea.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail', @level2type = N'COLUMN', @level2name = N'IdFactoringPreparation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la preparacion de factoring', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail', @level2type = N'COLUMN', @level2name = N'IdFactoringPreparation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail', @level2type = N'COLUMN', @level2name = N'IdFactoringPreparation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador incremental único del detalle de factoring (PK). Tipo: INT IDENTITY(1,1). Clave primaria que identifica unívocamente cada relación entre factoring y cuenta por pagar.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id incremental del detalle', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la preparación de factoring: registra cada cuenta por pagar incluida en un proceso de cesión o anticipo de cartera (factoring), vinculando el encabezado de preparación con las obligaciones de pago individuales seleccionadas.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'FactoringPreparationDetail';
