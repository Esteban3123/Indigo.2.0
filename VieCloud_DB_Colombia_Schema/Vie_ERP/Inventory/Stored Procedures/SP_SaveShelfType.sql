

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza un tipo de estantería (anaquel) en el inventario físico de bodega. Si el código del tipo de estante ya existe en la tabla ShelfType, actualiza sus datos (descripción, dimensiones, particiones y estado); si no existe, inserta un nuevo registro. Gestiona los atributos físicos del estante: largo, ancho, profundidad, número de particiones por profundidad, cantidad de particiones y ubicaciones por partición, junto con el usuario y la fecha de creación o modificación. Se utiliza para mantener el catálogo de tipos de estantes disponibles para la organización del almacén o bodega.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveShelfType';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveShelfType';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Realiza un upsert (insert/update) de un Tipo de Estante en el catálogo de inventario, registrando sus dimensiones físicas, particiones y trazabilidad de usuario/fecha.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveShelfType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El Code debe ser provisto para poder identificar la existencia previa del Tipo de Estante; Debe existir el esquema Common con la función GETDATE para obtener la fecha del sistema', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveShelfType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El Code actúa como clave única para distinguir entre operación de inserción y actualización (upsert); La fecha de creación/modificación se obtiene siempre desde Common.GETDATE() y nunca del cliente; Los errores capturados retornan CodeMessage=999 y el mensaje original del error sin propagar la excepción; Operación exitosa retorna CodeMessage=0 con mensaje de confirmación incluyendo el Code', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveShelfType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de Estante; Inventario; Dimensiones físicas (largo, ancho, profundidad); Particiones de estante; Ubicaciones por partición', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveShelfType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Inventory.ShelfType: Cuando existe un registro con el mismo Code (COUNT(*)>0), se actualizan dimensiones, particiones, estado y se registran ModificationDate=Common.GETDATE() y ModificationUser; [INSERT] Inventory.ShelfType: Cuando no existe un registro con el Code recibido, se inserta uno nuevo con CreationDate=Common.GETDATE() y CreationUser; [RETURN_RESULT] (resultset): Tras éxito devuelve CodeMessage=0 con mensaje de confirmación; ante excepción capturada en CATCH devuelve CodeMessage=999 con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveShelfType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe un registro en Inventory.ShelfType con el mismo Code → Actualiza el registro existente con los nuevos valores y registra fecha/usuario de modificación else Inserta un nuevo registro con fecha/usuario de creación', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveShelfType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveShelfType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ShelfType', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveShelfType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveShelfType';
-- GO
