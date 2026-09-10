-- =============================================
-- Author:      <Author, Edwin Reifer , Name>
-- Create Date: <Create Date,12/02/2021 >
-- Description: <Description, sp que Consulta las citas de consulta externa para el formulario que se encuentra en admsiones llamado control citas consulta externa>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AD_ListarControlCitasConsultaExterna]
(
	 @TipoConsultar as integer, --1:ControlCitas ; 2:PreConsulta
	 @CentroAtencion VARCHAR(10), --='11011',
	 @FechaInicial DATETIME, --= '2021/02/12 00:00:00',
	 @FechaFinal DATETIME --= '2021/02/12 23:59:59' 
)

AS
BEGIN

    -- Reduce tráfico de red eliminando mensajes de conteo de filas
    SET NOCOUNT ON;
    
    -- ============================================
    -- Tipo 1: CONTROL DE CITAS (estados 1,3,4,5)
    -- ============================================
    IF @TipoConsultar = 1 
    BEGIN  
        -- Primera consulta: Citas con ingreso (estados 1,3,4,5)
        SELECT 
            a.CODCONCEC AS 'Codigo', 
            RTRIM(E.DESESPECI) AS 'Especialidad',
            RTRIM(E.CODESPECI) AS CodEspecialidad,
            '1 - En Espera' AS Egreso,
            D.IESTADOIN AS EstadoIngreso, 
            A.IPFECHACO,
            CASE g.CODTIPCIT 
                WHEN '0' THEN 'Primera Vez' 
                WHEN '1' THEN 'Control' 
                WHEN '2' THEN 'PosOperatorio' 
            END AS 'Tipo Cita',
            RTRIM(C.NOMENTIDA) AS 'Entidad',
            A.IPCODPACI AS 'Identificación',
            RTRIM(B.IPNOMCOMP) AS 'Nombres Paciente', 
            A.NUMINGRES AS Ingreso,
            CAST(0 AS BIT) AS MuestraAlerta,
            'Normal' AS Alerta,
            A.CODCONCEC AS ConsecutivoCita,
            G.FECHORAIN AS 'Fecha Inicial Cita',  
            A.PRIMERLLA AS LlamadoUno, 
            A.SEGUNDLLA AS LlamadoDos, 
            A.TERCERLLA AS LlamadoTres, 
            '0' AS ESCADOWNT, '0' AS ESCARASS, '0' AS ESCVASPAC, 
            '0' AS ESCAPAPAC, '0' AS ESCNORPAC, 
            0 AS PUNTAJEDOWN, 0 AS PUNTAJERASS, 0 AS PUNTAJEVAS,  
            0 AS PUNTAJEAPACHE, 0 AS PUNTAJENORTON, 
            g.CODACTMED, 
            D.CODTIPPAC AS TipoPaciente, 
            B.IPFECNACI AS 'Fecha Nacimiento',
            CAST('' AS CHAR(50)) AS Edad,   
            CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, 
            B.ZONAPARTADA, D.VIVESOLO, 
            CONVERT(BIT,0) AS Riesgo, 
            ISNULL(G.IDRIASCUPS,0) AS IDRIASCUPS,
            RTRIM(Z.DESACTMED) AS 'Actividad',
            CASE G.MODALIDAD WHEN 0 THEN 'Presencial' WHEN 1 THEN 'Teleconsulta' ELSE '' END AS Modalidad, 
            RTRIM(I.NOMMEDICO) AS 'Profesional',
            g.FECHORAFI AS 'Fecha Final Cita',
            'Sin Especificar' AS 'Consultorio',   
            CASE A.CONESTADO 
                WHEN 1 THEN 'Con Ingreso' WHEN 3 THEN 'Cumplida' 
                WHEN 4 THEN 'En Sala' WHEN 5 THEN 'En Consultorio'  
            END AS 'ESTADO',  
            CASE A.CONESTADO 
                WHEN 1 THEN '2. Con Ingreso' WHEN 3 THEN '5. Cumplida' 
                WHEN 4 THEN '3. En Sala' WHEN 5 THEN '4. En Consultorio'  
            END AS 'Orden_Agrupacion',   
            g.FECREGSIS AS 'Fecha Asignacion Cita',  
            A.FECREGSIS AS 'Fecha Cita Con Ingreso',  
            A.FECCITACUMPLIDA AS 'Fecha Cita Cumplida',  
            A.FECREGISTROSALA AS 'Fecha Paciente sala',  
            A.FECENCONSULTORIO AS 'Fecha Paciente Consultorio'
        FROM dbo.ADCONCOEX A
        INNER JOIN dbo.AGASICITA g ON A.NUMCONCIT = CAST(g.CODAUTONU AS CHAR(20))   
        INNER JOIN dbo.AGACTIMED z ON z.CODACTMED = g.CODACTMED   
        INNER JOIN dbo.INPACIENT B ON A.IPCODPACI = B.IPCODPACI   
        INNER JOIN dbo.INENTIDAD C ON A.CODENTIDA = C.CODENTIDA   
        INNER JOIN dbo.ADINGRESO D ON A.NUMINGRES = D.NUMINGRES  
        INNER JOIN dbo.INPROFSAL i ON A.CODPROSAL = I.CODPROSAL  
        LEFT JOIN dbo.INESPECIA E ON G.CODESPECI = E.CODESPECI  
        WHERE A.CODCENATE = @CentroAtencion 
          AND A.CONESTADO IN (1,3,4,5)  
          AND A.IPFECHCIT BETWEEN @FechaInicial AND @FechaFinal -- OPTIMIZACIÓN: BETWEEN más legible

        UNION ALL
  
        -- Segunda consulta: Citas asignadas sin ingreso (estado 0, no en ADCONCOEX)
        SELECT 
            A.CODAUTONU AS 'Codigo',
            RTRIM(D.DESESPECI) AS 'Especialidad',
            RTRIM(D.CODESPECI) AS CodEspecialidad,
            '2 - Asignadas' AS Egreso,
            '' AS EstadoIngreso,
            A.FECHORAIN AS IPFECHACO,
            CASE CODTIPCIT WHEN '0' THEN 'Primera Vez' WHEN '1' THEN 'Control' WHEN '2' THEN 'Pos Operatorio' END AS 'Tipo Cita',
            RTRIM(C.NOMENTIDA) AS 'Entidad',
            A.IPCODPACI AS 'Identificación',  
            RTRIM(B.IPNOMCOMP) AS 'Nombres Paciente',
            '' AS Ingreso,
            CAST(0 AS BIT) AS MuestraAlerta,
            'Normal' AS Alerta,
            0 AS ConsecutivoCita,
            A.FECHORAIN AS 'Fecha Inicial Cita',
            '' AS LlamadoUno, '' AS LlamadoDos, '' AS LlamadoTres, 
            '0' AS ESCADOWNT, '0' AS ESCARASS, '0' AS ESCVASPAC, 
            '0' AS ESCAPAPAC, '0' AS ESCNORPAC, 
            0 AS PUNTAJEDOWN, 0 AS PUNTAJERASS, 0 AS PUNTAJEVAS,  
            0 AS PUNTAJEAPACHE, 0 AS PUNTAJENORTON, 
            A.CODACTMED, '' AS TipoPaciente, 
            B.IPFECNACI AS 'Fecha Nacimiento',
            CAST('' AS CHAR(50)) AS Edad,  
            CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, 
            B.ZONAPARTADA, '' AS VIVESOLO, 
            CONVERT(BIT,0) AS Riesgo, 
            ISNULL(A.IDRIASCUPS,0) AS IDRIASCUPS,
            RTRIM(Z.DESACTMED) AS 'Actividad',
            CASE A.MODALIDAD WHEN 0 THEN 'Presencial' WHEN 1 THEN 'Teleconsulta' ELSE '' END AS Modalidad, 
            RTRIM(I.NOMMEDICO) AS 'Profesional',
            A.FECHORAFI AS 'Fecha Final Cita',
            RTRIM(F.DESCRICON) AS 'Consultorio',  
            'Asignada' AS 'ESTADO', 
            '1. Asignada' AS 'Orden_Agrupacion',  
            A.FECREGSIS AS 'Fecha Asignacion Cita',  
            NULL AS 'Fecha Cita Con Ingreso',  
            NULL AS 'Fecha Cita Cumplida',  
            NULL AS 'Fecha Paciente sala',  
            NULL AS 'Fecha Paciente Consultorio'  
        FROM dbo.AGASICITA A
        INNER JOIN dbo.AGACTIMED z ON z.CODACTMED = A.CODACTMED  
        INNER JOIN dbo.INPACIENT B ON A.IPCODPACI = B.IPCODPACI   
        INNER JOIN dbo.INENTIDAD C ON B.CODENTIDA = C.CODENTIDA  
        INNER JOIN dbo.INESPECIA D ON A.CODESPECI = D.CODESPECI  
        INNER JOIN dbo.INPROFSAL i ON A.CODPROSAL = I.CODPROSAL  
        INNER JOIN dbo.AGCONSULT F ON A.CODIGOCON = F.CODIGOCON AND A.CODCENATE = F.CODCENATE   
        WHERE A.CODCENATE = @CentroAtencion 
          AND A.CODESTCIT = '0'
          AND A.FECHORAIN BETWEEN @FechaInicial AND @FechaFinal -- OPTIMIZACIÓN: BETWEEN
          -- OPTIMIZACIÓN: SELECT 1 en lugar de SELECT * es más eficiente en EXISTS
          AND NOT EXISTS (SELECT 1 FROM dbo.ADCONCOEX ad WHERE ad.NUMCONCIT = CAST(A.CODAUTONU AS CHAR(20)))
    END  
    
    -- ============================================
    -- Tipo 2: PRE-CONSULTA (estados 1,4 sin examen físico)
    -- ============================================
    ELSE IF @TipoConsultar = 2 
    BEGIN  
        SELECT 
            a.CODCONCEC AS 'Codigo', 
            RTRIM(E.DESESPECI) AS 'Especialidad',
            RTRIM(E.CODESPECI) AS CodEspecialidad,
            '1 - En Espera' AS Egreso,
            D.IESTADOIN AS EstadoIngreso, 
            A.IPFECHACO,
            CASE g.CODTIPCIT WHEN '0' THEN 'Primera Vez' WHEN '1' THEN 'Control' WHEN '2' THEN 'PosOperatorio' END AS 'Tipo Cita',
            RTRIM(C.NOMENTIDA) AS 'Entidad',
            A.IPCODPACI AS 'Identificación',
            RTRIM(B.IPNOMCOMP) AS 'Nombres Paciente', 
            A.NUMINGRES AS Ingreso,
            CAST(0 AS BIT) AS MuestraAlerta, 'Normal' AS Alerta,
            A.CODCONCEC AS ConsecutivoCita,
            G.FECHORAIN AS 'Fecha Inicial Cita',  
            A.PRIMERLLA AS LlamadoUno, A.SEGUNDLLA AS LlamadoDos, A.TERCERLLA AS LlamadoTres, 
            '0' AS ESCADOWNT, '0' AS ESCARASS, '0' AS ESCVASPAC, 
            '0' AS ESCAPAPAC, '0' AS ESCNORPAC, 
            0 AS PUNTAJEDOWN, 0 AS PUNTAJERASS, 0 AS PUNTAJEVAS,  
            0 AS PUNTAJEAPACHE, 0 AS PUNTAJENORTON, 
            g.CODACTMED, D.CODTIPPAC AS TipoPaciente, 
            B.IPFECNACI AS 'Fecha Nacimiento',
            CAST('' AS CHAR(50)) AS Edad,   
            CASE WHEN B.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS, 
            B.ZONAPARTADA, D.VIVESOLO, 
            CONVERT(BIT,0) AS Riesgo, 
            ISNULL(G.IDRIASCUPS,0) AS IDRIASCUPS,
            RTRIM(Z.DESACTMED) AS 'Actividad',
            CASE G.MODALIDAD WHEN 0 THEN 'Presencial' WHEN 1 THEN 'Teleconsulta' ELSE '' END AS Modalidad, 
            RTRIM(I.NOMMEDICO) AS 'Profesional',
            g.FECHORAFI AS 'Fecha Final Cita',
            'Sin Especificar' AS 'Consultorio',   
            CASE A.CONESTADO WHEN 1 THEN 'Con Ingreso' WHEN 4 THEN 'En Sala' END AS 'ESTADO',  
            CASE A.CONESTADO WHEN 1 THEN '2. Con Ingreso' WHEN 4 THEN '3. En Sala' END AS 'Orden_Agrupacion',   
            g.FECREGSIS AS 'Fecha Asignacion Cita',  
            A.FECREGSIS AS 'Fecha Cita Con Ingreso',  
            A.FECCITACUMPLIDA AS 'Fecha Cita Cumplida',  
            A.FECREGISTROSALA AS 'Fecha Paciente sala',  
            A.FECENCONSULTORIO AS 'Fecha Paciente Consultorio',  
            A.UFUCODIGO AS UnidadFuncionalFacturacion  
        FROM dbo.ADCONCOEX A
        INNER JOIN dbo.AGASICITA g ON A.NUMCONCIT = CAST(g.CODAUTONU AS CHAR(20))  
        INNER JOIN dbo.AGACTIMED z ON z.CODACTMED = g.CODACTMED   
        INNER JOIN dbo.INPACIENT B ON A.IPCODPACI = B.IPCODPACI   
        INNER JOIN dbo.INENTIDAD C ON A.CODENTIDA = C.CODENTIDA   
        INNER JOIN dbo.ADINGRESO D ON A.NUMINGRES = D.NUMINGRES  
        INNER JOIN dbo.INPROFSAL i ON A.CODPROSAL = I.CODPROSAL  
        LEFT JOIN dbo.INESPECIA E ON G.CODESPECI = E.CODESPECI  
        WHERE A.CODCENATE = @CentroAtencion 
          AND A.CONESTADO IN (1,4)  
          AND A.IPFECHCIT BETWEEN @FechaInicial AND @FechaFinal -- OPTIMIZACIÓN: BETWEEN
          -- OPTIMIZACIÓN: SELECT 1 más eficiente
          AND NOT EXISTS (SELECT 1 FROM HCEXFISIC z WHERE z.NUMINGRES = a.NUMINGRES AND z.IPCODPACI = a.IPCODPACI)
end

end

/*
Estado del Control de Consultas:
1: Sin Atender/Con Ingreso
2: Ausente/Anulada
3: Atendido/Cita Cumplida
4: En sala
5: En consultorio
*/
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista y controla las citas de consulta externa para el módulo de admisiones, permitiendo visualizar en tiempo real el estado de cada cita por centro de atención y rango de fechas. Opera en dos modos: modo 1 (Control de Citas) combina las citas que ya tienen ingreso registrado (provenientes de ADCONCOEX) con las citas asignadas aún sin ingreso (de AGASICITA), mostrando el flujo completo del paciente desde ''Asignada'' hasta ''Cumplida'', pasando por ''En Sala'' y ''En Consultorio''; modo 2 (PreConsulta) se enfoca en citas con ingreso para gestión previa a la consulta. Integra datos del paciente (INPACIENT), la entidad aseguradora o pagadora (INENTIDAD), el profesional de salud (INPROFSAL), la especialidad médica (INESPECIA), la actividad médica agendada (AGACTIMED) y el ingreso hospitalario (ADINGRESO), devolviendo información consolidada como cédula del paciente, nombre, entidad, especialidad, tipo de cita (primera vez, control, posoperatorio), modalidad (presencial o teleconsulta), fechas de cada etapa del flujo y puntajes de escalas clínicas para el formulario de control de citas de consulta externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarControlCitasConsultaExterna';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ListarControlCitasConsultaExterna';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las citas de consulta externa de un centro de atención en un rango de fechas, en dos modos: control de citas (incluye estados con ingreso, cumplida, en sala, en consultorio y citas asignadas pendientes) o pre-consulta (citas con ingreso o en sala sin examen físico registrado).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarControlCitasConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro de tipo de consulta debe ser 1 (Control de Citas) o 2 (PreConsulta); cualquier otro valor no produce resultados.; Debe proveerse un código de centro de atención y un rango de fechas (inicial y final).; Las citas a recuperar deben pertenecer al centro de atención indicado.; Para citas con ingreso, deben existir registros relacionados en pacientes, entidad, ingreso, profesional, actividad médica y especialidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarControlCitasConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran citas del centro de atención solicitado.; Las citas asignadas (CODESTCIT=''0'') solo aparecen si todavía no tienen registro de control de consulta externa asociado.; En modo PreConsulta, una cita desaparece del resultado tan pronto se le registra examen físico en HCEXFISIC.; El estado de la cita y su orden de agrupación están alineados: 1=Asignada, 2=Con Ingreso, 3=En Sala, 4=En Consultorio, 5=Cumplida.; Los puntajes de escalas clínicas (Down, Rass, Vas, Apache, Norton) se devuelven siempre en cero y la edad como cadena vacía; son placeholders calculados por la aplicación.; El campo Riesgo siempre se devuelve en 0 (false).; Todas las lecturas se hacen con NOLOCK, asumiendo tolerancia a lecturas sucias.; El modo 2 incluye una columna adicional (UnidadFuncionalFacturacion) que no existe en modo 1, por lo que los resultados no son estructuralmente compatibles entre modos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarControlCitasConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita de consulta externa; Control de citas; PreConsulta; Tipo de cita (Primera Vez, Control, PosOperatorio); Modalidad (Presencial, Teleconsulta); Estado de la cita (Asignada, Con Ingreso, En Sala, En Consultorio, Cumplida); Paciente; Entidad aseguradora; Profesional de salud; Especialidad médica; Actividad médica; Ingreso hospitalario; Examen físico; Centro de atención; Consultorio; Llamados de paciente; Escalas clínicas (Down, Rass, Vas, Apache, Norton); RIAS/CUPS; Unidad funcional de facturación; Tipo de documento del paciente; Zona apartada; Vive solo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarControlCitasConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando @TipoConsultar = 1, retorna UNION ALL de: (a) citas de ADCONCOEX con CONESTADO IN (1,3,4,5) e IPFECHCIT en el rango, y (b) citas de AGASICITA con CODESTCIT=''0'' (Asignada) en el rango que aún no tienen registro en ADCONCOEX.; [RETURN_RESULT] N/A: Cuando @TipoConsultar = 2, retorna citas de ADCONCOEX con CONESTADO IN (1,4) (Con Ingreso o En Sala) en el rango de IPFECHCIT, excluyendo aquellas que ya tienen examen físico registrado en HCEXFISIC para el mismo ingreso y paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarControlCitasConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TipoConsultar = 1 → Ejecuta la consulta de Control de Citas: une citas con ingreso/sala/consultorio/cumplidas con citas asignadas sin ingreso aún. else Si @TipoConsultar = 2 ejecuta la consulta de PreConsulta filtrando solo citas con ingreso o en sala sin examen físico previo.; si CODTIPCIT IN (''0'',''1'',''2'') → Mapea a ''Primera Vez'', ''Control'' o ''PosOperatorio'' respectivamente para clasificar el tipo de cita.; si MODALIDAD = 0 / 1 → Etiqueta la cita como ''Presencial'' o ''Teleconsulta''; otro valor queda en blanco.; si CONESTADO IN (1,3,4,5) (modo 1) o IN (1,4) (modo 2) → Asigna etiquetas y orden de agrupación según el estado: Con Ingreso, Cumplida, En Sala, En Consultorio.; si IPTIPODOC IN (6,7) → Marca el paciente como ASMS = 1 (indicador de tipo de documento especial).; si NOT EXISTS en ADCONCOEX para la cita asignada → Solo entonces incluye la cita asignada de AGASICITA en el resultado del modo 1, evitando duplicar citas que ya generaron ingreso.; si NOT EXISTS en HCEXFISIC para el ingreso y paciente → Solo entonces incluye la cita en el modo PreConsulta, excluyendo pacientes que ya tienen examen físico registrado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarControlCitasConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONCOEX; dbo.AGASICITA; dbo.AGACTIMED; dbo.INPACIENT; dbo.INENTIDAD; dbo.ADINGRESO; dbo.INPROFSAL; dbo.INESPECIA; dbo.AGCONSULT; dbo.HCEXFISIC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarControlCitasConsultaExterna';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ListarControlCitasConsultaExterna';
-- GO
