-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [EHR].[CargarAdministracion]
(

 @TIPO INT, --1-Dosis; 2-Insumos
 @NUMEFOLIO VARCHAR(10),
 @IDESQUEMA VARCHAR(20),
 @codigomedicamento varchar(100),
 @CICLO INT
 --,
 --@IDHCORDMEDICAM INT
 )
RETURNS varchar(1000)
AS
BEGIN
	  DECLARE @valores VARCHAR(1000)

IF  @TIPO = 1 --Medicamentos
	BEGIN  
		 SELECT @valores= COALESCE(@valores + ' / ', '') +  Rtrim(administracion)
			  FROM (
			 SELECT (SELECT CONVERT(VARCHAR(2000),A.DESCRIPADMIN + ' de forma ' + B.DESVIAADM) FROM EHR.HCORMEDICAMESQUEMA A INNER JOIN HCVIAADMI B ON A.CODVIAADM = B.CODVIAADM WHERE A.IDHCORDQUIMIO = @IDESQUEMA AND A.CODPRODUC = @codigomedicamento AND A.NUMEFOLIO = @NUMEFOLIO AND A.CICLO = @CICLO FOR XML PATH('')) AS 'Administracion'
			 ) AS administracion
			 -- Si el valor de consulta de arriba es null consultamos la descripcion como lo hacia anteriormente
			 if @valores is NULL OR @valores = ''
			 BEGIN
			 SELECT @valores = [dbo].[CargarDosisXcicloXMedicamento](@TIPO, @IDESQUEMA,@CICLO,@codigomedicamento, NULL)
			 END
	END
 return @valores

END