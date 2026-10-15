

CREATE VIEW [dbo].[INVProductosCodigosBarraInterfazNet]
AS
Select 1 as Nombre
--SELECT     A.IPRCODIGO AS Codigo, RTRIM(A.IPRDESCOR) AS Descripcion, B.CODBARRAS AS CodigoBarras, B.FECHCREAC AS FechaCreacion, 
--                      CASE WHEN IPRTIPPRO = '0' THEN 'Ninguno' WHEN IPRTIPPRO = '1' THEN 'Suministro' WHEN IPRTIPPRO = '2' THEN 'Medicamento' END AS Tipo
--FROM         DGEMPRES12.dbo.INNPRODUC AS A INNER JOIN
--                      dbo.IHCODBARR AS B ON A.IPRCODIGO = B.IPRCODIGO