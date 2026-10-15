
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