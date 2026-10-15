

CREATE PROCEDURE [dbo].[SP_ONCO_ListarEsquemasActivosPaciente]
(
@Paciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;
	 
 
	select Rtrim(C.Description) +'   -    N° de Ciclos: '+ Rtrim(A.CICLOS) as 'Nombre Esquema',Rtrim(D.CODPRODUC) AS 'Codigo Producto', Rtrim(D.DESPRODUC) As 'Nombre Producto', convert(numeric(18,2),B.DOSISPROD) as 'Dosis', Rtrim(E.DESVIAADM) as 'Via Administracion',
	   CASE A.ESTADO  WHEN 1 Then 'Solicitado sin cita' when 2 Then 'Solicitado con cita' When 3 Then 'Esquema Iniciado' End as 'Estado Esquema',
	   Rtrim(F.NOMMEDICO) As 'Nombre Medico',A.NUMINGRES AS 'Ingreso',
	   b.TIPOFACTOR as 'Tipo Factor',B.INSTRUADMINIS as 'Instrucciones',B.CICLO,B.DIA As 'Dia'
	from [EHR].[HCORDQUIMIO] A
			INNER JOIN [EHR].[HCORDMEDICAM] B with(nolock) ON A.ID = B.IDHCORDQUIMIO and B.CICLO = A.CICLOACTUAL 
			INNER JOIN [EHR].Schemes C ON A.SchemesId = C.Id 
			INNER JOIN [dbo].IHLISTPRO D with(nolock) ON D.CODPRODUC = B.CODPRODUC 
			INNER JOIN [dbo].HCVIAADMI E with(nolock) ON E.CODVIAADM = b.CODVIAADM
			INNER JOIN [dbo].INPROFSAL F with(nolock) ON F.CODPROSAL = A.CODPROSAL
	Where A.IPCODPACI = @Paciente AND A.ESTADO IN (1,2,3) order by B.CICLO,B.DIA asc

end