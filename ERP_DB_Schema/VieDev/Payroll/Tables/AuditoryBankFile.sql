CREATE TABLE [Payroll].[AuditoryBankFile] (
    [Id]           INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BankId]       INT      NOT NULL,
    [GroupId]      INT      NOT NULL,
    [PayrollDate]  DATE     NOT NULL,
    [UserId]       INT      NOT NULL,
    [CreationDate] DATETIME CONSTRAINT [DF_AuditoryBankFile_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_AuditoryBankFile] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuditoryBankFile_Bank] FOREIGN KEY ([BankId]) REFERENCES [Payroll].[Bank] ([Id]),
    CONSTRAINT [FK_AuditoryBankFile_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id])
);


GO
ALTER TABLE [Payroll].[AuditoryBankFile] NOCHECK CONSTRAINT [FK_AuditoryBankFile_Bank];


GO
ALTER TABLE [Payroll].[AuditoryBankFile] NOCHECK CONSTRAINT [FK_AuditoryBankFile_Group];

