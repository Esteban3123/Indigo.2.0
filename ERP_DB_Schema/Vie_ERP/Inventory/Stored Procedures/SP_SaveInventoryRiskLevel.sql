


-- =============================================
-- Author:		Daniel Eduardo Arévalo
-- Create date: 11-04-2019
-- Description:	sp que se ejecuta para guardar o actualizar un Nivel de Riesgo en VIE y CRYSTAL
-- =============================================

CREATE PROCEDURE [Inventory].[SP_SaveInventoryRiskLevel]
	@CodeRiskLevel as VARCHAR(20),
	@NameRiskLevel as VARCHAR(100),
	@Status AS BIT,
	@CodeUser as varchar(20)
AS
BEGIN
		
	begin try
		
		if (select count(*) from Inventory.[InventoryRiskLevel] td where Code = @CodeRiskLevel) > 0 begin
			--Se actualiza el registro			
			UPDATE Inventory.[InventoryRiskLevel]
			SET Code = @CodeRiskLevel,
				[Name] = @NameRiskLevel,
				[Status] = @Status,
				ModificationDate = [Common].[GETDATE](),
				ModificationUser = @CodeUser
			WHERE Code = @CodeRiskLevel			
		END ELSE BEGIN
			--Se Inserta el registro
			INSERT INTO Inventory.[InventoryRiskLevel](Code, [Name], [Status], CreationDate, CreationUser)
			VALUES(@CodeRiskLevel,@NameRiskLevel, @Status, [Common].[GETDATE](), @CodeUser)			
		END
		
		if exists(select 1 from .INIVERIES where CODNIVRIE = @CodeRiskLevel)
		begin
			-- Actualizamos en Crystal
			UPDATE dbo.INIVERIES
			SET	CODNIVRIE = @CodeRiskLevel,
				DESNIVRIE = @NameRiskLevel,
				ESTNIVRIE = @Status 
			WHERE CODNIVRIE = @CodeRiskLevel
		end
		else begin
			-- Insertamos en Crystal
			INSERT INTO dbo.INIVERIES(CODNIVRIE, DESNIVRIE, ESTNIVRIE)
			VALUES(@CodeRiskLevel, @NameRiskLevel, @Status)
		end

		select 0 as CodeMessage, 'Se guardó correctamente el Nivel del Riesgo ' + @CodeRiskLevel  as Message
	end try
	begin catch
		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch
END