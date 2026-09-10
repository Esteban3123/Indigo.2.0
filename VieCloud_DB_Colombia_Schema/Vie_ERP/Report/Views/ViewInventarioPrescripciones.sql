

    /*******************************************************************************************************************
Nombre: [Report].[ViewInventarioPrescripciones]
Tipo:Vista
Observacion:ordenes y despensaciones de medicamentos e insumos.
Profesional: Nilsson Miguel Galindo Lopez
Fecha:09-08-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha: 07-09-2022
Observaciones: se agrega las solicitudes de liquidos
--------------------------------------
Version 3
Persona que modifico: Nilsson Miguel Galindo Lopez 
Observacion:Se cambia la logica de los liquidos y mezclas, ya que antes no las estaba trayendo la orden completa.
Fecha: 03-05-2023
--------------------------------------------------------------------------------------------
Version 4
Persona que modifico: Nilsson Miguel Galindo Lopez 
Observacion:Se cambia la unidad funcional, no de la orden sino de la unidad actual, esto solicitado por HOMI en el ticket: 10514
Fecha: 21-06-2023
--------------------------------------------------------------------------------------------
Version 5
Persona que modifico: Nilsson Miguel Galindo Lopez 
Observacion:Se invierte la logica del reporte, que primero no llame la tabla de ATC y luego la de PRODUCTOS, para que funcione
			en San Francisco
Fecha:20-11-2023
***********************************************************************************************************************************/
CREATE VIEW [Report].[ViewInventarioPrescripciones]
AS

WITH

CTE_INGRESO AS
(
SELECT ING.NUMINGRES,ING.FECHEGRESO,CAM.DESCCAMAS,UNF.UFUDESCRI
FROM 
dbo.ADINGRESO ING INNER JOIN
dbo.CHCAMASHO CAM ON ING.CODCAMACT=CAM.CODICAMAS /*IN V4*/ INNER JOIN
DBO.INUNIFUNC UNF ON CAM.UFUCODIGO=UNF.UFUCODIGO /*FN V4*/
WHERE ING.FECHEGRESO IS NULL 
),

--------------------------------------DATOS DE EXAMEN FISICO DEL PACIENTE------------------------------------------------------------------------
CTE_FISICO AS
(
SELECT 
CONVERT(BIGINT,FIS.PESOPACIE)/1000 AS PESO,
ING.NUMINGRES AS INGRESO
FROM
CTE_INGRESO ING INNER JOIN
dbo.HCEXFISIC FIS ON ING.NUMINGRES=FIS.NUMINGRES AND FIS.NUMEFOLIO=(SELECT MAX(FI.NUMEFOLIO) FROM dbo.HCEXFISIC FI WHERE FIS.NUMINGRES=FI.NUMINGRES AND FI.PESOPACIE IS NOT NULL)
),
--534
-------------------------------------llamar la orden inicial de los medicos para las solicitudes por parte de enfermeria---------------------------------
CTE_DETALLE_FARMACIA AS
(
select 
D.CODCONCEC,a.NUMINGRES,a.CODPRODUC,D.CODUNIMED,D.ID,
ISNULL(A.DOSISPROD,D.DOSISPROD)AS DOSISPROD,A.FRECUENCI,A.UNIFRECUE,A.DURACIDOS,D.CANPEDPRO
from 
dbo.HCFARMEPC c inner join
dbo.HCFARMEPD d on c.CODCONCEC=d.CODCONCEC inner join
dbo.HCPRESCRA  a on a.NUMINGRES= d.NUMINGRES and a.CODPRODUC=d.CODPRODUC and (c.FECHAORDE>=a.FECINIDOS and c.FECHAORDE<=a.FECFINDOS) INNER JOIN
CTE_INGRESO ING ON A.NUMINGRES=ING.NUMINGRES
union all
select
D.CODCONCEC,a.NUMINGRES,a.CODPRODUC,D.CODUNIMED,D.ID,
ISNULL(A.DOSISPROD,D.DOSISPROD)AS DOSISPROD,A.FRECUENCI,A.UNIFRECUE,A.DURACIDOS,D.CANPEDPRO
from 
dbo.HCFARMEPC c inner join
dbo.HCFARMEPD d on c.CODCONCEC=d.CODCONCEC inner join
dbo.HCPRESCRA a on a.NUMINGRES= d.NUMINGRES and a.CODPRODUC=d.CODPRODUC and c.FECHAORDE>=a.FECINIDOS and a.PREESTADO NOT IN (2,4,5,6,7)INNER JOIN
CTE_INGRESO ING ON A.NUMINGRES=ING.NUMINGRES
),
----------------------CTE PARA LOS LIQUIDOS----------------------------------------------------------------------------------
--IN V2, IN V6
CTE_LIQUIDOS AS
(
SELECT 
FAR.ID,
ISNULL(FAR.FECINIDOS,A.FECHAINIC) AS FECHA,
CASE A.PREESTADO WHEN 1 THEN 'TRATAMIENTO NUEVO'ELSE 'TRATAMIENTO ANTIGUO'END AS ESTADO,
A.NUMINGRES,
A.UFUCODIGO,
A.NUMEFOLIO,A.IPCODPACI,
FAR.CODPRODUC,
FAR.DOSISPROD AS DOSIS,
CASE WHEN D.UNIMEDBOL IS NOT NULL THEN D.UNIMEDBOL
	 WHEN D.UNIMEDINF IS NOT NULL THEN D.UNIMEDINF 
	 WHEN D.UNIMEDDIL IS NOT NULL THEN D.UNIMEDDIL END AS MEDIDA,
FAR.FRECUENCI AS FRECUENCIA,
CASE FAR.UNIFRECUE WHEN 1 THEN 'MINUTOS'
				   WHEN 2 THEN 'HORAS'
				   WHEN 3 THEN 'DIAS' END AS TIEMPO,
'INTRAVENOSA' as ADMINISTRACION,
ISNULL(D.DURACIINF,'Dosis Unica')as DURACION,
CASE D.DURACIINF WHEN 'Tratamiento Continuo' THEN DATEDIFF(DAY,A.FECHAINIC,GETDATE()) END AS TRATAMIENTO,
D.CANPROCAL AS CANTIDAD,
A.CODPROSAL AS PROFESIONAL
FROM 
dbo.HCFARMEPD FAR INNER JOIN
DBO.HCINFLIQA A ON FAR.IdSourceTable=A.CONSECUTI AND FAR.SourceTable='HCINFLIQA' INNER JOIN
DBO.HCINFLIQD D ON A.CODCONCEC=D.CODCONCEC INNER JOIN
dbo.ADINGRESO ING ON FAR.NUMINGRES=ING.NUMINGRES
)
--FN V2 FN V6

-------------------------------SELECT DE LOS MEDICAMENTOS------------------------------------------------------------------------------------------

SELECT top 1000
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
FAR.FECHAORDE AS [FECHA DE LA ORDEN],
CASE FAR.ORDTRANUE WHEN 1 THEN 'TRATAMIENTO NUEVO' 
				   WHEN 2 THEN 'TRATAMIENTO ANTIGUO'
				   WHEN 3 THEN 'CODIGO AZUL' else 'NA' END AS [TIPO DE ORDEN],
FAR.NUMINGRES AS [NUMERO DE INGRESO],
FAR.NUMEFOLIO AS [FOLIO],
PAC.IPCODPACI AS [ID PACIENTE],
PAC.IPNOMCOMP AS PACIENTE,
CAST(PAC.IPFECNACI AS DATE) AS [FECHA DE NACIMIENTO],
FIS.PESO AS [PESO (KG)],
ING.DESCCAMAS AS [CAMA ACTUAL],
ING.UFUDESCRI AS [UNIDAD FUNCIONAL],
FARD.CODPRODUC AS [CODIGO DE PRODUCTO],
INV.Name AS PRODUCTO,
TIP.Name AS [TIPO DE PRODUCTO],
GFA.Name AS [GRUPO FARMACOLOGICO],
CASE INV.POSProduct WHEN 1 THEN 'SI' ELSE 'NO' END AS PBS,
CASE ATC.Antibiotic WHEN 1 THEN 'SI' ELSE 'NO' END AS ANTIBIOTICO,
CASE INV.ProductControl WHEN 1 THEN 'SI' ELSE 'NO' END AS CONTROL,
INV.Presentation AS PRESENTACION,
ISNULL(FARD.DOSISPROD,'0.00') AS DOSIS,
UM.Name AS MEDIDA,
FARD.FRECUENCI AS FRECUENCIA,
CASE UNIFRECUE WHEN 1 THEN 'MINUTOS'
			   WHEN 2 THEN 'HORAS'
			   WHEN 3 THEN 'DIAS' END AS TIEMPO,
ADM.NAME AS [VIA DE ADMINISTRACION],
FARD.DURACIDOS AS DURACION,
CASE FARD.DURACIDOS WHEN 'Tratamiento Continuo' THEN DATEDIFF(DAY,FAR.FECHAORDE,GETDATE()) END AS [DIA DE TRATAMIENTO],
FARD.CANPEDPRO AS [CANTIDAD FORMULADA],
CASE HC.CONCILIACIONMED WHEN 'TRUE' THEN 'SI' 
						WHEN 'FALSE' THEN 'NO' END AS [CONCILIACION MEDICAMENTOSA],
DISD.Quantity AS [CANTIDAD DISPENSADA],
'' AS [CANTIDAD APLICADA],
DIS.Code AS [CODIGO DE DISPENSACION],
DIS.ConfirmationDate AS [FECHA Y HORA DISPENSACION],
USU.NOMUSUARI AS [USUARIO CONFIRMACION],
ENT.NOMENTIDA AS ENTIDAD,
CASE PAC.IPTIPOPAC WHEN 1 THEN 'Contributivo'
				   WHEN 2 THEN 'Subsidiado'
				   WHEN 3 THEN 'Vinculado'
				   WHEN 4 THEN 'Particular'
				   WHEN 5 THEN 'Otro'
				   WHEN 6 THEN 'Desplazado Reg. Contributivo'
				   WHEN 7 THEN 'Desplazado Reg. Subsidiado' 
				   WHEN 8 THEN 'Desplazado No Asegurado' END AS [GRUPO DE ATENCION],
CASE WHEN ING.FECHEGRESO IS NULL THEN 'HOSPITALIZADO' ELSE 'EGRESADO' END AS ESTADO,
PRO.NOMMEDICO AS [PROFESIONAL QUE ORDENO],
INV.ProductCost AS [COSTO PROMEDIO],
CAST(FAR.FECHAORDE AS date) AS 'FECHA BUSQUEDA',
YEAR(FAR.FECHAORDE) AS 'AÑO FECHA BUSQUEDA',
MONTH(FAR.FECHAORDE) AS 'MES AÑO FECHA BUSQUEDA',
CASE MONTH(FAR.FECHAORDE) WHEN 1 THEN 'ENERO'
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
							 WHEN 12 THEN 'DICIEMBRE' END AS 'MES NOMBRE FECHA BUSQUEDA',
FORMAT(DAY(FAR.FECHAORDE), '00') AS 'DIA FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(FAR.FECHAORDE), '00') ,' - ', 
CASE MONTH(FAR.FECHAORDE) WHEN 1 THEN 'ENERO'
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
							 WHEN 12 THEN 'DICIEMBRE'END) MES_LABEL_VENCIMIENTO,
YEAR(FAR.FECHAORDE) AS 'AÑO FECHA VENCIMIENTO',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
CTE_INGRESO ING  INNER JOIN
dbo.HCFARMEPC FAR ON ING.NUMINGRES=FAR.NUMINGRES INNER JOIN
CTE_DETALLE_FARMACIA FARD ON FAR.CODCONCEC=FARD.CODCONCEC INNER JOIN
DBO.INPACIENT PAC ON FAR.IPCODPACI=PAC.IPCODPACI
--IN V4 DBO.INUNIFUNC FUN ON FAR.UFUCODIGO=FUN.UFUCODIGO INNER JOIN FN V4
--IN V5
INNER JOIN Inventory.ATC ATC ON FARD.CODPRODUC=ATC.CODE
INNER JOIN Inventory.InventoryProduct INV ON ATC.ID=INV.ATCId
--FN V5
LEFT JOIN Inventory.ProductType TIP ON INV.ProductTypeId=TIP.Id AND TIP.Name='MEDICAMENTOS' LEFT JOIN
Inventory.PharmacologicalGroup GFA ON ATC.PharmacologicalGroupId=GFA.Id LEFT JOIN
Inventory.InventoryMeasurementUnit UM ON FARD.CODUNIMED=UM.Code LEFT JOIN
Inventory.AdministrationRoute ADM ON ATC.AdministrationRouteId=ADM.Id LEFT JOIN
Inventory.PharmaceuticalDispensingDetail DISD ON FARD.ID=DISD.EntityId LEFT JOIN
Inventory.PharmaceuticalDispensing DIS ON DISD.PharmaceuticalDispensingId=DIS.Id AND DIS.Status=2 LEFT JOIN
dbo.SEGusuaru USU ON DIS.ConfirmationUser=USU.CODUSUARI LEFT JOIN
dbo.INENTIDAD ENT ON PAC.CODENTIDA=ENT.CODENTIDA LEFT JOIN
dbo.INPROFSAL PRO ON FAR.CODPROSAL=PRO.CODPROSAL LEFT JOIN
CTE_FISICO FIS ON ING.NUMINGRES=FIS.INGRESO LEFT JOIN
dbo.HCHISPACA HC ON FAR.NUMINGRES=HC.NUMINGRES AND FAR.NUMEFOLIO=HC.NUMEFOLIO
--WHERE CODPRODUC='1103170021' --AND ING.NUMINGRES ='59571'
--where pac.ipcodpaci='1020848548' and CODPRODUC='1103170021'

UNION ALL
------------------------------------SELECT DE LOS LIQUIDOS------------------------------------------------------------
--IN V2
SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
FAR.FECHA AS [FECHA DE LA ORDEN],
FAR.ESTADO AS [TIPO DE ORDEN],
FAR.NUMINGRES AS [NUMERO DE INGRESO],
FAR.NUMEFOLIO AS [FOLIO],
PAC.IPCODPACI AS [ID PACIENTE],
PAC.IPNOMCOMP AS PACIENTE,
CAST(PAC.IPFECNACI AS DATE) AS [FECHA DE NACIMIENTO],
FIS.PESO AS [PESO (KG)],
ING.DESCCAMAS AS [CAMA ACTUAL],
ING.UFUDESCRI AS [UNIDAD FUNCIONAL],
FAR.CODPRODUC AS [CODIGO DE PRODUCTO],
INV.Name AS PRODUCTO,
'MEZCLA' AS [TIPO DE PRODUCTO],
GFA.Name AS [GRUPO FARMACOLOGICO],
CASE INV.POSProduct WHEN 1 THEN 'SI' ELSE 'NO' END AS PBS,
CASE ATC.Antibiotic WHEN 1 THEN 'SI' ELSE 'NO' END AS ANTIBIOTICO,
CASE INV.ProductControl WHEN 1 THEN 'SI' ELSE 'NO' END AS CONTROL,
INV.Presentation AS PRESENTACION,
FAR.DOSIS,
UM.Name AS MEDIDA,
FAR.FRECUENCIA,
FAR.TIEMPO,
ADM.NAME AS [VIA DE ADMINISTRACION],
FAR.DURACION,
FAR.TRATAMIENTO AS [DIA DE TRATAMIENTO],
FAR.CANTIDAD AS [CANTIDAD FORMULADA],
NULL AS [CONCILIACION MEDICAMENTOSA],
DISD.Quantity AS [CANTIDAD DISPENSADA],
'' AS [CANTIDAD APLICADA],
DIS.Code AS [CODIGO DE DISPENSACION],
DIS.ConfirmationDate AS [FECHA Y HORA DISPENSACION],
USU.NOMUSUARI AS [USUARIO CONFIRMACION],
ENT.NOMENTIDA AS ENTIDAD,
CASE PAC.IPTIPOPAC WHEN 1 THEN 'Contributivo'
				   WHEN 2 THEN 'Subsidiado'
				   WHEN 3 THEN 'Vinculado'
				   WHEN 4 THEN 'Particular'
				   WHEN 5 THEN 'Otro'
				   WHEN 6 THEN 'Desplazado Reg. Contributivo'
				   WHEN 7 THEN 'Desplazado Reg. Subsidiado' 
				   WHEN 8 THEN 'Desplazado No Asegurado' END AS [GRUPO DE ATENCION],
CASE WHEN ING.FECHEGRESO IS NULL THEN 'HOSPITALIZADO' ELSE 'EGRESADO' END AS ESTADO,
PRO.NOMMEDICO AS [PROFESIONAL QUE ORDENO],
INV.ProductCost AS [COSTO PROMEDIO],
CAST(FAR.FECHA AS date) AS 'FECHA BUSQUEDA',
YEAR(FAR.FECHA) AS 'AÑO FECHA BUSQUEDA',
MONTH(FAR.FECHA) AS 'MES AÑO FECHA BUSQUEDA',
CASE MONTH(FAR.FECHA) WHEN 1 THEN 'ENERO'
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
							 WHEN 12 THEN 'DICIEMBRE' END AS 'MES NOMBRE FECHA BUSQUEDA',
FORMAT(DAY(FAR.FECHA), '00') AS 'DIA FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(FAR.FECHA), '00') ,' - ', 
CASE MONTH(FAR.FECHA) WHEN 1 THEN 'ENERO'
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
							 WHEN 12 THEN 'DICIEMBRE'END) MES_LABEL_VENCIMIENTO,
YEAR(FAR.FECHA) AS 'AÑO FECHA VENCIMIENTO',
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
CTE_INGRESO ING 
INNER JOIN CTE_LIQUIDOS FAR ON ING.NUMINGRES=FAR.NUMINGRES
--CTE_DETALLE_FARMACIA FARD ON FAR.CODCONCEC=FARD.CODCONCEC INNER JOIN--- NO ES NECESARIO YA QUE TODO SE FILTRA EN EL CTE DE LIQUIDOS
INNER JOIN DBO.INPACIENT PAC ON FAR.IPCODPACI=PAC.IPCODPACI
-- IN V4 DBO.INUNIFUNC FUN ON FAR.UFUCODIGO=FUN.UFUCODIGO INNER JOIN FN V4
--IN V5
INNER JOIN Inventory.ATC ATC ON FAR.CODPRODUC=ATC.CODE
INNER JOIN Inventory.InventoryProduct INV ON ATC.ID=INV.ATCId
--FN V5
INNER JOIN Inventory.PharmacologicalGroup GFA ON ATC.PharmacologicalGroupId=GFA.Id
LEFT JOIN Inventory.InventoryMeasurementUnit UM ON FAR.MEDIDA=UM.Code LEFT JOIN
Inventory.AdministrationRoute ADM ON ATC.AdministrationRouteId=ADM.Id LEFT JOIN
Inventory.PharmaceuticalDispensingDetail DISD ON FAR.ID=DISD.EntityId LEFT JOIN
Inventory.PharmaceuticalDispensing DIS ON DISD.PharmaceuticalDispensingId=DIS.Id AND DIS.Status=2 LEFT JOIN
dbo.SEGusuaru USU ON DIS.ConfirmationUser=USU.CODUSUARI LEFT JOIN
dbo.INENTIDAD ENT ON PAC.CODENTIDA=ENT.CODENTIDA LEFT JOIN
dbo.INPROFSAL PRO ON FAR.PROFESIONAL=PRO.CODPROSAL LEFT JOIN
CTE_FISICO FIS ON ING.NUMINGRES=FIS.INGRESO
--FN V2
--where pac.ipcodpaci='1020848548'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida y aplana el inventario de prescripciones hospitalarias activas, uniendo órdenes de medicamentos convencionales y líquidos/mezclas intravenosas para pacientes hospitalizados. Cruza datos de admisión, cama actual, unidad funcional, prescripción médica, clasificación ATC, grupo farmacológico, dispensación farmacéutica confirmada y costos de producto, permitiendo analizar cantidades formuladas vs. dispensadas por paciente, entidad pagadora, tipo de orden, vía de administración y período de tiempo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioPrescripciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioPrescripciones';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de inventario de prescripciones que consolida órdenes de medicamentos/insumos y solicitudes de líquidos/mezclas para pacientes hospitalizados, junto con su dispensación farmacéutica y datos clínicos/administrativos asociados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioPrescripciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener un ingreso activo (FECHEGRESO IS NULL) en ADINGRESO para ser considerado en la vista; La cama actual del ingreso debe existir en CHCAMASHO y estar asociada a una unidad funcional en INUNIFUNC; Los productos de las órdenes deben existir en Inventory.ATC y tener su correspondiente registro en Inventory.InventoryProduct; Para los líquidos, FAR.IdSourceTable debe coincidir con HCINFLIQA.CONSECUTI y FAR.SourceTable debe ser ''HCINFLIQA''; Para detalle de farmacia, las prescripciones (HCPRESCRA) deben coincidir por NUMINGRES y CODPRODUC con HCFARMEPD, y la fecha de la orden debe estar dentro del rango FECINIDOS-FECFINDOS o tener estado distinto de 2,4,5,6,7', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioPrescripciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen pacientes con ingreso activo (FECHEGRESO IS NULL); El peso del paciente se toma del último examen físico (MAX(NUMEFOLIO) con PESOPACIE no nulo) y se convierte de gramos a kilogramos dividiendo por 1000; El producto debe estar clasificado en ATC antes de cruzar con InventoryProduct (orden invertido en V5 para compatibilidad con San Francisco); La unidad funcional reportada corresponde a la unidad funcional ACTUAL del paciente (vía cama actual), no la de la orden original (cambio V4 ticket 10514 HOMI); Los líquidos siempre se reportan con TIPO DE PRODUCTO = ''MEZCLA'' y VIA DE ADMINISTRACION = ''INTRAVENOSA''; El TIPO DE PRODUCTO en la rama de medicamentos solo se muestra cuando ProductType.Name = ''MEDICAMENTOS'' (LEFT JOIN filtrado); La marca temporal ULT_ACTUAL se calcula convirtiendo GETDATE() a zona horaria ''Pakistan Standard Time''; El resultado se limita a TOP 1000 filas sin ORDER BY explícito', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioPrescripciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewInventarioPrescripciones: Devuelve TOP 1000 filas combinadas vía UNION ALL: prescripciones de medicamentos (HCFARMEPC + HCFARMEPD/HCPRESCRA) y órdenes de líquidos/mezclas (HCINFLIQA + HCINFLIQD), únicamente para ingresos sin egreso', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioPrescripciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ORDTRANUE = 1 / 2 / 3 (en HCFARMEPC) → Clasifica el TIPO DE ORDEN como ''TRATAMIENTO NUEVO'', ''TRATAMIENTO ANTIGUO'' o ''CODIGO AZUL'' else ''NA''; si PREESTADO = 1 (en HCPRESCRA, rama de líquidos) → Marca el ESTADO como ''TRATAMIENTO NUEVO'' else ''TRATAMIENTO ANTIGUO''; si ING.FECHEGRESO IS NULL → Estado del paciente = ''HOSPITALIZADO'' else ''EGRESADO'' (aunque CTE_INGRESO solo trae ingresos sin egreso); si FARD.DURACIDOS = ''Tratamiento Continuo'' → Calcula DIA DE TRATAMIENTO como DATEDIFF(DAY, FECHAORDE, GETDATE()) else NULL; si Para detalle de farmacia: c.FECHAORDE entre FECINIDOS y FECFINDOS → Une la prescripción con el pedido (primera rama del UNION ALL en CTE_DETALLE_FARMACIA) else Si FECHAORDE >= FECINIDOS y PREESTADO NOT IN (2,4,5,6,7) une por la segunda rama; si D.UNIMEDBOL / UNIMEDINF / UNIMEDDIL no nulo (líquidos) → Selecciona en cascada la unidad de medida del bolo, infusión o dilución (en ese orden); si DIS.Status = 2 → Considera la dispensación farmacéutica como válida para mostrar cantidad, código y fecha de dispensación else No se trae información de dispensación (LEFT JOIN); si IPTIPOPAC entre 1 y 8 → Mapea el GRUPO DE ATENCION a Contributivo, Subsidiado, Vinculado, Particular, Otro o variantes de Desplazado; si UNIFRECUE = 1 / 2 / 3 → Mapea TIEMPO a ''MINUTOS'', ''HORAS'' o ''DIAS''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioPrescripciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.HCEXFISIC; dbo.HCFARMEPC; dbo.HCFARMEPD; dbo.HCPRESCRA; dbo.HCINFLIQA; dbo.HCINFLIQD; dbo.INPACIENT; Inventory.ATC; Inventory.InventoryProduct; Inventory.ProductType; Inventory.PharmacologicalGroup; Inventory.InventoryMeasurementUnit; Inventory.AdministrationRoute; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensing; dbo.SEGusuaru; dbo.INENTIDAD; dbo.INPROFSAL; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioPrescripciones';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioPrescripciones';
GO
