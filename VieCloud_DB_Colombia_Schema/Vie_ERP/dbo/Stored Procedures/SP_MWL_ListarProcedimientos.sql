    -- =============================================
    -- Author:		Emanuel Olaya Penagos
    -- Create date: 09/01/2020
    -- Description: sp para listar en el Modality WorkList(MWL)
    -- =============================================
    CREATE PROCEDURE [dbo].[SP_MWL_ListarProcedimientos]
		@attentionCenter VARCHAR(100),
	    @ScheduledStationAeTitle VARCHAR(16),
		@Modality VARCHAR(16),
		@PatientsName VARCHAR(64),
		@PatientId VARCHAR(25),
		@ScheduledProcedureStepStartDateS VARCHAR(30),
		@ScheduledProcedureStepStartDateE VARCHAR(30)
AS BEGIN
SET NOCOUNT ON;
SELECT
	CONCAT(rtrim(ltrim(A.NUMINGRES)), '-', rtrim(ltrim(A.AUTO))) as numero_acceso,
    A.AUTO AS id_paso_procedimiento,
    A.NUMINGRES AS numero_solicitud,
    A.CODSERIPS as cups,
    A.FECORDMED as fecha_hora_solicitud,
    -- P.TIPMODALI as modalidad,
	RG.ABREVIACION as modalidad,
    S.DESSERIPS as nombre_cups,
    --VIEFU.UFUCODIGO AS codigo_interno_servicio,
    rtrim(ltrim(IP.IPCODPACI)) AS documento,
    rtrim(ltrim(IP.IPPRINOMB)) AS primer_nombre,
    rtrim(ltrim(IP.IPSEGNOMB)) AS segundo_nombre,
    rtrim(ltrim(IP.IPPRIAPEL)) AS primer_apellido,
    rtrim(ltrim(IP.IPSEGAPEL)) AS segundo_apellido,
    CASE
        IP.IPSEXOPAC
        WHEN 1 THEN 'M'
        WHEN 2 THEN 'F'
    END AS sexo,
    IP.IPFECNACI AS Fecha_nacimiento,
    PRO.NOMMEDICO as nombre_medico,
    'Rutinario' as prioridad,
	'UNKNOWN' as aet,
    'AMBULATORIO' as via_ingreso,
	A.CODCENATE as centro_atencion
FROM
    AMBORDIMA A
    INNER JOIN INPACIENT IP ON IP.IPCODPACI = A.IPCODPACI
    INNER JOIN INCUPSIPS S ON S.CODSERIPS = A.CODSERIPS
    INNER JOIN INPROFSAL PRO ON PRO.CODPROSAL = A.CODPROSAL
    INNER JOIN INCUPSSUB CS with(nolock) on CS.CODGRUSUB=S.CODGRUSUB
	INNER JOIN RISGRIMAGE RG with(nolock) on RG.ID = CS.IDRISGRIMAGE
WHERE
    A.ESTSERIPS = '1'
	AND A.CODCENATE in (select value from [dbo].[SplitString](@attentionCenter))
	--AND cast (A.FECORDMED as Date) = cast('20180826' as Date) -- DEMO PURPORSES
	--AND RG.ABREVIACION = 'CR'
	--AND (@Modality IS NULL OR RG.ABREVIACION = @Modality)
	--AND (@PatientsName IS NULL OR IP.IPNOMCOMP like @PatientsName)
	--AND (@PatientId IS NULL OR rtrim(ltrim(IP.IPCODPACI)) = @PatientId)
	--AND (@ScheduledProcedureStepStartDateS IS NULL OR A.FECORDMED >= @ScheduledProcedureStepStartDateS)
	--AND (@ScheduledProcedureStepStartDateE IS NULL OR A.FECORDMED < @ScheduledProcedureStepStartDateE)

UNION
ALL
SELECT
	CONCAT(rtrim(ltrim(A.NUMINGRES)), '-', rtrim(ltrim(A.AUTO))) as numero_acceso,
    A.AUTO AS id_paso_procedimiento,
    A.NUMINGRES AS numero_solicitud,
    A.CODSERIPS as cups,
    A.FECORDMED as fecha_hora_solicitud,
    -- P.TIPMODALI as modalidad,
	RG.ABREVIACION as modalidad,
    S.DESSERIPS as nombre_cups,
    rtrim(ltrim(IP.IPCODPACI)) AS documento,
    rtrim(ltrim(IP.IPPRINOMB)) AS primer_nombre,
    rtrim(ltrim(IP.IPSEGNOMB)) AS segundo_nombre,
    rtrim(ltrim(IP.IPPRIAPEL)) AS primer_apellido,
    rtrim(ltrim(IP.IPSEGAPEL)) AS segundo_apellido,
    CASE
        IP.IPSEXOPAC
        WHEN 1 THEN 'M'
        WHEN 2 THEN 'F'
    END AS sexo,
    IPFECNACI AS Fecha_nacimiento,
    PRO.NOMMEDICO as nombre_medico,
    CASE
		A.PRISERIPS
		when '1' then 'Urgente'
		when '2' then 'Rutinario'
		else 'Rutinario'
	END AS prioridad,
	'UNKNOWN' as aet,
    'HOSPITALARIO' as via_ingreso,
	A.CODCENATE as centro_atencion
FROM
    HCORDIMAG A
    INNER JOIN INPACIENT IP ON IP.IPCODPACI = A.IPCODPACI
    INNER JOIN INCUPSIPS S ON S.CODSERIPS = A.CODSERIPS
    INNER JOIN INPROFSAL PRO ON PRO.CODPROSAL = A.CODPROSAL
	INNER JOIN INCUPSSUB CS with(nolock) on CS.CODGRUSUB=S.CODGRUSUB
	INNER JOIN RISGRIMAGE RG with(nolock) on RG.ID = CS.IDRISGRIMAGE
WHERE
    A.ESTSERIPS = '1'
	AND A.CODCENATE in (select value from [dbo].[SplitString](@attentionCenter))
	--AND cast (A.FECORDMED as Date) = cast('20180826' as Date) -- DEMO PURPORSES
	--AND RG.ABREVIACION = 'CR'
	--AND (@Modality IS NULL OR RG.ABREVIACION = @Modality)
	--AND (@PatientsName IS NULL OR IP.IPNOMCOMP like @PatientsName)
	--AND (@PatientId IS NULL OR rtrim(ltrim(IP.IPCODPACI)) = @PatientId)
	--AND (@ScheduledProcedureStepStartDateS IS NULL OR A.FECORDMED >= @ScheduledProcedureStepStartDateS)
	--AND (@ScheduledProcedureStepStartDateE IS NULL OR A.FECORDMED < @ScheduledProcedureStepStartDateE) 

END

-- exec [dbo].[SP_MWL_ListarProcedimientos] '0545', null, null, null, null, null, null

-- select cast('20180328' as Date)
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los procedimientos de imágenes diagnósticas pendientes de atención para el módulo Modality Worklist (MWL) del sistema RIS/PACS, utilizado por equipos de radiología (rayos X, tomografía, ecografía, resonancia, etc.) para conocer qué estudios deben realizarse. Combina órdenes ambulatorias (AMBORDIMA) y órdenes hospitalarias (HCORDIMAG) con estado activo, enriqueciendo cada orden con los datos del paciente (cédula, nombre completo, sexo, fecha de nacimiento), el nombre del procedimiento CUPS (INCUPSIPS), la modalidad de imagen abreviada (RISGRIMAGE), el médico solicitante (INPROFSAL) y la vía de ingreso (ambulatorio u hospitalario). Filtra por centro de atención y permite acotar por rango de fechas de solicitud, nombre del paciente, documento o modalidad. Es el punto de integración entre el HIS y los equipos de imágenes diagnósticas para la lista de trabajo de la modalidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_MWL_ListarProcedimientos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_MWL_ListarProcedimientos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas (ambulatorias y hospitalarias) activas de los centros de atención indicados, en el formato requerido por el Modality WorkList (MWL) DICOM.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MWL_ListarProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de centros de atención debe contener uno o varios códigos separados, parseables por dbo.SplitString.; Deben existir relaciones válidas entre la orden, el paciente (INPACIENT), el CUPS (INCUPSIPS), el profesional (INPROFSAL), el subgrupo CUPS (INCUPSSUB) y el grupo de imagen (RISGRIMAGE).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MWL_ListarProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes con ESTSERIPS = ''1'' (estado activo).; El centro de atención de la orden debe pertenecer a la lista parseada del parámetro de centros.; El número de acceso siempre se compone como NUMINGRES-AUTO (sin espacios).; El AET siempre se devuelve como ''UNKNOWN''.; La modalidad reportada proviene de la abreviación del grupo de imagen RISGRIMAGE asociado al subgrupo CUPS.; Las órdenes ambulatorias siempre se etiquetan como ''AMBULATORIO'' y prioridad ''Rutinario''.; Las órdenes hospitalarias siempre se etiquetan como ''HOSPITALARIO''.; Los filtros por modalidad, nombre/ID de paciente y rango de fechas están comentados, por lo que no se aplican aunque se reciban parámetros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MWL_ListarProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Modality WorkList (MWL); Orden de imagen diagnóstica ambulatoria; Orden de imagen diagnóstica hospitalaria; Paciente; CUPS; Profesional de salud; Modalidad de imagen; Centro de atención; Prioridad de atención (Urgente/Rutinario); Vía de ingreso (Ambulatorio/Hospitalario); AET (Application Entity Title DICOM)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MWL_ListarProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] client: Devuelve el listado unificado (UNION ALL) de procedimientos de imagen ambulatorios y hospitalarios activos para los centros de atención indicados, etiquetados con su vía de ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MWL_ListarProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Sexo del paciente IPSEXOPAC = 1 → Se reporta sexo ''M'' else Si IPSEXOPAC = 2 se reporta ''F''; cualquier otro valor queda nulo; si Origen ambulatorio (AMBORDIMA) → Se asigna prioridad fija ''Rutinario'' y vía de ingreso ''AMBULATORIO''; si Origen hospitalario (HCORDIMAG) con PRISERIPS = ''1'' → Se asigna prioridad ''Urgente'' else Si PRISERIPS = ''2'' o cualquier otro valor, se asigna ''Rutinario''; vía de ingreso ''HOSPITALARIO''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MWL_ListarProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MWL_ListarProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AMBORDIMA; dbo.HCORDIMAG; dbo.INPACIENT; dbo.INCUPSIPS; dbo.INPROFSAL; dbo.INCUPSSUB; dbo.RISGRIMAGE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MWL_ListarProcedimientos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_MWL_ListarProcedimientos';
-- GO
