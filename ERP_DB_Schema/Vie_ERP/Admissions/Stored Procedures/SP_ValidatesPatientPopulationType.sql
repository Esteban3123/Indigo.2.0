  
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