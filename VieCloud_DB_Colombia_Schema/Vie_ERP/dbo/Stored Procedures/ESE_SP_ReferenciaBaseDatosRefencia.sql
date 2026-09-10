CREATE PROCEDURE [dbo].[ESE_SP_ReferenciaBaseDatosRefencia] @FechaIni DATETIME, 
                                                           @FechaFin DATETIME
AS
     SELECT ING.NUMINGRES AS Ingreso, 
            RTRIM(R.CODCENATE) + ' - ' + RTRIM(CEN.NOMCENATE) AS Centro, 
            RTRIM(R.UFUCODIGO) + ' - ' + RTRIM(UNI.UFUDESCRI) AS UnidadRemision,
            CASE
                WHEN UNI.UFUTIPUNI = 1
                THEN 'Urgencias'
                WHEN UNI.UFUTIPUNI = 2
                THEN 'Observacion/Hospitalizacion'
                WHEN UNI.UFUTIPUNI = 15
                THEN 'Consulta Externa'
                ELSE 'Otros'
            END AS TipoUnidad,
            CASE
                WHEN R.ESTADO = 1
                THEN 'Solicitado'
                WHEN R.ESTADO = 2
                THEN 'Pendinte o Gestionando'
                WHEN R.ESTADO = 3
                THEN 'Aceptado con pediente de salida'
                WHEN R.ESTADO = 4
                THEN 'Suspendido'
                WHEN R.ESTADO = 5
                THEN 'Salio'
                WHEN R.ESTADO = 6
                THEN 'Solicitud con Pertinencia'
            END EstadoRemision, 
            NR.Nombre AS MotivoSuspension, 
            R.JUSTSUSPEN AS Justificacion, 
            R.IPCODPACI AS Identificacion,
            CASE P.IPTIPODOC
                WHEN 1
                THEN 'Cédula de Ciudadanía'
                WHEN 2
                THEN 'Cédula de Extranjería '
                WHEN 3
                THEN 'Tarjeta de Identidad '
                WHEN 4
                THEN 'Registro Civil '
                WHEN 5
                THEN 'Pasporte '
                WHEN 6
                THEN 'Adulto Sin Identificación '
                WHEN 7
                THEN 'Menor Sin Identificación '
                WHEN 8
                THEN 'Número único de identificación personal'
            END AS 'TipoIdentificacion', 
            p.IPNOMCOMP AS Paciente, 
            CONVERT(VARCHAR(10), P.IPFECNACI, 105) AS FechaNacimiento, 
            [dbo].[Edad](P.IPFECNACI, [Common].[GETDATE]()) AS Edad,
            CASE P.IPSEXOPAC
                WHEN 1
                THEN 'Masculino'
                WHEN 2
                THEN 'Femenino'
            END AS 'Genero', 
            RTRIM(ING.CODENTIDA) + ' - ' + RTRIM(ENT.NOMENTIDA) AS Entidad,
            CASE
                WHEN CGR.EntityType = 1
                THEN 'EPS Contributivo'
                WHEN CGR.EntityType = 2
                THEN 'EPS Subsidiado'
                WHEN CGR.EntityType = 3
                THEN 'ET Vinculados Municipios'
                WHEN CGR.EntityType = 4
                THEN 'ET Vinculados Departamentos'
                WHEN CGR.EntityType = 5
                THEN 'ARL Riesgos Laborales'
                WHEN CGR.EntityType = 6
                THEN 'MP Medicina Prepagada'
                WHEN CGR.EntityType = 7
                THEN 'IPS Privada'
                WHEN CGR.EntityType = 8
                THEN 'IPS Publica'
                WHEN CGR.EntityType = 9
                THEN 'Regimen Especial'
                WHEN CGR.EntityType = 10
                THEN 'Accidentes de transito'
                WHEN CGR.EntityType = 11
                THEN 'Fosyga'
                WHEN CGR.EntityType = 12
                THEN 'Otros'
                WHEN CGR.EntityType = 13
                THEN 'Aseguradoras'
                WHEN CGR.EntityType = 99
                THEN 'Particulares'
            END AS Regimen, 
            SR.Nombre AS ServicioAlQueSeRemite, 
            DIAG.CODDIAGNO + ' - ' + RTRIM(D.NOMDIAGNO) AS DiagnosticoPrincipal, 
            E.DESESPECI AS EspecialidadARemitir, 
            I.DSCRIPIPS AS EntidadDondeRemite, 
            T.FECSOLICIT AS FechaSolicitudRemision, 
            IPS.FECHCONFIR AS FechaAceptacion, 
            DATEDIFF(HOUR, T.FECSOLICIT, IPS.FECHCONFIR) AS HorasEspera, 
            PRO.NOMMEDICO AS Medico
     FROM.HCREFCONP AS R WITH(NOLOCK)
         INNER JOIN.ADINGRESO AS ING WITH(NOLOCK) ON R.NUMINGRES = ING.NUMINGRES
         INNER JOIN Contract.CareGroup AS CGR WITH(NOLOCK) ON ING.GENCAREGROUP = CGR.Id
         INNER JOIN.INPACIENT AS P WITH(NOLOCK) ON R.IPCODPACI = P.IPCODPACI
         INNER JOIN.INUNIFUNC AS UNI WITH(NOLOCK) ON UNI.UFUCODIGO = R.UFUCODIGO
         INNER JOIN.ADCENATEN AS Cen WITH(NOLOCK) ON Cen.CODCENATE = R.CODCENATE
         INNER JOIN.INENTIDAD AS ENT WITH(NOLOCK) ON ING.CODENTIDA = ENT.CODENTIDA
         LEFT OUTER JOIN
     (
         SELECT T1.IPCODPACI, 
                T1.SERVIDOREM, 
                T1.FECSOLICIT, 
                T1.NUMINGRES, 
                T1.AUTO
         FROM dbo.HCREFCONT AS T1
              INNER JOIN
         (
             SELECT MIN(FECSOLICIT) AS FS, 
                    NUMINGRES, 
                    MIN(AUTO) AS AUTO
             FROM dbo.HCREFCONT
             GROUP BY NUMINGRES
         ) AS T2 ON T2.NUMINGRES = T1.NUMINGRES
                    AND T2.FS = T1.FECSOLICIT
                    AND T2.AUTO = T1.AUTO
     ) AS T ON T.IPCODPACI = R.IPCODPACI
               AND T.NUMINGRES = R.NUMINGRES
         INNER JOIN dbo.RCSERVICIOS AS SR WITH(NOLOCK) ON SR.Codigo = T.SERVIDOREM
         INNER JOIN.INDIAGNOP AS DIAG WITH(NOLOCK) ON ING.NUMINGRES = DIAG.NUMINGRES
                                                      AND ING.CODDIAEGR = DIAG.CODDIAGNO
         INNER JOIN.INDIAGNOS D WITH(NOLOCK) ON DIAG.CODDIAGNO = D.CODDIAGNO
         INNER JOIN dbo.INESPECIA AS E WITH(NOLOCK) ON E.CODESPECI = R.CODESPECI
         LEFT OUTER JOIN
     (
         SELECT DISTINCT 
                HCREFCONPID, 
                IPSACEPTADO, 
                FECHCONFIR
         FROM dbo.HCREFCONTD AS DT WITH(NOLOCK)
              INNER JOIN.HCREFCONP AS R WITH(NOLOCK) ON DT.HCREFCONPID = R.AUTO
         WHERE R.ESTADO = 5
               AND DT.IPSACEPTADO IS NOT NULL
               AND DT.FECHCONFIR IS NOT NULL
     ) AS IPS ON IPS.HCREFCONPID = R.AUTO
         LEFT OUTER JOIN dbo.ADCONTIPS AS I WITH(NOLOCK) ON I.CODIGOIPS = IPS.IPSACEPTADO
         INNER JOIN.INPROFSAL AS PRO WITH(NOLOCK) ON PRO.CODPROSAL = R.CODPROSAL
         LEFT OUTER JOIN.RCMOTNOREF AS NR WITH(NOLOCK) ON NR.Id = R.RCMOTNOREFID
     WHERE DIAG.CODDIAPRI = 1
           AND T.FECSOLICIT >= @FechaIni
           AND T.FECSOLICIT <= @FechaFin;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte detallado del proceso de referencia y contrarreferencia de pacientes para un rango de fechas dado. Consolida información del ingreso del paciente (número de ingreso, centro de atención, unidad funcional y tipo de unidad), datos demográficos del paciente (cédula, nombre, fecha de nacimiento, edad, género), entidad aseguradora y régimen de afiliación, diagnóstico principal (código CIE-10), especialidad y entidad a la que se remite, estado de la remisión (solicitado, pendiente, aceptado, suspendido, salió, con pertinencia), justificación de suspensión, fecha de solicitud de remisión, fecha de aceptación y horas de espera entre solicitud y aceptación. Cruza las tablas de remisiones (HCREFCONP, HCREFCONT, HCREFCONTD), ingresos (ADINGRESO), pacientes (INPACIENT), unidades funcionales (INUNIFUNC), centros de atención (ADCENATEN), entidades pagadoras (INENTIDAD) y grupos de contrato (CareGroup) para ofrecer una vista integral del flujo de referencia asistencial, útil para auditoría, seguimiento operativo y reportería gerencial de referencias médicas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_ReferenciaBaseDatosRefencia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_ReferenciaBaseDatosRefencia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta las solicitudes de referencia/remisión de pacientes a otras instituciones dentro de un rango de fechas, mostrando datos clínicos, administrativos, estado de la remisión y tiempos de espera hasta la aceptación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaBaseDatosRefencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de inicio y fin deben acotar el rango de FECSOLICIT de la primera solicitud por ingreso; El ingreso debe tener al menos un diagnóstico marcado como principal (CODDIAPRI = 1) coincidente con el diagnóstico de egreso; El ingreso debe estar asociado a un CareGroup, entidad, paciente, unidad funcional, centro de atención y profesional válidos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaBaseDatosRefencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Por cada NUMINGRES se toma como fecha de solicitud la mínima (MIN(FECSOLICIT)) y el AUTO mínimo asociado a esa fecha; Solo se incluyen remisiones cuyo diagnóstico está marcado como principal (CODDIAPRI = 1); Las horas de espera se calculan como diferencia en horas entre la primera fecha de solicitud y la fecha de confirmación de la IPS aceptada; La edad del paciente se calcula respecto a la fecha actual del sistema mediante Common.GETDATE(); Solo cuando la remisión finalizó (ESTADO = 5) se reporta IPS aceptada y fecha de aceptación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaBaseDatosRefencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Referencia y contrarreferencia; Remisión de pacientes; Ingreso hospitalario; Unidad funcional (Urgencias, Observación/Hospitalización, Consulta Externa); Estado de remisión (Solicitado, Gestionando, Aceptado, Suspendido, Salió, Pertinencia); Motivo de suspensión / no referencia; Tipo de identificación del paciente; Régimen de afiliación (Contributivo, Subsidiado, ARL, Prepagada, IPS, Fosyga, Particular, etc.); Diagnóstico principal; Especialidad médica; IPS aceptante; Tiempo de espera para aceptación; Profesional de salud solicitante', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaBaseDatosRefencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto de resultados con la información de remisiones cuyo diagnóstico es principal (DIAG.CODDIAPRI = 1) y cuya primera fecha de solicitud (MIN(FECSOLICIT) por NUMINGRES) está entre @FechaIni y @FechaFin', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaBaseDatosRefencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si UNI.UFUTIPUNI = 1/2/15 → Clasifica TipoUnidad como ''Urgencias'', ''Observacion/Hospitalizacion'' o ''Consulta Externa'' respectivamente else Clasifica como ''Otros''; si R.ESTADO entre 1 y 6 → Traduce el estado a etiqueta de negocio: Solicitado, Pendiente/Gestionando, Aceptado pendiente de salida, Suspendido, Salió, o Solicitud con Pertinencia; si CGR.EntityType entre 1..13 o 99 → Clasifica el régimen de la entidad (EPS Contributivo/Subsidiado, ET Vinculados, ARL, Medicina Prepagada, IPS Pública/Privada, Régimen Especial, Accidentes de tránsito, Fosyga, Aseguradoras, Particulares, Otros); si P.IPTIPODOC entre 1..8 → Traduce el tipo de documento del paciente a su descripción (CC, CE, TI, RC, Pasaporte, Adulto/Menor sin identificación, NUIP); si P.IPSEXOPAC = 1 o 2 → Clasifica género como Masculino o Femenino; si Subconsulta IPS: R.ESTADO = 5 AND IPSACEPTADO IS NOT NULL AND FECHCONFIR IS NOT NULL → Solo se considera la IPS aceptada y la fecha de confirmación cuando la remisión está en estado ''Salio'' y tiene IPS y fecha de confirmación informadas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaBaseDatosRefencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaBaseDatosRefencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREFCONP; dbo.ADINGRESO; Contract.CareGroup; dbo.INPACIENT; dbo.INUNIFUNC; dbo.ADCENATEN; dbo.INENTIDAD; dbo.HCREFCONT; dbo.RCSERVICIOS; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.INESPECIA; dbo.HCREFCONTD; dbo.ADCONTIPS; dbo.INPROFSAL; dbo.RCMOTNOREF', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaBaseDatosRefencia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_ReferenciaBaseDatosRefencia';
-- GO
