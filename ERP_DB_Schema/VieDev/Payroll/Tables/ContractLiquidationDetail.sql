CREATE TABLE [Payroll].[ContractLiquidationDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractLiquidationId]   INT             NOT NULL,
    [Description]             VARCHAR (500)   NOT NULL,
    [Accrued]                 DECIMAL (18, 2) NOT NULL,
    [Deducted]                DECIMAL (18, 2) CONSTRAINT [DF_ContractLiquidationDetail_Deducted] DEFAULT ((0)) NOT NULL,
    [InitialDate]             DATE            NOT NULL,
    [EndingDate]              DATE            NOT NULL,
    [ConceptFormulate]        VARCHAR (MAX)   NULL,
    [ReplaceConceptFormulate] VARCHAR (MAX)   NULL,
    [IdConcept]               INT             NULL,
    [ConceptType]             TINYINT         NULL,
    [RetentionId]             INT             NULL,
    [RetentionPercentage]     DECIMAL (6, 3)  NULL,
    [RetentionBase]           DECIMAL (18)    NULL,
    CONSTRAINT [PK_ContractLiquidationDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractLiquidationDetail_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ContractLiquidationDetail_ContractLiquidation] FOREIGN KEY ([ContractLiquidationId]) REFERENCES [Payroll].[ContractLiquidation] ([Id]),
    CONSTRAINT [FK_ContractLiquidationDetail_RetentionConcept] FOREIGN KEY ([RetentionId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [Payroll].[ContractLiquidationDetail] NOCHECK CONSTRAINT [FK_ContractLiquidationDetail_Concept];


GO
ALTER TABLE [Payroll].[ContractLiquidationDetail] NOCHECK CONSTRAINT [FK_ContractLiquidationDetail_ContractLiquidation];


GO
ALTER TABLE [Payroll].[ContractLiquidationDetail] NOCHECK CONSTRAINT [FK_ContractLiquidationDetail_RetentionConcept];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de concepto', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'ContractLiquidationDetail', @level2type = N'COLUMN', @level2name = N'ConceptType';

