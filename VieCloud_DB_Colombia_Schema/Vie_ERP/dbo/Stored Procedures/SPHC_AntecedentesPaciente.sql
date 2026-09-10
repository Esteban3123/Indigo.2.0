-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[SPHC_AntecedentesPaciente]
(
    -- Add the parameters for the stored procedure here
    @Paciente varchar(25),
	@Ingreso varchar(25)
    
)
AS
BEGIN

	SET LANGUAGE ESPAÑOL;

		
	WITH AntCTE (Fecha,Medico, Quirurgicos, Inmunologicos, Alergicos, Traumatico, Farmacologico, Toxicos, Nutricionales) AS (
    SELECT FECHISPAC,ANTMEDPAC, ANTQUIPAC, ANTTRAPAC, ANTALEPAC , ANTTRUPAC, ANTFARPAC , ANTTOXPAC, ANTNUTRICION
    FROM HCANTPACI
    WHERE IPCODPACI = @Paciente AND NUMINGRES = @Ingreso
)

Select STUFF((Select CASE WHEN Medico IS NULL THEN NULL WHEN TRIM(Medico) = '' THEN NULL ELSE REPLACE(STUFF(CONCAT(convert(varchar, Fecha, 100), CHAR(10), STUFF(Medico, 1,19,''), CHAR(10)),1,0, ''), '/', CHAR(10)) END AS 'Description' from AntCTE FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 0, '') AS 'Descripcion', 1 As 'Tipo'

Union All 

Select STUFF((Select CASE WHEN Quirurgicos IS NULL THEN NULL WHEN TRIM(Quirurgicos) = '' THEN NULL ELSE REPLACE(STUFF(CONCAT(convert(varchar, Fecha, 100), CHAR(10), STUFF(Quirurgicos, 1,19,''), CHAR(10)),1,0, ''), '/', CHAR(10)) END AS 'Description' from AntCTE FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 0, '') AS 'Descripcion', 2 As 'Tipo'

Union All

Select STUFF((Select CASE WHEN Inmunologicos IS NULL THEN NULL WHEN TRIM(Inmunologicos) = '' THEN NULL ELSE REPLACE(STUFF(CONCAT(convert(varchar, Fecha, 100), CHAR(10), STUFF(Inmunologicos, 1,19,''), CHAR(10)),1,0, ''), '/', CHAR(10)) END AS 'Description' from AntCTE FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 0, '') AS 'Descripcion', 3 As 'Tipo'

Union All

Select STUFF((Select CASE WHEN Traumatico IS NULL THEN NULL WHEN TRIM(Traumatico) = '' THEN NULL ELSE REPLACE(STUFF(CONCAT(convert(varchar, Fecha, 100), CHAR(10), STUFF(Traumatico, 1,19,''), CHAR(10)),1,0, ''), '/', CHAR(10)) END AS 'Description' from AntCTE FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 0, '') AS 'Descripcion', 4 As 'Tipo'

Union All

Select STUFF((Select CASE WHEN Farmacologico IS NULL THEN NULL WHEN TRIM(Farmacologico) = ' ' THEN NULL ELSE REPLACE(STUFF(CONCAT(convert(varchar, Fecha, 100), CHAR(10), STUFF(Farmacologico, 1,19,''), CHAR(10)),1,0, ''), '/', CHAR(10)) END AS 'Description' from AntCTE FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 0, '') AS 'Descripcion', 5 As 'Tipo'

Union All 

Select STUFF((Select CASE WHEN Toxicos IS NULL THEN NULL WHEN TRIM(Toxicos) = '' THEN NULL ELSE REPLACE(STUFF(CONCAT(convert(varchar, Fecha, 100), CHAR(10), STUFF(Toxicos, 1,19,''), CHAR(10)),1,0, ''), '/', CHAR(10)) END AS 'Description' from AntCTE FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 0, '') AS 'Descripcion', 6 As 'Tipo'

Union All

Select STUFF((Select CASE WHEN Nutricionales IS NULL THEN NULL WHEN TRIM(Nutricionales) = '' THEN NULL ELSE REPLACE(STUFF(CONCAT(convert(varchar, Fecha, 100), CHAR(10), STUFF(Nutricionales, 1,19,''), CHAR(10)),1,0, ''), '/', CHAR(10)) END AS 'Description' from AntCTE FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 0, '') AS 'Descripcion', 7 As 'Tipo'

Union All

Select STUFF((Select CASE WHEN Alergicos IS NULL THEN NULL WHEN TRIM(Alergicos) = '' THEN NULL ELSE REPLACE(STUFF(CONCAT(convert(varchar, Fecha, 100), CHAR(10), STUFF(Alergicos, 1,19,''), CHAR(10)),1,0, ''), '/', CHAR(10)) END AS 'Description' from AntCTE FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 0, '') AS 'Descripcion', 8 As 'Tipo'

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y retorna los antecedentes clínicos de un paciente para un ingreso hospitalario específico, clasificados por tipo: médicos, quirúrgicos, inmunológicos, alérgicos, traumáticos, farmacológicos, tóxicos y nutricionales. Recibe como parámetros la cédula del paciente y el número de ingreso, y obtiene la información desde la tabla de antecedentes clínicos (HCANTPACI). Devuelve un registro por cada categoría de antecedente con su descripción en texto y un código numérico que identifica el tipo, permitiendo visualizar el historial clínico previo del paciente en la historia clínica del ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_AntecedentesPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_AntecedentesPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los antecedentes clínicos de un paciente en un ingreso específico, agrupados y clasificados por tipo (médicos, quirúrgicos, inmunológicos, traumáticos, farmacológicos, tóxicos, nutricionales y alérgicos) en formato de texto concatenado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AntecedentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir registro en HCANTPACI que coincida con el paciente y número de ingreso indicados; El idioma de sesión se fija en ESPAÑOL para el formato de fecha', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AntecedentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre se retornan 8 filas, una por cada tipo de antecedente; El tipo numérico identifica unívocamente la categoría de antecedente (1..8); Se omiten los primeros 19 caracteres del texto del antecedente al formatearlo; El separador ''/'' dentro del texto se transforma en salto de línea (CHAR(10))', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AntecedentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Antecedentes médicos; Antecedentes quirúrgicos; Antecedentes inmunológicos; Antecedentes traumáticos; Antecedentes farmacológicos; Antecedentes tóxicos; Antecedentes nutricionales; Antecedentes alérgicos; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AntecedentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve 8 filas (una por tipo de antecedente: 1=Médico, 2=Quirúrgico, 3=Inmunológico, 4=Traumático, 5=Farmacológico, 6=Tóxico, 7=Nutricional, 8=Alérgico) con la descripción concatenada o NULL si el campo está vacío o nulo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AntecedentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Campo de antecedente IS NULL o TRIM = '''' (en farmacológico se compara contra '' '') → La descripción se devuelve como NULL para ese tipo else Se construye la descripción concatenando fecha (formato 100) + contenido del antecedente (omitiendo los primeros 19 caracteres) reemplazando ''/'' por saltos de línea', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AntecedentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTPACI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AntecedentesPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_AntecedentesPaciente';
-- GO
