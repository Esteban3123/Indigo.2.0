
-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 01/07/2020
-- Description:	Procedimiento que se encarga de guardar los campos de cubrimiento contractual en el detalle de la orden de servicio
-- =============================================
CREATE PROCEDURE [Authorization].[SP_SaveContractCoverage] 
    @Xml AS xml
AS
BEGIN
	SET NOCOUNT ON

	--Tabla para obtener los datos del xml
	declare @TableDetail table(ServiceOrderDetailId int, ContractCoverageStatus tinyint, ContractCoverageObservations varchar(max))
	
	begin try	
	
		--Se obtienen los datos de la cabecera del xml
		insert into @TableDetail
		select 
			t.x.value('ServiceOrderDetailId[1]','int') as ServiceOrderDetailId,
			t.x.value('ContractCoverageStatus[1]','tinyint') as ContractCoverageStatus,
			t.x.value('ContractCoverageObservations[1]','varchar(max)') as ContractCoverageObservations
		from @Xml.nodes('/TableDetail') t(x)
		
		--Se actualizan los campos del cubrimiento contractual
		update sod set sod.ContractCoverageStatus = t.ContractCoverageStatus, sod.ContractCoverageObservations = t.ContractCoverageObservations
		from @TableDetail t
		inner join Billing.ServiceOrderDetail sod on sod.Id = t.ServiceOrderDetailId

		select 0 AS CodeResult, 'Se guardó correctamente' AS MessageResult
		return
	end try
	begin catch
		select 999 AS CodeResult, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult
		return
	end catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda o actualiza el estado de cubrimiento contractual y sus observaciones en el detalle de las órdenes de servicio facturadas. Recibe un XML con uno o varios ítems (ServiceOrderDetailId, estado de cobertura y observaciones) y aplica la actualización directamente sobre la tabla de detalle de facturación (Billing.ServiceOrderDetail). Se usa en el proceso de autorización de contratos para registrar si un procedimiento, medicamento o insumo está cubierto por el contrato del pagador (EPS u otro tercero) y bajo qué condiciones o restricciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveContractCoverage';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveContractCoverage';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste el estado y observaciones de cubrimiento contractual sobre los ítems del detalle de una orden de servicio a partir de un XML con uno o varios registros.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContractCoverage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe contener nodos /TableDetail con los elementos ServiceOrderDetailId, ContractCoverageStatus y ContractCoverageObservations.; Los ServiceOrderDetailId provistos deben existir en Billing.ServiceOrderDetail para que el UPDATE afecte filas.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContractCoverage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se realizan inserciones ni eliminaciones; solo se actualizan dos columnas de cubrimiento contractual.; El procedimiento siempre devuelve un resultset con las columnas CodeResult y MessageResult.; El UPDATE no filtra por estado del detalle: se aplica a cualquier ServiceOrderDetail cuyo Id esté en el XML.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContractCoverage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cubrimiento contractual; Orden de servicio; Detalle de orden de servicio; Observaciones de cubrimiento', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContractCoverage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Billing.ServiceOrderDetail: Para cada nodo del XML, actualiza ContractCoverageStatus y ContractCoverageObservations en el detalle cuyo Id coincide con ServiceOrderDetailId.; [RETURN_RESULT] RESULT: Si la operación es exitosa retorna CodeResult=0 con mensaje ''Se guardó correctamente''; si ocurre una excepción retorna CodeResult=999 con ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContractCoverage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TRY/CATCH: ejecución sin excepción → Aplica el UPDATE y retorna CodeResult=0 else Retorna CodeResult=999 con el mensaje de error y la línea', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContractCoverage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetail', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContractCoverage';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveContractCoverage';
-- GO
