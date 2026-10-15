CREATE TABLE [Payroll].[VacationDetail] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdVacation]              INT           NOT NULL,
    [IdConcept]               INT           NULL,
    [Description]             VARCHAR (500) NOT NULL,
    [Accrued]                 NUMERIC (18)  NOT NULL,
    [Deducted]                NUMERIC (18)  NOT NULL,
    [ConceptFormulate]        VARCHAR (MAX) NULL,
    [ReplaceConceptFormulate] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_VacationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VacationDetail_Concept] FOREIGN KEY ([IdConcept]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_VacationDetail_Vacation] FOREIGN KEY ([IdVacation]) REFERENCES [Payroll].[Vacation] ([Id])
);


GO
ALTER TABLE [Payroll].[VacationDetail] NOCHECK CONSTRAINT [FK_VacationDetail_Concept];


GO
ALTER TABLE [Payroll].[VacationDetail] NOCHECK CONSTRAINT [FK_VacationDetail_Vacation];

