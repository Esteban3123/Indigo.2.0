CREATE TABLE [HumanTalent].[PvPositionProfile] (
    [Id]                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PositionProfileId] INT NOT NULL,
    [PositionsId]       INT NOT NULL,
    CONSTRAINT [PK_PvPositionProfile_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PvPositionProfile_Positions] FOREIGN KEY ([PositionsId]) REFERENCES [HumanTalent].[Positions] ([Id])
);


GO
ALTER TABLE [HumanTalent].[PvPositionProfile] NOCHECK CONSTRAINT [FK_PvPositionProfile_Positions];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de las posiciones ', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'PositionsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id cabecera de perfil del cargo', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'PositionProfileId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'TABLE', @level1name = N'PvPositionProfile', @level2type = N'COLUMN', @level2name = N'Id';

