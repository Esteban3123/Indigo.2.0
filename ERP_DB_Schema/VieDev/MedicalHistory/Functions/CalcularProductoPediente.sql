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