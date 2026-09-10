CREATE PROCEDURE [dbo].[SPCH_GestionHospitalariaPacientesHospitalizados_EMR]
(
    @CentroAtencion   CHAR(10),
    @UnidadFuncional  CHAR(10),
    @SearchText       VARCHAR(250),
    @Page             INT,
    @PageSize         INT,
    @Usuario          CHAR(20),
    @Grupo            CHAR(3)
)
AS
BEGIN
    SET NOCOUNT ON;

    ---------------------------------------------------
    -- Normalizar paginación
    ---------------------------------------------------
    IF (@Page < 1) SET @Page = 1;
    IF (@PageSize < 1) SET @PageSize = 20;

    DECLARE @Offset INT = (@Page - 1) * @PageSize;

    ---------------------------------------------------
    -- Crear tabla temporal para permisos
    ---------------------------------------------------
    CREATE TABLE #UFPermisos (
        Codigo CHAR(10),
        UnidadFuncional VARCHAR(200),
        TipoUnidadFuncional INT,
        NombreTipoUnidadFuncional VARCHAR(100),
        CodigoDescripcion VARCHAR(300)
    );

    ---------------------------------------------------
    -- Llamar al SP de permisos
    ---------------------------------------------------
    INSERT INTO #UFPermisos
    EXEC dbo.SP_SEG_UnidadFuncional_Autorizado 
         @Usuario      = @Usuario,
         @Grupo        = @Grupo,
         @CentroAtencion = @CentroAtencion;

    ---------------------------------------------------
    -- Query principal con permisos
    ---------------------------------------------------
    SELECT DISTINCT
        CASE 
            WHEN X.INDICAPAC = '22' THEN 'presalida'
            WHEN I.NUMINGRES IS NULL THEN 'en_unidad'
            ELSE 'salida'
        END AS Egreso,
        RTRIM(C.NUMINGRES) AS Ingreso,
        RTRIM(H.IPNOMCOMP) AS Paciente,
        RTRIM(H.IPPRINOMB) AS PrimerNombre,
        RTRIM(H.IPSEGNOMB) AS SegundoNombre,
        RTRIM(H.IPPRIAPEL) AS PrimerApellido,
        RTRIM(H.IPSEGAPEL) AS SegundoApellido,
        C.IPCODPACI AS Identificacion,
        E.UFUCODIGO AS CodigoUnidadFuncional,
        RTRIM(E.UFUDESCRI) AS UnidadFuncional,
        H.IPFECNACI AS FechaNacimiento,
        A.CODICAMAS CodigoCama,
        RTRIM(A.DESCCAMAS) AS Cama,
        dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion,
        H.IPDIRECCI AS Direccion,
        H.IPTELEFON AS Telefono,
        H.IPSEXOPAC AS Sexo,
        RTRIM(NUMEFOLIO) AS Folio,
        RTRIM(K.DESESPECI) AS DescripcionEspecialidad,
        RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) AS Diagnostico,
        dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) AS PUNTAJEDOWN,
        dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) AS PUNTAJERASS,
        dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) AS PUNTAJENORTON,
        dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) AS PUNTAJEVAS,
        dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) AS PUNTAJEAPACHE,
        J.CODTIPPAC AS TipoPaciente,
        RTRIM(P.CODENTIDA) + '-' + RTRIM(P.NOMENTIDA) AS EntidadPaciente
    FROM dbo.CHCAMASHO A WITH (NOLOCK)
    INNER JOIN dbo.INUNIFUNC E WITH (NOLOCK)
        ON A.UFUCODIGO = E.UFUCODIGO
    LEFT JOIN dbo.CHREGESTA C WITH (NOLOCK)
        ON A.CODICAMAS = C.CODICAMAS AND C.REGESTADO = 1
    INNER JOIN dbo.INPACIENT H WITH (NOLOCK)
        ON C.IPCODPACI = H.IPCODPACI
    LEFT JOIN dbo.ADINGRESO J WITH (NOLOCK)
        ON C.NUMINGRES = J.NUMINGRES
    LEFT JOIN dbo.HCREGEGRE I WITH (NOLOCK)
        ON C.NUMINGRES = I.NUMINGRES
    OUTER APPLY (
        SELECT TOP 1 INDICAPAC
        FROM HCHISPACA 
        WHERE IPCODPACI = J.IPCODPACI 
          AND NUMINGRES = J.NUMINGRES
        ORDER BY FECHISPAC DESC
    ) AS X
    LEFT JOIN dbo.INESPECIA K WITH (NOLOCK)
        ON C.CODESPECI = K.CODESPECI
    LEFT JOIN dbo.INENTIDAD P WITH (NOLOCK)
        ON P.CODENTIDA = J.CODENTIDA
    LEFT JOIN dbo.INDIAGNOS Q WITH (NOLOCK)
        ON Q.CODDIAGNO = (
            SELECT TOP 1 CODDIAGNO
            FROM INDIAGNOP
            WHERE IPCODPACI = H.IPCODPACI 
              AND NUMINGRES = J.NUMINGRES 
              AND CODDIAPRI = 1
        )
    WHERE 
        ---------------------------------------------------
        -- Centro actual
        ---------------------------------------------------
        A.CODCENATE = @CentroAtencion

        ---------------------------------------------------
        -- Filtrar SOLO unidades autorizadas por el médico
        ---------------------------------------------------
        AND A.UFUCODIGO IN (SELECT Codigo FROM #UFPermisos)

        ---------------------------------------------------
        -- Filtro opcional por unidad específica
        ---------------------------------------------------
        AND (@UnidadFuncional IS NULL OR A.UFUCODIGO = @UnidadFuncional)

        ---------------------------------------------------
        -- Filtro por búsqueda
        ---------------------------------------------------
        AND (
            @SearchText IS NULL OR @SearchText = ''
            OR H.IPNOMCOMP LIKE '%' + @SearchText + '%'
            OR C.IPCODPACI LIKE '%' + @SearchText + '%'
            OR A.DESCCAMAS LIKE '%' + @SearchText + '%'
            OR Q.NOMDIAGNO LIKE '%' + @SearchText + '%'
            OR Q.CODDIAGNO LIKE '%' + @SearchText + '%'
        )
        AND ESTADCAMA IN ('2','8')
    ORDER BY A.CODICAMAS ASC
    -- ORDER BY Ingreso DESC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista paginada de pacientes actualmente hospitalizados en un centro de atención, pensada para el módulo de gestión hospitalaria (EMR). Cruza información de camas, unidades funcionales, datos del paciente, ingresos, egresos y la última nota clínica para determinar el estado del paciente: ''en unidad'', ''pre-salida'' o ''salida''. Para cada paciente hospitalizado devuelve datos clave como nombre, cédula o identificación, cama asignada, clase de habitación, diagnóstico principal (CIE-10), entidad aseguradora, especialidad médica y los puntajes de escalas clínicas de riesgo (Down-Ton, RASS, Norton, VAS, APACHE). Permite filtrar por centro de atención, unidad funcional y texto libre (nombre del paciente, documento, cama o diagnóstico), validando además los permisos de acceso del usuario a las unidades funcionales autorizadas mediante el procedimiento SP_SEG_UnidadFuncional_Autorizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_GestionHospitalariaPacientesHospitalizados_EMR';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_GestionHospitalariaPacientesHospitalizados_EMR';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista paginada de pacientes hospitalizados (camas ocupadas/reservadas) del centro de atención, restringida a las unidades funcionales autorizadas al usuario, con datos clínicos, escalas y estado de egreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El usuario y grupo deben tener unidades funcionales autorizadas vía SP_SEG_UnidadFuncional_Autorizado para el centro de atención dado; Las camas deben pertenecer al centro de atención solicitado (CODCENATE); Solo se consideran camas con ESTADCAMA en (''2'',''8''); Solo se considera el registro de estancia activo (CHREGESTA.REGESTADO = 1)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven camas en estados ''2'' u ''8'' (ocupada/reservada) del centro indicado; El acceso a unidades funcionales está siempre limitado a las autorizadas por seguridad para el usuario y grupo; El diagnóstico mostrado es siempre el principal (CODDIAPRI = 1) del ingreso; La estancia mostrada es siempre la activa (REGESTADO = 1); El estado de presalida prevalece sobre la existencia de egreso o la permanencia en unidad; Las escalas clínicas (Down/Ton, Rass, Norton, Vas, Apache) se calculan por ingreso y paciente vía funciones escalares', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente hospitalizado; Cama hospitalaria; Unidad funcional; Centro de atención; Ingreso/Admisión; Egreso hospitalario; Presalida; Diagnóstico principal (CIE); Especialidad médica; Entidad/Asegurador; Clase de habitación; Escalas clínicas (Down-Ton, RASS, Norton, VAS, APACHE); Autorización por unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] #UFPermisos: Se cargan las unidades funcionales autorizadas al usuario/grupo en el centro de atención llamando a SP_SEG_UnidadFuncional_Autorizado; [RETURN_RESULT] resultset: Devuelve resultado paginado (OFFSET/FETCH) ordenado por CODICAMAS ascendente, con tamaño y página normalizados (Page>=1, PageSize>=1, default 20)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Última HCHISPACA.INDICAPAC = ''22'' → Se etiqueta el egreso como ''presalida'' else Si no existe registro en HCREGEGRE para el ingreso → ''en_unidad''; en otro caso → ''salida''; si @UnidadFuncional IS NULL → No filtra por unidad funcional (todas las autorizadas) else Filtra solo la unidad funcional indicada; si @SearchText IS NULL o vacío → No aplica filtro de búsqueda else Filtra por coincidencia LIKE en nombre de paciente, identificación, descripción de cama, código o nombre del diagnóstico; si @Page < 1 o @PageSize < 1 → Se normalizan a 1 y 20 respectivamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SP_SEG_UnidadFuncional_Autorizado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.INPACIENT; dbo.ADINGRESO; dbo.HCREGEGRE; dbo.HCHISPACA; dbo.INESPECIA; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.INDIAGNOP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EMR';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados_EMR';
-- GO
