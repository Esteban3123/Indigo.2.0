CREATE TABLE [MixingStation].[Harnessed] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [QuantityRemainingId] INT             NOT NULL,
    [CampaignDetailId]    INT             NOT NULL,
    [Quantity]            DECIMAL (20, 6) NOT NULL,
    [CreationUser]        VARCHAR (20)    NOT NULL,
    [CreationDate]        DATETIME        NOT NULL,
    CONSTRAINT [PK_Harnessed] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Harnessed_CampaignDetail] FOREIGN KEY ([CampaignDetailId]) REFERENCES [MixingStation].[CampaignDetail] ([Id]),
    CONSTRAINT [FK_Harnessed_QuantityRemaining] FOREIGN KEY ([QuantityRemainingId]) REFERENCES [MixingStation].[QuantityRemaining] ([Id])
);




GO



GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de cantidades de insumos o materiales asignados (vinculados) a una campaña de mezcla o preparación, llevando el control de lo que se ha comprometido de cada saldo disponible.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de asignación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al saldo o cantidad disponible del insumo desde el cual se hace la asignación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed', @level2type = N'COLUMN', @level2name = N'QuantityRemainingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed', @level2type = N'COLUMN', @level2name = N'QuantityRemainingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al detalle de la campaña de mezcla o preparación a la que se asigna la cantidad.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed', @level2type = N'COLUMN', @level2name = N'CampaignDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad asignada o comprometida del insumo para el detalle de campaña correspondiente.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró la asignación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se realizó el registro de la asignación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'Harnessed', @level2type = N'COLUMN', @level2name = N'CreationDate';
