CREATE TABLE [Authorization].[AuthorizationRejectionUser] (
    [Id]                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationRejectionId] INT          NOT NULL,
    [UserId]                   INT          NOT NULL,
    [UserCode]                 VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AuthorizationRejectionUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationRejectionUser_AuthorizationRejection] FOREIGN KEY ([AuthorizationRejectionId]) REFERENCES [Authorization].[AuthorizationRejection] ([Id])
);


GO
ALTER TABLE [Authorization].[AuthorizationRejectionUser] NOCHECK CONSTRAINT [FK_AuthorizationRejectionUser_AuthorizationRejection];

