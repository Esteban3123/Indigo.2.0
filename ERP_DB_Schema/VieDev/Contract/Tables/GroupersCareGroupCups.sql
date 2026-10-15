CREATE TABLE [Contract].[GroupersCareGroupCups] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [GroupersCareGroupId] INT NOT NULL,
    [CUPSEntityId]        INT NOT NULL,
    CONSTRAINT [PK_GroupersCareGroupCups] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersCareGroupCups_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_GroupersCareGroupCups_GroupersCareGroup] FOREIGN KEY ([GroupersCareGroupId]) REFERENCES [Contract].[GroupersCareGroup] ([Id])
);


GO
ALTER TABLE [Contract].[GroupersCareGroupCups] NOCHECK CONSTRAINT [FK_GroupersCareGroupCups_CUPSEntity];


GO
ALTER TABLE [Contract].[GroupersCareGroupCups] NOCHECK CONSTRAINT [FK_GroupersCareGroupCups_GroupersCareGroup];

