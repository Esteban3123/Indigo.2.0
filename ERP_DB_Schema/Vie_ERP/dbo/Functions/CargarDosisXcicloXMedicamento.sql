CREATE FUNCTION [dbo].[CargarDosisXcicloXMedicamento]	
(
 @TIPO INT, --1-Dosis; 2-Insumos
 @IDHCORDQUIMIO INT,
 @CICLO   INT,
 @codigomedicamento varchar(100),
 @Dia as varchar 
 
 )
RETURNS varchar(1000)
AS
BEGIN
	  DECLARE @valores VARCHAR(1000)

IF  @TIPO = 1 --Medicamentos

	BEGIN  
		 SELECT @valores= COALESCE(@valores + ' / ', '') +  Rtrim(administracion)
			  FROM (
				 SELECT 'Dosis: ' + Rtrim(convert(numeric(18,2), m.DOSISPROD)) +' '+ Rtrim(LTRIM(F.ABRUNIMED)) + ' de forma '+ Rtrim(LTRIM(D.DESVIAADM)) + ' Día(s):' + /*Rtrim(H.DESCRIPCIONDIA)*/  + STUFF(( SELECT ', ' + CONVERT(VARCHAR(20),DIA) FROM ehr.HCORDMEDICAM WHERE IDHCORDQUIMIO = @IDHCORDQUIMIO  and CICLO = @CICLO  AND CODPRODUC = @codigomedicamento and DOSISPROD = m.DOSISPROD  FOR XML PATH('')),1,2, '')  as 'Administracion' 
				  from ehr.HCORDMEDICAM M  With(Nolock)
 						  INNER JOIN ehr.HCORMEDICAMESQUEMA H With(Nolock) on M.CODPRODUC = H.CODPRODUC AND M.CICLO = H.CICLO AND H.IDHCORDQUIMIO = @IDHCORDQUIMIO AND H.CICLO = @CICLO
						  INNER JOIN dbo.HCVIAADMI D With(Nolock) ON m.CODVIAADM = D.CODVIAADM																 
						  INNER JOIN dbo.INUNIMEDI F With(Nolock) ON m.CODUNIMED = F.CODUNIMED	
				 WHERE M.IDHCORDQUIMIO = @IDHCORDQUIMIO and M.CICLO = @CICLO AND M.CODPRODUC = @codigomedicamento/* AND ID = @IDHCORDMEDICAM*/ GROUP BY f.ABRUNIMED,d.DESVIAADM,M.DOSISPROD,H.DESCRIPCIONDIA
			 ) AS administracion

	END
ELSE IF @TIPO = 2 --Insumos
	BEGIN
	
			SELECT @valores =(SELECT ' Día(s):' + STUFF(( SELECT DISTINCT ', ' + CONVERT(VARCHAR(20),DIA) FROM ehr.HCORDINSUMOS With(Nolock) WHERE IDHCORDQUIMIO = @IDHCORDQUIMIO  and CICLO = @CICLO  AND CODIGOINSUMO = @codigomedicamento FOR XML PATH('')),1,2, '')  as 'Dias' )
				
	END

ELSE IF @TIPO = 4 --Confirmar Quimioterapia Enfermeria
	BEGIN
	
		SELECT @valores= COALESCE(@valores + ' / ', 'Administrar por una sola vez ' ) +  Rtrim(administracion)  
			  FROM (
				 SELECT case TypePrescription 
					 when 1 then  Rtrim(convert(numeric(18,2), m.DOSISPROD)) +' '+ Rtrim(LTRIM(F.ABRUNIMED)) + ' de forma '+ Rtrim(LTRIM(D.DESVIAADM))
					 when 2 then  Rtrim(convert(numeric(18,2), m.DOSISPROD)) +' '+ Rtrim(LTRIM(F.ABRUNIMED)) + ' a las '+  format(m.HORAFRECUDIA,'HH:mm')
				   end as 'Administracion' 
				from ehr.HCORDMEDICAM M  With(Nolock)
 						  INNER JOIN ehr.HCORMEDICAMESQUEMA H With(Nolock) on M.CODPRODUC = H.CODPRODUC AND M.CICLO = H.CICLO AND H.IDHCORDQUIMIO = @IDHCORDQUIMIO AND H.CICLO = @CICLO 
						  INNER JOIN dbo.HCVIAADMI D With(Nolock) ON m.CODVIAADM = D.CODVIAADM																 
						  INNER JOIN dbo.INUNIMEDI F With(Nolock) ON m.CODUNIMED = F.CODUNIMED	
				 WHERE M.IDHCORDQUIMIO = @IDHCORDQUIMIO and M.CICLO = @CICLO AND M.DIA = @Dia AND M.CODPRODUC = @codigomedicamento 
				 GROUP BY f.ABRUNIMED,d.DESVIAADM,M.DOSISPROD,H.DESCRIPCIONDIA,m.HORAFRECUDIA ,TypePrescription
			 ) AS administracion 	
	END


ELSE IF @TIPO = 3 --Diluyentes

			SELECT @valores =(SELECT ' Día(s):' + STUFF(( SELECT DISTINCT ', ' + CONVERT(VARCHAR(20),DIA) FROM ehr.HCORDMEDICAM With(Nolock) WHERE IDHCORDQUIMIO = @IDHCORDQUIMIO  and CICLO = @CICLO  AND CODDILUYENTE = @codigomedicamento FOR XML PATH('')),1,2, '')  as 'Dias' )


 return @valores

END