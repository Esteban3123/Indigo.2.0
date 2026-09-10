
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de cartera que recibe un listado de facturas en formato XML y las clasifica para determinar cuáles pueden ser marcadas como de difícil recaudo. Para cada factura recibida, consulta las cuentas por cobrar (Portfolio.AccountReceivable) y valida tres condiciones: si la factura ya está en estado de difícil recaudo, si no tiene saldo pendiente, o si es válida para ser trasladada. Devuelve un resultado detallado por cada factura indicando su estado, saldo y un mensaje explicativo del resultado de la validación. Se usa en el módulo de cartera para gestionar el proceso de clasificación de facturas morosas o de cobro difícil, ya sea por carga manual (copiar y pegar) o importación de archivo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SetInvoicesHardCollection';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_SetInvoicesHardCollection';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Clasifica un listado de facturas recibido en XML para determinar cuáles son aptas para pasar a cuentas de difícil recaudo y cuáles deben rechazarse, devolviendo el resultado del análisis.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SetInvoicesHardCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /HardCollectionInvoice/InvoiceNumber con los números de factura.; Las facturas evaluadas deben existir en Portfolio.AccountReceivable con AccountReceivableType = 2 (cuenta por cobrar tipo factura) para ser consideradas.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SetInvoicesHardCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan cuentas por cobrar con AccountReceivableType = 2.; Una factura ya marcada con PortfolioStatus = 15 nunca se reclasifica como válida.; Una factura sin saldo (Balance = 0) nunca se considera válida para difícil recaudo.; El procedimiento no modifica AccountReceivable; solo evalúa y reporta resultados.; Los errores se capturan y devuelven como fila de resultado con Status=2 en lugar de propagarse.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SetInvoicesHardCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Cuenta por cobrar; Cuentas de difícil recaudo; Saldo de cartera; Estado de cartera (PortfolioStatus)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SetInvoicesHardCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @HardCollectionDetail: Cuando la factura existe con AccountReceivableType=2 y PortfolioStatus=15, se marca con Status=2 y mensaje ''La factura X ya esta en cuentas de dificil recaudo''.; [INSERT] @HardCollectionDetail: Cuando la factura existe con AccountReceivableType=2 y Balance=0, se marca con Status=2 y mensaje ''La factura X no tiene saldo''.; [INSERT] @HardCollectionDetail: Cuando la factura existe con AccountReceivableType=2, PortfolioStatus<>15 y Balance>0, se marca como válida (Status=1) registrando su Id y Balance.; [RETURN_RESULT] (resultset): Devuelve el contenido de @HardCollectionDetail con el detalle de facturas válidas y rechazadas; en caso de error, devuelve una fila Status=2 con ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SetInvoicesHardCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ar.AccountReceivableType=2 AND ar.PortfolioStatus=15 → Rechaza la factura indicando que ya está en cuentas de difícil recaudo (Status=2).; si ar.AccountReceivableType=2 AND ar.Balance=0 → Rechaza la factura indicando que no tiene saldo (Status=2).; si ar.AccountReceivableType=2 AND ar.PortfolioStatus<>15 AND ar.Balance>0 → Acepta la factura como candidata válida a difícil recaudo (Status=1).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SetInvoicesHardCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SetInvoicesHardCollection';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_SetInvoicesHardCollection';
-- GO
