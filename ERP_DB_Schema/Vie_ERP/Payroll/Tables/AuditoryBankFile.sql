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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creacion del Registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuditoryBankFile', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Usuario', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuditoryBankFile', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Nómina', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuditoryBankFile', @level2type = N'COLUMN', @level2name = N'PayrollDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id del Grupo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuditoryBankFile', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FK Id del Banco', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuditoryBankFile', @level2type = N'COLUMN', @level2name = N'BankId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Auditoria Archivo Plano Bancos', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AuditoryBankFile', @level2type = N'COLUMN', @level2name = N'Id';

