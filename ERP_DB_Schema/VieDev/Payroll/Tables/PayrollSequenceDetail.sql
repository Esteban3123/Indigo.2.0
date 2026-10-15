CREATE TABLE [Payroll].[PayrollSequenceDetail] (
    [Id]                INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PayrollSequenceId] INT    NOT NULL,
    [IdSequense]        INT    NOT NULL,
    [IdOperatingUnit]   INT    NULL,
    [Next]              BIGINT CONSTRAINT [DF_PayrollSequenceDetail_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_PayrollSequenceDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PayrollSequenceDetail_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_PayrollSequenceDetail_PayrollSequence] FOREIGN KEY ([PayrollSequenceId]) REFERENCES [Payroll].[PayrollSequence] ([Id]),
    CONSTRAINT [FK_PayrollSequenceDetail_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [Payroll].[PayrollSequenceDetail] NOCHECK CONSTRAINT [FK_PayrollSequenceDetail_OperatingUnit];


GO
ALTER TABLE [Payroll].[PayrollSequenceDetail] NOCHECK CONSTRAINT [FK_PayrollSequenceDetail_PayrollSequence];


GO
ALTER TABLE [Payroll].[PayrollSequenceDetail] NOCHECK CONSTRAINT [FK_PayrollSequenceDetail_Sequense];

