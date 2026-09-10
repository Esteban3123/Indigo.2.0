

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o actualiza un grupo farmacológico (familia o categoría terapéutica de medicamentos, como analgésicos, antibióticos o antihipertensivos) tanto en el catálogo moderno de inventario (Inventory.PharmacologicalGroup) como en la tabla legada de Crystal (dbo.IHGRUFARM), manteniendo ambas bases sincronizadas. Si el código del grupo ya existe realiza una actualización del nombre, estado y usuario de modificación; si no existe, lo inserta como registro nuevo con su fecha y usuario de creación. Se utiliza desde la administración del módulo de farmacia para mantener el maestro de grupos farmacológicos vigente y consistente entre los dos sistemas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmacologicalGroup';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmacologicalGroup';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Sincroniza el maestro de grupos farmacológicos entre el sistema VIE (Inventory.PharmacologicalGroup) y el sistema legado Crystal (dbo.IHGRUFARM), realizando upsert en ambos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código del grupo farmacológico no debe ser nulo para poder evaluar su existencia en ambas tablas; Debe existir la función [Common].[GETDATE]() para asignar fechas de auditoría', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código del grupo farmacológico es la clave de sincronización entre VIE y Crystal: siempre se replica el mismo @CodePharmaCologicalGroup en ambas tablas; En Inventory.PharmacologicalGroup, el campo CrystalPharmacologicalGroup siempre queda igual al Code; Los errores nunca se propagan al cliente: se capturan y se devuelven como resultset con CodeMessage=999; No utiliza transacción explícita: las operaciones en VIE y Crystal pueden quedar parcialmente aplicadas si falla la segunda (aunque el comentario menciona rollback, está deshabilitado); El Status solo se mantiene en VIE; en Crystal (IHGRUFARM) no se persiste estado activo/inactivo', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Grupo farmacológico; Maestro de farmacia; Sincronización VIE-Crystal; Auditoría de creación/modificación', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Inventory.PharmacologicalGroup: Cuando ya existe un registro con Code = @CodePharmaCologicalGroup, se actualizan Name, Status, ModificationDate, ModificationUser y CrystalPharmacologicalGroup (este último se fuerza al mismo código); [INSERT] Inventory.PharmacologicalGroup: Cuando no existe el código, se inserta el registro con CreationUser/CreationDate y CrystalPharmacologicalGroup igual al Code; [UPDATE] dbo.IHGRUFARM: Si existe registro con CODGRUFAR = @CodePharmaCologicalGroup, se actualiza únicamente DESGRUFAR con la descripción recibida; [INSERT] dbo.IHGRUFARM: Si no existe el código en Crystal, se inserta solo CODGRUFAR y DESGRUFAR (sin auditoría ni status); [RETURN_RESULT] (resultset): Al finalizar exitosamente devuelve CodeMessage=0 con mensaje de confirmación incluyendo el código guardado; ante excepción devuelve CodeMessage=999 con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro en Inventory.PharmacologicalGroup con Code = @CodePharmaCologicalGroup → UPDATE del registro en VIE else INSERT de nuevo registro en VIE con datos de creación; si Existe registro en dbo.IHGRUFARM con CODGRUFAR = @CodePharmaCologicalGroup → UPDATE de DESGRUFAR en Crystal else INSERT de nuevo registro en Crystal; si Ocurre excepción en cualquier operación (TRY/CATCH) → Devuelve resultset con CodeMessage=999 y ERROR_MESSAGE() else Devuelve resultset con CodeMessage=0 y mensaje de éxito', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmacologicalGroup; dbo.IHGRUFARM', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmacologicalGroup';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmacologicalGroup';
-- GO
