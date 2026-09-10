CREATE  VIEW [Billing].[ViewOpenRevenueReport] AS
SELECT	 
		ROW_NUMBER() OVER(ORDER BY I.NUMINGRES ASC ) AS Id,
		I.NUMINGRES AS Income,
		RCD.FolioOrder,
		CASE RCD.FolioType
		WHEN 1 THEN 'EAPB con Contrato'
		WHEN 2 THEN 'EAPB sin contrato'
		WHEN 3 THEN 'Particulares'
		WHEN 4 THEN 'Aseguradoras'
		ELSE 'Desconocido'
		END AS FolioType,
		CASE RCD.Status
		WHEN 1 THEN 'Registrado'
		WHEN 2 THEN 'Facturado'
		WHEN 3 THEN 'Bloqueado'
		WHEN 4 THEN 'Anulado'
		WHEN 5 THEN 'Reconocimiento Ingresos'
		WHEN 6 THEN 'Factura Asociada'
		ELSE 'N/A'
		END AS FolioStatusName,
		CASE RCD.ResponsibleRecoveryFee
		WHEN 1 THEN 'Ninguno'
		WHEN 2 THEN 'Paciente'
		WHEN 3 THEN 'Tercero'
		ELSE 'N/A'
		END AS ResponsibleRecoveryFeeName,
		RCD.Observation AS FolioObservations,
		RCD.TotalFolio AS TotalFolio,
		IIF(CSS.Id IS NULL,'No Tiene Asociado',CONCAT(CSS.Code,'-',CSS.Name)) AS ConceptStatusFolio,
		IIF(GA.Id IS NULL, ISNULL(EA.Id, '0'), ISNULL(GA.Id, '0')) AS CareGroupId,
		IIF(GA.CODE IS NULL, ISNULL(EA.CODE, '0'), ISNULL(GA.CODE, '0')) AS CodCareGroup,
		IIF(GA.NAME IS NULL, ISNULL(EA.NAME, 'DESCONOCIDO'), ISNULL(GA.NAME, 'DESCONOCIDO')) AS CareGroup,
		ISNULL(EA.Id,0) AS EntityId,
		ISNULL(EA.NAME,'DESCONOCIDO') AS Entity,
		ISNULL(I.CODCENATE,'0') AS CodCareCenter,
		ISNULL(AD.NOMCENATE,'DESCONOCIDO') AS CareCenter,
       CASE
           WHEN I.IPCODPACI IS NULL THEN '0'
           ELSE I.IPCODPACI
       END AS Identification,
       CASE
           WHEN P.GENEXPEDITIONCITY IS NULL
                AND CI.NAME IS NULL THEN '0 - DESCONOCIDO'
           ELSE CAST(P.GENEXPEDITIONCITY AS VARCHAR(20)) + ' - ' + ISNULL(CI.NAME, '')
       END AS PlaceExpedition,
       CASE
           WHEN P.IPNOMCOMP IS NULL THEN 'DESCONOCIDO'
           ELSE P.IPNOMCOMP
       END AS Patient,
	   DATEDIFF(YEAR,P.IPFECNACI, I.IFECHAING) AS Age,
	   DGE.GRUPO_ETAREO_CICLO_VITAL AS LifeCycleEtareoGroup,
	   DGE.GRUPO_ETAREO_RES_5268 AS GroupEtareo_RES5268,
	   DGE.GRUPO_ETAREO_UPC AS GroupEtareoUPC,
       ISNULL(I.IFECHAING, TRY_CONVERT(DATETIME, '1900-12-31', 121)) DateIncome,
	   CASE I.IESTADOIN
           WHEN '  'THEN 1
           WHEN 'A' THEN 2
           WHEN 'P' THEN 3
           ELSE 0
       END AS Status,
       CASE I.IESTADOIN
           WHEN '  ' THEN 'SIN CONFIRMAR HOJA DE TRABAJO'
           WHEN 'F' THEN 'CONFIRMADA HOJA DE TRABAJO'
           WHEN 'A' THEN 'ANULADO'
           WHEN 'C' THEN 'CERRADO'
           WHEN 'P' THEN 'FACTURADO PARCIAL'
		   WHEN 'B' THEN 'BLOQUEADO'
           ELSE 'DESCONOCIDO'
       END AS StatusName,
       CASE
           WHEN UF.UFUDESCRI IS NULL THEN 'DESCONOCIDO'
           ELSE UF.UFUDESCRI
       END AS FunctionalUnit,
       ISNULL(EM.FECALTPAC, TRY_CONVERT(DATETIME, '1900-12-31', 121)) AS MedicalDischargeDate,
       CASE
           WHEN I.CODDIAING IS NULL THEN '0'
           ELSE I.CODDIAING
       END AS CIE10Income,
       CASE
           WHEN CIE10.NOMDIAGNO IS NULL THEN 'DESCONOCIDO'
           ELSE CIE10.NOMDIAGNO
       END AS AdmissionDiagnosis,
       CASE
           WHEN I.CODDIAEGR IS NULL THEN 'DESCONOCIDO'
           ELSE I.CODDIAEGR
       END AS CIE10Egress,
       CASE
           WHEN DI.NOMDIAGNO IS NULL THEN 'DESCONOCIDO'
           ELSE DI.NOMDIAGNO
       END AS EgressDiagnosis,
       ISNULL(SEG2.CODUSUARI, UU.CODUSUARI) CodCreationUser,
       ISNULL(SEG2.NOMUSUARI, UU.NOMUSUARI) CreationUser,
       ISNULL(I.FECREGCRE, TRY_CONVERT(DATETIME, '1900-12-31', 121)) CreationDate,
       CASE
           WHEN I.CODUSUMOD IS NULL THEN '0'
           ELSE I.CODUSUMOD
       END AS CodMedificationUser,
       CASE
           WHEN UU2.NOMUSUARI IS NULL THEN 'DESCONOCIDO'
           ELSE UU2.NOMUSUARI
       END AS ModificationUser,
	   ISNULL(I.FECREGMOD, TRY_CONVERT(DATETIME, '1900-12-31', 121)) AS ModificationDate,
       CASE
           WHEN D.UFUDESCRI IS NULL THEN 'DESCONOCIDO'
           ELSE D.UFUDESCRI
       END AS CurrentUnit,
       CASE
           WHEN I.IOBSERVAC IS NULL
                OR I.IOBSERVAC LIKE '' THEN 'NO APLICA'
           ELSE I.IOBSERVAC
       END AS Observation,
       CASE I.TIPOINGRE
           WHEN 1 THEN 'AMBULATORIO'
           WHEN 2 THEN 'HOSPITALARIO'
       END AS TypeIncome,
       CASE
           WHEN CIE10_ac.NOMDIAGNO IS NULL THEN 'DESCONOCIDO'
           ELSE UPPER(CIE10_ac.NOMDIAGNO)
       END AS CurrentIllness,
       CASE
           WHEN UBINOMBRE IS NULL THEN 'DESCONOCIDO'
           ELSE UBINOMBRE
       END AS Location,
       CASE
           WHEN MUNNOMBRE IS NULL THEN 'DESCONOCIDO'
           ELSE MUNNOMBRE
       END AS Municipality,
       CASE
           WHEN P.IPTELEFON IS NULL
                OR P.IPTELEFON LIKE '' THEN '0'
           ELSE P.IPTELEFON
       END AS Landline,
       CASE
           WHEN P.IPTELMOVI IS NULL
                OR P.IPTELMOVI LIKE '' THEN '0'
           ELSE P.IPTELMOVI
       END AS MobilePhone,
       CASE I.ICAUSAING
           WHEN '1' THEN 'HERIDOS EN COMBATE'
           WHEN '2' THEN 'ENFERMEDAD PROFESIONAL'
           WHEN '3' THEN 'ENFERMEDAD GENERAL ADULTO'
           WHEN '4' THEN 'ENFERMEDAD GENERAL PEDIATRIA'
           WHEN '5' THEN 'ODONTOLOGÍA'
           WHEN '6' THEN 'ACCIDENTE DE TRANSITO'
           WHEN '7' THEN 'CATASTROFE/FISALUD'
           WHEN '8' THEN 'QUEMADOS'
           WHEN '9' THEN 'MATERNIDAD'
           WHEN '10' THEN 'ACCIDENTE LABORAL'
           WHEN '11' THEN 'CIRUGIA PROGRAMADA'
           ELSE 'DESCONOCIDO'
       END AS CauseIncome,
       CASE I.ITIPORIES
           WHEN 1 THEN 'ENFERMEDAD GENERAL Y MATERNIDAD'
           WHEN 2 THEN 'ACCIDENTE DE TRANSITO'
           WHEN 3 THEN 'CATASTROFE'
           ELSE 'DESCONOCIDO'
       END AS RiskType,
       CASE MONTH(I.IFECHAING)
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
           WHEN 12 THEN 'DICIEMBRE'
           ELSE 'DESCONOCIDO'
       END MonthAdmissionDate,
       MONTH(I.IFECHAING) MonthYearEntryDate,
       CASE
           WHEN EM.FECALTPAC IS NULL THEN 'SIN ALTA MÉDICA'
           ELSE 'CON ALTA MÉDICA'
       END AS IncomeMedicalDischarge,
	   CASE I.IESTADOIN
           WHEN '  ' THEN DATEDIFF(DAY, EM.FECALTPAC, GETDATE())
           WHEN 'F' THEN 0
           WHEN 'A' THEN 0
           WHEN 'C' THEN 0
           WHEN 'P' THEN DATEDIFF(DAY, EM.FECALTPAC, GETDATE())
           ELSE 0
       END AS DaysMedicalDischarge,
	   IIF(RCD.TotalFolio > 0 ,'Con Servicio','Sin Servicio') AS StateLoads
FROM		Billing.RevenueControlDetail RCD	WITH(NOLOCK)
LEFT JOIN	Billing.RevenueControl RC			WITH(NOLOCK) ON  RCD.RevenueControlId = RC.Id
LEFT JOIN	DBO.ADINGRESO AS I					WITH(NOLOCK) ON RC.AdmissionNumber = I.NUMINGRES
LEFT JOIN	DBO.INUNIFUNC AS UF					WITH(NOLOCK) ON UF.UFUCODIGO = I.UFUCODIGO
LEFT JOIN	CONTRACT.CAREGROUP AS GA			WITH(NOLOCK) ON GA.ID = RCD.CareGroupId
LEFT JOIN	DBO.INPACIENT AS P					WITH(NOLOCK) ON P.IPCODPACI = I.IPCODPACI
LEFT JOIN	CONTRACT.HEALTHADMINISTRATOR AS EA	WITH(NOLOCK) ON EA.ID = RCD.HealthAdministratorId
LEFT JOIN	COMMON.CITY AS CI					WITH(NOLOCK) ON CI.ID = P.GENEXPEDITIONCITY
LEFT JOIN
(	SELECT	HC.NUMEFOLIO,
			HC.NUMINGRES,
			HC.IPCODPACI,
			CODDIAGNO 
	FROM	DBO.HCHISPACA HC WITH(NOLOCK)
	INNER JOIN (SELECT	MAX(NUMEFOLIO) NUMEFOLIO,
						NUMINGRES,
						IPCODPACI
				FROM	DBO.HCHISPACA WITH(NOLOCK)
				GROUP BY NUMINGRES, IPCODPACI ) HC1 ON HC.NUMINGRES=HC1.NUMINGRES AND  HC.NUMEFOLIO=HC1.NUMEFOLIO AND HC.IPCODPACI=HC1.IPCODPACI) DA ON I.NUMINGRES=DA.NUMINGRES AND I.IPCODPACI=DA.IPCODPACI --DIAGNOSTICO ACTUAL
LEFT JOIN	DBO.INDIAGNOS AS CIE10		WITH(NOLOCK) ON CIE10.CODDIAGNO = I.CODDIAING
LEFT JOIN	DBO.INDIAGNOS AS CIE10_ac	WITH(NOLOCK) ON CIE10_ac.CODDIAGNO = DA.CODDIAGNO
LEFT JOIN	DBO.ADINGRESO AS I2			WITH(NOLOCK) ON I2.NUMINGRES = I.NUMINGRES
LEFT JOIN	DBO.INUNIFUNC AS D			WITH(NOLOCK) ON I2.UFUAACTHOS = D .UFUCODIGO
LEFT JOIN	DBO.HCREGEGRE AS EM			WITH(NOLOCK)ON EM.IPCODPACI = I.IPCODPACI
AND			EM.NUMINGRES = I.NUMINGRES
LEFT JOIN
  (		SELECT	MIN(TRIANUMER) TRIANUMER,
				NUMINGRES,
				MIN(CODCONCEC) CODCONCEC
		FROM	DBO.ADTRIAGEU WITH(NOLOCK)
		WHERE CODCONCEC IS NOT NULL
		GROUP BY NUMINGRES) AS ADT ON ADT.NUMINGRES = I.NUMINGRES
LEFT JOIN	DBO.ADCONTURG	AS ADCO WITH(NOLOCK) ON ADT.CODCONCEC = ADCO.CODCONCEC
LEFT JOIN	DBO.SEGUSUARU	AS SEG2 WITH(NOLOCK) ON SEG2.CODUSUARI = ADCO.CODUSUARI
LEFT JOIN	DBO.SEGUSUARU	AS UU	WITH(NOLOCK) ON UU.CODUSUARI = I.CODUSUCRE
LEFT JOIN	DBO.SEGUSUARU	AS UU2	WITH(NOLOCK) ON UU2.CODUSUARI = I.CODUSUMOD
LEFT JOIN	DBO.INUBICACI	AS BB	WITH(NOLOCK) ON BB.AUUBICACI = P.AUUBICACI
LEFT JOIN	DBO.INMUNICIP	AS EE	WITH(NOLOCK) ON EE.DEPMUNCOD = BB.DEPMUNCOD
LEFT JOIN	DBO.INDIAGNOS	AS DI	WITH(NOLOCK) ON I.CODDIAEGR = DI.CODDIAGNO
LEFT JOIN	DBO.ADCENATEN	AS AD	WITH(NOLOCK) ON I.CODCENATE = AD.CODCENATE 
LEFT JOIN	[DBO].[HOMO_GRUPO_ETARIO] HGE WITH(NOLOCK) ON DATEDIFF(YEAR, P.IPFECNACI,I.IFECHAING) = HGE.GRUPO_ETAREO_EDAD
LEFT JOIN	[DBO].[DIM_GRUPO_ETAREO] DGE WITH(NOLOCK) ON HGE.ID_GRUPO_ETAREO = DGE.ID_GRUPO_ETAREO
LEFT JOIN	Billing.ConceptsCausesStatusFolio CSS with(NOLOCK) ON RCD.StatusFolioId = CSS.Id
WHERE I.IESTADOIN <> 'F' AND  I.IESTADOIN <> 'C'
GO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el reporte de folios de ingresos abiertos o en proceso de facturación, integrando datos del ingreso o admisión del paciente (número de ingreso, fecha, estado, tipo, causa y diagnósticos de entrada y salida), información demográfica del paciente (cédula, nombre completo, lugar de expedición del documento, edad, grupo etáreo, teléfono fijo y móvil), detalle del folio de facturación (tipo de folio, estado, observaciones, total liquidado, responsable de cuota de recuperación), grupo de atención y entidad pagadora (EPS, aseguradora u otro), unidad funcional y centro de atención, usuarios de creación y modificación, municipio y ubicación del paciente, y clasificaciones por tipo de riesgo, causa de ingreso y mes. Combina las tablas de control de ingresos y detalle de folios (RevenueControl y RevenueControlDetail) con el maestro de admisiones (ADINGRESO), historia clínica (HCHISPACA), pacientes (INPACIENT), unidades funcionales (INUNIFUNC), grupos de atención del contrato (CareGroup), administradoras de salud (HealthAdministrator) y el catálogo de ciudades (City). Está diseñada para reportería de cartera, seguimiento de facturación pendiente y auditoría de ingresos no facturados o en estado parcial, bloqueado o sin confirmar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewOpenRevenueReport';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewOpenRevenueReport';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporte consolidado de ingresos abiertos (no cerrados ni con hoja de trabajo confirmada) con su detalle de folios de facturación, datos demográficos del paciente, diagnósticos, unidad funcional y días desde el alta médica para análisis de cartera/ingresos pendientes.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros en Billing.RevenueControlDetail enlazados a Billing.RevenueControl y este último a una admisión válida en DBO.ADINGRESO vía AdmissionNumber=NUMINGRES.; El ingreso debe tener estado distinto de ''F'' (Confirmada hoja de trabajo) y distinto de ''C'' (Cerrado) para aparecer en el reporte.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ingresos abiertos: se excluyen estados ''F'' (confirmada hoja de trabajo) y ''C'' (cerrado).; Para cada admisión (NUMINGRES, IPCODPACI) se toma únicamente el último folio de historia clínica (MAX(NUMEFOLIO)) de HCHISPACA como diagnóstico actual.; El triage considerado por ingreso es el de menor TRIANUMER y menor CODCONCEC con CODCONCEC NOT NULL.; Cuando faltan datos catalogados se sustituye por ''DESCONOCIDO'', ''0'' o ''1900-12-31'' como valores por defecto, garantizando que el reporte nunca devuelva NULL en columnas descriptivas.; La edad se calcula como DATEDIFF(YEAR, IPFECNACI, IFECHAING) y se usa para enlazar el grupo etáreo (HOMO_GRUPO_ETARIO → DIM_GRUPO_ETAREO).; Si no existe grupo de atención contractual (CAREGROUP), se hereda la identidad de la entidad administradora de salud (HEALTHADMINISTRATOR) como agrupador de cuidado.; Los días desde el alta médica solo aplican para ingresos sin confirmar hoja de trabajo o facturados parcialmente; el resto reporta 0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.ViewOpenRevenueReport: Devuelve filas únicamente para ingresos cuyo IESTADOIN no sea ''F'' ni ''C'', generando un Id secuencial vía ROW_NUMBER() ordenado por NUMINGRES ascendente.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RCD.FolioType IN (1,2,3,4) → Traduce a etiquetas: 1=EAPB con Contrato, 2=EAPB sin contrato, 3=Particulares, 4=Aseguradoras else ''Desconocido''; si RCD.Status IN (1..6) → Mapea estado del folio: 1=Registrado, 2=Facturado, 3=Bloqueado, 4=Anulado, 5=Reconocimiento Ingresos, 6=Factura Asociada else ''N/A''; si RCD.ResponsibleRecoveryFee IN (1,2,3) → 1=Ninguno, 2=Paciente, 3=Tercero (responsable de la cuota de recuperación) else ''N/A''; si GA.Id IS NULL (no hay grupo de atención de contrato) → Usa los datos del HealthAdministrator (EA) como CareGroup else Usa CONTRACT.CAREGROUP (GA); si I.IESTADOIN IN (''  '',''F'',''A'',''C'',''P'',''B'') → Mapea a StatusName: ''  ''=SIN CONFIRMAR HOJA DE TRABAJO, F=CONFIRMADA, A=ANULADO, C=CERRADO, P=FACTURADO PARCIAL, B=BLOQUEADO else ''DESCONOCIDO''; si I.IESTADOIN = ''  '' o ''P'' → DaysMedicalDischarge = DATEDIFF(DAY, EM.FECALTPAC, GETDATE()) — días transcurridos desde el alta médica else 0 días; si EM.FECALTPAC IS NULL → IncomeMedicalDischarge = ''SIN ALTA MÉDICA'' else ''CON ALTA MÉDICA''; si RCD.TotalFolio > 0 → StateLoads = ''Con Servicio'' else ''Sin Servicio''; si I.TIPOINGRE = 1 / 2 → TypeIncome: 1=AMBULATORIO, 2=HOSPITALARIO else NULL; si I.ICAUSAING en ''1''..''11'' → Clasifica causa de ingreso (combate, enfermedad profesional, general adulto/pediatría, odontología, tránsito, catástrofe/FISALUD, quemados, maternidad, accidente laboral, cirugía programada) else ''DESCONOCIDO''; si I.ITIPORIES IN (1,2,3) → RiskType: 1=ENFERMEDAD GENERAL Y MATERNIDAD, 2=ACCIDENTE DE TRANSITO, 3=CATASTROFE else ''DESCONOCIDO''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.RevenueControlDetail; Billing.RevenueControl; DBO.ADINGRESO; DBO.INUNIFUNC; CONTRACT.CAREGROUP; DBO.INPACIENT; CONTRACT.HEALTHADMINISTRATOR; COMMON.CITY; DBO.HCHISPACA; DBO.INDIAGNOS; DBO.HCREGEGRE; DBO.ADTRIAGEU; DBO.ADCONTURG; DBO.SEGUSUARU; DBO.INUBICACI; DBO.INMUNICIP; DBO.ADCENATEN; DBO.HOMO_GRUPO_ETARIO; DBO.DIM_GRUPO_ETAREO; Billing.ConceptsCausesStatusFolio', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewOpenRevenueReport';
GO
