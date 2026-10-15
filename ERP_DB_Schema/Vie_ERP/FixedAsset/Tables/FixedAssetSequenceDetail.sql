CREATE TABLE [FixedAsset].[FixedAssetSequenceDetail] (
    [Id]                    INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseFixedAssetC] INT    NOT NULL,
    [IdSequense]            INT    NOT NULL,
    [IdOperatingUnit]       INT    NULL,
    [Next]                  BIGINT CONSTRAINT [DF_SequenseFixedAssetD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseFixedAssetD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseFixedAssetD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseFixedAssetD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseFixedAssetD_SequenseFixedAssetC] FOREIGN KEY ([IdSequenseFixedAssetC]) REFERENCES [FixedAsset].[FixedAssetSequence] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseFixedAssetD_OperatingUnit];


GO
ALTER TABLE [FixedAsset].[FixedAssetSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseFixedAssetD_Sequense];


GO
ALTER TABLE [FixedAsset].[FixedAssetSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseFixedAssetD_SequenseFixedAssetC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseFixedAssetC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

