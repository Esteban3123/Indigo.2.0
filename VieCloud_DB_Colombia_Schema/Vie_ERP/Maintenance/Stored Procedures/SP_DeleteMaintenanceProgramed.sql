-- =============================================
-- Author:		Diego A. Roldán Lozano
-- Create date: 2018-10-09
-- Description:	Sp que elimina la programación de un activo
-- =============================================
CREATE PROCEDURE [Maintenance].[SP_DeleteMaintenanceProgramed]
	-- Add the parameters for the stored procedure here
	@ProgramatedDateIdsXml Xml
AS
BEGIN

	SET NOCOUNT ON;

	begin try
		declare @tmpProgramatedDateIds Table(RowId Int Identity(1, 1) primary key, ProgramatedDateId Int, MaintenancePlanAndMetrologyId Int)

		Insert Into @tmpProgramatedDateIds
		select t.x.value('Id[1]', 'Int'), mp.MaintenancePlanAndMetrologyId
		from @ProgramatedDateIdsXml.nodes('ProgramatedDateIds/Item') as t(x)
		inner join Maintenance.MaintenancePlanProgramated mp with(nolock) on mp.Id = t.x.value('Id[1]', 'Int')
		
		-- Validamos primero si existe programaciones con orden de trabajo o no
		declare @msgValidation varchar(max) = ''

		SELECT @msgValidation = Stuff((
			select N', ' + concat('Existe una órden de trabajo (', wo.Consecutive, ') para la programación del activo ', pa.Plate)
			from Maintenance.MaintenancePlanProgramated mp with(nolock)
			Inner Join @tmpProgramatedDateIds mid On mid.ProgramatedDateId = mp.Id
			inner join FixedAsset.FixedAssetPhysicalAsset pa with(nolock) on mp.FixedAssetPhysicalId = pa.id
			inner join Maintenance.MaintenancePlanAndMetrology mpm with(nolock) on mpm.Id = mp.MaintenancePlanAndMetrologyId
			inner join Maintenance.WorkOrder wo WITH(NOLOCK) ON wo.PhysicalAssetId = pa.Id			
		  FOR XML PATH(''),TYPE
		).value('text()[1]','nvarchar(max)'),1,2,N'')

		if IsNull(@msgValidation, '') <> '' begin
			select CAST(0 as bit) as StatusResult, @msgValidation as MessageResult
			return
		end
				
		Delete mp2
		From Maintenance.MaintenancePlanProgramated mp
		Inner Join @tmpProgramatedDateIds mid On mid.ProgramatedDateId = mp.Id
		Inner Join Maintenance.MaintenancePlanAndMetrology mpm On mpm.Id = mp.MaintenancePlanAndMetrologyId
		inner join Maintenance.MaintenancePlanProgramated mp2 on mp2.MaintenancePlanAndMetrologyId = mpm.Id And mp2.FixedAssetPhysicalId = mp.FixedAssetPhysicalId
		
		--miramos si el registro del plan metrology queda huerfano lo eliminamos		
		if exists (
			select mm.Id
			From Maintenance.MaintenancePlanAndMetrology mm
			left join Maintenance.MaintenancePlanProgramated mp on mp.MaintenancePlanAndMetrologyId = mm.Id
			left join (select distinct MaintenancePlanAndMetrologyId from @tmpProgramatedDateIds) ids on ids.MaintenancePlanAndMetrologyId = mm.Id
			where mp.Id is null
		) begin

			Delete mm
			From Maintenance.MaintenancePlanAndMetrology mm
			left join Maintenance.MaintenancePlanProgramated mp on mp.MaintenancePlanAndMetrologyId = mm.Id
			left join (select distinct MaintenancePlanAndMetrologyId from @tmpProgramatedDateIds) ids on ids.MaintenancePlanAndMetrologyId = mm.Id
			where mp.Id is null

		end

		select CAST(1 as bit) as StatusResult, '' as MessageResult
	end try
	begin catch
		select CAST(0 as bit) as StatusResult, ERROR_MESSAGE() as MessageResult
	end catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Elimina la programación de mantenimiento de uno o más activos fijos a partir de una lista de identificadores enviados en formato XML. Antes de borrar, valida que ninguna programación a eliminar tenga ya una orden de trabajo generada; si existe alguna, retorna un mensaje de error indicando el consecutivo de la orden y la placa del activo afectado. Si la validación pasa, elimina los registros de programación (MaintenancePlanProgramated) y, adicionalmente, limpia los planes de mantenimiento y metrología (MaintenancePlanAndMetrology) que queden huérfanos sin programaciones asociadas. Retorna un indicador de éxito o fracaso junto con un mensaje descriptivo del resultado.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteMaintenanceProgramed';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'PROCEDURE', @level1name = N'SP_DeleteMaintenanceProgramed';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Elimina programaciones de mantenimiento de activos validando que no tengan órdenes de trabajo asociadas, y limpia planes de mantenimiento/metrología que queden sin programaciones.', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMaintenanceProgramed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura ProgramatedDateIds/Item con nodo Id entero; Los Id provistos deben existir en Maintenance.MaintenancePlanProgramated para resolver su MaintenancePlanAndMetrologyId; No deben existir órdenes de trabajo (Maintenance.WorkOrder) asociadas al activo físico de las programaciones a eliminar', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMaintenanceProgramed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca elimina programaciones si existe al menos una WorkOrder asociada al activo físico correspondiente; Tras la ejecución exitosa, no quedan registros de MaintenancePlanAndMetrology sin al menos una programación asociada; Cualquier error en la transacción se captura y devuelve como StatusResult=0 sin propagar la excepción', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMaintenanceProgramed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Programación de mantenimiento; Orden de trabajo; Activo fijo físico (placa); Plan de mantenimiento y metrología; Mantenimiento de equipos', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMaintenanceProgramed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[DELETE] Maintenance.MaintenancePlanProgramated: Cuando la validación de órdenes de trabajo pasa, elimina las programaciones cuyo MaintenancePlanAndMetrologyId y FixedAssetPhysicalId coinciden con los de los Ids recibidos en el XML; [DELETE] Maintenance.MaintenancePlanAndMetrology: Si tras eliminar las programaciones quedan registros de plan sin programaciones asociadas (LEFT JOIN MaintenancePlanProgramated con mp.Id IS NULL), se eliminan esos planes huérfanos; [RETURN_RESULT] (resultset): Retorna StatusResult=0 con @msgValidation cuando existen órdenes de trabajo, StatusResult=1 con mensaje vacío en éxito, o StatusResult=0 con ERROR_MESSAGE() si ocurre excepción en el TRY/CATCH', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMaintenanceProgramed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IsNull(@msgValidation,'''') <> '''' (existen órdenes de trabajo asociadas a la programación) → Retorna StatusResult=0 con el mensaje concatenado indicando consecutivo de la orden y placa del activo, y termina sin borrar else Procede a eliminar las programaciones y planes huérfanos; si EXISTS plan de mantenimiento sin programaciones asociadas (mp.Id IS NULL tras LEFT JOIN) → Elimina los registros huérfanos de Maintenance.MaintenancePlanAndMetrology', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMaintenanceProgramed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Maintenance.MaintenancePlanProgramated; Maintenance.MaintenancePlanAndMetrology; FixedAsset.FixedAssetPhysicalAsset; Maintenance.WorkOrder', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMaintenanceProgramed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Maintenance', @level1type=N'PROCEDURE', @level1name=N'SP_DeleteMaintenanceProgramed';
-- GO
