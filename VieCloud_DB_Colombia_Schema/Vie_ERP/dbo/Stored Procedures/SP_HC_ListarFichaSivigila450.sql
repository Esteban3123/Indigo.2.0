-- Stored Procedure

-- =============================================
-- Author:		<Author,Jean Carlos Roldan Lozano,Name>
-- Create date: <Create Date,30-10-2018,>
-- Description:	<Description,Sp que me lista la Información de las Fichas del Sivigila>
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarFichaSivigila450]
(
  @IdFicha as Int,
  @NumFicha as varchar(10)
)

AS
BEGIN
  SET NOCOUNT ON;
 

	Select
	CASE TIPOINGRESO WHEN '1' THEN 'X' END AS 'NUEVO', CASE TIPOINGRESO WHEN '2' THEN 'X' END AS 'RECIDIVA', CASE TIPOINGRESO WHEN '3' THEN 'X' END AS 'RETRATAMIENTO',
	CASE CLASIFCASO WHEN '1' THEN 'X' END AS 'PAUCIBACILAR', CASE CLASIFCASO WHEN '2' THEN 'X' END AS 'MULTIBACILAR', Rtrim(NUMLESIONES) As 'NUMLESIONES',
	CASE BACILOS WHEN '1' THEN 'X' END AS 'BACILOS Si',CASE BACILOS WHEN '0' THEN 'X' END AS 'BACILOS No', Rtrim(RESULTADO) As 'RESULTADO',
	CASE BIOPSIA WHEN '1' THEN 'X' END AS 'BIOPSIA Si',CASE BIOPSIA WHEN '0' THEN 'X' END AS 'BIOPSIA No',
	CASE RESULHISPA WHEN '1' THEN 'X' END AS 'INDETERMINADA', CASE RESULHISPA WHEN '2' THEN 'X' END AS 'TUBERCULOIDE', CASE RESULHISPA WHEN '3' THEN 'X' END AS 'DIMORFA',
	CASE RESULHISPA WHEN '4' THEN 'X' END AS 'LEPROMATOSA', CASE RESULHISPA WHEN '5' THEN 'X' END AS 'NEURAL', CASE RESULHISPA WHEN '6' THEN 'X' END AS 'OTRODIAG',
	CASE MAXIGRADO WHEN '1' THEN 'X' END AS 'GRADOCERO', CASE MAXIGRADO WHEN '2' THEN 'X' END AS 'GRADOUNO', CASE MAXIGRADO WHEN '3' THEN 'X' END AS 'GRADODOS',
	CASE PRESEREACCION WHEN '1' THEN 'X' END AS 'TIPOUNO', CASE PRESEREACCION WHEN '2' THEN 'X' END AS 'TIPODOS', CASE PRESEREACCION WHEN '3' THEN 'X' END AS 'NINGUNA',
	VERSION AS 'VERSION', JSON AS 'JSON'
	From HCFICHA450 where IDFICHANOTIFICACION  = @IdFicha

END

--select * from hcfichanotificacion where id = '8'
--select * from HCFICHA450
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que recupera y formatea los datos clínicos específicos de la ficha SIVIGILA 450 para notificación de casos de lepra (enfermedad de Hansen). A partir del identificador de la ficha de notificación, consulta la tabla HCFICHA450 y traduce los códigos internos a etiquetas legibles para impresión del formulario oficial: tipo de ingreso (nuevo, recidiva, retratamiento), clasificación del caso (paucibacilar, multibacilar), número de lesiones, resultados de baciloscopía, biopsia e histopatología (indeterminada, tuberculoide, dimorfa, lepromatosa, neural, otro diagnóstico), grado máximo de discapacidad y presencia de reacción. Se usa para generar o visualizar la ficha de notificación epidemiológica de lepra dentro de la historia clínica del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila450';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarFichaSivigila450';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera y formatea para visualización los datos de la ficha epidemiológica Sivigila 450 (lepra), traduciendo códigos numéricos en marcas tipo casilla (''X'') por categoría clínica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila450';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en HCFICHA450 cuyo IDFICHANOTIFICACION coincida con el identificador de ficha provisto.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila450';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los campos categóricos se exponen como mutuamente excluyentes: solo una opción por grupo recibe la marca ''X''.; Si el código almacenado no coincide con ninguno de los valores esperados, todas las columnas del grupo quedan en NULL.; No se modifica información; la operación es estrictamente de lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila450';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Lepra (clasificación paucibacilar/multibacilar); Tipo de ingreso del caso (nuevo, recidiva, retratamiento); Baciloscopia; Biopsia; Resultado histopatológico (indeterminada, tuberculoide, dimorfa, lepromatosa, neural); Grado de discapacidad; Reacción leprosa (tipo 1, tipo 2)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila450';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] HCFICHA450: Devuelve una fila con las categorías clínicas de la ficha 450 traducidas a marcas ''X'' según el código almacenado, más VERSION y JSON, filtrando por el identificador de ficha de notificación.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila450';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOINGRESO = ''1'' / ''2'' / ''3'' → Marca ''X'' en NUEVO, RECIDIVA o RETRATAMIENTO respectivamente; si CLASIFCASO = ''1'' / ''2'' → Marca ''X'' en PAUCIBACILAR o MULTIBACILAR; si BACILOS = ''1'' / ''0'' → Marca ''X'' en ''BACILOS Si'' o ''BACILOS No''; si BIOPSIA = ''1'' / ''0'' → Marca ''X'' en ''BIOPSIA Si'' o ''BIOPSIA No''; si RESULHISPA entre ''1'' y ''6'' → Marca ''X'' en INDETERMINADA, TUBERCULOIDE, DIMORFA, LEPROMATOSA, NEURAL u OTRODIAG según el valor; si MAXIGRADO = ''1'' / ''2'' / ''3'' → Marca ''X'' en GRADOCERO, GRADOUNO o GRADODOS; si PRESEREACCION = ''1'' / ''2'' / ''3'' → Marca ''X'' en TIPOUNO, TIPODOS o NINGUNA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila450';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHA450', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila450';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarFichaSivigila450';
-- GO
