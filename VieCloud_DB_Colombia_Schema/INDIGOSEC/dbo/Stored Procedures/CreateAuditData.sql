
-- =======================================================
-- Author:		Juan Diego Diaz Mosquera
-- Create date: 31/10/2013
-- Description:	Sp para crear dinamicamente la data de auditoria

-- Modified:    Cristhian Mauricio Salazar
-- Description:	se modifica para que inserte los agregados y para que inserte mas de un objeto
-- =======================================================
CREATE PROCEDURE [dbo].[CreateAuditData]
	@Xml as xml
AS
BEGIN
SET NOCOUNT ON;

	BEGIN TRY
		BEGIN TRAN   
	
	DECLARE @month as NVARCHAR(2)
	DECLARE @year as INT
	DECLARE @sql as NVARCHAR(MAX)
	DECLARE @sqlCreate NVARCHAR(MAX)
	DECLARE @nameTableHeader NVARCHAR(100)
	DECLARE @nameTableDetail NVARCHAR(100)
	Declare @tableResult table(
	result  int
	)

	declare @TablaCabecera table(
	   Auto int,
	   Accion int,
	   TablaEntidad varchar(50),
	   KeyTablaEntidad int,
	   IdAudit int,
	   Fecha datetime,
	   Usuario varchar(20),
	   Frontal int,
	   UsuarioWin varchar(50),
	   Maquina varchar(50),
	   Aplicacion varchar(50),
	   Company varchar(5),
	   IsParent bit,
	   IdReal int
	)

	declare @TablaDetalle table(
	   AUTO int,
	   AUTOAUDITORIA int,
	   TABLAASOCIADA varchar(50),
	   IDAUDITASOCIADA int,
	   PROPIEDAD varchar(30),
	   VALOR_ANTERIOR varchar(MAX),
	   NUEVO_VALOR varchar(MAX)
	)

	insert into @TablaCabecera (Auto, Accion, TablaEntidad, KeyTablaEntidad, IdAudit, Fecha, Usuario, Frontal, UsuarioWin, Maquina, Aplicacion, Company, IsParent)
	select T.N.value('AUTO[1]',			'int')     as AUTO,
	   T.N.value('ACCION[1]',			'int')     as ACCION,
	   T.N.value('TABLAENTIDAD[1]',			'varchar(50)')     as TABLAENTIDAD,
	   T.N.value('KEYTABLAENTIDAD[1]',			'int')     as KEYTABLAENTIDAD,
	   T.N.value('IDAUDIT[1]',			'int')     as IDAUDIT,
	   T.N.value('FECHA[1]',			'datetime')     as FECHA,
	   T.N.value('USUARIO[1]',			'varchar(20)')     as USUARIO,
	   T.N.value('FRONTAL[1]',			'int')     as FRONTAL,
	   T.N.value('USUARIOWIN[1]',			'varchar(50)')     as USUARIOWIN,
	   T.N.value('MAQUINA[1]',			'varchar(50)')     as MAQUINA,
	   T.N.value('APLICACION[1]', 'varchar(50)') as APLICACION,
	   T.N.value('COMPANY[1]',			'varchar(5)')     as COMPANY,
	   T.N.value('ISPARENT[1]',			'bit')     as ISPARENT
				 from @XML.nodes('/Auditoria/Cabecera') as T(N)
	

	insert into @TablaDetalle
	select T.N.value('AUTO[1]',			'int')     as AUTO,
	   T.N.value('AUTOAUDITORIA[1]',			'int')     as AUTOAUDITORIA,
	   T.N.value('TABLAASOCIADA[1]', 'varchar(50)') as TABLAASOCIADA,
       T.N.value('IDAUDITASOCIADA[1]', 'int') as IDAUDITASOCIADA,
	   T.N.value('PROPIEDAD[1]',			'varchar(30)')     as PROPIEDAD,
	   T.N.value('VALOR_ANTERIOR[1]',			'varchar(MAX)')     as VALOR_ANTERIOR,
	   T.N.value('NUEVO_VALOR[1]',			'varchar(MAX)')     as NUEVO_VALOR
	from @XML.nodes('/Auditoria/Detalle') as T(N)


	select * from @TablaCabecera
	select * from @TablaDetalle

	declare @index int = 0
	declare @AutoMaxCabecera int = (select MAX(Auto) from @TablaCabecera)
	--Agrego la cabecera de la auditoria
	while @index <= @AutoMaxCabecera
	BEGIN
		IF (SELECT Count(*) FROM @TablaCabecera WHERE Auto = @index) > 0 begin
			declare @AUTO int
			declare @ACCION int
			declare @TABLAENTIDAD varchar(50)
			declare @KEYTABLAENTIDAD int
			declare @IdAudit as int
			declare @FECHA datetime
			declare @USUARIO varchar(20)
			declare @FRONTAL int
			declare @USUARIOWIN varchar(50)
			declare @MAQUINA varchar(50)
			declare @Aplicacion varchar(50)
			declare @COMPANY varchar(5)
			declare @ISPARENT varchar(5)

			SELECT @AUTO = Auto, @ACCION = Accion, @TABLAENTIDAD = TablaEntidad, @KEYTABLAENTIDAD = KeyTablaEntidad, @IdAudit = IdAudit
			, @FECHA = Fecha, @USUARIO = Usuario, @FRONTAL = Frontal, @USUARIOWIN = UsuarioWin, @MAQUINA = Maquina
			, @Aplicacion = Aplicacion, @company = Company, @ISPARENT = IsParent
			FROM @TablaCabecera WHERE Auto = @index

			declare @IdAuditReal int = null
			set @IdAuditReal = @IdAudit

			if @IdAudit is not null begin
				select @IdAuditReal = IdReal from @TablaCabecera where Auto = @IdAudit
			end 


			INSERT INTO Audit.Audit
				   ([Action],
					[Entity],
					[EntityKey],
					[IdAudit],
					[Date],
					[Users],
					[Form],
					[UserWindows],
					[Workstation],
					[Application],
					[Company],
					[IsParent])
				values (@ACCION, @TABLAENTIDAD, @KEYTABLAENTIDAD, @IdAuditReal, @FECHA, @USUARIO, @FRONTAL, @USUARIOWIN, @MAQUINA, @Aplicacion, @company, @ISPARENT)


			DECLARE @LastId as int = (SELECT SCOPE_IDENTITY())
			update @TablaCabecera set IdReal = @LastId where Auto = @AUTO
			set @index = @index + 1
		end
	END
	
	--Ahora inserto el detalle, primero inserto toda la cabecera por que necesito terner todos los id reales
	set @index = 0
	while @index <= @AutoMaxCabecera
	BEGIN
		IF (SELECT Count(*) FROM @TablaCabecera WHERE Auto = @index) > 0 begin
			declare  @AUTODetail int,@AUTOAUDITORIADetail int,@TABLAASOCIADADetail varchar(50),@IDAUDITASOCIADADetail int
			, @PROPIEDADDetail varchar(30), @VALOR_ANTERIORDetail varchar(MAX),@NUEVO_VALORDetail varchar(MAX), @IdTablaCabecera int

			SELECT @IdTablaCabecera = IdReal FROM @TablaCabecera WHERE Auto = @index

			DECLARE detail_cursor CURSOR FOR 
			Select AUTO, AUTOAUDITORIA, TABLAASOCIADA, IDAUDITASOCIADA, PROPIEDAD, VALOR_ANTERIOR, NUEVO_VALOR FROM @TablaDetalle where AUTOAUDITORIA = @index

			OPEN detail_cursor

			FETCH NEXT FROM detail_cursor 
			INTO @AUTODetail,@AUTOAUDITORIADetail,@TABLAASOCIADADetail,@IDAUDITASOCIADADetail,@PROPIEDADDetail,@VALOR_ANTERIORDetail,@NUEVO_VALORDetail

			WHILE @@FETCH_STATUS = 0
			BEGIN
				
				declare @IdAuditAsociadaReal int
				set @IdAuditAsociadaReal = @IDAUDITASOCIADADetail

				if @IdAuditAsociadaReal is not null begin
					set @IdAuditAsociadaReal = (select IdReal from @TablaCabecera where Auto = @IdAuditAsociadaReal)
				end

				INSERT INTO [Audit].[AuditDetail]
						([IdAudit],
						[AssociatedTable],
						[IdAuditAssociated],
						[Property],
						[PreviousValue],
						[NewValue])
				VALUES(@IdTablaCabecera, @TABLAASOCIADADetail, @IdAuditAsociadaReal, @PROPIEDADDetail, @VALOR_ANTERIORDetail, @NUEVO_VALORDetail)

				FETCH NEXT FROM detail_cursor 
				INTO @AUTODetail,@AUTOAUDITORIADetail,@TABLAASOCIADADetail,@IDAUDITASOCIADADetail,@PROPIEDADDetail,@VALOR_ANTERIORDetail,@NUEVO_VALORDetail
			END 
			CLOSE detail_cursor;
			DEALLOCATE detail_cursor;
		end
		set @index = @index + 1
	END
	
	Commit TRAN
	select 'Se creo correctamente la auditoria' as Message
	END TRY
	BEGIN CATCH
	  ROLLBACK TRAN
		  SELECT  'Se ha producido un error!',  ERROR_MESSAGE()	as mensajeError, ERROR_NUMBER() NumeroError
	END CATCH

END

