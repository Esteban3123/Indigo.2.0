CREATE TABLE [Inventory].[EntranceVoucherDevolutionObligationBudget] (
    [Id]                          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EntranceVoucherDevolutionId] INT             NOT NULL,
    [ObligationDetailId]          INT             NOT NULL,
    [Value]                       DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_EntranceVoucherDevolutionObligationBudget] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntranceVoucherDevolutionObligationBudget_EntranceVoucherDevolution] FOREIGN KEY ([EntranceVoucherDevolutionId]) REFERENCES [Inventory].[EntranceVoucherDevolution] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolutionObligationBudget_ObligationDetail] FOREIGN KEY ([ObligationDetailId]) REFERENCES [Budget].[ObligationDetail] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) con el cual se modificará, ajustará o imputará la obligación presupuestal en el detalle seleccionado; monto de devolución a descontar de la obligación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor con el cual se modificará la obligación en el detalle seleccionado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle de la obligación presupuestal; referencia a Budget.ObligationDetail para vincular la línea de obligación a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Detalle de la obligacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'ObligationDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera/encabezado del comprobante de entrada en devolución; referencia a Inventory.EntranceVoucherDevolution que agrupa todas las devoluciones de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la devolución', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de asignación presupuestal en devolución de comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las devoluciones de comprobantes de entrada (vales de devolución) con las obligaciones presupuestales afectadas, registrando el valor económico de cada obligación comprometida en la devolución de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionObligationBudget';
