

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y presenta en formato tabular (pivot) los valores de variables clínicas dinámicas registradas en una historia clínica electrónica, según el tipo de formulario solicitado: antecedentes del paciente (tipo 1), variables de riesgo o escalas (tipo 2), exámenes o laboratorios (tipo 3), u otras variables (tipo 4). Determina el modelo de historia clínica activo a partir del ID de la nota clínica (HCHISPACA) o del código de plantilla (PRMODELOHC), luego obtiene las variables configuradas para ese modelo cruzando las tablas de variables (ANTVARIABLES, RSVARIABLES, EXAVARIABLES, OTVARIABLES) con sus relaciones de parametrización (PRHCXANTVARIABLES, PRHCXRSVARIABLES, PRHCXEXAVARIABLES), y finalmente construye y ejecuta SQL dinámico que transforma los valores registrados en columnas, resolviendo además los ítems de listas desplegables a sus nombres legibles. Se utiliza para renderizar en pantalla o en reportes los campos clínicos capturados en una consulta médica, adaptándose automáticamente a la configuración de cada plantilla de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarVariablesDinamicas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarVariablesDinamicas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, en formato pivote dinámico, los valores de variables (antecedentes, revisión por sistemas, examen físico u otras) asociadas a un modelo de historia clínica, transformando filas variable/valor en columnas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El modelo de historia clínica debe poder resolverse: si no se envía código, debe existir el registro en HCHISPACA; si se envía código, debe existir en PRMODELOHC.; Debe existir al menos una variable parametrizada para el modelo de HC seleccionado; de lo contrario el procedimiento retorna sin ejecutar consulta.; El selector de tabla debe estar en {1,2,3,4} para producir resultados (1=Antecedentes, 2=Revisión por Sistemas, 3=Examen Físico, 4=Otros).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen como columnas las variables vinculadas al IDMODELOHC mediante la tabla puente PRHCX*VARIABLES correspondiente.; Las variables se listan distintas y ordenadas alfabéticamente.; Cuando una variable referencia un ítem de lista (IDITEMLISTA), se muestra el NOMBRE del ítem en lugar del VALOR crudo.; El pivote agrega con MAX(VALOR), por lo que ante duplicados de la misma variable para una HC se conserva un solo valor.; El filtro por IDHCHISPACA se aplica siempre en la consulta dinámica, restringiendo los datos a una única historia clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Modelo de historia clínica; Antecedentes; Revisión por sistemas; Examen físico; Variables dinámicas de HC; Parametrización de HC; Ítems de lista de variables', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dynamic_query: Cuando se obtienen columnas dinámicas (variables parametrizadas), se ejecuta un SQL dinámico con PIVOT que retorna una sola fila por historia clínica con cada variable como columna.; [RETURN_RESULT] ANTVALORES: Cuando @tablaVariables=1, se pivotean los valores de ANTVALORES filtrados por IDHCHISPACA, reemplazando VALOR por NOMBRE de ANTVARIABLESL si existe ítem de lista.; [RETURN_RESULT] RSVALORES: Cuando @tablaVariables=2, se pivotean los valores de RSVALORES filtrados por IDHCHISPACA, reemplazando VALOR por NOMBRE de RSVARIABLESL si existe ítem de lista.; [RETURN_RESULT] EXAVALORES: Cuando @tablaVariables=3, se pivotean los valores de EXAVALORES filtrados por IDHCHISPACA, reemplazando VALOR por NOMBRE de EXAVARIABLESL si existe ítem de lista.; [RETURN_RESULT] OTVALORES: Cuando @tablaVariables=4, se pivotean los valores de OTVALORES filtrados por IDHCHISPACA, reemplazando VALOR por NOMBRE de OTVARIABLESL si existe ítem de lista.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodigoHC = '''' → Resuelve IDMODELOHC desde HCHISPACA usando @IDHCHISPACA (caso: reporte abierto desde una historia clínica). else Resuelve IDMODELOHC desde PRMODELOHC usando @CodigoHC (caso: reporte abierto desde el parametrizador de HC).; si @tablaVariables = 1 → Construye lista de columnas y query pivote sobre ANTVARIABLES/ANTVALORES/ANTVARIABLESL.; si @tablaVariables = 2 → Construye lista de columnas y query pivote sobre RSVARIABLES/RSVALORES/RSVARIABLESL.; si @tablaVariables = 3 → Construye lista de columnas y query pivote sobre EXAVARIABLES/EXAVALORES/EXAVARIABLESL.; si @tablaVariables = 4 → Construye lista de columnas y query pivote sobre OTVARIABLES/OTVALORES/OTVARIABLESL.; si @columnas = '''' (no hay variables parametrizadas para el modelo) → RETURN inmediato sin ejecutar la consulta dinámica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; dbo.PRMODELOHC; dbo.ANTVARIABLES; dbo.PRHCXANTVARIABLES; dbo.ANTVALORES; dbo.ANTVARIABLESL; dbo.RSVARIABLES; dbo.PRHCXRSVARIABLES; dbo.RSVALORES; dbo.RSVARIABLESL; dbo.EXAVARIABLES; dbo.PRHCXEXAVARIABLES; dbo.EXAVALORES; dbo.EXAVARIABLESL; dbo.OTVARIABLES; dbo.PRHCXOTVARIABLES; dbo.OTVALORES; dbo.OTVARIABLESL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicas';
-- GO
