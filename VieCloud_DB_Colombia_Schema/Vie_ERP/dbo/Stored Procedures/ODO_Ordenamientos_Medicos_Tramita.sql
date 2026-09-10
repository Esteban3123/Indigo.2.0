

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
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento orquestador que consolida órdenes médicas de odontología provenientes de dos fuentes de datos remotas (INDIGO045 e INDIGO047) ejecutando en cada una el procedimiento `ODO_Ordenamientos_Medicos` para un rango de fechas dado. Los resultados se acumulan en una tabla temporal y se retorna un conjunto unificado con información clínica, demográfica y administrativa del paciente, incluyendo datos de la orden, diagnóstico, aseguradora y estancia hospitalaria.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos_Tramita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos_Tramita';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida y entrega ordenamientos médicos generados en un rango de fechas, ejecutándose remotamente sobre dos fuentes de datos (sedes/instancias) y devolviendo un resultado unificado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos_Tramita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir y estar accesibles los external data sources ''INDIGO045'' e ''INDIGO047''.; En cada data source remoto debe existir el procedimiento [dbo].[ODO_Ordenamientos_Medicos] con firma (@inidate date, @enddate date).; El rango de fechas (inicio y fin) debe ser provisto para delimitar la consulta.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos_Tramita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La consulta siempre se ejecuta contra dos orígenes remotos: INDIGO045 e INDIGO047.; El campo ID se entrega con TRIM aplicado en el resultado final.; El procedimiento expone permisos EXECUTE con GRANT OPTION al rol/usuario usr_tramita.; El esquema del resultado es fijo (columnas predefinidas en la tabla temporal #ordenes).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos_Tramita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ordenamiento médico; Centro de atención; Unidad funcional; Médico tratante y especialidad; Ingreso hospitalario y cama; Tipo de estancia; Servicio y descripción clínica; Estado y tipo de orden; Esquema y ciclo (oncológico/tratamiento); Diagnóstico (CIE); Paciente (datos demográficos y contacto); Régimen y EPS; Justificación clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos_Tramita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] #ordenes: Se define la tabla temporal con el esquema de ordenamientos médicos pero el resultado final se entrega vía SELECT sobre #ordenes (los INSERT desde sp_execute_remote están comentados, por lo que el SELECT final retorna vacío salvo que los EXEC remotos devuelvan resultset propio).; [RETURN_RESULT] REMOTE:INDIGO045.dbo.ODO_Ordenamientos_Medicos: Se ejecuta remotamente el SP de ordenamientos médicos en la fuente INDIGO045 con el rango de fechas recibido.; [RETURN_RESULT] REMOTE:INDIGO047.dbo.ODO_Ordenamientos_Medicos: Se ejecuta remotamente el SP de ordenamientos médicos en la fuente INDIGO047 con el rango de fechas recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos_Tramita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'INDIGO045.dbo.ODO_Ordenamientos_Medicos; INDIGO047.dbo.ODO_Ordenamientos_Medicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos_Tramita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'tempdb..#ordenes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos_Tramita';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ODO_Ordenamientos_Medicos_Tramita';
-- GO
