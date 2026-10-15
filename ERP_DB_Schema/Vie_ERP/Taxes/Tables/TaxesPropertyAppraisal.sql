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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de creacion', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Avaluo', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'Appraisal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la vigencia en que se modifico el avaluo', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'Validity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del predio', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'TaxesPropertyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesPropertyAppraisal', @level2type = N'COLUMN', @level2name = N'Id';

