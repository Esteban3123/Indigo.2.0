
CREATE Function [Payments].[GetDetailValueAccountPayable]
(
	@EntityId Int,
	@EntityName varchar(250),
	@InvoiceNumber varchar(100),
	@SupplierId int
)
Returns @DetailValueAccountPayable Table
(
	StatusResult Bit,
	MessageResult Varchar(Max),
	
	IdAccountPayable Int,
	BaseValue Decimal(18, 2),
	ValueTax Decimal(18, 2),
	RateValue Decimal(5,2)
	
) 
As
Begin	
	Declare @IdEntity Int = @EntityId,
			@NameEntity varchar(250) = @EntityName,
			@BillNumber varchar(100) = @InvoiceNumber,
			@TableName varchar,
			@query VARCHAR(MAX)

	Set @TableName =
		CASE
			WHEN @NameEntity ='EntranceVoucher'
				THEN  'Inventory.EntranceVoucher'

			WHEN @NameEntity ='LoadMassive'
				THEN 'Payments.LoadMassiveAccountPayable'
			
			WHEN @NameEntity ='InitialBalance'
				THEN 'Payments.InitialBalanceAccountPayable'

			WHEN @NameEntity ='FixedAssetEntry'
				THEN 'FixedAsset.FixedAssetEntry'
			END;
			
	IF @NameEntity is not null and @NameEntity <>''
		begin
			If @NameEntity = 'EntranceVoucher'
				begin
				insert into @DetailValueAccountPayable
				SELECT 1,null,e.AccountPayableId,(e.Value-(isnull(evd.DevValue,0))),e.ValueTax,((e.ValueTax*100)/e.Value)
				FROM Inventory.EntranceVoucher e WITH(NOLOCK)
				OUTER APPLY(SELECT sum(evd.Value) DevValue 
							from Inventory.EntranceVoucherDevolution evd WITH(NOLOCK)
							where e.Id=evd.EntranceVoucherId) evd
				WHERE e.Id =@IdEntity and e.InvoiceNumber =@BillNumber and e.SupplierId= @SupplierId
			end

			else if @NameEntity ='LoadMassive'
				begin 
				INSERT INTO @DetailValueAccountPayable
				SELECT 1,NULL,lmap.AccountPayableId,lmap.InvoiceValue,0,0
				FROM Payments.LoadMassiveAccountPayable lmap WITH(NOLOCK)
				WHERE lmap.LoadMassiveId=@IdEntity and lmap.BillNumber=@BillNumber and lmap.SupplierId=@SupplierId
			end	

			else if @NameEntity='InitialBalance'
				BEGIN
				INSERT into @DetailValueAccountPayable
				SELECT 1,null,inbap.AccountPayableId,inbap.Value,0,0
				FROM Payments.InitialBalanceAccountPayable inbap WITH(NOLOCK)
				WHERE inbap.InitialBalanceId= @IdEntity AND inbap.BillNumber=@BillNumber and inbap.SupplierId =@SupplierId
			end

			else if @NameEntity='FixedAssetEntry'
				BEGIN
				INSERT into @DetailValueAccountPayable
				SELECT 1,null,fe.AccountPayableId,fe.Value,fe.ValueTax,((fe.ValueTax*100)/fe.Value)
				FROM FixedAsset.FixedAssetEntry fe WITH(NOLOCK)
				WHERE fe.Id= @IdEntity AND fe.InvoiceNumber=@BillNumber and fe.SupplierId = @SupplierId
			end
	END
	ELSE
		BEGIN
		INSERT into @DetailValueAccountPayable
		VALUES(0,'No tiene el nombre del modulo',NULL,0,0,0)
	END
	Return
End