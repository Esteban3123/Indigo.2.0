

CREATE VIEW [Inventory].[ViewLASAMedication]
AS
	SELECT *
	FROM (
			SELECT	CONCAT(atc.Code, '-', dci.Code, '-', sdci.CODDCIMED) UUID,
					atc.Code,
					CONCAT(atc.Code, ' - ', atc.Name) RequestedMedication,
					CASE p.TIPO
						WHEN 1 THEN 'Suena Igual'
						WHEN 2 THEN 'Se parece'
						WHEN 3 THEN 'Suena Igual y se Parece'
					END Likeness,
					CONCAT(sdci.CODDCIMED, ' - ', sdci.DESDCIMED) SimilarMedicine,
					dci.TypeWarning as IsRestrictive
			FROM Inventory.ATC atc
			JOIN Inventory.DCI dci ON atc.DCIId = dci.Id
			JOIN dbo.IHPARAMDCI p ON dci.Code = p.CODDCIMEDPADRE OR dci.Code = p.CODDCIMED
			JOIN dbo.IHDCIMEDI sdci ON IIF(dci.Code = p.CODDCIMEDPADRE, p.CODDCIMED, p.CODDCIMEDPADRE) = sdci.CODDCIMED AND dci.Code <> p.CODDCIMED) sub
			GROUP by sub.UUID,sub.RequestedMedication,sub.Likeness,sub.SimilarMedicine,sub.IsRestrictive, sub.Code