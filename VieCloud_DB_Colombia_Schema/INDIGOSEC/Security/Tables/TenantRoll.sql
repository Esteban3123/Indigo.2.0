CREATE TABLE [Security].[TenantRoll] (
    [Id]                           INT                                                     IDENTITY (1, 1) NOT NULL,
    [TenantId]                     SMALLINT                                                NOT NULL,
    [RollId]                       INT                                                     NOT NULL,
    [ledger_start_time]            DATETIME2 (7) GENERATED ALWAYS AS ROW START             NOT NULL,
    [ledger_end_time]              DATETIME2 (7) GENERATED ALWAYS AS ROW END               NOT NULL,
    [ledger_start_transaction_id]  BIGINT GENERATED ALWAYS AS TRANSACTION_ID START HIDDEN  NOT NULL,
    [ledger_end_transaction_id]    BIGINT GENERATED ALWAYS AS TRANSACTION_ID END HIDDEN    NULL,
    [ledger_start_sequence_number] BIGINT GENERATED ALWAYS AS SEQUENCE_NUMBER START HIDDEN NOT NULL,
    [ledger_end_sequence_number]   BIGINT GENERATED ALWAYS AS SEQUENCE_NUMBER END HIDDEN   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenantRoll_Ledger_Roll] FOREIGN KEY ([RollId]) REFERENCES [Security].[Roll] ([Id]),
    CONSTRAINT [FK_TenantRoll_Ledger_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [Security].[Tenant] ([Id]),
    PERIOD FOR SYSTEM_TIME ([ledger_start_time], [ledger_end_time])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE=[Security].[TenantRoll_Ledger_History]), LEDGER = ON (LEDGER_VIEW=[Security].[TenantRoll_Ledger_Ledger] (TRANSACTION_ID_COLUMN_NAME=ledger_transaction_id,SEQUENCE_NUMBER_COLUMN_NAME=ledger_sequence_number,OPERATION_TYPE_COLUMN_NAME=ledger_operation_type,OPERATION_TYPE_DESC_COLUMN_NAME=ledger_operation_type_desc)));


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se utiliza para almacenar la relación roles - tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantRoll';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla TenantRoll_Ledger', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantRoll', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantRoll', @level2type = N'COLUMN', @level2name = N'TenantId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de rol', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantRoll', @level2type = N'COLUMN', @level2name = N'RollId';

