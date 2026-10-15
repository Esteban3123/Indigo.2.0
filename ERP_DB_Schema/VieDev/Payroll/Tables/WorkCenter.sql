CREATE TABLE [Payroll].[WorkCenter] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             CHAR (20)    NOT NULL,
    [Name]             VARCHAR (50) NOT NULL,
    [CityId]           INT          NOT NULL,
    [State]            BIT          NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_WorkCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WorkCenter_City] FOREIGN KEY ([CityId]) REFERENCES [Common].[City] ([Id])
);


GO
ALTER TABLE [Payroll].[WorkCenter] NOCHECK CONSTRAINT [FK_WorkCenter_City];

