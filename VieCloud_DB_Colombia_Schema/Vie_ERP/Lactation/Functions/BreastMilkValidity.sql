CREATE FUNCTION [Lactation].[BreastMilkValidity]
(
    @ID INT
)
RETURNS INT
AS
BEGIN
	-- Valida la vigencia de las leches maternas almacenadas 
	-- 1) verde
	-- 2) Naranja
	-- 3) Rojo
    DECLARE @result INT;

    SELECT @result = IIF(DateADD(DAY, C.ExpiredQuantity,A.ExtractionDate) <= Common.GETDATE(),3,IIF(DATEADD(DAY, C.ValidQuantity - C.WarningQuantity, A.ExtractionDate) <= Common.GETDATE(), 2, 1)) 
	FROM Lactation.BreastMilkIntakeRecords A
	INNER JOIN Lactation.ParametersCareCenters B ON A.CODCENATE = B.CenterCode
	INNER JOIN Lactation.ParametersBreastMilkValidity C ON B.ParametersConfigurationId = C.ParametersConfigurationId AND A.StorageType = C.MilkTypeCode
	WHERE A.Id = @ID

	SET @result = IIF(@result IS NULL,0,@result) 

    RETURN @result;
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Evalúa el estado de vigencia de una unidad de leche materna almacenada comparando su fecha de extracción con los umbrales configurados por centro de atención y tipo de leche. Retorna 1 (verde/vigente), 2 (naranja/por vencer), 3 (rojo/vencida) o 0 si no se encuentra el registro. La lógica aplica los días configurados en `ParametersBreastMilkValidity` para determinar los límites de advertencia y caducidad.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'BreastMilkValidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'BreastMilkValidity';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el estado de vigencia (semáforo) de un registro de leche materna almacenada según los parámetros configurados para el centro de atención y tipo de almacenamiento.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'BreastMilkValidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el registro de extracción en Lactation.BreastMilkIntakeRecords con el Id recibido.; El centro (CODCENATE) del registro debe estar parametrizado en Lactation.ParametersCareCenters.; Debe existir parametrización de vigencia en Lactation.ParametersBreastMilkValidity para la combinación ParametersConfigurationId y StorageType del registro.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'BreastMilkValidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre pertenece al conjunto {0,1,2,3}.; La vigencia se calcula sobre la fecha de extracción (ExtractionDate) usando los días parametrizados por tipo de leche y configuración del centro.; La fecha de referencia para el cálculo es Common.GETDATE() (no GETDATE() del sistema), garantizando uniformidad de zona horaria/lógica de negocio.', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'BreastMilkValidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'leche materna; vigencia de almacenamiento; extracción de leche; centro de atención / lactario; tipo de almacenamiento de leche; semáforo de vencimiento (verde/naranja/rojo)', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'BreastMilkValidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Retorna 3 (Rojo/vencida) cuando ExtractionDate + ExpiredQuantity días <= fecha actual (Common.GETDATE()).; [RETURN_RESULT] : Retorna 2 (Naranja/por vencer) cuando ExtractionDate + (ValidQuantity - WarningQuantity) días <= fecha actual y aún no está vencida.; [RETURN_RESULT] : Retorna 1 (Verde/vigente) cuando no se cumplen las condiciones de vencimiento ni de advertencia.; [RETURN_RESULT] : Retorna 0 cuando no se encuentra registro o no hay parametrización asociada (resultado NULL convertido a 0).', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'BreastMilkValidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si DATEADD(DAY, ExpiredQuantity, ExtractionDate) <= Common.GETDATE() → Estado 3 (Rojo - vencida) else Evalúa condición de advertencia; si DATEADD(DAY, ValidQuantity - WarningQuantity, ExtractionDate) <= Common.GETDATE() → Estado 2 (Naranja - por vencer) else Estado 1 (Verde - vigente); si @result IS NULL (no hubo match en los JOINs) → Estado 0 (sin clasificación) else Conserva el estado calculado', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'BreastMilkValidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'BreastMilkValidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Lactation.BreastMilkIntakeRecords; Lactation.ParametersCareCenters; Lactation.ParametersBreastMilkValidity', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'BreastMilkValidity';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Lactation', @level1type=N'FUNCTION', @level1name=N'BreastMilkValidity';
GO
