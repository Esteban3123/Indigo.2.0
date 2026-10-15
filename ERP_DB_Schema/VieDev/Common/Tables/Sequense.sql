CREATE TABLE [Common].[Sequense] (
    [Id]      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Name]    VARCHAR (20) NOT NULL,
    [Pattern] VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_Sequense__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);

