-- =============================================
-- Author:		Diego Andrés Roldán Lozano
-- Create date: 27-12-2014
-- Description:	
-- =============================================
CREATE PROCEDURE [InteropCost].[SP_MainAccountValue]
	@Container varchar(15), 
    @MainAccountNumber nvarchar(30),
	@Year varchar(4),
	@Month int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	/****** Script for SelectTopNRows command from SSMS  ******/
	declare @sql nvarchar(MAX)
	declare @sqlCuenta nvarchar(MAX)
	declare @MainAccountId int
	declare @tablaTmp table(
		Diference numeric(18,2)
	)

	--set @sql = N'SELECT @MainAccountId = OID FROM [' + @Container + '].[dbo].[CTNCUENTA] Where CUECODIGO = @NumCuenta '
	--exec sp_executesql @sql,N'@NumCuenta nvarchar(30), @MainAccountId int OUTPUT',@NumCuenta= @MainAccountNumber, @MainAccountId = @MainAccountId OUTPUT
	
	set @sqlCuenta = 'SELECT (ABS(SUM([CSCDEBITO]) - SUM([CSCCREDITO]))) as diference
						FROM [' + @Container + '].[dbo].[CTNSAL' + @Year + ']
						where CTNCUENTA = @MainAccountId
						and CSCMES = @Month'
	INSERT INTO @tablaTmp
	exec sp_executesql @sqlCuenta, N'@MainAccountId int, @Month int',@MainAccountId= @MainAccountNumber, @Month = @Month

	SELECT Diference FROM @tablaTmp
	--select SCOPE_IDENTITY() as id;
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento contable que calcula el saldo o diferencia de una cuenta principal del plan de cuentas (cuenta contable mayor) para un contenedor de empresa, año y mes específicos. Consulta la tabla de saldos contables CTNSAL del año indicado, restando los movimientos débito menos crédito (valor absoluto) de la cuenta solicitada. Se utiliza en el módulo de interoperabilidad de costos para obtener el valor neto de una cuenta contable en un período determinado, apoyando procesos de cierre contable, análisis de costos y conciliación financiera.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_MainAccountValue';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'PROCEDURE', @level1name = N'SP_MainAccountValue';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el valor absoluto del saldo (diferencia entre débitos y créditos) de una cuenta contable principal en un contenedor, año y mes específicos, consultando dinámicamente la tabla de saldos correspondiente al año.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_MainAccountValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el esquema/base de datos referenciado por @Container con la tabla CTNSAL{Year} en su esquema dbo.; La tabla dinámica [Container].[dbo].[CTNSAL{Year}] debe existir para el año solicitado.; El identificador de cuenta debe ser numérico ya que se asigna @MainAccountNumber (nvarchar) al parámetro @MainAccountId (int) en sp_executesql.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_MainAccountValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor retornado siempre es no negativo por aplicar ABS sobre la diferencia entre débitos y créditos.; La tabla de saldos se selecciona dinámicamente concatenando ''CTNSAL'' con el año recibido, permitiendo segmentación anual de saldos.; El filtro siempre se realiza por cuenta y mes; no se discrimina por otros criterios contables.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_MainAccountValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'cuenta contable principal (MainAccount); saldo contable (débito/crédito); diferencia/saldo mensual; contenedor multi-empresa (Container); periodo contable (año/mes)', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_MainAccountValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @tablaTmp: Inserta el resultado de SUM(CSCDEBITO)-SUM(CSCCREDITO) en valor absoluto desde [Container].[dbo].[CTNSAL{Year}] filtrando por CTNCUENTA=@MainAccountId y CSCMES=@Month.; [RETURN_RESULT] (resultset): Devuelve la columna Diference desde la tabla temporal @tablaTmp como conjunto de resultados.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_MainAccountValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_MainAccountValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'[Container].dbo.CTNSAL{Year}', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_MainAccountValue';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'PROCEDURE', @level1name=N'SP_MainAccountValue';
-- GO
