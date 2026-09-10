

CREATE view [Report].[UploadCubeVieClinicalMedicalSurgicalPackages] AS

SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
PAQ.CODIGO,PAQ.NOMBRE, PROD.CODPRODUC 'CODIGO MEDICAMENTO/INSUMO',ISNULL(ATC.Name,ISS.SupplieName ) 'NOMBRE MEDICAMENTO/INSUMO' ,PROD.CANTIDAD 'CANTIDAD',
		cast(common.getdate() as date) as 'FECHA BUSQUEDA',
		CONVERT(DATETIME,COMMON.GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM DBO.AGPAQUETES PAQ
INNER JOIN DBO.AGPAQUETESD PROD ON PROD.IDAGPAQUETE=PAQ.ID
LEFT JOIN Inventory .ATC AS ATC ON ATC.Code =PROD.CODPRODUC 
LEFT JOIN Inventory .InventorySupplie  AS ISS ON ISS.Code =PROD.CODPRODUC
--where paq.CODIGO =@Codigo
--order by PAQ.CODIGO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte diseñada para alimentar un cubo analítico con los paquetes médico-quirúrgicos clínicos definidos en el sistema. Cruza la cabecera del paquete con su detalle de productos, resolviendo el nombre del medicamento o insumo ya sea desde el catálogo ATC o desde el catálogo de proveedores de inventario. Incluye la empresa (base de datos activa), código y nombre del paquete, código y cantidad del producto, y marca de tiempo de última actualización ajustada a la zona horaria de Pakistán.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSurgicalPackages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSurgicalPackages';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de medicamentos e insumos que componen cada paquete clínico médico-quirúrgico, enriquecido con nombres desde catálogos ATC e insumos, para alimentar un cubo de reporte.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSurgicalPackages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de paquetes en AGPAQUETES con sus detalles asociados en AGPAQUETESD vinculados por IDAGPAQUETE=ID; Catálogos Inventory.ATC e Inventory.InventorySupplie disponibles para resolver nombres por Code', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSurgicalPackages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila identifica la base de datos actual mediante DB_NAME() truncado a VARCHAR(9) como ID_COMPANY; La fecha de búsqueda se calcula con common.getdate() casteado a date y la última actualización se convierte a la zona horaria ''Pakistan Standard Time''; Solo se retornan productos que pertenecen a algún paquete (INNER JOIN entre AGPAQUETES y AGPAQUETESD); Las uniones con catálogos de productos son LEFT JOIN, por lo que un producto sin match en ATC ni en InventorySupplie aparece con nombre NULL pero no se descarta', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSurgicalPackages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paquete clínico médico-quirúrgico; Medicamento; Insumo; Clasificación ATC; Catálogo de insumos de inventario', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSurgicalPackages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalMedicalSurgicalPackages: Devuelve filas DISTINCT con código y nombre del paquete, código y cantidad del producto, y nombre resuelto desde ATC o (si ATC no existe) desde InventorySupplie vía ISNULL(ATC.Name, ISS.SupplieName)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSurgicalPackages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ATC.Name IS NOT NULL para PROD.CODPRODUC → Usa ATC.Name como ''NOMBRE MEDICAMENTO/INSUMO'' else Usa InventorySupplie.SupplieName como nombre del producto', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSurgicalPackages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'common.getdate', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSurgicalPackages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.AGPAQUETES; DBO.AGPAQUETESD; Inventory.ATC; Inventory.InventorySupplie', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSurgicalPackages';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalMedicalSurgicalPackages';
GO
