CREATE VIEW [dbo].[HCVistaMedicamentosDenominacionComunLiquidos]
AS
SELECT DISTINCT A.CODDCIMED AS Codigo, RTRIM(A.DESDCIMED) AS Medicamento, RTRIM(A.CODDCIMED) + ' - ' + RTRIM(A.DESDCIMED) AS CodigoDescripcion
FROM            dbo.IHDCIMEDI AS A INNER JOIN
                         dbo.IHLISTPRO AS B ON A.CODDCIMED = B.CODDCIMED AND B.ESPDILPRO = 1 AND B.TIPPRODUC IN ('1', '3')