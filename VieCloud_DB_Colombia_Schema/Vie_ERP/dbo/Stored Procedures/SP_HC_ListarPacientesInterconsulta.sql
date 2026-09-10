/**********************************************************************************************
  Modificated:      miguel angel ruiz vega DBA
  datemodificated:  2026-07-21

  OBJETO: [dbo].[SP_HC_ListarPacientesInterconsulta]

  PROBLEMA ORIGINAL
  -----------------
  El SP presentaba alto consumo de CPU e intermitencia. El plan de ejecucion mostraba que
  el OPTIMIZADOR ABORTABA la compilacion por exceso de complejidad
  (StatementOptmEarlyAbortReason = "TimeOut", CompileCPU ~2000 ms, ~155 MB de memoria de
  compilacion). Mas del 75% del tiempo total era COMPILACION, no ejecucion. La causa: el
  cuerpo del SP (rama estandar) era UNA sola consulta gigante (3 SELECT unidos por UNION,
  cada uno con ~10 joins + subconsultas correlacionadas). El optimizador no alcanzaba a
  explorar un buen plan y se quedaba con uno subóptimo, distinto en cada recompilacion
  (de ahi la intermitencia).

  CAMBIOS APLICADOS EN ESTA VERSION (lista completa)
  --------------------------------------------------
  [A] DESCOMPOSICION EN TABLA TEMPORAL #Resultado  (cambio de mayor impacto)
      El bloque estandar se parte en 3 pasos INDEPENDIENTES que insertan en #Resultado
      (uno por cada tipo de paciente), y al final un unico "SELECT * FROM #Resultado".
      MOTIVO: asi el optimizador compila 3 consultas PEQUENAS (espacio de busqueda chico,
      compila en decenas/cientos de ms cada una) en lugar de un monstruo que aborta por
      TimeOut. Es la accion que elimina la compilacion patologica de ~2 s y la intermitencia.

  [B] #Resultado se crea con "SELECT ... INTO" desde la RAMA 1 usando sus EXPRESIONES REALES.
      MOTIVO: la tabla temporal hereda los tipos NATIVOS exactos de las columnas origen;
      asi la rama 1 no genera conversiones de tipo y se evita el ruido de CONVERT en el plan.

  [C] UNION -> INSERT independientes (equivale a UNION ALL: sin DISTINCT global).
      MOTIVO: las 3 ramas tienen un valor 'Tipo' constante y distinto ('1','2/4','3'); no hay
      duplicados que deduplicar, por lo que el DISTINCT (Sort/Hash Aggregate) del UNION era
      trabajo de CPU desperdiciado.

  [D] ESTSERIPS unificado a INT con CAST EXPLICITO en la rama 1.
      MOTIVO: en el original, la rama 1 devolvia CHAR y las otras INT (0); el UNION forzaba
      un CONVERT_IMPLICIT sobre la columna base de HCORDINTE, marcado como PlanAffectingConvert
      (dañaba la estimacion y el uso del indice). Con el CAST explicito en la proyeccion, el
      filtro del WHERE sigue sobre la columna CHAR nativa (Index Seek limpio) y el tipo de
      salida sigue siendo INT (contrato con la app SIN cambios).

  [E] Subconsulta correlacionada "TOP 1 ... INDIAGNOP" que estaba DENTRO del predicado del
      JOIN a INDIAGNOS -> reescrita como OUTER APPLY.
      MOTIVO: una subconsulta correlacionada en la condicion de JOIN obliga a evaluarla por
      cada fila y agranda el arbol logico; el OUTER APPLY es equivalente y mas eficiente.
      Se conserva el "TOP 1" sin ORDER BY (mismo comportamiento no-determinista del original).

  [F] RecommendPatient: "(SELECT COUNT(*) ...) > 0" -> "CASE WHEN EXISTS(...)".
      MOTIVO: EXISTS corta en la primera coincidencia; COUNT(*) recorre todo. Resultado (BIT)
      identico.

  [G] ADPOBESPEPAC y ADACOMPAN: subconsultas escalares "COUNT(*)" -> OUTER APPLY.
      MOTIVO: reduce el numero de subconsultas correlacionadas que el optimizador debe
      procesar. Semantica identica (el APPLY con agregado siempre devuelve una fila, 0 incl.).

**********************************************************************************************/

CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesInterconsulta]
(
    @CentroAtencion   CHAR(10),
    @UnidadFuncional  CHAR(10),
    @CodeProfesional  CHAR(20),
    @Usuario          CHAR(20)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @PerformAssistedInterconsultation AS BIT = 0;
    DECLARE @CodigoEspecialidadUno  AS VARCHAR(3);
    DECLARE @CodigoEspecialidadDos  AS VARCHAR(3);
    DECLARE @CodigoEspecialidadTres AS VARCHAR(3);

    SELECT @PerformAssistedInterconsultation = ISNULL(PerformAssistedInterconsultation, 0),
           @CodigoEspecialidadUno  = CODESPEC1,
           @CodigoEspecialidadDos  = CODESPEC2,
           @CodigoEspecialidadTres = CODESPEC3
    FROM INPROFSAL
    WHERE CODPROSAL = @CodeProfesional;

    IF @PerformAssistedInterconsultation = 1
    BEGIN
        /* ============================ RAMA ASISTIDA (Tipo 2) — sin temporales ============= */
        SELECT RTRIM(A.IPCODPACI) AS Identificacion,
               RTRIM(IPNOMCOMP)   AS Paciente,
               A.NUMINGRES        AS Ingreso,
               CASE WHEN AD.UFUAACTHOS IS NULL THEN AD.UFUCODIGO ELSE AD.UFUAACTHOS END AS CodigoUnidadFuncional,
               CASE WHEN RTRIM(D.UFUDESCRI) IS NULL
                    THEN (SELECT RTRIM(UFUDESCRI) FROM dbo.INUNIFUNC WHERE UFUCODIGO = AD.UFUCODIGO)
                    ELSE RTRIM(D.UFUDESCRI) END AS DescripcionUnidadFuncional,
               RTRIM(DESCCAMAS) AS Cama,
               dbo.TipoAislamiento(CA.CODAISLAM) AS Aislamiento,
               '2 - Solicitudes valoración de interconsultas' AS Tipo,
               H.DESESPECI AS Especialidad,
               TRIANUMER   AS Consecutivo,
               CASE WHEN B.IPTIPODOC IN (6,7) THEN 1 ELSE 0 END AS ASMS,
               B.ZONAPARTADA,
               L.RIESGOAGRE,
               pob.POBESPECIAL,
               AD.VIVESOLO,
               aco.ACOMPANANTES,
               CONVERT(BIT,0) AS Riesgo,
               B.IPSEXOPAC AS Sexo,
               AD.CODTIPPAC AS TipoPaciente,
               IPFECNACI AS 'Fecha Nacimiento',
               CAST('' AS CHAR(50)) AS Edad,
               RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) AS Diagnostico,
               RTRIM(x.CODENTIDA) + '-' + RTRIM(x.NOMENTIDA) AS 'EntidadPaciente',
               A.FECORDMED AS 'Fecha Solicitud',
               RTRIM(z.NOMMEDICO) AS 'Profesional',
               A.ESTSERIPS,
               ISNULL(A.NUMFOLIORESIDENTE,0) AS NUMFOLIORESIDENTE,
               A.IDETIPHIS,
               A.AUTO,
               A.CODESPECI AS CODESPECI,
               CASE WHEN EXISTS (SELECT 1 FROM dbo.RecommendPatient rp
                                 WHERE rp.IPCODPACI = A.IPCODPACI AND rp.NUMINGRES = A.NUMINGRES AND rp.Status = 1)
                    THEN CONVERT(BIT,1) ELSE CONVERT(BIT,0) END AS Recomendacion,
               IIF(B.PoblacionPAPSIVI = 1, CAST(1 AS BIT), CAST(0 AS BIT)) AS EsPoblacionPAPSIVI
        FROM dbo.HCORDINTE A
            INNER JOIN dbo.ADINGRESO AD
                ON A.NUMINGRES = AD.NUMINGRES
               AND (AD.IESTADOIN IN ('','P') OR (AD.IESTADOIN IN ('C') AND AD.CREADOAUTOMA = 1))
            LEFT OUTER JOIN dbo.INUNIFUNC D ON AD.UFUAACTHOS = D.UFUCODIGO
            INNER JOIN dbo.INPacient B ON A.IPCODPACI = B.IPCODPACI
            LEFT OUTER JOIN dbo.CHCAMASHO CA ON AD.CODCAMACT = CA.CODICAMAS
            INNER JOIN dbo.INESPECIA H ON A.CODESPECI = H.CODESPECI
            -- TOP 1: mismo caso de HCORDINTE/ADTRIAGEU 1-a-muchos que en Rama 3.
            OUTER APPLY (SELECT TOP 1 tr.TRIANUMER FROM dbo.ADTRIAGEU tr
                         WHERE tr.IPCODPACI = A.IPCODPACI AND tr.NUMINGRES = A.NUMINGRES) E
            LEFT OUTER JOIN dbo.ADACTIVID L ON B.CODACTIVI = L.codactivi
            OUTER APPLY (SELECT TOP 1 dp.CODDIAGNO FROM dbo.INDIAGNOP dp
                         WHERE dp.IPCODPACI = A.IPCODPACI AND dp.NUMINGRES = A.NUMINGRES AND dp.CODDIAPRI = 1) dgp
            LEFT OUTER JOIN dbo.INDIAGNOS Q ON Q.CODDIAGNO = dgp.CODDIAGNO
            INNER JOIN INENTIDAD x ON AD.CODENTIDA = x.CODENTIDA
            INNER JOIN INPROFSAL Z ON A.CODPROSAL = Z.CODPROSAL
            INNER JOIN PerformAssistedInterconsultation tm
                ON tm.CODESPECI = A.CODESPECI AND tm.CODPROSAL = @CodeProfesional
            OUTER APPLY (SELECT COUNT(*) AS POBESPECIAL FROM dbo.ADPOBESPEPAC pe WHERE pe.IPCODPACI = B.IPCODPACI) pob
            OUTER APPLY (SELECT COUNT(*) AS ACOMPANANTES FROM dbo.ADACOMPAN ac WHERE ac.NUMINGRES = AD.NUMINGRES) aco
        WHERE A.CODCENATE = @CentroAtencion
          AND A.ESTSERIPS = '1';

        RETURN;
    END

    /* ================================================================================
       CAMINO ESTANDAR: DESCOMPOSICION EN #Resultado
       #Resultado se declara explicitamente (tipos tomados de sp_help sobre el
       SELECT...INTO original) con TODAS las columnas NULL, porque la Rama 3
       (Mis pacientes) inserta NULL literal en 'Fecha Solicitud' y 'Profesional'.
       Con SELECT...INTO, SQL Server heredaba NOT NULL de las columnas origen de la
       Rama 1 (HCORDINTE.FECORDMED, INPROFSAL.NOMMEDICO) y el INSERT de la Rama 3
       fallaba: "Cannot insert the value NULL into column ...".
       ================================================================================ */
    CREATE TABLE #Resultado
    (
        Identificacion              VARCHAR(25)   NULL,
        Paciente                    VARCHAR(250)  NULL,
        Ingreso                     CHAR(10)      NULL,
        CodigoUnidadFuncional       CHAR(10)      NULL,
        DescripcionUnidadFuncional  VARCHAR(60)   NULL,
        Cama                        VARCHAR(50)   NULL,
        Aislamiento                 NVARCHAR(120) NULL,
        Tipo                        VARCHAR(50)   NULL,
        Especialidad                CHAR(60)      NULL,
        Consecutivo                 CHAR(20)      NULL,
        ASMS                        INT           NULL,
        ZONAPARTADA                 BIT           NULL,
        RIESGOAGRE                  BIT           NULL,
        POBESPECIAL                 INT           NULL,
        VIVESOLO                    BIT           NULL,
        ACOMPANANTES                INT           NULL,
        Riesgo                      BIT           NULL,
        Sexo                        INT           NULL,
        TipoPaciente                 INT           NULL,
        [Fecha Nacimiento]          DATETIME      NULL,
        Edad                        CHAR(50)      NULL,
        Diagnostico                 VARCHAR(355)  NULL,
        EntidadPaciente             VARCHAR(260)  NULL,
        [Fecha Solicitud]           DATETIME      NULL,
        Profesional                 VARCHAR(60)   NULL,
        ESTSERIPS                   INT           NULL,
        NUMFOLIORESIDENTE           NCHAR(20)     NULL,
        AUTO                        INT           NULL,
        CODESPECI                   CHAR(3)       NULL,
        Recomendacion               BIT           NULL,
        EsPoblacionPAPSIVI          BIT           NULL
    );

    /* -------------------- RAMA 1: Interconsultas (Tipo 2/4)  [puebla #Resultado] -------------------- */
    INSERT INTO #Resultado
    SELECT RTRIM(A.IPCODPACI) AS Identificacion,
           RTRIM(IPNOMCOMP)   AS Paciente,
           A.NUMINGRES        AS Ingreso,
           ISNULL(CA.UFUCODIGO, CASE WHEN AD.UFUAACTHOS IS NULL THEN AD.UFUCODIGO ELSE AD.UFUAACTHOS END) AS CodigoUnidadFuncional,
           ISNULL(RTRIM(UF_Cama.UFUDESCRI),
                  CASE WHEN RTRIM(D.UFUDESCRI) IS NULL
                       THEN (SELECT RTRIM(UFUDESCRI) FROM dbo.INUNIFUNC WHERE UFUCODIGO = ISNULL(AD.UFUAACTHOS, AD.UFUCODIGO))
                       ELSE RTRIM(D.UFUDESCRI) END) AS DescripcionUnidadFuncional,
           RTRIM(DESCCAMAS) AS Cama,
           dbo.TipoAislamiento(CA.CODAISLAM) AS Aislamiento,
           CAST(CASE ESTSERIPS WHEN '6' THEN '4 - Pendientes por validar'
                               ELSE '2 - Solicitudes valoración de interconsultas' END AS VARCHAR(50)) AS Tipo,
           H.DESESPECI AS Especialidad,
           TRIANUMER   AS Consecutivo,
           CASE WHEN B.IPTIPODOC IN (6,7) THEN 1 ELSE 0 END AS ASMS,
           B.ZONAPARTADA,
           L.RIESGOAGRE,
           pob.POBESPECIAL,
           AD.VIVESOLO,
           aco.ACOMPANANTES,
           CONVERT(BIT,0) AS Riesgo,
           B.IPSEXOPAC AS Sexo,
           AD.CODTIPPAC AS TipoPaciente,
           IPFECNACI AS [Fecha Nacimiento],
           CAST('' AS CHAR(50)) AS Edad,
           RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) AS Diagnostico,    
           RTRIM(x.CODENTIDA) + '-' + RTRIM(x.NOMENTIDA) AS [EntidadPaciente],
           A.FECORDMED AS [Fecha Solicitud],
           RTRIM(z.NOMMEDICO) AS [Profesional],
           CAST(A.ESTSERIPS AS INT) AS ESTSERIPS,                 -- [2] contrato INT sin cambios
           ISNULL(A.NUMFOLIORESIDENTE,0) AS NUMFOLIORESIDENTE,
           A.AUTO,
           A.CODESPECI AS CODESPECI,
           CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.RecommendPatient rp
                                  WHERE rp.IPCODPACI = A.IPCODPACI AND rp.NUMINGRES = A.NUMINGRES AND rp.Status = 1)
                     THEN 1 ELSE 0 END AS BIT) AS Recomendacion,
           IIF(B.PoblacionPAPSIVI = 1, CAST(1 AS BIT), CAST(0 AS BIT)) AS EsPoblacionPAPSIVI
    FROM dbo.HCORDINTE A
        INNER JOIN dbo.ADINGRESO AD
            ON A.NUMINGRES = AD.NUMINGRES
           AND (AD.IESTADOIN IN ('','P') OR (AD.IESTADOIN IN ('C') AND AD.CREADOAUTOMA = 1))
        LEFT OUTER JOIN dbo.INUNIFUNC D ON AD.UFUAACTHOS = D.UFUCODIGO
        INNER JOIN dbo.INPacient B ON A.IPCODPACI = B.IPCODPACI
        OUTER APPLY (SELECT TOP 1 CODICAMAS FROM dbo.CHREGESTA
                     WHERE NUMINGRES = AD.NUMINGRES AND REGESTADO = 1 ORDER BY FECINIEST DESC) RG
        LEFT OUTER JOIN dbo.CHCAMASHO CA ON RG.CODICAMAS = CA.CODICAMAS
        LEFT OUTER JOIN dbo.INUNIFUNC UF_Cama ON CA.UFUCODIGO = UF_Cama.UFUCODIGO
        INNER JOIN dbo.INESPECIA H ON A.CODESPECI = H.CODESPECI
        -- TOP 1: mismo caso de HCORDINTE/ADTRIAGEU 1-a-muchos que en Rama 3 (ver Rama 3
        -- Mis Pacientes) -> LEFT JOIN plano duplicaba la fila del paciente en esta rama.
        OUTER APPLY (SELECT TOP 1 tr.TRIANUMER FROM dbo.ADTRIAGEU tr
                     WHERE tr.IPCODPACI = A.IPCODPACI AND tr.NUMINGRES = A.NUMINGRES) E
        LEFT OUTER JOIN dbo.ADACTIVID L ON B.CODACTIVI = L.codactivi
        OUTER APPLY (SELECT TOP 1 dp.CODDIAGNO FROM dbo.INDIAGNOP dp
                     WHERE dp.IPCODPACI = A.IPCODPACI AND dp.NUMINGRES = A.NUMINGRES AND dp.CODDIAPRI = 1) dgp
        LEFT OUTER JOIN dbo.INDIAGNOS Q ON Q.CODDIAGNO = dgp.CODDIAGNO
        INNER JOIN INENTIDAD x ON AD.CODENTIDA = x.CODENTIDA
        INNER JOIN INPROFSAL Z ON A.CODPROSAL = Z.CODPROSAL
        OUTER APPLY (SELECT COUNT(*) AS POBESPECIAL FROM dbo.ADPOBESPEPAC pe WHERE pe.IPCODPACI = B.IPCODPACI) pob
        OUTER APPLY (SELECT COUNT(*) AS ACOMPANANTES FROM dbo.ADACOMPAN ac WHERE ac.NUMINGRES = AD.NUMINGRES) aco
    WHERE A.CODCENATE = @CentroAtencion
      AND ESTSERIPS IN ('1','6')
      AND A.CODESPECI IN (@CodigoEspecialidadUno, @CodigoEspecialidadDos, @CodigoEspecialidadTres);

    /* -------------------- RAMA 2: Remisión de urgencias (Tipo 1) -------------------- */
    INSERT INTO #Resultado
    SELECT RTRIM(B.IPCODPACI),
           RTRIM(F.IPNOMCOMP),
           A.NUMINGRES,
           B.ufucodigo,
           UF.UFUDESCRI,
           CAST('' AS CHAR),
           CAST('' AS CHAR),
           '1 - Remisión de urgencias',
           H.DESESPECI,
           TRIANUMER,
           CASE WHEN F.IPTIPODOC IN (6,7) THEN 1 ELSE 0 END,
           F.ZONAPARTADA,
           L.RIESGOAGRE,
           pob.POBESPECIAL,
           AD.VIVESOLO,
           aco.ACOMPANANTES,
           CONVERT(BIT,0),
           F.IPSEXOPAC,
           AD.CODTIPPAC,
           IPFECNACI,
           CAST('' AS CHAR(50)),
           RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO),
           RTRIM(x.CODENTIDA) + '-' + RTRIM(x.NOMENTIDA),
           A.TRIAFECHA,
           RTRIM(z.NOMMEDICO),
           0,                                                     -- ESTSERIPS INT
           0,
           0,
           A.CODESPECI,
           CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.RecommendPatient rp
                                  WHERE rp.IPCODPACI = A.IPCODPACI AND rp.NUMINGRES = A.NUMINGRES AND rp.Status = 1)
                     THEN 1 ELSE 0 END AS BIT),
           IIF(F.PoblacionPAPSIVI = 1, CAST(1 AS BIT), CAST(0 AS BIT))
    FROM dbo.ADTRIAGEU A
        INNER JOIN dbo.ADINGRESO AD ON A.NUMINGRES = AD.NUMINGRES AND AD.IESTADOIN IN ('','P')
        INNER JOIN dbo.ADCONTURG B ON A.CODCONCEC = B.CODCONCEC
        INNER JOIN INUNIFUNC UF ON B.UFUCODIGO = UF.UFUCODIGO
        INNER JOIN dbo.ADCATTRIU E ON E.TRIACATEG = A.TRIACATEG
        LEFT OUTER JOIN dbo.INPACIENT F ON B.IPCODPACI = F.IPCODPACI
        INNER JOIN dbo.INESPECIA H ON A.CODESPECI = H.CODESPECI
        INNER JOIN dbo.ADACTIVID L ON F.CODACTIVI = L.codactivi
        OUTER APPLY (SELECT TOP 1 dp.CODDIAGNO FROM dbo.INDIAGNOP dp
                     WHERE dp.IPCODPACI = A.IPCODPACI AND dp.NUMINGRES = A.NUMINGRES AND dp.CODDIAPRI = 1) dgp
        LEFT OUTER JOIN dbo.INDIAGNOS Q ON Q.CODDIAGNO = dgp.CODDIAGNO
        INNER JOIN INENTIDAD x ON AD.CODENTIDA = x.CODENTIDA
        INNER JOIN INPROFSAL Z ON A.CODPROSAL = Z.CODPROSAL
        OUTER APPLY (SELECT COUNT(*) AS POBESPECIAL FROM dbo.ADPOBESPEPAC pe WHERE pe.IPCODPACI = B.IPCODPACI) pob
        OUTER APPLY (SELECT COUNT(*) AS ACOMPANANTES FROM dbo.ADACOMPAN ac WHERE ac.NUMINGRES = AD.NUMINGRES) aco
    WHERE TRIORIGEN = '2'
      AND TRREMITID = '1'
      AND A.CODESPECI IN (@CodigoEspecialidadUno, @CodigoEspecialidadDos, @CodigoEspecialidadTres)
      AND CONESTADO = '4'
      AND A.CODCENATE = @CentroAtencion
      AND A.NUMINGRES IS NOT NULL
      AND A.NUMINGRES NOT IN (SELECT NUMDOCUME FROM dbo.INDOCUMEN
                              WHERE CODDOCUME = '3' AND CODUSUARI <> @Usuario);

    /* -------------------- RAMA 3: Mis pacientes (Tipo 3) -------------------- */
    INSERT INTO #Resultado
    SELECT A.IPCODPACI,
           RTRIM(IPNOMCOMP),
           A.NUMINGRES,
           ISNULL(CA.UFUCODIGO, CASE WHEN A.UFUAACTHOS IS NULL THEN A.UFUCODIGO ELSE A.UFUAACTHOS END),
           ISNULL(RTRIM(UF_Cama.UFUDESCRI),
                  CASE WHEN RTRIM(D.UFUDESCRI) IS NULL
                       THEN (SELECT RTRIM(UFUDESCRI) FROM dbo.INUNIFUNC WHERE UFUCODIGO = ISNULL(A.UFUAACTHOS, A.UFUCODIGO))
                       ELSE RTRIM(D.UFUDESCRI) END),
           RTRIM(DESCCAMAS),
           CAST('' AS CHAR),
           '3 - Mis pacientes',
           H.DESESPECI,
           TRIANUMER,
           CASE WHEN C.IPTIPODOC IN (6,7) THEN 1 ELSE 0 END,
           C.ZONAPARTADA,
           L.RIESGOAGRE,
           pob.POBESPECIAL,
           A.VIVESOLO,
           aco.ACOMPANANTES,
           CONVERT(BIT,0),
           C.IPSEXOPAC,
           A.CODTIPPAC,
           IPFECNACI,
           CAST('' AS CHAR(50)),
           RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO),
           RTRIM(x.CODENTIDA) + '-' + RTRIM(x.NOMENTIDA),
           NULL,
           NULL,
           0,                                                     -- ESTSERIPS INT
           0,
           0,
           A.CODESPTRA,
           CAST(CASE WHEN EXISTS (SELECT 1 FROM dbo.RecommendPatient rp
                                  WHERE rp.IPCODPACI = A.IPCODPACI AND rp.NUMINGRES = A.NUMINGRES AND rp.Status = 1)
                     THEN 1 ELSE 0 END AS BIT),
           IIF(C.PoblacionPAPSIVI = 1, CAST(1 AS BIT), CAST(0 AS BIT))
    FROM ADINGRESO A
        INNER JOIN INPACIENT C ON A.IPCODPACI = C.IPCODPACI
        INNER JOIN ADACTIVID L ON C.CODACTIVI = L.codactivi
        LEFT JOIN INUNIFUNC D ON A.UFUAACTHOS = D.UFUCODIGO
        OUTER APPLY (SELECT TOP 1 CODICAMAS FROM dbo.CHREGESTA
                     WHERE NUMINGRES = A.NUMINGRES AND REGESTADO = 1 ORDER BY FECINIEST DESC) N
        LEFT OUTER JOIN dbo.CHCAMASHO CA ON N.CODICAMAS = CA.CODICAMAS
        LEFT OUTER JOIN dbo.INUNIFUNC UF_Cama ON CA.UFUCODIGO = UF_Cama.UFUCODIGO
        INNER JOIN dbo.INESPECIA H ON A.CODESPTRA = H.CODESPECI
        -- TOP 1: en binomio madre-hijo puede haber 2 filas en ADTRIAGEU para el mismo
        -- IPCODPACI+NUMINGRES (mismo TRIANUMER en ambas); un LEFT JOIN plano duplicaba
        -- la fila del paciente en "Mis pacientes". Confirmado con negocio: 1 fila esperada.
        OUTER APPLY (SELECT TOP 1 tr.TRIANUMER FROM dbo.ADTRIAGEU tr
                     WHERE tr.IPCODPACI = A.IPCODPACI AND tr.NUMINGRES = A.NUMINGRES) E
        OUTER APPLY (SELECT TOP 1 dp.CODDIAGNO FROM dbo.INDIAGNOP dp
                     WHERE dp.IPCODPACI = A.IPCODPACI AND dp.NUMINGRES = A.NUMINGRES AND dp.CODDIAPRI = 1) dgp
        LEFT OUTER JOIN dbo.INDIAGNOS Q ON Q.CODDIAGNO = dgp.CODDIAGNO
        INNER JOIN INENTIDAD x ON A.CODENTIDA = x.CODENTIDA
        OUTER APPLY (SELECT COUNT(*) AS POBESPECIAL FROM dbo.ADPOBESPEPAC pe WHERE pe.IPCODPACI = C.IPCODPACI) pob
        OUTER APPLY (SELECT COUNT(*) AS ACOMPANANTES FROM dbo.ADACOMPAN ac WHERE ac.NUMINGRES = A.NUMINGRES) aco
    WHERE A.CODCENATE = @CentroAtencion
      AND A.CODESPTRA IN (@CodigoEspecialidadUno, @CodigoEspecialidadDos, @CodigoEspecialidadTres)
      AND A.IESTADOIN = ''
      AND A.UFUEGRHOS IS NULL
      AND UF_Cama.UFUTIPUNI NOT IN ('15','24');

    /* -------------------- ENSAMBLE FINAL -------------------- */
    SELECT * FROM #Resultado;

    DROP TABLE #Resultado;
END
GO
