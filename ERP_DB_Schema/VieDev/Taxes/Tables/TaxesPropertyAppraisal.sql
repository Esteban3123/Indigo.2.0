CREATE TABLE [Taxes].[TaxesPropertyAppraisal] (
    [Id]              INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TaxesPropertyId] INT          NOT NULL,
    [Validity]        INT          NOT NULL,
    [Appraisal]       NUMERIC (18) NOT NULL,
    [CreationDate]    DATETIME     NOT NULL,
    CONSTRAINT [PK_TaxesPropertyAppraisal] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesPropertyAppraisal_TaxesProperty] FOREIGN KEY ([TaxesPropertyId]) REFERENCES [Taxes].[TaxesProperty] ([Id])
);


GO
ALTER TABLE [Taxes].[TaxesPropertyAppraisal] NOCHECK CONSTRAINT [FK_TaxesPropertyAppraisal_TaxesProperty];

