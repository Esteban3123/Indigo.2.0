CREATE TABLE [Contract].[SettingContractCareGroupType] (
    [Id]                INT     IDENTITY (1, 1) NOT NULL,
    [SettingContractId] INT     NOT NULL,
    [CareGroupType]     TINYINT NOT NULL,
    CONSTRAINT [PK_SettingContractCareGroupType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingContractId] FOREIGN KEY ([SettingContractId]) REFERENCES [Contract].[SettingsContract] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de grupo de atención/aseguradora según localización. Colombia: 1=EAPB con contrato, 2=EAPB sin contrato, 3=Particulares, 4=Aseguradoras. Costa Rica: 1=Clientes con contrato, 2=Clientes sin contrato, 3=Particulares, 4=Aseguradoras. TINYINT, clave para identificar categoría de cobertura y relación contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType', @level2type = N'COLUMN', @level2name = N'CareGroupType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'a. localización Colombia y por defecto:  1 - EAPB con contrato.  2 - EAPB sin contrato.  3 - Particulares.  4 - Aseguradoras.    b. localización Costa Rica:  1 - Clientes con Contrato.  2 - Clientes sin Contrato.  3 - Particulares.  4 - Aseguradoras.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType', @level2type = N'COLUMN', @level2name = N'CareGroupType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType', @level2type = N'COLUMN', @level2name = N'CareGroupType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del parámetro/configuración de contrato (FK a SettingsContract.Id). Vincula la clasificación de grupo de atención a un contrato específico del sistema. INT, referencia a tabla de configuración contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType', @level2type = N'COLUMN', @level2name = N'SettingContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del parámetro de contratos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType', @level2type = N'COLUMN', @level2name = N'SettingContractId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType', @level2type = N'COLUMN', @level2name = N'SettingContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único secuencial del registro de asociación entre configuración de contrato y tipo de grupo de atención. INT IDENTITY, clave primaria del registro.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona configuraciones de contrato con los tipos de grupo de atención habilitados, determinando qué categorías de atención (hospitalización, urgencias, ambulatorio, etc.) aplican para cada configuración contractual.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType';
