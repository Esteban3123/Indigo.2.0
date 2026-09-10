-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 18/09/2013
-- Description:	Sp para listar los conceptos de DGH
-- =============================================
CREATE PROCEDURE [Glosas].[SP_AccountingConceptList__FOX]
@Empresa as varchar(100),
@ConceptType integer	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	declare @sql nvarchar(MAX)

	Declare @tablaTmp table(
	Conceptcode  varchar(20),
	ConceptName varchar(150),
	ConceptNameCode varchar(200)
	)

	IF @ConceptType = 0 BEGIN

	 set @sql = 'select RTRIM(LTRIM(cnoCodCon)) as Conceptcode,RTRIM(LTRIM(cnonomcon)) as ConceptName, RTRIM(LTRIM(cnoCodCon)) + '' - '' +  RTRIM(LTRIM(cnonomcon)) AS ConceptNameCode   from '  + @Empresa + '..crConcep  '
	
	END ELSE IF @ConceptType = 1 BEGIN
	
	---	Metodo Privado concepto cartera 1 
	 set @sql = 'select RTRIM(LTRIM(cnoCodCon)) as Conceptcode,RTRIM(LTRIM(cnonomcon)) as ConceptName, RTRIM(LTRIM(cnoCodCon)) + '' - '' +  RTRIM(LTRIM(cnonomcon)) AS ConceptNameCode   from '  + @Empresa + '..crConcep WHERE cnoconglo=1 '
	   
	END ELSE IF @ConceptType = 2 BEGIN
	
	--Metodo Privado concepto aceptacion nota credito 2
	 set @sql =  'select  RTRIM(LTRIM(cnoCodCon)) as Conceptcode,RTRIM(LTRIM(cnonomcon)) as ConceptName, RTRIM(LTRIM(cnoCodCon)) + '' - '' +  RTRIM(LTRIM(cnonomcon)) AS ConceptNameCode  from ' + @Empresa + '..crConcep WHERE cnoTipnot = 1 and cnomanter = 2 and cnoafecar= 2 and cnoconglo=1 and cnoafeban=1'

	END ELSE IF @ConceptType = 3 BEGIN
	
	--Metodo Privado concepto pago nota credito 3
	 set @sql =  'select  RTRIM(LTRIM(cnoCodCon)) as Conceptcode,RTRIM(LTRIM(cnonomcon)) as ConceptName, RTRIM(LTRIM(cnoCodCon)) + '' - '' +  RTRIM(LTRIM(cnonomcon)) AS ConceptNameCode  from ' + @Empresa + '..crConcep WHERE  cnoTipnot = 2 and cnoconglo=1 '

	END ELSE IF @ConceptType = 4 BEGIN
	
	--Metodo Privado concepto recibos de caja 4 -- naturaleza credito
	 set @sql = 'select  RTRIM(LTRIM(tcrcodcon)) as Conceptcode,RTRIM(LTRIM(tcrnomcon)) as ConceptName, RTRIM(LTRIM(tcrcodcon)) + '' - '' +  RTRIM(LTRIM(tcrnomcon)) AS ConceptNameCode FROM ' + @Empresa + '..tsconrec WHERE TCRNATCON = 2 '

	END ELSE IF @ConceptType = 5 BEGIN
	
	--Metodo Publico concepto 	
	 set @sql =  'select RTRIM(LTRIM(cnocodcon)) as Conceptcode,RTRIM(LTRIM(cnonomcon)) as ConceptName, RTRIM(LTRIM(cnocodcon)) + '' - '' +  RTRIM(LTRIM(cnonomcon)) AS ConceptNameCode  from ' + @Empresa + '..crConcep WHERE  cnomanter = 2 and cnoafecar= 2 and cnoconglo=1 and cnoafeban=1 and cnotipnot=2'
	END
	

	print @sql
	INSERT INTO @tablatmp
	execute sp_executesql @sql

	select * from @tablatmp
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los conceptos contables de cartera (DGH) disponibles en una empresa del ERP, filtrando según el tipo de concepto solicitado. Soporta hasta seis categorías: todos los conceptos, conceptos de glosa, conceptos para aceptación de nota crédito, conceptos para pago de nota crédito, conceptos de recibos de caja con naturaleza crédito, y conceptos de método público. Se usa principalmente en el módulo de Glosas para poblar listas desplegables al registrar movimientos contables relacionados con glosas, notas crédito y recaudos de cartera.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_AccountingConceptList__FOX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_AccountingConceptList__FOX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista conceptos contables (cartera, notas crédito, recibos de caja) desde la base de la empresa indicada, filtrando según el tipo de concepto solicitado para uso en el módulo de glosas.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La empresa recibida debe corresponder al nombre de una base de datos accesible que contenga las tablas crConcep y tsconrec.; El tipo de concepto debe estar en el rango 0..5; otros valores no producen consulta y devuelven resultado vacío.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los resultados siempre traen los campos recortados (RTRIM/LTRIM) y un campo compuesto ''código - nombre''.; La consulta se ejecuta dinámicamente sobre la base de la empresa pasada como parámetro, permitiendo multiempresa.; Los conceptos de cartera/notas crédito siempre exigen cnoconglo=1 (afectan glosas).; Sólo se devuelven datos si el tipo coincide con uno de los seis casos definidos.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Concepto contable; Cartera; Nota crédito (aceptación y pago); Recibo de caja; Glosa; Multiempresa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @tablatmp: Cuando tipo=0: inserta todos los conceptos de crConcep sin filtro.; [INSERT] @tablatmp: Cuando tipo=1 (concepto cartera): inserta de crConcep donde cnoconglo=1.; [INSERT] @tablatmp: Cuando tipo=2 (aceptación nota crédito): inserta de crConcep donde cnoTipnot=1, cnomanter=2, cnoafecar=2, cnoconglo=1 y cnoafeban=1.; [INSERT] @tablatmp: Cuando tipo=3 (pago nota crédito): inserta de crConcep donde cnoTipnot=2 y cnoconglo=1.; [INSERT] @tablatmp: Cuando tipo=4 (recibos de caja): inserta de tsconrec donde TCRNATCON=2 (naturaleza crédito).; [INSERT] @tablatmp: Cuando tipo=5 (concepto público): inserta de crConcep donde cnomanter=2, cnoafecar=2, cnoconglo=1, cnoafeban=1 y cnotipnot=2.; [RETURN_RESULT] @tablatmp: Devuelve el contenido completo de la tabla temporal con código, nombre y la concatenación ''código - nombre''.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @ConceptType = 0 → Lista todos los conceptos de crConcep sin filtros.; si @ConceptType = 1 → Lista conceptos de cartera (cnoconglo=1).; si @ConceptType = 2 → Lista conceptos para aceptación de nota crédito (tipnot=1, manter=2, afecar=2, conglo=1, afeban=1).; si @ConceptType = 3 → Lista conceptos para pago de nota crédito (tipnot=2, conglo=1).; si @ConceptType = 4 → Lista conceptos de recibos de caja con naturaleza crédito (TCRNATCON=2) desde tsconrec.; si @ConceptType = 5 → Lista conceptos públicos (manter=2, afecar=2, conglo=1, afeban=1, tipnot=2).', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'crConcep; tsconrec', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList__FOX';
-- GO
