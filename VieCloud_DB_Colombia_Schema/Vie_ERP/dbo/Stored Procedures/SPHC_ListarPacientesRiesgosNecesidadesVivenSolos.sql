
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesRiesgosNecesidadesVivenSolos]
(
@CentroAtencion Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT  CASE WHEN  I.NUMINGRES IS NULL THEN '1 - Pacientes en la Unidad' ELSE '2 - Pacientes Con Salida' END AS Egreso, 'Normal' as Alerta, A.CODICAMAS AS 'Codigo Cama',  RTRIM(DESCCAMAS) AS Cama,dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion,dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', C.IPCODPACI AS Identificacion,C.NUMINGRES AS Ingreso, dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento,RTRIM(DESTIPEST) AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS Paciente,CAST(0 AS BIT) AS Resultado, A.CAMTRACIR AS TrasladoCirugia, A.CAMTRAMED AS TrasladoMedicamentos, A.CODCONCEC AS Consecutivo,CAST('' as bit) AS MuestraAlerta,
J.CODESPTRA AS CodigoEspecialidad,RTRIM(K.DESESPECI) AS DescripcionEspecialidad,IFECHAING,  J.ESCADOWNT ,  J.ESCARASS, J.ESCNORPAC, J.ESCVASPAC, J.ESCAPAPAC, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON, dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS, dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE
, RTRIM(E.UFUCODIGO) + ' - ' + RTRIM(E.UFUDESCRI) AS UFUCODIGO, iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = C.IPCODPACI and NUMINGRES = C.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
FROM dbo.CHCAMASHO A INNER JOIN dbo.ADcenaten D ON A.CODCENATE=D.CODCENATE INNER JOIN 
dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO 
LEFT OUTER JOIN dbo.CHREGESTA C ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
LEFT OUTER JOIN dbo.CHTIPESTA G ON G.CODTIPEST=C.CODTIPEST 
LEFT OUTER JOIN dbo.INPacient H ON C.IPCODPACI=H.IPCODPACI 
LEFT OUTER JOIN dbo.HCREGEGRE I ON C.NUMINGRES=I.NUMINGRES
LEFT OUTER JOIN dbo.ADINGRESO J ON C.NUMINGRES=J.NUMINGRES
LEFT OUTER JOIN dbo.INESPECIA K ON J.CODESPTRA=K.CODESPECI
WHERE A.CODCENATE=@CentroAtencion AND ESTADCAMA ='2' AND J.VIVESOLO = 1
ORDER BY A.CODICAMAS
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes hospitalizados que viven solos, con sus riesgos clínicos y necesidades especiales, filtrando por centro de atención. Para cada cama ocupada muestra los datos del paciente (identificación, nombre, ingreso), el tipo de estancia y unidad funcional, la especialidad tratante, indicadores de traslado a cirugía o medicamentos, clase de habitación y tipo de aislamiento, y los puntajes de escalas clínicas de riesgo como Down-Ton, RASS, Norton, VAS y Apache. Distingue si el paciente sigue en la unidad o ya tiene salida registrada, e indica si cuenta con una recomendación activa. Compone información de camas (CHCAMASHO), estado de estancia (CHREGESTA), datos del paciente (INPACIENT), ingreso (ADINGRESO), egreso (HCREGEGRE), tipo de estancia (CHTIPESTA), unidad funcional (INUNIFUNC), centro de atención (ADCENATEN) y especialidad médica (INESPECIA), aplicando el filtro de pacientes que viven solos para apoyar la gestión de riesgo hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesRiesgosNecesidadesVivenSolos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesRiesgosNecesidadesVivenSolos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las camas ocupadas de un centro de atención cuyos pacientes ingresados viven solos, mostrando datos de estancia, escalas de valoración clínica y si tienen recomendaciones activas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesVivenSolos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en ADcenaten.; Solo se consideran camas con ESTADCAMA=''2'' (ocupada).; Solo se consideran ingresos donde VIVESOLO=1.; El registro de estancia se toma con REGESTADO=1 (estancia vigente).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesVivenSolos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre filtra por camas en estado ''2'' (ocupadas).; Siempre restringe a pacientes con condición ''vive solo'' (VIVESOLO=1).; Solo considera la estancia activa del paciente (REGESTADO=1).; El campo Alerta siempre se devuelve como ''Normal''.; Resultado y MuestraAlerta se devuelven siempre como BIT vacío/0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesVivenSolos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cama hospitalaria; Centro de atención; Unidad funcional; Ingreso/Egreso hospitalario; Aislamiento; Clase de habitación; Clase de cama; Especialidad médica; Escalas de valoración clínica (Down-Ton, RASS, Norton, VAS, APACHE); Riesgo: paciente que vive solo; Recomendación de interconsulta', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesVivenSolos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas de camas ocupadas (ESTADCAMA=''2'') del centro indicado cuyo ingreso tiene VIVESOLO=1, ordenadas por CODICAMAS.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesVivenSolos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si I.NUMINGRES IS NULL (no hay registro de egreso) → Marca Egreso=''1 - Pacientes en la Unidad'' else Marca Egreso=''2 - Pacientes Con Salida''; si Existe al menos un RecommendPatient con Status=1 para el paciente e ingreso → Recomendacion=1 (true) else Recomendacion=0 (false)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesVivenSolos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaNorton; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesVivenSolos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPacient; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.INESPECIA; dbo.RecommendPatient', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesVivenSolos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesVivenSolos';
-- GO
