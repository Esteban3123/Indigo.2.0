

-- =============================================
-- Author:		Josselyne ester chilito galindez
-- Create date: 11-04-2019
-- Description:	Listar variables dinamicas en formatos de educación
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarVariablesDinamicasEducacionPaciente]
(
@IDHCHISPACA INT,
@CodigoHC VARCHAR(3)
)
AS
BEGIN
	SET NOCOUNT ON;

	 DECLARE @query AS NVARCHAR(MAX);
	 DECLARE @columnas Nvarchar(max)='';
	 DECLARE @IDMODELOHC as integer 

if @CodigoHC = '' begin 
	SET @IDMODELOHC = (select IdParamEducationFormatsC from PatientEducationFormatsC where ID = @IDHCHISPACA  )  --cuando el reporte se abre desde una HC
end else begin
	SET @IDMODELOHC = (select ID from ParamEducationFormatsC  where Code  = @CodigoHC   )  --Cuando el reporte se abre desde parametrizador de HC
END

SELECT @columnas = coalesce(@columnas + quotename( cast(VARIABLE as varchar(150))) + ',', '')
	FROM (select distinct vr.ID, Concat(vr.[Name], vr.Id) AS  VARIABLE from ParamEducationFormatsVariablesC vr inner join ParamEducationFormatsXVariables vrHC on vr.Id = vrHC.IdParamEducationFormatsVariablesC where vrHC.IdParamEducationFormatsC = @IDMODELOHC) as DTM
order by DTM.ID 


if @columnas = '' begin
   Print 'No hay Columnas'
   return 
END

set @columnas  = left(@columnas,LEN(@columnas)-1)

SELECT @query = CONCAT('
	SELECT ', @columnas ,'
	from
	(
	  SELECT Concat(A.[Name], A.Id) as Name , IIF(L.Name IS NULL,Value,L.Name) as VALOR 
	  FROM ParamEducationFormatsVariablesC A ',
	  ' lEFT JOIN ', 'PatientEducationFormatsD' , 
	  ' B on A.ID=B.IdParamEducationFormatsVariablesC LEFT JOIN ParamEducationFormatsVariablesD L on B.IdParamEducationFormatsVariablesD = L.Id ',
	  ' WHERE IdPatientEducationFormatsC=',@IDHCHISPACA,'
	) as st
	pivot
	(
	  max(VALOR)
	  FOR Name in (' + @columnas + ')
	) as pivottable');

END

execute(@query);
print @query;