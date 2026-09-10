


CREATE PROCEDURE [dbo].[SPCH_ListarPacientesEnObservacion]
(
@UnidadFuncional Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT
    A.CODTIPPAC AS TipoPaciente,
    RTRIM(C.DESESPECI) AS EspecialidadTratante,
    CAST('' AS varchar(100)) AS Origen,
    'Normal' AS Alerta,
    A.IPCODPACI AS Identificacion,
    A.NUMINGRES AS Ingreso,
    RTRIM(B.IPNOMCOMP) AS Paciente,
    CONVERT(bit, 0) AS Resultado,
    A.IFECHAING,
    CONVERT(bit, 0) AS MuestraAlerta,
    A.INGRECEXT AS Remitido,
    CASE D.TRIAGECLA
        WHEN 1 THEN 'TRIAGE I'
        WHEN 2 THEN 'TRIAGE II'
        WHEN 3 THEN 'TRIAGE III'
        WHEN 4 THEN 'TRIAGE IV'
        WHEN 5 THEN 'TRIAGE V'
    END AS Triage,
    CASE
        WHEN B.IPTIPODOC IN (6, 7) THEN 1
        ELSE 0
    END AS ASMS,
    B.ZONAPARTADA,
    L.RIESGOAGRE,
    ISNULL(PE.Cantidad, 0) AS POBESPECIAL,
    A.VIVESOLO,
    ISNULL(AC.Cantidad, 0) AS ACOMPANANTES,
    CONVERT(bit, 0) AS Riesgo,
    ISNULL(A.DESTINOPAC, 0) AS Agrupacion,
    B.IPFECNACI AS [Fecha Nacimiento],
    CAST('' AS varchar(50)) AS Edad,

    CONCAT(
        RTRIM(P.CODENTIDA),
        '-',
        RTRIM(P.NOMENTIDA)
    ) AS EntidadPaciente,

    CONCAT(
        RTRIM(Q.CODDIAGNO),
        '-',
        RTRIM(Q.NOMDIAGNO)
    ) AS Diagnostico,

    HC.FECHISPAC AS FechaHC,
    0 AS Minutos,
    0 AS Barra,
    CONVERT(bit, 0) AS AlertaLAB
FROM dbo.ADINGRESO AS A WITH (NOLOCK) 
INNER JOIN dbo.INPACIENT AS B WITH (NOLOCK) ON B.IPCODPACI = A.IPCODPACI
INNER JOIN dbo.ADACTIVID AS L WITH (NOLOCK) ON L.CODACTIVI = B.CODACTIVI
LEFT JOIN dbo.INESPECIA AS C WITH (NOLOCK) ON C.CODESPECI = A.CODESPTRA
LEFT JOIN dbo.INENTIDAD AS P WITH (NOLOCK) ON P.CODENTIDA = B.CODENTIDA
OUTER APPLY
(
    SELECT COUNT(*) AS Cantidad
    FROM dbo.ADACOMPAN AS AC WITH (NOLOCK)
    WHERE AC.NUMINGRES = A.NUMINGRES
) AS AC
OUTER APPLY
(
    SELECT COUNT(*) AS Cantidad
    FROM dbo.ADPOBESPEPAC AS Z WITH (NOLOCK)
    INNER JOIN dbo.ADPOBESPE AS X WITH (NOLOCK)
        ON X.ID = Z.IDADPOBESPE
       AND X.TIPOPOESPERIES = 1
    WHERE Z.IPCODPACI = B.IPCODPACI
) AS PE
OUTER APPLY
(
    SELECT TOP (1)
        T.TRIAGECLA
    FROM dbo.ADTRIAGEU AS T WITH (NOLOCK)
    WHERE T.NUMINGRES = A.NUMINGRES
    ORDER BY T.ID DESC
) AS D
OUTER APPLY
(
    SELECT TOP (1)
        DP.CODDIAGNO
    FROM dbo.INDIAGNOP AS DP WITH (NOLOCK)
    WHERE DP.IPCODPACI = A.IPCODPACI
      AND DP.NUMINGRES = A.NUMINGRES
      AND DP.CODDIAPRI = 1
 ORDER BY DP.FECDIAGNO DESC
) AS DIAG
LEFT JOIN dbo.INDIAGNOS AS Q WITH (NOLOCK) ON Q.CODDIAGNO = DIAG.CODDIAGNO
OUTER APPLY
(
    SELECT TOP (1)
        H.FECHISPAC
    FROM dbo.HCHISPACA AS H WITH (NOLOCK)
    WHERE H.IPCODPACI = A.IPCODPACI
      AND H.NUMINGRES = A.NUMINGRES
    ORDER BY H.FECHISPAC DESC
) AS HC 
WHERE A.IESTADOIN = '' AND A.UFUINGMED = @UnidadFuncional AND A.UFUINGHOS IS NULL
  AND (
        A.UFUEGRMED IS NULL
        OR A.DESTINOPAC = 1
      )
  AND A.DESTINOPAC IS NOT NULL;

END

GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes que se encuentran actualmente en observación (sala de urgencias o área de espera médica) para una unidad funcional específica. Combina datos del ingreso (ADINGRESO), la información demográfica del paciente (INPACIENT), la clasificación de triage más reciente (ADTRIAGEU), la especialidad tratante (INESPECIA), la entidad aseguradora o pagadora del paciente (INENTIDAD) y el diagnóstico principal CIE-10 (INDIAGNOS/INDIAGNOP) para armar una vista consolidada del estado de cada paciente en sala. También incluye indicadores operativos como número de acompañantes, si el paciente vive solo, si pertenece a población especial, si fue remitido, nivel de triage, fecha de última historia clínica y alertas de riesgo agregado, siendo utilizado típicamente por los módulos de gestión de urgencias y monitoreo en tiempo real de camas de observación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesEnObservacion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_ListarPacientesEnObservacion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes actualmente en observación de urgencias para una unidad funcional, enriqueciendo cada caso con datos de triage, diagnóstico principal, entidad responsable, acompañantes y marcadores de riesgo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEnObservacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La unidad funcional debe corresponder al campo UFUINGMED de los ingresos; Los ingresos deben tener IESTADOIN vacío (activos); DESTINOPAC debe estar definido (no nulo) para considerarse en observación; Existencia de catálogos de paciente, especialidad, entidad y diagnóstico para enriquecer la información', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEnObservacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se consideran ingresos activos (IESTADOIN vacío) y sin egreso hospitalario (UFUINGHOS IS NULL); Para cada ingreso se reporta un único triage: el más reciente según ID descendente; El diagnóstico mostrado siempre es el marcado como principal (CODDIAPRI=1) del ingreso y paciente correspondientes; El conteo de población especial sólo considera registros con TIPOPOESPERIES=1; Se entregan campos placeholder (Origen, Alerta=''Normal'', Resultado=0, MuestraAlerta, Edad, Minutos, Barra, AlertaLAB, Riesgo) para ser calculados/llenados por el consumidor; Agrupacion se devuelve como 0 cuando DESTINOPAC es nulo (vía ISNULL)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEnObservacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente en observación de urgencias; Unidad funcional; Triage (clasificación I-V); Especialidad tratante; Entidad responsable de pago; Diagnóstico principal; Población especial; Acompañantes; Riesgo de agresión; Paciente que vive solo; Zona apartada; Paciente remitido; Historia clínica del paciente; Tipo de documento ASMS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEnObservacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve el conjunto de pacientes en observación filtrando por IESTADOIN='''' AND UFUINGMED=@UnidadFuncional AND UFUINGHOS IS NULL AND (UFUEGRMED IS NULL OR DESTINOPAC = 1) AND DESTINOPAC IS NOT NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEnObservacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TRIAGECLA del último triage del ingreso (1..5) → Etiqueta el triage como ''TRIAGE I'' a ''TRIAGE V'' según el valor numérico; si B.IPTIPODOC IN (6,7) → Marca al paciente como ASMS=1 (tipo de documento que indica condición especial) else ASMS=0; si UFUEGRMED IS NULL OR DESTINOPAC = 1 → Incluye el ingreso en el listado (paciente sigue en observación o destino marcado como observación); si Selección de triage por NUMINGRES → Toma sólo el último registro de ADTRIAGEU usando ROW_NUMBER() particionado por NUMINGRES ordenado por ID DESC (IDparticion=1); si Selección de diagnóstico → Toma el primer diagnóstico (TOP 1) de INDIAGNOP donde CODDIAPRI=1, considerándolo el diagnóstico principal del ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEnObservacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; dbo.INPACIENT; dbo.ADACTIVID; dbo.ADTRIAGEU; dbo.INESPECIA; dbo.ADCONTURG; dbo.INENTIDAD; dbo.INDIAGNOS; dbo.INDIAGNOP; dbo.ADPOBESPEPAC; dbo.ADPOBESPE; dbo.ADACOMPAN; dbo.HCHISPACA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEnObservacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_ListarPacientesEnObservacion';
-- GO
