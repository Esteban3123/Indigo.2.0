CREATE TABLE [Security].[TenantUsers] (
    [Id]                           INT                                                     IDENTITY (1, 1) NOT NULL,
    [TenantId]                     SMALLINT                                                NOT NULL,
    [UserId]                       INT                                                     NOT NULL,
    [RollId]                       INT                                                     NOT NULL,
    [GroupId]                      INT                                                     NOT NULL,
    [Position]                     VARCHAR (30)                                            NULL,
    [UserType]                     CHAR (1)                                                NOT NULL,
    [State]                        BIT                                                     NOT NULL,
    [CodeInterface]                VARCHAR (12)                                            NULL,
    [IsLockedOut]                  BIT                                                     CONSTRAINT [DF_TenantUsers_IsLockedOut_Ledger] DEFAULT ((0)) NOT NULL,
    [ManageCompany]                BIT                                                     CONSTRAINT [DF_TenantUsers_Ledger_ManageCompany_Ledger] DEFAULT ((1)) NOT NULL,
    [TimeStamp]                    ROWVERSION                                              NOT NULL,
    [ledger_start_time]            DATETIME2 (7) GENERATED ALWAYS AS ROW START             NOT NULL,
    [ledger_end_time]              DATETIME2 (7) GENERATED ALWAYS AS ROW END               NOT NULL,
    [ledger_start_transaction_id]  BIGINT GENERATED ALWAYS AS TRANSACTION_ID START HIDDEN  NOT NULL,
    [ledger_end_transaction_id]    BIGINT GENERATED ALWAYS AS TRANSACTION_ID END HIDDEN    NULL,
    [ledger_start_sequence_number] BIGINT GENERATED ALWAYS AS SEQUENCE_NUMBER START HIDDEN NOT NULL,
    [ledger_end_sequence_number]   BIGINT GENERATED ALWAYS AS SEQUENCE_NUMBER END HIDDEN   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TenantUsers_Ledger_Group] FOREIGN KEY ([GroupId]) REFERENCES [Security].[Group] ([Id]),
    CONSTRAINT [FK_TenantUsers_Ledger_Roll] FOREIGN KEY ([RollId]) REFERENCES [Security].[Roll] ([Id]),
    CONSTRAINT [FK_TenantUsers_Ledger_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [Security].[Tenant] ([Id]),
    CONSTRAINT [FK_TenantUsers_Ledger_User] FOREIGN KEY ([UserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_TenantUsers_TenantUsers] FOREIGN KEY ([Id]) REFERENCES [Security].[TenantUsers] ([Id]),
    PERIOD FOR SYSTEM_TIME ([ledger_start_time], [ledger_end_time])
)
WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE=[Security].[TenantUsers_Ledger_History]), LEDGER = ON (LEDGER_VIEW=[Security].[TenantUsers_Ledger_Ledger] (TRANSACTION_ID_COLUMN_NAME=ledger_transaction_id,SEQUENCE_NUMBER_COLUMN_NAME=ledger_sequence_number,OPERATION_TYPE_COLUMN_NAME=ledger_operation_type,OPERATION_TYPE_DESC_COLUMN_NAME=ledger_operation_type_desc)));


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsers', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de tenant al que pertenece el usuario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsers', @level2type = N'COLUMN', @level2name = N'TenantId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de usuario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsers', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de rol', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsers', @level2type = N'COLUMN', @level2name = N'RollId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de grupo', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsers', @level2type = N'COLUMN', @level2name = N'GroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargo del usuario', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsers', @level2type = N'COLUMN', @level2name = N'Position';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de usuario: 1: Administrador Empresa - 2: Administrador Tenant - 3: Administrador Global', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsers', @level2type = N'COLUMN', @level2name = N'UserType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del usuario en el tenant: 1:Activo, 2:Inactivo', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsers', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de usuario en la aplicación del tercero', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsers', @level2type = N'COLUMN', @level2name = N'CodeInterface';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si el usuario está bloqueado en el tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsers', @level2type = N'COLUMN', @level2name = N'IsLockedOut';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se usa para el usuario de tipo "Administrador Empresa", para filtrar entre los tenant del usuario en cuales administra alguna compañía.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsers', @level2type = N'COLUMN', @level2name = N'ManageCompany';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Para controlar acceso concurrente', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'TenantUsers', @level2type = N'COLUMN', @level2name = N'TimeStamp';

