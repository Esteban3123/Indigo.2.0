
CREATE PROCEDURE [dbo].[SP_ENF_ListarMezclasNuevasProgramar]
(
@CodigoPaciente varchar(25),
@Ingreso Char(10)
)
AS
BEGIN
	SET NOCOUNT ON;
             
	select  
		A.CONSECUTI as 'IDHCINFLIQA', A.CODCONCEC, A.CODCONCEC_ORIGEN,
		case a.METAPLMED when '1' then 'Bolo Mezcla' when '2' then 'Infusion Mezcla Continua' when '3' then 'Bolo Medicamento'  end as 'Solicitud',A.METAPLMED,
		'Mezcla Infusión Continua' As 'Tipo Mezcla',A.TIPMEZLIQ,
		Rtrim(a.MEZLIQPAC)  as 'Productos',
		Rtrim(a.ADMMEZLIQ) as 'Dosis',
		Rtrim(a.INDAPLMED) as 'Instrucciones Adicionales',
		a.FECHAINIC as 'Inicio Aplicacion',
		case a.METAPLMED when '1' then 'Dosis única' when '2' then rtrim(c.DURACIINFM) when '3' then 'Dosis única' end as 'Duracion',
		rtrim(b.NOMMEDICO)  As 'Prescribe'
	from HCINFLIQA As a 
		Inner Join INPROFSAL b WITH (NOLOCK) ON a.CODPROSAL = b.CODPROSAL 
		Inner Join HCINFLIQD c WITH (NOLOCK) ON a.CODCONCEC = c.CODCONCEC
	where a.TIPMEZLIQ = 1 AND a.IPCODPACI = @CodigoPaciente AND a.NUMINGRES = @Ingreso AND (a.MEDPROGRAMADO = 0 OR a.MEDPROGRAMADO = NULL) AND PREESTADO <> 4    --1.Mezcla Continua
union all
	select  
		A.CONSECUTI As 'IDHCINFLIQA', A.CODCONCEC, A.CODCONCEC_ORIGEN,
		case A.TIPMEZLIQ when 3 then 'Infusion Mezcla Frecuencia' when 4 then 'Mezcla Magistral' end  As 'Solicitud',A.METAPLMED,
		case a.TIPMEZLIQ when 3 then 'Mezcla Infusión Frecuencia' when 4 Then 'Mezcla Magistral' end as 'Tipo Mezcla',A.TIPMEZLIQ,
		Rtrim(a.MEZLIQPAC)  as 'Productos',
		Rtrim(a.ADMMEZLIQ) as 'Dosis',
		Rtrim(INDAPLMED) as 'Instrucciones Adicionales',
		a.INICIOAPLICACION as 'Inicio Aplicacion',
		a.TIPODURACION as 'Duracion',
		rtrim(b.NOMMEDICO)  As 'Prescribe'
	from HCINFLIQA As a 
		Inner Join INPROFSAL b WITH (NOLOCK) ON a.CODPROSAL = b.CODPROSAL 
	where A.TIPMEZLIQ in (3,4)  AND a.IPCODPACI = @CodigoPaciente AND a.NUMINGRES = @Ingreso and (a.MEDPROGRAMADO = 0 OR a.MEDPROGRAMADO = NULL) AND PREESTADO <> 4 --3.Mezcla Frecuencia y 4.Mezcla Magistral
UNION ALL
	select  
		A.CONSECUTI as 'IDHCINFLIQA', A.CODCONCEC, A.CODCONCEC_ORIGEN,
		case a.METAPLMED when '4' then 'Infusion Líquidos' when '5' then 'Bolo Medicamento Líquidos' END as 'Solicitud',A.METAPLMED,
		'Líquidos' As 'Tipo Mezcla',A.TIPMEZLIQ,
		Rtrim(a.MEZLIQPAC)  as 'Productos',
		Rtrim(a.ADMMEZLIQ) as 'Dosis',
		Rtrim(a.INDAPLMED) as 'Instrucciones Adicionales',
		a.FECHAINIC as 'Inicio Aplicacion',
		case a.METAPLMED when '5' then 'Dosis única' when '4' then rtrim(c.DURACIINF)  end as 'Duracion',
		rtrim(b.NOMMEDICO)  As 'Prescribe'
	from HCINFLIQA As a 
			Inner Join INPROFSAL b WITH (NOLOCK) ON a.CODPROSAL = b.CODPROSAL 
			Inner Join HCINFLIQD c WITH (NOLOCK) ON a.CODCONCEC = c.CODCONCEC
	where a.TIPMEZLIQ = 2 AND a.IPCODPACI = @CodigoPaciente AND a.NUMINGRES = @Ingreso AND (a.MEDPROGRAMADO = 0 OR a.MEDPROGRAMADO = NULL) AND PREESTADO <> 4   --2.Liquidos
UNION ALL

	 -- Nutrición Parenteral 

	select  
	A.ID AS 'IDHCINFLIQA', 
	A.ID AS CODCONCEC, 
	NULL AS CODCONCEC_ORIGEN,
	'Nutrición Parenteral' AS 'Solicitud',
	'6' AS 'METAPLMED',
	NULL AS 'Tipo Mezcla',
	'5' AS TIPMEZLIQ,
        Rtrim(c.FullProductName) as 'Productos',
        Rtrim(a.INDICACIONADM) as 'Dosis',
        Rtrim(a.INDICACIONADI) as 'Instrucciones Adicionales',
		a.FECHAORDEN as 'Inicio Aplicacion',
		case a.TIPOINFUSION when '1' then 'Continua' when '2' then 'Ciclada' end as 'Duracion',
        rtrim(b.NOMMEDICO) As 'Prescribe'
		
    from HCNUTPAREC As a 
        Inner Join INPROFSAL b WITH (NOLOCK) ON a.CODPROSAL = b.CODPROSAL
		Inner join [MedicalHistory].[ProductSusceptibleMixingStation] c WITH (NOLOCK) ON a.ID = c.IdOrigin AND c.Origin = 'HCNUTPAREC'
		--inner join [MedicalHistory].[PharmaDose]  D ON D.CodeSusceptibleMixingStation = C.CodeSusceptibleMixingStation --AND D.IsDispensed = 1
		where A.IPCODPACI = @CodigoPaciente AND a.NUMINGRES = @Ingreso  AND A.STATUS = 1
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista todas las mezclas e infusiones nuevas que aún no han sido programadas para un paciente en un ingreso específico, permitiendo al personal de enfermería identificar las preparaciones farmacéuticas pendientes de programar en la estación de mezclas. Consolida cuatro tipos de solicitudes médicas: mezclas de infusión continua, líquidos, mezclas por frecuencia o magistrales, y nutrición parenteral, unificándolas en un solo resultado con sus productos, dosis, instrucciones, fecha de inicio, duración y el nombre del médico que prescribió. Consulta las órdenes de mezclas (HCINFLIQA y HCINFLIQD) y las órdenes de nutrición parenteral (HCNUTPAREC), cruzando con el maestro de profesionales de la salud (INPROFSAL) para obtener el nombre del prescriptor. Recibe como parámetros la cédula del paciente y el número de ingreso, y excluye las mezclas ya programadas o anuladas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarMezclasNuevasProgramar';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_ENF_ListarMezclasNuevasProgramar';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las prescripciones de mezclas, líquidos y nutrición parenteral pendientes de programar para un paciente en un ingreso, clasificándolas por tipo y excluyendo las anuladas o ya programadas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasNuevasProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse el código del paciente y el número de ingreso; Deben existir prescripciones en HCINFLIQA o nutriciones parenterales en HCNUTPAREC asociadas al paciente/ingreso; Las prescripciones de mezcla deben tener un profesional prescriptor válido en INPROFSAL; Para mezclas continuas y de líquidos debe existir el detalle correspondiente en HCINFLIQD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasNuevasProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan prescripciones aún no programadas (MEDPROGRAMADO = 0 o NULL) para las fuentes de HCINFLIQA; Se excluyen prescripciones con PREESTADO = 4 (estado anulado/cancelado); Para Nutrición Parenteral solo se incluyen registros con STATUS = 1 (activos); El resultado siempre está acotado al paciente e ingreso indicados; Los productos de Nutrición Parenteral provienen exclusivamente de ProductSusceptibleMixingStation cuyo Origin sea ''HCNUTPAREC''; Las cuatro categorías (mezcla continua, frecuencia/magistral, líquidos, nutrición parenteral) se devuelven unificadas en un mismo conjunto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasNuevasProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Mezcla de infusión continua; Mezcla de infusión por frecuencia; Mezcla magistral; Líquidos endovenosos; Bolo de medicamento; Nutrición parenteral; Prescripción médica; Programación de medicamentos; Estación de mezclas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasNuevasProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCINFLIQA: Cuando TIPMEZLIQ=1, MEDPROGRAMADO IN (0,NULL) y PREESTADO<>4, retorna la prescripción como ''Mezcla Infusión Continua'' con duración derivada de HCINFLIQD según METAPLMED; [RETURN_RESULT] dbo.HCINFLIQA: Cuando TIPMEZLIQ IN (3,4), MEDPROGRAMADO IN (0,NULL) y PREESTADO<>4, retorna la prescripción etiquetada como ''Infusion Mezcla Frecuencia'' o ''Mezcla Magistral'' según corresponda; [RETURN_RESULT] dbo.HCINFLIQA: Cuando TIPMEZLIQ=2, MEDPROGRAMADO IN (0,NULL) y PREESTADO<>4, retorna la prescripción como ''Líquidos'' con etiqueta ''Infusion Líquidos'' o ''Bolo Medicamento Líquidos'' según METAPLMED; [RETURN_RESULT] dbo.HCNUTPAREC: Cuando HCNUTPAREC.STATUS=1 y existe producto vinculado en ProductSusceptibleMixingStation con Origin=''HCNUTPAREC'', retorna la orden como ''Nutrición Parenteral'' con TIPMEZLIQ=5 y METAPLMED=6', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasNuevasProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPMEZLIQ = 1 → Se clasifica como Mezcla Infusión Continua y la duración se toma de HCINFLIQD.DURACIINFM cuando METAPLMED=2; si METAPLMED=1 o 3 la duración es ''Dosis única''; si TIPMEZLIQ IN (3,4) → Se clasifica como Mezcla Infusión Frecuencia (3) o Mezcla Magistral (4); la duración se toma de TIPODURACION y la fecha inicio de INICIOAPLICACION; si TIPMEZLIQ = 2 → Se clasifica como Líquidos; si METAPLMED=4 ''Infusion Líquidos'' con duración HCINFLIQD.DURACIINF; si METAPLMED=5 ''Bolo Medicamento Líquidos'' con duración ''Dosis única''; si Origen HCNUTPAREC con STATUS=1 → Se clasifica como Nutrición Parenteral (TIPMEZLIQ=5, METAPLMED=6); duración ''Continua'' si TIPOINFUSION=1, ''Ciclada'' si TIPOINFUSION=2', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasNuevasProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCINFLIQA; dbo.INPROFSAL; dbo.HCINFLIQD; dbo.HCNUTPAREC; MedicalHistory.ProductSusceptibleMixingStation', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasNuevasProgramar';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_ENF_ListarMezclasNuevasProgramar';
-- GO
