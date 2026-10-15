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