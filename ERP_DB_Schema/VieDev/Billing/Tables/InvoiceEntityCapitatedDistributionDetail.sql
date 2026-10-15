CREATE TABLE [Billing].[InvoiceEntityCapitatedDistributionDetail] (
    [Id]                                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceEntityCapitatedDistributionId] INT             NOT NULL,
    [InvoiceId]                            INT             NOT NULL,
    [HealthAdministratorId]                INT             NOT NULL,
    [InvoiceCategoryId]                    INT             NULL,
    [InvoiceNumber]                        VARCHAR (20)    NOT NULL,
    [InvoiceDate]                          DATETIME        NOT NULL,
    [InvoiceValue]                         NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_InvoiceEntityCapitatedDistributionDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_HealthAdministrator] FOREIGN KEY ([HealthAdministratorId]) REFERENCES [Contract].[HealthAdministrator] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_InvoiceCategories] FOREIGN KEY ([InvoiceCategoryId]) REFERENCES [Billing].[InvoiceCategories] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_InvoiceEntityCapitatedDistribution] FOREIGN KEY ([InvoiceEntityCapitatedDistributionId]) REFERENCES [Billing].[InvoiceEntityCapitatedDistribution] ([Id])
);


GO
ALTER TABLE [Billing].[InvoiceEntityCapitatedDistributionDetail] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_HealthAdministrator];


GO
ALTER TABLE [Billing].[InvoiceEntityCapitatedDistributionDetail] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_Invoice];


GO
ALTER TABLE [Billing].[InvoiceEntityCapitatedDistributionDetail] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_InvoiceCategories];


GO
ALTER TABLE [Billing].[InvoiceEntityCapitatedDistributionDetail] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitatedDistributionDetail_InvoiceEntityCapitatedDistribution];

