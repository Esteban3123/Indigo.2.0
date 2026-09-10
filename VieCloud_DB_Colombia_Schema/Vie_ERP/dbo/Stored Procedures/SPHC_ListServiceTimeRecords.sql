/****** Object:  StoredProcedure [dbo].[SPHC_ListServiceTimeRecords]    Script Date: 28/5/2026 14:11:22 ******/

CREATE PROCEDURE [dbo].[SPHC_ListServiceTimeRecords]
    @FechaInicio DATE,
    @FechaFin   DATE,
    @CentroAtencion VARCHAR(25),
    @OffsetConsecutivo INT = 0

AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @FechaFinMas1 DATE = DATEADD(DAY, 1, @FechaFin);

    ;WITH CTE_UnidadesUrgencias AS (
    SELECT UFUCODIGO
    FROM INUNIFUNC
    WHERE UFUTIPUNI = 1
)
, CTE_UltimaHC AS (
  SELECT IPCODPACI, NUMINGRES, INDICAPAC,
  CASE INDICAPAC
        -- 1: Observacion Urgencias
        WHEN 2  THEN 1   -- Trasladar a Observacioon Urgencias
        WHEN 13 THEN 1   -- Continua en la Unidad
        -- 4: Atención Cirugiia
        WHEN 8  THEN 4   -- Trasladar a Cirugía

        -- 5: Hospitalizacion
        WHEN 3  THEN 5   -- Trasladar a Hospitalizacion
        WHEN 4  THEN 5   -- Trasladar a UCI Adulto
        WHEN 5  THEN 5   -- Trasladar a UCI Pediatrica
        WHEN 6  THEN 5   -- Trasladar a UCI Neonatal
        WHEN 19 THEN 5   -- U. Cuidado Intermedio
        WHEN 20 THEN 5   -- U. Basica  (confirmar)
        WHEN 21 THEN 5   -- Hospitalizacion Pediatria

        -- 6: Manejo Ambulatorio
        WHEN 12 THEN 6   -- Salida
        WHEN 9  THEN 6   -- Hospitalizacion en Casa
        WHEN 10 THEN 6  -- Referencia
        WHEN 11 THEN 6  -- Morgue

        -- 7: Salida Voluntaria / Abandono
        WHEN 15 THEN 7   -- Retiro Voluntario
        WHEN 16 THEN 7   -- Fuga
    END AS DestinoNormativo,

    CASE INDICAPAC
        -- 1: Observacion Urgencias
        WHEN 2  THEN '1 - Observación urgencias'
        WHEN 13 THEN '1 - Observación urgencias'
        -- 4: Atención Cirugiia
        WHEN 8  THEN '4 - Atención cirugía'   -- Trasladar a Cirugía

        -- 5: Hospitalizacion
        WHEN 3  THEN '5 - Hospitalización'
        WHEN 4  THEN '5 - Hospitalización'
        WHEN 5  THEN '5 - Hospitalización'
        WHEN 6  THEN '5 - Hospitalización'
        WHEN 19 THEN '5 - Hospitalización'
        WHEN 20 THEN '5 - Hospitalización'
        WHEN 21 THEN '5 - Hospitalización'

        -- 6: Manejo Ambulatorio
        WHEN 12 THEN '6 - Manejo ambulatorio'
        WHEN 9  THEN '6 - Manejo ambulatorio'
        WHEN 10 THEN '6 - Manejo ambulatorio'
        WHEN 11 THEN '6 - Manejo ambulatorio'

        -- 7: Salida Voluntaria / Abandono
        WHEN 15 THEN '7 - Salida voluntaria o abandonó antes de ser atendido'
        WHEN 16 THEN '7 - Salida voluntaria o abandonó antes de ser atendido'
    END AS DestinoNormativoview,
    FECHISPAC AS FechaDefinicionConducta,

    -- Fecha Egreso: solo cuando el destino implica salida real de urgencias
    CASE
        WHEN INDICAPAC  IN (8, 9, 12, 10,11,3,4,5,6,19,20,21,15,16)
        THEN FECHISPAC
        ELSE NULL
    END AS FechaEgresoHC,

     -- Estado Egreso: 2-Muerto solo si destino es Morgue (11), resto 1-Vivo
    CASE
        WHEN INDICAPAC = 11 THEN 2  -- Morgue  Muerto
        WHEN INDICAPAC  IN (8, 9, 12, 10,11,3,4,5,6,19,20,21,16,15)  THEN 1  -- Sale vivo
        ELSE NULL  -- Continua, no aplica egreso
    END AS EstadoEgreso,
     CASE
        WHEN INDICAPAC = 11 THEN '2 - Muerto'  -- Morgue  Muerto
        WHEN INDICAPAC  IN (8, 9, 12, 10,11,3,4,5,6,19,20,21,16,15)  THEN '1 - Vivo'  -- Sale vivo
        ELSE NULL  -- Continua, no aplica egreso
    END AS EstadoEgresoview


    -- Sin filtro de unidad (CTE_UnidadesUrgencias): la decisión debe tomarse del
    -- verdadero último registro del ingreso, incluso si el paciente fue trasladado a
    -- otra unidad (p. ej. Cirugía, Hospitalización) y su disposición final quedó
    -- registrada allí. Restringir a UFUTIPUNI=1 excluía esos ingresos por completo
    -- (el paciente sí tuvo consulta de Urgencias, ver CTE_ConsultaI, pero su nota final
    -- no vive en una unidad tipo Urgencias).
    -- Toma el registro más reciente que YA tenga un INDICAPAC válido (filtra por
    -- INDICAPAC antes de rankear, no después): el ingreso puede tener notas de
    -- continuación (INDICAPAC 13/14/18/22, p.ej. "Pre-alta hospitalaria") registradas
    -- después de la disposición final real, y estas no deben anularla. No basta con
    -- priorizar TIPHISPAC='E' porque el registro de Egreso no siempre lleva el
    -- INDICAPAC correcto (a veces queda una nota 'N' posterior con la disposición
    -- real) — se probó y causaba exclusiones incorrectas en la dirección opuesta.
    -- Sin tope superior de fecha (solo @FechaInicio como piso): un ingreso admitido
    -- dentro del período puede resolverse (egreso real) días o semanas después, fuera
    -- de la ventana de consulta. Acotar también por @FechaFinMas1 aquí hacía que esa
    -- disposición final, ya registrada pero posterior al cierre del período, nunca se
    -- viera, y el ingreso quedaba excluido aunque sí tuviera un destino válido.
    FROM (
        SELECT H.IPCODPACI, H.NUMINGRES, H.INDICAPAC, H.FECHISPAC,
            ROW_NUMBER() OVER (
                PARTITION BY H.IPCODPACI, H.NUMINGRES
                ORDER BY CAST(H.NUMEFOLIO AS INT) DESC
            ) AS RN
        FROM HCHISPACA H
        WHERE H.CODCENATE  = @CentroAtencion
          AND H.FECHISPAC >= @FechaInicio
          AND H.INDICAPAC IN (8, 3, 4, 5, 6, 19, 20, 21, 12, 9, 10, 11, 15, 16)
    ) X
    WHERE RN = 1

)
, CTE_ConsultaI AS (
    -- El centro de atención se valida contra ADINGRESO (el ingreso), no contra
    -- HCHISPACA.CODCENATE (el centro donde se registró la nota clínica puntual), por
    -- consistencia con SPHC_ListarRipsUrgenciasUsuarios (ver CTE_TieneConsultaI ahí):
    -- en clientes multi-sede ambos valores pueden diferir para el mismo ingreso.
    SELECT IPCODPACI, NUMINGRES, NUMEFOLIO, CODDIAGNO, FECHISPAC
    FROM (
        SELECT H.IPCODPACI, H.NUMINGRES, H.NUMEFOLIO, H.CODDIAGNO, H.FECHISPAC,
            ROW_NUMBER() OVER (
                PARTITION BY H.IPCODPACI, H.NUMINGRES
                ORDER BY CAST(H.NUMEFOLIO AS INT) DESC
            ) AS RN
        FROM HCHISPACA H
        INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = H.UFUCODIGO
        INNER JOIN dbo.ADINGRESO AI ON AI.IPCODPACI = H.IPCODPACI AND AI.NUMINGRES = H.NUMINGRES
        WHERE AI.CODCENATE = @CentroAtencion
          AND H.TIPHISPAC  = 'I'
          AND H.FECHISPAC >= @FechaInicio
          AND H.FECHISPAC  < @FechaFinMas1
    ) X
    WHERE RN = 1
)
, CTE_Triage AS (
    -- Filtra por los NUMINGRES de pacientes que tienen HC en el rango (no por fecha de triage).
    -- Así se incluye el triage aunque haya ocurrido antes o después del rango consultado,
    -- y el scan de ADTRIAGEu queda acotado a los ingresos relevantes en lugar de toda la tabla.
    -- Reutiliza CTE_ConsultaI (mismo filtro exacto: CODCENATE + TIPHISPAC='I' + rango de fechas)
    -- en vez de repetir el escaneo de HCHISPACA/INUNIFUNC.
    SELECT
        NUMINGRES,
        IPCODPACI,
        COALESCE(FECHINITR, TRIAFECHA) AS FechaTriage,
        x.TRIAGECLA AS ClasificacionTriage,
        CASE CAST(x.TRIAGECLA AS CHAR)
            WHEN '1' THEN 'Triage I'   WHEN '2' THEN 'Triage II'
            WHEN '3' THEN 'Triage III' WHEN '4' THEN 'Triage IV'
            WHEN '5' THEN 'Triage V'
        END AS ClasificacionTriageview
    FROM (
        SELECT
            T.NUMINGRES,
            T.IPCODPACI,
            T.FECHINITR,
            T.TRIAFECHA,
            T.TRIAGECLA,
            ROW_NUMBER() OVER (PARTITION BY T.NUMINGRES ORDER BY COALESCE(T.FECHINITR, T.TRIAFECHA) DESC) AS RN
        FROM ADTRIAGEu T
        INNER JOIN (SELECT DISTINCT NUMINGRES FROM CTE_ConsultaI) HC ON HC.NUMINGRES = T.NUMINGRES
        WHERE T.CODCENATE = @CentroAtencion
    ) X
    WHERE RN = 1
)
,CTE_TInterconsultasIngreso AS (


     SELECT
    NUMEFOLIO, IPCODPACI, NUMINGRES,
    FechaSolicitudInterconsulta,
    FechaRespuestaInter,
    trim(CupInterconsulta) as CupInterconsulta,
    DxPrincipalInter,CODSERIPS
FROM (
    SELECT
        OI.NUMEFOLIO, OI.IPCODPACI, OI.NUMINGRES,
        OI.FECORDMED AS FechaSolicitudInterconsulta,
        OI.FECHAINT  AS FechaRespuestaInter,
        C.DESSERIPS  AS CupInterconsulta,
        D.CODDIAGNO  AS DxPrincipalInter,oi.CODSERIPS,
        ROW_NUMBER() OVER (
            PARTITION BY OI.NUMEFOLIO, OI.IPCODPACI, OI.NUMINGRES
            ORDER BY
                CASE WHEN OI.ESTSERIPS = 3 THEN 0 ELSE 1 END,
                OI.FECHAINT DESC
        ) AS RN
    FROM HCORDINTE OI
    INNER JOIN HCHISPACA H ON  H.NUMEFOLIO = OI.NUMEFOLIO AND H.IPCODPACI = OI.IPCODPACI AND H.NUMINGRES = OI.NUMINGRES AND H.TIPHISPAC = 'I'
    INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = h.UFUCODIGO
    LEFT JOIN INDIAGNOH D ON  D.NUMEFOLIO = OI.NUMFOLINT AND D.IPCODPACI = OI.IPCODPACI AND D.NUMINGRES = OI.NUMINGRES AND D.CODDIAPRI = 1
    LEFT JOIN INCUPSIPS C ON  C.CODSERIPS = OI.CODSERIPS
    WHERE OI.CODCENATE=@CentroAtencion AND  OI.FECORDMED >= @FechaInicio AND OI.FECORDMED < @FechaFinMas1
) X
WHERE RN = 1
)

, CTE_RemisionSolicitud AS (
    SELECT
        IPCODPACI,
        NUMINGRES,
        FechaSolicitudRemision,
        DxPrincipalRemision
    FROM (
        SELECT
            R.IPCODPACI,
            R.NUMINGRES,
            CT.FECSOLICIT AS FechaSolicitudRemision,
            D.CODDIAGNO AS DxPrincipalRemision,
            ROW_NUMBER() OVER (
                PARTITION BY R.IPCODPACI, R.NUMINGRES
                ORDER BY CT.FECSOLICIT asc
            ) AS RN
        FROM HCREFCONP R
        INNER JOIN HCREFCONT CT ON CT.IDHCREFCONP = R.AUTO AND CT.NUMEFOLIO IS NOT NULL
        INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = R.UFUCODIGO
        LEFT JOIN HCREFCONTDET D ON D.HCREFCONTAUTO = CT.AUTO
        WHERE R.CODCENATE = @CentroAtencion AND R.FECSOLICIT >= @FechaInicio AND R.FECSOLICIT < @FechaFinMas1
    ) X
    WHERE RN = 1
)
, CTE_RemisionEfectiva AS (
    -- Deduplicado a 1 fila por ingreso: HCREGEGRE/CHREGESTA pueden tener más de un
    -- registro que cumpla la condición para el mismo (IPCODPACI, NUMINGRES), y al no
    -- deduplicar (a diferencia del resto de CTEs de este SP) el LEFT JOIN posterior
    -- multiplicaba filas en el resultado final (encuentros duplicados en el archivo plano).
    SELECT IPCODPACI, NUMINGRES, FechaRemisionEfectiva
    FROM (
        SELECT
            E.IPCODPACI,
            E.NUMINGRES,
            E.FECALTPAC AS FechaRemisionEfectiva,
            ROW_NUMBER() OVER (
                PARTITION BY E.IPCODPACI, E.NUMINGRES
                ORDER BY E.FECALTPAC ASC
            ) AS RN
        FROM HCREGEGRE E
        INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = E.UFUCODIGO
        INNER JOIN CHREGESTA C ON C.IPCODPACI = E.IPCODPACI AND C.NUMINGRES = E.NUMINGRES AND C.REGESTADO = 2
        WHERE E.CODCENATE = @CentroAtencion AND E.ESTPACEGR = 4 AND E.FECALTPAC >= @FechaInicio AND E.FECALTPAC < @FechaFinMas1
    ) X
    WHERE RN = 1
)
  , CTE_RequirioProcedimientos AS (
    SELECT IPCODPACI, NUMINGRES, '1 = Si' AS RequirioProcedimiento,MIN(FECORDMED) AS FechaSolicitudProcedimiento
    FROM (
        SELECT P.IPCODPACI, P.NUMINGRES,P.FECORDMED
        FROM HCORDPRON P
          INNER JOIN HCHISPACA H ON H.NUMEFOLIO = P.NUMEFOLIO AND H.IPCODPACI = P.IPCODPACI  AND H.NUMINGRES = P.NUMINGRES
        INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = H.UFUCODIGO
        WHERE H.CODCENATE = @CentroAtencion
        AND H.FECHISPAC >= @FechaInicio AND H.FECHISPAC < @FechaFinMas1

        UNION ALL

        SELECT Q.IPCODPACI, Q.NUMINGRES,Q.FECORDMED
        FROM HCORDPROQ Q
         INNER JOIN HCHISPACA H ON H.NUMEFOLIO = Q.NUMEFOLIO AND H.IPCODPACI = Q.IPCODPACI AND H.NUMINGRES = Q.NUMINGRES
        INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = H.UFUCODIGO
        WHERE H.CODCENATE = @CentroAtencion
        AND H.FECHISPAC >= @FechaInicio AND H.FECHISPAC < @FechaFinMas1
    ) X
    GROUP BY IPCODPACI, NUMINGRES
)
, CTE_FechaRealizacionProcedimiento AS (

SELECT IPCODPACI, NUMINGRES, MIN(FechaRealizacion) AS FechaRealizacionProcedimiento
    FROM (
        -- NO QX: ESTSERIPS = 2 completado
        SELECT P.IPCODPACI, P.NUMINGRES, P.FECORDMED AS FechaRealizacion
        FROM HCORDPRON P
        INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = p.UFUCODIGO
        WHERE p.CODCENATE = @CentroAtencion AND p.FECORDMED >= @FechaInicio AND p.FECORDMED < @FechaFinMas1
        AND P.ESTSERIPS in(2,3,4)
        
        UNION ALL
        -- QX realizado: existe en HCQXINFOR
        SELECT I.IPCODPACI, I.NUMINGRES, COALESCE(I.FECHORINI, H.FECHISPAC) AS FechaRealizacion
        FROM HCQXINFOR I
        INNER JOIN HCHISPACA H ON H.NUMEFOLIO = I.NUMEFOLIO AND H.IPCODPACI = I.IPCODPACI AND H.NUMINGRES = I.NUMINGRES
        INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = H.UFUCODIGO
        WHERE H.CODCENATE = @CentroAtencion AND H.FECHISPAC >= @FechaInicio AND H.FECHISPAC < @FechaFinMas1
    ) X
    GROUP BY IPCODPACI, NUMINGRES

    )

, CTE_ProcedimientoPrincipal AS (

    SELECT IPCODPACI, NUMINGRES, min(CODSERIPS) AS ProcedimientoPrincipal
    FROM (
        SELECT P.IPCODPACI, P.NUMINGRES, P.CODSERIPS
        FROM HCORDPRON P
        INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = P.UFUCODIGO
        WHERE P.CODCENATE = @CentroAtencion AND P.FECORDMED >= @FechaInicio AND P.FECORDMED < @FechaFinMas1  AND P.PRINCIPAL = 1

        UNION ALL

        SELECT Q.IPCODPACI, Q.NUMINGRES, Q.CODSERIPS
        FROM HCORDPROQ Q
        INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = Q.UFUCODIGO
        WHERE Q.CODCENATE = @CentroAtencion AND Q.FECORDMED >= @FechaInicio AND Q.FECORDMED < @FechaFinMas1  AND Q.PRINCIPAL = 1
    ) X
    GROUP BY IPCODPACI, NUMINGRES
)
,CTE_Medicamentos AS (

SELECT M.IPCODPACI, M.NUMINGRES, 
    '1 = Si' AS RequirioMedicamentos,MIN(HT.FECHISPAC) AS FechaPrescripcionMedicamentos
    FROM HCPRESCRA M
    INNER JOIN HCHISPACA HT ON HT.NUMEFOLIO= M.NUMEFOLIO AND HT.IPCODPACI = M.IPCODPACI AND HT.NUMINGRES = M.NUMINGRES
    INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = HT.UFUCODIGO
    WHERE HT.CODCENATE = @CentroAtencion AND HT.FECHISPAC >= @FechaInicio AND HT.FECHISPAC < @FechaFinMas1
    GROUP BY M.IPCODPACI, M.NUMINGRES
)
, CTE_AdministracionMedicamentos AS (
    SELECT 
        A.IPCODPACI, 
        A.NUMINGRES,
        MIN(A.FECAPLMED) AS FechaAdministracionMedicamentos
    FROM HCHOJAMED A
    INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = A.UFUCODIGO
    WHERE A.CODCENATE = @CentroAtencion AND A.FECAPLMED >= @FechaInicio AND A.FECAPLMED < @FechaFinMas1
    GROUP BY A.IPCODPACI, A.NUMINGRES
)
, CTE_MedicamentoPrincipal AS (
    SELECT 
        M.IPCODPACI, 
        M.NUMINGRES,
        MIN(COALESCE(P.CODDCIMED, M.CODPRODUC)) AS MedicamentoPrincipal
    FROM HCPRESCRA M
    INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = M.UFUCODIGO
    INNER JOIN IHLISTPRO P ON P.CODPRODUC=M.CODPRODUC
    WHERE M.CODCENATE = @CentroAtencion
    AND M.FECINIDOS >= @FechaInicio AND M.FECINIDOS < @FechaFinMas1 AND M.Principal = 1
    GROUP BY M.IPCODPACI, M.NUMINGRES
)

, CTE_AyudasDx AS (

  SELECT IPCODPACI, NUMINGRES,
        '1 = Si' AS RequirioAyudasDx,
        MIN(FECORDMED) AS FechaOrdenamientoAyudaDx,
        min(FechaRealizado) as FechaRealizado, MIN(trim(DESSERIPS)) AS DesServicio,min(CODSERIPS) as codigo
    FROM (
        SELECT H.IPCODPACI, H.NUMINGRES, I.FECORDMED, I.CODSERIPS,i.FECRECEXA as FechaRealizado,C.DESSERIPS
        FROM HCORDIMAG I
        INNER JOIN HCHISPACA H ON H.NUMEFOLIO = I.NUMEFOLIO AND H.IPCODPACI = I.IPCODPACI AND H.NUMINGRES = I.NUMINGRES
        INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = H.UFUCODIGO
        INNER JOIN INCUPSIPS c on i.CODSERIPS=c.CODSERIPS
        WHERE H.CODCENATE = @CentroAtencion AND H.FECHISPAC >= @FechaInicio AND H.FECHISPAC < @FechaFinMas1 AND I.PRINCIPAL = 1 
        UNION ALL

        SELECT H.IPCODPACI, H.NUMINGRES, L.FECORDMED, L.CODSERIPS,l.FECRECMUE as FechaRealizado,C.DESSERIPS
        FROM HCORDLABO L
        INNER JOIN HCHISPACA H ON H.NUMEFOLIO = L.NUMEFOLIO  AND H.IPCODPACI = L.IPCODPACI AND H.NUMINGRES = L.NUMINGRES
        INNER JOIN CTE_UnidadesUrgencias U ON U.UFUCODIGO = H.UFUCODIGO
        INNER JOIN INCUPSIPS C  on L.CODSERIPS=c.CODSERIPS
        WHERE H.CODCENATE = @CentroAtencion AND H.FECHISPAC >= @FechaInicio AND H.FECHISPAC < @FechaFinMas1  AND L.PRINCIPAL = 1
       
    ) X
    GROUP BY IPCODPACI, NUMINGRES
)
, CTE_FechaLlegada AS (
    SELECT NUMINGRES, IPCODPACI, FechaLlegada
    FROM (
        SELECT
            I.NUMINGRES,
            I.IPCODPACI,
            COALESCE(C.IPFECLLEGA, I.IFECHAING) AS FechaLlegada,
            ROW_NUMBER() OVER (
                PARTITION BY I.NUMINGRES, I.IPCODPACI
                ORDER BY COALESCE(C.IPFECLLEGA, I.IFECHAING) ASC
            ) AS RN
        FROM ADINGRESO I
        INNER JOIN CTE_UltimaHC UH ON UH.IPCODPACI = I.IPCODPACI AND UH.NUMINGRES = I.NUMINGRES
        LEFT JOIN ADTRIAGEU T ON T.NUMINGRES = I.NUMINGRES AND T.IPCODPACI = I.IPCODPACI
        LEFT JOIN ADCONTURG C ON C.CODCONCEC = T.CODCONCEC
        WHERE I.CODCENATE = @CentroAtencion
    ) X
    WHERE RN = 1
)
-------------consulta principal
-- Tabla base: CTE_UltimaHC — ya tiene un único registro por ingreso, filtrado por
-- centro, fecha, TIPHISPAC='I' e INDICAPAC válido. Elimina duplicados y evita
-- re-unir HCHISPACA e INUNIFUNC en la consulta principal.

SELECT
ROW_NUMBER() OVER (ORDER BY I.NUMINGRES DESC) + @OffsetConsecutivo AS Consecutivo,
TD.SIGLA AS TipoIdentificacion,
RTRIM(P.IPCODPACI) AS NumeroIdentificacion,
CONVERT(VARCHAR(16), FL.FechaLlegada, 120) AS FechaIngreso,
CONVERT(VARCHAR(16), T.FechaTriage, 120) AS FechaTriage,
T.ClasificacionTriage,
T.ClasificacionTriageview,
CONVERT(VARCHAR(16), CI.FECHISPAC, 120) AS FechaConsulta,
CI.CODDIAGNO AS DXPrincipal,
CASE
    WHEN UH.INDICAPAC = 13 AND INT.NUMEFOLIO IS NOT NULL THEN 1
    WHEN UH.INDICAPAC = 10 THEN 2
    WHEN UH.INDICAPAC IN (2,3,4,5,6,7,8,19,20,21) THEN 3
    WHEN UH.INDICAPAC = 13 AND INT.NUMEFOLIO IS NULL THEN 3
    WHEN UH.INDICAPAC IN (9,11,12,15,16,17) THEN 4
END AS ConductaDefinida,
CASE
    WHEN UH.INDICAPAC = 13 AND INT.NUMEFOLIO IS NOT NULL THEN '1 = Interconsulta a especialista'
    WHEN UH.INDICAPAC = 10 THEN '2 = Remisión a otra IPS'
    WHEN UH.INDICAPAC IN (2,3,4,5,6,7,8,19,20,21) THEN '3 = Hospitalización'
    WHEN UH.INDICAPAC = 13 AND INT.NUMEFOLIO IS NULL THEN '3 = Hospitalización'
    WHEN UH.INDICAPAC IN (9,11,12,15,16,17) THEN '4 = Alta o egreso'
END AS ConductaDefinidaview,
CONVERT(VARCHAR(16), INT.FechaSolicitudInterconsulta, 120) AS FechaSolicitudInterconsulta,
CONVERT(VARCHAR(16), INT.FechaRespuestaInter, 120) AS FechaRespuestaInter,
-------------------------interconsulta ----------------------------------------------
IIF(INT.CODSERIPS IS NULL, INT.CupInterconsulta, CONCAT(RTRIM(INT.CODSERIPS), ' - ', INT.CupInterconsulta)) AS CupInterconsultaview,
INT.CODSERIPS AS CupInterconsulta,
---------------------------------------------------------------------
INT.DxPrincipalInter,
CONVERT(VARCHAR(16), REM.FechaSolicitudRemision, 120) AS FechaSolicitudRemision,
CONVERT(VARCHAR(16), EFE.FechaRemisionEfectiva, 120) AS FechaRemisionEfectiva,
REM.DxPrincipalRemision,
CASE WHEN PRO.RequirioProcedimiento IS NOT NULL THEN 1 ELSE 2 END AS RequirioProcedimiento,
CASE WHEN PRO.RequirioProcedimiento IS NOT NULL THEN '1 = Si' ELSE '2 = No' END AS RequirioProcedimientoview,
CONVERT(VARCHAR(16), PRO.FechaSolicitudProcedimiento, 120) AS FechaSolicitudProcedimiento,
CONVERT(VARCHAR(16), RF.FechaRealizacionProcedimiento, 120) AS FechaRealizacionProcedimiento,
PP.ProcedimientoPrincipal,
CASE WHEN MED.RequirioMedicamentos IS NOT NULL THEN 1 ELSE 2 END AS RequirioMedicamentos,
CASE WHEN MED.RequirioMedicamentos IS NOT NULL THEN '1 = Si' ELSE '2 = No' END AS RequirioMedicamentosview,
CONVERT(VARCHAR(16), MED.FechaPrescripcionMedicamentos, 120) AS FechaPrescripcionMedicamentos,
CONVERT(VARCHAR(16), ADM.FechaAdministracionMedicamentos, 120) AS FechaAdministracionMedicamentos,
MP.MedicamentoPrincipal,
CASE WHEN ADX.RequirioAyudasDx IS NOT NULL THEN 1 ELSE 2 END AS RequirioAyudasDx,
CASE WHEN ADX.RequirioAyudasDx IS NOT NULL THEN '1 = Si' ELSE '2 = No' END AS RequirioAyudasDxview,
CONVERT(VARCHAR(16), ADX.FechaOrdenamientoAyudaDx, 120) AS FechaOrdenamientoAyudaDx,
CONVERT(VARCHAR(16), ADX.FechaRealizado, 120) AS FechaRealizado,
--ayudas dx
IIF(ADX.codigo IS NULL, ADX.DesServicio, CONCAT(RTRIM(ADX.codigo), ' - ', ADX.DesServicio)) AS DesServicioview,
ADX.codigo AS DesServicio,
--------
UH.DestinoNormativo,
UH.DestinoNormativoview,
CONVERT(VARCHAR(16), UH.FechaDefinicionConducta, 120) AS FechaDefinicionConducta,
CONVERT(VARCHAR(16), UH.FechaEgresoHC, 120) AS FechaEgresoHC,
UH.EstadoEgreso,
UH.EstadoEgresoview,
'3' AS TipoRegistro
FROM CTE_UltimaHC AS UH
-- CTE_ConsultaI garantiza que el ingreso tiene al menos un registro TIPHISPAC='I'
-- y aporta NUMEFOLIO/CODDIAGNO/FechaConsulta desde ese registro.
INNER JOIN CTE_ConsultaI AS CI
    ON CI.IPCODPACI = UH.IPCODPACI AND CI.NUMINGRES = UH.NUMINGRES
INNER JOIN ADINGRESO AS I
    ON I.IPCODPACI = UH.IPCODPACI AND I.NUMINGRES = UH.NUMINGRES
    AND I.CODCENATE = @CentroAtencion
INNER JOIN INPACIENT AS P
    ON P.IPCODPACI = UH.IPCODPACI
INNER JOIN ADTIPOIDENTIFICA TD
    ON TD.CODIGO = P.IPTIPODOC
LEFT JOIN CTE_Triage AS T
    ON T.NUMINGRES = UH.NUMINGRES
LEFT JOIN CTE_TInterconsultasIngreso AS INT
    ON INT.NUMEFOLIO = CI.NUMEFOLIO AND INT.NUMINGRES = CI.NUMINGRES AND INT.IPCODPACI = CI.IPCODPACI
LEFT JOIN CTE_RemisionSolicitud AS REM
    ON REM.IPCODPACI = UH.IPCODPACI AND REM.NUMINGRES = UH.NUMINGRES
LEFT JOIN CTE_RemisionEfectiva AS EFE
    ON EFE.IPCODPACI = UH.IPCODPACI AND EFE.NUMINGRES = UH.NUMINGRES
LEFT JOIN CTE_RequirioProcedimientos AS PRO
    ON PRO.IPCODPACI = UH.IPCODPACI AND PRO.NUMINGRES = UH.NUMINGRES
LEFT JOIN CTE_FechaRealizacionProcedimiento AS RF
    ON RF.IPCODPACI = UH.IPCODPACI AND RF.NUMINGRES = UH.NUMINGRES
LEFT JOIN CTE_ProcedimientoPrincipal AS PP
    ON PP.IPCODPACI = UH.IPCODPACI AND PP.NUMINGRES = UH.NUMINGRES
LEFT JOIN CTE_Medicamentos AS MED
    ON MED.IPCODPACI = UH.IPCODPACI AND MED.NUMINGRES = UH.NUMINGRES
LEFT JOIN CTE_AdministracionMedicamentos AS ADM
    ON ADM.IPCODPACI = UH.IPCODPACI AND ADM.NUMINGRES = UH.NUMINGRES
LEFT JOIN CTE_MedicamentoPrincipal AS MP
    ON MP.IPCODPACI = UH.IPCODPACI AND MP.NUMINGRES = UH.NUMINGRES
LEFT JOIN CTE_AyudasDx AS ADX
    ON ADX.IPCODPACI = UH.IPCODPACI AND ADX.NUMINGRES = UH.NUMINGRES
LEFT JOIN CTE_FechaLlegada AS FL
    ON FL.IPCODPACI = UH.IPCODPACI AND FL.NUMINGRES = UH.NUMINGRES
ORDER BY I.IFECHAING DESC
OPTION (RECOMPILE);

END
