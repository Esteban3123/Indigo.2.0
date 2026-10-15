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

