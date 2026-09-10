

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o actualiza un nivel de riesgo del inventario farmacéutico y de almacén, operando simultáneamente sobre la base de datos VIE (tabla Inventory.InventoryRiskLevel) y la base de datos legada Crystal (tabla dbo.INIVERIES), garantizando consistencia entre ambos sistemas. Recibe el código, nombre, estado activo/inactivo y usuario responsable del nivel de riesgo; si el código ya existe realiza una actualización, de lo contrario inserta un nuevo registro. Se usa para administrar el catálogo de niveles de riesgo (por ejemplo: alto, medio, bajo) que clasifican los ítems o medicamentos según su criticidad para la gestión farmacéutica y control de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInventoryRiskLevel';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveInventoryRiskLevel';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Sincroniza (upsert) un nivel de riesgo de inventario en los catálogos de VIE y Crystal manteniéndolos consistentes.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryRiskLevel';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de nivel de riesgo no debe ser nulo para poder evaluar existencia en ambos catálogos; Debe existir el esquema/función Common.GETDATE() para timestamps de auditoría; La tabla dbo.INIVERIES debe ser accesible desde el contexto actual', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryRiskLevel';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de niveles de riesgo se mantiene replicado entre Inventory.InventoryRiskLevel (VIE) y dbo.INIVERIES (Crystal); El Code/CODNIVRIE actúa como clave única lógica que evita duplicados (se hace upsert por código); Los errores no propagan excepción; se capturan y se devuelven como resultset con CodeMessage=999; Las inserciones registran auditoría de creación; las actualizaciones registran auditoría de modificación, sólo en la tabla VIE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryRiskLevel';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nivel de riesgo de inventario; Catálogo de inventario; Sincronización VIE-Crystal; Auditoría de creación/modificación', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryRiskLevel';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Inventory.InventoryRiskLevel: Cuando existe un registro con Code igual al código recibido, se actualizan Name, Status, ModificationDate y ModificationUser; [INSERT] Inventory.InventoryRiskLevel: Cuando no existe registro con Code igual al código recibido, se inserta con CreationDate=Common.GETDATE() y CreationUser del usuario recibido; [UPDATE] dbo.INIVERIES: Cuando existe CODNIVRIE igual al código recibido, se actualizan DESNIVRIE y ESTNIVRIE (espejo en Crystal); [INSERT] dbo.INIVERIES: Cuando no existe CODNIVRIE igual al código recibido, se inserta el nivel de riesgo en Crystal; [RETURN_RESULT] resultset: Si todo concluye sin error retorna CodeMessage=0 con mensaje de éxito; si ocurre excepción retorna CodeMessage=999 con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryRiskLevel';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro en Inventory.InventoryRiskLevel con el mismo Code → Actualiza el registro existente else Inserta un nuevo registro en el catálogo VIE; si Existe registro en dbo.INIVERIES con el mismo CODNIVRIE → Actualiza el registro espejo en Crystal else Inserta nuevo registro espejo en Crystal', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryRiskLevel';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryRiskLevel';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryRiskLevel; dbo.INIVERIES', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryRiskLevel';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveInventoryRiskLevel';
-- GO
