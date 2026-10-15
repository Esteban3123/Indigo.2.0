CREATE TABLE [Common].[Holiday] (
    [Id]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Holiday]     DATE         NOT NULL,
    [Description] VARCHAR (50) NULL,
    [State]       BIT          NOT NULL,
    CONSTRAINT [PK_Holiday__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_Holiday__Holiday] UNIQUE NONCLUSTERED ([Holiday] ASC)
);

