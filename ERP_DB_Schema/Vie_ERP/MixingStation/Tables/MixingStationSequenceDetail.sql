CREATE TABLE [MixingStation].[MixingStationSequenceDetail] (
    [Id]                       INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseMixingStationC] INT    NOT NULL,
    [IdSequense]               INT    NOT NULL,
    [IdOperatingUnit]          INT    NULL,
    [Next]                     BIGINT CONSTRAINT [DF_SequenseMixingStationD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseMixingStationD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseMixingStationD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseMixingStationD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseMixingStationD_SequenseMixingStationC] FOREIGN KEY ([IdSequenseMixingStationC]) REFERENCES [MixingStation].[MixingStationSequence] ([Id])
);


GO
ALTER TABLE [MixingStation].[MixingStationSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseMixingStationD_OperatingUnit];


GO
ALTER TABLE [MixingStation].[MixingStationSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseMixingStationD_Sequense];


GO
ALTER TABLE [MixingStation].[MixingStationSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseMixingStationD_SequenseMixingStationC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseMixingStationC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'MixingStationSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

