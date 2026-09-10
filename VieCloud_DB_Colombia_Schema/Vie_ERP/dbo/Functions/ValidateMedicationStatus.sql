

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE FUNCTION [dbo].[ValidateMedicationStatus] (@CONSECPRESCRA AS Varchar(25), @CODPRODUC AS Varchar(50))
RETURNS INT
AS
BEGIN

DECLARE @ESTADOMODIFICADO INT = 0;

	WITH DATOS AS (
		Select B.TRATMODIF, A.PREESTADO from HCPRESCRA A 
		INNER JOIN HCPRESCRD B ON A.CODPRODUC = B.CODPRODUC AND A.NUMEFOLIO = B.FOLIOINIC AND A.IPCODPACI = B.IPCODPACI AND A.NUMINGRES = B.NUMINGRES
		WHERE A.CODPRODUC = @CODPRODUC AND A.CODCONCEC = @CONSECPRESCRA
	) SELECT @ESTADOMODIFICADO = CASE WHEN EXISTS (SELECT 1 FROM DATOS WHERE PREESTADO = 4) THEN 2 WHEN EXISTS (SELECT 1 FROM DATOS WHERE TRATMODIF = 1) THEN 1 ELSE 0 END;

RETURN @ESTADOMODIFICADO -- 0 Sin modificar, 1 Modificado y 2 Suspendido

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que verifica el estado actual de un medicamento dentro de una prescripción médica. Recibe el consecutivo de la prescripción y el código del producto/medicamento, y devuelve un valor numérico que indica si el tratamiento está sin modificar (0), fue modificado (1) o fue suspendido (2). Para determinarlo, cruza el encabezado de la prescripción (HCPRESCRA) con el detalle de líneas de medicamento (HCPRESCRD) usando el folio, paciente e ingreso, evaluando el estado de la prescripción (PREESTADO = 4 indica suspensión) y la marca de modificación del tratamiento (TRATMODIF = 1). Se usa en la gestión de medicamentos y prescripciones de historia clínica para saber si una receta o línea de medicamento sigue vigente, fue ajustada o fue cancelada durante el ingreso del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ValidateMedicationStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ValidateMedicationStatus';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el estado de una prescripción de medicamento devolviendo si está suspendido (2), modificado (1) o sin modificar (0), priorizando el estado suspendido sobre el modificado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateMedicationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre cabecera (HCPRESCRA) y detalle (HCPRESCRD) de prescripción por producto, folio inicial, paciente e ingreso.; Los identificadores de prescripción y producto deben corresponder a un registro existente; si no existen coincidencias, el resultado es 0.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateMedicationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado ''Suspendido'' (PREESTADO=4) tiene prioridad sobre ''Modificado'' al determinar el estado del medicamento.; El resultado siempre es 0, 1 o 2.; La relación entre cabecera y detalle de prescripción se establece por producto, folio inicial, paciente e ingreso.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateMedicationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'prescripción médica; medicamento; tratamiento modificado; estado de prescripción; suspensión de medicamento; paciente; ingreso (episodio asistencial)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateMedicationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Cuando existe un registro con PREESTADO=4 → retorna 2 (Suspendido); si no, cuando existe TRATMODIF=1 → retorna 1 (Modificado); en otro caso retorna 0 (Sin modificar).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateMedicationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro relacionado con PREESTADO = 4 → Retorna 2 (Suspendido) else Evalúa siguiente condición; si No hay PREESTADO=4 pero existe registro con TRATMODIF = 1 → Retorna 1 (Modificado) else Retorna 0 (Sin modificar)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateMedicationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCPRESCRA; dbo.HCPRESCRD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateMedicationStatus';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ValidateMedicationStatus';
GO
