

CREATE FUNCTION [dbo].[CalculateAgePayments] (@Fecha as datetime, @AccountPayableId as int)
RETURNS varchar(100)
AS
BEGIN

declare @Dias int
declare @Edad varchar(100)

SELECT @Dias = DATEDIFF(DAY, @Fecha, GETDATE()) + 1 

--set @edad = (SELECT Name 
--			FROM Payments.AgesPayments 
--			WHERE InitialRange <= @dias and EndRange >= @dias)

set @Edad = (SELECT DISTINCT AGP.Name
	FROM Payments.AccountPayable as AP
	inner join Payments.SettingPayments as SP on SP.IdOperatingUnit = AP.IdOperatingUnit
	inner join Payments.AgesPayments as AGP on AGP.SettingPaymentId = SP.Id
	WHERE InitialRange <= @Dias and EndRange >= @Dias and AP.Id = @AccountPayableId)

RETURN @Edad

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que calcula el tramo de antigüedad (aging) al que pertenece una cuenta por pagar, dado un documento de proveedor y una fecha de referencia. Calcula la diferencia en días entre la fecha indicada y el día actual, y luego consulta los rangos de antigüedad configurados (por ejemplo: 0-30 días, 31-60 días) que correspondan a la unidad operativa de esa cuenta por pagar, devolviendo el nombre del tramo al que pertenece. Se usa para clasificar facturas o documentos de proveedores según su vencimiento o mora dentro del módulo de cuentas por pagar, permitiendo análisis de cartera vencida y reportes de aging de pagos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CalculateAgePayments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'CalculateAgePayments';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el nombre del rango de antigüedad (aging) aplicable a una cuenta por pagar según los días transcurridos desde una fecha dada hasta hoy, usando la configuración de pagos de la unidad operativa.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalculateAgePayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una cuenta por pagar con el identificador suministrado.; La cuenta por pagar debe tener una unidad operativa con configuración de pagos (SettingPayments) registrada.; Debe existir al menos un rango de antigüedad (AgesPayments) asociado a esa configuración que contenga el número de días calculado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalculateAgePayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La antigüedad en días se calcula como la diferencia entre la fecha actual y la fecha provista más 1 día (DATEDIFF + 1).; El rango de antigüedad se obtiene de la configuración de pagos (SettingPayments) asociada a la unidad operativa de la cuenta por pagar, no de un catálogo global.; Solo se devuelve el rango cuyo InitialRange <= días <= EndRange.; Si no existe rango que contenga los días calculados, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalculateAgePayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas por pagar; Antigüedad de pagos (aging); Configuración de pagos por unidad operativa; Rangos de antigüedad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalculateAgePayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Cuando InitialRange <= (DATEDIFF(DAY,@Fecha,GETDATE())+1) <= EndRange y la cuenta por pagar coincide con la unidad operativa de la configuración, retorna el Name del rango de antigüedad correspondiente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalculateAgePayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payments.AccountPayable; Payments.SettingPayments; Payments.AgesPayments', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalculateAgePayments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'CalculateAgePayments';
GO
