-- =============================================
-- Author:		Yefersson Caicedo
-- Create date: 25/10/2023
-- Description:	se crea sp para listar las actividades de enfermeria para el PBI - 11122 San José-EHR-2. Ajuste al reporte Hoja de procedimientos de enfermería
-- =============================================
CREATE PROCEDURE [dbo].[SP_HC_ListarHojaProcedimientoEnfermeriaNuevo] 
	-- Add the parameters for the stored procedure here
	@condiciones varchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @SQl as nvarchar(max)

	set @SQl = '
				select CANACTENF,VALACTENF,RTRIM(B.DESPRODUC) AS ''DESCRIPCION DEL PRODUCTO'',CANUTIPRO AS ''CANTIDAD UTILIZADA DEL PRODUCTO'',  RTRIM(A.CODPRODUC) AS ''CODIGO DEL PRODUCTO''
				, CAST(1 AS INT) AS NUMERO, CAST(CAST(DAY(A.FECHAUTIL) AS CHAR) + ''/'' + CAST(MONTH(A.FECHAUTIL) AS CHAR) + ''/'' + CAST(YEAR(A.FECHAUTIL) AS CHAR) AS DATETIME) AS DIAS
				, A.FECHAUTIL AS DIAS2, RTRIM(UFUDESCRI) AS ''DESCRIPCION UNIDAD FUNCIONAL'',ACT.DESACTENF AS ''ACTIVIDAD ENFERMERIA'',A.VALACTENF AS ''RESULTADO DEL PROCEDIMIENTO''
				, DOC.MEDIFIRMA AS ''FIRMA PROFESIONAL'', DOC.NOMMEDICO as ''NOMBRE PROFESIONAL'',Spec.DESESPECI as ''NOMBRE ESPECIALIDAD'', DOC.TARJETAPR as ''TARJETA PROFESIONAL'', T.NOMBRE AS ''TIPO IDENTIFICACION'', rtrim(DOC.CODIGONIT) AS ''IDENTIFICACION PROFESIONAL'', iif(AG.NOMBRE is null, ''Productos'', ''Paquete de enfermería - '') as ''AGRUPADOR'',iif(AG.NOMBRE is null, '' '',AG.NOMBRE) as ''NOMBRE PAQUETE''
				, A.FECHAUTIL AS FECHA, A.OBSERVACI AS OBSERVACION, A.CANACTENF AS  ''CANTIDAD ACTIVIDAD ENFERMERIA'', A.VALACTENF AS ''RESULTADO ACTIVIDAD ENFERMERIA''
					from dbo.HCHOGASIN A
					INNER JOIN dbo.IHLISTPRO B ON B.CODPRODUC = A.CODPRODUC
					LEFT JOIN dbo.HCACTENFE ACT ON ACT.CODACTENF = A.CODACTENF
					INNER JOIN dbo.INUNIFUNC C  with (nolock) ON A.UFUCODIGO = C.UFUCODIGO
					INNER JOIN dbo.INPROFSAL DOC  with (nolock) ON A.CODPROSAL = DOC.CODPROSAL
					INNER JOIN dbo.INESPECIA Spec ON DOC.CODESPEC1 = Spec.CODESPECI
					LEFT JOIN AGPAQUETES AG ON AG.ID = A.IdNursingPackage
					LEFT JOIN  dbo.ADTIPOIDENTIFICA T ON DOC.IDADTIPOIDENTIFICA = T.ID 
	                where ' + @condiciones + '
			   '
	exec sp_executesql  @SQl 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las actividades y procedimientos de enfermería registrados en la hoja de procedimientos, incluyendo los productos o insumos utilizados, las cantidades, fechas de utilización, unidad funcional donde se realizó y datos del profesional de salud responsable (nombre, identificación, tarjeta profesional y especialidad). También distingue si el procedimiento corresponde a un producto individual o a un paquete de enfermería. Se utiliza para generar el reporte ''Hoja de procedimientos de enfermería'' en Power BI, aplicando condiciones de filtro dinámicas en tiempo de ejecución sobre las tablas de actividades de enfermería (HCHOGASIN), insumos/productos (IHLISTPRO), unidades funcionales, profesionales de salud y paquetes de enfermería.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHojaProcedimientoEnfermeriaNuevo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_ListarHojaProcedimientoEnfermeriaNuevo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las actividades de enfermería y productos consumidos durante la atención, junto con datos del profesional responsable y agrupador de paquete, para alimentar el reporte "Hoja de procedimientos de enfermería".', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeriaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe recibir una cadena de condiciones SQL válida que se concatena directamente al WHERE final.; Deben existir registros relacionables en HCHOGASIN con catálogos de productos (IHLISTPRO), unidad funcional (INUNIFUNC), profesional (INPROFSAL) y especialidad (INESPECIA); de lo contrario el registro se excluye por ser INNER JOIN.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeriaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran consumos cuyo producto exista en el catálogo IHLISTPRO y cuya unidad funcional, profesional y especialidad existan (INNER JOIN).; La actividad de enfermería, el paquete y el tipo de identificación del profesional son opcionales (LEFT JOIN), no excluyen el registro.; La especialidad reportada corresponde siempre a la especialidad principal (CODESPEC1) del profesional.; La fecha del procedimiento se expone tanto en formato DATETIME truncado a día como en su valor original.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeriaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Actividad de enfermería; Hoja de procedimientos de enfermería; Consumo de productos/insumos; Profesional de la salud; Especialidad médica; Unidad funcional; Paquete de enfermería; Tarjeta profesional; Tipo de identificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeriaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un conjunto con datos del consumo, actividad de enfermería, profesional y agrupador, filtrado dinámicamente por @condiciones.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeriaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AG.NOMBRE IS NULL (no hay paquete de enfermería asociado vía IdNursingPackage) → El agrupador se etiqueta como ''Productos'' y el nombre del paquete queda en blanco. else El agrupador se etiqueta como ''Paquete de enfermería - '' y se muestra el nombre del paquete (AG.NOMBRE).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeriaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'sys.sp_executesql', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeriaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOGASIN; dbo.IHLISTPRO; dbo.HCACTENFE; dbo.INUNIFUNC; dbo.INPROFSAL; dbo.INESPECIA; dbo.AGPAQUETES; dbo.ADTIPOIDENTIFICA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeriaNuevo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_ListarHojaProcedimientoEnfermeriaNuevo';
-- GO
