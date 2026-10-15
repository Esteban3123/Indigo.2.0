CREATE TABLE [Contract].[GroupersActivities] (
    [Id]            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GrouperId]     INT           NOT NULL,
    [AGACTIMEDCode] VARCHAR (5)   NOT NULL,
    [AGACTIMEDName] VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_GroupersActivities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersActivities_Groupers] FOREIGN KEY ([GrouperId]) REFERENCES [Contract].[Groupers] ([Id])
);


GO
ALTER TABLE [Contract].[GroupersActivities] NOCHECK CONSTRAINT [FK_GroupersActivities_Groupers];

