CREATE TABLE [Portfolio].[PortfolioInitialBalanceAccountReceivableShare] (
    [Id]                                         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioInitialBalanceAccountReceivableId] INT          NOT NULL,
    [Number]                                     INT          NOT NULL,
    [ExpiredDate]                                DATETIME     NOT NULL,
    [Value]                                      NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_PortfolioInitialBalanceAccountReceivableShare] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioInitialBalanceAccountReceivableShare_PortfolioInitialBalanceAccountReceivable] FOREIGN KEY ([PortfolioInitialBalanceAccountReceivableId]) REFERENCES [Portfolio].[PortfolioInitialBalanceAccountReceivable] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario de la cuota o instalación. Monto de dinero que corresponde a esta porción del pago dividido. Tipo: NUMERIC(18), sin decimales explícitos.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de vencimiento de la cuota o plazo límite de pago. Cuándo vence esta instalación. Tipo: DATETIME.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de vencimiento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'ExpiredDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'ExpiredDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial de la cuota o instalación. Identificador ordinal de cada porción de pago dentro del plan de cuotas. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Number';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Number';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de Cuenta por Cobrar (Cartera) del saldo inicial. FK que vincula a la factura, crédito o deuda origen que se divide en cuotas. Referencia: PortfolioInitialBalanceAccountReceivable.Id', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'PortfolioInitialBalanceAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id cabecera de la cuenta por pagar del saldo inicial', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'PortfolioInitialBalanceAccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'PortfolioInitialBalanceAccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la cuota o share (porción de pago). Clave primaria de esta línea de instalación. Tipo: INT IDENTITY(1,1).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuota', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cuotas o plazos de la cartera inicial por cobrar: registra el desglose en cuotas de cada saldo inicial de cuentas por cobrar de cartera, con su número de cuota, fecha de vencimiento y valor correspondiente.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioInitialBalanceAccountReceivableShare';
