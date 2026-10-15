
CREATE view [Report].[UploadCubeVieClinicalBedManagementMedicalDepartureDate] AS

	SELECT 
		hc.ipcodpaci,
		hc.numingres,
		hce.fecaltpac  
	FROM dbo.hchispaca AS hc
	INNER JOIN dbo.hcregegre AS hce ON hc.numingres = hce.numingres AND hc.numefolio = hce.numefolio 
	WHERE CAST(hc.fechispac AS DATE) = CAST(DATEADD(DD, -1, COMMON.GETDATE()) AS DATE)