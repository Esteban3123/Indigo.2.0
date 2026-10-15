

-- ============================================================================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 08/01/2019
-- Description:	Store procedure que se encarga de realizar todo el proceso de rias, se ejecuta primero el proceso de inscripción y después el proceso de actividades futuras
-- ============================================================================================================================================================================
CREATE PROCEDURE [dbo].[SP_RIASProcess]
	@Xml as xml,
	@PatientCode as varchar(25),
	@AdmissionNumber as varchar(10),
	@UserCode as varchar(20),

	@CodeMessage varchar(20) output, 
	@Message varchar(max) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON
	
	--Variables en donde se almacena el resultado del proceso de inscripción de RIAS
	declare @CodeMessageInscription varchar(20), @MessageInscription varchar(max)

	--Variables en donde se almacena el resultado del proceso de actividades futuras
	declare @CodeMessageFutureActivities varchar(20), @MessageFutureActivities varchar(max)
	
	begin try

		--Se ejecuta el proceso de inscripción de RIAS
		exec dbo.SP_RIASInscription @Xml, @PatientCode, @AdmissionNumber, @UserCode, 0, 2, 4, 1, @CodeMessageInscription output, @MessageInscription output
		
		--Si hay error ejecutando el sp de inscripción
		if @CodeMessageInscription = '999'
		begin
			set @CodeMessage = '999'
			set @Message = @MessageInscription
			return
		end
		
		--Se ejecuta el proceso de actividades futuras
		exec dbo.SP_RIASFutureActivities @Xml, @PatientCode, @AdmissionNumber, @UserCode, 4, @CodeMessageFutureActivities output, @MessageFutureActivities output
		
		--Si hay error ejecutando el sp de actividades futuras
		if @CodeMessageFutureActivities = '999'
		begin
			set @CodeMessage = '999'
			set @Message = @MessageFutureActivities
			return
		end
													
		set @CodeMessage = '0'
		set @Message = 'Se realizó el proceso de RIAS correctamente'
		return		
	end try
	begin catch
		set @CodeMessage = '999'
		set @Message = (select ERROR_MESSAGE())
		return
	end catch

END