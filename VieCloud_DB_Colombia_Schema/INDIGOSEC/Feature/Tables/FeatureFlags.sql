CREATE TABLE [Feature].[FeatureFlags] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [Code]        VARCHAR (30) NOT NULL,
    [IdContainer] INT          NOT NULL,
    [Status]      BIT          NOT NULL,
    CONSTRAINT [PK_Endpoints] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FeatureFlags_Containers] FOREIGN KEY ([IdContainer]) REFERENCES [Security].[Containers] ([Id])
);

