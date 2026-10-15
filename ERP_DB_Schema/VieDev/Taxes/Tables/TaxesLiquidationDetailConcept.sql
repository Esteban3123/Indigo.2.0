CREATE TABLE [Taxes].[TaxesLiquidationDetailConcept] (
    [Id]                        INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TaxesLiquidationDetailId]  INT            NOT NULL,
    [TaxesLiquidationConceptId] INT            NOT NULL,
    [PercentageConcept]         NUMERIC (5, 2) NOT NULL,
    [BaseValue]                 NUMERIC (18)   NOT NULL,
    [Value]                     NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_TaxesLiquidationDetailConcept__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesLiquidationDetailConcept_TaxesLiquidationConcept] FOREIGN KEY ([TaxesLiquidationConceptId]) REFERENCES [Taxes].[TaxesLiquidationConcept] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationDetailConcept_TaxesLiquidationDetail] FOREIGN KEY ([TaxesLiquidationDetailId]) REFERENCES [Taxes].[TaxesLiquidationDetail] ([Id])
);


GO
ALTER TABLE [Taxes].[TaxesLiquidationDetailConcept] NOCHECK CONSTRAINT [FK_TaxesLiquidationDetailConcept_TaxesLiquidationConcept];


GO
ALTER TABLE [Taxes].[TaxesLiquidationDetailConcept] NOCHECK CONSTRAINT [FK_TaxesLiquidationDetailConcept_TaxesLiquidationDetail];

