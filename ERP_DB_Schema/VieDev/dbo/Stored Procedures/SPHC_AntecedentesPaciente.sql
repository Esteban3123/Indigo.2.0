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