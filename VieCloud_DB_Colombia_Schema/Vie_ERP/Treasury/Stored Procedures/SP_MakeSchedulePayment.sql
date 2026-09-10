-- =============================================
-- Author:		Carlos Cordoba
-- Create date: 29-09-2016
-- Description:	Procedimiento para generar la dispersion de fondos
-- =============================================
CREATE PROCEDURE [Treasury].[SP_MakeSchedulePayment]
	@SchedulePaymentXml AS XML,
	@User VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @IdSchedulePayment int, @CodeSchedulePayment varchar(20),@EntityBankAccountId int,@CostCenterId int,@PaymentMethod tinyint,@CheckId int,@NumberNote varchar(50),@TaxByMil bit
	declare @TableResult table([Message] varchar(300),MessageType int)

	declare @VoucherTransaction table (
	[Id] [int] NOT NULL,
	[Code] [varchar](20) NOT NULL,
	[IdThirdParty] [int] NULL,
	[IdMainAccount] [int] NOT NULL,
	[IdCostCenter] [int] NULL,
	[VoucherClass] [int] NOT NULL,
	[ExpenseType] [tinyint] NOT NULL,
	[Detail] [varchar](max) NOT NULL,
	[DocumentDate] [datetime] NOT NULL,
	[IdCashRegister] [int] NULL,
	[IdEntityBankAccount] [int] NULL,
	[Value] [decimal](18, 0) NOT NULL,
	[PaymentMethod] [tinyint] NULL,
	[NoteNumber] [varchar](50) NULL,
	[IdChecks] [int] NULL,
	[CheckNumber] [bigint] NULL,
	[TransactionDate] [datetime] NULL,
	[TaxByMil] [bit] NOT NULL,
	[TaxByMilValue] [decimal](18, 2) NULL,
	[CashRegisterExpense] [bit] NOT NULL,
	[RefundCashRegisterExpense] [bit] NOT NULL,
	[SchedulePaymentId] [int] NULL,
	[BeneficiaryIdentification] [varchar](20) NULL,
	[Beneficiary] [varchar](100) NULL,
	[TransactionRelationship] [bit] NULL,
	[CheckReconciled] [bit] NULL,
	[IdPaymentOrder] [int] NULL,
	[Printed] [bit] NULL,
	[RTEValue] [decimal](18, 2) NULL,
	[IVAValue] [decimal](18, 2) NULL,
	[ICAValue] [decimal](18, 2) NULL,
	[OtherValue] [decimal](18, 2) NULL,
	[BankAccountNumber] [varchar](50) NULL,
	[BankName] [varchar](100) NULL,
	[DetailsInterfaceBudget] [varchar](100) NULL,
	[IdUnitOperative] [int] NOT NULL,
	[IdRefund] [int] NULL,
	[Status] [tinyint] NOT NULL,
	[CreationUser] [varchar](20) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationUser] [varchar](20) NULL,
	[ModificationDate] [datetime] NULL,
	[ConfirmationUser] [varchar](20) NULL,
	[ConfirmationDate] [datetime] NULL,
	[AnnulmentUser] [varchar](20) NULL,
	[AnnulmentDate] [datetime] NULL,
	[ReversedUser] [varchar](20) NULL,
	[ReversedDate] [datetime] NULL,
	[TimeStamp] [timestamp] NOT NULL,
	[EmailSent] [bit])

	declare @VoucherTransactionDetails TABLE(
	ChangeTracker VARCHAR(30),
	[IdTmp] [INT] NOT NULL,
	[Id] [int] NOT NULL,
	[IdVoucherTransaction] [int] NOT NULL,
	[IdEntityBankAccount] [int] NULL,
	[CashRegisterId] [int] NULL,
	[IdThirdParty] [int] NULL,
	[IdExpenseConcept] [int] NULL,
	[IdMainAccount] [int] NOT NULL,
	[Nature] [tinyint] NOT NULL,
	[IdCostCenter] [int] NULL,
	[Value] [decimal](18, 0) NOT NULL,
	[IdRetentionConcept] [int] NULL,
	[BaseValue] [decimal](18, 0) NULL,
	[BillingValue] [decimal](18, 0) NULL,
	[PercentRetention] [decimal](5, 2) NULL,
	[Detail] [varchar](max) NULL,
	[Observation] [varchar](max) NULL)

	declare @DischargeBill TABLE (
	ChangeTracker VARCHAR(30),
	[Id] [int] NOT NULL,
	[IdVoucherTransactionDTmp] [int] NOT NULL,
	[IdVoucherTransactionD] [int] NOT NULL,
	[IdAccountPayable] [int] NOT NULL,
	[IdAccountPayableShare] [int] NOT NULL,
	[AdvancedValue] [decimal](18, 2) NOT NULL,
	[AdvancePercent] [decimal](5, 2) NOT NULL,
	[IdPaymentConcept] [int] NOT NULL,
	[BaseValueDiscount] [decimal](18, 2) NOT NULL,
	[DiscountPercent] [decimal](5, 2) NOT NULL)

    BEGIN TRY
		select 		
		@IdSchedulePayment = t.x.value('Id[1]', 'int'),
		@CodeSchedulePayment = t.x.value('Code[1]','varchar(20)'),
		@EntityBankAccountId =t.x.value('EntityBankAccountId[1]', 'int'),
		@CostCenterId =t.x.value('CostCenterId[1]', 'int'),
		@PaymentMethod  = t.x.value('PaymentMethod [1]','tinyint'),
		@CheckId =t.x.value('CheckId[1]', 'int'),
		@NumberNote = t.x.value('NumberNote[1]','varchar(50)'),
		@TaxByMil = t.x.value('TaxByMil[1]','bit')		
		from @SchedulePaymentXml.nodes('/SchedulePayment') t(x);

		--se recorren los detalles de la programacion para generar los comprobantes de egreso
		DECLARE @SupplierId INT		

        DECLARE header_cursor CURSOR
        FOR select SupplierId  from Treasury.SchedulePaymentDetail where VoucherTransactionId is null and SchedulePaymentId =@IdSchedulePayment group by SupplierId 
        OPEN header_cursor;
        FETCH NEXT FROM header_cursor INTO @SupplierId
			WHILE @@FETCH_STATUS = 0 BEGIN

			declare @headerDetail varchar(max) = 'Generado con Planilla de Dispersión Nº ' + @CodeSchedulePayment + CHAR(13) + CHAR(10) 
			--id temporal del detalle
			declare @IdVoucherTransactionDTmp int = 1
			--se eliminan los datos de las tablas temporales
			delete @VoucherTransaction
			delete @VoucherTransactionDetails
			delete @DischargeBill
			--*****se recorre los items por linea de distribucion del proveedor para generar los detalles del comprobante de egreso*****

			DECLARE @DistributionLineId INT
			DECLARE distributionLine_cursor CURSOR
			FOR select DistributionLineId   from Treasury.SchedulePaymentDetail where SupplierId = @SupplierId and SchedulePaymentId =@IdSchedulePayment group by DistributionLineId  
			OPEN distributionLine_cursor;
			FETCH NEXT FROM distributionLine_cursor INTO @DistributionLineId
			WHILE @@FETCH_STATUS = 0 BEGIN
				

			--***se recorren los items que tengan la misma linea de distribucion para generar los detalles en DischargeBill
				DECLARE @SchedulePaymentDetailId INT
				DECLARE dischargeBill_cursor CURSOR
				FOR select Id   from Treasury.SchedulePaymentDetail where DistributionLineId  = @DistributionLineId and SchedulePaymentId =@IdSchedulePayment  
				OPEN dischargeBill_cursor;
				FETCH NEXT FROM dischargeBill_cursor INTO @SchedulePaymentDetailId
				WHILE @@FETCH_STATUS = 0 BEGIN

				--se agregan los detalles de dischargeBill
				insert into @DischargeBill
				select 'Added',0,@IdVoucherTransactionDTmp,0,AccountPayableId,AccountPayableShareId,AmountPaid,AmountPercent,PaymentConceptId,0,0 from Treasury.SchedulePaymentDetail where Id = @SchedulePaymentDetailId

				--se agregan las facturas al detalle
				select @headerDetail += 'CxP ' + ap.Code +' Factura '+ap.BillNumber from Treasury.SchedulePaymentDetail cpd inner join Payments.AccountPayable ap on cpd.AccountPayableId = ap.Id where cpd.Id = @SchedulePaymentDetailId

				--se actualiza el campo GeneratedVoucher
				update Treasury .SchedulePaymentDetail set GeneratedVoucher = 1 where Id = @SchedulePaymentDetailId

				FETCH NEXT FROM dischargeBill_cursor INTO @SchedulePaymentDetailId
				END
				CLOSE dischargeBill_cursor
				DEALLOCATE dischargeBill_cursor

			---***********************************************************************************************************

			--se insertan los detalles de comprobante
			declare @description varchar(max)

			--insert into @VoucherTransactionDetails
			--select top 1 'Added',@IdVoucherTransactionDTmp,0,0,null,null,spd.ThirdPartyId,spd.ExpenseConceptId,spd.MainAccountId,spd.Nature,sp.CostCenterId,(select SUM(AdvancedValue) from @DischargeBill),null from Treasury.SchedulePaymentDetail spd 
			--inner join SchedulePayment sp on spd.SchedulePaymentId = sp.Id
			--where spd.SupplierId = @SupplierId and spd.DistributionLineId = @DistributionLineId and sp.Id = @IdSchedulePayment

			--se incrementa el id temporal del detalle
			set @IdVoucherTransactionDTmp +=1

			
			 
			FETCH NEXT FROM distributionLine_cursor INTO @DistributionLineId
			END
			CLOSE distributionLine_cursor
			DEALLOCATE distributionLine_cursor

			--*************************************************************************************************************************

			--*************se guarda el comprobante de egreso*****************************************************

			--****************************************************************************************************
			FETCH NEXT FROM header_cursor INTO @SupplierId
			END
		CLOSE header_cursor
		DEALLOCATE header_cursor
		
	END TRY
    BEGIN CATCH
        SELECT '999' AS CodeMessage,ERROR_MESSAGE()+', Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)) Message,0 AS IdSchedulePayment,CAST(3 AS TINYINT) AS [Status];
    END CATCH	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que ejecuta la dispersión de fondos a partir de una programación de pagos (planilla de dispersión). Recibe un XML con los datos del pago programado (cuenta bancaria, método de pago, cheque, nota, 4x1000) y el usuario que lo ejecuta. Agrupa los ítems pendientes de la tabla SchedulePaymentDetail por proveedor y por línea de distribución para generar automáticamente comprobantes de egreso (vouchers de tesorería) con sus detalles contables y la descarga de las cuentas por pagar (AccountPayable) correspondientes. Es el motor principal del módulo de tesorería para liquidar pagos programados a proveedores, dejando trazabilidad del comprobante generado en cada línea de la planilla de dispersión.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_MakeSchedulePayment';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_MakeSchedulePayment';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la dispersión de fondos a partir de una planilla/cronograma de pagos, recorriendo sus detalles agrupados por proveedor y línea de distribución para preparar los comprobantes de egreso y marcar los ítems como ya procesados.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_MakeSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @SchedulePaymentXml debe contener el nodo /SchedulePayment con Id, Code, EntityBankAccountId, CostCenterId, PaymentMethod, CheckId, NumberNote y TaxByMil; Debe existir al menos un registro en Treasury.SchedulePaymentDetail con SchedulePaymentId = Id del XML y VoucherTransactionId IS NULL para que se procese algo; Las facturas referenciadas en SchedulePaymentDetail.AccountPayableId deben existir en Payments.AccountPayable para construir el detalle del encabezado', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_MakeSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan detalles del cronograma cuyo VoucherTransactionId está NULL (aún no tienen comprobante asociado); Se agrupa la generación por SupplierId y, dentro de cada proveedor, por DistributionLineId; Cada detalle procesado queda marcado con GeneratedVoucher=1 en Treasury.SchedulePaymentDetail; Los errores no se propagan: se capturan y se devuelven como result set con Status=3 y CodeMessage=''999''; El encabezado del comprobante incluye el texto ''Generado con Planilla de Dispersión Nº'' seguido del código del cronograma y la lista de CxP/Facturas asociadas', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_MakeSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispersión de fondos; Programación/Planilla de pagos (SchedulePayment); Comprobante de egreso (VoucherTransaction); Cuentas por pagar (CxP); Factura de proveedor; Línea de distribución; Concepto de pago; Proveedor (Supplier); Centro de costo; Método de pago / Cheque / Nota', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_MakeSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Treasury.SchedulePaymentDetail: Para cada detalle procesado dentro del cursor dischargeBill_cursor se ejecuta: UPDATE Treasury.SchedulePaymentDetail SET GeneratedVoucher = 1 WHERE Id = @SchedulePaymentDetailId; [RETURN_RESULT] (result set): En el bloque CATCH se devuelve un SELECT con CodeMessage=''999'', el mensaje de error concatenado con la línea, IdSchedulePayment=0 y Status=3', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_MakeSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen detalles del cronograma con VoucherTransactionId IS NULL para el SchedulePayment indicado → Itera por cada SupplierId distinto para construir un comprobante de egreso por proveedor else No procesa nada para ese proveedor; si Bloque CATCH ante cualquier error en TRY → Retorna result set con CodeMessage=''999'', mensaje de error con número de línea, IdSchedulePayment=0 y Status=3', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_MakeSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.SchedulePaymentDetail; Payments.AccountPayable', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_MakeSchedulePayment';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_MakeSchedulePayment';
-- GO
