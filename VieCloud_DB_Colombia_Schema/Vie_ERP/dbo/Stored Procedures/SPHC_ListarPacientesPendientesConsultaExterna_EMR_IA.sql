CREATE PROCEDURE [dbo].[SPHC_ListarPacientesPendientesConsultaExterna_EMR_IA]
(
    @CentroAtencion CHAR(10),
    @Profesional CHAR(20),
    @FechaInicial DATETIME,
    @FechaFinal DATETIME,
    @ProfesionalConAsistida CHAR(10),
    @Consultorio CHAR(10),
    @EsConsultaAsistida BIT,
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @Page < 1 SET @Page = 1;
    IF @PageSize < 1 SET @PageSize = 20;
    DECLARE @Offset INT = (@Page - 1) * @PageSize;

    ;WITH CTE_Resultados AS
    (
        -- BLOQUE 1: CONSULTAS ATENDIDAS / EN ESPERA
        SELECT  
            g.CODAUTONU AS Consecutivo,
            g.FECHORAIN AS FechaInicioCita,
            g.FECHORAFI AS FechaFinCita,
            RTRIM(E.DESESPECI) AS DescripcionEspecialidad,
            CASE A.CONESTADO WHEN '6' THEN 'attended' ELSE 'waiting' END AS Egreso,
            'consulta_externa' AS TipoCita,
            A.IPCODPACI AS Identificacion,
            B.IPDIRECCI AS Direccion,
            B.IPTELMOVI AS Telefono, 
            RTRIM(B.IPNOMCOMP) AS Paciente,
            RTRIM(B.IPPRINOMB) AS PrimerNombre,
            RTRIM(B.IPSEGNOMB) AS SegundoNombre,
            RTRIM(B.IPPRIAPEL) AS PrimerApellido,
            RTRIM(B.IPSEGAPEL) AS SegundoApellido,
            A.NUMINGRES AS Ingreso,
            B.IPSEXOPAC AS Sexo,
            RTRIM(C.NOMENTIDA) AS NombreEntidad,
            RTRIM(Z.DESACTMED) AS Actividad,
            CASE WHEN G.MODALIDAD = 0 THEN 'presencial' WHEN G.MODALIDAD = 1 THEN 'teleconsulta' ELSE '' END AS Modalidad,
            B.IPFECNACI AS FechaNacimiento,
            CONCAT('Consulta externa: ', g.CODIGOCON, ' - ', con.DESCRICON) AS AppointmentLocation
        FROM dbo.ADCONCOEX A WITH (NOLOCK)
        INNER JOIN dbo.AGASICITA g WITH (NOLOCK) ON A.NUMCONCIT = g.CODAUTONU
        INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI = B.IPCODPACI
        INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON A.CODENTIDA = C.CODENTIDA
        INNER JOIN dbo.AGACTIMED Z WITH (NOLOCK) ON Z.CODACTMED = g.CODACTMED
        LEFT JOIN dbo.AGCONSULT con WITH (NOLOCK) ON g.CODIGOCON = con.CODIGOCON 
        LEFT JOIN dbo.INESPECIA E WITH (NOLOCK) ON g.CODESPECI = E.CODESPECI
        WHERE @EsConsultaAsistida = 0
          AND A.CODCENATE = @CentroAtencion
          AND A.CONESTADO IN ('1','4','5','6')
          AND g.TIPSOLICITU = 1
          AND A.CODPROSAL = @Profesional
          AND g.FECHORAIN >= @FechaInicial
          AND g.FECHORAFI <= @FechaFinal

        UNION ALL

        -- BLOQUE 2: CITAS ASIGNADAS
        SELECT  
            A.CODAUTONU AS Consecutivo,
            A.FECHORAIN AS FechaInicioCita,
            A.FECHORAFI AS FechaFinCita,
            RTRIM(D.DESESPECI) AS DescripcionEspecialidad,
            'assigned' AS Egreso,
            'consulta_externa' AS TipoCita,
            B.IPDIRECCI AS Direccion,
            B.IPTELMOVI AS Telefono, 
            A.IPCODPACI AS Identificacion,
            RTRIM(B.IPNOMCOMP) AS Paciente,
            RTRIM(B.IPPRINOMB) AS PrimerNombre,
            RTRIM(B.IPSEGNOMB) AS SegundoNombre,
            RTRIM(B.IPPRIAPEL) AS PrimerApellido,
            RTRIM(B.IPSEGAPEL) AS SegundoApellido,
            '' AS Ingreso,
            B.IPSEXOPAC AS Sexo,
            RTRIM(C.NOMENTIDA) AS NombreEntidad,
            RTRIM(Z.DESACTMED) AS Actividad,
            CASE WHEN A.MODALIDAD = 0 THEN 'presencial' WHEN A.MODALIDAD = 1 THEN 'teleconsulta' ELSE '' END AS Modalidad,
            B.IPFECNACI AS FechaNacimiento,
            CONCAT('Consulta externa: ', A.CODIGOCON, ' - ', con.DESCRICON) AS AppointmentLocation
        FROM dbo.AGASICITA A WITH (NOLOCK)
        INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI = B.IPCODPACI
        INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON B.CODENTIDA = C.CODENTIDA
        INNER JOIN dbo.AGACTIMED Z WITH (NOLOCK) ON Z.CODACTMED = A.CODACTMED
        LEFT JOIN dbo.AGCONSULT con WITH (NOLOCK) ON A.CODIGOCON = con.CODIGOCON 
        LEFT JOIN dbo.INESPECIA D WITH (NOLOCK) ON A.CODESPECI = D.CODESPECI
        WHERE @EsConsultaAsistida = 0
          AND A.CODCENATE = @CentroAtencion
          AND A.CODESTCIT = '0'
          AND A.TIPSOLICITU = 1
          AND A.CODPROSAL = @Profesional
          AND A.FECHORAIN >= @FechaInicial
          AND A.FECHORAIN <= @FechaFinal

        UNION ALL

        -- BLOQUE 3: CONSULTAS SIN CITA
        SELECT  
            0 AS Consecutivo,
            A.IPFECHCIT AS FechaInicioCita,
            A.FECAUSENT AS FechaFinCita,
            RTRIM(E.DESESPECI) AS DescripcionEspecialidad,
            CASE A.CONESTADO WHEN '6' THEN 'attended' ELSE 'waiting' END AS Egreso,
            'consulta_externa' AS TipoCita,
            B.IPDIRECCI AS Direccion,
            B.IPTELMOVI AS Telefono, 
            A.IPCODPACI AS Identificacion,
            RTRIM(B.IPNOMCOMP) AS Paciente,
            RTRIM(B.IPPRINOMB) AS PrimerNombre,
            RTRIM(B.IPSEGNOMB) AS SegundoNombre,
            RTRIM(B.IPPRIAPEL) AS PrimerApellido,
            RTRIM(B.IPSEGAPEL) AS SegundoApellido,
            D.NUMINGRES AS Ingreso,
            B.IPSEXOPAC AS Sexo,
            RTRIM(C.NOMENTIDA) AS NombreEntidad,
            NULL AS Actividad,
            NULL AS Modalidad,
            B.IPFECNACI AS FechaNacimiento,
            CONCAT('Consulta externa: ', A.CodigoConsultorio, ' - ', con.DESCRICON) AS AppointmentLocation
        FROM dbo.ADCONCOEX A WITH (NOLOCK)
        INNER JOIN dbo.INPACIENT B WITH (NOLOCK) ON A.IPCODPACI = B.IPCODPACI
        INNER JOIN dbo.INENTIDAD C WITH (NOLOCK) ON A.CODENTIDA = C.CODENTIDA
        INNER JOIN dbo.ADINGRESO D WITH (NOLOCK) ON A.NUMINGRES = D.NUMINGRES
        LEFT JOIN dbo.INESPECIA E WITH (NOLOCK) ON A.CODESPECI = E.CODESPECI
        LEFT JOIN dbo.AGCONSULT con WITH (NOLOCK) ON A.CodigoConsultorio = con.CODIGOCON
        WHERE @EsConsultaAsistida = 0
          AND A.CODCENATE = @CentroAtencion
          AND A.CONESTADO IN ('1','4','5','6')
          AND A.CODPROSAL = @Profesional
          AND A.IPFECHCIT >= @FechaInicial
          AND A.IPFECHCIT <= @FechaFinal
          AND A.NUMCONCIT IS NULL
          AND A.CODTIPCON IS NOT NULL

        UNION ALL

        -- BLOQUE 4: CIRUGÍAS PROGRAMADAS
        SELECT  
            A.CODAUTONU AS Consecutivo,
            A.FECHORAIN AS FechaInicioCita,
            A.FECHORAFI AS FechaFinCita,
            RTRIM(ISNULL(G.DESESPECI, '')) AS DescripcionEspecialidad,
            'scheduled_surgery' AS Egreso,
            'cirugia_programada' AS TipoCita,
            C.IPDIRECCI AS Direccion,
            C.IPTELMOVI AS Telefono, 
            A.IPCODPACI AS Identificacion,
            RTRIM(C.IPNOMCOMP) AS Paciente,
            RTRIM(C.IPPRINOMB) AS PrimerNombre,
            RTRIM(C.IPSEGNOMB) AS SegundoNombre,
            RTRIM(C.IPPRIAPEL) AS PrimerApellido,
            RTRIM(C.IPSEGAPEL) AS SegundoApellido,
            CASE A.ORIGENQX 
                WHEN 1 THEN A.NUMINGRES 
                WHEN 2 THEN 
                    CASE 
                        WHEN A.AUTOHCORDPROQ IS NULL 
                             THEN (SELECT H.NUMINGRES FROM HCORDPRON H WHERE H.AUTO = A.AUTOHCORDPROQ)
                        ELSE (SELECT H.NUMINGRES FROM HCORDPROQ H WHERE H.AUTO = A.AUTOHCORDPROQ)
                    END 
            END AS Ingreso,
            C.IPSEXOPAC AS Sexo,
            RTRIM(EN.NOMENTIDA) AS NombreEntidad,
            RTRIM(E.DESSERIPS) AS Actividad,
            NULL AS Modalidad,
            C.IPFECNACI AS FechaNacimiento,
            'Cirugía programada: ' + RTRIM(B.CODIGSALA) + ' - ' + RTRIM(B.DESCRIPSAL) AS AppointmentLocation
        FROM AGEPROGQX A WITH (NOLOCK)
        INNER JOIN AGENSALAC B WITH (NOLOCK) ON A.AGENSALAC = B.CODCONCEC 
        INNER JOIN INPACIENT C WITH (NOLOCK) ON A.IPCODPACI = C.IPCODPACI
        INNER JOIN INCUPSIPS  E WITH (NOLOCK) ON A.CODSERIPS = E.CODSERIPS
        LEFT JOIN INESPECIA G WITH (NOLOCK) ON A.CODESPECI = G.CODESPECI
        INNER JOIN ADCENATEN CE WITH (NOLOCK) ON CE.CODCENATE = A.CODCENATE
        INNER JOIN INENTIDAD EN WITH (NOLOCK) ON C.CODENTIDA = EN.CODENTIDA
        WHERE A.CODESTPQX <> 6
          AND ((@EsConsultaAsistida = 0 AND A.CODPROSAL = @Profesional) 
               OR (@EsConsultaAsistida = 1 AND A.CODPROSAL = @ProfesionalConAsistida))
          AND A.CODCENATE = @CentroAtencion
          AND A.FECHORAIN >= @FechaInicial
          AND A.FECHORAFI <= @FechaFinal
    )
    SELECT *
    FROM CTE_Resultados
    ORDER BY FechaInicioCita DESC, Consecutivo DESC
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes pendientes o atendidos en consulta externa para un profesional de salud, centro de atención y rango de fechas determinados, con soporte de paginación. Integra hasta cuatro escenarios distintos: consultas con cita atendidas o en espera (desde ADCONCOEX y AGASICITA), citas asignadas aún no iniciadas (desde AGASICITA), consultas sin cita previa y cirugías programadas; consolidando en cada caso datos del paciente (cédula, nombre, dirección, teléfono, fecha de nacimiento desde INPACIENT), la entidad aseguradora o pagadora (desde INENTIDAD), la especialidad médica (desde INESPECIA), la actividad o tipo de consulta agendada (desde AGACTIMED y AGCONSULT), y el estado del encuentro (en espera, atendido, asignado, cirugía programada). Su propósito principal es alimentar la agenda de consulta externa en el módulo EMR/IA, permitiendo al médico o al sistema de inteligencia artificial conocer qué pacientes tiene pendientes de atención, con su información clínica y administrativa completa, incluyendo modalidad presencial o teleconsulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesPendientesConsultaExterna_EMR_IA';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesPendientesConsultaExterna_EMR_IA';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista paginada y unificada de pacientes pendientes/atendidos en consulta externa y cirugías programadas para un profesional, centro y rango de fechas, consolidando datos demográficos, entidad, especialidad y modalidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR_IA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@Page por debajo de 1 se normaliza a 1; @PageSize por debajo de 1 se normaliza a 20; Se requiere @CentroAtencion, @Profesional y rango @FechaInicial/@FechaFinal para filtrar; Si @EsConsultaAsistida = 1 se utiliza @ProfesionalConAsistida en lugar de @Profesional (solo aplica al bloque de cirugías)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR_IA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se consideran consultas externas con TIPSOLICITU = 1; Las consultas atendidas/en espera y sin cita filtran por CONESTADO IN (''1'',''4'',''5'',''6''); Las citas asignadas requieren CODESTCIT = ''0'' (estado pendiente/asignada); Las cirugías excluyen CODESTPQX = 6 (estado cancelado/finalizado); Todas las consultas se etiquetan con TipoCita = ''consulta_externa'' y las cirugías con ''cirugia_programada''; Las consultas sin cita no reportan Actividad ni Modalidad (NULL); El procedimiento es de sólo lectura (todas las tablas con NOLOCK, sin DML); El filtro por centro de atención (CODCENATE) aplica a los cuatro bloques', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR_IA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Consulta externa; Cita médica asignada; Consulta sin cita; Cirugía programada; Paciente; Especialidad médica; Entidad aseguradora/pagadora; Profesional de la salud; Centro de atención; Consultorio; Modalidad presencial / teleconsulta; Consulta asistida; Estado de cita (asignada, en espera, atendida); Ingreso/admisión; Sala de cirugía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR_IA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] CTE_Resultados: Devuelve resultset paginado (OFFSET/FETCH) ordenado por FechaInicioCita DESC, Consecutivo DESC, uniendo cuatro bloques: consultas atendidas/en espera, citas asignadas, consultas sin cita y cirugías programadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR_IA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @EsConsultaAsistida = 0 → Se ejecutan los bloques 1, 2 y 3 (consultas externas con/sin cita y citas asignadas) filtrando por @Profesional else Se omiten los bloques 1-3; sólo aporta filas el bloque 4 (cirugías) usando @ProfesionalConAsistida; si A.CONESTADO = ''6'' → Estado de egreso se reporta como ''attended'' else Se reporta como ''waiting''; si G.MODALIDAD = 0 / = 1 / otro → Modalidad se etiqueta como ''presencial'' / ''teleconsulta'' / cadena vacía; si En cirugías: A.ORIGENQX = 1 → Ingreso se toma directamente de AGEPROGQX.NUMINGRES else Si ORIGENQX = 2 y AUTOHCORDPROQ IS NULL se busca NUMINGRES en HCORDPRON; en caso contrario se busca en HCORDPROQ; si A.NUMCONCIT IS NULL AND A.CODTIPCON IS NOT NULL (bloque 3) → Se considera consulta sin cita previa y se incluye en el resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR_IA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONCOEX; dbo.AGASICITA; dbo.INPACIENT; dbo.INENTIDAD; dbo.AGACTIMED; dbo.AGCONSULT; dbo.INESPECIA; dbo.ADINGRESO; dbo.AGEPROGQX; dbo.AGENSALAC; dbo.INCUPSIPS; dbo.ADCENATEN; dbo.HCORDPRON; dbo.HCORDPROQ', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR_IA';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExterna_EMR_IA';
-- GO
