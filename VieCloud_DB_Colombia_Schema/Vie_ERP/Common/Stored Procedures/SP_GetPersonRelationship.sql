-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetPersonRelationship]
	-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20) 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [Payroll].[Relationship].[Id]     
      ,[Payroll].[Kinship].[Name] as KinshipName
	  ,[Payroll].[Relationship].[Name]  
      ,[DependsValue]      
	  FROM [Payroll].[Relationship]
	  INNER JOIN [Payroll].[Employee]
	  ON [Payroll].[Employee].[Id] =[Payroll].[Relationship].[EmployeeId]
	  INNER JOIN [Payroll].[Kinship]
	  ON [Payroll].[Kinship].[Id] = [Payroll].[Relationship].[KinshipId]
	  INNER JOIN [Common].[ThirdParty]
	  ON [Common].[ThirdParty].[Id] = [Payroll].[Employee].[ThirdPartyId]
	  INNER JOIN [Common].[Person]
	  ON [Common].[Person].[Id] = [Common].[ThirdParty].[PersonId]
	  WHERE [Common].[Person].[IdentificationNumber]= @IdentificationNumber;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta los familiares o beneficiarios a cargo de un empleado a partir de su número de identificación (cédula o documento). Recibe el número de identificación como parámetro, localiza al empleado en el registro maestro de personas y terceros, y retorna la lista de relaciones familiares vinculadas a ese empleado: el identificador del registro, el tipo de parentesco (cónyuge, hijo, padre, etc.), el nombre del beneficiario y el valor o porcentaje de beneficio que le corresponde. Se utiliza en procesos de nómina y gestión de beneficios para conocer las cargas familiares de un trabajador.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonRelationship';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonRelationship';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los familiares/beneficiarios registrados de un empleado, identificándolo por su número de identificación personal, junto con el tipo de parentesco y si genera dependencia económica.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonRelationship';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en Common.Person con el IdentificationNumber suministrado; La persona debe estar vinculada a un ThirdParty y éste a un Employee de nómina; Deben existir relaciones (Payroll.Relationship) asociadas al empleado con un Kinship válido', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonRelationship';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan relaciones cuyo empleado esté ligado a un ThirdParty con Person existente (INNER JOIN encadenado); Solo se retornan relaciones con un Kinship válido (INNER JOIN obligatorio); El filtro siempre se hace por IdentificationNumber de la Persona, no por Id de empleado', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonRelationship';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Empleado; Parentesco; Beneficiario/Familiar a cargo; Tercero; Persona; Dependencia económica (DependsValue); Identificación personal', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonRelationship';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Relationship: Cuando Person.IdentificationNumber coincide con el parámetro, retorna Id, nombre del parentesco (Kinship.Name), nombre de la relación y DependsValue de cada vínculo familiar del empleado', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonRelationship';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.Relationship; Payroll.Employee; Payroll.Kinship; Common.ThirdParty; Common.Person', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonRelationship';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonRelationship';
-- GO
