CREATE TABLE [MixingStation].[ContractExternalClientsDefinitionRate] (
    [Id]                        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractExternalClientsId] INT      NOT NULL,
    [DefinitionRateId]          INT      NOT NULL,
    [InitialDate]               DATETIME NOT NULL,
    [EndDate]                   DATETIME NOT NULL,
    CONSTRAINT [PK_ContractExternalClientsDefinitionRate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractExternalClientsDefinitionRate_ContractExternalClients] FOREIGN KEY ([ContractExternalClientsId]) REFERENCES [MixingStation].[ContractExternalClients] ([Id]),
    CONSTRAINT [FK_ContractExternalClientsDefinitionRate_DefinitionRate] FOREIGN KEY ([DefinitionRateId]) REFERENCES [Contract].[DefinitionRate] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final o de vencimiento de vigencia del contrato con cliente externo y su definición de tarifa asociada (DATETIME).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'EndDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'EndDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha inicial o de inicio de vigencia del contrato con cliente externo y su definición de tarifa asociada (DATETIME).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial ', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera de definición de tarifa vinculada al contrato; referencia a Contract.DefinitionRate.Id para valores, moneda, porcentajes o escala de facturación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'DefinitionRateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la definicion de la tarifa', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'DefinitionRateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'DefinitionRateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera del contrato con cliente externo; referencia a MixingStation.ContractExternalClients.Id que almacena datos del tercero, acuerdo y condiciones comerciales.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, IDENTITY) de la relación entre contrato de cliente externo y su definición de tarifa vigente en un rango de fechas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarifas vigentes asignadas a cada contrato de clientes externos. Registra el período de validez (fecha inicial y fecha final) de cada tarifa definida para un contrato específico.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDefinitionRate';
