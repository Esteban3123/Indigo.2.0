-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 26/02/2018
-- Description:	Sp encargado de listar los reconocimientos
-- =============================================
CREATE PROCEDURE [Budget].[Sp_RecognitionReport]
	@InitialDate date,
	@EndDate date,
	@BudgetaryValidityId int,
	@RecognitionCode varchar(20)
AS
BEGIN
	
	SET NOCOUNT ON

    select ROW_NUMBER() OVER(ORDER BY r.Code DESC) as Id,
	r.Code as RecognitionCode, 
	cast(r.DocumentDate as date) as DocumentDate, 
	t.Nit + ' - ' + t.Name as ThirdPartyDescription, 
	c.Code as CategoryCode, 
	c.Name as NameCategory, 
	f.Code + ' - ' + f.Name as FinancialSourceDescription,
	case r.RecognitonType when 1 then 'Reconocimiento' when 2 then 'Cuenta x Cobrar' else 'Vigencia Anterior' end as RecognitionTypeDescription,
	case r.Status when 1 then 'Registrado' when 2 then 'Confirmado' else 'Anulado' end as RecognitionStatus,
	SUM(rd.InitialValue) as InitialValue, 
	ISNULL(SUM(IIF(rmd.Nature = 1, rmd.Value, 0)),0) as DebitValue, 
	ISNULL(SUM(IIF(rmd.Nature = 2, rmd.Value, 0)),0) as CreditValue, 
	SUM(rd.InitialValue) + ISNULL(SUM(IIF(rmd.Nature = 2, rmd.Value, 0)),0) - ISNULL(SUM(IIF(rmd.Nature = 1, rmd.Value, 0)),0) as Balance
	from Budget.Recognition r
	inner join Budget.RecognitionDetail rd on rd.RecognitionId = r.Id
	inner join Common.ThirdParty t on t.Id = r.ThirdPartyId
	inner join Budget.Category c on c.Id = rd.CategoryId
	inner join Budget.FinancialSource f on f.Id = c.FinancialSourceId
	left outer join Budget.RecognitionModificationDetail rmd on rmd.RecognitionDetailId = rd.Id
	left outer join Budget.RecognitionModification rm on rm.Id = rmd.RecognitionModificationId and cast(rm.DocumentDate as date) BETWEEN @InitialDate AND @EndDate And rm.Status = 2
	where cast(r.DocumentDate as date) >= @InitialDate and cast(r.DocumentDate as date) <= @EndDate and r.BudgetaryValidityId = @BudgetaryValidityId and r.Code like @RecognitionCode + '%'
	group by r.Code, r.DocumentDate, t.Nit, t.Name, c.Code, c.Name, r.Status, r.RecognitonType, f.Code, f.Name

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de reconocimientos presupuestarios filtrado por rango de fechas, vigencia presupuestal y código de reconocimiento. Consolida cada reconocimiento con su tercero (proveedor o contratista identificado por NIT y nombre), la categoría presupuestal y la fuente de financiación, calculando el valor inicial, los movimientos de modificación en débito y crédito, y el saldo resultante. Clasifica cada reconocimiento según su tipo (reconocimiento, cuenta por cobrar o vigencia anterior) y su estado (registrado, confirmado o anulado). Se utiliza para auditoría y seguimiento presupuestal, permitiendo conocer el estado financiero de los derechos u obligaciones económicas reconocidas dentro de una vigencia.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'Sp_RecognitionReport';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'Sp_RecognitionReport';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los reconocimientos presupuestales de una vigencia dentro de un rango de fechas, mostrando valores iniciales, débitos, créditos y saldo calculado a partir de modificaciones confirmadas.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'Sp_RecognitionReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas (@InitialDate, @EndDate) y la vigencia presupuestal (@BudgetaryValidityId) deben estar definidos para acotar la consulta.; El prefijo @RecognitionCode se usa con LIKE; si se desea traer todos, debe pasarse una cadena vacía.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'Sp_RecognitionReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen reconocimientos cuyo DocumentDate esté entre @InitialDate y @EndDate y pertenezcan a la vigencia @BudgetaryValidityId.; El código del reconocimiento debe iniciar con el prefijo @RecognitionCode (LIKE @RecognitionCode + ''%'').; Las modificaciones (RecognitionModification) solo afectan los totales si están en estado 2 (Confirmado) y dentro del rango de fechas.; Balance siempre se calcula como InitialValue + créditos (Nature=2) - débitos (Nature=1).; El identificador de fila (Id) se asigna por ROW_NUMBER ordenado descendentemente por código de reconocimiento.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'Sp_RecognitionReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reconocimiento presupuestal; Cuenta por cobrar; Vigencia anterior; Vigencia presupuestal; Tercero; Categoría presupuestal; Fuente de financiación; Modificación de reconocimiento (débito/crédito); Saldo presupuestal', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'Sp_RecognitionReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un resultset agrupado por reconocimiento con columnas calculadas: InitialValue, DebitValue, CreditValue y Balance = InitialValue + Créditos - Débitos.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'Sp_RecognitionReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si r.RecognitonType = 1 → Se etiqueta como ''Reconocimiento'' else Si =2 ''Cuenta x Cobrar''; en otro caso ''Vigencia Anterior''; si r.Status = 1 → Se etiqueta como ''Registrado'' else Si =2 ''Confirmado''; en otro caso ''Anulado''; si rmd.Nature = 1 → El valor de la modificación suma como DebitValue else Si Nature=2 suma como CreditValue; si rm.DocumentDate BETWEEN @InitialDate AND @EndDate AND rm.Status = 2 → Solo se consideran modificaciones confirmadas dentro del rango de fechas para débitos/créditos else Modificaciones fuera del rango o no confirmadas se excluyen del cálculo (LEFT JOIN deja NULL → 0 vía ISNULL)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'Sp_RecognitionReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Recognition; Budget.RecognitionDetail; Common.ThirdParty; Budget.Category; Budget.FinancialSource; Budget.RecognitionModificationDetail; Budget.RecognitionModification', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'Sp_RecognitionReport';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'Sp_RecognitionReport';
-- GO
