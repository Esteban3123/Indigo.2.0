CREATE PROCEDURE [dbo].[SP_AD_ValidaAnulacionIngreso]
(
@Ingreso Char(10),
@Paciente Varchar(25)
)
AS
BEGIN
	SET NOCOUNT ON;
	declare @EsperaDeTriagge as int
	declare @Historias as int
	declare @CodAzul as int
	declare @OrdenesDespacho as int
	
	--El Paciente Esta En Lista de Espera Triage
	set @EsperaDeTriagge = (SELECT COUNT(*) FROM dbo.ADCONTURG A INNER JOIN dbo.INEntidad B ON A.CODENTIDA=b.Codentida LEFT OUTER JOIN dbo.INPACIENT C ON A.IPCODPACI=C.IPCODPACI WHERE A.IPCODPACI=@Paciente AND CONESTADO='4')
	
	--El paciente tiene Historias en estado activo 
	set @Historias = (SELECT DISTINCT  COUNT(*)  FROM dbo.HCHISPACA WHERE NUMINGRES=@Ingreso AND ESTAFOLIO=1)

	--El paciente tiene ordenes de codigo azul
	set @CodAzul = (SELECT DISTINCT  COUNT(*)  FROM dbo.HCCODAZUC WHERE NUMINGRES=@Ingreso)

	--El paciente tiene ordenes de medicamentos 
	set @OrdenesDespacho = (SELECT DISTINCT  COUNT(*)  FROM dbo.HCFARMEPC WHERE NUMINGRES=@Ingreso)

	if @EsperaDeTriagge > 1
		begin
			SELECT 0 AS ESTADO_VALIDACION,'El Paciente Esta En Lista de Espera Triage' AS MENSAJE
		end
	else if @Historias > 1
		begin
			SELECT 0 AS ESTADO_VALIDACION,'El paciente tiene Historias en estado activo' AS MENSAJE
		end
	else if @CodAzul > 1
		begin
			SELECT 0 AS ESTADO_VALIDACION,'El paciente tiene ordenes de codigo azul' AS MENSAJE
		end
	else if @OrdenesDespacho > 1
		begin
			SELECT 0 AS ESTADO_VALIDACION,'El paciente tiene ordenes de medicamentos' AS MENSAJE
		end
	else
		begin
			SELECT 1 AS ESTADO_VALIDACION,'' AS MENSAJE
		end
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valida si un ingreso hospitalario puede ser anulado para un paciente determinado, verificando cuatro condiciones de negocio que impedirían la anulación: que el paciente no esté en lista de espera de triage en urgencias, que no tenga historias clínicas activas asociadas al ingreso, que no tenga órdenes de código azul emitidas, y que no tenga órdenes de despacho de medicamentos pendientes. Recibe el número de ingreso y la cédula o código del paciente, y retorna un indicador de estado (1 = puede anularse, 0 = no puede anularse) junto con el mensaje explicativo del impedimento encontrado. Se usa como control previo a la anulación de un ingreso, garantizando que no existan registros clínicos o asistenciales activos que comprometan la integridad de la atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ValidaAnulacionIngreso';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_ValidaAnulacionIngreso';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si un ingreso de paciente puede anularse, verificando que no existan registros bloqueantes de espera de triage, historias clínicas activas, órdenes de código azul ni órdenes de medicamentos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ValidaAnulacionIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador de ingreso debe corresponder a un ingreso existente para que los conteos sean significativos.; El identificador de paciente debe corresponder a un paciente existente en INPACIENT/ADCONTURG.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ValidaAnulacionIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La validación es excluyente y secuencial: se devuelve el primer impedimento encontrado en el orden triage → historias activas → código azul → medicamentos.; Solo se permite la anulación (ESTADO_VALIDACION=1) cuando ninguno de los conteos verificados supera 1.; El procedimiento no modifica datos; únicamente retorna un resultado de validación.; Folio de historia clínica se considera activo cuando ESTAFOLIO=1.; El estado ''4'' en ADCONTURG.CONESTADO se interpreta como ''en lista de espera de triage''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ValidaAnulacionIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso; lista de espera de triage; historia clínica; folio activo; código azul; órdenes de medicamentos; despacho farmacéutico; anulación de ingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ValidaAnulacionIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Cuando el conteo de ADCONTURG con CONESTADO=''4'' para el paciente es > 1, retorna ESTADO_VALIDACION=0 y mensaje ''El Paciente Esta En Lista de Espera Triage''.; [RETURN_RESULT] resultset: Cuando el conteo de HCHISPACA con ESTAFOLIO=1 para el ingreso es > 1, retorna ESTADO_VALIDACION=0 y mensaje ''El paciente tiene Historias en estado activo''.; [RETURN_RESULT] resultset: Cuando el conteo de HCCODAZUC para el ingreso es > 1, retorna ESTADO_VALIDACION=0 y mensaje ''El paciente tiene ordenes de codigo azul''.; [RETURN_RESULT] resultset: Cuando el conteo de HCFARMEPC para el ingreso es > 1, retorna ESTADO_VALIDACION=0 y mensaje ''El paciente tiene ordenes de medicamentos''.; [RETURN_RESULT] resultset: Cuando ninguna de las condiciones bloqueantes se cumple, retorna ESTADO_VALIDACION=1 y mensaje vacío indicando que la anulación es válida.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ValidaAnulacionIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Conteo de registros del paciente en ADCONTURG con CONESTADO=''4'' mayor que 1 → Retorna ESTADO_VALIDACION=0 con mensaje ''El Paciente Esta En Lista de Espera Triage'' else Evalúa siguiente condición; si Conteo de HCHISPACA del ingreso con ESTAFOLIO=1 mayor que 1 → Retorna ESTADO_VALIDACION=0 con mensaje ''El paciente tiene Historias en estado activo'' else Evalúa siguiente condición; si Conteo de HCCODAZUC para el ingreso mayor que 1 → Retorna ESTADO_VALIDACION=0 con mensaje ''El paciente tiene ordenes de codigo azul'' else Evalúa siguiente condición; si Conteo de HCFARMEPC para el ingreso mayor que 1 → Retorna ESTADO_VALIDACION=0 con mensaje ''El paciente tiene ordenes de medicamentos'' else Retorna ESTADO_VALIDACION=1 con mensaje vacío (anulación permitida)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ValidaAnulacionIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADCONTURG; dbo.INEntidad; dbo.INPACIENT; dbo.HCHISPACA; dbo.HCCODAZUC; dbo.HCFARMEPC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ValidaAnulacionIngreso';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_ValidaAnulacionIngreso';
-- GO
