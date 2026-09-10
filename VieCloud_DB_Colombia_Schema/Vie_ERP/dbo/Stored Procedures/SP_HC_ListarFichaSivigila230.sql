
-- Stored Procedure
-- =============================================
-- Autor:		Jean Carlos Roldan Lozano
-- Fecha Creación: 01-11-2018
-- Descripción:	Sp que me lista la Información de las Fichas del Sivigila, Ficha 230
-- Modificó:    Yezid Garcia Medina
-- Fecha Modificación : 03-02-2022
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila230]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;

			Select
				CASE CONTACASO  WHEN '1' THEN 'X' END AS 'CONTACASO Si',CASE CONTACASO WHEN '2' THEN 'X' END AS 'CONTACASO No', CASE CONTACASO WHEN '3' THEN 'X' END AS 'CONTACASO Desconocido',
				CASE DOSISAPLI  WHEN 1 THEN 'X' END AS 'Ninguna', 
				CASE DOSISAPLI  WHEN 2 THEN 'X' END AS 'Una', 
				CASE DOSISAPLI  WHEN 3 THEN 'X' END AS 'Dos',
				CASE DOSISAPLI  WHEN 4 THEN 'X' END AS 'Tres o Más', 
				CASE DOSISAPLI  WHEN 5 THEN 'X' END AS 'Primer_Refuerzo', 
				CASE DOSISAPLI  WHEN 6 THEN 'X' END AS 'Segundo_Refuerzo',
				CASE FIEBRE  WHEN '1' THEN 'X' END AS 'FIEBRE Si',CASE FIEBRE WHEN '2' THEN 'X' END AS 'FIEBRE No', CASE FIEBRE WHEN '3' THEN 'X' END AS 'FIEBRE Desconocido',
				CASE AMIGDALITIS  WHEN '1' THEN 'X' END AS 'AMIGDALITIS Si',CASE AMIGDALITIS WHEN '2' THEN 'X' END AS 'AMIGDALITIS No', CASE AMIGDALITIS WHEN '3' THEN 'X' END AS 'AMIGDALITIS Desconocido',
				CASE FARINGITIS  WHEN '1' THEN 'X' END AS 'FARINGITIS Si',CASE FARINGITIS WHEN '2' THEN 'X' END AS 'FARINGITIS No', CASE FARINGITIS WHEN '3' THEN 'X' END AS 'FARINGITIS Desconocido',
				CASE LARINGITIS  WHEN '1' THEN 'X' END AS 'LARINGITIS Si',CASE LARINGITIS WHEN '2' THEN 'X' END AS 'LARINGITIS No', CASE LARINGITIS WHEN '3' THEN 'X' END AS 'LARINGITIS Desconocido',
				CASE PRESEMEMB  WHEN '1' THEN 'X' END AS 'PRESEMEMB Si',CASE PRESEMEMB WHEN '2' THEN 'X' END AS 'PRESEMEMB No', CASE PRESEMEMB WHEN '3' THEN 'X' END AS 'PRESEMEMB Desconocido',
				CASE COMPLICA  WHEN '1' THEN 'X' END AS 'COMPLICA Si',CASE COMPLICA WHEN '2' THEN 'X' END AS 'COMPLICA No', CASE COMPLICA WHEN '3' THEN 'X' END AS 'COMPLICA Desconocido',
				CASE TIPOCOMPLI  WHEN '1' THEN 'X' END AS 'Neurologica',CASE TIPOCOMPLI WHEN '2' THEN 'X' END AS 'Renal', CASE TIPOCOMPLI WHEN '3' THEN 'X' END AS 'Cardiaca',
				CASE TIPOCOMPLI  WHEN '4' THEN 'X' END AS 'Traquetomia',CASE TIPOCOMPLI WHEN '5' THEN 'X' END AS 'Otro',
				convert(varchar(10),FECHATOMA,103) As 'FECHATOMA', convert(varchar(10),FECHARECE,103) As 'FECHARECE', Rtrim(MUESTRA) As 'MUESTRA',
				Rtrim(PRUEBA) As 'PRUEBA', Rtrim(AGENTE) As 'AGENTE', Rtrim(RESULTADO) As 'RESULTADO', 
				convert(varchar(10),FECHARESUL,103) As 'FECHARESUL', Rtrim(VALOR) As 'VALOR', 
				VERSION AS 'VERSION', 
				JSON AS 'JSON',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.CARNET_VACUNACION') AS BIT ) WHEN 1 THEN 'X' END AS 'Carnet_SI', 
				CASE TRY_CAST( JSON_VALUE(JSON,'$.CARNET_VACUNACION') AS BIT ) WHEN 0 THEN 'X' END AS 'Carnet_NO',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.TIPO_VACUNA') AS int ) WHEN 1 THEN 'X' END AS 'Tipo_DPT',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.TIPO_VACUNA') AS int ) WHEN 2 THEN 'X' END AS 'Tipo_Pentavalente',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.TIPO_VACUNA') AS int ) WHEN 3 THEN 'X' END AS 'Tipo_TD',
				CASE TRY_CAST( JSON_VALUE(JSON,'$.TIPO_VACUNA') AS int ) WHEN 4 THEN 'X' END AS 'Tipo_Otra',
				RTRIM( JSON_VALUE(JSON,'$.OTRA_VACUNA') ) AS 'Otra_Vacuna',	
				FORMAT( TRY_CONVERT( date, JSON_VALUE(JSON,'$.FECHA_ULTIMA_DOSIS') ),'dd/MM/yyyy') As 'Fecha_Ultima_Dosis'        
			From 
				HCFICHA230 
			Where 
				IDFICHANOTIFICACION  = @IdFicha 
				AND ISJSON(JSON) > 0	
				
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta y lista la información clínica y epidemiológica registrada en la Ficha 230 de SIVIGILA, correspondiente a la notificación de casos de difteria u otras enfermedades inmunoprevenibles con síntomas respiratorios. Recupera datos del paciente como contacto con el caso, dosis de vacuna aplicadas, presencia de síntomas (fiebre, amigdalitis, faringitis, laringitis, presencia de membranas), complicaciones y su tipo (neurológica, renal, cardíaca, traqueotomía), así como resultados de laboratorio (tipo de muestra, prueba, agente, resultado y fechas de toma, recepción y resultado). Además, extrae información extendida almacenada en formato JSON dentro de la ficha, como carnet de vacunación, tipo de vacuna aplicada (DPT, pentavalente, TD u otra) y la fecha de la última dosis. Se utiliza para imprimir o visualizar la ficha oficial de notificación epidemiológica SIVIGILA 230 a partir del identificador de ficha y número de ficha.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila230';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila230';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista y formatea la información clínica y epidemiológica de la ficha 230 de notificación al Sivigila (difteria), traduciendo códigos a marcas de selección y extrayendo datos del esquema JSON de vacunación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila230';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un identificador de ficha de notificación válido; El registro en HCFICHA230 debe tener el campo JSON con contenido JSON válido para ser retornado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila230';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo retorna filas cuyo campo JSON sea un JSON válido (ISJSON(JSON) > 0); Las fechas se formatean al estilo dd/MM/yyyy (formato 103 o FORMAT); Los códigos numéricos de variables clínicas se traducen a marcas ''X'' en columnas categóricas (Si/No/Desconocido o tipo de complicación/dosis); Los conteos de dosis aplicadas se mapean a 6 categorías fijas: Ninguna, Una, Dos, Tres o Más, Primer Refuerzo, Segundo Refuerzo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila230';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Sivigila; Ficha 230 (difteria); Vigilancia epidemiológica; Vacunación (DPT, Pentavalente, TD); Carnet de vacunación; Dosis y refuerzos; Síntomas: fiebre, amigdalitis, faringitis, laringitis, pseudomembranas; Complicaciones: neurológica, renal, cardíaca, traqueotomía; Contacto con caso; Muestra de laboratorio (toma, recepción, prueba, agente, resultado)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila230';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA230: Cuando IDFICHANOTIFICACION coincide con el parámetro y el campo JSON es válido, retorna una fila con la ficha 230 transformada para reporte', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila230';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CONTACASO/FIEBRE/AMIGDALITIS/FARINGITIS/LARINGITIS/PRESEMEMB/COMPLICA = ''1'',''2'',''3'' → Se marca con ''X'' la columna correspondiente a Si, No o Desconocido; si DOSISAPLI entre 1 y 6 → Marca ''X'' en Ninguna, Una, Dos, Tres o Más, Primer_Refuerzo o Segundo_Refuerzo según el valor; si TIPOCOMPLI entre ''1'' y ''5'' → Marca ''X'' en Neurológica, Renal, Cardíaca, Traquetomía u Otro; si JSON_VALUE($.CARNET_VACUNACION) cast a BIT = 1 o 0 → Marca ''X'' en Carnet_SI o Carnet_NO respectivamente; si JSON_VALUE($.TIPO_VACUNA) cast a int entre 1 y 4 → Marca ''X'' en Tipo_DPT, Tipo_Pentavalente, Tipo_TD o Tipo_Otra', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila230';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA230', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila230';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila230';
-- GO
