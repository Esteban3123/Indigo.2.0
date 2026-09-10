CREATE TABLE [Inventory].[AdjustmentConceptUser] (
    [Id]                  INT          IDENTITY (1, 1) NOT NULL,
    [AdjustmentConceptId] INT          NOT NULL,
    [UserId]              INT          NOT NULL,
    [UserCode]            VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_AdjustmentConceptUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdjustmentConceptUser_AdjustmentConcept] FOREIGN KEY ([AdjustmentConceptId]) REFERENCES [Inventory].[AdjustmentConcept] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario en el sistema, identificador alfanumérico (VARCHAR 20) para búsqueda de personal de inventario o almacén', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico del usuario (INT), FK que vincula al registro de usuario en el sistema para auditoría de ajustes de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del usuario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'UserId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'UserId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto o motivo de ajuste de inventario (INT), FK a tabla AdjustmentConcept que clasifica el tipo de movimiento (entrada, salida, devolución, etc.)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'AdjustmentConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de movimiento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'AdjustmentConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'AdjustmentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria identidad (INT IDENTITY) de la relación usuario-concepto de ajuste, registro único que asocia profesionales de almacén con conceptos de movimiento permitidos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de usuarios por almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre conceptos de ajuste de inventario y los usuarios autorizados para usarlos. Controla qué usuarios tienen permiso para registrar cada tipo de ajuste en el inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AdjustmentConceptUser';
