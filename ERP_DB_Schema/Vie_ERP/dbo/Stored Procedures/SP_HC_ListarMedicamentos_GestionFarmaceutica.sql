


CREATE PROCEDURE [dbo].[SP_HC_ListarMedicamentos_GestionFarmaceutica] 
(
@Paciente Varchar(25),
@Ingreso  Char(10)
)
AS
BEGIN

	SET NOCOUNT ON;
	-------------------

	DECLARE @MaxFolio_OtrosMedicamentos nchar(10)
	SELECT @MaxFolio_OtrosMedicamentos = ISNULL(MAX(NUMEFOLIO),'0')  FROM HCNOSERFOTROMED AS A WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso ;
			   	
			SELECT
				A.ID,
				RTRIM(B.CODDCIMED) AS 'Codigo DCI',
				A.CODPROSAL,
				RTRIM(C.NOMMEDICO) AS 'Profesional',
				RTRIM(D.DESESPECI) AS 'Especialidad',
				A.CODPRODUC,
				RTRIM(B.DESPRODUC) AS 'Medicamento',
				RTRIM(A.ADMINISTRACION) AS 'Administracion',
				A.CANTIDAD AS 'Cantidad',
				A.PREESTADO AS 'Estado',
				A.OBSERVACIONES AS 'Observaciones',
				A.FECHAORDE AS 'Fecha',
				A.Justification,
				A.RejectedUser,
				A.RejectionDate			
			FROM 
				dbo.HCNOSERFOTROMED As A with(nolock) 
				INNER JOIN dbo.IHLISTPRO As B with(nolock) ON A.CODPRODUC = B.CODPRODUC 				
				INNER JOIN dbo.INPROFSAL AS C with(nolock) ON A.CODPROSAL = C.CODPROSAL
				INNER JOIN dbo.INESPECIA AS D with(nolock) ON C.CODESPEC1 = D.CODESPECI
		   WHERE 
				 A.IPCODPACI = @Paciente
				 AND A.NUMINGRES = @Ingreso
				 AND A.PREESTADO = 1
				 AND A.NUMEFOLIO = @MaxFolio_OtrosMedicamentos
				 				 				
END