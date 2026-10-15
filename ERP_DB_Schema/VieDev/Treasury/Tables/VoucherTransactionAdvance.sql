CREATE TABLE [Treasury].[VoucherTransactionAdvance] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVoucherTransactionD] INT             NOT NULL,
    [PortfolioAdvanceId]    INT             NOT NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [Percentage]            NUMERIC (5, 2)  NOT NULL,
    CONSTRAINT [PK_VoucherTransactionAdvance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VoucherTransactionAdvance_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id]),
    CONSTRAINT [FK_VoucherTransactionAdvance_VoucherTransactionDetails] FOREIGN KEY ([IdVoucherTransactionD]) REFERENCES [Treasury].[VoucherTransactionDetails] ([Id])
);


GO
ALTER TABLE [Treasury].[VoucherTransactionAdvance] NOCHECK CONSTRAINT [FK_VoucherTransactionAdvance_PortfolioAdvance];


GO
ALTER TABLE [Treasury].[VoucherTransactionAdvance] NOCHECK CONSTRAINT [FK_VoucherTransactionAdvance_VoucherTransactionDetails];

