

-- Stored Procedure
-- =============================================
-- Autor:		Yezid Garcia Medina
-- Fecha Creación: 15-Febrero-2022
-- Descripción:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 650
-- Modificó:    ---
-- Fecha Modificación : ---
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila650]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 
			Select 
				Rtrim(NOMBEVENTO) As 'NOMBEVENTO', 
				Rtrim(CODEVENTO) As 'CODEVENTO', 
				convert(varchar(10),FECHANOTIFICA,103) As 'FECHANOTIFICA', 
				Rtrim(RAZONSOCIAL) As 'RAZONSOCIAL', 
				Rtrim(UPGD) As 'UPGD', 				
				CASE CLASIFICACION WHEN 1 THEN 'X' END AS 'Clasificacion_Probable', CASE CLASIFICACION WHEN 2 THEN 'X' END AS 'Clasificiacion_Confirmado', 
				Rtrim(PROPIETARIO) As 'PROPIETARIO', 
				Rtrim(RESIDENCIA) As 'RESIDENCIA', 
				Rtrim(TELEFONO) As 'TELEFONO', 
				RTRIM(C.depcodigo) + ' - ' + RTRIM(C.nomdepart) As 'DTOCODRESI', 
				RTRIM(B.MUNCODIGO) + ' - ' + RTRIM(B.MUNNOMBRE) As 'MUNCODRESI', 
				CASE ESPECIE WHEN 1 THEN 'X' END AS 'Especie_Perro', CASE ESPECIE WHEN 2 THEN 'X' END AS 'Especie_Gato',  CASE ESPECIE WHEN 3 THEN 'X' END AS 'Especie_Zorro', CASE ESPECIE WHEN 4 THEN 'X' END AS 'Especie_Murcielago',
				Rtrim(RAZA) As 'RAZA', 
				Rtrim(COLOR) As 'COLOR', 
				EDAD AS 'EDAD', 
				CASE UNIDAD WHEN 1 THEN 'X' END AS 'Unidad_Años', CASE UNIDAD WHEN 2 THEN 'X' END AS 'Unidad_Meses', 
				CASE ANTECEDENTE WHEN 1 THEN 'X' END AS 'Antecedente_SI', CASE ANTECEDENTE WHEN 2 THEN 'X' END AS 'Antecedente_NO', CASE ANTECEDENTE WHEN 3 THEN 'X' END AS 'Antecedente_Desconocido', 
				convert(varchar(10),FECHAVACUNA,103) As 'FECHAVACUNA', 
				CASE AREA WHEN 1 THEN 'X' END AS 'Area_Cabecera', CASE AREA WHEN 2 THEN 'X' END AS 'Area_Centro', CASE AREA WHEN 3 THEN 'X' END AS 'Area_Rural',  
				CASE AGRESIVIDAD WHEN 1 THEN 'X' END AS 'AGRESIVIDAD', 
				CASE PARALISIS WHEN 1 THEN 'X' END AS 'PARALISIS', 
				CASE SALIVACION WHEN 1 THEN 'X' END AS 'SALIVACION', 
				CASE APETITO WHEN 1 THEN 'X' END AS 'APETITO', 
				CASE VORACIDAD WHEN 1 THEN 'X' END AS 'VORACIDAD', 
				CASE DEGLUCION WHEN 1 THEN 'X' END AS 'DEGLUCION', 
				CASE LADRIDO WHEN 1 THEN 'X' END AS 'LADRIDO', 
				CASE MANDIBULA WHEN 1 THEN 'X' END AS 'MANDIBULA', 
				CASE ANISOCORIA WHEN 1 THEN 'X' END AS 'ANISOCORIA', 
				CASE OTROSIGNO WHEN 1 THEN 'X' END AS 'OTROSIGNO', 
				Rtrim(CUALSIGNO) As 'CUALSIGNO', 
				convert(varchar(10),FECHASINTOMA,103) As 'FECHASINTOMA', 
				CASE TIPOMUERTE WHEN 1 THEN 'X' END AS 'Muerte_Espontanea', CASE TIPOMUERTE WHEN 2 THEN 'X' END AS 'Muerte_Sacrificio',  CASE TIPOMUERTE WHEN 3 THEN 'X' END AS 'Muerte_Accidente', CASE TIPOMUERTE WHEN 4 THEN 'X' END AS 'Muerte_Desconocida', 
				convert(varchar(10),FECHAMUERTE,103) As 'FECHAMUERTE', 
				CASE INFOLABORATORIO WHEN 1 THEN 'X' END AS 'InfoLaboratorio_SI', CASE INFOLABORATORIO WHEN 0 THEN 'X' END AS 'InfoLaboratorio_NO',
				convert(varchar(10),FECHATOMA,103) As 'FECHATOMA', 
				convert(varchar(10),FECHAREMISION,103) As 'FECHAREMISION', 
				CASE PRUEBADX WHEN 1 THEN 'X' END AS 'PruebaDx_IFD', CASE PRUEBADX WHEN 2 THEN 'X' END AS 'PruebaDx_Biologica',
				CASE RESULTADO WHEN 1 THEN 'X' END AS 'Resultado_Positivo', CASE RESULTADO WHEN 2 THEN 'X' END AS 'Resultado_Negativo',  CASE RESULTADO WHEN 3 THEN 'X' END AS 'Resultado_Inadecuado', CASE RESULTADO WHEN 4 THEN 'X' END AS 'Resultado_Pendiente',  
				CASE IDENTIFICACION WHEN 1 THEN 'X' END AS 'Identificacion_SI', CASE IDENTIFICACION WHEN 0 THEN 'X' END AS 'Identificacion_NO',
				CASE VARIANTE WHEN 1 THEN 'X' END AS 'Variante_1', CASE VARIANTE WHEN 3 THEN 'X' END AS 'Variante_3',  CASE VARIANTE WHEN 4 THEN 'X' END AS 'Variante_4', CASE VARIANTE WHEN 5 THEN 'X' END AS 'Variante_5', CASE VARIANTE WHEN 8 THEN 'X' END AS 'Variante_8',CASE VARIANTE WHEN 0 THEN 'X' END AS 'Variante_Otra', 
				Rtrim(CUALVARIANTE) As 'CUALVARIANTE', 
				VERSION, 
				JSON 
			
			From 
				HCFICHA650 AS A
				Left Join INMUNICIP AS B ON A.DEPMUNCOD = B.DEPMUNCOD
				Left Join INDEPARTA AS C ON B.DEPCODIGO = C.depcodigo
				
			Where 
				IDFICHANOTIFICACION  = @IdFicha
			   
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle completo de una ficha de notificación epidemiológica SIVIGILA código 650, correspondiente a eventos de agresión por animales potencialmente transmisores de rabia (APTR), a partir del identificador interno de la ficha. Consolida la información del animal agresor (especie, raza, color, edad, signos clínicos como agresividad, parálisis, sialorrea, dificultad para deglutir, entre otros), los antecedentes de vacunación, los datos del propietario y su lugar de residencia (departamento y municipio, obtenidos de los catálogos INDEPARTA e INMUNICIP), y los resultados de laboratorio con tipo de prueba diagnóstica, resultado y variante antigénica identificada. Se utiliza para imprimir o visualizar el formulario oficial de notificación obligatoria ante las autoridades de salud pública, presentando los campos codificados como marcas de selección (X) según las opciones definidas por el SIVIGILA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila650';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila650';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la información completa de una ficha de notificación Sivigila 650 (agresión por animal potencialmente transmisor de rabia), formateando códigos categóricos como marcadores ''X'' y resolviendo nombres de departamento y municipio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila650';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA650 cuyo IDFICHANOTIFICACION coincida con el parámetro de entrada; Para resolver descripciones geográficas, los códigos DEPMUNCOD y DEPCODIGO deben existir en INMUNICIP e INDEPARTA respectivamente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila650';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La consulta filtra siempre por el identificador de la ficha de notificación; Los campos categóricos se transforman a marcadores ''X'' por opción mediante CASE; Las fechas se devuelven en formato dd/mm/yyyy (estilo 103); Se aplica RTRIM a campos de texto para limpiar espacios finales; Las relaciones con municipio y departamento son opcionales (LEFT JOIN), por lo que la ficha se devuelve aun sin geolocalización', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila650';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila 650; Evento de salud pública; Agresión por animal potencialmente transmisor de rabia; UPGD (Unidad Primaria Generadora de Datos); Clasificación del caso (Probable/Confirmado); Especie animal agresor; Antecedente de vacunación animal; Signos clínicos del animal (agresividad, parálisis, salivación, etc.); Tipo de muerte del animal; Pruebas diagnósticas de laboratorio (IFD, Biológica); Variante viral de rabia; Departamento y municipio de residencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila650';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA650: Cuando IDFICHANOTIFICACION = @IdFicha, retorna el conjunto de datos de la ficha 650 con códigos categóricos convertidos a marcadores ''X'' y fechas en formato dd/mm/yyyy', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila650';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CLASIFICACION = 1 → Marca ''X'' en Clasificacion_Probable else CLASIFICACION = 2 marca Clasificiacion_Confirmado; si ESPECIE IN (1,2,3,4) → Marca ''X'' según especie: 1=Perro, 2=Gato, 3=Zorro, 4=Murcielago; si UNIDAD = 1 o 2 → Edad expresada en Años (1) o Meses (2); si ANTECEDENTE IN (1,2,3) → Antecedente vacunal: 1=SI, 2=NO, 3=Desconocido; si AREA IN (1,2,3) → Zona: 1=Cabecera, 2=Centro, 3=Rural; si TIPOMUERTE IN (1,2,3,4) → 1=Espontánea, 2=Sacrificio, 3=Accidente, 4=Desconocida; si INFOLABORATORIO = 1 / 0 → Información de laboratorio SI / NO; si PRUEBADX = 1 o 2 → 1=IFD, 2=Biológica; si RESULTADO IN (1,2,3,4) → 1=Positivo, 2=Negativo, 3=Inadecuado, 4=Pendiente; si IDENTIFICACION = 1 / 0 → Identificación SI / NO; si VARIANTE IN (1,3,4,5,8,0) → Marca variante correspondiente; 0=Otra', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila650';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA650; dbo.INMUNICIP; dbo.INDEPARTA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila650';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila650';
-- GO
