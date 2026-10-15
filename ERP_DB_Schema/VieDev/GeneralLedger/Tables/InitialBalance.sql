CREATE TABLE [GeneralLedger].[InitialBalance] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LegalBookCode]        VARCHAR (50)    NOT NULL,
    [MainAccountNumber]    VARCHAR (50)    NOT NULL,
    [ThirdPartyNit]        VARCHAR (20)    NULL,
    [CostCenterCode]       VARCHAR (20)    NULL,
    [Nature]               TINYINT         NOT NULL,
    [Detail]               VARCHAR (MAX)   NULL,
    [Value]                DECIMAL (18, 2) NOT NULL,
    [RetentionConceptCode] VARCHAR (20)    NULL,
    [BillingValue]         DECIMAL (18, 2) NULL,
    [BaseValue]            DECIMAL (18, 2) NULL,
    CONSTRAINT [PK__InitialB__3214EC07DCBBFCD2] PRIMARY KEY CLUSTERED ([Id] ASC)
);

