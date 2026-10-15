CREATE TABLE [Cost].[ClosedMonth] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Year]         INT          NOT NULL,
    [Month]        INT          NOT NULL,
    [CreationUser] VARCHAR (20) NOT NULL,
    [CreationDate] DATETIME     NOT NULL,
    CONSTRAINT [PK_Cost_ClosedMonth] PRIMARY KEY CLUSTERED ([Id] ASC)
);

