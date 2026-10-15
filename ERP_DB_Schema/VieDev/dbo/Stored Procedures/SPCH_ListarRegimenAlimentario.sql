-- =============================================
-- Author:      Maria Rozo
-- Create Date: 01/03/2022
-- Description: SP Para listar consultas de Regimen Alimentario
-- =============================================
CREATE PROCEDURE [dbo].[SPCH_ListarRegimenAlimentario]
(
	@TipoUnidad Char(2),
	@CentroAtencion Char(10),
	@UnidadFuncional Char(10),
	@FechaInicial DateTime,
	@FechaFinal DateTime,
	@Comida Int
)
AS
	SET @FechaInicial = @FechaInicial+'00:00:00.000'
	SET @FechaFinal = @FechaFinal+'23:59:00.000'
BEGIN
    SET NOCOUNT ON
	SELECT
		A.CreationDate AS 'DateRequestDiet',
		RTRIM(B.UFUDESCRI) AS 'FunctionalUnit',
		RTRIM(C.DESCCAMAS) AS 'Bed',
		(CASE A.FoodKind
		WHEN 1 THEN  'Desayuno'
		WHEN 2 THEN 'Almuerzo'
		WHEN 3 THEN 'Cena'
		WHEN 4 THEN 'Complemento Mañana'
		WHEN 5 THEN 'Complemento Tarde'
		WHEN 6 THEN 'Otro'
		ELSE 'Indeterminado'
		END) as 'FoodKind',
		A.Id as 'DietPatientCode',
		dbo.ConsultarDietasEnfermeria(D.IPCODPACI, E.NUMINGRES,A.ID) As 'DietType',
		RTRIM(A.PhysicianObservation) AS 'PhysicianObservation',
		RTRIM(A.NurseObservation) AS 'NurseObservation',
		CAST(GETDATE() AS DATE) AS 'DatePrint',
		RTRIM(D.IPNOMCOMP) AS 'PatientName',
		RTRIM(A.PatientCode) AS 'PatientIdentification',
		dbo.TipoDocumento(D.IPTIPODOC) AS 'IDAcronym',
		CAST(D.IPFECNACI as DATE) AS 'DateBirth',
		RTRIM([dbo].[EDAD] (D.IPFECNACI,[Common].[GETDATE]())) As 'Age',
		A.DateDietOrder AS 'DateDietOrder',
		F.FECINIEST AS 'DateHospitalization',
		DATEDIFF("d",F.FECINIEST,[Common].[GETDATE]())+1 AS 'Days',
		F.FECINIEST AS 'AdmissionDate',
		RTRIM(COALESCE(NULLIF(H.NOMDIAGNO,''),'')) AS 'Diagnostic',
		RTRIM(G.ENTCONTAC) AS 'Entity',
		CAST(1 AS BIT) AS 'Print'
	FROM
		MedicalDiet.DietControlNursing A
		INNER JOIN dbo.INUNIFUNC B ON A.FunctionalUnit = B.UFUCODIGO
		INNER JOIN dbo.CHCAMASHO C ON A.CodeBed=C.CODICAMAS
		INNER JOIN dbo.INPACIENT D ON A.PatientCode = D.IPCODPACI
		INNER JOIN dbo.ADINGRESO E ON A.AdmissionNumber = E.NUMINGRES
		INNER JOIN dbo.CHREGESTA F ON E.NUMINGRES = F.NUMINGRES
		INNER JOIN dbo.INENTIDAD G ON E.CODENTIDA = G.CODENTIDA
		INNER JOIN dbo.INDIAGNOS AS H ON A.CodeDiagnostic = H.CODDIAGNO
	WHERE
		F.REGESTADO='1'
		AND A.AttentionCenter=@CentroAtencion
		AND A.FunctionalUnit=@UnidadFuncional
		AND A.CreationDate BETWEEN @FechaInicial AND @FechaFinal
		AND A.FoodKind LIKE (CASE WHEN @Comida <> 0 THEN CAST(@Comida AS CHAR(1)) ELSE '%' END)
	ORDER BY
		C.DESCCAMAS ASC
END