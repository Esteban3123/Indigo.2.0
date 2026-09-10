
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
			@Status as tinyint,
			@Source tinyint
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

		SELECT @Source = rmsd.Source
		FROM MixingStation.RequestMixingStationDetail rmsd
		WHERE rmsd.Id = @RequestMixingStationDetailId

		IF EXISTS (
			SELECT 1
			FROM @XmlTable x
			LEFT JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = x.RequestMixingStationDetailId
			LEFT JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) ON rpds.Id = x.RequestPackageDetailStatusId
				AND rpds.RequestMixingStationDetailId = x.RequestMixingStationDetailId
			LEFT JOIN MixingStation.RequestMixingStationDetailPatients rmsdp WITH(NOLOCK) ON rmsdp.Id = x.RequestMixingStationDetailPatientsId
				AND rmsdp.RequestMixingStationDetailId = x.RequestMixingStationDetailId
			WHERE rmsd.Id IS NULL
				OR rpds.Id IS NULL
				OR (x.RequestMixingStationDetailPatientsId <> 0 AND rmsdp.Id IS NULL)
		)
		BEGIN
				select 99 as CodeMessage, 'La información enviada para anular no corresponde a la misma solicitud' as Message
				return
		END

		IF @Status IN (3, 6) AND EXISTS (
			SELECT 1
			FROM @XmlTable x
			INNER JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = x.RequestMixingStationDetailId
			LEFT JOIN MixingStation.RequestMixingStationDetailPatients rmsdp WITH(NOLOCK) ON rmsdp.Id = x.RequestMixingStationDetailPatientsId
				AND rmsdp.RequestMixingStationDetailId = x.RequestMixingStationDetailId
			INNER JOIN MixingStation.CampaignDetail cd WITH(NOLOCK) ON cd.Id = ISNULL(rmsdp.CampaignDetailId, rmsd.CampaignDetailId)
			INNER JOIN MixingStation.Campaign c WITH(NOLOCK) ON c.Id = cd.CampaignId
			WHERE NOT EXISTS (
				SELECT 1
				FROM MixingStation.CMConfigurationUsers cmu WITH(NOLOCK)
				WHERE cmu.CMConfigurationId = c.CMConfigurationId
					AND LTRIM(RTRIM(cmu.UserCode)) = LTRIM(RTRIM(@UserCode))
			)
		)
		BEGIN
				select 99 as CodeMessage, 'El usuario no tiene permisos para anular solicitudes de la central de mezclas asociada a la campaña' as Message
				return
		END

		IF @Status IN (3, 6) AND EXISTS (
			SELECT 1
			FROM @XmlTable x
			INNER JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) ON rpds.RequestMixingStationDetailId = x.RequestMixingStationDetailId
				AND (@Source = 4 OR rpds.Id = x.RequestPackageDetailStatusId)
			INNER JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = x.RequestMixingStationDetailId
			LEFT JOIN MixingStation.RequestMixingStationDetailPatients rmsdp WITH(NOLOCK) ON rmsdp.Id = x.RequestMixingStationDetailPatientsId
				AND rmsdp.RequestMixingStationDetailId = x.RequestMixingStationDetailId
			LEFT JOIN MixingStation.CampaignDetail cd WITH(NOLOCK) ON cd.Id = ISNULL(rmsdp.CampaignDetailId, rmsd.CampaignDetailId)
			WHERE ISNULL(rpds.Status, 0) <> 1
				OR ISNULL(rpds.QualityStatus, 0) <> 0
				OR ISNULL(rpds.SendTo, 0) <> 0
				OR rpds.PhysicalInventoryId IS NOT NULL
				OR rpds.VerificationTagUser IS NOT NULL
				OR rpds.VerificationTagDate IS NOT NULL
				OR rpds.PreparationTime IS NOT NULL
				OR ISNULL(cd.CampaignStatus, 0) IN (5, 6)
				OR EXISTS (
					SELECT 1
					FROM MixingStation.CampaignRawMaterial crm WITH(NOLOCK)
					WHERE crm.RequestPackageDetailStatusId = rpds.Id
				)
		)
		BEGIN
				select 99 as CodeMessage, 'No se puede anular una solicitud que ya fue procesada o pertenece a una campaña procesada o terminada' as Message
				return
		END

		/* Si el estado al que se va a cambiar es anulado se actualiza el origen en farmacia
		para que la solicitud no siga visible como pendiente en Dashboard Farmacia */
		IF @Status IN (3, 6)
		BEGIN
			;WITH SusceptibleMixingStation AS
			(
				SELECT DISTINCT TRY_CONVERT(uniqueidentifier, NULLIF(x.CodeSusceptibleMixingStation, '')) AS CodeSusceptibleMixingStation
				FROM @XmlTable x
				WHERE NULLIF(x.CodeSusceptibleMixingStation, '') IS NOT NULL

				UNION

				SELECT DISTINCT psms.CodeSusceptibleMixingStation
				FROM @XmlTable x
				JOIN MixingStation.RequestMixingStationDetailPatients rmsdp WITH(NOLOCK) ON rmsdp.Id = x.RequestMixingStationDetailPatientsId
				JOIN MedicalHistory.ProductSusceptibleMixingStation psms WITH(NOLOCK) ON rmsdp.EntityName = 'ProductSusceptibleMixingStation'
					AND psms.Id = rmsdp.EntityId

				UNION

				SELECT DISTINCT psms.CodeSusceptibleMixingStation
				FROM @XmlTable x
				JOIN MixingStation.RequestMixingStationDetail rmsd WITH(NOLOCK) ON rmsd.Id = x.RequestMixingStationDetailId
				JOIN MedicalHistory.ProductSusceptibleMixingStation psms WITH(NOLOCK) ON rmsd.EntityName = 'ProductSusceptibleMixingStation'
					AND psms.Id = rmsd.EntityId
			)
			UPDATE hcd
			SET		hcd.VIEPROCESSED = 0,
					hcd.CANPENPRO = 0,
					hcd.PROESTADO = '3'
			FROM dbo.HCFARMEPD hcd
			JOIN SusceptibleMixingStation sms ON sms.CodeSusceptibleMixingStation = hcd.CodeSusceptibleMixingStation
			WHERE sms.CodeSusceptibleMixingStation IS NOT NULL
			
		END

		-- Cantidad a descontar: solicitud inventario (Source=4) tiene N filas en RequestPackageDetailStatus; el XML suele traer una.
		-- Con esto RequestMixingStationDetail.Quantity llega a 0 y el detalle pasa a anulado (3) cuando corresponde.
		IF @Source = 4 AND @Status IN (3, 6)
			SELECT @Quantity = COUNT(*)
			FROM MixingStation.RequestPackageDetailStatus
			WHERE RequestMixingStationDetailId = @RequestMixingStationDetailId
		ELSE
			SELECT @Quantity = COUNT(*) FROM @XmlTable

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

		-- RequestPackageDetailStatus: por filas del XML, o todo el detalle en solicitud inventario al anular (3/6)
		IF @Source = 4 AND @Status IN (3, 6)
			UPDATE MixingStation.RequestPackageDetailStatus
			SET Status = @Status
			WHERE RequestMixingStationDetailId = @RequestMixingStationDetailId
		ELSE
			UPDATE rpds
				SET rpds.Status = x.Status
			FROM MixingStation.RequestPackageDetailStatus rpds
			INNER JOIN @XmlTable x ON rpds.Id = x.RequestPackageDetailStatusId

		--Se retorna la Confirmación
		select 0 as CodeMessage, 'Se anularon los Registros correctamente' as Message

	End try
	Begin Catch
		--Se retorna el error
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
		return

	End Catch
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que anula pacientes y productos asignados a solicitudes de preparación en la estación de mezclas farmacéuticas (preparados magistrales). Recibe un XML con los identificadores de paquetes, detalles de solicitud y pacientes a anular, y actualiza su estado a anulado en RequestMixingStationDetailPatients, RequestMixingStationDetail y RequestPackageDetailStatus. Cuando el estado destino representa anulación de solicitud o paquete (3 o 6), identifica los productos susceptibles de mezcla desde el XML o desde las tablas de central de mezclas y actualiza HCFARMEPD para que no continúen visibles como pendientes en Dashboard Farmacia (VIEPROCESSED=0, CANPENPRO=0, PROESTADO=3), conservando el enrutamiento actual. Si la cantidad de unidades de un paciente llega a cero, desvincula además el detalle de la campaña farmacéutica asociada y registra el usuario y fecha de anulación.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ProcessMixingStation';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_ProcessMixingStation';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa la anulación de pacientes/productos dentro de solicitudes de la estación de mezclas, ajustando cantidades y estados, y propagando la anulación a farmacia para que no queden pendientes visibles en Dashboard Farmacia.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessMixingStation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener al menos un nodo /Data con RequestPackageDetailStatusId y RequestMixingStationDetailId distintos de 0; de lo contrario se retorna CodeMessage=99 sin ejecutar cambios.; Se requiere el código de usuario para registrar la auditoría de anulación cuando la cantidad llegue a cero.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessMixingStation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cantidad a descontar (@Quantity) es siempre el número de filas recibidas en el XML.; La desvinculación de la campaña (CampaignDetailId=NULL) y el registro de AnnulmentUser/AnnulmentDate solo ocurren cuando la cantidad remanente queda en cero.; El estado 3 se reserva para indicar anulación total tanto en RequestMixingStationDetailPatients como en RequestMixingStationDetail.; La actualización de productos susceptibles en HCFARMEPD solo se ejecuta cuando el estado enviado representa anulación de solicitud o paquete (@Status IN (3,6)).; Toda la operación está envuelta en TRY/CATCH; ante excepción se devuelve el error sin propagar (no hay transacción explícita ni ROLLBACK).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessMixingStation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'anulación de solicitud; estación de mezclas (farmacia); campaña; paciente en solicitud de mezcla; producto susceptible; Dashboard Farmacia; paquete/preparado magistral; auditoría de anulación (usuario y fecha)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessMixingStation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.HCFARMEPD: Cuando @Status IN (3,6), identifica CodeSusceptibleMixingStation desde el XML, RequestMixingStationDetailPatients o RequestMixingStationDetail, y actualiza VIEPROCESSED=0, CANPENPRO=0 y PROESTADO=''3'' para que el Dashboard Farmacia no siga mostrando la solicitud como pendiente de central de mezclas, sin modificar SENDTO.; [UPDATE] MixingStation.RequestMixingStationDetailPatients: Si el XML trae RequestMixingStationDetailPatientsId<>0, se descuenta @Quantity (número de filas del XML) a Quantity; si la cantidad resultante es 0 se anula completamente: Status=3, CampaignDetailId=NULL (desvinculación de campaña), AnnulmentUser=@UserCode y AnnulmentDate=Common.GETDATE(); si no es 0, esos campos de anulación quedan en NULL.; [UPDATE] MixingStation.RequestMixingStationDetail: Para el RequestMixingStationDetailId tomado del XML, si Quantity-@Quantity=0 el Status se fija en 3 (anulado); en caso contrario el Status toma el valor enviado en @Status.; [UPDATE] MixingStation.RequestPackageDetailStatus: Para cada RequestPackageDetailStatusId presente en el XML se actualiza su Status al valor indicado en el XML, marcando la anulación a nivel de paquete/producto.; [RETURN_RESULT] (resultset): Devuelve CodeMessage=99 cuando faltan ids en el detalle, CodeMessage=0 con mensaje de éxito tras procesar, o CodeMessage=999 con ERROR_MESSAGE() si ocurre una excepción capturada.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessMixingStation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe alguna fila del XML con RequestPackageDetailStatusId=0 o RequestMixingStationDetailId=0 → Retorna CodeMessage=99 con mensaje de error y aborta sin aplicar cambios else Continúa con el procesamiento de anulación; si @Status IN (3,6) → Actualiza HCFARMEPD para los productos susceptibles identificados desde el XML o desde las entidades ProductSusceptibleMixingStation relacionadas, dejando la solicitud fuera del Dashboard Farmacia como pendiente de central de mezclas else No se altera el enrutamiento en HCFARMEPD; si Existe en el XML algún RequestMixingStationDetailPatientsId<>0 → Actualiza RequestMixingStationDetailPatients descontando cantidad y, si llega a 0, anula y desvincula la campaña else Omite la actualización a nivel de paciente; si rmsdp.Quantity - @Quantity = 0 (en RequestMixingStationDetailPatients) o rmsd.Quantity - @Quantity = 0 (en RequestMixingStationDetail) → Marca Status=3 (anulado), y en el caso de pacientes registra usuario/fecha de anulación y libera CampaignDetailId else Solo descuenta cantidad o aplica el @Status enviado, sin marcar anulación total', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessMixingStation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessMixingStation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; MixingStation.RequestMixingStationDetailPatients; MixingStation.RequestMixingStationDetail; MixingStation.RequestPackageDetailStatus; MedicalHistory.ProductSusceptibleMixingStation', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessMixingStation';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_ProcessMixingStation';
-- GO
