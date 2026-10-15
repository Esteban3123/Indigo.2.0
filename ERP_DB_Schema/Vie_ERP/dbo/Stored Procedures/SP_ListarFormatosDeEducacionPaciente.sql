
  
CREATE PROCEDURE [dbo].[SP_ListarFormatosDeEducacionPaciente]  
(  
@CentroAtencion as char(10),
@UnidadFuncional as char(10),
@Identificacion varchar(25),
@TipoFormato AS BIT
)  
AS  
BEGIN  
 -- SET NOCOUNT ON added to prevent extra result sets from  
 -- interfering with SELECT statements.  
 SET NOCOUNT ON;  
  
    -- Insert statements for procedure here  
	declare @EdadPacienteDias as integer
	declare @SexoPaciente as integer
	set @EdadPacienteDias = (select datediff(DAY,IPFECNACI, Common.GETDATE()) from INPACIENT where IPCODPACI = @Identificacion)
	set @SexoPaciente = (select IPSEXOPAC from INPACIENT where IPCODPACI = @Identificacion) -- 1: Masculino 2:Femenino

	if exists (select * from INPACIENT where IPCODPACI = @Identificacion) begin
			select  * from (
					select 
						c.ID,
						case UnitMinAge 
										 when 3 then (MinAge + 1) ---dejo en  Dias
					   					 when 2 then (MinAge * 30) -- paso de Meses a dias
										 when 1 then (MinAge * 365)  --paso de años a Dias
						End as EDADMINIMA_DIAS,
						case UnitMaxAge 
										 when 3 then (MaxAge  + 1)    --dejo en Dias
					   					 when 2 then (MaxAge * 30) + 30 --paso de  Meses a dias
										 when 1 then (MaxAge * 365) + 365 --paso de  años a Dias
						End as EDADMAXIMA_DIAS,
						case UnitMinAge 
										 when 3 then 'Dias'
										 when 2 then 'Meses'
										 when 1 then 'Años'	
						END as UnidadRangoEdadMinima,
						case UnitMaxAge 
										 when 3 then 'Dias'
										 when 2 then 'Meses'
										 when 1 then 'Años'	
						END as UnidadRangoEdadMaxima, Sex, Status,Type, Case c.Obligatory WHEN 1 THEN 'Sí' WHEN 0 THEN 'No' end as Obligatorio, c.Obligatory , Description 'NOMBRE', Code as 'CODIGO', DashboardMedico,DashboardEspecialista,DashboardAtencionFarmaceutica, DashboardNursing, DashboardTerapia,DashboardServicioApoyo, Triage
					from dbo.ParamEducationFormatsC c ) as tmp		
	where  (@EdadPacienteDias BETWEEN tmp.EDADMINIMA_DIAS AND tmp.EDADMAXIMA_DIAS) and tmp.Sex in (@SexoPaciente,3) AND tmp.Type=@TipoFormato  AND Status = 1 
		and EXISTS(select ID from [dbo].[ParamEducationFormatsCareCenters] where IdParamEducationFormatsC = tmp.Id AND CODCENATE = @CentroAtencion )
		and EXISTS(select ID from [dbo].[ParamEducationFormatsFunctionalUnits] where IdParamEducationFormatsC = tmp.Id AND UFUCODIGO = @UnidadFuncional  )
		END		
	Else BEGIN
		select  * from (
					select 
						c.ID,
						 Sex, Status,Type, Case c.Obligatory WHEN 1 THEN 'Sí' WHEN 0 THEN 'No' end as Obligatorio, c.Obligatory , Description 'NOMBRE', Code as 'CODIGO', DashboardMedico,DashboardEspecialista,DashboardAtencionFarmaceutica, DashboardNursing, DashboardTerapia,DashboardServicioApoyo, Triage
					from dbo.ParamEducationFormatsC c ) as tmp		
	where  tmp.Type=@TipoFormato  AND Status = 1 
		and EXISTS(select ID from [dbo].[ParamEducationFormatsCareCenters] where IdParamEducationFormatsC = tmp.Id AND CODCENATE = @CentroAtencion )
		and EXISTS(select ID from [dbo].[ParamEducationFormatsFunctionalUnits] where IdParamEducationFormatsC = tmp.Id AND UFUCODIGO = @UnidadFuncional  );

	END
END