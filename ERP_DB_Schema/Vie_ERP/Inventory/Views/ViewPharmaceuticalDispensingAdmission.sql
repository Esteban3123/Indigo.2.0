

CREATE VIEW [Inventory].[ViewPharmaceuticalDispensingAdmission]
AS
SELECT  CONCAT(pd.Id, ' - ', ing.NUMINGRES) Id,
		pd.Id PharmaceuticalDispensingId,
		CONCAT(RTRIM(pac.IPCODPACI), ' - ', pac.IPNOMCOMP) PatientName,
		ISNULL(bed.NUMCAMHOS, '') AS Bed
FROM Inventory.PharmaceuticalDispensing pd
JOIN dbo.ADINGRESO ing ON pd.AdmissionNumber = ing.NUMINGRES
JOIN dbo.INPACIENT pac ON ing.IPCODPACI = pac.IPCODPACI
LEFT JOIN dbo.CHCAMASHO bed ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END, '') = bed.CODICAMAS