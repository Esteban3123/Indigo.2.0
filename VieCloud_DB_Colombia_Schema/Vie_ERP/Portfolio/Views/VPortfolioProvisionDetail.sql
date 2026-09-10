
CREATE VIEW [Portfolio].[VPortfolioProvisionDetail]
AS
	SELECT	ppd.Id, 
			ppd.ConfirmDateAccountReceivable, 
			ppd.PortfolioProvisionId, 
			ppd.AccountReceivableId, 
			ppd.InvoiceNumber, 
			ppd.Days,
			ppd.AgesId, 
			ppd.FacturerValue, 
			ppd.BalanceAccountReceivable, 
			ppd.ValueGlosado, 
			ppd.Expectative, 
			ppd.[Percentage], 
			ppd.NetPresentValue, 
			ppd.AccumulatedDeterioration, 
			ppd.Value,		
			cg.EntityType AS Regimen, 
			COALESCE(
            pgr.RegimenName,
            CASE WHEN i.DocumentType = 6 THEN N'Factura Básica'
                 WHEN i.DocumentType = 7 THEN N'Factura de Productos'
                 ELSE NULL END
			) AS RegimenName, 
			CONCAT(tp.Nit, ' - ', tp.[Name]) AS ThirdPartyNitName, 		
			IIF(ap.Id IS NULL, sp.NameMaximumAgeRange, CONCAT('De: ', ap.InitialRange, ' a ', ap.EndRange)) Range,
			lb.TypeBook,
			ppd.LegalBookId,
			ppd.PortfolioDeteriorationClassificationId,
			CONCAT(pdc.Code, ' - ', pdc.Description) AS PortfolioClassification
	FROM Portfolio.PortfolioProvision pp
	JOIN Portfolio.PortfolioProvisionDetail ppd ON pp.Id = ppd.PortfolioProvisionId
	JOIN Portfolio.AccountReceivable ar ON ppd.AccountReceivableId = ar.Id
	JOIN Common.ThirdParty tp ON ar.ThirdPartyId = tp.Id	
	LEFT JOIN GeneralLedger.MainAccounts mar ON mar.Id = ar.AccountWithoutRadicateId 
	LEFT JOIN Portfolio.GetRegimes() pgr ON mar.Number = pgr.AccountNumber
	LEFT JOIN Billing.Invoice i ON ar.InvoiceId = i.Id
	LEFT JOIN [Contract].CareGroup cg ON ISNULL(ar.CareGroupId, i.CareGroupId) = cg.Id
	LEFT JOIN Portfolio.AgesPortfolio ap ON ppd.AgesId = ap.Id
	LEFT JOIN Portfolio.SettingPortfolio sp ON pp.OperatingUnitId = sp.OperatingUnitId
	LEFT JOIN GeneralLedger.LegalBook lb ON lb.Id = ppd.LegalBookId
	LEFT JOIN Portfolio.PortfolioDeteriorationClassification pdc ON pdc.Id = ppd.PortfolioDeteriorationClassificationId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista detallada de provisiones de cartera que combina cada línea de provisión (factura o cuenta por cobrar) con su encabezado de provisión contable, el tercero pagador (EPS, aseguradora, empresa), el rango de envejecimiento (aging) aplicado, el régimen o grupo de atención, el libro contable y la clasificación de deterioro. Integra datos de PortfolioProvisionDetail y PortfolioProvision con las cuentas por cobrar, facturas, terceros, configuración de cartera y rangos de edad para mostrar por cada documento de cobro: días de mora, saldo pendiente, valor glosado, porcentaje de provisión, valor neto presente, deterioro acumulado y el tramo de vencimiento al que pertenece. Sirve como fuente principal para reportes de provisión contable, análisis de cartera por antigüedad, seguimiento del deterioro de cuentas por cobrar y control de cobro jurídico por unidad operativa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VPortfolioProvisionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'VIEW', @level1name = N'VPortfolioProvisionDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que expone el detalle de provisiones de cartera enriquecido con datos del tercero, régimen contable, rango de edades (aging), libro legal y clasificación de deterioro asociados a cada cuenta por cobrar provisionada.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioProvisionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existe una PortfolioProvision con detalle (PortfolioProvisionDetail) vinculado a una AccountReceivable.; La cuenta por cobrar debe tener un ThirdParty asociado (JOIN obligatorio sobre ThirdParty).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioProvisionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El régimen (RegimenName) se deriva primero del plan de cuentas (MainAccounts.Number vs GetRegimes()); solo si no se encuentra se infiere del tipo de documento de la factura.; El CareGroup se resuelve con prioridad al de la cuenta por cobrar (ar.CareGroupId) y, si es nulo, al de la factura (i.CareGroupId).; ThirdPartyNitName siempre se presenta en el formato ''NIT - Nombre''.; PortfolioClassification siempre se presenta en el formato ''Código - Descripción'' del catálogo de clasificación de deterioro.; El rango de edades se expresa como ''De: X a Y'' cuando existe AgesPortfolio; de lo contrario se usa la denominación del rango máximo configurado por unidad operativa.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioProvisionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Provisión de cartera; Cuenta por cobrar; Tercero (NIT); Régimen; Factura Básica; Factura de Productos; Grupo de atención (CareGroup); Rango de edades (aging) de cartera; Deterioro acumulado; Valor presente neto; Glosa (ValueGlosado); Libro legal contable; Clasificación de deterioro de cartera', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioProvisionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.VPortfolioProvisionDetail: Devuelve una fila por cada PortfolioProvisionDetail unido a su PortfolioProvision, AccountReceivable y ThirdParty; los demás joins son LEFT y pueden producir nulos.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioProvisionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pgr.RegimenName obtenido vía Portfolio.GetRegimes() por número de cuenta es NULL → Si i.DocumentType = 6 → ''Factura Básica''; si i.DocumentType = 7 → ''Factura de Productos''; en otro caso NULL else Se usa el RegimenName devuelto por GetRegimes(); si ap.Id IS NULL (no hay rango de edades AgesPortfolio asociado) → Range = sp.NameMaximumAgeRange (nombre del rango máximo configurado en SettingPortfolio) else Range = CONCAT(''De: '', ap.InitialRange, '' a '', ap.EndRange)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioProvisionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.GetRegimes', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioProvisionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioProvision; Portfolio.PortfolioProvisionDetail; Portfolio.AccountReceivable; Common.ThirdParty; GeneralLedger.MainAccounts; Portfolio.GetRegimes; Billing.Invoice; Contract.CareGroup; Portfolio.AgesPortfolio; Portfolio.SettingPortfolio; GeneralLedger.LegalBook; Portfolio.PortfolioDeteriorationClassification', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioProvisionDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'VIEW', @level1name=N'VPortfolioProvisionDetail';
GO
