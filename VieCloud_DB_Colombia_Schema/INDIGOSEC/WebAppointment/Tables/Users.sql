CREATE TABLE [WebAppointment].[Users] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR (200) NOT NULL,
    [Email]    VARCHAR (100) NOT NULL,
    [Role]     INT           NOT NULL,
    [Password] VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC)
);

