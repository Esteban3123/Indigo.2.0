
CREATE PROCEDURE [dbo].[SP_NOTAS_ListarHistoricoNotasAdministrativas_EMR]
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

    ;WITH Notas AS
    (
        SELECT 
            'notas_administrativas' AS Tipo,
            RTRIM(D.CODIGO) AS Codigo, 
            A.ID AS IdNota, 
            RTRIM(A.IPCODPACI) AS CodigoPaciente,
            RTRIM(A.FECHACREACION) AS FechaCreacion, 
            RTRIM(C.NOMCENATE) AS CentroAtencion, 
            RTRIM(C.CODCENATE) AS CodigoCentroAtencion,
            RTRIM(B.NOMUSUARI) AS Medico, 
            RTRIM(A.NUMINGRES) AS NumeroIngreso, 
            A.IDNOTAADMINISTRATIVA AS IdNotaAdmin, 
            RTRIM(D.CODIGO) + ' - ' + RTRIM(D.NOMBRE) AS Formato,
            RTRIM(D.NOMBRE) AS NombreNota
        FROM NTNOTASADMINISTRATIVASC A 
            INNER JOIN SEGusuaru B ON A.CODUSUARI = B.CODUSUARI 
            INNER JOIN ADCENATEN C ON A.CODCENATE = C.CODCENATE 
            INNER JOIN NTADMINISTRATIVAS D ON A.IDNOTAADMINISTRATIVA = D.ID 
        WHERE A.IPCODPACI = @CodigoPaciente 

        UNION ALL

        SELECT 	
            'Formatos_educativos_encuestas' AS Tipo,
            RTRIM(D.Code) AS Codigo, 
            A.ID AS IdNota, 
            A.IPCODPACI AS CodigoPaciente, 
            RTRIM(A.DateCreation) AS FechaCreacion, 
            RTRIM(C.NOMCENATE) AS CentroAtencion, 
            RTRIM(C.CODCENATE) AS CodigoCentroAtencion,
            RTRIM(B.NOMMEDICO) AS Medico, 
            RTRIM(NUMINGRES) AS NumeroIngreso, 
            RTRIM(A.IdParamEducationFormatsC) AS IdNotaAdmin, 
            RTRIM(D.Code) + ' - ' + RTRIM(D.Description) AS Formato,
            RTRIM(D.Description) AS Nombre
        FROM PatientEducationFormatsC A 
            INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL
            INNER JOIN ADCENATEN C ON A.CODCENATE = C.CODCENATE 
            INNER JOIN ParamEducationFormatsC D ON A.IdParamEducationFormatsC = D.ID 
        WHERE A.IPCODPACI = @CodigoPaciente
    )

    SELECT *
    FROM Notas
    ORDER BY FechaCreacion DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento paginado que recupera el historial consolidado de dos tipos de documentos clínico-administrativos para un paciente: notas administrativas (de `NTNOTASADMINISTRATIVASC`) y formatos educativos/encuestas (de `PatientEducationFormatsC`), combinándolos mediante `UNION ALL`. Los resultados se ordenan por fecha de creación descendente e incluyen datos del centro de atención, usuario/médico responsable y número de ingreso, orientado a la visualización en el módulo EMR.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista paginada del histórico unificado de notas administrativas y formatos educativos/encuestas asociados a un paciente, ordenado por fecha de creación descendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe existir y tener notas en NTNOTASADMINISTRATIVASC o registros en PatientEducationFormatsC; Las notas administrativas requieren usuario válido en SEGusuaru, centro de atención válido en ADCENATEN y formato definido en NTADMINISTRATIVAS; Los formatos educativos requieren profesional de salud válido en INPROFSAL, centro de atención en ADCENATEN y parámetro de formato en ParamEducationFormatsC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La paginación nunca usa valores menores a 1 para página o tamaño; El offset siempre se calcula como (Page-1)*PageSize; Solo se retornan registros cuyo IPCODPACI coincida exactamente con el paciente solicitado; Los registros se etiquetan con un Tipo discriminador (''notas_administrativas'' o ''Formatos_educativos_encuestas'') que identifica su origen; Solo se incluyen notas/formatos con relaciones íntegras (INNER JOIN) a usuario/profesional, centro de atención y catálogo de formato', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Nota administrativa; Formato educativo; Encuesta; Centro de atención; Médico/Profesional de salud; Ingreso (episodio); Histórico clínico (EMR)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Notas (resultset): Devuelve la unión de notas administrativas y formatos educativos/encuestas del paciente, paginada con OFFSET/FETCH y ordenada por FechaCreacion DESC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Page < 1 → Se fuerza @Page = 1 (normalización de paginación); si @PageSize < 1 → Se fuerza @PageSize = 20 (tamaño de página por defecto)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.NTNOTASADMINISTRATIVASC; dbo.SEGusuaru; dbo.ADCENATEN; dbo.NTADMINISTRATIVAS; dbo.PatientEducationFormatsC; dbo.INPROFSAL; dbo.ParamEducationFormatsC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_NOTAS_ListarHistoricoNotasAdministrativas_EMR';
-- GO
