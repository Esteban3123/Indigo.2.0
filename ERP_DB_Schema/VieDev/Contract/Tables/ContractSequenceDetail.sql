CREATE TABLE [Contract].[ContractSequenceDetail] (
    [Id]                 INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractSequenceId] INT    NOT NULL,
    [IdSequense]         INT    NOT NULL,
    [IdOperatingUnit]    INT    NULL,
    [Next]               BIGINT CONSTRAINT [DF_ContractSequenceDetail_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_ContractSequenceDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractSequenceDetail_ContractSequence] FOREIGN KEY ([ContractSequenceId]) REFERENCES [Contract].[ContractSequence] ([Id]),
    CONSTRAINT [FK_ContractSequenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_ContractSequenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Contract].[ContractSequenceDetail] NOCHECK CONSTRAINT [FK_ContractSequenceDetail_ContractSequence];


GO
ALTER TABLE [Contract].[ContractSequenceDetail] NOCHECK CONSTRAINT [FK_ContractSequenceDetail_OperatingUnit];


GO
ALTER TABLE [Contract].[ContractSequenceDetail] NOCHECK CONSTRAINT [FK_ContractSequenceDetail_Sequense];

