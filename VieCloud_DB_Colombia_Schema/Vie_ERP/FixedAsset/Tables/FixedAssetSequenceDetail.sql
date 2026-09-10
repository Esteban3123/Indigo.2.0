CREATE TABLE [FixedAsset].[FixedAssetSequenceDetail] (
    [Id]                    INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSequenseFixedAssetC] INT    NOT NULL,
    [IdSequense]            INT    NOT NULL,
    [IdOperatingUnit]       INT    NULL,
    [Next]                  BIGINT CONSTRAINT [DF_SequenseFixedAssetD_Next] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_SequenseFixedAssetD] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SequenseFixedAssetD_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id]),
    CONSTRAINT [FK_SequenseFixedAssetD_Sequense] FOREIGN KEY ([IdSequense]) REFERENCES [Common].[Sequense] ([Id]),
    CONSTRAINT [FK_SequenseFixedAssetD_SequenseFixedAssetC] FOREIGN KEY ([IdSequenseFixedAssetC]) REFERENCES [FixedAsset].[FixedAssetSequence] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Siguiente número a generar en la secuencia de activos fijos (BIGINT). Valor inicial 1, incrementa con cada documento/transacción generada en esta configuración de numeración.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Siguiente numero a generar con la secuenacia', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'Next';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad operativa (centro de atención, sucursal, clínica) vinculada a la secuencia. Solo se completa cuando el ámbito es UO (Unidad Operativa); nulo si la secuencia aplica a nivel global o por contrato. FK → [Common].[OperatingUnit].', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad operativa asignada a la secuencia. Solo cuando el ambito es UO-Unidad Operativa, de lo contrario el campo es nulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la secuencia base de numeración común que define el patrón y reglas generales de generación de números. FK → [Common].[Sequense].', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la secuencia base', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/configuración principal de secuencia para activos fijos. FK → [FixedAsset].[FixedAssetSequence]. Agrupa múltiples detalles por activo o grupo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseFixedAssetC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseFixedAssetC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'IdSequenseFixedAssetC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de detalle de secuencia de activos fijos. Clave primaria clustered. PK.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las secuencias de numeración para activos fijos. Registra el siguiente número disponible en cada secuencia de consecutivos, asociada a una configuración de secuencia y a una unidad operativa.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetSequenceDetail';
