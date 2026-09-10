
CREATE PROCEDURE [dbo].[SP_ONCO_ListarPacientesDashboardQuimioterapia]
(
@CentroAtencion varchar(500),
@PestanaConsultar INT,
@FechaInicial datetime,
@FechaFinal datetime
)
AS
BEGIN
	SET NOCOUNT ON;

IF @PestanaConsultar = 1  --Pacientes con Ordenes Nuevas

			Select  Case A.MANEJOEXTERNO when 1 then 'Ambulatorio' when 0 then 'Hospitalario' end as 'Tipo Orden' ,A.ID,C.CODTIPPAC,A.FECHAREGISTRO AS 'Fecha Registro',B.IPCODPACI AS 'Identificacion',rtrim(ltrim(B.IPNOMCOMP)) as 'NombrePaciente', rtrim(ltrim(ENT.CODENTIDA)) + ' - ' + rtrim(ltrim(ENT.NOMENTIDA)) as 'Entidad',[dbo].[EDAD] (B.IPFECNACI,[Common].[GETDATE]()) As 'Edad',
				 Rtrim(D.NOMCENATE) AS 'Nombre Centro Atencion',Rtrim(A.CODCENATE) AS 'Codigo Centro Atencion',
				 Rtrim(A.CODDIAGNO) +'-'+ Rtrim(S.NOMDIAGNO) As 'Diagnostico',Rtrim(P.UFUDESCRI) As 'Nombre Unidad Funcional',A.UFUCODIGO as 'Codigo Unidad Funcional',
				 A.NUMEFOLIO AS 'Folio', Rtrim(o.Description) As 'Nombre Esquema', A.CICLOS As 'Ciclos Ordenados', Rtrim(M.NOMMEDICO) as 'Medico Ordeno',
				 Rtrim(N.DESESPECI) As 'Especialidad Medico',
				 Rtrim(A.CODDIAGNO) as 'Codigo Diagnostico', 
				 CI.IDHCORDPRON,
				 CASE CI.Environment WHEN 1 THEN 'Ambulatorio' WHEN 2 THEN 'Hospitalario' WHEN 3 THEN 'Mixta (Ambulatorio y Hospitalario)' ELSE 'N/A' END Ambiente,
				'Autorizacion' as Autorizacion, O.TypeScheme
				,Ingreso = (select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 and IESTADOIN IN ('','P','B')) --	,Ingreso = dbo.IngresoOncologico(A.IPCODPACI) 
				,A.NUMINGRES AS IngresoOrdenMedica, --Este ingreso es el que esta marcado como quimioterapia
				A.ESTADO AS 'Estado Quimioterapia',
				case A.ESTADO when 1 then 'Orden Solicitada' when 2 then 'Esquema iniciado' end as 'Estado Quimioterapia Nombre',
				[dbo].[ValidateSchemaStatus](1,a.ID,'',0,'') AS 'EstadoEsquemaMod',
				dbo.[RiskFactorAlert](A.IPCODPACI, ISNULL((select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 and IESTADOIN IN ('','P','B')),(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('','P','B') ORDER BY IFECHAING DESC)), 1) AS 'AlertaFactoresRiesgo', 
				dbo.[RiskFactorAlert](A.IPCODPACI, ISNULL((select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND TRATAESPECIA = 3 and IESTADOIN IN ('','P','B')),(select top 1 NUMINGRES from ADINGRESO where IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('','P','B') ORDER BY IFECHAING DESC)), 2) AS 'AlertaEscalas',
				(SELECT TOP 1 NUMINGRES FROM dbo.ADINGRESO WHERE IPCODPACI = A.IPCODPACI AND IESTADOIN IN ('', 'P', 'B') ORDER BY IFECHAING DESC) AS 'UltimoIngreso'
			FROM  [EHR].HCORDQUIMIO  A 
				INNER JOIN ehr.Schemes O with(nolock) ON O.Id  = A.SchemesId 
				INNER JOIN INPACIENT B with(nolock) ON A.IPCODPACI = B.IPCODPACI 
				INNER JOIN ADINGRESO C with(nolock) on A.NUMINGRES = C.NUMINGRES
				INNER JOIN INENTIDAD ENT with(nolock) on ENT.CODENTIDA = C.CODENTIDA 
				INNER JOIN ADCENATEN D with(nolock) on A.CODCENATE = D.CODCENATE 
				INNER JOIN INDIAGNOS S with(nolock) on S.CODDIAGNO = A.CODDIAGNO 
				INNER JOIN INUNIFUNC P with(nolock) on P.UFUCODIGO = A.UFUCODIGO 
				INNER JOIN INPROFSAL M with(nolock) on M.CODPROSAL = A.CODPROSAL
				INNER JOIN INESPECIA N with(nolock) on N.CODESPECI = A.CODESPECI 
				LEFT OUTER JOIN EHR.HCORDCICLOS CI WITH(NOLOCK) ON CI.IDHCORDQUIMIO = A.ID AND CI.CICLO = A.CICLOACTUAL --Agregado por Hector
		where A.ESTADO IN (1) and ORDENCONCITA = 0 AND A.CODCENATE in (SELECT Value FROM dbo.splitstring(@CentroAtencion)) ORDER BY A.FECHAREGISTRO DESC   --- 1.Solicitado sin cita
	

	ELSE IF @PestanaConsultar = 2     --Pacientes Con Esquemas 

-- Usar CTEs para precalcular valores repetidos
WITH IngresosPacientes AS (
    -- Precalcular ingresos oncológicos y último ingreso por paciente
    SELECT 
        IPCODPACI,
        MAX(CASE WHEN TRATAESPECIA = 3 AND IESTADOIN IN ('','P','B') THEN NUMINGRES END) AS IngresoOncologico,
        (SELECT TOP 1 NUMINGRES 
         FROM dbo.ADINGRESO i2 
         WHERE i2.IPCODPACI = i1.IPCODPACI 
           AND i2.IESTADOIN IN ('','P','B') 
         ORDER BY i2.IFECHAING DESC) AS UltimoIngreso
    FROM dbo.ADINGRESO i1 WITH(NOLOCK)
    WHERE IESTADOIN IN ('','P','B')
    GROUP BY IPCODPACI
),
CiclosInfo AS (
    -- Precalcular información de ciclos
    SELECT 
        IDHCORDQUIMIO,
        CICLO,
        MAX(FECHAREGISTRO) AS FechaRegistro
    FROM EHR.HCORDCICLOS WITH(NOLOCK)
    GROUP BY IDHCORDQUIMIO, CICLO
),
DiasAplicacion AS (
    -- Precalcular días de aplicación
    SELECT 
        IDHCORDQUIMIO,
        CICLO,
        MAX(Dia) AS MaxDia
    FROM EHR.HCORDCICLOSD WITH(NOLOCK)
    WHERE ESTADODIA = 2
    GROUP BY IDHCORDQUIMIO, CICLO
)
SELECT 
    CI.FechaRegistro AS 'Fecha Registro',
    CASE A.ORDENCONCITA WHEN 1 THEN 'Con Esquema Iniciado' ELSE 'Con Esquema, Sin Cita' END AS 'Estado Esquema',
    A.ID,
    C.CODTIPPAC,
    B.IPCODPACI AS 'Identificacion',
    RTRIM(LTRIM(B.IPNOMCOMP)) AS 'NombrePaciente',
    RTRIM(LTRIM(ENT.CODENTIDA)) + ' - ' + RTRIM(LTRIM(ENT.NOMENTIDA)) AS 'Entidad',
    [dbo].[EDAD](B.IPFECNACI, [Common].[GETDATE]()) AS 'Edad',
    RTRIM(D.NOMCENATE) AS 'Nombre Centro Atencion',
    RTRIM(A.CODCENATE) AS 'Codigo Centro Atencion',
    RTRIM(A.CODDIAGNO) + '-' + RTRIM(S.NOMDIAGNO) AS 'Diagnostico',
    RTRIM(P.UFUDESCRI) AS 'Nombre Unidad Funcional',
    A.UFUCODIGO AS 'Codigo Unidad Funcional',
    A.NUMEFOLIO AS 'Folio',
    RTRIM(O.Description) AS 'Nombre Esquema',
    A.CICLOS AS 'Ciclos Ordenados',
    RTRIM(M.NOMMEDICO) AS 'Medico Ordeno',
    RTRIM(N.DESESPECI) AS 'Especialidad Medico',
    RTRIM(A.CODDIAGNO) AS 'Codigo Diagnostico',
    NULL AS IDHCORDPRON,
    NULL AS Environment,
    'Autorizacion' AS Autorizacion,
    O.TypeScheme,
    IP.IngresoOncologico AS Ingreso,
    A.NUMINGRES AS IngresoOrdenMedica,
    A.CICLOS AS 'Ciclos Ordenados',
    A.CICLOACTUAL AS 'Ciclos Actual',
    A.ULTIMOCICLOAUTORIZADO AS 'Ultimo Ciclo Autorizado',
    CONCAT('Ciclo ', A.CICLOACTUAL, ' de ', A.CICLOS, ' Día: ', 
           COALESCE(RTRIM(CAST(DA.MaxDia AS VARCHAR)), 'Sin Administración')) AS Intervalo,
    A.ESTADO AS 'Estado Quimioterapia',
    CASE A.ESTADO 
        WHEN 1 THEN 'Orden Solicitada' 
        WHEN 2 THEN 'Esquema iniciado' 
    END AS 'Estado Quimioterapia Nombre',
    [dbo].[ValidateSchemaStatus](1, A.ID, '', 0, '') AS 'EstadoEsquemaMod',
    [dbo].[RiskFactorAlert](A.IPCODPACI, COALESCE(IP.IngresoOncologico, IP.UltimoIngreso), 1) AS 'AlertaFactoresRiesgo',
    [dbo].[RiskFactorAlert](A.IPCODPACI, COALESCE(IP.IngresoOncologico, IP.UltimoIngreso), 2) AS 'AlertaEscalas',
    IP.UltimoIngreso AS 'UltimoIngreso'
FROM [EHR].HCORDQUIMIO A WITH(NOLOCK)
    INNER JOIN ehr.Schemes O WITH(NOLOCK) ON O.Id = A.SchemesId 
    INNER JOIN INPACIENT B WITH(NOLOCK) ON A.IPCODPACI = B.IPCODPACI 
    INNER JOIN ADINGRESO C WITH(NOLOCK) ON A.NUMINGRES = C.NUMINGRES
    INNER JOIN INENTIDAD ENT WITH(NOLOCK) ON ENT.CODENTIDA = C.CODENTIDA 
    INNER JOIN INDIAGNOS S WITH(NOLOCK) ON S.CODDIAGNO = A.CODDIAGNO 
    INNER JOIN INUNIFUNC P WITH(NOLOCK) ON P.UFUCODIGO = A.UFUCODIGO
    INNER JOIN INPROFSAL M WITH(NOLOCK) ON M.CODPROSAL = A.CODPROSAL
    INNER JOIN INESPECIA N WITH(NOLOCK) ON N.CODESPECI = A.CODESPECI  
    INNER JOIN ADCENATEN D WITH(NOLOCK) ON A.CODCENATE = D.CODCENATE
    LEFT JOIN IngresosPacientes IP ON IP.IPCODPACI = A.IPCODPACI
    LEFT JOIN CiclosInfo CI ON CI.IDHCORDQUIMIO = A.ID AND CI.CICLO = A.CICLOACTUAL
    LEFT JOIN DiasAplicacion DA ON DA.IDHCORDQUIMIO = A.ID AND DA.CICLO = A.CICLOACTUAL
WHERE A.ESTADO IN (1, 2) 
    AND A.CODCENATE IN (SELECT Value FROM dbo.splitstring(@CentroAtencion))
    AND A.FECHAREGISTRO >= DATEADD(DAY, -30, Common.GETDATE())
ORDER BY A.FECHAREGISTRO DESC 			

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que alimenta el dashboard oncológico de quimioterapia, listando los pacientes con órdenes o ciclos activos según la pestaña seleccionada por el usuario. Según el parámetro de pestaña (@PestanaConsultar), devuelve: pacientes con órdenes nuevas solicitadas (pestaña 1), pacientes con esquemas iniciados o en curso (pestaña 2), pacientes con citas programadas (pestaña 3) u otras vistas de seguimiento. Para cada paciente consolida información de la orden de quimioterapia (HCORDQUIMIO), el esquema terapéutico aplicado (Schemes), los ciclos en ejecución (HCORDCICLOS), datos del paciente (INPACIENT), su ingreso hospitalario o ambulatorio (ADINGRESO), la entidad pagadora o EPS (INENTIDAD), el centro de atención (ADCENATEN), el diagnóstico oncológico CIE-10 (INDIAGNOS), la unidad funcional (INUNIFUNC), el médico oncólogo tratante (INPROFSAL) y su especialidad (INESPECIA). Se filtra por centro de atención y rango de fechas, e incluye alertas de factores de riesgo, estado del esquema, ciclo actual, último ciclo autorizado y el ingreso oncológico activo del paciente, siendo el insumo principal para el seguimiento clínico y operativo del programa de oncología.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesDashboardQuimioterapia';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ONCO_ListarPacientesDashboardQuimioterapia';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta el dashboard de quimioterapia devolviendo, según la pestaña solicitada, los pacientes con órdenes nuevas, con esquemas activos o agendados, filtrados por centros de atención y fechas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboardQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse una lista de centros de atención separable por dbo.splitstring.; @PestanaConsultar debe ser 1, 2 o 3 para producir resultados.; Para la pestaña 3, @FechaInicial debe corresponder al día de las citas a consultar (comparación por dd/MM/yyyy).; Existencia de relaciones íntegras entre HCORDQUIMIO y catálogos (paciente, ingreso, entidad, centro, diagnóstico, unidad funcional, profesional, especialidad, esquema).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboardQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El filtro por centro de atención siempre se aplica vía dbo.splitstring sobre la lista recibida.; El ingreso oncológico se identifica por TRATAESPECIA=3 e IESTADOIN IN ('''',''P'',''B''); si no existe, se usa el último ingreso activo (IESTADOIN IN ('''',''P'',''B'')) por IFECHAING desc.; Los días de aplicación contabilizados solo consideran ESTADODIA=2.; La pestaña 1 solo expone órdenes sin cita asignada (ORDENCONCITA=0) y en estado solicitado.; La pestaña 2 limita el resultado a órdenes registradas en los últimos 30 días.; La pestaña 3 solo muestra citas en estado 0 (no atendidas/pendientes), del tipo solicitud=3 y tratamiento=1 (quimioterapia), de la fecha indicada.; Los estados de cita y de orden se traducen siempre con los mismos mapeos (CASE) en todas las pestañas.; El procedimiento es de solo lectura (no realiza INSERT/UPDATE/DELETE).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboardQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Quimioterapia; Esquema de tratamiento oncológico; Ciclos de quimioterapia; Días de administración; Órdenes médicas oncológicas; Ingreso oncológico; Centro de atención; Cita / Agendamiento; Autorización; Factores de riesgo; Escalas clínicas; Diagnóstico; Unidad funcional; Especialidad médica; Entidad (asegurador); Manejo ambulatorio / hospitalario / mixto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboardQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando @PestanaConsultar=1: retorna órdenes de HCORDQUIMIO con ESTADO=1 y ORDENCONCITA=0 cuyos CODCENATE están en la lista, ordenadas por FECHAREGISTRO desc.; [RETURN_RESULT] resultset: Cuando @PestanaConsultar=2: retorna órdenes con ESTADO IN (1,2), CODCENATE en la lista y FECHAREGISTRO >= hoy-30 días, incluyendo ciclo actual e indicador de día máximo administrado (ESTADODIA=2).; [RETURN_RESULT] resultset: Cuando @PestanaConsultar=3: retorna citas de AGASICITA con CODESTCIT=0, TIPSOLICITU=3, TIPTRATAMIENTO=1, FECHORAIN igual a @FechaInicial (formato dd/MM/yyyy) y CODCENATE en la lista, unidas con la orden de quimioterapia, ciclo y día.; [RETURN_RESULT] resultset: En todas las pestañas se enriquece cada fila con edad del paciente, alertas de factores de riesgo y de escalas (RiskFactorAlert) y, en pestaña 1-2, estado del esquema (ValidateSchemaStatus).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboardQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Pestaña = 1 (Pacientes con Órdenes Nuevas) → Lista órdenes de quimioterapia con ESTADO=1 (Orden Solicitada) y ORDENCONCITA=0, filtradas por centros de atención else Evalúa siguiente pestaña; si Pestaña = 2 (Pacientes con Esquemas) → Lista órdenes con ESTADO IN (1,2) registradas en los últimos 30 días, con info de ciclo actual y máximo día administrado (ESTADODIA=2) else Evalúa siguiente pestaña; si Pestaña = 3 (Pacientes Agendados) → Lista citas (AGASICITA) con CODESTCIT=0, TIPSOLICITU=3, TIPTRATAMIENTO=1, en la fecha inicial indicada y centros filtrados; si MANEJOEXTERNO de la orden → 1=''Ambulatorio'', 0=''Hospitalario''; si Environment del ciclo → 1=''Ambulatorio'', 2=''Hospitalario'', 3=''Mixta'', otro=''N/A''; si ORDENCONCITA en pestaña 2 → 1=''Con Esquema Iniciado'', otro=''Con Esquema, Sin Cita''; si ESTADO de la quimioterapia → 1=''Orden Solicitada'', 2=''Esquema iniciado''; si ConfirmationStatus de la cita → 2 → confirmada (bit 1), otro → no confirmada (bit 0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboardQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.EDAD; Common.GETDATE; dbo.ValidateSchemaStatus; dbo.RiskFactorAlert; dbo.splitstring', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboardQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORDQUIMIO; ehr.Schemes; dbo.INPACIENT; dbo.ADINGRESO; dbo.INENTIDAD; dbo.ADCENATEN; dbo.INDIAGNOS; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; EHR.HCORDCICLOS; EHR.HCORDCICLOSD; dbo.AGASICITA; dbo.AGACTIMED; dbo.AGENSALAC; dbo.AGEQUIPTRA; dbo.SEGusuaru; dbo.HCFARMEPC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboardQuimioterapia';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ONCO_ListarPacientesDashboardQuimioterapia';
-- GO
