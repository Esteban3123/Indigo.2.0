CREATE TABLE [SelfService].[PortalUser] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [UserCode]       VARCHAR (20)  NOT NULL,
    [Password]       VARCHAR (100) NOT NULL,
    [LockedUser]     BIT           NOT NULL,
    [Status]         BIT           NOT NULL,
    [RefreshToken]   VARCHAR (50)  CONSTRAINT [DF_PortalUser_RefreshToken] DEFAULT ((0)) NOT NULL,
    [RandomPassword] BIT           CONSTRAINT [DF_PortalUser_RandomPassword] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_PortalUser] PRIMARY KEY CLUSTERED ([Id] ASC)
);

