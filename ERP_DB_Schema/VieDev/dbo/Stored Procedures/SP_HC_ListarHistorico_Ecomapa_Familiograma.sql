
CREATE  PROCEDURE [dbo].[SP_HC_ListarHistorico_Ecomapa_Familiograma]
(
@TipoDiagrama Integer,
@Paciente varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;

Select A.ID,A.NUMINGRES as 'Ingreso', A.IPCODPACI AS 'Paciente',Rtrim(B.IPNOMCOMP) As 'Nombre Paciente',A.FECHACREACION as 'Fecha Creacion', A.FECHAMODIFICO As 'Fecha Modificacion',Rtrim(A.USUARIOCREACION) + '-' + Rtrim(C.NOMUSUARI) AS 'Usuario Creacion',
	   Rtrim(A.USUARIOMODIFICO) + '-' + Rtrim(D.NOMUSUARI) AS 'Usuario Modificacion',DIAGRAMA
From HCDIAGRAMAS A 
		INNER JOIN INPACIENT B ON A.IPCODPACI = B.IPCODPACI
		INNER JOIN SEGusuaru C ON A.USUARIOCREACION = C.CODUSUARI
		LEFT JOIN SEGusuaru D ON A.USUARIOMODIFICO = D.CODUSUARI
Where A. IPCODPACI = @Paciente AND A.TIPODIAGRAMA = @TipoDiagrama


End