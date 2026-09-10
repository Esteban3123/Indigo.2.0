CREATE TABLE [Portfolio].[AccountReceivablePromptPayment] (
    [Id]                  INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountReceivableId] INT            NOT NULL,
    [DeadLine]            DATE           NOT NULL,
    [PercentageDiscount]  NUMERIC (5, 2) NOT NULL,
    [MainAccountId]       INT            NOT NULL,
    CONSTRAINT [PK_AccountReceivablePromptPayment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivablePromptPayment_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_AccountReceivablePromptPayment_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cuenta contable principal (GeneralLedger.MainAccounts) donde se registra el descuento. Impacto presupuestario.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cuenta contable del descuento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento (NUMERIC 5,2) aplicable a la cuenta por cobrar si se paga antes de la fecha límite. Bonificación financiera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'PercentageDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de Descuento', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'PercentageDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'PercentageDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha límite (DATE) hasta la cual es válido el descuento por pago anticipado o pronto pago. Vencimiento de la promoción.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'DeadLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fecha limite', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'DeadLine';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'DeadLine';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT, FK) de la cuenta por cobrar, factura o deuda a la que aplica el descuento por pronto pago. Referencia a Portfolio.AccountReceivable.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (INT IDENTITY) de la tabla de descuentos por pago pronto. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de condiciones de pronto pago asociadas a cuentas por cobrar: descuentos porcentuales que se aplican si el deudor paga antes de una fecha límite establecida. Usado en la gestión de cartera para incentivar el pago anticipado de facturas o cuentas.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'AccountReceivablePromptPayment';
