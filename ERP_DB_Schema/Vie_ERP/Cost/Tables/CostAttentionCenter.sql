CREATE TABLE [Cost].[CostAttentionCenter] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]             VARCHAR (20)  NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [HealthProviderId] INT           NOT NULL,
    CONSTRAINT [PK_CostAttentionCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostAttentionCenter_HealthProvider] FOREIGN KEY ([HealthProviderId]) REFERENCES [Common].[HealthProvider] ([Id])
);


GO
ALTER TABLE [Cost].[CostAttentionCenter] NOCHECK CONSTRAINT [FK_CostAttentionCenter_HealthProvider];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la IPS', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostAttentionCenter', @level2type = N'COLUMN', @level2name = N'HealthProviderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del centro de atencion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostAttentionCenter', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostAttentionCenter', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del centro de atencion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostAttentionCenter', @level2type = N'COLUMN', @level2name = N'Id';

