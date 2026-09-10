 CREATE VIEW [Report].[DefinitionRate]  AS
 SELECT *
 FROM  [Contract].[DefinitionRate]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista que expone en el esquema `Report` el catálogo maestro de definiciones de tarifas contractuales almacenado en `Contract.DefinitionRate`, facilitando el acceso a los tipos o esquemas tarifarios (SOAT, ISS, manuales u otras tarifas negociadas) desde contextos de reporte sin requerir acceso directo al esquema transaccional.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'DefinitionRate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'DefinitionRate';
GO
