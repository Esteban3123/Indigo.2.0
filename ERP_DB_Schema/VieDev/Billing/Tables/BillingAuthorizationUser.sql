CREATE TABLE [Billing].[BillingAuthorizationUser] (
    [Id]                     INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BillingAuthorizationId] INT          NOT NULL,
    [UserId]                 INT          NOT NULL,
    [UserCode]               VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_BillingAuthorizationUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingAuthorizationUser_BillingAuthorization] FOREIGN KEY ([BillingAuthorizationId]) REFERENCES [Billing].[BillingAuthorization] ([Id])
);


GO
ALTER TABLE [Billing].[BillingAuthorizationUser] NOCHECK CONSTRAINT [FK_BillingAuthorizationUser_BillingAuthorization];

