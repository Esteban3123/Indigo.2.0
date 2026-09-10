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



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de secuencia numérica (FK a [Common].[Sequense]). Referencia para generar números secuenciales automáticos en lotes y códigos de la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'SequenseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de secuencia numerica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'SequenseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'SequenseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de formato de fecha para códigos de lote. TINYINT: 1=ddmmyy (ej. 060522), 2=yymmdd (ej. 220506), 3=días desde 01/01/1900 (fecha relativa). Configura cómo se representa la fecha en identificadores de lotes.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'DateFormatType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo formato de fecha :                   1-ddmmaa=  Ejemplo 06052022                   2-aammdd = Ejemplo 20220506                   3-# dias = (01/01/1900 menos CurrentDate + 1)            ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'DateFormatType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'DateFormatType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de codificación para dosis unitaria. BIT: 0=Numérico, 1=Alfabético. Define si el código de dosis unitaria usa solo números o caracteres alfanuméricos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'CodeUDTType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Tipo de dosis unitaria :                   0-Númerico                   1-Alfabetico', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'CodeUDTType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'CodeUDTType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Activar generación automática de códigos de lote según tipo de dosis unitaria. BIT booleano. Si está activo, el sistema genera códigos identificadores para cada dosis unitaria en la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'ActivateUnitDoseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Activar el codigo de lote por Tipo de dosis unitaria', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'ActivateUnitDoseType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'ActivateUnitDoseType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de codificación para central/estación de mezclas. BIT: 0=Numérico, 1=Alfabético. Define formato del código identificador de la estación de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'CodeMSType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo central de mezclas :                   0-Númerico                   1-Alfabetico', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'CodeMSType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'CodeMSType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Activar generación automática de códigos de lote por central de mezclas. BIT booleano. Si está activo, la estación de mezcla genera y asigna códigos únicos secuenciales a cada lote de medicamentos preparados.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'ActivateMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Activar el codigo de lote por central de mezclas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'ActivateMixingStation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'ActivateMixingStation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración general de la estación de mezcla (MixingStation): controla si está activa la estación de mezcla, el tipo de dosis unitaria, el formato de fecha y la secuencia de lotes y seriales utilizada en la preparación de medicamentos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de configuración.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro de configuración.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se creó el registro de configuración.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación al registro de configuración.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación al registro de configuración.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Control de concurrencia y versión del registro; se actualiza automáticamente con cada cambio.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'Timestamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'BatchSerialSetting', @level2type = N'COLUMN', @level2name = N'Timestamp';
