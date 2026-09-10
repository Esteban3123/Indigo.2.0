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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento encargado de registrar y validar el detalle de un documento de factoring a partir de un archivo XML importado o copiado manualmente. Recibe líneas con número de factura, fecha de negociación, valor negociado, concepto de egreso de flujo de caja y NIT del proveedor/acreedor, aplicando validaciones de datos vacíos y formatos antes de persistir la información. Consulta los conceptos de egreso de tesorería (CashFlowConcept) para verificar que el rubro indicado exista, y valida al proveedor junto con sus líneas de distribución habilitadas para factoring (SuppliersDistributionLines). Se usa en el módulo de pagos y tesorería para cargar masivamente las cuentas por pagar que serán negociadas mediante factoring.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_SetFactoringDocumentDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'PROCEDURE', @level1name = N'SP_SetFactoringDocumentDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida y prepara los detalles de un documento de factoring (cargados vía XML por copia/pega o importación) verificando datos obligatorios, formatos, existencia de concepto de egreso, proveedor con línea de factoring y parámetros, para devolver el detalle enriquecido listo para registrar.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SetFactoringDocumentDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML debe seguir la estructura /FactoringDocumentDetail con nodos InvoiceNumber, NegotiateDate, ValueNegotiate, CashFlowConceptExpenseCode, SupplierNit, Status y Message.; Debe existir al menos un parámetro en SettingPayments con IdCashFlowConcept no nulo (concepto de ingreso de CxP).; El proveedor debe tener al menos una línea de distribución marcada como Factoring=1.; El concepto de egreso referenciado debe existir en CashFlowConcept con TypeConcept=2.; La factura referenciada debe existir en Payments.AccountPayable con saldo igual al valor a negociar.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SetFactoringDocumentDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El detalle final solo se enriquece con datos contables (cuenta, proveedor, línea de distribución, conceptos) si todas las validaciones pasaron (ningún registro en Status=2).; Solo se consideran proveedores cuya línea de distribución esté marcada como Factoring=1.; Solo se consideran conceptos de flujo de caja con TypeConcept=2 (egreso) para el concepto de egreso del documento.; El valor a negociar debe coincidir exactamente con el saldo (Balance) de la cuenta por pagar de la factura.; Todo error capturado en CATCH se devuelve como un único resultset con Status=2 incluyendo mensaje y línea del error.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SetFactoringDocumentDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factoring; Factura (cuenta por pagar); Concepto de egreso de flujo de caja; Concepto de ingreso de flujo de caja; Proveedor / Acreedor; Línea de distribución contable; Cuenta contable principal; Saldo a negociar; Fecha de negociación; Parámetros de CxP', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SetFactoringDocumentDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @Detail: Se insertan filas leídas del XML de entrada con los campos InvoiceNumber, NegotiateDateString, ValueNegotiateString, CashFlowConceptExpenseCode, SupplierNit, Status y Message.; [UPDATE] @Detail: Si el número de factura está vacío o nulo, marca Status=2 y Message=''No se ha agregado un numero de factura''.; [UPDATE] @Detail: Si la fecha de negociación no se puede parsear como DATE en es-co (tras quitar ''a. m.''), marca Status=2 con mensaje de fecha vacía o formato inválido.; [UPDATE] @Detail: Si el valor a negociar está vacío/nulo o no es numérico, marca Status=2 con mensaje de valor vacío o formato inválido.; [UPDATE] @Detail: Si el código de concepto de egreso o el NIT del proveedor están vacíos/nulos, marca Status=2 con el mensaje correspondiente.; [UPDATE] @Detail: Si no existe concepto de egreso (TypeConcept=2) con el código dado, marca Status=2 con ''No existe concepto de egreso''.; [UPDATE] @Detail: Si no existe proveedor con línea de distribución marcada como Factoring=1 para el NIT, marca Status=2 con ''No existe el proveedor con una linea de distribución como factoring''.; [UPDATE] @Detail: Si no hay parámetro en SettingPayments con IdCashFlowConcept, marca Status=2 con ''No existe el concepto de egreso en los parametros de CxP''.; [UPDATE] @Detail: Cuando ningún detalle está en Status=2, convierte NegotiateDateString a DATE y ValueNegotiateString (reemplazando '','' por ''.'') a DECIMAL(18,2), y marca Status=1.; [UPDATE] @Detail: Si hay más de un registro con el mismo InvoiceNumber (GROUP BY HAVING COUNT(1)>1), marca Status=2 con ''Existen datos duplicados''.; [UPDATE] @Detail: Si existe AccountPayable con BillNumber igual a la factura pero su Balance es distinto al valor a negociar, marca Status=2 con ''El saldo a negociar es diferente al valor de la factura''.; [UPDATE] @Detail: Si tras todas las validaciones ningún detalle está en Status=2, enriquece el detalle con IdAccountPayable, fechas, saldos, IdCashFlowConceptExpense, IdSupplier, línea de distribución, IdCashFlowConceptIncome, cuenta principal, plazo, fecha de expiración y comentarios desde AccountPayable y MainAccounts.; [RETURN_RESULT] (resultset): Retorna todas las filas de @Detail con su estado y datos enriquecidos; en caso de excepción retorna una fila con Status=2 y el mensaje y línea del error.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SetFactoringDocumentDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Cualquier validación previa marcó Status=2 en @Detail → Se omiten las conversiones de tipo (fecha y valor numérico) y el enriquecimiento final desde AccountPayable/MainAccounts else Se realizan las conversiones de fecha y valor, y luego se enriquecen los detalles con datos de la cuenta por pagar y cuenta contable principal; si Existe AccountPayable.BillNumber = InvoiceNumber con Balance distinto al valor a negociar → Se marca Status=2 con mensaje de saldo diferente; si COUNT(1) por InvoiceNumber > 1 en @Detail → Se marca Status=2 indicando datos duplicados', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SetFactoringDocumentDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CashFlowConcept; Common.Supplier; Common.SuppliersDistributionLines; Common.DistributionLines; Payments.SettingPayments; Payments.AccountPayable; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SetFactoringDocumentDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'PROCEDURE', @level1name=N'SP_SetFactoringDocumentDetail';
-- GO
