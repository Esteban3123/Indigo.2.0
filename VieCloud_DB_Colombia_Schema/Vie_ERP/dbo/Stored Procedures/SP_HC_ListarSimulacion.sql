CREATE PROCEDURE [dbo].[SP_HC_ListarSimulacion]
(
  @Identificacion as varchar(25),
  @IdordenRadio as integer
)

AS
BEGIN
  SET NOCOUNT ON;
 
		select case S.protocolo when 1 then 'Cráneo' when 2 then 'Cabeza y cuello' when 3 then 'Abdomen' when 4 then 'Tórax' when 5 then 'Pelvis' when 6 then 'Extremidades' when 7 then 'Radiocirugía' when 8 then 'Otros ' END as 'Protocolo',
		S.OTROPROTOCOLO,
		case S.SOPPOPLITEO when 1 then 'X' else '' END as 'Popliteo',
		case S.SOPDUALLEG when 1 then 'X' else '' END as 'Dualleg',
		case S.SOPOVERLAY when 1 then 'X' else '' END as 'Overlay',
		case S.SOPCARAPRONO when 1 then 'X' else '' END as 'Caraprono',
		case S.SOPCOJIN when 1 then 'X' else '' END as 'Cojinprono',
		case S.SOPWING when 1 then 'X' else '' END as 'Wing',
		case S.SOPRETRACTOR when 1 then 'X' else '' END as 'Retractorhombros',
		case S.SOPHOLDER when 1 then 'X' else '' END as 'Holderprono',
		case S.SOPMARCO when 1 then 'X' else '' END as 'Marcoestereotaxico',
		case S.SOPPIES when 1 then 'X' else '' END as 'Soporte_Pies',
		case S.SOPBELLY when 1 then 'X' else '' END as 'Bellyboard',
		case S.SOPPLANO when 1 then 'X' else '' END as 'Planoinclinado',
		case S.SOPVAC when 1 then 'X' else '' END as 'Vaclok',
		case S.SOPTIMO when 1 then 'X' else '' END as 'Timo',
		case S.SOPMASCARA when 1 then 'X' else '' END as 'Mascara',
		case S.SOPOTROS when 1 then 'X' else '' END as 'Otros ',
		S.OTROSSOPORTE,
		case S.POSICION when 1 then 'Prono' when 2 then 'Supina' when 3 then 'otros' END as 'Posicion',
		S.OTROSPOSICION,
		case S.BRAZOS  when 1 then 'A lo largo del cuerpo' when 2 then 'Sobre la cabeza' when 3 then 'Sobre el pecho' when 4 then 'Otros ' END as 'Brazos',
		S.OTROSBRAZOS,
		case S.PIES  when 1 then 'Hacia Gantry' when 2 then 'Rana' END as 'Pies',
		case S.CABEZA  when 1 then 'Neutra' when 2 then 'Rotación' when 3 then 'Extensión' when 4 then 'Otros ' END as 'Cabeza',
		S.OTROSCABEZA,
		S.BOLUScm,case S.PARAFINA when 1 then 'X' else '' END as 'Parafina', 
		S.WALTURA as 'Wing_Altura',S.WLONGITUD as 'Wing_Longitud',
		S.PINCLINAGENERAL as 'InclinacionGeneral',
		S.PCABEZA as 'Posicion_cabeza',S.PTATTO as 'posicion_tatto',S.PGLUTEOS as 'posicion_gluteos',
		BDERECHOBASE as 'brazoderecho_base',BDERECHOPOSICION as 'brazoderecho_poscion',BDERECHOALTURA as 'brazoderecho_altura',
		BIZQUIBASE as 'brazoizq_base',BIZQUIPOSICION as 'brazoizq_posicion',BIZQUIALTURA as 'brazoizq_altura',
		MPOSCION as 'muneca_posicion',MFIX as 'muneca_Fix',MANGULO as 'muneca_Angulo',
		OBSERVACION, P.NOMCENATE AS 'CENTROATENCION', FECHASIMULA, O.USUARIOSIMULA AS 'DOCUMENTO', Q.NOMMEDICO AS 'USUARIO', Q.MEDIFIRMA AS 'FIRMA'
		from HCRADSIMULACION S 
		INNER JOIN HCRADORDEN O on S.IDHCRADORDEN = O.ID 
		LEFT JOIN ADCENATEN P ON S.CODCENATESIMULACION = P.CODCENATE
		left JOIN INPROFSAL Q ON O.USUARIOSIMULA = Q.CODUSUARI 
		where O.IPCODPACI  = @Identificacion AND S.IDHCRADORDEN = @IdordenRadio
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y devuelve el detalle completo de la simulación de radioterapia asociada a una orden de radiación específica de un paciente. Recibe como parámetros la identificación del paciente y el identificador de la orden, y combina la información de la tabla de simulaciones (HCRADSIMULACION) con la orden de radiación (HCRADORDEN), el centro de atención donde se realizó la simulación (ADCENATEN) y el profesional que la ejecutó (INPROFSAL). Devuelve todos los elementos clínicos y técnicos del proceso de posicionamiento: protocolo de tratamiento (cráneo, tórax, pelvis, etc.), soportes utilizados (vaclok, máscara, bellyboard, marco estereotáxico, entre otros), posición del paciente (prono, supina), configuración de brazos, cabeza y pies, parámetros de bolus y parafina, medidas del wing, referencias de tatuajes y marcas corporales, observaciones, nombre del centro de atención, fecha de simulación y datos del profesional responsable con su firma.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarSimulacion';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarSimulacion';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de simulación de radioterapia (protocolo, soportes, posición, brazos, cabeza, bolus, observaciones y firma del médico) asociados a una orden radioterápica de un paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSimulacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente identificado debe existir en HCRADORDEN con la orden radioterápica indicada; Debe existir un registro de simulación en HCRADSIMULACION asociado a la orden', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSimulacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo devuelve simulaciones cuya orden pertenezca al paciente indicado (cruce IPCODPACI con identificación); El centro de atención y el profesional firmante son opcionales (LEFT JOIN): la simulación se muestra aunque no exista código de centro o usuario simulador en catálogos; Los códigos numéricos de protocolo, posición, brazos, pies y cabeza fuera del rango definido se devuelven como NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSimulacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Simulación de radioterapia; Protocolo de tratamiento (Cráneo, Tórax, Pelvis, Radiocirugía, etc.); Soportes de inmovilización (Vaclok, Máscara, Bellyboard, Marco estereotáxico, Wing, Holder prono, etc.); Posicionamiento del paciente (prono/supina, brazos, pies, cabeza); Bolus y parafina; Centro de atención; Profesional de salud / firma médica; Orden radioterápica; Paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSimulacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCRADSIMULACION: Cuando O.IPCODPACI = @Identificacion AND S.IDHCRADORDEN = @IdordenRadio, retorna los datos de simulación traducidos a etiquetas legibles (protocolo, posición, brazos, cabeza, soportes con ''X'' si están activos)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSimulacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si S.protocolo IN (1..8) → Traduce a etiqueta de protocolo de radioterapia: Cráneo, Cabeza y cuello, Abdomen, Tórax, Pelvis, Extremidades, Radiocirugía u Otros; si S.POSICION IN (1,2,3) → Traduce a Prono / Supina / otros; si S.BRAZOS IN (1..4) → Traduce a ''A lo largo del cuerpo'', ''Sobre la cabeza'', ''Sobre el pecho'' u ''Otros''; si S.PIES IN (1,2) → Traduce a ''Hacia Gantry'' o ''Rana''; si S.CABEZA IN (1..4) → Traduce a Neutra / Rotación / Extensión / Otros; si Cualquier flag SOP* o PARAFINA = 1 → Marca con ''X'', en caso contrario cadena vacía', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSimulacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCRADSIMULACION; dbo.HCRADORDEN; dbo.ADCENATEN; dbo.INPROFSAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSimulacion';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarSimulacion';
-- GO
