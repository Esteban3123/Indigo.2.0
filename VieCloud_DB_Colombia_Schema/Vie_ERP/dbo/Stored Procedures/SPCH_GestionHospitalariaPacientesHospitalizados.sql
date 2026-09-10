CREATE PROCEDURE [dbo].[SPCH_GestionHospitalariaPacientesHospitalizados]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH CTE_PobEspecial AS
    (
        SELECT
            Z.IPCODPACI,
            COUNT(*) AS POBESPECIAL
        FROM dbo.ADPOBESPEPAC Z WITH (NOLOCK)
        INNER JOIN dbo.ADPOBESPE X WITH (NOLOCK)
            ON X.ID = Z.IDADPOBESPE
        WHERE X.TIPOPOESPERIES = 1
        GROUP BY Z.IPCODPACI
    ),
    CTE_Acompanantes AS
    (
        SELECT
            NUMINGRES,
            COUNT(*) AS ACOMPANANTES
        FROM dbo.ADACOMPAN WITH (NOLOCK)
        GROUP BY NUMINGRES
    ),
    CTE_Hemocomponentes AS
    (
        SELECT
            N.IPCODPACI,
            COUNT(CASE WHEN M.CONFRECBOL = 1 THEN 1 END) AS BOLSAS,
            COUNT(CASE WHEN M.ESTADO = 6 AND M.CONFRECBOL = 1 THEN 1 END) AS ESTADO
        FROM dbo.HCORHEMBOL M WITH (NOLOCK)
        INNER JOIN dbo.HCORHEMCO N WITH (NOLOCK)
            ON M.HCORHEMCOID = N.ID
        GROUP BY N.IPCODPACI
    ),
    CTE_RecommendPatient AS
    (
        SELECT
            IPCODPACI,
            NUMINGRES,
            CONVERT(BIT, 1) AS Recomendacion
        FROM dbo.RecommendPatient WITH (NOLOCK)
        WHERE Status = 1
        GROUP BY IPCODPACI, NUMINGRES
    )
    SELECT DISTINCT
        CASE
            WHEN X.INDICAPAC = '22' THEN '2 - Pre-alta hospitalaria'
            WHEN I.NUMINGRES IS NULL THEN '1 - Pacientes en la unidad'
            ELSE '3 - Pacientes con salida'
        END AS Egreso,
        ISNULL(DESTINOPAC, 0) AS DestinoSalidaParcial,
        CAST(' ' AS CHAR(100)) AS Origen,
        'Normal' AS Alerta,
        A.CODICAMAS AS [Codigo Cama],
        RTRIM(DESCCAMAS) AS Cama,
        dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion,
        dbo.ClaseCama(A.CODCLACAM) AS [Clase de Cama],
        C.IPCODPACI AS Identificacion,
        C.NUMINGRES AS Ingreso,
        dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento,
        RTRIM(DESTIPEST) AS [Tipo Estancia],
        RTRIM(H.IPNOMCOMP) AS Paciente,
        CAST(0 AS BIT) AS Resultado,
        A.CAMTRACIR AS TrasladoCirugia,
        A.CAMTRAMED AS TrasladoMedicamentos,
        A.CODCONCEC AS Consecutivo,
        CAST('' AS BIT) AS MuestraAlerta,
        K.CODESPECI AS CodigoEspecialidad,
        RTRIM(K.DESESPECI) AS DescripcionEspecialidad,
        IFECHAING,

        J.ESCADOWNT,
        J.ESCARASS,
        J.ESCNORPAC,
        J.ESCVASPAC,
        J.ESCAPAPAC,

        dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) AS PUNTAJEDOWN,
        dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) AS PUNTAJERASS,
        dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) AS PUNTAJENORTON,
        dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) AS PUNTAJEVAS,
        dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) AS PUNTAJEAPACHE,

        Escalas.ESCALACAIDA,
        Escalas.ESCALADOLOR,
        Escalas.ESCALASGENERAL,
        Resultados.RESULTESCAIDA,
        Resultados.RESULTESDOLOR,
        Tipos.TIPOESCAIDA,
        Tipos.TIPOESDOLOR,

        CASE WHEN H.IPTIPODOC IN (6, 7) THEN 1 ELSE 0 END AS ASMS,
        H.ZONAPARTADA,
        L.RIESGOAGRE,
        ISNULL(PE.POBESPECIAL, 0) AS POBESPECIAL,
        J.VIVESOLO,
        ISNULL(AC.ACOMPANANTES, 0) AS ACOMPANANTES,
        ISNULL(HC.BOLSAS, 0) AS BOLSAS,
        ISNULL(HC.ESTADO, 0) AS ESTADO,
        CONVERT(BIT, 0) AS Riesgo,
        CONVERT(BIT, 0) AS Hemocomponente,
        IPFECNACI AS [Fecha Nacimiento],
        CAST('' AS CHAR(50)) AS Edad,
        H.IPSEXOPAC AS Sexo,
        J.CODTIPPAC AS TipoPaciente,
        RTRIM(P.CODENTIDA) + '-' + RTRIM(P.NOMENTIDA) AS EntidadPaciente,
        RTRIM(Q.CODDIAGNO) + '-' + RTRIM(Q.NOMDIAGNO) AS Diagnostico,
        Z.Color,
        Prof.CODPROSAL AS CodigoMedicoTratante,
        RTRIM(Prof.NOMMEDICO) AS NombreMedicoTratante,
        ISNULL(RP.Recomendacion, CONVERT(BIT, 0)) AS Recomendacion,
        IIF(H.PoblacionPAPSIVI = 1, CAST(1 AS BIT), CAST(0 AS BIT)) AS EsPoblacionPAPSIVI
    FROM dbo.CHCAMASHO A WITH (NOLOCK)
    INNER JOIN dbo.ADCENATEN D WITH (NOLOCK)
        ON A.CODCENATE = D.CODCENATE
    INNER JOIN dbo.INUNIFUNC E WITH (NOLOCK)
        ON A.UFUCODIGO = E.UFUCODIGO
    LEFT OUTER JOIN dbo.CHREGESTA C WITH (NOLOCK)
        ON A.CODICAMAS = C.CODICAMAS
       AND C.REGESTADO = 1
    LEFT OUTER JOIN dbo.CHTIPESTA G WITH (NOLOCK)
        ON G.CODTIPEST = C.CODTIPEST
    INNER JOIN dbo.INPACIENT H WITH (NOLOCK)
        ON C.IPCODPACI = H.IPCODPACI
    LEFT OUTER JOIN dbo.HCREGEGRE I WITH (NOLOCK)
        ON C.NUMINGRES = I.NUMINGRES
    INNER JOIN dbo.ADINGRESO J WITH (NOLOCK)
        ON C.NUMINGRES = J.NUMINGRES
    OUTER APPLY
    (
        SELECT TOP (1)
            INDICAPAC,
            IPCODPACI,
            NUMINGRES
        FROM dbo.HCHISPACA WITH (NOLOCK)
        WHERE IPCODPACI = J.IPCODPACI
          AND NUMINGRES = J.NUMINGRES
        ORDER BY FECHISPAC DESC
    ) AS X
    LEFT OUTER JOIN dbo.INESPECIA K WITH (NOLOCK)
        ON C.CODESPECI = K.CODESPECI
    LEFT OUTER JOIN dbo.ADACTIVID L WITH (NOLOCK)
        ON H.CODACTIVI = L.CODACTIVI
    LEFT OUTER JOIN dbo.ADTRIAGEU M WITH (NOLOCK)
        ON M.NUMINGRES = J.NUMINGRES
    LEFT OUTER JOIN dbo.ADCONTURG N WITH (NOLOCK)
        ON N.CODCONCEC = M.CODCONCEC
    INNER JOIN dbo.INENTIDAD P WITH (NOLOCK)
        ON P.CODENTIDA = J.CODENTIDA
    OUTER APPLY
    (
        SELECT TOP (1)
            DP.CODDIAGNO,
            DN.NOMDIAGNO
        FROM dbo.INDIAGNOP DP WITH (NOLOCK)
        INNER JOIN dbo.INDIAGNOS DN WITH (NOLOCK)
            ON DN.CODDIAGNO = DP.CODDIAGNO
        WHERE DP.IPCODPACI = H.IPCODPACI
          AND DP.NUMINGRES = J.NUMINGRES
          AND DP.CODDIAPRI = 1
    ) AS Q
    LEFT OUTER JOIN dbo.CHTIPOSAISLAMIENTOS Z WITH (NOLOCK)
        ON A.CODAISLAM = Z.Id
    LEFT OUTER JOIN dbo.INPROFSAL Prof WITH (NOLOCK)
        ON C.CODPROSAL = Prof.CODPROSAL
    LEFT JOIN CTE_PobEspecial PE
        ON PE.IPCODPACI = H.IPCODPACI
    LEFT JOIN CTE_Acompanantes AC
        ON AC.NUMINGRES = J.NUMINGRES
    LEFT JOIN CTE_Hemocomponentes HC
        ON HC.IPCODPACI = H.IPCODPACI
    LEFT JOIN CTE_RecommendPatient RP
        ON RP.IPCODPACI = C.IPCODPACI
       AND RP.NUMINGRES = C.NUMINGRES
    OUTER APPLY
    (
        SELECT
            CAST
            (
                CASE
                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM dbo.HCESCALAS HE WITH (NOLOCK)
                        WHERE HE.IPCODPACI = J.IPCODPACI
                          AND HE.NUMINGRES = J.NUMINGRES
                          AND HE.TIPOESCALA IN (47, 92, 113)
                    ) THEN 1
                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM dbo.HCESCDOWN HW WITH (NOLOCK)
                        WHERE HW.IPCODPACI = J.IPCODPACI
                          AND HW.NUMINGRES = J.NUMINGRES
                    ) THEN 1
                    ELSE 0
                END AS BIT
            ) AS ESCALACAIDA,

            CAST
            (
                CASE
                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM dbo.HCESCALAS HE WITH (NOLOCK)
                        WHERE HE.IPCODPACI = J.IPCODPACI
                          AND HE.NUMINGRES = J.NUMINGRES
                          AND HE.TIPOESCALA IN (49, 90, 91, 93)
                    ) THEN 1
                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM dbo.HCESCVASC VASC WITH (NOLOCK)
                        INNER JOIN dbo.HCESCVASD VASD WITH (NOLOCK)
                            ON VASC.CODCONSEC = VASD.CODCONSEC
                        WHERE VASC.IPCODPACI = J.IPCODPACI
                          AND VASC.NUMINGRES = J.NUMINGRES
                    ) THEN 1
                    ELSE 0
                END AS BIT
            ) AS ESCALADOLOR,

            CAST
            (
                CASE
                    WHEN EXISTS
                    (
                        SELECT 1
                        FROM dbo.HCESCALAS HE WITH (NOLOCK)
                        WHERE HE.IPCODPACI = J.IPCODPACI
                          AND HE.NUMINGRES = J.NUMINGRES
                          AND NOT (HE.TIPOESCALA IN (47, 92, 49, 90, 91, 93))
                    ) THEN 1
                    ELSE 0
                END AS BIT
            ) AS ESCALASGENERAL
    ) AS Escalas
    OUTER APPLY
    (
        SELECT
            (
                SELECT TOP (1) RESULTESCAIDA
                FROM
                (
                    SELECT TOP (1)
                        HE.RESULTADO AS RESULTESCAIDA,
                        HE.FECHAREGISTRO AS FECHA
                    FROM dbo.HCESCALAS HE WITH (NOLOCK)
                    WHERE HE.IPCODPACI = J.IPCODPACI
                      AND HE.NUMINGRES = J.NUMINGRES
                      AND HE.TIPOESCALA IN (47, 92, 113)
                    ORDER BY HE.FECHAREGISTRO DESC

                    UNION ALL

                    SELECT TOP (1)
                        ISNULL(HW.RESULTADO, 0) AS RESULTESCAIDA,
                        HW.FECREGSIS AS FECHA
                    FROM dbo.HCESCDOWN HW WITH (NOLOCK)
                    WHERE HW.IPCODPACI = J.IPCODPACI
                      AND HW.NUMINGRES = J.NUMINGRES
                      AND HW.TIPOESCALA IN (47, 92, 113)
                    ORDER BY HW.FECREGSIS DESC
                ) T
                ORDER BY FECHA DESC
            ) AS RESULTESCAIDA,

            (
                SELECT TOP (1) RESULTESDOLOR
                FROM
                (
                    SELECT TOP (1)
                        VASD.CODTIPDOL AS RESULTESDOLOR,
                        VASC.FECREGSIS AS FECHA
                    FROM dbo.HCESCVASC VASC WITH (NOLOCK)
                    INNER JOIN dbo.HCESCVASD VASD WITH (NOLOCK)
                        ON VASC.CODCONSEC = VASD.CODCONSEC
                    WHERE VASC.IPCODPACI = J.IPCODPACI
                      AND VASC.NUMINGRES = J.NUMINGRES
                    ORDER BY VASC.FECREGSIS DESC

                    UNION ALL

                    SELECT TOP (1)
                        HE.RESULTADO AS RESULTESDOLOR,
                        HE.FECHAREGISTRO AS FECHA
                    FROM dbo.HCESCALAS HE WITH (NOLOCK)
                    WHERE HE.IPCODPACI = J.IPCODPACI
                      AND HE.NUMINGRES = J.NUMINGRES
                      AND HE.TIPOESCALA IN (49, 90, 91, 93)
                    ORDER BY HE.FECHAREGISTRO DESC
                ) T
                ORDER BY FECHA DESC
            ) AS RESULTESDOLOR
    ) AS Resultados
    OUTER APPLY
    (
        SELECT
            (
                SELECT TOP (1) TIPOESCAIDA
                FROM
                (
                    SELECT TOP (1)
                        HE.TIPOESCALA AS TIPOESCAIDA,
                        HE.FECHAREGISTRO AS FECHA
                    FROM dbo.HCESCALAS HE WITH (NOLOCK)
                    WHERE HE.IPCODPACI = J.IPCODPACI
                      AND HE.NUMINGRES = J.NUMINGRES
                      AND HE.TIPOESCALA IN (47, 92, 113)
                    ORDER BY HE.FECHAREGISTRO DESC

                    UNION ALL

                    SELECT TOP (1)
                        HW.TIPOESCALA AS TIPOESCAIDA,
                        HW.FECREGSIS AS FECHA
                    FROM dbo.HCESCDOWN HW WITH (NOLOCK)
                    WHERE HW.IPCODPACI = J.IPCODPACI
                      AND HW.NUMINGRES = J.NUMINGRES
                    ORDER BY HW.FECREGSIS DESC
                ) T
                ORDER BY FECHA DESC
            ) AS TIPOESCAIDA,

            (
                SELECT TOP (1) TIPOESDOLOR
                FROM
                (
                    SELECT TOP (1)
                        HE.TIPOESCALA AS TIPOESDOLOR,
                        HE.FECHAREGISTRO AS FECHA
                    FROM dbo.HCESCALAS HE WITH (NOLOCK)
                    WHERE HE.IPCODPACI = J.IPCODPACI
                      AND HE.NUMINGRES = J.NUMINGRES
                      AND HE.TIPOESCALA IN (49, 90, 91, 93)
                    ORDER BY HE.FECHAREGISTRO DESC

                    UNION ALL

                    SELECT TOP (1)
                        49 AS TIPOESDOLOR,
                        VASC.FECREGSIS AS FECHA
                    FROM dbo.HCESCVASC VASC WITH (NOLOCK)
                    INNER JOIN dbo.HCESCVASD VASD WITH (NOLOCK)
                        ON VASC.CODCONSEC = VASD.CODCONSEC
                    WHERE VASC.IPCODPACI = J.IPCODPACI
                      AND VASC.NUMINGRES = J.NUMINGRES
                      AND VASD.CODTIPDOL IS NOT NULL
                    ORDER BY VASC.FECREGSIS DESC
                ) T
                ORDER BY FECHA DESC
            ) AS TIPOESDOLOR
    ) AS Tipos
    WHERE A.CODCENATE = @CentroAtencion
      AND A.UFUCODIGO = @UnidadFuncional
      AND ESTADCAMA IN ('2', '8')
    ORDER BY A.CODICAMAS;
END
GO
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de gestión hospitalaria que obtiene el censo en tiempo real de pacientes hospitalizados en una unidad funcional y centro de atención específicos. Consolida información de camas (estado, clase, aislamiento, traslado a cirugía o medicamentos), el ingreso del paciente (número de ingreso, identificación, tipo de paciente, entidad aseguradora, diagnóstico principal) y el estado de estancia (en unidad, pre-alta o con salida). Además incorpora puntajes clínicos de escalas de evaluación como riesgo de caída (Down-Ton), nivel de sedación (RASS), riesgo de úlceras por presión (Norton), dolor (VAS) y gravedad (APACHE), indicando si cada escala ha sido diligenciada. Complementa con datos sociales del paciente (si vive solo, zona apartada, riesgo de agresividad, población especial, acompañantes) y estado de hemocomponentes (bolsas solicitadas y entregadas), siendo el núcleo del tablero de control de enfermería y coordinación hospitalaria para la toma de decisiones asistenciales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_GestionHospitalariaPacientesHospitalizados';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_GestionHospitalariaPacientesHospitalizados';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes hospitalizados (y sus indicadores clínicos: escalas, hemocomponentes, acompañantes, diagnóstico, médico tratante, alertas) ocupando camas de un centro de atención y unidad funcional dados, clasificando su estado de egreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir registros en CHCAMASHO para el centro de atención y unidad funcional indicados; Las camas a listar deben estar en estado ''2'' u ''8''; Debe existir paciente en INPACIENT y un ingreso en ADINGRESO asociados al registro de estancia activo (CHREGESTA.REGESTADO = 1); Debe existir la entidad (INENTIDAD) asociada al ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen camas cuyo ESTADCAMA esté en (''2'',''8'') (camas ocupadas/aplicables al listado hospitalario); El listado se restringe al centro de atención y unidad funcional indicados en los parámetros; Solo se considera el registro activo de estancia en CHREGESTA (REGESTADO = 1); Para población especial solo se cuentan registros de ADPOBESPE con TIPOPOESPERIES = 1; Para hemocomponentes solo se cuentan bolsas con CONFRECBOL = 1, y aparte aquellas en ESTADO = 6 confirmadas; Solo se consideran recomendaciones activas (RecommendPatient.Status = 1); El diagnóstico mostrado es el principal (CODDIAPRI = 1) del ingreso; El indicador de pre-alta se toma del último registro de HCHISPACA (ORDER BY FECHISPAC DESC); Resultados y tipos de escalas de caída/dolor se obtienen del registro más reciente entre las fuentes posibles; El procedimiento es de solo lectura (SET NOCOUNT ON, sin DML)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Pacientes hospitalizados; Camas hospitalarias; Centro de atención; Unidad funcional; Egreso/Pre-alta hospitalaria; Aislamiento; Clase de habitación y cama; Tipo de estancia; Especialidad médica; Médico tratante; Diagnóstico principal; Entidad/Aseguradora; Triage de urgencias; Población especial (riesgo); Acompañantes; Hemocomponentes/Transfusiones; Recomendaciones de interconsulta; Escalas clínicas (Down-Ton, RASS, Norton, VAS, Apache); Escalas de caída y dolor; Población PAPSIVI; ASMS (tipo de documento); Riesgo de agresión; Vive solo / Zona apartada', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando ESTADCAMA IN (''2'',''8'') y la cama pertenece al centro y unidad funcional indicados, se retorna una fila por cama con los datos del paciente y su clasificación de egreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Última historia clínica del paciente (HCHISPACA) tiene INDICAPAC = ''22'' → Se clasifica el egreso como ''2 - Pre-alta hospitalaria'' else Si no existe registro de egreso (HCREGEGRE.NUMINGRES IS NULL) se clasifica como ''1 - Pacientes en la unidad''; en caso contrario ''3 - Pacientes con salida''; si Tipo de documento del paciente IN (6,7) → Se marca la bandera ASMS = 1 else ASMS = 0; si Existen escalas de caída (HCESCALAS con TIPOESCALA 47, 92, 113) o registro en HCESCDOWN → Se marca ESCALACAIDA = 1 else ESCALACAIDA = 0; si Existen escalas de dolor (HCESCALAS con TIPOESCALA 49, 90, 91, 93) o registro VAS (HCESCVASC/HCESCVASD) → Se marca ESCALADOLOR = 1 else ESCALADOLOR = 0; si Existen escalas en HCESCALAS distintas a las de caída/dolor (47,92,49,90,91,93) → Se marca ESCALASGENERAL = 1 else ESCALASGENERAL = 0; si PoblacionPAPSIVI = 1 en INPACIENT → Se marca EsPoblacionPAPSIVI = 1 else EsPoblacionPAPSIVI = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaNorton; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.HCORHEMBOL; dbo.HCORHEMCO; dbo.RecommendPatient; dbo.CHCAMASHO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPACIENT; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.HCHISPACA; dbo.INESPECIA; dbo.ADACTIVID; dbo.ADTRIAGEU; dbo.ADCONTURG; dbo.INENTIDAD; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.CHTIPOSAISLAMIENTOS; dbo.INPROFSAL; dbo.HCESCALAS; dbo.HCESCDOWN; dbo.HCESCVASC; dbo.HCESCVASD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizados';
-- GO
