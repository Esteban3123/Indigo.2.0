CREATE TABLE [GeneralLedger].[GeneralLedgerSequenceDetail] (
    [Id]                    INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseAccountingC] INT    NOT NULL,
    [IdSequense]            INT    NOT NULL,
    [IdOperatingUnit]       INT    NULL,
    [Next]                  BIGINT CONSTRAINT [DF_SequenseAccountingD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseAccountingD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseAccountingD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseAccountingD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseAccountingD_SequenseAccountingC] FOREIGN KEY ([IdSequenseAccountingC]) REFERENCES [GeneralLedger].[GeneralLedgerSequence] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseAccountingD_OperatingUnit];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseAccountingD_Sequense];


GO
ALTER TABLE [GeneralLedger].[GeneralLedgerSequenceDetail] NOCHECK CONSTRAINT [FK_SequenseAccountingD_SequenseAccountingC];

