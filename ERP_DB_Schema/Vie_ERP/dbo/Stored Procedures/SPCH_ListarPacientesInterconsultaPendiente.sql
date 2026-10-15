CREATE PROCEDURE [dbo].[SPCH_ListarPacientesInterconsultaPendiente]
(
@CentroAtencion Char(10),
@UnidadFuncional Char(10),
@Especialidad1 char(3),
@Especialidad2 char(3),
@Especialidad3 char(3)
)
AS
BEGIN
	SET NOCOUNT ON;

SELECT DISTINCT  RTRIM(E.DESESPECI) AS DescripcionEspecialidad,RTRIM(D.UFUDESCRI) AS UFUDESCRI,AD.UFUAACTHOS AS UFUCODIGO,A.IPCODPACI AS Identificacion,A.NUMINGRES AS Ingreso, RTRIM(IPNOMCOMP) AS Paciente,RTRIM(ca.NUMCAMHOS) + ' - ' + RTRIM(DESCCAMAS) AS Cama,dbo.ClaseHabitacion(CA.CODCLAHAB) AS ClaseHabitacion,dbo.ClaseCama(CA.CODCLACAM) AS 'Clase de Cama',dbo.TipoAislamiento(CA.CODAISLAM) AS Aislamiento,CAST(0 AS BIT) AS Resultado,A.NUMEFOLIO,A.CODSERIPS,CAST('' as bit) AS MuestraAlerta,'Normal' as Alerta, 
AD.ESCADOWNT , AD.ESCARASS, AD.ESCVASPAC, AD.ESCAPAPAC, AD.ESCNORPAC, dbo.PuntajeEscalaDownTon(A.NUMINGRES, A.IPCODPACI) as PUNTAJEDOWN, dbo.PuntajeEscalaRass(A.NUMINGRES, A.IPCODPACI) as PUNTAJERASS, dbo.PuntajeEscalaVas(A.NUMINGRES, A.IPCODPACI) as PUNTAJEVAS, dbo.PuntajeEscalaApache(A.NUMINGRES, A.IPCODPACI) as PUNTAJEAPACHE, dbo.PuntajeEscalaNorton(A.NUMINGRES, A.IPCODPACI) as PUNTAJENORTON, iif((select COUNT(*) from dbo.RecommendPatient where IPCODPACI = A.IPCODPACI and NUMINGRES = A.NUMINGRES and Status = 1) > 0, Convert(Bit,1), Convert(Bit,0)) as Recomendacion
FROM dbo.HCORDINTE  A with(nolock) INNER JOIN 
dbo.ADINGRESO AD with(nolock) ON A.NUMINGRES = AD.NUMINGRES INNER JOIN 
dbo.INUNIFUNC D with(nolock) ON AD.UFUAACTHOS=D.UFUCODIGO INNER JOIN
dbo.INPacient B with(nolock) ON A.IPCODPACI=B.IPCODPACI INNER JOIN 
dbo.INCUPSIPS J with(nolock) ON A.CODSERIPS=J.CODSERIPS LEFT OUTER JOIN 
dbo.CHCAMASHO CA with(nolock) ON AD.CODCAMACT=CA.CODICAMAS LEFT OUTER JOIN
DBO.INESPECIA E with(nolock) ON AD.CODESPTRA=E.CODESPECI
WHERE A.CODCENATE=@CentroAtencion  AND ESTSERIPS='1' AND A.CODESPECI IN (@Especialidad1,@Especialidad2,@Especialidad3)

end