

CREATE view [ViewInternal].[VistaCruceAnticipos]
as (

select pa.code as NumeroAnticipo, isnull(cr.Code, case pa.OpeningBalance when 1 then 'Saldo Inicial' end ) as DocumentoFuente, pa.DocumentDate as FechaDocumento, tp.nit as Nit, tp.name as Cliente, pa.Value as ValorAnticipo, pa.DebitValue as NotasDB, 
pa.CreditValue as NotasCR, pa.TransferValue as ValorCruzado, pa.DistributionValue as ValorDistribuido, pa.Balance as Saldo

  from Portfolio.PortfolioAdvance as pa left outer join treasury.CashReceipts as cr on pa.cashreceiptid = cr.id
inner join common.ThirdParty as tp on pa.ThirdPartyid = tp.id and tp.id in (select ThirdPartyId from Common.Customer)
where pa.Status = 2

)
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida el estado de los anticipos de cartera activos (estado = 2), cruzando cada anticipo con su recibo de caja origen o identificándolo como saldo inicial cuando corresponde. Presenta por cliente (NIT, nombre) los valores recibidos, notas débito/crédito aplicadas, montos cruzados, distribuidos y saldo disponible, para facilitar la conciliación y seguimiento de anticipos pendientes de aplicar a facturas.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticipos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticipos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el cruce de anticipos de cartera de clientes mostrando documento fuente, valores de notas débito/crédito, valores cruzados, distribuidos y saldo pendiente.', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticipos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de anticipos en Portfolio.PortfolioAdvance con Status = 2; El tercero del anticipo debe existir en common.ThirdParty y estar registrado en Common.Customer', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticipos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen anticipos con Status = 2; Solo se incluyen terceros que estén registrados como clientes (Common.Customer); Cada anticipo se asocia opcionalmente a un único recibo de caja; si no existe y es saldo inicial, se etiqueta como ''Saldo Inicial''', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticipos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Anticipos de cartera; Recibos de caja; Terceros; Clientes; Saldo inicial; Notas débito; Notas crédito; Valor cruzado; Valor distribuido; Saldo de anticipo', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticipos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Portfolio.PortfolioAdvance: Cuando PortfolioAdvance.Status = 2 y el tercero pertenece a Common.Customer, se retorna una fila con la información del anticipo y su documento fuente', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticipos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PortfolioAdvance.OpeningBalance = 1 y no existe CashReceipt asociado → El documento fuente se rotula como ''Saldo Inicial'' else Se muestra el código del recibo de caja (CashReceipts.Code) cuando exista', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticipos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioAdvance; treasury.CashReceipts; common.ThirdParty; Common.Customer', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticipos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'ViewInternal', @level1type=N'VIEW', @level1name=N'VistaCruceAnticipos';
GO
