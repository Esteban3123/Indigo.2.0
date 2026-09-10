
  
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los formatos de educación al paciente o cuidador que aplican según el centro de atención, la unidad funcional y el tipo de formato solicitado. Cuando se proporciona la cédula o identificación del paciente, filtra los formatos por rango de edad (calculada en días desde la fecha de nacimiento) y sexo, asegurando que solo se presenten los formularios pertinentes para el perfil del paciente. Si no existe el paciente en el sistema, devuelve todos los formatos activos que correspondan al centro, unidad funcional y tipo indicados, sin filtro de edad ni sexo. Compone la información de parametrización de formatos (ParamEducationFormatsC) con la habilitación por sede (ParamEducationFormatsCareCenters) y por unidad funcional, indicando además en qué dashboards clínicos (médico, especialista, enfermería, farmacia, terapia, apoyo) y triage está disponible cada formato, y si es de diligenciamiento obligatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarFormatosDeEducacionPaciente';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ListarFormatosDeEducacionPaciente';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los formatos de educación al paciente aplicables según centro de atención, unidad funcional, tipo de formato y, si el paciente existe, además filtra por edad y sexo compatibles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarFormatosDeEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El centro de atención y la unidad funcional deben estar relacionados con el formato en las tablas de parametrización para que aparezca; Solo se consideran formatos activos (Status = 1); Si el paciente existe, debe tener fecha de nacimiento y sexo registrados para el cálculo de edad y filtro por sexo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarFormatosDeEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La edad del paciente se compara siempre en días, normalizando las unidades configuradas en el formato; Sex=3 representa formatos aplicables a ambos sexos; Solo se devuelven formatos con Status=1 (activos); Un formato solo se muestra si está habilitado simultáneamente para el centro de atención y la unidad funcional indicados; El campo Obligatory se traduce a ''Sí''/''No'' para presentación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarFormatosDeEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Formato de educación al paciente; Centro de atención; Unidad funcional; Edad del paciente; Sexo del paciente; Triage; Dashboard médico/especialista/farmacéutico/enfermería/terapia/servicio de apoyo; Formato obligatorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarFormatosDeEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ParamEducationFormatsC: Cuando el paciente existe en INPACIENT, retorna formatos cuyo rango de edad (convertido a días según UnitMinAge/UnitMaxAge: 1=años, 2=meses, 3=días) contenga la edad del paciente, cuyo Sex coincida con el del paciente o sea 3 (ambos), Type=@TipoFormato y Status=1, vinculados al centro y unidad funcional; [RETURN_RESULT] dbo.ParamEducationFormatsC: Cuando el paciente NO existe en INPACIENT, retorna formatos sin filtrar por edad ni sexo, solo por Type=@TipoFormato, Status=1 y vinculados al centro y unidad funcional', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarFormatosDeEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe paciente con la identificación en INPACIENT → Aplica filtros adicionales por rango de edad (en días) y por sexo (igual al del paciente o 3=ambos) else Omite filtros de edad y sexo y solo filtra por tipo, estado activo, centro y unidad funcional; si UnitMinAge/UnitMaxAge = 1 (Años), 2 (Meses) o 3 (Días) → Convierte el rango a días multiplicando por 365, 30 o sumando 1 respectivamente, para comparar contra la edad en días del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarFormatosDeEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.ParamEducationFormatsC; dbo.ParamEducationFormatsCareCenters; dbo.ParamEducationFormatsFunctionalUnits', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarFormatosDeEducacionPaciente';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ListarFormatosDeEducacionPaciente';
-- GO
