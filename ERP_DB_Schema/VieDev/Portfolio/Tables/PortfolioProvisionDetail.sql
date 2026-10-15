CREATE TABLE [Portfolio].[PortfolioProvisionDetail] (
    [Id]                               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioProvisionId]             INT             NOT NULL,
    [AccountReceivableId]              INT             NOT NULL,
    [InvoiceNumber]                    VARCHAR (50)    NOT NULL,
    [Percentage]                       NUMERIC (7, 4)  NOT NULL,
    [Value]                            NUMERIC (18, 2) NOT NULL,
    [BalanceAccountReceivable]         NUMERIC (18, 2) NOT NULL,
    [ConfirmDateAccountReceivable]     DATETIME        NOT NULL,
    [AgesId]                           INT             NULL,
    [NetPresentValue]                  DECIMAL (18, 2) CONSTRAINT [DF__Portfolio__NetPr__48E37C33] DEFAULT ((0)) NOT NULL,
    [Expectative]                      INT             CONSTRAINT [DF__Portfolio__Expec__47EF57FA] DEFAULT ((0)) NOT NULL,
    [ValueGlosado]                     DECIMAL (18, 2) CONSTRAINT [DF__Portfolio__Value__46FB33C1] DEFAULT ((0)) NOT NULL,
    [FacturerValue]                    DECIMAL (18, 2) CONSTRAINT [DF__Portfolio__Factu__46070F88] DEFAULT ((0)) NOT NULL,
    [AccumulatedDeterioration]         DECIMAL (18, 2) CONSTRAINT [DF_PortfolioProvisionDetail_AccumulatedDeterioration] DEFAULT ((0)) NOT NULL,
    [DeteriorationBalanceCurrentYear]  DECIMAL (18, 2) CONSTRAINT [DF_PortfolioProvisionDetail_DeteriorationBalanceCurrentYear] DEFAULT ((0)) NOT NULL,
    [DeteriorationBalancePreviousYear] DECIMAL (18, 2) CONSTRAINT [DF_PortfolioProvisionDetail_DeteriorationBalancePreviousYear] DEFAULT ((0)) NOT NULL,
    [CurrentDeteriorationYear]         INT             NULL,
    [Days]                             INT             CONSTRAINT [DF_PortfolioProvisionDetail_Days] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PortfolioProvisionDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioProvisionDetail_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioProvisionDetail_AgesPortfolio] FOREIGN KEY ([AgesId]) REFERENCES [Portfolio].[AgesPortfolio] ([Id]),
    CONSTRAINT [FK_PortfolioProvisionDetail_PortfolioProvision] FOREIGN KEY ([PortfolioProvisionId]) REFERENCES [Portfolio].[PortfolioProvision] ([Id])
);


GO
ALTER TABLE [Portfolio].[PortfolioProvisionDetail] NOCHECK CONSTRAINT [FK_PortfolioProvisionDetail_AccountReceivable];


GO
ALTER TABLE [Portfolio].[PortfolioProvisionDetail] NOCHECK CONSTRAINT [FK_PortfolioProvisionDetail_AgesPortfolio];


GO
ALTER TABLE [Portfolio].[PortfolioProvisionDetail] NOCHECK CONSTRAINT [FK_PortfolioProvisionDetail_PortfolioProvision];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Año actual deteriorado.  Corresponde al valor registrado en el campo CurrentDeteriorationYear de la cuenta por cobrar al momento de realizar el deterioro.  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'CurrentDeteriorationYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo deteriorado en el año anterior.  Corresponde al valor registrado en el campo DeteriorationBalancePreviousYear de la cuenta por cobrar al momento de realizar el deterioro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'DeteriorationBalancePreviousYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor deteriorado para la factura en lo transcurrido del año.  Corresponde al valor registrado en el campo DeteriorationBalanceCurrentYear de la cuenta por cobrar al momento de realizar el deterioro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'DeteriorationBalanceCurrentYear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El valor del Deterioro anterior. Si es primera vez no hay Deterioro acumulado.   Corresponde al valor registrado en el campo DeteriorationBalance de la cuenta por cobrar al momento de realizar el deterioro.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioProvisionDetail', @level2type = N'COLUMN', @level2name = N'AccumulatedDeterioration';

