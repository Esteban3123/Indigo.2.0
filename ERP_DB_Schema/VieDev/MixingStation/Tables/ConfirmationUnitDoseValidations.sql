CREATE TABLE [MixingStation].[ConfirmationUnitDoseValidations] (
    [Id]                         VARCHAR (88) NOT NULL,
    [NPTVerified]                BIT          DEFAULT ((0)) NOT NULL,
    [SafeStatus]                 TINYINT      DEFAULT ((1)) NOT NULL,
    [ModificationDateSafeStatus] DATETIME     NULL,
    [ModificationUserSafeStatus] VARCHAR (20) NULL,
    CONSTRAINT [PK_ConfirmationUnitDoseValidations] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Verifica si la solicitud de tipo nutrición parental es aprobada', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'NPTVerified';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ConfirmationUnitDoseValidations', @level2type = N'COLUMN', @level2name = N'Id';

