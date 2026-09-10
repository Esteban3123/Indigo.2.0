CREATE TABLE [Security].[PhoneType] (
    [Id]    SMALLINT     IDENTITY (1, 1) NOT NULL,
    [Code]  VARCHAR (1)  NOT NULL,
    [Name]  VARCHAR (30) NOT NULL,
    [State] BIT          NOT NULL,
    CONSTRAINT [PK_PhoneType_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [IX_PhoneType] UNIQUE NONCLUSTERED ([Code] ASC)
);

