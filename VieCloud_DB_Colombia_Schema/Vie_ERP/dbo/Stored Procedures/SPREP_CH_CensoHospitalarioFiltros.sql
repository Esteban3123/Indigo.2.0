-- Stored Procedure
CREATE PROCEDURE [dbo].[SPREP_CH_CensoHospitalarioFiltros]
(
@FechaInicialMayor Datetime,
@FechaInicialMenor Datetime,
@FechaFinalMayor Datetime,
@FechaFinalMenor Datetime,
@CentroAtencion Char(10) = NULL,
@UnidadFuncional Varchar(max) = NULL, -- Lista de codigos separados por coma, sin comillas (ej: '01,02,03'). NULL para no filtrar.
@Entidad Char(9) = NULL,
@ClaseHabitacion Int = 0,
@ClaseCama Int = 0
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
With EstanciasFiltradas as (
	SELECT
		IPCODPACI, NUMINGRES, CODICAMAS, FECINIEST, FECFINEST,
		CODTIPEST, REGUSUARI, FECREGSIS, CODPROSAL, CODESPECI
	FROM CHREGESTA with(nolock)
	WHERE
			(FECINIEST BETWEEN @FechaInicialMenor AND @FechaFinalMayor)
		OR
			(
				(FECINIEST BETWEEN @FechaInicialMenor AND @FechaInicialMayor)
				OR (FECFINEST >= @FechaFinalMayor OR FECFINEST = '1900-01-01 00:00:00')
			)
			AND FECINIEST <= @FechaFinalMayor
),
Datos as (

	SELECT DISTINCT
		B.NUMCAMHOS AS 'CODIGO CAMA',
		RTRIM(B.DESCCAMAS) AS 'DESCRIPCION CAMA', 
		CONVERT(VARCHAR(20), A.FECINIEST, 120) AS 'FECHA INICIAL',
		A.FECINIEST,
		CASE 
			WHEN A.FECFINEST IS NULL OR A.FECFINEST ='1900-01-01 00:00:00.000' THEN '-'
			WHEN A.FECFINEST IS NOT NULL AND A.FECFINEST <> '1900-01-01 00:00:00.000' THEN CONVERT(VARCHAR(20), A.FECFINEST, 120)
		END 'FECHA FINAL', 
		CONVERT(VARCHAR(20), A.FECREGSIS, 120) AS 'FECHA DE REGISTRO',
		A.CODTIPEST AS 'TIPO ESTANCIA', 
		A.NUMINGRES AS INGRESO, 
		--di.NOMDIAGNO AS 'DIAGNOSTICO PRINCIPAL',
		C.CODENTIDA AS 'CODIGO ENTIDAD', 
		G.NOMENTIDA AS 'NOMBRE ENTIDAD', 
		B.CODCENATE AS 'CODIGO CENTRO DE ATENCION', 
		D.NOMCENATE AS 'CENTRO DE ATENCION', 
		RTRIM(B.UFUCODIGO) AS 'CODIGO UNIDAD FUNCIONAL', 
		E.UFUDESCRI AS 'UNIDAD FUNCIONAL',
		RTRIM(F.DESTIPEST) AS ESTANCIA, 
		A.IPCODPACI AS 'CODIGO PACIENTE', 
		RTRIM(C.IPNOMCOMP) AS 'NOMBRE PACIENTE',
		C.IPFECNACI AS 'FECHA NACIMIENTO',
		B.CODCLAHAB AS 'CODIGO CLASE HABITACION',
		CASE B.CODCLAHAB
			WHEN 1 THEN 'Sala de observación' WHEN 2 THEN 'Sala de procedimientos' WHEN 3 THEN 'Sala de recuperación'
			WHEN 4 THEN 'Habitacion 1 cama' WHEN 5 THEN 'Habitacion 2 camas' WHEN 6 THEN 'Habitacion 3 camas'
			WHEN 7 THEN 'Habitacion 4 camas' WHEN 8 THEN 'Suite' WHEN 9 THEN 'Habitacion especial'
			WHEN 10 THEN 'Uci' WHEN 11 THEN 'Otro'
		END AS 'CLASE HABITACION',
		B.CODCLACAM AS 'CODIGO CLASE CAMA',
		CASE B.CODCLACAM
			WHEN 1 THEN 'Observacion Urgencias' WHEN 2 THEN 'Recuperacion Post-Quirurgico'
			WHEN 3 THEN 'Hospitalaria' WHEN 4 THEN 'Cuna de Observación'
		END AS 'CLASE CAMA',
		CASE 
			WHEN A.FECFINEST = '1900-01-01 00:00:00.000' THEN DATEDIFF(d,A.FECINIEST,GETDATE())
			ELSE DATEDIFF(d,A.FECINIEST,A.FECFINEST) 
		END AS 'DIAS TRANSCURRIDOS',
		CASE 
			WHEN EGREU.NOMUSUARI IS NULL THEN U.NOMUSUARI 
			ELSE EGREU.NOMUSUARI 
		END AS 'NOMBRE USUARIO',
		X.IFECHAING  as 'FECHA INGRESO',
		Prof.CODPROSAL AS 'CodigoMedico', 
		RTRIM(Prof.NOMMEDICO) AS 'NombreMedico', 
		Espe.CODESPECI AS 'CodigoEspecialidad',
		RTRIM(Espe.DESESPECI) AS 'DescripcionEspecialidad',
		DATEDIFF(YEAR, C.IPFECNACI, GETDATE()) as Edad,
		CASE WHEN H.INDICAPAC = '22' THEN '2 - Pre-alta hospitalaria' WHEN I.NUMINGRES IS NULL THEN '1 - Pacientes en la unidad' ELSE '3 - Pacientes con salida' END AS TipoEstancia
	FROM
		EstanciasFiltradas A
		INNER JOIN CHCAMASHO B with(nolock) ON A.CODICAMAS=B.CODICAMAS
		INNER JOIN INPacient C with(nolock) ON A.IPCODPACI=C.IPCODPACI
		INNER JOIN ADcenaten D with(nolock) ON B.CODCENATE=D.CODCENATE
		INNER JOIN INUNIFUNC E with(nolock) ON B.UFUCODIGO=E.UFUCODIGO
		INNER JOIN CHTIPESTA F with(nolock) ON A.CODTIPEST=F.CODTIPEST
		INNER JOIN INENTIDAD G with(nolock) ON C.CODENTIDA=G.CODENTIDA
		INNER JOIN ADINGRESO X with(nolock) ON X.NUMINGRES=A.NUMINGRES
		LEFT OUTER JOIN SEGUSUARU U with(nolock) ON A.REGUSUARI=U.CODUSUARI
		LEFT OUTER JOIN (SELECT NUMINGRES,CODUSUARI FROM CHREGEGRE with(nolock)) EGRE ON A.NUMINGRES=EGRE.NUMINGRES
		LEFT OUTER JOIN SEGUSUARU EGREU with(nolock) ON EGRE.CODUSUARI=EGREU.CODUSUARI
		LEFT OUTER JOIN INPROFSAL Prof with (nolock) ON A.CODPROSAL = Prof.CODPROSAL
		LEFT OUTER JOIN INESPECIA Espe with (nolock) ON A.CODESPECI = Espe.CODESPECI
		LEFT OUTER JOIN dbo.HCREGEGRE I with(nolock) ON A.NUMINGRES = I.NUMINGRES
		LEFT OUTER JOIN (
			SELECT h.IPCODPACI, h.NUMINGRES, h.INDICAPAC
			FROM HCHISPACA h with(nolock)
			INNER JOIN (
				SELECT IPCODPACI, NUMINGRES, MAX(FECHISPAC) AS MaxFecha
				FROM HCHISPACA with(nolock)
				GROUP BY IPCODPACI, NUMINGRES
			) m ON m.IPCODPACI = h.IPCODPACI AND m.NUMINGRES = h.NUMINGRES AND m.MaxFecha = h.FECHISPAC
		) H ON H.IPCODPACI = X.IPCODPACI AND H.NUMINGRES = X.NUMINGRES
	WHERE
		(
				(A.FECINIEST BETWEEN @FechaInicialMenor AND @FechaFinalMayor)
			OR
			( (A.FECINIEST BETWEEN @FechaInicialMenor AND @FechaInicialMayor )
				OR (A.FECFINEST >= @FechaFinalMayor OR A.FECFINEST = '1900-01-01 00:00:00') )
			 AND A.FECINIEST <= @FechaFinalMayor
		)
		AND (@CentroAtencion IS NULL OR B.CODCENATE = @CentroAtencion)
		AND (@UnidadFuncional IS NULL OR B.UFUCODIGO IN (SELECT value FROM STRING_SPLIT(@UnidadFuncional, ',')))
		AND (@Entidad IS NULL OR C.CODENTIDA = @Entidad)
		AND (@ClaseHabitacion = 0 OR B.CODCLAHAB = @ClaseHabitacion)
		AND (@ClaseCama = 0 OR B.CODCLACAM = @ClaseCama)
)

Select * from Datos ORDER BY FECINIEST DESC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el censo hospitalario filtrado por rango de fechas de inicio y fin de estancia. Consolida información de pacientes hospitalizados combinando datos de camas (ubicación, clase y unidad funcional), estado de estancia (urgencias, hospitalización, pre-alta o egreso), ingreso, entidad aseguradora, diagnóstico principal CIE-10, médico tratante con especialidad, días transcurridos en cama y nombre del usuario que registró el movimiento. Se apoya en los registros de estados de cama (CHREGESTA), el maestro de camas (CHCAMASHO), el directorio de pacientes (INPACIENT), los ingresos (ADINGRESO), diagnósticos por ingreso (INDIAGNOP), centros de atención (ADCENATEN), unidades funcionales (INUNIFUNC), tipos de estancia (CHTIPESTA) y entidades aseguradoras (INENTIDAD). Se usa para reportería operativa de ocupación hospitalaria, seguimiento de pacientes activos en salas y auditoría de estancias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_CensoHospitalarioFiltros';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_CH_CensoHospitalarioFiltros';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de censo hospitalario listando estancias de pacientes con datos de cama, ingreso, médico, entidad y clasificación del estado (en unidad, pre-alta o con salida) filtrado por rangos de fechas de inicio y fin de estancia.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben proveerse los cuatro rangos de fechas (inicial mayor/menor y final mayor/menor); Deben existir las tablas maestras de camas, pacientes, centros de atención, unidades funcionales, tipos de estancia, entidades, ingresos y diagnósticos para el INNER JOIN; El ingreso debe tener al menos un diagnóstico registrado en INDIAGNOP marcado como principal (CODDIAPRI = 1)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen estancias cuyo diagnóstico esté marcado como diagnóstico principal (CODDIAPRI = 1); La fecha final ''1900-01-01'' se interpreta como estancia abierta (sin egreso); Los días transcurridos para estancias abiertas se calculan hasta la fecha actual; El estado de la estancia se determina priorizando la última historia clínica del paciente para ese ingreso (orden por FECHISPAC desc); Se aplica DISTINCT para evitar duplicados producto de los joins con diagnósticos; El resultado se ordena por fecha inicial de estancia descendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Censo hospitalario; Estancia hospitalaria; Cama hospitalaria; Clase de habitación; Clase de cama; Unidad funcional; Centro de atención; Ingreso hospitalario; Egreso hospitalario; Diagnóstico principal; Pre-alta hospitalaria; Paciente; Entidad (aseguradora); Profesional de salud / Médico tratante; Especialidad médica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve el listado de estancias filtradas por los rangos de fecha y diagnóstico principal, ordenado por FECINIEST descendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FECFINEST IS NULL o igual a ''1900-01-01'' → Se muestra ''-'' como fecha final y se calculan días transcurridos contra GETDATE() else Se muestra la fecha final formateada y los días se calculan entre inicio y fin de estancia; si Existe usuario de egreso (CHREGEGRE) → Se reporta el nombre del usuario que registró el egreso else Se reporta el nombre del usuario que registró la estancia; si Última historia (HCHISPACA) tiene INDICAPAC=''22'' → Clasifica como ''2 - Pre-alta hospitalaria'' else Si no hay registro en HCREGEGRE clasifica como ''1 - Pacientes en la unidad''; en otro caso ''3 - Pacientes con salida''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHREGESTA; dbo.CHCAMASHO; dbo.INPacient; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHTIPESTA; dbo.INENTIDAD; dbo.ADINGRESO; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.SEGUSUARU; dbo.CHREGEGRE; dbo.INPROFSAL; dbo.INESPECIA; dbo.HCREGEGRE; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltros';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_CH_CensoHospitalarioFiltros';
-- GO
