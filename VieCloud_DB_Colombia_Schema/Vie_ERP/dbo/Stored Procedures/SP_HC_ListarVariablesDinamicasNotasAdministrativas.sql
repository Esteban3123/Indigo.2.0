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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera dinámicamente una tabla pivote con los valores capturados para las variables configurables de una nota administrativa del paciente en historia clínica. Recibe el identificador de la nota del paciente y un código de nota administrativa: si el código está vacío, busca el modelo de nota desde el registro de notas del ingreso (NTNOTASADMINISTRATIVASC); si se provee el código, lo resuelve desde el catálogo de notas administrativas (NTADMINISTRATIVAS). Luego obtiene las variables aplicables a ese modelo cruzando NTVARIABLES con NTXVARIABLES, y construye en tiempo de ejecución una consulta PIVOT sobre la tabla de detalle de valores (NTNOTASADMINISTRATIVASD), mostrando cada variable como columna con su valor registrado —o su etiqueta de lista si aplica—. Se utiliza para visualizar en forma de ficha o formulario los datos dinámicos de una nota administrativa específica dentro del módulo de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarVariablesDinamicasNotasAdministrativas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarVariablesDinamicasNotasAdministrativas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera dinámicamente una consulta PIVOT que presenta las variables dinámicas y sus valores asociados a una nota administrativa de historia clínica como columnas tipo ficha.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el modelo de nota administrativa: si no se provee código, el ID de nota administrativa debe existir en NTNOTASADMINISTRATIVASC; si se provee, el código debe existir en NTADMINISTRATIVAS.; El modelo debe tener variables asociadas en NTXVARIABLES; si no, no se ejecuta el pivot.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor mostrado por variable es siempre el máximo (max(VALOR)) cuando hay múltiples registros para una misma variable en la nota.; Las columnas del pivot se ordenan por NTVARIABLES.ID.; Si la variable está asociada a una lista (IDITEMLISTA con coincidencia en NTVARIABLESL), prevalece la etiqueta NOMBRE sobre el valor literal.; El origen del modelo es excluyente: o por ID de nota existente, o por código de catálogo, nunca ambos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica; Notas administrativas; Variables dinámicas; Listas de ítems / catálogos; Parametrizador de HC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset dinámico): Cuando existen variables asociadas al modelo (@columnas no vacío), se ejecuta dinámicamente un SELECT con PIVOT sobre NTVARIABLES + NTNOTASADMINISTRATIVASD + NTVARIABLESL filtrado por IDNTNOTASADMINISTRATIVASC = @IDHCHISPACA, devolviendo una fila con cada variable como columna.; [RETURN_RESULT] (mensaje): Cuando @columnas queda vacío (no hay variables asociadas al modelo), imprime ''No hay Columnas'' y retorna sin ejecutar el pivot.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @CodigoHC es cadena vacía → Resuelve el modelo (@IDMODELOHC) desde NTNOTASADMINISTRATIVASC.IDNOTAADMINISTRATIVA usando @IDHCHISPACA (caso: reporte abierto desde una HC) else Resuelve el modelo (@IDMODELOHC) desde NTADMINISTRATIVAS.ID filtrando por CODIGO = @CodigoHC (caso: reporte abierto desde el parametrizador de HC); si No hay columnas/variables asociadas al modelo → Imprime ''No hay Columnas'' y termina la ejecución sin armar el pivot else Recorta la coma final de @columnas y construye la consulta PIVOT dinámica; si En el SELECT interno: L.NOMBRE IS NULL → Se muestra el campo VALOR crudo de la nota else Se muestra la etiqueta L.NOMBRE proveniente de NTVARIABLESL (lista de ítems)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.NTNOTASADMINISTRATIVASC; dbo.NTADMINISTRATIVAS; dbo.NTVARIABLES; dbo.NTXVARIABLES; dbo.NTNOTASADMINISTRATIVASD; dbo.NTVARIABLESL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasNotasAdministrativas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasNotasAdministrativas';
-- GO
