-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 14/09/2013
-- Description:	Sp para listar las cuentas contables de DGH
-- =============================================
CREATE PROCEDURE [Glosas].[SP_AccountsList__FOX]
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
	set @sql = 'SELECT RTRIM(LTRIM(cpccodcue)) as AccountCode, RTRIM(LTRIM(cpcnomcue)) AS AccountName,
	RTRIM(LTRIM(cpccodcue)) + '' - '' +  RTRIM(LTRIM(cpcnomcue)) AS AccountNameCode
	FROM ' + @Empresa + '..ctPlaCue  WHERE /* cpcmancen=''2'' and cpcmanter=''2'' and */  cpctipcue = ''5''  '

	print @sql
	insert into @tablaTmp
	execute sp_executesql @sql

	select * from @tablaTmp
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las cuentas contables del plan de cuentas de una empresa específica, filtrando únicamente las cuentas de tipo ''5'' (cuentas de resultado o gasto, según el catálogo contable DGH/FOX). Recibe como parámetro el nombre de la empresa o base de datos y consulta dinámicamente la tabla del plan de cuentas (ctPlaCue) de esa empresa. Devuelve el código de cuenta, el nombre de la cuenta y una combinación código-nombre lista para usar en listas desplegables o selectores dentro del módulo de Glosas. Se utiliza para asociar cuentas contables a conceptos de glosa o facturación en el proceso de gestión de cartera y cuentas médicas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_AccountsList__FOX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_AccountsList__FOX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las cuentas contables de tipo gasto (cpctipcue=''5'') desde el plan de cuentas de una empresa externa indicada dinámicamente, retornando código, nombre y su concatenación.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una base de datos cuyo nombre coincida con el parámetro de empresa recibido.; La base de datos referenciada debe contener la tabla ctPlaCue con las columnas cpccodcue, cpcnomcue y cpctipcue.; El invocador requiere permisos para ejecutar SQL dinámico sobre la base externa.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen cuentas contables del tipo ''5'' (clase gasto en planes contables tipo DGH).; Los valores retornados siempre se entregan sin espacios iniciales o finales.; El campo concatenado siempre se construye como ''código - nombre''.; Los filtros por cpcmancen y cpcmanter están comentados, por lo que no se aplican restricciones de manejo por centro ni tercero.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuentas contables; Plan de cuentas (DGH); Tipo de cuenta', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve únicamente las cuentas contables donde cpctipcue=''5'', con código y nombre depurados de espacios mediante LTRIM/RTRIM.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'{Empresa}..ctPlaCue', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_AccountsList__FOX';
-- GO
