





-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 22/03/2016
-- Description:	Procedimiento que se encarga de obtener los servicios no quirurgicos
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_ViewListNoSurgical] 
	@InvoiceId as int
AS
BEGIN

	Begin try
	
		
		select 0 as CodeMessage, 'Se anuló correctamente' as Message
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch

END