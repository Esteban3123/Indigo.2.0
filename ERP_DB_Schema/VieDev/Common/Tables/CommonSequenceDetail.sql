CREATE TABLE [Common].[CommonSequenceDetail] (
    [Id]                INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseCommonC] INT    NOT NULL,
    [IdSequense]        INT    NOT NULL,
    [IdOperatingUnit]   INT    NULL,
    [Next]              BIGINT CONSTRAINT [DF_SequenseCommonD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseCommonD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseCommonD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseCommonD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseCommonD_SequenseCommonC] FOREIGN KEY ([IdSequenseCommonC]) REFERENCES [Common].[CommonSequence] ([Id])
);


GO
ALTER TABLE [Common].[CommonSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseCommonD_OperatingUnit];


GO
ALTER TABLE [Common].[CommonSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseCommonD_Sequense];


GO
ALTER TABLE [Common].[CommonSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseCommonD_SequenseCommonC];

