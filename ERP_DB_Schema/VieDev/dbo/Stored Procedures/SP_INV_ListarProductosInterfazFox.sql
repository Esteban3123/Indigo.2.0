
CREATE PROCEDURE [dbo].[SP_INV_ListarProductosInterfazFox]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT IPRCODIGO AS 'Codigo',RTRIM(IPRDESCOR) AS 'Descripción Corta', RTRIM(IPRDESLAR) AS 'Descripción Larga', CASE WHEN IPRCLAPRO='P' THEN 'Producto' WHEN IPRCLAPRO='S' THEN 'Servicio' END AS 'Clase', CASE WHEN IPRTIPPRO='0' THEN 'Ninguno' WHEN IPRTIPPRO='1' THEN 'Suministro' WHEN IPRTIPPRO='2' THEN 'Medicamento' END AS 'Tipo', RTRIM(B.IUNNOMBRE) AS 'U. de Compra', RTRIM(B1.IUNNOMBRE) AS 'U. de Consumo', RTRIM(IPRCONCEN) AS 'Concentración'
FROM dbo.INPRODUC A
LEFT OUTER JOIN dbo.INUNIDAD B ON A.IUNCODIGO=B.IUNCODIGO 
LEFT OUTER JOIN dbo.INUNIDAD B1 ON B.IUNCODUND = B1.IUNCODIGO 
END