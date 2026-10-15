-- =============================================
-- Author:		Yezid Garcia Medina
-- Create date: 10 Mayo 2021
-- Description:	Listar Interacciones - Nota Servicio Farmaceutico
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarInteracciones_NotaServicioFarmaceutico] 
(
@Paciente Varchar(25),
@Ingreso  Char(10),
@ProductoA  Char(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	-------------------		
	
	declare @MaxFolio_OtrosMedicamentos nchar(10)

	SELECT @MaxFolio_OtrosMedicamentos = ISNULL(MAX(NUMEFOLIO),'0')  FROM HCNOSERFOTROMED AS A WHERE A.IPCODPACI = @Paciente AND A.NUMINGRES = @Ingreso  ;

	
	with Tabla
	as
	(	SELECT
				
				RTRIM(A.CODPRODUC) As 'Codigo', 
				RTRIM(E.DESPRODUC) As 'Medicamento',
                RTRIM(CODDCIMED) AS 'CODDCIMED'

			FROM
				dbo.HCPRESCRA As A with(nolock)
				INNER JOIN dbo.IHLISTPRO As E with(nolock) ON A.CODPRODUC=E.CODPRODUC 
			
			WHERE 
				A.IPCODPACI= @Paciente 
				AND A.NUMINGRES= @Ingreso
				AND A.PREESTADO IN (1,6) 

		UNION ALL

			SELECT 
				RTRIM(pro.CODPRODUC) As 'Codigo',
				RTRIM(pro.DESPRODUC) As 'Medicamento',
                RTRIM(pro.CODDCIMED) AS 'CODDCIMED' 

			FROM 
				dbo.HCINFLIQC as cab with(nolock)
				INNER JOIN dbo.HCINFCONC as med with(nolock) ON cab.CODCONCEC = med.CODCONCEC 
				INNER JOIN dbo.IHLISTPRO as pro with(nolock) ON med.CODPRODUC = pro.CODPRODUC  
				INNER JOIN dbo.HCINFLIQA as admi with(nolock) ON cab.CODCONCEC = admi.CODCONCEC 
				
			WHERE 
				 cab.IPCODPACI = @Paciente
				 AND cab.NUMINGRES = @Ingreso
				 AND admi.PREESTADO IN (1,5)
		
		UNION ALL
			
			SELECT
				RTRIM(pro.CODPRODUC) As 'Codigo',
				RTRIM(pro.DESPRODUC) As 'Medicamento',
                RTRIM(pro.CODDCIMED) AS 'CODDCIMED'

			FROM 
				dbo.HCINFLIQC As cab with(nolock) 
				INNER JOIN dbo.HCINFLIQD As dil with(nolock) ON cab.CODCONCEC = dil.CODCONCEC 
				INNER JOIN dbo.IHLISTPRO As pro with(nolock) ON dil.CODPRODUC = pro.CODPRODUC 
				INNER JOIN dbo.HCINFLIQA As admi with(nolock) ON cab.CODCONCEC = admi.CODCONCEC
           
		   WHERE 
				 cab.IPCODPACI = @Paciente
				 AND cab.NUMINGRES = @Ingreso
				 AND admi.PREESTADO IN (1,5)
				 				 
		UNION ALL
			
			SELECT
				
				RTRIM(B.CODPRODUC) As 'Codigo',
				RTRIM(B.DESPRODUC) As 'Medicamento',
				RTRIM(B.CODDCIMED) AS 'CODDCIMED' 
			FROM 
				dbo.HCNOSERFOTROMED As A with(nolock) 
				INNER JOIN dbo.IHLISTPRO As B with(nolock) ON A.CODPRODUC = B.CODPRODUC 				
				INNER JOIN dbo.INPROFSAL as C with(nolock) ON A.CODPROSAL = C.CODPROSAL
				INNER JOIN dbo.INUNIFUNC as D with(nolock) ON A.UFUCODIGO = D.UFUCODIGO 
				INNER JOIN dbo.ADcenaten as E with(nolock) ON A.CODCENATE = E.CODCENATE
           
		   WHERE 
				 A.IPCODPACI = @Paciente
				 AND A.NUMINGRES = @Ingreso
				 AND A.PREESTADO = 1
				 AND A.NUMEFOLIO = @MaxFolio_OtrosMedicamentos
				 				 				
	 )	
	 SELECT	Distinct		 
			Z.* , 
			CASE WHEN inter.CODPRODUA IS NOT NULL THEN Cast(1 As Bit) ELSE Cast(0 As Bit) END AS 'Interaccion',
            RTRIM(inter.CODPRODUA) As 'Codigo_A' ,
            RTRIM(inter.CODPRODUB) AS 'Codigo_B' ,
			CASE inter.NIVRIESGO
				WHEN '1' THEN 'Grave'
				WHEN '2' THEN 'Moderada'
				WHEN '3' THEN 'Leve' 
			END	AS 'Riesgo',
            RTRIM(inter.OBSERVACI) AS 'Observaciones' ,
            RTRIM(Z.Medicamento) As 'Medicamento_A',
            RTRIM(pro.DESPRODUC) As 'Medicamento_B'
	 FROM Tabla As Z
	 LEFT JOIN dbo.HCINTEMED As inter	
		ON inter.CODPRODUA in (Z.Codigo, Z.CODDCIMED) and inter.CODPRODUB in (select Codigo from Tabla union all select CODDCIMED from Tabla)
     LEFT JOIN dbo.IHLISTPRO As pro  ON inter.CODPRODUB IN ( pro.CODPRODUC , pro.CODDCIMED)
    WHERE  Z.Codigo =@ProductoA 

END