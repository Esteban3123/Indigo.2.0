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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseAccountingC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'TABLE', @level1name = N'GeneralLedgerSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

