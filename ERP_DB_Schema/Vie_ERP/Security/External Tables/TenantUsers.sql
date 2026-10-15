CREATE EXTERNAL TABLE [Security].[TenantUsers] (
    [Id] INT NOT NULL,
    [TenantId] SMALLINT NOT NULL,
    [UserId] INT NOT NULL,
    [RollId] INT NOT NULL,
    [GroupId] INT NOT NULL,
    [Position] VARCHAR (30) NULL,
    [UserType] CHAR (1) NOT NULL,
    [State] BIT NOT NULL,
    [CodeInterface] VARCHAR (12) NULL,
    [IsLockedOut] BIT NOT NULL,
    [ManageCompany] BIT NOT NULL,
    [TimeStamp] ROWVERSION NOT NULL
)
    WITH (
    DATA_SOURCE = [INDIGOSEC],
    SCHEMA_NAME = N'Security',
    OBJECT_NAME = N'TenantUsers'
    );

GO