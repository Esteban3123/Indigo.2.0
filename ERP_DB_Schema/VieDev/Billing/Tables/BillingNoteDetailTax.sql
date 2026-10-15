CREATE TABLE [Billing].[BillingNoteDetailTax] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BillingNoteDetailId] INT             NOT NULL,
    [TaxPercentage]       NUMERIC (5, 2)  NOT NULL,
    [TaxValue]            NUMERIC (20, 2) NOT NULL,
    [BaseValue]           NUMERIC (20, 2) CONSTRAINT [DF_BillingNoteDetailTax_BaseValue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_BillingNoteDetailTax] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingNoteDetailTax_BillingNoteDetail] FOREIGN KEY ([BillingNoteDetailId]) REFERENCES [Billing].[BillingNoteDetail] ([Id])
);


GO
ALTER TABLE [Billing].[BillingNoteDetailTax] NOCHECK CONSTRAINT [FK_BillingNoteDetailTax_BillingNoteDetail];

