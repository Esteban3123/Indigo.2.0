CREATE TABLE [Budget].[AnnualizedCashFlowTransferDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PACTransferId]        INT             NOT NULL,
    [AnnualizedCashFlowId] INT             NOT NULL,
    [Nature]               TINYINT         NOT NULL,
    [Value]                NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_PACTransferDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AnnualizedCashFlowTransferDetail_AnnualizedCashFlow] FOREIGN KEY ([AnnualizedCashFlowId]) REFERENCES [Budget].[AnnualizedCashFlow] ([Id]),
    CONSTRAINT [FK_PACTransferDetail_PACTransfer] FOREIGN KEY ([PACTransferId]) REFERENCES [Budget].[AnnualizedCashFlowTransfer] ([Id]),
    CONSTRAINT [UQ_AnnualizedCashFlowTransferDetail__PACTransferId__AnnualizedCashFlowId] UNIQUE NONCLUSTERED ([PACTransferId] ASC, [AnnualizedCashFlowId] ASC)
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_AnnualizedCashFlowTransferDetail__PACTransferId]
    ON [Budget].[AnnualizedCashFlowTransferDetail]([PACTransferId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18,2) del movimiento de dinero en el detalle del traslado, expresado en unidades monetarias según configuración presupuestaria.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Naturaleza del movimiento contable en el detalle: Débito (1) o Crédito (2), indica si la operación disminuye o aumenta el saldo del PAC.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza de la modificacion (Debito = 1, Credito = 2)', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'Nature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del Plan Anual de Caja (PAC) destino especificado en el traslado, referencia a flujo de caja anualizado (FK a AnnualizedCashFlow).', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'AnnualizedCashFlowId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del PAC', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'AnnualizedCashFlowId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'AnnualizedCashFlowId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera del traslado de flujo de caja anualizado (FK a AnnualizedCashFlowTransfer), vincula este detalle al movimiento padre de transferencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'PACTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del traslado al pac', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'PACTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'PACTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de traslado, clave primaria de la línea individual del movimiento de flujo de caja anualizado (IDENTITY INT).', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del traslado', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las transferencias asociadas al flujo de caja anualizado del presupuesto. Registra cada movimiento de traslado presupuestal con su naturaleza (débito o crédito) y el valor transferido.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'AnnualizedCashFlowTransferDetail';
