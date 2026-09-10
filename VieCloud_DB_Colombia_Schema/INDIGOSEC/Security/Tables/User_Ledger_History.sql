CREATE TABLE [Security].[User_Ledger_History] (
    [Id]                           INT           NOT NULL,
    [IdPerson]                     INT           NOT NULL,
    [UserCode]                     VARCHAR (20)  NOT NULL,
    [RollCode]                     INT           NOT NULL,
    [GroupCode]                    INT           NOT NULL,
    [Position]                     VARCHAR (150) NULL,
    [UserType]                     CHAR (1)      NULL,
    [ChangePassword]               BIT           NULL,
    [DaysChangePassword]           INT           NULL,
    [DateLastChangePassword]       DATETIME      NULL,
    [DateExpiryAccount]            DATETIME      NULL,
    [Password]                     VARCHAR (50)  NOT NULL,
    [State]                        BIT           NOT NULL,
    [UserNameLync]                 VARCHAR (100) NULL,
    [AddressSingInLync]            VARCHAR (100) NULL,
    [PasswordLync]                 VARCHAR (100) NULL,
    [PersonalNote]                 VARCHAR (100) NULL,
    [ViewForm]                     BIT           NOT NULL,
    [CodeInterface]                VARCHAR (12)  NULL,
    [IsLockedOut]                  BIT           NOT NULL,
    [FailedPasswordCount]          INT           NOT NULL,
    [ProfileType]                  CHAR (1)      NULL,
    [Email]                        VARCHAR (60)  NULL,
    [TimeStamp]                    ROWVERSION    NOT NULL,
    [OldId]                        INT           NULL,
    [RefreshToken]                 VARCHAR (100) NULL,
    [TenantId]                     SMALLINT      NULL,
    [CreationUser]                 VARCHAR (20)  NOT NULL,
    [CreationDate]                 DATETIME      NOT NULL,
    [ModificationUser]             VARCHAR (20)  NULL,
    [ModificationDate]             DATETIME      NULL,
    [ElectronicSignatureToken]     VARCHAR (100) NULL,
    [ledger_start_time]            DATETIME2 (7) NOT NULL,
    [ledger_end_time]              DATETIME2 (7) NOT NULL,
    [ledger_start_transaction_id]  BIGINT        NOT NULL,
    [ledger_end_transaction_id]    BIGINT        NULL,
    [ledger_start_sequence_number] BIGINT        NOT NULL,
    [ledger_end_sequence_number]   BIGINT        NULL
);


GO
CREATE CLUSTERED INDEX [ix_User_Ledger_History]
    ON [Security].[User_Ledger_History]([ledger_end_time] ASC, [ledger_start_time] ASC) WITH (DATA_COMPRESSION = PAGE);

