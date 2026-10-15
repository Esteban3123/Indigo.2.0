-- =============================================
-- Author:		Daniel Eduardo Arévalo
-- Create date: 10-04-2019
-- Description:	sp que se ejecuta para guardar o actualizar Vía de Administración en VIE y CRYSTAL
-- =============================================

CREATE PROCEDURE [Inventory].[SP_SaveAdministrationRoute]
	@PharmaceuticalFormId as INT,
	@CodeAdministrationRoute as VARCHAR(20),
	@NameAdministrationRoute as VARCHAR(100),
	@Status AS BIT,
	@CodeUser as varchar(20)
AS
BEGIN
	DECLARE @CodeCrystal as VARCHAR(20) = NULL

	if @PharmaceuticalFormId = 0 set @PharmaceuticalFormId = NULL
	begin try
		SELECT @CodeCrystal = CrystalAdministrationRoute  FROM Inventory.AdministrationRoute WHERE Code = @CodeAdministrationRoute
		IF @CodeCrystal IS NULL
		BEGIN
			SELECT  @CodeCrystal = CODVIAADM FROM dbo.HCVIAADMI WHERE RTRIM(DESVIAADM) = convert(varchar(30),@NameAdministrationRoute)
		END

		if (select count(*) from Inventory.AdministrationRoute td where Code = @CodeAdministrationRoute) > 0 begin
			--Se actualiza el registro			
			UPDATE Inventory.AdministrationRoute
			SET	PharmaceuticalFormId = @PharmaceuticalFormId,
				[Name] = @NameAdministrationRoute,
				[Status] = @Status,
				ModificationDate = [Common].[GETDATE](),
				ModificationUser = @CodeUser
				,CrystalAdministrationRoute = ISNULL(@CodeCrystal, @CodeAdministrationRoute)
			WHERE Code = @CodeAdministrationRoute		
		END ELSE BEGIN
			--Se Inserta el registro
			INSERT INTO Inventory.AdministrationRoute( PharmaceuticalFormId, Code, [Name], [Status], CreationUser, CreationDate, CrystalAdministrationRoute)
			VALUES(@PharmaceuticalFormId, @CodeAdministrationRoute, @NameAdministrationRoute, @Status, @CodeUser, [Common].[GETDATE](), ISNULL(@CodeCrystal,@CodeAdministrationRoute))
		END
		
		--if exists(select 1 from dbo.HCVIAADMI where CODVIAADM = @CodeAdministrationRoute)
		IF @CodeCrystal IS NOT NULL
		begin
			-- Actualizamos en Crystal
			UPDATE dbo.HCVIAADMI
			SET	DESVIAADM = convert(varchar(30),@NameAdministrationRoute)
			--WHERE CODVIAADM = @CodeAdministrationRoute
			WHERE CODVIAADM = @CodeCrystal
		end
		else begin
			-- Insertamos en Crystal
			INSERT INTO dbo.HCVIAADMI(CODVIAADM, DESVIAADM)
			VALUES(@CodeAdministrationRoute, convert(varchar(30),@NameAdministrationRoute))
		end

		select 0 as CodeMessage, 'Se guardó correctamente la Vía de Administración con Código ' + @CodeAdministrationRoute as Message
	end try
	begin catch
		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch
END