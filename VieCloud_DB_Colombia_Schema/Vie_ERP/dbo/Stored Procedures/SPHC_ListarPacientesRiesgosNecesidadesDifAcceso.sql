
CREATE PROCEDURE [dbo].[SPHC_ListarPacientesRiesgosNecesidadesDifAcceso]
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
WHERE A.CODCENATE=@CentroAtencion AND ESTADCAMA ='2' AND H.ZONAPARTADA = 1
ORDER BY A.CODICAMAS
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes hospitalizados que tienen marcada la bandera de zona apartada (necesidades o dificultades de acceso especiales), filtrando por centro de atención. Para cada paciente muestra la cama asignada con su clase y habitación, el tipo de estancia, la especialidad médica tratante, la fecha de ingreso y si ya tiene egreso registrado. Además calcula y expone los puntajes de las escalas clínicas de riesgo: caídas (Downton), sedación/agitación (RASS), úlceras por presión (Norton), dolor (VAS) y severidad (APACHE), junto con el tipo de aislamiento y si el paciente cuenta con una recomendación activa. Sirve principalmente para que enfermería o coordinación de pisos monitoree en tiempo real el censo hospitalario de pacientes con condiciones especiales de atención, riesgos y necesidades diferenciadas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesRiesgosNecesidadesDifAcceso';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesRiesgosNecesidadesDifAcceso';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las camas ocupadas de un centro de atención con datos del paciente, escalas clínicas (Downton, Rass, Norton, Vas, Apache), egreso, especialidad y recomendaciones, restringido a pacientes en zona apartada (riesgo/dificultad de acceso).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesDifAcceso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención debe existir en CHCAMASHO; Solo se consideran camas con ESTADCAMA=''2'' (ocupadas); El paciente asociado debe tener INPacient.ZONAPARTADA = 1; El registro de estancia debe estar activo (CHREGESTA.REGESTADO = 1)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesDifAcceso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan camas en estado ocupada (''2''); Solo se listan pacientes con marca de zona apartada (ZONAPARTADA=1); Solo se considera la estancia activa de la cama (REGESTADO=1); Resultados ordenados por código de cama; Las puntuaciones de escalas (Downton, Rass, Norton, Vas, Apache) se calculan vía funciones escalares por paciente e ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesDifAcceso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cama hospitalaria; Centro de atención; Unidad funcional; Ingreso hospitalario; Egreso; Estancia; Aislamiento; Clase de habitación; Clase de cama; Especialidad médica; Escala Downton; Escala Rass; Escala Norton; Escala Vas; Escala Apache; Traslado de cirugía; Traslado de medicamentos; Recomendación de interconsulta; Zona apartada (dificultad de acceso)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesDifAcceso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna conjunto de camas ocupadas del centro de atención dado, con clasificación ''Egreso'' = ''1 - Pacientes en la Unidad'' cuando HCREGEGRE.NUMINGRES IS NULL y ''2 - Pacientes Con Salida'' en caso contrario; [RETURN_RESULT] resultset: Marca ''Recomendacion'' = 1 cuando existe al menos un registro en RecommendPatient con Status=1 para el paciente e ingreso, de lo contrario 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesDifAcceso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si I.NUMINGRES IS NULL (no existe registro de egreso) → Etiqueta paciente como ''1 - Pacientes en la Unidad'' else Etiqueta paciente como ''2 - Pacientes Con Salida''; si Existe registro en RecommendPatient con Status=1 para (IPCODPACI, NUMINGRES) → Marca campo Recomendacion como verdadero else Marca Recomendacion como falso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesDifAcceso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaNorton; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesDifAcceso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPacient; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.INESPECIA; dbo.RecommendPatient', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesDifAcceso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesRiesgosNecesidadesDifAcceso';
-- GO
