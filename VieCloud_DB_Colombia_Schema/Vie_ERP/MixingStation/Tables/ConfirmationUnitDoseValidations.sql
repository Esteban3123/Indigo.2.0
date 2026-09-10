CREATE TABLE [MixingStation].[ConfirmationUnitDoseValidations] (
    [Id]                         VARCHAR (88) NOT NULL,
    [NPTVerified]                BIT          DEFAULT ((0)) NOT NULL,
    [SafeStatus]                 TINYINT      DEFAULT ((1)) NOT NULL,
    [ModificationDateSafeStatus] DATETIME     NULL,
    [ModificationUserSafeStatus] VARCHAR (20) NULL,
    CONSTRAINT [PK_ConfirmationUnitDoseValidations] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que verifica si la solicitud de Nutrición Parenteral Total (NPT) ha sido aprobada y validada en la estación de mezcla. Valores: 1=Verificada/Aprobada, 0=No verificada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'NPTVerified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Verifica si la solicitud de tipo nutrición parental es aprobada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'NPTVerified';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'NPTVerified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (VARCHAR 88) que funciona como clave primaria y consecutivo de la validación de dosis unitaria en la estación de mezcla. Referencia a la confirmación de preparación de medicamento.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Validaciones de confirmación de dosis unitaria en la estación de mezclas. Registra si una preparación (como nutrición parenteral) fue verificada y el estado de seguridad (caja fuerte/dispensador) asociado a cada dosis.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de seguridad del dispensador o caja fuerte para la dosis unitaria (por ejemplo: 1=cerrado/seguro, otros valores indican estados alterados o abiertos).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'SafeStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'SafeStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se modificó por última vez el estado de seguridad del dispensador de la dosis.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'ModificationDateSafeStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'ModificationDateSafeStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó el último cambio en el estado de seguridad del dispensador de la dosis.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'ModificationUserSafeStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'ModificationUserSafeStatus';
