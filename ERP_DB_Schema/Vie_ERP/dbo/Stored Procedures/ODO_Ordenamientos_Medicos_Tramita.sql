

-- =============================================
-- Author:		Michael Soto
-- Create date: 2022-08-26
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[ODO_Ordenamientos_Medicos_Tramita]
	-- Add the parameters for the stored procedure here
	@inidate DATETIME,
	@enddate DATETIME
AS
BEGIN

	SET NOCOUNT ON;


	CREATE TABLE #ordenes
	(
		centroAtencion VARCHAR(100),
		unidadFuncional VARCHAR(100),
		documentoMedico VARCHAR(15),
		nombreMedico VARCHAR(50),
		especialidad VARCHAR(80),
		fechaOrden VARCHAR(19),
		ingreso INT,
		fechaIngreso VARCHAR(19),
		cama VARCHAR(50),
		tipoEstancia VARCHAR(100),
		folio INT, 
		ID INT,
		codigoServicio VARCHAR(15),
		nombreServicio VARCHAR(MAX),
		codigoDescripcion VARCHAR(10),
		nombreDescripcion VARCHAR(MAX),
		cantidad INT, 
		estadoOrden VARCHAR(25),
		tipoOrden VARCHAR(15),
		esquema VARCHAR(MAX),
		ciclo INT,
		tipoCodigo VARCHAR(25),
		codigoDx VARCHAR(6),
		nombreDx VARCHAR(MAX),
		tipoDocumento VARCHAR(3),
		numeroDocumento VARCHAR(15),
		primerNombre VARCHAR(25),
		segundoNombre VARCHAR(25),
		primerApellido VARCHAR(25),
		segundoApellido VARCHAR(25),
		genero VARCHAR(10),
		edad TINYINT,
		municipio VARCHAR(50),
		departamento VARCHAR(50),
		direccion VARCHAR(150),
		telPrincipal VARCHAR(MAX),
		telSecundario VARCHAR(MAX),
		regimen VARCHAR(30),
		codigoEPS VARCHAR(10),
		nombreEPS VARCHAR(100),
		codigoGrupo VARCHAR(15),
		nombreGrupo VARCHAR(100),
		jusclinica TEXT,
		origen CHAR(3),
		createdat VARCHAR(19),
		ShardName varchar(500)
	)

	--ODO
	--INSERT INTO #ordenes
	
	EXEC sp_execute_remote @data_source_name  = N'INDIGO045', 
		@stmt = N'[dbo].[ODO_Ordenamientos_Medicos] @inidate, @enddate', 
		@params = N'@inidate date, @enddate date',
		@inidate = @inidate, @enddate = @enddate; 


	--CCB
	--INSERT INTO #ordenes
	EXEC sp_execute_remote @data_source_name  = N'INDIGO047', 
		@stmt = N'[dbo].[ODO_Ordenamientos_Medicos] @inidate, @enddate', 
		@params = N'@inidate date, @enddate date',
		@inidate = @inidate, @enddate = @enddate;
		


	
	SELECT 	
	    centroAtencion 
		,unidadFuncional 
		,documentoMedico 
		,nombreMedico 
		,especialidad 
		,fechaOrden
		,ingreso 
		,fechaIngreso 
		,cama 
		,tipoEstancia 
		,folio 
		,trim(ID) as ID
		,codigoServicio 
		,nombreServicio 
		,codigoDescripcion 
		,nombreDescripcion 
		,cantidad 
		,estadoOrden 
		,tipoOrden 
		,esquema 
		,ciclo 
		,tipoCodigo
		,codigoDx
		,nombreDx 
		,tipoDocumento 
		,numeroDocumento 
		,primerNombre 
		,segundoNombre 
		,primerApellido 
		,segundoApellido 
		,genero 
		,edad 
		,municipio
		,departamento 
		,direccion 
		,telPrincipal 
		,telSecundario 
		,regimen 
		,codigoEPS 
		,nombreEPS 
		,codigoGrupo 
		,nombreGrupo 
		,jusclinica 
		,origen 
		,createdat 
		FROM #ordenes


END
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[ODO_Ordenamientos_Medicos_Tramita] TO [usr_tramita]
    WITH GRANT OPTION
    AS [dbo];

