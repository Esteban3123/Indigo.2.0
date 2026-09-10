CREATE TABLE [EHR].[SchamesDrugsRelation] (
    [ID]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdSchemesDrugs]         INT             NOT NULL,
    [DrugCode]               CHAR (20)       NOT NULL,
    [CostMinimumUnitMeasure] DECIMAL (18, 2) CONSTRAINT [DF_SchamesDrugsRelation_CostMinimumUnitMeasure] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_SchamesDrugsRelation] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_SchamesDrugsRelation_SchemesDrugs] FOREIGN KEY ([IdSchemesDrugs]) REFERENCES [EHR].[SchemesDrugs] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo por unidad mínima de medida del medicamento; valor decimal (18,2) que representa el precio unitario mínimo facturable en RIPS, recetas y facturación; vinculado al esquema de drogas y normativa de costos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'CostMinimumUnitMeasure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Costo por minima unidad de medida:', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'CostMinimumUnitMeasure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'CostMinimumUnitMeasure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento, fármaco o droga (CHAR 20); identificador único del producto farmacéutico en catálogos de inventario, recetas, prescripciones y facturación de atención en salud', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'DrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Código de drogas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'DrugCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'DrugCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del esquema de drogas (FK a SchemesDrugs); agrupa medicamentos por planes de cobertura, protocolos terapéuticos, convenios y políticas de prescripción en centros de atención y urgencias', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'IdSchemesDrugs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el IdEsquemasDrogas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'IdSchemesDrugs';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'IdSchemesDrugs';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PRIMARY KEY IDENTITY); clave consecutiva de la relación entre esquemas de medicamentos y códigos de drogas para rastreo de cobertura y costos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre esquemas de medicamentos y los fármacos que los componen, incluyendo el costo mínimo por unidad de medida de cada medicamento asociado al esquema terapéutico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchamesDrugsRelation';
