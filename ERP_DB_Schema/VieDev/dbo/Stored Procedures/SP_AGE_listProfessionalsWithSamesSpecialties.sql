

CREATE PROCEDURE [dbo].[SP_AGE_listProfessionalsWithSamesSpecialties] (@CodigoProfesional Varchar(25), @Especialidades Varchar(50)) AS BEGIN 

SET NOCOUNT ON;

DECLARE @CantidadEspecialidades AS INT = LEN(@Especialidades) - LEN(REPLACE(@Especialidades, ',', '')) + 1;

WITH Profesionales AS (
	SELECT CODESPECIA, CODPROSAL, RTRIM(NOMMEDICO) AS NOMMEDICO
	FROM INPROFSAL
	UNPIVOT
	(
	[CODESPECIA]
	FOR [Especialidad] in (CODESPEC1,CODESPEC2,CODESPEC3)
	) as p WHERE RTRIM(CODPROSAL) <> @CodigoProfesional AND (ESTADOMED='1' AND REACONEXT='True') and [CODESPECIA] in (SELECT * FROM [dbo].[SplitString](@Especialidades))
),
ConteoProfecionales AS (
	Select CODESPECIA, CODPROSAL, NOMMEDICO, COUNT(CODPROSAL) OVER (PARTITION BY CODPROSAL) AS Repeticiones From Profesionales
)

Select Distinct Rtrim(CODPROSAL) AS CODPROSAL, NOMMEDICO From ConteoProfecionales where Repeticiones = @CantidadEspecialidades

END