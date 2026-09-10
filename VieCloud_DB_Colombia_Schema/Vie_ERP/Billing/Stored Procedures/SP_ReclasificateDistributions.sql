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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de reclasificación de distribuciones financieras de órdenes de servicio entre controles de ingreso (admisiones). Permite mover líneas de distribución de facturación —que representan el reparto del valor de un servicio entre asegurador y paciente— desde sus folios originales hacia un nuevo control de ingresos (admisión destino), creando folios equivalentes en el control de destino y reasignando las distribuciones correspondientes. Antes de ejecutar la reclasificación, valida que los folios origen estén en estado ''Registrado'' (no facturados, bloqueados, anulados ni con reconocimiento de ingresos activo), protegiendo la integridad contable y evitando mover servicios ya comprometidos en procesos de facturación o causación contable. Es utilizado cuando una o varias prestaciones de salud fueron cargadas a la admisión equivocada y deben trasladarse a la admisión correcta sin afectar la trazabilidad de folios, copagos, cuotas moderadoras y reconocimiento de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReclasificateDistributions';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_ReclasificateDistributions';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reclasifica líneas de distribución de servicios hacia un nuevo control de ingresos, clonando los folios involucrados, reasignando las distribuciones, recalculando valores y depurando folios que quedan sin distribuciones.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReclasificateDistributions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener nodos /ServiceOrderDetailDistributionIds/Ids con el Id de cada distribución a reclasificar.; Todas las distribuciones recibidas deben pertenecer a folios (RevenueControlDetail) en estado 1 (Registrado); si alguna está en estado 2 (Facturado), 3 (Bloqueado), 4 (Anulado), 5 (Reconocimiento Ingresos) o 6 (Factura Asociada) se aborta.; No debe existir un reconocimiento de ingresos activo (RevenueRecognition.State <> 2) ligado a las líneas a reclasificar.; Debe existir un RevenueControl destino válido al que se asignarán los nuevos folios.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReclasificateDistributions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada folio origen afectado se clona una sola vez por ejecución (cursor sobre @tmpOldFolios construido con DISTINCT).; El FolioOrder del nuevo folio creado en el RevenueControl destino siempre es MAX(FolioOrder)+1 vigente en ese RevenueControl, garantizando numeración correlativa.; Solo se eliminan folios origen que quedaron sin ninguna distribución asociada después del UPDATE.; Antes de eliminar un RevenueControlDetail siempre se elimina previamente su RevenueRecognitionDetail dependiente.; FolioQuantity de RevenueControl (destino y origen) siempre queda sincronizado con la cantidad real de folios tras la operación.; Toda la operación se envuelve en TRY/CATCH devolviendo siempre un resultset (StatusResult, MessageResult).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReclasificateDistributions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Billing.RevenueControlDetail: Por cada folio origen distinto afectado por las distribuciones se crea un nuevo RevenueControlDetail clonando todos sus atributos pero asignando el RevenueControlId destino, FolioOrder = MAX(FolioOrder)+1 dentro del nuevo RevenueControl, CreationDate=[Common].[GETDATE]() y CreationUser=@UserCode.; [UPDATE] Billing.ServiceOrderDetailDistribution: Las distribuciones recibidas en el XML cuyo RevenueControlDetailId coincide con un folio origen se reasignan al nuevo RevenueControlDetailId recién creado.; [UPDATE] Billing.RevenueControl: Tras crear los nuevos folios se actualiza FolioQuantity del RevenueControl destino con el COUNT(*) de RevenueControlDetail asociado.; [UPDATE] Billing.RevenueControlDetail: En los RevenueControl origen cuyos folios quedaron vacíos se renumera FolioOrder secuencialmente (1..N) sobre los folios que NO serán eliminados.; [DELETE] Billing.RevenueRecognitionDetail: Se elimina el detalle de reconocimiento de ingresos (en estado reversado) cuyo RevenueControlDetailId corresponde a folios origen que quedaron sin distribuciones.; [DELETE] Billing.RevenueControlDetail: Se eliminan los folios origen que quedaron sin ninguna ServiceOrderDetailDistribution asociada tras la reclasificación.; [UPDATE] Billing.RevenueControl: Se actualiza FolioQuantity del RevenueControl origen con la cantidad final de folios renumerados (@folioOrder - 1).; [RETURN_RESULT] RESULT: Devuelve StatusResult=0 con mensaje detallado listando Admisión/Folio/Estado cuando alguna línea no está en folio en estado registrado; StatusResult=0 con ''existe un reconocimiento activo'' si hay RevenueRecognition.State<>2; StatusResult=0 si SP_UpdateRevenueControlDetailValues_Output falla; StatusResult=1 con MessageResult vacío en éxito; StatusResult=0 con ERROR_MESSAGE+ERROR_LINE en CATCH.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReclasificateDistributions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen distribuciones cuyo RevenueControlDetail tiene Status <> 1 (no Registrado) → Construye mensaje listando admisión, folio y estado (Facturado/Bloqueado/Anulado/Reconocimiento Ingresos/Factura Asociada) y retorna StatusResult=0 sin reclasificar. else Continúa con validación de reconocimiento activo.; si Existe un RevenueRecognition asociado con State <> 2 → Retorna StatusResult=0 con mensaje ''existe un reconocimiento activo'' y aborta. else Procede a clonar folios y reasignar distribuciones.; si Tras reasignar distribuciones, COUNT de ServiceOrderDetailDistribution para el folio origen = 0 → Marca el folio origen en @tmpFolioToRemove para renumeración y posterior eliminación junto con su detalle de reconocimiento. else El folio origen se conserva sin cambios estructurales.; si SP_UpdateRevenueControlDetailValues_Output devuelve @StatusResult=0 (sobre folio antiguo o nuevo) → Retorna inmediatamente el StatusResult/MessageResult de la dependencia y aborta el proceso. else Continúa con la siguiente iteración del cursor.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReclasificateDistributions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Billing.SP_UpdateRevenueControlDetailValues_Output', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReclasificateDistributions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.ServiceOrderDetailDistribution; Billing.RevenueControlDetail; Billing.RevenueControl; Billing.RevenueRecognitionDetail; Billing.RevenueRecognition', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReclasificateDistributions';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_ReclasificateDistributions';
-- GO
