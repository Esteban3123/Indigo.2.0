-- =============================================
-- Author:      Carlos jhefersson Muñoz
-- Create date: 03/08/2017
-- Description: Procedimiento para el reporte de ArchivoFT001
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportArchiveFT001]

    @mesFinal int ,
    @Year int,
    @bookId as int,
	@accountLevel as int,
	@accountStart as varchar(50),
	@accountEnd as varchar(50)
AS
BEGIN
    --Tabla donde se acumula los saldos de los meses anteriores y del mes 14 del año anterior
	declare @TableBalance table(Id int identity(1,1), IdAccount int, Number varchar(50), NameAccount varchar(100), balanceCurrent decimal(20,4), nature tinyint, allowsMovement bit, [Availability] tinyint, mainAccountLevel int)
    
	declare @TableBalanceMayor table(Id int identity(1,1), IdAccount int, Number varchar(50), NameAccount varchar(100), balanceCurrent decimal(20,4), nature tinyint, allowsMovement bit, [Availability] tinyint, mainAccountLevel int)

	INSERT INTO @TableBalance
		SELECT 
			ma.id,
			ma.Number, 
			ma.[Name],
			isnull(case ma.Nature when 1 then sum(gb.DebitValue) - sum(gb.CreditValue) when 2 then sum(gb.CreditValue) - sum(gb.DebitValue) end,0) as Balance, 
			ma.Nature,
			ma.AllowsMovement,
			case ma.AllowsMovement when 0 then 0 else ma.[Availability] end,
			mal.[Level]
		FROM GeneralLedger.GeneralLedgerBalance as gb
			inner join GeneralLedger.MainAccounts as ma on  ma.id = gb.IdMainAccount
			inner join GeneralLedger.MainAccountLevels mal on mal.Id = ma.IdAccountLevel
		WHERE ma.LegalBookId = @bookId and ((gb.[Month] = 14 and gb.[Year] = @Year - 1) or (gb.[Month] <= @mesFinal and gb.[Year] = @Year))
			and (gb.CreditValue <> 0 Or gb.DebitValue <> 0)
		GROUP BY ma.id, ma.Number, ma.[Name], ma.Nature, ma.AllowsMovement, ma.[Availability], mal.[Level]
		HAVING (SUM(gb.CreditValue) <> SUM(gb.DebitValue))

    --mayorizamos los saldos a nivel de subcuenta
    declare @IdAccount int
    declare @Number varchar(50)
	declare @NameAccount varchar(50)
    declare @natureAccount varchar(10)
	declare @Level int

    declare detail_cursor cursor for
		select ma.Id, ma.Number, ma.[Name], ma.Nature, mal.[level] 
		from GeneralLedger.MainAccounts as ma 
			inner join GeneralLedger.MainAccountLevels as mal on mal.id = ma.IdAccountLevel 
		where ma.allowsMovement = 0 and mal.Level < @accountLevel and ma.LegalBookId = @bookId

		open detail_cursor
			FETCH NEXT FROM detail_cursor
			INTO @IdAccount, @Number, @NameAccount, @natureAccount, @Level

			WHILE @@FETCH_STATUS = 0
			BEGIN
                  
				INSERT @TableBalanceMayor
					select @IdAccount, @Number, @NameAccount, balanceCurrent, @natureAccount, 0, [Availability], @Level from (
						select SUM(te.balanceCurrent) as balanceCurrent, [Availability]
						from @TableBalance as te where te.Number like @Number + '%' and allowsMovement = 1
						group by [Availability]
					) as Mayor
                      
				FETCH NEXT FROM detail_cursor
				INTO @IdAccount, @Number, @NameAccount, @natureAccount, @Level
			End
		close detail_cursor
    deallocate detail_cursor

	INSERT INTO @TableBalanceMayor
		SELECT IdAccount, Number, NameAccount, balanceCurrent, nature, allowsMovement, [Availability], mainAccountLevel FROM @TableBalance
    	
	SELECT Number,NameAccount,round(balanceCurrent,0),[Availability], mainAccountLevel 
	FROM @TableBalanceMayor
	WHERE mainAccountLevel <= case @accountLevel when 5 then (select max([Level]) from GeneralLedger.MainAccountLevels) else @accountLevel end
	ORDER BY Number, [Availability]
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte contable Archivo FT001 del libro mayor, calculando los saldos de cuentas contables para un año y mes final determinados. Consolida los movimientos débito y crédito desde el saldo de apertura del año (mes 14 del año anterior) hasta el mes de corte indicado, acumulando saldos a nivel de subcuentas y cuentas mayores según el plan de cuentas. Utiliza el balance del libro mayor (GeneralLedgerBalance), el catálogo de cuentas principales (MainAccounts) y los niveles del plan de cuentas (MainAccountLevels) para producir un reporte jerárquico de saldos contables filtrado por libro legal, rango de cuentas y nivel de agrupación. Se usa para la presentación de libros contables oficiales y reportes de cierre contable periódico.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportArchiveFT001';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportArchiveFT001';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte contable ArchivoFT001 calculando saldos acumulados por cuenta hasta un mes/año dado, mayorizando hacia niveles superiores y filtrando por rango de nivel de cuenta.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El libro contable (bookId) debe existir en MainAccounts; Debe existir la jerarquía de niveles en MainAccountLevels; Los saldos del año anterior se consideran en el mes 14 (cierre) del Year-1; El mes final debe corresponder a un período válido (1-12 o cierre) del año contable', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen movimientos donde DebitValue o CreditValue sean distintos de cero; Solo se consideran cuentas con SUM(Credit) <> SUM(Debit) (excluye saldos netos cero); El saldo del año anterior se toma exclusivamente del mes 14 (período de cierre); El saldo del año actual considera todos los meses hasta @mesFinal inclusive; La mayorización solo agrega saldos de cuentas hoja (allowsMovement = 1) hacia cuentas agrupadoras (allowsMovement = 0); El reporte siempre se ordena por número de cuenta y disponibilidad; Los saldos se devuelven redondeados a 0 decimales', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Plan de cuentas (PUC); Libro mayor contable; Naturaleza débito/crédito; Mayorización de saldos; Período de cierre (mes 14); Disponibilidad de cuenta; Niveles jerárquicos de cuenta; Cuentas de movimiento vs agrupadoras; Reporte ArchivoFT001', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve el listado de cuentas con saldo redondeado a entero, filtrando aquellas cuyo nivel sea menor o igual al nivel solicitado; cuando @accountLevel = 5 se usa el nivel máximo configurado en MainAccountLevels', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ma.Nature = 1 (naturaleza débito) → Saldo = SUM(DebitValue) - SUM(CreditValue) else Si Nature = 2 (crédito): Saldo = SUM(CreditValue) - SUM(DebitValue); si ma.AllowsMovement = 0 → Disponibilidad se fuerza a 0 else Se conserva la Availability propia de la cuenta; si @accountLevel = 5 → Se filtra hasta el nivel máximo existente en MainAccountLevels else Se filtra hasta el nivel indicado por @accountLevel; si Cuenta con allowsMovement = 0 y nivel < @accountLevel → Se mayoriza sumando los saldos de cuentas hijas (Number LIKE prefijo) que sí permiten movimiento, agrupando por Availability', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.MainAccountLevels', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportArchiveFT001';
-- GO
