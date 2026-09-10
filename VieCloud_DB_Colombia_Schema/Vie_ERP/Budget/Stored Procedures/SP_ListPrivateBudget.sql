-- =============================================
-- Author:		Daniel Eduardo Arévalo Bonilla
-- Create date: 29-01-2018
-- Description:	Genera el Listado del Presupuesto Privado
-- =============================================
CREATE PROCEDURE [Budget].[SP_ListPrivateBudget]
	
AS 
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @tableResult TABLE (
		[IdVista] INT IDENTITY(1,1) PRIMARY KEY, 
		[IdPrivateBudget] INT,
		[IdParent] INT,
		[IdRubro] INT,
		[NombreRubro] VARCHAR(100),
		[IdCentroCosto] INT,
		[NombreCentroCosto] VARCHAR(200),
		[IdCuenta] INT,
		[NumeroCuentaContable] VARCHAR(50),
		[NombreCuentaContable] VARCHAR(100),
		[IdTercero] INT,
		[NitTercero] VARCHAR(20),
		[NombreTercero] VARCHAR(300),
		[Enero] [numeric](24, 0),
		[Febrero] [numeric](24, 0),
		[Marzo] [numeric](24, 0),
		[Abril] [numeric](24, 0),
		[Mayo] [numeric](24, 0),
		[Junio] [numeric](24, 0),
		[Julio] [numeric](24, 0),
		[Agosto] [numeric](24, 0),
		[Septiembre] [numeric](24, 0),
		[Octubre] [numeric](24, 0),
		[Noviembre] [numeric](24, 0),
		[Diciembre] [numeric](24, 0)
	)
	INSERT INTO @tableResult
		SELECT 
			0,
			IdParent, IdRubro, NombreRubro, 
			IdCentroCosto, NombreCentroCosto, 
			IdCuenta, NumeroCuentaContable, NombreCuentaContable, 
			IdTercero, NitTercero, NombreTercero,
			SUM(Enero) AS Enero, SUM(Febrero) AS Febrero, SUM(Marzo) AS Marzo, SUM(Abril) AS Abril, SUM(Mayo) AS Mayo, SUM(Junio) AS Junio, SUM(Julio) AS Julio, SUM(Agosto) AS Agosto, SUM(Septiembre) AS Septiembre, SUM(Octubre) AS Octubre, SUM(Noviembre) AS Noviembre, SUM(Diciembre) AS Diciembre 
		FROM 
		(
			SELECT 
				IdPrivateBudget, 
				IdParent, IdEstructuraPrivate as IdRubro, NombrePrivateBudget as NombreRubro, 
				IdCentroCosto, NombreCentroCosto, 
				IdCuenta, NumeroCuentaContable, NombreCuentaContable, 
				IdTercero, NitTercero, NombreTercero,
				coalesce([1], 0) as Enero, coalesce([2], 0) as Febrero, coalesce([3], 0) as Marzo, coalesce([4], 0) as Abril, coalesce([5], 0) as Mayo, coalesce([6], 0) as Junio, coalesce([7], 0) as Julio, coalesce([8], 0) as Agosto, coalesce([9], 0) as Septiembre, coalesce([10], 0) as Octubre, coalesce([11], 0) as Noviembre, coalesce([12], 0) as Diciembre 
			FROM
			(
				SELECT 
					SUM(IdPrivateBudget) AS IdPrivateBudget, Mes, 
					IdParent, IdPrivateBudgetStructure as IdEstructuraPrivate, DescriptionPrivateBudgetStructure as NombrePrivateBudget,
					IdCostCenter as IdCentroCosto, CostCenterName as NombreCentroCosto, 
					IdAccounting as IdCuenta, NumberAccount as NumeroCuentaContable, AccountingName as NombreCuentaContable, 
					IdThirdParty as IdTercero, NitThirdParty as NitTercero, NameThirdParty as NombreTercero, 
					SUM([Value]) AS Valor 
				FROM  
				(

					SELECT 	
						ISNULL(PB.Id, 0) as IdPrivateBudget, ISNULL(PB.[Month], 1) as Mes,
						PBSI.parentId as IdParent, PBSI.Id as IdPrivateBudgetStructure, PBSI.Description as DescriptionPrivateBudgetStructure, 
						cc.Id as IdCostCenter, cc.[Name] as CostCenterName, 
						ma.Id as IdAccounting, MA.Number as NumberAccount, MA.[Name] as AccountingName, 
						TP.Id as IdThirdParty, TP.Nit as NitThirdParty, TP.Name as NameThirdParty, 
						SUM(ISNULL(pb.[Value],0)) AS [Value] 	
					FROM Budget.PrivateBudgetItemsStructure PBSI
					LEFT JOIN Budget.PrivateBudgetItemsStructureDetail PBSID ON PBSID.PrivateBudgetItemsStructureId = PBSI.Id
					LEFT JOIN Payroll.CostCenter CC ON CC.Id = PBSID.CostCenterId
					LEFT JOIN GeneralLedger.MainAccounts MA ON MA.Id = PBSID.MainAccountId
					LEFT JOIN Common.ThirdParty TP ON TP.Id = PBSID.ThirdPartyId
					LEFT JOIN Budget.PrivateBudget PB ON PBSI.Id = PB.PrivateBudgetItemsStructureId AND ISNULL(PBSID.MainAccountId, 0) = ISNULL(PB.MainAccountId, 0) AND ISNULL(PBSID.ThirdPartyId, 0) = ISNULL(PB.ThirdPartyId, 0) AND ISNULL(PBSID.CostCenterId, 0) = ISNULL(PB.CostCenterId, 0)
					GROUP BY PB.Id, PB.[Month], PBSI.parentId, PBSI.Id, PBSI.Description, cc.Id, cc.[Name], ma.Id, MA.Number, MA.[Name], TP.Id, TP.Nit, TP.Name

				) AS OTHER 
				GROUP BY Mes, IdParent, IdPrivateBudgetStructure, DescriptionPrivateBudgetStructure, IdCostCenter, CostCenterName, IdAccounting, NumberAccount, AccountingName, IdThirdParty, NitThirdParty, NameThirdParty
			) as TablaConsulta
			PIVOT
			(
				SUM(Valor)
				FOR Mes in 
				( [1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12] )
			) as TablaPivote
		) As result
		-- WHERE ConceptType = @ConceptType
		GROUP BY IdParent, IdRubro, NombreRubro, IdCentroCosto, NombreCentroCosto, IdCuenta, NumeroCuentaContable, NombreCuentaContable, IdTercero, NitTercero, NombreTercero
		ORDER BY IdRubro, IdCentroCosto, IdCuenta, IdTercero

	UPDATE tr
		SET tr.idParent = trParent.IdVista
	FROM @tableResult tr
	INNER JOIN @tableResult trParent ON tr.IdParent = trParent.IdRubro

	SELECT 
		[IdVista], [IdPrivateBudget], 
		[IdParent], [IdRubro], [NombreRubro], 
		[IdCentroCosto], [NombreCentroCosto], 
		[IdCuenta], [NumeroCuentaContable], [NombreCuentaContable], 
		[IdTercero], [NitTercero], [NombreTercero],
		[Enero], [Febrero], [Marzo], [Abril], [Mayo], [Junio], [Julio], [Agosto], [Septiembre], [Octubre], [Noviembre], [Diciembre] 
	FROM @tableResult
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el listado completo del presupuesto privado de la organización, presentando los valores presupuestados mes a mes (de enero a diciembre) en formato de tabla pivote. Consolida la estructura jerárquica de rubros o conceptos presupuestales con su distribución contable, asociando para cada ítem el centro de costo, la cuenta contable del libro mayor y el tercero responsable. Combina la estructura de ítems presupuestales con los montos registrados en el presupuesto privado, resolviendo la jerarquía padre-hijo entre rubros para permitir una visualización en árbol. Es utilizado para reportería y consulta del presupuesto privado anual, permitiendo visualizar la planeación financiera por rubro, área, cuenta y proveedor o contratista.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ListPrivateBudget';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ListPrivateBudget';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un listado tabular del presupuesto privado con valores mensuales (enero-diciembre) por rubro, centro de costo, cuenta contable y tercero, manteniendo la relación jerárquica padre-hijo entre rubros.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ListPrivateBudget';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la estructura de ítems en Budget.PrivateBudgetItemsStructure (los rubros se obtienen siempre de esta tabla aunque no tengan detalle ni valores asociados).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ListPrivateBudget';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los importes mensuales nunca son NULL en la salida: cuando el PIVOT no encuentra valor para un mes, COALESCE([n],0) lo fuerza a 0.; Se incluyen rubros sin movimiento presupuestal porque PrivateBudget se une con LEFT JOIN; los meses quedarán en 0.; El emparejamiento entre PrivateBudgetItemsStructureDetail y PrivateBudget exige coincidencia exacta en MainAccountId, ThirdPartyId y CostCenterId, tratando NULL como 0 vía ISNULL para evitar discrepancias por nulos.; El resultado se ordena por IdRubro, IdCentroCosto, IdCuenta, IdTercero antes de la asignación de IdVista (IDENTITY), por lo que IdVista refleja ese orden jerárquico/contable.; Sólo se consideran los meses 1 a 12; cualquier valor de Month fuera de ese rango quedaría excluido del PIVOT.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ListPrivateBudget';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Presupuesto privado; Rubro presupuestal; Estructura jerárquica de rubros (padre/hijo); Centro de costo; Cuenta contable (PUC); Tercero (NIT); Distribución mensual del presupuesto', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ListPrivateBudget';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULT_SET: Devuelve un conjunto con una fila por combinación (Rubro, CentroCosto, Cuenta, Tercero) e importes pivoteados en 12 columnas (Enero..Diciembre); valores nulos se reemplazan por 0 vía COALESCE.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ListPrivateBudget';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Para cada fila, si existe otra fila cuyo IdRubro coincide con su IdParent original → Reescribe IdParent con el IdVista (identidad temporal) de la fila padre, traduciendo la jerarquía de rubros a referencias por posición de la vista else IdParent queda con el valor original (rubro raíz o sin padre presente en el resultado)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ListPrivateBudget';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.PrivateBudgetItemsStructure; Budget.PrivateBudgetItemsStructureDetail; Payroll.CostCenter; GeneralLedger.MainAccounts; Common.ThirdParty; Budget.PrivateBudget', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ListPrivateBudget';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ListPrivateBudget';
-- GO
