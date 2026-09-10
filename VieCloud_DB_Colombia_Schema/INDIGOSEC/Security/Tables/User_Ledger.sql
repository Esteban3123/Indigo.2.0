CREATE TABLE [Security].[User_Ledger] (
    [Id]                           INT                                                     IDENTITY (1, 1) NOT NULL,
    [IdPerson]                     INT                                                     NOT NULL,
    [UserCode]                     VARCHAR (20)                                            NOT NULL,
    [RollCode]                     INT                                                     NOT NULL,
    [GroupCode]                    INT                                                     NOT NULL,
    [Position]                     VARCHAR (150)                                           NULL,
    [UserType]                     CHAR (1)                                                NULL,
    [ChangePassword]               BIT                                                     NULL,
    [DaysChangePassword]           INT                                                     NULL,
    [DateLastChangePassword]       DATETIME                                                NULL,
    [DateExpiryAccount]            DATETIME                                                NULL,
    [Password]                     VARCHAR (50)                                            NOT NULL,
    [State]                        BIT                                                     NOT NULL,
    [UserNameLync]                 VARCHAR (100)                                           NULL,
    [AddressSingInLync]            VARCHAR (100)                                           NULL,
    [PasswordLync]                 VARCHAR (100)                                           NULL,
    [PersonalNote]                 VARCHAR (100)                                           NULL,
    [ViewForm]                     BIT                                                     NOT NULL,
    [CodeInterface]                VARCHAR (12)                                            NULL,
    [IsLockedOut]                  BIT                                                     CONSTRAINT [DF_User_Ledger_IsLockedOut] DEFAULT ((0)) NOT NULL,
    [FailedPasswordCount]          INT                                                     CONSTRAINT [DF_User_Ledger_FailedPasswordCount] DEFAULT ((0)) NOT NULL,
    [ProfileType]                  CHAR (1)                                                NULL,
    [Email]                        VARCHAR (60)                                            NULL,
    [TimeStamp]                    ROWVERSION                                              NOT NULL,
    [OldId]                        INT                                                     NULL,
    [RefreshToken]                 VARCHAR (100)                                           NULL,
    [TenantId]                     SMALLINT                                                NULL,
    [CreationUser]                 VARCHAR (20)                                            DEFAULT ((999)) NOT NULL,
    [CreationDate]                 DATETIME                                                DEFAULT ([Common].[GETDATE]()) NOT NULL,
    [ModificationUser]             VARCHAR (20)                                            NULL,
    [ModificationDate]             DATETIME                                                NULL,
    [ElectronicSignatureToken]     VARCHAR (100)                                           NULL,
    [ledger_start_time]            DATETIME2 (7) GENERATED ALWAYS AS ROW START             NOT NULL,
    [ledger_end_time]              DATETIME2 (7) GENERATED ALWAYS AS ROW END               NOT NULL,
    [ledger_start_transaction_id]  BIGINT GENERATED ALWAYS AS TRANSACTION_ID START HIDDEN  NOT NULL,
    [ledger_end_transaction_id]    BIGINT GENERATED ALWAYS AS TRANSACTION_ID END HIDDEN    NULL,
    [ledger_start_sequence_number] BIGINT GENERATED ALWAYS AS SEQUENCE_NUMBER START HIDDEN NOT NULL,
    [ledger_end_sequence_number]   BIGINT GENERATED ALWAYS AS SEQUENCE_NUMBER END HIDDEN   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_Ledger_Group] FOREIGN KEY ([GroupCode]) REFERENCES [Security].[Group] ([Id]),
    CONSTRAINT [FK_User_Ledger_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Security].[Person] ([Id]),
    CONSTRAINT [FK_User_Ledger_Roll] FOREIGN KEY ([RollCode]) REFERENCES [Security].[Roll] ([Id]),
    UNIQUE NONCLUSTERED ([UserCode] ASC),
    UNIQUE NONCLUSTERED ([IdPerson] ASC, [Email] ASC),
    PERIOD FOR SYSTEM_TIME ([ledger_start_time], [ledger_end_time])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE=[Security].[User_Ledger_History]), LEDGER = ON (LEDGER_VIEW=[Security].[User_Ledger_Ledger] (TRANSACTION_ID_COLUMN_NAME=ledger_transaction_id,SEQUENCE_NUMBER_COLUMN_NAME=ledger_sequence_number,OPERATION_TYPE_COLUMN_NAME=ledger_operation_type,OPERATION_TYPE_DESC_COLUMN_NAME=ledger_operation_type_desc)));


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de usuario: 0: StandardUser - 1: Administrador Empresa - 2: Administrador Tenant - 3: Administrador Global', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User_Ledger', @level2type = N'COLUMN', @level2name = N'UserType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Define el Tipo de Perfil: 1= Administrativo, 2= Asistencial', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User_Ledger', @level2type = N'COLUMN', @level2name = N'ProfileType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico, debe ser único por usuario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User_Ledger', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de eventos de creación o modificación de registros.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User_Ledger', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario de Creación', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User_Ledger', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'User_Ledger', @level2type = N'COLUMN', @level2name = N'CreationDate';

