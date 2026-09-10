CREATE TABLE [Portfolio].[PortfolioTransferReceivableReduce] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioTransferId]            INT             NOT NULL,
    [AccountReceivableId]            INT             NOT NULL,
    [ProvisionValue]                 DECIMAL (18, 2) NOT NULL,
    [DeteriorationValue]             DECIMAL (18, 2) NOT NULL,
    [DeteriorationCurrentYearValue]  DECIMAL (18, 2) NOT NULL,
    [DeteriorationPreviousYearValue] DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_PortfolioTransferReceivableReduce__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioTransferReceivableReduce_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioTransferReceivableReduce_PortfolioTransfer] FOREIGN KEY ([PortfolioTransferId]) REFERENCES [Portfolio].[PortfolioTransfer] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_PortfolioTransferReceivableReduce__PortfolioTransferId]
    ON [Portfolio].[PortfolioTransferReceivableReduce]([PortfolioTransferId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PortfolioTransferReceivableReduce__AccountReceivableId]
    ON [Portfolio].[PortfolioTransferReceivableReduce]([AccountReceivableId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de deterioro de cartera disminuido en el año anterior, impacto acumulado por reducción de provision en transferencia de anticipo vs cuentas por cobrar (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'DeteriorationPreviousYearValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de deterioro disminuido en el año anterior', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'DeteriorationPreviousYearValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'DeteriorationPreviousYearValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de deterioro de cartera disminuido en el año actual, ajuste periódico de glosa o castigo en transferencia de anticipo vs cuentas por cobrar (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'DeteriorationCurrentYearValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de deterioro disminuido en el año actual', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'DeteriorationCurrentYearValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'DeteriorationCurrentYearValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total de deterioro de cartera disminuido, impacto neto de reducción por transferencia de anticipo vs cuentas por cobrar (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'DeteriorationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de deterioro disminuido', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'DeteriorationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'DeteriorationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de provision disminuido, ajuste de reserva contable para cobranza dubiosa en transferencia de anticipo vs cuentas por cobrar (DECIMAL 18,2)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'ProvisionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de provisión disminuido', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'ProvisionValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'ProvisionValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cuenta por cobrar (anticipo), referencia a cartera o deuda pendiente de facturación, equivalente a cxc o factura pendiente', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de anticipo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de transferencia de anticipo vs cuentas por cobrar, vincula movimiento de cartera entre anticipos y cxc para reducción de provision', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'PortfolioTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de anticipo vs cxc', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'PortfolioTransferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'PortfolioTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de detalle de valores disminuidos, registro atomico de reducción de provision y deterioro por anticipo vs cuentas por cobrar (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de detalle de valores disminuidos por anticipo vs cxc', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de reducción de cuentas por cobrar asociadas a una transferencia de cartera, incluyendo los valores de provisión y deterioro (año actual y año anterior) aplicados a cada cuenta por cobrar transferida.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferReceivableReduce';
