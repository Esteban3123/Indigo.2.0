CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_VigilaciaEpidemiologica] @FechaIni DATETIME, 
                                                                   @FechaFin DATETIME
AS
     SELECT DISTINCT 
            ING.IFECHAING AS Fecha, 
            ING.NUMINGRES AS Ingreso,
            CASE
                WHEN ING.TIPOINGRE = 1
                THEN 'Ambulatorio'
                ELSE 'hospitalario'
            END AS 'TipodeIngreso',
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
            ING.IPCODPACI AS 'Identificacion',
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
            UBI.UBINOMBRE AS Barrio
     FROM.HCURGING1 AS HIS
         INNER JOIN.ADINGRESO AS ING ON ING.NUMINGRES = HIS.NUMINGRES
         INNER JOIN.ADCENATEN AS CEN ON ING.CODCENATE = CEN.CODCENATE
         INNER JOIN.INUNIFUNC AS UNI ON UNI.UFUCODIGO = ING.UFUCODIGO
         INNER JOIN.INPACIENT P ON P.IPCODPACI = ING.IPCODPACI
         INNER JOIN.INUBICACI AS UBI ON UBI.AUUBICACI = P.AUUBICACI
         INNER JOIN.INDIAGNOP AS DIAG ON ING.NUMINGRES = DIAG.NUMINGRES
         INNER JOIN.INDIAGNOS D ON ING.CODDIAING = D.CODDIAGNO
         INNER JOIN.INDIAGNOS D2 ON ING.CODDIAEGR = D2.CODDIAGNO
         INNER JOIN.INDIAGNOS D3 ON DIAG.CODDIAGNO = D3.CODDIAGNO
         INNER JOIN.INENTIDAD ENT ON ING.CODENTIDA = ENT.CODENTIDA
         LEFT OUTER JOIN.ADCONCOEX AS ce ON ING.NUMINGRES = ce.NUMINGRES
                                            AND ING.IPCODPACI = ce.IPCODPACI
                                            AND ce.CONESTADO = '3'
         LEFT OUTER JOIN.AGASICITA AS ci ON ce.NUMCONCIT = ci.CODAUTONU
         LEFT OUTER JOIN.AGACTIMED ACT ON ci.CODACTMED = ACT.CODACTMED
     WHERE DIAG.CODDIAPRI = 1
           AND ING.IFECHAING >= @FechaIni
           AND ING.IFECHAING <= @FechaFin;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de vigilancia epidemiológica que consolida, para un rango de fechas dado, los ingresos de urgencias con sus datos clínicos, demográficos y administrativos. Cruza historia clínica de urgencias, admisiones, diagnósticos CIE-10 (ingreso y egreso), paciente, entidad aseguradora, centro de atención y unidad funcional para construir un reporte nominal de casos. Filtra únicamente el diagnóstico principal (CODDIAPRI = 1) y clasifica cada caso por tipo de ingreso (ambulatorio u hospitalario), causa del ingreso (accidente, maternidad, enfermedad general, etc.), género, tipo de documento de identidad, barrio de residencia y teléfono de contacto. Se usa para notificación y seguimiento de eventos de interés en salud pública, trazabilidad epidemiológica y reportes institucionales de morbilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_VigilaciaEpidemiologica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_VigilaciaEpidemiologica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un listado de ingresos asistenciales en un rango de fechas, con datos demográficos del paciente, diagnósticos de ingreso/egreso y ubicación, para fines de vigilancia epidemiológica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_VigilaciaEpidemiologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (inicio y fin) debe estar definido y aplicarse sobre la fecha de ingreso.; El ingreso debe tener un diagnóstico marcado como principal (CODDIAPRI = 1) en la tabla de diagnósticos del ingreso.; El paciente debe tener registro maestro y ubicación (barrio) asociada.; El ingreso debe tener entidad, centro de atención, unidad funcional y diagnósticos de ingreso y egreso registrados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_VigilaciaEpidemiologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ingresos asociados a una historia clínica de urgencias (HCURGING1).; Solo considera diagnósticos marcados como principales del ingreso.; Las concesiones/citas externas se enlazan únicamente cuando el estado de la concesión es ''3'' (LEFT JOIN condicionado).; La edad se calcula a la fecha actual del sistema mediante la función [dbo].[Edad] usando [Common].[GETDATE]().; El filtro de fechas es inclusivo en ambos extremos (>= @FechaIni y <= @FechaFin).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_VigilaciaEpidemiologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Vigilancia epidemiológica; Ingreso asistencial (ambulatorio/hospitalario); Causa de ingreso; Paciente; Tipo de identificación; Diagnóstico de ingreso y egreso; Entidad (asegurador); Centro de atención; Unidad funcional; Historia clínica de urgencias; Concesión/cita externa; Ubicación geográfica (barrio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_VigilaciaEpidemiologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve filas distintas de ingresos cuyo diagnóstico principal está marcado (CODDIAPRI=1) y cuya fecha de ingreso está entre @FechaIni y @FechaFin, enriquecidas con datos del paciente, entidad, diagnósticos, centro y unidad funcional.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_VigilaciaEpidemiologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOINGRE = 1 → Clasifica el ingreso como ''Ambulatorio'' else Clasifica el ingreso como ''hospitalario''; si ICAUSAING entre ''1'' y ''11'' → Mapea el código de causa de ingreso a su descripción textual (Heridos en combate, Enfermedad profesional, Enfermedad general adulto/pediátrica, Odontología, Accidente de tránsito, Catástrofe/Fisalud, Quemados, Maternidad, Accidente Laboral, Cirugía Programada); si IPTIPODOC entre 1 y 8 → Traduce el tipo de documento del paciente (CC, CE, TI, RC, Pasaporte, Adulto/Menor sin identificación, NUIP); si IPSEXOPAC = 1 → Reporta género ''Masculino'' else Si IPSEXOPAC = 2, reporta ''Femenino''; si IPTELMOVI vacío → Usa el teléfono fijo (IPTELEFON) como teléfono de contacto else Usa el teléfono móvil (IPTELMOVI)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_VigilaciaEpidemiologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_VigilaciaEpidemiologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCURGING1; dbo.ADINGRESO; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPACIENT; dbo.INUBICACI; dbo.INDIAGNOP; dbo.INDIAGNOS; dbo.INENTIDAD; dbo.ADCONCOEX; dbo.AGASICITA; dbo.AGACTIMED', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_VigilaciaEpidemiologica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_VigilaciaEpidemiologica';
-- GO
