
CREATE PROCEDURE [dbo].[SP_NOTAS_ListarHistoricoNotasAdministrativas]
(
@INDPaciente Varchar(25)

)
AS
BEGIN
	SET NOCOUNT ON;
	
	Select 
		'Notas administrativas' AS Tipo,
		Rtrim(D.CODIGO) AS 'CODIGO', 
		A.ID, 
		IPCODPACI, 
		FECHACREACION, 
		Rtrim(C.NOMCENATE) as 'Centro Atencion', 
		Rtrim(b.NOMUSUARI) as 'Usuario', 
		NUMINGRES, 
		A.IDNOTAADMINISTRATIVA, 
		Rtrim(D.CODIGO) + ' - ' + RTRIM(D.NOMBRE) AS FORMATO,
		Rtrim(D.NOMBRE) AS 'NOMBRE'
	From 
		NTNOTASADMINISTRATIVASC A 
		INNER JOIN SEGusuaru B ON A.CODUSUARI = B.CODUSUARI 
		INNER JOIN ADCENATEN C ON A.CODCENATE = C.CODCENATE 
		INNER JOIN NTADMINISTRATIVAS D ON A.IDNOTAADMINISTRATIVA = D.ID 
	Where 
		A.IPCODPACI = @INDPaciente 
UNION ALL 

		Select 	
	   'Formatos educativos/encuestas' AS Tipo,
		Rtrim(D.Code) AS 'CODIGO', 
		A.ID, 
		A.IPCODPACI, 
		A.DateCreation, 
		Rtrim(C.NOMCENATE) as 'Centro Atencion', 
		Rtrim(b.NOMMEDICO) as 'Usuario', 
		NUMINGRES, 
		A.IdParamEducationFormatsC, 
		Rtrim(D.Code) + ' - ' + RTRIM(D.Description) AS FORMATO,
		Rtrim(D.Description) AS 'NOMBRE' 
	From 
		PatientEducationFormatsC A 
		INNER JOIN INPROFSAL B ON A.CODPROSAL = B.CODPROSAL
		INNER JOIN ADCENATEN C ON A.CODCENATE = C.CODCENATE 
		INNER JOIN ParamEducationFormatsC D ON A.IdParamEducationFormatsC = D.ID 
	Where 
		A.IPCODPACI = @INDPaciente 

end