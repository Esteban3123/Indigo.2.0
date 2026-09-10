CREATE PROCEDURE [dbo].[SPHC_ListarPacientesSinAcompanantes]
(
@CentroAtencion Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT  CASE WHEN  I.NUMINGRES IS NULL THEN '1 - Pacientes en la Unidad' ELSE '2 - Pacientes Con Salida' END AS Egreso, 'Normal' as Alerta, A.CODICAMAS AS 'Codigo Cama',  RTRIM(DESCCAMAS) AS Cama,dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion,dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', C.IPCODPACI AS Identificacion,C.NUMINGRES AS Ingreso, dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento,RTRIM(DESTIPEST) AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS Paciente,CAST(0 AS BIT) AS Resultado, A.CAMTRACIR AS TrasladoCirugia, A.CAMTRAMED AS TrasladoMedicamentos, A.CODCONCEC AS Consecutivo,CAST('' as bit) AS MuestraAlerta,
J.CODESPTRA AS CodigoEspecialidad,RTRIM(K.DESESPECI) AS DescripcionEspecialidad,IFECHAING,  J.ESCADOWNT ,  J.ESCARASS, J.ESCNORPAC, J.ESCVASPAC, J.ESCAPAPAC, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON, dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS, dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE, L.IDACOMPAN
, RTRIM(E.UFUCODIGO) + ' - ' + RTRIM(E.UFUDESCRI) AS UFUCODIGO, iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = C.IPCODPACI and NUMINGRES = C.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
FROM dbo.CHCAMASHO A with(nolock) INNER JOIN dbo.ADcenaten D with(nolock) ON A.CODCENATE=D.CODCENATE INNER JOIN 
dbo.INUNIFUNC E with(nolock) ON A.UFUCODIGO=E.UFUCODIGO 
LEFT OUTER JOIN dbo.CHREGESTA C with(nolock) ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
LEFT OUTER JOIN dbo.CHTIPESTA G with(nolock) ON G.CODTIPEST=C.CODTIPEST 
LEFT OUTER JOIN dbo.INPacient H with(nolock) ON C.IPCODPACI=H.IPCODPACI 
LEFT OUTER JOIN dbo.HCREGEGRE I with(nolock) ON C.NUMINGRES=I.NUMINGRES
LEFT OUTER JOIN dbo.ADINGRESO J with(nolock) ON C.NUMINGRES=J.NUMINGRES
LEFT OUTER JOIN dbo.INESPECIA K with(nolock) ON J.CODESPTRA=K.CODESPECI
LEFT OUTER JOIN dbo.ADACOMPAN L with(nolock) ON J.NUMINGRES = L.NUMINGRES
WHERE A.CODCENATE=@CentroAtencion AND ESTADCAMA ='2' 
ORDER BY A.CODICAMAS
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todos los pacientes actualmente hospitalizados en camas ocupadas de un centro de atención, identificando cuáles NO tienen acompañante o responsable registrado. Para cada paciente muestra datos de la cama asignada (código, nombre, clase de habitación, clase de cama, tipo de aislamiento, unidad funcional), información del ingreso (número de ingreso, cédula, nombre completo, especialidad tratante, fecha de ingreso, si ya tiene egreso registrado), y los puntajes de escalas clínicas de riesgo (Down-Ton, RASS, Norton, VAS, Apache). Combina información de camas (CHCAMASHO), centros de atención (ADCENATEN), unidades funcionales (INUNIFUNC), estados de estancia del paciente (CHREGESTA), tipos de estancia (CHTIPESTA), datos del paciente (INPACIENT), egresos (HCREGEGRE), ingresos (ADINGRESO), especialidades (INESPECIA) y acompañantes (ADACOMPAN), permitiendo al personal de enfermería y coordinación hospitalaria detectar pacientes sin acompañante y gestionar alertas de seguridad y cuidado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesSinAcompanantes';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListarPacientesSinAcompanantes';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las camas ocupadas de un centro de atención con datos del paciente, ingreso, escalas clínicas y acompañante, para identificar pacientes sin acompañante registrado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesSinAcompanantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el centro de atención indicado en CHCAMASHO; Solo se consideran camas con ESTADCAMA=''2'' (ocupada)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesSinAcompanantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna camas con estado ''2'' (ocupada); Solo considera registros de estancia activos (CHREGESTA.REGESTADO = 1); Filtra estrictamente por el centro de atención solicitado; Resultado ordenado por código de cama', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesSinAcompanantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cama hospitalaria; Centro de atención; Unidad funcional; Ingreso hospitalario; Egreso; Acompañante; Especialidad médica; Aislamiento; Clase de habitación; Clase de cama; Tipo de estancia; Escalas clínicas (Down-Ton, RASS, Norton, VAS, Apache); Recomendación de interconsulta; Traslado a cirugía; Traslado de medicamentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesSinAcompanantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas de camas con ESTADCAMA=''2'' del centro indicado, marcando Egreso=''1 - Pacientes en la Unidad'' cuando HCREGEGRE.NUMINGRES IS NULL y ''2 - Pacientes Con Salida'' en caso contrario; [RETURN_RESULT] resultset: Marca Recomendacion=1 cuando existe al menos un registro en RecommendPatient con Status=1 para el paciente e ingreso; de lo contrario 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesSinAcompanantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si I.NUMINGRES IS NULL (no existe registro de egreso) → Egreso = ''1 - Pacientes en la Unidad'' else Egreso = ''2 - Pacientes Con Salida''; si Existe RecommendPatient con Status=1 para el paciente e ingreso → Recomendacion = 1 else Recomendacion = 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesSinAcompanantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaNorton; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesSinAcompanantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPacient; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.INESPECIA; dbo.ADACOMPAN; dbo.RecommendPatient', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesSinAcompanantes';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListarPacientesSinAcompanantes';
-- GO
