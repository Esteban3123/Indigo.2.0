CREATE TABLE [Inventory].[RequestParamFuncionalUnit] (
    [Id]               INT IDENTITY (1, 1) NOT NULL,
    [RequestParamId]   INT NOT NULL,
    [FunctionalUnitId] INT NOT NULL,
    CONSTRAINT [PK_RequestParamFuncionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RequestParamFuncionalUnit_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_RequestParamFuncionalUnit_RequestParam] FOREIGN KEY ([RequestParamId]) REFERENCES [Inventory].[RequestParam] ([Id])
);




GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RequestParamFuncionalUnit]
    ON [Inventory].[RequestParamFuncionalUnit]([FunctionalUnitId] ASC, [RequestParamId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (clave foránea) de la Unidad Funcional o Centro de Atención asociado al parámetro de solicitud. Tipo INT, referencias a [Payroll].[FunctionalUnit].[Id].', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit', @level2type = N'COLUMN', @level2name = N'FunctionalUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (clave foránea) del Parámetro de Solicitud o configuración de inventario vinculado. Tipo INT, referencias a [Inventory].[RequestParam].[Id].', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit', @level2type = N'COLUMN', @level2name = N'RequestParamId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del parámetro de solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit', @level2type = N'COLUMN', @level2name = N'RequestParamId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit', @level2type = N'COLUMN', @level2name = N'RequestParamId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) de la relación entre parámetro de solicitud y unidad funcional. Tipo INT, clave primaria.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los parámetros de solicitud de inventario con las unidades funcionales (servicios o áreas) autorizadas para usarlos. Permite definir qué unidades funcionales tienen acceso o aplican a cada configuración de solicitud de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParamFuncionalUnit';
