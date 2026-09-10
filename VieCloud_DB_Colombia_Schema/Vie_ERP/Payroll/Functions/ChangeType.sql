-- =============================================
-- Author:      Juan Pablo Daza Medina 
-- Create Date: 2024-12-24
-- Description: OBTIENE LA ABREVIATURA POR EL TIPO DE CAMBIO
-- =============================================
CREATE FUNCTION [Payroll].[ChangeType]
(
    @employeeId Int,
	@payrollDateLiquidated Date
	

)
RETURNS VARCHAR
AS
BEGIN
	Declare @ChangeType VARCHAR
	--Razones de modificacion de contrato
	Declare @ModificationC int = IsNull((Select C.ContractModificationReasonId From Payroll.VerifyAutoliquidationFile VAF
				join Payroll.Contract C on C.EmployeeId = VAF.EmployeeId
				WHERE VAF.EmployeeId = @employeeId AND  C.RetirementDate >=  DATEADD(DAY, 1, EOMONTH(@payrollDateLiquidated, -2))and C.RetirementDate <= @payrollDateLiquidated
				AND C.ContractModificationReasonId IS NOT NULL),0)

	--Novedades
	Declare @Novelty int = IsNull((SELECT N.TypeNovelty FROM Payroll.VerifyAutoliquidationFile VA 
							JOIN Payroll.Novelty N ON VA.EmployeeId = N.EmployeeId 
							WHERE @employeeId =Va.EmployeeId AND  N.RealDate >=  DATEADD(DAY, 1, EOMONTH(@payrollDateLiquidated, -2)) AND N.RealDate <= @payrollDateLiquidated
							AND n.TypeNovelty = 3 and (n.LicenseClass = 1 OR n.LicenseClass = 2)),0)

	--Retiro 
	Declare @Exclusion BIT = IsNull((SELECT 1 FROM Payroll.VerifyAutoliquidationFile VA 
									  JOIN Payroll.ContractLiquidation CL ON CL.EmployeeId = VA.EmployeeId
									  WHERE @employeeId =Va.EmployeeId AND CL.RetirementDate >=  DATEADD(DAY, 1, EOMONTH(@payrollDateLiquidated, -2)) 
									  AND CL.RetirementDate <= @payrollDateLiquidated), 0)
	

	If @ModificationC <> 0
	BEGIN
		SET @ChangeType = CASE 
								WHEN @ModificationC = 2  THEN 'OC'
								WHEN @ModificationC = 3 THEN 'SA'
								ELSE ''
						  END
		
		RETURN @ChangeType
	END
	IF @Novelty  <> 0 
	BEGIN
		SET @ChangeType = CASE 
							WHEN @Novelty = 1 THEN 'IN'
							WHEN @Novelty = 3 THEN 'PE'
							ELSE ''
							END
		RETURN @ChangeType
	END

	IF @Exclusion  <> 0 
	BEGIN
		SET @ChangeType = 'EX'
		RETURN @ChangeType
	END

	--select top 100* from Payroll.VerifyAutoliquidationFile
	--select * from Payroll.Novelty
	--select * from Payroll.ContractLiquidation

	--select top 1000 * from Payroll.Liquidation
	RETURN @ChangeType
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina y devuelve la abreviatura del tipo de cambio (novedad PILA) que aplica a un empleado en un período de liquidación de nómina, consultando tres situaciones en orden de prioridad: modificaciones contractuales (cambio de ocupación ''OC'' o variación salarial ''SA''), novedades de licencia de maternidad/paternidad (''IN'' o ''PE''), y retiro o exclusión del empleado (''EX''). Cruza el archivo de verificación de autoliquidación con los contratos, novedades y liquidaciones de contrato para identificar cuál evento ocurrió dentro del mes liquidado. Es utilizada en la generación del archivo PILA de seguridad social para reportar correctamente el tipo de novedad de cada empleado ante las entidades de seguridad social.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'ChangeType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'FUNCTION', @level1name = N'ChangeType';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina el código abreviado del tipo de cambio (novedad PILA) aplicable a un empleado en un período de autoliquidación, según modificación de contrato, licencia o retiro detectado en la ventana de los dos meses previos.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'ChangeType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El empleado debe tener al menos un registro en Payroll.VerifyAutoliquidationFile para el período evaluado; La fecha de liquidación debe ser válida para calcular el rango EOMONTH(-2)+1 día', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'ChangeType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La ventana de evaluación siempre es desde el primer día del mes calendario anterior al de liquidación hasta la propia fecha de liquidación (DATEADD(DAY,1,EOMONTH(@payrollDateLiquidated,-2)) .. @payrollDateLiquidated); Prioridad fija de detección: 1) modificación contractual, 2) novedad de licencia, 3) retiro; la primera condición que se cumpla determina el código retornado; Solo se consideran empleados con registro previo en VerifyAutoliquidationFile (todas las consultas hacen JOIN obligatorio contra esta tabla); Solo se reconocen novedades cuyo TypeNovelty=3 y LicenseClass sea 1 o 2 como generadoras de tipo de cambio; Códigos de salida posibles: ''OC'', ''SA'', ''PE'', ''EX'' o cadena vacía; nunca se retorna ''IN'' bajo el filtro vigente', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'ChangeType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'autoliquidación PILA; novedad de nómina; modificación de contrato; liquidación de contrato; retiro de empleado; licencia (clases 1 y 2); tipo de cambio PILA', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'ChangeType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.Contract: Cuando ContractModificationReasonId=2 en Payroll.Contract dentro del rango → retorna ''OC''; [RETURN_RESULT] Payroll.Contract: Cuando ContractModificationReasonId=3 en Payroll.Contract dentro del rango → retorna ''SA''; [RETURN_RESULT] Payroll.Novelty: Cuando existe Novelty con TypeNovelty=3 y LicenseClass IN (1,2) dentro del rango → retorna ''PE''; [RETURN_RESULT] Payroll.ContractLiquidation: Cuando existe ContractLiquidation con RetirementDate dentro del rango → retorna ''EX''', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'ChangeType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe modificación contractual (ContractModificationReasonId no nulo) en el rango [primer día del mes anterior, fecha de liquidación] → Retorna ''OC'' si la razón=2, ''SA'' si la razón=3, '''' en otro caso else Evalúa novedades; si Existe novedad con TypeNovelty=3 y LicenseClass IN (1,2) en el rango de fechas → Retorna ''PE'' (cuando @Novelty=3); rama ''IN'' para @Novelty=1 nunca se alcanza porque el filtro exige TypeNovelty=3 else Evalúa retiro; si Existe ContractLiquidation con RetirementDate dentro del rango [primer día del mes anterior, fecha liquidación] → Retorna ''EX'' (exclusión por retiro) else Retorna cadena vacía/no inicializada', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'ChangeType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.VerifyAutoliquidationFile; Payroll.Contract; Payroll.Novelty; Payroll.ContractLiquidation', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'ChangeType';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'FUNCTION', @level1name=N'ChangeType';
GO
