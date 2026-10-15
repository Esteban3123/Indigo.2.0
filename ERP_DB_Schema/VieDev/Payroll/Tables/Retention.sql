CREATE TABLE [Payroll].[Retention] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ConsecutiveId]    INT             NOT NULL,
    [Code]             CHAR (3)        NOT NULL,
    [UVTMin]           NUMERIC (18, 2) NOT NULL,
    [UVTMax]           NUMERIC (18, 2) NOT NULL,
    [Rate]             NUMERIC (18, 2) NOT NULL,
    [AdditionalRate]   NUMERIC (18, 2) NOT NULL,
    [UVTValue]         NUMERIC (18, 2) NOT NULL,
    [Year]             INT             NOT NULL,
    [Consecutive]      INT             NOT NULL,
    [State]            BIT             NOT NULL,
    [CreationUser]     VARCHAR (20)    NOT NULL,
    [CreationDate]     DATETIME        NOT NULL,
    [ModificationUser] VARCHAR (20)    NULL,
    [ModificationDate] DATETIME        NULL,
    [TimeStamp]        ROWVERSION      NOT NULL,
    CONSTRAINT [PK_Retention] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Retention_Consecutive] FOREIGN KEY ([ConsecutiveId]) REFERENCES [Common].[Consecutive] ([Id])
);


GO
ALTER TABLE [Payroll].[Retention] NOCHECK CONSTRAINT [FK_Retention_Consecutive];

