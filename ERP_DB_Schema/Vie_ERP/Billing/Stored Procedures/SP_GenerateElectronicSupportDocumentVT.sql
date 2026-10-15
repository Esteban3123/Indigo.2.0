
-- ===============================================================================================================
-- Author:	Giovanny Plazas Lozano	
-- Create date: 2022/08/01
-- Description:	Procedimiento para Generar Documentos Soporte, apartir de Comprobante de egreso
-- ==============================================================================================================
CREATE PROCEDURE [Billing].[SP_GenerateElectronicSupportDocumentVT]
	@VoucherTransactionIds As xml,
	@IdAutorization as int = null
	------------------------------------------------------
AS
SET
  ANSI_NULLS,
  QUOTED_IDENTIFIER,
  CONCAT_NULL_YIELDS_NULL,
  ANSI_WARNINGS,
  ANSI_PADDING
ON;
BEGIN
	SET NOCOUNT ON

	/*se declara las tablas variables y las variables*/
	DECLARE @_voucherTransaction TABLE (
									Id						INT				NOT NULL,
									Code					VARCHAR(20)		NOT NULL,
									IdOperatingUnit			INT				NOT NULL,
									IdThirdParty			INT,
									DocumentDate			DATETIME		NOT NULL,
									Detail					VARCHAR(MAX),
									[Status]				TINYINT			NOT NULL,
									[VoucherClass]			INT				NOT NULL,
									HandlesDocumentSupport	BIT				NOT NULL,
									SubTotalValue			DECIMAL(18,2)	NOT NULL,
									TaxValue				DECIMAL(18,2)	NOT NULL)


	DECLARE	@_voucherTransactionIds TABLE(	Id	INT NOT NULL)

	DECLARE @MessageAuxTablet  Table(	Id		INT IDENTITY(1,1),
										IdDS	INT,
										Code	VARCHAR(20),
										Message	VARCHAR(max))
	DECLARE
			@CodeUser				VARCHAR(20),
			@CompanyNit				VARCHAR(25),
			@CompanyThirdPartyId	INT,
			@BillingAuthorizationId INT,
			@Id						INT,
			@IdOperatingUnit		INT,
			@DocumentDate			DATETIME
/*----------------------------------------------------*/
BEGIN TRY
	
	/*se extrae el xml user y nit company*/
	SELECT  @CodeUser = t.x.value('CodeUser[1]','varchar(20)'),
			@CompanyNit =t.x.value('CompanyNit[1]','varchar(25)')
	from @VoucherTransactionIds.nodes('/Data') t(x)

	/*se extrae el xml id de cxp*/
	INSERT INTO @_voucherTransactionIds
	SELECT	t.x.value('Id[1]','int')
	from @VoucherTransactionIds.nodes('/Data/VoucherTransactionIds') t(x)

/*-----------------------------------------------------------------------*/

	/* se valida que el codigo del usuario y el nit vengan dentro del xml */
	IF (@CodeUser is null or @CodeUser='') or (@CompanyNit='' or @CompanyNit is null)
	begin
		INSERT INTO	@MessageAuxTablet VALUES(NULL, '999',concat(iif((@CodeUser is null or @CodeUser=''),'Codigo Usuario',''),' ',iif((@CompanyNit is null or @CompanyNit=''),'El Nit de la compañia esta vacio','')))
		
		select *
		from @MessageAuxTablet
		RETURN
	END

	/*se valida que se inserte datos en la tabla variable*/
	IF not EXISTS(SELECT TOP 1  1 from @_voucherTransactionIds)
	begin
			INSERT INTO	@MessageAuxTablet VALUES(NULL ,'999','No hay comprobantes de egreso para generar documento soporte electrónico')
				
				select *
				from @MessageAuxTablet
			RETURN
	END

	IF NOT EXISTS(	SELECT TOP 1 vtd.Id
					FROM Treasury.VoucherTransaction vt WITH(NOLOCK)
					JOIN @_voucherTransactionIds vti on vt.Id=vti.Id
					JOIN Treasury.VoucherTransactionDetails vtd WITH(NOLOCK) on vt.Id=vtd.IdVoucherTransaction)
							BEGIN
								INSERT INTO	@MessageAuxTablet VALUES(NULL ,'999','El Comprobante de egreso no existe o no tiene detalles para generar documento soporte electrónico')
								
								select *
								from @MessageAuxTablet
								RETURN
							END

	/*se consulta el por el nit de la compañia para sacar el Id del tercero y se valida que el tercero exista*/
	set @CompanyThirdPartyId = (SELECT top 1  th.Id
								FROM Common.ThirdParty th
								WHERE th.Nit= @CompanyNit)

	IF(@CompanyThirdPartyId is NULL)
	BEGIN
			INSERT INTO	@MessageAuxTablet VALUES(NULL,'999','No existe el Nit de la compañia en la base de datos');
		
		select *
		from @MessageAuxTablet
		RETURN
	END

	if @IdAutorization is null begin
		/*Se verifica que el usuario tenga una autorizacion de facturacion*/
		set @BillingAuthorizationId = (	SELECT TOP 1 ba.Id
										FROM Billing.BillingAuthorizationUser bau WITH(NOLOCK) 
										JOIN Billing.BillingAuthorization ba WITH(NOLOCK) on ba.Id=bau.BillingAuthorizationId
										where ba.InvoiceType = 4 And bau.UserCode=@CodeUser and ba.Status=1 AND (ba.Consecutive < ba.FinalInvoice))
	end
	ELSE IF NOT EXISTS (
						SELECT 1 FROM 
						Billing.BillingAuthorization 
						WITH (NOLOCK) WHERE Id = @IdAutorization 
						AND InvoiceType = 4
					)
	BEGIN 
		INSERT INTO	@MessageAuxTablet VALUES(NULL,'999','La autorización seleccionada no es de tipo documento soporte');

		SELECT *
		FROM @MessageAuxTablet
		RETURN
	END
	ELSE
	BEGIN 
		set @BillingAuthorizationId = @IdAutorization
	END

	IF(@BillingAuthorizationId is NULL)
	BEGIN
			INSERT INTO	@MessageAuxTablet VALUES(NULL,'999','No existe autorización de facturación para el usuario');

		select *
		from @MessageAuxTablet
		RETURN
	END

 /*se inserta en la tabla variable comprobante de egreso, para trabajar sobre ella*/
	INSERT INTO @_voucherTransaction
	SELECT	 vt.Id
			,vt.Code
			,vt.IdUnitOperative
			,vt.IdThirdParty
			,vt.DocumentDate
			,vt.Detail
			,vt.Status
			,vt.VoucherClass
			,1 as HandlesDocumentSupport --se cambia esta linea
			,sum(vtd.Value*iif(vtd.Nature=1,1,-1))
			,0
	FROM Treasury.VoucherTransaction vt WITH(NOLOCK)
	JOIN @_voucherTransactionIds i on vt.Id=i.Id
	JOIN Treasury.VoucherTransactionDetails vtd WITH(NOLOCK) on vt.Id=vtd.IdVoucherTransaction
	JOIN Treasury.ExpenseConcepts ec WITH(NOLOCK) ON vtd.IdExpenseConcept = ec.Id
	WHERE vt.Status= 2 AND  ec.Behavior =6 --se quita el filtro pormaneja documento soporte
	GROUP by vt.Id,vt.code,vt.IdUnitOperative,vt.IdThirdParty,vt.DocumentDate,vt.Detail,vt.Status,vt.VoucherClass


	/*se valida si existe Comprobantes de egresos que no esten confirmados o que no manejen documento soporte electronico*/
	IF EXISTS(	SELECT 1 
				FROM @_voucherTransactionIds tmp
				WHERE tmp.Id not IN(SELECT vt.Id
									from @_voucherTransaction vt ))
			BEGIN
			INSERT INTO @MessageAuxTablet VALUES(NULL,'666',CONCAT('Los siguientes Ids de Comprobantes de egreso no estan confirmados o no manejaban documento soporte: ',(SELECT STRING_AGG(tmp.Id,',')	
																																											FROM @_voucherTransactionIds tmp
																																											WHERE tmp.Id not IN(SELECT vt.Id
																																																from @_voucherTransaction vt )) ))
			END

	/*se valida que el formulario de documento soporte electronico tenga configurada la secuencia automatica para generar los codigos*/	
	IF NOT EXISTS (
		SELECT 1
		FROM Billing.BillingSequence s WITH (NOLOCK)
		JOIN Billing.BillingSequenceDetail sd WITH (NOLOCK) ON s.Id = sd.IdSequenseBillingC
		JOIN Common.Sequense cs on sd.IdSequense = cs.Id
		WHERE s.IdForm = '2818' AND s.IsManual = 0
			AND 
			(
				(s.Scope = 'O')
				OR
				(s.Scope = 'OU' AND sd.IdOperatingUnit IN( SELECT vt.IdOperatingUnit
															from @_voucherTransaction vt))
			)
			) 
			BEGIN
				INSERT INTO @MessageAuxTablet VALUES(NULL,'999','No existe una secuencia automatica de Documento Soporte Electronico');				
				select *
				from @MessageAuxTablet
				return
			END

/* se crea un cursor para recorrer la tabla variable de comprobantes de egreso para generar el documento soporte electronico*/
	DECLARE DATA_CURSOR CURSOR FOR
	SELECT Id,IdOperatingUnit,DocumentDate
	FROM @_voucherTransaction
	
	OPEN DATA_CURSOR;
		FETCH NEXT FROM DATA_CURSOR INTO @Id,@IdOperatingUnit,@DocumentDate;
			WHILE @@fetch_status=0
				BEGIN									
									--genero la secuencia para EL DOCUMENTO SOPORTE ELECTRONICO
									declare @idSequenceDetail	int
									declare @pattern			varchar(300)
									declare @NextS				bigint
									declare @Scope				varchar(5)
									declare @IdSequence			int
									DECLARE @CodeDS				VARCHAR(20)
									DECLARE @IdDs				INT,
											@DocumentNumber		varchar(50)

									select @IdSequence = Id, @Scope = Scope from Billing.BillingSequence With(Nolock) where IdForm = '2818'

									if @Scope = 'OU' begin
										select @pattern = cs.Pattern, @idSequenceDetail = psd.Id  
										from Billing.BillingSequenceDetail psd  With(Nolock)
										inner join Common.Sequense cs With(Nolock) on cs.Id = psd.IdSequense 
										where psd.IdSequenseBillingC = @IdSequence and IdOperatingUnit = @IdOperatingUnit
									end
									else begin
										select @pattern = cs.Pattern, @idSequenceDetail = psd.Id  
										from Billing.BillingSequenceDetail psd With(Nolock) 
										inner join Common.Sequense cs With(Nolock) on cs.Id = psd.IdSequense 
										where psd.IdSequenseBillingC = @IdSequence
									end
									/*se actualiza la tabla de secuencia con el numero de documento que se van a generar*/
									update Billing.BillingSequenceDetail set @NextS = [Next] += 1 where Id = @idSequenceDetail
									SELECT @CodeDS =dbo.GetSequence('',@pattern,(@NextS - 1))

									/*se guarda documento soporte electronico*/
									INSERT INTO billing.ElectronicSupportDocument (	Code,
																					DocumentDate,
																					RadicationDate,
																					DueDate,
																					SupplierThirdPartyId,
																					CustomerThirdPartyId,
																					BillingAuthorizationId,
																					[Description],
																					SubTotalValue,
																					TaxValue,
																					TotalValue,
																					Status,
																					EntityCode,
																					EntityName,
																					EntityId,
																					CreationUser,
																					CreationDate,
																					OperativeUnitId)
												
																			SELECT	@CodeDS,
																					vt.DocumentDate,
																					vt.DocumentDate,
																					vt.DocumentDate,
																					vt.IdThirdParty,
																					@CompanyThirdPartyId,
																					@BillingAuthorizationId,
																					vt.Detail,
																					vt.SubTotalValue,
																					vt.TaxValue,
																					(vt.SubTotalValue + vt.TaxValue),
																					1,
																					vt.Code,
																					'VoucherTransaction',
																					vt.Id,
																					@CodeUser,
																					Common.GETDATE(),
																					vt.IdOperatingUnit																					
																			FROM @_voucherTransaction vt
																			WHERE vt.Id=@Id
																					
									SET @IdDs=	SCOPE_IDENTITY()

									INSERT INTO @MessageAuxTablet VALUES(@IdDs,'001',CONCAT('Se guardó el documento: ',@CodeDS)) 

									/*se verifica que la fecha de la autorizacion este vigente a la fecha de la radicacion de la cxp*/
									IF EXISTS(SELECT 1 FROM Billing.BillingAuthorization ba WHERE ba.Id=@BillingAuthorizationId and ba.FinalDate < @DocumentDate)
										BEGIN
											INSERT into @MessageAuxTablet VALUES(@IdDs,'666','La fecha de autorización esta vencida')
											FETCH NEXT FROM DATA_CURSOR INTO @Id,@IdOperatingUnit,@DocumentDate;
										END							

									/*obtengo la secuencia de la autorizacion de facturacion*/
									SET @DocumentNumber = (SELECT CONCAT(ba.InvoicePrefix,ba.Consecutive)
															FROM Billing.BillingAuthorization ba 
															WHERE ba.Id=@BillingAuthorizationId)
									
									/*actualizo la secuencia de la autorizacion*/
									update Billing.BillingAuthorization set Consecutive +=1 where Id=@BillingAuthorizationId

									/*Confirmo el documento Soporte*/
									UPDATE billing.ElectronicSupportDocument SET Status=1, DocumentNumber=@DocumentNumber, ModificationUser=@CodeUser, ModificationDate= Common.GETDATE() where Id=@IdDs
									UPDATE @MessageAuxTablet SET Code='002',Message=CONCAT('Se Confirmó el documento: ',@CodeDS) where IdDS=@IdDs
											
					FETCH NEXT FROM DATA_CURSOR INTO @Id,@IdOperatingUnit,@DocumentDate;
				END;
	CLOSE DATA_CURSOR;
	DEALLOCATE DATA_CURSOR;

	/*se retorna la tabla de control*/
	SELECT *
	from @MessageAuxTablet
	RETURN
END TRY
BEGIN CATCH
	SELECT 0 as Id ,NULL as IdDS,'999' as Code,CONCAT('Error generando el documento Soporte', ERROR_MESSAGE(),' - Linea: ',ERROR_LINE()) AS [Message]
END CATCH
END