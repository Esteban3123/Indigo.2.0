-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 18/09/2013
-- Description:	Sp para listar los conceptos de DGH
-- =============================================
CREATE PROCEDURE [Glosas].[SP_AccountingConceptList]
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
/*
	IF @ConceptType = 1 BEGIN
	
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
	
	*/
	set @sql = 'select RTRIM(LTRIM(CONCODIGO)) as Conceptcode,RTRIM(LTRIM(CONNOMBRE)) as ConceptName, RTRIM(LTRIM(CONCODIGO)) + '' - '' +  RTRIM(LTRIM(CONNOMBRE)) AS ConceptNameCode from '  + @Empresa + '..crNConNOT conc'
	 

	print @sql
	INSERT INTO @tablatmp
	execute sp_executesql @sql

	select * from @tablatmp
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los conceptos contables de glosas disponibles en el sistema DGH, consultando la tabla de conceptos de notas (crNConNOT) en la base de datos de la empresa indicada. Devuelve el código del concepto, su nombre y una descripción combinada (código - nombre) lista para mostrar en listas desplegables o formularios de gestión de glosas. Recibe como parámetros la empresa (base de datos contable) y el tipo de concepto, aunque actualmente el filtro por tipo está desactivado y se retornan todos los conceptos sin distinción de tipo (cartera, nota crédito, recibo de caja, etc.).', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_AccountingConceptList';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_AccountingConceptList';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los conceptos contables de notas (código, nombre y combinación código-nombre) desde la base de datos de la empresa indicada para uso en gestión de glosas.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El nombre de base de datos recibido debe corresponder a una BD accesible que contenga la tabla crNConNOT con las columnas CONCODIGO y CONNOMBRE; El servidor debe permitir SQL dinámico vía sp_executesql', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los valores de código y nombre se retornan siempre sin espacios en blanco al inicio o al final (RTRIM/LTRIM); El campo combinado siempre tiene el formato ''CODIGO - NOMBRE''; La consulta se realiza dinámicamente sobre la base de datos pasada como parámetro, permitiendo multiempresa; El parámetro de tipo de concepto actualmente no afecta el resultado (lógica de ramificación está comentada)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Glosas; Conceptos contables; Notas (crédito/débito); Multiempresa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] tabla temporal de conceptos: Inserta el resultado del SELECT dinámico sobre crNConNOT en la tabla variable interna antes de retornarla; [RETURN_RESULT] resultset: Devuelve todos los conceptos consultados con código, nombre y la concatenación ''código - nombre''', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'crNConNOT', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountingConceptList';
-- GO
