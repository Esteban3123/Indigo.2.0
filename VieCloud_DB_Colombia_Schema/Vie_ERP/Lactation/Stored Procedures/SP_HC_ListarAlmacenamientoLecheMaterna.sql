
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
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que lista los registros activos de almacenamiento de leche materna filtrados por centro de atención. Retorna datos de trazabilidad como origen de la leche (autóloga o heteróloga), paciente receptor, donante, volúmenes extraídos y almacenados, lote, fechas, tipo de conservación (refrigeración o congelación) y vigencia calculada mediante función escalar. Los resultados se ordenan por fecha de almacenamiento.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAlmacenamientoLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAlmacenamientoLecheMaterna';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los registros activos de almacenamiento de leche materna filtrados por centros de atención, enriquecidos con datos del paciente receptor, donante, centro y vigencia calculada.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAlmacenamientoLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe contener uno o varios códigos separados por delimitador compatible con dbo.splitstring; Los códigos de centro deben existir en ADCENATEN; Los registros a listar deben tener Status = 1 (activos)', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAlmacenamientoLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen registros activos (Status=1); los inactivos quedan ocultos; El centro de atención es obligatorio en el resultado (INNER JOIN con ADCENATEN), por lo que registros sin centro válido no aparecen; Paciente receptor y donante son opcionales (LEFT JOIN con INPACIENT), permitiendo registros sin identificación poblada; La vigencia del lote se calcula on-the-fly mediante la función Lactation.BreastMilkValidity sobre el Id del registro; El campo ''Marcar'' siempre se inicializa en 0 (false) para selección en UI; PersonaFuente concatena el código del donante con el nombre completo (IPNOMCOMP) separados por '' - ''', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAlmacenamientoLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Leche materna; Almacenamiento (refrigeración/congelación); Origen autólogo/heterólogo; Donante; Receptor; Lote; Vigencia del lote; Centro de atención; Volumen extraído; Volumen almacenado', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAlmacenamientoLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Lactation.BreastMilkIntakeRecords: Devuelve únicamente registros con Status=1 cuyo CODCENATE esté en la lista de centros recibida, ordenados por StorageDate ascendente', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAlmacenamientoLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.SourceMilk = 1 → Origen se etiqueta como ''Autóloga (madre)'' else Si SourceMilk = 2 se etiqueta como ''Heteróloga (donante)''; si A.StorageType = 1 → Tipo de almacenamiento se etiqueta como ''Refrigeración'' else Si StorageType = 2 se etiqueta como ''Congelación''', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAlmacenamientoLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Lactation.BreastMilkValidity; dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAlmacenamientoLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Lactation.BreastMilkIntakeRecords; ADCENATEN; INPACIENT', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAlmacenamientoLecheMaterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAlmacenamientoLecheMaterna';
-- GO
