-- =============================================
-- Author:		<Author,Juan David Patiño Cabrea,Name>
-- Create date: <Create Date,08-05-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila307]
(
  @IdFicha as Int
)

AS
BEGIN
  SET NOCOUNT ON;

			SELECT 	
				 CASE TIPOAGRESION WHEN '1' THEN 'X' END AS 'TipoAgre_Mordedura',CASE TIPOAGRESION  WHEN '2' THEN 'X' END AS 'TipoAgre_Arañazo',CASE TIPOAGRESION WHEN '3' THEN 'X' END AS 'TipoAgre_ContactoMucosa_SalivaAgresor',CASE TIPOAGRESION  WHEN '4' THEN 'X' END AS 'TipoAgre_ContactoMucosa_VirusRabico',CASE TIPOAGRESION WHEN '5' THEN 'X' END AS 'TipoAgre_inhalacion',CASE TIPOAGRESION  WHEN '6' THEN 'X' END AS 'TipoAgre_Trasplante'
				,CASE AGRESIONPROVOCADA WHEN '1' THEN 'X' END AS 'AGRESIONPROVOCADA_SI',CASE AGRESIONPROVOCADA  WHEN '0' THEN 'X' END AS 'AGRESIONPROVOCADA_NO'
				,CASE TIPOLESION  WHEN '1' THEN 'X' END AS 'TipoLesion_Unica',CASE TIPOLESION  WHEN '2' THEN 'X' END AS 'TipoLesion_Multiple'
				,CASE PROFUNDIDAD   WHEN '1' THEN 'X' END AS 'Profundidad_Superfi',CASE PROFUNDIDAD   WHEN '2' THEN 'X' END AS 'Profundidad_Profunda'
				,CASE CABEZA WHEN '1' THEN 'X' END AS 'Localizacion_Cabeza'
				,CASE MANOS WHEN '1' THEN 'X' END AS 'Localizacion_MANOS'
				,CASE TRONCO WHEN '1' THEN 'X' END AS 'Localizacion_TRONCO'
				,CASE MIEMBROSSUPERI WHEN '1' THEN 'X' END AS 'Localizacion_MIemSuperiores'
				,CASE MIEMBROSINFERI WHEN '1' THEN 'X' END AS 'Localizacion_MIemInferiores'
				,CASE PIES WHEN '1' THEN 'X' END AS 'Localizacion_PIES'
				,CASE GENITALESEXTERN WHEN '1' THEN 'X' END AS 'Localizacion_GENITALES'
				,CONVERT(varchar(10),FECHAAGRESION,103) as 'FechaAgresion'
				,CASE ESPECIEAGRESORA WHEN 1 THEN 'X' END AS 'EspAgresora_Perro',CASE ESPECIEAGRESORA  WHEN 2 THEN 'X' END AS 'EspAgresora_Gato',CASE ESPECIEAGRESORA WHEN 3 THEN 'X' END AS 'EspAgresora_Bovino',CASE ESPECIEAGRESORA  WHEN 4 THEN 'X' END AS 'EspAgresora_Equino',CASE ESPECIEAGRESORA WHEN 5 THEN 'X' END AS 'EspAgresora_Procino',CASE ESPECIEAGRESORA  WHEN 6 THEN 'X' END AS 'EspAgresora_Murcielago',CASE ESPECIEAGRESORA WHEN 7 THEN 'X' END AS 'EspAgresora_Zorro',CASE ESPECIEAGRESORA  WHEN 8 THEN 'X' END AS 'EspAgresora_Mico',CASE ESPECIEAGRESORA WHEN 9 THEN 'X' END AS 'EspAgresora_Humano', CASE ESPECIEAGRESORA WHEN 11 THEN 'X' END AS 'EspAgresora_OtrosSilvestres',CASE ESPECIEAGRESORA  WHEN 12 THEN 'X' END AS 'EspAgresora_Ovino_Caprino',CASE ESPECIEAGRESORA WHEN 13 THEN 'X' END AS 'EspAgresora_GrandesRoedores'
				,CASE VACUNADO WHEN '1' THEN 'X' END AS 'Vacunado_SI',CASE VACUNADO   WHEN '2' THEN 'X' END AS 'Vacunado_NO',CASE VACUNADO   WHEN '3' THEN 'X' END AS 'Vacunado_Desconocido'
				,CONVERT(varchar(10),FECHAVACUNACION,103) as 'FechaVacunacion'
				,CASE PRESENTOCARNE  WHEN '1' THEN 'X' END AS 'PresentoCarnet_SI',CASE PRESENTOCARNE   WHEN '2' THEN 'X' END AS 'PresentoCarnet_NO',CASE PRESENTOCARNE   WHEN '3' THEN 'X' END AS 'PresentoCarnet_Desconocido'
				,NOMBREPROPIETARIO 
				,DIRECCIONPROPIETAR 
				,TELEFPROPIETARIO 
				,CASE ESTADOANIMAL WHEN '1' THEN 'X' END AS 'EstadoAnimal_ConRabia',CASE ESTADOANIMAL   WHEN '2' THEN 'X' END AS 'EstadoAnimal_SinRabia',CASE ESTADOANIMAL   WHEN '3' THEN 'X' END AS 'EstadoAnimal_Desconocido'
				,CASE UBICACION  WHEN 1 THEN 'X' END AS 'Ubicacion_Observable',CASE UBICACION   WHEN 2 THEN 'X' END AS 'Ubicacion_Perdido'
				,CASE SUEROANTIRRABICO WHEN '1' THEN 'X' END AS 'SueroAnti_SI',CASE SUEROANTIRRABICO   WHEN '2' THEN 'X' END AS 'SueroAnti_NO',CASE SUEROANTIRRABICO   WHEN '3' THEN 'X' END AS 'SueroAnti_Nosabe'
				,CONVERT(varchar(10),FECHAAPLICACION,103) as 'FechaAplicacion'
				,CASE VACUNAANTIRRA WHEN '1' THEN 'X' END AS 'VacunaAnti_SI',CASE VACUNAANTIRRA   WHEN '2' THEN 'X' END AS 'VacunaAnti_NO',CASE VACUNAANTIRRA   WHEN '3' THEN 'X' END AS 'VacunaAnti_Nosabe'
				,NUMERODOSIS 
				,CONVERT(varchar(10),FECHAULTIMADOSIS,103) as 'FechaUltimaDosis'
				,CASE LAVADOHERIDAD WHEN '1' THEN 'X' END AS 'LavadoHerida_SI',CASE LAVADOHERIDAD  WHEN '0' THEN 'X' END AS 'LavadoHerida_NO'
				,CASE ORDENOSUERO  WHEN '1' THEN 'X' END AS 'OrdenoSuero_SI',CASE ORDENOSUERO  WHEN '0' THEN 'X' END AS 'OrdenoSuero_NO'
				,CASE ORDENOAPLICACION WHEN '1' THEN 'X' END AS 'OrdenoAplicacion_SI',CASE ORDENOAPLICACION  WHEN '0' THEN 'X' END AS 'OrdenoAplicacion_NO'
				,CASE FIEBRE WHEN '1' THEN 'X' END AS 'Signos_FIEBRE'
				,CASE HIPOREXIA  WHEN '1' THEN 'X' END AS 'Signos_HIPOREXIA'
				,CASE CEFALEA  WHEN '1' THEN 'X' END AS 'Signos_CEFALEA'
				,CASE VOMITO  WHEN '1' THEN 'X' END AS 'Signos_VOMITO'
				,CASE PARESIAS  WHEN '1' THEN 'X' END AS 'Signos_PARESIAS'
				,CASE PARESTESIAS  WHEN '1' THEN 'X' END AS 'Signos_PARESTESIAS'
				,CASE DISFAGIA  WHEN '1' THEN 'X' END AS 'Signos_DISFAGIA'
				,CASE ODINOFAGIA  WHEN '1' THEN 'X' END AS 'Signos_ODINOFAGIA'
				,CASE ARREFLEXIA  WHEN '1' THEN 'X' END AS 'Signos_ARREFLEXIA'
				,CASE ALUCINACIONES  WHEN '1' THEN 'X' END AS 'Signos_ALUCINACIONES'
				,CASE EXPRETERROR  WHEN '1' THEN 'X' END AS 'Signos_EXPRETERROR'
				,CASE SIALORREA  WHEN '1' THEN 'X' END AS 'Signos_SIALORREA'
				,CASE AEROFOBIA  WHEN '1' THEN 'X' END AS 'Signos_AEROFOBIA'
				,CASE HIDROFOBIA  WHEN '1' THEN 'X' END AS 'Signos_HIDROFOBIA'
				,CASE TRANQUILIDAD  WHEN '1' THEN 'X' END AS 'Signos_TRANQUILIDAD'
				,CASE DEPRESION  WHEN '1' THEN 'X' END AS 'Signos_DEPRESION'
				,CASE HIPEREXITABILIDAD  WHEN '1' THEN 'X' END AS 'Signos_HIPEREXITABILIDAD'
				,CASE AGRESIVIDAD  WHEN '1' THEN 'X' END AS 'Signos_AGRESIVIDAD'
				,CASE ESPASMOSMUSCUL  WHEN '1' THEN 'X' END AS 'Signos_ESPASMOSMUSCUL'
				,CASE CONVULSIONES  WHEN '1' THEN 'X' END AS 'Signos_CONVULSIONES'
				,CASE PARALISIS  WHEN '1' THEN 'X' END AS 'Signos_PARALISIS'
				,CASE CRISISRESPIRATORIA  WHEN '1' THEN 'X' END AS 'Signos_CRISISRESPIRATORIA'
				,CASE COMA WHEN '1' THEN 'X' END AS 'Signos_COMA'
				,CASE PAROCARDIORESPIRATO  WHEN '1' THEN 'X' END AS 'Signos_PAROCARDIORESPIRATO'		
				,CASE PRUEDIAGNOSTICA WHEN 1 THEN 'X' END AS 'PruebaDiagnostica_IFD',CASE PRUEDIAGNOSTICA  WHEN 2 THEN 'X' END AS 'PruebaDiagnostica_PruebaBiologica',CASE PRUEDIAGNOSTICA  WHEN 3 THEN 'X' END AS 'PruebaDiagnostica_Histopatologia',CASE PRUEDIAGNOSTICA  WHEN 4 THEN 'X' END AS 'PruebaDiagnostica_Inmunohistoqumica', CASE PRUEDIAGNOSTICA  WHEN 5 THEN 'X' END AS 'PruebaDiagnostica_Titulacion_AA'
				,CASE RESULTADO WHEN 1 THEN 'X' END AS 'Resultado_Positivo',CASE RESULTADO  WHEN 2 THEN 'X' END AS 'Resultado_Negativo', CASE RESULTADO  WHEN 4 THEN 'X' END AS 'Resultado_Pendiente'
				,CASE IDENTIFICACIONVARIANTE WHEN '1' THEN 'X' END AS 'IdentificacionVariante_SI',CASE IDENTIFICACIONVARIANTE  WHEN '2' THEN 'X' END AS 'IdentificacionVariante_NO',CASE IDENTIFICACIONVARIANTE  WHEN '3' THEN 'X' END AS 'IdentificacionVariante_Pendiente'
				,CASE VARIANTEIDENTIFICADA WHEN '1' THEN 'X' END AS 'Variante_Uno',CASE VARIANTEIDENTIFICADA  WHEN '2' THEN 'X' END AS 'Variante_Tres',CASE VARIANTEIDENTIFICADA  WHEN '3' THEN 'X' END AS 'Variante_Cuatro',CASE VARIANTEIDENTIFICADA WHEN '4' THEN 'X' END AS 'Variante_Cinco',CASE VARIANTEIDENTIFICADA  WHEN '5' THEN 'X' END AS 'Variante_Ocho',CASE VARIANTEIDENTIFICADA  WHEN '6' THEN 'X' END AS 'Variante_Atipica',CASE VARIANTEIDENTIFICADA WHEN '7' THEN 'X' END AS 'Variante_Otra'
				,OTRA 
				,CONVERT(varchar(10),FECHARESULTADOLABOR,103) as 'FechaResultadoUltimoLaboratorio'
				,CASE INFORAMCIONLABORATORIO  WHEN '1' THEN 'X' END AS 'InfoLaboratorio_SI',CASE INFORAMCIONLABORATORIO  WHEN '2' THEN 'X' END AS 'InfoLaboratorio_NO'
				,CASE AREAMORDEDURA  WHEN 1 THEN 'X' END AS 'Mordedura_Area_Cubierta',CASE AREAMORDEDURA  WHEN 2 THEN 'X' END AS 'Mordedura_Area_Descubierta' 
				, VERSION AS 'VERSION' 
				, JSON AS 'JSON'  
				, CASE JSON_VALUE(JSON,'$.ESTADO_ANIMAL_CONSULTA') WHEN 1 THEN 'X' END AS 'EAC_Vivo', CASE JSON_VALUE(JSON,'$.ESTADO_ANIMAL_CONSULTA') WHEN 2 THEN 'X' END AS 'EAC_Muerto',  CASE JSON_VALUE(JSON,'$.ESTADO_ANIMAL_CONSULTA') WHEN 3 THEN 'X' END AS 'EAC_Desconocido' 
								

			FROM 
				HCFICHA307
	
			WHERE 
				IDFICHANOTIFICACION  = @IdFicha
				AND ISJSON(JSON) > 0
		
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y formatea el detalle completo de una ficha SIVIGILA 307 (notificación obligatoria de exposición rábica o agresión animal) a partir de su identificador. Extrae de la tabla HCFICHA307 todos los campos clínicos y epidemiológicos de la atención: tipo y circunstancias de la agresión (mordedura, arañazo, contacto mucosa), localización corporal de las lesiones, datos del animal agresor y su propietario, estado vacunal antirrábico del animal, tratamiento aplicado al paciente (suero y vacuna antirrábica, lavado de herida) y signos y síntomas clínicos de rabia (fiebre, hidrofobia, convulsiones, coma, entre otros). Cada campo codificado se convierte a etiquetas legibles (por ejemplo, tipo de especie agresora: perro, gato, murciélago, humano) listas para imprimir o reportar en el formato oficial de vigilancia epidemiológica del SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila307';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila307';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la información detallada de una ficha de notificación SIVIGILA 307 (agresión animal/rabia) transformando códigos a marcas ''X'' para presentación en formato de ficha epidemiológica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila307';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La ficha identificada debe existir en HCFICHA307.; La columna JSON de la ficha debe contener un JSON válido (ISJSON(JSON) > 0); de lo contrario no se retornan filas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila307';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas (agresión, vacunación, aplicación, última dosis, resultado de laboratorio) se formatean a varchar(10) en estilo 103 (dd/mm/yyyy).; Sólo se devuelven filas cuyo campo JSON sea JSON válido.; La salida es de solo lectura: el procedimiento no modifica datos.; El mapeo de banderas binarias usa ''X'' para el valor positivo y NULL en cualquier otro caso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila307';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha SIVIGILA 307; Agresión animal; Rabia; Vacunación antirrábica; Suero antirrábico; Especie agresora; Localización de lesión; Signos y síntomas neurológicos; Pruebas diagnósticas de rabia; Variante de virus rábico; Estado del animal en consulta; Notificación epidemiológica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila307';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA307: Cuando IDFICHANOTIFICACION = @IdFicha AND ISJSON(JSON) > 0, retorna un único registro con los campos de la ficha mapeados a marcas ''X'' por código.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila307';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOAGRESION = 1..6 → Marca ''X'' en la columna correspondiente: Mordedura, Arañazo, Contacto Mucosa Saliva, Contacto Mucosa Virus Rábico, Inhalación o Trasplante.; si ESPECIEAGRESORA IN (1..9,11,12,13) → Marca la especie agresora (Perro, Gato, Bovino, Equino, Porcino, Murciélago, Zorro, Mico, Humano, Otros Silvestres, Ovino/Caprino, Grandes Roedores).; si VACUNADO/PRESENTOCARNE/SUEROANTIRRABICO/VACUNAANTIRRA/IDENTIFICACIONVARIANTE = 1/2/3 → Marca SI / NO / Desconocido (o Pendiente) según el código.; si ESTADOANIMAL = 1/2/3 → Marca Con Rabia / Sin Rabia / Desconocido.; si UBICACION = 1/2 → Marca Observable o Perdido.; si PRUEDIAGNOSTICA = 1..5 → Marca la prueba diagnóstica: IFD, Prueba Biológica, Histopatología, Inmunohistoquímica o Titulación AA.; si RESULTADO = 1/2/4 → Marca Positivo, Negativo o Pendiente.; si VARIANTEIDENTIFICADA = 1..7 → Marca la variante identificada (Uno, Tres, Cuatro, Cinco, Ocho, Atípica, Otra).; si AREAMORDEDURA = 1/2 → Marca Área Cubierta o Descubierta.; si JSON_VALUE(JSON,''$.ESTADO_ANIMAL_CONSULTA'') = 1/2/3 → Marca EAC_Vivo, EAC_Muerto o EAC_Desconocido.; si AGRESIONPROVOCADA / LAVADOHERIDAD / ORDENOSUERO / ORDENOAPLICACION = 1 o 0 → Marca SI cuando vale 1 y NO cuando vale 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila307';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA307', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila307';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila307';
-- GO
