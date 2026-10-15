--Obtiene el dato de las ambulancias a las que el usuario esta autorizado
CREATE procedure [dbo].[SP_ConsultarAmbulanciasPorUsuario]
@userCode char(15)
as
SELECT		a.Id as idAmbulance,
			a.Codigo as codeAmbulance,
			a.Nombre as description,
			case when a.TipoAmbulancia = 2 then '1' else '0' end as isTAM,
			a.Placa as licensePlate
FROM		SEGusuaru AS U 
INNER JOIN	INPROFSAL AS P ON U.CODUSUARI = P.CODPROSAL
INNER JOIN	INAUTORIU AS AU ON AU.CODUSUARI = P.CODUSUARI
INNER JOIN	RCVEHICTRAS AS A ON A.Id = AU.PKVALORCO
WHERE		u.CODUSUARI = @userCode AND AU.PKTIPOCON = '11'