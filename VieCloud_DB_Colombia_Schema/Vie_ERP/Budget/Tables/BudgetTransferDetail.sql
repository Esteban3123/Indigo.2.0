CREATE TABLE [Budget].[BudgetTransferDetail] (
    [Id]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferId]    INT             NOT NULL,
    [BudgetId]      INT             NOT NULL,
    [RevenueTypeId] INT             NOT NULL,
    [Nature]        TINYINT         NOT NULL,
    [Value]         NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_TransferDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BudgetTransferDetail_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_TransferDetail_RevenueType] FOREIGN KEY ([RevenueTypeId]) REFERENCES [Budget].[RevenueType] ([Id]),
    CONSTRAINT [FK_TransferDetail_Transfer] FOREIGN KEY ([TransferId]) REFERENCES [Budget].[BudgetTransfer] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de la transferencia presupuestaria en moneda local (NUMERIC 18,2). Monto que se debita o acredita según la naturaleza de la operación.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la trasferencia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza de la transferencia presupuestaria: Débito (1) = egreso/reducción, Crédito (2) = ingreso/aumento. TINYINT que define el sentido contable del movimiento.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza ( Debito = 1, Credito = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tipo de ingreso o egreso presupuestario. Referencia a la tabla RevenueType que clasifica la línea presupuestaria (ingresos por servicios, subsidios, egresos por nómina, gastos operativos, etc.).', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del tipo de ingreso/ Egreso', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'RevenueTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del presupuesto origen o destino de la transferencia. Referencia a la tabla Budget que vincula el movimiento con el presupuesto inicial o vigente.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del presupuesto inicial', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'BudgetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o encabezado de la transferencia presupuestaria. Referencia a BudgetTransfer que agrupa los detalles de una misma operación de movimiento de fondos.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'TransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la trasferencia', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'TransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'TransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY INT) de cada línea de detalle en la transferencia presupuestaria. Clave primaria de la tabla BudgetTransferDetail.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las transferencias presupuestarias: registra cada movimiento individual de traslado de recursos entre presupuestos, indicando el tipo de ingreso, la naturaleza del movimiento (débito o crédito) y el valor transferido.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'BudgetTransferDetail';
