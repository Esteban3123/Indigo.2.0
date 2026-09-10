CREATE TABLE [EHR].[SchemesSuppliesDays] (
    [Id]               INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesId]        INT       NOT NULL,
    [SuppliesDrugCode] CHAR (20) NOT NULL,
    [Day]              INT       NOT NULL,
    [Quantity]         INT       NOT NULL,
    CONSTRAINT [PK_DaysAdministration] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SchemesSuppliesDays_IHLISTPRO] FOREIGN KEY ([SuppliesDrugCode]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_SchemesSuppliesDays_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de insumo o medicamento a suministrar en el día especificado del esquema de tratamiento (INT, valor numérico positivo)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Insumo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día del esquema en que se requiere el insumo (1, 8, 15, 22, etc.; INT, secuencia de administración o dispensación)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'Day';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dia en que se requiere el insumo (1-8-15-22)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'Day';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'Day';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del insumo, medicamento o producto farmacéutico (CHAR 20, referencia a IHLISTPRO.CODPRODUC; búsqueda: insumo, droga, medicamento, producto)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'SuppliesDrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del insumo - relacion con (IHLISTPRO)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'SuppliesDrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'SuppliesDrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema de tratamiento o protocolo (INT FK a EHR.Schemes; búsqueda: esquema, protocolo, plan terapéutico, régimen)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del esquema (ERH.Schemes)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable de la relación día-insumo (INT IDENTITY, clave primaria, surrogate key)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de días y cantidades de insumos o medicamentos asociados a esquemas de tratamiento. Indica cuántas unidades de cada medicamento o suministro deben administrarse en cada día del esquema terapéutico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesSuppliesDays';
