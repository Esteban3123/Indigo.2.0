CREATE TABLE [MixingStation].[ContractExternalClientsDetail] (
    [Id]                        INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ContractExternalClientsId] INT     NOT NULL,
    [Type]                      TINYINT NOT NULL,
    [ProductId]                 INT     NULL,
    [AtcId]                     INT     NULL,
    [SupplieId]                 INT     NULL,
    [CUPSEntityId]              INT     NULL,
    [SuppliedBy]                TINYINT NOT NULL,
    CONSTRAINT [PK_ContractExternalClientsDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ContractExternalClientsDetail_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_ContractExternalClientsDetail_ContractExternalClients] FOREIGN KEY ([ContractExternalClientsId]) REFERENCES [MixingStation].[ContractExternalClients] ([Id]),
    CONSTRAINT [FK_ContractExternalClientsDetail_CUPSEntity] FOREIGN KEY ([CUPSEntityId]) REFERENCES [Contract].[CUPSEntity] ([Id]),
    CONSTRAINT [FK_ContractExternalClientsDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ContractExternalClientsDetail_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de suministrador (TINYINT): 0=No Aplica, 1=Cliente provee, 2=Cliente y Central de Mezclas. Indica quién suministra medicamentos, insumos o productos en el contrato.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'SuppliedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Suministrado por:  0. No Aplica  1. Cliente  2. Cliente y Central Mezclas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'SuppliedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'SuppliedBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del servicio CUPS (Código Único de Procedimientos en Salud), referencia a la entidad de procedimiento o servicio prestado. FK → Contract.CUPSEntity.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del servicio', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'CUPSEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del insumo, medicamento o material de consumo. FK → Inventory.InventorySupplie. Relaciona el detalle con el catálogo de insumos disponibles.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de insumo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del medicamento según clasificación ATC (Anatomical Therapeutic Chemical). FK → Inventory.ATC. Vinculación con fármaco específico en contrato.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto inventariado (medicamento, insumo u otro bien). FK → Inventory.InventoryProduct. Referencia al catálogo de productos del contrato.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de elemento contratado (TINYINT): 1=Medicamento, 2=Insumo, 3=Producto otro, 4=Servicio. Define la naturaleza del ítem en el detalle del contrato.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo:   1: Medicamento  2: Insumo  3: Producto de tipo otro  4: Servicio', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'Type';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera o contrato padre. FK → MixingStation.ContractExternalClients. Vincula cada detalle con su contrato matriz.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'ContractExternalClientsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (Identity INT) del registro de detalle en ContractExternalClientsDetail. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los ítems incluidos en contratos con clientes externos del área de preparaciones (mezclas). Registra los productos, insumos, códigos ATC o servicios CUPS pactados en cada contrato externo, junto con el tipo de ítem y quién lo suministra.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ContractExternalClientsDetail';
