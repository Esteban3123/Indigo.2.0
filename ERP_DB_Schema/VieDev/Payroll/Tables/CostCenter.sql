CREATE TABLE [Payroll].[CostCenter] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (200) NOT NULL,
    [State]            BIT           NOT NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [BranchOfficeId]   INT           NULL,
    CONSTRAINT [PK_CostCenter_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostCenter_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id])
);


GO
ALTER TABLE [Payroll].[CostCenter] NOCHECK CONSTRAINT [FK_CostCenter_BranchOffice];

