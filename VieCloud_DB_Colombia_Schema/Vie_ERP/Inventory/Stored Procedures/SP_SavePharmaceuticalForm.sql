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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea o actualiza una forma farmacéutica (tableta, cápsula, jarabe, ampolla, etc.) de forma simultánea en dos sistemas: el catálogo moderno de inventario (Inventory.PharmaceuticalForm) y el catálogo legado Crystal (dbo.IHFORMEDI). Recibe los datos de la forma farmacéutica en formato XML —código, nombre, estado activo/inactivo, si requiere estabilidad y el grupo al que pertenece— junto con el usuario que realiza la operación. Si el código ya existe en cada sistema realiza una actualización de nombre, estado y estabilidad; si no existe, inserta un nuevo registro. Garantiza la sincronización del maestro de formas farmacéuticas entre VIE y Crystal para la correcta clasificación, dispensación y gestión de medicamentos en farmacia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmaceuticalForm';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmaceuticalForm';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Sincroniza el maestro de formas farmacéuticas entre el catálogo VIE (Inventory) y el catálogo legado Crystal (IHFORMEDI), realizando upsert en ambos sistemas a partir de un payload XML.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener un nodo /PharmaceuticalForm con los hijos Code, Name, Status, RequireStability y PharmaceuticalFormGroupingId.; El Code extraído del XML es la clave que determina si el registro existe en cada catálogo.; Debe existir el esquema Common con la función GETDATE() para registrar fechas de auditoría.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El campo CrystalMedicalForm en Inventory.PharmaceuticalForm siempre se mantiene igual al Code, garantizando el vínculo 1:1 con CODFORMED de Crystal.; El mismo Code se usa como llave en ambos catálogos (VIE y Crystal), asegurando trazabilidad cruzada.; Ningún error provoca rollback explícito; los errores se capturan y se devuelven como result set en lugar de propagarse.; PharmaceuticalFormGroupingId y Status solo se persisten en VIE; Crystal no almacena agrupación ni estado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Forma farmacéutica; Medicamento; Catálogo VIE; Catálogo Crystal; Estabilidad de medicamento; Agrupación de formas farmacéuticas', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Inventory.PharmaceuticalForm: Si ya existe un registro con Code = @Code, actualiza Name, Status, RequireStability, PharmaceuticalFormGroupingId, asigna CrystalMedicalForm = Code y registra ModificationDate/ModificationUser de auditoría.; [INSERT] Inventory.PharmaceuticalForm: Si no existe registro con Code = @Code, inserta uno nuevo asignando CrystalMedicalForm = Code y registrando CreationDate/CreationUser.; [UPDATE] dbo.IHFORMEDI: Si existe CODFORMED = @Code en Crystal, actualiza DESFORMED con el nombre y RequireStability.; [INSERT] dbo.IHFORMEDI: Si no existe CODFORMED = @Code en Crystal, inserta un nuevo registro con CODFORMED, DESFORMED y RequireStability (sin sincronizar agrupación ni estado).; [RETURN_RESULT] : Devuelve un result set con CodeMessage=0 y mensaje de éxito incluyendo el Code; ante cualquier excepción devuelve CodeMessage=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EXISTS en Inventory.PharmaceuticalForm con Code = @Code → UPDATE del registro existente en VIE else INSERT de nuevo registro en VIE; si EXISTS en dbo.IHFORMEDI con CODFORMED = @Code → UPDATE en Crystal (DESFORMED, RequireStability) else INSERT en Crystal con Code, Name y RequireStability', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalForm; dbo.IHFORMEDI', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalForm';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalForm';
-- GO
