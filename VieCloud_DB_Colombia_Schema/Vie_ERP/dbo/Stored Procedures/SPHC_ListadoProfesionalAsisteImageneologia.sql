

CREATE PROCEDURE [dbo].[SPHC_ListadoProfesionalAsisteImageneologia]
(
    @FechaInicial DATETIME,
    @FechaFinal DATETIME,
    @CodCentrosAtencion VARCHAR(300)
)
WITH RECOMPILE
AS
BEGIN
    SET NOCOUNT ON;

	WITH CentrosAtencion AS (
        SELECT value AS CodCentro
        FROM STRING_SPLIT(@CodCentrosAtencion, ',')
    ),
	--Trae los datos de Ordenes Medicas Imagenes Dx Asistidas en la lectura por un profesional
	--Intrahospitalario
	OrigenUnificado AS (
		SELECT
			H.AttendingProfessionalCode,
			H.AttendingSpecialty,
			H.CODSERIPS
		FROM HCORDIMAG H
		WHERE H.AttendingProfessionalCode IS NOT NULL
			  AND H.FECHLECT BETWEEN @FechaInicial AND @FechaFinal
			  AND H.CODCENATE IN (SELECT CodCentro FROM CentrosAtencion)

		UNION ALL
	--Ambulatorio
		SELECT
			A.AttendingProfessionalCode,
			A.AttendingSpecialty,
			A.CODSERIPS
		FROM AMBORDIMA A
		WHERE A.AttendingProfessionalCode IS NOT NULL
			  AND A.FECHLECT BETWEEN @FechaInicial AND @FechaFinal
			  AND A.CODCENATE IN (SELECT CodCentro FROM CentrosAtencion)
	)
	SELECT 
		RTRIM(O.AttendingProfessionalCode) AS [CodProfesionalAsiste],
		MAX(RTRIM(I.NOMMEDICO)) AS [NombreProfesionalAsiste],
		MAX(RTRIM(N.DESESPECI)) AS [Especialidad],
		RTRIM(R.NOMBRE) AS [Modalidad],
		COUNT(O.CODSERIPS) AS [CantidadAsistida]
	FROM OrigenUnificado O
		INNER JOIN INPROFSAL I ON O.AttendingProfessionalCode = I.CODPROSAL
		INNER JOIN INESPECIA N ON O.AttendingSpecialty = N.CODESPECI
		INNER JOIN CONTRACT.CUPSEntity CE On CE.Code = O.CODSERIPS
		INNER JOIN Contract.CupsSubgroup G ON G.Id = CE.CUPSSubGroupId
		INNER JOIN RISGRIMAGE R ON R.ID = G.IdRisGrImage
	GROUP BY
		O.AttendingProfessionalCode,
		O.AttendingSpecialty,
		R.NOMBRE
END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el listado de profesionales de la salud que asistieron (leyeron) órdenes de imágenes diagnósticas en un rango de fechas y para uno o varios centros de atención seleccionados. Consolida en un único resultado tanto las órdenes intrahospitalarias (HCORDIMAG) como las ambulatorias (AMBORDIMA), filtrando únicamente aquellas que tienen un profesional asistente registrado en la fecha de lectura. Para cada profesional muestra su código, nombre completo (desde el maestro INPROFSAL), especialidad (desde INESPECIA), la modalidad de imagen (radiología, ecografía, tomografía, resonancia, etc., desde RISGRIMAGE a través de la clasificación CUPS contractual) y la cantidad de imágenes asistidas. Se usa principalmente en reportes de productividad y control de lectura de imágenes diagnósticas por profesional, especialidad y modalidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListadoProfesionalAsisteImageneologia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListadoProfesionalAsisteImageneologia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista la cantidad de imágenes diagnósticas leídas/asistidas por cada profesional, agrupadas por especialidad y modalidad, unificando órdenes intrahospitalarias y ambulatorias dentro de un rango de fechas y centros de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas debe ser válido para filtrar por fecha de lectura.; La lista de centros de atención debe venir como cadena separada por comas.; Los códigos de profesional y especialidad deben existir en los catálogos INPROFSAL e INESPECIA.; El servicio CUPS de la orden debe existir en CUPSEntity y tener subgrupo asociado a un grupo de imagenología (RISGRIMAGE).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se contabilizan órdenes con profesional asistente registrado (AttendingProfessionalCode IS NOT NULL).; Solo se incluyen órdenes cuya fecha de lectura (FECHLECT) esté dentro del rango solicitado.; Solo se consideran órdenes pertenecientes a los centros de atención indicados en la lista.; Se unifican órdenes de imágenes intrahospitalarias y ambulatorias en un mismo conteo.; El conteo se agrupa por profesional, especialidad y modalidad de imagen (vía RISGRIMAGE a través del subgrupo CUPS).; Solo se incluyen órdenes cuyo CUPS exista en CUPSEntity y esté asociado a un subgrupo con modalidad de imagen definida (joins INNER).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes médicas de imágenes diagnósticas; Atención intrahospitalaria; Atención ambulatoria; Profesional asistente (lectura de imágenes); Especialidad médica; Modalidad de imagenología; Centro de atención; Servicios CUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] OrigenUnificado (HCORDIMAG + AMBORDIMA): Devuelve resultset con CodProfesionalAsiste, NombreProfesionalAsiste, Especialidad, Modalidad y CantidadAsistida (COUNT de CODSERIPS) cuando AttendingProfessionalCode IS NOT NULL, FECHLECT entre fechas y CODCENATE en la lista de centros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.AMBORDIMA; dbo.INPROFSAL; dbo.INESPECIA; CONTRACT.CUPSEntity; Contract.CupsSubgroup; dbo.RISGRIMAGE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProfesionalAsisteImageneologia';
-- GO
