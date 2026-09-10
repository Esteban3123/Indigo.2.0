
CREATE PROCEDURE [dbo].[SP_HC_ListarMedicamentosHistoricoGestionLogisticaFarmaceutica] 
(
@Paciente Varchar(25),
@Ingreso  Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-------------------
SELECT A.ID, A.NUMINGRES AS 'Agrupador', V.FECHAORDE AS 'FechaSolicitud', E.CODDCIMED, RTRIM(A.CODPRODUC) AS 'Codigo', RTRIM(E.DESPRODUC) AS 'Medicamentos', A.DESADMINI AS 'Administracion', A.FECFINDOS AS 'FechaInicio', A.FECFINDOS AS 'FechaFin', RTRIM(A.CODPROSAL) AS 'CodigoProfesional', 
RTRIM(B.NOMMEDICO) AS 'NombreProfesional', RTRIM(N.DESESPECI) AS 'Especialidad', RTRIM(MOTSUSMED) AS 'MotivoSuspension', RTRIM(K.CODPROSAL) AS 'CodigoProfesionalSusp', RTRIM(PROF.NOMMEDICO) AS 'NombreProfesionalSusp', 
CASE WHEN A.VALDURFIJ IS NULL THEN A.DURACIDOS ELSE CONVERT(varchar, A.VALDURFIJ) + ' ' + CASE A.UNIDURFIJ WHEN 1 THEN 'Minutos' WHEN 2 THEN 'Horas' WHEN 3 THEN 'Dias' WHEN 4 THEN 'Semanas' WHEN 5 THEN 'Meses' WHEN 6 THEN 'Años'  END END AS 'Duracion', RTRIM(A.INDAPLMED) AS 'Indicaciones'
FROM .dbo.HCPRESCRA A WITH(NOLOCK) 
INNER JOIN .dbo.INPROFSAL B WITH(NOLOCK) ON A.CODPROSAL = B.CODPROSAL 
INNER JOIN .dbo.IHLISTPRO E WITH(NOLOCK) ON A.CODPRODUC = E.CODPRODUC 
INNER JOIN .DBO.HCHISPACA J WITH(NOLOCK) ON A.IPCODPACI = J.IPCODPACI AND A.NUMINGRES = J.NUMINGRES AND A.NUMEFOLIO = J.NUMEFOLIO 
LEFT JOIN .DBO.HCHISPACA K WITH(NOLOCK) ON A.IPCODPACI = K.IPCODPACI AND A.NUMINGRES = K.NUMINGRES AND A.NUMFOLSUS = K.NUMEFOLIO 
LEFT JOIN .dbo.INPROFSAL PROF WITH(NOLOCK) ON K.CODPROSAL = PROF.CODPROSAL 
LEFT OUTER JOIN .dbo.INESPECIA N WITH(NOLOCK) ON J.CODESPTRA = N.CODESPECI 
LEFT JOIN .dbo.HCPRESCRC V WITH(NOLOCK) ON A.CODCONCEC = V.CODCONCEC
LEFT OUTER JOIN dbo.HCINTEMED inter	WITH(NOLOCK) ON inter.CODPRODUA IN (A.CODPRODUC, E.CODDCIMED) AND inter.CODPRODUB IN (A.CODPRODUC, E.CODDCIMED)
where A.IPCODPACI= @Paciente AND A.PREESTADO IN (2,3,4,7) AND A.MANEXTPRO = 0 
ORDER BY V.FECHAORDE DESC

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista el historial completo de medicamentos prescritos internamente para un paciente y un ingreso específico, orientado a la gestión logística y farmacéutica. Combina las prescripciones de medicamentos (HCPRESCRA) con el catálogo de productos farmacéuticos (IHLISTPRO), los datos del profesional prescriptor y del profesional que suspendió la orden (INPROFSAL), la especialidad del folio clínico (INESPECIA e HCHISPACA), el encabezado de la receta (HCPRESCRC) y las alertas de interacciones entre medicamentos (HCINTEMED). Devuelve para cada medicamento: código y nombre del producto, vía de administración, fechas de inicio y fin de la dosis, duración (en minutos, horas, días, semanas, meses o años), indicaciones, motivo de suspensión, profesional prescriptor y profesional que suspendió con su especialidad. Solo incluye prescripciones de uso interno (no externas) con estados activos, suspendidos o finalizados, ordenadas por fecha de solicitud más reciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarMedicamentosHistoricoGestionLogisticaFarmaceutica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarMedicamentosHistoricoGestionLogisticaFarmaceutica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista el histórico de medicamentos prescritos a un paciente con información clínica, profesional prescriptor, suspensión y duración, para gestión logística farmacéutica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosHistoricoGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente identificado para retornar resultados; Las prescripciones deben estar en estados 2, 3, 4 o 7 (PREESTADO); Las prescripciones no deben ser de manejo extra-institucional (MANEXTPRO = 0)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosHistoricoGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone prescripciones con estado en (2,3,4,7), excluyendo otros estados como anuladas/borrador; Excluye explícitamente prescripciones marcadas como manejo externo (MANEXTPRO=0); Filtra siempre por paciente recibido como parámetro; El resultado se ordena cronológicamente desde la solicitud más reciente; La unidad de duración fija se traduce a etiqueta legible en español', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosHistoricoGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Prescripción de medicamentos; Suspensión de medicamento; Motivo de suspensión; Profesional de la salud; Especialidad médica; Duración de tratamiento; Indicaciones de aplicación; Interacciones medicamentosas; DCI (Denominación Común Internacional); Folio de historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosHistoricoGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve prescripciones del paciente con PREESTADO IN (2,3,4,7) y MANEXTPRO = 0, ordenadas por FECHAORDE descendente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosHistoricoGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si A.VALDURFIJ IS NULL → Se reporta DURACIDOS como duración del medicamento else Se concatena VALDURFIJ con la unidad correspondiente según UNIDURFIJ (1=Minutos, 2=Horas, 3=Días, 4=Semanas, 5=Meses, 6=Años)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosHistoricoGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.INPROFSAL; dbo.IHLISTPRO; dbo.HCHISPACA; dbo.INESPECIA; dbo.HCPRESCRC; dbo.HCINTEMED', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosHistoricoGestionLogisticaFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentosHistoricoGestionLogisticaFarmaceutica';
-- GO
