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



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_ContractAccountingStructure__Code]
    ON [Contract].[ContractAccountingStructure]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MainAccounts para servicios pendientes de facturación bajo norma NIIF; default 3869; usado en provisión de ingresos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'ServicesPendingBillingMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta Niif para Servicios Pendientes por Facturar', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'ServicesPendingBillingMainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'ServicesPendingBillingMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP automático generado por SQL Server en cada insert/update; control de concurrencia y auditoría de eventos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME nullable de última modificación; NULL si no ha sido modificada desde creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20) usuario que modificó últimamente la estructura contable; auditoría de cambios', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME de creación de la estructura contable; referencia temporal de vigencia inicial', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(20) usuario que creó la estructura contable; auditoría de origen', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT 1=Activo (estructura contable vigente), 0=Inactivo (no se usa en nuevas atenciones/facturas); default 1', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del grupo de atencion  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT NOT NULL a MainAccounts para provisión por débito, contrapartida de reserva', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'DebitProvisionAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable para provisión débito', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'DebitProvisionAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'DebitProvisionAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT NOT NULL a MainAccounts para provisión por crédito, reserva contable de incobrables', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreditProvisionAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta coontable para provisión crédito', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreditProvisionAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreditProvisionAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT NOT NULL a MainAccounts para reversión de deterioro de períodos anteriores', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de deterioro de cuenta de reversión de período anterior', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccountDeteriorationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT NOT NULL a MainAccounts para reversión de deterioro en período actual', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'ReversalAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de deterioro de cuenta de reversión', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'ReversalAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'ReversalAccountDeteriorationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT NOT NULL a MainAccounts para crédito por deterioro, contrapartida de ajuste de cartera', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreditAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de deterioro de la cuenta de crédito', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreditAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CreditAccountDeteriorationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT NOT NULL a MainAccounts para débito por deterioro, ajuste contable de cartera vencida', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'DebitAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de deterioro de la cuenta de débito', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'DebitAccountDeteriorationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'DebitAccountDeteriorationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MainAccounts para órdenes de crédito de glosas (acreedores); solo sector público', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountCreditOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta de orden de acreedores glosas, Solo para el sector publico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountCreditOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountCreditOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MainAccounts para órdenes de débito de glosas; solo sector público', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountDebitOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cuenta de orden de glosas, Solo para el sector publico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountDebitOrderId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountDebitOrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MainAccounts para recaudos difíciles, cartera morosa o gestión de cobros especial', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountHardCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta de dificil recuado', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountHardCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountHardCollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MainAccounts para gestión de cobro jurídico de facturas no pagadas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountLegalCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable cuentas de cobro juridico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountLegalCollectionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountLegalCollectionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MainAccounts para conciliaciones contables; solo habilitado en empresas privadas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountConciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta de conciliacion, solo para las empresas privadas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountConciliationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountConciliationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MainAccounts para glosas subsanables; solo empresas privadas (EAPB); default 203', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountObjectionRemediedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de la Glosa Subsanable, Solo para las empresas privadas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountObjectionRemediedId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountObjectionRemediedId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MainAccounts para reclamaciones radicadas; NULL si tipo grupo es Particular; default 203', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable radicada  Se coloca el campo null porque en el form de grupos de atención cuando el tipo de grupo de atencion es particular no pide esta cuenta', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountRadicateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MainAccounts para reclamaciones sin radicar; NULL si tipo grupo es Particular; default 203', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable sin Radicar  Se coloca el campo null porque en el form de grupos de atención cuando el tipo de grupo de atencion es particular no pide esta cuenta', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountWithoutRadicateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MainAccounts para servicios a particulares; solo se habilita cuando CareGroupType=3 (Particulares)', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountParticularId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable a particulares, Solo se habilita si el tipo de grupo de atencion es a particulares', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountParticularId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountParticularId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK INT a MainAccounts para cuota de recuperación; solo habilitado en EAPB, deshabilitado en Aseguradoras y Particulares', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountRecoveryFeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable de la cuota de recuperacion, Solo se habilita cuando sea EAPB, es decir que se desabilita cuando sea Aseguradoras y Particulares', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountRecoveryFeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'AccountRecoveryFeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de grupo de atención TINYINT: 1=EAPB con contrato, 2=EAPB sin contrato, 3=Particulares, 4=Aseguradoras; determina cuentas habilitadas', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CareGroupType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de Grupo de atencion  1 - EAPB Con contrato  2 - EAPB Sin Contrato  3 - Particulares  4 - Aseguradoras', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CareGroupType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'CareGroupType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo VARCHAR(100) de la estructura contable del contrato, para búsqueda por denominación en centros de atención', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el nombre de la estructura contable', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código VARCHAR(20) que identifica la estructura contable, usado para referencia rápida en RIPS, facturación y reportes contables', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) de la estructura contable del contrato en GeneralLedger', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estructura contable asociada a contratos: define qué cuentas contables se usan para cada etapa del ciclo de facturación y cartera (radicación, glosas, conciliación, cobro jurídico, deterioro de cartera y provisiones) según el tipo de grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractAccountingStructure';
