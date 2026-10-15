CREATE TABLE [Payroll].[KindsAgreements] (
    [Id]                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                         VARCHAR (20)  NOT NULL,
    [Description]                  VARCHAR (150) NOT NULL,
    [State]                        BIT           NOT NULL,
    [TimeStamp]                    ROWVERSION    NOT NULL,
    [CreationUser]                 VARCHAR (20)  NOT NULL,
    [CreationDate]                 DATETIME      NOT NULL,
    [ModificationUser]             VARCHAR (20)  NULL,
    [ModificationDate]             DATETIME      NULL,
    [IdExpenseConcepts]            INT           NULL,
    [AffectsAccountsReceivable]    BIT           CONSTRAINT [DF__KindsAgre__Affec__33EE883B] DEFAULT ((0)) NOT NULL,
    [ReclassifyAccountsReceivable] BIT           CONSTRAINT [DF__KindsAgre__Recla__34E2AC74] DEFAULT ((0)) NOT NULL,
    [AccountReceivableConceptId]   INT           NULL,
    [AccountId]                    INT           NULL,
    CONSTRAINT [PK_KindsAgreements] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_KindsAgreements_AccountReceivableConcept] FOREIGN KEY ([AccountReceivableConceptId]) REFERENCES [Portfolio].[AccountReceivableConcept] ([Id]),
    CONSTRAINT [FK_KindsAgreements_ExpenseConcepts] FOREIGN KEY ([IdExpenseConcepts]) REFERENCES [Treasury].[ExpenseConcepts] ([Id]),
    CONSTRAINT [FK_KindsAgreements_MainAccounts] FOREIGN KEY ([AccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Payroll].[KindsAgreements] NOCHECK CONSTRAINT [FK_KindsAgreements_AccountReceivableConcept];


GO
ALTER TABLE [Payroll].[KindsAgreements] NOCHECK CONSTRAINT [FK_KindsAgreements_ExpenseConcepts];


GO
ALTER TABLE [Payroll].[KindsAgreements] NOCHECK CONSTRAINT [FK_KindsAgreements_MainAccounts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo indica si el convenio afecta o no una cuenta por cobrar:  0 - No afecta  1 - Afecta', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'KindsAgreements', @level2type = N'COLUMN', @level2name = N'AffectsAccountsReceivable';

