
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que obtiene el detalle de valor de una cuenta por pagar a proveedor, consultando la fuente de origen según el módulo indicado: comprobantes de entrada de inventario (con descuento de devoluciones), cargas masivas de proveedores, saldos iniciales de cuentas por pagar o entradas de activos fijos. Recibe como parámetros el identificador del registro, el nombre del módulo origen, el número de factura y el proveedor, y retorna el valor base, el impuesto (IVA) y la tasa impositiva calculada asociados a la cuenta por pagar. Se utiliza para consultar el desglose económico de una obligación con un proveedor, independientemente de cuál fue el proceso contable que la generó (compra de inventario, activo fijo, carga masiva o apertura de saldo).', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'FUNCTION', @level1name = N'GetDetailValueAccountPayable';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'FUNCTION', @level1name = N'GetDetailValueAccountPayable';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el detalle de valores (base, impuesto y tasa) de una cuenta por pagar a partir del módulo origen (entrada de inventario, carga masiva, saldo inicial o activo fijo) para un proveedor y factura específicos.', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetDetailValueAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El nombre del módulo origen no debe ser nulo ni vacío; El módulo debe ser uno de: EntranceVoucher, LoadMassive, InitialBalance o FixedAssetEntry; Para EntranceVoucher y FixedAssetEntry el valor base no puede ser cero (se usa como divisor para calcular la tasa de impuesto)', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetDetailValueAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La búsqueda siempre filtra por la combinación Id de entidad, número de factura y proveedor; Para módulos sin impuesto registrado (LoadMassive, InitialBalance) el ValueTax y RateValue siempre se devuelven en cero; En EntranceVoucher la base siempre se ajusta restando las devoluciones acumuladas del comprobante; Los resultados exitosos siempre retornan StatusResult=1 con MessageResult nulo; Solo se retorna información de un único módulo por invocación según el nombre recibido', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetDetailValueAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por pagar; Factura; Proveedor; Comprobante de entrada de inventario; Devolución a proveedor; Carga masiva de cuentas por pagar; Saldo inicial de cuentas por pagar; Entrada de activo fijo; Impuesto (IVA) y tasa de impuesto', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetDetailValueAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @DetailValueAccountPayable: Cuando el módulo es ''EntranceVoucher'' se inserta el AccountPayableId del comprobante de entrada con BaseValue = Value menos la suma de devoluciones asociadas, ValueTax del comprobante y RateValue = (ValueTax*100)/Value; [INSERT] @DetailValueAccountPayable: Cuando el módulo es ''LoadMassive'' se inserta el AccountPayableId con BaseValue = InvoiceValue, ValueTax = 0 y RateValue = 0; [INSERT] @DetailValueAccountPayable: Cuando el módulo es ''InitialBalance'' se inserta el AccountPayableId con BaseValue = Value, ValueTax = 0 y RateValue = 0; [INSERT] @DetailValueAccountPayable: Cuando el módulo es ''FixedAssetEntry'' se inserta el AccountPayableId con BaseValue = Value, ValueTax y RateValue = (ValueTax*100)/Value; [INSERT] @DetailValueAccountPayable: Cuando el nombre del módulo es nulo o vacío se inserta StatusResult=0 y MessageResult=''No tiene el nombre del modulo''', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetDetailValueAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @NameEntity es nulo o vacío → Devuelve fila de error con mensaje ''No tiene el nombre del modulo'' y StatusResult=0 else Evalúa el módulo origen para seleccionar la tabla fuente; si @NameEntity = ''EntranceVoucher'' → Consulta Inventory.EntranceVoucher y descuenta devoluciones de Inventory.EntranceVoucherDevolution para calcular BaseValue; si @NameEntity = ''LoadMassive'' → Consulta Payments.LoadMassiveAccountPayable usando InvoiceValue como base sin impuesto; si @NameEntity = ''InitialBalance'' → Consulta Payments.InitialBalanceAccountPayable usando Value como base sin impuesto; si @NameEntity = ''FixedAssetEntry'' → Consulta FixedAsset.FixedAssetEntry calculando base, impuesto y tasa', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetDetailValueAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucher; Inventory.EntranceVoucherDevolution; Payments.LoadMassiveAccountPayable; Payments.InitialBalanceAccountPayable; FixedAsset.FixedAssetEntry', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetDetailValueAccountPayable';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payments', @level1type=N'FUNCTION', @level1name=N'GetDetailValueAccountPayable';
GO
