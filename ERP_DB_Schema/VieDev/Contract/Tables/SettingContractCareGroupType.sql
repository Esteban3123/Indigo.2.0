CREATE TABLE [Contract].[SettingContractCareGroupType] (
    [Id]                INT     IDENTITY (1, 1) NOT NULL,
    [SettingContractId] INT     NOT NULL,
    [CareGroupType]     TINYINT NOT NULL,
    CONSTRAINT [PK_SettingContractCareGroupType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingContractId] FOREIGN KEY ([SettingContractId]) REFERENCES [Contract].[SettingsContract] ([Id])
);


GO
ALTER TABLE [Contract].[SettingContractCareGroupType] NOCHECK CONSTRAINT [FK_SettingContractId];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'a. localización Colombia y por defecto:  1 - EAPB con contrato.  2 - EAPB sin contrato.  3 - Particulares.  4 - Aseguradoras.    b. localización Costa Rica:  1 - Clientes con Contrato.  2 - Clientes sin Contrato.  3 - Particulares.  4 - Aseguradoras.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType', @level2type = N'COLUMN', @level2name = N'CareGroupType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del parámetro de contratos', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType', @level2type = N'COLUMN', @level2name = N'SettingContractId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SettingContractCareGroupType', @level2type = N'COLUMN', @level2name = N'Id';

