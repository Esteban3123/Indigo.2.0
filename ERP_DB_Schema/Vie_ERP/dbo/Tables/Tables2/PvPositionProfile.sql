CREATE TABLE [dbo].[PvPositionProfile] (
    [Id]                INT NOT NULL,
    [PositionProfileId] INT NOT NULL,
    [PositionsId]       INT NOT NULL,
    CONSTRAINT [PK_PvPositionProfile] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PvPositionProfile_PositionProfile] FOREIGN KEY ([PositionProfileId]) REFERENCES [HumanTalent].[PositionProfile] ([Id]),
    CONSTRAINT [FK_PvPositionProfile_Positions] FOREIGN KEY ([PositionsId]) REFERENCES [HumanTalent].[Positions] ([Id])
);


GO
ALTER TABLE [dbo].[PvPositionProfile] NOCHECK CONSTRAINT [FK_PvPositionProfile_PositionProfile];


GO
ALTER TABLE [dbo].[PvPositionProfile] NOCHECK CONSTRAINT [FK_PvPositionProfile_Positions];

