-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [HumanTalent].[SP_InsertVacationRequestDetail]
	-- Add the parameters for the stored procedure here
	    @IdVacationRequest INT,         -- IdVacationRequest - int
		@StatusRequest INT,         -- StatusRequest - tinyint
		@UserStatus VARCHAR(20),        -- UserStatus - varchar(20)
		@Comments VARCHAR(500)        -- Comments - varchar(500)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT OFF;

    -- Insert statements for procedure here
	INSERT HumanTalent.VacationRequestDetail
	(
		IdVacationRequest,
		StatusRequest,
		UserStatus,
		DateStatus,
		Comments
	)
	VALUES
	(   @IdVacationRequest,         -- IdVacationRequest - int
		@StatusRequest,         -- StatusRequest - tinyint
		@UserStatus,        -- UserStatus - varchar(20)
		[Common].[GETDATE](), -- DateStatus - datetime
		@Comments         -- Comments - varchar(500)
    )

	--Update VacationRequest
	UPDATE HumanTalent.VacationRequest SET LastStatus = @StatusRequest WHERE Id = @IdVacationRequest;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra un nuevo cambio de estado en el historial de seguimiento de una solicitud de vacaciones del personal. Inserta un registro en el detalle de la solicitud con el estado nuevo (aprobado, rechazado, pendiente), el usuario responsable del cambio, la fecha actual y los comentarios justificativos. Simultáneamente actualiza el último estado vigente en la solicitud de vacaciones principal, manteniendo ambas tablas sincronizadas. Se utiliza en el flujo de aprobación o rechazo de solicitudes de disfrute de vacaciones de empleados.', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'PROCEDURE', @level1name = N'SP_InsertVacationRequestDetail';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'HumanTalent', @level1type = N'PROCEDURE', @level1name = N'SP_InsertVacationRequestDetail';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra un movimiento de cambio de estado en el detalle de una solicitud de vacaciones y sincroniza el último estado en la cabecera.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequestDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una solicitud de vacaciones (HumanTalent.VacationRequest) con Id = @IdVacationRequest para que el UPDATE afecte filas.; La función [Common].[GETDATE]() debe estar disponible para timbrar la fecha del estado.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequestDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha del cambio de estado (DateStatus) se obtiene siempre desde [Common].[GETDATE](), nunca se acepta como parámetro.; Cada inserción de detalle va acompañada de la actualización del LastStatus en la cabecera de la solicitud, garantizando coherencia entre detalle y cabecera.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequestDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de vacaciones; Detalle/historial de estados de solicitud; Último estado de solicitud', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequestDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] HumanTalent.VacationRequestDetail: Siempre inserta una fila con el nuevo estado, usuario, comentarios y DateStatus = [Common].[GETDATE]() para la solicitud indicada.; [UPDATE] HumanTalent.VacationRequest: Tras insertar el detalle, actualiza LastStatus = @StatusRequest WHERE Id = @IdVacationRequest, manteniendo sincronizado el último estado de la cabecera.', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequestDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequestDetail';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'HumanTalent', @level1type=N'PROCEDURE', @level1name=N'SP_InsertVacationRequestDetail';
-- GO
