-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Security].[SP_GetFormFolder]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
	[SelfService].[FormFolder].[Id] as FormFolderId, 
	[SelfService].[FormFolder].[Name] as FormFolderName, 
	[SelfService].[FormFolder].[Code] as FormFolderCode, 
	[SelfService].[Form].[Id] as FormId, 
	[SelfService].[Form].[Name] as FormName, 
	[SelfService].[Form].[Code] as FormCode, 
	[SelfService].[Action].[Id] as ActionId, 
	[SelfService].[Action].[Name] as ActionName, 
	[SelfService].[Action].[Code] as ActionCode 
	FROM[SelfService].[FormFolder] 
	INNER JOIN[SelfService].[Form] 
	ON[SelfService].[FormFolder].[Id] = [SelfService].[Form].[FormFolderId] 
	INNER JOIN[SelfService].[FormAction] 
	ON[SelfService].[FormAction].[FormId] = [SelfService].[Form].[Id] 
	INNER JOIN[SelfService].[Action] 
	ON[SelfService].[FormAction].[ActionId] = [SelfService].[Action].[Id];	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene la estructura completa de carpetas de formularios del portal de autoservicio, incluyendo los formularios contenidos en cada carpeta y las acciones disponibles para cada formulario. Combina las entidades de carpeta (FormFolder), formulario (Form) y acción (Action) en un único resultado jerárquico. Se utiliza para cargar el menú o árbol de navegación de formularios y sus operaciones permitidas dentro del módulo de autoservicio.', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetFormFolder';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'PROCEDURE', @level1name = N'SP_GetFormFolder';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el catálogo jerárquico de carpetas de formularios con sus formularios y acciones asociadas para uso en seguridad/menús de la aplicación.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormFolder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir relaciones consistentes entre FormFolder→Form→FormAction→Action (las filas sin acción asociada no se devuelven por uso de INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormFolder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen formularios que pertenecen a una carpeta y que tienen al menos una acción registrada en FormAction.; Cada fila del resultado representa una combinación carpeta-formulario-acción válida.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormFolder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Carpeta de formularios; Formulario; Acción; Asociación formulario-acción', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormFolder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] SelfService.FormFolder: Retorna un resultset combinando FormFolder, Form y Action mediante INNER JOIN a través de FormAction.', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormFolder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'SelfService.FormFolder; SelfService.Form; SelfService.FormAction; SelfService.Action', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormFolder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Security', @level1type=N'PROCEDURE', @level1name=N'SP_GetFormFolder';
-- GO
