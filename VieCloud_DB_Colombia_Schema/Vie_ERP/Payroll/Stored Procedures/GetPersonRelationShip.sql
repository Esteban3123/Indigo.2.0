-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Payroll].[GetPersonRelationShip]
	-- Add the parameters for the stored procedure here
	@EmployeeId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Payroll].[Employee].[Id]
      ,[EmployeeId]
      ,[Payroll].[Kinship].[Name]
	  ,[Payroll].[Relationship].[Name]  
      ,[DependsValue]      
	  FROM [Payroll].[Relationship]
	  INNER JOIN [Payroll].[Employee]
	  ON [Payroll].[Employee].[Id] =[Payroll].[Relationship].[EmployeeId]
	  INNER JOIN [Payroll].[Kinship]
	  ON [Payroll].[Kinship].[Id] = [Payroll].[Relationship].[KinshipId]
	  WHERE [Payroll].[Employee].[Id]= @EmployeeId;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los familiares y beneficiarios a cargo de un empleado específico de nómina, identificado por su ID de empleado. Combina la información del empleado con sus relaciones familiares registradas (cónyuge, hijos, padres, etc.) y el tipo de parentesco correspondiente, mostrando el nombre del vínculo familiar, el nombre del beneficiario y el valor o porcentaje del beneficio dependiente. Se utiliza para gestionar cargas familiares, beneficios de nómina y deducciones asociadas a dependientes de un trabajador.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'GetPersonRelationShip';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'GetPersonRelationShip';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los vínculos familiares (parentescos) y el valor de dependencia registrados para un empleado específico, combinando datos del empleado, su relación y el catálogo de parentescos.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationShip';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un empleado en Payroll.Employee cuyo Id coincida con el parámetro recibido para obtener resultados.; Las relaciones en Payroll.Relationship deben tener un KinshipId válido en Payroll.Kinship; de lo contrario quedan excluidas por el INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationShip';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven relaciones cuyo empleado existe en Payroll.Employee y cuyo parentesco existe en Payroll.Kinship (INNER JOIN obliga a integridad referencial).; El resultado se restringe siempre al empleado solicitado por el parámetro de entrada.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationShip';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Parentesco; Relación familiar; Beneficiario/Dependiente; Valor de dependencia', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationShip';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Relationship: Devuelve un conjunto con Id del empleado, EmployeeId de la relación, nombre del parentesco (Kinship), nombre de la relación y DependsValue, filtrando por Employee.Id = @EmployeeId.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationShip';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Relationship; Payroll.Employee; Payroll.Kinship', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationShip';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'GetPersonRelationShip';
-- GO
