CREATE PROCEDURE [dbo].[SP_HC_GuardarMedicamentosAdicionales]
	@CodigoUsuario varchar(20),
	@Xml xml
AS
BEGIN

	SET NOCOUNT ON;

	declare @CodigoProducto as varchar(255)
	Declare @calculoAutomaticoForma as bit = 0
	Declare @UnidadMedida as varchar(25) 
	declare @FechaProceso as datetime = [Common].[GETDATE]()
	

	declare @TablaListaProductos table (
				RowId Int Identity(1,1) Primary Key,
				DESPRODUC varchar(255), --' Nombre Producto
				ABRPROMEZ varchar(200), --' Abreviatura Nombre Producto
				CONCENMED varchar(50),  --' Concentracion
				CODFORMED varchar(20),  --' Codigo Foma producto 
				PRESENMED varchar(160), --' Nombre  forma producto 
				CODVIAADM varchar(20)   --' Via administracion
				)
	BEGIN TRY 

	IF Exists (SELECT t.x.value('DESPRODUC[1]','varchar(250)') FROM @Xml.nodes('/ListaDatosMedicamento/DatosMedicamento') t(x) where t.x.value('DESPRODUC[1]','varchar(250)') Is Null) Begin
		select '999' as CodeMessage, 'No se encontró lista de productos' as Message
		return
	END
		
	insert @TablaListaProductos
	select 
		t.x.value('DESPRODUC[1]','varchar(255)'),
		t.x.value('ABRPROMEZ[1]','varchar(200)'),
		t.x.value('CONCENMED[1]','varchar(50)'),
		t.x.value('CODFORMED[1]','varchar(20)'),
		t.x.value('PRESENMED[1]','varchar(160)'),
		t.x.value('CODVIAADM[1]','varchar(20)')	
	from @xml.nodes('/ListaDatosMedicamento/DatosMedicamento') t(x)
	

	--Sacamos si calcula automatico por medico de la forma farmaceutica
	set @calculoAutomaticoForma  = (select top 1 AutomaticCalculation  from IHFORMEDI z inner join @TablaListaProductos p  on p.CODFORMED = z.CODFORMED )

	--Saco la unidad de medida por medio de la forma farmaceutica del medicamento
	set @UnidadMedida =  (select top 1 Z.CODUNIMED  from IHFORMEDI_MeasurementUnit z inner join @TablaListaProductos p  on p.CODFORMED = z.CODFORMED )
	IF @UnidadMedida = '' or @UnidadMedida IS Null BEGIN
		SELECT '999' as CodeMessage,  'No se puede guardar el medicamento, deben configurar las unidades de medida a la forma del medicamento' as Message
		return
	END

	---select  @UnidadMedida 

	set @CodigoProducto =  concat('M',FORMAT(CURRENT_TIMESTAMP, 'yyyyMMddHHmmss'))

		INSERT INTO dbo.IHLISTPRO
							(
							 CODPRODUC,CODDCIMED,DESPRODUC
							,NOPOSPROD,TIPPRODUC,MANCONPRO
							,REGINVACT,REGINVIMA,CODGRUFAR
							,CODVIAADM,CONCENMED,PRESENMED
							,CODFORMED,TIEESTMED,TIPFORMED
							,PESTOTMED,CODUNIPES,VOLTOTMED
							,CODUNIVOL,CODUNIADM,CALCANAUT
							,ESPDILPRO,ABRPROMEZ,PROESTADO
							,PROCONTRO,TODASPATO,MANLOCALI
							,RETRASOGE,CODUSUCRE,FECUSUCRE
							,CODUSUMOD,FECUSUMOD,JUSINMEDI
							,CODNIVRIE,CODJUMEES,ADVERTENC
							,POSOLOGIA,MEDTRAZA,MATOSTOSIN
							,MEDICAMENTONPT,OSMOLARIDAD,DENSIDAD
							,UNIDADMANEJO,CONSUMPTION,OPTOMETRYDEVICE
							,AddedAutomatic
							)
						select 
							@CodigoProducto
							,NULL
							,A.DESPRODUC --CONCAT(A.DESPRODUC,' ',A.CONCENMED,' ',A.PRESENMED)
							,0
							,1 --Medicamento
							,0
							,1
							,null
							,null
							,A.CODVIAADM
							,A.CONCENMED
							,A.PRESENMED
							,A.CODFORMED
							,0
							,4 --unidad de asministracion
							,null
							,null
							,null
							,null
							,@UnidadMedida
							,@calculoAutomaticoForma
							,0
							,A.ABRPROMEZ
							,1
							,0
							,0
							,0
							,0
							,@CodigoUsuario
							,@FechaProceso
							,@CodigoUsuario
							,@FechaProceso
							,0
							,NULL
							,0
							,NULL
							,NULL
							,0
							,0
							,0
							,NULL
							,NULL
							,NULL
							,NULL
							,0
							,1
							FROM 
		   					@TablaListaProductos A

		select '0' as CodeMessage, @CodigoProducto as CodigoProducto,  'Se guardo correctamente'  Message 
end try
begin catch
		select '999' as CodeMessage, ERROR_MESSAGE() + ', Linea: ' + cast(ERROR_LINE() as varchar(20)) Message
	end catch
END