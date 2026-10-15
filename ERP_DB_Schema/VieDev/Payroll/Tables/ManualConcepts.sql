CREATE TABLE [Payroll].[ManualConcepts] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Process]             TINYINT         CONSTRAINT [DF_ManualConcepts_Process] DEFAULT ((1)) NOT NULL,
    [Consecutive]         INT             NOT NULL,
    [GroupId]             INT             NOT NULL,
    [EmployeeId]          INT             NOT NULL,
    [ContractNumber]      INT             NOT NULL,
    [ContractId]          INT             NOT NULL,
    [CostCenterId]        INT             NOT NULL,
    [BranchOfficeId]      INT             NOT NULL,
    [FunctionalUnitId]    INT             NOT NULL,
    [InitialDate]         DATE            NOT NULL,
    [PayrollInitialDate]  DATE            NOT NULL,
    [PayrollEndingDate]   DATE            NOT NULL,
    [ConceptId]           INT             NOT NULL,
    [Description]         VARCHAR (200)   NOT NULL,
    [PaidEndContract]     BIT             NOT NULL,
    [PaidFormat]          TINYINT         NOT NULL,
    [QuoteNumber]         TINYINT         NOT NULL,
    [QuoteValue]          DECIMAL (18, 2) NULL,
    [State]               TINYINT         NOT NULL,
    [RetentionId]         INT             NULL,
    [RetentionPercentage] DECIMAL (6, 3)  NULL,
    [RetentionBase]       DECIMAL (18)    NULL,
    CONSTRAINT [PK_ManualConcepts__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ManualConcepts_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_ManualConcepts_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_ManualConcepts_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_ManualConcepts_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_ManualConcepts_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_ManualConcepts_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_ManualConcepts_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_ManualConcepts_RetentionConcept] FOREIGN KEY ([RetentionId]) REFERENCES [GeneralLedger].[RetentionConcepts] ([Id])
);


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_BranchOffice];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_Concept];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_Contract];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_CostCenter];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_Employee];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_FunctionalUnit];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_Group];


GO
ALTER TABLE [Payroll].[ManualConcepts] NOCHECK CONSTRAINT [FK_ManualConcepts_RetentionConcept];

