
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
	

	execute(@query);
	--select @query;

END