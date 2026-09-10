-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Common].[SP_GetPersonAddress]
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y retorna las direcciones registradas de una persona (paciente, profesional u otro actor del sistema) a partir de su número de identificación o cédula. Combina la información de direcciones con los catálogos de ciudades y departamentos para devolver la dirección completa, incluyendo el nombre del municipio y del departamento de residencia o contacto. Se usa para obtener la ubicación geográfica de una persona cuando se conoce su documento de identidad, útil en procesos de admisión, contacto o verificación de datos demográficos.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonAddress';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'PROCEDURE', @level1name = N'SP_GetPersonAddress';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene las direcciones registradas de una persona, junto con la ciudad y el departamento asociados, identificándola por su número de identificación.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonAddress';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La persona debe existir en Common.Person con el número de identificación suministrado.; Cada dirección debe tener CityId y DepartmentId válidos en los catálogos Common.City y Common.Department (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonAddress';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan direcciones que tienen ciudad y departamento existentes en los catálogos (debido al INNER JOIN).; El filtro se realiza por número de identificación de la persona, no por su Id interno.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonAddress';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Persona; Dirección; Ciudad; Departamento; Número de identificación', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonAddress';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve Id, AddressName, CityId, DepartmentId, CityName y DepartmentName de las direcciones cuyo IdPerson corresponde a la persona con IdentificationNumber = @IdentificationNumber.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonAddress';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.Address; Common.Person; Common.City; Common.Department', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonAddress';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'PROCEDURE', @level1name=N'SP_GetPersonAddress';
-- GO
