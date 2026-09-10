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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o actualiza una vía de administración de medicamentos (por ejemplo: oral, intravenosa, intramuscular) en el catálogo del módulo de Inventario/Farmacia y simultáneamente sincroniza el cambio en el catálogo legacy de historia clínica (HCVIAADMI). Si la vía ya existe la actualiza; si no existe, la inserta en ambas tablas. Permite asociar la vía de administración a una forma farmacéutica específica y registra el usuario y fecha de creación o modificación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAdministrationRoute';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAdministrationRoute';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Sincroniza el catálogo de vías de administración entre el módulo nuevo (Inventory) y el legado (Crystal/HCVIAADMI), insertando o actualizando según exista el código.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de vía de administración debe ser único en Inventory.AdministrationRoute para distinguir INSERT vs UPDATE.; Si el PharmaceuticalFormId llega en 0 se interpreta como ausencia de forma farmacéutica (se normaliza a NULL).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'CrystalAdministrationRoute en Inventory.AdministrationRoute nunca queda NULL: si no se resuelve un código Crystal, se almacena el propio Code.; El nombre persistido en HCVIAADMI.DESVIAADM se trunca/convierte a varchar(30).; Toda excepción se captura y se devuelve como resultset con CodeMessage=999 (no se propaga).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Vía de administración; Forma farmacéutica; Catálogo Crystal (legado); Sincronización de catálogos clínicos', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Inventory.AdministrationRoute: Cuando ya existe un registro con el mismo Code, se actualizan PharmaceuticalFormId, Name, Status, ModificationDate/User y CrystalAdministrationRoute (usando el código Crystal existente o, en su defecto, el propio Code).; [INSERT] Inventory.AdministrationRoute: Cuando no existe un registro con el Code dado, se inserta con CreationDate/User y CrystalAdministrationRoute = código Crystal hallado o, si no, el propio Code.; [UPDATE] dbo.HCVIAADMI: Si se halló un CodeCrystal (vía CrystalAdministrationRoute previo o por coincidencia de DESVIAADM con el nombre), se actualiza DESVIAADM en HCVIAADMI para CODVIAADM = CodeCrystal.; [INSERT] dbo.HCVIAADMI: Si no se halló ningún CodeCrystal, se inserta una nueva fila en HCVIAADMI con CODVIAADM = Code recibido y DESVIAADM = nombre.; [RETURN_RESULT] (resultset): Retorna CodeMessage=0 con mensaje de éxito incluyendo el código guardado; ante excepción retorna CodeMessage=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @PharmaceuticalFormId = 0 → Se reemplaza por NULL antes de persistir.; si CrystalAdministrationRoute existente en Inventory.AdministrationRoute para el Code → Se reutiliza ese código como CodeCrystal. else Se busca CODVIAADM en dbo.HCVIAADMI cuya DESVIAADM coincida (RTRIM) con el nombre recibido.; si Existe registro en Inventory.AdministrationRoute con el Code → UPDATE del registro. else INSERT del registro.; si @CodeCrystal IS NOT NULL tras la búsqueda → UPDATE de HCVIAADMI sobre el CODVIAADM resuelto. else INSERT en HCVIAADMI con el Code recibido.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.AdministrationRoute; dbo.HCVIAADMI', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAdministrationRoute';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAdministrationRoute';
-- GO
