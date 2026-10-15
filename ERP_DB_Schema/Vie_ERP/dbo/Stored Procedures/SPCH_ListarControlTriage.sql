

CREATE PROCEDURE [dbo].[SPCH_ListarControlTriage]
(
@CentroAtencion Char(10), 
@UnidadFuncional Char(10), 
@Usuario char(20) 
)
AS
BEGIN
	SET NOCOUNT ON;

	--necesitamos saber si la unidad que esta haciendo la consulta es de atención prioritaria o urgencias
declare @AtencionPrioritaria bit = (select case UFUTIPUNI when 31 then 1 else 0 end from INUNIFUNC where UFUCODIGO = @UnidadFuncional)

if @AtencionPrioritaria = 1 begin
	SELECT A.CODTIPPAC as Tipo, A.NUMINGRES, CAST(A.IFECHAING AS DATETIME) as 'Fecha Hora Llegada',RTRIM(A.IPCODPACI) as Identificacion,  RTRIM(C.IPNOMCOMP) as 'Nombre Paciente',
	 RTRIM(B.NOMENTIDA) as Entidad,0 AS [Minutos],0 AS 'Barra','' AS TipoEntidad,'' AS Consecutivo,CAST(0 AS BIT) AS Ausencia,IPFECNACI AS 'Fecha Nacimiento' ,
	A.PRIMERLLA AS LlamadoUno,A.SEGUNDLLA AS LlamadoDos,A.TERCERLLA AS LlamadoTres
	, CASE WHEN C.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
	, C.ZONAPARTADA,
	D.RIESGOAGRE,
	(SELECT count(*) FROM dbo.ADPOBESPEPAC Z with(nolock) INNER JOIN ADPOBESPE X with(nolock) ON X.ID = Z.IDADPOBESPE  WHERE IPCODPACI = C.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
	CONVERT(BIT,0) AS Riesgo,
	CAST('' AS CHAR(50)) AS Edad
	FROM dbo.ADINGRESO A with(nolock)
	INNER JOIN dbo.INEntidad B with(nolock) ON A.CODENTIDA=b.Codentida 
	LEFT OUTER JOIN dbo.INPacient C with(nolock) ON A.IPCODPACI=C.IPCODPACI 
	LEFT OUTER JOIN dbo.ADACTIVID D with(nolock) ON C.CODACTIVI = D.codactivi
	left outer join dbo.ADTRIAGEU T with(nolock) on T.NUMINGRES = A.NUMINGRES
	WHERE A.CODCENATE = @CentroAtencion AND A.UFUCODIGO = @UnidadFuncional AND A.REQTRIAGE = 1 AND A.IESTADOIN = ' ' AND T.TRIANUMER IS NULL
end
else begin
	SELECT  A.CODTIPPAC as Tipo, CAST(A.IPFECLLEGA AS DATETIME) as 'Fecha Hora Llegada',RTRIM(A.IPCODPACI) as Identificacion, ISNULL(Rtrim(C.IPNOMCOMP),Rtrim(A.IPNOMCOMP)) as 'Nombre Paciente', RTRIM(B.NOMENTIDA) as Entidad,0 AS [Minutos],0 AS 'Barra','' AS TipoEntidad,A.CODCONCEC AS Consecutivo,CAST(0 AS BIT) AS Ausencia,IPFECNACI AS 'Fecha Nacimiento' ,
	A.PRIMERLLA AS LlamadoUno,A.SEGUNDLLA AS LlamadoDos,A.TERCERLLA AS LlamadoTres
	, CASE WHEN C.IPTIPODOC IN(6,7) THEN 1 ELSE 0 END AS ASMS
	, C.ZONAPARTADA,
	D.RIESGOAGRE,
	(SELECT count(*) FROM dbo.ADPOBESPEPAC Z with(nolock) INNER JOIN ADPOBESPE X with(nolock) ON X.ID = Z.IDADPOBESPE  WHERE IPCODPACI = C.IPCODPACI AND TIPOPOESPERIES = 1) AS POBESPECIAL,
	CONVERT(BIT,0) AS Riesgo,
	CAST('' AS CHAR(50)) AS Edad
	FROM dbo.ADCONTURG A with(nolock)
	INNER JOIN dbo.INEntidad B with(nolock) ON A.CODENTIDA=b.Codentida 
	LEFT OUTER JOIN dbo.INPacient C with(nolock) ON A.IPCODPACI=C.IPCODPACI 
	LEFT OUTER JOIN dbo.ADACTIVID D with(nolock) ON C.CODACTIVI = D.codactivi
	WHERE CONESTADO = '1' AND CODCENATE = @CentroAtencion AND UFUCODIGO = @UnidadFuncional AND A.CODCONCEC NOT IN 
	(SELECT NUMDOCUME FROM dbo.INDOCUMEN with(nolock) WHERE CODDOCUME='1' AND CODUSUARI<>@Usuario) 
end


end