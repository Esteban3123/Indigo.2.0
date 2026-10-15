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


GO
CREATE NONCLUSTERED INDEX [IX_ContractSequenceDetail__ContractSequenceId]
    ON [Contract].[ContractSequenceDetail]([ContractSequenceId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractSequenceDetail', @level2type = N'COLUMN', @level2name = N'ContractSequenceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'ContractSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';

