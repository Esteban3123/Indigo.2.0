
CREATE VIEW [Common].[ViewThirdPartyDocuments]
AS
select convert( varchar(20), IdThirdParty) + Code+'635'  as [Key], IdThirdParty as ThirdPartyId, 'Recibos de Caja' as Process, Code,DocumentDate as DocumentDate,Status as Status,'635' as TagForm  from Treasury.CashReceipts 
union all
select convert( varchar(20), IdThirdParty)+Code+'636' as [Key],IdThirdParty as ThirdPartyId, 'Comprobantes de Egreso' as Process, Code,DocumentDate as DocumentDate,Status as Status,'636' as TagForm  from Treasury.VoucherTransaction where IdThirdParty is not null
union all 
select convert( varchar(20), ThirdPartyId)+Code+'682' as [Key],ThirdPartyId  as ThirdPartyId, 'Cuentas por Cobrar' as Process, Code,AccountReceivableDate  as DocumentDate,Status as Status,'682' as TagForm  from Portfolio.AccountReceivable 
union all
select convert( varchar(20), ThirdPartyId)+Code+'1507' as [Key],ThirdPartyId  as ThirdPartyId, 'Anticipos de Cartera' as Process, Code,DocumentDate as DocumentDate,Status as Status,'' as TagForm  from Portfolio.PortfolioAdvance
union all
select convert( varchar(20), IdThirdParty)+Code+'730' as [Key],IdThirdParty as ThirdPartyId, 'Cuentas por Pagar' as Process, Code,DocumentDate as DocumentDate,Status as Status,'730' as TagForm  from Payments.AccountPayable where IdThirdParty is not null
union all 
select convert( varchar(20), IdThirdParty)+Code+'999' as [Key],IdThirdParty as ThirdPartyId, 'Anticipos de Pagos' as Process, Code,DocumentDate as DocumentDate,Status as Status,'' as TagForm  from Payments.AdvancePayments  where IdThirdParty is not null
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista unificada de todos los documentos financieros asociados a un tercero (paciente, proveedor, aseguradora o cliente). Consolida en una sola consulta seis tipos de movimientos: recibos de caja, comprobantes de egreso, cuentas por cobrar, anticipos de cartera, cuentas por pagar y anticipos de pagos, tomando datos de tesorería, cartera y pagos. Cada fila incluye un identificador único del tercero, el código del documento, la fecha, el estado y el tipo de proceso al que pertenece. Sirve para consultar de forma centralizada el historial financiero completo de un tercero, facilitando reportería de cartera, tesorería y cuentas por pagar sin necesidad de consultar cada módulo por separado.', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'VIEW', @level1name = N'ViewThirdPartyDocuments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Common', @level1type = N'VIEW', @level1name = N'ViewThirdPartyDocuments';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una sola lista los documentos de tesorería, cartera y pagos asociados a terceros (recibos, egresos, cuentas por cobrar/pagar y anticipos) para consulta unificada por tercero.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewThirdPartyDocuments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila se identifica con una llave compuesta por el id del tercero, el código del documento y un sufijo numérico que representa el tipo de proceso (635, 636, 682, 1507, 730, 999).; Los procesos de Comprobantes de Egreso, Cuentas por Pagar y Anticipos de Pagos solo se incluyen cuando el documento tiene tercero asociado (IdThirdParty no nulo).; Los procesos de Recibos de Caja, Cuentas por Cobrar y Anticipos de Cartera se incluyen sin filtrar por tercero.; El TagForm queda vacío para Anticipos de Cartera y Anticipos de Pagos; los demás procesos exponen el código de formulario asociado.; La etiqueta de proceso (Process) está fija por origen: Recibos de Caja, Comprobantes de Egreso, Cuentas por Cobrar, Anticipos de Cartera, Cuentas por Pagar y Anticipos de Pagos.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewThirdPartyDocuments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Recibos de Caja; Comprobantes de Egreso; Cuentas por Cobrar; Anticipos de Cartera; Cuentas por Pagar; Anticipos de Pagos; Tercero', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewThirdPartyDocuments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Treasury.CashReceipts: Expone todos los Recibos de Caja con TagForm ''635'' y proceso ''Recibos de Caja''.; [RETURN_RESULT] Treasury.VoucherTransaction: WHERE IdThirdParty IS NOT NULL → expone Comprobantes de Egreso con TagForm ''636'' y proceso ''Comprobantes de Egreso''.; [RETURN_RESULT] Portfolio.AccountReceivable: Expone Cuentas por Cobrar con TagForm ''682'' usando AccountReceivableDate como fecha del documento.; [RETURN_RESULT] Portfolio.PortfolioAdvance: Expone Anticipos de Cartera con TagForm vacío y proceso ''Anticipos de Cartera''.; [RETURN_RESULT] Payments.AccountPayable: WHERE IdThirdParty IS NOT NULL → expone Cuentas por Pagar con TagForm ''730'' y proceso ''Cuentas por Pagar''.; [RETURN_RESULT] Payments.AdvancePayments: WHERE IdThirdParty IS NOT NULL → expone Anticipos de Pagos con TagForm vacío y proceso ''Anticipos de Pagos''.', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewThirdPartyDocuments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.CashReceipts; Treasury.VoucherTransaction; Portfolio.AccountReceivable; Portfolio.PortfolioAdvance; Payments.AccountPayable; Payments.AdvancePayments', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewThirdPartyDocuments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Common', @level1type=N'VIEW', @level1name=N'ViewThirdPartyDocuments';
GO
