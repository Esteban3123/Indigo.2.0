CREATE PROCEDURE [dbo].[SP_HC_ListarProductosxMezcla]
(
@Paciente varchar(25),
@Ingreso char(20)
)
AS
BEGIN
	SET NOCOUNT ON;

			SELECT 
				A.CODCONCEC,
				RTRIM(A.CODPRODUC) As 'Codigo',
				RTRIM(PRO.DESPRODUC) As 'Medicamento',
				A.CANPROCAL As 'Cantidad'	
			FROM 
				dbo.HCINFCONC AS A with(nolock)
				INNER JOIN dbo.IHLISTPRO AS PRO with(nolock) ON A.CODPRODUC = PRO.CODPRODUC  
			WHERE 
				 A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso
		
			UNION ALL
			SELECT 
				D.IDHCNUTPAREC AS 'CODCONCEC',
				RTRIM(D.CODPRODUC) As 'Codigo',
				RTRIM(PRO.DESPRODUC) As 'Medicamento',
				D.CANTMED As 'Cantidad'	
			FROM HCNUTPAREND d
			INNER JOIN HCNUTPAREC AS c ON d.IDHCNUTPAREC = c.ID
			INNER JOIN dbo.IHLISTPRO AS PRO with(nolock) ON D.CODPRODUC = PRO.CODPRODUC  
			WHERE c.IPCODPACI = @Paciente AND C.NUMINGRES =  @Ingreso
			UNION ALL 
			
			SELECT 
				A.CODCONCEC,
				RTRIM(A.CODPRODUC) As 'Codigo',
				RTRIM(PRO.DESPRODUC) As 'Medicamento',
				A.CANPROCAL As 'Cantidad'	
			FROM 
				dbo.HCINFLIQD AS A with(nolock)
				INNER JOIN dbo.IHLISTPRO AS PRO with(nolock) ON A.CODPRODUC = PRO.CODPRODUC  
			WHERE 
				 A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso

END