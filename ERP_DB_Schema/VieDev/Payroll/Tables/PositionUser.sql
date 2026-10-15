CREATE TABLE [Payroll].[PositionUser] (
    [Id]         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdUser]     INT          NOT NULL,
    [CodeUser]   VARCHAR (20) NOT NULL,
    [IdPosition] INT          NOT NULL,
    CONSTRAINT [PK_PositionUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PositionUser_Position] FOREIGN KEY ([IdPosition]) REFERENCES [Payroll].[Position] ([Id])
);


GO
ALTER TABLE [Payroll].[PositionUser] NOCHECK CONSTRAINT [FK_PositionUser_Position];

