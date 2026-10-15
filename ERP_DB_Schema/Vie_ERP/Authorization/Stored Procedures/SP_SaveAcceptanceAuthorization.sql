


-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 09/07/2020
-- Description:	Procedimiento que se encarga actualizar los campos de aceptación de autorización
-- =============================================
CREATE PROCEDURE [Authorization].[SP_SaveAcceptanceAuthorization] 
    @Xml AS xml
AS
BEGIN
	SET NOCOUNT ON

	--Tabla para obtener los datos del xml
	declare @TableAcceptance table(TraceabilityPaperworkId int, AcceptanceStatus tinyint, RejectionObservations varchar(max), AuthorizationRejectionId int, RejectionUserCode varchar(20))
	
	begin try	
	
		--Se obtienen los datos del xml
		insert into @TableAcceptance
		select 
			t.x.value('TraceabilityPaperworkId[1]','int') as TraceabilityPaperworkId,
			t.x.value('AcceptanceStatus[1]','tinyint') as AcceptanceStatus,
			IIF(t.x.value('RejectionObservations[1]','varchar(max)') = '', null, t.x.value('RejectionObservations[1]','varchar(max)')) as RejectionObservations,
			IIF(t.x.value('AuthorizationRejectionId[1]','varchar(20)') = '' or t.x.value('AuthorizationRejectionId[1]','varchar(20)') = '0', null, t.x.value('AuthorizationRejectionId[1]','varchar(20)')) as AuthorizationRejectionId,
			IIF(t.x.value('RejectionUserCode[1]','varchar(20)') = '', null, t.x.value('RejectionUserCode[1]','varchar(20)')) as RejectionUserCode
		from @Xml.nodes('/TableAcceptance') t(x)
		
		--Se actualiza el estado de la aceptación
		update tp set tp.AcceptanceStatus = t.AcceptanceStatus, tp.RejectionObservations = t.RejectionObservations, 
		tp.AuthorizationRejectionId = t.AuthorizationRejectionId, tp.RejectionUserCode = IIF(t.AcceptanceStatus = 2, t.RejectionUserCode, null),
		tp.Status = IIF(t.AcceptanceStatus = 2, 5, 7),
		tp.PreviousStatus = tp.Status
		from @TableAcceptance t
		inner join [Authorization].TraceabilityPaperwork tp on tp.Id = t.TraceabilityPaperworkId

		select 0 AS CodeResult, 'Se guardó correctamente' AS MessageResult
		return
	end try
	begin catch
		select 999 AS CodeResult, ERROR_MESSAGE() + ' Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)) AS MessageResult
		return
	end catch
END