CREATE PROCEDURE [dbo].[SPHC_ListarPacientesPendientesConsultaExternaTemporal]
(@CentroAtencion  CHAR(10), 
 @UnidadFuncional CHAR(10), 
 @Profesional     CHAR(20), 
 @FechaInicial    DATETIME, 
 @FechaFinal      DATETIME
)
AS
    BEGIN
        DECLARE @Tmp_Resultado TABLE
        ([CODAUTONU]               [INT] NOT NULL, 
         [DescripcionEspecialidad] [VARCHAR](60) NULL, 
         [CodEspecialidad]         [VARCHAR](3) NULL, 
         [Egreso]                  [VARCHAR](13) NOT NULL, 
         [EstadoIngreso]           [CHAR](1) NOT NULL, 
         [IPFECHACO]               [DATETIME] NULL, 
         [TipoCita]                [VARCHAR](14) NOT NULL, 
         [NOMENTIDA]               [VARCHAR](150) NULL, 
         [Identificacion]          [CHAR](15) NULL, 
         [Paciente]                [VARCHAR](250) NULL, 
         [Ingreso]                 [VARCHAR](10) NULL, 
         [MuestraAlerta]           [BIT] NULL, 
         [Alerta]                  [VARCHAR](6) NOT NULL, 
         [ConsecutivoCita]         [INT] NULL, 
         [IPFECHCIT]               [DATETIME] NULL, 
         [LlamadoUno]              [BIT] NULL, 
         [LlamadoDos]              [BIT] NULL, 
         [LlamadoTres]             [BIT] NULL, 
         [ESCADOWNT]               [VARCHAR](1) NOT NULL, 
         [ESCARASS]                [VARCHAR](1) NOT NULL, 
         [ESCVASPAC]               [VARCHAR](1) NOT NULL, 
         [ESCAPAPAC]               [VARCHAR](1) NOT NULL, 
         [ESCNORPAC]               [VARCHAR](1) NOT NULL, 
         [PUNTAJEDOWN]             [INT] NOT NULL, 
         [PUNTAJERASS]             [INT] NOT NULL, 
         [PUNTAJEVAS]              [INT] NOT NULL, 
         [PUNTAJEAPACHE]           [INT] NOT NULL, 
         [PUNTAJENORTON]           [INT] NOT NULL, 
         [CODACTMED]               [VARCHAR](3) NULL, 
         [TipoPaciente]            [INT] NULL, 
         [Fecha Nacimiento]        [DATETIME] NOT NULL, 
         [Edad]                    [CHAR](50) NULL, 
         [ASMS]                    [INT] NOT NULL, 
         [ZONAPARTADA]             [BIT] NULL, 
         [POBESPECIAL]             [INT] NULL, 
         [VIVESOLO]                [BIT] NULL, 
         [ACOMPANANTES]            [INT] NULL, 
         [Riesgo]                  [BIT] NULL, 
         [IDRIASCUPS]              [INT] NOT NULL, 
         [Actividad]               [NVARCHAR](150) NOT NULL
        );
        INSERT INTO @Tmp_Resultado
        (CODAUTONU, 
         DescripcionEspecialidad, 
         CodEspecialidad, 
         Egreso, 
         EstadoIngreso, 
         IPFECHACO, 
         TipoCita, 
         NOMENTIDA, 
         Identificacion, 
         Paciente, 
         Ingreso, 
         MuestraAlerta, 
         Alerta, 
         ConsecutivoCita, 
         IPFECHCIT, 
         LlamadoUno, 
         LlamadoDos, 
         LlamadoTres, 
         ESCADOWNT, 
         ESCARASS, 
         ESCVASPAC, 
         ESCAPAPAC, 
         ESCNORPAC, 
         PUNTAJEDOWN, 
         PUNTAJERASS, 
         PUNTAJEVAS, 
         PUNTAJEAPACHE, 
         PUNTAJENORTON, 
         CODACTMED, 
         TipoPaciente, 
         [Fecha Nacimiento], 
         Edad, 
         ASMS, 
         ZONAPARTADA, 
         POBESPECIAL, 
         VIVESOLO, 
         ACOMPANANTES, 
         Riesgo, 
         IDRIASCUPS, 
         Actividad
        )
               SELECT g.CODAUTONU, 
                      RTRIM(E.DESESPECI) AS DescripcionEspecialidad, 
                      RTRIM(E.CODESPECI) AS CodEspecialidad, 
                      '1 - En Espera' AS Egreso, 
                      D.IESTADOIN AS EstadoIngreso, 
                      A.IPFECHACO,
                      CASE g.CODTIPCIT
                          WHEN '0'
                          THEN 'Primera Vez'
                          WHEN '1'
                          THEN 'Control'
                          WHEN '2'
                          THEN 'PosOperatorio'
                      END AS TipoCita, 
                      RTRIM(C.NOMENTIDA) AS NOMENTIDA, 
                      A.IPCODPACI AS Identificacion, 
                      RTRIM(B.IPNOMCOMP) AS Paciente, 
                      A.NUMINGRES AS Ingreso, 
                      CAST('' AS BIT) AS MuestraAlerta, 
                      'Normal' AS Alerta, 
                      A.CODCONCEC AS ConsecutivoCita, 
                      G.FECHORAIN AS IPFECHCIT, 
                      A.PRIMERLLA AS LlamadoUno, 
                      A.SEGUNDLLA AS LlamadoDos, 
                      A.TERCERLLA AS LlamadoTres, 
                      '0' AS ESCADOWNT, 
                      '0' AS ESCARASS, 
                      '0' AS ESCVASPAC, 
                      '0' AS ESCAPAPAC, 
                      '0' AS ESCNORPAC, 
                      0 AS PUNTAJEDOWN, 
                      0 AS PUNTAJERASS, 
                      0 AS PUNTAJEVAS, 
                      0 AS PUNTAJEAPACHE, 
                      0 AS PUNTAJENORTON, 
                      g.CODACTMED, 
                      D.CODTIPPAC AS TipoPaciente, 
                      IPFECNACI AS 'Fecha Nacimiento', 
                      CAST('' AS CHAR(50)) AS Edad,
                      CASE
                          WHEN B.IPTIPODOC IN(6, 7)
                          THEN 1
                          ELSE 0
                      END AS ASMS, 
                      B.ZONAPARTADA, 
               (
                   SELECT COUNT(*)
                   FROM dbo.ADPOBESPEPAC Z
                        INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE
                   WHERE IPCODPACI = B.IPCODPACI
                         AND TIPOPOESPERIES = 1
               ) AS POBESPECIAL, 
                      D.VIVESOLO, 
               (
                   SELECT COUNT(*)
                   FROM ADACOMPAN AS AD
                   WHERE AD.NUMINGRES = D.NUMINGRES
               ) AS ACOMPANANTES, 
                      CONVERT(BIT, 0) AS Riesgo, 
                      ISNULL(G.IDRIASCUPS, 0) AS IDRIASCUPS, 
                      RTRIM(Z.DESACTMED) AS 'Actividad'
               FROM dbo.ADCONCOEX A WITH(NOLOCK) 
                    --inner join dbo.ADCONCOED f on a.CODCONCEC=f.CODCONCEC 
                    INNER JOIN dbo.AGASICITA g WITH(NOLOCK) ON A.NUMCONCIT = g.CODAUTONU
                    INNER JOIN dbo.AGACTIMED z WITH(NOLOCK) ON z.CODACTMED = g.CODACTMED
                    INNER JOIN dbo.INPACIENT B WITH(NOLOCK) ON A.IPCODPACI = B.IPCODPACI
                    INNER JOIN dbo.INENTIDAD C WITH(NOLOCK) ON A.CODENTIDA = C.CODENTIDA
                    INNER JOIN dbo.ADINGRESO D WITH(NOLOCK) ON A.NUMINGRES = D.NUMINGRES
                    LEFT OUTER JOIN DBO.INESPECIA E WITH(NOLOCK) ON G.CODESPECI = E.CODESPECI
               WHERE A.CODCENATE = @CentroAtencion
                     AND A.CONESTADO = '1'
                     AND A.CODPROSAL = @Profesional
                     AND (G.FECHORAIN >= @FechaInicial
                          AND G.FECHORAFI <= @FechaFinal);
        INSERT INTO @Tmp_Resultado
        (CODAUTONU, 
         DescripcionEspecialidad, 
         CodEspecialidad, 
         Egreso, 
         EstadoIngreso, 
         IPFECHACO, 
         TipoCita, 
         NOMENTIDA, 
         Identificacion, 
         Paciente, 
         Ingreso, 
         MuestraAlerta, 
         Alerta, 
         ConsecutivoCita, 
         IPFECHCIT, 
         LlamadoUno, 
         LlamadoDos, 
         LlamadoTres, 
         ESCADOWNT, 
         ESCARASS, 
         ESCVASPAC, 
         ESCAPAPAC, 
         ESCNORPAC, 
         PUNTAJEDOWN, 
         PUNTAJERASS, 
         PUNTAJEVAS, 
         PUNTAJEAPACHE, 
         PUNTAJENORTON, 
         CODACTMED, 
         TipoPaciente, 
         [Fecha Nacimiento], 
         Edad, 
         ASMS, 
         ZONAPARTADA, 
         POBESPECIAL, 
         VIVESOLO, 
         ACOMPANANTES, 
         Riesgo, 
         IDRIASCUPS, 
         Actividad
        )
               SELECT A.CODAUTONU, 
                      RTRIM(D.DESESPECI) AS DescripcionEspecialidad, 
                      RTRIM(D.CODESPECI) AS CodEspecialidad, 
                      '2 - Asignadas' AS Egreso, 
                      '' AS EstadoIngreso, 
                      A.FECHORAIN AS IPFECHACO,
                      CASE a.CODTIPCIT
                          WHEN '0'
                          THEN 'Primera Vez'
                          WHEN '1'
                          THEN 'Control'
                          WHEN '2'
                          THEN 'Pos Operatorio'
                      END AS TipoCita, 
                      RTRIM(C.NOMENTIDA) AS NOMENTIDA, 
                      A.IPCODPACI AS Identificacion, 
                      RTRIM(B.IPNOMCOMP) AS Paciente, 
                      '' AS Ingreso, 
                      CAST('' AS BIT) AS MuestraAlerta, 
                      'Normal' AS Alerta, 
                      CAST('' AS INT) AS ConsecutivoCita, 
                      A.FECHORAIN AS IPFECHCIT, 
                      '' AS LlamadoUno, 
                      '' AS LlamadoDos, 
                      '' AS LlamadoTres, 
                      '0' AS ESCADOWNT, 
                      '0' AS ESCARASS, 
                      '0' AS ESCVASPAC, 
                      '0' AS ESCAPAPAC, 
                      '0' AS ESCNORPAC, 
                      0 AS PUNTAJEDOWN, 
                      0 AS PUNTAJERASS, 
                      0 AS PUNTAJEVAS, 
                      0 AS PUNTAJEAPACHE, 
                      0 AS PUNTAJENORTON, 
                      A.CODACTMED, 
                      '' AS TipoPaciente, 
                      IPFECNACI AS 'Fecha Nacimiento', 
                      CAST('' AS CHAR(50)) AS Edad,
                      CASE
                          WHEN B.IPTIPODOC IN(6, 7)
                          THEN 1
                          ELSE 0
                      END AS ASMS, 
                      B.ZONAPARTADA, 
               (
                   SELECT COUNT(*)
                   FROM dbo.ADPOBESPEPAC Z
                        INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE
                   WHERE IPCODPACI = B.IPCODPACI
                         AND TIPOPOESPERIES = 1
               ) AS POBESPECIAL, 
                      '' AS VIVESOLO, 
                      '' AS ACOMPANANTES, 
                      CONVERT(BIT, 0) AS Riesgo, 
                      ISNULL(A.IDRIASCUPS, 0) AS IDRIASCUPS, 
                      RTRIM(Z.DESACTMED) AS 'Actividad'
               FROM dbo.AGASICITA A WITH(NOLOCK)
                    INNER JOIN dbo.AGACTIMED z WITH(NOLOCK) ON z.CODACTMED = A.CODACTMED
                    INNER JOIN dbo.INPACIENT B WITH(NOLOCK) ON A.IPCODPACI = B.IPCODPACI
                    INNER JOIN dbo.INENTIDAD C WITH(NOLOCK) ON B.CODENTIDA = C.CODENTIDA
                    INNER JOIN dbo.INESPECIA D WITH(NOLOCK) ON A.CODESPECI = D.CODESPECI
                    LEFT JOIN dbo.ADCONCOEX ad WITH(NOLOCK) ON ad.NUMCONCIT = a.CODAUTONU
               WHERE A.CODCENATE = @CentroAtencion
                     AND A.CODESTCIT = '0'
                     AND A.CODPROSAL = @Profesional
                     AND (A.FECHORAIN >= @FechaInicial
                          AND A.FECHORAIN <= @FechaFinal)
                     AND ad.CODCONCEC IS NULL;
        INSERT INTO @Tmp_Resultado
        (CODAUTONU, 
         DescripcionEspecialidad, 
         CodEspecialidad, 
         Egreso, 
         EstadoIngreso, 
         IPFECHACO, 
         TipoCita, 
         NOMENTIDA, 
         Identificacion, 
         Paciente, 
         Ingreso, 
         MuestraAlerta, 
         Alerta, 
         ConsecutivoCita, 
         IPFECHCIT, 
         LlamadoUno, 
         LlamadoDos, 
         LlamadoTres, 
         ESCADOWNT, 
         ESCARASS, 
         ESCVASPAC, 
         ESCAPAPAC, 
         ESCNORPAC, 
         PUNTAJEDOWN, 
         PUNTAJERASS, 
         PUNTAJEVAS, 
         PUNTAJEAPACHE, 
         PUNTAJENORTON, 
         CODACTMED, 
         TipoPaciente, 
         [Fecha Nacimiento], 
         Edad, 
         ASMS, 
         ZONAPARTADA, 
         POBESPECIAL, 
         VIVESOLO, 
         ACOMPANANTES, 
         Riesgo, 
         IDRIASCUPS, 
         Actividad
        )
               SELECT 0 AS CODAUTONU, 
                      RTRIM(E.DESESPECI) AS DescripcionEspecialidad, 
                      RTRIM(E.CODESPECI) AS CodEspecialidad, 
                      '1 - En Espera' AS Egreso, 
                      D.IESTADOIN AS EstadoIngreso, 
                      A.IPFECHACO AS IPFECHACO,
                      CASE CODTIPCON
                          WHEN '1'
                          THEN 'Primera Vez'
                          WHEN '2'
                          THEN 'Control'
                          WHEN '3'
                          THEN 'Pos Operatorio'
                      END AS TipoCita, 
                      RTRIM(C.NOMENTIDA) AS NOMENTIDA, 
                      A.IPCODPACI AS Identificacion, 
                      RTRIM(B.IPNOMCOMP) AS Paciente, 
                      d.NUMINGRES AS Ingreso, 
                      CAST('' AS BIT) AS MuestraAlerta, 
                      'Normal' AS Alerta, 
                      A.CODCONCEC AS ConsecutivoCita, 
                      A.IPFECHCIT AS IPFECHCIT, 
                      '' AS LlamadoUno, 
                      '' AS LlamadoDos, 
                      '' AS LlamadoTres, 
                      '0' AS ESCADOWNT, 
                      '0' AS ESCARASS, 
                      '0' AS ESCVASPAC, 
                      '0' AS ESCAPAPAC, 
                      '0' AS ESCNORPAC, 
                      0 AS PUNTAJEDOWN, 
                      0 AS PUNTAJERASS, 
                      0 AS PUNTAJEVAS, 
                      0 AS PUNTAJEAPACHE, 
                      0 AS PUNTAJENORTON, 
                      '' AS CODACTMED, 
                      D.CODTIPPAC AS TipoPaciente, 
                      IPFECNACI AS 'Fecha Nacimiento', 
                      CAST('' AS CHAR(50)) AS Edad,
                      CASE
                          WHEN B.IPTIPODOC IN(6, 7)
                          THEN 1
                          ELSE 0
                      END AS ASMS, 
                      B.ZONAPARTADA, 
               (
                   SELECT COUNT(*)
                   FROM dbo.ADPOBESPEPAC Z
                        INNER JOIN ADPOBESPE X ON X.ID = Z.IDADPOBESPE
                   WHERE IPCODPACI = B.IPCODPACI
                         AND TIPOPOESPERIES = 1
               ) AS POBESPECIAL, 
                      D.VIVESOLO, 
               (
                   SELECT COUNT(*)
                   FROM ADACOMPAN AS AD
                   WHERE AD.NUMINGRES = D.NUMINGRES
               ) AS ACOMPANANTES, 
                      CONVERT(BIT, 0) AS Riesgo, 
                      0 AS IDRIASCUPS, 
                      '' AS 'Actividad'
               FROM dbo.ADCONCOEX A
                    INNER JOIN dbo.INPACIENT B WITH(NOLOCK) ON A.IPCODPACI = B.IPCODPACI
                    INNER JOIN dbo.INENTIDAD C WITH(NOLOCK) ON A.CODENTIDA = C.CODENTIDA
                    INNER JOIN dbo.ADINGRESO D WITH(NOLOCK) ON A.NUMINGRES = D.NUMINGRES
                    INNER JOIN dbo.INPROFSAL F WITH(NOLOCK) ON A.CODPROSAL = F.CODPROSAL
                    LEFT OUTER JOIN dbo.INESPECIA E WITH(NOLOCK) ON A.CODESPECI = E.CODESPECI
               WHERE A.CODCENATE = @CentroAtencion
                     AND A.CONESTADO = '1'
                     AND A.CODPROSAL = @Profesional
                     AND (A.IPFECHCIT >= @FechaInicial
                          AND A.IPFECHCIT <= @FechaFinal)
                     AND A.CODCONCEC IN
               (
                   SELECT CODCONCEC
                   FROM ADCONCOED WITH(NOLOCK)
                   WHERE CODCONCEC = A.CODCONCEC
                         AND (A.NUMCONCIT = 0)
               );
        SELECT CODAUTONU, 
               DescripcionEspecialidad, 
               CodEspecialidad, 
               Egreso, 
               EstadoIngreso, 
               IPFECHACO, 
               TipoCita, 
               NOMENTIDA, 
               Identificacion, 
               Paciente, 
               Ingreso, 
               MuestraAlerta, 
               Alerta, 
               ConsecutivoCita, 
               IPFECHCIT, 
               LlamadoUno, 
               LlamadoDos, 
               LlamadoTres, 
               ESCADOWNT, 
               ESCARASS, 
               ESCVASPAC, 
               ESCAPAPAC, 
               ESCNORPAC, 
               PUNTAJEDOWN, 
               PUNTAJERASS, 
               PUNTAJEVAS, 
               PUNTAJEAPACHE, 
               PUNTAJENORTON, 
               CODACTMED, 
               TipoPaciente, 
               [Fecha Nacimiento], 
               Edad, 
               ASMS, 
               ZONAPARTADA, 
               POBESPECIAL, 
               VIVESOLO, 
               ACOMPANANTES, 
               Riesgo, 
               IDRIASCUPS, 
               Actividad
        FROM @Tmp_Resultado;
        --  DROP TABLE @Tmp_Resultado;
    END;
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes pendientes de atención en consulta externa para una sala de espera virtual o tablero de llamado. Dado un centro de atención, unidad funcional, profesional de la salud y rango de fechas, retorna las citas agendadas que aún no han sido atendidas, incluyendo datos del paciente (identificación, nombre, fecha de nacimiento, edad), tipo de cita (primera vez, control, posoperatorio), especialidad, estado del ingreso, historial de llamados (primer, segundo y tercer llamado), escalas clínicas de riesgo (Down, RASS, VAS, APACHE, Norton) y características sociales del paciente como si vive solo, si pertenece a población especial, zona apartada o si es afiliado al régimen subsidiado. Su propósito es alimentar el módulo de gestión de consulta externa para que el personal de salud controle el flujo de pacientes en espera y registre los llamados realizados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesPendientesConsultaExternaTemporal';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesPendientesConsultaExternaTemporal';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la lista consolidada de pacientes pendientes de consulta externa (en espera y citas asignadas) para un centro de atención, profesional y rango de fechas, con datos demográficos, de cita y banderas de riesgo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExternaTemporal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir maestros de paciente, entidad, especialidad, actividad médica e ingreso referenciados por las citas/consultas.; Los rangos de fecha @FechaInicial/@FechaFinal deben ser coherentes para acotar la búsqueda.; El centro de atención y el profesional deben existir y corresponder a los registros buscados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExternaTemporal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado consolida tres orígenes de datos en una sola lista de pacientes pendientes de consulta externa.; Las escalas y puntajes (Down, Rass, Vas, Apache, Norton) siempre se inicializan en ''0''/0; el procedimiento no calcula puntajes reales.; El campo Alerta siempre se devuelve como ''Normal'' y MuestraAlerta y Riesgo se inicializan en falso.; La columna POBESPECIAL solo cuenta poblaciones especiales con TIPOPOESPERIES=1.; Solo se incluyen registros del centro de atención y profesional indicados, dentro del rango de fechas.; En el segundo bloque, se excluyen citas ya vinculadas a una consulta externa (ad.CODCONCEC IS NULL) para evitar duplicar con el primer bloque.; El parámetro @UnidadFuncional se recibe pero no se utiliza en ningún filtro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExternaTemporal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Consulta externa; Cita médica (asignada / en espera); Tipo de cita (Primera vez, Control, PosOperatorio); Especialidad médica; Actividad médica; Paciente; Entidad/Aseguradora; Ingreso/Admisión; Profesional de salud; Población especial; Acompañantes; Zona apartada; Tipo de documento ASMS; Llamados de turno; Escalas de riesgo (Down, Rass, Vas, Apache, Norton); Riesgo CUPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExternaTemporal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Tmp_Resultado: Cuando ADCONCOEX.CONESTADO=''1'' y coincide centro/profesional/rango de fechas con AGASICITA, se inserta el paciente como ''En Espera'' con la cita y actividad médica asociadas.; [INSERT] @Tmp_Resultado: Cuando AGASICITA.CODESTCIT=''0'' (cita asignada) en el centro/profesional/rango y no existe consulta externa relacionada (ad.CODCONCEC IS NULL), se inserta como ''Asignadas''.; [INSERT] @Tmp_Resultado: Cuando ADCONCOEX.CONESTADO=''1'' y su CODCONCEC está en ADCONCOED con NUMCONCIT=0, se inserta como ''En Espera'' usando el tipo de consulta CODTIPCON.; [RETURN_RESULT] @Tmp_Resultado: Al final se devuelve el contenido completo de la tabla temporal con todos los pacientes pendientes consolidados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExternaTemporal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Origen ADCONCOEX con cita asociada en AGASICITA (CONESTADO=''1'') → Se clasifica como Egreso=''1 - En Espera'' y se mapea CODTIPCIT 0/1/2 a ''Primera Vez''/''Control''/''PosOperatorio''; si Origen AGASICITA con CODESTCIT=''0'' y sin registro en ADCONCOEX (ad.CODCONCEC IS NULL) → Se clasifica como Egreso=''2 - Asignadas'' y se mapea CODTIPCIT 0/1/2 a ''Primera Vez''/''Control''/''Pos Operatorio''; si Origen ADCONCOEX (CONESTADO=''1'') cuyo CODCONCEC existe en ADCONCOED y NUMCONCIT=0 → Se clasifica como Egreso=''1 - En Espera'' y se mapea CODTIPCON 1/2/3 a ''Primera Vez''/''Control''/''Pos Operatorio''; si IPTIPODOC del paciente IN (6,7) → ASMS=1 else ASMS=0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExternaTemporal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONCOEX; dbo.AGASICITA; dbo.AGACTIMED; dbo.INPACIENT; dbo.INENTIDAD; dbo.ADINGRESO; dbo.INESPECIA; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.ADCONCOED; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExternaTemporal';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesPendientesConsultaExternaTemporal';
-- GO
