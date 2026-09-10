CREATE TABLE [Security].[Email] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [IdPerson] INT          NOT NULL,
    [Email]    VARCHAR (60) NOT NULL,
    [State]    BIT          NOT NULL,
    CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Email_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Security].[Person] ([Id])
);

