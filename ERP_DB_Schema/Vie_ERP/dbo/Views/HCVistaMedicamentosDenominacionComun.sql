CREATE VIEW [dbo].[HCVistaMedicamentosDenominacionComun]
AS
/*SELECT        Codigo, Medicamento, CodigoDescripcion, ROW_NUMBER() OVER (ORDER BY base.Codigo) AS NumeroFila
FROM            (SELECT DISTINCT A.CODDCIMED AS Codigo, RTRIM(A.DESDCIMED) AS Medicamento, RTRIM(A.CODDCIMED) + ' - ' + RTRIM(A.DESDCIMED) AS CodigoDescripcion
                          FROM            dbo.IHDCIMEDI AS A INNER JOIN
                                                    dbo.IHLISTPRO AS B ON A.CODDCIMED = B.CODDCIMED AND B.ESPDILPRO = 1 OR B.TIPPRODUC IN ('1', '3')) Base
													*/

SELECT DISTINCT A.CODDCIMED AS Codigo, RTRIM(A.DESDCIMED) AS Medicamento, RTRIM(A.CODDCIMED) + ' - ' + RTRIM(A.DESDCIMED) AS CodigoDescripcion
FROM  dbo.IHDCIMEDI AS A 
	INNER JOIN Inventory.DCI d on a.CODDCIMED = d.DCICrystal and d.Status = 1
	INNER JOIN dbo.IHLISTPRO AS B ON A.CODDCIMED = B.CODDCIMED AND B.ESPDILPRO = 1 OR B.TIPPRODUC IN ('1', '3') AND PROESTADO=1