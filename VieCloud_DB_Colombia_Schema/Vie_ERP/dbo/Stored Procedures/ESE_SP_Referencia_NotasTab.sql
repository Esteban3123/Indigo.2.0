CREATE PROCEDURE [dbo].[ESE_SP_Referencia_NotasTab] @FechaIni DATETIME, 
                                                   @FechaFin DATETIME
AS
     SELECT ENT.NOMENTIDA AS 'Entidad',
            CASE
                WHEN CGR.EntityType = 1
                THEN 'Contributivo'
                WHEN CGR.EntityType = 2
                THEN 'Subsidiado'
                WHEN CGR.EntityType = 3
                THEN 'ET Vinculados Municipios - N'
                WHEN CGR.EntityType = 4
                THEN 'ET Vinculados Departamentos - N'
                WHEN CGR.EntityType = 5
                THEN 'ARL Riesgos Laborales - P'
                WHEN CGR.EntityType = 6
                THEN 'MP Medicina Prepagada - P'
                WHEN CGR.EntityType = 7
                THEN 'IPS Privada - P'
                WHEN CGR.EntityType = 8
                THEN 'IPS Publica - P'
                WHEN CGR.EntityType = 9
                THEN 'E'
                WHEN CGR.EntityType = 10
                THEN 'Accidentes de transito -P'
                WHEN CGR.EntityType = 11
                THEN 'Fosyga - P'
                WHEN CGR.EntityType = 12
                THEN 'Otros - N'
                WHEN CGR.EntityType = 13
                THEN 'Aseguradoras - P'
                WHEN CGR.EntityType = 99
                THEN 'Particulares - P'
            END AS 'Regimen', 
            ING.IFECHAING AS FechaIngreso, 
            T.FECREGIST AS FechaHistoria, 
            ING.NUMINGRES AS Ingreso, 
            ING.CODCENATE AS CodCentro, 
            RTRIM(cen.NOMCENATE) AS CentroAtencion, 
            RTRIM(PAC.IPPRINOMB) AS PrimerNombre, 
            RTRIM(PAC.IPSEGNOMB) AS SegundoNombre, 
            RTRIM(PAC.IPPRIAPEL) AS PrimerApellido, 
            RTRIM(PAC.IPSEGAPEL) AS SegundoApellido,
            CASE PAC.IPTIPODOC
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
            END AS TipoIdentificacion, 
            RTRIM(ING.IPCODPACI) AS Identificacion, 
            CAST(PAC.IPFECNACI AS DATE) AS FechaNacimiento, 
            YEAR([Common].[GETDATE]()) - YEAR(PAC.IPFECNACI) AS Edad,
            CASE PAC.IPSEXOPAC
                WHEN 1
                THEN 'M'
                WHEN 2
                THEN 'F'
            END AS Sexo, 
            PAC.IPTELMOVI AS Celular, 
            PAC.IPTELEFON AS TelefonoFijo,
            CASE PAC.IPESTADOC
                WHEN 1
                THEN 'Soltero'
                WHEN 2
                THEN 'Casado'
                WHEN 3
                THEN 'Viudo'
                WHEN 4
                THEN 'UnionLibre'
                WHEN 5
                THEN 'Separado'
            END AS EstadoCivil, 
            TITNOTENF AS Titulo, 
            NOTENFSUB AS Subjetivo, 
            NOTENFOBJ AS Objetivo, 
            NOTENFANA AS Analisis, 
            DIAG.Diagnostico1
     FROM.HCCTRNOTE AS T
         INNER JOIN.ADINGRESO AS ING ON ING.NUMINGRES = T.NUMINGRES
         INNER JOIN.INPACIENT AS PAC WITH(NOLOCK) ON T.IPCODPACI = PAC.IPCODPACI
         INNER JOIN.INENTIDAD AS ENT WITH(NOLOCK) ON ING.CODENTIDA = ENT.CODENTIDA
         INNER JOIN.ADCENATEN AS Cen WITH(NOLOCK) ON Cen.CODCENATE = ING.CODCENATE
         INNER JOIN.INPROFSAL AS S WITH(NOLOCK) ON S.CODPROSAL = T.CODPROSAL
         INNER JOIN Contract.CareGroup AS CGR WITH(NOLOCK) ON ING.GENCAREGROUP = CGR.Id
         INNER JOIN.DWHDiagnosticosTotal AS DIAG WITH(NOLOCK) ON DIAG.NUMINGRES = ING.NUMINGRES
     WHERE(CODNIVIMP = 'C')
          AND CAST(T.FECREGIST AS DATE) BETWEEN @FechaIni AND @FechaFin;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera un reporte tabular de notas de enfermería (SOAP: subjetivo, objetivo, análisis) registradas en la historia clínica, filtradas por un rango de fechas de registro. Para cada nota, integra los datos del ingreso hospitalario del paciente (número de ingreso, fecha de ingreso, centro de atención), la información demográfica completa del paciente (nombre, apellidos, tipo y número de identificación/cédula, fecha de nacimiento, edad, sexo, estado civil, teléfonos), la entidad aseguradora o pagadora con su régimen (contributivo, subsidiado, ARL, medicina prepagada, particular, entre otros), y el diagnóstico principal asociado al ingreso. Se utiliza para informes de referencia clínica y seguimiento asistencial de pacientes hospitalizados o en urgencias, permitiendo revisar las notas de enfermería por período junto con el contexto completo del ingreso y la identidad del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Referencia_NotasTab';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Referencia_NotasTab';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte tabular de notas de enfermería de nivel ''C'' registradas en un rango de fechas, enriquecido con datos demográficos del paciente, ingreso, entidad/régimen, centro de atención y diagnóstico principal.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_NotasTab';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas de inicio y fin deben ser válidas y coherentes (FechaIni <= FechaFin); Cada ingreso debe tener entidad, centro de atención, paciente, profesional, CareGroup y diagnóstico registrados (joins INNER); Debe existir la función [Common].[GETDATE]() para el cálculo de edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_NotasTab';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan notas con nivel de importancia ''C'' (CODNIVIMP=''C''); El filtro temporal se aplica sobre la fecha de registro de la nota truncada a DATE; La edad se calcula como diferencia simple de años (YEAR(hoy) - YEAR(nacimiento)), sin ajuste por mes/día; Solo se incluyen ingresos que tengan diagnóstico en DWHDiagnosticosTotal y un CareGroup asociado (INNER JOIN); Códigos de EntityType, tipo de documento, sexo y estado civil fuera de los listados se devuelven como NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_NotasTab';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/Admisión; Entidad aseguradora; Régimen de afiliación (Contributivo/Subsidiado/ARL/etc.); Centro de atención; Profesional de salud; Nota de enfermería (Subjetivo/Objetivo/Análisis); Diagnóstico; Grupo de cuidado contractual (CareGroup)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_NotasTab';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Cuando CODNIVIMP=''C'' y CAST(T.FECREGIST AS DATE) está entre @FechaIni y @FechaFin, devuelve fila con datos de nota, paciente, ingreso, régimen y diagnóstico', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_NotasTab';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CGR.EntityType (valor 1..13 o 99) → Traduce el código numérico a etiqueta de régimen (Contributivo, Subsidiado, ARL, IPS Privada/Pública, Fosyga, Particulares, etc.); si PAC.IPTIPODOC (1..8, 12) → Mapea a abreviatura del tipo de documento (CC, CE, TI, RC, PA, AS, MS, NUIP, PEP); si PAC.IPSEXOPAC = 1 / = 2 → Clasifica sexo como M o F; si PAC.IPESTADOC (1..5) → Traduce a estado civil (Soltero, Casado, Viudo, UnionLibre, Separado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_NotasTab';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCCTRNOTE; dbo.ADINGRESO; dbo.INPACIENT; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INPROFSAL; Contract.CareGroup; dbo.DWHDiagnosticosTotal', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_NotasTab';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Referencia_NotasTab';
-- GO
