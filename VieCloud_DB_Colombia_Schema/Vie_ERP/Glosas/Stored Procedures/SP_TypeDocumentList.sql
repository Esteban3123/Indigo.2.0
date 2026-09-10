
-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 24/04/2014
-- Description:	Sp para listar los tipo de documentos version NET
-- =============================================
CREATE PROCEDURE [Glosas].[SP_TypeDocumentList]
@Empresa as varchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	declare @sql nvarchar(MAX)

	Declare @tablaTmp table(
	Code  varchar(5),
	Name varchar(100),
	Consecutive integer,
	CodeName varchar(150)
	)

	SET NOCOUNT ON;
	set @sql = 'SELECT RTRIM(LTRIM(TCCODIGO )) as Code , RTRIM(LTRIM(TCNOMBRE )) as Name,GENCONSEC as Consecutive, RTRIM(LTRIM(TCCODIGO )) + '' - '' +  RTRIM(LTRIM(TCNOMBRE )) CodeName  FROM ' +  @Empresa + '..CTNTIPCOM '

	print @sql
	insert into @tablaTmp
	execute sp_executesql @sql

	select * from @tablaTmp
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los tipos de documento (por ejemplo: cédula, NIT, pasaporte, tarjeta de identidad) configurados en la empresa seleccionada, consultando la tabla maestra CTNTIPCOM de la base de datos indicada por el parámetro de empresa. Devuelve el código, el nombre y un campo combinado código-nombre útil para poblar listas desplegables en los módulos de glosas y facturación. Se usa para que el usuario pueda seleccionar el tipo de identificación al registrar o filtrar documentos de glosa.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_TypeDocumentList';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_TypeDocumentList';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los tipos de comprobante/documento de una empresa específica, devolviendo código, nombre, consecutivo y una etiqueta concatenada código-nombre.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El nombre de la base de datos (empresa) recibido debe existir y ser accesible desde el servidor.; La base de datos indicada debe contener la tabla CTNTIPCOM con las columnas TCCODIGO, TCNOMBRE y GENCONSEC.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nombre de la empresa se interpola dinámicamente como prefijo de base de datos para CTNTIPCOM (cross-database).; Los valores de Code y Name se entregan siempre sin espacios al inicio/final.; El campo CodeName siempre tiene el formato ''TCCODIGO - TCNOMBRE''.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de documento; Comprobante contable; Empresa (multi-base de datos); Consecutivo', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] tabla temporal en memoria: Devuelve todos los registros de CTNTIPCOM de la empresa indicada con los campos recortados (RTRIM/LTRIM) y un campo concatenado ''Code - Name''.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'CTNTIPCOM', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList';
-- GO
