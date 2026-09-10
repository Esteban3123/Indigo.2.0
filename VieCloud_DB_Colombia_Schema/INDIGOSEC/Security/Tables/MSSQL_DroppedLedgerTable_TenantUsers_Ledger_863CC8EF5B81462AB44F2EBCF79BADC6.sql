CREATE TABLE [Security].[MSSQL_DroppedLedgerTable_TenantUsers_Ledger_863CC8EF5B81462AB44F2EBCF79BADC6] (
    [Id]                           INT                                                     IDENTITY (1, 1) NOT NULL,
    [TenantId]                     SMALLINT                                                NOT NULL,
    [UserId]                       INT                                                     NOT NULL,
    [RollId]                       INT                                                     NOT NULL,
    [GroupId]                      INT                                                     NOT NULL,
    [State]                        BIT                                                     NOT NULL,
    [IsLockedOut]                  BIT                                                     NOT NULL,
    [ManageCompany]                BIT                                                     NOT NULL,
    [TimeStamp]                    ROWVERSION                                              NOT NULL,
    [ledger_start_time]            DATETIME2 (7) GENERATED ALWAYS AS ROW START             NOT NULL,
    [ledger_end_time]              DATETIME2 (7) GENERATED ALWAYS AS ROW END               NOT NULL,
    [ledger_start_transaction_id]  BIGINT GENERATED ALWAYS AS TRANSACTION_ID START HIDDEN  NOT NULL,
    [ledger_end_transaction_id]    BIGINT GENERATED ALWAYS AS TRANSACTION_ID END HIDDEN    NULL,
    [ledger_start_sequence_number] BIGINT GENERATED ALWAYS AS SEQUENCE_NUMBER START HIDDEN NOT NULL,
    [ledger_end_sequence_number]   BIGINT GENERATED ALWAYS AS SEQUENCE_NUMBER END HIDDEN   NULL,
    PERIOD FOR SYSTEM_TIME ([ledger_start_time], [ledger_end_time])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE=[Security].[MSSQL_DroppedLedgerHistory_TenantUsers_Ledger_History_28B0299157CD45D3B45B2620EBD11E66]), LEDGER = ON (LEDGER_VIEW=[Security].[MSSQL_DroppedLedgerView_TenantUsers_Ledger_Ledger_DA92F703E9DD4CF1AC296F32EADE7608] (TRANSACTION_ID_COLUMN_NAME=ledger_transaction_id,SEQUENCE_NUMBER_COLUMN_NAME=ledger_sequence_number,OPERATION_TYPE_COLUMN_NAME=ledger_operation_type,OPERATION_TYPE_DESC_COLUMN_NAME=ledger_operation_type_desc)));


GO

