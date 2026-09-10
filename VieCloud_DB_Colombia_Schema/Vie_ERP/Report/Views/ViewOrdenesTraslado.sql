
CREATE VIEW [Report].[ViewOrdenesTraslado] AS

WITH CTE_DESTINOS AS 
 (
  SELECT 
   ROW_NUMBER () OVER(PARTITION BY HCP.IPCODPACI ORDER BY HCP.NUMEFOLIO DESC) 'NUMERO',
   HCP.NUMEFOLIO, HCP.NUMINGRES, HCP.CODDIAGNO, HCP.IPCODPACI, HCP.UFUCODIGO,  HCP.FECHISPAC, HCP.INDICAPAC,
   PRON.CODSERIPS
  FROM 
   HCHISPACA HCP --48745
   LEFT JOIN HCORDPRON PRON ON HCP.IPCODPACI = PRON.IPCODPACI AND HCP.NUMINGRES = PRON.NUMINGRES AND HCP.NUMEFOLIO = PRON.NUMEFOLIO 
  WHERE 
   INDICAPAC IN (2,3,4,5,6,8,18,19)
 )

SELECT 
 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
 CASE 
  PAC.IPTIPODOC
  WHEN 1 THEN 'CC'
  WHEN 2 THEN 'CE'
  WHEN 3 THEN 'TI'
  WHEN 4 THEN 'RC'
  WHEN 5 THEN 'PA'
  WHEN 6 THEN 'AS'
  WHEN 7 THEN 'MS'
  WHEN 8 THEN 'NU'
  WHEN 9 THEN 'CN'
  WHEN 10 THEN 'CD'
  WHEN 11 THEN 'SC'
  WHEN 12 THEN 'PE' 
  WHEN 13 THEN 'PT' 
  WHEN 14 THEN 'DE' 
  WHEN 15 THEN 'SI'
 END 'TIPO DOC',
 PRON.IPCODPACI 'ID. PACIENTE', 
 PAC.IPNOMCOMP 'NOMBRE PACIENTE', 
 CAST(PRON.FECHISPAC AS date) AS 'FECHA SOLICITUD ORDEN',
 ENT.CODENTIDA 'COD. EAPB', 
 ENT.NOMENTIDA 'NOMBRE EAPB', 
 CASE PRON.INDICAPAC 
  WHEN 2 THEN 'Trasladar a Observacion Urgencias'
  WHEN 3 THEN 'Trasladar a Hospitalizacion' 
  WHEN 4 THEN  'Trasladar a UCI Adulto'
  WHEN 5 THEN 'Trasladar a UCI Pediatrica' 
  WHEN 6 THEN 'Trasladar a UCI Neonatal'
  WHEN 8 THEN 'Trasladar a Cirugia'
  WHEN 18 THEN 'Estancia Con la Madre'
  WHEN 19 THEN 'U.Cuidado Intermedio'
  WHEN 20 THEN 'U.Basica' END 'TIPO DE TRASLADO',
  PRON.CODSERIPS 'COD. CUPS',
  CUPS.DESCODCUPS 'NOMBRE CUPS', 
  PRON.CODDIAGNO 'COD.CIE-10', 
  DIAG.NOMDIAGNO 'DIAGNOSTICO',
  PRON.UFUCODIGO 'COD. UF',
  FUN.UFUDESCRI 'UNIDAD FUNCIONAL',
  CAM.DESCCAMAS 'CAMA ESTANCIA ACTUAL',
  TIP.Nombre 'AISLAMIENTO',
  CAST(AIS.FECINIAIS as date) 'FECHA AISLAMIENTO',
  1 as 'CANTIDAD',
  CAST(PRON.FECHISPAC AS date) AS 'FECHA BUSQUEDA',
  YEAR(PRON.FECHISPAC) AS 'AÑO BUSQUEDA',
  MONTH(PRON.FECHISPAC) AS 'MES BUSQUEDA',
  CONCAT(FORMAT(MONTH(PRON.FECHISPAC), '00') ,' - ', 
	   CASE MONTH(PRON.FECHISPAC) 
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
 CTE_DESTINOS PRON
 INNER JOIN INCUPSIPS CUPS ON CUPS.CODSERIPS=PRON.CODSERIPS
 INNER JOIN INPACIENT PAC ON PAC.IPCODPACI=PRON.IPCODPACI
 INNER JOIN INENTIDAD ENT ON ENT.CODENTIDA=PAC.CODENTIDA
 INNER JOIN INDIAGNOS DIAG ON DIAG.CODDIAGNO=PRON.CODDIAGNO
 LEFT JOIN CHREGESTA EST ON PRON.IPCODPACI = EST.IPCODPACI AND EST.REGESTADO=1
 LEFT JOIN CHCAMASHO CAM ON CAM.CODICAMAS=EST.CODICAMAS
 LEFT JOIN CHREGAISL AIS ON AIS.IPCODPACI=PRON.IPCODPACI AND AIS.CODICAMAS=EST.CODICAMAS AND AIS.NUMINGRES = PRON.NUMINGRES
 LEFT JOIN CHTIPOSAISLAMIENTOS TIP ON TIP.Codigo=AIS.CODAISLAM
 LEFT JOIN INUNIFUNC FUN ON FUN.UFUCODIGO=PRON.UFUCODIGO
WHERE 
 --PRON.IPCODPACI IN ('1146147140') AND
 PRON.NUMERO=1  AND 
 PRON.CODSERIPS IN ('105M01','106M01','106M02','107M01','107M02','108A01','109A01','10A001','10A002','10A003','10A004','10A005','10B001','10B002','10B003',
                    '10B004','10M001','10M002','10M003','10M004','110A01','111A01','120N01','121B01','121M01','121M02','124P01','125A01','126A01','126A02',
					'126A03','126A04','126M01','126M02','126M03','126M04','820P01')
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a consumo en herramientas de BI/reporting que consolida las órdenes de traslado interno de pacientes hospitalizados, incluyendo únicamente el folio más reciente por paciente. Cruza datos demográficos del paciente (tipo de documento, nombre, aseguradora EAPB), el destino de traslado (observación, hospitalización, UCI adulto/pediátrica/neonatal, cirugía, entre otros), el procedimiento CUPS asociado, diagnóstico CIE-10, unidad funcional, cama actual y condición de aislamiento vigente. Filtra por un conjunto específico de códigos CUPS de traslado y agrega dimensiones temporales (año, mes) para análisis histórico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenesTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenesTraslado';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para reportería, las órdenes de traslado intrahospitalario más recientes por paciente, enriquecidas con datos demográficos, EAPB, CUPS, diagnóstico, ubicación (cama/UF) y aislamiento.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenesTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'HCHISPACA debe contener registros con INDICAPAC en (2,3,4,5,6,8,18,19) para considerarse orden de traslado; El paciente debe existir en INPACIENT y su entidad en INENTIDAD; El servicio CODSERIPS debe estar catalogado en INCUPSIPS y el diagnóstico CODDIAGNO en INDIAGNOS; Debe existir correspondencia entre HCHISPACA y HCORDPRON por IPCODPACI+NUMINGRES+NUMEFOLIO para obtener CODSERIPS', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenesTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reporta una fila por paciente (la del NUMEFOLIO más reciente); El campo CANTIDAD siempre es 1 (conteo unitario por orden); Solo se incluyen órdenes asociadas a un conjunto cerrado de códigos CUPS de hospitalización/UCI/cirugía; INDICAPAC=20 (U.Básica) está mapeado en el CASE pero queda excluido por el filtro del CTE; ID_COMPANY se deriva del nombre de la base de datos actual truncado a 9 caracteres', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenesTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de documento; EAPB (entidad responsable de pago); Orden de traslado intrahospitalario; Hospitalización; UCI Adulto/Pediátrica/Neonatal; Cuidado Intermedio; Observación de Urgencias; Cirugía; Estancia con la madre; Cama hospitalaria; Unidad funcional; Aislamiento; Diagnóstico CIE-10; Código CUPS; Folio de historia clínica; Ingreso', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenesTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewOrdenesTraslado: Devuelve solo la orden de traslado más reciente por paciente (ROW_NUMBER OVER PARTITION BY IPCODPACI ORDER BY NUMEFOLIO DESC = 1); [RETURN_RESULT] Report.ViewOrdenesTraslado: Filtra exclusivamente CODSERIPS dentro de un listado fijo de códigos CUPS de servicios de internación/UCI/cirugía (105M01, 106M01..820P01); [RETURN_RESULT] Report.ViewOrdenesTraslado: Reporta la cama y aislamiento solo del registro de estancia activo (CHREGESTA.REGESTADO=1); [RETURN_RESULT] Report.ViewOrdenesTraslado: Convierte GETDATE() a zona horaria ''Pakistan Standard Time'' para el campo ULT_ACTUAL', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenesTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PAC.IPTIPODOC (1..15) → Mapea a etiquetas de tipo de documento: 1=CC, 2=CE, 3=TI, 4=RC, 5=PA, 6=AS, 7=MS, 8=NU, 9=CN, 10=CD, 11=SC, 12=PE, 13=PT, 14=DE, 15=SI; si PRON.INDICAPAC → Determina el tipo de traslado: 2=Observación Urgencias, 3=Hospitalización, 4=UCI Adulto, 5=UCI Pediátrica, 6=UCI Neonatal, 8=Cirugía, 18=Estancia con la Madre, 19=U.Cuidado Intermedio, 20=U.Básica; si HCHISPACA.INDICAPAC IN (2,3,4,5,6,8,18,19) → Se considera registro de orden de traslado (incluido en CTE_DESTINOS) else Excluido del reporte; si PRON.NUMERO = 1 → Se selecciona únicamente la orden con folio más alto por paciente else Descartada', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenesTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.HCORDPRON; dbo.INCUPSIPS; dbo.INPACIENT; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.CHREGESTA; dbo.CHCAMASHO; dbo.CHREGAISL; dbo.CHTIPOSAISLAMIENTOS; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenesTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewOrdenesTraslado';
GO
