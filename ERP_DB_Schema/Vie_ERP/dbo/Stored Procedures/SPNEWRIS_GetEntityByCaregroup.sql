-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SPNEWRIS_GetEntityByCaregroup] 
(
    -- Add the parameters for the stored procedure here
@CareGroup int -- = Null
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    -- SET NOCOUNT ON

    -- Insert statements for procedure here
	BEGIN
		DECLARE @careGroupType int = (Select CareGroupType FROM Contract.CareGroup where id = @CareGroup)
		DECLARE @EntityType int = (Select EntityType FROM Contract.CareGroup WHERE id = @CareGroup)
		DECLARE @fecha datetime = [Common].[GETDATE]() + 1
	END

	IF @careGroupType = 1
	BEGIN 
		select CAST(Car.CareGroupType AS int), Con.HealthAdministratorId, Hea.Name as HealthAdministratorDescription, 
				Con.ContractName as ContractDescription, Car.EntityType, Con.EndDate, Con.Code As ContractCode 
			from Contract.CareGroup as Car
				left join Contract.Contract as Con on Con.Id = Car.ContractId
				--left join Contract.ContractEntity as ce on ce.Id = cc.ContractEntityId
				left join Contract.HealthAdministrator as Hea on Hea.Id = Con.HealthAdministratorId
			where Car.id = @CareGroup
	END

	IF @careGroupType = 2
	BEGIN 
		select CAST(@careGroupType AS int), id, Name, Name, EntityType, @fecha, Code from Contract.HealthAdministrator where EntityType = @EntityType and Status = 1
	END

	IF @careGroupType = 3
	BEGIN 
		select CAST(@careGroupType AS int), id, Name, Name, EntityType, @fecha, '999' from Contract.HealthAdministrator where Code = '999' and Status = 1
	END

	IF @careGroupType = 4
	BEGIN
		select CAST(@careGroupType AS int), id, Name, Name, EntityType, @fecha, Code from Contract.HealthAdministrator where EntityType = @EntityType and Status = 1
	END

END