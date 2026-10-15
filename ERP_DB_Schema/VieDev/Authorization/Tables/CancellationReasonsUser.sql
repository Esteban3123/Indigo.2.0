CREATE TABLE [Authorization].[CancellationReasonsUser] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CancellationReasonsId] INT          NOT NULL,
    [UserId]                INT          NOT NULL,
    [UserCode]              VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_CancellationReasonsUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CancellationReasonsUser_CancellationReasons] FOREIGN KEY ([CancellationReasonsId]) REFERENCES [Authorization].[CancellationReasons] ([Id])
);


GO
ALTER TABLE [Authorization].[CancellationReasonsUser] NOCHECK CONSTRAINT [FK_CancellationReasonsUser_CancellationReasons];

