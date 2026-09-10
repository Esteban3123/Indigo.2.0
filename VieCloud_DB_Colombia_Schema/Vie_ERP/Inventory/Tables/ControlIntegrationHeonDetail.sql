CREATE TABLE [Inventory].[ControlIntegrationHeonDetail] (
    [Id]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ControlIntegrationHeonId] INT           NOT NULL,
    [MedicalOrderRecipe]       VARCHAR (20)  NOT NULL,
    [ProductCodeHeon]          VARCHAR (20)  NOT NULL,
    [TotalQuantity]            INT           NOT NULL,
    [Status]                   TINYINT       NOT NULL,
    [Message]                  VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ControlIntegrationHeonDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ControlIntegrationHeonDetail_ControlIntegrationHeon] FOREIGN KEY ([ControlIntegrationHeonId]) REFERENCES [Inventory].[ControlIntegrationHeon] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mensaje de log o error en la integración HEON; descripción del resultado del procesamiento de la orden médica en el sistema externo (VARCHAR MAX, nullable)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'Message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mensaje', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'Message';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de sincronización: 1=Correcto/Exitoso, 2=Error HEON; indica si la integración de la orden médica con inventario se completó sin novedad o con fallo en HEON (TINYINT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 Correcto - 2 Error HEON', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de unidades del producto solicitado en la orden médica/receta; suma del inventario a reservar o dispensar (INT)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'TotalQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad total', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'TotalQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'TotalQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del producto en el sistema HEON (inventario externo); referencia de medicamento, dispositivo o insumo (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'ProductCodeHeon';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de producto Heon', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'ProductCodeHeon';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'ProductCodeHeon';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación única de la orden médica o receta asociada; referencia a la prescripción del profesional de salud (VARCHAR 20)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'MedicalOrderRecipe';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de la Orden Medica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'MedicalOrderRecipe';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'MedicalOrderRecipe';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden/control de integración padre en tabla ControlIntegrationHeon; clave foránea que agrupa detalles de sincronización con HEON (INT, FK)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'ControlIntegrationHeonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de integracion de controles Heon', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'ControlIntegrationHeonId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'ControlIntegrationHeonId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (surrogate key) del detalle de integración; clave primaria de la tabla ControlIntegrationHeonDetail (INT IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la integración con el sistema Heon para inventario: registra el estado de sincronización de cada ítem (medicamento o producto) por orden médica o receta, incluyendo cantidades y mensajes de respuesta del proceso de integración.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ControlIntegrationHeonDetail';
