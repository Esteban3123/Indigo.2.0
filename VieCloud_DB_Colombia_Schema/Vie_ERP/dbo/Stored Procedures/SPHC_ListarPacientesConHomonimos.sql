
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesConHomonimos]
(
@CentroAtencion Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT  CASE WHEN  I.NUMINGRES IS NULL THEN '1 - Pacientes en la Unidad' ELSE '2 - Pacientes Con Salida' END AS Egreso, 'Normal' as Alerta, A.CODICAMAS AS 'Codigo Cama',  RTRIM(DESCCAMAS) AS Cama,dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion,dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', RTRIM(C.IPCODPACI) AS Identificacion,C.NUMINGRES AS Ingreso, dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento,RTRIM(DESTIPEST) AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS Paciente,CAST(0 AS BIT) AS Resultado, A.CAMTRACIR AS TrasladoCirugia, A.CAMTRAMED AS TrasladoMedicamentos, A.CODCONCEC AS Consecutivo,CAST('' as bit) AS MuestraAlerta,
J.CODESPTRA AS CodigoEspecialidad,RTRIM(K.DESESPECI) AS DescripcionEspecialidad,IFECHAING,  J.ESCADOWNT ,  J.ESCARASS, J.ESCNORPAC, J.ESCVASPAC, J.ESCAPAPAC, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON, dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS, dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE, RTRIM(H.IPPRINOMB) AS IPPRINOMB, RTRIM(H.IPPRIAPEL) AS IPPRIAPEL
, RTRIM(E.UFUCODIGO) + ' - ' + RTRIM(E.UFUDESCRI) AS UFUCODIGO, RTRIM(E.UFUCODIGO) AS CodUnidadFuncional, iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = C.IPCODPACI and NUMINGRES = C.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
FROM dbo.CHCAMASHO A INNER JOIN dbo.ADcenaten D ON A.CODCENATE=D.CODCENATE INNER JOIN 
dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO 
LEFT OUTER JOIN dbo.CHREGESTA C ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
LEFT OUTER JOIN dbo.CHTIPESTA G ON G.CODTIPEST=C.CODTIPEST 
LEFT OUTER JOIN dbo.INPacient H ON C.IPCODPACI=H.IPCODPACI 
LEFT OUTER JOIN dbo.HCREGEGRE I ON C.NUMINGRES=I.NUMINGRES
LEFT OUTER JOIN dbo.ADINGRESO J ON C.NUMINGRES=J.NUMINGRES
LEFT OUTER JOIN dbo.INESPECIA K ON J.CODESPTRA=K.CODESPECI
WHERE A.CODCENATE=@CentroAtencion AND ESTADCAMA ='2' 
ORDER BY A.CODICAMAS, H.IPPRINOMB, H.IPPRIAPEL
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes hospitalizados (con cama asignada y en estado activo) en un centro de atención específico, indicando para cada uno si aún está en la unidad o ya tiene egreso registrado. Combina información de camas hospitalarias, unidades funcionales, historia clínica de estancias, datos demográficos del paciente, ingreso/admisión y especialidad tratante. Además calcula y expone los puntajes de escalas clínicas de riesgo (Down-Ton, RASS, Norton, VAS, Apache) y señala si el paciente tiene una recomendación activa. Se usa principalmente en el censo de camas y el tablero de enfermería para identificar pacientes activos, incluyendo aquellos con posibles homónimos (pacientes distintos con nombres similares) dentro de la misma unidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesConHomonimos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesConHomonimos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las camas ocupadas de un centro de atención mostrando información de pacientes asignados, su estado de ingreso/egreso, escalas clínicas y recomendaciones activas, para apoyar la gestión censal hospitalaria.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesConHomonimos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el centro de atención indicado en CHCAMASHO; Solo se consideran camas con ESTADCAMA=''2'' (ocupadas)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesConHomonimos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan camas en estado ocupada (''2''); El registro de estancia considerado es el activo (CHREGESTA.REGESTADO=1); El indicador de recomendación se activa únicamente cuando hay recomendaciones con Status=1; Alerta siempre se devuelve como ''Normal'' y Resultado siempre como 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesConHomonimos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cama hospitalaria; Clase de habitación; Clase de cama; Tipo de aislamiento; Tipo de estancia; Paciente; Ingreso hospitalario; Egreso; Unidad funcional; Especialidad de tratamiento; Escalas clínicas (Downton, RASS, Norton, VAS, Apache); Traslado a cirugía; Traslado para medicamentos; Recomendación de paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesConHomonimos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas para camas con ESTADCAMA=''2'' del centro de atención filtrado, junto con datos del paciente, ingreso, especialidad de tratamiento, escalas clínicas y bandera de recomendación activa', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesConHomonimos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si I.NUMINGRES IS NULL (no existe registro de egreso para el ingreso) → Marca la fila como ''1 - Pacientes en la Unidad'' else Marca la fila como ''2 - Pacientes Con Salida''; si Existe al menos un registro en RecommendPatient con Status=1 para el paciente e ingreso → Marca Recomendacion = 1 (true) else Marca Recomendacion = 0 (false)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesConHomonimos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPacient; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.INESPECIA; dbo.RecommendPatient', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesConHomonimos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesConHomonimos';
-- GO
