-- =============================================
-- Author:		Diego Andrés Roldán Lozano
-- Create date: 29-09-2015
-- Description:	Procedimiento para reclasificar los distributions a un nuevo ingreso
-- =============================================
CREATE PROCEDURE [Billing].[SP_ReclasificateDistributions]
	@ServiceOrderDetailDistributionIds XML,
	@RevenueControlId INT,
	@UserCode VARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @StatusResult AS BIT, 
			@MessageResult AS VARCHAR(MAX), 
			@Message VARCHAR(MAX)

	--tabla con los id a distribuir
	DECLARE @tmpDistributionsId TABLE 
	(
		IdDistribution INT PRIMARY KEY
	)

	--tabla donde se insertan los folios a afectar
	DECLARE @tmpOldFolios TABLE
	(
		Id INT PRIMARY KEY, 
		RevenueControlId INT, 
		BillingAuthorizationId INT, 
		FolioOrder TINYINT, 
		FolioType TINYINT, 
		LiquidationType TINYINT, 
		ContractEntityId INT, 
		HealthAdministratorId INT, 
		ThirdPartyId INT, 
		CareGroupId INT, 
		TotalFolio NUMERIC(18,0), 
		ResponsibleRecoveryFee TINYINT, 
		TotalPatientSalesPrice NUMERIC(18,0), 
		PatientDiscount NUMERIC(18,0), 
		PatientDiscountPercentage NUMERIC(5,2), 
		TotalPatientWithDiscount NUMERIC(18,0), 
		ValueCopay NUMERIC(18,0), 
		ValueFeeModerator NUMERIC(18,0), 
		ValueVoucher NUMERIC(18,0), 
		Observation VARCHAR(100), 
		InvoiceCategoryId INT, 
		OutputDate DATETIME, 
		IsCutAccount BIT, 
		OutputDiagnosis CHAR(4), 
		[Status] TINYINT, 
		CreationDate DATETIME, 
		CreationUser VARCHAR(20)
	)

	--tabla donde se va a insertar los nuevos folios
	DECLARE @tmpNewFolios TABLE
	(
		IdNewFolio INT PRIMARY KEY, 
		IdOldFolio INT
	)

	-------------------------------------------------------------------------------------------------------------------

    BEGIN TRY
		
		INSERT INTO @tmpDistributionsId
			SELECT
				t.x.value('Id[1]','int')
			FROM @ServiceOrderDetailDistributionIds.nodes('/ServiceOrderDetailDistributionIds/Ids') t(x)

		IF EXISTS
		(
			SELECT 1
			FROM @tmpDistributionsId td
			JOIN Billing.ServiceOrderDetailDistribution sodd ON td.IdDistribution = sodd.Id
			JOIN Billing.RevenueControlDetail rcd ON sodd.RevenueControlDetailId = rcd.Id
			WHERE rcd.Status <> 1
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + CONCAT(' - Admision: ', rc.AdmissionNumber, ' - Folio: ', rcd.FolioOrder, ' - Estado: ', CASE rcd.Status
									WHEN 2 THEN 'Facturado'
									WHEN 3 THEN 'Bloqueado'
									WHEN 4 THEN 'Anulado'
									WHEN 5 THEN 'Reconocimiento Ingresos'
									WHEN 6 THEN 'Factura Asociada'
								END)
						FROM @tmpDistributionsId td
						JOIN Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK) ON td.IdDistribution = sodd.Id
						JOIN Billing.RevenueControlDetail rcd WITH (NOLOCK) ON sodd.RevenueControlDetailId = rcd.Id
						JOIN Billing.RevenueControl rc WITH (NOLOCK) ON rcd.RevenueControlId = rc.Id
						WHERE rcd.Status <> 1
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	CONVERT(BIT, 0) AS StatusResult, 
					'No se logro realizar la reclasificación debido a que las siguientes lineas no pertenecen a folios en estado registrado: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '') AS MessageResult
			RETURN
		END

		IF EXISTS
			(
				SELECT 1
				FROM @tmpDistributionsId td
				JOIN Billing.ServiceOrderDetailDistribution sodd with(nolock) ON  td.IdDistribution = sodd.Id
				JOIN Billing.RevenueRecognitionDetail rrcd with(nolock) on sodd.ServiceOrderDetailId= rrcd.ServiceOrderDetailId AND sodd.RevenueControlDetailId=rrcd.RevenueControlDetailId
				join Billing.RevenueRecognition rr with(nolock) on rrcd.RevenueRecognitionId=rr.Id
				where rr.State<>2
			)
		BEGIN
			SELECT	CONVERT(BIT, 0) AS StatusResult, 
					'No se puede llevar acabo la accion ya que existe un reconocimiento activo' AS MessageResult
			RETURN
		END


		--Creamos los nuevos folios
		insert into @tmpOldFolios
		select distinct rcd.Id, rcd.RevenueControlId, rcd.BillingAuthorizationId, rcd.FolioOrder
			, rcd.FolioType, rcd.LiquidationType, rcd.ContractEntityId, rcd.HealthAdministratorId
			, rcd.ThirdPartyId, rcd.CareGroupId, rcd.TotalFolio, rcd.ResponsibleRecoveryFee
			, rcd.TotalPatientSalesPrice, rcd.PatientDiscount, rcd.PatientDiscountPercentage
			, rcd.TotalPatientWithDiscount, rcd.ValueCopay, rcd.ValueFeeModerator
			, rcd.ValueVoucher, rcd.Observation, rcd.InvoiceCategoryId, rcd.OutputDate
			, rcd.IsCutAccount, rcd.OutputDiagnosis, rcd.[Status], rcd.CreationDate, rcd.CreationUser 
		from @tmpDistributionsId Idx
		inner join Billing.ServiceOrderDetailDistribution sodd with(nolock) on idx.IdDistribution = sodd.Id
		inner join Billing.RevenueControlDetail rcd with(nolock) on sodd.RevenueControlDetailId = rcd.Id
		order by rcd.Id
		

		declare @tmpFolioToRemove table(RevenueControlDetailId int primary key, RevenueControlId int)

		declare @IdOldFolio int, @IdNewFolio int, @OldRevenueControlId int
		declare cursor_folio cursor for
		select Id, RevenueControlId from @tmpOldFolios
		open cursor_folio
		fetch next from cursor_folio into @IdOldFolio, @OldRevenueControlId
		while @@fetch_status = 0
		begin			
			--select 'Folios' + cast(@IdOldFolio as varchar)
			insert into Billing.RevenueControlDetail (RevenueControlId, BillingAuthorizationId, FolioOrder
			, FolioType, LiquidationType, ContractEntityId, HealthAdministratorId
			, ThirdPartyId, CareGroupId, TotalFolio, ResponsibleRecoveryFee
			, TotalPatientSalesPrice, PatientDiscount, PatientDiscountPercentage
			, TotalPatientWithDiscount, ValueCopay, ValueFeeModerator
			, ValueVoucher, Observation, InvoiceCategoryId, OutputDate
			, IsCutAccount, OutputDiagnosis, [Status], CreationDate, CreationUser)
			select @RevenueControlId, BillingAuthorizationId
			, (select Coalesce(Max(FolioOrder), 0) + 1 from Billing.RevenueControlDetail where RevenueControlId = @RevenueControlId) --verificar si aumenta bien
			, FolioType, LiquidationType, ContractEntityId, HealthAdministratorId
			, ThirdPartyId, CareGroupId, TotalFolio, ResponsibleRecoveryFee
			, TotalPatientSalesPrice, PatientDiscount, PatientDiscountPercentage
			, TotalPatientWithDiscount, ValueCopay, ValueFeeModerator
			, ValueVoucher, Observation, InvoiceCategoryId, OutputDate
			, IsCutAccount, OutputDiagnosis, [Status], [Common].[GETDATE](), @UserCode 
			from @tmpOldFolios where Id = @IdOldFolio

			set @IdNewFolio = scope_identity()

			update Billing.ServiceOrderDetailDistribution set RevenueControlDetailId = @IdNewFolio
			where RevenueControlDetailId = @IdOldFolio And Id In (select IdDistribution from @tmpDistributionsId)
			
			if (select count(*) from Billing.ServiceOrderDetailDistribution where RevenueControlDetailId = @IdOldFolio) = 0 begin
				--folio a eliminar
				insert into @tmpFolioToRemove
				select @IdOldFolio, @OldRevenueControlId
			end

			exec [Billing].[SP_UpdateRevenueControlDetailValues_Output] @IdOldFolio, NULL, @StatusResult OUT, @MessageResult OUT
			IF @StatusResult = 0
			BEGIN
				SELECT @StatusResult AS StatusResult, @MessageResult AS MessageResult
				RETURN
			END

			exec [Billing].[SP_UpdateRevenueControlDetailValues_Output] @IdNewFolio, NULL, @StatusResult OUT, @MessageResult OUT
			IF @StatusResult = 0
			BEGIN
				SELECT @StatusResult AS StatusResult, @MessageResult AS MessageResult
				RETURN
			END
			
			fetch next from cursor_folio into @IdOldFolio, @OldRevenueControlId
		end
		close cursor_folio
		deallocate cursor_folio
		
		update Billing.RevenueControl set FolioQuantity = 
			(select Coalesce(Count(*), 0) From Billing.RevenueControlDetail where RevenueControlId = @RevenueControlId) Where Id = @RevenueControlId

		declare @folioToUpdateId int, @folioOrder int = 1
		declare cursor_folio_actualizar cursor for
		select Id from Billing.RevenueControlDetail r
		inner join @tmpFolioToRemove td on r.RevenueControlId = td.RevenueControlId 
		where r.Id Not In (select RevenueControlDetailId from @tmpFolioToRemove) 
		open cursor_folio_actualizar
		fetch next from cursor_folio_actualizar into @folioToUpdateId
		while @@fetch_status = 0
		begin			
			update Billing.RevenueControlDetail set FolioOrder = @folioOrder where Id = @folioToUpdateId
			set @folioOrder = @folioOrder + 1
			fetch next from cursor_folio_actualizar into @folioToUpdateId
		end
		close cursor_folio_actualizar
		deallocate cursor_folio_actualizar

		--tratamos de eliminar los folios vacíos
		delete Billing.RevenueRecognitionDetail where RevenueControlDetailId In (select RevenueControlDetailId from @tmpFolioToRemove) /*se elimina el detalle de los reconocimiento en estado reversado*/
		delete Billing.RevenueControlDetail where Id In (select RevenueControlDetailId from @tmpFolioToRemove)
		update Billing.RevenueControl set FolioQuantity = (@folioOrder - 1) Where Id = (select top 1 RevenueControlId from @tmpFolioToRemove)

		/************************************************* RESULTADO *************************************************/
		
		SELECT CONVERT(BIT, 1) AS StatusResult, '' AS MessageResult
	END TRY
	BEGIN CATCH
		SELECT CONVERT(BIT, 0) AS StatusResult, 'Error ! '+ ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50)) AS MessageResult
	END CATCH
END