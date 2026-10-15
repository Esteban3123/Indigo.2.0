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
				, DOC.MEDIFIRMA AS ''FIRMA PROFESIONAL'', DOC.NOMMEDICO as ''NOMBRE PROFESIONAL'',Spec.DESESPECI as ''NOMBRE ESPECIALIDAD'', DOC.TARJETAPR as ''TARJETA PROFESIONAL'', iif(AG.NOMBRE is null, ''Productos'', ''Paquete de enfermería - '') as ''AGRUPADOR'',iif(AG.NOMBRE is null, '' '',AG.NOMBRE) as ''NOMBRE PAQUETE''
				, A.FECHAUTIL AS FECHA, A.OBSERVACI AS OBSERVACION, A.CANACTENF AS  ''CANTIDAD ACTIVIDAD ENFERMERIA'', A.VALACTENF AS ''RESULTADO ACTIVIDAD ENFERMERIA''
					from dbo.HCHOGASIN A
					INNER JOIN dbo.IHLISTPRO B ON B.CODPRODUC = A.CODPRODUC
					LEFT JOIN dbo.HCACTENFE ACT ON ACT.CODACTENF = A.CODACTENF
					INNER JOIN dbo.INUNIFUNC C  with (nolock) ON A.UFUCODIGO = C.UFUCODIGO
					INNER JOIN dbo.INPROFSAL DOC  with (nolock) ON A.CODPROSAL = DOC.CODPROSAL
					INNER JOIN dbo.INESPECIA Spec ON DOC.CODESPEC1 = Spec.CODESPECI
					LEFT JOIN AGPAQUETES AG ON AG.ID = A.IdNursingPackage
					where ' + @condiciones + '
			   '
	exec sp_executesql  @SQl 
END