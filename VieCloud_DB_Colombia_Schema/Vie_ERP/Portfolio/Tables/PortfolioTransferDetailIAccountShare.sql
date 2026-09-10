CREATE TABLE [Portfolio].[PortfolioTransferDetailIAccountShare] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PortfolioTransferDetailId] INT             NOT NULL,
    [AccountReceivableId]       INT             NOT NULL,
    [AccountReceivableShareId]  INT             NOT NULL,
    [Value]                     NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_PortfolioTransferDetailIAccountShare] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortfolioTransferDetailIAccountShare_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_PortfolioTransferDetailIAccountShare_AccountReceivableShare] FOREIGN KEY ([AccountReceivableShareId]) REFERENCES [Portfolio].[AccountReceivableShare] ([Id]),
    CONSTRAINT [FK_PortfolioTransferDetailIAccountShare_PortfolioTransferDetail] FOREIGN KEY ([PortfolioTransferDetailId]) REFERENCES [Portfolio].[PortfolioTransferDetail] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor ajustado de la cuota o participación en la cuenta por cobrar, expresado en unidades monetarias (NUMERIC 18,2). Representa el monto específico transferido o asignado en la operación de traslado de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor ajustado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la participación o cuota en la cuenta por cobrar compartida. Referencia a la tabla AccountReceivableShare que define la proporción o porcentaje de participación en la deuda o factura.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableShareId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta por cobrar compartida.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableShareId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableShareId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta por cobrar (factura, documento de ingreso, reclamación, glosa o RIPS). Referencia a la tabla AccountReceivable que contiene el detalle de la deuda, valor total y estado de pago.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle o línea del traslado de cartera. Referencia a PortfolioTransferDetail que especifica qué parte del portafolio se transfiere, a quién y en qué fecha.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'PortfolioTransferDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del traslado', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'PortfolioTransferDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'PortfolioTransferDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la tabla (clave primaria). INT IDENTITY, autoincrementable. Distingue cada asignación de valor de cuota en el traslado de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la distribución de cuentas por cobrar y sus cuotas o participaciones asignadas dentro del detalle de una transferencia de cartera, permitiendo rastrear qué parte de cada cuenta fue cedida o reasignada y por qué valor.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'PortfolioTransferDetailIAccountShare';
