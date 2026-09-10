-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,13-11-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila825]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	--Select 	*	From HCFICHA825 where IDFICHANOTIFICACION  = 13

	Select
	CASE CONDICION WHEN '1' THEN 'X' END AS 'Sensible', CASE CONDICION WHEN '0' THEN 'X' END AS 'Resistente',
	CASE TIPOTUBER WHEN '1' THEN 'X' END AS 'Pulmonar', CASE TIPOTUBER WHEN '0' THEN 'X' END AS 'ExtraPulmonar',
	CASE LOCALTUBER WHEN '1' THEN 'X' END AS 'Pleural', CASE LOCALTUBER WHEN '2' THEN 'X' END AS 'Meningea', CASE LOCALTUBER WHEN '3' THEN 'X' END AS 'Peritoneal',
	CASE LOCALTUBER WHEN '4' THEN 'X' END AS 'Ganglional', CASE LOCALTUBER WHEN '5' THEN 'X' END AS 'Renal', CASE LOCALTUBER WHEN '6' THEN 'X' END AS 'Intestinal',
	CASE LOCALTUBER WHEN '7' THEN 'X' END AS 'OsteoArticular', CASE LOCALTUBER WHEN '8' THEN 'X' END AS 'Genitourinaria', CASE LOCALTUBER WHEN '9' THEN 'X' END AS 'Pericardica',
	CASE LOCALTUBER WHEN '10' THEN 'X' END AS 'Cutanea', CASE LOCALTUBER WHEN '11' THEN 'X' END AS 'Otro',
	CASE SEGUANTECE WHEN '1' THEN 'X' END AS 'Nuevo', CASE SEGUANTECE WHEN '0' THEN 'X' END AS 'Previamente Tratado',
	CASE PREVTRAT WHEN '1' THEN 'X' END AS 'TrasRecai', CASE PREVTRAT WHEN '2' THEN 'X' END AS 'TrasFrac', CASE PREVTRAT WHEN '3' THEN 'X' END AS 'PaciRecu',
	CASE PREVTRAT WHEN '4' THEN 'X' END AS 'OtrosPaci', CASE PREVTRAT WHEN '5' THEN 'X' END AS 'Trat1rali', CASE PREVTRAT WHEN '6' THEN 'X' END AS 'trat2dali',
	CASE PACTRASAL WHEN '1' THEN 'X' END AS 'PACTRASAL si', CASE PACTRASAL WHEN '0' THEN 'X' END AS 'PACTRASAL no',
	Rtrim(OCUPACION) As 'OCUPACION', Rtrim(PESOACT) As 'PESOACT', Rtrim(TALLAACT) As 'TALLAACT', Rtrim(IMC) As 'IMC',
	CASE PACCUENDIA WHEN '1' THEN 'X' END AS 'PACCUENDIA si', CASE PACCUENDIA WHEN '0' THEN 'X' END AS 'PACCUENDIA no',
	CASE BACILOS WHEN '1' THEN 'X' END AS 'BACILOS si', CASE BACILOS WHEN '0' THEN 'X' END AS 'BACILOS no',
	CASE RESULBACI WHEN '1' THEN 'X' END AS 'RESULBACI si', CASE RESULBACI WHEN '0' THEN 'X' END AS 'RESULBACI no',
	CASE CULTIVO WHEN '1' THEN 'X' END AS 'CULTIVO si', CASE CULTIVO WHEN '0' THEN 'X' END AS 'CULTIVO no',
	CASE RESUCULT WHEN '1' THEN 'X' END AS 'RESUCULT si', CASE RESUCULT WHEN '2' THEN 'X' END AS 'RESUCULT no', CASE RESUCULT WHEN '3' THEN 'X' END AS 'RESUCULT enproc',
	CASE PRUEMOL WHEN '1' THEN 'X' END AS 'PRUEMOL si', CASE PRUEMOL WHEN '0' THEN 'X' END AS 'PRUEMOL no',
	CASE RESULPRUEMOL WHEN '1' THEN 'X' END AS 'RESULPRUEMOL si', CASE RESULPRUEMOL WHEN '0' THEN 'X' END AS 'RESULPRUEMOL no',
	CASE NOMESPIDENT WHEN '1' THEN 'X' END AS 'mycoTuberculosis', CASE NOMESPIDENT WHEN '2' THEN 'X' END AS 'mycobovis', CASE NOMESPIDENT WHEN '3' THEN 'X' END AS 'mycoafrica',
	CASE NOMESPIDENT WHEN '4' THEN 'X' END AS 'mycomicroti', CASE NOMESPIDENT WHEN '5' THEN 'X' END AS 'mycocanet',
	CASE HISPATO WHEN '1' THEN 'X' END AS 'HISPATO si', CASE HISPATO WHEN '0' THEN 'X' END AS 'HISPATO no',
	CASE RESUHISPAT WHEN '1' THEN 'X' END AS 'RESUHISPAT si', CASE RESUHISPAT WHEN '0' THEN 'X' END AS 'RESUHISPAT no',
	CASE RESPRUSENS WHEN '1' THEN 'X' END AS 'RESPRUSENS si', CASE RESPRUSENS WHEN '0' THEN 'X' END AS 'RESPRUSENS no',
	CASE CUADCLI WHEN '1' THEN 'X' END AS 'CUADCLI si', CASE CUADCLI WHEN '0' THEN 'X' END AS 'CUADCLI no',
	CASE NEXOEPI WHEN '1' THEN 'X' END AS 'NEXOEPI si', CASE NEXOEPI WHEN '0' THEN 'X' END AS 'NEXOEPI no',
	CASE RADIOL WHEN '1' THEN 'X' END AS 'RADIOL si', CASE RADIOL WHEN '0' THEN 'X' END AS 'RADIOL no',
	CASE ADA WHEN '1' THEN 'X' END AS 'ADA si', CASE ADA WHEN '0' THEN 'X' END AS 'ADA no',
	CASE TUBERCULINA WHEN '1' THEN 'X' END AS 'TUBERCULINA si', CASE TUBERCULINA WHEN '0' THEN 'X' END AS 'TUBERCULINA no',
	CASE COOMOR WHEN '1' THEN 'X' END AS 'Diabetes', CASE COOMOR WHEN '2' THEN 'X' END AS 'Silicosis', CASE COOMOR WHEN '3' THEN 'X' END AS 'EnfRenal',
	CASE COOMOR WHEN '4' THEN 'X' END AS 'EPOC', CASE COOMOR WHEN '5' THEN 'X' END AS 'EnfHepat', CASE COOMOR WHEN '6' THEN 'X' END AS 'Cancer',
	CASE COOMOR WHEN '7' THEN 'X' END AS 'Artritis', CASE COOMOR WHEN '8' THEN 'X' END AS 'Desnutricion',
	convert(varchar(10),FECHACONFIR,103) As 'FECHACONFIR',
	CASE MONORES WHEN '1' THEN 'X' END AS 'MONORES', CASE MDR WHEN '1' THEN 'X' END AS 'MDR', CASE POLIRES WHEN '1' THEN 'X' END AS 'POLIRES',
	CASE XDR WHEN '1' THEN 'X' END AS 'XDR', CASE RESRIFAMP WHEN '1' THEN 'X' END AS 'RESRIFAMP', CASE RESPREXDR WHEN '1' THEN 'X' END AS 'RESPREXDR',
	CASE ESTREP1 WHEN '1' THEN 'X' END AS 'ESTREP1-1', CASE ESTREP1 WHEN '2' THEN 'X' END AS 'ESTREP1-2', CASE ESTREP1 WHEN '3' THEN 'X' END AS 'ESTREP1-3',
	CASE ISONIA1 WHEN '1' THEN 'X' END AS 'ISONIA1-1', CASE ISONIA1 WHEN '2' THEN 'X' END AS 'ISONIA1-2', CASE ISONIA1 WHEN '3' THEN 'X' END AS 'ISONIA1-3',
	CASE ETAMBU1 WHEN '1' THEN 'X' END AS 'ETAMBU1-1', CASE ETAMBU1 WHEN '2' THEN 'X' END AS 'ETAMBU1-2', CASE ETAMBU1 WHEN '3' THEN 'X' END AS 'ETAMBU1-3',
	CASE PIRAZI1 WHEN '1' THEN 'X' END AS 'PIRAZI1-1', CASE PIRAZI1 WHEN '2' THEN 'X' END AS 'PIRAZI1-2', CASE PIRAZI1 WHEN '3' THEN 'X' END AS 'PIRAZI1-3',
	CASE ISONIA2 WHEN '1' THEN 'X' END AS 'ISONIA2-2', CASE RIFAMP1 WHEN '1' THEN 'X' END AS 'RIFAMP1-2', CASE ESTREP2 WHEN '1' THEN 'X' END AS 'ESTREP2-2',
	CASE ISONIA3 WHEN '1' THEN 'X' END AS 'ISONIA3-2', CASE ETAMBU2 WHEN '1' THEN 'X' END AS 'ETAMBU2-2', CASE PIRAZI2 WHEN '1' THEN 'X' END AS 'PIRAZI2-2',
	CASE QUINDO1 WHEN '1' THEN 'X' END AS 'QUINDO1-2', CASE INYECT1 WHEN '1' THEN 'X' END AS 'INYECT1-2', CASE RIFAMP2 WHEN '1' THEN 'X' END AS 'RIFAMP2-2',
	CASE QUINDO2 WHEN '1' THEN 'X' END AS 'QUINDO2-1', CASE QUINDO2 WHEN '2' THEN 'X' END AS 'QUINDO2-2',
	CASE INYECT2 WHEN '1' THEN 'X' END AS 'INYECT2-1', CASE INYECT2 WHEN '2' THEN 'X' END AS 'INYECT2-2'

	From HCFICHA825 where IDFICHANOTIFICACION  = @IdFicha

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA825
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento almacenado que recupera y formatea la información clínica y epidemiológica de la Ficha de Notificación SIVIGILA 825 correspondiente a casos de tuberculosis. Dado un identificador y número de ficha, consulta la tabla HCFICHA825 y despliega en formato de casillas marcadas (X) todos los campos clave del reporte: tipo y localización de la tuberculosis, condición de sensibilidad o resistencia a medicamentos, antecedentes de tratamientos previos, resultados de pruebas diagnósticas (baciloscopía, cultivo, prueba molecular, histopatología, radiología, tuberculina, ADA), perfil de resistencia a fármacos antituberculosos (MDR, XDR, monorresistencia, polirresistencia) y comorbilidades asociadas (diabetes, cáncer, desnutrición, entre otras). Se utiliza para imprimir o visualizar la ficha oficial de vigilancia epidemiológica de tuberculosis exigida por el sistema SIVIGILA en Colombia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila825';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila825';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, en formato de casillas marcadas con ''X'', los datos clínicos y epidemiológicos de una ficha de notificación Sivigila 825 (tuberculosis) para su impresión o visualización.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila825';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en la tabla de fichas 825 cuyo identificador de notificación coincida con el parámetro recibido.; Los códigos almacenados en columnas tipo CONDICION, TIPOTUBER, LOCALTUBER, etc., deben corresponder a los valores esperados (cadenas ''0'',''1'',''2''... según el caso) para que se marquen las casillas con ''X''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila825';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada columna de salida marca ''X'' solo cuando el código almacenado coincide con el valor mapeado; en cualquier otro caso retorna NULL (sin ELSE).; Las fechas de confirmación se devuelven formateadas como dd/mm/yyyy (estilo 103).; Los campos de texto OCUPACION, PESOACT, TALLAACT e IMC se devuelven sin espacios a la derecha (RTRIM).; El filtro se realiza únicamente por el identificador de ficha de notificación; el número de ficha recibido como parámetro no se utiliza en la consulta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila825';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Tuberculosis (sensible/resistente, pulmonar/extrapulmonar); Localización de tuberculosis (pleural, meníngea, peritoneal, ganglionar, renal, intestinal, osteoarticular, genitourinaria, pericárdica, cutánea); Antecedentes de tratamiento (nuevo, previamente tratado, recaída, fracaso, recuperado); Pruebas diagnósticas (baciloscopia, cultivo, prueba molecular, histopatología, ADA, tuberculina, radiología); Especies de Mycobacterium (tuberculosis, bovis, africanum, microti, canetti); Comorbilidades (diabetes, silicosis, enfermedad renal, EPOC, enfermedad hepática, cáncer, artritis, desnutrición); Resistencia a fármacos (MONORES, MDR, POLIRES, XDR, resistencia a rifampicina, pre-XDR); Medicamentos antituberculosos (estreptomicina, isoniacida, etambutol, pirazinamida, rifampicina, quinolonas, inyectables); Datos antropométricos (peso, talla, IMC)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila825';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.HCFICHA825: Cuando IDFICHANOTIFICACION coincide con el parámetro de entrada, retorna un único conjunto de resultados con la traducción de códigos a marcas ''X'' por categoría clínica de la ficha 825.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila825';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA825', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila825';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila825';
-- GO
