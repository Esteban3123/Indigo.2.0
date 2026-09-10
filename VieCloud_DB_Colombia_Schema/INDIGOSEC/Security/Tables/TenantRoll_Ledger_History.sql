CREATE TABLE [Security].[TenantRoll_Ledger_History] (
    [Id]                           INT           NOT NULL,
    [TenantId]                     SMALLINT      NOT NULL,
    [RollId]                       INT           NOT NULL,
    [ledger_start_time]            DATETIME2 (7) NOT NULL,
    [ledger_end_time]              DATETIME2 (7) NOT NULL,
    [ledger_start_transaction_id]  BIGINT        NOT NULL,
    [ledger_end_transaction_id]    BIGINT        NULL,
    [ledger_start_sequence_number] BIGINT        NOT NULL,
    [ledger_end_sequence_number]   BIGINT        NULL
);


GO
CREATE CLUSTERED INDEX [ix_TenantRoll_Ledger_History]
    ON [Security].[TenantRoll_Ledger_History]([ledger_end_time] ASC, [ledger_start_time] ASC) WITH (DATA_COMPRESSION = PAGE);

