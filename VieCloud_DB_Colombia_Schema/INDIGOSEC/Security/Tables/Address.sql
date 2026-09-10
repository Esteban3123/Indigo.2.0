CREATE TABLE [Security].[Address] (
    [Id]       INT                                                IDENTITY (1, 1) NOT NULL,
    [IdPerson] INT                                                NOT NULL,
    [Addresss] VARCHAR (100) MASKED WITH (FUNCTION = 'default()') NOT NULL,
    [State]    BIT                                                NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Address_Person] FOREIGN KEY ([IdPerson]) REFERENCES [Security].[Person] ([Id])
);

