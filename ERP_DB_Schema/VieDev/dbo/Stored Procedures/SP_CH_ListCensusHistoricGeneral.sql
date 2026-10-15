CREATE PROCEDURE [dbo].[SP_CH_ListCensusHistoricGeneral]
(
  @CentroAtencion as varchar(max) ,
  @FechaInicial as varchar(max) ,
  @FechaFinal as varchar(max) 
)

AS
BEGIN
  SET NOCOUNT ON;

With Datos AS(
	SELECT Distinct 
	A.IPCODPACI AS CedulaPaciente, RTRIM(B.IPNOMCOMP) AS NombrePaciente, dbo.Edad(B.IPFECNACI, GETDATE()) AS FechaNAcimientoPaciente, Rtrim(A.NUMINGRES) AS NIngreso,
	C.IFECHAING AS FechaIngreso, D.Name AS CausaIngreso, F.Name as Entidad,
	(Select TOP 1 FECINIEST FROM CHREGESTA WHERE IPCODPACI = A.IPCODPACI AND NUMINGRES = A.NUMINGRES ORDER BY ID) AS FechaInicioEstancia,
	(Select TOP 1 FECFINEST FROM CHREGESTA WHERE IPCODPACI = A.IPCODPACI AND NUMINGRES = A.NUMINGRES ORDER BY ID DESC) AS FechaFinalEstancia,
	(Select Top 1 CA.DESCCAMAS FROM CHREGESTA RE INNER JOIN CHCAMASHO CA ON RE.CODICAMAS = CA.CODICAMAS WHERE RE.IPCODPACI = A.IPCODPACI AND RE.NUMINGRES = A.NUMINGRES ORDER BY ID) AS CamaInicioEstancia,
	(Select Top 1 CA.DESCCAMAS FROM CHREGESTA RE INNER JOIN CHCAMASHO CA ON RE.CODICAMAS = CA.CODICAMAS WHERE RE.IPCODPACI = A.IPCODPACI AND RE.NUMINGRES = A.NUMINGRES ORDER BY ID DESC) AS CamaFinalEstancia,
	(Select Top 1 INU.UFUDESCRI FROM CHREGESTA RE INNER JOIN CHCAMASHO CA ON RE.CODICAMAS = CA.CODICAMAS INNER JOIN INUNIFUNC INU ON CA.UFUCODIGO = INU.UFUCODIGO WHERE RE.IPCODPACI = A.IPCODPACI AND RE.NUMINGRES = A.NUMINGRES ORDER BY ID) AS UnidadFuncionalInicioEstancia,
	(Select Top 1 INU.UFUDESCRI FROM CHREGESTA RE INNER JOIN CHCAMASHO CA ON RE.CODICAMAS = CA.CODICAMAS INNER JOIN INUNIFUNC INU ON CA.UFUCODIGO = INU.UFUCODIGO WHERE RE.IPCODPACI = A.IPCODPACI AND RE.NUMINGRES = A.NUMINGRES ORDER BY ID DESC) AS UnidadFuncionalFinalEstancia
	FROM CHREGESTA A
	INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI 
	INNER JOIN ADINGRESO C ON A.NUMINGRES = C.NUMINGRES
	INNER JOIN Causesofattention D ON C.ICAUSAING = D.Code
	INNER JOIN CHCAMASHO E ON A.CODICAMAS = E.CODICAMAS
	INNER JOIN Contract.HealthAdministrator F ON C.GENCONENTITY = F.Id
	WHERE E.CODCENATE = @CentroAtencion
)

SELECT 
	CedulaPaciente, NombrePaciente, FechaNAcimientoPaciente, NIngreso, Format(FechaIngreso, 'dd/mm/yyy HH:mm') AS FechaIngreso, CausaIngreso, 
	Format(FechaInicioEstancia, 'dd/mm/yyy HH:mm') AS FechaInicioEstancia, Entidad, Rtrim(UnidadFuncionalInicioEstancia) AS UnidadFuncionalInicioEstancia,
	CASE 
		WHEN FechaFinalEstancia IS NULL OR FechaFinalEstancia ='1900-01-01 00:00:00.000' THEN ''
		WHEN FechaFinalEstancia IS NOT NULL AND FechaFinalEstancia <> '1900-01-01 00:00:00.000' THEN Rtrim(UnidadFuncionalFinalEstancia)
	END AS 'UnidadFuncionalFinalEstancia',
	CASE 
		WHEN FechaFinalEstancia IS NULL OR FechaFinalEstancia ='1900-01-01 00:00:00.000' THEN ''
		WHEN FechaFinalEstancia IS NOT NULL AND FechaFinalEstancia <> '1900-01-01 00:00:00.000' THEN CONVERT(VARCHAR(20), Format(FechaFinalEstancia, 'dd/mm/yyy HH:mm'), 120)
	END AS 'FechaFinalEstancia',
	RTRIM(CamaInicioEstancia) AS 'CamaInicioEstancia',
	CASE 
		WHEN FechaFinalEstancia IS NULL OR FechaFinalEstancia ='1900-01-01 00:00:00.000' THEN ''
		WHEN FechaFinalEstancia IS NOT NULL AND FechaFinalEstancia <> '1900-01-01 00:00:00.000' THEN RTRIM(CamaFinalEstancia)
	END AS 'CamaFinalEstancia',
	CASE 
		WHEN FechaFinalEstancia = '1900-01-01 00:00:00.000' THEN DATEDIFF(d,FechaInicioEstancia,GETDATE())
		ELSE DATEDIFF(d,FechaInicioEstancia,FechaFinalEstancia) 
	END AS 'DiasTranscurridos',
	CASE 
		WHEN FechaFinalEstancia IS NULL OR FechaFinalEstancia ='1900-01-01 00:00:00.000' THEN '1. Pacientes con estancia vigente'
		WHEN FechaFinalEstancia IS NOT NULL AND FechaFinalEstancia <> '1900-01-01 00:00:00.000' THEN '2. Pacientes con egreso'
	END AS TipoEstancia
FROM DATOS where FechaInicioEstancia BETWEEN @FechaInicial AND @FechaFinal
  
END