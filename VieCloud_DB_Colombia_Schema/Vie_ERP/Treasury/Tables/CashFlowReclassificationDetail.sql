CREATE TABLE [Treasury].[CashFlowReclassificationDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdCashFlowReclassification] INT             NOT NULL,
    [DocumentId]                 INT             NOT NULL,
    [CreditValue]                DECIMAL (18, 2) NULL,
    [DebitValue]                 DECIMAL (18, 2) NULL,
    [PreviousCashFlowConcept]    INT             NULL,
    [CurrentCashFlowConcept]     INT             NULL,
    CONSTRAINT [PK_CashFlowReclassificationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashFlowReclassificationDetail_CashFlowReclassification] FOREIGN KEY ([IdCashFlowReclassification]) REFERENCES [Treasury].[CashFlowReclassification] ([Id]),
    CONSTRAINT [FK_CashFlowReclassificationDetail_CurrentCashFlowConcept] FOREIGN KEY ([CurrentCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_CashFlowReclassificationDetail_PreviousCashFlowConcept] FOREIGN KEY ([PreviousCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id])
);




GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_CashFlowReclassificationDetail_PreviousCashFlowConcept]
    ON [Treasury].[CashFlowReclassificationDetail]([PreviousCashFlowConcept] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CashFlowReclassificationDetail_IdCashFlowReclassification]
    ON [Treasury].[CashFlowReclassificationDetail]([IdCashFlowReclassification] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CashFlowReclassificationDetail_CurrentCashFlowConcept]
    ON [Treasury].[CashFlowReclassificationDetail]([CurrentCashFlowConcept] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de flujo de efectivo actual (categoría contable nueva: operativo, inversión, financiamiento). Referencia FK a [Treasury].[CashFlowConcept]. Tipo: INT, nullable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'CurrentCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de flujo de efectivo actual', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'CurrentCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'CurrentCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto de flujo de efectivo anterior (categoría contable previa: operativo, inversión, financiamiento). Referencia FK a [Treasury].[CashFlowConcept]. Tipo: INT, nullable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'PreviousCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto de flujo de efectivo anterior', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'PreviousCashFlowConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'PreviousCashFlowConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en débito (salida de efectivo, egreso, cargo). Registrado en moneda contable. Tipo: DECIMAL(18,2), nullable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor débito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'DebitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'DebitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor en crédito (entrada de efectivo, ingreso, abono). Registrado en moneda contable. Tipo: DECIMAL(18,2), nullable.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor crédito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'CreditValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'CreditValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del documento origen (factura, recibo, comprobante, cheque). Referencia al movimiento contable o de caja. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'DocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de documento origen', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'DocumentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'DocumentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la reclasificación de concepto de flujo de efectivo padre (FK). Vincula al movimiento tesorería principal. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowReclassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de reclasificación de concepto de flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowReclassification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'IdCashFlowReclassification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de detalle de reclasificación de flujo de efectivo. Tipo: INT IDENTITY.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las reclasificaciones de flujo de caja (tesorería): registra cada documento o movimiento financiero que fue reclasificado, indicando los valores de crédito y débito involucrados, y el concepto de flujo de caja anterior y el nuevo concepto al que fue reasignado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'CashFlowReclassificationDetail';
