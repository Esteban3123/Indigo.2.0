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
	CedulaPaciente, NombrePaciente, FechaNAcimientoPaciente, NIngreso, Format(FechaIngreso, 'dd/MM/yyy HH:mm') AS FechaIngreso, CausaIngreso, 
	Format(FechaInicioEstancia, 'dd/MM/yyy HH:mm') AS FechaInicioEstancia, Entidad, Rtrim(UnidadFuncionalInicioEstancia) AS UnidadFuncionalInicioEstancia,
	CASE 
		WHEN FechaFinalEstancia IS NULL OR FechaFinalEstancia ='1900-01-01 00:00:00.000' THEN ''
		WHEN FechaFinalEstancia IS NOT NULL AND FechaFinalEstancia <> '1900-01-01 00:00:00.000' THEN Rtrim(UnidadFuncionalFinalEstancia)
	END AS 'UnidadFuncionalFinalEstancia',
	CASE 
		WHEN FechaFinalEstancia IS NULL OR FechaFinalEstancia ='1900-01-01 00:00:00.000' THEN ''
		WHEN FechaFinalEstancia IS NOT NULL AND FechaFinalEstancia <> '1900-01-01 00:00:00.000' THEN Format(FechaFinalEstancia, 'dd/MM/yyy HH:mm')
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el censo histórico general de pacientes hospitalizados en un centro de atención, para un rango de fechas determinado. Consolida información de ingresos (ADINGRESO), datos demográficos del paciente (INPACIENT), estancias y movimientos de cama (CHREGESTA), camas hospitalarias (CHCAMASHO) y el pagador o EPS correspondiente (HealthAdministrator), permitiendo ver por cada ingreso: la cédula y nombre del paciente, la causa de ingreso, las fechas y camas de inicio y fin de estancia, las unidades funcionales de inicio y salida, y los días transcurridos. Distingue entre pacientes con estancia vigente (aún hospitalizados) y pacientes con egreso, siendo útil para reportes de ocupación hospitalaria, auditoría de camas y seguimiento de estancias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CH_ListCensusHistoricGeneral';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CH_ListCensusHistoricGeneral';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el histórico de censo hospitalario por centro de atención, mostrando pacientes con estancia vigente o egresada dentro de un rango de fechas de inicio de estancia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_ListCensusHistoricGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe corresponder a un valor válido en CHCAMASHO.CODCENATE; Las fechas inicial y final deben ser comparables como datetime contra FECINIEST; Cada ingreso debe tener registro en CHREGESTA, INPACIENT, ADINGRESO, Causesofattention, CHCAMASHO y Contract.HealthAdministrator para aparecer en el resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_ListCensusHistoricGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'FechaInicioEstancia se toma del primer registro de CHREGESTA del ingreso (ORDER BY ID ASC) y FechaFinalEstancia del último (ORDER BY ID DESC); La cama y unidad funcional iniciales/finales se derivan del mismo orden cronológico por ID en CHREGESTA; Una fecha final igual a ''1900-01-01'' se interpreta como ausencia de egreso (estancia vigente); La edad del paciente se calcula con la función dbo.Edad respecto a la fecha actual; Solo se incluyen ingresos cuya cama pertenece al centro de atención solicitado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_ListCensusHistoricGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Estancia hospitalaria; Cama hospitalaria; Unidad funcional; Centro de atención; Causa de ingreso; Administradora de salud (entidad); Egreso; Censo hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_ListCensusHistoricGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve un set tabular de censo histórico filtrado por CHCAMASHO.CODCENATE = @CentroAtencion y FechaInicioEstancia BETWEEN @FechaInicial AND @FechaFinal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_ListCensusHistoricGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FechaFinalEstancia IS NULL o ''1900-01-01 00:00:00.000'' → Se considera estancia vigente: UnidadFuncionalFinalEstancia, FechaFinalEstancia y CamaFinalEstancia se devuelven vacíos y TipoEstancia=''1. Pacientes con estancia vigente'' else Se considera egreso: se muestran los datos finales de unidad funcional, fecha y cama, y TipoEstancia=''2. Pacientes con egreso''; si FechaFinalEstancia = ''1900-01-01 00:00:00.000'' → DiasTranscurridos = DATEDIFF(d, FechaInicioEstancia, GETDATE()) else DiasTranscurridos = DATEDIFF(d, FechaInicioEstancia, FechaFinalEstancia)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_ListCensusHistoricGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_ListCensusHistoricGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.INPACIENT; dbo.ADINGRESO; dbo.Causesofattention; dbo.CHCAMASHO; Contract.HealthAdministrator; dbo.INUNIFUNC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_ListCensusHistoricGeneral';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CH_ListCensusHistoricGeneral';
-- GO
