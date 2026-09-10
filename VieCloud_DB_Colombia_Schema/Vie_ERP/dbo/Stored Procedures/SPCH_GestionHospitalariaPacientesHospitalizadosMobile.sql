
CREATE PROCEDURE [dbo].[SPCH_GestionHospitalariaPacientesHospitalizadosMobile]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT
CASE WHEN  I.NUMINGRES IS NULL THEN '1 - Pacientes en la Unidad' ELSE '2 - Pacientes Con Salida' END AS Egreso,
CASE CAST(L.TRIAGECLA AS CHAR) WHEN '1' THEN '1 - EMERGENCIA' WHEN '2' THEN '2 - URGENCIA MEDICA' WHEN '3' THEN '3 - URGENCIA DIFERIDA' WHEN '4' THEN '4 - NO URGENTE' END AS Triage,
 'Normal' as Alerta, A.CODICAMAS AS 'Codigo Cama',RTRIM(DESCCAMAS) AS Cama,dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion,dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', C.IPCODPACI AS Identificacion,C.NUMINGRES AS Ingreso, dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento,RTRIM(DESTIPEST) AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS Paciente,CAST(0 AS BIT) AS Resultado, A.CAMTRACIR AS TrasladoCirugia, A.CAMTRAMED AS TrasladoMedicamentos, A.CODCONCEC AS Consecutivo,CAST('' as bit) AS MuestraAlerta,
J.CODESPTRA AS CodigoEspecialidad,RTRIM(K.DESESPECI) AS DescripcionEspecialidad,IFECHAING,  J.ESCADOWNT , J.ESCABIERI , J.ESCARASS, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS ,
M.NOMDIAGNO as 'Diagnostico' , DATEDIFF(day,IFECHAING, [Common].[GETDATE]()) as 'DiasHospitalizado',
CASE
	when H.IPGRUPSAN is null then 'No registra Grupo Sanguineo'
	else H.IPGRUPSAN
END as 'GrupoSanguineo',
CASE
	when H.IPRHSANGR is null then 'No registra Rh'
	else H.IPRHSANGR
END as 'RhSanguineo'
FROM dbo.CHCAMASHO A
INNER JOIN dbo.ADcenaten D ON A.CODCENATE=D.CODCENATE
INNER JOIN dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO 
LEFT OUTER JOIN dbo.CHREGESTA C ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
LEFT OUTER JOIN dbo.CHTIPESTA G ON G.CODTIPEST=C.CODTIPEST 
LEFT OUTER JOIN dbo.INPacient H ON C.IPCODPACI=H.IPCODPACI 
LEFT OUTER JOIN dbo.HCREGEGRE I ON C.NUMINGRES=I.NUMINGRES
LEFT OUTER JOIN dbo.ADINGRESO J ON C.NUMINGRES=J.NUMINGRES
LEFT OUTER JOIN dbo.ADTRIAGEU L on L.IPCODPACI = J.IPCODPACI AND L.NUMINGRES = J.NUMINGRES
LEFT OUTER JOIN dbo.INESPECIA K ON J.CODESPTRA=K.CODESPECI
LEFT OUTER JOIN INDIAGNOS M on M.CODDIAGNO =  (SELECT TOP 1 CODDIAGNO FROM INDIAGNOP WHERE IPCODPACI = H.IPCODPACI AND NUMINGRES = J.NUMINGRES AND CODDIAPRI = 1)
WHERE A.CODCENATE=@CentroAtencion AND A.UFUCODIGO = @UnidadFuncional AND ESTADCAMA ='2' 
ORDER BY A.CODICAMAS
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento utilizado por la aplicación móvil de gestión hospitalaria para consultar en tiempo real el listado de pacientes hospitalizados en una unidad funcional y centro de atención específicos. Para cada cama ocupada muestra información clínica y operativa del paciente: identificación, nombre, número de ingreso, diagnóstico principal (CIE-10), clasificación de triage, tipo de estancia, clase de cama y habitación, aislamiento, especialidad médica tratante, días de hospitalización, grupo sanguíneo y factor Rh, así como puntajes de escalas clínicas (Downton y RASS). Integra datos de camas (CHCAMASHO), estados de estancia (CHREGESTA), admisiones (ADINGRESO), triage de urgencias (ADTRIAGEU), egreso hospitalario (HCREGEGRE) e información maestra del paciente (INPACIENT), diferenciando visualmente entre pacientes activos en la unidad y pacientes con salida ya registrada. Es el núcleo del censo hospitalario móvil, permitiendo al personal asistencial conocer el estado de ocupación y condición clínica de cada cama en su servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_GestionHospitalariaPacientesHospitalizadosMobile';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPCH_GestionHospitalariaPacientesHospitalizadosMobile';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las camas ocupadas de una unidad funcional con datos del paciente hospitalizado, su triage, especialidad, diagnóstico principal, escalas clínicas y estado de egreso, para visualización móvil.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer centro de atención y unidad funcional para filtrar las camas.; Existen funciones escalares dbo.ClaseHabitacion, dbo.ClaseCama, dbo.TipoAislamiento, dbo.PuntajeEscalaDownTon, dbo.PuntajeEscalaRass y [Common].[GETDATE] disponibles.; El registro de estancia activo se identifica con REGESTADO=1 en CHREGESTA.; El diagnóstico principal del ingreso se marca con CODDIAPRI=1 en INDIAGNOP.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen camas en estado ocupado (ESTADCAMA=''2'').; El diagnóstico mostrado corresponde al diagnóstico marcado como principal (CODDIAPRI=1) del ingreso del paciente.; Los días hospitalizados se calculan desde IFECHAING hasta la fecha actual del sistema.; Una cama puede aparecer aunque no tenga estancia activa (LEFT JOIN con CHREGESTA), mostrando datos vacíos del paciente.; Se marca alerta como ''Normal'' y resultado/MuestraAlerta como falsos por defecto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cama hospitalaria; Hospitalización; Egreso; Triage de urgencias; Clase de habitación; Clase de cama; Aislamiento; Tipo de estancia; Especialidad médica tratante; Diagnóstico principal; Escala Down-Ton; Escala RASS; Grupo sanguíneo y Rh; Días de hospitalización; Traslado a cirugía; Traslado de medicamentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve solo camas con ESTADCAMA=''2'' (ocupadas) del centro y unidad funcional indicados, ordenadas por código de cama.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si HCREGEGRE.NUMINGRES IS NULL (no hay egreso registrado) → Clasifica al paciente como ''1 - Pacientes en la Unidad'' else Clasifica al paciente como ''2 - Pacientes Con Salida''; si ADTRIAGEU.TRIAGECLA en {1,2,3,4} → Traduce a etiqueta: 1-EMERGENCIA, 2-URGENCIA MEDICA, 3-URGENCIA DIFERIDA, 4-NO URGENTE; si INPacient.IPGRUPSAN IS NULL → Reporta ''No registra Grupo Sanguineo'' else Devuelve el grupo sanguíneo del paciente; si INPacient.IPRHSANGR IS NULL → Reporta ''No registra Rh'' else Devuelve el Rh del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPacient; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.ADTRIAGEU; dbo.INESPECIA; dbo.INDIAGNOS; dbo.INDIAGNOP', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosMobile';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPCH_GestionHospitalariaPacientesHospitalizadosMobile';
-- GO
