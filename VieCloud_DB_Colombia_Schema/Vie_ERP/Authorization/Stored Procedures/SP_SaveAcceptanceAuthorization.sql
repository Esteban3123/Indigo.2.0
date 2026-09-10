

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra la decisión de aceptación o rechazo de una autorización de servicio de salud. Recibe un XML con los datos de la respuesta (estado de aceptación, observaciones de rechazo, motivo de rechazo y usuario que rechaza) y actualiza el trámite de autorización correspondiente en la tabla de trazabilidad. Cuando el estado es rechazo (valor 2), marca el trámite con estado 5 (rechazado) y conserva el código del usuario que rechazó; cuando es aceptación, lo lleva a estado 7 (aceptado). Se usa en el flujo de auditoría médica o de autorizaciones para registrar la resolución final de una solicitud de servicio o procedimiento.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAcceptanceAuthorization';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_SaveAcceptanceAuthorization';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra la resolución (aceptación o rechazo) de un trámite de autorización actualizando su estado, observaciones y motivos a partir de un XML de entrada.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAcceptanceAuthorization';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener nodos /TableAcceptance con TraceabilityPaperworkId, AcceptanceStatus y, según el caso, datos de rechazo.; Debe existir un registro en Authorization.TraceabilityPaperwork con Id = TraceabilityPaperworkId del XML; de lo contrario el UPDATE no afecta filas.; AcceptanceStatus debe representar la decisión (valor 2 = rechazo; otro valor = aceptación) para que el mapeo de Status sea coherente.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAcceptanceAuthorization';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'PreviousStatus se sincroniza con el Status anterior antes de la transición (preserva historial de estado).; RejectionUserCode solo se persiste cuando la decisión es rechazo (AcceptanceStatus = 2); en aceptación queda NULL.; Cadenas vacías o ''0'' en AuthorizationRejectionId se normalizan a NULL; cadenas vacías en RejectionObservations y RejectionUserCode también se normalizan a NULL.; Toda excepción se captura y se devuelve como resultado tabular con CodeResult=999, sin propagar el error.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAcceptanceAuthorization';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'autorización de servicios; aceptación/rechazo de autorización; trazabilidad de trámites; auditoría médica', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAcceptanceAuthorization';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] Authorization.TraceabilityPaperwork: Para cada fila del XML, actualiza AcceptanceStatus, RejectionObservations y AuthorizationRejectionId; si AcceptanceStatus=2 fija Status=5 y conserva RejectionUserCode, en caso contrario fija Status=7 y RejectionUserCode=NULL; PreviousStatus toma el valor previo de Status.; [RETURN_RESULT] (resultset): Si la operación es exitosa devuelve CodeResult=0 y mensaje ''Se guardó correctamente''; si ocurre una excepción devuelve CodeResult=999 con ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAcceptanceAuthorization';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AcceptanceStatus = 2 (rechazo) → Status pasa a 5 y se conserva el RejectionUserCode recibido else Status pasa a 7 (aceptado) y RejectionUserCode se fuerza a NULL', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAcceptanceAuthorization';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.TraceabilityPaperwork', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAcceptanceAuthorization';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_SaveAcceptanceAuthorization';
-- GO
