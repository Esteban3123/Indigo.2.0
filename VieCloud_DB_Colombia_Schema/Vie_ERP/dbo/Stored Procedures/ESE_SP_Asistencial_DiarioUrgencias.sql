CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_DiarioUrgencias] @FechaIni DATETIME, 
                                                           @FechaFin DATETIME, 
                                                           @Centro   VARCHAR(10)
AS
     SELECT DISTINCT 
            CASE
                WHEN ADCO.IPFECLLEGA IS NULL
                THEN
     (
         SELECT MIN(G.FECINIATE) AS FechaAte
         FROM.HCURGING1 AS G
         WHERE G.NUMINGRES = ING.NUMINGRES
     ) - 5
                ELSE ADCO.IPFECLLEGA
            END AS FechaControl,
            CASE
                WHEN AD.FECHINITR IS NULL
                THEN
     (
         SELECT MIN(G.FECINIATE) AS FechaAte
         FROM.HCURGING1 AS G
         WHERE G.NUMINGRES = ING.NUMINGRES
     )
                ELSE AD.FECHINITR
            END AS FechaTriage, 
            AD.TRIAGECLA AS ClasificacionTriage, 
     (
         SELECT MIN(G.FECINIATE) AS FechaAte
         FROM.HCURGING1 AS G
         WHERE G.NUMINGRES = ING.NUMINGRES
     ) AS FechaConsulta, 
            ING.NUMINGRES AS Ingreso,
            CASE
                WHEN ING.ICAUSAING = '1'
                THEN '1. Heridos en combate'
                WHEN ING.ICAUSAING = '2'
                THEN '2. Enfermedad profesional'
                WHEN ING.ICAUSAING = '3'
                THEN '3. Enfermedad general adulto'
                WHEN ING.ICAUSAING = '4'
                THEN '4. Enfermedad general pediatria'
                WHEN ING.ICAUSAING = '5'
                THEN '5. Odontología'
                WHEN ING.ICAUSAING = '6'
                THEN '6. Accidente de transito'
                WHEN ING.ICAUSAING = '7'
                THEN '7. Catastrofe/Fisalud'
                WHEN ING.ICAUSAING = '8'
                THEN '8. Quemados'
                WHEN ING.ICAUSAING = '9'
                THEN '9. Maternidad'
                WHEN ING.ICAUSAING = '10'
                THEN '10. Accidente Laboral'
                WHEN ING.ICAUSAING = '11'
                THEN '11. Cirugia Programada'
            END AS 'CausaDelIngreso',
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
            ING.IPCODPACI AS Identificacion, 
            P.IPNOMCOMP AS NombrePaciente, 
            CONVERT(VARCHAR(10), P.IPFECNACI, 105) AS FechaNacimiento, 
            [dbo].[Edad](P.IPFECNACI, [Common].[GETDATE]()) AS Edad,
            CASE P.IPSEXOPAC
                WHEN 1
                THEN 'Masculino'
                WHEN 2
                THEN 'Femenino'
            END AS 'Genero', 
            ENT.CODENTIDA + ' - ' + ENt.NOMENTIDA AS 'Entidad', 
            ING.CODDIAING + ' - ' + D.NOMDIAGNO AS DiagnosticoIngreso, 
            ING.CODDIAEGR + '-' + d2.NOMDIAGNO AS DiagnosticoEgreso, 
            RTRIM(ING.CODCENATE) + ' - ' + RTRIM(CEN.NOMCENATE) AS CentroAtencion, 
            RTRIM(ING.UFUCODIGO) + ' - ' + RTRIM(UNI.UFUDESCRI) AS UnidadFuncional, 
            P.IPDIRECCI AS Direccion,
            CASE
                WHEN P.IPTELMOVI = ''
                THEN P.IPTELEFON
                ELSE P.IPTELMOVI
            END AS Telefono, 
            UBI.UBINOMBRE AS Barrio, 
     (
         SELECT TOP (1) RTRIM(G.CODPROSAL) + ' - ' + RTRIM(S.NOMMEDICO)
         FROM.HCURGING1 AS G
             INNER JOIN.INPROFSAL AS S ON S.CODPROSAL = G.CODPROSAL
         WHERE G.NUMINGRES = ING.NUMINGRES
         ORDER BY G.NUMEFOLIO
     ) AS Medico
     FROM.ADINGRESO AS ING
         INNER JOIN.HCURGING1 AS URG ON URG.NUMINGRES = ING.NUMINGRES
         INNER JOIN.INPACIENT P ON P.IPCODPACI = ING.IPCODPACI
         INNER JOIN.ADCENATEN AS CEN ON ING.CODCENATE = CEN.CODCENATE
         INNER JOIN.INUNIFUNC AS UNI ON UNI.UFUCODIGO = ING.UFUCODIGO
         INNER JOIN.INUBICACI AS UBI ON UBI.AUUBICACI = P.AUUBICACI
         INNER JOIN.INDIAGNOP AS DIAG ON ING.NUMINGRES = DIAG.NUMINGRES
         INNER JOIN.INDIAGNOS D ON ING.CODDIAING = D.CODDIAGNO
         INNER JOIN.INDIAGNOS D2 ON ING.CODDIAEGR = D2.CODDIAGNO
         INNER JOIN.INDIAGNOS D3 ON DIAG.CODDIAGNO = D3.CODDIAGNO
         INNER JOIN.INENTIDAD ENT ON ING.CODENTIDA = ENT.CODENTIDA
         LEFT OUTER JOIN.ADTRIAGEU AS AD ON AD.NUMINGRES = ING.NUMINGRES
         LEFT OUTER JOIN.ADCONTURG AS ADCO ON AD.CODCONCEC = ADCO.CODCONCEC
     WHERE DIAG.CODDIAPRI = 1
           AND UNI.UFUTIPUNI = 1
           AND AD.TRIAGECLA IS NOT NULL
           AND CAST(ING.IFECHAING AS DATE) BETWEEN @FechaIni AND @FechaFin
           AND ING.CODCENATE = @Centro;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte diario de atenciones en urgencias para un rango de fechas y centro de atención indicados. Consolida información del ingreso del paciente (ADINGRESO), su historia clínica de urgencias (HCURGING1), el triage registrado (ADTRIAGEU), diagnósticos de ingreso y egreso (CIE-10), entidad aseguradora, unidad funcional y datos sociodemográficos del paciente (cédula, nombre, fecha de nacimiento, edad, género, dirección, teléfono, barrio). Para cada atención muestra: fecha de llegada o de triage, clasificación de triage, fecha de consulta, causa del ingreso, tipo y número de identificación del paciente, diagnósticos, centro y unidad funcional, entidad pagadora, y médico tratante. Se utiliza como insumo de seguimiento operativo y estadístico del servicio de urgencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_DiarioUrgencias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_DiarioUrgencias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte diario de atenciones de urgencias por centro y rango de fechas, incluyendo datos del paciente, triage, diagnósticos, médico tratante y tiempos clave (llegada, triage y consulta).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_DiarioUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe tener un diagnóstico marcado como principal (CODDIAPRI = 1).; La unidad funcional del ingreso debe ser de tipo Urgencias (UFUTIPUNI = 1).; El ingreso debe tener clasificación de triage asignada (TRIAGECLA NOT NULL).; La fecha de ingreso (IFECHAING) debe estar dentro del rango [@FechaIni, @FechaFin].; El ingreso debe pertenecer al centro de atención indicado en @Centro.; Deben existir registros relacionados en HCURGING1, INPACIENT, ADCENATEN, INUNIFUNC, INUBICACI, INDIAGNOP, INDIAGNOS y INENTIDAD para el ingreso (joins INNER).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_DiarioUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan ingresos cuya unidad funcional es de Urgencias (UFUTIPUNI=1).; Solo se reportan ingresos con diagnóstico principal definido (CODDIAPRI=1) y triage clasificado.; El médico tratante reportado es el del primer folio de atención de urgencias del ingreso (TOP 1 ordenado por NUMEFOLIO).; La fecha de consulta corresponde siempre al inicio mínimo de atención registrado en HCURGING1 para el ingreso.; Cuando no hay registro de llegada en ADCONTURG, la fecha de control se calcula desfasando la fecha mínima de atención (FechaConsulta - 5).; El reporte está acotado a un único centro de atención por ejecución.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_DiarioUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ingreso hospitalario; Urgencias; Triage; Clasificación de triage; Causa de ingreso; Diagnóstico de ingreso; Diagnóstico de egreso; Diagnóstico principal; Paciente; Tipo de identificación; Entidad (asegurador/EPS); Centro de atención; Unidad funcional; Profesional de salud / Médico tratante; Fecha de llegada; Fecha de inicio de triage; Fecha de consulta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_DiarioUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto de filas DISTINCT con el detalle de atenciones de urgencias filtradas por centro, rango de fechas, diagnóstico principal, unidad de urgencias y triage no nulo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_DiarioUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ADCO.IPFECLLEGA IS NULL → FechaControl = MIN(HCURGING1.FECINIATE) del ingreso menos 5 (unidades de tiempo del tipo del campo). else FechaControl = ADCO.IPFECLLEGA.; si AD.FECHINITR IS NULL → FechaTriage = MIN(HCURGING1.FECINIATE) del ingreso. else FechaTriage = AD.FECHINITR.; si ING.ICAUSAING ∈ {''1''..''11''} → Mapea la causa del ingreso a su descripción textual (Heridos en combate, Enfermedad profesional, Enfermedad general adulto/pediatría, Odontología, Accidente de tránsito, Catástrofe/Fisalud, Quemados, Maternidad, Accidente Laboral, Cirugía Programada).; si P.IPTIPODOC ∈ {1..8} → Mapea el tipo de documento (CC, CE, TI, RC, Pasaporte, Adulto/Menor sin identificación, NUIP).; si P.IPSEXOPAC = 1 / 2 → Género = Masculino / Femenino.; si P.IPTELMOVI = '''' → Teléfono mostrado = IPTELEFON (fijo). else Teléfono mostrado = IPTELMOVI (móvil).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_DiarioUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCURGING1; dbo.INPACIENT; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INUBICACI; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.INENTIDAD; dbo.ADTRIAGEU; dbo.ADCONTURG; dbo.INPROFSAL; dbo.Edad; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_DiarioUrgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_DiarioUrgencias';
-- GO
