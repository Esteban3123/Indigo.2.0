
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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista orientada a reporting/cubo OLAP que consolida el catálogo de actividades médicas configuradas para agendamiento clínico. Combina mediante UNION dos conjuntos: actividades de consulta externa, tratamientos y diálisis (tomando duración directamente de `AGACTIMED`), y actividades de procedimientos quirúrgicos y apoyo diagnóstico (tomando duración desde el detalle `agactmedd`). Cada registro expone código, descripción, tipo de actividad, duración, tiempo variable, estado y código CUPS asociado, identificado por empresa (DB_NAME) con marca de tiempo en zona horaria de Pakistán para control de actualización.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingParametersSchedulingActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingParametersSchedulingActivities';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida y expone el catálogo de actividades médicas para agendamiento, traduciendo códigos a etiquetas legibles y enlazando con el código CUPS, para alimentar un cubo de reporte.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingParametersSchedulingActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las actividades deben existir en dbo.AGACTIMED.; Para actividades quirúrgicas o de apoyo diagnóstico (ACTIVICON IN (1,2)) se requiere registro en dbo.agactmedd con detalles de duración.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingParametersSchedulingActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ID_COMPANY siempre corresponde al nombre de la base de datos actual truncado a 9 caracteres.; FECHA BUSQUEDA siempre es la fecha actual del servidor en la ejecución.; ULT_ACTUAL siempre se calcula convirtiendo la hora actual a la zona horaria ''Pakistan Standard Time''.; El TIPO ACTIVIDAD solo puede tomar uno de los cinco valores codificados (0-4); otros valores quedarían en NULL.; Las actividades quirúrgicas (1) y de apoyo diagnóstico (2) se reportan exclusivamente con base en su detalle (agactmedd); las demás solo desde la cabecera.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingParametersSchedulingActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'actividad médica; agendamiento de citas; consulta externa; procedimiento quirúrgico; apoyo diagnóstico; tratamientos especiales; diálisis; CUPS; duración de servicio; tiempo variable de atención; estado de actividad', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingParametersSchedulingActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieClinicalSchedulingParametersSchedulingActivities: Para actividades con ACTIVICON NOT IN (1,2) (Consulta externa, Tratamientos Especiales, Diálisis) se devuelve la duración y tiempo variable directamente desde AGACTIMED (DURAACTIV, TIEMPOVAR) junto al CUPS de la actividad.; [RETURN_RESULT] Report.UploadCubeVieClinicalSchedulingParametersSchedulingActivities: Para actividades con ACTIVICON IN (1,2) (Procedimiento Qx, Apoyo Diagnóstico) se devuelve la duración y variabilidad desde el detalle agactmedd (duracservi, duravariab) y el CUPS desde el detalle.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingParametersSchedulingActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ACTIVICON = 0/1/2/3/4 → Se traduce a ''Consulta externa'' / ''Procedimiento Qx'' / ''Apoyo Diagnóstico'' / ''Tratamientos Especiales'' / ''Diálisis'' respectivamente; si TIEMPOVAR (o duravariab) = 0/1/otro → Se traduce a ''No'' / ''Si'' / ''N/A''; si ESTADOACT = 1 → Se reporta ''Activo'' else Se reporta ''Inactivo''; si act.activicon NOT IN (1,2) → Toma duración y CUPS desde AGACTIMED (cabecera) else Toma duración y CUPS desde agactmedd (detalle) cuando activicon IN (1,2)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingParametersSchedulingActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGACTIMED; dbo.INCUPSIPS; dbo.agactmedd', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingParametersSchedulingActivities';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalSchedulingParametersSchedulingActivities';
GO
