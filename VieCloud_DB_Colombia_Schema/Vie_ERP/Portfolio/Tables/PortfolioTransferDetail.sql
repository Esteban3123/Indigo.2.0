CREATE TABLE [Portfolio].[PortfolioTransferDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioTrasferId]     INT             NOT NULL,
    [AccountReceivableId]    INT             NOT NULL,
    [MainAccountId]          INT             NOT NULL,
    [CostCenterId]           INT             NULL,
    [Value]                  NUMERIC (18, 2) NOT NULL,
    [TRMValue]               NUMERIC (20, 5) CONSTRAINT [DF__Portfolio__TRMVa__1D579DAB] DEFAULT ((1)) NOT NULL,
    [ValueInCurrencyInvoice] NUMERIC (18, 2) CONSTRAINT [DF__Portfolio__Value__1166C812] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PortfolioTransferDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioTransferDetail_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioTransferDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PortfolioTransferDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PortfolioTransferDetail_PortfolioTransfer] FOREIGN KEY ([PortfolioTrasferId]) REFERENCES [Portfolio].[PortfolioTransfer] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioTransferDetail] NOCHECK CONSTRAINT [FK_PortfolioTransferDetail_AccountReceivable];




GO
ALTER TABLE [Portfolio].[PortfolioTransferDetail] NOCHECK CONSTRAINT [FK_PortfolioTransferDetail_AccountReceivable];


GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IDX_PortfolioTransferDetail_PortfolioTrasferId]
    ON [Portfolio].[PortfolioTransferDetail]([PortfolioTrasferId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PortfolioTransferDetail_AccountReceivableId]
    ON [Portfolio].[PortfolioTransferDetail]([AccountReceivableId] ASC)
    INCLUDE([PortfolioTrasferId], [Value]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de la factura en su moneda original de emisión (NUMERIC 18,2). Representa el monto a transferir expresado en la divisa del contrato/factura, convertido mediante TRM.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la moneda del contrato', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'ValueInCurrencyInvoice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tasa de cambio (TRM), tasa representativa del mercado o factor de conversión aplicado al valor base. NUMERIC 20,5, default=1. Convierte Value a ValueInCurrencyInvoice.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Establece el valor de la moneda.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'TRMValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'TRMValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor base a pagar/transferir de la factura o cuenta por cobrar en moneda local o de referencia (NUMERIC 18,2). Monto principal antes de aplicar conversión de moneda.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor que se pagar de esta factura', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de costo, unidad funcional o área responsable de la transferencia (FK Payroll.CostCenter). Requerido solo si la cuenta contable lo demanda. Puede ser nulo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro del costo, solo si la cuenta lo requiere', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal, cuenta de mayor o código contable que será afectado/debitado por la transferencia (FK GeneralLedger.MainAccounts). Ubicación en libro mayor.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable que se va afectar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por cobrar, factura o comprobante de ingresos que se cruza/liquida mediante esta transferencia de cartera (FK Portfolio.AccountReceivable). Documento a afectar.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar que se va cruzar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera, encabezado o registro maestro de la transferencia de cartera (FK Portfolio.PortfolioTransfer). Documento padre del detalle.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'PortfolioTrasferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la traslado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'PortfolioTrasferId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'PortfolioTrasferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, INT IDENTITY) del registro de detalle en la transferencia de cartera. Clave primaria de PortfolioTransferDetail.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del traslado de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las transferencias de cartera: registra cada cuenta por cobrar incluida en una transferencia, con su valor, el centro de costos asociado y el valor convertido según la tasa de cambio (TRM) en la moneda de la factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetail';
