-- Stored Procedure
-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,08-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila300]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 
 
	Select
	CASE TIPOAGRE WHEN '1' THEN 'X' END AS 'Mordedura', CASE TIPOAGRE WHEN '2' THEN 'X' END AS 'Arañazo', CASE TIPOAGRE WHEN '3' THEN 'X' END AS 'Inhalacion',
	CASE TIPOAGRE WHEN '4' THEN 'X' END AS 'Piel Lesionada', CASE TIPOAGRE WHEN '5' THEN 'X' END AS 'Tejido', CASE TIPOAGRE WHEN '6' THEN 'X' END AS 'Trans Organos',
	CASE AREAMORD WHEN '1' THEN 'X' END AS 'Cubierta', CASE AREAMORD WHEN '2' THEN 'X' END AS 'Descubierta',
	CASE AGREPROV WHEN '1' THEN 'X' END AS 'AgreProv Si', CASE AGREPROV WHEN '0' THEN 'X' END AS 'AgreProv No',
	CASE TIPOLESI WHEN '1' THEN 'X' END AS 'TIPOLESI Si', CASE TIPOLESI WHEN '0' THEN 'X' END AS 'TIPOLESI No',
	CASE PROFUNDI WHEN '1' THEN 'X' END AS 'PROFUNDI Si', CASE PROFUNDI WHEN '0' THEN 'X' END AS 'PROFUNDI No',
	CASE CABEZA WHEN '1' THEN 'X' END AS 'CABEZA', CASE MANOS WHEN '1' THEN 'X' END AS 'MANOS', CASE TRONCO WHEN '1' THEN 'X' END AS 'TRONCO',
	CASE MIEMSUP WHEN '1' THEN 'X' END AS 'MIEMSUP', CASE MIEMINF WHEN '1' THEN 'X' END AS 'MIEMINF', CASE PIES WHEN '1' THEN 'X' END AS 'PIES',
	CASE GENITA WHEN '1' THEN 'X' END AS 'GENITA', convert(varchar(10),FECHAAGRE,103) As 'FECHAAGRE',
	CASE ESPAGRE WHEN '1' THEN 'X' END AS 'Perro', CASE ESPAGRE WHEN '2' THEN 'X' END AS 'Gato', CASE ESPAGRE WHEN '3' THEN 'X' END AS 'Bovino',
	CASE ESPAGRE WHEN '4' THEN 'X' END AS 'Equidos', CASE ESPAGRE WHEN '5' THEN 'X' END AS 'Porcino', CASE ESPAGRE WHEN '6' THEN 'X' END AS 'Murcielago',
	CASE ESPAGRE WHEN '7' THEN 'X' END AS 'Zorro', CASE ESPAGRE WHEN '8' THEN 'X' END AS 'Mico', CASE ESPAGRE WHEN '9' THEN 'X' END AS 'Humano',
	CASE ESPAGRE WHEN '10' THEN 'X' END AS 'OtrosSilves', CASE ESPAGRE WHEN '11' THEN 'X' END AS 'Ovino', CASE ESPAGRE WHEN '12' THEN 'X' END AS 'GrandesRoed',
	CASE ANIVAC WHEN '1' THEN 'X' END AS 'Anivac Si', CASE ANIVAC WHEN '2' THEN 'X' END AS 'Aanivac No', CASE ANIVAC WHEN '3' THEN 'X' END AS 'AnivacDesc',
	CASE PRESECARNE WHEN '1' THEN 'X' END AS 'PRESECARNE Si', CASE PRESECARNE WHEN '0' THEN 'X' END AS 'PRESECARNE No', convert(varchar(10),FECHAVACU,103) As 'FECHAVACU',
	Rtrim(NOMPROP) As 'NOMPROP',  Rtrim(DIREPROP) As 'DIREPROP',  Rtrim(TELEFONO) As 'TELEFONO',
	CASE ESTAANAGRE WHEN '1' THEN 'X' END AS 'Signos Rabia', CASE ESTAANAGRE WHEN '2' THEN 'X' END AS 'Sin Signos Rabia', CASE ESTAANAGRE WHEN '3' THEN 'X' END AS 'Estaan Desco',
	CASE ESTAANCON WHEN '1' THEN 'X' END AS 'Vivo', CASE ESTAANCON WHEN '2' THEN 'X' END AS 'Muerto', CASE ESTAANCON WHEN '3' THEN 'X' END AS 'ESTAANCON desco',
	CASE UBIANIM WHEN '1' THEN 'X' END AS 'Observable', CASE UBIANIM WHEN '2' THEN 'X' END AS 'Perdido',
	CASE TIPOEXPO WHEN '1' THEN 'X' END AS 'No expo', CASE TIPOEXPO WHEN '2' THEN 'X' END AS 'Expo leve', CASE TIPOEXPO WHEN '3' THEN 'X' END AS 'Expo grave',
	CASE SUEROANTI WHEN '1' THEN 'X' END AS 'SUEROANTI si', CASE SUEROANTI WHEN '2' THEN 'X' END AS 'SUEROANTI no', CASE SUEROANTI WHEN '3' THEN 'X' END AS 'SUEROANTI no sabe',
	convert(varchar(10),FECHAAPLI,103) As 'FECHAAPLI',
	CASE VACUANTI WHEN '1' THEN 'X' END AS 'VACUANTI si', CASE VACUANTI WHEN '2' THEN 'X' END AS 'VACUANTI no', CASE VACUANTI WHEN '3' THEN 'X' END AS 'VACUANTI no sabe',
	Rtrim(NUMDOSIS) As 'NUMDOSIS', convert(varchar(10),FECHAULT,103) As 'FECHAULT',
	CASE LAVHERI WHEN '1' THEN 'X' END AS 'LAVHERI Si', CASE LAVHERI WHEN '0' THEN 'X' END AS 'LAVHERI No',
	CASE SUTUHER WHEN '1' THEN 'X' END AS 'SUTUHER Si', CASE SUTUHER WHEN '0' THEN 'X' END AS 'SUTUHER No',
	CASE ORDENSUE WHEN '1' THEN 'X' END AS 'ORDENSUE Si', CASE ORDENSUE WHEN '0' THEN 'X' END AS 'ORDENSUE No',
	CASE ORDENAPLI WHEN '1' THEN 'X' END AS 'ORDENAPLI Si', CASE ORDENAPLI WHEN '0' THEN 'X' END AS 'ORDENAPLI No',
	VERSION AS 'VERSION', JSON AS 'JSON'

	From HCFICHA300 where IDFICHANOTIFICACION  = @IdFicha

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y presenta el detalle completo de una ficha de notificación SIVIGILA tipo 300, correspondiente a eventos de agresión por animal (mordedura, arañazo, contacto) o accidente ofídico. Dado un identificador de ficha, consulta la tabla HCFICHA300 y expande los campos codificados en etiquetas legibles para el formulario oficial, incluyendo: tipo de agresión (mordedura, arañazo, inhalación, piel lesionada, trasplante de órganos), área corporal afectada (cubierta o descubierta y región específica: cabeza, manos, tronco, miembros, pies, genitales), especie agresora (perro, gato, bovino, equino, porcino, murciélago, zorro, mico, humano, silvestre, entre otros), estado clínico y ubicación del animal agresor, vacunación del animal, así como tratamiento recibido por el paciente (lavado de herida, sutura, suero antirrábico, vacuna antirrábica, número de dosis y fecha de última dosis). Este procedimiento se usa para imprimir o visualizar la ficha epidemiológica de notificación obligatoria ante eventos de agresión animal, en cumplimiento del sistema de vigilancia en salud pública (SIVIGILA).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila300';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila300';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve los datos de la ficha de notificación Sivigila 300 (agresiones por animales potencialmente transmisores de rabia) traduciendo códigos a marcas tipo ''X'' para presentación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila300';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA300 cuyo IDFICHANOTIFICACION coincida con el identificador recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila300';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las fechas (FECHAAGRE, FECHAVACU, FECHAAPLI, FECHAULT) siempre se devuelven en formato dd/mm/yyyy (estilo 103).; Los campos textuales (NOMPROP, DIREPROP, TELEFONO, NUMDOSIS) se devuelven sin espacios a la derecha (RTRIM).; Cada conjunto de columnas de tipo ''X'' es excluyente: solo una se marca según el código almacenado.; El parámetro NumFicha se recibe pero no se utiliza en el filtro; la consulta se filtra exclusivamente por IDFICHANOTIFICACION.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila300';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Agresión por animal potencialmente transmisor de rabia; Tipo de agresión (mordedura/arañazo/inhalación); Especie agresora; Vacunación animal; Exposición rábica (leve/grave); Suero antirrábico; Vacuna antirrábica; Estado y condición del animal agresor; Localización anatómica de la lesión; Manejo de herida (lavado, sutura)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila300';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA300: Cuando IDFICHANOTIFICACION coincide con el parámetro recibido → retorna una fila con los campos de la ficha transformados (códigos a ''X'' y fechas formato dd/mm/yyyy).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila300';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOAGRE en {1..6} → Marca con ''X'' la columna correspondiente: Mordedura, Arañazo, Inhalación, Piel Lesionada, Tejido o Trans Órganos.; si AREAMORD = 1 / 2 → Marca ''Cubierta'' o ''Descubierta''.; si ESPAGRE en {1..12} → Marca la especie agresora: Perro, Gato, Bovino, Équidos, Porcino, Murciélago, Zorro, Mico, Humano, Otros Silvestres, Ovino o Grandes Roedores.; si ANIVAC = 1/2/3 → Marca si el animal estaba vacunado (Sí/No/Desconocido).; si TIPOEXPO = 1/2/3 → Clasifica la exposición como No exposición, Exposición leve o Exposición grave.; si ESTAANAGRE = 1/2/3 → Marca estado del animal agresor: Signos de rabia, Sin signos, Desconocido.; si ESTAANCON = 1/2/3 → Marca condición del animal: Vivo, Muerto, Desconocido.; si UBIANIM = 1/2 → Marca ubicación del animal: Observable o Perdido.; si SUEROANTI / VACUANTI = 1/2/3 → Marca si recibió suero/vacuna antirrábica previa: Sí, No, No sabe.; si Campos booleanos (AGREPROV, TIPOLESI, PROFUNDI, PRESECARNE, LAVHERI, SUTUHER, ORDENSUE, ORDENAPLI) = 1/0 → Marca ''X'' en columna Sí o No según corresponda.; si Banderas anatómicas (CABEZA, MANOS, TRONCO, MIEMSUP, MIEMINF, PIES, GENITA) = 1 → Marca con ''X'' la zona corporal afectada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila300';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA300', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila300';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila300';
-- GO
