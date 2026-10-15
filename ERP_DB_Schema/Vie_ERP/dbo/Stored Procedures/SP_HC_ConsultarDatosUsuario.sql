
CREATE PROC [dbo].[SP_HC_ConsultarDatosUsuario]
@userCode char(15)
AS
SELECT		rtrim(U.CODUSUARI) AS Code,
			rtrim(U.NOMUSUARI) as Name,
			U.USUADMINI AS IsAdministrator,
			p.TIPPROFES AS Profession
FROM		SEGusuaru AS U 
INNER JOIN	INPROFSAL AS P ON U.CODUSUARI = P.CODPROSAL
WHERE		u.CODUSUARI = @userCode