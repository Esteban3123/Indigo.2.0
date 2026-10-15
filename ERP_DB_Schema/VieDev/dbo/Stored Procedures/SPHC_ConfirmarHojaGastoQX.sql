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