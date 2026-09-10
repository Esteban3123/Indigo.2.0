  
CREATE PROCEDURE [Admissions].[SP_ValidatesPatientPopulationType]  
(  
@Identificacion varchar(25)
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
						case UnitMinimumAge 
										 when 3 then MinimumAge + 0 -- 56     --Dia
					   					 when 2 then (MinimumAge * 30) --Meses a dias
										 when 1 then (MinimumAge * 365)  --años a Dias
						End as EDADMINIMA_DIAS,
						case UnitMaximumAge 
										 when 3 then  MaximumAge  + 1    --Dia
					   					 when 2 then (MaximumAge * 30) + 30 --Meses a dias
										 when 1 then (MaximumAge * 365) + 365 --años a Dias
						End as EDADMAXIMA_DIAS,
						case UnitMinimumAge 
										 when 3 then 'Dias'
										 when 2 then 'Meses'
										 when 1 then 'Años'	
						END as UnidadRangoEdadMinima,
						case UnitMaximumAge 
										 when 3 then 'Dias'
										 when 2 then 'Meses'
										 when 1 then 'Años'	
						END as UnidadRangoEdadMaxima, AppliesSex, Status, Name 'Nombre', Code as 'Codigo', Image
					from Admissions.TypesPopulationGroups ) as tmp
					where  (@EdadPacienteDias BETWEEN tmp.EDADMINIMA_DIAS AND tmp.EDADMAXIMA_DIAS) and tmp.AppliesSex in (@SexoPaciente,3) and Status = 1
	end else begin
			select AppliesSex, Status, Name 'Nombre', Code as 'Codigo', Image from Admissions.TypesPopulationGroups where Status = 1
	end

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que determina el grupo poblacional al que pertenece un paciente (por ejemplo: neonato, pediátrico, adulto mayor, gestante) según su cédula o identificación. Calcula la edad del paciente en días a partir de su fecha de nacimiento y consulta su sexo en la tabla maestra de pacientes (INPACIENT), para luego cruzar esos datos con la tabla de tipos de grupos poblacionales (TypesPopulationGroups), filtrando por los rangos de edad mínima y máxima y el sexo aplicable. Si el paciente existe en el sistema, retorna únicamente los grupos que corresponden a su perfil demográfico; si no existe, retorna todos los grupos poblacionales activos disponibles. Se utiliza durante el proceso de admisión para clasificar automáticamente al paciente según su perfil de edad y sexo.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_ValidatesPatientPopulationType';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'PROCEDURE', @level1name = N'SP_ValidatesPatientPopulationType';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina los grupos poblacionales aplicables a un paciente según su edad (en días) y sexo, o devuelve todos los grupos activos si el paciente no está registrado.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ValidatesPatientPopulationType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente identificado debe existir en INPACIENT con IPFECNACI e IPSEXOPAC válidos para que se filtre por edad/sexo; en caso contrario, se devuelve la lista general.; Los registros de Admissions.TypesPopulationGroups deben tener UnitMinimumAge/UnitMaximumAge en el dominio {1=Años, 2=Meses, 3=Días}.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ValidatesPatientPopulationType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran grupos poblacionales con Status=1 (activos).; AppliesSex=3 actúa como comodín que aplica a ambos sexos.; La conversión a días para el límite máximo añade un margen (+1 día, +30 días o +365 días) según la unidad, ampliando el rango superior.; La edad del paciente se calcula siempre en días usando Common.GETDATE() como referencia.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ValidatesPatientPopulationType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Grupo poblacional; Edad (días/meses/años); Sexo del paciente; Admisión; Clasificación demográfica', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ValidatesPatientPopulationType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Admissions.TypesPopulationGroups: Si el paciente existe en INPACIENT: retorna grupos poblacionales activos (Status=1) cuyo rango de edad convertido a días incluya la edad del paciente y cuyo AppliesSex coincida con el sexo del paciente o sea 3 (ambos).; [RETURN_RESULT] Admissions.TypesPopulationGroups: Si el paciente NO existe en INPACIENT: retorna todos los grupos poblacionales con Status=1 sin filtrar por edad ni sexo.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ValidatesPatientPopulationType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si EXISTS (SELECT * FROM INPACIENT WHERE IPCODPACI = @Identificacion) → Calcula edad en días (DATEDIFF DAY desde IPFECNACI) y sexo (IPSEXOPAC), y filtra TypesPopulationGroups por rango de edad y AppliesSex IN (@SexoPaciente, 3) con Status=1. else Retorna todos los grupos poblacionales activos (Status=1) sin filtros demográficos.; si UnitMinimumAge/UnitMaximumAge = 1 → Convierte la edad mínima/máxima multiplicando por 365 (años a días); en máxima añade +365.; si UnitMinimumAge/UnitMaximumAge = 2 → Convierte multiplicando por 30 (meses a días); en máxima añade +30.; si UnitMinimumAge/UnitMaximumAge = 3 → Trata el valor como días; en máxima añade +1.', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ValidatesPatientPopulationType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'INPACIENT; Admissions.TypesPopulationGroups', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ValidatesPatientPopulationType';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Admissions', @level1type=N'PROCEDURE', @level1name=N'SP_ValidatesPatientPopulationType';
-- GO
