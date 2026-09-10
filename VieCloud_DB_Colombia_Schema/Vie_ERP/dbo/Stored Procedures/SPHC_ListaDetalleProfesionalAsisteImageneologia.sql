

CREATE PROCEDURE [dbo].[SPHC_ListaDetalleProfesionalAsisteImageneologia]
(
    @Profesional VARCHAR(50),
    @CodCentrosAtencion VARCHAR(300)
)
AS
BEGIN
    SET NOCOUNT ON;

	-- Convertimos la lista de centros de atención en una tabla de valores usando STRING_SPLIT, Separamos la lista por comas
    WITH CentrosAtencion AS (
        SELECT value AS CodCentro
        FROM STRING_SPLIT(@CodCentrosAtencion, ',')
    ),
	------------------------------------------------------------
    -- Listado Individual por Profesional que Asiste una Lectura de Imagenologia
    ------------------------------------------------------------
	OrigenUnificado As (
	--Intrahospitalaria
		SELECT
			FECHLECT,
			IPCODPACI,
			CODSERIPS,
			NUMEFOLIO,
			NUMINGRES,
			CODPROSAL
		FROM HCORDIMAG
		WHERE AttendingProfessionalCode IS NOT NULL
			AND AttendingProfessionalCode = @Profesional
			AND CODCENATE IN (SELECT CodCentro FROM CentrosAtencion)

		UNION ALL
	--Ambulatoria
		SELECT
			FECHLECT,
			IPCODPACI,
			CODSERIPS,
			'' AS [NUMEFOLIO],
			NUMINGRES,
			CODPROSAL
		FROM AMBORDIMA
		WHERE AttendingProfessionalCode IS NOT NULL
			AND AttendingProfessionalCode = @Profesional
			AND CODCENATE IN (SELECT CodCentro FROM CentrosAtencion)
	)
	SELECT
		O.FECHLECT AS [FechaAsistencia],
		O.IPCODPACI AS [Identificacion],
		RTRIM(P.IPNOMCOMP) AS [NombrePaciente],
		CONCAT(RTRIM(P.CODENTIDA), ' - ', RTRIM(ET.Name)) AS [Entidad],
		CONCAT(RTRIM(O.CODSERIPS), ' - ', RTRIM(S.DESSERIPS)) AS [CUPS],
		O.NUMEFOLIO AS [FolioOrden],
		O.NUMINGRES AS [Ingreso],
		O.CODPROSAL AS [CodProfesionalPrincipal],
		RTRIM(I.NOMMEDICO) AS [NombreProfesionalPrincipal],
		CONCAT(RTRIM(I.CODESPEC1), ' - ', RTRIM(N.DESESPECI)) AS [Especialidad]
	FROM OrigenUnificado O
		INNER JOIN INPACIENT P ON O.IPCODPACI = P.IPCODPACI
		INNER JOIN INPROFSAL I ON O.CODPROSAL = I.CODPROSAL
		INNER JOIN INESPECIA N ON I.CODESPEC1 = N.CODESPECI
		INNER JOIN Contract.HealthAdministrator ET ON P.CODENTIDA = ET.Code
		INNER JOIN INCUPSIPS S ON O.CODSERIPS = S.CODSERIPS

END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el detalle de estudios de imagenología (radiología, ecografías, tomografías, resonancias, entre otros) asistidos o leídos por un profesional de la salud específico, filtrando por uno o varios centros de atención. Consolida en una sola consulta las órdenes de imagen tanto intrahospitalarias (HCORDIMAG) como ambulatorias (AMBORDIMA), identificando aquellas donde el profesional indicado figure como profesional asistente (AttendingProfessionalCode). Para cada registro devuelve la fecha de asistencia o lectura, la identificación y nombre del paciente, la entidad de salud o aseguradora (EPS/ARS), el código y descripción CUPS del servicio, el folio de la orden, el número de ingreso, y el nombre y especialidad del profesional principal que generó la orden. Se utiliza principalmente en reportes de productividad, auditoría de lectura de imágenes y control de carga asistencial por profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListaDetalleProfesionalAsisteImageneologia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListaDetalleProfesionalAsisteImageneologia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las lecturas de imagenología (intrahospitalarias y ambulatorias) que un profesional ha asistido en uno o varios centros de atención, enriquecidas con datos del paciente, entidad pagadora, CUPS y profesional principal.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListaDetalleProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código del profesional asistente debe corresponder a un valor existente en AttendingProfessionalCode de las órdenes de imagenología.; La lista de centros de atención debe entregarse separada por comas para ser procesada por STRING_SPLIT.; Cada orden debe tener paciente (INPACIENT), profesional principal (INPROFSAL), especialidad (INESPECIA), entidad (Contract.HealthAdministrator) y CUPS (INCUPSIPS) válidos, ya que los JOIN son INNER.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListaDetalleProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran órdenes con un profesional asistente registrado (AttendingProfessionalCode IS NOT NULL).; Las órdenes ambulatorias siempre se reportan con folio en blanco, diferenciándolas de las intrahospitalarias.; El filtrado por centro de atención es obligatorio: solo se incluyen registros cuyo CODCENATE esté en la lista parametrizada.; La consulta es de solo lectura; no modifica datos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListaDetalleProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Imagenología; Lectura de imagenología; Profesional asistente; Atención intrahospitalaria; Atención ambulatoria; Centro de atención; Paciente; Entidad administradora de salud; CUPS (servicio); Especialidad médica; Folio de orden; Ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListaDetalleProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve un único conjunto unificado de lecturas de imagenología intrahospitalarias (HCORDIMAG) y ambulatorias (AMBORDIMA) donde AttendingProfessionalCode coincide con el profesional indicado y CODCENATE pertenece a la lista de centros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListaDetalleProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen intrahospitalario (HCORDIMAG) con AttendingProfessionalCode no nulo e igual al profesional y centro en la lista → Incluye la fila con NUMEFOLIO real de la orden; si Origen ambulatorio (AMBORDIMA) con AttendingProfessionalCode no nulo e igual al profesional y centro en la lista → Incluye la fila con NUMEFOLIO vacío ('''') porque las órdenes ambulatorias no manejan folio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListaDetalleProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.AMBORDIMA; dbo.INPACIENT; dbo.INPROFSAL; dbo.INESPECIA; Contract.HealthAdministrator; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListaDetalleProfesionalAsisteImageneologia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListaDetalleProfesionalAsisteImageneologia';
-- GO
