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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad para estado vencido', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'ExpiredQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad para estado por vencer', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'WarningQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad para estado vigente', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'ValidQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del tipo de leche: 1 = Refrigerada, 2 = Congelada', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'MilkTypeCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de leche (Refrigerada o Congelada)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'MilkType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de referencia a la configuración principal', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity', @level2type = N'COLUMN', @level2name = N'ParametersConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vigencia de leche materna refrigerada o congelada', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersBreastMilkValidity';

