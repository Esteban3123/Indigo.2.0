-- Stored Procedure

-- =============================================
-- Author:		Rafael Eduardo patiño Cabrera
-- Create date: 26/03/2019
-- Description:	retorna los programas RIAS a los que aplica un paciente y informacion si ha estado icrito (el ultimo regsitro de isncripcion)
-- =============================================
CREATE PROCEDURE [dbo].[SP_RIAS_GestionRiasXPaciente]
	@Identificacion Varchar(25)
AS
BEGIN
	

	SET NOCOUNT ON;
	declare @FechaNacimiento as datetime
	select  @FechaNacimiento = IPFECNACI from INPACIENT where IPCODPACI = @Identificacion	 
	declare @Edaddias as integer= datediff(DAY,@FechaNacimiento,[Common].[GETDATE]()) 


	SELECT RP.ID as IDRIASXPACIENTE, R.ID, R.CODPRO + ' - ' + R.NOMBRE as RIAS, IIF(RP.ESTADO is null, 'No Inscrito',case RP.ESTADO when 1 then 'No Inscrito' when 2 then 'Inscrito' when 3 then 'Egresado' END) as Estado, RP.FECHAINCRIPCION  as FechaInscripcion,
						isnull(A.DESMOTANU,'') as MotivoEgreso, isnull(RP.JUSTIFICACIONEGRE,'') as JustificacionEgreso 	
	FROM (
					select ID,
						CODPRO,
						NOMBRE,
						case TIPOEDAD when 0 then EDADMINIMA     --Dia
					   						when 1 then (EDADMINIMA * 30) --Meses a dias
											when 2 then (EDADMINIMA * 365)  --años a Dias
						End as EDADMINIMA_DIAS,
						case TIPOEDAD when 0 then EDADMAXIMA  + 1    --Dia
					   						when 1 then (EDADMAXIMA * 30) + 30 --Meses a dias
											when 2 then (EDADMAXIMA * 365) + 365 --años a Dias
						End as EDADMAXIMA_DIAS,
						case TIPOEDAD when 0 then 'Dias'
											when 1 then 'Meses'
											when 2 then 'Años'	END as UnidadRangoEdad,
						SEXO
						from RIAS 
			) as R left join RIASXPACIENTE RP on R.ID = RP.IDRIAS and RP.IPCODPACI = @Identificacion
			left join HCMOANULB A on A.CODMOTANU = RP.CODMOTIVOEGRE
	 WHERE @Edaddias BETWEEN R.EDADMINIMA_DIAS AND R.EDADMAXIMA_DIAS   

END