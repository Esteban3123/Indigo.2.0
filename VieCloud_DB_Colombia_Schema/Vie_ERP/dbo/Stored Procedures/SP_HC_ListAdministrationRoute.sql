
CREATE PROCEDURE [dbo].[SP_HC_ListAdministrationRoute]
( @Medicamento VARCHAR(100)
) 
AS 
BEGIN
	SET NOCOUNT ON;

	SELECT RTRIM(ltrim(VA.CODVIAADM)) AS Codigo, RTRIM(VA.DESVIAADM) AS Via 
	FROM Inventory.ATC ATC WITH(NOLOCK)
	INNER JOIN Inventory.ATCAdministrationRoute ATCAR  WITH(NOLOCK)
	ON ATCAR.ATCId = ATC.Id
	INNER JOIN Inventory.AdministrationRoute AR  WITH(NOLOCK)
	ON AR.Id = ATCAR.AdministrationRouteId
	AND AR.[Status] = 1
	INNER JOIN dbo.HCVIAADMI VA  WITH(NOLOCK)
	ON VA.CODVIAADM = AR.CrystalAdministrationRoute
	WHERE ATC.Code in (@Medicamento)
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las vías de administración válidas y activas para un medicamento específico, identificado por su código ATC. Combina el catálogo de medicamentos (ATC), la relación de vías permitidas por medicamento (ATCAdministrationRoute) y el catálogo de vías de administración (AdministrationRoute), cruzando con el maestro de vías del módulo de historia clínica (HCVIAADMI) para devolver el código y la descripción en el formato que reconoce el sistema clínico. Se utiliza en la prescripción médica y farmacéutica para presentar al profesional de salud las rutas de administración habilitadas (oral, intravenosa, intramuscular, etc.) según el medicamento seleccionado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListAdministrationRoute';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListAdministrationRoute';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las vías de administración válidas y activas asociadas a un medicamento identificado por su código ATC, devolviendo el código y descripción según el catálogo de historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código ATC recibido debe existir en Inventory.ATC; Debe haber correspondencia entre la vía de administración (AdministrationRoute.CrystalAdministrationRoute) y el catálogo dbo.HCVIAADMI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna vías de administración con Status = 1 (activas) en Inventory.AdministrationRoute; Solo retorna vías que tengan equivalencia mapeada (CrystalAdministrationRoute) hacia el catálogo dbo.HCVIAADMI; Aplica RTRIM/LTRIM para entregar códigos y descripciones sin espacios', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento; Código ATC; Vía de administración; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.ATC: Devuelve código y descripción de las vías de administración (HCVIAADMI) cruzadas con ATC cuyo Code coincide con el parámetro y cuya AdministrationRoute.Status = 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ATC; Inventory.ATCAdministrationRoute; Inventory.AdministrationRoute; dbo.HCVIAADMI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListAdministrationRoute';
-- GO
