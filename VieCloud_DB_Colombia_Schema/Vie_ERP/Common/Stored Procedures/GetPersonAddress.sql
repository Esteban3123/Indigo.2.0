-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[GetPersonAddress]
	-- Add the parameters for the stored procedure here
	@IdentificationNumber Varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
    [Common].[Address].[Id], [Common].[Address].[Addresss] as AddressName, 
    [Common].[Address].[CityId], 
    [Common].[Address].[DepartmentId], 
    
    [Common].[City].[Name] as CityName, 
    [Common].[Department].[Name] as DepartmentName
    FROM [Common].[Address] 
    INNER JOIN [Common].[Person] 
    ON [Common].[Address].[IdPerson] = [Common].[Person].[Id] 
    INNER JOIN [Common].[City] 
    ON [Common].[City].[Id] = [Common].[Address].[CityId] 
    INNER JOIN [Common].[Department] 
    ON [Common].[Department].[Id] = [Common].[Address].[DepartmentId]
    WHERE[Common].[Person].[IdentificationNumber] = @IdentificationNumber;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta todas las direcciones registradas de una persona (paciente, profesional u otro actor) a partir de su número de identificación o cédula. Combina los datos de direcciones, ciudades y departamentos para devolver la ubicación completa de residencia o contacto. Se usa para obtener la información de domicilio de un individuo dado su documento de identidad, incluyendo el nombre de la ciudad y el departamento asociados a cada dirección registrada.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonAddress';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'GetPersonAddress';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene las direcciones registradas de una persona identificada por su número de identificación, incluyendo el nombre de la ciudad y del departamento asociados.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonAddress';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en el registro maestro con el número de identificación suministrado; Cada dirección debe tener una ciudad y un departamento válidos en los catálogos correspondientes', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonAddress';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna direcciones que tengan ciudad y departamento existentes en los catálogos (INNER JOIN excluye huérfanos); Solo retorna direcciones cuyo IdPerson corresponda a una persona existente', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonAddress';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Dirección; Ciudad; Departamento; Número de identificación', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonAddress';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Common.Address: Cuando Person.IdentificationNumber coincide con el parámetro, retorna las direcciones de esa persona enriquecidas con CityName y DepartmentName', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonAddress';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.Address; Common.Person; Common.City; Common.Department', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonAddress';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'GetPersonAddress';
-- GO
