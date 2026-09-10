
CREATE view [Report].[ViewOrdenamientoInsumosQx] as

SELECT 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 INF.FECHAORDE FECHA_ORDEN,  
 INF.IPCODPACI AS DOCUMENTO,   
 LTRIM(RTRIM(PAC.IPNOMCOMP)) AS NOMBREPACIENTE,   
 INF.NUMINGRES AS INGRESO,   
 INF.NUMEFOLIO AS FOLIO_SOLICITUD,  
 UF.UFUDESCRI AS UNIDAD_FUNCIONAL,   
 INF.INDFARINT AS MATERIAL,  
 PRO.NOMMEDICO AS MEDICO_SOLICITA, 
 ESP.DESESPECI AS ESPECIALIDAD, 
 ENT.NOMENTIDA AS ENTIDAD,  
 CASE ING.TIPOINGRE WHEN 1 THEN 'AMBULATORIO' WHEN 2 THEN 'HOSPITALARIO' END AS TIPOINGRESO,
 T2.CODDIAGNO AS DIAGNOSTICO_PRINCIPAL, 
 ND.NOMDIAGNO AS NOMBRE_DIAGNOSTICO, 
 1 as 'CANTIDAD',
  CAST(INF.FECHAORDE AS date) AS 'FECHA BUSQUEDA',
  YEAR(INF.FECHAORDE) AS 'AÑO BUSQUEDA',
  MONTH(INF.FECHAORDE) AS 'MES BUSQUEDA',
  CONCAT(FORMAT(MONTH(INF.FECHAORDE), '00') ,' - ', 
	   CASE MONTH(INF.FECHAORDE) 
	    WHEN 1 THEN 'ENERO'
   	    WHEN 2 THEN 'FEBRERO'
	    WHEN 3 THEN 'MARZO'
	    WHEN 4 THEN 'ABRIL'
	    WHEN 5 THEN 'MAYO'
	    WHEN 6 THEN 'JUNIO'
	    WHEN 7 THEN 'JULIO'
	    WHEN 8 THEN 'AGOSTO'
	    WHEN 9 THEN 'SEPTIEMBRE'
	    WHEN 10 THEN 'OCTUBRE'
	    WHEN 11 THEN 'NOVIEMBRE'
	    WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
 DBO.HCPRESCRC AS INF  
 RIGHT JOIN  
(  
    SELECT MIN(CODCONCEC) ID, NUMINGRES,MIN(FECHAORDE) FECHA  
    FROM DBO.HCPRESCRC AS INF  
    WHERE INDFARINT <> '' AND INDFARINT IS NOT NULL   
    GROUP BY NUMINGRES  
) AS T ON INF.CODCONCEC = T.ID  
     INNER JOIN DBO.INPACIENT AS PAC ON INF.IPCODPACI = PAC.IPCODPACI  
     INNER JOIN DBO.INUNIFUNC AS UF ON INF.UFUCODIGO = UF.UFUCODIGO  
  INNER JOIN DBO.INENTIDAD AS ENT ON PAC.CODENTIDA =ENT.CODENTIDA  
  INNER JOIN DBO.INPROFSAL AS PRO ON INF.CODPROSAL=PRO.CODPROSAL  
  INNER JOIN DBO.INESPECIA AS ESP ON PRO.CODESPEC1=ESP.CODESPECI  
  INNER JOIN DBO.ADINGRESO AS ING ON ING.NUMINGRES=T.NUMINGRES  
  INNER JOIN (SELECT NUMINGRES, CODDIAGNO FROM DBO.INDIAGNOP WHERE CODDIAPRI=1) AS T2 ON T2.NUMINGRES=T.NUMINGRES  
  INNER JOIN DBO.INDIAGNOS AS ND ON T2.CODDIAGNO=ND.CODDIAGNO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida el primer ordenamiento de insumos quirúrgicos por ingreso, tomando la prescripción más temprana con material registrado. Cruza datos de paciente, unidad funcional, médico solicitante, especialidad, entidad aseguradora, tipo de ingreso (ambulatorio/hospitalario) y diagnóstico principal. Incluye campos de fecha desagregados (año, mes, nombre de mes) para facilitar análisis periódicos, con marca de última actualización en zona horaria Pakistan Standard Time.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenamientoInsumosQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenamientoInsumosQx';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la primera orden de insumos/material farmacéutico por ingreso para procedimientos quirúrgicos, enriquecida con datos del paciente, médico, especialidad, entidad y diagnóstico principal.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenamientoInsumosQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada ingreso (NUMINGRES) debe tener al menos un registro en HCPRESCRC con INDFARINT no vacío y no nulo para aparecer; El ingreso debe existir en ADINGRESO; El ingreso debe tener un diagnóstico marcado como principal (CODDIAPRI=1) en INDIAGNOP; El paciente, unidad funcional, entidad, profesional y especialidad referenciados deben existir en sus respectivos catálogos (joins INNER)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenamientoInsumosQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se selecciona una prescripción por ingreso: la del menor CODCONCEC con material no vacío; El diagnóstico reportado es siempre el principal (CODDIAPRI=1) del ingreso; ID_COMPANY se deriva del nombre de la base de datos actual truncado a 9 caracteres; La cantidad reportada es siempre 1 (constante); ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''; FECHA_ORDEN proviene de la prescripción seleccionada (no del MIN(FECHAORDE) del subquery, que se calcula pero no se usa en la salida)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenamientoInsumosQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; ingreso ambulatorio; prescripción; material/insumo farmacéutico; orden quirúrgica; unidad funcional; entidad (asegurador/pagador); profesional de salud; especialidad médica; diagnóstico principal; folio de solicitud', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenamientoInsumosQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewOrdenamientoInsumosQx: Devuelve una fila por ingreso con la prescripción de menor CODCONCEC entre las que tienen INDFARINT no vacío (MIN(CODCONCEC) agrupado por NUMINGRES)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenamientoInsumosQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ING.TIPOINGRE = 1 → Etiqueta el ingreso como ''AMBULATORIO'' else Si TIPOINGRE = 2 etiqueta como ''HOSPITALARIO''; otros valores quedan NULL; si MONTH(INF.FECHAORDE) entre 1 y 12 → Traduce el número de mes a su nombre en español (ENERO..DICIEMBRE) para el campo de búsqueda mensual; si INDFARINT <> '''' AND INDFARINT IS NOT NULL → Solo se consideran prescripciones con material/insumo farmacéutico informado para elegir la orden mínima del ingreso', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenamientoInsumosQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'DBO.HCPRESCRC; DBO.INPACIENT; DBO.INUNIFUNC; DBO.INENTIDAD; DBO.INPROFSAL; DBO.INESPECIA; DBO.ADINGRESO; DBO.INDIAGNOP; DBO.INDIAGNOS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenamientoInsumosQx';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenamientoInsumosQx';
GO
