
--CREATE PROCEDURE [Contract].[ReporteVerificacionTarifasServicio]

	--@CODE AS VARCHAR(10)

--AS 

CREATE view [Report].[UploadCubeVieRCMServiceRateVerification] as

	WITH template AS 
	(
		SELECT 
			tmp.code,
			RTRIM(tmp.name) AS name,
			grp.code AS codegrp,
			cup.code AS codecup,
			cds.code AS [codedescription],
			CASE pro.contracted WHEN 1 THEN 'Si' WHEN 0 THEN 'No' ELSE 'NaN' END AS [Contratado],
			CASE pro.quoted WHEN 1 THEN 'Si' WHEN 0 THEN 'No' ELSE 'NaN' END AS [Cotizado],
			CASE 
				WHEN cds.code IS NULL THEN RTRIM(grp.code) + RTRIM(cup.code)
				ELSE RTRIM(grp.code) + RTRIM(cup.code) + RTRIM(cds.code) END AS llave
		FROM contract.proceduretemplate tmp 
		INNER JOIN contract.procedurecups AS pro ON tmp.id = pro.procedurestemplateid
		INNER JOIN contract.cupsentity AS cup ON pro.cupsid = cup.id
		--LEFT JOIN contract.cupsentitycontractdescriptions AS ccd WITH (NOLOCK) ON pro.cupsentitycontractdescriptionid = ccd.id
		LEFT JOIN contract.contractdescriptions AS cds ON pro.contractdescriptionid = cds.id
		LEFT JOIN contract.caregroup AS grp ON tmp.id = grp.proceduretemplateid 
		WHERE tmp.status = 1
	), rate AS 
	(
		SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
			dr.code AS 'CODIGO PLANTILLA',--[CodigoPlantilla],
			dr.name AS 'PLANTILLA',--[Plantilla],
			grp.code AS 'CODIGO GRUPO ATENCION',--[CodigoGrupoAtencion],
			grp.name AS 'GRUPO ATENCION',--[GrupoAtencion],
			CASE 
				WHEN drd.ruletype = 1 THEN 'Servicio' ELSE 'CUPS' END AS 'TIPO REGLA',--[TipoRegla],
			ips.code AS 'CODIGO SERVICIO IPS',--[CodigoServicioIPS],
			ips.name AS 'SERVICIO IPS',--[ServicioIPS],
			cg.code AS 'CODIGO GRUPO',--[CodigoGrupo],
			cg.name AS 'GRUPO',--[Grupo],
			csg.code AS 'CODIGO SUBGRUPO',--[CodigoSubgrupo],
			csg.name AS 'SUBGRUPO',--[Subgrupo],
			cup.code AS 'CODIGO CUPS',--[CodigoCUP],
			cup.description AS 'DESCRIPCION CUPS',--[DescripcionCUP], 
			CASE cup.status 
				WHEN 1 THEN 'Activo' 
				WHEN 0 THEN 'Inactivo' END AS 'ESTADO CUPS',--[EstadoCUP],
			CASE cup.financedresourceupc
				WHEN 1 THEN 'Si' 
				WHEN 0 THEN 'No' END AS 'FINANCIADO UPC',--[FinanciadoUPC],
			CASE conditiontype 
				WHEN 5 THEN 'Ninguna' 
				WHEN 7 THEN 'Descripcion Relacionada' 
				ELSE 'Otro' END 'TIPO CONDICION',--[TipoCondicion],
			CASE drd.liquidationtype 
				WHEN 1 THEN 'Fija' 
				WHEN 2 THEN 'Estandar' ELSE 'N/A' END AS 'TIPO LIQUIDACION',--[TipoLiquidacion],
			CASE drd.manualtype 
				WHEN 1 THEN 'ISS 2001' 
				WHEN 2 THEN 'ISS 2004' 
				WHEN 3 THEN 'SOAT'
				ELSE 'N/A' END AS 'MNUAL TARIFARIO',--[ManualTarifario],
			RTRIM(rmv.code) + ' - ' + RTRIM(rmv.name) AS 'VIGENCIA MANUAL TARIFARIO',--[VigenciaManualTarifario],
			drd.salesvalue AS 'VALOR FIJO',--[ValorFijo],
			drd.salesvaluewithsurcharge AS 'VALOR MES RECARGO',--[ValorMasRecargo],
			rm.code AS 'CODIGO MANUAL',--[CodigoManual],
			rm.name AS 'MANUAL',--[Manual],
			drd.ratevariation AS '% VARIACION',--[PorcentajeVariacion],
			CASE drdc.liquidationtype WHEN 1 THEN 'Fija' WHEN 2 THEN 'Estandar' ELSE 'N/A' END 'TIPO LIQUDACION CONDICION',--[TipoLiquidacionCondicion],
			CASE drdc.manualtype WHEN 1 THEN 'ISS' WHEN 2 THEN 'SOAT' ELSE 'N/A' END 'TIPO MANUAL CONDICION',--[TipoManualCondicion],
			drdc.salesvalue AS 'VALOR CONDICION',--[ValorCondicion],
			drdc.salesvaluewithsurcharge AS 'VALOR RECARGO CONDICION',--[ValorRecargoCondicion],
			rm2.code AS 'CODIGO MANUAL II',--[CodigoManualII],
			rm2.name 'MANUAL II',--[ManualII],
			cd.code AS 'CODIGO DESCRIPCION RELACIONADA',--[CodigoDescripcionRelacionada], 
			cd.name AS 'DESCRIPCION RELACIONADA',--[DescripcionRelacionada],
			drdc.ratevariation AS 'VARIACION CONDICION',--[VariacionCondicion],
			ips2.code AS 'CODIGO SERVICIO IPS HIJO',--[CodigoServicioIPSHijo], 
			ips2.name AS 'SERVICIO HIJO',--[ServicioIPSHijo],
			CASE 
				WHEN cd.code IS NULL THEN RTRIM(grp.code) + RTRIM(cup.code)
				ELSE RTRIM(grp.code) + RTRIM(cup.code) + RTRIM(cd.code) END AS 'LLAVE',--[Llave]
		    cast(GETDATE() as date) 'FECHA BUSQUEDA',
		    CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
		FROM contract.definitionrate AS dr 
		LEFT JOIN contract.caregroupdefinitionrate AS dfr ON  dr.id = dfr.definitionrateid 
		LEFT JOIN contract.caregroup AS grp ON dfr.caregroupid = grp.id
		LEFT JOIN contract.definitionratedetail AS drd ON dr.id = drd.definitionrateid 
		LEFT JOIN contract.ipsservice AS ips ON drd.ipsserviceid = ips.id 
		LEFT JOIN contract.cupsentity AS cup ON cup.id = drd.cupsentityid 
		LEFT JOIN contract.cupssubgroup csg ON cup.cupssubgroupid = csg.id 
		LEFT JOIN contract.cupsgroup cg ON csg.cupsgroupid = cg.id 
		LEFT JOIN contract.ratemanual AS rm ON rm.id = drd.ratemanualid 
		LEFT JOIN contract.definitionratedetailcondition AS drdc ON drd.id = drdc.definitionratedetailid
		LEFT JOIN contract.ratemanual AS rm2 ON rm2.id = drdc.ratemanualid 
		LEFT JOIN contract.contractdescriptions AS cd ON cd.id = drdc.contractdescriptionid 
		LEFT JOIN contract.definitionratedetailsurgicalprocedures AS drdsp ON drdsp.definitionratedetailid = drd.id  
		LEFT JOIN contract.ipsservice AS ips2 ON drdsp.ipsserviceid = ips2.id
		LEFT JOIN contract.ratemanualvalidity AS rmv ON drd.ratemanualvalidityid = rmv.id
		WHERE dr.status = 1
	)


SELECT

	rate.*,
	tmp.code AS [CodigoCubrimientoProcedimientos],
	tmp.name AS [CubrimientoProcedimientos],
	tmp.Contratado,
	tmp.Cotizado
FROM rate AS rate 
LEFT JOIN template AS tmp ON rate.llave = tmp.llave