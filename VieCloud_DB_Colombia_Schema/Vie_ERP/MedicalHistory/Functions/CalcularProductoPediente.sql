-- =============================================
-- Author:      Felipe Ortiz
-- Create Date: 03/02/2022
-- Description: Función para calcular la cantidad pendiente de entregar a un paciente.
-- =============================================
CREATE FUNCTION [MedicalHistory].[CalcularProductoPediente]
(
 @Paciente VARCHAR(20),
 @Ingreso VARCHAR(20),
 @Producto VARCHAR(20)
 )
RETURNS int
AS
BEGIN
	  DECLARE @cantidadPendiente int

	  select @cantidadPendiente = (SELECT ISNULL(SUM(CANPENPRO),0) AS Cantidad FROM dbo.HCFARMEPD WHERE IPCODPACI= @Paciente AND NUMINGRES= @Ingreso AND CODPRODUC= @Producto AND PROESTADO='1' AND EXTRAMURAL =0)

 return @cantidadPendiente

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que calcula la cantidad pendiente de entrega de un medicamento o producto farmacéutico a un paciente hospitalizado. Recibe como parámetros la cédula del paciente, el número de ingreso y el código del producto, y consulta la tabla de entregas farmacéuticas (HCFARMEPD) sumando las cantidades pendientes (CANPENPRO) de los registros activos (estado ''1'') que no sean extramuros. Es útil para determinar cuántas unidades de un medicamento o insumo aún no han sido dispensadas al paciente durante su ingreso.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'FUNCTION', @level1name = N'CalcularProductoPediente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'FUNCTION', @level1name = N'CalcularProductoPediente';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la cantidad total pendiente de entregar de un producto farmacéutico para un paciente en un ingreso específico, considerando solo pedidos activos e intramurales.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'FUNCTION', @level1name=N'CalcularProductoPediente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben proporcionarse identificadores de paciente, ingreso y producto para filtrar los pedidos farmacéuticos.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'FUNCTION', @level1name=N'CalcularProductoPediente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran pedidos en estado ''1'' (activos/vigentes).; Solo se consideran pedidos no extramurales (EXTRAMURAL = 0).; Si no existen pedidos que cumplan los criterios, retorna 0 en lugar de NULL.; El cálculo se restringe al trío paciente + ingreso + producto.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'FUNCTION', @level1name=N'CalcularProductoPediente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso; producto farmacéutico; pedido de medicamento; cantidad pendiente de entrega', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'FUNCTION', @level1name=N'CalcularProductoPediente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFARMEPD: Suma CANPENPRO de los pedidos donde paciente, ingreso y producto coinciden, PROESTADO=''1'' y EXTRAMURAL=0; si no hay registros, retorna 0.', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'FUNCTION', @level1name=N'CalcularProductoPediente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'FUNCTION', @level1name=N'CalcularProductoPediente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalHistory', @level1type=N'FUNCTION', @level1name=N'CalcularProductoPediente';
GO
