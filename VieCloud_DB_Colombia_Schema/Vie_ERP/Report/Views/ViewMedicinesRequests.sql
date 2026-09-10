

/*******************************************************************************************************************
Nombre: [Report].[ViewMedicinesRequests]
Tipo:Vista
Observacion:Ordenamiento de medicamentos
Profesional:
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 2
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha: 25-09-2023
Ovservaciones: Se agrgan los campos de diagnosticos y la formulación manual, esto solicitado en el ticket 12546 para medici
--------------------------------------
Vercion 3
Persona que modifico: 
Observacion:
Fecha:
--***********************************************************************************************************************************/
CREATE VIEW [Report].[ViewMedicinesRequests] AS

WITH CTE_INGRESO AS
(
SELECT ING.NUMINGRES,ING.FECHEGRESO,CAM.DESCCAMAS,DIA.CODDIAGNO+' - '+DIA.NOMDIAGNO AS NOMDIAGNO
FROM 
dbo.ADINGRESO ING INNER JOIN
dbo.INDIAGNOS DIA ON ING.CODDIAING=DIA.CODDIAGNO LEFT JOIN
dbo.CHCAMASHO CAM ON ING.CODCAMACT=CAM.CODICAMAS 

),

CTE_DEVOLUCION_DISPENSACION AS
(
SELECT
DISD.Id AS DispensacionDetalleId,
DD.Code AS CodigoDevolucionDispensacion,
DD.ConfirmationDate as FechaDevolucion,
USU.NOMUSUARI AS Regente,
DDD.Quantity AS Cantidad,
--in v3
PRO.NOMMEDICO,
CASE PRO.TIPPROFES WHEN 1 THEN 'Medico General' 
				   WHEN 2 THEN 'Medico Especialista' 
				   WHEN 3 THEN 'Enfermera'
				   WHEN 4 THEN 'Auxiliar Enfermeria' 
				   WHEN 16 THEN 'Terapeuta'
				   WHEN 21 THEN 'Instrumentador' END PROFESION,
FECREGISTR
--fn v3
FROM
Inventory.PharmaceuticalDispensingDevolutionDetail DDD INNER JOIN
Inventory.PharmaceuticalDispensingDetailBatchSerial BAT ON DDD.PharmaceuticalDispensingDetailBatchSerialId=BAT.Id INNER JOIN
Inventory.PharmaceuticalDispensingDetail DISD ON BAT.PharmaceuticalDispensingDetailId=DISD.Id INNER JOIN
Inventory.PharmaceuticalDispensingDevolution DD ON DDD.PharmaceuticalDispensingDevolutionId=DD.Id AND DD.Status=2 INNER JOIN
dbo.SEGusuaru USU ON DD.ConfirmationUser=USU.CODUSUARI LEFT JOIN
--in v3
dbo.HCDEVMEDC DEV ON DD.EntityId=DEV.CODCONCEC LEFT JOIN
dbo.INPROFSAL PRO ON DEV.CODPROSAL=PRO.CODPROSAL
--fn v3
),

CTE_DETALLE_FARMACIA AS
(
select 
D.CODCONCEC,a.NUMINGRES,a.CODPRODUC,D.CODUNIMED,D.ID,
ISNULL(A.DOSISPROD,D.DOSISPROD)AS DOSISPROD,A.FRECUENCI,A.UNIFRECUE,A.DURACIDOS,D.CANPEDPRO
from 
dbo.HCFARMEPC c inner join
dbo.HCFARMEPD d on c.CODCONCEC=d.CODCONCEC inner join
dbo.HCPRESCRA  a on a.NUMINGRES= d.NUMINGRES and a.CODPRODUC=d.CODPRODUC and (c.FECHAORDE>=a.FECINIDOS and c.FECHAORDE<=a.FECFINDOS)
union all
select
D.CODCONCEC,a.NUMINGRES,a.CODPRODUC,D.CODUNIMED,D.ID,
ISNULL(A.DOSISPROD,D.DOSISPROD)AS DOSISPROD,A.FRECUENCI,A.UNIFRECUE,A.DURACIDOS,D.CANPEDPRO
from 
dbo.HCFARMEPC c inner join
dbo.HCFARMEPD d on c.CODCONCEC=d.CODCONCEC inner join
dbo.HCPRESCRA a on a.NUMINGRES= d.NUMINGRES and a.CODPRODUC=d.CODPRODUC and c.FECHAORDE>=a.FECINIDOS and a.PREESTADO!=4
),
--IN V5
CTE_ORDENAMIENTO AS(
	SELECT
		H.FECHISPAC AS FECHAORDE,
		A.NUMINGRES,
		A.NUMEFOLIO,
		A.IPCODPACI,
		H.UFUCODIGO,
		D.CODCONCEC,
		A.CODPROSAL,
		C.ORDTRANUE,
		A.CODPRODUC,
		ISNULL(A.CODUNIMED,D.CODUNIMED) AS CODUNIMED,
		A.DOSISPROD,
		A.FRECUENCI,
		A.UNIFRECUE,
		A.DURACIDOS,
		A.CANPEDPRO,
		A.DESADMINI
	FROM
	dbo.HCPRESCRA A INNER JOIN
	dbo.HCHISPACA H ON A.NUMINGRES=H.NUMINGRES AND A.NUMEFOLIO=H.NUMEFOLIO LEFT JOIN
	dbo.HCFARMEPD D ON A.ID=D.IdSourceTable AND D.SourceTable='HCHISPACA' LEFT JOIN
	dbo.HCFARMEPC C ON D.CODCONCEC=C.CODCONCEC
)
--FN V5
-------------------------------SELECT DE LOS MEDICAMENTOS------------------------------------------------------------------------------------------

SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
FAR.FECHAORDE AS [FECHA DE LA ORDEN],
CASE FAR.ORDTRANUE WHEN 1 THEN 'TRATAMIENTO NUEVO' 
				   WHEN 2 THEN 'TRATAMIENTO ANTIGUO'
				   WHEN 3 THEN 'CODIGO AZUL' ELSE 'EXTRAMURAL' END AS [TIPO DE ORDEN],
FAR.NUMINGRES AS [NUMERO DE INGRESO],
FAR.NUMEFOLIO AS [NUMERO DE FOLIO],
PAC.IPCODPACI AS [ID PACIENTE],
PAC.IPNOMCOMP AS PACIENTE,
FUN.UFUDESCRI AS [UNIDAD FUNCIONAL],
ING.DESCCAMAS AS CAMA,
ING.NOMDIAGNO AS DIAGNOSTICO,
ATC.Code AS [CODIGO DE PRODUCTO],
ATC.Name AS PRODUCTO,
GFA.Name AS [GRUPO FARMACOLOGICO],
IIF(FARD.DOSISPROD IS NULL,ISNULL(FAR.DOSISPROD,'0.00'),FARD.DOSISPROD) AS DOSIS,
UM.Name AS MEDIDA,
ISNULL(FARD.FRECUENCI,FAR.FRECUENCI) AS FRECUENCIA,
CASE ISNULL(FARD.UNIFRECUE,FAR.UNIFRECUE) WHEN 1 THEN 'MINUTOS'
										  WHEN 2 THEN 'HORAS'
										  WHEN 3 THEN 'DIAS' END AS TIEMPO,
ADM.NAME AS [VIA DE ADMINISTRACION],
ISNULL(FARD.DURACIDOS,FAR.DURACIDOS) AS DURACION,
ISNULL(FARD.CANPEDPRO,FAR.CANPEDPRO) AS [CANTIDAD FORMULADA],
FAR.DESADMINI AS [ADMINISTRACION MANUAL],
DISD.Quantity AS [CANTIDAD DISPENSADA],
DD.Cantidad AS [CANTIDAD DEVUELTA],
DIS.Code AS [CODIGO DE DISPENSACION],
DIS.ConfirmationDate AS [FECHA Y HORA DISPENSACION],
USU.NOMUSUARI AS [USUARIO CONFIRMACION],
--in v3
DD.NOMMEDICO AS [USUARIO DE DEVOLUCION],
DD.PROFESION AS [CARGO DE DEVOLUCION],
DD.FECREGISTR AS [REGISTRO DE DEVOLUCIÓN],
--fn v3
DD.CodigoDevolucionDispensacion AS [CODIGO DEVOLUCION DISPENSACION],
DD.FechaDevolucion AS [FECHA Y HORA DEVOLUCION DISPENSACION],
DD.Regente AS [USUARIO CONFIRMA DEVOLUCION],
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
							 WHEN 12 THEN 'DICIEMBRE'END) MES_LABEL_BUSQUEDA,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
CTE_ORDENAMIENTO FAR INNER JOIN
DBO.INPACIENT PAC ON FAR.IPCODPACI=PAC.IPCODPACI INNER JOIN
DBO.INUNIFUNC FUN ON FAR.UFUCODIGO=FUN.UFUCODIGO LEFT JOIN
CTE_DETALLE_FARMACIA FARD ON FAR.CODCONCEC=FARD.CODCONCEC LEFT JOIN
Inventory.ATC ATC ON ISNULL(FARD.CODPRODUC,FAR.CODPRODUC)=ATC.Code LEFT JOIN
Inventory.PharmacologicalGroup GFA ON ATC.PharmacologicalGroupId=GFA.Id LEFT JOIN
Inventory.InventoryMeasurementUnit UM ON ISNULL(FARD.CODUNIMED,FAR.CODUNIMED)=UM.Code LEFT JOIN
Inventory.AdministrationRoute ADM ON ATC.AdministrationRouteId=ADM.Id LEFT JOIN
Inventory.PharmaceuticalDispensingDetail DISD ON FARD.ID=DISD.EntityId LEFT JOIN
Inventory.PharmaceuticalDispensing DIS ON DISD.PharmaceuticalDispensingId=DIS.Id AND DIS.Status=2 LEFT JOIN
dbo.SEGusuaru USU ON DIS.ConfirmationUser=USU.CODUSUARI LEFT JOIN
dbo.INENTIDAD ENT ON PAC.CODENTIDA=ENT.CODENTIDA LEFT JOIN
CTE_INGRESO ING ON FAR.NUMINGRES=ING.NUMINGRES LEFT JOIN
dbo.INPROFSAL PRO ON FAR.CODPROSAL=PRO.CODPROSAL LEFT JOIN
CTE_DEVOLUCION_DISPENSACION DD ON DISD.Id=DD.DispensacionDetalleId  
--where FAR.FECHAORDE BETWEEN '2023-08-01' AND '2023-08-31'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting orientada a consumo analítico y operativo de farmacia hospitalaria. Consolida el ciclo completo de medicamentos prescritos durante un ingreso: prescripción médica (producto, dosis, frecuencia, duración, vía de administración y formulación manual), dispensación farmacéutica confirmada y devoluciones de medicamentos, enriquecida con datos del paciente, cama, diagnóstico de ingreso, grupo farmacológico, entidad aseguradora, profesional prescriptor y usuario dispensador. Incluye campos de fecha desagregados (año, mes, día, etiqueta de mes) para facilitar filtros y agrupaciones en herramientas de BI.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicinesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicinesRequests';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único reporte las órdenes/prescripciones de medicamentos por ingreso hospitalario con su dispensación, devolución, paciente, diagnóstico, profesional ordenante y datos farmacológicos para análisis y trazabilidad.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicinesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe correspondencia entre HCPRESCRA y HCHISPACA por NUMINGRES y NUMEFOLIO para construir el ordenamiento base.; El producto prescrito (CODPRODUC) debe existir en Inventory.ATC para enlazar grupo farmacológico y vía de administración.; Solo se consideran dispensaciones farmacéuticas con Status=2 (confirmadas) y devoluciones con Status=2.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicinesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan dispensaciones cuyo documento principal está confirmado (PharmaceuticalDispensing.Status=2) y devoluciones confirmadas (PharmaceuticalDispensingDevolution.Status=2).; El detalle de farmacia se vincula a la prescripción exigiendo que la fecha de la orden esté dentro del rango de dosis (FECINIDOS–FECFINDOS) o, alternativamente, ≥ FECINIDOS y prescripción no en estado 4.; El ordenamiento (CTE_ORDENAMIENTO) une HCFARMEPD a HCHISPACA por IdSourceTable y SourceTable=''HCHISPACA'', asegurando que el detalle farmacéutico provenga de la historia clínica.; ID_COMPANY se obtiene del nombre de la base de datos actual (DB_NAME) y se trunca a 9 caracteres.; ULT_ACTUAL refleja la fecha/hora actual convertida a la zona ''Pakistan Standard Time''.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicinesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden médica de medicamentos; Prescripción farmacéutica; Dispensación farmacéutica; Devolución de dispensación; Ingreso hospitalario; Diagnóstico (CIE-10); Cama hospitalaria; Unidad funcional; Grupo farmacológico (ATC); Vía de administración; Dosis, frecuencia y duración; Profesional de salud ordenante; Entidad pagadora / aseguradora; Grupo de atención (régimen de afiliación); Estado del paciente (hospitalizado/egresado); Tipo de orden (tratamiento nuevo, antiguo, código azul, extramural)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicinesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewMedicinesRequests: Devuelve una fila por orden de medicamento (CTE_ORDENAMIENTO) enriquecida con paciente, unidad funcional, cama, diagnóstico, dispensación y devolución.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicinesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FAR.ORDTRANUE = 1/2/3/otro → Clasifica el TIPO DE ORDEN como ''TRATAMIENTO NUEVO'', ''TRATAMIENTO ANTIGUO'', ''CODIGO AZUL'' o ''EXTRAMURAL'' respectivamente. else EXTRAMURAL; si ING.FECHEGRESO IS NULL → Estado del paciente=''HOSPITALIZADO'' else Estado=''EGRESADO''; si PAC.IPTIPOPAC entre 1 y 8 → Asigna grupo de atención: Contributivo, Subsidiado, Vinculado, Particular, Otro, Desplazado Reg. Contributivo, Desplazado Reg. Subsidiado o Desplazado No Asegurado.; si PRO.TIPPROFES en (1,2,3,4,16,21) → Determina la profesión del usuario que registró la devolución: Médico General, Especialista, Enfermera, Auxiliar Enfermería, Terapeuta o Instrumentador.; si ISNULL(FARD.UNIFRECUE,FAR.UNIFRECUE) = 1/2/3 → Traduce la unidad de frecuencia a ''MINUTOS'', ''HORAS'' o ''DIAS''.; si FARD.DOSISPROD IS NULL → Toma la dosis desde la prescripción FAR.DOSISPROD (o ''0.00'' si también es nulo). else Toma la dosis del detalle de farmacia FARD.DOSISPROD.; si En CTE_DETALLE_FARMACIA segunda rama: a.PREESTADO != 4 → Considera la prescripción si su estado no es 4 (anulada/cancelada) cuando solo se cumple FECHAORDE >= FECINIDOS.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicinesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INDIAGNOS; dbo.CHCAMASHO; Inventory.PharmaceuticalDispensingDevolutionDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensingDevolution; dbo.SEGusuaru; dbo.HCDEVMEDC; dbo.INPROFSAL; dbo.HCFARMEPC; dbo.HCFARMEPD; dbo.HCPRESCRA; dbo.HCHISPACA; dbo.INPACIENT; dbo.INUNIFUNC; Inventory.ATC; Inventory.PharmacologicalGroup; Inventory.InventoryMeasurementUnit; Inventory.AdministrationRoute; Inventory.PharmaceuticalDispensing; dbo.INENTIDAD', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicinesRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewMedicinesRequests';
GO
