CREATE TABLE [Payroll].[PositionRoll] (
    [Id]         INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdRol]      INT          NOT NULL,
    [CodeRol]    VARCHAR (20) NOT NULL,
    [IdPosition] INT          NOT NULL,
    CONSTRAINT [PK_PositionRoll] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PositionRoll_Position] FOREIGN KEY ([IdPosition]) REFERENCES [Payroll].[Position] ([Id])
);


GO
ALTER TABLE [Payroll].[PositionRoll] NOCHECK CONSTRAINT [FK_PositionRoll_Position];

