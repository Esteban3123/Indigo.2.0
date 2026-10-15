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

