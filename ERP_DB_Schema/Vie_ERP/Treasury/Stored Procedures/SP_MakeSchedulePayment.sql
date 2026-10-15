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