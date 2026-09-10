-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,30-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila452]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 
	Select
		CASE LACERACION  WHEN '1' THEN 'X' END AS 'LACERACION si',CASE LACERACION WHEN '0' THEN 'X' END AS 'LACERACION no',
		CASE CONFUSION  WHEN '1' THEN 'X' END AS 'CONFUSION si',CASE CONFUSION WHEN '0' THEN 'X' END AS 'CONFUSION no',
		CASE QUEMADURA  WHEN '1' THEN 'X' END AS 'QUEMADURA si',CASE QUEMADURA WHEN '0' THEN 'X' END AS 'QUEMADURA no',
		CASE AMPUTACION  WHEN '1' THEN 'X' END AS 'AMPUTACION si',CASE AMPUTACION WHEN '0' THEN 'X' END AS 'AMPUTACION no',
		CASE DANIOCULAR  WHEN '1' THEN 'X' END AS 'DANIOCULAR si',CASE DANIOCULAR WHEN '0' THEN 'X' END AS 'DANIOCULAR no',
		CASE DANIOAUDIT  WHEN '1' THEN 'X' END AS 'DANIOAUDIT si',CASE DANIOAUDIT WHEN '0' THEN 'X' END AS 'DANIOAUDIT no',
		CASE FRACTURAS  WHEN '1' THEN 'X' END AS 'FRACTURAS si',CASE FRACTURAS WHEN '0' THEN 'X' END AS 'FRACTURAS no',
		CASE VIAAEREA  WHEN '1' THEN 'X' END AS 'VIAAEREA si',CASE VIAAEREA WHEN '0' THEN 'X' END AS 'VIAAEREA no',
		CASE TRAUMAABD  WHEN '1' THEN 'X' END AS 'TRAUMAABD si',CASE TRAUMAABD WHEN '0' THEN 'X' END AS 'TRAUMAABD no',
		CASE OTRO1  WHEN '1' THEN 'X' END AS 'OTRO1 si',CASE OTRO1 WHEN '0' THEN 'X' END AS 'OTRO1 no',
		CASE LESIOEFECTALCO  WHEN '1' THEN 'X' END AS 'LESIOEFECTALCO si',CASE LESIOEFECTALCO WHEN '0' THEN 'X' END AS 'LESIOEFECTALCO no',
		CASE MENOREDADSPA  WHEN '1' THEN 'X' END AS 'MENOREDADSPA si',CASE MENOREDADSPA WHEN '0' THEN 'X' END AS 'MENOREDADSPA no',
		CASE CARA WHEN '1' THEN 'X' END AS 'CARA',CASE CUELLO WHEN '1' THEN 'X' END AS 'CUELLO', CASE MANOS WHEN '1' THEN 'X' END AS 'MANOS',
		CASE PIES WHEN '1' THEN 'X' END AS 'PIES',CASE PLIEGUES WHEN '1' THEN 'X' END AS 'PLIEGUES', CASE GENITALES WHEN '1' THEN 'X' END AS 'GENITALES',
		CASE TRONCO WHEN '1' THEN 'X' END AS 'TRONCO',CASE MIEMSUP WHEN '1' THEN 'X' END AS 'MIEMSUP', CASE MIEMINF WHEN '1' THEN 'X' END AS 'MIEMINF',
		CASE DEDOSMANO WHEN '1' THEN 'X' END AS 'DEDOSMANO',CASE MANO WHEN '1' THEN 'X' END AS 'MANO', CASE ANTEBRAZO WHEN '1' THEN 'X' END AS 'ANTEBRAZO',
		CASE BRAZO WHEN '1' THEN 'X' END AS 'BRAZO',CASE MUSLO WHEN '1' THEN 'X' END AS 'MUSLO', CASE PIERNA WHEN '1' THEN 'X' END AS 'PIERNA',
		CASE PIE WHEN '1' THEN 'X' END AS 'PIE',CASE DEDOSPIE WHEN '1' THEN 'X' END AS 'DEDOSPIE', CASE HUESOSCRAN WHEN '1' THEN 'X' END AS 'HUESOSCRAN',
		CASE HUESOSMANO WHEN '1' THEN 'X' END AS 'HUESOSMANO',CASE MIEMBROSUP WHEN '1' THEN 'X' END AS 'MIEMBROSUP', CASE REJACOSTAL WHEN '1' THEN 'X' END AS 'REJACOSTAL',
		CASE COLUMNA WHEN '1' THEN 'X' END AS 'COLUMNA',CASE CADERA WHEN '1' THEN 'X' END AS 'CADERA', CASE MIEMBROINF WHEN '1' THEN 'X' END AS 'MIEMBROINF',
		CASE HUESOSPIE WHEN '1' THEN 'X' END AS 'HUESOSPIE',
		CASE CLASIGRADO WHEN '1' THEN 'X' END AS 'Primgrado',CASE CLASIGRADO WHEN '2' THEN 'X' END AS 'Seggrado', CASE CLASIGRADO WHEN '3' THEN 'X' END AS 'Tercgrado',
		CASE EXTENSION WHEN '1' THEN 'X' END AS 'menor a 5',CASE EXTENSION WHEN '2' THEN 'X' END AS 'del 6 a 14', CASE EXTENSION WHEN '3' THEN 'X' END AS 'mayor del 15',
		Rtrim(OTROCUAL) As 'OTROCUAL', Rtrim(CUALOTROART) As 'CUALOTROART', Rtrim(CUAL1) As 'CUAL1', Rtrim(CUAL2) As 'CUAL2',
		CASE TIPOARTEFLESI WHEN '1' THEN 'X' END AS 'Artepiro',CASE TIPOARTEFLESI WHEN '2' THEN 'X' END AS 'minaanti', CASE TIPOARTEFLESI WHEN '3' THEN 'X' END AS 'municiones',
		CASE TIPOARTEPIRO WHEN '1' THEN 'X' END AS 'cohetes',CASE TIPOARTEPIRO WHEN '2' THEN 'X' END AS 'globos', CASE TIPOARTEPIRO WHEN '3' THEN 'X' END AS 'pito',
		CASE TIPOARTEPIRO WHEN '4' THEN 'X' END AS 'totes',CASE TIPOARTEPIRO WHEN '5' THEN 'X' END AS 'volcanes', CASE TIPOARTEPIRO WHEN '6' THEN 'X' END AS 'voladores',
		CASE TIPOARTEPIRO WHEN '7' THEN 'X' END AS 'lucesbeng',CASE TIPOARTEPIRO WHEN '8' THEN 'X' END AS 'juegospiro', CASE TIPOARTEPIRO WHEN '9' THEN 'X' END AS 'sindato',
		CASE TIPOARTEPIRO WHEN '10' THEN 'X' END AS 'otro',
		CASE LUGAREVENTO WHEN '1' THEN 'X' END AS 'Vivienda',CASE LUGAREVENTO WHEN '2' THEN 'X' END AS 'viapubli', CASE LUGAREVENTO WHEN '3' THEN 'X' END AS 'parquepu',
		CASE LUGAREVENTO WHEN '4' THEN 'X' END AS 'lugtrabj',CASE LUGAREVENTO WHEN '5' THEN 'X' END AS 'zonarural', CASE LUGAREVENTO WHEN '6' THEN 'X' END AS 'sindato2',
		CASE LUGAREVENTO WHEN '7' THEN 'X' END AS 'otro2',
		CASE POLVORAPIRO WHEN '1' THEN 'X' END AS 'almacen',CASE POLVORAPIRO WHEN '2' THEN 'X' END AS 'transporte', CASE POLVORAPIRO WHEN '3' THEN 'X' END AS 'fabricaci',
		CASE POLVORAPIRO WHEN '4' THEN 'X' END AS 'manipula',CASE POLVORAPIRO WHEN '5' THEN 'X' END AS 'venta', CASE POLVORAPIRO WHEN '6' THEN 'X' END AS 'observador',
		CASE POLVORAPIRO WHEN '7' THEN 'X' END AS 'otro3',
		CASE ARTEEXPLOMAP WHEN '1' THEN 'X' END AS 'transito',CASE ARTEEXPLOMAP WHEN '2' THEN 'X' END AS 'contacto', CASE ARTEEXPLOMAP WHEN '3' THEN 'X' END AS 'actidesmi',
		CASE ARTEEXPLOMAP WHEN '4' THEN 'X' END AS 'actierrad',CASE ARTEEXPLOMAP WHEN '5' THEN 'X' END AS 'Otro4',
		VERSION AS 'VERSION', 
		JSON AS 'JSON' 

	From 
		HCFICHA452 

	Where 
		IDFICHANOTIFICACION  = @IdFicha

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera el detalle clínico completo de la ficha SIVIGILA 452 correspondiente a un caso de accidente por pólvora, explosivos o minas antipersona, identificada por su número y código interno de ficha. Consulta la tabla HCFICHA452 y transforma los valores numéricos de cada campo en marcas visuales (X / en blanco) listas para imprimir el formulario oficial de notificación obligatoria al sistema de vigilancia epidemiológica. Cubre tipos de lesión (laceración, quemadura, amputación, daño ocular, daño auditivo, fractura, trauma abdominal, compromiso de vía aérea), zonas corporales afectadas (cara, cuello, manos, pies, tronco, miembros superiores e inferiores, genitales, pliegues, huesos específicos), clasificación y extensión de las quemaduras, tipo de artefacto causante (artículos pirotécnicos, minas antipersona, municiones), lugar del evento, actividad que realizaba el afectado al momento del accidente, y factores asociados como consumo de alcohol o participación de menores de edad. Se usa para generar e imprimir la ficha 452 del SIVIGILA en la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila452';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila452';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de una ficha de notificación Sivigila (lesiones por pólvora/artefactos explosivos) transformando flags y códigos en marcas ''X'' para impresión/visualización del formato.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila452';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA452 con IDFICHANOTIFICACION igual al identificador suministrado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila452';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los flags binarios sólo aceptan ''1'' o ''0''; cualquier otro valor produce NULL en ambas columnas si/no; Las categorías codificadas son mutuamente excluyentes: sólo una columna ''X'' por grupo (grado, extensión, tipo de artefacto, lugar, actividad); Las áreas anatómicas (CARA, CUELLO, MANOS, etc.) sólo se marcan cuando el valor es ''1'', nunca se reporta el negativo; Los campos de texto libre (OTROCUAL, CUALOTROART, CUAL1, CUAL2) se devuelven sin espacios finales (RTRIM); El parámetro NumFicha se recibe pero no se utiliza en el filtro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila452';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Lesiones por pólvora/pirotecnia; Artefactos explosivos / minas antipersonal; Clasificación de quemaduras por grado y extensión; Áreas anatómicas afectadas; Lesiones por efecto del alcohol; Consumo de SPA en menores de edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila452';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA452: Cuando IDFICHANOTIFICACION coincide con el parámetro de entrada, se retorna una fila con los campos de la ficha convertidos a marcas ''X'' según valor 1/0 o códigos numéricos de cada categoría', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila452';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Campos booleanos de lesión (LACERACION, CONFUSION, QUEMADURA, AMPUTACION, DANIOCULAR, DANIOAUDIT, FRACTURAS, VIAAEREA, TRAUMAABD, OTRO1, LESIOEFECTALCO, MENOREDADSPA) = ''1'' → Marca ''X'' en columna ''si'' else Si = ''0'' marca ''X'' en columna ''no''; otro valor queda NULL; si CLASIGRADO ∈ {1,2,3} → Marca ''X'' en Primgrado / Seggrado / Tercgrado respectivamente; si EXTENSION ∈ {1,2,3} → Marca ''X'' en ''menor a 5'' / ''del 6 a 14'' / ''mayor del 15''; si TIPOARTEFLESI ∈ {1,2,3} → Marca el tipo de artefacto: pirotécnico, mina antipersonal o municiones; si TIPOARTEPIRO ∈ {1..10} → Marca el tipo específico de pirotecnia (cohetes, globos, pito, totes, volcanes, voladores, luces de bengala, juegos pirotécnicos, sin dato, otro); si LUGAREVENTO ∈ {1..7} → Marca el lugar del evento (vivienda, vía pública, parque, trabajo, zona rural, sin dato, otro); si POLVORAPIRO ∈ {1..7} → Marca la actividad con pólvora (almacenamiento, transporte, fabricación, manipulación, venta, observador, otro); si ARTEEXPLOMAP ∈ {1..5} → Marca la circunstancia con artefacto explosivo (tránsito, contacto, desminado, erradicación, otro)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila452';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA452', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila452';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila452';
-- GO
