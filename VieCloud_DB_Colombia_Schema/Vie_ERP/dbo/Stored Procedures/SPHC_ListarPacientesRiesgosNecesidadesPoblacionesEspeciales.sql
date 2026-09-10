

CREATE PROCEDURE [dbo].[SPHC_ListarPacientesRiesgosNecesidadesPoblacionesEspeciales]
(
@CentroAtencion Char(10) 
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT DISTINCT CASE WHEN  I.NUMINGRES IS NULL THEN '1 - Pacientes en la Unidad' ELSE '2 - Pacientes Con Salida' END AS Egreso, 'Normal' as Alerta, A.CODICAMAS AS 'Codigo Cama',  RTRIM(DESCCAMAS) AS Cama,dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion,dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', C.IPCODPACI AS Identificacion,C.NUMINGRES AS Ingreso, dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento,RTRIM(DESTIPEST) AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS Paciente,CAST(0 AS BIT) AS Resultado, A.CAMTRACIR AS TrasladoCirugia, A.CAMTRAMED AS TrasladoMedicamentos, A.CODCONCEC AS Consecutivo,CAST('' as bit) AS MuestraAlerta,
J.CODESPTRA AS CodigoEspecialidad,RTRIM(K.DESESPECI) AS DescripcionEspecialidad,IFECHAING,  J.ESCADOWNT ,  J.ESCARASS, J.ESCNORPAC, J.ESCVASPAC, J.ESCAPAPAC, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON, dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS, dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE
, RTRIM(E.UFUCODIGO) + ' - ' + RTRIM(E.UFUDESCRI) AS UFUCODIGO, iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = C.IPCODPACI and NUMINGRES = C.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
FROM dbo.CHCAMASHO A 
INNER JOIN dbo.ADcenaten D ON A.CODCENATE=D.CODCENATE 
INNER JOIN dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO 
INNER JOIN dbo.CHREGESTA C ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
INNER JOIN dbo.ADPOBESPEPAC L ON C.IPCODPACI = L.IPCODPACI
LEFT OUTER JOIN dbo.CHTIPESTA G ON G.CODTIPEST=C.CODTIPEST 
LEFT OUTER JOIN dbo.INPacient H ON C.IPCODPACI=H.IPCODPACI 
LEFT OUTER JOIN dbo.HCREGEGRE I ON C.NUMINGRES=I.NUMINGRES
LEFT OUTER JOIN dbo.ADINGRESO J ON C.NUMINGRES=J.NUMINGRES
LEFT OUTER JOIN dbo.INESPECIA K ON J.CODESPTRA=K.CODESPECI
INNER JOIN dbo.ADPOBESPE S ON S.ID = L.IDADPOBESPE 
WHERE A.CODCENATE=@CentroAtencion AND ESTADCAMA ='2' AND TIPOPOESPERIES = 1
ORDER BY A.CODICAMAS
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes actualmente hospitalizados (camas ocupadas) en un centro de atención específico que pertenecen a poblaciones especiales (gestantes, adultos mayores, víctimas, discapacitados, etc.) y que tienen riesgos o necesidades identificadas. Combina información de camas hospitalarias, estancias activas, datos del paciente, ingreso/admisión, especialidad tratante y escalas clínicas de riesgo (Down-Ton, RASS, Norton, VAS, Apache), indicando además si el paciente ya tiene egreso registrado o sigue en la unidad. Se usa para que el personal clínico y administrativo identifique rápidamente los pacientes de alto riesgo o condición especial que están hospitalizados, con sus alertas, escalas de valoración y recomendaciones activas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesRiesgosNecesidadesPoblacionesEspeciales';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesRiesgosNecesidadesPoblacionesEspeciales';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las camas ocupadas de un centro de atención con pacientes catalogados como población especial de tipo riesgo, mostrando datos clínicos, escalas de valoración y si tienen recomendación activa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesPoblacionesEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibir un código de centro de atención válido; Deben existir camas en estado ocupado (''2'') en el centro indicado; Deben existir registros en ADPOBESPEPAC vinculados a ADPOBESPE con TIPOPOESPERIES = 1 (poblaciones especiales tipo riesgo); El registro de estancia (CHREGESTA) debe tener REGESTADO = 1 (estado activo)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesPoblacionesEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna camas con ESTADCAMA = ''2'' (ocupadas); Solo considera estancias activas (CHREGESTA.REGESTADO = 1); Solo incluye pacientes vinculados a poblaciones especiales con TIPOPOESPERIES = 1 (riesgo); La alerta siempre se inicializa en ''Normal'' y el campo Resultado en 0 (BIT); Filtra siempre por el centro de atención recibido como parámetro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesPoblacionesEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cama hospitalaria; Centro de atención; Unidad funcional; Ingreso hospitalario; Egreso; Aislamiento; Tipo de estancia; Clase de habitación; Clase de cama; Especialidad médica; Población especial de riesgo; Escala Down-Ton; Escala RASS; Escala Norton; Escala VAS; Escala APACHE; Traslado a cirugía; Traslado para medicamentos; Recomendación de interconsulta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesPoblacionesEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas únicas (DISTINCT) por cama ocupada con paciente perteneciente a población especial de riesgo en el centro de atención solicitado, ordenadas por código de cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesPoblacionesEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCREGEGRE.NUMINGRES IS NULL (no hay egreso registrado) → Marca el paciente como ''1 - Pacientes en la Unidad'' else Marca el paciente como ''2 - Pacientes Con Salida''; si Existe al menos un registro en RecommendPatient para el paciente e ingreso con Status = 1 → Marca Recomendacion = 1 (true) else Marca Recomendacion = 0 (false)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesPoblacionesEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaNorton; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesPoblacionesEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.ADPOBESPEPAC; dbo.CHTIPESTA; dbo.INPacient; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.INESPECIA; dbo.ADPOBESPE; dbo.RecommendPatient', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesPoblacionesEspeciales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesPoblacionesEspeciales';
-- GO
