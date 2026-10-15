CREATE TABLE [Billing].[RevenueControlCourtAccount] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RevenueControlId] INT          NOT NULL,
    [CourtDate]        DATE         NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    CONSTRAINT [PK_RevenueControlCourtAccount] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RevenueControlCourtAccount_RevenueControl] FOREIGN KEY ([RevenueControlId]) REFERENCES [Billing].[RevenueControl] ([Id])
);


GO
ALTER TABLE [Billing].[RevenueControlCourtAccount] NOCHECK CONSTRAINT [FK_RevenueControlCourtAccount_RevenueControl];

