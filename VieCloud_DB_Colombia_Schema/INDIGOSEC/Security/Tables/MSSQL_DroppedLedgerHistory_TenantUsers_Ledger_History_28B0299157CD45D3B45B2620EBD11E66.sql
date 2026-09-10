CREATE TABLE [Security].[MSSQL_DroppedLedgerHistory_TenantUsers_Ledger_History_28B0299157CD45D3B45B2620EBD11E66] (
    [Id]                           INT           NOT NULL,
    [TenantId]                     SMALLINT      NOT NULL,
    [UserId]                       INT           NOT NULL,
    [RollId]                       INT           NOT NULL,
    [GroupId]                      INT           NOT NULL,
    [State]                        BIT           NOT NULL,
    [IsLockedOut]                  BIT           NOT NULL,
    [ManageCompany]                BIT           NOT NULL,
    [TimeStamp]                    ROWVERSION    NOT NULL,
    [ledger_start_time]            DATETIME2 (7) NOT NULL,
    [ledger_end_time]              DATETIME2 (7) NOT NULL,
    [ledger_start_transaction_id]  BIGINT        NOT NULL,
    [ledger_end_transaction_id]    BIGINT        NULL,
    [ledger_start_sequence_number] BIGINT        NOT NULL,
    [ledger_end_sequence_number]   BIGINT        NULL
);

