
CREATE PROCEDURE [dbo].[SPMOV_ListarCentrosDeAtencion]
(
	@Usuario nvarchar(50)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  RTRIM(A.codcenate) AS Codigo,
		RTRIM(A.nomcenate) as 'Centro Atencion',
		RTRIM(A.codcenate)+' - '+RTRIM(A.nomcenate) AS CodigoDescripcion
    FROM ADcenaten A
		INNER JOIN (SELECT PKVALORCO FROM INAUTORIU WHERE PKTIPOCON='1' AND CODUSUARI = @Usuario
					UNION 
					SELECT PKVALORCO FROM INAUTORIG WHERE PKTIPOCON='1' AND CODGRUPOU  in (select CODGRUPOU  from [dbo].[SEGusuaru] where CODUSUARI = @Usuario )) AS B ON A.codcenate=B.PKVALORCO
end