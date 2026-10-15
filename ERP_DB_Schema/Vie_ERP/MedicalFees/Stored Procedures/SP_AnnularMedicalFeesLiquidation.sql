




-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 16/02/2016
-- Description:	Procedimiento que se encarga de guardar, actualizar la liquidación de honorarios médicos
-- =============================================
CREATE PROCEDURE [MedicalFees].[SP_AnnularMedicalFeesLiquidation] 
	@MedicalFeesLiquidationId as int,
	@CodeUser as varchar(20)
AS
BEGIN

	Begin try
	
		--Actualizo el estado de MedicalFeesCausation
		update [MedicalFees].MedicalFeesCausation
		set Status = case when InvoiceReversal = 1 then 4 when ObjectionAccepted = 1 then 4 else 1 end
		where Id in (select MedicalFeesCausationId from MedicalFees.MedicalFeesLiquidationDetail where MedicalFeesLiquidacionId = @MedicalFeesLiquidationId and LiquidationType = 1)
		
		--Actualizo el estado de MedicalFeesLiquidation a anulado
		update MedicalFees.MedicalFeesLiquidation
		set Status = 3, AnnulmentUser = @CodeUser, AnnulmentDate = [Common].[GETDATE]()
		where Id = @MedicalFeesLiquidationId

		select 0 as CodeMessage, 'Se anuló correctamente' as Message
		
	end try
	begin catch
		select 999 as CodeMessage, ERROR_MESSAGE() as Message
	end catch

END