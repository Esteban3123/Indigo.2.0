CREATE TABLE [Payroll].[IncentivePaymentDetail] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IncentivePaymentId]      INT           NOT NULL,
    [ConceptId]               INT           NOT NULL,
    [AccruedValue]            NUMERIC (18)  NOT NULL,
    [DeductedValue]           NUMERIC (18)  NOT NULL,
    [ConceptFormulate]        VARCHAR (MAX) NULL,
    [ReplaceConceptFormulate] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_IncentivePaymentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_IncentivePaymentDetail_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_IncentivePaymentDetail_IncentivePayment] FOREIGN KEY ([IncentivePaymentId]) REFERENCES [Payroll].[IncentivePayment] ([Id])
);


GO
ALTER TABLE [Payroll].[IncentivePaymentDetail] NOCHECK CONSTRAINT [FK_IncentivePaymentDetail_Concept];


GO
ALTER TABLE [Payroll].[IncentivePaymentDetail] NOCHECK CONSTRAINT [FK_IncentivePaymentDetail_IncentivePayment];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reemplazar Concepto Formular', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'ReplaceConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto Formular', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'ConceptFormulate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Deducido', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'DeductedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor Devengado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'AccruedValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'ConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Cabecera', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'IncentivePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'IncentivePaymentDetail', @level2type = N'COLUMN', @level2name = N'Id';

