-- =====================================================
-- Author:		Johan Sebastian Cuellar Esquivel
-- Create date: 2022-03-25
-- Description:	Procedimiento que se encarga de copiar y pegar o la importacion de archivo para documento factoring
-- =====================================================
CREATE PROCEDURE [Payments].[SP_SetFactoringDocumentDetail]
    @FactoringDocumentDetail AS XML	
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @InvoiceNumberVar VARCHAR(100),			
			@ValueNegotiateVar VARCHAR(60),	
			@CashFlowConceptExpenseCodeVar  VARCHAR(20),
			@SupplierNitVar VARCHAR(25)
			
	
	--Tabla temporal de los detalles para el return
	DECLARE @Detail TABLE
	(					
		IdAccountPayable Int,------------
		PreviusDate Date,
		NewDate Date, -----------
		PreviusBalance DECIMAL(18,2),
		Newbalance DECIMAL(18,2),----
		IdCashFlowConceptExpense int,----
		IdSupplier int,----
		SupplierName VARCHAR(100),

		IdDistributionLine int,
		DistributionLineString  VARCHAR(120),
		IdCashFlowConceptIncome int, 

		MainAccountNumber VARCHAR(50),
		Term int,
		ExpirationDate date,
		Coments varchar(max),

		Status tinyint,
		Message varchar(200) NULL,
		-----------------------------
		
		InvoiceNumber VARCHAR(100),	----
		NegotiateDate Date,			----	
		ValueNegotiate DECIMAL(18,2),	----	
		CashFlowConceptExpenseCode VARCHAR(20),	----	
		SupplierNit  VARCHAR(25),----
		-----------------------------
		NegotiateDateString VARCHAR(200),
		ValueNegotiateString VARCHAR(60)
		
	)

	BEGIN TRY
	
		--Se obtiene los detalles que vienen en el xml
			INSERT INTO @Detail
			(
				InvoiceNumber, NegotiateDateString, ValueNegotiateString, CashFlowConceptExpenseCode, SupplierNit, Status, Message
			)
				SELECT  t.x.value('InvoiceNumber[1]','VARCHAR(100)'),
						t.x.value('NegotiateDate[1]','VARCHAR(200)'),
						t.x.value('ValueNegotiate[1]','VARCHAR(60)'),
						t.x.value('CashFlowConceptExpenseCode[1]','VARCHAR(20)'),
						t.x.value('SupplierNit[1]','VARCHAR(25)'),					
						t.x.value('Status[1]','TINYINT'),
						t.x.value('Message[1]','VARCHAR(200)')
				FROM @FactoringDocumentDetail.nodes('/FactoringDocumentDetail') t(x)

			------------------------------------------------------------------------------------------------------------------------------------------------------------------
			
			------------------------------------------------------------------------------------------------------------------------------------------------------------------
			--Obtengo para el respectivo uso de actualización
			SELECT   @InvoiceNumberVar = d.InvoiceNumber,					
					@CashFlowConceptExpenseCodeVar = d.CashFlowConceptExpenseCode,
					@SupplierNitVar =  d.SupplierNit,
					@ValueNegotiateVar = D.ValueNegotiateString
			FROM @Detail d 
				

			/********************************************************** VALIDACIONES DATOS VACIOS *************************************************************************/
	
			--valido que se ingrese un nuevo de factura
			IF EXISTS (SELECT D.InvoiceNumber FROM @Detail d WHERE d.InvoiceNumber = @InvoiceNumberVar AND @InvoiceNumberVar IS NULL OR @InvoiceNumberVar = '' )
			BEGIN
				UPDATE d
					SET d.Status = 2,
						d.Message = 'No se ha agregado un numero de factura '
				FROM @Detail D						
				WHERE d.InvoiceNumber  = @InvoiceNumberVar 		
			END

			--valido que se ingreso una fecha 			
			IF EXISTS (SELECT D.NegotiateDateString FROM @Detail d WHERE TRY_PARSE(REPLACE(d.NegotiateDateString,'a. m.','') AS DATE USING 'es-co') IS NULL ) 
			BEGIN
				UPDATE d
					SET d.Status = 2,
						d.Message = CONCAT('La fecha de negociación esta vacia, en la factura : ',D.InvoiceNumber)
				FROM @Detail D						
				WHERE d.InvoiceNumber  = @InvoiceNumberVar 	
			END

			--valido que se ingreso un valor
			IF EXISTS (SELECT D.ValueNegotiateString FROM @Detail d WHERE d.InvoiceNumber = @InvoiceNumberVar AND @ValueNegotiateVar  IS NULL OR @ValueNegotiateVar = '' )
			BEGIN
				UPDATE d
					SET d.Status = 2,
						d.Message = CONCAT('La valor de negociación esta vacio, en la factura : ',D.InvoiceNumber)
				FROM @Detail D						
				WHERE d.InvoiceNumber  = @InvoiceNumberVar 	
			END
			
			
			--valido que se ingreso concepto de egreso
			IF EXISTS (SELECT D.CashFlowConceptExpenseCode FROM @Detail d WHERE d.InvoiceNumber = @InvoiceNumberVar AND @CashFlowConceptExpenseCodeVar IS NULL OR @CashFlowConceptExpenseCodeVar = '' )
			BEGIN
				UPDATE d
					SET d.Status = 2,
						d.Message = CONCAT('El concepto de egreso esta vacio, en la factura : ',D.InvoiceNumber)
				FROM @Detail D						
				WHERE d.InvoiceNumber  = @InvoiceNumberVar 	
			END

			--valido que se ingreso un acreedor
			IF EXISTS (SELECT D.SupplierNit FROM @Detail d WHERE d.InvoiceNumber = @InvoiceNumberVar AND @SupplierNitVar IS NULL OR @SupplierNitVar = '' )
			BEGIN
				UPDATE d
					SET d.Status = 2,
						d.Message = CONCAT('El acreedor esta vacio, en la factura : ',D.InvoiceNumber)
				FROM @Detail D						
				WHERE d.InvoiceNumber  = @InvoiceNumberVar 	
			END


			/********************************************************** VALIDACIONES FORMATOS *************************************************************************/
			
			-- Validar que el valor ingresado sea una fecha
			UPDATE d
				SET d.Status = 2,
					d.Message = CONCAT('La Fecha de negociacion no tiene un formato válido, en la factura : ',D.InvoiceNumber)
			FROM @Detail D
			WHERE  TRY_PARSE(REPLACE(d.NegotiateDateString,'a. m.','') AS DATE USING 'es-co') IS NULL
				
			-- Validar que el valor ingresado sea un valor de negociacion
			UPDATE d
				SET d.Status = 2,
					d.Message = CONCAT('El valor negociacion no tiene un formato válido, en la factura : ',D.InvoiceNumber)
			FROM @Detail D
			WHERE ISNUMERIC(D.ValueNegotiateString) = 0
										
			----------------------------------------------------------------------------------------------------------------------------------------------------
			/********************************************************** VALIDACIONES *************************************************************************/
			Declare @IdcashFlowConcept As Int,@IdSupplier Int,@SupplierName VARCHAR(100), @IdDistributionLine Int, @DistributionLineName VARCHAR(120),
			@IdcashFlowConceptIncome As Int
			
			---consultar el concepto de egreso
			SELECT top 1 
					@IdcashFlowConcept = cfc.id
			FROM Treasury.CashFlowConcept cfc 
			WHERE cfc.Code = @CashFlowConceptExpenseCodeVar and cfc.TypeConcept = 2

			--Consultar proveedor con linea de distribucion como factoring
			SELECT top 1 
				@IdSupplier = s.id,
				@SupplierName =s.Name,
				@IdDistributionLine = dl.Id,
				@DistributionLineName = CONCAT(dl.Code,' - ',dl.Name)
			FROM Common.Supplier s 
			JOIN Common.SuppliersDistributionLines sdl WITH (NOLOCK) ON sdl.IdSupplier = s.id
			JOIN Common.DistributionLines dl  WITH (NOLOCK) ON sdl.IdDistributionLine = dl.Id
			WHERE s.Code = @SupplierNitVar and sdl.Factoring = 1 

			--Consultar parametro
			SELECT TOP 1 
				@IdcashFlowConceptIncome = sp.IdCashFlowConcept
			FROM Payments.SettingPayments sp
			where sp.IdCashFlowConcept IS NOT NULL


			--valido que exista un concepto de egreso con ese codigo
			IF @IdcashFlowConcept = NULL
			BEGIN
				UPDATE d
					SET d.Status = 2,
						d.Message = CONCAT('No existe concepto de egreso, en la factura : ',D.InvoiceNumber)
				FROM @Detail D						
				WHERE d.InvoiceNumber  = @InvoiceNumberVar 	
			END

			--valido que exista el proveedor
			IF @IdSupplier = NULL
			BEGIN
				UPDATE d
					SET d.Status = 2,
						d.Message = CONCAT('No existe el proveedor con una linea de distribución como factoring, en la factura : ',D.InvoiceNumber)
				FROM @Detail D						
				WHERE d.InvoiceNumber  = @InvoiceNumberVar 	
			END
			
			--valido que exista el concepto de ingreso en parametros
			IF @IdcashFlowConceptIncome = NULL
			BEGIN
				UPDATE d
					SET d.Status = 2,
						d.Message = 'No existe el concepto de egreso en los parametros de CxP'
				FROM @Detail D
			END

			----------------------------------------------------------------------------------------------------------------------------------------------------
			/********************************************************** CONVERSIONES *************************************************************************/
			IF NOT EXISTS (	SELECT d.Status FROM @Detail d WHERE d.Status = 2 )
			BEGIN			
				UPDATE d						
						SET d.Status = 1,
							d.NegotiateDate = TRY_PARSE(REPLACE(d.NegotiateDateString,'a. m.','') AS DATE USING 'es-co')
					FROM @Detail d					

					-- Actualizamos el valor a negociar	
					UPDATE d
							SET d.Status = 1,
							d.ValueNegotiate = CAST(REPLACE(d.ValueNegotiateString,',','.') AS DECIMAL(18, 2))
					FROM @Detail d						
			END

			---Valido los datos duplicados
			IF EXISTS
			(
				SELECT 1 
				FROM @Detail d
				GROUP BY d.InvoiceNumber
				HAVING COUNT(1) > 1
			)
			BEGIN
				UPDATE d
				SET d.Status = 2,									
					d.Message = CONCAT('Existen datos duplicados : ',D.InvoiceNumber)
				FROM @Detail D				
			END	
			
			
			----------------------------------------------------------------------------------------------------------------------------------------------------
			IF EXISTS (select *  FROM @Detail d	JOIN Payments.AccountPayable ac ON ac.BillNumber = D.InvoiceNumber AND AC.BALANCE <> @ValueNegotiateVar)
			BEGIN	
				UPDATE d
						SET d.Status = 2,
							d.Message = 'El saldo a negociar es diferente al valor de la factura'
					FROM @Detail D					
				WHERE d.InvoiceNumber  = @InvoiceNumberVar 	
			END
						

			/********************************************************** UPDATE *************************************************************************/
			IF NOT EXISTS (	SELECT d.Status FROM @Detail d WHERE d.Status = 2 )
			BEGIN			
		
				UPDATE d
						SET d.IdAccountPayable = ac.Id,
						d.PreviusDate = ac.BillDate,
						d.NewDate = d.NegotiateDate,
						d.PreviusBalance = ac.Balance,
						d.Newbalance = d.ValueNegotiate,
						d.IdCashFlowConceptExpense = @IdcashFlowConcept,
						d.IdSupplier = @IdSupplier,
						d.SupplierName = @SupplierName,						
						d.IdDistributionLine = @IdDistributionLine,
						d.DistributionLineString = @DistributionLineName,
						d.IdCashFlowConceptIncome =  @IdcashFlowConceptIncome,
						d.MainAccountNumber = ma.Number,
						d.Term = ac.Term,
						d.ExpirationDate = ac.ExpirationDate,
						d.Coments = ac.Coments
				FROM @Detail d					
				JOIN Payments.AccountPayable ac ON ac.BillNumber = D.InvoiceNumber
				JOIN  GeneralLedger.MainAccounts ma ON ac.IdAccount = ma.id
				WHERE  d.InvoiceNumber  = @InvoiceNumberVar 				
			END

			---------------------------------------------------------------------------------------------------------------------------------------------------------------- 			
			SELECT	*
			FROM @Detail d

	END TRY
	BEGIN CATCH
		  SELECT '0' ,0,0,2 As Status,ERROR_MESSAGE()+', Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)) Message
	END CATCH
END