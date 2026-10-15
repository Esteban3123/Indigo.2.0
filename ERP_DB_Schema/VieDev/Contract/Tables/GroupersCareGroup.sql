CREATE TABLE [Contract].[GroupersCareGroup] (
    [Id]           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]  INT          NOT NULL,
    [GroupersId]   INT          NOT NULL,
    [EjectEvent]   INT          CONSTRAINT [DF_GroupersCareGroup_EjectEvent] DEFAULT ((0)) NOT NULL,
    [RealCME]      NUMERIC (18) CONSTRAINT [DF_GroupersCareGroup_RealCME] DEFAULT ((0)) NOT NULL,
    [TotalEject]   NUMERIC (18) CONSTRAINT [DF_GroupersCareGroup_TotalEject] DEFAULT ((0)) NOT NULL,
    [DocumentDate] DATE         NOT NULL,
    CONSTRAINT [PK_GroupersCareGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_GroupersCareGroup_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_GroupersCareGroup_Groupers] FOREIGN KEY ([GroupersId]) REFERENCES [Contract].[Groupers] ([Id])
);


GO
ALTER TABLE [Contract].[GroupersCareGroup] NOCHECK CONSTRAINT [FK_GroupersCareGroup_CareGroup];


GO
ALTER TABLE [Contract].[GroupersCareGroup] NOCHECK CONSTRAINT [FK_GroupersCareGroup_Groupers];

