CREATE TABLE [MixingStation].[BatchSerialSequence] (
    [Id]                   INT      IDENTITY (1, 1) NOT NULL,
    [BatchSerialSettingId] INT      NOT NULL,
    [Next]                 BIGINT   NOT NULL,
    [SequenceDate]         DATETIME NULL,
    CONSTRAINT [PK_BatchSerialSequence] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BatchSerialSequence_BatchSerialSettingId] FOREIGN KEY ([BatchSerialSettingId]) REFERENCES [MixingStation].[BatchSerialSetting] ([Id])
);


GO
ALTER TABLE [MixingStation].[BatchSerialSequence] NOCHECK CONSTRAINT [FK_BatchSerialSequence_BatchSerialSettingId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la secuencia numerica, se crea una secuencia por dia', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'SequenceDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Siguente Secuencia numerica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabcera de parametros de lote', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSequence', @level2type = N'COLUMN', @level2name = N'BatchSerialSettingId';

