CREATE TABLE [Common].[ThirdPartyEconomicActivities] (
    [Id]                 INT IDENTITY (1, 1) NOT NULL,
    [ThirdPartyId]       INT NOT NULL,
    [EconomicActivityId] INT NOT NULL,
    [Defect]             BIT DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_ThirdPartyEconomicActivities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ThirdPartyEconomicActivities_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_ThirdPartyEconomicActivities_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Common].[ThirdPartyEconomicActivities] NOCHECK CONSTRAINT [FK_ThirdPartyEconomicActivities_EconomicActivity];


GO
ALTER TABLE [Common].[ThirdPartyEconomicActivities] NOCHECK CONSTRAINT [FK_ThirdPartyEconomicActivities_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la actividad economica sera marcada "Por Defecto"', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'Defect';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Actividad economica', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'TABLE', @level1name = N'ThirdPartyEconomicActivities', @level2type = N'COLUMN', @level2name = N'Id';

