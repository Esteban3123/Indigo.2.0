
-- =============================================
-- Author:		Carlos Cordoba
-- Create date: 21-07-2016
-- Description:	Procedimiento almacenado para establecer las facturas que se van a pasar a cuentas de dificil recaudo por medio de copiar y pegar o importacion de archivo
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_SetInvoicesHardCollection]
	@InvoicesXml AS XML
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @InvoicesTmp table(Id int IDENTITY PRIMARY KEY not null,InvoiceNumber varchar(30))
	declare @HardCollectionDetail table(InvoiceNumber varchar(30),AccountReceivableId int,Balance numeric(18, 2),[Status] tinyint ,Message varchar(max))
	
	BEGIN TRY
			
		
		INSERT into @InvoicesTmp
		SELECT XTags.value('.', 'varchar(30)') AS InvoiceNumber
		FROM @InvoicesXml.nodes('/HardCollectionInvoice/InvoiceNumber') as XTbl(XTags);	
		
		

		--agrego las facturas que ya estan en estado 15
		insert into @HardCollectionDetail
		select '0' ,0,0,2,'La factura '+i.InvoiceNumber +' ya esta en cuentas de dificil recaudo' 
		from @InvoicesTmp i inner join Portfolio.AccountReceivable ar on i.InvoiceNumber = ar.InvoiceNumber where ar.AccountReceivableType = 2 and ar.PortfolioStatus = 15

		--agrego las facturas que no tengan saldo 
		insert into @HardCollectionDetail
		select '0' ,0,0,2,'La factura '+i.InvoiceNumber +' no tiene saldo' 
		from @InvoicesTmp i inner join Portfolio.AccountReceivable ar on i.InvoiceNumber = ar.InvoiceNumber where ar.AccountReceivableType = 2 and ar.Balance  = 0

		--agrego las facturas que son validas
		insert into @HardCollectionDetail
		select i.InvoiceNumber ,ar.Id,ar.Balance ,1,'' 
		from @InvoicesTmp i inner join Portfolio.AccountReceivable ar on i.InvoiceNumber = ar.InvoiceNumber where ar.AccountReceivableType = 2 and ar.PortfolioStatus <> 15 and ar.Balance >0

		select * from  @HardCollectionDetail
		return
	END TRY
    BEGIN CATCH
        SELECT '0' ,0,0,2,ERROR_MESSAGE()+', Linea: '+CAST(ERROR_LINE() AS VARCHAR(20)) Message
    END CATCH;
END