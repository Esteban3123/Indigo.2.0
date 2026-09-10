CREATE PROCEDURE  [dbo].[SPHC_ConfirmarHojaGastoQX]
(
       -- Add the parameters for the stored procedure here
	@OrigenDevolutivo as Int,
	@CamaOrigen as varchar,
	@NombreUnidadDestino as char(60),
	@NombreUsuarioIndigo as char(60),
	@CodigoBodega as varchar(20),
	@CentroCosto as char(14),
	@UsuarioIndigo as char(20),
	@CentroAtencion as char(10),
	@UnidadFuncional as char(10),
	@Ingreso as char(10),
	@Paciente as varchar(25),
	@Profesional as char(60),
	@Productos as xml,
	@UsuarioHoja as char(20),
	@IdHojaToConfirm as int

)
AS
BEGIN
	BEGIN TRY
			DECLARE @IdProgramacionCirugia as int
			DECLARE @ProductosTemp as table([ID] INT,[IDHCHOJAGASTOQX] INT,[CODPRODUC] CHAR(20),[IDPRODUCTO] INT, [CANTIDADENTREGADA] INT,[CANTIDADGASTADA] INT,[CANTIDADDEVOLVER] INT,
											[CANTIDADACEPTADADEV] INT, [StatusOrder] INT, [RequestType] INT, [IDAGEPROGQX] INT,[CONSEKARDEX] VARCHAR(50))
			INSERT INTO @ProductosTemp (ID, IDHCHOJAGASTOQX,CODPRODUC,IDPRODUCTO, CANTIDADENTREGADA, CANTIDADGASTADA, CANTIDADDEVOLVER, CANTIDADACEPTADADEV, StatusOrder, RequestType, IDAGEPROGQX, CONSEKARDEX)
						SELECT
							Producto.value('(ID)[1]', 'INT') AS ID,
							Producto.value('(IDHCHOJAGASTOQX)[1]', 'INT') AS IDHCHOJAGASTOQX,
							Producto.value('(CodigoProducto)[1]', 'CHAR(20)') AS CODPRODUC,
							Producto.value('(CodigoP)[1]', 'INT') AS IDPRODUCTO,
							Producto.value('(CANTIDADENTREGADA)[1]', 'DECIMAL(18,2)') AS CANTIDADENTREGADA,
							Producto.value('(CANTIDADGASTADA)[1]', 'DECIMAL(18,2)') AS CANTIDADGASTADA,
							Producto.value('(CANTIDADDEVOLVER)[1]', 'DECIMAL(18,2)') AS CANTIDADDEVOLVER,
							Producto.value('(CANTIDADACEPTADADEV)[1]', 'DECIMAL(18,2)') AS CANTIDADACEPTADADEV,
							Producto.value('(StatusOrder)[1]', 'VARCHAR(50)') AS StatusOrder,
							Producto.value('(RequestType)[1]', 'INT') AS RequestType,
							Producto.value('(IDAGEPROGQX)[1]', 'INT') AS RequestType,
							NEWID() 
						FROM
						    @Productos.nodes('/Root/Products') AS T(Producto);
			SET @IdProgramacionCirugia = (select TOP 1 @Productos.value('(IDAGEPROGQX)[1]', 'INT'))
			--Guardar el histórico del guardado y confirmaciones de la hoja de gastos QX
			INSERT INTO [dbo].[HistoricalQXExpenseSheet] 
            (IDHCHOGASTOQX, CODPROSAL, DATE, CODCENATE, UFUCODIGO, TYPE) 
            VALUES (@IdHojaToConfirm, @Profesional, COMMON.GETDATE(), @CentroAtencion, @UnidadFuncional, 2);

			--Insertar o actualizar el detalle de la hoja de gasto quirúrgico
			WITH ProductData AS (
			    SELECT 
			        Producto.value('(ID)[1]', 'INT') AS ID,
					Producto.value('(IDHCHOJAGASTOQX)[1]', 'INT') AS IDHCHOJAGASTOQX,
			        Producto.value('(CODPRODUC)[1]', 'VARCHAR(50)') AS CODPRODUC,
					Producto.value('(IDPRODUCTO)[1]', 'VARCHAR(50)') AS IDPRODUCTO,
			        Producto.value('(CANTIDADENTREGADA)[1]', 'DECIMAL(18,2)') AS CANTIDADENTREGADA,
			        Producto.value('(CANTIDADGASTADA)[1]', 'DECIMAL(18,2)') AS CANTIDADGASTADA,
			        Producto.value('(CANTIDADDEVOLVER)[1]', 'DECIMAL(18,2)') AS CANTIDADDEVOLVER,
			        Producto.value('(CANTIDADACEPTADADEV)[1]', 'DECIMAL(18,2)') AS CANTIDADACEPTADADEV,
			        Producto.value('(StatusOrder)[1]', 'VARCHAR(50)') AS StatusOrder,
					Producto.value('(RequestType)[1]', 'INT') AS RequestType
			    FROM @Productos.nodes('/Root/Product') AS X(Producto)
			)
			MERGE INTO [HCHOJAGASTOQXD] AS hqx
			USING ProductData AS dp
			    ON dp.CODPRODUC = hqx.CODPRODUC
			    AND dp.RequestType = hqx.RequestType
			    AND hqx.ID = dp.ID
			WHEN MATCHED THEN
			    UPDATE SET 
			        hqx.CANTIDADGASTADA = dp.CANTIDADGASTADA,
			        hqx.CANTIDADDEVOLVER = dp.CANTIDADDEVOLVER,
			        hqx.CANTIDADACEPTADADEV = dp.CANTIDADACEPTADADEV,
			        hqx.StatusOrder = dp.StatusOrder
			WHEN NOT MATCHED BY TARGET THEN
			    INSERT (IDHCHOJAGASTOQX, CODPRODUC, IDPRODUCTO, CANTIDADENTREGADA, 
			            CANTIDADGASTADA, CANTIDADDEVOLVER, CANTIDADACEPTADADEV, StatusOrder, RequestType)
			    VALUES (dp.IDHCHOJAGASTOQX, dp.CODPRODUC, NULL, dp.CANTIDADENTREGADA,
			            dp.CANTIDADGASTADA, dp.CANTIDADDEVOLVER, dp.CANTIDADACEPTADADEV, dp.StatusOrder, dp.RequestType);
			
			--Se actualiza la columna que indica si se diligención hoja de gasto qx de la programación de la cirugía.
			UPDATE dbo.AGEPROGQX 
			SET DILIGENCIOHOJAGASTO = 1 
			WHERE CODAUTONU = @IdProgramacionCirugia

			--Insertamos los registros en el kardex del paciente
			INSERT INTO HCKARDPAC (NUMCONSEC, IPCODPACI, NUMINGRES, CODCENATE, UFUCODIGO, CODPROSAL, CODPRODUC, CANPRODUCT, TIPREGIST, FECREGKAR, TIPORIREG, DESMOVPRO)
			SELECT CONSEKARDEX, @Paciente, @Ingreso, @CentroAtencion, @UnidadFuncional, @Profesional, CODPRODUC, CANTIDADGASTADA, 5, COMMON.GETDATE(), 13, 
			IIF(RequestType = 1, CONCAT('Gasto del medicamento desde la Hoja de gasto quirúrgica - Paquete quirúrgico, origen enfermería - usuario : ',@UsuarioIndigo),
			IIF(RequestType = 2, CONCAT('Gasto del medicamento desde la Hoja de gasto quirúrgica - Producto adicional, origen enfermería - usuario : ',@UsuarioIndigo),
								 CONCAT('Gasto del medicamento desde la Hoja de gasto quirúrgica - Paquete enfermeria, origen enfermería - usuario : ',@UsuarioIndigo))) 
			FROM @ProductosTemp

			--Actualizamos el consecutivo de la tabla detalle de la hoja de gasto QX.
			UPDATE B 
			SET CONSEKARDEX = A.CONSEKARDEX 
			FROM @ProductosTemp A 
			INNER JOIN dbo.HCHOJAGASTOQXD B ON B.IDHCHOJAGASTOQX = A.IDHCHOJAGASTOQX AND B.CODPRODUC = A.CODPRODUC AND B.RequestType = A.RequestType

			UPDATE dbo.HCHOJAGASTOQX SET ESTADO=2, UserTryingConfirm =@UsuarioIndigo WHERE ID=@IdHojaToConfirm

			SELECT '0' AS CodeMessage, 'Se confirmó correctamente la hoja de gasto qx' Message 
	END TRY
	BEGIN CATCH
			SELECT '999' AS CodeMessage, ERROR_MESSAGE() + ', Linea: ' + cast(ERROR_LINE() as varchar(20)) Message
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma la hoja de gasto quirúrgico de una cirugía programada: registra el historial de confirmación en HistoricalQXExpenseSheet, inserta o actualiza el detalle de medicamentos y materiales consumidos en la hoja (HCHOJAGASTOQXD) con cantidades gastadas, devueltas y aceptadas para devolución, y genera los movimientos de kardex del paciente (HCKARDPAC) para cada producto utilizado durante la cirugía. Además, marca la programación quirúrgica (AGEPROGQX) como diligenciada y cambia el estado de la hoja de gasto (HCHOJAGASTOQX) a confirmado, garantizando así el cierre del consumo de insumos y medicamentos del acto quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ConfirmarHojaGastoQX';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ConfirmarHojaGastoQX';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma la hoja de gasto quirúrgico aplicando los consumos al kardex del paciente, actualizando el detalle, registrando histórico y marcando la programación de cirugía como diligenciada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ConfirmarHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de productos debe tener estructura /Root/Products y /Root/Product con los nodos esperados (ID, IDHCHOJAGASTOQX, CODPRODUC, CANTIDADENTREGADA, CANTIDADGASTADA, CANTIDADDEVOLVER, CANTIDADACEPTADADEV, StatusOrder, RequestType, IDAGEPROGQX).; Debe existir la hoja de gasto QX identificada por el id a confirmar.; Debe existir la programación de cirugía referenciada en IDAGEPROGQX para que la actualización tenga efecto.; La función COMMON.GETDATE() debe estar disponible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ConfirmarHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada confirmación deja traza en HistoricalQXExpenseSheet con TYPE=2.; Los movimientos al kardex generados por esta operación siempre usan TIPREGIST=5 y TIPORIREG=13.; El estado final de la hoja confirmada queda en ESTADO=2.; Cuando la programación de cirugía existe, queda marcada como DILIGENCIOHOJAGASTO=1.; Los nuevos detalles insertados vía MERGE quedan con IDPRODUCTO en NULL.; Cualquier error es capturado y devuelto como resultset con CodeMessage=''999'' (no se relanza).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ConfirmarHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Hoja de gasto quirúrgico; Programación de cirugía; Kardex del paciente; Paquete quirúrgico; Paquete de enfermería; Producto adicional; Consumo de medicamentos/insumos en quirófano; Confirmación de hoja de gasto; Histórico de hoja de gasto QX; Centro de atención; Unidad funcional; Profesional de la salud', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ConfirmarHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.HistoricalQXExpenseSheet: Siempre que se ejecuta la confirmación, se inserta un registro histórico con TYPE=2 (confirmación) asociado a la hoja, profesional, centro de atención y unidad funcional.; [MERGE] dbo.HCHOJAGASTOQXD: Si existe coincidencia por CODPRODUC + RequestType + ID, se actualizan CANTIDADGASTADA, CANTIDADDEVOLVER, CANTIDADACEPTADADEV y StatusOrder; si no, se inserta un nuevo detalle con IDPRODUCTO=NULL.; [UPDATE] dbo.AGEPROGQX: Cuando CODAUTONU coincide con el IdProgramacionCirugia derivado del XML, se marca DILIGENCIOHOJAGASTO = 1.; [INSERT] dbo.HCKARDPAC: Por cada producto del XML se inserta un movimiento de kardex con TIPREGIST=5 y TIPORIREG=13, usando CANTIDADGASTADA como cantidad y un descriptivo según RequestType (1=Paquete quirúrgico, 2=Producto adicional, otro=Paquete enfermería).; [UPDATE] dbo.HCHOJAGASTOQXD: Tras insertar en kardex, se actualiza CONSEKARDEX en el detalle haciendo match por IDHCHOJAGASTOQX + CODPRODUC + RequestType con el consecutivo generado (NEWID).; [UPDATE] dbo.HCHOJAGASTOQX: Al finalizar el proceso, la hoja identificada se marca ESTADO=2 y se registra el usuario que intentó confirmar en UserTryingConfirm.; [RETURN_RESULT] (resultset): Si todo termina sin error, retorna CodeMessage=''0'' con mensaje de éxito; si ocurre excepción, retorna CodeMessage=''999'' con el ERROR_MESSAGE y la línea.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ConfirmarHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si RequestType = 1 en el producto del XML → El movimiento de kardex se describe como ''Paquete quirúrgico, origen enfermería''. else Se evalúa la siguiente condición.; si RequestType = 2 en el producto del XML → El movimiento de kardex se describe como ''Producto adicional, origen enfermería''. else Se describe como ''Paquete enfermería, origen enfermería''.; si Existe coincidencia (CODPRODUC, RequestType, ID) entre XML y HCHOJAGASTOQXD → Se actualizan cantidades y StatusOrder del detalle existente. else Se inserta un nuevo registro de detalle.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ConfirmarHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'COMMON.GETDATE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ConfirmarHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCHOJAGASTOQXD', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ConfirmarHojaGastoQX';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ConfirmarHojaGastoQX';
-- GO
