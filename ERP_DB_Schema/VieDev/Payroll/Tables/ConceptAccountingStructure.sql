CREATE TABLE [Payroll].[ConceptAccountingStructure] (
    [Id]                                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConceptId]                          INT          NULL,
    [AccountingStructureId]              INT          NULL,
    [AccruedAccount]                     VARCHAR (20) NULL,
    [DeductedAccount]                    VARCHAR (20) NULL,
    [InterfazName]                       VARCHAR (50) NULL,
    [CreationDate]                       DATETIME     NULL,
    [ModifiedDate]                       DATETIME     NULL,
    [CreationUserId]                     INT          NULL,
    [ModificationUserId]                 INT          NULL,
    [InabilityDebitValueEmployeeAccount] VARCHAR (20) NULL,
    [InabilityDebitValueEPSAccount]      VARCHAR (20) NULL,
    CONSTRAINT [PK_ConceptAccountingStructure] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ConceptAccountingStructure_AccountingStructure] FOREIGN KEY ([AccountingStructureId]) REFERENCES [Payroll].[AccountingStructure] ([Id]),
    CONSTRAINT [FK_ConceptAccountingStructure_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id])
);


GO
ALTER TABLE [Payroll].[ConceptAccountingStructure] NOCHECK CONSTRAINT [FK_ConceptAccountingStructure_AccountingStructure];


GO
ALTER TABLE [Payroll].[ConceptAccountingStructure] NOCHECK CONSTRAINT [FK_ConceptAccountingStructure_Concept];

