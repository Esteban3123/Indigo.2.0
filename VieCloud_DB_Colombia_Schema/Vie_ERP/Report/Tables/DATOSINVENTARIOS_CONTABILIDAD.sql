CREATE TABLE [Report].[DATOSINVENTARIOS_CONTABILIDAD] (
    [ID]          INT             NOT NULL,
    [Consecutive] BIGINT          NOT NULL,
    [EntityCode]  VARCHAR (20)    NULL,
    [EntityName]  VARCHAR (250)   NULL,
    [VoucherDate] DATETIME        NOT NULL,
    [number]      VARCHAR (50)    NULL,
    [DebitValue]  NUMERIC (18, 2) NULL,
    [CreditValue] NUMERIC (18, 2) NULL
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla de reporte que almacena datos contables de inventarios, asociando entidades (código y nombre) con comprobantes contables identificados por fecha y número. Registra los valores de débito y crédito correspondientes a movimientos de inventario para uso en reporting contable o integración con módulos de contabilidad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'DATOSINVENTARIOS_CONTABILIDAD';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'TABLE', @level1name=N'DATOSINVENTARIOS_CONTABILIDAD';
GO
