
CREATE PROCEDURE [Nephrology].[SP_HC_ListarGestionDesinfeccionEquiposDializacion]
    @CentroAtencion VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    --DECLARE @Now DATETIME2 = SYSUTCDATETIME(); -- hora del servidor

    -------------------------------------------------------------------------
    -- Consulta principal
    -------------------------------------------------------------------------
    SELECT
        -- Datos base del equipo
        E.ID,
        B.IDAGEQUIPTRA,
        A.CODCONCEC AS IDSALA,

        RTRIM(E.CODEQUIPO) AS CodigoEquipo,
        RTRIM(E.DESCREQUI) AS NombreEquipo,		      
        CONCAT(RTRIM(A.CODIGSALA), ' - ', RTRIM(A.DESCRIPSAL)) AS Sala,
        CONCAT(RTRIM(C.UFUCODIGO), ' - ', RTRIM(C.UFUDESCRI))  AS UnidadFuncional,

        -----------------------------------------------------------------
        -- Determinar estado efectivo: prioridad
        -- 1) Si hay cita activa AHORA -> 1 (Ocupado)
        -- 2) else -> tomar estado del log L (si existe) o 4 = Disponible
        -----------------------------------------------------------------
        CASE
            WHEN APT.CODAUTONU IS NOT NULL THEN 1
            ELSE ISNULL(L.State, 4)
        END AS CodigoEstado,

        CASE
            WHEN APT.CODAUTONU IS NOT NULL THEN '1. Equipos ocupados'
            WHEN L.State = 1 THEN '1. Equipos ocupados'
            WHEN L.State = 2 THEN '2. Equipos en desinfección - En limpieza'
            WHEN L.State = 3 THEN '3. Equipos en desinfección - En enjuague y verificación técnica'
            WHEN L.State = 4 THEN '4. Equipos disponibles'
            ELSE '4. Equipos disponibles'
        END AS NombreEstado,

        -- Fechas de movimiento (del log)
        L.StartedAt AS FechaInicio,
        L.EndedAt   AS FechaFin,

        -- Usuario del movimiento (log)
        ISNULL(CONCAT(RTRIM(L.CodeUser), ' - ', RTRIM(U.NOMUSUARI)), ' - ') AS CodigoUsuarioMoviemiento,

        -- Última limpieza/verification finalizada (del mismo equipo)
        RC.LastRoomCleaningDate AS FechaUltimaLimpiezaSala,
        RC.LastRoomCleaningUser AS UsuarioUltimaLimpiezaSala,

        -----------------------------------------------------------------
        -- Información de la cita (si existe) para la UI / debugging
        -----------------------------------------------------------------
        APT.CODAUTONU          AS CitaId,
        APT.IPCODPACI          AS Cita_PacienteId,
        APT.FECHORAIN          AS Cita_FechaInicio,
        APT.FECHORAFI          AS Cita_FechaFin,
        APT.TIPSOLICITU        AS Cita_Tipo,

        -----------------------------------------------------------------
        -- Estado agregado de la SALA (prioridad: limpieza/verificación > en uso > disponible)
        -----------------------------------------------------------------
        CASE
            WHEN EXISTS (
                SELECT 1
                FROM Nephrology.EquipmentStateLog L3
                INNER JOIN AGENSALAEQU B3 ON B3.IDAGEQUIPTRA = L3.IdAGEQUIPTRA
                INNER JOIN AGENSALAC A3 ON A3.CODCONCEC = B3.CODCONCEC
                WHERE A3.CODCONCEC = A.CODCONCEC
                  AND L3.EndedAt IS NULL
                  AND L3.State IN (2, 3)
            ) THEN 'En limpieza'
            WHEN EXISTS (
                SELECT 1
                FROM Nephrology.EquipmentStateLog L4
                INNER JOIN AGENSALAEQU B4 ON B4.IDAGEQUIPTRA = L4.IdAGEQUIPTRA
                INNER JOIN AGENSALAC A4 ON A4.CODCONCEC = B4.CODCONCEC
                WHERE A4.CODCONCEC = A.CODCONCEC
                  AND L4.EndedAt IS NULL
                  AND L4.State = 1
            ) THEN 'En uso'
            ELSE 'Disponible'
        END AS EstadoSalaActual

    -------------------------------------------------------------------------
    -- FROM principal: equipo + relaciones
    -------------------------------------------------------------------------
    FROM dbo.AGEQUIPTRA AS E
    INNER JOIN AGENSALAEQU AS B  ON B.IDAGEQUIPTRA = E.ID
    INNER JOIN AGENSALAC AS A    ON A.CODCONCEC = B.CODCONCEC AND A.CODCENATE = @CentroAtencion
    INNER JOIN INUNIFUNC AS C    ON C.UFUCODIGO = A.UFUCODIGO

    ---------------------------------------------------------------------
    -- Último movimiento del equipo (abierto o más reciente)
    ---------------------------------------------------------------------
    OUTER APPLY
    (
        SELECT TOP (1)
            L.State,
            L.StartedAt,
            L.EndedAt,
            L.CodeUser
        FROM Nephrology.EquipmentStateLog AS L
        WHERE L.IdAGEQUIPTRA = E.ID
        ORDER BY
            CASE WHEN L.EndedAt IS NULL THEN 0 ELSE 1 END,
            L.StartedAt DESC
    ) AS L

    ---------------------------------------------------------------------
    -- Usuario del último movimiento
    ---------------------------------------------------------------------
    LEFT JOIN SEGusuaru AS U 
        ON L.CodeUser = U.CODUSUARI

    ---------------------------------------------------------------------
    -- Última limpieza o verificación finalizada (solo de ese equipo)
    ---------------------------------------------------------------------
    OUTER APPLY
    (
        SELECT TOP(1)
            L2.EndedAt  AS LastRoomCleaningDate,
            CONCAT(RTRIM(L2.CodeUser), ' - ', RTRIM(U2.NOMUSUARI)) AS LastRoomCleaningUser
        FROM Nephrology.EquipmentStateLog AS L2
        LEFT JOIN SEGusuaru AS U2 
            ON L2.CodeUser = U2.CODUSUARI
        WHERE L2.IdAGEQUIPTRA = E.ID
          AND L2.State IN (2, 3)
          AND L2.EndedAt IS NOT NULL
        ORDER BY L2.EndedAt DESC
    ) AS RC

    ---------------------------------------------------------------------
    -- OUTER APPLY para detectar una cita ACTIVA AHORA (sobre el mismo equipo)
    ---------------------------------------------------------------------
    OUTER APPLY
    (
        SELECT TOP(1)
            G.CODAUTONU,
            G.IPCODPACI,
            G.FECHORAIN,
            G.FECHORAFI,
            G.TIPSOLICITU
        FROM AGASICITA G
        WHERE G.IDEQUIPOTRA = E.ID
          AND G.TIPSOLICITU = 3               -- tipo de solicitud: tratamiento (según tu filtro)
          AND G.FECHORAIN <= Common.getdate()
          AND G.FECHORAFI >= Common.getdate()
          AND G.CODESTCIT IN ('0','2','3')
        ORDER BY G.FECHORAIN DESC
    ) AS APT

    -------------------------------------------------------------------------
    -- Filtros
    -------------------------------------------------------------------------
    WHERE 
        E.CODCENATE = @CentroAtencion 
        AND E.TIPOEQUIP = 3        -- tipo equipo tratamiento
        AND E.ESTEQUIP = 1         -- activo

    -------------------------------------------------------------------------
    -- Ordenamiento (por código equipo o como prefieras)
    -------------------------------------------------------------------------
    ORDER BY E.CODEQUIPO DESC;
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que lista el estado actual de los equipos de diálisis (tipo tratamiento, activos) de un centro de atención, combinando su último registro de ciclo de desinfección con la detección de citas activas en curso. Retorna por equipo: sala, unidad funcional, estado efectivo (ocupado, en limpieza, en enjuague/verificación o disponible), datos de la cita vigente si existe, última limpieza finalizada y estado agregado de la sala, ordenado por código de equipo.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGestionDesinfeccionEquiposDializacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGestionDesinfeccionEquiposDializacion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los equipos de diálisis activos de un centro con su estado efectivo (ocupado por cita vigente, en limpieza/verificación, o disponible), datos de la sala, último movimiento, última limpieza y estado agregado de la sala.', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGestionDesinfeccionEquiposDializacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse un centro de atención válido para filtrar equipos y salas; Las tablas de catálogo de salas, unidades funcionales y equipos deben estar relacionadas vía AGENSALAEQU; La función Common.getdate() debe estar disponible para evaluar la vigencia de la cita', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGestionDesinfeccionEquiposDializacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan equipos del centro de atención solicitado, de tipo tratamiento (TIPOEQUIP=3) y activos (ESTEQUIP=1); Una cita se considera activa solo si es de tipo tratamiento (TIPSOLICITU=3), su rango FECHORAIN/FECHORAFI contiene la hora actual y su estado está en (''0'',''2'',''3''); El estado del equipo prioriza la existencia de una cita activa por encima del estado registrado en el log; Cuando no hay registros en el log para un equipo, su estado por defecto es 4 (Disponible); La ''última limpieza'' considera únicamente movimientos finalizados (EndedAt NOT NULL) cuyo State sea 2 o 3; El último movimiento del equipo prioriza registros abiertos (EndedAt NULL) y luego el más reciente por StartedAt; El estado agregado de la sala prioriza limpieza/verificación (2,3) sobre uso (1) y luego disponible', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGestionDesinfeccionEquiposDializacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Equipos de diálisis; Desinfección de equipos; Limpieza; Enjuague y verificación técnica; Sala de tratamiento; Unidad funcional; Cita/agendamiento; Centro de atención; Estado de equipo (ocupado/disponible); Tratamiento (tipo de solicitud)', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGestionDesinfeccionEquiposDializacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto de resultados con un registro por equipo de tratamiento activo del centro indicado, ordenado por CODEQUIPO descendente', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGestionDesinfeccionEquiposDializacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe una cita activa actual sobre el equipo (CODAUTONU IS NOT NULL) → El estado efectivo del equipo se fuerza a 1 (Ocupado) con etiqueta ''1. Equipos ocupados'' else Se toma el State del último movimiento en el log; si no hay log, se asume 4 (Disponible); si L.State = 2 → Estado etiquetado como ''2. Equipos en desinfección - En limpieza''; si L.State = 3 → Estado etiquetado como ''3. Equipos en desinfección - En enjuague y verificación técnica''; si Existe en la sala algún equipo con log abierto (EndedAt IS NULL) y State IN (2,3) → EstadoSalaActual = ''En limpieza'' else Si existe algún equipo con log abierto y State=1 -> ''En uso''; en caso contrario ''Disponible''', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGestionDesinfeccionEquiposDializacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGEQUIPTRA; dbo.AGENSALAEQU; dbo.AGENSALAC; dbo.INUNIFUNC; Nephrology.EquipmentStateLog; dbo.SEGusuaru; dbo.AGASICITA', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGestionDesinfeccionEquiposDializacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Nephrology', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarGestionDesinfeccionEquiposDializacion';
-- GO
