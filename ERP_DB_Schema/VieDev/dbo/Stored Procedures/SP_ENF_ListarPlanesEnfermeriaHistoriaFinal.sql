CREATE PROCEDURE [dbo].[SP_ENF_ListarPlanesEnfermeriaHistoriaFinal](
@Ingreso char(10)
,@Paciente varchar(20)
)
AS
BEGIN	
	SET NOCOUNT ON;

-- Primera consulta - trae todos los planes de enfermeria activos al paciente que se refleja en la HISTORIA INICIAL enfermeria
-- se valida que no esten dentro de HCCTRACTP para mostrarlos en la Historia final
	 SELECT 
		DISTINCT
			M.ID AS IdControl,
			P.IDPLANVAL AS IdPlan,
			M.FECHACREA AS 'Fecha Registro',
			K.NOMUSUARI   AS Creador,
			O.IDINTACT,
			M.CODPROSAL,
			0 AS Tipo,
			rtrim(A.CODPLANCENF) as Codigo, 
			Rtrim(A.NOMPLANCENF) as Nombre,
			RTRIM(C.NOMBDIAGENF) AS Diagnostico,
			RTRIM(J.NOMINTPCE	) AS Intervencion,
			RTRIM(ltrim(REPLACE(L.NOMACTPCE, CHAR(9), ''))) AS Actividades,
			'' AS Descripcion,
			'' AS Fecha,
			CAST(0 As bit) As Realizada,  
			CAST(0 As bit) As NoRealizada,  
			CAST(0 As bit) As NoAplicaTurno,
			CAST('' AS varchar(25)) AS POSICION
		FROM .dbo.HCPCECONTROL M
			INNER JOIN .dbo.HCPLANCUIENF A ON M.CODPLANCENF = A.CODPLANCENF
			INNER JOIN .dbo.HCPCEPLANVAL P ON M.ID = P.IDPCECONTROL
			INNER JOIN .dbo.HCPCEVALDIAG N ON N.IDPLANVAL = P.IDPLANVAL AND M.ID = N.IDHCPCECONTROL 
			INNER JOIN .dbo.HCVALDIAGPCE B ON M.CODPLANCENF = B.CODPLANCENF
			INNER JOIN .dbo.HCDIAGENF C On N.CODDIAGENF = C.CODDIAGENF 	
			INNER JOIN .dbo.HCPCEVALOBJ G ON M.ID = G.IDHCPCECONTROL
			INNER JOIN .dbo.HCOBJPCE H ON G.CODOBJPCE = H.CODOBJPCE
			INNER JOIN .dbo.HCPCEDIAGINT I ON N.IDVALDIAG = I.IDVALDIAG AND M.ID = I.IDHCPCECONTROL 
			INNER JOIN .dbo.HCINTERVENPCE J on I.CODINTPCE = J.CODINTPCE
			INNER JOIN .dbo.HCPCEINTACT O ON O.IDDIAGINT = I.IDDIAGINT AND M.ID = O.IDHCPCECONTROL 
			INNER JOIN .dbo.HCACTIVIPCE L ON L.CODACTPCE = O.CODACTPCE
			INNER JOIN .dbo.SEGusuaru K with (nolock) ON M.CODUSUCREA = K.CODUSUARI 
		WHERE M.NUMINGRES = @Ingreso AND M.ESTADO IN(1) 
					AND M.ID NOT IN (SELECT distinct IdHCPCECONTROL FROM .dbo.HCCTRACTP  WHERE NUMINGRES = @Ingreso AND IdHCPCECONTROL is not null)
	
UNION ALL

-- Segunda consulta, se traen el plan completo del paciente para la HISTORIA FINAL enfermeria
-- pero con las actividades guardados en HCCTRACTP que se guardan cuando se realiza la historia inicial
	SELECT 
		DISTINCT
			M.ID AS IdControl,
			P.IDPLANVAL AS IdPlan,
			T.FECREGIST as 'Fecha Registro' ,
			K.NOMMEDICO   AS Creador,
			T.IDINTACT,
			M.CODPROSAL,
			0 AS Tipo,
			rtrim(A.CODPLANCENF) as Codigo, 
			Rtrim(A.NOMPLANCENF) as Nombre,
			RTRIM(C.NOMBDIAGENF) AS Diagnostico,
			RTRIM(J.NOMINTPCE	) AS Intervencion,
			RTRIM(ltrim(REPLACE(T.ASUACTPEN, CHAR(9), ''))) AS Actividades,
			Rtrim(T.COMGENACT) As Descripcion,

			Case  WHEN T.IDINTACT Is NULL THEN  FECREGIST  
			Else( Select AA.FECHACREA 
					From .dbo.HCPCECONTROL AA with (nolock)   
						INNER Join .dbo.HCPCEPLANVAL BB with (nolock) ON AA.ID = BB.IDPCECONTROL  
						INNER Join .dbo.HCPCEVALDIAG CC with (nolock) ON CC.IDPLANVAL = BB.IDPLANVAL   
						INNER Join .dbo.HCPCEDIAGINT DD with (nolock) ON DD.IDVALDIAG = CC.IDVALDIAG   
						INNER Join .dbo.HCPCEINTACT EE with (nolock) ON EE.IDDIAGINT = DD.IDDIAGINT  
					WHERE EE.IDINTACT = T.IDINTACT) End AS 'Fecha',  

			CASE T.ESTACTACT WHEN 3 THEN 'True' ELSE 'False' END AS 'Realizada',
			CASE T.ESTACTACT WHEN 7 THEN 'True' ELSE 'False' END AS 'NoRealizada',
			CASE T.ESTACTACT WHEN 8 THEN 'True' ELSE 'False' END AS 'NoAplicaTurno',
			CAST('' AS varchar(25)) AS POSICION
	 FROM .dbo.HCCTRACTP T 
			INNER JOIN .dbo.HCPCECONTROL M ON T.IdHCPCECONTROL = M.ID
			INNER JOIN .dbo.HCPLANCUIENF A ON M.CODPLANCENF = A.CODPLANCENF
			INNER JOIN .dbo.HCPCEPLANVAL P ON M.ID = P.IDPCECONTROL
			INNER JOIN .dbo.HCPCEVALDIAG N ON N.IDPLANVAL = P.IDPLANVAL AND M.ID = N.IDHCPCECONTROL 
			INNER JOIN .dbo.HCDIAGENF C On N.CODDIAGENF = C.CODDIAGENF 	
			INNER JOIN .dbo.HCPCEDIAGINT I ON N.IDVALDIAG = I.IDVALDIAG AND M.ID = I.IDHCPCECONTROL 
			INNER JOIN .dbo.HCINTERVENPCE J on I.CODINTPCE = J.CODINTPCE
			INNER JOIN .dbo.HCPCEINTACT O ON O.IDDIAGINT = I.IDDIAGINT AND M.ID = O.IDHCPCECONTROL AND T.IDINTACT = O.IDINTACT
			INNER JOIN .dbo.INPROFSAL As K With (nolock) On T.CODPROSAL = K.CODPROSAL  
	Where  T.NUMINGRES = @Ingreso AND M.ESTADO = 1 And T.Identifier = (SELECT top 1 Identifier From .dbo.HCCTRACTP with (nolock) Where NUMINGRES =@Ingreso ORDER BY FECREGIST DESC)

UNION ALL

-- Tercera consulta, se traen las actividades agregadas sin padre desde la HISTORIA INICIAL, solo se registra la actividad con su IDINTACT null
	SELECT 
			0 AS IdControl,
			0  AS IdPlan,
			A.FECREGIST AS 'Fecha Registro',
			RTRIM(K.NOMMEDICO) AS Creador,
			A.IDINTACT,
			K.CODPROSAL,
			'' AS Tipo,
			'No aplica' AS Codigo,
			'Actividades adicionales' AS Nombre,
			'No aplica' AS Diagnostico,
			'No aplica' AS Intervencion,
			RTRIM(ltrim(REPLACE(A.ASUACTPEN, CHAR(9), ''))) AS Actividades,
			RTRIM(A.COMGENACT) AS Descripcion,
			''  AS Fecha,
			CASE A.ESTACTACT WHEN 3 THEN 'True' ELSE 'False' END AS 'Realizada',
			CASE A.ESTACTACT WHEN 7 THEN 'True' ELSE 'False' END AS 'NoRealizada',
			CASE A.ESTACTACT WHEN 8 THEN 'True' ELSE 'False' END AS 'NoAplicaTurno',
			CAST('' AS varchar(25)) AS POSICION
	FROM .dbo.HCCTRACTP  A
	INNER Join .dbo.INPROFSAL As K With (nolock) On A.CODPROSAL = K.CODPROSAL 
	WHERE NUMINGRES =@Ingreso AND IDINTACT IS NULL AND A.ESTACTACT IN (1,7,8)
	AND A.Identifier = (SELECT top 1 Identifier From .dbo.HCCTRACTP with (nolock) Where NUMINGRES = @Ingreso ORDER BY FECREGIST DESC)

	ORDER BY Intervencion ASC

END