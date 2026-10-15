CREATE VIEW [dbo].[HCVistaMedicamentosDenominacionComunMezclas]
AS
SELECT DISTINCT A.CODDCIMED AS Codigo, RTRIM(A.DESDCIMED) AS Medicamento, RTRIM(A.CODDCIMED) + ' - ' + RTRIM(A.DESDCIMED) AS CodigoDescripcion
FROM            dbo.IHDCIMEDI AS A INNER JOIN
                         dbo.IHLISTPRO AS B ON A.CODDCIMED = B.CODDCIMED AND B.TIPFORMED IN ('1', '2', '3') AND B.TIPPRODUC IN ('1', '3')