CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_Reingresosurgencias] @FechaIni DATETIME, 
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte de reingresos a urgencias en un rango de fechas determinado. Consolida información de ingresos (ADINGRESO), folios de historia clínica (HCHISPACA) y datos maestros del paciente (INPACIENT) para listar los pacientes atendidos en unidades funcionales de tipo urgencias (UFUTIPUNI = 1), excluyendo ingresos anulados. Por cada registro devuelve el tipo y número de documento del paciente, nombre completo, número de ingreso, fecha del folio clínico, sede de atención, código y nombre del diagnóstico CIE-10, cruzando además con entidad aseguradora (INENTIDAD), profesional tratante (INPROFSAL) y ubicación del paciente (INUBICACI). Se utiliza para monitoreo asistencial y seguimiento de pacientes que regresan al servicio de urgencias dentro de un período dado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Reingresosurgencias';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_Reingresosurgencias';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista atenciones de urgencias en un rango de fechas para análisis de reingresos, devolviendo datos demográficos del paciente, ingreso, diagnóstico y centro de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresosurgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de inicio y fin deben recibirse como parámetros válidos.; Debe existir correspondencia entre ingreso, historia clínica, paciente, unidad funcional, centro de atención, diagnóstico, profesional y entidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresosurgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran ingresos cuyo IESTADOIN sea distinto de ''A'' (excluye anulados).; Solo se incluyen registros asociados a unidades funcionales con UFUTIPUNI = 1 (urgencias).; El filtro por fecha aplica sobre la fecha de la historia clínica truncada a día.; Las consultas usan WITH(NOLOCK), aceptando lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresosurgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de documento de identidad; Ingreso asistencial; Historia clínica; Centro de atención; Unidad funcional de urgencias; Diagnóstico; Profesional de salud; Entidad (asegurador); Reingreso a urgencias; Riesgos del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresosurgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve registros donde CAST(HC.FECHISPAC AS DATE) BETWEEN @FechaIni AND @FechaFin, el ingreso no está en estado ''A'' (anulado) y la unidad funcional es de tipo 1 (urgencias).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresosurgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PAC.IPTIPODOC ∈ {1..8,12} → Mapea el código numérico a etiqueta textual del tipo de documento (CC, CE, TI, RC, PA, AS, MS, NUIP, PEP). else Devuelve NULL como tipo de documento.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresosurgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'ADINGRESO; HCHISPACA; ADCENATEN; INPACIENT; INUNIFUNC; INUBICACI; INDIAGNOS; INPROFSAL; INENTIDAD; PRMODELOHC; HCRIESGOSP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresosurgencias';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_Reingresosurgencias';
-- GO
