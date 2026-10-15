

CREATE VIEW [dbo].[INVProductosInterfazNet]
AS
Select 1 as Nombre
--SELECT     A.IPRCODIGO AS Codigo, RTRIM(A.IPRDESCOR) AS DescripcionCorta, RTRIM(A.IPRDESLAR) AS DescripcionLarga, 
--                      CASE WHEN IPRCLAPRO = '0' THEN 'Producto' WHEN IPRCLAPRO = '1' THEN 'Servicio' END AS Clase, 
--                      CASE WHEN IPRTIPPRO = '0' THEN 'Ninguno' WHEN IPRTIPPRO = '1' THEN 'Suministro' WHEN IPRTIPPRO = '2' THEN 'Medicamento' END AS Tipo, 
--                      RTRIM(B.IUNUNICOM) AS UCompra, RTRIM(B1.IUNUNICOM) AS UConsumo, RTRIM(A.IPRCONCEN) AS Concentracion
--FROM         DGEMPRES12.dbo.INNPRODUC AS A LEFT OUTER JOIN
--                      DGEMPRES12.dbo.INNUNIDAD AS B ON A.INNUNIDADD = B.OID LEFT OUTER JOIN
--                      DGEMPRES12.dbo.INNUNIDAD AS B1 ON B.IUNCODIGO = B1.IUNCODIGO