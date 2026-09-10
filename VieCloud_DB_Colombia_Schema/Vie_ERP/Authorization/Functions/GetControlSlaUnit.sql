-- =============================================
-- Author:      Maria Fernanda Gasca
-- Create date: 2026-07-03
-- Description: Devuelve la UNIDAD del tiempo SLA (1=Minutos, 2=Horas, 3=Días)
--              aplicable a un control de autorización del modelo integral
--              ([Authorization].[AuthorizationControl]) según su estado.
--              Complemento de [Authorization].[GetControlSlaTime]; mismo mapeo
--              de la máquina de estados nueva. Estados terminales (5-8) → NULL.
-- =============================================
CREATE FUNCTION [Authorization].[GetControlSlaUnit]
(
	@Status TINYINT,				-- Estado del control (AuthorizationControl.Status)
	@RequestUnit TINYINT,			-- Unidad SLA fase solicitud
	@RadicatedUnit TINYINT,			-- Unidad SLA fase radicación
	@DeliveryServiceUnit TINYINT	-- Unidad SLA fase entrega del servicio
)
RETURNS INT
AS
BEGIN
	RETURN	CASE @Status
				WHEN 1 THEN @RequestUnit
				WHEN 2 THEN @RequestUnit
				WHEN 3 THEN @RadicatedUnit
				WHEN 4 THEN @DeliveryServiceUnit
			END									-- 5-8 terminales → NULL (sin semáforo)
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Selecciona la unidad del tiempo SLA (1=Minutos, 2=Horas, 3=Días) aplicable a un control de autorización del modelo integral según su estado: 1/2 → unidad de solicitud; 3 → unidad de radicación; 4 → unidad de entrega. Retorna NULL para estados terminales (5-8). Complemento de GetControlSlaTime; se usa como parámetro de GetRequestElapsedTime en las vistas ViewDashboardIntrahospital*.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetControlSlaUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'mgasca_2026-07-03_semaforizacion-intrahospitalaria', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'FUNCTION', @level1name = N'GetControlSlaUnit';
GO
