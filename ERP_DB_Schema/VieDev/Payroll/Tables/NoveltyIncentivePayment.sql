CREATE TABLE [Payroll].[NoveltyIncentivePayment] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EmployeeId]            INT          NOT NULL,
    [ContractId]            INT          NOT NULL,
    [InitialContractNumber] INT          NOT NULL,
    [ConceptId]             INT          NOT NULL,
    [Value]                 NUMERIC (18) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [ModificationDate]      DATETIME     NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [Status]                TINYINT      NULL,
    CONSTRAINT [PK_NoveltyIncentivePayment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NoveltyIncentivePayment_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_NoveltyIncentivePayment_Contract] FOREIGN KEY ([ContractId]) REFERENCES [Payroll].[Contract] ([Id]),
    CONSTRAINT [FK_NoveltyIncentivePayment_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id])
);


GO
ALTER TABLE [Payroll].[NoveltyIncentivePayment] NOCHECK CONSTRAINT [FK_NoveltyIncentivePayment_Concept];


GO
ALTER TABLE [Payroll].[NoveltyIncentivePayment] NOCHECK CONSTRAINT [FK_NoveltyIncentivePayment_Contract];


GO
ALTER TABLE [Payroll].[NoveltyIncentivePayment] NOCHECK CONSTRAINT [FK_NoveltyIncentivePayment_Employee];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estados  1. Activo  2. Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'NoveltyIncentivePayment', @level2type = N'COLUMN', @level2name = N'Status';

