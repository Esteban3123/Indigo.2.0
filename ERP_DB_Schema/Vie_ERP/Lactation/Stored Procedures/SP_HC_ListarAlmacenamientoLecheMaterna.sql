
CREATE PROCEDURE [Lactation].[SP_HC_ListarAlmacenamientoLecheMaterna]
    @CentroAtencion VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT		
		cast (0 as bit) As 'Marcar',
        A.Id AS Id,		
        CASE A.SourceMilk WHEN 1 THEN 'Autóloga (madre)' WHEN 2 THEN  'Heteróloga (donante)' END AS 'Origen',  
		RTRIM(P.IPNOMCOMP) AS 'Paciente',
		A.PatientCodeRecipient AS 'CodPaciente',
		CONCAT(A.PatientCodeDonor, ' - ', PE.IPNOMCOMP) AS 'PersonaFuente',
		A.PatientCodeDonor As 'IdentificacionDonante',
		A.ExtractionDate AS 'FechaExtracción',
		A.ExtractedVolume AS 'VolumenExtraido',
		A.StorageDate AS 'FechaAlmacenamiento',
		A.StoredVolume AS 'VolumenAlmacenado',
		A.BatchNumber AS 'Lote',
		A.Observations AS 'Observaciones' ,
		CASE A.StorageType WHEN 1 THEN 'Refrigeración' WHEN 2 THEN 'Congelación' END AS 'TipoAlmacenamiento',
		RTRIM(CA.CODCENATE) AS 'CodCentroAtencion',
        RTRIM(CA.NOMCENATE) AS 'CentroAtencion',  
		A.SourceMilk AS 'Tipo',
		Lactation.BreastMilkValidity(A.Id) AS 'Vigencia'
    FROM Lactation.BreastMilkIntakeRecords A 	
	INNER JOIN ADCENATEN CA WITH(NOLOCK) ON A.CODCENATE = CA.CODCENATE
    LEFT JOIN INPACIENT P ON A.PatientCodeRecipient = P.IPCODPACI 
	LEFT JOIN INPACIENT PE ON A.PatientCodeDonor = PE.IPCODPACI
    WHERE A.Status = 1 AND A.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion)) 

    ORDER BY A.StorageDate
END