

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que obtiene y presenta de forma dinámica las variables registradas en un formato de educación al paciente (y/o cuidador), pivotando los datos para mostrar cada variable como columna del resultado. Puede abrirse desde una historia clínica existente (usando el ID del registro de educación) o desde el parametrizador de formatos (usando el código del formato). Consulta la configuración de variables en ParamEducationFormatsVariablesC y su relación con el formato en ParamEducationFormatsXVariables, y luego recupera los valores reales registrados en PatientEducationFormatsD para el formulario indicado. Sirve para visualizar en reportes o dashboards de historia clínica los datos de educación impartida al paciente, con cada campo educativo como columna independiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarVariablesDinamicasEducacionPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarVariablesDinamicasEducacionPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera dinámicamente un resultado pivoteado con los valores de las variables asociadas a un formato de educación al paciente, ya sea invocado desde una historia clínica o desde el parametrizador de formatos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el formato de educación referenciado: si se recibe código vacío, debe existir un registro en PatientEducationFormatsC con el ID dado; en caso contrario, debe existir un ParamEducationFormatsC con el código suministrado.; El formato debe tener variables asociadas en ParamEducationFormatsXVariables; si no hay variables, no se ejecuta el pivote.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El identificador de columna pivote se construye concatenando el Name y el Id de la variable, garantizando unicidad ante nombres repetidos.; Las columnas del pivote se ordenan por el ID de la variable.; Solo se incluyen variables vinculadas explícitamente al formato mediante ParamEducationFormatsXVariables.; Los valores mostrados provienen exclusivamente del registro de educación del paciente identificado por el ID recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Educación al paciente; Formato de educación; Historia clínica; Variables dinámicas de formato', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset dinámico): Cuando existen variables asociadas al formato (lista de columnas no vacía), se construye y ejecuta una consulta dinámica con PIVOT sobre los valores de las variables del paciente y se devuelve como resultset.; [RETURN_RESULT] (mensaje): Cuando no se encontraron variables para el formato (@columnas vacío), se imprime ''No hay Columnas'' y se retorna sin ejecutar la consulta dinámica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Código de HC vacío → Se obtiene el modelo de formato desde PatientEducationFormatsC usando el ID recibido (apertura desde una HC). else Se obtiene el modelo de formato desde ParamEducationFormatsC buscando por el código (apertura desde el parametrizador de HC).; si No existen variables asociadas al formato (@columnas = '''') → Imprime ''No hay Columnas'' y termina la ejecución sin generar pivote. else Construye la lista de columnas, arma el SQL dinámico con PIVOT y lo ejecuta.; si El valor de la variable en PatientEducationFormatsD no tiene un nombre asociado en ParamEducationFormatsVariablesD (L.Name IS NULL) → Se utiliza el campo Value como valor mostrado. else Se utiliza el Name del catálogo de valores de variable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.PatientEducationFormatsC; dbo.ParamEducationFormatsC; dbo.ParamEducationFormatsVariablesC; dbo.ParamEducationFormatsXVariables; dbo.PatientEducationFormatsD; dbo.ParamEducationFormatsVariablesD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasEducacionPaciente';
-- GO
