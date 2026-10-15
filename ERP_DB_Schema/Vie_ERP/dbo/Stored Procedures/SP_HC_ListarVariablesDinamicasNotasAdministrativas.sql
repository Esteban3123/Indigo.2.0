-- =============================================
-- Author:		<Author,Juan David Patiño Cabrera,Name>
-- Create date: <Create Date,11-04-2019,>
-- Description:	<Description,Listar variables dinamicas en Notas Administrativas,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarVariablesDinamicasNotasAdministrativas]
(
	 @IDHCHISPACA INT,
	 @CodigoHC VARCHAR(6)
)
AS
BEGIN
	SET NOCOUNT ON;

	 DECLARE @query AS NVARCHAR(MAX);
	 DECLARE @columnas Nvarchar(max)='';
	 DECLARE @IDMODELOHC as integer 


if @CodigoHC = '' begin 
	SET @IDMODELOHC = (select IDNOTAADMINISTRATIVA from NTNOTASADMINISTRATIVASC where ID = @IDHCHISPACA  )  --cuando el reporte se abre desde una HC
end else begin
	SET @IDMODELOHC = (select ID from NTADMINISTRATIVAS where CODIGO  = @CodigoHC   )  --Cuando el reporte se abre desde parametrizador de HC
end


SELECT @columnas = coalesce(@columnas + quotename( cast(VARIABLE as varchar(100))) + ',', '')
	FROM (select distinct vr.ID, vr.VARIABLE from NTVARIABLES vr inner join NTXVARIABLES vrHC on vr.ID = vrHC.IDNTVARIABLES where vrHC.IDNTADMINISTRATIVAS = @IDMODELOHC) as DTM
order by DTM.ID 

if @columnas = '' begin
   Print 'No hay Columnas'
   return 
end

set @columnas  = left(@columnas,LEN(@columnas)-1)



SELECT @query = CONCAT('
	SELECT ', @columnas ,'
	from
	(
	  SELECT VARIABLE, IIF(L.NOMBRE IS NULL,VALOR,L.NOMBRE) as VALOR 
	  FROM NTVARIABLES A ',
	  ' LEFT JOIN ', 'NTNOTASADMINISTRATIVASD' , 
	  ' B on A.ID=B.IDNTVARIABLE  LEFT JOIN NTVARIABLESL L on B.IDITEMLISTA = L.ID ',
	  ' WHERE IDNTNOTASADMINISTRATIVASC=',@IDHCHISPACA,'
	) as st
	pivot
	(
	  max(VALOR)
	  FOR VARIABLE in (' + @columnas + ')
	) as pivottable');

END


execute(@query);
print @query;