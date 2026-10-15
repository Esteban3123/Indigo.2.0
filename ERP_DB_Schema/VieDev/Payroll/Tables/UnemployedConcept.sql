CREATE TABLE [Payroll].[UnemployedConcept] (
    [Id]                         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdUnemployementLiquidation] INT           NOT NULL,
    [IdConcept]                  INT           NOT NULL,
    [AccruedValue]               NUMERIC (18)  NOT NULL,
    [DeductedValue]              NUMERIC (18)  NOT NULL,
    [ConceptFormulate]           VARCHAR (MAX) NULL,
    [ReplaceConceptFormulate]    VARCHAR (MAX) NULL,
    CONSTRAINT [PK_UnemployedConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UnemployedConcept_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_UnemployedConcept_UnemployedLiquidation] FOREIGN KEY ([IdUnemployementLiquidation]) REFERENCES [Payroll].[UnemployedLiquidation] ([Id])
);


GO
ALTER TABLE [Payroll].[UnemployedConcept] NOCHECK CONSTRAINT [FK_UnemployedConcept_Concept];


GO
ALTER TABLE [Payroll].[UnemployedConcept] NOCHECK CONSTRAINT [FK_UnemployedConcept_UnemployedLiquidation];

