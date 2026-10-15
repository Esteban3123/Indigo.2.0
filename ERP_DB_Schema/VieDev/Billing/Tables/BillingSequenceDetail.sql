CREATE TABLE [Billing].[BillingSequenceDetail] (
    [Id]                 INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseBillingC] INT    NOT NULL,
    [IdSequense]         INT    NOT NULL,
    [IdOperatingUnit]    INT    NULL,
    [Next]               BIGINT CONSTRAINT [DF_SequenseBillingD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseBillingD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseBillingD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseBillingD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseBillingD_SequenseBillingC] FOREIGN KEY ([IdSequenseBillingC]) REFERENCES [Billing].[BillingSequence] ([Id])
);


GO
ALTER TABLE [Billing].[BillingSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBillingD_OperatingUnit];


GO
ALTER TABLE [Billing].[BillingSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBillingD_Sequense];


GO
ALTER TABLE [Billing].[BillingSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseBillingD_SequenseBillingC];

