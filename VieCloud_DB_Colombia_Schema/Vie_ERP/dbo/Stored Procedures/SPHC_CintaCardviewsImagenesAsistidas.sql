

CREATE PROCEDURE [dbo].[SPHC_CintaCardviewsImagenesAsistidas]
(
    @FechaInicial DATETIME,
    @FechaFinal DATETIME,
    @CodCentrosAtencion VARCHAR(300)
)
AS
BEGIN
    SET NOCOUNT ON;

    	------------------------------------------------------------
    --	Cinta de Cardviews
    ------------------------------------------------------------
    -- Convertimos la lista de centros de atención en una tabla de valores usando STRING_SPLIT, Separamos la lista por comas
    WITH CentrosAtencion AS (
        SELECT value AS CodCentro
        FROM STRING_SPLIT(@CodCentrosAtencion, ',')
    ),
	OrigenUnificado AS (
	--Intrahospitalario
		SELECT 
			AttendingProfessionalCode,
			AttendingSpecialty,
			CODSERIPS
        FROM HCORDIMAG
        WHERE AttendingProfessionalCode IS NOT NULL
          AND FECHLECT BETWEEN @FechaInicial AND @FechaFinal
          AND CODCENATE IN (SELECT CodCentro FROM CentrosAtencion)

		UNION ALL

	--Ambulatoria
		SELECT 
			AttendingProfessionalCode,
			AttendingSpecialty,
			CODSERIPS
        FROM AMBORDIMA
        WHERE AttendingProfessionalCode IS NOT NULL
          AND FECHLECT BETWEEN @FechaInicial AND @FechaFinal
          AND CODCENATE IN (SELECT CodCentro FROM CentrosAtencion)
	),
	ProfesionalMayorAsistencia AS (
		SELECT TOP 1
			I.NOMMEDICO AS [Profesional],
			COUNT(*) AS [CantidadAsisitencia]
		FROM OrigenUnificado O
			INNER JOIN INPROFSAL I ON O.AttendingProfessionalCode = I.CODPROSAL
		GROUP BY O.AttendingProfessionalCode, I.NOMMEDICO
		ORDER BY COUNT(*) DESC
	),
	MayorServicio AS (
		SELECT TOP 1
			CONCAT(RTRIM(O.CODSERIPS), ' - ', RTRIM(S.DESSERIPS)) AS [Servicio]
		FROM OrigenUnificado O
			INNER JOIN INCUPSIPS S ON O.CODSERIPS = S.CODSERIPS
		GROUP BY O.CODSERIPS, S.DESSERIPS
		ORDER BY COUNT(*) DESC
	)
	SELECT 
		COUNT(O.AttendingProfessionalCode) AS [TotalAsistencias],
		(SELECT RTRIM(Servicio) FROM MayorServicio) AS [MayorServicioAsistido],
		COUNT(O.AttendingProfessionalCode) - (SELECT CantidadAsisitencia FROM ProfesionalMayorAsistencia) AS [AsistenciasOtrosProfesionales],
		(SELECT RTRIM(Profesional) FROM ProfesionalMayorAsistencia) AS [ProfesionalMayorAsistencia]
    FROM OrigenUnificado O
		INNER JOIN INPROFSAL I ON O.AttendingProfessionalCode = I.CODPROSAL
		INNER JOIN INESPECIA N ON O.AttendingSpecialty = N.CODESPECI
		INNER JOIN CONTRACT.CUPSEntity CE On CE.Code = O.CODSERIPS
		INNER JOIN Contract.CupsSubgroup G ON G.Id = CE.CUPSSubGroupId
		INNER JOIN RISGRIMAGE R ON R.ID = G.IdRisGrImage

END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera indicadores tipo ''cinta de tarjetas'' (cardviews) sobre imágenes diagnósticas asistidas —radiologías, ecografías, tomografías, resonancias y similares— en un rango de fechas y uno o varios centros de atención. Consolida en una sola consulta las órdenes de imágenes intrahospitalarias (HCORDIMAG) y ambulatorias (AMBORDIMA), filtrando por fecha de lectura del estudio, para calcular: el total de asistencias, el profesional de salud con mayor cantidad de imágenes leídas, el servicio CUPS más frecuente y la cantidad de asistencias atribuidas a otros profesionales. Cruza el maestro de profesionales (INPROFSAL), el catálogo de servicios CUPS (INCUPSIPS y CUPSEntity), las especialidades médicas (INESPECIA) y la clasificación de grupos de imágenes (RIS), entregando métricas resumidas para tableros de monitoreo y seguimiento de productividad en imagenología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_CintaCardviewsImagenesAsistidas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_CintaCardviewsImagenesAsistidas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve indicadores agregados (total de asistencias, servicio más asistido, profesional con mayor asistencia y asistencias de otros profesionales) sobre órdenes de imágenes diagnósticas intrahospitalarias y ambulatorias en un rango de fechas y centros de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_CintaCardviewsImagenesAsistidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La lista de centros de atención debe venir como cadena separada por comas, compatible con STRING_SPLIT.; El rango de fechas (inicial y final) debe estar definido para filtrar por FECHLECT.; Las tablas maestras INPROFSAL, INESPECIA, INCUPSIPS, CUPSEntity, CupsSubgroup y RISGRIMAGE deben tener correspondencia con los códigos de las órdenes para que la fila se incluya en el resultado final.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_CintaCardviewsImagenesAsistidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes con profesional tratante asignado (AttendingProfessionalCode IS NOT NULL).; El rango de fechas se evalúa sobre la fecha de lectura (FECHLECT) de la orden.; Las asistencias se restringen a los centros de atención indicados en el parámetro (lista separada por comas).; Se unifican órdenes intrahospitalarias (HCORDIMAG) y ambulatorias (AMBORDIMA) para el cálculo agregado.; El ''profesional con mayor asistencia'' es único: solo el primero en orden descendente de conteo (TOP 1).; El ''mayor servicio asistido'' es único: solo el servicio CUPS con mayor número de órdenes (TOP 1).; Las asistencias de otros profesionales se calculan como total menos las del profesional con mayor asistencia.; El conteo final exige que el servicio CUPS exista en CUPSEntity, esté ligado a un CupsSubgroup y a un grupo RIS de imágenes (RISGRIMAGE), excluyendo órdenes cuyo servicio no esté parametrizado como imagen.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_CintaCardviewsImagenesAsistidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de imágenes diagnósticas intrahospitalarias; Órdenes de imágenes diagnósticas ambulatorias; Profesional asistencial (médico tratante); Especialidad médica; Servicios CUPS; Subgrupo CUPS de imagenología (RIS); Centros de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_CintaCardviewsImagenesAsistidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Retorna una fila con TotalAsistencias, MayorServicioAsistido, AsistenciasOtrosProfesionales y ProfesionalMayorAsistencia, calculada sobre la unión de HCORDIMAG y AMBORDIMA filtradas por FECHLECT entre @FechaInicial y @FechaFinal y CODCENATE en la lista de centros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_CintaCardviewsImagenesAsistidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.AMBORDIMA; dbo.INPROFSAL; dbo.INCUPSIPS; dbo.INESPECIA; CONTRACT.CUPSEntity; Contract.CupsSubgroup; dbo.RISGRIMAGE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_CintaCardviewsImagenesAsistidas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_CintaCardviewsImagenesAsistidas';
-- GO
