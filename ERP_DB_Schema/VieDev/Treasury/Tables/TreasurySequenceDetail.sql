CREATE TABLE [Treasury].[TreasurySequenceDetail] (
    [Id]                  INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseTreasuryC] INT         NOT NULL,
    [IdSequense]          INT         NOT NULL,
    [IdOperatingUnit]     INT         NULL,
    [Next]                BIGINT      CONSTRAINT [DF_SequenseTreasuryD_Next] DEFAULT ((1)) NOT NULL,
    [Prefix]              VARCHAR (4) NULL,
    [Type]                TINYINT     NULL,
    CONSTRAINT [PK_SequenseTreasuryD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseTreasuryD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseTreasuryD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseTreasuryD_SequenseTreasuryC] FOREIGN KEY ([IdSequenseTreasuryC]) REFERENCES [Treasury].[TreasurySequence] ([Id])
);


GO
ALTER TABLE [Treasury].[TreasurySequenceDetail] NOCHECK CONSTRAINT [FK_SequenseTreasuryD_OperatingUnit];


GO
ALTER TABLE [Treasury].[TreasurySequenceDetail] NOCHECK CONSTRAINT [FK_SequenseTreasuryD_Sequense];


GO
ALTER TABLE [Treasury].[TreasurySequenceDetail] NOCHECK CONSTRAINT [FK_SequenseTreasuryD_SequenseTreasuryC];

