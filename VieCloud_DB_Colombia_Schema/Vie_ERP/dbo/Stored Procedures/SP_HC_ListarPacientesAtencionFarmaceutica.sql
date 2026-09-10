

-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarPacientesAtencionFarmaceutica] 
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT DISTINCT CASE WHEN  I.NUMINGRES IS NULL THEN '1 - Pacientes en la Unidad' ELSE '2 - Pacientes Con Salida' END AS Egreso, 'Normal' as Alerta, A.CODICAMAS AS 'Codigo Cama',RTRIM(DESCCAMAS) AS Cama,dbo.ClaseHabitacion(A.CODCLAHAB) AS ClaseHabitacion,dbo.ClaseCama(A.CODCLACAM) AS 'Clase de Cama', C.IPCODPACI AS Identificacion,C.NUMINGRES AS Ingreso, dbo.TipoAislamiento(A.CODAISLAM) AS Aislamiento,RTRIM(DESTIPEST) AS 'Tipo Estancia',RTRIM(IPNOMCOMP) AS Paciente,CAST(0 AS BIT) AS Resultado, A.CAMTRACIR AS TrasladoCirugia, A.CAMTRAMED AS TrasladoMedicamentos, A.CODCONCEC AS Consecutivo,CAST('' as bit) AS MuestraAlerta,
J.CODESPTRA AS CodigoEspecialidad,RTRIM(K.DESESPECI) AS DescripcionEspecialidad,IFECHAING ,MEDTRAZA, 
J.ESCADOWNT , J.ESCARASS , J.ESCVASPAC, J.ESCAPAPAC, ESCNORPAC, dbo.PuntajeEscalaDownTon(J.NUMINGRES, J.IPCODPACI) AS PUNTAJEDOWNT, dbo.PuntajeEscalaRass(J.NUMINGRES, J.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaVas(J.NUMINGRES, J.IPCODPACI) as PUNTAJEVAS, dbo.PuntajeEscalaApache(J.NUMINGRES, J.IPCODPACI) as PUNTAJEAPACHE, dbo.PuntajeEscalaNorton(J.NUMINGRES, J.IPCODPACI) as PUNTAJENORTON
FROM dbo.CHCAMASHO A 
INNER JOIN dbo.ADcenaten D ON A.CODCENATE=D.CODCENATE 
INNER JOIN dbo.INUNIFUNC E ON A.UFUCODIGO=E.UFUCODIGO 
LEFT OUTER JOIN dbo.CHREGESTA C ON A.CODICAMAS=C.CODICAMAS AND C.REGESTADO = 1 
LEFT OUTER JOIN dbo.CHTIPESTA G ON G.CODTIPEST=C.CODTIPEST 
LEFT OUTER JOIN dbo.INPacient H ON C.IPCODPACI=H.IPCODPACI 
LEFT OUTER JOIN dbo.HCREGEGRE I ON C.NUMINGRES=I.NUMINGRES
LEFT OUTER JOIN dbo.ADINGRESO J ON C.NUMINGRES=J.NUMINGRES
LEFT OUTER JOIN dbo.INESPECIA K ON J.CODESPTRA=K.CODESPECI
inner join dbo.HCPRESCRA L ON H.IPCODPACI = L.IPCODPACI
LEFT OUTER JOIN dbo.IHLISTPRO M ON L.CODPRODUC= M.CODPRODUC

WHERE A.CODCENATE=@CentroAtencion AND A.UFUCODIGO =@UnidadFuncional AND ESTADCAMA ='2' 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los pacientes hospitalizados que tienen prescripciones farmacéuticas activas en una unidad funcional y centro de atención específicos, mostrando su cama asignada, tipo de estancia, especialidad médica tratante y puntajes de escalas clínicas (Down-Ton, RASS, VAS, Apache, Norton). Compone información de camas (CHCAMASHO), estados de ingreso (CHREGESTA), datos del paciente (INPACIENT), egreso hospitalario (HCREGEGRE), admisión (ADINGRESO), especialidades (INESPECIA) y prescripciones de medicamentos (HCPRESCRA), filtrando solo camas ocupadas. Es utilizado por el módulo de atención farmacéutica para que el químico farmacéutico o enfermero identifique qué pacientes hospitalizados tienen medicamentos prescritos, diferenciando entre pacientes activos en la unidad y pacientes con salida ya registrada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarPacientesAtencionFarmaceutica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los pacientes ocupando camas activas en una unidad funcional para seguimiento de atención farmacéutica, incluyendo datos clínicos, escalas de valoración y estado de egreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer un centro de atención y una unidad funcional válidos para filtrar las camas.; Las camas consideradas deben estar en estado ''2'' (ocupadas).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran camas con ESTADCAMA=''2''.; El cruce con CHREGESTA se restringe a registros activos (REGESTADO=1).; El campo ''Alerta'' siempre se devuelve como literal ''Normal''.; El campo ''Resultado'' siempre se devuelve como BIT 0.; Se aplica DISTINCT para evitar duplicados generados por el JOIN con prescripciones y productos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cama hospitalaria; Clase de habitación; Clase de cama; Aislamiento; Tipo de estancia; Ingreso hospitalario; Egreso; Especialidad médica; Prescripción de medicamentos; Atención farmacéutica; Escala Downton; Escala RASS; Escala VAS (dolor); Escala APACHE; Escala Norton; Traslado a cirugía; Traslado de medicamentos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve filas DISTINCT de camas ocupadas (ESTADCAMA=''2'') del centro y unidad solicitados, con datos del paciente, ingreso, especialidad, escalas y clasificación de egreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si I.NUMINGRES IS NULL (no existe registro en HCREGEGRE) → Marca al paciente como ''1 - Pacientes en la Unidad'' else Marca al paciente como ''2 - Pacientes Con Salida''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.ClaseHabitacion; dbo.ClaseCama; dbo.TipoAislamiento; dbo.PuntajeEscalaDownTon; dbo.PuntajeEscalaRass; dbo.PuntajeEscalaVas; dbo.PuntajeEscalaApache; dbo.PuntajeEscalaNorton', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHCAMASHO; dbo.ADcenaten; dbo.INUNIFUNC; dbo.CHREGESTA; dbo.CHTIPESTA; dbo.INPacient; dbo.HCREGEGRE; dbo.ADINGRESO; dbo.INESPECIA; dbo.HCPRESCRA; dbo.IHLISTPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesAtencionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarPacientesAtencionFarmaceutica';
-- GO
