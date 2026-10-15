-- =============================================
-- Author:      Hector Rodriguez Rubiano
-- Create Date: 27/01/2021
-- Description: Centros de atención autorizados por usuario o grupo
-- =============================================
CREATE PROCEDURE [dbo].[SP_SEG_CentroAtencion_Autorizado]
(
    -- Add the parameters for the stored procedure here
    @Usuario CHAR(20),
    @Grupo CHAR(3)
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- Insert statements for procedure here
	SELECT DISTINCT
		RTRIM(A.codcenate) AS Codigo
	   ,RTRIM(A.nomcenate) AS CentroAtencion
	   ,RTRIM(A.codcenate) + ' - ' + RTRIM(A.nomcenate) AS CodigoDescripcion
	FROM dbo.ADcenaten A
	INNER JOIN (SELECT
			PKVALORCO
		   ,CAST(1 AS BIT) AS Valor
		   ,'AutUsuario' AS Permiso
		FROM dbo.INAUTORIU
		WHERE PKTIPOCON = '1'
		AND CODUSUARI = @Usuario
		UNION SELECT
			PKVALORCO
		   ,CAST(1 AS BIT) AS Valor
		   ,'AutGrupo' AS Permiso
		FROM dbo.INAUTORIG
		WHERE PKTIPOCON = '1'
		AND CODGRUPOU = @Grupo) AS B
		ON A.codcenate = B.PKVALORCO
END