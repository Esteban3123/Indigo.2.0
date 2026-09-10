

CREATE PROCEDURE [dbo].[SP_HC_ListarMedicamentos_GestionFarmaceutica] 
(
@Paciente Varchar(25),
@Ingreso  Char(10)
)
AS
BEGIN

	SET NOCOUNT ON;
	-------------------

	DECLARE @MaxFolio_OtrosMedicamentos nchar(10)
	SELECT @MaxFolio_OtrosMedicamentos = ISNULL(MAX(NUMEFOLIO),'0')  FROM HCNOSERFOTROMED AS A WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso ;
			   	
			SELECT
				A.ID,
				RTRIM(B.CODDCIMED) AS 'Codigo DCI',
				A.CODPROSAL,
				RTRIM(C.NOMMEDICO) AS 'Profesional',
				RTRIM(D.DESESPECI) AS 'Especialidad',
				A.CODPRODUC,
				RTRIM(B.DESPRODUC) AS 'Medicamento',
				RTRIM(A.ADMINISTRACION) AS 'Administracion',
				A.CANTIDAD AS 'Cantidad',
				A.PREESTADO AS 'Estado',
				A.OBSERVACIONES AS 'Observaciones',
				A.FECHAORDE AS 'Fecha',
				A.Justification,
				A.RejectedUser,
				A.RejectionDate			
			FROM 
				dbo.HCNOSERFOTROMED As A with(nolock) 
				INNER JOIN dbo.IHLISTPRO As B with(nolock) ON A.CODPRODUC = B.CODPRODUC 				
				INNER JOIN dbo.INPROFSAL AS C with(nolock) ON A.CODPROSAL = C.CODPROSAL
				INNER JOIN dbo.INESPECIA AS D with(nolock) ON C.CODESPEC1 = D.CODESPECI
		   WHERE 
				 A.IPCODPACI = @Paciente
				 AND A.NUMINGRES = @Ingreso
				 AND A.PREESTADO = 1
				 AND A.NUMEFOLIO = @MaxFolio_OtrosMedicamentos
				 				 				
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos activos del último folio de órdenes farmacéuticas registradas para un paciente en un ingreso específico. Consulta la historia clínica de medicamentos no seriados (HCNOSERFOTROMED), cruzando con el catálogo de productos farmacéuticos para obtener el nombre y código DCI del medicamento, con el maestro de profesionales de la salud para identificar al médico prescriptor, y con el catálogo de especialidades para mostrar la especialidad del profesional. Se usa en la gestión farmacéutica para visualizar las órdenes de medicamentos vigentes (estado activo) del ingreso hospitalario de un paciente, incluyendo dosis, cantidad, vía de administración, observaciones, justificación y datos de rechazo si aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarMedicamentos_GestionFarmaceutica';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarMedicamentos_GestionFarmaceutica';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los medicamentos vigentes (último folio, estado activo) ordenados a un paciente en un ingreso específico, mostrando profesional, especialidad y datos de la prescripción.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_GestionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente y el ingreso deben existir con registros en HCNOSERFOTROMED; Los productos deben estar en el catálogo IHLISTPRO; Los profesionales deben estar registrados en INPROFSAL con especialidad válida en INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_GestionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna registros del último folio (MAX) generado para el paciente e ingreso; Solo retorna medicamentos en estado activo (PREESTADO = 1); Si no existen folios previos, se asume folio ''0'' (ISNULL) y no retornará filas; Cada medicamento listado siempre tiene profesional y especialidad asociada (INNER JOIN obligatorio)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_GestionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; medicamento; prescripción / orden médica; profesional de salud; especialidad médica; folio de orden; gestión farmacéutica; justificación de medicamento; rechazo de medicamento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_GestionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCNOSERFOTROMED: Devuelve solo medicamentos cuyo PREESTADO = 1 y cuyo NUMEFOLIO coincide con el folio máximo del paciente/ingreso (MAX(NUMEFOLIO))', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_GestionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCNOSERFOTROMED; dbo.IHLISTPRO; dbo.INPROFSAL; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_GestionFarmaceutica';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarMedicamentos_GestionFarmaceutica';
-- GO
