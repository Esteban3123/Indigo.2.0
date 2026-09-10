

--CREATE PROCEDURE  [dbo].[ODO_Ordenes_Extramurales_SanitasEPS_II]

--DECLARE	@ini_date DATE='2024-05-01';
--DECLARE	@end_date DATE='2024-05-05';

--AS
--BEGIN


CREATE view [Report].[UploadCubeVieClinicalSanitasEPS2ExtramuralOrders] as 

	SELECT DISTINCT  CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CAST(sips.fecordmed AS DATE) AS 'FECHA ORDEN',--[FechaOrden] 
		sips.auto AS 'NRO ORDEN',--[NroOrden]
		CASE paci.iptipodoc 
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
			WHEN 15 THEN 'SI' END AS 'TIPO IDENTIFICACION AFILIADO',--[TipoIdentificacionAfiliado]
		sips.ipcodpaci AS 'NUMERO IDENTIFICACION AFILIADO',--[NumeroIdentificacionAfiliado]
		paci.ipprinomb AS 'PRIMER NOMBRE',--[PrimerNombre]
		paci.ipsegnomb AS 'SEGUNDO NOMBRE',--[SegundoNombre]
		paci.ippriapel AS 'PRIMER APELLIDO',-- [PrimerApellido]
		paci.ipsegapel AS 'SEGUNDO APELLIDO',--[SegundoApellido]
		paci.iptelmovi AS 'TELEFONO CELULAR',--[TelefonoCelular]
		CASE 
		WHEN paci.corelepac LIKE '%none%' THEN '' ELSE RTRIM(LOWER(paci.corelepac)) END AS 'DIRECCION EMAIL',--[DireccionCorreoElectronico]
		(SELECT TOP 1 coddiagno FROM dbo.indiagnop AS indx WHERE indx.coddiapri = 1 AND numingres = sips.numingres) AS 'CODIGO DIAGNOSTICO',--[CodigoDiagnostico]
		'820001277-2' AS 'NIT PRESTADOR',--[NITPrestador]
		'35051' AS 'CODIGO PRESTADOR',--[CodigoPrestador]
		'CENTRO DE CANCEROLOGIA DE BOYACA' AS 'DESCRIPCION PRESTADOR',--[DescripcionPrestador]
		CASE per.identificationtype WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' END AS 'TIPO IDENTIFICACION MEDICO',--[TipoIdentificacionMedico] 
		per.identification AS 'NRO IDENTIFICACION MEDICO',--[NroIdentificacionMedico]
		med.nommedico AS 'NOMBRE MEDICO',--[NombreMedico]
		esp.desespeci AS 'ESPECIALIDAD REMITENTE',--[EspecialidadRemitente]
		sips.codserips AS 'CODIGO CUPS',--[CodigoCUPS]
		ISNULL([desc].name, cups.description) AS 'DESCRIPCION CUPS',--[DescripcionCUPS]
		sips.canserips AS 'CANTIDAD',--Cantidad
		sips.[CitaControl] 'CITA CONTROL',--
		sips.[DiasMesesControl] 'DIAS MESES CONTROL',--
		sips.cupscode AS 'CODIGO CUMS',--[CodigoCUMS] 
		sips.[DescripcionMedicamento] 'DESCRIPCION MEDICAMENTO',--
		sips.[CantidadMedicamento] 'CANTODAD MEDICAMENTO',--
		1 'NUMERO ENTREGAS',--[NumeroEntregas] 
		LEFT(sips.obsserips, 1000) AS 'JUSTIFICACION CLINICA',--[JustificacionClinica],
		'' AS 'OBSERVACIONES',--[Observaciones],
		'' AS 'ID TRAMITE SERVICIOS',--[IDTramiteServicio],
		'' AS 'FECHA CITA',--[FechaCita],
		'' AS 'HORA CITA',--[HoraCita],
		'' AS 'MEDICO',--[Medico],
		'Ninguna' AS 'PREPARACION',--[Preparacion]
		CAST(sips.fecordmed AS DATE) 'FECHA BUSQUEDA',
	    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
 
	FROM 
	(	
	
		--	==================================================================== Imagenes
		SELECT
			oimg.auto
			,oimg.ipcodpaci
			,oimg.numingres
			,oimg.numefolio AS folio
			,oimg.codprosal
			,oimg.fecordmed
			,oimg.codserips
			,oimg.iddescripcionrelacionada
			,oimg.canserips
			,CASE oimg.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = oimg.ipcodpaci AND numingres = oimg.numingres AND numefolio = oimg.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = oimg.ipcodpaci AND numingres = oimg.numingres AND numefolio = oimg.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = oimg.ipcodpaci AND numingres = oimg.numingres AND numefolio = oimg.numefolio) END AS obsserips
			,'No' AS [CitaControl]
			,'' AS [DiasMesesControl]
			,'' AS cupscode
			,'' AS [DescripcionMedicamento]
			,'' AS [CantidadMedicamento]		
		FROM dbo.hcordimag AS oimg
		INNER JOIN dbo.adingreso AS ing ON oimg.numingres = ing.numingres AND ing.genconentity IN (36, 177)
		WHERE oimg.estserips NOT IN(6) AND oimg.manextpro = 1 AND oimg.codserips NOT IN ('C00272') and year(oimg.fecordmed)>=2022
		--CAST(oimg.fecordmed AS DATE) BETWEEN @ini_date AND @end_date 
	
		UNION ALL
		
		--	==================================================================== Laboratorios 
		SELECT
			olab.auto 
			,olab.ipcodpaci
			,olab.numingres
			,olab.numefolio AS folio
			,olab.codprosal
			,olab.fecordmed
			,olab.codserips
			,olab.iddescripcionrelacionada AS iddescripcionrelacionada
			,olab.canserips
			,CASE olab.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = olab.ipcodpaci AND numingres = olab.numingres AND numefolio = olab.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = olab.ipcodpaci AND numingres = olab.numingres AND numefolio = olab.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = olab.ipcodpaci AND numingres = olab.numingres AND numefolio = olab.numefolio) END AS obsserips
			,'No' AS [CitaControl]
			,'' AS [DiasMesesControl]
			,'' AS cupscode
			,'' AS [DescripcionMedicamento]
			,'' AS [CantidadMedicamento]
		FROM dbo.hcordlabo AS olab
		INNER JOIN dbo.adingreso AS ing ON olab.numingres = ing.numingres AND ing.genconentity IN (36, 177)
		WHERE olab.estserips NOT IN(6) AND olab.manextpro = 1 AND olab.codserips NOT IN('906340') AND YEAR(olab.fecordmed)>=2022
		--CAST(olab.fecordmed AS DATE) BETWEEN @ini_date AND @end_date   
		
		UNION ALL

		--	==================================================================== Patologias
		SELECT
			opat.auto
			,opat.ipcodpaci
			,opat.numingres
			,opat.numefolio AS folio
			,opat.codprosal
			,opat.fecordmed
			,opat.codserips
			,NULL AS iddescripcionrelacionada
			,opat.canserips
			,CASE opat.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = opat.ipcodpaci AND numingres = opat.numingres AND numefolio = opat.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = opat.ipcodpaci AND numingres = opat.numingres AND numefolio = opat.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = opat.ipcodpaci AND numingres = opat.numingres AND numefolio = opat.numefolio)
			END AS obsserips
			,'No' AS [CitaControl]
			,'' AS [DiasMesesControl]
			,'' AS cupscode
			,'' AS [DescripcionMedicamento]
			,'' AS [CantidadMedicamento]
		FROM dbo.hcordpato AS opat
		INNER JOIN dbo.adingreso AS ing ON opat.numingres = ing.numingres AND ing.genconentity IN (36, 177)
		WHERE opat.estserips NOT IN(6) AND opat.manextpro = 1 AND  YEAR(opat.fecordmed)>=2022
		--CAST(opat.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL 

		--	==================================================================== Procedimientos no quirurgicos
		SELECT		
			onqx.auto
			,onqx.ipcodpaci
			,onqx.numingres
			,onqx.numefolio AS folio
			,onqx.codprosal
			,onqx.fecordmed
			,onqx.codserips
			,onqx.iddescripcionrelacionada 
			,onqx.canserips
			,CASE onqx.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = onqx.ipcodpaci AND numingres = onqx.numingres AND numefolio = onqx.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = onqx.ipcodpaci AND numingres = onqx.numingres AND numefolio = onqx.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = onqx.ipcodpaci AND numingres = onqx.numingres AND numefolio = onqx.numefolio) END AS obsserips
			,'No' AS [CitaControl]
			,'' AS [DiasMesesControl]
			,'' AS cupscode
			,'' AS [DescripcionMedicamento]
			,'' AS [CantidadMedicamento]
		FROM dbo.hcordpron AS onqx
		INNER JOIN dbo.adingreso AS ing ON onqx.numingres = ing.numingres AND ing.genconentity IN (36, 177)
		WHERE onqx.estserips NOT IN(5) AND onqx.manextpro = 1 AND YEAR(onqx.fecordmed)>=2022
		--CAST(onqx.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL

		--	==================================================================== Procedimientos quirurgicos
		SELECT	
			opqx.auto
			,opqx.ipcodpaci
			,opqx.numingres
			,opqx.numefolio AS folio
			,opqx.codprosal
			,opqx.fecordmed
			,opqx.codserips
			,NULL AS iddescripcionrelacionada
			,opqx.canserips
			,CASE opqx.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = opqx.ipcodpaci AND numingres = opqx.numingres AND numefolio = opqx.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = opqx.ipcodpaci AND numingres = opqx.numingres AND numefolio = opqx.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = opqx.ipcodpaci AND numingres = opqx.numingres AND numefolio = opqx.numefolio) END AS obsserips
			,'No' AS [CitaControl]
			,'' AS [DiasMesesControl]
			,'' AS cupscode
			,'' AS [DescripcionMedicamento]
			,'' AS [CantidadMedicamento]
		FROM dbo.hcordproq AS opqx
		INNER JOIN dbo.adingreso AS ing ON opqx.numingres = ing.numingres AND ing.genconentity IN (36, 177)
		WHERE opqx.estserips NOT IN(3) AND opqx.manextpro = 1 AND YEAR(opqx.fecordmed)>=2022
		--CAST(opqx.fecordmed AS DATE) BETWEEN @ini_date AND @end_date

		UNION ALL

		--	==================================================================== Interconsultas 
		SELECT		
			oint.auto
			,oint.ipcodpaci
			,oint.numingres
			,oint.numefolio AS folio
			,oint.codprosal
			,oint.fecordmed
			,oint.codserips
			,NULL AS iddescripcionrelacionada
			,oint.canserips
			,CASE oint.idetiphis 
				WHEN N'hcurging1' THEN (SELECT analisisp FROM dbo.hcurging1 WHERE ipcodpaci = oint.ipcodpaci AND numingres = oint.numingres AND numefolio = oint.numefolio)
				WHEN N'hcurgevo1' THEN (SELECT analisisp FROM dbo.hcurgevo1 WHERE ipcodpaci = oint.ipcodpaci AND numingres = oint.numingres AND numefolio = oint.numefolio)
				WHEN N'hcnotevo1' THEN (SELECT analisisp FROM dbo.hcnotevo1 WHERE ipcodpaci = oint.ipcodpaci AND numingres = oint.numingres AND numefolio = oint.numefolio) END AS obsserips		
			,'No' AS [CitaControl]
			,'' AS [Dias Control]
			,'' AS cupscode
			,'' AS [DescripcionMedicamento]
			,'' AS [CantidadMedicamento]
		FROM dbo.hcordinte AS oint
		INNER JOIN dbo.adingreso AS ing ON oint.numingres = ing.numingres AND ing.genconentity IN (36, 177)
		WHERE oint.estserips NOT IN(5) AND oint.manextpro = 1 AND YEAR(oint.fecordmed)>=2022
		--CAST(oint.fecordmed AS DATE) BETWEEN @ini_date AND @end_date
		
		UNION ALL 

		--	==================================================================== Ordenes de control
		SELECT 
			coex.auto
			,hchi.ipcodpaci
			,hchi.numingres
			,hchi.numefolio AS folio
			,hchi.codprosal
			,hchi.fechispac fecordmed
			,coex.codserips
			,coex.iddescripcionrelacionada 
			,1 AS canserips
			,hchi.datobjeti
			,'Si' AS [CitaControl]
			,CASE coex.unicontrl WHEN 1 THEN CAST(coex.unicontrl AS VARCHAR) + ' Dia(s)' WHEN 2 THEN CAST(coex.unicontrl AS VARCHAR) + ' Mes(es)' END AS [DiasMesesControl]
			,'' AS cupscode
			,'' AS [DescripcionMedicamento]
			,'' AS [CantidadMedicamento]
		FROM dbo.hchispaca AS hchi
		INNER JOIN dbo.hcdescoex AS coex ON hchi.numingres = coex.numingres AND hchi.numefolio = coex.numefolio
		INNER JOIN dbo.adingreso AS ing ON hchi.numingres = ing.numingres AND ing.genconentity IN (36, 177)
		WHERE YEAR(hchi.fechispac)>=2022
		--CAST(hchi.fechispac AS DATE) BETWEEN @ini_date AND @end_date
		
		UNION ALL 
		
		--	==================================================================== Medicamentos quimioterapia
		SELECT 
			ordc.id AS auto,
			ordc.ipcodpaci,
			ordc.numingres,
			ordc.numefolio,
			ordc.codprosal,
			ordc.fecharegistro AS fecordmed,
			'' AS codserips,
			'' AS iddescripcionrelacionada,
			'' AS canserips,
			'' AS obsserips,
			'No' AS [CitaControl],
			'' AS [DiasMesesControl],
			atc.code AS cupscode,
			med.name AS [DescripcionMedicamento],
			SUM(mede.cantidad) AS [CantidadMedicamento]
		FROM ehr.hcordciclos AS ordc
		INNER JOIN ehr.hcordmedicam AS mede WITH (NOLOCK) ON ordc.schemesid = mede.schemesid AND ordc.idhcordquimio = mede.idhcordquimio AND ordc.ciclo = mede.ciclo
		INNER JOIN dbo.adingreso  AS ing ON ordc.numingres = ing.numingres AND ing.genconentity IN (36, 177)
		INNER JOIN inventory.atcentity AS atc ON  mede.atcentityid = atc.id
		INNER JOIN inventory.atc AS med ON mede.codproduc = med.code
		WHERE YEAR(ordc.fecharegistro)>=2022
		--CAST(ordc.fecharegistro AS DATE) BETWEEN @ini_date AND @end_date
		GROUP BY ordc.id, ordc.ipcodpaci, ordc.numingres, ordc.numefolio, ordc.codprosal, ordc.fecharegistro, atc.code, med.name
		
	) AS sips
	INNER JOIN dbo.inpacient AS paci ON sips.ipcodpaci = paci.ipcodpaci 
	LEFT JOIN dbo.inprofsal AS med ON sips.codprosal = med.codprosal
	LEFT JOIN security.personINT AS per ON med.codprosal = per.identification 
	LEFT JOIN dbo.inespecia AS esp ON med.codespec1 = esp.codespeci 
	LEFT JOIN contract.cupsentity AS cups ON sips.codserips = cups.code AND cups.billinggroupid NOT IN (10, 12)
	LEFT JOIN contract.cupsentitycontractdescriptions AS descr WITH (NOLOCK) ON sips.iddescripcionrelacionada = descr.id
	LEFT JOIN contract.contractdescriptions AS [desc] WITH (NOLOCK) ON descr.contractdescriptionid = [desc].id
	INNER JOIN dbo.adingreso AS ing ON sips.numingres = ing.numingres;

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida órdenes médicas extramurales (imágenes, laboratorios, patología, procedimientos QX/no QX, interconsultas, controles y medicamentos de quimioterapia) de pacientes afiliados a Sanitas EPS, para carga en cubo de reporte clínico.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSanitasEPS2ExtramuralOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso (adingreso) debe pertenecer a la entidad 36 o 177 (Sanitas EPS); Las órdenes deben estar marcadas como manejo extramural (manextpro = 1); La fecha de la orden/registro debe ser de año 2022 en adelante; El paciente debe existir en inpacient', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSanitasEPS2ExtramuralOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes de pacientes con ingreso vinculado a entidades 36 o 177; Solo se procesan órdenes con manextpro = 1 (extramurales); Se filtran órdenes con año de fecha >= 2022; El prestador siempre se reporta como NIT 820001277-2 / código 35051 / Centro de Cancerología de Boyacá; El número de entregas siempre es 1 y la preparación siempre ''Ninguna''; Los medicamentos no-quimioterapia tienen CitaControl=''No'' y campos de medicamento vacíos; las órdenes de control siempre tienen CitaControl=''Si''; La descripción CUPS prioriza la descripción contractual ([desc].name) sobre la genérica (cups.description); La fecha se entrega como DATE y el timestamp ULT_ACTUAL se calcula en zona horaria ''Pakistan Standard Time''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSanitasEPS2ExtramuralOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Afiliado; EPS Sanitas; Orden médica extramural; Diagnóstico principal; CUPS; CUMS; Médico tratante; Especialidad; Imágenes diagnósticas; Laboratorio clínico; Patología; Procedimientos quirúrgicos y no quirúrgicos; Interconsulta; Cita de control; Quimioterapia/Medicamentos oncológicos; Justificación clínica; Prestador (IPS)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSanitasEPS2ExtramuralOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalSanitasEPS2ExtramuralOrders: Devuelve filas DISTINCT con NIT prestador fijo ''820001277-2'', código ''35051'' y descripción ''CENTRO DE CANCEROLOGIA DE BOYACA''; campo NUMERO ENTREGAS siempre = 1 y PREPARACION siempre ''Ninguna''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSanitasEPS2ExtramuralOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si iptipodoc del paciente (1..15) → Mapea a códigos de tipo de documento: CC, CE, TI, RC, PA, AS, MS, NU, CN, CD, SC, PE, PT, DE, SI; si identificationtype del médico (1,2,3) → Mapea a CC, CE o TI; si correo del paciente contiene ''none'' → Se devuelve cadena vacía else Se devuelve el correo en minúsculas y sin espacios a la derecha; si idetiphis = ''hcurging1'' / ''hcurgevo1'' / ''hcnotevo1'' → Obtiene observación (analisisp/datobjeti) desde la tabla de historia correspondiente; si Tipo de orden = imágenes/laboratorios/interconsultas/no quirúrgicas → estserips NOT IN (6 ó 5); quirúrgicas → estserips NOT IN (3) → Excluye órdenes anuladas/canceladas según el tipo; si codserips = ''C00272'' (imágenes) o ''906340'' (laboratorios) → Se excluye la orden del resultado; si unicontrl en orden de control = 1 ó 2 → Formatea como ''N Dia(s)'' o ''N Mes(es)''; si cups.billinggroupid IN (10,12) → No se asocia descripción CUPS (LEFT JOIN filtrado); si coddiapri = 1 en indiagnop → Toma ese diagnóstico como diagnóstico principal del ingreso', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSanitasEPS2ExtramuralOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.indiagnop; dbo.hcurging1; dbo.hcurgevo1; dbo.hcnotevo1; dbo.hcordimag; dbo.adingreso; dbo.hcordlabo; dbo.hcordpato; dbo.hcordpron; dbo.hcordproq; dbo.hcordinte; dbo.hchispaca; dbo.hcdescoex; ehr.hcordciclos; ehr.hcordmedicam; inventory.atcentity; inventory.atc; dbo.inpacient; dbo.inprofsal; security.personINT; dbo.inespecia; contract.cupsentity; contract.cupsentitycontractdescriptions; contract.contractdescriptions', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSanitasEPS2ExtramuralOrders';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSanitasEPS2ExtramuralOrders';
GO
