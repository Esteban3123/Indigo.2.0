

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Crea o actualiza una unidad de medida en el catálogo de inventario (por ejemplo: unidad, caja, frasco, miligramo). Valida que la abreviatura no esté duplicada en otra unidad antes de guardar, y tanto inserta como actualiza el registro en la tabla moderna de inventario (InventoryMeasurementUnit) y en la tabla legada de Crystal (INUNIMEDI) para mantener ambos sistemas sincronizados. Recibe como parámetros el código, nombre, abreviatura, tipo de unidad, estado activo/inactivo, permiso de edición de costo, valor de costo, código de usuario que realiza la operación, y si requiere código estándar externo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveMeasureUnit';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveMeasureUnit';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Crea o actualiza una unidad de medida sincronizando los catálogos de inventario (VIE) y Crystal, validando que la abreviatura no esté duplicada en otra unidad.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMeasureUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proveerse un código de unidad de medida no nulo; La abreviatura no debe estar usada por otra unidad de medida distinta; El usuario que registra/modifica debe estar identificado para auditoría', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMeasureUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La comparación de abreviaturas se realiza ignorando mayúsculas/minúsculas (collation Latin1_General_CI_AS); No se permiten dos unidades de medida distintas con la misma abreviatura; La unidad de medida se sincroniza siempre entre el catálogo VIE (Inventory.InventoryMeasurementUnit) y el catálogo Crystal (dbo.INUNIMEDI); El campo CrystalMeasurementUnit se mantiene igual al Code de la unidad; Las inserciones en Crystal fijan INDAUDFOR = 0; Cualquier error es capturado y retornado como CodeMessage=999 con el mensaje de error, sin propagar excepción', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMeasureUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Unidad de medida; Inventario; Catálogo Crystal; Abreviatura; Costo; Código estándar', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMeasureUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Inventory.InventoryMeasurementUnit: Cuando ya existe un registro con el mismo Code, actualiza nombre, abreviatura, tipo, estado, costo, código estándar y datos de auditoría de modificación; [INSERT] Inventory.InventoryMeasurementUnit: Cuando no existe el Code y la abreviatura no está usada por otra unidad, inserta el nuevo registro con fecha y usuario de creación; [RAISERROR] Inventory.InventoryMeasurementUnit: Si al actualizar se detecta que la abreviatura pertenece a otra unidad (Code distinto), lanza error severidad 16 indicando duplicidad; [RAISERROR] Inventory.InventoryMeasurementUnit: Si al insertar la abreviatura ya está descrita en otra unidad de medida, lanza error severidad 16 y termina con RETURN; [UPDATE] dbo.INUNIMEDI: Cuando ya existe CODUNIMED en Crystal, actualiza descripción, abreviatura, tipo y fija INDAUDFOR=0; [INSERT] dbo.INUNIMEDI: Cuando no existe CODUNIMED en Crystal, inserta el registro con INDAUDFOR=0; [RETURN_RESULT] (resultset): Al finalizar correctamente devuelve CodeMessage=0 con mensaje de éxito; ante excepción devuelve CodeMessage=999 con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMeasureUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro en Inventory.InventoryMeasurementUnit con el mismo Code → Valida duplicidad de abreviatura con otro código y, si no hay conflicto, actualiza el registro existente else Si la abreviatura ya pertenece a otra unidad de medida lanza error; en caso contrario inserta un nuevo registro; si Existe abreviatura asociada a otra unidad distinta (Abbreviation igual y Code distinto) durante actualización → Lanza RAISERROR indicando que la abreviación ya existe para esa unidad; si En modo inserción, la abreviatura ya está registrada para otra unidad de medida → Lanza RAISERROR y retorna sin insertar else Procede con el INSERT; si Existe registro en dbo.INUNIMEDI con el mismo CODUNIMED → Actualiza el catálogo Crystal else Inserta en el catálogo Crystal', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMeasureUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMeasureUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryMeasurementUnit; dbo.INUNIMEDI', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMeasureUnit';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveMeasureUnit';
-- GO
