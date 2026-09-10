CREATE TABLE [SelfService].[PortalUserEmail] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Email]        VARCHAR (256) NOT NULL,
    [PortalUserId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PortalUserEmail_PortalUser] FOREIGN KEY ([PortalUserId]) REFERENCES [SelfService].[PortalUser] ([Id]),
    CONSTRAINT [UQ_PortalUserEmail_Email] UNIQUE NONCLUSTERED ([Email] ASC)
);

