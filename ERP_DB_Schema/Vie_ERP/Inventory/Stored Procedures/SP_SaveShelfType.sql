

-- =======================================================================================
-- Author:		Judy Andrea Díaz Reyes
-- Create date: 28-05-2019
-- Description:	sp que se ejecuta para guardar o actualizar un Tipo de Estante en VIE.
-- =======================================================================================

CREATE PROCEDURE [Inventory].[SP_SaveShelfType]
	@Code as VARCHAR(20),
	@Description as VARCHAR(100),
	@Large as Decimal(6,0),
	@Wide as Decimal(6,0),
	@Deep as Decimal(6,0),
	@PartitionXDeep as Decimal (6,0),
	@Partitions as Decimal(6,0),
	@LocationXPartition as Decimal(6,0),
	@State AS BIT,
	@CodeUser as varchar(20)
AS
BEGIN
		
	Begin Try
		If (Select count(*) From Inventory.ShelfType td Where Code = @Code) > 0 
		Begin
			-- Actualiza el registro
			UPDATE Inventory.ShelfType
			SET Code = @Code,
				[Description] = @Description,
				[large] = @Large,
				[Wide] = @Wide,
				[Deep] = @Deep,
				[PartitionXDeep] = @PartitionXDeep,
				[Partitions] = @Partitions,
				[LocationXPartition] = @LocationXPartition,
				[State] = @State,
				ModificationDate = [Common].[GETDATE](),
				ModificationUser = @CodeUser
			WHERE Code = @Code
		END ELSE BEGIN
			--Inserta el registro
			INSERT INTO Inventory.ShelfType (Code, [Description], [Large], [Wide], [Deep], [PartitionXDeep], [Partitions], [LocationXPartition],[State], CreationDate, CreationUser)
			VALUES (@Code, @Description, @Large, @Wide, @Deep, @PartitionXDeep, @Partitions, @LocationXPartition, @State, [Common].[GETDATE](), @CodeUser)
		END
		
		Select 0 as CodeMessage, 'Se guardó correctamente el Tipo de Estante con Código ' + @Code  as Message

	End Try
	Begin Catch
		Select 999 as CodeMessage, ERROR_MESSAGE() as Message
	End Catch
END