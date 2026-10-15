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

