CREATE PROCEDURE [dbo].[ESE_SP_Asistencial_InscritosRIAS] @FechaIni DATETIME, 
                                                         @FechaFin DATETIME
AS
     SELECT E.NOMENTIDA AS Entidad, 
            CC.NOMCENATE AS CentroAtencion, 
            I.IFECHAING AS FechaIngreso, 
            HC.FECHISPAC AS FechaHistoria, 
            I.NUMINGRES AS Ingreso,
            CASE P.IPTIPODOC
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
            END AS 'TipoIdentificacion', 
            RTRIM(I.IPCODPACI) AS 'Identificacion', 
            P.IPNOMCOMP AS Paciente, 
            CAST(P.IPFECNACI AS DATE) AS 'FechaNacimiento', 
            YEAR([Common].[GETDATE]()) - YEAR(P.IPFECNACI) AS Edad,
            CASE P.IPSEXOPAC
                WHEN 1
                THEN 'M'
                WHEN 2
                THEN 'F'
            END AS 'Sexo', 
            P.IPTELMOVI AS TelCelular, 
            P.IPTELEFON AS TelFijo, 
            P.IPDIRECCI AS Direccion, 
            IIF(C.IDRIASCUPS IS NOT NULL, 'SI', '') AS AplicaRIAS, 
            IIF(c.CODTIPCIT = 0, 'Primera Vez', 'Control') AS TipoConsulta, 
            UF.UFUDESCRI AS UnidadFuncional, 
            I.CODDIAEGR AS 'CodCIE-10', 
            Diag.NOMDIAGNO AS Diagnostico
     FROM AGASICITA C WITH(NOLOCK)
          INNER JOIN ADCONCOEX F WITH(NOLOCK) ON F.NUMCONCIT = C.CODAUTONU
          INNER JOIN ADINGRESO I WITH(NOLOCK) ON I.NUMINGRES = F.NUMINGRES
          INNER JOIN.HCHISPACA AS HC WITH(NOLOCK) ON I.NUMINGRES = HC.NUMINGRES
                                                     AND I.IPCODPACI = HC.IPCODPACI
          INNER JOIN INENTIDAD E WITH(NOLOCK) ON E.CODENTIDA = I.CODENTIDA
          INNER JOIN ADCENATEN CC WITH(NOLOCK) ON CC.CODCENATE = I.CODCENATE
          INNER JOIN INUNIFUNC AS UF WITH(NOLOCK) ON UF.UFUCODIGO = F.UFUCODIGO
          INNER JOIN INPACIENT P WITH(NOLOCK) ON P.IPCODPACI = C.IPCODPACI
          LEFT OUTER JOIN INDIAGNOS AS Diag WITH(NOLOCK) ON I.CODDIAEGR = Diag.CODDIAGNO
     WHERE HC.TIPHISPAC = 'I'
           AND HC.INDICAPAC <> 13
           AND CAST(HC.FECHISPAC AS DATE) BETWEEN @FechaIni AND @FechaFin
     UNION ALL
     SELECT E.NOMENTIDA AS Entidad, 
            CC.NOMCENATE AS CentroAtencion, 
            I.IFECHAING AS FechaIngreso, 
            H.FECHISPAC AS FechaHistoria, 
            I.NUMINGRES AS Ingreso,
            CASE P.IPTIPODOC
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
            END AS 'TipoIdentificacion', 
            RTRIM(I.IPCODPACI) AS 'Identificacion', 
            P.IPNOMCOMP AS Paciente, 
            CAST(P.IPFECNACI AS DATE) AS 'FechaNacimiento', 
            YEAR([Common].[GETDATE]()) - YEAR(P.IPFECNACI) AS Edad,
            CASE P.IPSEXOPAC
                WHEN 1
                THEN 'M'
                WHEN 2
                THEN 'F'
            END AS 'Sexo', 
            P.IPTELMOVI AS TelCelular, 
            P.IPTELEFON AS TelFijo, 
            P.IPDIRECCI AS Direccion, 
            'SinDato' + '-' + PH.DESCRIPCION AS AplicaRIAS, 
            'SinDato' AS TipoConsulta, 
            UF.UFUDESCRI AS UnidadFuncional, 
            I.CODDIAEGR AS 'CodCIE-10', 
            Diag.NOMDIAGNO AS Diagnostico
     FROM HCHISPACA H
          INNER JOIN PRMODELOHC PH ON H.IDMODELOHC = PH.ID
          INNER JOIN ADINGRESO I ON I.NUMINGRES = H.NUMINGRES
          INNER JOIN INENTIDAD E ON E.CODENTIDA = I.CODENTIDA
          INNER JOIN ADCENATEN CC ON CC.CODCENATE = I.CODCENATE
          INNER JOIN INPACIENT P ON P.IPCODPACI = I.IPCODPACI
          INNER JOIN INUNIFUNC AS UF WITH(NOLOCK) ON UF.UFUCODIGO = I.UFUCODIGO
          INNER JOIN INDIAGNOS AS Diag WITH(NOLOCK) ON I.CODDIAEGR = Diag.CODDIAGNO
     WHERE H.TIPHISPAC = 'I'
           AND H.INDICAPAC <> 13
           AND CAST(H.FECHISPAC AS DATE) BETWEEN @FechaIni AND @FechaFin
           AND IDMODELOHC IS NOT NULL
           AND H.NUMINGRES NOT IN
     (
         SELECT I.NUMINGRES
         FROM AGASICITA C
              INNER JOIN ADCONCOEX F ON F.NUMCONCIT = C.CODAUTONU
              INNER JOIN ADINGRESO I ON I.NUMINGRES = F.NUMINGRES
     );
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el listado de pacientes inscritos en RIAS (Rutas Integrales de Atención en Salud) atendidos en un rango de fechas. Combina dos fuentes: los ingresos vinculados a citas agendadas (AGASICITA y ADCONCOEX) y los ingresos con historia clínica registrada mediante modelos de HC sin cita previa, evitando duplicados mediante una exclusión explícita. Para cada paciente reporta datos demográficos (cédula, nombre, fecha de nacimiento, edad, sexo, teléfono, dirección), información del episodio de atención (número de ingreso, fecha de ingreso, fecha de historia clínica, entidad aseguradora, centro de atención, unidad funcional, tipo de consulta, diagnóstico CIE-10) y si el ingreso aplica a una ruta RIAS. Este procedimiento se utiliza para seguimiento asistencial, auditoría de rutas de atención y reporte de actividad ante entidades de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_InscritosRIAS';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ESE_SP_Asistencial_InscritosRIAS';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista pacientes con historia clínica de ingreso en un rango de fechas, indicando si la atención aplica a Rutas Integrales de Atención en Salud (RIAS) y datos demográficos/clínicos asociados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_InscritosRIAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las fechas @FechaIni y @FechaFin deben estar definidas y conformar un rango válido sobre FECHISPAC.; Deben existir maestros de entidad, centro de atención, unidad funcional, paciente y diagnóstico referenciados por el ingreso.; La historia clínica debe ser de tipo ''I'' (ingreso) y con INDICAPAC distinto de 13.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_InscritosRIAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran historias clínicas de tipo ingreso (''I'') y con INDICAPAC distinto de 13.; El filtro de fechas se aplica sobre la fecha de la historia clínica (FECHISPAC) truncada a DATE.; La edad se calcula como diferencia simple de años entre la fecha actual y la fecha de nacimiento (no considera mes/día).; Un mismo ingreso no aparece duplicado entre los dos bloques: el segundo excluye los ingresos ya cubiertos por el primero (con cita).; El segundo bloque exige que el ingreso tenga modelo de historia clínica definido (IDMODELOHC IS NOT NULL).; No realiza modificaciones de datos; es solo lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_InscritosRIAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso asistencial; Historia clínica; Cita asistencial; Entidad (asegurador/EPS); Centro de atención; Unidad funcional; Diagnóstico CIE-10; RIAS (Rutas Integrales de Atención en Salud); Tipo de consulta (Primera vez/Control); Tipo de identificación; Modelo de historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_InscritosRIAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un conjunto unificado (UNION ALL) de inscritos: primero los ingresos con cita asociada (AGASICITA→ADCONCOEX) marcando AplicaRIAS=''SI'' cuando IDRIASCUPS no es nulo, y luego los ingresos con modelo de HC (IDMODELOHC) que NO están en el conjunto anterior, marcados como ''SinDato-<modelo>''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_InscritosRIAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.IDRIASCUPS IS NOT NULL → AplicaRIAS = ''SI'' else AplicaRIAS = '''' (vacío); si C.CODTIPCIT = 0 → TipoConsulta = ''Primera Vez'' else TipoConsulta = ''Control''; si P.IPTIPODOC ∈ {1..8} → Mapea a etiquetas CC/CE/TI/RC/PA/AS/MS/NUIP; si P.IPSEXOPAC ∈ {1,2} → Mapea a ''M'' o ''F''; si Segunda consulta: H.NUMINGRES NOT IN (ingresos con cita en AGASICITA/ADCONCOEX) AND IDMODELOHC IS NOT NULL → Incluye el ingreso con AplicaRIAS=''SinDato-''+descripción del modelo de HC y TipoConsulta=''SinDato'' else Se excluye del segundo bloque (ya considerado en el primero)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_InscritosRIAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA; dbo.ADCONCOEX; dbo.ADINGRESO; dbo.HCHISPACA; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPACIENT; dbo.INDIAGNOS; dbo.PRMODELOHC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_InscritosRIAS';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ESE_SP_Asistencial_InscritosRIAS';
-- GO
