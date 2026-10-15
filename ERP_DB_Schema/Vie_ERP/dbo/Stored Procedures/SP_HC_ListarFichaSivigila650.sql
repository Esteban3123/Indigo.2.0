

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