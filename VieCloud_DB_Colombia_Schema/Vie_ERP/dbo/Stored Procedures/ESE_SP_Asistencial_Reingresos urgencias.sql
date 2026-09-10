CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_Reingresos urgencias] @FechaIni DATETIME, 
                                                                @FechaFin DATETIME
AS
     SELECT CASE PAC.IPTIPODOC
                WHEN 1
                THEN 'CC'
                WHEN 2
                THEN 'CE'
                WHEN 3
                THEN 'TI'
                WHEN 4
                THEN 'RC '
                WHEN 5
                THEN 'PA'
                WHEN 6
                THEN 'AS'
                WHEN 7
                THEN 'MS'
                WHEN 8
                THEN 'NUIP'
                WHEN 12
                THEN 'PEP'
            END AS TIPODOC, 
            RTRIM(ING.IPCODPACI) AS PACNUMDOC, 
            RTRIM(PAC.IPPRINOMB) AS PACPRINOM, 
            RTRIM(PAC.IPSEGNOMB) AS PACSEGNOM, 
            RTRIM(PAC.IPPRIAPEL) AS PACPRIAPE, 
            RTRIM(PAC.IPSEGAPEL) AS PACSEGAPE, 
            ING.NUMINGRES AS 'AINCONSEC', 
            HC.FECHISPAC AS 'HCFECFOL', 
            ING.CODCENATE AS 'ACACODIGO', 
            RTRIM(cen.NOMCENATE) AS 'ACANOMBRE', 
            HC.CODDIAGNO AS 'DIACODIGO', 
            RTRIM(DIAG.NOMDIAGNO) AS Diagnostico
     FROM.ADINGRESO AS ING WITH(NOLOCK)
         INNER JOIN.HCHISPACA AS HC WITH(NOLOCK) ON ING.NUMINGRES = HC.NUMINGRES
                                                    AND ING.IPCODPACI = HC.IPCODPACI
         INNER JOIN.ADCENATEN AS Cen WITH(NOLOCK) ON Cen.CODCENATE = ING.CODCENATE
         INNER JOIN.INPACIENT AS PAC WITH(NOLOCK) ON ING.IPCODPACI = PAC.IPCODPACI
         INNER JOIN.INUNIFUNC AS UNI WITH(NOLOCK) ON UNI.UFUCODIGO = HC.UFUCODIGO
         INNER JOIN.INUBICACI AS UBI WITH(NOLOCK) ON PAC.AUUBICACI = UBI.AUUBICACI
         INNER JOIN.INDIAGNOS AS DIAG WITH(NOLOCK) ON HC.CODDIAGNO = DIAG.CODDIAGNO
         INNER JOIN.INPROFSAL AS S WITH(NOLOCK) ON S.CODPROSAL = hc.CODPROSAL
         INNER JOIN.INENTIDAD AS ENT WITH(NOLOCK) ON ING.CODENTIDA = ENT.CODENTIDA
         LEFT OUTER JOIN.PRMODELOHC AS MO WITH(NOLOCK) ON MO.ID = HC.IDMODELOHC
         LEFT OUTER JOIN.HCRIESGOSP AS RIE WITH(NOLOCK) ON RIE.NUMINGRCES = ING.NUMINGRES
     WHERE CAST(HC.FECHISPAC AS DATE) BETWEEN @FechaIni AND @FechaFin
           AND ING.IESTADOIN <> 'A'
           AND UNI.UFUTIPUNI = 1;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de reingresos a urgencias: dado un rango de fechas, identifica todos los pacientes que generaron una nota o folio de historia clínica en unidades funcionales de urgencias (UFUTIPUNI = 1), excluyendo ingresos anulados. Combina datos de ingresos (ADINGRESO), historias clínicas (HCHISPACA), información demográfica del paciente (INPACIENT), centro de atención (ADCENATEN), diagnóstico CIE-10 (INDIAGNOS), profesional tratante (INPROFSAL) y entidad aseguradora o pagadora (INENTIDAD) para entregar un listado consolidado con tipo y número de documento del paciente, nombre completo, número de ingreso, fecha del folio clínico, sede de atención y diagnóstico. Se utiliza para monitorear reingresos a urgencias en un período determinado y apoyar indicadores de calidad asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Reingresos urgencias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Reingresos urgencias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los registros de atención en urgencias dentro de un rango de fechas, mostrando datos del paciente, ingreso, diagnóstico y centro de atención, para análisis de reingresos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresos urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas debe estar definido y ser válido para filtrar la fecha de la historia clínica.; Las tablas maestras de pacientes, diagnósticos, entidades, profesionales, unidades funcionales y centros de atención deben tener las claves referenciadas por el ingreso/historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresos urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye ingresos con estado ''A'' (anulados).; Solo considera atenciones cuya unidad funcional sea de tipo 1 (urgencias).; Solo retorna registros con diagnóstico, centro de atención, profesional, entidad y ubicación válidos (INNER JOIN obligatorio).; El filtro de fechas se aplica sobre la fecha de la historia clínica truncada a DATE.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresos urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Historia clínica; Urgencias; Diagnóstico; Centro de atención; Unidad funcional; Tipo de documento de identidad; Reingresos; Profesional de salud; Entidad (asegurador)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresos urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas cuando CAST(HC.FECHISPAC AS DATE) está entre las fechas indicadas, el ingreso no está en estado ''A'' (anulado) y la unidad funcional es de tipo 1 (urgencias).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresos urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PAC.IPTIPODOC en {1..8,12} → Mapea el código numérico de tipo de documento a su sigla (CC, CE, TI, RC, PA, AS, MS, NUIP, PEP). else Devuelve NULL como tipo de documento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresos urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.HCHISPACA; dbo.ADCENATEN; dbo.INPACIENT; dbo.INUNIFUNC; dbo.INUBICACI; dbo.INDIAGNOS; dbo.INPROFSAL; dbo.INENTIDAD; dbo.PRMODELOHC; dbo.HCRIESGOSP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresos urgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresos urgencias';
-- GO
