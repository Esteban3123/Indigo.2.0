-- =============================================
-- Author:		Rafael Eduardo Patiño
-- Create date: 24/04/2014
-- Description:	Sp para listar los tipo de documentos
-- =============================================
CREATE PROCEDURE [Glosas].[SP_TypeDocumentList__FOX]
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
	set @sql = 'SELECT  RTRIM(LTRIM(CCDCODCOM)) as Code , RTRIM(LTRIM(CCDNOMCOM)) as Name,CCDNUMCOM as Consecutive, RTRIM(LTRIM(CCDCODCOM)) + '' - '' +  RTRIM(LTRIM(CCDNOMCOM)) CodeName  FROM ' +  @Empresa + '..CTCOMDIA '

	print @sql
	insert into @tablaTmp
	execute sp_executesql @sql

	select * from @tablaTmp
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los tipos de documento de identidad disponibles en el sistema, consultando la tabla maestra de códigos comunes (CTCOMDIA) de la empresa indicada. Devuelve el código, nombre y una descripción combinada de cada tipo de documento (por ejemplo: CC - Cédula de Ciudadanía). Se utiliza en el módulo de Glosas para poblar listas desplegables de selección de tipo de documento al registrar o gestionar glosas.', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_TypeDocumentList__FOX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'PROCEDURE', @level1name = N'SP_TypeDocumentList__FOX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los tipos de documento (comprobantes diarios) de una empresa contable, devolviendo código, nombre, consecutivo y una etiqueta concatenada código-nombre.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El nombre de la base de datos de la empresa debe ser válido y accesible desde el servidor.; La base destino debe contener la tabla CTCOMDIA con las columnas CCDCODCOM, CCDNOMCOM y CCDNUMCOM.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La consulta se construye dinámicamente sobre la base recibida, por lo que los datos siempre provienen de la tabla CTCOMDIA de esa empresa.; Los campos Code y Name siempre se entregan sin espacios iniciales/finales (RTRIM/LTRIM).; El campo CodeName siempre tiene el formato ''Code - Name''.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Tipo de documento; Comprobante diario contable; Empresa (multiempresa por base de datos)', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve el listado completo de registros de CTCOMDIA de la empresa indicada, formateados con TRIM y con campo CodeName = Code + '' - '' + Name.', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'CTCOMDIA', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList__FOX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Glosas', @level1type=N'PROCEDURE', @level1name=N'SP_TypeDocumentList__FOX';
-- GO
