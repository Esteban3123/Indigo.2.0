

CREATE VIEW [dbo].[INVProductosRelacionInterfazNet]
AS
 Select 1 as Nombre

--SELECT     A.IPRCODIGO AS Codigo, RTRIM(A.IPRDESCOR) AS Descripcion, C.CODPRODUC AS CodigoHC, C.DESPRODUC AS DescripcionHC, 
--                      CASE WHEN IPRCLAPRO = '0' THEN 'Producto' WHEN IPRCLAPRO = '1' THEN 'Servicio' END AS Clase, 
--                      CASE WHEN IPRTIPPRO = '0' THEN 'Ninguno' WHEN IPRTIPPRO = '1' THEN 'Suministro' WHEN IPRTIPPRO = '2' THEN 'Medicamento' END AS Tipo
--FROM         DGEMPRES12.dbo.INNPRODUC AS A INNER JOIN
--                      dbo.IHRINDDGH AS B ON A.IPRCODIGO = B.IPRCODIGO INNER JOIN
--                      dbo.IHLISTPRO AS C ON B.CODPRODUC = C.CODPRODUC