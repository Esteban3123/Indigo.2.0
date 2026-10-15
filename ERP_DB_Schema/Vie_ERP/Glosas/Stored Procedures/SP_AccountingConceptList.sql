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