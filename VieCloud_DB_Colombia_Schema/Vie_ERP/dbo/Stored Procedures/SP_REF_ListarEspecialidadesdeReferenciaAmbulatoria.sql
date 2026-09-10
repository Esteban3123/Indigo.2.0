-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,09-04-2019,>
-- Description:	<Description,Sp que me lista las especialidades de referencia al dar el egreso del paciente por Ambulatorio,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_REF_ListarEspecialidadesdeReferenciaAmbulatoria] 
(
@AUTO as varchar(200)
)

AS
BEGIN
	
	SET NOCOUNT ON;

	Declare @valores nvarchar(MAX)
	SELECT @valores= COALESCE(Rtrim(@valores)+ ' - ', '') + Rtrim(DESESPECI) FROM HCREFESPECIALIDADES a
	 Inner join INESPECIA B ON A.CODESPECI = B.CODESPECI WHERE  IDHCREFCONT = @AUTO
	select @valores as 'Especialidades'

	--exec sp_executesql @valores 

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que lista las especialidades médicas a las que fue referido un paciente ambulatorio al momento de su egreso. Recibe como parámetro el identificador de la referencia o remisión (@AUTO) y concatena en un único texto los nombres de todas las especialidades asociadas (por ejemplo: ''Cardiología - Ortopedia - Neurología''). Combina la tabla de especialidades vinculadas a la remisión (HCREFESPECIALIDADES) con el catálogo maestro de especialidades (INESPECIA) para obtener la descripción legible de cada una. Se usa en el proceso de egreso ambulatorio para mostrar o registrar el resumen de especialidades hacia las cuales fue derivado el paciente durante su referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_ListarEspecialidadesdeReferenciaAmbulatoria';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_REF_ListarEspecialidadesdeReferenciaAmbulatoria';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve, como una sola cadena concatenada, las descripciones de las especialidades médicas asociadas a una referencia ambulatoria al momento del egreso del paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeReferenciaAmbulatoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un identificador de referencia (IDHCREFCONT) válido para obtener resultados.; Debe existir correspondencia entre CODESPECI de HCREFESPECIALIDADES e INESPECIA para que la especialidad aparezca.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeReferenciaAmbulatoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las especialidades se concatenan en una sola cadena separadas por '' - ''.; Solo se incluyen especialidades cuyo código exista en el catálogo de especialidades (INNER JOIN con INESPECIA).; Las especialidades se filtran por el identificador de la referencia/contrarreferencia recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeReferenciaAmbulatoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Especialidad médica; Referencia ambulatoria; Egreso del paciente', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeReferenciaAmbulatoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Cuando existen registros en HCREFESPECIALIDADES con IDHCREFCONT igual al identificador recibido, retorna un resultset de una fila con la columna ''Especialidades'' que concatena las descripciones (DESESPECI) separadas por '' - ''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeReferenciaAmbulatoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCREFESPECIALIDADES; dbo.INESPECIA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeReferenciaAmbulatoria';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_REF_ListarEspecialidadesdeReferenciaAmbulatoria';
-- GO
