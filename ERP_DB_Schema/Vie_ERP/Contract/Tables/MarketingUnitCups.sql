CREATE TABLE [Contract].[MarketingUnitCups] (
    [Id]              INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MarketingUnitId] INT NULL,
    [CupsEntityId]    INT NULL,
    CONSTRAINT [PK_MarketingUnitCups__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MarketingUnitCups_CupsEntity] FOREIGN KEY ([CupsEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_MarketingUnitCups_MarketingUnit] FOREIGN KEY ([MarketingUnitId]) REFERENCES [Contract].[MarketingUnit] ([Id])
);


GO
ALTER TABLE [Contract].[MarketingUnitCups] NOCHECK CONSTRAINT [FK_MarketingUnitCups_CupsEntity];


GO
ALTER TABLE [Contract].[MarketingUnitCups] NOCHECK CONSTRAINT [FK_MarketingUnitCups_MarketingUnit];


GO
CREATE NONCLUSTERED INDEX [IX_MarketingUnitCups__MarketingUnitId__CupsEntityId]
    ON [Contract].[MarketingUnitCups]([MarketingUnitId] ASC, [CupsEntityId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del cups', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups', @level2type = N'COLUMN', @level2name = N'CupsEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de mercadeo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups', @level2type = N'COLUMN', @level2name = N'MarketingUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'MarketingUnitCups', @level2type = N'COLUMN', @level2name = N'Id';

