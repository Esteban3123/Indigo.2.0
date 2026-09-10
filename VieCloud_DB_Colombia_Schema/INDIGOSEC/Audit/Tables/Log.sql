CREATE TABLE [Audit].[Log] (
    [Id]              INT             IDENTITY (1, 1) NOT NULL,
    [IdEvent]         INT             NULL,
    [Priority]        INT             NOT NULL,
    [Severity]        NVARCHAR (32)   NOT NULL,
    [Title]           NVARCHAR (256)  NOT NULL,
    [TimeSpam]        DATETIME        NOT NULL,
    [WorkstationName] NVARCHAR (32)   NOT NULL,
    [AppDomainName]   NVARCHAR (512)  NOT NULL,
    [ProcessId]       NVARCHAR (256)  NOT NULL,
    [ProcessName]     NVARCHAR (512)  NOT NULL,
    [ThreadName]      NVARCHAR (512)  NULL,
    [Win32ThreadId]   NVARCHAR (128)  NULL,
    [Message]         NVARCHAR (1500) NULL,
    [FormatedMessage] NTEXT           NULL,
    CONSTRAINT [PK_LOG] PRIMARY KEY CLUSTERED ([Id] ASC)
);

