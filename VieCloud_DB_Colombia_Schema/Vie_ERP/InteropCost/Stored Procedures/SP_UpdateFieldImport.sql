
-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 2/12/2016
-- Description:	Procedimiento que se encarga de actualizar el campo de import en la tabla LogisticsProductionCenterRecordDetail
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_UpdateFieldImport] 
	@ObjectXml as Xml,
	@Status as int
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml y poder actualizar el campo en la tabla
	declare @TableIds table(Id int)

	--begin transaction
	begin try

		insert into @TableIds
		select 
		t.x.value('Id[1]','int') as Id
		from @ObjectXml.nodes('/Data') t(x)

		--Actualizo los registros si hay datos y sea diferente de anular
		if (select count(*) from @TableIds) > 0 and @Status <> 3 begin
			update lpcrd set lpcrd.Import = 1
			from InteropCost.LogisticsProductionCenterRecordDetail lpcrd 
			inner join @TableIds ti on ti.Id = lpcrd.Id
		end

		--Actualizo los registros si hay datos y sea igual a anular
		if (select count(*) from @TableIds) > 0 and @Status = 3 begin
			update lpcrd set lpcrd.Import = 0
			from InteropCost.LogisticsProductionCenterRecordDetail lpcrd 
			inner join @TableIds ti on ti.Id = lpcrd.Id
		end

		--commit transaction
		select 0 as CodeMessage, 'Se actualizó correctamente' as Message

	end try
	begin catch

		--rollback transaction
		select 999 as CodeMessage, ERROR_MESSAGE() as Message

	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que actualiza el indicador de importación (campo Import) en los registros de detalle de producción logística por centro (LogisticsProductionCenterRecordDetail). Recibe un XML con una lista de identificadores de registros y un estado: si el estado es distinto de 3 (anular), marca los registros como importados (Import = 1); si el estado es 3, los desmarca como no importados (Import = 0). Se utiliza en el proceso de interoperabilidad de costos para confirmar o revertir la importación de unidades de productos asignadas a centros de producción logística.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateFieldImport';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_UpdateFieldImport';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Marca o desmarca el flag de importación en el detalle de registros de producción logística según una lista de IDs recibida en XML y el estado indicado.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /Data con nodos hijos Id de tipo int.; Los Id incluidos deben corresponder a registros existentes en InteropCost.LogisticsProductionCenterRecordDetail para que el UPDATE tenga efecto.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor de Import solo toma 0 o 1; no usa otros valores.; El estado 3 se interpreta como anulación y siempre desmarca la importación.; Los errores no se propagan: se capturan con TRY/CATCH y se retornan como resultset con CodeMessage=999.; Solo se afectan filas cuyo Id provenga del XML recibido.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Importación de costos; Anulación (estado 3); Centro de producción logística; Detalle de registro de producción; Interoperabilidad de costos', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] InteropCost.LogisticsProductionCenterRecordDetail: Cuando el XML contiene al menos un Id y el estado es distinto de 3, se establece Import = 1 para los detalles cuyo Id esté en la lista.; [UPDATE] InteropCost.LogisticsProductionCenterRecordDetail: Cuando el XML contiene al menos un Id y el estado es igual a 3 (anulación), se establece Import = 0 para los detalles cuyo Id esté en la lista.; [RETURN_RESULT] (resultset): En ejecución exitosa retorna CodeMessage=0 y Message=''Se actualizó correctamente''; ante excepción retorna CodeMessage=999 y Message=ERROR_MESSAGE().', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen Ids en el XML y @Status <> 3 → Marca Import = 1 en los detalles correspondientes (confirmación de importación).; si Existen Ids en el XML y @Status = 3 → Marca Import = 0 en los detalles correspondientes (reversión por anulación).; si No hay Ids en el XML → No realiza ningún UPDATE y retorna mensaje de éxito.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.LogisticsProductionCenterRecordDetail', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_UpdateFieldImport';
-- GO
