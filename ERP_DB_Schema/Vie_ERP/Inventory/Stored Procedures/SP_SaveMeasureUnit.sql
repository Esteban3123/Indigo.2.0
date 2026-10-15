


-- =============================================
-- Author:		Daniel Eduardo Arévalo
-- Create date: 03-04-2019
-- Description:	sp que se ejecuta para guardar o actualizar una Unidad de Medida en VIE y CRYSTAL
-- =============================================

CREATE PROCEDURE [Inventory].[SP_SaveMeasureUnit]
	@CodeMeasurementUnit as VARCHAR(20),
	@DescriptionMeasurementUnit as VARCHAR(100),
	@Abbreviation as VARCHAR(10),
	@UnitType AS Tinyint,
	@Status AS BIT,
	@AllowEditCostValue as BIT,
	@CostValue decimal(18,4),
	@CodeUser as varchar(20),
	@RequiresStandardCode as BIT,
	@StandardCode as varchar (5)
AS
BEGIN
		
	begin try


		DECLARE @MeasurementUnit NVARCHAR(200);
		DECLARE @ExistMeasurementUnit bit = 0;
		DECLARE @ErrorMessage NVARCHAR(200);
		DECLARE @DuplicatedMeasurementUnit NVARCHAR(200);
-- Intentamos obtener el nombre de la unidad de medida con la abreviatura específica
		SELECT @MeasurementUnit = NAME 
		FROM Inventory.InventoryMeasurementUnit 
		WHERE Abbreviation COLLATE Latin1_General_CI_AS = @Abbreviation COLLATE Latin1_General_CI_AS;


		IF (select count(*) from Inventory.InventoryMeasurementUnit td where Code = @CodeMeasurementUnit) > 0 
		BEGIN
			SELECT @ExistMeasurementUnit = 1,@DuplicatedMeasurementUnit = [Name]					
				FROM Inventory.InventoryMeasurementUnit
				WHERE Abbreviation COLLATE Latin1_General_CI_AS = @Abbreviation COLLATE Latin1_General_CI_AS AND Code <> @CodeMeasurementUnit;

				IF @ExistMeasurementUnit = 1
				BEGIN
					SET @ErrorMessage = 'La abreviación ya existe para la unidad de medida ' + @DuplicatedMeasurementUnit
					RAISERROR(@ErrorMessage, 16, 1);
				END
		
			--Se actualiza el registro
			
			UPDATE Inventory.InventoryMeasurementUnit
			SET Code = @CodeMeasurementUnit,
				[Name] = @DescriptionMeasurementUnit,
				Abbreviation = @Abbreviation,
				UnitType = @UnitType,
				[Status] = @Status,
				AllowEditCostValue = @AllowEditCostValue,
				CostValue = @CostValue,
				ModificationDate = [Common].[GETDATE](),
				ModificationUser = @CodeUser,
				CrystalMeasurementUnit = @CodeMeasurementUnit,
				RequiresStandardCode = @RequiresStandardCode,
				StandardCode = @StandardCode
			WHERE Code = @CodeMeasurementUnit

		END ELSE BEGIN
		IF @MeasurementUnit IS NOT NULL
		BEGIN
			
			SET @ErrorMessage = 'La Abreviatura ' + @Abbreviation + ' ya se encuentra descrita en la Unidad de Medida ' + @MeasurementUnit;

			RAISERROR(@ErrorMessage, 16, 1);
			RETURN;
		END;
			--Se Inserta el registro
			INSERT INTO Inventory.InventoryMeasurementUnit(Code, [Name], Abbreviation, UnitType, [Status], AllowEditCostValue, CostValue, CreationDate, CreationUser, CrystalMeasurementUnit,RequiresStandardCode,StandardCode)
			VALUES(@CodeMeasurementUnit,@DescriptionMeasurementUnit,@Abbreviation,@UnitType,@Status,@AllowEditCostValue,@CostValue,[Common].[GETDATE](),@CodeUser,@CodeMeasurementUnit,@RequiresStandardCode,@StandardCode)
		END
		
		if (select count(*) from dbo.INUNIMEDI td where CODUNIMED = @CodeMeasurementUnit) > 0 begin
			-- Actualizamos en Crystal
			UPDATE dbo.INUNIMEDI
			SET	CODUNIMED = @CodeMeasurementUnit,
				DESUNIMED = @DescriptionMeasurementUnit,
				ABRUNIMED = @Abbreviation ,
				TIPUNIDAD = @UnitType,
				INDAUDFOR = 0
			WHERE CODUNIMED = @CodeMeasurementUnit
		end
		else begin
			-- Insertamos en Crystal
			INSERT INTO dbo.INUNIMEDI(CODUNIMED,DESUNIMED,ABRUNIMED,TIPUNIDAD,INDAUDFOR)
			VALUES(@CodeMeasurementUnit, @DescriptionMeasurementUnit, @Abbreviation, @UnitType, 0)
		end

		select 0 as CodeMessage, 'Se guardó correctamente la Unidad de Medida con Código ' + @CodeMeasurementUnit  as Message
	end try
	begin catch
		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch
END