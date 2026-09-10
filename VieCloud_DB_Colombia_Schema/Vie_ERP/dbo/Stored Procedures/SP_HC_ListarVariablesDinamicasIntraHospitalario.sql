
CREATE PROCEDURE [dbo].[SP_HC_ListarVariablesDinamicasIntraHospitalario]
(
 @IDHCHISPACA INT,
 @CodigoHC VARCHAR(6),
 @IdHistoryPages INT,
 @User VARCHAR(20)
)
AS
BEGIN
	SET NOCOUNT ON;
			
	--eXEC SP_HC_ListarVariablesDinamicasIntraHospitalario 28164, '', 5;

	DECLARE @query AS NVARCHAR(MAX);
	DECLARE @columnas Nvarchar(max)='';

	declare @IdClinicalHistoryFormats as integer 

	if @CodigoHC = '' begin 
		SET @IdClinicalHistoryFormats = (select IdClinicalHistoryFormats from HCHISPACA where ID = @IDHCHISPACA  )  --cuando el reporte se abre desde una HC
		print 'paso aqui ' + convert(varchar,@IdClinicalHistoryFormats)
	end else begin
		SET @IdClinicalHistoryFormats = (select ID from ClinicalParameters.ClinicalHistoryFormats where CODE  = @CodigoHC   )  --Cuando el reporte se abre desde parametrizador de HC
		print 'id clinical :' + convert(varchar,@IdClinicalHistoryFormats)
	end

	SELECT @columnas = STRING_AGG(isnull(VARIABLE,''), ',')
	FROM (		select distinct concat('[',hg.IdHistoryPages,' - ',hg.Code,' - ', pv.Id,' - ', cast(pv.Name as varchar(100)),']') as VARIABLE
				from ClinicalParameters.ClinicalHistoryFormats model inner join 
				ClinicalParameters.ClinicalHistoryPages cp on model.id = cp.IdClinicalHistoryFormats inner join 
				ClinicalParameters.HistoryGroups hg on hg.IdHistoryPages = cp.IdHistoryPages inner join
				ClinicalParameters.ClinicalHistoryGroupXPage chgp on chgp.IdClinicalHistoryFormats = model.Id and chgp.IdHistoryGroups = hg.Id inner join
				ClinicalParameters.ClinicalHistoryVariableXGroup chvg on chvg.IdClinicalHistoryFormats = model.Id and chvg.idClinicalHistoryGroupXPage = chgp.id inner join
				ClinicalParameters.HistoryVariables pv on pv.Id = chvg.IdHistoryVariables 
				where model.Id = @IdClinicalHistoryFormats and pv.Status = 1 and hg.IdHistoryPages =@IdHistoryPages) as DTM
		
   --select @columnas
   print 'Columnas' + @columnas
	if @columnas = '' or @columnas is null  begin
	   return	
	end

	
	--set @columnas  = left(@columnas,LEN(@columnas)-1)
						

	SELECT @query = CONCAT('
							SELECT ', @columnas ,'
							from
							(
							  select concat(g.IdHistoryPages,'' - '',g.Code,'' - '', variable.Id,'' - '', cast(variable.Name as varchar(100))) as VARIABLE, 
								CASE variable.TypeVariable 
									WHEN 5 THEN ISNULL(NULLIF(IIF(L.Name IS NULL, dbo.FormatValueDateForUser(',@User,', CONVERT(DATETIME, v.value, 103)), L.Name),''''), ''No dato'') 
									ELSE ISNULL(NULLIF(IIF(L.Name IS NULL, v.value, L.Name),''''), ''No dato'')
								END AS VALOR  
							  from MedicalHistory.ResultVariablesHistoryDynamicsIntra v inner join 
							  ClinicalParameters.HistoryGroups g on g.Id = v.IdHistoryGroups inner join
							  ClinicalParameters.HistoryVariables variable on variable.Id = v.IdHistoryVariables left join
							  ClinicalParameters.HistoryVariablesList l on L.Id = v.IdHistoryVariablesList 
							  where IDHCHISPACA=',@IDHCHISPACA,' 
							) as st
							pivot
							(
							  max(VALOR)
							  FOR VARIABLE in (' + @columnas + ')
							) as pivottable');
	
	print @query
	execute(@query);
	--select @query;

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y presenta en formato tabular pivotante las variables clínicas dinámicas registradas en la sección intrahospitalaria de una historia clínica. Recibe el identificador de una historia clínica (o el código del formato de HC) y una página específica del formulario, y construye dinámicamente las columnas a partir de la parametrización de variables clínicas (signos vitales, parámetros de monitoreo, etc.) definidas en los catálogos de formatos, páginas, grupos y variables de historia clínica. Los valores reales ingresados por el personal de salud se obtienen desde el repositorio de resultados intrahospitalarios dinámicos, resolviendo además listas de valores y formateando fechas según el usuario. El resultado es una tabla dinámica donde cada columna representa una variable clínica parametrizada, útil para visualizar en pantalla o en reportes el seguimiento clínico del paciente hospitalizado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarVariablesDinamicasIntraHospitalario';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarVariablesDinamicasIntraHospitalario';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera dinámicamente un pivote con los valores capturados de las variables clínicas configuradas para una página de historia clínica intrahospitalaria, presentándolas como columnas por variable.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasIntraHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el formato de historia clínica identificado por el ID de la HC o por el código de HC suministrado.; Deben existir variables activas (Status = 1) parametrizadas para la página solicitada dentro del formato; si no, el procedimiento retorna sin ejecutar el pivote.; Las variables deben estar asociadas al formato a través de grupos y páginas (ClinicalHistoryGroupXPage y ClinicalHistoryVariableXGroup).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasIntraHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen variables con Status = 1 y pertenecientes a la página indicada.; Las variables se identifican por la combinación IdHistoryPages + Code de grupo + Id y Name de variable.; Valores nulos o vacíos se sustituyen siempre por la cadena ''No dato''.; El pivote se construye exclusivamente sobre los registros del paciente/HC indicado por IDHCHISPACA.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasIntraHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Historia clínica intrahospitalaria; Formato de historia clínica; Página de historia clínica; Grupo de variables clínicas; Variables clínicas dinámicas; Tipo de variable (fecha); Parametrizador de historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasIntraHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MedicalHistory.ResultVariablesHistoryDynamicsIntra: Devuelve un resultset pivotado con una columna por variable clínica activa de la página, mostrando ''No dato'' cuando el valor es NULL o vacío.; [RETURN_RESULT] MedicalHistory.ResultVariablesHistoryDynamicsIntra: Cuando TypeVariable = 5 (fecha), el valor se formatea con dbo.FormatValueDateForUser convirtiendo desde formato 103; en otros tipos se devuelve el valor crudo o el Name de la lista asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasIntraHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El código de HC viene vacío → Obtiene IdClinicalHistoryFormats desde HCHISPACA usando el ID de la historia clínica (apertura desde HC) else Obtiene IdClinicalHistoryFormats desde ClinicalHistoryFormats por CODE (apertura desde parametrizador de HC); si No hay columnas/variables encontradas para el formato y página → Termina la ejecución sin generar el pivote (RETURN); si La variable es de tipo 5 (fecha) → Aplica formateo de fecha por usuario con conversión estilo 103 else Retorna el valor textual o el Name de HistoryVariablesList si aplica; si Existe un IdHistoryVariablesList asociado al valor (L.Name no nulo) → Muestra el Name de la lista en vez del value crudo else Muestra el value capturado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasIntraHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.FormatValueDateForUser', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasIntraHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHISPACA; ClinicalParameters.ClinicalHistoryFormats; ClinicalParameters.ClinicalHistoryPages; ClinicalParameters.HistoryGroups; ClinicalParameters.ClinicalHistoryGroupXPage; ClinicalParameters.ClinicalHistoryVariableXGroup; ClinicalParameters.HistoryVariables; ClinicalParameters.HistoryVariablesList; MedicalHistory.ResultVariablesHistoryDynamicsIntra', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasIntraHospitalario';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarVariablesDinamicasIntraHospitalario';
-- GO
