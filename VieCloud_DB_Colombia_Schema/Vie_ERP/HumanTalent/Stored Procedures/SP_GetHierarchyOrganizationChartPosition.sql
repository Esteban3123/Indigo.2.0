-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [HumanTalent].[SP_GetHierarchyOrganizationChartPosition]
	-- Add the parameters for the stored procedure here
	@PositionImmediateBossId int
AS
BEGIN
	declare @TableVariable table(PositionImmediateBossId int)
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	--Select Organization Chart Position
	insert into @TableVariable select * from HumanTalent.[GetHierarchyOrganizationChartPosition](@PositionImmediateBossId)  

	--Select Employee
	SELECT e.Id, tp.Name
	FROM @TableVariable as t
	inner join [Payroll].[Contract]	as c
	on c.[OrganizationChartPositionId] = t.PositionImmediateBossId
	inner join [Payroll].[Employee] as e
	on c.[EmployeeId] = e.[Id]
	inner join [Common].[ThirdParty] as tp
	on e.[ThirdPartyId] = tp.[Id]  
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Obtiene la jerarquía de posiciones en el organigrama a partir de un cargo jefe inmediato dado, y retorna los empleados asignados a todas las posiciones subordinadas dentro de esa cadena jerárquica. Utiliza la función recursiva GetHierarchyOrganizationChartPosition para recorrer el árbol de posiciones hacia abajo, luego cruza esas posiciones con los contratos laborales (Payroll.Contract) para identificar qué empleados ocupan dichos cargos, y finalmente trae el nombre completo del empleado desde el registro de terceros (Common.ThirdParty). Se usa para consultar quiénes reportan directa o indirectamente a un jefe específico dentro de la estructura organizacional de talento humano, útil en procesos de nómina, aprobaciones y gestión de personal.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'PROCEDURE', @level1name = N'SP_GetHierarchyOrganizationChartPosition';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'PROCEDURE', @level1name = N'SP_GetHierarchyOrganizationChartPosition';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los empleados (Id y nombre del tercero) que pertenecen a la jerarquía organizacional descendente de un cargo/jefe inmediato dado, recorriendo el organigrama y cruzando con sus contratos.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetHierarchyOrganizationChartPosition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el identificador de posición/cargo del jefe inmediato recibido como parámetro y ser resoluble por la función HumanTalent.GetHierarchyOrganizationChartPosition.; Las tablas Payroll.Contract, Payroll.Employee y Common.ThirdParty deben tener integridad referencial entre OrganizationChartPositionId, EmployeeId y ThirdPartyId.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetHierarchyOrganizationChartPosition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan empleados cuyo contrato esté vinculado a una posición presente en la jerarquía descendente del jefe indicado (resultado de la función GetHierarchyOrganizationChartPosition).; Un empleado aparece tantas veces como contratos tenga asociados a posiciones dentro de la jerarquía (no hay DISTINCT).; Los INNER JOIN aseguran que solo se listan empleados con contrato vigente en el árbol y con tercero asociado en Common.ThirdParty.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetHierarchyOrganizationChartPosition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Jerarquía organizacional; Cargo / Posición organizacional; Jefe inmediato; Contrato laboral; Empleado; Tercero', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetHierarchyOrganizationChartPosition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Employee: Retorna Employee.Id y ThirdParty.Name de los empleados cuyos contratos (Payroll.Contract.OrganizationChartPositionId) coinciden con alguna de las posiciones devueltas por HumanTalent.GetHierarchyOrganizationChartPosition(@PositionImmediateBossId).', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetHierarchyOrganizationChartPosition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'HumanTalent.GetHierarchyOrganizationChartPosition', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetHierarchyOrganizationChartPosition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'HumanTalent.GetHierarchyOrganizationChartPosition; Payroll.Contract; Payroll.Employee; Common.ThirdParty', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetHierarchyOrganizationChartPosition';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_GetHierarchyOrganizationChartPosition';
-- GO
