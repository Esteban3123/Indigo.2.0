CREATE TABLE [Security].[TenantUsers_Ledger_History] (
    [Id]                           INT           NOT NULL,
    [TenantId]                     SMALLINT      NOT NULL,
    [UserId]                       INT           NOT NULL,
    [RollId]                       INT           NOT NULL,
    [GroupId]                      INT           NOT NULL,
    [Position]                     VARCHAR (30)  NULL,
    [UserType]                     CHAR (1)      NOT NULL,
    [State]                        BIT           NOT NULL,
    [CodeInterface]                VARCHAR (12)  NULL,
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


GO
CREATE CLUSTERED INDEX [ix_TenantUsers_Ledger_History]
    ON [Security].[TenantUsers_Ledger_History]([ledger_end_time] ASC, [ledger_start_time] ASC) WITH (DATA_COMPRESSION = PAGE);

