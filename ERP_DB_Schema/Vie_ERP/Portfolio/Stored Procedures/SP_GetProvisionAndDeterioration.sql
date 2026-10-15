



-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 13/10/2016
-- Description:	Procedimiento que se encarga de validar el traslado
-- =============================================
CREATE PROCEDURE [Portfolio].[SP_GetProvisionAndDeterioration] 
	@CourtDate as date,
	@Process as int
AS
BEGIN

	--Tabla temporal para registrar las cuentas por cobrar y retornar
	declare @TempTable table(Id int primary key identity, AccountReceivableId int, CodeAccountReceivable varchar(20), 
	InvoiceNumber varchar(20), Balance numeric(18,0), ConfirmDate date, Ages varchar(50), Percentage decimal(5,2), Value numeric(18,0))
	
	Begin try
	
		
		
		select * from @TempTable
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message, 0 Id
	end catch

END