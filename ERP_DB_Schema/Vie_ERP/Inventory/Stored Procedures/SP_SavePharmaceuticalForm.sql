-- =============================================
-- Author:		Daniel Eduardo Arévalo
-- Create date: 10-04-2019
-- Description:	sp que se ejecuta para guardar o actualizar Forma Farmaceutica en VIE y CRYSTAL
-- =============================================

CREATE PROCEDURE [Inventory].[SP_SavePharmaceuticalForm]
	@Xml xml,
	@CodeUser varchar(20)

AS
BEGIN
		
	declare
		@Code as VARCHAR(20),
		@Name as VARCHAR(100),
		@Status AS BIT,
		@RequireStability AS BIT,
		@PharmaceuticalFormGroupingId AS INT

	begin try
		
		select
			@Code = t.x.value('Code[1]','varchar(20)'),
			@Name = t.x.value('Name[1]','varchar(100)'),
			@Status = t.x.value('Status[1]','bit'),
			@RequireStability = t.x.value('RequireStability[1]','bit'),
			@PharmaceuticalFormGroupingId = t.x.value('PharmaceuticalFormGroupingId[1]','int')
		from @Xml.nodes('/PharmaceuticalForm') t(x)

		if (select count(*) from Inventory.PharmaceuticalForm td where Code = @Code) > 0 begin
			--Se actualiza el registro			
			UPDATE Inventory.PharmaceuticalForm
			SET [Name] = @Name,
				[Status] = @Status,
				ModificationDate = [Common].[GETDATE](),
				ModificationUser = @CodeUser,
				CrystalMedicalForm = @Code,
				RequireStability = @RequireStability,
				PharmaceuticalFormGroupingId = @PharmaceuticalFormGroupingId
			WHERE Code = @Code
		END ELSE BEGIN
			--Se Inserta el registro
			INSERT INTO Inventory.PharmaceuticalForm(Code, [Name], [Status], CreationUser, CreationDate, CrystalMedicalForm, RequireStability, PharmaceuticalFormGroupingId)
			VALUES(@Code, @Name, @Status, @CodeUser, [Common].[GETDATE](), @Code, @RequireStability, @PharmaceuticalFormGroupingId)
		END
		
		
		if exists(select 1 from dbo.IHFORMEDI where CODFORMED = @Code)
		begin
			-- Actualizamos en Crystal
			UPDATE dbo.IHFORMEDI
			SET	DESFORMED = @Name
			,RequireStability = @RequireStability
			WHERE CODFORMED = @Code
		end
		else begin
			-- Insertamos en Crystal
			INSERT INTO dbo.IHFORMEDI(CODFORMED, DESFORMED, RequireStability)
			VALUES(@Code, @Name, @RequireStability)
		end

		select 0 as CodeMessage, 'Se guardó correctamente la Forma Farmacéutica con Código ' + @Code as Message
	end try
	begin catch
		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch
END