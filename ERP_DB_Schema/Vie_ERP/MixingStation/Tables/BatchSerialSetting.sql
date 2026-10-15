CREATE TABLE [MixingStation].[BatchSerialSetting] (
    [Id]                    INT          IDENTITY (1, 1) NOT NULL,
    [ActivateMixingStation] BIT          NOT NULL,
    [CodeMSType]            BIT          NULL,
    [ActivateUnitDoseType]  BIT          NOT NULL,
    [CodeUDTType]           BIT          NULL,
    [DateFormatType]        TINYINT      NULL,
    [SequenseId]            INT          NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    [Timestamp]             ROWVERSION   NOT NULL,
    CONSTRAINT [PK_BatchSerialSetting] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BatchSerialSetting_SequenseId] FOREIGN KEY ([SequenseId]) REFERENCES [Common].[Sequense] ([Id])
);


GO
ALTER TABLE [MixingStation].[BatchSerialSetting] NOCHECK CONSTRAINT [FK_BatchSerialSetting_SequenseId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de secuencia numerica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'SequenseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo formato de fecha :                   1-ddmmaa=  Ejemplo 06052022                   2-aammdd = Ejemplo 20220506                   3-# dias = (01/01/1900 menos CurrentDate + 1)            ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'DateFormatType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Tipo de dosis unitaria :                   0-Númerico                   1-Alfabetico', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'CodeUDTType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Activar el codigo de lote por Tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'ActivateUnitDoseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo central de mezclas :                   0-Númerico                   1-Alfabetico', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'CodeMSType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Activar el codigo de lote por central de mezclas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'ActivateMixingStation';

