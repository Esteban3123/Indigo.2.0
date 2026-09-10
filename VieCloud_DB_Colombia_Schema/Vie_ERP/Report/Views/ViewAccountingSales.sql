
CREATE VIEW [Report].[ViewAccountingSales] AS
     WITH CTE_AÑO_ACTUAL
          AS (SELECT ISNULL(NIT, 0) NIT, 
                     CLIENTE, 
                     CENTROCOSTO, 
                     [DESCRIPCION CENTRO COSTO], 
                     [CENTRO ATENCION], 
                     YEAR(GETDATE()) AÑO, 
                     ISNULL([1], 0) AS 'ENERO', 
                     ISNULL([2], 0) AS 'FEBRERO', 
                     ISNULL([3], 0) AS 'MARZO', 
                     ISNULL([4], 0) AS 'ABRIL', 
                     ISNULL([5], 0) AS 'MAYO', 
                     ISNULL([6], 0) AS 'JUNIO', 
                     ISNULL([7], 0) AS 'JULIO', 
                     ISNULL([8], 0) AS 'AGOSTO', 
                     ISNULL([9], 0) AS 'SEPTIEMBRE', 
                     ISNULL([10], 0) AS 'OCTUBRE', 
                     ISNULL([11], 0) AS 'NOVIEMBRE', 
                     ISNULL([12], 0) AS 'DICIEMBRE', 
                     'CONTABILIDAD' ORIGEN, 
                     'VENTAS EJECUTADAS' AS LOGICA
              FROM
              (
                  SELECT SC.MONTH AS MES, 
                         T.NIT, 
                         T.NAME AS CLIENTE, 
                         CCO.CODE AS CENTROCOSTO, 
                         CCO.NAME AS [DESCRIPCION CENTRO COSTO], 
                         BO.NAME AS [CENTRO ATENCION], --AC.DESCRIPCION AS AGRUPADOR,
                         CASE
                             WHEN SC.MONTH = '1'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '2'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '3'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '4'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '5'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '6'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '7'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '8'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '9'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '10'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '11'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '12'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                         END ACUMULADO
                  FROM GENERALLEDGER.GENERALLEDGERBALANCE AS SC WITH(NOLOCK)
                       INNER JOIN COMMON.THIRDPARTY AS T WITH(NOLOCK) ON T.ID = SC.IDTHIRDPARTY
                       LEFT JOIN PAYROLL.COSTCENTER AS CCO WITH(NOLOCK) ON CCO.ID = SC.IDCOSTCENTER
                       LEFT JOIN PAYROLL.BRANCHOFFICE AS BO WITH(NOLOCK) ON CCO.BRANCHOFFICEID = BO.ID
                       LEFT JOIN GENERALLEDGER.MAINACCOUNTS AS C WITH(NOLOCK) ON C.ID = SC.IDMAINACCOUNT --LEFT JOIN
                  --INDIGODWH.GENERALLEDGER.DWH_AGRUPADORES_CONTABILIDAD AS AC WITH (NOLOCK) ON AC.CODECC=CCO.CODE 
                  WHERE(T.PERSONTYPE = '2')
                       AND (C.LEGALBOOKID = '1')
                       AND (C.NUMBER BETWEEN '41' AND '41999999')
                       AND YEAR = YEAR(GETDATE())
                  GROUP BY T.NIT, 
                           T.NAME, 
                           CCO.CODE, 
                           CCO.NAME, 
                           SC.MONTH, 
                           SC.YEAR, 
                           BO.NAME--,AC.DESCRIPCION

                  UNION
                  SELECT SC.MONTH AS MES, 
                         0 NIT, 
                         'PERSONAS NATURALES' AS CLIENTE, 
                         CCO.CODE AS CENTROCOSTO, 
                         CCO.NAME AS [DESCRIPCION CENTRO COSTO], 
                         BO.NAME AS [CENTRO ATENCION], --AC.DESCRIPCION AS AGRUPADOR,
                         CASE
                             WHEN SC.MONTH = '1'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '2'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '3'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '4'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '5'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '6'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '7'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '8'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '9'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '10'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '11'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '12'
                                  AND SC.YEAR = YEAR(GETDATE())
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                         END ACUMULADO
                  FROM GENERALLEDGER.GENERALLEDGERBALANCE AS SC WITH(NOLOCK)
                       INNER JOIN COMMON.THIRDPARTY AS T WITH(NOLOCK) ON T.ID = SC.IDTHIRDPARTY
                       LEFT JOIN PAYROLL.COSTCENTER AS CCO WITH(NOLOCK) ON CCO.ID = SC.IDCOSTCENTER
                       LEFT JOIN PAYROLL.BRANCHOFFICE AS BO WITH(NOLOCK) ON CCO.BRANCHOFFICEID = BO.ID
                       LEFT JOIN GENERALLEDGER.MAINACCOUNTS AS C WITH(NOLOCK) ON C.ID = SC.IDMAINACCOUNT --LEFT JOIN
                  --INDIGODWH.GENERALLEDGER.DWH_AGRUPADORES_CONTABILIDAD AS AC WITH (NOLOCK) ON AC.CODECC=CCO.CODE 
                  WHERE(T.PERSONTYPE = '1')
                       AND (C.LEGALBOOKID = '1')
                       AND (C.NUMBER BETWEEN '41' AND '41999999')
                       AND YEAR = YEAR(GETDATE())
                  GROUP BY CCO.CODE, 
                           CCO.NAME, 
                           SC.MONTH, 
                           SC.YEAR, 
                           BO.NAME--,AC.DESCRIPCION
              ) DET PIVOT(SUM(ACUMULADO) FOR MES IN([1], 
                                                    [2], 
                                                    [3], 
                                                    [4], 
                                                    [5], 
                                                    [6], 
                                                    [7], 
                                                    [8], 
                                                    [9], 
                                                    [10], 
                                                    [11], 
                                                    [12])) AS PIV
              --ORDER BY 1,2,3,4,5,6
              ),
          CTE_AÑO_ANTERIOR
          AS (SELECT ISNULL(NIT, 0) NIT, 
                     CLIENTE, 
                     CENTROCOSTO, 
                     [DESCRIPCION CENTRO COSTO], 
                     [CENTRO ATENCION], 
                     YEAR(GETDATE()) - 1 AÑO, 
                     ISNULL([1], 0) AS 'ENERO', 
                     ISNULL([2], 0) AS 'FEBRERO', 
                     ISNULL([3], 0) AS 'MARZO', 
                     ISNULL([4], 0) AS 'ABRIL', 
                     ISNULL([5], 0) AS 'MAYO', 
                     ISNULL([6], 0) AS 'JUNIO', 
                     ISNULL([7], 0) AS 'JULIO', 
                     ISNULL([8], 0) AS 'AGOSTO', 
                     ISNULL([9], 0) AS 'SEPTIEMBRE', 
                     ISNULL([10], 0) AS 'OCTUBRE', 
                     ISNULL([11], 0) AS 'NOVIEMBRE', 
                     ISNULL([12], 0) AS 'DICIEMBRE', 
                     'CONTABILIDAD' ORIGEN, 
                     'VENTAS EJECUTADAS' AS LOGICA
              FROM
              (
                  SELECT SC.MONTH AS MES, 
                         T.NIT, 
                         T.NAME AS CLIENTE, 
                         CCO.CODE AS CENTROCOSTO, 
                         CCO.NAME AS [DESCRIPCION CENTRO COSTO], 
                         BO.NAME AS [CENTRO ATENCION], --AC.DESCRIPCION AS AGRUPADOR,
                         CASE
                             WHEN SC.MONTH = '1'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '2'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '3'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '4'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '5'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '6'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '7'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '8'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '9'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '10'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '11'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '12'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                         END ACUMULADO
                  FROM GENERALLEDGER.GENERALLEDGERBALANCE AS SC WITH(NOLOCK)
                       INNER JOIN COMMON.THIRDPARTY AS T WITH(NOLOCK) ON T.ID = SC.IDTHIRDPARTY
                       LEFT JOIN PAYROLL.COSTCENTER AS CCO WITH(NOLOCK) ON CCO.ID = SC.IDCOSTCENTER
                       LEFT JOIN PAYROLL.BRANCHOFFICE AS BO WITH(NOLOCK) ON CCO.BRANCHOFFICEID = BO.ID
                       LEFT JOIN GENERALLEDGER.MAINACCOUNTS AS C WITH(NOLOCK) ON C.ID = SC.IDMAINACCOUNT --LEFT JOIN
                  --INDIGODWH.GENERALLEDGER.DWH_AGRUPADORES_CONTABILIDAD AS AC WITH (NOLOCK) ON AC.CODECC=CCO.CODE 
                  WHERE(T.PERSONTYPE = '2')
                       AND (C.LEGALBOOKID = '1')
                       AND (C.NUMBER BETWEEN '41' AND '41999999')
                       AND YEAR = YEAR(GETDATE()) - 1
                  GROUP BY T.NIT, 
                           T.NAME, 
                           CCO.CODE, 
                           CCO.NAME, 
                           SC.MONTH, 
                           SC.YEAR, 
                           BO.NAME--,AC.DESCRIPCION

                  UNION
                  SELECT SC.MONTH AS MES, 
                         0 NIT, 
                         'PERSONAS NATURALES' AS CLIENTE, 
                         CCO.CODE AS CENTROCOSTO, 
                         CCO.NAME AS [DESCRIPCION CENTRO COSTO], 
                         BO.NAME AS [CENTRO ATENCION], --AC.DESCRIPCION AS AGRUPADOR,
                         CASE
                             WHEN SC.MONTH = '1'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '2'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '3'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '4'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '5'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '6'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '7'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '8'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '9'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '10'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '11'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                             WHEN SC.MONTH = '12'
                                  AND SC.YEAR = YEAR(GETDATE()) - 1
                             THEN((SUM(SC.DEBITVALUE) - SUM(SC.CREDITVALUE))) * -1
                         END ACUMULADO
                  FROM GENERALLEDGER.GENERALLEDGERBALANCE AS SC WITH(NOLOCK)
                       INNER JOIN COMMON.THIRDPARTY AS T WITH(NOLOCK) ON T.ID = SC.IDTHIRDPARTY
                       LEFT JOIN PAYROLL.COSTCENTER AS CCO WITH(NOLOCK) ON CCO.ID = SC.IDCOSTCENTER
                       LEFT JOIN PAYROLL.BRANCHOFFICE AS BO WITH(NOLOCK) ON CCO.BRANCHOFFICEID = BO.ID
                       LEFT JOIN GENERALLEDGER.MAINACCOUNTS AS C WITH(NOLOCK) ON C.ID = SC.IDMAINACCOUNT --LEFT JOIN
                  --INDIGODWH.GENERALLEDGER.DWH_AGRUPADORES_CONTABILIDAD AS AC WITH (NOLOCK) ON AC.CODECC=CCO.CODE 
                  WHERE(T.PERSONTYPE = '1')
                       AND (C.LEGALBOOKID = '1')
                       AND (C.NUMBER BETWEEN '41' AND '41999999')
                       AND YEAR = YEAR(GETDATE()) - 1
                  GROUP BY CCO.CODE, 
                           CCO.NAME, 
                           SC.MONTH, 
                           SC.YEAR, 
                           BO.NAME--,AC.DESCRIPCION
              ) DET PIVOT(SUM(ACUMULADO) FOR MES IN([1], 
                                                    [2], 
                                                    [3], 
                                                    [4], 
                                                    [5], 
                                                    [6], 
                                                    [7], 
                                                    [8], 
                                                    [9], 
                                                    [10], 
                                                    [11], 
                                                    [12])) AS PIV
              --ORDER BY 1,2,3,4,5,6
              )
          SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, *,
		         ([ENERO]+[FEBRERO]+[MARZO]+[ABRIL]+[MAYO]+[JUNIO]+[JULIO]+[AGOSTO]+[SEPTIEMBRE]+[OCTUBRE]+[NOVIEMBRE]+[DICIEMBRE]) as TOTAL,
				 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
          FROM CTE_AÑO_ANTERIOR
          UNION ALL
          SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, *,
		         ([ENERO]+[FEBRERO]+[MARZO]+[ABRIL]+[MAYO]+[JUNIO]+[JULIO]+[AGOSTO]+[SEPTIEMBRE]+[OCTUBRE]+[NOVIEMBRE]+[DICIEMBRE]) as TOTAL,				 
				 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
          FROM CTE_AÑO_ACTUAL;
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que presenta las ventas ejecutadas del año actual y del año anterior, pivoteadas por mes (enero a diciembre), obtenidas desde los saldos del libro mayor (`GENERALLEDGERBALANCE`) filtrando cuentas contables del rango 41 (ingresos) del libro legal. Diferencia terceros jurídicos (con NIT) de personas naturales, agrupando por centro de costo y centro de atención. El valor mensual se calcula como `(débitos - créditos) * -1`, propio de cuentas de ingreso con saldo crédito.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAccountingSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAccountingSales';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida y pivota las ventas ejecutadas (cuentas de ingreso 41xx del libro legal) por cliente, centro de costo y centro de atención, con desglose mensual para el año actual y el año anterior, diferenciando personas jurídicas (con NIT) de naturales (agregadas).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAccountingSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las cuentas contables deben estar parametrizadas en GENERALLEDGER.MAINACCOUNTS con LEGALBOOKID=''1'' y rango NUMBER ''41''-''41999999'' para ser consideradas ventas; Los terceros deben tener PERSONTYPE definido como ''1'' (natural) o ''2'' (jurídica) en COMMON.THIRDPARTY; Los saldos mensuales deben existir en GENERALLEDGER.GENERALLEDGERBALANCE con MONTH (1-12) y YEAR válidos; La base de datos debe estar configurada con la zona horaria ''Pakistan Standard Time'' disponible en sys.time_zone_info', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAccountingSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran movimientos de cuentas cuyo NUMBER esté entre ''41'' y ''41999999'' (cuentas de ingresos por ventas en PUC colombiano); Solo se consideran asientos del libro legal con LEGALBOOKID=''1'' (libro oficial); El valor de ventas se expresa siempre como (Crédito - Débito), invirtiendo el signo natural de las cuentas de ingreso ((D-C)*-1); Las personas naturales se consolidan siempre bajo un único cliente sintético ''PERSONAS NATURALES'' con NIT=0, sin desagregar por tercero; Se reportan exclusivamente dos años: el año en curso y el inmediatamente anterior, según YEAR(GETDATE()); Los meses sin movimiento se rellenan con 0 mediante ISNULL tras el PIVOT, garantizando 12 columnas mensuales siempre presentes; TOTAL = suma aritmética de los 12 meses de la fila; ID_COMPANY se deriva de DB_NAME() truncado a VARCHAR(9), identificando la base/empresa de origen; ULT_ACTUAL se calcula con la fecha del servidor convertida a la zona horaria ''Pakistan Standard Time''; El campo ORIGEN siempre se etiqueta como ''CONTABILIDAD'' y LOGICA como ''VENTAS EJECUTADAS''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAccountingSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ventas ejecutadas; Contabilidad; Cuentas de ingresos (clase 41); Libro legal oficial; Tercero / Cliente; Persona natural vs persona jurídica; NIT; Centro de costo; Centro de atención (sucursal); Saldo contable mensual (débitos vs créditos)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAccountingSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewAccountingSales: Devuelve filas pivotadas (ENERO..DICIEMBRE + TOTAL) por NIT/CLIENTE/CENTROCOSTO/CENTRO ATENCION para AÑO actual y AÑO anterior, filtrando cuentas ''41''-''41999999'' del libro legal ''1'' y separando PERSONTYPE 1 vs 2', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAccountingSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si T.PERSONTYPE = ''2'' (persona jurídica) → Se reportan ventas agrupadas por NIT y nombre del tercero (cliente real) else Cuando T.PERSONTYPE = ''1'' (persona natural), se agrupa todo bajo NIT=0 y CLIENTE=''PERSONAS NATURALES'' (anonimización); si SC.YEAR = YEAR(GETDATE()) → El registro se incluye en el bloque CTE_AÑO_ACTUAL etiquetado con el año actual else Si SC.YEAR = YEAR(GETDATE())-1, se incluye en CTE_AÑO_ANTERIOR etiquetado con el año previo; si Mes contable entre 1 y 12 del año vigente o anterior → Se calcula ACUMULADO = (SUM(DEBITVALUE) - SUM(CREDITVALUE)) * -1 para invertir el signo natural de cuentas de ingresos', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAccountingSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GENERALLEDGER.GENERALLEDGERBALANCE; COMMON.THIRDPARTY; PAYROLL.COSTCENTER; PAYROLL.BRANCHOFFICE; GENERALLEDGER.MAINACCOUNTS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAccountingSales';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewAccountingSales';
GO
