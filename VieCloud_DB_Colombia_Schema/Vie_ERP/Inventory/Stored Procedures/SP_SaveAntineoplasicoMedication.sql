-- ==========================================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 04/08/2020
-- Description:	Store que se encarga de guardar medicamentos antineoplásico CAC
-- ==========================================================
CREATE PROCEDURE [Inventory].[SP_SaveAntineoplasicoMedication]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Tabla para asignar los valores desde el xml para poder guardar
	declare @AntineoplasicoMedication table(Id int, AtcId int, ClassificationId tinyint, StateAPM tinyint)

	insert into @AntineoplasicoMedication
	select 
	t.x.value('Id[1]','int')
	,t.x.value('AtcId[1]','int')
	,t.x.value('ClassificationId[1]','tinyint')
	,t.x.value('StateAPM[1]','tinyint')
	from @Xml.nodes('/Data/APM') t(x)
	
	begin try
        --Error
		--select 999 as CodeResult, 'La unidad de medida para peso del ATC no tiene una clase seleccionada' as MessageResult, '' as Code, 0 as Id
		--return 

		--Nuevos
		insert into Inventory.AntineoplasicoMedication(AtcId, ClassificationId, StateAPM, CreationUser, CreationDate)
		SELECT AtcId, ClassificationId, StateAPM, @UserCode, getdate() FROM @AntineoplasicoMedication WHERE StateAPM = 1
	
		--Inactivos
		update Inventory.AntineoplasicoMedication set StateAPM = 2, InactivateUser = @UserCode, InactivateDate = [Common].[GETDATE]() 
		WHERE Id IN (SELECT Id from @AntineoplasicoMedication where StateAPM = 0) 
		and StateAPM <> 2

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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o inactiva medicamentos antineoplásicos (quimioterapia y oncología) en el catálogo de inventario del módulo CAC. Recibe un XML con uno o varios registros que incluyen el código ATC, la clasificación y el estado del medicamento; los registros nuevos (estado activo) se insertan en la tabla maestra AntineoplasicoMedication, mientras que los registros marcados como inactivos se desactivan registrando el usuario y la fecha de inactivación. Retorna un código de resultado indicando éxito o el mensaje de error en caso de falla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAntineoplasicoMedication';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAntineoplasicoMedication';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste un lote de medicamentos antineoplásicos CAC a partir de un XML, dando de alta los nuevos e inactivando los marcados, con auditoría de usuario y fecha.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAntineoplasicoMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /Data/APM con los nodos Id, AtcId, ClassificationId y StateAPM.; Debe existir un código de usuario para auditoría de creación/inactivación.; Los registros con StateAPM=0 deben referenciar un Id existente en la tabla maestra para poder inactivarse.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAntineoplasicoMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se reinactivan registros que ya están en estado 2 (inactivo).; Toda alta queda con auditoría de usuario y fecha de creación.; Toda inactivación queda con auditoría de usuario y fecha de inactivación.; Los errores no propagan excepción al cliente: se devuelven como resultset con CodeResult=999.; La fecha de inactivación se obtiene de [Common].[GETDATE]() mientras que la de creación usa GETDATE() del servidor.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAntineoplasicoMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento antineoplásico; CAC (Cuenta de Alto Costo); ATC (clasificación de medicamentos); Clasificación de medicamento; Estado de medicamento (activo/inactivo); Auditoría de creación e inactivación', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAntineoplasicoMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Inventory.AntineoplasicoMedication: Cuando StateAPM = 1 en el XML, se inserta un nuevo medicamento antineoplásico con el usuario y fecha actual como datos de creación.; [UPDATE] Inventory.AntineoplasicoMedication: Cuando StateAPM = 0 en el XML y el registro existente no está ya en estado 2, se marca como inactivo (StateAPM=2) registrando usuario y fecha de inactivación.; [RETURN_RESULT] (resultset): Si la transacción finaliza sin error retorna CodeResult=0 con mensaje de éxito; ante excepción retorna CodeResult=999 con ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAntineoplasicoMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si StateAPM = 1 en el registro del XML → Se inserta como nuevo medicamento antineoplásico activo.; si StateAPM = 0 en el registro del XML y el actual en BD es distinto de 2 → Se inactiva el medicamento (StateAPM=2) con auditoría de inactivación. else No se modifica el registro.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAntineoplasicoMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAntineoplasicoMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.AntineoplasicoMedication', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAntineoplasicoMedication';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAntineoplasicoMedication';
-- GO
