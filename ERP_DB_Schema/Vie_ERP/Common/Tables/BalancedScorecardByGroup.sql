CREATE TABLE [Common].[BalancedScorecardByGroup] (
    [Id]                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UserGroup]           INT NOT NULL,
    [BalancedScorecardId] INT NOT NULL,
    CONSTRAINT [PK_BalancedScorecardByGroup] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BalancedScorecardByGroup_BalancedScorecard] FOREIGN KEY ([BalancedScorecardId]) REFERENCES [Common].[BalancedScorecard] ([Id])
);


GO
ALTER TABLE [Common].[BalancedScorecardByGroup] NOCHECK CONSTRAINT [FK_BalancedScorecardByGroup_BalancedScorecard];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Saldo en el score card', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup', @level2type = N'COLUMN', @level2name = N'BalancedScorecardId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo al que pertenece el usuario', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup', @level2type = N'COLUMN', @level2name = N'UserGroup';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'BalancedScorecardByGroup', @level2type = N'COLUMN', @level2name = N'Id';

