
-- ===============================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 26/03/2021
-- Description:	Procedimiento que se encarga de anular los pacientes que estan en las solicitudes de cada campaña
-- ===============================================================================================================================
CREATE PROCEDURE [MixingStation].[SP_ProcessMixingStation]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN

	--Tabla de para obtener el xml
	declare @XmlTable table(RequestPackageDetailStatusId int,RequestMixingStationDetailPatientsId int,RequestMixingStationDetailId int,CodeSusceptibleMixingStation varchar(36) ,Status tinyint)
	-- Variables para filtrar
	declare @RequestMixingStationDetailPatientsId as int,
			@Quantity as int,
			@RequestMixingStationDetailId as int,
			@Status as tinyint
	Begin try
				
		--Se obtienen los detalles del xml
		insert into @XmlTable
		select 
			t.x.value('RequestPackageDetailStatusId[1]','int') as RequestPackageDetailStatusId,
			t.x.value('RequestMixingStationDetailPatientsId[1]','int') as RequestMixingStationDetailPatientsId,
			t.x.value('RequestMixingStationDetailId[1]','int') as RequestMixingStationDetailId,
			t.x.value('CodeSusceptibleMixingStation[1]','varchar(36)') as CodeSusceptibleMixingStation,
			t.x.value('Status[1]','tinyint') as Status
		from @Xml.nodes('/Data') t(x)
		
		if EXISTS (SELECT 1
					FROM @XmlTable x
					where x.RequestPackageDetailStatusId = 0 or x.RequestMixingStationDetailId = 0)
		BEGIN
				select 99 as CodeMessage, 'No se envio la informacion del detalle de la solicitud para realizar la anulación' as Message
				return
		END

		--SELECT * from @XmlTable x
		
		SELECT TOP 1 @Status = x.Status, @RequestMixingStationDetailId = x.RequestMixingStationDetailId FROM @XmlTable x

		/* Si el estado al que se va a cambiar es anulado se cambia el enrutamiento de los productos
		a farmacia intraHospitalaria*/
		IF @Status=3 AND EXISTS(SELECT 1 FROM @XmlTable x where x.CodeSusceptibleMixingStation is not null)
		BEGIN
			
			UPDATE  hcd
			SET		hcd.SENDTO =1,
					hcd.VIEPROCESSED=0
			FROM @XmlTable x
			JOIN HCFARMEPD hcd WITH(NOLOCK) on X.CodeSusceptibleMixingStation=hcd.CodeSusceptibleMixingStation
			
		END

		--Se saca la cantidad de Productos a anular para restarle a la cantidad de producto de una solicitud
		SELECT @Quantity = COUNT(*) from @XmlTable

		--Si exite un registro con Id de la tabla RequestMixingStationDetailPatients se actualiza esta tabla
		if EXISTS(SELECT 1 
				from @XmlTable x
				where x.RequestMixingStationDetailPatientsId <> 0)
		BEGIN

			SELECT TOP 1 @RequestMixingStationDetailPatientsId = x.RequestMixingStationDetailPatientsId
			FROM @XmlTable x
			-- si la cantidad queda en 0 se procede hacer la anulacion de toda la solicitud ademas de la devinculacion de la campaña
			UPDATE rmsdp 
				SET rmsdp.Quantity = rmsdp.Quantity - @Quantity,
					rmsdp.CampaignDetailId = iif( rmsdp.Quantity -@Quantity = 0, null,rmsdp.CampaignDetailId),
					rmsdp.Status =iif(rmsdp.Quantity - @Quantity = 0, 3,rmsdp.Status),
					rmsdp.AnnulmentUser = iif(rmsdp.Quantity - @Quantity = 0, @UserCode ,null),
					rmsdp.AnnulmentDate =iif(rmsdp.Quantity - @Quantity = 0, Common.GETDATE() ,null)
			FROM MixingStation.RequestMixingStationDetailPatients rmsdp
			WHERE rmsdp.Id =@RequestMixingStationDetailPatientsId

		END

		--Aqui se procede a realizar la respectiva Actualizacion  de la tabla RequestMixingStationDetail
		UPDATE rmsd
			SET 
				rmsd.Status = iif(rmsd.Quantity-@Quantity =0, 3, @Status)
		FROM MixingStation.RequestMixingStationDetail rmsd
		where rmsd.Id=@RequestMixingStationDetailId

		--Se actualiza la tabla RequestPackageDetailStatus para indicar a cada producto su anulación
		UPDATE rpds
			SET rpds.Status= x.Status
		FROM MixingStation.RequestPackageDetailStatus rpds
		INNER JOIN @XmlTable x on rpds.Id = x.RequestPackageDetailStatusId

		--Se retorna la Confirmación
		select 0 as CodeMessage, 'Se anularon los Registros correctamente' as Message

	End try
	Begin Catch
		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
		return

	End Catch
END