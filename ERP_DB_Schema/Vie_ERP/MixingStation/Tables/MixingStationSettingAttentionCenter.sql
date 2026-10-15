CREATE TABLE [MixingStation].[MixingStationSettingAttentionCenter] (
    [Id]        INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE] CHAR (10) NOT NULL,
    [UFUCODIGO] CHAR (10) NOT NULL,
    CONSTRAINT [PK_MixingStationSettingAttentionCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MixingStationSettingAttentionCenter_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_MixingStationSettingAttentionCenter_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [MixingStation].[MixingStationSettingAttentionCenter] NOCHECK CONSTRAINT [FK_MixingStationSettingAttentionCenter_ADCENATEN];


GO
ALTER TABLE [MixingStation].[MixingStationSettingAttentionCenter] NOCHECK CONSTRAINT [FK_MixingStationSettingAttentionCenter_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSettingAttentionCenter', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSettingAttentionCenter', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSettingAttentionCenter', @level2type = N'COLUMN', @level2name = N'Id';

