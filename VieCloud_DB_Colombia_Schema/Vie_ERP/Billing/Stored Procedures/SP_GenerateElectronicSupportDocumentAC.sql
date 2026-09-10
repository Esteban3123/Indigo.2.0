
-- ===============================================================================================================
-- Author:	Giovanny Plazas Lozano	
-- Create date: 2022/07/28
-- Description:	Procedimiento para Generar Documentos Soporte, apartir de CxP
-- ==============================================================================================================
CREATE PROCEDURE [Billing].[SP_GenerateElectronicSupportDocumentAC]
	@AccountPayableIds As xml,
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
	DECLARE @_accountPayable TABLE (
									Id  int not null,
									Code varchar(20) not null,
									IdOperatingUnit  int,
									IdThirdParty int,
									BillNumber varchar(100) not null,
									BillDate datetime not null,
									DocumentDate datetime not null,
									ServicePeriodDate datetime not null,
									ExpirationDate datetime not null,
									Coments varchar(max),
									Status tinyint not null,
									HandlesDocumentSupport bit not null
	)
	
	DECLARE	@_accountPayableDetailConcept TABLE(	
												Id int identity(1,1) not null,
												ThirdPartyId int not null,
												SubTotalValue decimal(18,2) not null,
												TaxValue decimal(18,2))


	DECLARE	@_accountPayablesIds TABLE(	Id int not null)

	DECLARE @MessageAuxTablet  Table(	Id int identity(1,1),
										IdDS INT,
										Code varchar(20),
										Message varchar(max))
	DECLARE	@Id  int,
			@Code varchar(20),
			@IdOperatingUnit  int,
			@IdThirdParty int,
			@BillNumber varchar(100) ,
			@BillDate datetime ,
			@DocumentDate datetime,
			@ServicePeriodDate datetime,
			@ExpirationDate datetime,
			@Coments varchar(max),
			@Status tinyint,
			@HandlesDocumentSupport bit,
			@ConfirmationUser varchar(20),
			@CodeUser VARCHAR(20),
			@CompanyNit VARCHAR(25),
			@CompanyThirdPartyId INT,
			@BillingAuthorizationId INT
/*----------------------------------------------------*/
BEGIN TRY
	
	/*se extrae el xml user y nit company*/
	SELECT  @CodeUser = t.x.value('CodeUser[1]','varchar(20)'),
			@CompanyNit =t.x.value('CompanyNit[1]','varchar(25)')
	from @AccountPayableIds.nodes('/Data') t(x)

	/*se extrae el xml id de cxp*/
	INSERT INTO @_accountPayablesIds
	SELECT	t.x.value('Id[1]','int')
	from @AccountPayableIds.nodes('/Data/AccountPayableIds') t(x)

/*-----------------------------------------------------------------------*/

	/* se valida que el codigo del usuario y el nit vengan dentro del xml */
	IF (@CodeUser is null or @CodeUser='')
	begin
		INSERT INTO	@MessageAuxTablet VALUES(NULL, '999', 'no se encontro usuario')
		
		select *
		from @MessageAuxTablet
		RETURN
	END


	/*se valida que se inserte datos en la tabla variable*/
	IF not EXISTS(SELECT TOP 1  1 from @_accountPayablesIds)
	begin
			INSERT INTO	@MessageAuxTablet VALUES(NULL ,'999','No hay cuentas por pagar para generar documento soporte electrónico')
				
				select *
				from @MessageAuxTablet

			RETURN
	END

	IF NOT EXISTS(	SELECT TOP 1 apd.Id
					FROM Payments.AccountPayable ap WITH(NOLOCK)
					JOIN @_accountPayablesIds api on ap.Id=api.Id
					JOIN Payments.AccountPayableDetailConcept apd WITH(NOLOCK) on ap.Id=apd.IdAccountPayable)
	BEGIN
		INSERT INTO	@MessageAuxTablet VALUES(NULL ,'999','La cuenta por pagar no existe o no tiene conceptos para generar documento soporte electrónico')
								
		select *
		from @MessageAuxTablet
		RETURN
	END

	/*se consulta el por el nit de la compañia para sacar el Id del tercero y se valida que el tercero exista*/
	set @CompanyThirdPartyId = (SELECT top 1  th.Id
								FROM Common.ThirdParty th
								JOIN GeneralLedger.GeneralLedgerSettings gls ON gls.IdDian = th.Id
								)

	IF(@CompanyThirdPartyId is NULL)
	BEGIN
			INSERT INTO	@MessageAuxTablet VALUES(NULL,'999','No existe el Nit de la compañia en la base de datos');
		
		select *
		from @MessageAuxTablet
		RETURN
	END

	if @IdAutorization is null begin
		/*Se verifica que el usuario tenga una autorizacion de facturacion*/
		set @BillingAuthorizationId = (	SELECT TOP 1 ap.DocumentSupportId
										FROM Billing.BillingAuthorizationUser bau WITH(NOLOCK) 
										JOIN Billing.BillingAuthorization ba WITH(NOLOCK) on ba.Id=bau.BillingAuthorizationId
										JOIN Payments.AccountPayable ap WITH(NOLOCK) on ap.Id = (SELECT TOP 1 a.Id from @_accountPayablesIds a)
										where ba.InvoiceType = 4 and bau.UserCode=@CodeUser and ba.Status=1 AND (ba.Consecutive < ba.FinalInvoice))
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
				(s.Scope = 'OU' AND sd.IdOperatingUnit IN( SELECT ap.IdOperatingUnit
															from @_accountPayable ap))
			)
			) 
			BEGIN
				INSERT INTO @MessageAuxTablet VALUES(NULL,'999','No existe una secuencia automatica de Documento Soporte Electronico');
				
				select *
				from @MessageAuxTablet
				return
			END

 /*se inserta en la tabla variable las cxp de la base, para trabajar sobre ella*/
	INSERT INTO @_accountPayable
	SELECT	ap.Id,
			ap.code,
			ap.IdOperatingUnit,
			ap.IdThirdParty,
			ap.BillNumber,
			ap.BillDate,
			ap.DocumentDate,
			ap.ServicePeriodDate,
			ap.ExpirationDate,
			ap.Coments,
			ap.Status,
			ap.HandlesDocumentSupport
	FROM Payments.AccountPayable ap WITH(NOLOCK)
	JOIN @_accountPayablesIds i on ap.Id=i.Id
	WHERE ap.Status= 2 AND ap.HandlesDocumentSupport=1

	/*se valida si existe Cxp que no esten confirmadas o que no manejen documento soporte electronico*/
	IF EXISTS(	SELECT 1 
				FROM @_accountPayablesIds tmp
				WHERE tmp.Id not IN(SELECT ap.Id
									from @_accountPayable ap ))
			BEGIN
			INSERT INTO @MessageAuxTablet VALUES(NULL,'666',CONCAT('Los siguientes Ids de CxP no estaban confirmados o no manejaban documento soporte: ',(SELECT STRING_AGG(tmp.Id,',')
																																						  FROM @_accountPayablesIds tmp
																																						  WHERE tmp.Id not IN(SELECT ap.Id
																																											  from @_accountPayable ap )) ))
			END

/* se crea un cursor para recorrer la tabla variable de CxP para generar el documento soporte electronico*/
	DECLARE DATA_CURSOR CURSOR FOR
	SELECT Id,Code,IdOperatingUnit,IdThirdParty,BillNumber,BillDate,DocumentDate,ServicePeriodDate,ExpirationDate,Coments
	FROM @_accountPayable
	
	OPEN DATA_CURSOR;
		FETCH NEXT FROM DATA_CURSOR INTO @Id,@Code, @IdOperatingUnit, @IdThirdParty,@BillNumber,@BillDate, @DocumentDate,@ServicePeriodDate,@ExpirationDate,@Coments;
			WHILE @@fetch_status=0
				BEGIN
					
					--Se valida que la CxP No exista como documento soporte
					IF EXISTS
					(
						SELECT 1
						FROM Billing.ElectronicSupportDocument esd 
						WHERE @Id = esd.EntityId
							AND 'AccountPayable' = esd.EntityName
					)
					BEGIN 
						--Se pasa a la siguiente posición
						CONTINUE

					END

					--genero la secuencia para EL DOCUMENTO SOPORTE ELECTRONICO
					declare @idSequenceDetail int
					declare @pattern varchar(300)
					declare @NextS bigint
					declare @Scope varchar(5)
					declare @IdSequence int
					DECLARE @CodeDS VARCHAR(20)
					DECLARE @IdDs INT,
							@MessageAux VARCHAR(MAX),
							@DocumentNumber varchar(50),
							@SoftwarePin VARCHAR(256),
							@Enviroment TINYINT,
							@CUDS VARCHAR(500)

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

					/*obtengo la secuencia de la autorizacion de facturacion*/
					SET @DocumentNumber = (SELECT CONCAT(ba.InvoicePrefix,ba.Consecutive)
											FROM Billing.BillingAuthorization ba 
											WHERE ba.Id=@BillingAuthorizationId)

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
																	DocumentNumber,
																	EntityCode,
																	EntityName,
																	EntityId,
																	CreationUser,
																	CreationDate,
																	OperativeUnitId,
																	StatusElectronic,
																	[Year])
								
															SELECT	@CodeDS,
																	@DocumentDate,
																	@ServicePeriodDate,
																	@ExpirationDate,
																	ap.IdThirdParty SupplierThirdPartyId,
																	@CompanyThirdPartyId,
																	@BillingAuthorizationId,
																	@Coments,
																	ap.Value AS SubTotalValue,
																	0 AS TaxValue,
																	ap.Value AS TotalValue,
																	1 Status,
																	@DocumentNumber,
																	@Code,
																	'AccountPayable',
																	@Id,
																	@CodeUser,
																	Common.GETDATE(),
																	@IdOperatingUnit,
																	1 StatusElectronic,
																	YEAR(@DocumentDate)
																FROM Payments.AccountPayable ap
																WHERE ap.Id = @Id
					SET @IdDs=	SCOPE_IDENTITY()

					INSERT INTO @MessageAuxTablet VALUES(@IdDs,'001',CONCAT('Se guardó el documento: ',@CodeDS)) 
					
					/*actualizo la secuencia de la autorizacion*/
					update Billing.BillingAuthorization set Consecutive +=1 where Id=@BillingAuthorizationId

					/*Confirmo el documento Soporte*/
					UPDATE @MessageAuxTablet SET Code='002',Message=CONCAT('Se Confirmó el documento: ',@CodeDS) where IdDS=@IdDs
			
					FETCH NEXT FROM DATA_CURSOR INTO @Id,@Code, @IdOperatingUnit,@IdThirdParty,@BillNumber,@BillDate, @DocumentDate,@ServicePeriodDate,	@ExpirationDate,@Coments;
				END;
	CLOSE DATA_CURSOR;
	DEALLOCATE DATA_CURSOR;
	
	IF (SELECT COUNT(*) from @MessageAuxTablet) = 0
	BEGIN
	 INSERT INTO @MessageAuxTablet VALUES (NULL, '999', 'No se proceso ningún documento')
	END

	/*se retorna la tabla de control*/
	SELECT *
	from @MessageAuxTablet
	RETURN
END TRY
BEGIN CATCH
	SELECT 0 as Id ,NULL as IdDS,'999' as Code,CONCAT('Error generando el documento Soporte', ERROR_MESSAGE(),' - Linea: ',ERROR_LINE()) AS [Message]
END CATCH
END