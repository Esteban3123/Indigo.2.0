CREATE TABLE [Billing].[BillingDeadline] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]  INT          NOT NULL,
    [PeriodNumber] INT          NOT NULL,
    [InitialDate]  DATE         NOT NULL,
    [EndDate]      DATE         NOT NULL,
    [Value]        NUMERIC (18) NOT NULL,
    [TimeStamp]    ROWVERSION   NOT NULL,
    CONSTRAINT [PK_BillingDeadline] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingDeadline_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id])
);


GO
ALTER TABLE [Billing].[BillingDeadline] NOCHECK CONSTRAINT [FK_BillingDeadline_CareGroup];

