



CREATE PROCEDURE [dbo].[SP_HC_ListarVariablesDinamicas]
(
 @tablaVariables int,
 @IDHCHISPACA INT,
 --@FechaHistoria DATE,
 @CodigoHC VARCHAR(6)
)
AS
BEGIN
	SET NOCOUNT ON;
	--1: ANTVARIABLES
	--2: RSVARIABLES
	--3: EXAVARIABLES
	--4: OTVARIABLES
			
	--eXEC sP_HC_ListarVariablesDinamicas 1, '', '';

DECLARE @query AS NVARCHAR(MAX);
DECLARE @columnas Nvarchar(max)='';

declare @IDMODELOHC as integer 

if @CodigoHC = '' begin 
	SET @IDMODELOHC = (select IDMODELOHC from HCHISPACA where ID = @IDHCHISPACA  )  --cuando el reporte se abre desde una HC
end else begin
	SET @IDMODELOHC = (select ID from PRMODELOHC where CODIGO  = @CodigoHC   )  --Cuando el reporte se abre desde parametrizador de HC
end

IF (@tablaVariables=1)
BEGIN
	SELECT @columnas = coalesce(@columnas + quotename( cast(VARIABLE as varchar(100))) + ',', '')
	FROM (select distinct vr.VARIABLE from ANTVARIABLES vr inner join PRHCXANTVARIABLES vrHC on vr.ID = vrHC.IDANTVARIABLES where vrHC.IDMODELOHC = @IDMODELOHC) as DTM
		order by DTM.VARIABLE 
	--FROM (select distinct vr.ID, vr.VARIABLE from ANTVARIABLES vr inner join PRHCXANTVARIABLES vrHC on vr.ID = vrHC.IDANTVARIABLES where vrHC.IDMODELOHC = @IDMODELOHC) as DTM
	--order by DTM.ID 
END
IF (@tablaVariables=2)
BEGIN
	SELECT   @columnas = coalesce(@columnas + quotename( cast(VARIABLE as varchar(100)) ) + ',', '')		
	FROM (select distinct vr.VARIABLE from RSVARIABLES vr inner join PRHCXRSVARIABLES vrHC on vr.ID = vrHC.IDRSVARIABLES where vrHC.IDMODELOHC = @IDMODELOHC) as DTM
	order by DTM.VARIABLE 
END
IF (@tablaVariables=3)
BEGIN
	SELECT @columnas = coalesce(@columnas + quotename( cast(VARIABLE as varchar(100))) + ',', '')
	FROM (select distinct  vr.VARIABLE from EXAVARIABLES vr inner join PRHCXEXAVARIABLES vrHC on vr.ID = vrHC.IDEXAVARIABLES where vrHC.IDMODELOHC = @IDMODELOHC) as DTM
	order by DTM.VARIABLE 
END
IF (@tablaVariables=4)
BEGIN
	SELECT @columnas = coalesce(@columnas + quotename( cast(VARIABLE as varchar(100))) + ',', '')
	FROM (select distinct  vr.VARIABLE from OTVARIABLES vr inner join PRHCXOTVARIABLES vrHC on vr.ID = vrHC.IDOTVARIABLES where vrHC.IDMODELOHC = @IDMODELOHC) as DTM
	order by DTM.VARIABLE 
END


if @columnas = '' begin
   return	
end


set @columnas  = left(@columnas,LEN(@columnas)-1)

--Se comentan debido a que no se va alistar por historico, de las tablas creadas pormes y año
--DECLARE @nombreTabla varchar(20)

--if @FechaHistoria =''
--begin
--SET @nombreTabla= CASE @tablaVariables WHEN 1 THEN 'ANTVALORES' WHEN 2 THEN 'RSVALORES' WHEN 3 THEN 'EXAVALORES' WHEN 4 THEN 'OTVALORES' END
--end
--else
--begin 
--declare @Mes varchar(20) = month(@FechaHistoria)

--	if len(@Mes) =1  
--	begin
--		set @Mes = '0' + @Mes 
--		SET @nombreTabla= CONCAT(CASE @tablaVariables WHEN 1 THEN 'ANTVALORES' WHEN 2 THEN 'RSVALORES' WHEN 3 THEN 'EXAVALORES' WHEN 4 THEN 'OTVALORES' END , YEAR(@FechaHistoria) , @Mes);
--	end else begin																											   
--		SET @nombreTabla= CONCAT(CASE @tablaVariables WHEN 1 THEN 'ANTVALORES' WHEN 2 THEN 'RSVALORES' WHEN 3 THEN 'EXAVALORES' WHEN 4 THEN 'OTVALORES' END , YEAR(@FechaHistoria) , MONTH(@FechaHistoria));
--	end
--end



--set @columnas2 = substring(@columnas2, 1, (len(@columnas2) - 1))


IF @tablaVariables = 1 BEGIN

	
	SELECT @query = CONCAT('
	SELECT ', @columnas ,'
	from
	(
	  SELECT VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR 
	  FROM ANTVARIABLES A ',
	  ' LEFT JOIN ANTVALORES', 
	  ' B on A.ID=B.IDANTVARIABLE  LEFT JOIN ANTVARIABLESL L on B.IDITEMLISTA = L.ID ',
	  ' WHERE IDHCHISPACA=',@IDHCHISPACA,'
	) as st
	pivot
	(
	  max(VALOR)
	  FOR VARIABLE in (' + @columnas + ')
	) as pivottable');

	

END ELSE IF @tablaVariables = 2 BEGIN
	
	SELECT @query = CONCAT('
	SELECT 	',@columnas,'

	from
	(
	  SELECT VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR 
	  FROM RSVARIABLES A ',
	  ' LEFT JOIN RSVALORES ', 
	  ' B on A.ID=B.IDRSVARIABLE  LEFT JOIN RSVARIABLESL L on B.IDITEMLISTA = L.ID ',
	  ' WHERE IDHCHISPACA=',@IDHCHISPACA,'
	) as st
	pivot
	(
	 MAX(VALOR)
	  FOR VARIABLE in (' + @columnas + ')
	) as pivottable');

END ELSE IF @tablaVariables = 3 BEGIN
	
	SELECT @query = CONCAT('
	SELECT ', @columnas ,'
	from
	(
	  SELECT VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR 
	  FROM EXAVARIABLES A ',
	  ' LEFT JOIN EXAVALORES ', 
	  ' B on A.ID=B.IDEXAVARIABLE  LEFT JOIN EXAVARIABLESL L on B.IDITEMLISTA = L.ID ',
	  ' WHERE IDHCHISPACA=',@IDHCHISPACA,'
	) as st
	pivot
	(
	  max(VALOR)
	  FOR VARIABLE in (' + @columnas + ')
	) as pivottable');


END ELSE IF @tablaVariables = 4 BEGIN
	
	SELECT @query = CONCAT('
	SELECT ', @columnas ,'
	from
	(
	  SELECT VARIABLE, IIF(L.NOMBRE is null,VALOR,L.NOMBRE) as VALOR 
	  FROM OTVARIABLES A ',
	  ' LEFT JOIN OTVALORES', 
	  ' B on A.ID=B.IDOTVARIABLE  LEFT JOIN OTVARIABLESL L on B.IDITEMLISTA = L.ID ',
	  ' WHERE IDHCHISPACA=',@IDHCHISPACA,'
	) as st
	pivot
	(
	 MAX(VALOR)
	  FOR VARIABLE in (' + @columnas + ')
	) as pivottable');

END




execute(@query);
print @query;

END