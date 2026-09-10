

/*******************************************************************************************************************
Nombre: [Report].[ViewControlledMedication]
Tipo: Vista
Observacion:Registro por parte del medico cuando solicita un medicamento controlado, con su respectiva dispensación y devoluciones,
			cuando se amerita.
Profesional: Nilsson Miguel Galindo Lopez
Fecha:27-10-2023
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 2
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:27-11-2023
Observaciones: Se cambia la logica para que la tabla principal no sea la tabla dbo.HCPRODUCTOSCONTROL, si no la de dispensacion.
-------------------------------------------------------------------------------------------------------------------------------------
Version 3
Persona que modifico:
Fecha:
Observaciones:
***********************************************************************************************************************************/

CREATE VIEW [Report].[ViewControlledMedication] AS 

SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
CEN.NOMCENATE AS [CENTRO DE ATENCION],
DOC.NOMBRE AS [TIPO IDENTIFICACION],
PAC.IPCODPACI AS [IDENTIFICACION],
PAC.IPNOMCOMP AS [PACIENTE],
CASE PAC.IPSEXOPAC WHEN 1 THEN 'MASCULINO' ELSE 'FEMENINO' END AS[GENERO DEL PACIENTE],
CAST(PAC.IPFECNACI AS DATE) AS [FECHA DE NACIMIENTO],
CASE ING.CODTIPPAC WHEN '1' THEN 'Maternas' 
				   WHEN '2' THEN 'Pediatrico'
				   WHEN '3' THEN 'Población general' 
				   WHEN '4' THEN 'Adulto mayor' 
				   WHEN '5' THEN 'Población general' END AS [TIPO DE PACIENTE], 
DATEDIFF(YEAR,PAC.IPFECNACI,ING.IFECHAING)-(CASE WHEN DATEADD(YY,DATEDIFF(YEAR,IPFECNACI,ING.IFECHAING),IPFECNACI)>ING.IFECHAING THEN 1 ELSE 0 END) AS EDAD,
HEA.Code AS [CODIGO ENTIDAD],
HEA.Name AS [ENTIDAD],
CG.Name AS [GRUPO DE ATENCION],
RTRIM(UFU.UFUCODIGO)+' - '+UFU.UFUDESCRI AS [UNIDAD FUNCIONAL],
CON.NUMINGRES AS INGRESO,
CON.NUMEFOLIO AS FOLIO,
CON.INCONSECUCONTROL AS [CONSECUTIVO DE CONTROL],
CON.FECHAREGISTRO AS [FECHA SOLICITUD],
CON.CODPRODUC AS [CODIGO MEDICAMENTO],
PRO.Description AS [MEDICAMENTO],
CON.CANTIDAD AS [CANTIDAD],
IIF(DIS.Code IS NULL,IIF(ING.FECHEGRESO IS NULL,'SOLICITADO','CANCELADO'),'DISPENSADO') AS ESTADO,
RTRIM(PROM.CODPROSAL)+' - '+PROM.NOMMEDICO AS [MEDICO PRESCRIPTOR],
DIS.Code AS [CODIGO DISPENSACION],
DIS.ConfirmationDate AS [FECHA Y HORA DISPENSACION],
DISD.Quantity AS [CANTIDAD DISPENSADA],
RTRIM(USU.CODUSUARI)+' - '+USU.NOMUSUARI AS [USUARIO DISPENSACION],
DD.Code AS [CODIGO DEVOLUCIÓN],
DD.ConfirmationDate as [FECHA DEVOLUCION],
DDD.Quantity AS [CANTIDAD DEVOLUCION],
SOD.RateManualSalePrice AS [VALOR UNITARIO],
CAST(CON.FECHAREGISTRO AS date) AS 'FECHA BUSQUEDA',
YEAR(CON.FECHAREGISTRO) AS 'AÑO FECHA BUSQUEDA',
MONTH(CON.FECHAREGISTRO) AS 'MES FECHA BUSQUEDA',
CASE MONTH(CON.FECHAREGISTRO) WHEN 1 THEN 'ENERO'
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
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
Inventory.PharmaceuticalDispensing DIS
INNER JOIN Inventory.PharmaceuticalDispensingDetail DISD ON DIS.Id=DISD.PharmaceuticalDispensingId
INNER JOIN Inventory.InventoryProduct PRO ON DISD.ProductId=PRO.Id AND ProductControl=1
INNER JOIN dbo.ADINGRESO ING ON DIS.AdmissionNumber=ING.NUMINGRES
INNER JOIN dbo.ADCENATEN CEN ON ING.CODCENATE=CEN.CODCENATE 
INNER JOIN dbo.INPACIENT PAC ON ING.IPCODPACI=PAC.IPCODPACI 
LEFT JOIN dbo.ADTIPOIDENTIFICA DOC ON PAC.IPTIPODOC=DOC.CODIGO
LEFT JOIN dbo.INUNIFUNC UFU ON DISD.FunctionalUnitId=UFU.UFUCODIGO
LEFT JOIN dbo.HCFARMEPD D ON DISD.EntityId=D.ID AND DISD.EntityName='HCFARMEPD'
LEFT JOIN dbo.HCPRESCRA A ON D.IdSourceTable=A.ID AND D.SourceTable='HCPRESCRA'
LEFT JOIN dbo.HCPRODUCTOSCONTROL CON ON A.CODCONCEC=CON.CODCONCECMED AND CON.CODPRODUC=A.CODPRODUC
LEFT JOIN dbo.INPROFSAL PROM ON CON.CODPROSAL=PROM.CODPROSAL
LEFT JOIN CONTRACT.HEALTHADMINISTRATOR HEA ON CAST(ING.GENCONENTITY as varchar) = HEA.Code
LEFT JOIN Contract.CareGroup CG ON CAST(ING.GENCAREGROUP as varchar) = CG.Code
--LEFT JOIN dbo.HCPRESCRA A ON CON.CODCONCECMED=A.CODCONCEC AND CON.CODPRODUC=A.CODPRODUC
--LEFT JOIN dbo.HCFARMEPD D ON A.ID=D.IdSourceTable AND D.SourceTable='HCPRESCRA'
--LEFT JOIN Inventory.PharmaceuticalDispensingDetail DISD ON D.ID=DISD.EntityId AND DISD.EntityName='HCFARMEPD'
--LEFT JOIN Inventory.PharmaceuticalDispensing DIS ON DISD.PharmaceuticalDispensingId=DIS.Id
LEFT JOIN dbo.SEGusuaru USU ON DIS.ConfirmationUser=USU.CODUSUARI
LEFT JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial BAT ON DISD.Id=BAT.PharmaceuticalDispensingDetailId
LEFT JOIN Inventory.PharmaceuticalDispensingDevolutionDetail DDD ON BAT.Id=DDD.PharmaceuticalDispensingDetailBatchSerialId
LEFT JOIN Inventory.PharmaceuticalDispensingDevolution DD ON DDD.PharmaceuticalDispensingDevolutionId=DD.Id AND DD.Status=2
LEFT JOIN Billing.ServiceOrder SO ON DIS.Id=SO.EntityId AND SO.EntityName='PharmaceuticalDispensing'
LEFT JOIN Billing.ServiceOrderDetail SOD ON SO.Id=SOD.ServiceOrderId AND PRO.Id=SOD.ProductId
--where ING.NUMINGRES='657773'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a reporting y auditoría que consolida el ciclo completo de medicamentos controlados por paciente ingresado: desde la solicitud registrada por el médico prescriptor, pasando por la dispensación farmacéutica confirmada, hasta las devoluciones realizadas. Aplana datos demográficos del paciente, entidad pagadora, grupo de atención, unidad funcional, estado del medicamento (solicitado, dispensado o cancelado), cantidades, valor unitario y usuario dispensador, filtrando únicamente productos con control activo (`ProductControl=1`).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewControlledMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewControlledMedication';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la trazabilidad de medicamentos controlados: solicitud médica, dispensación farmacéutica, devolución y valor unitario, con datos de paciente, ingreso, entidad responsable y profesional prescriptor para reportería.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewControlledMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Inventory.PharmaceuticalDispensing debe tener detalle (DISD) y producto (PRO) asociados.; Solo se consideran productos marcados como controlados: InventoryProduct.ProductControl = 1.; El ingreso (ADINGRESO) referenciado por la dispensación debe existir, así como su centro de atención y paciente.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewControlledMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La tabla principal del flujo es Inventory.PharmaceuticalDispensing (no HCPRODUCTOSCONTROL) tras el cambio de versión 2.; Únicamente se reportan productos cuyo ProductControl = 1 (medicamentos controlados).; ID_COMPANY siempre corresponde al nombre de la base de datos actual (DB_NAME).; ULT_ACTUAL se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''.; La edad se calcula como diferencia de años entre IPFECNACI e IFECHAING ajustando si aún no se cumplió el aniversario en el año del ingreso.; Solo las devoluciones con Status=2 son consideradas; las demás aparecen como nulas en columnas de devolución.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewControlledMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento controlado; Dispensación farmacéutica; Devolución de medicamentos; Prescripción médica; Ingreso hospitalario; Paciente; Centro de atención; Unidad funcional; Entidad responsable de pago; Grupo de atención; Médico prescriptor; Tipo de paciente (Maternas, Pediátrico, Adulto mayor, Población general); Valor unitario de venta', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewControlledMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewControlledMedication: Devuelve una fila por línea de dispensación de medicamento controlado, enriquecida con prescripción, devolución y valor unitario facturado.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewControlledMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DIS.Code IS NULL AND ING.FECHEGRESO IS NULL → ESTADO = ''SOLICITADO'' else Si DIS.Code IS NULL y FECHEGRESO no es nulo => ''CANCELADO''; si DIS.Code no es nulo => ''DISPENSADO''.; si PAC.IPSEXOPAC = 1 → GENERO DEL PACIENTE = ''MASCULINO'' else ''FEMENINO''; si ING.CODTIPPAC ∈ {''1'',''2'',''3'',''4'',''5''} → Mapea a {Maternas, Pediatrico, Población general, Adulto mayor, Población general} respectivamente; si DD.Status = 2 → Solo se incluyen devoluciones con estado 2 (confirmadas) en CODIGO/FECHA/CANTIDAD DEVOLUCION; si SO.EntityName = ''PharmaceuticalDispensing'' y SOD.ProductId = PRO.Id → Toma RateManualSalePrice de la orden de servicio asociada como VALOR UNITARIO; si DISD.EntityName = ''HCFARMEPD'' y D.SourceTable = ''HCPRESCRA'' → Encadena dispensación → pedido farmacéutico → prescripción → control de productos para obtener la solicitud médica', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewControlledMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.InventoryProduct; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; dbo.INUNIFUNC; dbo.HCFARMEPD; dbo.HCPRESCRA; dbo.HCPRODUCTOSCONTROL; dbo.INPROFSAL; CONTRACT.HEALTHADMINISTRATOR; Contract.CareGroup; dbo.SEGusuaru; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PharmaceuticalDispensingDevolutionDetail; Inventory.PharmaceuticalDispensingDevolution; Billing.ServiceOrder; Billing.ServiceOrderDetail', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewControlledMedication';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewControlledMedication';
GO
