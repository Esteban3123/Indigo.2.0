
CREATE view [Report].[UploadCubeVieClinicalSchedulingParametersSchedulingActivities] as 

	SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		CODACTMED AS 'CODIGO ACTIVIDAD',--[CodigoActividad], 
		DESACTMED AS 'DESCRIPCION ACTIVIDAD',--[DescripcionActividad], 
		case ACTIVICON 
			when 0 then 'Consulta externa' 
			when 1 then 'Procedimiento Qx' 
			when 2 then 'Apoyo Diagnóstico'
			when 3 then 'Tratamientos Especiales' 
			when 4 then 'Diálisis' end AS 'TIPO ACTIVIDAD',--[TipoActividad], 
		isnull(DURAACTIV,'') AS 'DURACION',--[Duracion],
		case  TIEMPOVAR 
			when 0 then 'No' 
			when 1 then 'Si' 
			else 'N/A' end 'TIEMPO VARIABLE',--[TiempoVariable],
		case when ESTADOACT=1 then 'Activo' else 'Inactivo' end 'ESTADO ACTIVIDAD',--[EstadoActividad], 
		ACT.CODSERIPS 'CUPS',--[CUP], 
		ips.DESSERIPS 'DESCRIPCION CUPS',--[DescripcionCUP]
        CAST(GETDATE() AS DATE) [FECHA BUSQUEDA],
        CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM dbo.AGACTIMED  AS ACT
	left join DBO.INCUPSIPS AS IPS ON ACT.CODSERIPS =IPS.CODSERIPS 
	WHERE act.activicon NOT IN(1, 2) 

	UNION 

	SELECT DISTINCT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		act.CODACTMED AS [CodigoActividad], 
		DESACTMED AS [DescripcionActividad], 
		case ACTIVICON 
			when 0 then 'Consulta externa' 
			when 1 then 'Procedimiento Qx' 
			when 2 then 'Apoyo Diagnóstico'
			when 3 then 'Tratamientos Especiales' 
			when 4 then 'Diálisis' end AS [TipoActividad], 
		ISNULL(det.duracservi,'') AS [Duracion],
		case  det.duravariab 
			when 0 then 'No' 
			when 1 then 'Si' 
			else 'N/A' end [TiempoVariable],
		case when ESTADOACT=1 then 'Activo' else 'Inactivo' end [EstadoActividad], 
		ips.CODSERIPS [CUP], 
		ips.DESSERIPS [DescripcionCUP],
	    CAST(GETDATE() AS DATE) [FECHA BUSQUEDA],
        CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
	FROM dbo.AGACTIMED  AS ACT
	INNER JOIN dbo.agactmedd AS det ON act.codactmed = act.codactmed
	LEFT JOIN DBO.INCUPSIPS AS IPS ON det.CODSERIPS = ips.CODSERIPS 
	WHERE act.activicon IN(1, 2)