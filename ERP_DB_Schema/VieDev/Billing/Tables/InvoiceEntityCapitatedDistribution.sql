CREATE TABLE [Billing].[InvoiceEntityCapitatedDistribution] (
    [Id]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                     VARCHAR (20)  NOT NULL,
    [OperatingUnitId]          INT           NOT NULL,
    [DocumentDate]             DATETIME      NOT NULL,
    [InvoiceEntityCapitatedId] INT           NOT NULL,
    [InvoiceValue]             NUMERIC (18)  NOT NULL,
    [TotalControlValue]        NUMERIC (18)  NOT NULL,
    [Observation]              VARCHAR (500) NOT NULL,
    [Status]                   TINYINT       NOT NULL,
    [CreationUser]             VARCHAR (20)  NOT NULL,
    [CreationDate]             DATETIME      NOT NULL,
    [ModificationUser]         VARCHAR (20)  NULL,
    [ModificationDate]         DATETIME      NULL,
    [ConfirmationUser]         VARCHAR (20)  NULL,
    [ConfirmationDate]         DATETIME      NULL,
    [AnnulmentUser]            VARCHAR (20)  NULL,
    [AnnulmentDate]            DATETIME      NULL,
    [TimeStamp]                ROWVERSION    NOT NULL,
    CONSTRAINT [PK_InvoiceEntityCapitatedDistribution] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InvoiceEntityCapitatedDistribution_InvoiceEntityCapitated] FOREIGN KEY ([InvoiceEntityCapitatedId]) REFERENCES [Billing].[InvoiceEntityCapitated] ([Id]),
    CONSTRAINT [FK_InvoiceEntityCapitatedDistribution_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Billing].[InvoiceEntityCapitatedDistribution] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitatedDistribution_InvoiceEntityCapitated];


GO
ALTER TABLE [Billing].[InvoiceEntityCapitatedDistribution] NOCHECK CONSTRAINT [FK_InvoiceEntityCapitatedDistribution_OperatingUnit];

