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
							,'MED-ADICIONAL'
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra medicamentos adicionales en el catálogo maestro de productos farmacéuticos (IHLISTPRO) a partir de una lista enviada en formato XML. Valida que la forma farmacéutica del medicamento tenga configurada su unidad de medida consultando IHFORMEDI y IHFORMEDI_MeasurementUnit; si no la tiene, rechaza el guardado con un mensaje de error. Genera automáticamente un código único de producto con prefijo ''M'' basado en la fecha y hora actual, y hereda de la forma farmacéutica si el cálculo de dosis es automático. Se usa cuando un médico u operador necesita incorporar al sistema un medicamento que aún no existe en el catálogo, asegurando que quede correctamente configurado con vía de administración, concentración, presentación y unidad de medida.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_GuardarMedicamentosAdicionales';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_HC_GuardarMedicamentosAdicionales';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra en el catálogo de productos un nuevo medicamento adicional aportado vía XML, generando un código único con prefijo ''M'' y heredando configuración (unidad de medida y cálculo automático) desde la forma farmacéutica.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GuardarMedicamentosAdicionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /ListaDatosMedicamento/DatosMedicamento con DESPRODUC no nulo; La forma farmacéutica indicada (CODFORMED) debe existir en IHFORMEDI; La forma farmacéutica debe tener configurada al menos una unidad de medida en IHFORMEDI_MeasurementUnit; Se requiere un código de usuario válido para auditoría', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GuardarMedicamentosAdicionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El código de producto generado siempre tiene prefijo ''M'' seguido de timestamp con formato yyyyMMddHHmmss; Todo medicamento adicional se registra con CODDCIMED=''MED-ADICIONAL'', TIPPRODUC=1 (Medicamento), TIEESTMED=4, PROESTADO=1 y AddedAutomatic=1; La unidad de administración (CODUNIADM) y el flag de cálculo automático (CALCANAUT) se heredan de la configuración de la forma farmacéutica; Nunca se inserta un medicamento si la forma farmacéutica no tiene unidad de medida asociada; Los campos de auditoría de creación y modificación se setean con el mismo usuario y fecha de proceso al momento del registro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GuardarMedicamentosAdicionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento adicional; Forma farmacéutica; Vía de administración; Concentración; Presentación; Unidad de medida; Cálculo automático de dosis; Catálogo de productos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GuardarMedicamentosAdicionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.IHLISTPRO: Cuando el XML trae productos válidos y la forma farmacéutica tiene unidad de medida configurada, se inserta un nuevo medicamento con CODPRODUC=''M''+timestamp, CODDCIMED=''MED-ADICIONAL'', TIPPRODUC=1, CODUNIADM heredado de IHFORMEDI_MeasurementUnit y CALCANAUT heredado de IHFORMEDI.AutomaticCalculation; [RETURN_RESULT] RESULTSET: Devuelve un resultset con CodeMessage (''0'' éxito o ''999'' error), el CodigoProducto generado en caso de éxito, y un Message descriptivo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GuardarMedicamentosAdicionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El XML contiene algún DatosMedicamento con DESPRODUC nulo → Retorna CodeMessage=''999'' con mensaje ''No se encontró lista de productos'' y termina sin insertar; si La forma farmacéutica no tiene unidad de medida configurada (UnidadMedida vacío o NULL) → Retorna CodeMessage=''999'' indicando que deben configurar las unidades de medida a la forma del medicamento y termina sin insertar; si Inserción exitosa → Retorna CodeMessage=''0'' junto con el código de producto generado y mensaje de confirmación; si Excepción capturada en TRY/CATCH → Retorna CodeMessage=''999'' con ERROR_MESSAGE() y línea del error', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GuardarMedicamentosAdicionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHFORMEDI; dbo.IHFORMEDI_MeasurementUnit', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GuardarMedicamentosAdicionales';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_HC_GuardarMedicamentosAdicionales';
-- GO
