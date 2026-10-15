CREATE TABLE [Contract].[CareGroupDefinitionRate] (
    [Id]               INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CareGroupId]      INT      NOT NULL,
    [DefinitionRateId] INT      NOT NULL,
    [InitialDate]      DATETIME NOT NULL,
    [EndDate]          DATETIME NOT NULL,
    CONSTRAINT [PK_CareGroupDefinitionRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CareGroupDefinitionRate_CareGroup] FOREIGN KEY ([CareGroupId]) REFERENCES [Contract].[CareGroup] ([Id]),
    CONSTRAINT [FK_CareGroupDefinitionRate_DefinitionRate] FOREIGN KEY ([DefinitionRateId]) REFERENCES [Contract].[DefinitionRate] ([Id])
);


GO
ALTER TABLE [Contract].[CareGroupDefinitionRate] NOCHECK CONSTRAINT [FK_CareGroupDefinitionRate_CareGroup];


GO
ALTER TABLE [Contract].[CareGroupDefinitionRate] NOCHECK CONSTRAINT [FK_CareGroupDefinitionRate_DefinitionRate];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupDefinitionRate', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial ', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupDefinitionRate', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la definicion de la tarifa', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupDefinitionRate', @level2type = N'COLUMN', @level2name = N'DefinitionRateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id Grupo de Atencion', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupDefinitionRate', @level2type = N'COLUMN', @level2name = N'CareGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'CareGroupDefinitionRate', @level2type = N'COLUMN', @level2name = N'Id';

