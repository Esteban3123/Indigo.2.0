CREATE TABLE [Payments].[FilingUnitUser] (
    [Id]            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FillingUnitId] INT          NOT NULL,
    [UserId]        INT          NOT NULL,
    [UserCode]      VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_FilingUnitUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FilingUnitUser_FilingUnit] FOREIGN KEY ([FillingUnitId]) REFERENCES [Payments].[FilingUnit] ([Id])
);


GO
ALTER TABLE [Payments].[FilingUnitUser] NOCHECK CONSTRAINT [FK_FilingUnitUser_FilingUnit];

