CREATE TABLE [dbo].[ImportJournalVoucherDetail] (
    [MainAccountNumber] VARCHAR (20)    NOT NULL,
    [ThirdPartyNit]     VARCHAR (20)    NULL,
    [CostCenterCode]    VARCHAR (20)    NULL,
    [Nature]            TINYINT         NOT NULL,
    [Observation]       VARCHAR (500)   NULL,
    [Value]             DECIMAL (18, 2) NOT NULL,
    [Retention]         VARCHAR (20)    NULL,
    [BillingValue]      DECIMAL (18, 2) NULL,
    [BaseValue]         DECIMAL (18, 2) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de detalle utilizada para la importación de comprobantes contables (journal vouchers). Cada registro representa una línea de movimiento con cuenta contable principal, tercero (NIT), centro de costos, naturaleza débito/crédito, valor del movimiento y valores de facturación y base para retenciones. Actúa como estructura temporal o de staging para cargar asientos contables desde fuentes externas al sistema financiero.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ImportJournalVoucherDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ImportJournalVoucherDetail';
GO
