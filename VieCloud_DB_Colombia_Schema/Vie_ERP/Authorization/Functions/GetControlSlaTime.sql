-- =============================================
-- Author:      Maria Fernanda Gasca
-- Create date: 2026-07-03
-- Description: Devuelve el tiempo SLA aplicable a un control de autorización del
--              modelo integral ([Authorization].[AuthorizationControl]) según su
--              estado. Equivalente funcional de [Authorization].[GetRequestTime]
--              pero para la máquina de estados NUEVA (1=No Solicitado, 2=En Trámite,
--              3=Radicado, 4=Autorizado, 5=Entregado, 6=Confirmado, 7=Cancelado,
--              8=Rechazado) — la función vieja está cableada a los estados del
--              flujo de TraceabilityPaperwork y daría colores incorrectos.
--              Estados terminales (5-8) o desconocidos → NULL (sin semáforo;
--              las vistas del dashboard intrahospitalario lo interpretan como
--              ColorRequest = NULL).
-- =============================================
CREATE FUNCTION [Authorization].[GetControlSlaTime]
(
	@Status TINYINT,			-- Estado del control (AuthorizationControl.Status)
	@Request INT,				-- SLA fase solicitud   (portafolio hospitalario)
	@Radicated INT,				-- SLA fase radicación
	@DeliveryService INT		-- SLA fase entrega del servicio
)
RETURNS INT
AS
BEGIN
	RETURN	CASE @Status
				WHEN 1 THEN @Request			-- No Solicitado  → tiempo para gestionar la solicitud
				WHEN 2 THEN @Request			-- En Trámite     → sigue corriendo el tiempo de solicitud
				WHEN 3 THEN @Radicated			-- Radicado       → tiempo de respuesta de la entidad
				WHEN 4 THEN @DeliveryService	-- Autorizado     → tiempo para entregar el servicio
			END									-- 5-8 terminales → NULL (sin semáforo)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Selecciona el tiempo SLA aplicable a un control de autorización del modelo integral (Authorization.AuthorizationControl) según su estado: 1/2 (No Solicitado/En Trámite) → tiempo de solicitud; 3 (Radicado) → tiempo de radicación; 4 (Autorizado) → tiempo de entrega del servicio. Retorna NULL para estados terminales (5=Entregado, 6=Confirmado, 7=Cancelado, 8=Rechazado), que no llevan semáforo. Equivalente de GetRequestTime para la máquina de estados nueva; se usa junto a fnGetColor y GetRequestElapsedTime en las vistas ViewDashboardIntrahospital*.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetControlSlaTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'mgasca_2026-07-03_semaforizacion-intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetControlSlaTime';
GO
