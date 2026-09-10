
CREATE PROCEDURE [dbo].[SP_Other_Activities_EMR]
(
    @CodigoPaciente Varchar(25),
    @Page int, 
    @PageSize int
)
AS
BEGIN
	SET NOCOUNT ON;

    IF @Page < 1 SET @Page = 1;
    IF @PageSize < 1 SET @PageSize = 20;
    DECLARE @Offset INT = (@Page - 1) * @PageSize;
	
	SELECT  FECREGIST AS FechaRegistro,
            RTRIM(NOMCENATE) AS CentroAtencion,
            RTRIM(UFUDESCRI) AS Unidad,
            RTRIM(NOMMEDICO) AS Medico,
            RTRIM(NUMINGRES) AS NumeroIngreso,
            RTRIM(DESESPECI) AS Especialidad,
            CASE A.TITNOTENF 
                WHEN 'FINALIZACIÓN PREMATURA QUIMIOTERAPIA' THEN 'finalizacion_prematura_quimioterapia' 
                WHEN 'FINALIZACIÓN PREMATURA RADIOTERAPIA' THEN 'finalizacion_prematura_radioterapia' 
                ELSE 'general' 
            END AS TipoNota,
            'notas_de_enfermeria' as Agrupador, 
            RTRIM(A.CODCENATE) as CodigoCentroAtencion,
            RTRIM(A.UFUCODIGO) as CodigoUnidadFuncional
            FROM dbo.HCCTRNOTE AS A INNER JOIN
            dbo.INPACIENT B ON A.IPCODPACI=B.IPCODPACI INNER JOIN
            dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL INNER JOIN
            dbo.ADCENATEN D ON A.CODCENATE=D.CODCENATE INNER JOIN
            dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO LEFT OUTER JOIN
            dbo.INESPECIA F ON C.CODESPEC1=F.CODESPECI
            WHERE A.IPCODPACI= @CodigoPaciente

UNION ALL

    SELECT FECREGIST AS FechaRegistro,
            RTRIM(NOMCENATE) AS CentroAtencion,
            RTRIM(UFUDESCRI) AS Unidad,
            RTRIM(NOMMEDICO) AS Medico,
            RTRIM(NUMINGRES) AS NumeroIngreso,
            RTRIM(DESESPECI) AS Especialidad,
            'general' AS TipoNota,
            'notas_de_terapia' as Agrupador, 
            RTRIM(A.CODCENATE) as CodigoCentroAtencion,
            RTRIM(A.UFUCODIGO) as CodigoUnidadFuncional
            FROM dbo.HCCTRNOTT AS A INNER JOIN
            dbo.INPACIENT B ON A.IPCODPACI=B.IPCODPACI INNER JOIN
            dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL INNER JOIN
            dbo.ADCENATEN D ON A.CODCENATE=D.CODCENATE INNER JOIN
            dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO LEFT OUTER JOIN
            dbo.INESPECIA F ON C.CODESPEC1=F.CODESPECI
            WHERE A.IPCODPACI= @CodigoPaciente

UNION ALL

    SELECT  A.FECREGIST as FechaRegistro,
            RTRIM(NOMCENATE) AS CentroAtencion,
            RTRIM(UFUDESCRI) AS Unidad,
            RTRIM(NOMMEDICO) AS Medico,
            RTRIM(NUMINGRES) AS NumeroIngreso,
            RTRIM(DESESPECI) AS Especialidad, 
            'notas_instrumentador_quirurgico' AS TipoNota,
            'notas_instrumentador_quirurgico'  as Agrupador, 
            RTRIM(A.CODCENATE) as CodigoCentroAtencion,
            RTRIM(A.UFUCODIGO) as CodigoUnidadFuncional
            FROM dbo.SurgicalInstrumentNote AS A INNER JOIN
            dbo.INPACIENT B ON A.IPCODPACI=B.IPCODPACI INNER JOIN
            dbo.INPROFSAL C ON A.CODPROSAL=C.CODPROSAL INNER JOIN
            dbo.ADCENATEN D ON A.CODCENATE=D.CODCENATE INNER JOIN
            dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO LEFT OUTER JOIN
            dbo.INESPECIA F ON C.CODESPEC1=F.CODESPECI
            WHERE A.IPCODPACI= @CodigoPaciente
            ORDER BY A.FECREGIST DESC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

end
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Recupera de forma paginada el historial de actividades clínicas "otras" registradas en el EMR para un paciente específico, consolidando tres tipos de notas: notas de enfermería (con distinción de finalizaciones prematuras de quimio/radioterapia), notas de terapia y notas del instrumentador quirúrgico. Cada registro incluye centro de atención, unidad funcional, médico, número de ingreso y especialidad, agrupados bajo un campo clasificador por tipo de nota.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Other_Activities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Other_Activities_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida y pagina las notas clínicas no médicas (enfermería, terapia e instrumentación quirúrgica) de un paciente para su visualización en la historia clínica electrónica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Other_Activities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir en INPACIENT y tener notas asociadas vinculadas a profesional, centro de atención y unidad funcional válidos; Los códigos de página y tamaño de página se normalizan a valores mínimos (1 y 20) si llegan inválidos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Other_Activities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las notas de enfermería se etiquetan siempre con el agrupador ''notas_de_enfermeria''; Las notas de terapia se etiquetan siempre con el agrupador ''notas_de_terapia'' y TipoNota ''general''; Las notas de instrumentación quirúrgica se etiquetan siempre con agrupador y TipoNota ''notas_instrumentador_quirurgico''; Solo se devuelven notas pertenecientes al paciente solicitado; La especialidad puede ser nula porque se enlaza con LEFT OUTER JOIN; El resultado se ordena por fecha de registro descendente y se pagina', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Other_Activities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; notas de enfermería; notas de terapia; notas de instrumentación quirúrgica; finalización prematura de quimioterapia; finalización prematura de radioterapia; centro de atención; unidad funcional; especialidad médica; profesional de la salud; ingreso hospitalario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Other_Activities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve un único resultset con la unión (UNION ALL) de notas de enfermería, notas de terapia y notas de instrumentación quirúrgica del paciente, ordenado por fecha de registro descendente y paginado con OFFSET/FETCH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Other_Activities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Página recibida menor a 1 → Se fuerza la página a 1; si Tamaño de página menor a 1 → Se fuerza el tamaño de página a 20; si Título de la nota de enfermería = ''FINALIZACIÓN PREMATURA QUIMIOTERAPIA'' → Se clasifica el TipoNota como ''finalizacion_prematura_quimioterapia''; si Título de la nota de enfermería = ''FINALIZACIÓN PREMATURA RADIOTERAPIA'' → Se clasifica el TipoNota como ''finalizacion_prematura_radioterapia''; si Título de la nota de enfermería distinto a los anteriores → Se clasifica el TipoNota como ''general''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Other_Activities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCTRNOTE; dbo.HCCTRNOTT; dbo.SurgicalInstrumentNote; dbo.INPACIENT; dbo.INPROFSAL; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Other_Activities_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_Other_Activities_EMR';
-- GO
