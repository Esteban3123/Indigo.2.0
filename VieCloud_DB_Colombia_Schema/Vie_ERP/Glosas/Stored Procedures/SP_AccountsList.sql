-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 28/03/2014
-- Description:	Sp para listar las cuentas contables de DGH VERSION NET
-- =============================================
CREATE PROCEDURE [Glosas].[SP_AccountsList]
@Empresa as varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @sql nvarchar(MAX)

	Declare @tablaTmp table(
	AccountCode  varchar(100),
	AccountName varchar(100),
	AccountNameCode varchar(200)
	)

	SET NOCOUNT ON;
	/*set @sql = 'SELECT RTRIM(LTRIM(cpccodcue)) as AccountCode, RTRIM(LTRIM(cpcnomcue)) AS AccountName,
	RTRIM(LTRIM(cpccodcue)) + '' - '' +  RTRIM(LTRIM(cpcnomcue)) AS AccountNameCode
	FROM ' + @Empresa + '..ctPlaCue WHERE  cpcmancen=1 and cpctipcue = ''5'' '*/

	set @sql = 'SELECT RTRIM(LTRIM(CUECODIGO)) as AccountCode, RTRIM(LTRIM(CUENOMBRE)) AS AccountName,
	RTRIM(LTRIM(CUECODIGO)) + '' - '' +  RTRIM(LTRIM(CUENOMBRE)) AS AccountNameCode
	FROM ' + @Empresa + '..CTNCUENTA WHERE  /*CUEmancen=1 and*/ CTNNIVEL = ''5'' '

	print @sql
	insert into @tablaTmp
	execute sp_executesql @sql

	select * from @tablaTmp
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las cuentas contables de nivel 5 (cuentas de detalle) registradas en el plan de cuentas de una empresa contable específica. Recibe como parámetro el nombre de la base de datos de la empresa y consulta dinámicamente la tabla CTNCUENTA de esa empresa para obtener el código y nombre de cada cuenta. Devuelve un listado con el código de cuenta, el nombre de la cuenta y una combinación de ambos, útil para seleccionar cuentas contables en el módulo de Glosas al momento de clasificar o registrar una glosa contra una cuenta del plan contable.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_AccountsList';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_AccountsList';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las cuentas contables de nivel 5 (cuentas de detalle) desde el plan de cuentas de una empresa indicada dinámicamente, devolviendo código, nombre y su concatenación.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una base de datos cuyo nombre coincida con el valor recibido como empresa.; La base destino debe contener la tabla CTNCUENTA con columnas CUECODIGO, CUENOMBRE y CTNNIVEL.; El usuario ejecutor requiere permisos para consultar dicha base mediante SQL dinámico.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se retornan cuentas de nivel 5 del plan contable.; Los valores de código y nombre se entregan sin espacios al inicio o final.; El filtro original por manejo de centro de costo (CUEmancen=1) está comentado y por lo tanto no se aplica.; La consulta se ejecuta contra la base de datos pasada por parámetro vía SQL dinámico.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'plan de cuentas contables; cuentas contables de detalle (nivel 5); multiempresa', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente las cuentas donde CTNNIVEL=''5'', con código y nombre recortados (LTRIM/RTRIM) y un campo concatenado ''codigo - nombre''.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'CTNCUENTA', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList';
-- GO
