-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,09-07-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas de reactivovigilancia>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CAL_ListarFichaReactivos]
(
  @IdFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;
  declare @FechaActual as date = [Common].[GETDATE]()
	select 
	CASE C.TIPOIDENTIFICA when 1 then 'X' end as 'CedulaCiudadania', case C.TIPOIDENTIFICA when 2 then 'X' end as 'Extranjeria', case C.TIPOIDENTIFICA when 3 then 'X' end as 'Tarjetaidentidad',case C.TIPOIDENTIFICA when 8 then 'X' end as 'NUIP'
	,Case SEXO when 1 then 'X' end as 'Masculino', Case SEXO when 2 then 'X' end as 'Femenino'
	--,[dbo].[EDAD] (C.FECNACIMIENTO,@FechaActual) As 'Edad'
	,c.IPCODPACI as 'IdentificacionPaciente'
	,C.NOMBRECOMP as 'NombrePaciente'
	,C.DIRECCION as 'DireccionPaciente'
	,C.TELEFONO as 'TelefonoPaciente'
	,NOMBREINSTITUCION
	,NIT
	,CASE NATURALEZA WHEN '1' THEN 'X' END AS 'Naturaleza_Publica',CASE NATURALEZA WHEN '2' THEN 'X' END AS 'Naturaleza_Privada',CASE NATURALEZA WHEN '3' THEN 'X' END AS 'Naturaleza_Mixta'
	,CASE NIVELCOMPLEJIDAD WHEN '1' THEN 'X' END AS 'NIvelComple_1',CASE NIVELCOMPLEJIDAD WHEN '2' THEN 'X' END AS 'NIvelComple_2',CASE NIVELCOMPLEJIDAD WHEN '3' THEN 'X' END AS 'NIvelComple_3',CASE NIVELCOMPLEJIDAD WHEN '4' THEN 'X' END AS 'NIvelComple_NA'
	,A.UBICACION 
	,A.DIRECCION
	,A.TELEFONO
	,CORREO 
	---,convert(varchar(20),A.FECHAREPORTE ,103)  as FECHAREPORTE
	,NOMBRECOMERCIAL 
	,REGISTROSANITARIO 
	,LOTE
	,convert(varchar(20),FECHAVENCIMIENTO,103)  as FECHAVENCIMIENTO 
	,CASE PROCEDENCIA WHEN '1' THEN 'X' END AS 'PROCEDENCIA_Nacional',CASE PROCEDENCIA WHEN '2' THEN 'X' END AS 'PROCEDENCIA_Importado'
	,CASE REQUIEREFRIO  WHEN '1' THEN 'X' END AS 'REQUIEREFRIO_SI',CASE REQUIEREFRIO WHEN '0' THEN 'X' END AS 'REQUIEREFRIO_NO'
	,TEMPERATURA
	,NOMBRERAZONSOCIALIMPORTA
	,CASE SERVICIOREACTIVO WHEN '1' THEN 'X' END AS 'ServicioReactivo_LaboratorioClinico',CASE SERVICIOREACTIVO WHEN '2' THEN 'X' END AS 'ServicioReactivo_LaboratorioSaludPublica',CASE SERVICIOREACTIVO WHEN '3' THEN 'X' END AS 'ServicioReactivo_ServicioTrans',CASE SERVICIOREACTIVO WHEN '4' THEN 'X' END AS 'ServicioReactivo_BancoSangre',CASE SERVICIOREACTIVO WHEN '5' THEN 'X' END AS 'ServicioReactivo_Otro'
	,CUALOTROSERVICIO
	,CASE CUMPLIERONCONDICIONES WHEN '1' THEN 'X' END AS 'CumplieronCondiciones_SI',CASE CUMPLIERONCONDICIONES WHEN '0' THEN 'X' END AS 'CumplieronCondiciones_NO' 
	,CASE PRODUCTOCERTIFICADO  WHEN '1' THEN 'X' END AS 'ProductoCertificado_SI',CASE PRODUCTOCERTIFICADO WHEN '0' THEN 'X' END AS 'ProductoCertificado_NO' 
	,convert(varchar(20),FECHAOCURRENCIA,103)  as FECHAOCURRENCIA 
	,convert(varchar(20),FECHAELABORACIONREPORTE,103)  as FECHAELABORACIONREPORTE 
	,CASE DETENCCIONEFECTOINDESEADO WHEN '1' THEN 'X' END AS 'Deteccion_AntesRDIV',CASE DETENCCIONEFECTOINDESEADO WHEN '2' THEN 'X' END AS 'Deteccion_DuranteRDIV',CASE DETENCCIONEFECTOINDESEADO WHEN '3' THEN 'X' END AS 'Deteccion_DespuesRDIV'
	,CASE PROBOEMAREACTIVO  WHEN '1' THEN 'X' END AS 'Problema_Envase',CASE PROBOEMAREACTIVO WHEN '2' THEN 'X' END AS 'Problema_Empaque',CASE PROBOEMAREACTIVO WHEN '3' THEN 'X' END AS 'Problema_Inserto',CASE PROBOEMAREACTIVO WHEN '4' THEN 'X' END AS 'Problema_RegistroSanitario',CASE PROBOEMAREACTIVO WHEN '5' THEN 'X' END AS 'Problema_ErroresImputables'
	,CASE CLASIFICACIONEFECTOINDESEADO WHEN '1' THEN 'X' END AS 'Clasificacion_EventoAdverso',CASE CLASIFICACIONEFECTOINDESEADO WHEN '2' THEN 'X' END AS 'Clasificacion_Incidente'
	,DESCRIPCIONEFECTOINDESEADO 
	,CASE DANOCORPORAL  WHEN '1' THEN 'X' END AS 'DesEnlace_DanoCorporal'
	,CASE MUERTE  WHEN '1' THEN 'X' END AS 'DesEnlace_MUERTE'
	,CASE RETRASO WHEN '1' THEN 'X' END AS 'DesEnlace_RETRASO'
	,CASE HOSPITALIZACION WHEN '1' THEN 'X' END AS 'DesEnlace_HOSPITALIZACION'
	,CASE PRESCRIPCION WHEN '1' THEN 'X' END AS 'DesEnlace_PRESCRIPCION'
	,CASE TRATAMIENTOINAPROP WHEN '1' THEN 'X' END AS 'DesEnlace_TRATAMIENTOINAPROP'
	,CASE TRANSFUSIONBIOLO WHEN '1' THEN 'X' END AS 'DesEnlace_TRANSFUSIONBIOLO'
	,CASE INTERVENMEDICA WHEN '1' THEN 'X' END AS 'DesEnlace_INTERVENMEDICA'
	,CASE INTERVENQX WHEN '1' THEN 'X' END AS 'DesEnlace_INTERVENQX'
	,CASE INTERVENPSICOLO WHEN '1' THEN 'X' END AS 'DesEnlace_INTERVENPSICOLO'
	,CASE DIAGNOSINCORREC WHEN '1' THEN 'X' END AS 'DesEnlace_DIAGNOSINCORREC'
	,CASE OTRA WHEN '1' THEN 'X' END AS 'DesEnlace_OTRA'
	,CUALOTRODESENLACE 
	,CASE DETECTOCAUSA WHEN '1' THEN 'X' END AS 'DETECTOCAUSA_SI',CASE DETECTOCAUSA WHEN '0' THEN 'X' END AS 'DETECTOCAUSA_NO'
	,CAUSAPROBABLE 
	,CASE NOTIFICOIMPORTADOR WHEN '1' THEN 'X' END AS 'NOTIFICOIMPORTADOR_SI',CASE NOTIFICOIMPORTADOR WHEN '0' THEN 'X' END AS 'NOTIFICOIMPORTADOR_NO'
	,CASE NOTIFICOFABRICANTE WHEN '1' THEN 'X' END AS 'NOTIFICOFABRICANTE_SI',CASE NOTIFICOFABRICANTE WHEN '0' THEN 'X' END AS 'NOTIFICOFABRICANTE_NO'
	,CASE NOTIFICOCOMERCIALIZA WHEN '1' THEN 'X' END AS 'NOTIFICOCOMERCIALIZA_SI',CASE NOTIFICOCOMERCIALIZA WHEN '0' THEN 'X' END AS 'NOTIFICOCOMERCIALIZA_NO'
	,CASE NOTIFICODISTRIBUIDOR WHEN '1' THEN 'X' END AS 'NOTIFICODISTRIBUIDOR_SI',CASE NOTIFICODISTRIBUIDOR WHEN '0' THEN 'X' END AS 'NOTIFICODISTRIBUIDOR_NO'
	,convert(varchar(20),FECHANOTIFICACION,103)  as FECHANOTIFICACION
	,CASE ENVIADOREACTIVO WHEN '1' THEN 'X' END AS 'ENVIADOREACTIVO_SI',CASE ENVIADOREACTIVO WHEN '0' THEN 'X' END AS 'ENVIADOREACTIVO_NO'
	,CASE INSTIPROGRAMARIESGOS  WHEN '1' THEN 'X' END AS 'INSTIPROGRAMARIESGOS_SI',CASE INSTIPROGRAMARIESGOS  WHEN '0' THEN 'X' END AS 'INSTIPROGRAMARIESGOS_NO'
	,CASE REALIZOTIPOANALISIS  WHEN '1' THEN 'X' END AS 'REALIZOTIPOANALISIS_SI',CASE REALIZOTIPOANALISIS  WHEN '0' THEN 'X' END AS 'REALIZOTIPOANALISIS_NO'
	,CASE HERRAMIENTAUTILIZO  WHEN '1' THEN 'X' END AS 'HerramientaUti_Protocolos',CASE HERRAMIENTAUTILIZO WHEN '2' THEN 'X' END AS 'HerramientaUti_AMFE',CASE HERRAMIENTAUTILIZO WHEN '3' THEN 'X' END AS 'HerramientaUti_EspinaPescado',CASE HERRAMIENTAUTILIZO WHEN '4' THEN 'X' END AS 'HerramientaUti_NA',CASE HERRAMIENTAUTILIZO WHEN '5' THEN 'X' END AS 'HerramientaUti_Otra'
	,CUALOTRAHERRAMIENTA 
	,DESCRIPCIONCAUSA 
	,CASE INICIOACCIONESMEJORA WHEN '1' THEN 'X' END AS 'INICIOACCIONESMEJORA_SI',CASE INICIOACCIONESMEJORA  WHEN '0' THEN 'X' END AS 'INICIOACCIONESMEJORA_NO'
	,ACCIONESMEJORAM as 'CualesAccionesMejora'
	,NOMBREREPORTANTE 
	,IDENTIREPORTANTE 
	,CARGOREPORTA 
	,AREAREPORTA 
	,DIRECCIONREPORTA
	,TELEFONOREPORTA 
	,CELULARREPORTA
	,convert(varchar(20),FECHANOTIREPORTAN,103)  as FECHANOTIREPORTAN
	,Rtrim(E.nomdepart) +' , '+ Rtrim(P.MUNNOMBRE)  as 'Departamento Municipio reportante'
	,Rtrim(z.desactivi) as 'Profesion reportante'
	,Rtrim(E2.nomdepart) +' , '+ Rtrim(P2.MUNNOMBRE)  as 'Departamento Municipio Ocurrencia'
	,B2.UBINOMBRE as 'CiudadOcurrencia'
	,B.UBINOMBRE as 'CiudadReporta'
	,A.CORREOREPORTA
	,CASE AUTORIZADIVULGACION  WHEN '1' THEN 'X' END AS 'AUTORIZADIVULGACION_SI',CASE AUTORIZADIVULGACION  WHEN '0' THEN 'X' END AS 'AUTORIZADIVULGACION_NO'
	from [dbo].[CALREACTIVOVIGILANCIA] A
	Inner Join dbo.CALREPORTE C on A.IDCALREPORTE = C.ID
	left Join dbo.INUBICACI B ON A.UBICACIONREPORTA = B.AUUBICACI
	left Join dbo.INMUNICIP P on B.DEPMUNCOD = P.DEPMUNCOD
	left  Join dbo.INDEPARTA E on P.DEPCODIGO = E.DEPCODIGO 
	left join dbo.ADACTIVID z on A.PROFESIONREPORTA = z.codactivi
	left Join dbo.INUBICACI B2 ON A.UBICACION = B2.AUUBICACI
	left Join dbo.INMUNICIP P2 on B.DEPMUNCOD = P2.DEPMUNCOD
	left Join dbo.INDEPARTA E2 on P.DEPCODIGO = E2.DEPCODIGO 
	where A.IDCALREPORTE = @IdFicha

	--select UBICACIONREPORTA,PROFESIONREPORTA,UBICACION from [dbo].[CALREACTIVOVIGILANCIA] where IDCALREPORTE = 18
	--select * from CALREPORTE where id = '18'
	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una ficha de vigilancia de reactivos de diagnóstico in vitro (RDIV) a partir de su identificador, combinando los datos del reporte de calidad (CALREPORTE) con la información específica del reactivo (CALREACTIVOVIGILANCIA). Consolida datos del paciente involucrado (identificación, nombre, dirección, teléfono, tipo de documento, sexo), de la institución que reporta (nombre, NIT, naturaleza jurídica, nivel de complejidad, municipio y departamento), del reactivo (nombre comercial, registro sanitario, lote, fecha de vencimiento, procedencia, condiciones de conservación), del incidente (fecha de ocurrencia, tipo de efecto indeseado, clasificación, descripción, desenlaces clínicos como muerte, hospitalización, daño corporal, diagnóstico incorrecto, entre otros) y de las acciones tomadas (notificación a importador, fabricante, distribuidor, análisis de causa probable, herramientas de gestión del riesgo utilizadas). Está diseñado para generar o imprimir la ficha oficial de reactivovigilancia exigida por la normativa colombiana de tecnovigilancia y seguridad del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarFichaReactivos';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_CAL_ListarFichaReactivos';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consulta y formatea la información completa de una ficha de reactivovigilancia (reporte de eventos/incidentes con reactivos de diagnóstico in vitro) para visualización tipo formulario con marcas ''X'' por opción seleccionada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaReactivos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en CALREACTIVOVIGILANCIA cuyo IDCALREPORTE coincida con el identificador recibido; Debe existir el reporte asociado en CALREPORTE para obtener datos del paciente y de la institución', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaReactivos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas (FECHAVENCIMIENTO, FECHAOCURRENCIA, FECHAELABORACIONREPORTE, FECHANOTIFICACION, FECHANOTIREPORTAN) se devuelven siempre con formato dd/mm/yyyy; Los catálogos de ubicación, municipio, departamento y profesión se enlazan con LEFT JOIN, por lo que la ficha se devuelve aunque falten esos datos geográficos o de profesión; El paciente y la institución se obtienen por INNER JOIN con CALREPORTE, por lo que sin reporte asociado no se devuelve fila; Los campos de departamento/municipio reportante y de ocurrencia se concatenan como ''Departamento , Municipio'' aplicando RTRIM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaReactivos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reactivovigilancia; Reactivo de diagnóstico in vitro (RDIV); Evento adverso; Incidente; Paciente; Institución prestadora de salud; Nivel de complejidad; Naturaleza jurídica (pública/privada/mixta); Registro sanitario; Lote y fecha de vencimiento del reactivo; Cadena de frío / temperatura; Importador, fabricante, comercializador, distribuidor; Desenlace clínico (daño corporal, muerte, hospitalización, intervención quirúrgica, etc.); Causa probable y acciones de mejora; Herramientas de análisis de causa (AMFE, Espina de Pescado, Protocolos de Londres); Reportante (profesión, ubicación, contacto); Autorización de divulgación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaReactivos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.CALREACTIVOVIGILANCIA: Cuando A.IDCALREPORTE = @IdFicha, retorna una fila con todos los datos de la ficha de reactivovigilancia y del reporte vinculado, formateando fechas como dd/mm/yyyy (estilo 103) y traduciendo códigos a marcas ''X''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaReactivos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si C.TIPOIDENTIFICA = 1/2/3/8 → Marca con ''X'' la columna correspondiente a Cédula, Extranjería, Tarjeta de Identidad o NUIP respectivamente; si SEXO = 1 vs 2 → Marca ''X'' en Masculino o Femenino; si NATURALEZA = 1/2/3 → Clasifica la institución como Pública, Privada o Mixta; si NIVELCOMPLEJIDAD = 1/2/3/4 → Marca el nivel de complejidad de la institución (1, 2, 3 o NA); si PROCEDENCIA = 1 vs 2 → Clasifica el reactivo como Nacional o Importado; si REQUIEREFRIO = 1 vs 0 → Indica si el reactivo requiere refrigeración; si SERVICIOREACTIVO = 1..5 → Clasifica el servicio donde se usa el reactivo (Laboratorio Clínico, Laboratorio Salud Pública, Servicio de Transfusión, Banco de Sangre, Otro); si DETENCCIONEFECTOINDESEADO = 1/2/3 → Indica el momento de detección del efecto indeseado (Antes, Durante o Después del RDIV); si PROBOEMAREACTIVO = 1..5 → Clasifica el problema del reactivo (Envase, Empaque, Inserto, Registro Sanitario, Errores Imputables); si CLASIFICACIONEFECTOINDESEADO = 1 vs 2 → Clasifica el efecto indeseado como Evento Adverso o Incidente; si HERRAMIENTAUTILIZO = 1..5 → Identifica la herramienta de análisis usada (Protocolos, AMFE, Espina de Pescado, NA, Otra); si Banderas DANOCORPORAL/MUERTE/RETRASO/HOSPITALIZACION/PRESCRIPCION/TRATAMIENTOINAPROP/TRANSFUSIONBIOLO/INTERVENMEDICA/INTERVENQX/INTERVENPSICOLO/DIAGNOSINCORREC/OTRA = 1 → Marca ''X'' en el desenlace correspondiente (múltiples desenlaces pueden marcarse simultáneamente); si Banderas de notificación (IMPORTADOR/FABRICANTE/COMERCIALIZA/DISTRIBUIDOR), CUMPLIERONCONDICIONES, PRODUCTOCERTIFICADO, DETECTOCAUSA, ENVIADOREACTIVO, INSTIPROGRAMARIESGOS, REALIZOTIPOANALISIS, INICIOACCIONESMEJORA, AUTORIZADIVULGACION = 1 vs 0 → Marca ''X'' en SI o NO según corresponda', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaReactivos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaReactivos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CALREACTIVOVIGILANCIA; dbo.CALREPORTE; dbo.INUBICACI; dbo.INMUNICIP; dbo.INDEPARTA; dbo.ADACTIVID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaReactivos';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_CAL_ListarFichaReactivos';
-- GO
