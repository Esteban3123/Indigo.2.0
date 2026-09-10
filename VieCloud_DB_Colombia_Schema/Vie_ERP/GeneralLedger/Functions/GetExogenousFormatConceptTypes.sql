-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-01-10
-- Description:	Obtiene las tipos de conceptos de información exógena
-- =============================================
CREATE FUNCTION [GeneralLedger].[GetExogenousFormatConceptTypes]
(	
)
RETURNS @ConceptType TABLE 
(
	[Format] INT,
	[ConceptType] INT,
	[ConceptTypeName] VARCHAR(MAX),
	[ConceptTypeAbbreviation] VARCHAR(20)
)
AS
BEGIN

	INSERT INTO @ConceptType VALUES
		(1001, 1, 'Pago o abono en cuenta deducible', 'pago'),
		(1001, 2, 'Pago o abono en cuenta NO deducible', 'pnded'),
		(1001, 3, 'IVA mayor valor del costo o gasto, deducible', 'ided'),
		(1001, 4, 'IVA mayor valor del costo o gasto, no deducible', 'inded'),
		(1001, 5, 'Retención en la fuente practicada Renta', 'retp'),
		(1001, 6, 'Retención en la fuente asumida Renta', 'reta'),
		(1001, 7, 'Retención en la fuente practicada IVA Régimen común', 'comun'),
		(1001, 8, 'Retención en la fuente practicada IVA no domiciliados', 'ndom'),
		/********************************************************************/
		(1003, 1, 'Valor acumulado del pago o abono sujeto a Retención en la fuente', 'valor'),
		(1003, 2, 'Retención que le practicaron', 'ret'),
		/********************************************************************/
		(1004, 1, 'Valor del pago o abono en cuenta', 'vpag'),
		(1004, 2, 'Valor del descuento tributario', 'vdes'),
		/********************************************************************/
		(1005, 1, 'Impuesto descontable', 'vimp'),
		(1005, 2, 'IVA resultante por devoluciones en ventas anuladas, rescindidas o resueltas', 'ivade'),
		/********************************************************************/
		(1006, 1, 'Impuesto generado', 'imp'),
		(1006, 2, 'IVA recuperado en devoluciones en compras anuladas, rescindidas o resueltas', 'iva'),
		(1006, 3, 'Impuesto al consumo', 'icon'),
		/********************************************************************/
		(1007, 1, 'Ingresos brutos recibidos', 'ibru'),
		(1007, 2, 'Devoluciones, rebajas y descuentos', 'dred'),
		/********************************************************************/
		(1008, 1, 'Saldo cuentas por cobrar al 31-12', 'sal'),
		/********************************************************************/
		(1009, 1, 'Saldo cuentas por pagar al 31-12', 'sal'),
		/********************************************************************/
		(1010, 1, 'Valor patrimonial acciones o aportes al 31-12', 'val'),
		(1010, 2, 'Porcentaje de participación', 'por'),
		(1010, 3, 'Porcentaje de participación (posición decimal)', 'dec'),
		/********************************************************************/
		(1011, 1, 'Saldos al 31-12', 'sal'),
		/********************************************************************/
		(1012, 1, 'Valor al 31-12', 'val'),
		/********************************************************************/
		(1056, 1, 'Pago o abono en cuenta', 'pag'),
		(1056, 2, 'IVA mayor valor del costo o gasto', 'iva'),
		(1056, 3, 'Retención en la fuente practicada RENTA', 'rpren'),
		(1056, 4, 'Retención en la fuente asumida RENTA', 'raren'),
		(1056, 5, 'Retención en la fuente practicada IVA Régimen Común', 'rpirc'),
		(1056, 6, 'Retención en la fuente practicada IVA no domiciliados', 'rpind'),
		/*******************************************************************/
		(1647, 1, 'Valor total de la operación', 'vtotal'),
		(1647, 2, 'Valor ingreso reintegrado transferido distribuido al tercero', 'ving'),
		(1647, 3, 'Valor Retención reintegrada transferida distribuida al tercero', 'vret'),
		/********************************************************************/
		(2275, 1, 'Valor total del ingreso', 'vtotali'),
		(2275, 2, 'Valor del ingreso no constitutivo de renta ni ganancia ocasional', 'vrenta'),
		/********************************************************************/
		(2276, 1, 'Pagos por salarios', 'pasa'),
		(2276, 2, 'Pagos por emolumentos eclesiásticos', 'paec'),
		(2276, 3, 'Pagos por honorarios', 'paho'),
		(2276, 4, 'Pagos por servicios', 'pase'),
		(2276, 5, 'Pagos por comisiones', 'paco'),
		(2276, 6, 'Pagos por prestaciones sociales', 'papre'),
		(2276, 7, 'Pagos por viáticos', 'pavia'),
		(2276, 8, 'Pagos por gastos de representación', 'paga'),
		(2276, 9, 'Pagos por compensaciones trabajo asociado cooperativo', 'patra'),
		(2276, 10, 'Otros pagos', 'potro'),
		(2276, 11, 'Pagos realizados con bonos electrónicos o de papel de servicio, cheques, tarjetas, vales, etc.', 'pabo'),
		(2276, 12, 'Cesantías e intereses de censantías efectivamente pagadas, consignadas o reconocidas en el periodo', 'cein'),
		(2276, 13, 'Pensiones de Jubilación, vejez o invalidez', 'peju'),
		(2276, 14, 'Aportes Obligatorios por Salud', 'apos'),
		(2276, 15, 'Aportes obligatorios a fondos de pensiones y solidaridad pensional y Aportes voluntarios al RAIS', 'apof'),
		(2276, 16, 'Aportes voluntarios a fondos de pensiones voluntarias', 'apov'),
		(2276, 17, 'Aportes a cuentas AFC', 'apafc'),
		(2276, 18, 'Valor de las retenciones en la fuente por pagos de rentas de trabajo o pensiones', 'vare')
	RETURN
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función de tabla que retorna el catálogo de tipos de conceptos utilizados en los formatos de información exógena tributaria (reportes DIAN). Para cada formato (1001, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010, 1011, 1012, 1056, 1647, 2275, 2276) devuelve los tipos de conceptos asociados con su código numérico, nombre descriptivo y abreviatura. Cubre conceptos como pagos y abonos en cuenta, retenciones en la fuente (renta e IVA), impuestos generados y descontables, ingresos brutos, saldos de cuentas por cobrar y por pagar, aportes parafiscales, pagos laborales y patrimonio. Se usa en el módulo de Contabilidad General (GeneralLedger) para parametrizar y clasificar la información exógena que se reporta a la DIAN, garantizando la correcta tipificación de cada concepto en la generación de reportes tributarios.', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'FUNCTION', @level1name = N'GetExogenousFormatConceptTypes';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'GeneralLedger', @level1type = N'FUNCTION', @level1name = N'GetExogenousFormatConceptTypes';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Provee un catálogo estático que mapea formatos de información exógena (DIAN) con sus tipos de concepto, nombre y abreviatura para uso contable/tributario.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetExogenousFormatConceptTypes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El formato 1001 siempre expone 8 tipos de concepto relacionados a pagos, IVA y retenciones en la fuente.; El formato 2276 siempre expone 18 tipos de concepto correspondientes a pagos laborales, aportes y retenciones de rentas de trabajo.; Cada combinación (Format, ConceptType) es única y trae una abreviatura asociada.; El catálogo es estático: no depende de parámetros ni de tablas externas.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetExogenousFormatConceptTypes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Información exógena; Retención en la fuente (Renta); Retención en la fuente IVA Régimen Común; Retención IVA no domiciliados; IVA descontable; IVA generado; Impuesto al consumo; Ingresos brutos; Devoluciones, rebajas y descuentos; Cuentas por cobrar; Cuentas por pagar; Valor patrimonial de acciones/aportes; Descuento tributario; Pagos por salarios; Emolumentos eclesiásticos; Honorarios; Servicios; Comisiones; Prestaciones sociales; Viáticos; Gastos de representación; Compensaciones trabajo asociado cooperativo; Cesantías e intereses de cesantías; Pensiones de jubilación, vejez o invalidez; Aportes obligatorios a salud; Aportes a fondos de pensiones (obligatorios y voluntarios, RAIS); Aportes a cuentas AFC; Rentas de trabajo', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetExogenousFormatConceptTypes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @ConceptType: Siempre inserta el catálogo fijo de tipos de concepto agrupados por formato exógeno (1001, 1003-1012, 1056, 1647, 2275, 2276) con su código, descripción y abreviatura.; [RETURN_RESULT] @ConceptType: Retorna la tabla con todas las combinaciones Format/ConceptType/ConceptTypeName/ConceptTypeAbbreviation precargadas.', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetExogenousFormatConceptTypes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'GeneralLedger', @level1type=N'FUNCTION', @level1name=N'GetExogenousFormatConceptTypes';
GO
