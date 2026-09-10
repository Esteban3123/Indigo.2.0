
-- =============================================
-- Author:      Carlos jhefersson Muñoz
-- Create date: 03/08/2017
-- Description: Procedimiento para el reporte de ArchivoFT001
-- =============================================
CREATE PROCEDURE [GeneralLedger].[SP_ReportSingleCircularFT001]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @Year int,
			@Month int,			
			@LegalBookId int,
			@LegalBookIdCopy int,
			@AccountLevel tinyint,
			@LegalBookType Tinyint

    --Tabla donde se acumula los saldos de los meses anteriores y del mes 14 del año anterior
	declare @TableBalance table(
								Id int identity(1,1),
								IdAccount int,
								Number varchar(50),
								NameAccount varchar(100),
								balanceCurrent decimal(20,4),
								nature tinyint,
								allowsMovement bit,
								[Availability] tinyint,
								mainAccountLevel int)

	--Tabla donde se acumulan los saldos de los meses anteriores y del mes 14 del año anterior pero con la informacion actualizada
	--cuando el libro es el 3 para el tema de que no haya problema si llega a ver homologacion
	declare @TableBalanceFinal table(
								Id int identity(1,1),
								IdAccount int,
								Number varchar(50),
								NameAccount varchar(100),
								balanceCurrent decimal(20,4),
								nature tinyint,
								allowsMovement bit,
								[Availability] tinyint,
								mainAccountLevel int)
    
	declare @TableBalanceMayor table(Id int identity(1,1), IdAccount int, Number varchar(50), NameAccount varchar(100), balanceCurrent decimal(20,4), nature tinyint, allowsMovement bit, [Availability] tinyint, mainAccountLevel int)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@LegalBookId = t.x.value('LegalBookId[1]','int'),
				@AccountLevel = t.x.value('AccountLevel[1]','tinyint')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/
		SET @LegalBookIdCopy = @LegalBookId
		/**************Evalua Si el Legal book es de tipo 3 para hacer el cambio al oficial y posterior hacer la Homolgacion********/
		if  EXISTS(
			SELECT lb.Id
			FROM GeneralLedger.LegalBook lb
			WHERE lb.Id = @LegalBookId and lb.TypeBook = 3)
		BEGIN
			SELECT	@LegalBookId= lb.Id,
					@LegalBookType = 3
			FROM GeneralLedger.LegalBook lb
			WHERE  lb.OfficialBook = 1 AND Status = 1
		END
/************************Se inserta los registros a la tabla balance************************/
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
				inner JOIN GeneralLedger.MainAccountClasses AS mac WITH (NOLOCK) ON ma.IdAccountClass = mac.Id
			WHERE ma.LegalBookId = @LegalBookId and ((gb.[Month] = 14 and gb.[Year] = @Year - 1) or (gb.[Month] <= @Month and gb.[Year] = @Year))
				and (gb.CreditValue <> 0 Or gb.DebitValue <> 0)
			GROUP BY ma.id, ma.Number, ma.[Name], ma.Nature, ma.AllowsMovement, ma.[Availability], mal.[Level],mac.Nature
			HAVING (SUM(gb.CreditValue) <> SUM(gb.DebitValue))
			
/*------------------------------------------------Se homolga las cuentas--------------------------------------------------------------*/
			IF @LegalBookType = 3
				BEGIN

				WITH Cte_MainAccount as (
											SELECT 
												Id,
												Number,
												Name,
												Nature,
												AllowsMovement,
												Availability,
												LegalBookId,
												IdAccountLevel
											FROM GeneralLedger.MainAccounts
										)
					
				--Se insertan los cambios de la cuentas homologadas y no se utiliza un update ya que por el tema de haber registros
				--repetidos este update no va a funcionar de forma correcta
				insert into @TableBalanceFinal(
							IdAccount ,
							Number ,
							NameAccount ,
							nature ,
							allowsMovement ,
							[Availability] ,
							mainAccountLevel ,
							balanceCurrent )
						
					--homologadas
				SELECT
						ma.Id, 
						ma.Number, 
						ma.Name, 
						ISNULL(ma.Nature, tb.nature), 
						ISNULL(ma.AllowsMovement, tb.allowsMovement), 
						case ma.AllowsMovement when 0 then 0 else ma.[Availability] end,
						ISNULL(mal.Level, 1),
						iif(ISNULL(ma.Nature, tb.nature)=tb.nature,tb.balanceCurrent,(tb.balanceCurrent*-1))				
				FROM @TableBalance AS tb
				JOIN GeneralLedger.HomologationAccount ha WITH (NOLOCK) ON tb.IdAccount = ha.OfficialMainAccountId  
				JOIN Cte_MainAccount AS ma WITH (NOLOCK) ON ha.MainAccountId = ma.Id AND ma.LegalBookId = @LegalBookIdCopy
				JOIN GeneralLedger.MainAccountLevels AS mal WITH (NOLOCK) ON ma.IdAccountLevel = mal.Id
				 
				union all 

				-- no homologadas
				SELECT
					ma.Id, 
					'NH-'+TB.Number, 
					'SIN HOMOLOGAR', 
					ISNULL(ma.Nature, tb.nature), 
					ISNULL(ma.AllowsMovement, tb.allowsMovement), 
					case ma.AllowsMovement when 0 then 0 else ma.[Availability] end,
					ISNULL(mal.Level, 1),
					iif(ISNULL(ma.Nature, tb.nature)=tb.nature,tb.balanceCurrent,(tb.balanceCurrent*-1))
				FROM @TableBalance AS tb
				LEFT JOIN ( SELECT 
								ma.id,
								ha.OfficialMainAccountId
							FROM GeneralLedger.HomologationAccount ha WITH (NOLOCK) 
							JOIN Cte_MainAccount ma on ma.Id = ha.MainAccountId
							where ma.LegalBookId = @LegalBookIdCopy
							) ha on ha.OfficialMainAccountId = tb.IdAccount 
				JOIN Cte_MainAccount AS ma WITH (NOLOCK) ON tb.IdAccount = ma.Id
				JOIN GeneralLedger.MainAccountLevels AS mal WITH (NOLOCK) ON ma.IdAccountLevel = mal.Id
				where ha.Id is null	

			END
			ELSE
			BEGIN
			--Si no es una homologacion se inserta la informacion que se trae
				insert into @TableBalanceFinal(
						IdAccount ,
						Number ,
						NameAccount ,
						nature ,
						allowsMovement ,
						[Availability] ,
						mainAccountLevel ,
						balanceCurrent )
				select 
					tb.IdAccount,
					tb.Number,
					tb.NameAccount,
					tb.nature,
					tb.allowsMovement,
					tb.Availability,
					tb.mainAccountLevel,
					tb.balanceCurrent
				from @TableBalance tb
			END
/*-------------------------------------------------------------------------------------------------------------*/
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
			where ma.allowsMovement = 0 and mal.Level <= @AccountLevel and ma.LegalBookId = @LegalBookIdCopy

			open detail_cursor
				FETCH NEXT FROM detail_cursor
				INTO @IdAccount, @Number, @NameAccount, @natureAccount, @Level

				WHILE @@FETCH_STATUS = 0
				BEGIN
                  
					INSERT @TableBalanceMayor
						select @IdAccount, @Number, @NameAccount, balanceCurrent, @natureAccount, 0, [Availability], @Level from (
							select SUM(te.balanceCurrent) as balanceCurrent, [Availability]
							from @TableBalanceFinal as te where te.Number like @Number + '%' and allowsMovement = 1
							group by [Availability]
						) as Mayor
                      
					FETCH NEXT FROM detail_cursor
					INTO @IdAccount, @Number, @NameAccount, @natureAccount, @Level
				End
			close detail_cursor
		deallocate detail_cursor
		INSERT INTO @TableBalanceMayor
			SELECT IdAccount, Number, NameAccount, SUM(balanceCurrent), nature, allowsMovement, [Availability], mainAccountLevel 
			FROM @TableBalanceFinal
			GROUP BY IdAccount, Number, NameAccount, nature, allowsMovement, [Availability], mainAccountLevel

		SELECT	Number AS codigoConcepto,
				iif([Availability] in (1,2,0), [Availability], 3) claseConcepto,
				round(balanceCurrent,0) AS valor
		FROM @TableBalanceMayor
		WHERE mainAccountLevel <= case @AccountLevel when 5 then (select max([Level]) from GeneralLedger.MainAccountLevels) else @AccountLevel end
		ORDER BY Number, [Availability]
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera el reporte contable oficial Circular FT001 (archivo regulatorio de estados financieros) para un período y libro contable determinados. Consolida los saldos del libro mayor tomando los movimientos del mes 14 del año anterior (saldo inicial) más los meses acumulados del año consultado, agrupados por cuenta contable con su número, nombre, naturaleza (débito/crédito) y nivel de cuenta. Cuando el libro contable es de tipo auxiliar (tipo 3), aplica homologación de cuentas hacia el libro oficial, identificando cuentas homologadas y no homologadas (''SIN HOMOLOGAR''). Se usa para la generación del informe contable regulatorio FT001 requerido por la circular de la Superintendencia, consumiendo saldos de GeneralLedgerBalance, plan de cuentas (MainAccounts), niveles de cuenta (MainAccountLevels) y clases de cuenta (MainAccountClasses).', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT001';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSingleCircularFT001';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte contable Circular FT001 calculando saldos acumulados por cuenta (incluyendo mes 14 del año anterior) con homologación de cuentas y mayorización por nivel jerárquico.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener Year, Month, LegalBookId y AccountLevel.; Si el libro contable es de tipo 3, debe existir un libro oficial (OfficialBook=1, Status=1) para realizar la homologación.; Las cuentas a mayorizar deben tener correspondencia en MainAccountLevels.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los saldos solo consideran movimientos del año actual hasta el mes solicitado más el cierre (mes 14) del año anterior.; Las cuentas con AllowsMovement=0 siempre reportan Availability=0.; Cuando hay homologación con cambio de naturaleza, el saldo se invierte de signo para mantener consistencia contable.; Las cuentas no homologadas se preservan en el reporte con marcador ''NH-'' en lugar de descartarse.; Solo se incluyen cuentas cuyo total débito y crédito sean diferentes (saldo neto distinto de cero).', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Plan Único de Cuentas (PUC); Libro contable oficial; Saldo contable acumulado; Cierre contable (mes 14); Naturaleza débito/crédito; Homologación de cuentas; Mayorización por nivel jerárquico; Reporte Circular FT001; Disponibilidad de cuenta', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableBalance: Inserta saldos acumulados por cuenta del libro indicado donde el mes sea 14 del año anterior o mes <= mes solicitado del año actual, con CreditValue o DebitValue distintos de cero y SUM(Credit) <> SUM(Debit); el balance se calcula según naturaleza (1=Débito-Crédito, 2=Crédito-Débito).; [INSERT] @TableBalanceFinal: Si el libro original es TypeBook=3, inserta cuentas homologadas (vía HomologationAccount cruzando OfficialMainAccountId) y cuentas no homologadas marcándolas con prefijo ''NH-'' y nombre ''SIN HOMOLOGAR''; si la naturaleza difiere entre cuenta oficial y homologada, invierte el signo del saldo (balance * -1).; [INSERT] @TableBalanceFinal: Si el libro NO es TypeBook=3, copia directamente todos los registros de @TableBalance sin transformación.; [INSERT] @TableBalanceMayor: Para cada cuenta con AllowsMovement=0 y Level <= AccountLevel del libro original, inserta la suma de saldos de subcuentas hijas (Number LIKE ''padre%'') agrupada por Availability.; [INSERT] @TableBalanceMayor: Inserta también todos los registros de @TableBalanceFinal agregados por cuenta y disponibilidad para conservar el detalle de cuentas de movimiento.; [RETURN_RESULT] (resultset): Devuelve codigoConcepto (Number), claseConcepto (Availability si está en 1,2,0; en otro caso 3) y valor (saldo redondeado a entero) filtrando por nivel <= AccountLevel solicitado, o nivel máximo cuando AccountLevel=5.; [RETURN_RESULT] (resultset): En caso de error captura la excepción y retorna CodeResult=''999'' con el mensaje de error y línea.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El libro indicado existe con TypeBook = 3 → Reasigna LegalBookId al libro oficial (OfficialBook=1, Status=1) y marca LegalBookType=3 para activar la homologación. else Conserva el libro original sin homologación.; si @LegalBookType = 3 → Carga @TableBalanceFinal con cuentas homologadas y no homologadas, invirtiendo signo cuando la naturaleza difiere. else Copia @TableBalance a @TableBalanceFinal sin cambios.; si AllowsMovement = 0 (cuenta no transaccional) → La disponibilidad se fuerza a 0 al cargar las cuentas. else Se usa la Availability propia de la cuenta.; si AccountLevel = 5 → Se usa el nivel máximo existente en MainAccountLevels como tope del reporte. else Se usa el AccountLevel solicitado como tope.; si Availability no está en (0,1,2) → claseConcepto se reporta como 3. else claseConcepto toma el valor de Availability.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'GeneralLedger.LegalBook; GeneralLedger.GeneralLedgerBalance; GeneralLedger.MainAccounts; GeneralLedger.MainAccountLevels; GeneralLedger.MainAccountClasses; GeneralLedger.HomologationAccount', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT001';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSingleCircularFT001';
-- GO
