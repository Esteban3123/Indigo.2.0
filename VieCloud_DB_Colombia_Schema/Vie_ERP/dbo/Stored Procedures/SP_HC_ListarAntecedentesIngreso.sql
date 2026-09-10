
-- =============================================
-- Author:		Juan David Patiño Cabrera
-- Create date: 12/10/2016
-- Description:	SP que lista los antecedentes del paciente por Ingreso esto es para la Epicrisis.
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarAntecedentesIngreso] 
(
@PACIENTE varchar(25) ,
@FECHALIMITE datetime
)
AS
BEGIN
	SET NOCOUNT ON;

DECLARE @valoresANTECEDENTESMEDICOS VARCHAR(1000)
DECLARE @valoresANTECEDENTESQUIRURGICOS VARCHAR(1000)
DECLARE @valoresANTECEDENTESTRANSFUCIONALES VARCHAR(1000)
DECLARE @valoresANTECEDENTESINMUNOLOGICOS VARCHAR(1000)
DECLARE @valoresANTECEDENTESALERGICOS VARCHAR(1000)
DECLARE @valoresANTECEDENTESTRAUMATICOS VARCHAR(1000)
DECLARE @valoresANTECEDENTESPSICOLOGICOS VARCHAR(1000)
DECLARE @valoresANTECEDENTESFARMACOLOGICOS VARCHAR(1000)
DECLARE @valoresANTECEDENTESFAMILIARES VARCHAR(1000)
DECLARE @valoresANTECEDENTESTOXICOS VARCHAR(1000)
DECLARE @valoresANTECEDENTESOTROS VARCHAR(1000)
DECLARE @valoresANTECEDENTESHABITOS VARCHAR(1000)
DECLARE @valoresANTECEDENTESESCOLARES VARCHAR(1000)
DECLARE @valoresANTECEDENTESLABORALES VARCHAR(1000)
DECLARE @valoresANTECEDENTESNUTRICIONALES VARCHAR(1000)
DECLARE @valoresANTECEDENTESODONTOLOGICOS VARCHAR(1000)
DECLARE @valoresANTECEDENTESSOCIOECONOMICOS VARCHAR(1000)
DECLARE @valoresANTECEDENTESOFTALMOLOGICO VARCHAR(1000)

SELECT TOP 1
	 @valoresANTECEDENTESMEDICOS= COALESCE(@valoresANTECEDENTESMEDICOS + ' ' ,'') + ANTMEDPAC,
	 @valoresANTECEDENTESQUIRURGICOS =  COALESCE(@valoresANTECEDENTESQUIRURGICOS + ' ', '') + rtrim(ltrim(ANTQUIPAC)),
	 @valoresANTECEDENTESTRANSFUCIONALES =  COALESCE(@valoresANTECEDENTESTRANSFUCIONALES + '', '') + ANTTRAPAC,
	 @valoresANTECEDENTESINMUNOLOGICOS =  COALESCE(@valoresANTECEDENTESINMUNOLOGICOS + ' ', '') + ANTINMPAC,
	 @valoresANTECEDENTESALERGICOS =  COALESCE(@valoresANTECEDENTESALERGICOS + ' ', '') + ANTALEPAC,
	 @valoresANTECEDENTESTRAUMATICOS =  COALESCE(@valoresANTECEDENTESTRAUMATICOS + ' ', '') + ANTTRUPAC,
	 @valoresANTECEDENTESPSICOLOGICOS =  COALESCE(@valoresANTECEDENTESPSICOLOGICOS + ' ', '') + ANTPSIPAC,
	 @valoresANTECEDENTESFARMACOLOGICOS =  COALESCE(@valoresANTECEDENTESFARMACOLOGICOS + ' ', '') + ANTFARPAC,
	 @valoresANTECEDENTESFAMILIARES =  COALESCE(@valoresANTECEDENTESFAMILIARES + ' ', '') + ANTFAMPAC,
	 @valoresANTECEDENTESTOXICOS =  COALESCE(@valoresANTECEDENTESTOXICOS + ' ', '') + ANTTOXPAC,
	 @valoresANTECEDENTESOTROS =  COALESCE(@valoresANTECEDENTESOTROS + ' ', '') + ANTOTRPAC,
	 @valoresANTECEDENTESHABITOS =  COALESCE(@valoresANTECEDENTESHABITOS + ' ', '') + ANTHABVID,
	 @valoresANTECEDENTESESCOLARES =  COALESCE(@valoresANTECEDENTESESCOLARES + ' ', '') + ANTESCOLARES,
	 @valoresANTECEDENTESLABORALES =  COALESCE(@valoresANTECEDENTESLABORALES + ' ', '') + ANTLABORALES,
	 @valoresANTECEDENTESNUTRICIONALES =  COALESCE(@valoresANTECEDENTESNUTRICIONALES + ' ', '') + ANTNUTRICION,
	 @valoresANTECEDENTESODONTOLOGICOS =  COALESCE(@valoresANTECEDENTESODONTOLOGICOS + ' ', '') + ANTODONTOLOG,
	 @valoresANTECEDENTESSOCIOECONOMICOS =  COALESCE(@valoresANTECEDENTESSOCIOECONOMICOS + ' ', '') + ANTSOCIOECON,
	 @valoresANTECEDENTESOFTALMOLOGICO =  COALESCE(@valoresANTECEDENTESOFTALMOLOGICO + ' ', '') + Ophthalmological
 FROM HCANTPACI WHERE IPCODPACI= @PACIENTE  AND FECHISPAC  <=  @FECHALIMITE ORDER BY NUMEFOLIO DESC
   Select  @valoresANTECEDENTESOFTALMOLOGICO AS 'ANTECEDENTES OFTALMOLOGICO ',@valoresANTECEDENTESMEDICOS as 'ANTECEDENTES MEDICOS' ,@valoresANTECEDENTESQUIRURGICOS  as 'ANTECEDENTES QUIRURGICOS' , @valoresANTECEDENTESTRANSFUCIONALES as 'ANTECEDENTES TRANSFUCIONALES', @valoresANTECEDENTESINMUNOLOGICOS As 'ANTECEDENTES INMUNOLOGICOS' , @valoresANTECEDENTESALERGICOS as 'ANTECEDENTES ALERGICOS', @valoresANTECEDENTESTRAUMATICOS As 'ANTECEDENTES TRAUMATICOS',@valoresANTECEDENTESPSICOLOGICOS As 'ANTECEDENTES PSICOLOGICOS PSIQUIATRICOS',@valoresANTECEDENTESFARMACOLOGICOS As 'ANTECEDENTES FARMACOLOGICOS',@valoresANTECEDENTESFAMILIARES As 'ANTECEDENTES FAMILIARES',@valoresANTECEDENTESTOXICOS as 'ANTECEDENTES TOXICOS',@valoresANTECEDENTESOTROS As 'ANTECEDENTES OTROS',
   @valoresANTECEDENTESHABITOS As ANTHABVID, @valoresANTECEDENTESESCOLARES As ANTESCOLARES,@valoresANTECEDENTESLABORALES As ANTLABORALES,@valoresANTECEDENTESNUTRICIONALES As ANTNUTRICION,@valoresANTECEDENTESODONTOLOGICOS As ANTODONTOLOG,@valoresANTECEDENTESSOCIOECONOMICOS As ANTSOCIOECON
	end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera todos los antecedentes clínicos registrados de un paciente para la generación de la epicrisis al momento del egreso. Recibe como parámetros la cédula o código del paciente y una fecha límite, y consulta el registro más reciente de antecedentes en la historia clínica (tabla HCANTPACI) que sea anterior o igual a dicha fecha. Consolida y devuelve en una sola fila los antecedentes médicos, quirúrgicos, transfusionales, inmunológicos, alérgicos, traumáticos, psicológicos, farmacológicos, familiares, tóxicos, hábitos de vida, escolares, laborales, nutricionales, odontológicos, socioeconómicos y oftalmológicos del paciente, permitiendo al profesional de salud visualizar el resumen completo del historial clínico previo del paciente durante el proceso de cierre del ingreso u hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarAntecedentesIngreso';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarAntecedentesIngreso';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el último registro de antecedentes clínicos del paciente (anterior o igual a una fecha límite) para alimentar la sección de antecedentes de la epicrisis.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el paciente identificado en la tabla de antecedentes; La fecha límite debe ser válida para filtrar registros con FECHISPAC menor o igual a ella', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se considera un registro: el de mayor NUMEFOLIO entre los que cumplen el filtro de paciente y fecha; Los antecedentes quirúrgicos se devuelven sin espacios iniciales/finales (LTRIM/RTRIM); Si no hay registros que cumplan el filtro, todas las variables retornan NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Epicrisis; Antecedentes médicos; Antecedentes quirúrgicos; Antecedentes transfusionales; Antecedentes inmunológicos; Antecedentes alérgicos; Antecedentes traumáticos; Antecedentes psicológicos/psiquiátricos; Antecedentes farmacológicos; Antecedentes familiares; Antecedentes tóxicos; Hábitos de vida; Antecedentes escolares; Antecedentes laborales; Antecedentes nutricionales; Antecedentes odontológicos; Antecedentes socioeconómicos; Antecedentes oftalmológicos; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCANTPACI: Devuelve un único conjunto de resultados con los antecedentes (médicos, quirúrgicos, transfusionales, inmunológicos, alérgicos, traumáticos, psicológicos, farmacológicos, familiares, tóxicos, otros, hábitos, escolares, laborales, nutricionales, odontológicos, socioeconómicos y oftalmológicos) del registro más reciente (mayor NUMEFOLIO) del paciente cuya FECHISPAC <= fecha límite', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCANTPACI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarAntecedentesIngreso';
-- GO
