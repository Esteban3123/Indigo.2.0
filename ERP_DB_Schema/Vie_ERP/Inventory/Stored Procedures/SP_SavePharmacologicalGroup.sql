


-- =============================================
-- Author:		Daniel Eduardo Arévalo
-- Create date: 03-04-2019
-- Description:	sp que se ejecuta para guardar o actualizar un Grupo Farmacológico en VIE y CRYSTAL
-- =============================================

CREATE PROCEDURE [Inventory].[SP_SavePharmacologicalGroup]
	@CodePharmaCologicalGroup as VARCHAR(20),
	@DescriptionPharmacologicalGroup as VARCHAR(100),
	@Status AS BIT,
	@CodeUser as varchar(20)
AS
BEGIN
		
	begin try
		
		if (select count(*) from Inventory.PharmacologicalGroup td where Code = @CodePharmaCologicalGroup) > 0 begin
			--Se actualiza el registro			
			UPDATE Inventory.PharmacologicalGroup
			SET [Name] = @DescriptionPharmacologicalGroup,
				[Status] = @Status,
				ModificationDate = [Common].[GETDATE](),
				ModificationUser = @CodeUser,
				CrystalPharmacologicalGroup = @CodePharmaCologicalGroup
			WHERE Code = @CodePharmaCologicalGroup
		END ELSE BEGIN
			--Se Inserta el registro
			INSERT INTO Inventory.PharmacologicalGroup(Code, [Name], [Status], CreationUser, CreationDate, CrystalPharmacologicalGroup)
			VALUES(@CodePharmaCologicalGroup, @DescriptionPharmacologicalGroup, @Status, @CodeUser, [Common].[GETDATE](), @CodePharmaCologicalGroup)
		END
		
		if exists(select 1 from dbo.IHGRUFARM where CODGRUFAR = @CodePharmaCologicalGroup)
		begin
			-- Actualizamos en Crystal
			UPDATE dbo.IHGRUFARM
			SET	DESGRUFAR = @DescriptionPharmacologicalGroup
			WHERE CODGRUFAR = @CodePharmaCologicalGroup
		end
		else begin
			-- Insertamos en Crystal
			INSERT INTO dbo.IHGRUFARM(CODGRUFAR, DESGRUFAR)
			VALUES(@CodePharmaCologicalGroup, @DescriptionPharmacologicalGroup)
		end

		select 0 as CodeMessage, 'Se guardó correctamente el Grupo Farmacológico con Código ' + @CodePharmaCologicalGroup as Message
	end try
	begin catch
		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch
END