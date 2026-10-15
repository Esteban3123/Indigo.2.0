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