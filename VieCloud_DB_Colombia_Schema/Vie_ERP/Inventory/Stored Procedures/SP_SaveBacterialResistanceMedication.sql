-- ==========================================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 03/04/2020
-- Description:	Store que se encarga de guardar medicamentos con resistencia bacteriana
-- ==========================================================
CREATE PROCEDURE [Inventory].[SP_SaveBacterialResistanceMedication]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Variables para asignar los valores desde el xml para poder guardar
	--declare @Id int, @AtcId int, @StartDate datetime, @EndDate datetime, @Observation varchar(500), @StateBRM tinyint

	declare @BacterialResistanceMedication table(Id int, AtcId int, StartDate datetime, EndDate datetime, Observation varchar(500), StateBRM tinyint)

	insert into @BacterialResistanceMedication
	select 
	t.x.value('Id[1]','int')
	,t.x.value('AtcId[1]','int')
	,t.x.value('StartDate[1]','datetime')
	,t.x.value('EndDate[1]','datetime')
	,t.x.value('Observation[1]','varchar(500)')
	,t.x.value('StateBRM[1]','tinyint')
	from @Xml.nodes('/Data/BRM') t(x)
	
	begin try
        --Error
		--select 999 as CodeResult, 'La unidad de medida para peso del ATC no tiene una clase seleccionada' as MessageResult, '' as Code, 0 as Id
		--return 

		--Nuevos
		insert into Inventory.BacterialResistanceMedication(AtcId, StartDate, EndDate, Observation, StateBRM, CreationUser, CreationDate)
		SELECT AtcId, StartDate, EndDate, Observation, StateBRM, @UserCode, getdate() FROM @BacterialResistanceMedication WHERE StateBRM = 1
	
		--Eliminar
		DELETE Inventory.BacterialResistanceMedication
		WHERE Id IN (SELECT Id from @BacterialResistanceMedication where StateBRM = 0)
		and StartDate > Getdate()
		
		--Inactivos
		update Inventory.BacterialResistanceMedication set StateBRM = 3, InactivateUser = @UserCode, InactivateDate = [Common].[GETDATE]() 
		WHERE Id IN (SELECT Id from @BacterialResistanceMedication where StateBRM = 0) 
		and StateBRM <> 3 and StartDate <= Getdate()

		--Modificados
		update BRM set AtcId = t.AtcId, StartDate = t.StartDate, EndDate = t.EndDate, Observation = t.Observation,
		ModificationUser = @UserCode, ModificationDate = [Common].[GETDATE]()
		from @BacterialResistanceMedication t
		inner join Inventory.BacterialResistanceMedication BRM
		ON t.StateBRM = 2
		and BRM.Id = t.Id

		--Vigentes
		update Inventory.BacterialResistanceMedication set StateBRM = 2, ModificationUser = @UserCode, ModificationDate = [Common].[GETDATE]()
		WHERE StateBRM = 1 AND [Common].[GETDATE]() BETWEEN StartDate and EndDate

		--Inactivos
		update Inventory.BacterialResistanceMedication set StateBRM = 3, InactivateUser = @UserCode, InactivateDate = [Common].[GETDATE]() 
		WHERE StateBRM <> 3 and [Common].[GETDATE]() > EndDate

		--Retorna el ok
		select 0 as CodeResult, 'Se guardó correctamente' as MessageResult, '' as Code, 0 as Id
		return

	end try
	begin catch

		--Retorna el error
		select 999 as CodeResult, ERROR_MESSAGE() as MessageResult, '' as Code, 0 as Id
		return

	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda, actualiza, inactiva y elimina medicamentos clasificados con resistencia bacteriana según su código ATC (clasificación anatómica, terapéutica y química). Recibe los datos mediante un XML con uno o varios registros y un código de usuario responsable de la operación. Según el estado enviado (nuevo, modificado o inactivo/eliminado) y la vigencia de las fechas de inicio y fin, determina si el medicamento debe insertarse como nuevo, actualizarse, marcarse como inactivo o eliminarse del catálogo. Gestiona el ciclo de vida completo de la tabla BacterialResistanceMedication, incluyendo auditoría de creación, modificación e inactivación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveBacterialResistanceMedication';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveBacterialResistanceMedication';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste de forma masiva (alta, modificación, eliminación o inactivación) los medicamentos con resistencia bacteriana enviados en un XML, y reclasifica su estado según la vigencia (StartDate/EndDate) respecto a la fecha actual.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBacterialResistanceMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro XML debe seguir la estructura /Data/BRM con nodos Id, AtcId, StartDate, EndDate, Observation y StateBRM; Cada elemento debe traer un StateBRM válido (0=eliminar/inactivar, 1=nuevo, 2=modificar) que dirige la operación a aplicar; Para inactivar, eliminar o modificar, el Id provisto debe existir en Inventory.BacterialResistanceMedication', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBacterialResistanceMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El borrado físico solo ocurre cuando el registro aún no ha entrado en vigencia (StartDate > fecha actual); Registros ya vigentes nunca se eliminan: se inactivan con StateBRM = 3; Un registro nunca se reinactiva si ya está en StateBRM = 3; Toda escritura registra el usuario y fecha de auditoría correspondiente (creación, modificación o inactivación); Los valores de StateBRM manejados son: 1=nuevo, 2=vigente/modificado, 3=inactivo, 0=marcado para eliminar/inactivar; Toda la operación se ejecuta dentro de un TRY/CATCH y devuelve un resultset estándar (CodeResult, MessageResult, Code, Id)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBacterialResistanceMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento con resistencia bacteriana; Código ATC; Vigencia (StartDate/EndDate); Estado del registro (nuevo/vigente/inactivo); Auditoría de creación, modificación e inactivación', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBacterialResistanceMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Inventory.BacterialResistanceMedication: Cuando el XML trae StateBRM = 1, inserta un nuevo medicamento con resistencia bacteriana registrando CreationUser y CreationDate = GETDATE(); [DELETE] Inventory.BacterialResistanceMedication: Cuando el XML trae StateBRM = 0 y el registro tiene StartDate > GETDATE(), elimina físicamente el registro; [UPDATE] Inventory.BacterialResistanceMedication: Cuando el XML trae StateBRM = 0, StartDate <= GETDATE() y el registro no está ya inactivo, lo marca como inactivo (StateBRM = 3) con InactivateUser e InactivateDate; [UPDATE] Inventory.BacterialResistanceMedication: Cuando el XML trae StateBRM = 2, actualiza AtcId, StartDate, EndDate y Observation con los valores del XML y registra ModificationUser/ModificationDate; [UPDATE] Inventory.BacterialResistanceMedication: Para registros con StateBRM = 1 cuya fecha actual cae entre StartDate y EndDate, los promueve a vigentes (StateBRM = 2) registrando ModificationUser/ModificationDate; [UPDATE] Inventory.BacterialResistanceMedication: Para todo registro no inactivo (StateBRM <> 3) cuyo EndDate ya pasó, lo inactiva (StateBRM = 3) registrando InactivateUser/InactivateDate; [RETURN_RESULT] (resultset): Si el flujo termina sin error, retorna CodeResult=0 y mensaje ''Se guardó correctamente''; si ocurre excepción, retorna CodeResult=999 con ERROR_MESSAGE()', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBacterialResistanceMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si StateBRM = 1 en el XML → Inserta nuevo registro de medicamento con resistencia bacteriana; si StateBRM = 0 en el XML y StartDate > GETDATE() → Elimina físicamente el registro (aún no ha entrado en vigencia); si StateBRM = 0 en el XML, StartDate <= GETDATE() y StateBRM actual <> 3 → Inactiva el registro (StateBRM = 3) en lugar de eliminarlo; si StateBRM = 2 en el XML → Actualiza AtcId, StartDate, EndDate y Observation del registro existente; si StateBRM = 1 y la fecha actual está entre StartDate y EndDate → Promueve el registro a vigente (StateBRM = 2); si StateBRM <> 3 y GETDATE() > EndDate → Inactiva el registro por vencimiento de vigencia (StateBRM = 3)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBacterialResistanceMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBacterialResistanceMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.BacterialResistanceMedication', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBacterialResistanceMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveBacterialResistanceMedication';
-- GO
