CREATE PROCEDURE [dbo].[SPHC_ListarImagenesPacientesNotificaciones]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


--SELECT AUTO, A.FECORDMED AS FechaSolicitud, C.IPCODPACI AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente, A.CODSERIPS AS CodigoServicio,
--	   RTRIM(B.DESSERIPS) AS DescripcionServicio ,A.SERREAINT AS RealizaInterfaz,
--	   A.NUMEFOLIO AS Folio,A.NUMINGRES AS Ingreso ,RTRIM(D.NOMMEDICO) AS Medico,A.NOMARCIMG AS Archivo,RTRIM(n.DESESPECI) as Especialidad
--	   ,'HCORDIMAG' AS 'Tabla'
--FROM HCORDIMAG  as A with(nolock) INNER JOIN 
--	INCUPSIPS AS B with(nolock) ON A.CODSERIPS =B.CODSERIPS INNER JOIN 
--	INPACIENT AS C with(nolock) ON A.IPCODPACI =C.IPCODPACI INNER JOIN 
--	INPROFSAL AS D with(nolock) ON A.CODPROSAL =D.CODPROSAL 
--	INNER JOIN INESPECIA AS N with(nolock) ON D.CODESPEC1 = N.CODESPECI
--	INNER JOIN ADINGRESO AS I with(nolock) ON A.NUMINGRES=I.NUMINGRES


--WHERE ESTSERIPS='3' AND A.CODCENATE=@CentroAtencion AND I.UFUACTPAC=@UnidadFuncional 


    -- Insert statements for procedure here
SELECT AUTO, A.FECORDMED AS FechaSolicitud, C.IPCODPACI AS Paciente,RTRIM(C.IPNOMCOMP) AS NombrePaciente, A.CODSERIPS AS CodigoServicio,
	   RTRIM(B.DESSERIPS) AS DescripcionServicio ,A.SERREAINT AS RealizaInterfaz,
	   A.NUMEFOLIO AS Folio,A.NUMINGRES AS Ingreso ,RTRIM(D.NOMMEDICO) AS Medico,A.NOMARCIMG AS Archivo,RTRIM(n.DESESPECI) as Especialidad
	   ,'HCORDIMAG' AS 'Tabla'
FROM HCORDIMAG  as A with(nolock) INNER JOIN 
	INCUPSIPS AS B with(nolock) ON A.CODSERIPS =B.CODSERIPS INNER JOIN 
	INPACIENT AS C with(nolock) ON A.IPCODPACI =C.IPCODPACI INNER JOIN 
	INPROFSAL AS D with(nolock) ON A.CODPROSAL =D.CODPROSAL 
	INNER JOIN INESPECIA AS N with(nolock) ON D.CODESPEC1 = N.CODESPECI
	INNER JOIN ADINGRESO AS I with(nolock) ON A.NUMINGRES=I.NUMINGRES AND I.IESTADOIN IN ('', 'P', 'B')


WHERE ESTSERIPS='3' AND A.CODCENATE=@CentroAtencion AND I.UFUACTPAC=@UnidadFuncional 
END