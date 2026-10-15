CREATE TABLE [Contract].[ContractAccountingStructure] (
    [Id]                                           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                                         VARCHAR (20)  NOT NULL,
    [Name]                                         VARCHAR (100) CONSTRAINT [DF_ContractAccountingStructure_Name] DEFAULT ('') NOT NULL,
    [CareGroupType]                                TINYINT       NOT NULL,
    [AccountRecoveryFeeId]                         INT           NULL,
    [AccountParticularId]                          INT           NULL,
    [AccountWithoutRadicateId]                     INT           CONSTRAINT [DF_ContractAccountingStructure_AccountWithoutRadicateId] DEFAULT ((203)) NULL,
    [AccountRadicateId]                            INT           CONSTRAINT [DF_ContractAccountingStructure_AccountRadicateId] DEFAULT ((203)) NULL,
    [AccountObjectionRemediedId]                   INT           CONSTRAINT [DF_ContractAccountingStructure_AccountObjectionRemediedId] DEFAULT ((203)) NULL,
    [AccountConciliationId]                        INT           NULL,
    [AccountLegalCollectionId]                     INT           NULL,
    [AccountHardCollectionId]                      INT           NULL,
    [AccountDebitOrderId]                          INT           NULL,
    [AccountCreditOrderId]                         INT           NULL,
    [DebitAccountDeteriorationId]                  INT           NOT NULL,
    [CreditAccountDeteriorationId]                 INT           NOT NULL,
    [ReversalAccountDeteriorationId]               INT           NOT NULL,
    [PreviousPeriodReversalAccountDeteriorationId] INT           NOT NULL,
    [CreditProvisionAccountId]                     INT           NOT NULL,
    [DebitProvisionAccountId]                      INT           NOT NULL,
    [Status]                                       BIT           CONSTRAINT [DF_ContractAccountingStructure_Status] DEFAULT ((1)) NOT NULL,
    [CreationUser]                                 VARCHAR (20)  NOT NULL,
    [CreationDate]                                 DATETIME      NOT NULL,
    [ModificationUser]                             VARCHAR (20)  NULL,
    [ModificationDate]                             DATETIME      NULL,
    [TimeStamp]                                    ROWVERSION    NOT NULL,
    [ServicesPendingBillingMainAccountId]          INT           CONSTRAINT [DF_ContractAccountingStructure_ServicesPendingBillingMainAccountId] DEFAULT ((3869)) NOT NULL,
    CONSTRAINT [PK_ContractAccountingStructure__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts] FOREIGN KEY ([AccountRecoveryFeeId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts1] FOREIGN KEY ([AccountParticularId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts10] FOREIGN KEY ([DebitAccountDeteriorationId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts11] FOREIGN KEY ([CreditAccountDeteriorationId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts12] FOREIGN KEY ([ReversalAccountDeteriorationId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts13] FOREIGN KEY ([PreviousPeriodReversalAccountDeteriorationId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts14] FOREIGN KEY ([CreditProvisionAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts15] FOREIGN KEY ([DebitProvisionAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts16] FOREIGN KEY ([ServicesPendingBillingMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts2] FOREIGN KEY ([AccountWithoutRadicateId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts3] FOREIGN KEY ([AccountRadicateId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts4] FOREIGN KEY ([AccountObjectionRemediedId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts5] FOREIGN KEY ([AccountConciliationId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts6] FOREIGN KEY ([AccountLegalCollectionId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts7] FOREIGN KEY ([AccountHardCollectionId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts8] FOREIGN KEY ([AccountDebitOrderId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_ContractAccountingStructure_MainAccounts9] FOREIGN KEY ([AccountCreditOrderId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts1];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts10];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts11];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts12];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts13];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts14];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts15];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts16];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts2];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts3];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts4];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts5];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts6];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts7];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts8];


GO
ALTER TABLE [Contract].[ContractAccountingStructure] NOCHECK CONSTRAINT [FK_ContractAccountingStructure_MainAccounts9];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del grupo de atencion  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable radicada  Se coloca el campo null porque en el form de grupos de atención cuando el tipo de grupo de atencion es particular no pide esta cuenta', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable sin Radicar  Se coloca el campo null porque en el form de grupos de atención cuando el tipo de grupo de atencion es particular no pide esta cuenta', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de Grupo de atencion  1 - EAPB Con contrato  2 - EAPB Sin Contrato  3 - Particulares  4 - Aseguradoras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CareGroupType';

