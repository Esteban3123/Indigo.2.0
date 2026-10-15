CREATE TABLE [Common].[MilitaryCard] (
    [Id]    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]  VARCHAR (3)  NOT NULL,
    [Name]  VARCHAR (50) NOT NULL,
    [State] BIT          NOT NULL,
    CONSTRAINT [PK_MilitaryCard] PRIMARY KEY CLUSTERED ([Id] ASC)
);

