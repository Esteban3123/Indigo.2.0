CREATE TABLE [Payroll].[AgreementsC] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Consecutive]                   INT             NOT NULL,
    [GroupId]                       INT             NULL,
    [EmployeeId]                    INT             NOT NULL,
    [CompanyId]                     INT             NOT NULL,
    [ConceptId]                     INT             NOT NULL,
    [KindsAgreementsId]             INT             NOT NULL,
    [Comments]                      VARCHAR (250)   NULL,
    [LiquidationType]               TINYINT         NOT NULL,
    [TermType]                      TINYINT         NOT NULL,
    [AgreementValue]                DECIMAL (18, 2) NOT NULL,
    [NumberShares]                  INT             NOT NULL,
    [State]                         VARCHAR (1)     NOT NULL,
    [StartingDate]                  DATETIME        NOT NULL,
    [CurrentBalance]                DECIMAL (18, 2) NULL,
    [EndDateSuspend]                DATETIME        NULL,
    [CommentChangeState]            VARCHAR (250)   NULL,
    [TimeStamp]                     ROWVERSION      NOT NULL,
    [PaidVacation]                  BIT             CONSTRAINT [DF_AgreementsC_PaidVacation] DEFAULT ((0)) NULL,
    [Transfertype]                  TINYINT         NULL,
    [AccountReceivableAccountingId] INT             NULL,
    CONSTRAINT [PK_AgreementsC__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Agreements_AccountReceivableAccounting] FOREIGN KEY ([AccountReceivableAccountingId]) REFERENCES [Portfolio].[AccountReceivableAccounting] ([Id]),
    CONSTRAINT [FK_Agreements_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Payroll].[Company] ([Id]),
    CONSTRAINT [FK_Agreements_Concept] FOREIGN KEY ([ConceptId]) REFERENCES [Payroll].[Concept] ([Id]),
    CONSTRAINT [FK_Agreements_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Payroll].[Employee] ([Id]),
    CONSTRAINT [FK_Agreements_Group] FOREIGN KEY ([GroupId]) REFERENCES [Payroll].[Group] ([Id]),
    CONSTRAINT [FK_AgreementsC_KindsAgreements] FOREIGN KEY ([KindsAgreementsId]) REFERENCES [Payroll].[KindsAgreements] ([Id]),
    CONSTRAINT [UQ_AgreementsC_Consecutive] UNIQUE NONCLUSTERED ([Consecutive] ASC)
);


GO
ALTER TABLE [Payroll].[AgreementsC] NOCHECK CONSTRAINT [FK_Agreements_AccountReceivableAccounting];


GO
ALTER TABLE [Payroll].[AgreementsC] NOCHECK CONSTRAINT [FK_Agreements_Company];


GO
ALTER TABLE [Payroll].[AgreementsC] NOCHECK CONSTRAINT [FK_Agreements_Concept];


GO
ALTER TABLE [Payroll].[AgreementsC] NOCHECK CONSTRAINT [FK_Agreements_Employee];


GO
ALTER TABLE [Payroll].[AgreementsC] NOCHECK CONSTRAINT [FK_Agreements_Group];


GO
ALTER TABLE [Payroll].[AgreementsC] NOCHECK CONSTRAINT [FK_AgreementsC_KindsAgreements];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando la clase de convenio es afecta cuentas por cobrar se solicita el tipo de traslado:  1 - Mismo Tercero  2 - Diferente Tercero', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'AgreementsC', @level2type = N'COLUMN', @level2name = N'Transfertype';

