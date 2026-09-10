

CREATE PROCEDURE [dbo].[SPHC_ListarPacientesRiesgosNecesidades]
(
@CentroAtencion Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT  CASE WHEN  I.NUMINGRES IS NULL THEN '1 - Pacientes en la Unidad' ELSE '2 - Pacientes Con Salida' END AS Egreso, 'Normal' as Alerta, A.CODICAMAS AS 'Codigo Cama',  RTRIM(DESCCAMAS) AS Cama,dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion,dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', C.IPCODPACI AS Identificacion,C.NUMINGRES AS Ingreso, dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento,RTRIM(DESTIPEST) AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS Paciente,CAST(0 AS BIT) AS Resultado, A.CAMTRACIR AS TrasladoCirugia, A.CAMTRAMED AS TrasladoMedicamentos, A.CODCONCEC AS Consecutivo,CAST('' as bit) AS MuestraAlerta,
J.CODESPTRA AS CodigoEspecialidad,RTRIM(K.DESESPECI) AS DescripcionEspecialidad,IFECHAING,  J.ESCADOWNT ,  J.ESCARASS, J.ESCNORPAC, J.ESCVASPAC, J.ESCAPAPAC, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON, dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS, dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE
FROM dbo.CHCAMASHO A INNER JOIN dbo.ADcenaten D ON A.CODCENATE=D.CODCENATE INNER JOIN 
dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO 
LEFT OUTER JOIN dbo.CHREGESTA C ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
LEFT OUTER JOIN dbo.CHTIPESTA G ON G.CODTIPEST=C.CODTIPEST 
LEFT OUTER JOIN dbo.INPacient H ON C.IPCODPACI=H.IPCODPACI 
LEFT OUTER JOIN dbo.HCREGEGRE I ON C.NUMINGRES=I.NUMINGRES
LEFT OUTER JOIN dbo.ADINGRESO J ON C.NUMINGRES=J.NUMINGRES
LEFT OUTER JOIN dbo.INESPECIA K ON J.CODESPTRA=K.CODESPECI
WHERE A.CODCENATE=@CentroAtencion AND ESTADCAMA ='2' 
ORDER BY A.CODICAMAS
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los pacientes actualmente hospitalizados (o con salida reciente) en las camas activas de un centro de atención, incluyendo información de la cama asignada, tipo de estancia, datos del paciente, especialidad médica tratante y los puntajes de escalas clínicas de riesgo y necesidades: escala de Downton (riesgo de caídas), RASS (sedación/agitación), Norton (úlceras por presión), VAS (dolor) y APACHE (gravedad del paciente crítico). Integra información de camas hospitalarias, registros de estancia, datos del paciente, ingresos, egresos y especialidades para ofrecer una vista consolidada del censo hospitalario con alertas de riesgo clínico. Se usa en el módulo de hospitalización para que enfermería y coordinación clínica monitoreen el estado de riesgo y las necesidades de cuidado de cada paciente por unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesRiesgosNecesidades';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesRiesgosNecesidades';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las camas ocupadas de un centro de atención con la información del paciente, su ingreso, especialidad de tratamiento y los puntajes de las escalas clínicas (Downton, RASS, Norton, VAS, APACHE), indicando si el paciente sigue en la unidad o ya egresó.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el centro de atención indicado en CHCAMASHO; Las camas deben encontrarse en estado ''2'' para ser consideradas; Para obtener datos clínicos del paciente debe existir un registro en CHREGESTA con REGESTADO = 1 vinculado a la cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan camas cuyo ESTADCAMA = ''2''; Solo se incluyen camas del centro de atención solicitado; Se considera únicamente el registro de estancia activo (REGESTADO = 1) por cama; El egreso se determina por la existencia o no de registro en HCREGEGRE asociado al ingreso; Cada cama listada incluye sus puntajes de las escalas Downton, RASS, Norton, VAS y APACHE calculadas para el ingreso/paciente activo; El campo Resultado siempre se devuelve como 0 (BIT) y MuestraAlerta como cadena vacía convertida a BIT; La columna Alerta se entrega siempre con valor fijo ''Normal''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cama hospitalaria; Centro de atención; Unidad funcional; Ingreso/Admisión; Egreso hospitalario; Aislamiento; Tipo de estancia; Clase de cama; Clase de habitación; Especialidad médica; Escalas de valoración (Downton, RASS, Norton, VAS, APACHE); Traslado a cirugía; Traslado de medicamentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando ESTADCAMA=''2'' y CODCENATE coincide con el centro solicitado, retorna fila por cama con datos de paciente, ingreso, egreso, escalas y especialidad de traslado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCREGEGRE.NUMINGRES IS NULL (no existe registro de egreso para el ingreso de la cama) → Se clasifica el paciente como ''Pacientes en la Unidad'' else Se clasifica como ''Pacientes Con Salida''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaNorton; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPacient; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidades';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidades';
-- GO
