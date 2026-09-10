CREATE VIEW [Report].[ViewInventarioInferido]
AS
SELECT        
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
PROD.Sede, PROD.Cod_Producto, PROD.Producto, PROD.Medicamento, PROD.CodigoCUM, PROD.CodSubgrupo, PROD.TipoProducto, ALMA.Unidad, ALMA.GrupoFacturación, ALMA.ProdControl, PROD.CodATC, PROD.ATC, 
PROD.Ene_A, PROD.Feb_A, PROD.Mar_A, PROD.Abr_A, PROD.May_A, PROD.Jun_A, PROD.Jul_A, PROD.Ago_A, PROD.Sep_A, PROD.Oct_A, PROD.Nov_A, PROD.Dic_A, PROD.Ene_V, PROD.Feb_V, PROD.Mar_V, PROD.Abr_V, 
PROD.May_V, PROD.Jun_V, PROD.Jul_V, PROD.Ago_V, PROD.Sep_V, PROD.Oct_V, PROD.Nov_V, PROD.Dic_V, PROD.Prom_Ulti_Semestre, ALMA.CostoPromedio, ALMA.Ultimocosto, PROD.Estado, 
CASE WHEN  ALMA.[001] > 0 THEN ALMA.[001] END AS [001], 
CASE WHEN  ALMA.[002] > 0 THEN ALMA.[002] END AS [002], 
CASE WHEN  ALMA.[003] > 0 THEN ALMA.[003] END AS [003], 
CASE WHEN  ALMA.[004] > 0 THEN ALMA.[004] END AS [004],
CASE WHEN  ALMA.[005] > 0 THEN ALMA.[005] END AS [005],
CASE WHEN  ALMA.[006] > 0 THEN ALMA.[006] END AS [006],
CASE WHEN  ALMA.[007] > 0 THEN ALMA.[007] END AS [007],
CASE WHEN  ALMA.[008] > 0 THEN ALMA.[008] END AS [008],
CASE WHEN  ALMA.[009] > 0 THEN ALMA.[009] END AS [009],
CASE WHEN  ALMA.[010] > 0 THEN ALMA.[010] END AS [010],
CASE WHEN  ALMA.[011] > 0 THEN ALMA.[011] END AS [011],
CASE WHEN  ALMA.[012] > 0 THEN ALMA.[012] END AS [012],
CASE WHEN  ALMA.[013] > 0 THEN ALMA.[013] END AS [013],
CASE WHEN  ALMA.[014] > 0 THEN ALMA.[014] END AS [014],
CASE WHEN  ALMA.[015] > 0 THEN ALMA.[015] END AS [015],
CASE WHEN  ALMA.[016] > 0 THEN ALMA.[016] END AS [016],
CASE WHEN  ALMA.[017] > 0 THEN ALMA.[017] END AS [017],
CASE WHEN  ALMA.[018] > 0 THEN ALMA.[018] END AS [018],
CASE WHEN  ALMA.[019] > 0 THEN ALMA.[019] END AS [019],
CASE WHEN  ALMA.[020] > 0 THEN ALMA.[020] END AS [020],
CASE WHEN  ALMA.[021] > 0 THEN ALMA.[021] END AS [021],
CASE WHEN  ALMA.[022] > 0 THEN ALMA.[022] END AS [022],
CASE WHEN  ALMA.[023] > 0 THEN ALMA.[023] END AS [023],
CASE WHEN  ALMA.[024] > 0 THEN ALMA.[024] END AS [024],
CASE WHEN  ALMA.[025] > 0 THEN ALMA.[025] END AS [025],
CASE WHEN  ALMA.[026] > 0 THEN ALMA.[026] END AS [026],
CASE WHEN  ALMA.[027] > 0 THEN ALMA.[027] END AS [027],
CASE WHEN  ALMA.[028] > 0 THEN ALMA.[028] END AS [028],
CASE WHEN  ALMA.[029] > 0 THEN ALMA.[029] END AS [029],
CASE WHEN  ALMA.[030] > 0 THEN ALMA.[030] END AS [030],
CASE WHEN  ALMA.[031] > 0 THEN ALMA.[031] END AS [031],
CASE WHEN  ALMA.[032] > 0 THEN ALMA.[032] END AS [032],
1 as 'CANTIDAD',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM            
[Report].[ViewInventarioConsumoPromedio] AS PROD LEFT OUTER JOIN
[Report].[ViewInventarioSaldoAlmacenes] AS ALMA ON ALMA.Código = PROD.Cod_Producto
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida el inventario inferido de medicamentos e insumos por sede y almacén, combinando datos de consumo promedio mensual (año anterior y vigente) con saldos actuales por almacén (columnas 001–032). Filtra únicamente saldos positivos por almacén y complementa la información con clasificación ATC, CUM, costos promedio y último costo. Está diseñada para consumo en reportes multiempresa, identificando la compañía mediante `DB_NAME()`.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioInferido';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioInferido';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola fila por producto la información maestra y de consumo mensual con los saldos disponibles por almacén (001–032), costos y marca temporal, para alimentar reportes de inventario inferido.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioInferido';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las vistas Report.ViewInventarioConsumoPromedio y Report.ViewInventarioSaldoAlmacenes deben existir y ser consultables.; Report.ViewInventarioSaldoAlmacenes debe exponer columnas pivotadas por almacén nombradas [001]…[032] y la llave ''Código''.; El servidor debe soportar la zona horaria ''Pakistan Standard Time'' para el cálculo de ULT_ACTUAL.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioInferido';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de compañía siempre se obtiene del nombre de la base de datos actual truncado a 9 caracteres (CAST DB_NAME() AS VARCHAR(9)).; Los saldos por almacén ([001]…[032]) sólo se exponen cuando el valor es estrictamente mayor que 0; saldos cero o negativos se devuelven como NULL.; Cada fila reporta una unidad lógica de inventario: la columna CANTIDAD siempre vale 1 (registro contador).; La marca de última actualización se calcula con la hora actual convertida a la zona horaria ''Pakistan Standard Time''.; La unión maestro-saldos es LEFT JOIN sobre Cod_Producto = Código, por lo que los productos sin saldo en almacenes igual aparecen (con saldos NULL).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioInferido';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario; Producto/Medicamento; Saldo por almacén; Consumo promedio; Costo promedio y último costo; Clasificación ATC; Código CUM; Sede; Grupo de facturación; Producto controlado', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioInferido';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewInventarioInferido: Devuelve un set por producto cruzando ViewInventarioConsumoPromedio (PROD) con ViewInventarioSaldoAlmacenes (ALMA) vía LEFT JOIN ALMA.Código = PROD.Cod_Producto.; [RETURN_RESULT] Report.ViewInventarioInferido: Para cada columna de almacén [001]…[032]: ''CASE WHEN ALMA.[NNN] > 0 THEN ALMA.[NNN] END'' — sólo se reporta el saldo si es positivo, en otro caso se entrega NULL.; [RETURN_RESULT] Report.ViewInventarioInferido: ID_COMPANY se fija como CAST(DB_NAME() AS VARCHAR(9)), tomando el nombre de la BD actual recortado a 9 caracteres.; [RETURN_RESULT] Report.ViewInventarioInferido: ULT_ACTUAL = CONVERT(DATETIME, GETDATE() AT TIME ZONE ''Pakistan Standard Time'', 1); todas las filas reflejan el mismo timestamp ajustado a esa zona.; [RETURN_RESULT] Report.ViewInventarioInferido: Se agrega literal CANTIDAD = 1 en cada fila para permitir conteos agregados aguas abajo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioInferido';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Report.ViewInventarioConsumoPromedio; Report.ViewInventarioSaldoAlmacenes', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioInferido';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioInferido';
GO
