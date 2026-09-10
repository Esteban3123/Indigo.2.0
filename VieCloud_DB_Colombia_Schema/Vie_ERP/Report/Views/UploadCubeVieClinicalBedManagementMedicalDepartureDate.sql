
CREATE view [Report].[UploadCubeVieClinicalBedManagementMedicalDepartureDate] AS

	SELECT 
		hc.ipcodpaci,
		hc.numingres,
		hce.fecaltpac  
	FROM dbo.hchispaca AS hc
	INNER JOIN dbo.hcregegre AS hce ON hc.numingres = hce.numingres AND hc.numefolio = hce.numefolio 
	WHERE CAST(hc.fechispac AS DATE) = CAST(DATEADD(DD, -1, COMMON.GETDATE()) AS DATE)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista diseñada para cargar datos en un cubo OLAP relacionado con gestión de camas clínicas. Combina registros de dos tablas de historia clínica filtrando únicamente los registros cuya fecha corresponde al día anterior, extrayendo el código de paciente, número de ingreso y la fecha de alta/egreso médico. Su propósito es alimentar procesos de reporting con información diaria sobre egresos de pacientes hospitalizados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementMedicalDepartureDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementMedicalDepartureDate';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone, para alimentación de un cubo analítico, los pacientes con egreso médico cuya historia clínica fue registrada el día anterior, mostrando paciente, ingreso y fecha de alta.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementMedicalDepartureDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros emparejados por numingres y numefolio entre hchispaca y hcregegre.; Disponibilidad de la función COMMON.GETDATE para obtener la fecha del sistema.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementMedicalDepartureDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone registros cuya fecha de historia clínica (hchispaca.fechispac) corresponde al día anterior a la fecha actual del sistema (COMMON.GETDATE - 1 día).; Se devuelve únicamente la combinación de ingreso (numingres) y folio (numefolio) que existe simultáneamente en hchispaca y hcregegre (INNER JOIN), garantizando egreso registrado.; La comparación de fechas se hace truncando a DATE, ignorando la componente de hora.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementMedicalDepartureDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; egreso médico; fecha de alta; historia clínica', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementMedicalDepartureDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.hchispaca: Cuando CAST(hc.fechispac AS DATE) = CAST(DATEADD(DD,-1,COMMON.GETDATE()) AS DATE) y existe coincidencia por numingres+numefolio en hcregegre, se retorna ipcodpaci, numingres y fecaltpac.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementMedicalDepartureDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'COMMON.GETDATE', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementMedicalDepartureDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.hchispaca; dbo.hcregegre', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementMedicalDepartureDate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieClinicalBedManagementMedicalDepartureDate';
GO
