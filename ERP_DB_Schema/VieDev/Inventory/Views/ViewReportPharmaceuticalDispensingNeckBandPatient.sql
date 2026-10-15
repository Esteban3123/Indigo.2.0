

CREATE VIEW [Inventory].[ViewReportPharmaceuticalDispensingNeckBandPatient]
AS 

SELECT distinct
	pdd.Id Id,
	pd.Id PharmaceuticalDispensingId,
	pd.Code AS 'CodePharmaceuticalDispensing',
	pac.IPNOMCOMP AS 'PacientName',
	pac.IPCODPACI AS 'PacientDocumentNumber',
	CONVERT(VARCHAR, pac.IPFECNACI, 107) 'PacientBirthDate',
	ing.NUMINGRES AS 'AdmissionNumber',
	lp.CODPRODUC AS 'ProductCode',
	RTRIM(isnull(concat(lp.DESPRODUC,CHAR(13)+CHAR(10), CHAR(13)+CHAR(10),RTRIM(mescla.MEZLIQPAC)), lp.DESPRODUC)) AS 'ProductName',
	sfd.CANPEDPRO AS QuantityRequest,
	pdd.Quantity AS QuantityDispensing,
	REPLACE(ISNULL(pres.DESADMINI, mescla.ADMMEZLIQ),'continuamente', '') AS 'Prescription', 
	REPLACE(isnull(pres.INDAPLMED, mescla.INDAPLMED), 'continuamente', '') AS 'ApplyInstructions',
	pdn.Note AS 'PharmaceuticalNotes',
	IIF
	(
		(CHARINDEX('vía', REPLACE(ISNULL(pres.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''))) > 0,
		SUBSTRING(REPLACE(ISNULL(pres.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''), CHARINDEX('vía',  REPLACE(ISNULL(pres.DESADMINI, mescla.ADMMEZLIQ),'continuamente', '')) + LEN('Via') + 1, LEN( REPLACE(ISNULL(pres.DESADMINI, mescla.ADMMEZLIQ),'continuamente', ''))),
		ISNULL(viaMed.DESVIAADM, via.DESVIAADM)
	) AS 'Via'
	--ISNULL(viaMed.DESVIAADM, via.DESVIAADM) AS 'Via'
FROM Inventory.PharmaceuticalDispensing pd WITH (NOLOCK)
JOIN Inventory.PharmaceuticalDispensingDetail pdd WITH (NOLOCK) ON pd.Id = pdd.PharmaceuticalDispensingId
JOIN dbo.ADINGRESO ing ON pd.AdmissionNumber = ing.NUMINGRES
JOIN dbo.INPACIENT pac WITH (NOLOCK) ON ing.IPCODPACI = pac.IPCODPACI
JOIN dbo.HCFARMEPD sfd WITH (NOLOCK) ON sfd.ID = pdd.EntityId AND 'HCFARMEPD' = pdd.EntityName
JOIN dbo.IHLISTPRO lp WITH (NOLOCK) ON lp.CODPRODUC = sfd.CODPRODUC
LEFT JOIN Inventory.PharmaceuticalDispensingNotes pdn WITH (NOLOCK) ON pdn.PharmaceuticalRequestDetailId = sfd.ID
--------- preescripciòn -----------
LEFT JOIN dbo.HCPRESCRA pres WITH (NOLOCK) ON pres.ID = sfd.IdSourceTable AND 'HCPRESCRA' = isnull(sfd.SourceTable, 'HCPRESCRA')
LEFT JOIN dbo.HCINFLIQA mescla WITH (NOLOCK) ON mescla.CONSECUTI = sfd.IdSourceTable AND 'HCINFLIQA' = sfd.SourceTable
--------- via ---------------
LEFT JOIN dbo.HCVIAADMI viaMed WITH (NOLOCK) ON viaMed.CODVIAADM = pres.CODVIAADM
LEFT JOIN dbo.HCVIAADMI via WITH (NOLOCK) ON via.CODVIAADM = lp.CODVIAADM
WHERE lp.TIPPRODUC IN (1, 3)