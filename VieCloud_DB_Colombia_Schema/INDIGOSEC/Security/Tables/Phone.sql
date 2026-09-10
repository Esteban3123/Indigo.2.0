CREATE TABLE [Security].[Phone] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [IdPerson]    INT          NOT NULL,
    [Phone]       VARCHAR (15) NOT NULL,
    [IdPhoneType] SMALLINT     NOT NULL,
    [State]       BIT          NOT NULL,
    CONSTRAINT [PK_Phone] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Phone_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Security].[Person] ([Id]),
    CONSTRAINT [FK_Phone_PhoneType] FOREIGN KEY ([IdPhoneType]) REFERENCES [Security].[PhoneType] ([Id])
);

