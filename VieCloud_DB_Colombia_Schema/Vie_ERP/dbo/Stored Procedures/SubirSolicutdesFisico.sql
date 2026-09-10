-- =============================================
-- Author:		Rafael PAtiño
-- Create date: 16/07/2019
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[SubirSolicutdesFisico]
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	
	DECLARE @Tabla AS TABLE(id INT IDENTITY,IPCODPACI varchar(100),NUMINGRES VARCHAR(50),CODCENATE VARCHAR(50), UFUCODIGO VARCHAR(50), CODPRODUC varchar(100),TIPPRODUC int,CANENTPRO int,CANPEDPRO int)

	INSERT INTO @Tabla
	select D.IPCODPACI,D.NUMINGRES,D.CODCENATE,D.UFUCODIGO, D.CODPRODUC, P.TIPPRODUC,D.CANPEDPRO  as cantidadPedida,D.CANENTPRO as cantidadEntregada    from HCFARMEPD D inner join HCFARMEPC C on C.CODCONCEC = D.CODCONCEC 
inner join IHLISTPRO P on P.CODPRODUC = D.CODPRODUC 
where C.ORDESTADO  =3 and D.PROESTADO = 2 and D.IDAGEPROGQX is null and format(C.FECHAORDE,'dd/MM/yyyy HH:mm') >= '16/07/2019 09:50' and format(C.FECHAORDE,'dd/MM/yyyy HH:mm') <= '16/07/2019 13:15'

	DECLARE @count AS INTEGER =  (SELECT COUNT(*) FROM @Tabla)
	DECLARE @contador AS INT = 1

	PRINT 'CANTIDA REG PRO: ' + CONVERT(VARCHAR(20),@count)

	WHILE @contador  <= @count begin
			
		declare @PatientCode varchar(25)
		declare @AdmissionNumberHijo varchar(100)
		declare @CareCenterCode varchar(100)
		declare @FunctionalUnitCode varchar(100)
		declare @ProductCodeCrystal varchar(100)
		declare @QuantityCrystal  int
		declare @ProductTypeCrystal int
		declare @CantidadSolicitadaCrystal int

		select  @PatientCode = IPCODPACI, @AdmissionNumberHijo = NUMINGRES , @CareCenterCode = CODCENATE,@FunctionalUnitCode = UFUCODIGO, @ProductCodeCrystal = CODPRODUC 
				, @QuantityCrystal = CANENTPRO, @ProductTypeCrystal = TIPPRODUC, @CantidadSolicitadaCrystal = CANPEDPRO   
		    from @Tabla WHERE ID = @contador

		if Exists (select IPCODPACI from dbo.HCFISIPRO 
										where IPCODPACI = @PatientCode and NUMINGRES = @AdmissionNumberHijo and CODCENATE = @CareCenterCode 
											And UFUCODIGO = @FunctionalUnitCode and CODPRODUC = @ProductCodeCrystal) BEGIN

											
										Update dbo.HCFISIPRO set CANACTPRO += @QuantityCrystal 
										Where IPCODPACI = @PatientCode and NUMINGRES = @AdmissionNumberHijo and CODCENATE = @CareCenterCode 
											And UFUCODIGO = @FunctionalUnitCode and CODPRODUC = @ProductCodeCrystal  -- select 11

				end else begin

								INSERT INTO [dbo].[HCFISIPRO]
										   ([IPCODPACI]
										   ,[NUMINGRES]
										   ,[CODCENATE]
										   ,[UFUCODIGO]
										   ,[CODPRODUC]
										   ,[TIPPRODUC]
										   ,[CANACTPRO]
										   ,[CANPEDPRO]
										   ,[CANPENPRO]
										   ,[TOTPROUNI]
										   ,[DOSPROACU]
										   ,[TOTHORACU]
										   ,[CODUNIMED]
										   ,[INDAUDFOR])
									 VALUES
										   (@PatientCode
										   ,@AdmissionNumberHijo
										   ,@CareCenterCode
										   ,@FunctionalUnitCode
										   ,@ProductCodeCrystal
										   ,@ProductTypeCrystal
										   ,@QuantityCrystal
										   ,@CantidadSolicitadaCrystal
										   ,@CantidadSolicitadaCrystal - @QuantityCrystal
										   ,NULL
										   ,NULL
										   ,NULL
										   ,NULL
										   ,0) 
										   --select 22

					end

	

			   PRINT 'CONTEO: ' + CONVERT(VARCHAR(20),@contador)
		SET @contador +=1 
	END

    
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sincroniza las solicitudes de despacho físico de medicamentos aprobadas en farmacia hacia el inventario de fisioterapia/físico (HCFISIPRO). Lee las órdenes de farmacia en estado aprobado (HCFARMEPC y HCFARMEPD) que no están asociadas a programación quirúrgica y que fueron generadas en un rango de fechas específico, tomando la cantidad pedida y entregada de cada producto por paciente, ingreso, centro de atención y unidad funcional. Si el producto ya existe en el registro físico del paciente actualiza la cantidad acumulada; si no existe, crea un nuevo registro con las cantidades pedidas, entregadas y pendientes. Este procedimiento fue diseñado como una migración puntual de datos históricos de órdenes farmacéuticas hacia el módulo de control físico de productos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SubirSolicutdesFisico';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SubirSolicutdesFisico';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Migra órdenes farmacéuticas entregadas (en estado finalizado) hacia el módulo de control físico de productos del paciente, acumulando cantidades cuando ya existe registro o creándolo en caso contrario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SubirSolicutdesFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir órdenes en HCFARMEPC con ORDESTADO=3 y detalles en HCFARMEPD con PROESTADO=2 e IDAGEPROGQX nulo; Las fechas de orden deben caer dentro del rango fijo 16/07/2019 09:50–13:15; Los productos deben existir en IHLISTPRO para obtener su tipo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SubirSolicutdesFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesan órdenes con ORDESTADO=3 y detalle con PROESTADO=2; Se excluyen detalles asociados a programación quirúrgica (IDAGEPROGQX no nulo); El rango de fecha de la orden está fijo (16/07/2019 09:50 a 13:15), por lo que es una migración puntual; La cantidad pendiente al insertar siempre es CANPEDPRO - CANENTPRO; Nuevos registros se crean con INDAUDFOR=0 y campos de dosificación/totales en NULL; La actualización solo incrementa CANACTPRO, no modifica cantidades pedidas ni pendientes', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SubirSolicutdesFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes farmacéuticas; Entrega de productos/medicamentos; Paciente; Ingreso hospitalario; Centro de atención; Unidad funcional; Control físico de productos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SubirSolicutdesFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] dbo.HCFISIPRO: Cuando ya existe un registro con misma llave (paciente, ingreso, centro de atención, unidad funcional y producto), incrementa CANACTPRO sumándole la cantidad entregada del detalle; [INSERT] dbo.HCFISIPRO: Cuando no existe registro previo para la combinación de llave, crea uno nuevo con la cantidad entregada en CANACTPRO, la cantidad pedida en CANPEDPRO, la diferencia (pedida-entregada) en CANPENPRO, INDAUDFOR=0 y campos de dosificación/unidad de medida en NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SubirSolicutdesFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe registro en HCFISIPRO con misma combinación de paciente, ingreso, centro de atención, unidad funcional y producto → Acumula la cantidad entregada sumándola a CANACTPRO del registro existente else Inserta un nuevo registro con cantidad pedida, cantidad entregada y la pendiente calculada como (pedida - entregada), marcando INDAUDFOR=0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SubirSolicutdesFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFARMEPD; dbo.HCFARMEPC; dbo.IHLISTPRO; dbo.HCFISIPRO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SubirSolicutdesFisico';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SubirSolicutdesFisico';
-- GO
