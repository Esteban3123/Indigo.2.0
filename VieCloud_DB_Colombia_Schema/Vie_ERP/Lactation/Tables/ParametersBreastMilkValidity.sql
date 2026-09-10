CREATE TABLE [Lactation].[ParametersBreastMilkValidity] (
    [Id]                        INT          IDENTITY (1, 1) NOT NULL,
    [ParametersConfigurationId] INT          NOT NULL,
    [MilkType]                  VARCHAR (20) NOT NULL,
    [MilkTypeCode]              INT          NOT NULL,
    [ValidQuantity]             INT          NOT NULL,
    [WarningQuantity]           INT          NOT NULL,
    [ExpiredQuantity]           INT          NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BreastMilkValidity_Config] FOREIGN KEY ([ParametersConfigurationId]) REFERENCES [Lactation].[ParametersConfiguration] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad umbral (días u horas) para marcar leche materna como vencida o expirada, no apta para consumo. Tipo: INT, unidad según ParametersConfiguration.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'ExpiredQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad para estado vencido', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'ExpiredQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'ExpiredQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad umbral (días u horas) que indica leche materna próxima a vencer. Estado de alerta antes de expiración. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'WarningQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad para estado por vencer', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'WarningQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'WarningQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad umbral (días u horas) durante el cual leche materna refrigerada o congelada se considera vigente y apta para usar. Tipo: INT.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'ValidQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad para estado vigente', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'ValidQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'ValidQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código numérico del tipo de leche: 1 = Refrigerada, 2 = Congelada. Identificador para clasificar parámetros de vigencia. Tipo: INT, PK correlacionado con MilkType.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'MilkTypeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del tipo de leche: 1 = Refrigerada, 2 = Congelada', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'MilkTypeCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'MilkTypeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o clasificación de conservación de leche materna: Refrigerada o Congelada. Determina parámetros de vigencia, temperatura y duración. Tipo: VARCHAR(20).', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'MilkType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de leche (Refrigerada o Congelada)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'MilkType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'MilkType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de referencia a tabla ParametersConfiguration. Vincula parámetros de vigencia a configuración principal de lactancia. Tipo: INT NOT NULL.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'ParametersConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de referencia a la configuración principal', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'ParametersConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'ParametersConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de vigencia y conservación de leche materna refrigerada o congelada. Define umbrales de cantidad (válida, por vencer, vencida) según tipo de leche y almacenamiento. Referencia a configuración principal de lactancia.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vigencia de leche materna refrigerada o congelada', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de parámetros de validez de leche materna.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'Id';
