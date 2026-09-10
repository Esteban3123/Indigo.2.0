-- =============================================
-- Author:		Diego A. Roldán
-- Create date: 2018-04-23
-- Description:	Liquida cuota recuperacion a paciente
-- =============================================
CREATE PROCEDURE [Billing].[SP_LiquidatePatientShare]
	@RevenueControlId Int,
	@TotalsItemsApplyRecoveryFee Decimal(18, 0),
	@TotalFolio Decimal(18, 0),
	@AdmissionCode Varchar(20),
	@ServiceorderDetailDistributionListIdXml Xml,
	@ListItemsApplyRecoveryFeeXml Xml
AS
Begin
	Set Nocount On;
		
	Begin Try
		--se obtiene el valor redondeado
		Declare @ValueProcessRecoveryFee Decimal(18, 0) = 0,
				@TypeRecoveryFeeProcess Int = 3,
				@PercentageProcessRecoveryFee Decimal(6, 2) = 100,
				@ValueProcess Decimal(18, 0) = 0,
				@RoundLevel INT = -2,
				@SettlementByItem bit = 0

		SELECT TOP 1 @RoundLevel =	CASE sb.RoundingTypeRecoveryFeeType
										WHEN 1 THEN 0
										WHEN 2 THEN -1
										WHEN 3 THEN -2
										WHEN 4 THEN -3
										ELSE @RoundLevel
									END
		FROM Billing.SettingsBilling sb

		Declare @ServiceorderDetailDistributionListId As Table(IdRow int identity(1,1), Id Int)
		Declare @ListItemsApplyRecoveryFee As Table(Id Int Primary key)
		
		Insert Into @ServiceorderDetailDistributionListId
		Select t.x.value('Id[1]','int')			
		From @ServiceorderDetailDistributionListIdXml.nodes('/ServiceorderDetailDistribution') t(x)

		Insert Into @ListItemsApplyRecoveryFee
		Select t.x.value('Id[1]','int')			
		From @ListItemsApplyRecoveryFeeXml.nodes('/ListItemsApplyRecoveryFee') t(x)
		
		Declare @PatientType Int, @LiquidationType Int, @PatientTypeAffiliation int, @AdmissionType Int,
			@NivelModeratorShareTop Decimal(19, 4), @NivelModeratorShareTopYear Decimal(19, 4),
			@PatientCode Varchar(20), @PAGADOCMO Decimal(18, 0), @PAGADOCOP Decimal(18, 0),
			@NivelModeratorSharePercentage Decimal(6, 2),
			@NivelCoPayContribPercentage Decimal(6, 2), @NivelCoPayContribTop Decimal(19, 4),
			@NivelCoPayContribTopYear Decimal(19, 4), @NivelCoPaySubsiPercentage Numeric(6, 2),
			@NivelCoPaySubsibTopYear Decimal(19, 4), @NivelCoPaySubsibTop Decimal(19, 4),
			@NivelCoPayVincuPercentage Numeric(6, 2), @NivelCoPayVincuTopYear Decimal(19, 4),
			@NivelCoPayVincuTop Decimal(19, 4),	@ServiceOrderDetailDistributionLastId Int
			,@SubTotalPatientSalesPrice numeric(18,2)

			
		SELECT @AdmissionType = TAdmissions.TIPOINGRE,
		@LiquidationType = TAdmissions.ILIQUIDAC,	
		
		@PatientCode = TPatient.IPCODPACI,		
		@PatientType = TPatient.IPTIPOPAC,
		@PatientTypeAffiliation = TPatient.IPTIPOAFI,
		@NivelModeratorSharePercentage = TNiveles.NIVPORCMO,
		@NivelCoPayContribPercentage = TNiveles.NIVPORCOP,
		@NivelCoPaySubsiPercentage = TNiveles.NIVPORSUB,
		@NivelCoPayVincuPercentage = TNiveles.NIVPORVIN,
		@NivelModeratorShareTop = TNiveles.TOPEVECMO,
		@NivelCoPayContribTop = TNiveles.TOPEVECOP,
		@NivelCoPaySubsibTop = TNiveles.TOPEVESUB,
		@NivelCoPayVincuTop = TNiveles.TOPEVEVIN,
		@NivelModeratorShareTopYear = TNiveles.TOPANUCMO,
		@NivelCoPayContribTopYear = TNiveles.TOPANUCOP,
		@NivelCoPaySubsibTopYear = TNiveles.TOPANUSUB,
		@NivelCoPayVincuTopYear = TNiveles.TOPANUVIN		
		FROM dbo.ADINGRESO TAdmissions WITH(NOLOCK)
		INNER JOIN dbo.INPACIENT TPatient WITH(NOLOCK) ON TAdmissions.IPCODPACI = TPatient.IPCODPACI
		INNER JOIN .ADNIVELES TNiveles WITH(NOLOCK) ON TPatient.NIVCODIGO = TNiveles.NIVCODIGO
		INNER JOIN .ADCENATEN TCentAtenc WITH(NOLOCK) ON TAdmissions.CODCENATE = TCentAtenc.CODCENATE
		INNER JOIN .INUNIFUNC TUniFunc WITH(NOLOCK) ON TAdmissions.UFUCODIGO = TUniFunc.UFUCODIGO
		WHERE LTRIM(RTRIM(TAdmissions.NUMINGRES)) = @AdmissionCode
		
		Select Top 1 @PAGADOCMO = PAGADOCMO, @PAGADOCOP = PAGADOCOP 
		From .INPACIENTTOPANU With(Nolock) 
		Where IPCODPACI = @PatientCode And ANIO = Year([Common].[GETDATE]())
		
		--si se seleccionaron algunos items en la rejilla se toma el valor de esos
		If @TotalsItemsApplyRecoveryFee > 0 Begin
			Set @ValueProcess = @TotalsItemsApplyRecoveryFee
		End
		Else Begin
			--se toma el valor del folio
			Set @ValueProcess = @TotalFolio
		End

		If @PatientType = 1 Begin --Contributivo			
			--se obtiene si se debe pagar cuota moderadora o copago
			If @LiquidationType = 1 --Copago				
				Set @TypeRecoveryFeeProcess = 3
			Else If @LiquidationType = 2 --Cuota Moderadora				
				Set @TypeRecoveryFeeProcess = 2
			Else If @LiquidationType = 4 Begin --Si puede ser Copago o cuota moderadora lo define entonces el tipo del ingreso
				set @SettlementByItem = 1
				--HRR INICIO cm/cp		
				--tabla de detalles
					DECLARE @Detalles as table(
					IdRow int identity(1,1),
					ServiceOrderDetailDistributionId INT,
					SubTotalPatientSalesPrice numeric(18,2),
					GrandTotalSalesPrice NUMERIC(18,2),
					ApplyRecoveryFee TINYINT,
					PatientPercentage NUMERIC(5,2),
					RecordType	tinyint,
					IPSCode VARCHAR(20) NULL,
					IPSName VARCHAR(300) NULL,
					RateManualCode VARCHAR(20) NULL,
					RateManualName VARCHAR(100) NULL,
					OutPatientRecoveryFeeType TINYINT NULL,
					InPatientRecoveryFeeType TINYINT NULL,
					Mensaje VARCHAR(200) NULL,
					InvalidId int,
					RecoveryFeeType TINYINT,
					ThirdPartySalesPrice numeric(18,2) ,
					ThirdPartyPercentage NUMERIC(5,2) ,
					Processed bit,
					StateResult TINYINT NULL,
					NivelTopValue DECIMAL(18,2), 
					NivelApplyPercentage Decimal(6,2), 
					AmmountAnualTop DECIMAL(18,2)
					)
				
					insert into @Detalles(
					ServiceOrderDetailDistributionId,
					SubTotalPatientSalesPrice,
					GrandTotalSalesPrice,
					ApplyRecoveryFee,
					PatientPercentage,
					RecordType,
					IPSCode,
					IPSName,
					RateManualCode,
					RateManualName,
					InvalidId,
					OutPatientRecoveryFeeType,
					InPatientRecoveryFeeType,
					Processed)
					select SODD.Id, SODD.SubTotalPatientSalesPrice, SODD.GrandTotalSalesPrice, 
					SODD.ApplyRecoveryFee, SODD.PatientPercentage,
					SOD.RecordType, 
					IPS.Code IPSCode, IPS.Name IPSName, RM.Code RateManualCode, RM.Name RateManualName
					,Coalesce(ARF.Id, 0)
					,IPS.OutPatientRecoveryFeeType
					,IPS.InPatientRecoveryFeeType,0
					from @ServiceorderDetailDistributionListId LISTA
					inner join BILLING.ServiceOrderDetailDistribution SODD WITH(NOLOCK) ON SODD.Id = LISTA.Id
					INNER JOIN billing.ServiceOrderDetail SOD WITH(NOLOCK) ON SOD.Id = SODD.ServiceOrderDetailId
					LEFT OUTER JOIN Contract.IPSService IPS WITH(NOLOCK) ON IPS.ID = SOD.IPSServiceId
					LEFT OUTER JOIN Contract.RateManualDetail RMD WITH(NOLOCK) ON RMD.Id = SOD.RateManualDetailId
					LEFT OUTER JOIN Contract.RateManual RM WITH(NOLOCK) ON RM.Id = RMD.RateManualId
					LEFT OUTER JOIN @ListItemsApplyRecoveryFee ARF ON ARF.ID = SODD.Id
										
					UPDATE D SET Processed = 1, StateResult = 2 --items que no se calcula copago o cuota
					FROM @Detalles D WHERE D.InvalidId IS NULL OR D.InvalidId = 0

					--MEDICAMENTOS cuota
					update D SET				
					SubTotalPatientSalesPrice = @NivelModeratorShareTop,
					RecoveryFeeType = 2,
					ApplyRecoveryFee = 2, 
					PatientPercentage = @NivelModeratorSharePercentage,
					ThirdPartySalesPrice = D.GrandTotalSalesPrice - @NivelModeratorShareTop,
					ThirdPartyPercentage = 100 - @NivelModeratorSharePercentage,
					Processed = 1,
					NivelTopValue = @NivelModeratorShareTop, 
					NivelApplyPercentage = @NivelModeratorSharePercentage, 
					AmmountAnualTop = @NivelModeratorShareTopYear
					FROM @Detalles D
					WHERE D.Processed = 0 AND D.RecordType = 2 AND @AdmissionType = 1 AND  
					(	(--(@LiquidationType = 0 OR @LiquidationType = 1 OR @LiquidationType = 2 ) AND 
					(D.ApplyRecoveryFee = 0 OR D.ApplyRecoveryFee = 1))
					) 
									
					update D SET				
					SubTotalPatientSalesPrice = 0,
					RecoveryFeeType = 1,
					ApplyRecoveryFee = 0, 
					PatientPercentage = 0,
					ThirdPartySalesPrice = D.GrandTotalSalesPrice,
					ThirdPartyPercentage = 100,
					Processed = 1,
					StateResult = 1,
					NivelTopValue = @NivelModeratorShareTop, 
					NivelApplyPercentage = @NivelModeratorSharePercentage, 
					AmmountAnualTop = @NivelModeratorShareTopYear
					FROM @Detalles D
					WHERE D.Processed = 0 AND D.RecordType = 2 AND @AdmissionType = 1 AND 
					--@LiquidationType = 1 AND 
					D.ApplyRecoveryFee = 2

					--MEDICAMENTOS copago
					update D SET				
					SubTotalPatientSalesPrice = (D.GrandTotalSalesPrice * (@NivelCoPayContribPercentage / 100)),
					RecoveryFeeType = 3,
					ApplyRecoveryFee = 2, 
					PatientPercentage = @NivelCoPayContribPercentage,
					ThirdPartySalesPrice = D.GrandTotalSalesPrice - (D.GrandTotalSalesPrice * (@NivelCoPayContribPercentage / 100)),
					ThirdPartyPercentage = 100 - @NivelCoPayContribPercentage,
					Processed = 1,
					NivelTopValue = @NivelCoPayContribTop, 
					NivelApplyPercentage = @NivelCoPayContribPercentage, 
					AmmountAnualTop = @NivelCoPayContribTopYear
					FROM @Detalles D
					WHERE D.Processed = 0 AND D.RecordType = 2 AND @AdmissionType <> 1 AND 
					(	(--(@LiquidationType = 0 OR @LiquidationType = 1 OR @LiquidationType = 2 ) AND 
					(D.ApplyRecoveryFee = 0 OR D.ApplyRecoveryFee = 1))
					)

					update D SET				
					SubTotalPatientSalesPrice = 0,
					RecoveryFeeType = 1,
					ApplyRecoveryFee = 0, 
					PatientPercentage = 0,
					ThirdPartySalesPrice = D.GrandTotalSalesPrice,
					ThirdPartyPercentage = 100,
					Processed = 1,
					StateResult = 1
					FROM @Detalles D
					WHERE D.Processed = 0 AND D.RecordType = 2 AND @AdmissionType <> 1 AND 
					--@LiquidationType = 1 AND 
					D.ApplyRecoveryFee = 2

					--Servcios cuota
					update D SET				
					SubTotalPatientSalesPrice = @NivelModeratorShareTop,
					RecoveryFeeType = 2,
					ApplyRecoveryFee = 2, 
					PatientPercentage = @NivelModeratorSharePercentage,
					ThirdPartySalesPrice = D.GrandTotalSalesPrice - @NivelModeratorShareTop,
					ThirdPartyPercentage = 100 - @NivelModeratorSharePercentage,
					Processed = 1,
					NivelTopValue = @NivelModeratorShareTop, 
					NivelApplyPercentage = @NivelModeratorSharePercentage, 
					AmmountAnualTop = @NivelModeratorShareTopYear
					FROM @Detalles D
					WHERE D.Processed = 0 AND D.RecordType <> 2 
					AND 2 = case when @AdmissionType = 1 then D.OutPatientRecoveryFeeType else D.InPatientRecoveryFeeType end
					AND 
					(	(--(@LiquidationType = 0 OR @LiquidationType = 1 OR @LiquidationType = 2 ) AND 
					(D.ApplyRecoveryFee = 0 OR D.ApplyRecoveryFee = 1)))

					update D SET				
					SubTotalPatientSalesPrice = 0,
					RecoveryFeeType = 1,
					ApplyRecoveryFee = 0, 
					PatientPercentage = 0,
					ThirdPartySalesPrice = D.GrandTotalSalesPrice,
					ThirdPartyPercentage = 100,
					Processed = 1,
					StateResult = 1,
					NivelTopValue = @NivelModeratorShareTop, 
					NivelApplyPercentage = @NivelModeratorSharePercentage, 
					AmmountAnualTop = @NivelModeratorShareTopYear
					FROM @Detalles D
					WHERE D.Processed = 0 AND D.RecordType <> 2 
					AND 2 = case when @AdmissionType = 1 then D.OutPatientRecoveryFeeType else D.InPatientRecoveryFeeType end
					--AND @LiquidationType = 1 
					AND D.ApplyRecoveryFee = 2

					--SERVICIOS COPAGO
					update D SET				
					SubTotalPatientSalesPrice = (D.GrandTotalSalesPrice * (@NivelCoPayContribPercentage / 100)),
					RecoveryFeeType = 3,
					ApplyRecoveryFee = 2, 
					PatientPercentage = @NivelCoPayContribPercentage,
					ThirdPartySalesPrice = D.GrandTotalSalesPrice - (D.GrandTotalSalesPrice * (@NivelCoPayContribPercentage / 100)),
					ThirdPartyPercentage = 100 - @NivelCoPayContribPercentage,
					Processed = 1,
					NivelTopValue = @NivelCoPayContribTop, 
					NivelApplyPercentage = @NivelCoPayContribPercentage, 
					AmmountAnualTop = @NivelCoPayContribTopYear
					FROM @Detalles D
					WHERE D.Processed = 0 AND D.RecordType <> 2 AND @PatientTypeAffiliation <> 1 
					AND 3 = case when @AdmissionType = 1 then D.OutPatientRecoveryFeeType else D.InPatientRecoveryFeeType end
					AND 
					(	(--(@LiquidationType = 0 OR @LiquidationType = 1 OR @LiquidationType = 2 ) AND 
					(D.ApplyRecoveryFee = 0 OR D.ApplyRecoveryFee = 1))
					) 

					update D SET				
					SubTotalPatientSalesPrice = 0,
					RecoveryFeeType = 1,
					ApplyRecoveryFee = 0, 
					PatientPercentage = 0,
					ThirdPartySalesPrice = D.GrandTotalSalesPrice,
					ThirdPartyPercentage = 100,
					Processed = 1,
					StateResult = 1
					FROM @Detalles D
					WHERE D.Processed = 0 AND D.RecordType <> 2 AND @PatientTypeAffiliation <> 1 
					AND 3 = case when @AdmissionType = 1 then D.OutPatientRecoveryFeeType else D.InPatientRecoveryFeeType end
					--AND @LiquidationType = 1 
					AND D.ApplyRecoveryFee = 2

					--SERVICIOS COPAGO --COTIZANTE
					update D SET				
					SubTotalPatientSalesPrice = 0,
					RecoveryFeeType = 1,
					ApplyRecoveryFee = 0, 
					PatientPercentage = 0,
					ThirdPartySalesPrice = D.GrandTotalSalesPrice,
					ThirdPartyPercentage = 100,
					Processed = 1,
					StateResult = 0,
					Mensaje = 'El paciente es contributivo y cotizante, por lo cual no se puede cobrar COPAGO'
					FROM @Detalles D
					WHERE D.Processed = 0 AND D.RecordType <> 2 AND @PatientTypeAffiliation = 1 
					AND 3 = case when @AdmissionType = 1 then D.OutPatientRecoveryFeeType else D.InPatientRecoveryFeeType end
					
					update D SET				
					SubTotalPatientSalesPrice = 0,
					RecoveryFeeType = 1,
					ApplyRecoveryFee = 0, 
					PatientPercentage = 0,
					ThirdPartySalesPrice = D.GrandTotalSalesPrice,
					ThirdPartyPercentage = 100,
					Processed = 1,
					StateResult = 0,
					Mensaje = 'El servicio IPS ('+D.IPSCode+' - '+D.IPSName+'), está parametrizado como NINGUNO, para el tipo de ingreso'
					FROM @Detalles D
					WHERE D.Processed = 0 AND D.RecordType <> 2
					AND 1 = case when @AdmissionType = 1 then D.OutPatientRecoveryFeeType else D.InPatientRecoveryFeeType end
					
					update D SET				
					SubTotalPatientSalesPrice = 0,
					RecoveryFeeType = 1,
					ApplyRecoveryFee = 0, 
					PatientPercentage = 0,
					ThirdPartySalesPrice = D.GrandTotalSalesPrice,
					ThirdPartyPercentage = 100,
					Processed = 1,
					StateResult = 0,
					Mensaje = 'El manual ('+D.RateManualCode+' - '+D.RateManualName+') al que pertenece el servicio, no se encuentra debidamente parametrizado para el tipo de ingreso'
					FROM @Detalles D
					WHERE D.Processed = 0

					Declare @IdRow int = 1,	@IdRowMax int, 
					@NivelTopValue DECIMAL(18,2), 
					@AmmountAnualTop DECIMAL(18,2),
					@GrandTotalSalesPrice NUMERIC(18,2),
					@RecoveryFeeType TINYINT,
					@CurrentAmmountAnualCOP NUMERIC(18,2) = @PAGADOCOP, 
					@CurrentAmmountAnualCMO NUMERIC(18,2) = @PAGADOCMO,
					@TopEventFeeModerator DECIMAL(18,2),
					@TopEventCopay DECIMAL(18,2),
					@StateResult TINYINT,
					@Mensaje varchar(1000)
					
					select @TopEventFeeModerator = TopEventFeeModerator,
					@TopEventCopay = TopEventCopay
					From Billing.RevenueControl where Id = @RevenueControlId

					select @IdRowMax = max(IdRow) FROM @Detalles D WHERE D.Processed = 1 AND D.StateResult IS NULL
					while @IdRow <= @IdRowMax
					begin

						IF (SELECT 1 FROM @Detalles D WHERE D.Processed = 1 AND D.StateResult IS NULL AND D.IdRow = @IdRow) > 0
						begin
							
							select @NivelTopValue = D.NivelTopValue
							,@AmmountAnualTop = D.AmmountAnualTop, @GrandTotalSalesPrice = D.GrandTotalSalesPrice, @SubTotalPatientSalesPrice = D.SubTotalPatientSalesPrice
							,@RecoveryFeeType = D.RecoveryFeeType
							from @Detalles D WHERE D.Processed = 1 AND D.StateResult IS NULL AND D.IdRow = @IdRow

							SET @StateResult = 3
							set @Mensaje = ''

							IF @RecoveryFeeType = 2 --CUOTA
							BEGIN
								
								IF @TopEventFeeModerator < @NivelTopValue
								BEGIN
									set @SubTotalPatientSalesPrice = @NivelModeratorShareTop
									if @SubTotalPatientSalesPrice > @GrandTotalSalesPrice
									begin
										set @SubTotalPatientSalesPrice = @GrandTotalSalesPrice										
									end
									If (@TopEventFeeModerator + @SubTotalPatientSalesPrice) > @NivelTopValue
									BEGIN
										SET @StateResult = 4
										SET @SubTotalPatientSalesPrice = @NivelTopValue - @TopEventFeeModerator
										set @Mensaje = 'Solo se liquido la cantidad de '+cast(@SubTotalPatientSalesPrice as VARCHAR(20))+' debido a que se completo el tope por nivel'
									END
									If @AmmountAnualTop > 0 
									BEGIN
										If @SubTotalPatientSalesPrice > (@AmmountAnualTop - @CurrentAmmountAnualCMO) 
										BEGIN
											SET @SubTotalPatientSalesPrice = (@AmmountAnualTop - @CurrentAmmountAnualCMO)
											SET @StateResult = 4
											If @SubTotalPatientSalesPrice > 0
											begin
												set @Mensaje = 'Solo se liquido la cantidad de '+cast(@SubTotalPatientSalesPrice as VARCHAR(20))+' debido a que se completo el tope anual'
											end
											else
											begin
												set @Mensaje =  'Se completó el tope maximo anual'
											end
										END
									END
					
									IF @SubTotalPatientSalesPrice > 0 OR @StateResult = 3
									BEGIN
										
										SET @SubTotalPatientSalesPrice = ROUND( @SubTotalPatientSalesPrice, 0)
										UPDATE D SET STATERESULT = @StateResult, SubTotalPatientSalesPrice = @SubTotalPatientSalesPrice
										,ThirdPartySalesPrice = D.GrandTotalSalesPrice - @SubTotalPatientSalesPrice
										,Mensaje = @Mensaje
										from @Detalles D WHERE D.Processed = 1 AND D.StateResult IS NULL AND D.IdRow = @IdRow
										SET @TopEventFeeModerator = @TopEventFeeModerator + @SubTotalPatientSalesPrice 
										SET @CurrentAmmountAnualCMO = @CurrentAmmountAnualCMO + @SubTotalPatientSalesPrice 
										set @NivelModeratorShareTop = @NivelModeratorShareTop - @SubTotalPatientSalesPrice
									END
									ELSE
									BEGIN
										UPDATE D SET SubTotalPatientSalesPrice = 0, RecoveryFeeType = 1, 
										ApplyRecoveryFee = 1,
										PatientPercentage = 0, 
										ThirdPartySalesPrice = GrandTotalSalesPrice,
										ThirdPartyPercentage = 100,
										StateResult = 4,
										Mensaje = CASE WHEN @Mensaje = '' THEN 'Se completo el tope maximo' else @Mensaje END
										from @Detalles D WHERE D.Processed = 1 AND D.StateResult IS NULL AND D.IdRow = @IdRow
									END
								END
								ELSE
								BEGIN
									UPDATE D SET SubTotalPatientSalesPrice = 0, RecoveryFeeType = 1, 
										ApplyRecoveryFee = 1,
										PatientPercentage = 0, 
										ThirdPartySalesPrice = GrandTotalSalesPrice,
										ThirdPartyPercentage = 100,
										StateResult = 4,
										Mensaje = 'Ya se superó el monto máximo de CUOTA MODERADORA para el evento'
									from @Detalles D WHERE D.Processed = 1 AND D.StateResult IS NULL AND D.IdRow = @IdRow
								END
							END
					
					
							IF @RecoveryFeeType = 3 --COPAGO
							BEGIN							
								IF @TopEventCopay < @NivelTopValue
								BEGIN
									If (@TopEventCopay + @SubTotalPatientSalesPrice) > @NivelTopValue
									BEGIN
										
										SET @StateResult = 4
										SET @SubTotalPatientSalesPrice = @NivelTopValue - @TopEventCopay
										set @Mensaje =CONCAT('Solo se liquido la cantidad de ', cast(@SubTotalPatientSalesPrice as VARCHAR(20)),' debido a que se completo el tope por nivel')
																			
									END
									If @AmmountAnualTop > 0 
									BEGIN
										
										If @SubTotalPatientSalesPrice > (@AmmountAnualTop - @CurrentAmmountAnualCOP) 
										BEGIN
											SET @SubTotalPatientSalesPrice = (@AmmountAnualTop - @CurrentAmmountAnualCOP)
											SET @StateResult = 4
											If @SubTotalPatientSalesPrice > 0
											begin
												set @Mensaje = 'Solo se liquido la cantidad de '+cast(@SubTotalPatientSalesPrice as VARCHAR(20))+' debido a que se completo el tope anual'
											end
											else
											begin
												set @Mensaje =  'Se completó el tope maximo anual'
											end
										END
									END

									IF @SubTotalPatientSalesPrice > 0  OR @StateResult = 3
									BEGIN
										
										SET @SubTotalPatientSalesPrice = ROUND( @SubTotalPatientSalesPrice, 0)
										UPDATE D SET STATERESULT = @StateResult, SubTotalPatientSalesPrice = @SubTotalPatientSalesPrice
										,ThirdPartySalesPrice = D.GrandTotalSalesPrice - @SubTotalPatientSalesPrice
										,Mensaje = @Mensaje
										from @Detalles D
										WHERE D.Processed = 1 AND D.StateResult IS NULL AND D.IdRow = @IdRow

										SET @TopEventCopay = @TopEventCopay + @SubTotalPatientSalesPrice 
										SET @CurrentAmmountAnualCOP = @CurrentAmmountAnualCOP + @SubTotalPatientSalesPrice 
									END
									ELSE
									BEGIN
										
										UPDATE D SET SubTotalPatientSalesPrice = 0, RecoveryFeeType = 1, 
										ApplyRecoveryFee = 1,
										PatientPercentage = 0, 
										ThirdPartySalesPrice = GrandTotalSalesPrice,
										ThirdPartyPercentage = 100,
										StateResult = 4,
										Mensaje = CASE WHEN @Mensaje = '' THEN 'Se completo el tope maximo' else @Mensaje END
										from @Detalles D WHERE D.Processed = 1 AND D.StateResult IS NULL AND D.IdRow = @IdRow
									END
								END
								ELSE
								BEGIN
									UPDATE D SET SubTotalPatientSalesPrice = 0, RecoveryFeeType = 1, 
										ApplyRecoveryFee = 1,
										PatientPercentage = 0, 
										ThirdPartySalesPrice = GrandTotalSalesPrice,
										ThirdPartyPercentage = 100,
										StateResult = 4,
										Mensaje = CONCAT('Ya se superó el monto máximo de COPAGO para el evento ', D.IPSCode,'')
									from @Detalles D WHERE D.Processed = 1 AND D.StateResult IS NULL AND D.IdRow = @IdRow
								END
							END
						END
						set @IdRow = @IdRow + 1
					END

				--StateResult = 2 --items marcados para no calcular copago o cuota --NO BLOQUEANTE
				--StateResult = 1 --en su configuracion no aplica copago o cuota --NO BLOQUEANTE
				--StateResult = 3 --se liquida con valor o sin valor el maximo de cuota --NO BLOQUEANTE
				--StateResult = 0 --El paciente es contributivo y cotizante, por lo cual no se puede cobrar COPAGO --BLOQUEANTE
				--StateResult = 0 --El servicio IPS ('+D.IPSCode+' - '+D.IPSName+'), está parametrizado como NINGUNO, para el tipo de ingreso --BLOQUEANTE
				--StateResult = 0 --El manual ('+D.RateManualCode+' - '+D.RateManualName+') al que pertenece el servicio, no se encuentra debidamente parametrizado para el tipo de ingreso --BLOQUEANTE
				--stateResult = 4 --ALCANZA EL TOP
				DECLARE @HasStateResult INT
				SELECT @HasStateResult = COUNT(*) FROM @Detalles D WHERE D.StateResult IN (0, 4)

				IF @HasStateResult > 0
				BEGIN		
					
					SELECT Convert(Bit, 0) As StatusResult,CAST(CONCAT('No se logró liquidar la Cuota: ', STUFF((SELECT ', ' + D.Mensaje AS [text()]
																	FROM @Detalles D
																	WHERE D.StateResult IN (0,4) FOR XML PATH('')), 1, 2, '')) AS VARCHAR(1000)) As MessageResult
					RETURN
				END

				IF @NivelModeratorShareTop > 0 AND EXISTS(SELECT 1 FROM @Detalles D WHERE D.RecoveryFeeType = 2 
				AND D.StateResult = 3)
				BEGIN
					set @ServiceOrderDetailDistributionLastId = 0
					SELECT @ServiceOrderDetailDistributionLastId = D.ServiceOrderDetailDistributionId from @Detalles D WHERE  
					D.RecoveryFeeType = 2 AND D.StateResult = 3 AND D.GrandTotalSalesPrice >= (D.SubTotalPatientSalesPrice + @NivelModeratorShareTop)
					if @ServiceOrderDetailDistributionLastId = 0
					BEGIN
						Select Convert(Bit, 0) As StatusResult, 'No se puede cruzar cuota moderadora ya que el valor paciente supera el valor entidad, puede distribuir por bono paciente' As MessageResult
						Return
					End

					Update @Detalles 
						Set SubTotalPatientSalesPrice = SubTotalPatientSalesPrice + @ValueProcessRecoveryFee,
						ThirdPartySalesPrice = GrandTotalSalesPrice - (SubTotalPatientSalesPrice + @ValueProcessRecoveryFee)
					Where ServiceOrderDetailDistributionId = @ServiceOrderDetailDistributionLastId
			
				END

				Update Billing.RevenueControl Set TopEventFeeModerator = TopEventFeeModerator + 
				ISNULL((SELECT SUM(SubTotalPatientSalesPrice) FROM @Detalles D where D.StateResult = 3 AND D.RecoveryFeeType = 2), 0)
				,TopEventCopay = TopEventCopay + ISNULL((SELECT SUM(SubTotalPatientSalesPrice) 
				FROM @Detalles D where D.StateResult = 3 AND D.RecoveryFeeType = 3), 0)
				where Id = @RevenueControlId

				Update SODD Set RecoveryFeeType = D.RecoveryFeeType,
					ApplyRecoveryFee = D.ApplyRecoveryFee,
					PatientPercentage = D.PatientPercentage,
					ThirdPartySalesPrice = D.ThirdPartySalesPrice,
					ThirdPartyPercentage = D.ThirdPartyPercentage,
					SubTotalPatientSalesPrice = D.SubTotalPatientSalesPrice
					from @Detalles D
					INNER JOIN Billing.ServiceOrderDetailDistribution SODD WITH(NOLOCK)
					ON SODD.Id = D.ServiceOrderDetailDistributionId
				Where D.StateResult = 3
			--HRR FIN

			End
			if @SettlementByItem = 0
			begin
				If @TypeRecoveryFeeProcess = 2 begin --Cuota Moderadora
					Set @ValueProcessRecoveryFee = @NivelModeratorShareTop
					If @NivelModeratorShareTopYear > 0 Begin
						If (@ValueProcessRecoveryFee + @PAGADOCMO) > @NivelModeratorShareTopYear
							Set @ValueProcessRecoveryFee = @NivelModeratorShareTopYear - @PAGADOCMO
						If @ValueProcessRecoveryFee < 0 Begin
							Select Convert(Bit, 0) As StatusResult, '(1) Se alcanzo el tope maximo para pagar en el año' As MessageResult
							Return
						End
					End
				End
				Else Begin --Copago
					Set @ValueProcessRecoveryFee = (@ValueProcess * @NivelCoPayContribPercentage) / 100
					Set @PercentageProcessRecoveryFee = @NivelCoPayContribPercentage
					If @NivelCoPayContribPercentage > 0 Begin
						If @ValueProcessRecoveryFee > @NivelCoPayContribTop
							Set @ValueProcessRecoveryFee = @NivelCoPayContribTop
						If (@ValueProcessRecoveryFee + @PAGADOCOP) > @NivelCoPayContribTopYear
							Set @ValueProcessRecoveryFee = @NivelCoPayContribTopYear - @PAGADOCOP
						If @ValueProcessRecoveryFee < 0 Begin
							Select Convert(Bit, 0) As StatusResult, 'Se alcanzo el tope maximo para pagar en el año, Tope: ' + Cast(@NivelCoPayContribTopYear As Varchar) + ' Valor Cuota: ' + Cast(@ValueProcessRecoveryFee As Varchar) As MessageResult
							Return
						End
					End
					Else Begin
						If @ValueProcessRecoveryFee > @NivelCoPayContribTop
							Set @ValueProcessRecoveryFee = @NivelCoPayContribTop
					End
				End
			end
		End
		Else If @PatientType = 2 Begin	--Subsidiado
			Set @ValueProcessRecoveryFee = (@ValueProcess * @NivelCoPaySubsiPercentage) / 100 --Vamos AQUI, El valor del % del copago es 0
			Set @PercentageProcessRecoveryFee = @NivelCoPaySubsiPercentage
			If @NivelCoPaySubsibTopYear > 0 Begin
				If @ValueProcessRecoveryFee > @NivelCoPaySubsibTop 
					Set @ValueProcessRecoveryFee = @NivelCoPaySubsibTop
				If (@ValueProcessRecoveryFee + @PAGADOCOP) > @NivelCoPaySubsibTopYear
					Set @ValueProcessRecoveryFee = @NivelCoPaySubsibTopYear - @PAGADOCOP
				If @ValueProcessRecoveryFee < 0 Begin
					Select Convert(Bit, 0) As StatusResult, '(2) Se alcanzo el tope maximo para pagar en el año' As MessageResult
					Return
				End
			End
			Else Begin
				If @ValueProcessRecoveryFee > @NivelCoPaySubsibTop
					SET @ValueProcessRecoveryFee = @NivelCoPaySubsibTop
			End
		End
		Else If @PatientType = 3 Begin	--Vinculado
			Set @ValueProcessRecoveryFee = (@ValueProcess * @NivelCoPayVincuPercentage) / 100
			Set @PercentageProcessRecoveryFee = @NivelCoPayVincuPercentage
			If @NivelCoPayVincuTopYear > 0 Begin
				If @ValueProcessRecoveryFee > @NivelCoPayVincuTop 
					Set @ValueProcessRecoveryFee = @NivelCoPayVincuTop
				If (@ValueProcessRecoveryFee + @PAGADOCOP) > @NivelCoPayVincuTopYear
					Set @ValueProcessRecoveryFee = @NivelCoPayVincuTopYear - @PAGADOCOP
				If @ValueProcessRecoveryFee < 0 Begin
					Select Convert(Bit, 0) As StatusResult, '(3) Se alcanzo el tope maximo para pagar en el año' As MessageResult
					Return
				End
			End
			Else Begin
				If @ValueProcessRecoveryFee > @NivelCoPayVincuTop
					Set @ValueProcessRecoveryFee = @NivelCoPayVincuTop
			End
		End
		if @SettlementByItem = 0
		begin
			If @ValueProcessRecoveryFee = 0 Begin
				Select Convert(Bit, 0) As StatusResult, '(4) Se alcanzo el tope maximo para pagar en el año' As MessageResult
				Return
			End

			Set @ValueProcessRecoveryFee = Round(@ValueProcessRecoveryFee, @RoundLevel)
			If @TypeRecoveryFeeProcess = 2 Begin --Cuota Moderadora
				Update Billing.RevenueControl Set TopEventFeeModerator = TopEventFeeModerator + @ValueProcessRecoveryFee 
				Where Id = @RevenueControlId
			End
			Else Begin --Copago
				Update Billing.RevenueControl Set TopEventCopay = TopEventCopay + @ValueProcessRecoveryFee 
				Where Id = @RevenueControlId
			End

			Declare @ServiceOrderDetailDistributionId Int, 
				@InvalidId Int, @ValueService Decimal(18, 2)		
		
			Declare Cursor_ServiceOrderDetailDistribution Cursor For
			Select Id From @ServiceorderDetailDistributionListId
			Open Cursor_ServiceOrderDetailDistribution

			Fetch Next From Cursor_ServiceOrderDetailDistribution Into @ServiceOrderDetailDistributionId
			While @@Fetch_Status = 0
			Begin
				Select @InvalidId = Coalesce(Id, 0) From @ListItemsApplyRecoveryFee Where Id = @ServiceOrderDetailDistributionId
				If @InvalidId Is Null Or @InvalidId = 0 Begin
					Fetch Next From Cursor_ServiceOrderDetailDistribution Into @ServiceOrderDetailDistributionId				
					Continue
				End
				--Print '@ValueProcessRecoveryFee -> ' + Cast(@ValueProcessRecoveryFee As Varchar)
				If @ValueProcessRecoveryFee = 0
					Break			
				--Print '@ServiceOrderDetailDistributionId -> ' + Cast(@ServiceOrderDetailDistributionId As Varchar)					
				Select @ValueService = Round(sodd.GrandTotalSalesPrice * @PercentageProcessRecoveryFee / 100,0)
				From Billing.ServiceOrderDetailDistribution sodd With(Nolock)
				Where sodd.Id = @ServiceOrderDetailDistributionId

				Print Cast(@ValueService As Varchar)
				If @ValueProcessRecoveryFee > @ValueService Begin
					Set @SubTotalPatientSalesPrice = @ValueService
					Set @ValueProcessRecoveryFee -= @ValueService
				End
				Else Begin
					Set @SubTotalPatientSalesPrice = @ValueProcessRecoveryFee
					Set @ValueProcessRecoveryFee = 0
				End

				Update Billing.ServiceOrderDetailDistribution Set RecoveryFeeType = @TypeRecoveryFeeProcess,
					ApplyRecoveryFee = 2,
					PatientPercentage = iif(@SubTotalPatientSalesPrice = 0, 0, @PercentageProcessRecoveryFee),
					ThirdPartySalesPrice = GrandTotalSalesPrice - @SubTotalPatientSalesPrice,
					ThirdPartyPercentage = 100 - iif(@SubTotalPatientSalesPrice = 0, 0, @PercentageProcessRecoveryFee),
					SubTotalPatientSalesPrice = @SubTotalPatientSalesPrice
				Where Id = @ServiceOrderDetailDistributionId
				
				Set @ServiceOrderDetailDistributionLastId = @ServiceOrderDetailDistributionId
				Fetch Next From Cursor_ServiceOrderDetailDistribution Into @ServiceOrderDetailDistributionId
			End
			Close Cursor_ServiceOrderDetailDistribution
			Deallocate Cursor_ServiceOrderDetailDistribution

			If @ServiceOrderDetailDistributionLastId > 0 And @ValueProcessRecoveryFee > 0 Begin
			
				IF EXISTS(SELECT 1 
				from Billing.ServiceOrderDetailDistribution 
				Where Id = @ServiceOrderDetailDistributionLastId AND GrandTotalSalesPrice < (SubTotalPatientSalesPrice + @ValueProcessRecoveryFee))
				BEGIN
					Select Convert(Bit, 0) As StatusResult, 'No se puede cruzar cuota moderadora ya que el valor paciente supera el valor entidad, puede distribuir por bono paciente' As MessageResult
					Return
				End

				Update Billing.ServiceOrderDetailDistribution 
					Set SubTotalPatientSalesPrice = SubTotalPatientSalesPrice + @ValueProcessRecoveryFee,
					ThirdPartySalesPrice = GrandTotalSalesPrice - (SubTotalPatientSalesPrice + @ValueProcessRecoveryFee)
				Where Id = @ServiceOrderDetailDistributionLastId
			End
		end

		
		Select Convert(Bit, 1) As StatusResult, '' As MessageResult
	End Try
	Begin Catch
		Select Convert(Bit, 0) As StatusResult, ERROR_MESSAGE() As MessageResult
	End Catch
	
End
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula y liquida la cuota de recuperación (cuota moderadora o copago) que debe pagar un paciente en un episodio de atención (ingreso/admisión). Recibe el código de admisión y los ítems del folio a los que aplica el cobro, consulta la configuración de facturación para determinar el tipo de redondeo, y combina los datos del ingreso (ADINGRESO), el perfil del paciente (INPACIENT), su nivel de afiliación con los porcentajes y topes de cuota moderadora, copago contributivo, subsidiado y vinculado (ADNIVELES), el centro de atención (ADCENATEN) y la unidad funcional (INUNIFUNC) para establecer el valor a cobrar. Aplica la lógica de liquidación ítem por ítem cuando el tipo de ingreso puede ser copago o cuota moderadora simultáneamente, respetando los topes anuales ya pagados por el paciente. Este procedimiento es el núcleo del proceso de facturación de la parte que asume el paciente dentro del sistema de salud (EPS, régimen subsidiado, vinculado).', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_LiquidatePatientShare';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_LiquidatePatientShare';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuota de recuperación; Cuota moderadora; Copago; Paciente contributivo; Paciente subsidiado; Paciente vinculado; Cotizante; Nivel del paciente (Niveles); Tope por evento; Tope anual; Admisión ambulatoria/hospitalaria; Servicio IPS; Manual tarifario; Distribución de orden de servicio; Folio de facturación; Redondeo de cuota', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidatePatientShare';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @TotalsItemsApplyRecoveryFee > 0 → Usa @TotalsItemsApplyRecoveryFee como valor base de cálculo else Usa @TotalFolio como valor base de cálculo; si @PatientType = 1 (Contributivo) y @LiquidationType = 1 → Aplica tipo COPAGO (@TypeRecoveryFeeProcess = 3); si @PatientType = 1 y @LiquidationType = 2 → Aplica tipo CUOTA MODERADORA (@TypeRecoveryFeeProcess = 2); si @PatientType = 1 y @LiquidationType = 4 → Liquidación por ítem (@SettlementByItem = 1): se evalúa por cada detalle si aplica copago o cuota moderadora según RecordType, AdmissionType y OutPatient/InPatientRecoveryFeeType del IPSService; si @PatientType = 2 (Subsidiado) → Calcula cuota = @ValueProcess * NIVPORSUB/100, topea contra TOPEVESUB y TOPANUSUB; si @PatientType = 3 (Vinculado) → Calcula cuota = @ValueProcess * NIVPORVIN/100, topea contra TOPEVEVIN y TOPANUVIN; si RecordType = 2 (medicamentos) y AdmissionType = 1 (ambulatorio) → Aplica CUOTA MODERADORA al ítem usando @NivelModeratorShareTop y @NivelModeratorSharePercentage; si RecordType = 2 y AdmissionType <> 1 (hospitalario) → Aplica COPAGO al ítem usando @NivelCoPayContribPercentage y tope @NivelCoPayContribTop; si RecordType <> 2 y OutPatient/InPatientRecoveryFeeType = 2 → Aplica CUOTA MODERADORA al servicio; si RecordType <> 2, OutPatient/InPatientRecoveryFeeType = 3 y @PatientTypeAffiliation <> 1 (no cotizante) → Aplica COPAGO al servicio; si RecordType <> 2, OutPatient/InPatientRecoveryFeeType = 3 y @PatientTypeAffiliation = 1 (cotizante) → Bloquea con StateResult=0 y mensaje ''El paciente es contributivo y cotizante, por lo cual no se puede cobrar COPAGO''; si OutPatient/InPatientRecoveryFeeType = 1 (NINGUNO) → Bloquea con StateResult=0 y mensaje sobre IPS parametrizado como NINGUNO; si Detalle no clasificado en reglas anteriores → Bloquea con StateResult=0 y mensaje sobre manual no parametrizado; si @TopEventFeeModerator + @SubTotalPatientSalesPrice > @NivelTopValue (en cuota) → Limita @SubTotalPatientSalesPrice al saldo disponible y marca StateResult=4 con mensaje de tope por nivel; si @SubTotalPatientSalesPrice > (@AmmountAnualTop - acumulado anual) → Limita al saldo anual disponible y marca StateResult=4 con mensaje de tope anual; si @TopEventFeeModerator >= @NivelTopValue → No liquida cuota: marca StateResult=4 ''Ya se superó el monto máximo de CUOTA MODERADORA para el evento''; si @TopEventCopay >= @NivelTopValue → No liquida copago: marca StateResult=4 ''Ya se superó el monto máximo de COPAGO para el evento''; si Existe algún detalle con StateResult IN (0,4) → Retorna StatusResult=0 con mensajes concatenados y aborta sin actualizar SODD/RevenueControl; si @ValueProcessRecoveryFee < 0 tras topes anuales → Retorna StatusResult=0 con mensaje ''Se alcanzo el tope maximo para pagar en el año''; si GrandTotalSalesPrice < (SubTotalPatientSalesPrice + @ValueProcessRecoveryFee) en última distribución → Retorna StatusResult=0 ''No se puede cruzar cuota moderadora ya que el valor paciente supera el valor entidad, puede distribuir por bono paciente''; si @ValueProcessRecoveryFee = 0 (modo no por ítem) → Retorna StatusResult=0 ''(4) Se alcanzo el tope maximo para pagar en el año''', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidatePatientShare';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.SettingsBilling; dbo.ADINGRESO; dbo.INPACIENT; dbo.ADNIVELES; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.INPACIENTTOPANU; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Contract.IPSService; Contract.RateManualDetail; Contract.RateManual; Billing.RevenueControl', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidatePatientShare';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_LiquidatePatientShare';
-- GO
