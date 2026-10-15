
CREATE PROCEDURE [dbo].[SPHC_ListarHCInterconsultasPacientesSinVisado]
(
@CentroAtencion char(10),
@UnidadFuncional char(10),
@CodigoPaciente varchar(25),
@NumeroIngreso char(15)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
			CASE WHEN B.IDETIPHIS IS NULL THEN 'Historia' ELSE 'Interconsulta' END AS TipoServicio, 
			TIPHISPAC AS TipoReporte, 	 a.NUMEFOLIO AS Folio ,
			A.NUMINGRES AS Ingreso,A.IPCODPACI AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente,A.FECHISPAC AS FechaEvolucion,
	   RTRIM(M.NOMMEDICO) AS Medico,N.DESESPECI AS Especialidad

	FROM HCHISPACA AS A 
		INNER JOIN INPACIENT AS C ON A.IPCODPACI = C.IPCODPACI 
		INNER JOIN INPROFSAL AS M ON A.CODPROSAL = M.CODPROSAL  
		INNER JOIN INESPECIA AS N ON M.CODESPEC1 = N.CODESPECI
		LEFT OUTER JOIN ADINGRESO AS I ON A.NUMINGRES = I.NUMINGRES AND A.UFUCODIGO = I.UFUAACTMED 
		LEFT OUTER JOIN HCORDINTE AS B ON A.IPCODPACI =B.IPCODPACI AND A.NUMEFOLIO =B.NUMFOLINT AND B.ESTSERIPS ='3'
	WHERE  
		A.CODCENATE =@CentroAtencion AND A.UFUCODIGO =@UnidadFuncional  AND A.IPCODPACI =@CodigoPaciente AND A.NUMINGRES =@NumeroIngreso AND A.CODUSUARI IS NULL
	ORDER BY
		A.FECHISPAC DESC

		--AND A.UFUCODIGO =@UnidadFuncional
		

 END