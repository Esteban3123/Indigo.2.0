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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recupera la descripción de administración de un medicamento dentro de un esquema de quimioterapia oncológica, combinando la descripción de administración propia del medicamento (DESCRIPADMIN) con la vía de administración (oral, intravenosa, etc.) del catálogo de vías. Recibe como parámetros el tipo de consulta (1=medicamentos, 2=insumos), el folio, el identificador del esquema de quimioterapia, el código del medicamento y el número de ciclo. Si no encuentra información de administración en la tabla de medicamentos por esquema (HCORMEDICAMESQUEMA), recurre como fallback a la función CargarDosisXcicloXMedicamento para obtener la descripción. Se usa principalmente para presentar en texto legible cómo se suministra cada medicamento oncológico en un protocolo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'FUNCTION', @level1name = N'CargarAdministracion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'FUNCTION', @level1name = N'CargarAdministracion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la descripción consolidada de la administración (forma y vía) de un medicamento dentro de un ciclo y esquema de quimioterapia, con un mecanismo de respaldo si no hay datos en el esquema.', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'FUNCTION', @level1name=N'CargarAdministracion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el esquema, folio, medicamento y ciclo para obtener registros en HCORMEDICAMESQUEMA; La vía de administración referenciada debe existir en HCVIAADMI para que se incluya la descripción', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'FUNCTION', @level1name=N'CargarAdministracion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se genera descripción cuando el tipo corresponde a medicamentos (dosis); para insumos no retorna valor; Concatena múltiples administraciones separadas por '' / '' cuando existe más de un registro; Prioriza la descripción derivada del esquema sobre la fuente alternativa, usándola solo como fallback', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'FUNCTION', @level1name=N'CargarAdministracion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Esquema de quimioterapia; Ciclo de tratamiento; Medicamento; Vía de administración; Dosis; Folio de orden médica', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'FUNCTION', @level1name=N'CargarAdministracion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultado escalar: Cuando @TIPO=1 retorna la concatenación de DESCRIPADMIN + '' de forma '' + DESVIAADM unidas con '' / '' filtrando por IDHCORDQUIMIO, CODPRODUC, NUMEFOLIO y CICLO; [RETURN_RESULT] resultado escalar: Cuando la consulta principal devuelve NULL o cadena vacía, retorna lo provisto por dbo.CargarDosisXcicloXMedicamento como descripción alternativa', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'FUNCTION', @level1name=N'CargarAdministracion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo de carga = 1 (medicamentos) → Construye descripción concatenada de administración + vía a partir del esquema de quimioterapia para el ciclo y medicamento dado else No procesa (la rama de insumos no está implementada); si El resultado armado desde HCORMEDICAMESQUEMA/HCVIAADMI es NULL o vacío → Recurre a la función dbo.CargarDosisXcicloXMedicamento como fuente alternativa de descripción', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'FUNCTION', @level1name=N'CargarAdministracion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.CargarDosisXcicloXMedicamento', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'FUNCTION', @level1name=N'CargarAdministracion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'EHR.HCORMEDICAMESQUEMA; HCVIAADMI', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'FUNCTION', @level1name=N'CargarAdministracion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'EHR', @level1type=N'FUNCTION', @level1name=N'CargarAdministracion';
GO
