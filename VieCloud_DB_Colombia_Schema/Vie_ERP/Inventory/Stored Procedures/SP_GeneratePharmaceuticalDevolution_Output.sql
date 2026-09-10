CREATE PROCEDURE [Inventory].[SP_GeneratePharmaceuticalDevolution_Output]
	@DevolutionXml xml,
	@AnnulateXml xml,
	@User varchar(20),
	------------------------------------------------------
	@CodeMessage VARCHAR(10) OUTPUT,
	@Message VARCHAR(MAX) OUTPUT,
	------------------------------------------------------
	@DevolutionId INT OUTPUT,
	@StatusResult TINYINT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @UserName AS Varchar(50) = (SELECT P.Fullname FROM [Security].[User] U INNER JOIN [Security].[Person] P ON U.IdPerson = P.Id WHERE UserCode = @user)

	IF OBJECT_ID('tempdb..#tmpBatchSerialProduct') IS NOT NULL DROP TABLE #tmpBatchSerialProduct

	DECLARE @IdDevolution int, @CodeDevolution varchar(20), 
		@OperatingUnitId int, @DocumentDate datetime, 
		@WarehouseId int, @CodeNameWarehouse varchar(20) , 
		@AdmissionNumber varchar(20), @AdmissionNumberDestination varchar(20), @CodePacient varchar(20), 
		@IdThirdPartyPacient int, @Observation varchar(300), 
		@Status tinyint, @CreationUser varchar(20), 
		@CreationDate date, @ModificationUser varchar(20), 
		@ModificationDate date, @ConfirmationUser varchar(20), 
		@ConfirmationDate date, @AnnulmentUser varchar(20), 
		@AnnulmentDate date, 
		@EntityName varchar(250), @EntityCode varchar(20), @EntityId int

	DECLARE @IsDashBoard int, @DevolutionOrigin varchar(2), 
		@CareCenterCode varchar(20), 
		@FunctionUnitCode varchar(20), 
		@FunctionUnitCodeWithTrim Varchar(20),
		@ConsecutiveCrystal int

	DECLARE @TableDetail table
	(
		RowId Int Identity(1,1) Primary Key Clustered, 
		Id int,
		PharmaceuticalDispensingDevolutionId int,
		PharmaceuticalDispensingDetailBatchSerialId int, 
		Quantity int, 
		ProductId int,
		CodeProduct varchar(100) null, 
		OrderedHealthProfessionalCode varchar(50) null, 
		EntityState varchar(50), 
		ConsecutiveKardex VARCHAR(50) DEFAULT NEWID(), 
		FunctionUnitCode varchar(20),
		IDHCHOJAGASTOQX INT,
		EntityId int null,
		EntityName varchar(250) null,
		HCDEVMEDDId int null
	)

	DECLARE @tmpADINGRESO Table(
		RowId Int Identity(1, 1) Primary Key,
		NUMINGRES Char(10),
		IPCODPACI Char(25),
		IESTADOIN Char(1),
		GENCONENTITY Int Null
	)

	DECLARE @TableServiceOrderDetailDistributionId table(RowId Int Identity(1,1) Primary Key Clustered, id int)
	DECLARE @MessageReturn varchar(max) = ''
	DECLARE @PendingCount int 
	DECLARE @IdStayOrigin int
	DECLARE @IdStayDestination int
	DECLARE @UfuCodigoDestination varchar(20)
	DECLARE @CodiCamaDestination varchar(20)
	DECLARE @FechaEgresoPaciente datetime
	DECLARE @Uno Tinyint = 1, @Cero Tinyint = 0, @tres Tinyint = 3
	DECLARE @IdForm Int = 1516
	DECLARE @StateAdded Varchar(5) = 'Added',
		@StateDeleted Varchar(7) = 'Deleted',
		@StateModified Varchar(8) = 'Modified',
		@DocumentType11 Tinyint = 11,
		@EntityNamePharmaceutical Varchar(24) = 'PharmaceuticalDispensing'
	DECLARE @IDHCHOJAGASTOQX as integer
	DECLARE @GetDateTime DATETIME = Common.GETDATE()

	DECLARE @ProccesType INT = 1  -- Variable que indica si es Medicamento o Mezcla (Por Defecto se debe enviar en 1)

	begin try

		/****** Realizo las anulaciones *****/
		if Exists (select t.x.value('Consecutivo[1]','varchar(20)') from @AnnulateXml.nodes('/ViewDashboardPharmacyDetailDevolution') t(x)) begin
			Declare @TableAnnulate Table (	RowId Int Identity(1,1) Primary Key Clustered, 
											Consecutivo varchar(20),
											CodigoPacienteDevolucion varchar(20), 
											Ingreso varchar(20), 
											CODCENATE varchar(20), 
											UFUCODIGO varchar(20), 
											CODPROSAL varchar(30),
											CODPRODUC varchar(30), 
											CantidadDevuelta int, 
											PROESTADO tinyint,
											FECRESGIS datetime, 
											CODUSUARI varchar(20),
											NOPOS bit,
											Entidad varchar(900), 
											Producto varchar(900),
											ContratoPlan varchar(900), 
											Tipo tinyint, 
											CantidadPendiente int,
											Medico varchar(900), 
											Especialidad  varchar(900), 
											ConsecutiveKardex VARCHAR(50) DEFAULT NEWID(),
											IdHCMOANULB VARCHAR(4),
											[Description] VARCHAR(500),
											[EntityName] VARCHAR(100),
											[HCDEVMEDDId] INT)

			Insert Into @TableAnnulate
			select 
			t.x.value('Consecutivo[1]','varchar(20)'),
			t.x.value('CodigoPacienteDevolucion[1]','varchar(20)'),
			t.x.value('Ingreso[1]','varchar(20)'),
			t.x.value('CODCENATE[1]','varchar(20)'),
			t.x.value('UFUCODIGO[1]','varchar(20)'),
			t.x.value('CODPROSAL[1]','varchar(30)'),
			t.x.value('CODPRODUC[1]','varchar(30)'),
			t.x.value('CantidadDevuelta[1]','int'),
			t.x.value('PROESTADO[1]','tinyint'),
			t.x.value('FECRESGIS[1]','datetime'),
			t.x.value('CODUSUARI[1]','varchar(20)'),
			t.x.value('NOPOS[1]','bit'),
			t.x.value('Entidad[1]','varchar(900)'),
			t.x.value('Producto[1]','varchar(900)'),
			t.x.value('ContratoPlan[1]','varchar(900)'),
			t.x.value('Tipo[1]','tinyint'),
			t.x.value('CantidadPendiente[1]','int'),
			t.x.value('Medico[1]','varchar(900)'),
			t.x.value('Especialidad[1]','varchar(900)'),
			null,
			t.x.value('HCMOANULBId[1]','VARCHAR(4)'),
			t.x.value('Description[1]','VARCHAR(500)'),
			IIF(t.x.value('EntityName[1]','varchar(250)') = '', null, t.x.value('EntityName[1]','varchar(250)')),
			t.x.value('HCDEVMEDDId[1]','int')
			from @AnnulateXml.nodes('/ViewDashboardPharmacyDetailDevolution') t(x)

			if Exists (select ing.NUMINGRES from @tmpADINGRESO ing
				Inner Join @TableAnnulate ta On ing.NUMINGRES = ta.Ingreso
				where IESTADOIN In ('F', 'C')) Begin
				
				SELECT	@CodeMessage = '999', 
						@Message = 'No se puede confirmar la devolución debido a que el ingreso esta facturado o cerrado', 
						@DevolutionId = 0, 
						@StatusResult = 3
				return
			end

			IF EXISTS(SELECT 1 FROM @TableAnnulate where IdHCMOANULB is null or IdHCMOANULB ='') BEGIN				
				SELECT	@CodeMessage = '999', 
						@Message = 'No se puede anular la devolución debido a que no hay motivo de anulación', 
						@DevolutionId = 0, 
						@StatusResult = 3
				return
			END
			
			---- Inserto en el Kardex de Crystal
			Insert Into dbo.HCKARDPAC
				(NUMCONSEC,IPCODPACI,NUMINGRES,CODCENATE,UFUCODIGO,CODPROSAL,CODPRODUC,CANPRODUCT,TIPREGIST,HCSOLINSN,FECREGKAR,TIPORIREG,DESMOVPRO)
			Select ISNULL(ConsecutiveKardex, NEWID()), CodigoPacienteDevolucion, Ingreso, 
				CODCENATE, UFUCODIGO, CODPROSAL, CODPRODUC, CantidadPendiente, 
				'4', '', @GetDateTime, 9, 'Anulación del devolutivo en farmacia, origen devolutivo: ' + UFUCODIGO + ' - usuario: ' + @UserName 
			From @TableAnnulate
			WHERE CantidadDevuelta > 0

			UPDATE dbo.HCDEVMEDD set PROESTADO = '3' 
			from @TableAnnulate ta 
			Inner join dbo.HCDEVMEDD hc 
			on ta.Consecutivo = hc.CODCONCEC and ta.CODPRODUC = hc.CODPRODUC and ta.HCDEVMEDDId = hc.Id

			/***SEGMENTO AUDITORIA MedicalHistory***/	
				
				-- Se evalua si es mezcla o líquido para cambiar el ProccesType a 2
				IF EXISTS
							(
							SELECT 1
								FROM dbo.HCINFCONC Medicamento 
								JOIN dbo.HCINFLIQA CabeceraMezcla ON Medicamento.CODCONCEC = CabeceraMezcla.CODCONCEC_ORIGEN OR Medicamento.CODCONCEC = CabeceraMezcla.CODCONCEC 
								JOIN @TableAnnulate ta ON ta.Ingreso = Medicamento.NUMINGRES AND ta.CodigoPacienteDevolucion = Medicamento.IPCODPACI AND ta.CODPRODUC = Medicamento.CODPRODUC
								WHERE CabeceraMezcla.PREESTADO = 1 
							UNION 
							SELECT 1
								FROM dbo.HCINFLIQD Diluyente
								JOIN dbo.HCINFLIQA CabeceraMezcla ON Diluyente.CODCONCEC = CabeceraMezcla.CODCONCEC_ORIGEN OR Diluyente.CODCONCEC = CabeceraMezcla.CODCONCEC 
								JOIN @TableAnnulate ta ON ta.Ingreso = Diluyente.NUMINGRES AND ta.CodigoPacienteDevolucion = Diluyente.IPCODPACI AND ta.CODPRODUC = Diluyente.CODPRODUC
								WHERE CabeceraMezcla.PREESTADO = 1
							)
				BEGIN
					SET @ProccesType = 2
				END
				---------------------------------
				
				INSERT INTO MedicalHistory.TraceabilityDrugs(	[ProductCode],
																[NUMINGRES] ,
																[ProfessionalCode],
																[RegistrationDate] ,
																[Action],
																[UFUCODIGO],
																[IdSourceTable],
																[SourceTable],
																[Justification],
																[ProccesType])				
				SELECT	atc.Code,
						ta.Ingreso,
						@User,
						@GetDateTime,
						19, -- Farmacia - Anulación de devolutivo
						ta.UFUCODIGO,
						MAX(ta.HCDEVMEDDId),
						'HCDEVMEDD',
						CONCAT(TRIM(h.DESMOTANU),' - ',ta.[Description]),
						@ProccesType
				from @TableAnnulate ta 
				join dbo.HCDEVMEDD hc on ta.HCDEVMEDDId = hc.Id and ta.CODPRODUC = hc.CODPRODUC
				join Inventory.ATC atc WITH(NOLOCK) on hc.CODPRODUC = atc.code
				join HCDEVMEDC hcc WITH(NOLOCK) on hc.CODCONCEC = hcc.CODCONCEC and hcc.IDHCHOJAGASTOQX is null
				join HCMOANULB h on h.CODMOTANU = ta.IdHCMOANULB
				GROUP BY atc.Code, ta.Ingreso, ta.UFUCODIGO, h.DESMOTANU, ta.[Description]
		/********************************************/

			if Not Exists (select hc.CODCONCEC 
				from dbo.HCDEVMEDD hc With(Nolock)
				inner join @TableAnnulate ta on ta.Consecutivo = hc.CODCONCEC 
				where hc.CANPENDIE > @Cero and hc.PROESTADO = @Uno) begin -- Si todos los detalles ya fueron devueltos o anulados				
				Update dbo.HCDEVMEDC set DEVESTADO = '3'
				from dbo.HCDEVMEDC hc inner join @TableAnnulate ta on ta.Consecutivo = hc.CODCONCEC --where hc.CANPENDIE > 0 and hc.PROESTADO = 1

				--cargamos el ID de la hoja de gasto quirurgica
				set @IDHCHOJAGASTOQX = (Select top 1 hc.IDHCHOJAGASTOQX From dbo.HCDEVMEDC hc With(Nolock) inner join @TableAnnulate ta on ta.Consecutivo = hc.CODCONCEC Where IDHCHOJAGASTOQX IS NOT NULL)
				if @IDHCHOJAGASTOQX > 0 begin 

				   --1- Sin confirmar (hoja de gasto QX  sin procesar)
					update dbo.HCHOJAGASTOQX set ESTADO =1  Where ID = @IDHCHOJAGASTOQX
				end

				--Actualizar la cantidad actual del paquete en la tabla de los productos de los paquetes de enfermería.
				Declare @NursingPackageOrderQuantity as Int  = (Select count(*) from dbo.HCDEVMEDD hd inner join @TableAnnulate ta on ta.Consecutivo = hd.CODCONCEC and ta.CODPRODUC = hd.CODPRODUC inner join MedicalHistory.NursingPackagesOrderDetail NP ON NP.Id = hd.IdNursingPackagesOrderDetail)
				if @NursingPackageOrderQuantity > 0 begin
 
				   update NP set CurrentQuantity += ta.CantidadDevuelta
				   from dbo.HCDEVMEDD hd 
				   inner join @TableAnnulate ta on ta.Consecutivo = hd.CODCONCEC and ta.CODPRODUC = hd.CODPRODUC
				   inner join MedicalHistory.NursingPackagesOrderDetail NP ON NP.Id = hd.IdNursingPackagesOrderDetail
 
				   update NPC set Status = 2
				   from dbo.HCDEVMEDD hd 
				   inner join @TableAnnulate ta on ta.Consecutivo = hd.CODCONCEC and ta.CODPRODUC = hd.CODPRODUC
				   inner join MedicalHistory.NursingPackagesOrderDetail NP ON NP.Id = hd.IdNursingPackagesOrderDetail 
				   inner join MedicalHistory.NursingPackagesOrder NPC on NP.IdNursingPackagesOrder = NPC.Id
				end

				INSERT INTO [Inventory].[ReasonCancellationOfDevolution] (HCDEVMEDCId,HCMOANULBId,[Description],CreationUser,CreationDate)
				SELECT TOP 1  Consecutivo, IdHCMOANULB,Description,@User,common.GETDATE()
				FROM @TableAnnulate

			end
			ELSE BEGIN
				INSERT INTO [Inventory].[ReasonCancellationOfDevolution] (HCDEVMEDCId,HCMOANULBId,[Description],CreationUser,CreationDate,HCDEVMEDDId)
				SELECT Consecutivo, ta.IdHCMOANULB,ta.Description,@User,common.GETDATE(),hc.Id
				from @TableAnnulate ta 
				Inner join dbo.HCDEVMEDD hc on ta.Consecutivo = hc.CODCONCEC and ta.CODPRODUC = hc.CODPRODUC
			END

			--********************************PROCESO DE LIBERACION DE CAMA*****************************************

			Declare @ConsecutiveCrystalCursor int, @AdmissionNumberCursor varchar(20)

			Select Top 1 @ConsecutiveCrystalCursor = Consecutivo, 
				@AdmissionNumberCursor = Ingreso 
			From @TableAnnulate Order By RowId
				
			set @DevolutionOrigin = (Select ORIDEVMED From .HCDEVMEDC With(Nolock) WHERE CODCONCEC = @ConsecutiveCrystalCursor)				
			
			if Not Exists (select CODCONCEC from .HCDEVMEDD With(Nolock) WHERE CODCONCEC = @ConsecutiveCrystalCursor AND PROESTADO in (@uno,@tres)) begin
				if @DevolutionOrigin = '1' begin --- Si es un traslado de cama						
					Declare @MinFECINIEST DateTime = (select MIN(FECINIEST) 
						From dbo.CHREGESTA With(Nolock) 
						Where NUMINGRES = @AdmissionNumberCursor And REGESTADO = @uno)

					Declare @MaxFECINIEST DateTime = (select MAX(FECINIEST) 
						From dbo.CHREGESTA With(Nolock)
						Where NUMINGRES = @AdmissionNumberCursor And REGESTADO = @uno)

					Set @IdStayOrigin = (select ID 
						From dbo.CHREGESTA With(Nolock) 
						Where NUMINGRES = @AdmissionNumberCursor and REGESTADO = @Uno And FECINIEST = @MinFECINIEST)
					Set @IdStayDestination = (select ID 
						From dbo.CHREGESTA With(Nolock) 
						Where NUMINGRES = @AdmissionNumberCursor and REGESTADO = @Uno and FECINIEST = @MaxFECINIEST)

					--- Actualizo la estancia de origen
					Declare @FECINIEST DateTime
					If @IdStayOrigin <> @IdStayDestination Begin
						Select @FECINIEST = FECINIEST from dbo.CHREGESTA where ID = @IdStayDestination
						update dbo.CHREGESTA set 
							FECFINEST = @FECINIEST, 
							REGESTADO = 2, 
							REGDIAEST = DATEDIFF(DAY, FECINIEST, @FECINIEST) 
						where ID = @IdStayOrigin
					End
					Else Begin							
						Select Top 1 @FECINIEST = FECEGRESO 
						From dbo.CHREGEGRE WHERE NUMINGRES = @AdmissionNumberCursor						
						If @FECINIEST Is Not Null
							update dbo.CHREGESTA set 
								FECFINEST = @FECINIEST, 
								REGESTADO = 2, 
								REGDIAEST = DATEDIFF(DAY, FECINIEST, @FECINIEST) 
							where ID = @IdStayOrigin											
						Else
							update dbo.CHREGESTA set REGESTADO = 2 where ID = @IdStayOrigin
					End
					--- Actualizo la cama de origen para liberarla
					Update dbo.CHCAMASHO Set ESTADCAMA = 1, CAMTRAMED = 0, CODCONCEC = null, CODAISLAM = null, CAMTIPANO = 0, CAMDEVMED = 0, BedType = null, TypeTransfer = null
					From dbo.CHREGESTA re 
					Inner Join dbo.CHCAMASHO ca on re.CODICAMAS = ca.CODICAMAS 
					Where re.ID = @IdStayOrigin

					--- Actualizo la cama de destino
					Update dbo.CHCAMASHO set CODCONCEC = null , BedType = null, TypeTransfer = null 
					From dbo.CHREGESTA re 
					Inner join dbo.CHCAMASHO ca on re.CODICAMAS = ca.CODICAMAS 
					Where re.ID = @IdStayDestination

					--- Actualizo el ingreso para colocarle la cama actual del paciente y la unidad funcional							
					Select @UfuCodigoDestination = uf.UFUCODIGO, @CodiCamaDestination = ca.CODICAMAS 
					From dbo.CHREGESTA re 
					Inner Join dbo.CHCAMASHO ca With(Nolock) On re.CODICAMAS = ca.CODICAMAS 
					Inner Join dbo.INUNIFUNC uf With(Nolock) On uf.UFUCODIGO = ca.UFUCODIGO 
					Where re.ID = @IdStayDestination

					Update dbo.ADINGRESO Set UFUAACTHOS = @UfuCodigoDestination, UFUACTPAC = @UfuCodigoDestination, 
						CODCAMACT = @CodiCamaDestination 
					Where NUMINGRES = @AdmissionNumberCursor

				end
				else if @DevolutionOrigin = '2' begin --- Si es Egreso de cama
					Update dbo.CHCAMASHO Set ESTADCAMA = 2, CAMTRAMED=0, CODCONCEC = null, CODAISLAM = null, 
						CAMTIPANO = 0, CAMDEVMED = 0, CAMRECDEV = 1 
					From dbo.CHREGESTA re 
					Inner Join dbo.CHCAMASHO ca on re.CODICAMAS = ca.CODICAMAS 
					Where re.NUMINGRES = @AdmissionNumberCursor and re.REGESTADO = @uno
					if Not Exists (select NUMINGRES from dbo.CHREGEGRE With(Nolock) Where NUMINGRES = @AdmissionNumberCursor and FECEGRESO is not null) begin
						SELECT	@CodeMessage = '999', 
								@Message = 'La fecha del egreso del paciente no existe', 
								@DevolutionId = 0, 
								@StatusResult = 3
						return
					end
					set @FechaEgresoPaciente = (select FECEGRESO from dbo.CHREGEGRE With(Nolock) where NUMINGRES = @AdmissionNumberCursor and FECEGRESO is not null)
				end
			end else if Exists (select CODCONCEC from .HCDEVMEDD With(Nolock) WHERE CODCONCEC = @ConsecutiveCrystalCursor AND PROESTADO in (@tres)) begin

				if @DevolutionOrigin = '1' begin --- Si es un traslado de cama y anulan total o parcial un devolutivo,marcamos rechazo
				
					Declare @MinFECINIEST_t DateTime = (select MIN(FECINIEST) 
						From dbo.CHREGESTA With(Nolock) 
						Where NUMINGRES = @AdmissionNumberCursor And REGESTADO = @uno)
					
					Set @IdStayOrigin = (select ID 
						From dbo.CHREGESTA With(Nolock) 
						Where NUMINGRES = @AdmissionNumberCursor and REGESTADO = @Uno And FECINIEST = @MinFECINIEST_t)
					

					    --anularon devolutivo por ende debemos marcar rechazado el devolutivo
						Update dbo.CHCAMASHO Set ESTADCAMA = 2, CAMTRAMED=0, CODCONCEC = null, CODAISLAM = null, 
								CAMTIPANO = 0, CAMDEVMED = 0, CAMRECDEV = 1
							From dbo.CHREGESTA re 
							Inner Join dbo.CHCAMASHO ca on re.CODICAMAS = ca.CODICAMAS 
							Where re.NUMINGRES = @AdmissionNumberCursor and re.REGESTADO = @uno and re.id = @IdStayOrigin
				end
				
			end
		

			--******************************************************************************************************
			if(Select Count(*) From @DevolutionXml.nodes('/PharmaceuticalDispensingDevolution') t(x)) = 0 begin
				SELECT	@CodeMessage = '0', 
						@Message = 'Se Anulo Correctamente la Devolución', 
						@DevolutionId = 0, 
						@StatusResult = 1
				return
			end
		end 	/*fin anulaciones*/

		if (Select Count(*) From @DevolutionXml.nodes('/PharmaceuticalDispensingDevolution') t(x)) > 0 begin
			select 
				@IdDevolution = t.x.value('Id[1]','int'),
				@CodeDevolution = t.x.value('Code[1]','varchar(20)'),
				@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
				@DocumentDate = t.x.value('DocumentDate[1]','datetime'),
				@WarehouseId = t.x.value('WarehouseId[1]','int'),
				@CodeNameWarehouse = t.x.value('CodeNameWarehouse[1]','varchar(20)'),
				@AdmissionNumber = t.x.value('AdmissionNumber[1]','varchar(10)'),
				@AdmissionNumberDestination = t.x.value('AdmissionNumberDestination[1]','varchar(10)'),
				@Observation = t.x.value('Observation[1]','varchar(300)'),
				@Status = t.x.value('Status[1]','tinyint'),
				@IsDashBoard = t.x.value('IsDashBoard[1]','bit'), ---- los campos de aca en adelante solo se solicitan cuando es desde dashboard
				@DevolutionOrigin = t.x.value('DevolutionOrigin[1]','varchar(2)'),
				@CareCenterCode = t.x.value('CareCenterCode[1]','varchar(20)'),
				@FunctionUnitCode = t.x.value('FunctionUnitCode[1]','varchar(20)'),
				@ConsecutiveCrystal = t.x.value('ConsecutiveCrystal[1]','int'),
				---------------------------------------------------------------
				@EntityName = t.x.value('EntityName[1]','varchar(250)'),
				@EntityCode = t.x.value('EntityCode[1]','varchar(20)'),
				@EntityId = t.x.value('EntityId[1]','int')
			from @DevolutionXml.nodes('/PharmaceuticalDispensingDevolution') t(x)

			Set @FunctionUnitCodeWithTrim = Ltrim(Rtrim(@FunctionUnitCode))

			--cargamos el ID de la hoja de gasto quirurgica
			set @IDHCHOJAGASTOQX = (Select IDHCHOJAGASTOQX From dbo.HCDEVMEDC With(Nolock) Where CODCONCEC = @ConsecutiveCrystal AND IDHCHOJAGASTOQX IS NOT NULL)
		
			Insert Into @tmpADINGRESO
			Select NUMINGRES, IPCODPACI, IESTADOIN, GENCONENTITY From dbo.ADINGRESO With(Nolock) Where NUMINGRES = @AdmissionNumber

			IF @IsDashBoard = 1
			BEGIN
				IF ISNULL(@EntityName, '') = ''
				BEGIN
					SET @EntityName = 'SaveDashboardPharmacyDevolution'
					SET @EntityId = @ConsecutiveCrystal
				END
			END

			If Exists (Select NUMINGRES From @tmpADINGRESO Where NUMINGRES = @AdmissionNumber And IESTADOIN in ('F', 'C')) begin
				SELECT	@CodeMessage = '999', 
						@Message = 'No se puede confirmar la devolución debido a que el ingreso esta facturado o cerrado', 
						@DevolutionId = 0, 
						@StatusResult = 3
				return
			end

			-- validamos que el usuario no este bloqueado y si tiene bloqueo en parametros de facturacion tipo (farmacia y facturacion)
			Declare @LockType TINYINT
			select Top 1 @LockType = sb.IncomeLockType 
			From Billing.SettingsBilling sb where sb.IdOperatingUnit = @OperatingUnitId

			Declare @LockStatus char(3)
			select Top 1 @LockStatus = ai.IESTADOIN 
			From dbo.ADINGRESO ai where ai.NUMINGRES= @AdmissionNumber

			IF  @LockStatus ='B' AND @LockType = 3 
			begin
				select	@CodeMessage = '999', 
							@Message = 'No se puede realizar la acción debido a que el ingreso se encuentra bloqueado en Farmacia y Facturación', 
							@DevolutionId = 0,
							@StatusResult = 3
				return
			END

			Insert Into @TableDetail
				Select
					t.x.value('Id[1]','int'),
					@IdDevolution,
					t.x.value('PharmaceuticalDispensingDetailBatchSerialId[1]','int'),
					t.x.value('Quantity[1]','int'),
					t.x.value('ProductId[1]','int'),
					t.x.value('CodeProduct[1]','varchar(100)'),  --- Solo se solicita si es desde DashBoard
					t.x.value('OrderedHealthProfessionalCode[1]','varchar(50)'),
					t.x.value('EntityState[1]','varchar(50)'),
					null,
					t.x.value('FunctionUnitCode[1]','varchar(20)'),
					t.x.value('IDHCHOJAGASTOQX[1]','int'),
					IIF(t.x.value('EntityId[1]','int') = 0, null, t.x.value('EntityId[1]','int')),
					IIF(t.x.value('EntityName[1]','varchar(250)') = '', null, t.x.value('EntityName[1]','varchar(250)')),
					t.x.value('HCDEVMEDDId[1]','int')
				from @DevolutionXml.nodes('/PharmaceuticalDispensingDevolution/PharmaceuticalDispensingDevolutionDetail') t(x)

			SET @CodePacient = (select IPCODPACI From @tmpADINGRESO where NUMINGRES = @AdmissionNumber)
			IF @CodePacient IS NULL BEGIN
				SELECT	@CodeMessage = '999', 
						@Message = 'El ingreso ' + @AdmissionNumber + ' no existe en Indigo Crystal', 
						@DevolutionId = 0, 
						@StatusResult = 3
				Return
			END

			Set @IdThirdPartyPacient = (Select Id From Common.ThirdParty With(Nolock) Where Nit = @CodePacient)
			if @IdThirdPartyPacient Is Null Begin
				SELECT	@CodeMessage = '999', 
						@Message = 'El paciente ' + @CodePacient + ' no existe como tercero en Indigo Vie', 
						@DevolutionId = 0, 
						@StatusResult = 3
				Return
			End

			--set @IdThirdPartyPacient = (select Id from Common.ThirdParty where Nit = @CodePacient)
			Declare @SettingInventoryId int, @IdJournalVoucherType int, 
				@PharmaceuticalDispensingGetThirdParty tinyint, 
				@PharmaceuticalDispensingThirdPartyId int, 
				@AssociateCostCenter tinyint,
				@AssociateCostMainAccount TINYINT, 
				@DiscountSalesMainAccountId int
			
			Select @SettingInventoryId = Id, 
				@IdJournalVoucherType = SalesReturnJournalVoucherTypeId, 
				@PharmaceuticalDispensingGetThirdParty = PharmaceuticalDispensingGetThirdParty, 
				@PharmaceuticalDispensingThirdPartyId = PharmaceuticalDispensingThirdPartyId, 
				@AssociateCostCenter = AssociateCostCenter, 
				@AssociateCostMainAccount = AssociateCostMainAccount,
				@DiscountSalesMainAccountId = DiscountSalesMainAccountId 
			From Inventory.SettingInventory With(Nolock) where OperatingUnitId = @OperatingUnitId

			If @SettingInventoryId Is Null Begin
				SELECT	@CodeMessage = '999', 
						@Message = 'No existe parametros de inventarios para la unidad operativa ' + (select UnitName from Common.OperatingUnit With(Nolock) where Id = @OperatingUnitId), 
						@DevolutionId = 0, 
						@StatusResult = 3
				return
			end

			if @Status <> 3 AND Not Exists (Select Id From Inventory.SettingInventory With(Nolock) Where OperatingUnitId = @OperatingUnitId and [Year] = Year(@DocumentDate) and [Month] = Month(@DocumentDate)) Begin				
				Declare @DatePeriod Varchar(20) = (Select Concat([Year], '-', Right(CONCAT('0', [Month]), 2)) From Inventory.SettingInventory With(Nolock) Where OperatingUnitId = @OperatingUnitId)

				SELECT	@CodeMessage = '999', 
						@Message = 'El periodo actual de inventario no coincide con la fecha del documento, Periodo Actual de Inventario: ' + @DatePeriod, 
						@DevolutionId = 0, 
						@StatusResult = 3
				return
			end
	
			Declare @Prefix varchar(20) = ''
			If IsNull(@WarehouseId, 0) = 0 begin --por aca entra cuando se hace desde dashboard				
				Select @WarehouseId = Id,
					@Prefix = Prefix
				From Inventory.Warehouse With(Nolock) Where Code = @CodeNameWarehouse
				if @WarehouseId Is Null Begin
					SELECT	@CodeMessage = '999', 
							@Message = 'El almacen ' + @CodeNameWarehouse + ' no esta homologado en Indigo Vie', 
							@DevolutionId = 0, 
							@StatusResult = 3
					Return
				end
				--set @WarehouseId = (select Id from Inventory.Warehouse where Code = @CodeNameWarehouse)
				if Not Exists (Select Id From Inventory.WarehouseUser With(Nolock) Where WarehouseId = @WarehouseId And UserCode = @User) begin
					SELECT	@CodeMessage = '999', 
							@Message = 'El usuario no tiene permisos para el almacen ' + @CodeNameWarehouse, 
							@DevolutionId = 0, 
							@StatusResult = 3
					Return
				end				
			end
			Else Begin
				Select @Prefix = Prefix From Inventory.Warehouse With(Nolock) Where Id = @WarehouseId
			End

			IF EXISTS 
			(
				SELECT 1
				FROM @TableDetail pddevd
				JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON pddevd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
				JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pddbs.PharmaceuticalDispensingDetailId = pdd.Id
				JOIN Inventory.Warehouse w WITH(NOLOCK) ON w.Id =pdd.WarehouseId
				WHERE @WarehouseId <> pdd.WarehouseId AND (w.VirtualStore =1 OR w.WarehouseConsignment =1 OR w.CustodyStore=1 OR w.TransitStore=1 OR w.ControlStore =1)
			) 
			BEGIN
				SELECT	@CodeMessage = '999', 
						@Message = 'Existen dispensaciones de almacenes diferentes al seleccionado', 
						@DevolutionId = 0, 
						@StatusResult = 3
				return
			END
/*se valida que al hacer la devolucion los almacenes tiene que ser del mismo tipo( en cuanto a si es o no de consignacion) para que no se permita, la devolucion de un almacen Normal a un de consginacion*/
			if EXISTS(SELECT 1
						from(	SELECT pdd.WarehouseId
								FROM @TableDetail td
								join Inventory.PharmaceuticalDispensingDetailBatchSerial pddb on td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id
								join Inventory.PharmaceuticalDispensingDetail pdd on pddb.PharmaceuticalDispensingDetailId=pdd.Id
								GROUP by pdd.WarehouseId) pdd
						join Inventory.Warehouse w1 on pdd.WarehouseId=w1.Id
						join Inventory.Warehouse w2 on w2.id =@WarehouseId
						where w1.WarehouseConsignment <> w2.WarehouseConsignment and (w1.Id<>w2.Id))
						BEGIN
							SELECT	@CodeMessage = '999', 
							@Message = 'el Tipo de almacen es diferente al que hizo la dispensación', 
							@DevolutionId = 0, 
							@StatusResult = 3
							return
						END
							
			-- Si es nuevo
			if @IdDevolution = 0 begin
				--- Obtenemos la secuencia numerica
				If IsNull(@CodeDevolution, '') = '' begin					
					Declare @idSequenceDetail int, 
						@pattern varchar(300), 
						@NextS bigint,
						@Scope varchar(5),
						@IdSequence int,
						@IdSequenceCommon int						

					Select @IdSequence = Id, @Scope = Scope, @IdSequenceCommon = IdSequence 
					From Inventory.InventorySequence With(Nolock)
					Where IdForm = @IdForm

					If @IdSequence Is Null Begin
						SELECT	@CodeMessage = '999', 
								@Message = 'No existe secuencia numerica para el formulario de Devolución de Dispensación', 
								@DevolutionId = 0, 
								@StatusResult = 3
						Return
					End
					If @Scope = 'O' Begin --- Secuencia por Prefijo
						Select @idSequenceDetail = Id From Inventory.InventorySequenceDetail With(Nolock)
						Where InventorySequenceId = @IdSequence And Prefix = @Prefix
						If @idSequenceDetail Is Null Begin
							Insert Into Inventory.InventorySequenceDetail(InventorySequenceId, IdSequense, IdOperatingUnit, [Next], Prefix)
							Values(@IdSequence, @IdSequenceCommon, null, 1, @Prefix)

							Set @idSequenceDetail = Scope_Identity()
						End
						Select @pattern = cs.Pattern From Common.Sequense cs With(Nolock) Where cs.Id = @IdSequenceCommon
					end
					else begin -- Secuencia por Unidad operativa
						Select @pattern = cs.Pattern, @idSequenceDetail = psd.Id  
						From Inventory.InventorySequenceDetail psd With(Nolock)
						Inner Join Common.Sequense cs With(Nolock) on cs.Id = psd.IdSequense 
						Where psd.InventorySequenceId = @IdSequence And IdOperatingUnit = @OperatingUnitId
					end
					Update Inventory.InventorySequenceDetail Set @NextS = [Next] += 1 Where Id = @idSequenceDetail
					Select @CodeDevolution = dbo.GetSequence(@Prefix, @pattern, (@NextS - 1))

					if @CodeDevolution = '__ERROR_MAXVALUE__' begin
						SELECT	@CodeMessage = '999', 
								@Message = 'La secuencia para Devolución de Dispensación alcanzo su valor maximo', 
								@DevolutionId = 0, 
								@StatusResult = 3
						return
					end

					--- Inserto en la tabla de control
					Insert Into Inventory.InventoryControlDocument(DocumentNumber,DocumentType,DocumentUser,DocumentDate) values(@CodeDevolution, 11, @User, [Common].[GETDATE]())

				end

				
				Insert Into Inventory.PharmaceuticalDispensingDevolution(Code,OperatingUnitId,DocumentDate,WarehouseId,AdmissionNumber,Observation,[Status],CreationUser,CreationDate, EntityName, EntityCode, EntityId)
				Values (@CodeDevolution, @OperatingUnitId, @DocumentDate, @WarehouseId, @AdmissionNumber, @Observation, @Status, @User, [Common].[GETDATE](), @EntityName, @EntityCode, @EntityId)				
				
				Set @IdDevolution = SCOPE_IDENTITY()

				DECLARE @MappingTable as TABLE(TempId  INT,
												PharmaceuticalDispensingDevolutionDetailId INT)

				Delete From @TableDetail Where EntityState = @StateDeleted

				MERGE INTO Inventory.PharmaceuticalDispensingDevolutionDetail AS target
				USING (
					SELECT
						MIN(temp.RowId) AS RowId,
						@IdDevolution AS PharmaceuticalDispensingDevolutionId,
						temp.PharmaceuticalDispensingDetailBatchSerialId,
						SUM(temp.Quantity) AS Quantity,
						MAX(temp.EntityId) AS EntityId,
						MAX(temp.EntityName) AS EntityName
					FROM @TableDetail temp
					WHERE temp.EntityState = @StateAdded
					GROUP BY temp.PharmaceuticalDispensingDetailBatchSerialId
				) AS source
				ON 1 = 0
				WHEN NOT MATCHED THEN
					INSERT (PharmaceuticalDispensingDevolutionId, PharmaceuticalDispensingDetailBatchSerialId, Quantity, EntityId, EntityName)
					VALUES (source.PharmaceuticalDispensingDevolutionId, source.PharmaceuticalDispensingDetailBatchSerialId, source.Quantity, source.EntityId, source.EntityName)
				OUTPUT
					source.RowId,
					INSERTED.Id
				INTO @MappingTable (TempId, PharmaceuticalDispensingDevolutionDetailId);
				
				
				/***SEGMENTO AUDITORIA MedicalHistory***/
				
				-- Se evalua si es mezcla o líquido para cambiar el ProccesType a 2
				IF EXISTS
							(
							SELECT 1
								FROM dbo.HCINFCONC Medicamento 
								JOIN dbo.HCINFLIQA CabeceraMezcla ON Medicamento.CODCONCEC = CabeceraMezcla.CODCONCEC_ORIGEN OR Medicamento.CODCONCEC = CabeceraMezcla.CODCONCEC 
								JOIN @tmpADINGRESO adin ON adin.NUMINGRES = Medicamento.NUMINGRES AND adin.IPCODPACI = Medicamento.IPCODPACI
								JOIN @TableDetail td ON td.CodeProduct = Medicamento.CODPRODUC
								WHERE CabeceraMezcla.PREESTADO = 1 
							UNION 
							SELECT 1
								FROM dbo.HCINFLIQD Diluyente
								JOIN dbo.HCINFLIQA CabeceraMezcla ON Diluyente.CODCONCEC = CabeceraMezcla.CODCONCEC_ORIGEN OR Diluyente.CODCONCEC = CabeceraMezcla.CODCONCEC 
								JOIN @tmpADINGRESO adin ON adin.NUMINGRES = Diluyente.NUMINGRES AND adin.IPCODPACI = Diluyente.IPCODPACI
								JOIN @TableDetail td ON td.CodeProduct = Diluyente.CODPRODUC
								WHERE CabeceraMezcla.PREESTADO = 1
							)
				BEGIN
					SET @ProccesType = 2
				END

				----------------------------------------

				IF @IsDashBoard =1 AND @Status <> 2 AND EXISTS(	SELECT 1 
												FROM Inventory.ATC atc	WITH(NOLOCK) 
												JOIN @TableDetail temp ON atc.Code =temp.CodeProduct
												WHERE temp.EntityState = @StateAdded) BEGIN

												
						INSERT INTO MedicalHistory.TraceabilityDrugs(	[ProductCode],
																		[NUMINGRES] ,
																		[ProfessionalCode],
																		[RegistrationDate] ,
																		[Action],
																		[UFUCODIGO],
																		[IdSourceTable],
																		[SourceTable],
																		[ProccesType])				
						SELECT	atc.Code,
								@AdmissionNumber,
								@User,
								@GetDateTime,
								18, -- Farmacia - Aceptación de devolutivo
								@FunctionUnitCode,
								@IdDevolution,
								'PharmaceuticalDispensingDevolution',
								@ProccesType
						from Inventory.ATC atc	WITH(NOLOCK) 
						JOIN @TableDetail temp on atc.Code =temp.CodeProduct
						JOIN @MappingTable mt on temp.RowId = mt.TempId
						JOIN HCDEVMEDC hc WITH(NOLOCK) 
								on temp.EntityId = hc.CODCONCEC and temp.EntityName ='HCDEVMEDD' --llega el nombre de la tabla detalle pero el entityId realmente es EL id de la cabecera
								and hc.IDHCHOJAGASTOQX IS NULL
						Where temp.EntityState = @StateAdded 
						GROUP BY atc.Code
				END
				/*************************************************************************/

			end
			else begin
				if @Status =  3 begin
					Update Inventory.PharmaceuticalDispensingDevolution set AnnulmentUser = @User, AnnulmentDate = [Common].[GETDATE](), [Status] = 3 where Id = @IdDevolution
					Delete from Inventory.InventoryControlDocument where DocumentNumber = @CodeDevolution and DocumentType = @DocumentType11

					--si proviene de una hoja de gasto QX
					if @IDHCHOJAGASTOQX > 0 begin 		
					   --1- Sin confirmar (hoja de gasto QX  sin procesar)
						update dbo.HCHOJAGASTOQX set ESTADO =1  Where ID = @IDHCHOJAGASTOQX
					end

					Set @MessageReturn = 'Se anulo correctamente la Devolucion de Dispensacion'
				end
				else begin
					---Inserto en los detalles 
					Insert Into Inventory.PharmaceuticalDispensingDevolutionDetail
						(PharmaceuticalDispensingDevolutionId, PharmaceuticalDispensingDetailBatchSerialId, Quantity, EntityId, EntityName)
					Select
						@IdDevolution,
						td.PharmaceuticalDispensingDetailBatchSerialId,
						SUM(td.Quantity) AS Quantity,
						MAX(td.EntityId) AS EntityId,
						MAX(td.EntityName) AS EntityName
					from @TableDetail td
					where td.EntityState = @StateAdded
					group by td.PharmaceuticalDispensingDetailBatchSerialId

					---Actualizo los datos
					Update Inventory.PharmaceuticalDispensingDevolutionDetail 
						set PharmaceuticalDispensingDetailBatchSerialId = td.PharmaceuticalDispensingDetailBatchSerialId, 
						Quantity = td.Quantity, EntityId = td.EntityId, EntityName = td.EntityName
					From @TableDetail td 
					inner join Inventory.PharmaceuticalDispensingDevolutionDetail pdd on pdd.Id = td.Id 
					where EntityState = @StateModified
					
					--Elimino los datos
					--Delete From Inventory.PharmaceuticalDispensingDevolutionDetail where Id in (select Id from @TableDetail where EntityState = @StateDeleted)
					Delete pdd From Inventory.PharmaceuticalDispensingDevolutionDetail pdd 
					Inner Join @TableDetail td On pdd.Id = td.Id Where td.EntityState = @StateDeleted
					Delete From @TableDetail where EntityState = @StateDeleted
					
					-- Actualizo la cabecera de la devolucion
					Update Inventory.PharmaceuticalDispensingDevolution set ModificationUser = @User, ModificationDate = [Common].[GETDATE](),Status = @Status  where Id = @IdDevolution
				end
			end

			if @Status = 2 begin
				/******* CODIGO DE CONFIRMAR LA DEVOLUCION ******/
				declare @TableServiceOrderTmp table(RowId Int Identity(1,1) Primary Key Clustered, 
					IdServiceOrder int,
					IdServiceOrderDetail int, 
					IdProduct int, 
					Quantity int)
				--if (select count(*) from Billing.ServiceOrder where [Status] > 1 And EntityName = 'PharmaceuticalDispensing' And EntityCode in (select pd.Code from @TableDetail td inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddb on td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id inner join Inventory.PharmaceuticalDispensingDetail pdd on pdd.Id = pddb.PharmaceuticalDispensingDetailId inner join Inventory.PharmaceuticalDispensing pd on pd.Id = pdd.PharmaceuticalDispensingId)) > 0 begin
				If Exists (select so.Id 
					from Billing.ServiceOrder so With(Nolock)
					Inner Join Inventory.PharmaceuticalDispensing pd With(Nolock) on pd.Code = so.EntityCode
					Inner Join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) on pdd.PharmaceuticalDispensingId = pd.Id
					Inner Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddb With(Nolock) on pddb.PharmaceuticalDispensingDetailId = pdd.Id
					Inner Join @TableDetail td On td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id
					Where so.[Status] > @Uno And so.EntityName = @EntityNamePharmaceutical) Begin
					Declare @StringServiceOrder varchar(max) = (Select so.Code + ', '
						from Billing.ServiceOrder so With(Nolock)
						Inner Join Inventory.PharmaceuticalDispensing pd With(Nolock) on pd.Code = so.EntityCode
						Inner Join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) on pdd.PharmaceuticalDispensingId = pd.Id
						Inner Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddb With(Nolock) on pddb.PharmaceuticalDispensingDetailId = pdd.Id
						Inner Join @TableDetail td On td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id
						Where so.[Status] > @Uno And so.EntityName = @EntityNamePharmaceutical For Xml Path(''))
					SELECT	@CodeMessage = '999', 
							@Message = 'No se puede hacer la devolución porque las siguientes ordenes de servicio estan anuladas o facturadas: ' + @StringServiceOrder, 
							@DevolutionId = 0, 
							@StatusResult = 3
					Return
				End										
				------------------------------------------------------------------------
				IF @IsDashboard = 1 BEGIN

					IF EXISTS (
						select 1 
						From (
							Select CodeProduct, Sum(Quantity) As Quantity, HCDEVMEDDId From @TableDetail Group By CodeProduct, HCDEVMEDDId
						) td 
						Inner Join dbo.HCDEVMEDD hcd With(Nolock) on hcd.CODCONCEC = @ConsecutiveCrystal And hcd.CODPRODUC = td.CodeProduct and td.	HCDEVMEDDId = hcd.id
						WHERE td.Quantity > CANPENDIE

					) BEGIN
						Declare @StringHCDEVMEDD varchar(max) = (Select td.CodeProduct + ', '
								From (
								Select CodeProduct, Sum(Quantity) As Quantity, HCDEVMEDDId From @TableDetail Group By CodeProduct, HCDEVMEDDId
							) td 
							Inner Join dbo.HCDEVMEDD hcd With(Nolock) on hcd.CODCONCEC = @ConsecutiveCrystal And hcd.CODPRODUC = td.CodeProduct and td.	HCDEVMEDDId = hcd.id			
							WHERE td.Quantity > CANPENDIE For Xml Path(''))

						SELECT	@CodeMessage = '999', 
								@Message = 'No se puede hacer la devolución porque los siguientes productos superan las cantidades pendientes por devolver: ' + @StringHCDEVMEDD, 
								@DevolutionId = 0, 
								@StatusResult = 3
						RETURN
					END	
				END

				/***/

				Declare @tmpProductIds Table(RowId Int Identity(1,1) Primary Key, ProductId Int Null, PharmaceuticalDispensingDetailBatchSerialId Int Null)
				Delete From @tmpProductIds
				Insert Into @tmpProductIds
				Select pdd.ProductId, pddb.Id
				from Inventory.PharmaceuticalDispensingDetailBatchSerial pddb With(Nolock)
				inner join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) on pdd.Id = pddb.PharmaceuticalDispensingDetailId 
				Inner Join @TableDetail td On td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id
				where td.Quantity > @Cero

				CREATE table #tmpBatchSerialProduct --Table
				(
					RowId Int Identity(1,1) Primary Key,
					ServiceOrderId Int Null, 
					ServiceOrderDetailId Int Null, 
					DistributionQuantity Int Null, 
					FunctionalUnitCode Varchar(20) Null,
					PharmaceuticalDispensingDetailBatchSerialId Int Null
				)
				Delete From #tmpBatchSerialProduct

				Insert Into #tmpBatchSerialProduct
				Select so.Id, sod.Id, sodd.Quantity, fu.Code, tmpProduct.PharmaceuticalDispensingDetailBatchSerialId
				From @tmpProductIds tmpProduct
				join Inventory.PharmaceuticalDispensingDetailBatchSerial pddb WITH(NOLOCK) on tmpProduct.PharmaceuticalDispensingDetailBatchSerialId=pddb.Id
				join Inventory.PharmaceuticalDispensingDetail pdd WITH(NOLOCK) on pdd.Id= pddb.PharmaceuticalDispensingDetailId
				join Inventory.PharmaceuticalDispensing pd WITH(NOLOCK) on pd.Id= pdd.PharmaceuticalDispensingId
				Inner Join Billing.ServiceOrderDetail sod With(Nolock) on sod.ProductId = tmpProduct.ProductId  
				Inner Join Payroll.FunctionalUnit fu With(Nolock) on fu.Id = sod.[PerformsFunctionalUnitId]
				Inner Join Billing.ServiceOrder so With(Nolock) on so.Id = sod.ServiceOrderId and so.AdmissionNumber = @AdmissionNumber
				and so.EntityName ='PharmaceuticalDispensing' --and so.EntityId = pd.Id
				Inner Join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on sodd.ServiceOrderDetailId = sod.Id and sodd.Quantity > @Cero and sodd.DistributionType In (1,4)
				Inner Join Billing.RevenueControlDetail rcd on rcd.Id = sodd.RevenueControlDetailId And rcd.[Status] = @Uno
				INNER JOIN Billing.RevenueControl rc ON rc.Id = rcd.RevenueControlId AND rc.AdmissionNumber = so.AdmissionNumber
				Order By so.Id, sod.Id, sodd.Quantity

				/***/
				IF EXISTS(
							SELECT 1
							FROM @TableDetail pddevd
							JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs ON pddevd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
							JOIN Inventory.PharmaceuticalDispensingDetail pdd ON pddbs.PharmaceuticalDispensingDetailId = pdd.Id
							JOIN Inventory.Warehouse w WITH(NOLOCK) ON w.Id =pdd.WarehouseId
							WHERE w.CustodyStore <> 1) 
				BEGIN
				
				/*****aqui******/
				---- Realizamos un recorrido para seleccionar los productos que vamos a devolver de los folios que esten activos y los productos que no esten distribuidos
				Declare @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp int
					, @product_cursorQuantityTmp int
					, @ProductTypeClass Tinyint

				Declare @RowId Int = 1, @Rows Int = 1
				While @Rows > 0
				begin

					Select Top 1 @RowId = td.RowId
						, @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp = td.PharmaceuticalDispensingDetailBatchSerialId
						, @product_cursorQuantityTmp = td.Quantity 
						, @ProductTypeClass = pt.Class
					From @TableDetail td
					Join Inventory.InventoryProduct pr with(nolock) on td.ProductId = pr.Id
					Join Inventory.ProductType pt with(nolock) on pr.ProductTypeId = pt.Id
					Where td.Quantity > @Cero And td.RowId >= @RowId 
					Order By td.RowId

					--Select Top 1 @RowId = RowId, @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp = PharmaceuticalDispensingDetailBatchSerialId
					--	, @product_cursorQuantityTmp = Quantity 
					--From @TableDetail Where Quantity > @Cero And RowId >= @RowId Order By RowId

					Set @Rows = @@RowCount
					If @Rows = 0 
						Break

					If @ProductTypeClass = 5 begin
						Set @RowId += 1
						continue
					End

					declare @IdServiceOrder_TmpQuantity int,
						@IdServiceOrderDetail_TmpQuantity int

					declare @ProductId_TmpQuantity int = (Select Top 1 ProductId 
						From @tmpProductIds
						where PharmaceuticalDispensingDetailBatchSerialId = @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp 
					Order By PharmaceuticalDispensingDetailBatchSerialId)

					--New
					--Copiamos los datos a recorrer en una tabla temporal
					Declare @tmpServiceOrderPharmaceutical Table(
						RowId Int Identity(1, 1) Primary Key,						
						ServiceOrderId Int,
						ServiceOrderDetailId Int,
						ServiceOrderDetailDistributionQuantity Int,
						FunctionalUnitCode Varchar(20) Not Null
					)

					Delete From @tmpServiceOrderPharmaceutical
					Insert Into @tmpServiceOrderPharmaceutical
					Select ServiceOrderId, ServiceOrderDetailId, DistributionQuantity, FunctionalUnitCode 
					From #tmpBatchSerialProduct
					Where PharmaceuticalDispensingDetailBatchSerialId = @product_cursorPharmaceuticalDispensingDetailBatchSerialIdTmp

					Declare @Quantity_TmpQuantity int
					Declare @QuantityReal_TmpQuantity int
					
					While @product_cursorQuantityTmp > 0
					Begin

						Select Top 1 
							@IdServiceOrder_TmpQuantity = ServiceOrderId, 
							@IdServiceOrderDetail_TmpQuantity = ServiceOrderDetailId, 
							@Quantity_TmpQuantity = tmp.ServiceOrderDetailDistributionQuantity - ISNULL(tso.Quantity, 0)
						From @tmpServiceOrderPharmaceutical tmp
						LEFT JOIN
						(
                            SELECT IdServiceOrderDetail, SUM(Quantity) Quantity
                            FROM @TableServiceOrderTmp
							GROUP BY IdServiceOrderDetail
                        ) tso ON tmp.ServiceOrderDetailId = tso.IdServiceOrderDetail
                        WHERE tmp.ServiceOrderDetailDistributionQuantity - ISNULL(tso.Quantity, 0) > 0
						Order By Case tmp.FunctionalUnitCode When @FunctionUnitCodeWithTrim then 1 else 2 end, tmp.FunctionalUnitCode
					
						Set @QuantityReal_TmpQuantity = 0

						If @IdServiceOrder_TmpQuantity Is Not Null Begin
							If @Quantity_TmpQuantity >= @product_cursorQuantityTmp Begin
								Set @QuantityReal_TmpQuantity = @product_cursorQuantityTmp
								Set @product_cursorQuantityTmp = 0
							End
							Else Begin
								Set @QuantityReal_TmpQuantity = @Quantity_TmpQuantity
								Set @product_cursorQuantityTmp -= @Quantity_TmpQuantity
							End
							Insert Into @TableServiceOrderTmp
							Values(@IdServiceOrder_TmpQuantity, @IdServiceOrderDetail_TmpQuantity, @ProductId_TmpQuantity, @QuantityReal_TmpQuantity)							
						end
						else begin
							SELECT	@CodeMessage = '999', 
									@Message = 'No se puede hacer la devolución porque el siguiente Producto no tiene cantidades en Facturacion para devolver: ' + (select Code + ' - ' + [Name] from Inventory.InventoryProduct With(Nolock) where Id = @ProductId_TmpQuantity), 
									@DevolutionId = 0, 
									@StatusResult = 3
							Return
						end
					End
					Set @RowId += 1
				End

				--- Valido que en las ordenes de servicio que tengo en tmp hayan cantidades para devolver
				Declare @StringItemsOrder Varchar(Max) = (Select ip.Code + ', ' 
					From (
						Select so.IdServiceOrder, so.IdProduct, Sum(sod.InvoicedQuantity) As InvoicedQuantity 
						From @TableServiceOrderTmp so 
						Inner Join Billing.ServiceOrderDetail sod With(Nolock) On so.IdServiceOrder = sod.ServiceOrderId And sod.ProductId = so.IdProduct  
						Inner Join Billing.ServiceOrderDetailDistribution sodd With(Nolock) On sodd.ServiceOrderDetailId = sod.Id And sodd.DistributionType In (1, 4) 
						Inner Join Billing.RevenueControlDetail rcd With(Nolock) On rcd.Id = sodd.RevenueControlDetailId And rcd.[Status] = @uno 
						Group By so.IdServiceOrder, so.IdProduct
					) As datos 
					Inner Join @TableServiceOrderTmp tstmp On datos.IdServiceOrder = tstmp.IdServiceOrder and datos.IdProduct = tstmp.IdProduct 
					Inner Join Inventory.InventoryProduct ip With(Nolock) On ip.Id = tstmp.IdProduct  
					Where datos.InvoicedQuantity < tstmp.Quantity For Xml Path(''))					
				If @StringItemsOrder Is Not Null begin
					SELECT	@CodeMessage = '999', 
							@Message = 'No se puede hacer la devolución porque los siguientes productos no tienen cantidad suficiente para devolver en facturacion: ' + @StringItemsOrder, 
							@DevolutionId = 0, 
							@StatusResult = 3
					return
				end

				--- Validacion para que No permita devolver productos divididos en una cuenta madre
				set @StringItemsOrder = null -- se limpia la variable que almacena el mensaje
				SET @StringItemsOrder = (Select ip.Code + ', ' 
											From (
												Select so.IdServiceOrder, so.IdProduct, Sum(sod.InvoicedQuantity) As InvoicedQuantity 
												From @TableServiceOrderTmp so 
												Inner Join Billing.ServiceOrderDetail sod With(Nolock) On so.IdServiceOrder = sod.ServiceOrderId And sod.ProductId = so.IdProduct  
												Inner Join Billing.ServiceOrderDetailDistribution sodd With(Nolock) On sodd.ServiceOrderDetailId = sod.Id And sodd.DistributionType In (1, 4) 
												Inner Join Billing.RevenueControlDetail rcd With(Nolock) On rcd.Id = sodd.RevenueControlDetailId And rcd.[Status] = @uno  and rcd.RevenueControlDetailMasterId is not null
												Group By so.IdServiceOrder, so.IdProduct
											) As datos 
											Inner Join @TableServiceOrderTmp tstmp On datos.IdServiceOrder = tstmp.IdServiceOrder and datos.IdProduct = tstmp.IdProduct 
											Inner Join Inventory.InventoryProduct ip With(Nolock) On ip.Id = tstmp.IdProduct 
											For Xml Path(''))					
				If @StringItemsOrder Is Not Null begin
					SELECT	@CodeMessage = '999', 
							@Message = 'No se puede hacer la devolución porque los siguientes productos estan en folios provenientes de una cuenta madre: ' + @StringItemsOrder, 
							@DevolutionId = 0, 
							@StatusResult = 3
					return
				end

				Declare @IdServiceOrderTmp int, @IdProductTmp int, @QuantityTmp int

				Set @RowId = 1
				Set @Rows = 1

				While @Rows > 0
				Begin
					Select Top 1 @RowId = RowId, 
						@IdServiceOrderTmp = IdServiceOrder,
						@IdProductTmp = IdProduct, 
						@QuantityTmp = Quantity 
					From @TableServiceOrderTmp
					Where RowId >= @RowId 
					Order By RowId

					Set @Rows = @@RowCount
					If @Rows = 0 
						Break
						
					--Se valida que los items a devolver no esten distribuidos
					if Not Exists (Select sod.Id 
						From Billing.ServiceOrderDetail sod With(Nolock) 
						Inner Join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on sodd.ServiceOrderDetailId = sod.Id and sodd.DistributionType in (1,4) 
						Inner Join Billing.RevenueControlDetail rcd With(Nolock) on rcd.Id = sodd.RevenueControlDetailId and rcd.[Status] = @uno
						where sod.ServiceOrderId = @IdServiceOrderTmp and sod.ProductId = @IdProductTmp) begin
						SELECT	@CodeMessage = '999', 
								@Message = 'No se encontraron items para hacer la devolución (los items distribuidos no se tienen en cuenta para devolver) Producto: ' + (select Concat(Code, ' - ', [Name]) from Inventory.InventoryProduct With(Nolock) where Id = @IdProductTmp), 
								@DevolutionId = 0, 
								@StatusResult = 3
						return
					end

					Declare @IdServiceOrderDetailTmp Int, 
						@InvoicedQuantityTmp Int, 
						@IdServiceOrderDetailDistributionTmp Int
					Declare @tmpServiceOrderDetail Table(
						RowId Int Identity(1,1) Primary Key Clustered
						, ServiceOrderDetailId Int
						, InvoicedQuantity Int
						, ServiceOrderDetailDistributionId Int Null)

					Delete From @tmpServiceOrderDetail

					Insert Into @tmpServiceOrderDetail
					select sod.Id, sod.InvoicedQuantity, sodd.Id
					from Billing.ServiceOrderDetail sod With(Nolock) 
					inner join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on sodd.ServiceOrderDetailId = sod.Id and sodd.DistributionType in (1,4) 
					inner join Billing.RevenueControlDetail rcd With(Nolock) on rcd.Id = sodd.RevenueControlDetailId and rcd.[Status] = @uno
					where sod.ServiceOrderId = @IdServiceOrderTmp and sod.ProductId = @IdProductTmp

					Declare @__Rows Int, @__RowId Int
					Set @__RowId = 1
					Set @__Rows = 1

					While @__Rows > 0
					Begin
						Select Top 1 @__RowId = RowId
							, @IdServiceOrderDetailTmp = ServiceOrderDetailId
							, @InvoicedQuantityTmp = InvoicedQuantity
							, @IdServiceOrderDetailDistributionTmp = ServiceOrderDetailDistributionId 
						From @tmpServiceOrderDetail Where RowId >= @__RowId Order By RowId

						Set @__Rows = @@RowCount
						If @__Rows = 0 
							Break

						Insert Into @TableServiceOrderDetailDistributionId(Id) values(@IdServiceOrderDetailDistributionTmp)
						--=======se obtiene el id del folio (para Actualizacion Cabecera Folio)=============
						declare @RevenueControlDetailId INT =(select sodd.RevenueControlDetailId 
																	from Billing.ServiceOrderDetailDistribution sodd 
																	where sodd.Id = @IdServiceOrderDetailDistributionTmp)
						--==============================================
						if @InvoicedQuantityTmp > @QuantityTmp begin
							Declare @msgValidacion Varchar(Max) = (Select Concat(ip.Code, ' - ', ip.[Name], ', ') From Billing.ServiceOrderDetail sod With(Nolock)
							Inner Join Inventory.InventoryProduct ip With(Nolock) On sod.ProductId = ip.Id
							Where sod.Id = @IdServiceOrderDetailTmp And sod.InvoicedQuantity - @QuantityTmp < 0
							For Xml Path(''))

							If @msgValidacion Is Not Null Begin								
									SELECT	@CodeMessage = '999', 
											@Message = 'El valor de la devolución es mayor a la existente para el producto: ' + @msgValidacion, 
											@DevolutionId = 0, 
											@StatusResult = 3
									Return
							End

							Update Billing.ServiceOrderDetail set InvoicedQuantity -= @QuantityTmp, DevolutionQuantity += @QuantityTmp, 
								GrandTotalSalesPrice = TotalSalesPrice * (InvoicedQuantity - @QuantityTmp)
								where Id = @IdServiceOrderDetailTmp
							Update Billing.ServiceOrderDetailDistribution set Quantity -= @QuantityTmp, GrandTotalSalesPrice = sod.GrandTotalSalesPrice, 
								ThirdPartySalesPrice = sod.GrandTotalSalesPrice, ThirdPartyPercentage = 100, ApplyRecoveryFee = 1, RecoveryFeeType = 2, 
								SubTotalPatientSalesPrice = 0, PatientPercentage = 0
							from Billing.ServiceOrderDetailDistribution sodd
							inner join Billing.ServiceOrderDetail sod on sod.Id = sodd.ServiceOrderDetailId
							where sodd.Id = @IdServiceOrderDetailDistributionTmp

							Update @tmpServiceOrderDetail Set InvoicedQuantity -= @QuantityTmp Where ServiceOrderDetailId = @IdServiceOrderDetailTmp

							Break
						end
						else begin
							update Billing.ServiceOrderDetail set DevolutionQuantity += InvoicedQuantity, @QuantityTmp -= InvoicedQuantity, 
								InvoicedQuantity = 0, GrandTotalSalesPrice = 0 where Id = @IdServiceOrderDetailTmp
							delete from Billing.ServiceOrderDetailDistribution where Id = @IdServiceOrderDetailDistributionTmp
							
							Update @tmpServiceOrderDetail Set InvoicedQuantity = 0 Where ServiceOrderDetailId = @IdServiceOrderDetailTmp	
						end

							-- Se actualiza la cabecera del folio
							UPDATE rcd SET rcd.TotalFolio = sodd.GrandTotalSalesPrice
							from Billing.RevenueControlDetail rcd WITH(NOLOCK)
							JOIN (select  sodd.RevenueControlDetailId, sum(sodd.GrandTotalSalesPrice) GrandTotalSalesPrice
									from Billing.ServiceOrderDetailDistribution sodd WITH(NOLOCK)
									GROUP by sodd.RevenueControlDetailId) sodd on sodd.RevenueControlDetailId = rcd.Id
							where rcd.Id = @RevenueControlDetailId

						Set @__RowId += 1
					End

					Set @RowId += 1
				End
			END
				/****hasta aca*****/
				--- Disminuto la cantidad disponoble para devolver en la dispensacion
				-- FIX BUG-39XXX (12/08/2026): se agrega @TableDetail por PharmaceuticalDispensingDetailBatchSerialId
				-- antes de comparar contra OutstandingQuantity. Si el mismo lote aparece repartido en varias filas
				-- de @TableDetail (mismo producto con varias solicitudes -HCDEVMEDD- pendientes en un mismo envio),
				-- la version anterior comparaba cada fila por separado y no detectaba que, sumadas, superaban el
				-- saldo real del lote -dejando pasar la operacion hasta reventar mas abajo con el CHECK constraint
				-- en vez de mostrar este mensaje funcional-.
				Declare @ProductNegative Varchar(max) = (select distinct Case When atc.Id Is not Null Then Concat(atc.Code COLLATE SQL_Latin1_General_CP1_CI_AS, ' - ', atc.[Name], ', ') when ins.Id is not null then CONCAT(ins.Code COLLATE SQL_Latin1_General_CP1_CI_AS, ' - ', ins.SupplieName) Else Concat(ip.Code COLLATE SQL_Latin1_General_CP1_CI_AS, ' - ', ip.[Name]) End
					From
					(
						Select PharmaceuticalDispensingDetailBatchSerialId, SUM(Quantity) As Quantity, MAX(CodeProduct) As CodeProduct
						From @TableDetail
						Group By PharmaceuticalDispensingDetailBatchSerialId
					) td
					Inner Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddb With(Nolock) On td.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id
					Left Join Inventory.ATC atc With(Nolock) On atc.Code = Ltrim(Rtrim(td.CodeProduct))
					Left Join Inventory.InventorySupplie ins With(Nolock) On ins.Code = Ltrim(Rtrim(td.CodeProduct))
					Left Join Inventory.InventoryProduct ip With(Nolock) On ip.Code = Ltrim(Rtrim(td.CodeProduct))
					Where (pddb.OutstandingQuantity - td.Quantity) < 0
					For Xml Path(''))

				If @ProductNegative Is Not Null Begin
					SELECT	@CodeMessage = '999', 
							@Message = 'Los siguientes productos ya se han devuelto: ' + @ProductNegative, 
							@DevolutionId = 0, 
							@StatusResult = 3
					Return
				End

				IF @IsDashBoard = 1
				BEGIN
					IF ISNULL(@EntityName, '') = 'PharmaceuticalDispensingTransfer'
					BEGIN
						IF EXISTS
						(
							SELECT 1
							FROM 
							(
								SELECT ISNULL(FunctionUnitCode, @FunctionUnitCode) FunctionUnitCode, CodeProduct, sum(Quantity) as Quantity 
								FROM @TableDetail 
								WHERE IDHCHOJAGASTOQX IS NULL
								GROUP BY ISNULL(FunctionUnitCode, @FunctionUnitCode), CodeProduct
							) d
							LEFT JOIN dbo.HCFISIPRO phy ON phy.NUMINGRES = @AdmissionNumber AND phy.CODCENATE = @CareCenterCode AND phy.UFUCODIGO = d.FunctionUnitCode and phy.CODPRODUC = d.CodeProduct
							WHERE d.Quantity > ISNULL(phy.CANACTPRO, 0)
						)
						BEGIN
							DECLARE @PhysicalErrors varchar(max) = (Select d.CodeProduct + ', '
								FROM 
								(
									SELECT ISNULL(FunctionUnitCode, @FunctionUnitCode) FunctionUnitCode, CodeProduct, sum(Quantity) as Quantity 
									FROM @TableDetail 
									WHERE IDHCHOJAGASTOQX IS NULL
									GROUP BY ISNULL(FunctionUnitCode, @FunctionUnitCode), CodeProduct
								) d
								LEFT JOIN dbo.HCFISIPRO phy ON phy.NUMINGRES = @AdmissionNumber AND phy.CODCENATE = @CareCenterCode AND phy.UFUCODIGO = d.FunctionUnitCode and phy.CODPRODUC = d.CodeProduct
								WHERE d.Quantity > ISNULL(phy.CANACTPRO, 0) For Xml Path(''))
							SELECT	@CodeMessage = '999', 
									@Message = 'No se puede hacer la devolución porque los siguientes productos no tienen suficiente cantidad en inventario físico del paciente: ' + @PhysicalErrors, 
									@DevolutionId = 0, 
									@StatusResult = 3
							RETURN
						END

						IF EXISTS
						(
							SELECT 1
							FROM 
							(
								SELECT IDHCHOJAGASTOQX, ProductId, sum(Quantity) as Quantity 
								FROM @TableDetail d
								WHERE d.IDHCHOJAGASTOQX IS NOT NULL
								GROUP BY d.IDHCHOJAGASTOQX, ProductId
							) d
							LEFT JOIN dbo.HCHOJAGASTOQXD hgqxd ON d.IDHCHOJAGASTOQX = hgqxd.IDHCHOJAGASTOQX AND d.ProductId = hgqxd.IDPRODUCTO
							WHERE d.Quantity > ISNULL(hgqxd.CANTIDADENTREGADA - hgqxd.CANTIDADGASTADA - hgqxd.CANTIDADACEPTADADEV, 0)
						)
						BEGIN
							DECLARE @PhysicalPackagesErrors varchar(max) 
							
							SELECT @PhysicalPackagesErrors = STUFF((
								Select DISTINCT CHAR(13) + CHAR(10) + CONCAT(d.CodeProduct, ': Cantidad Disponible ', ISNULL(hgqxd.CANTIDADENTREGADA - hgqxd.CANTIDADGASTADA - hgqxd.CANTIDADACEPTADADEV, 0), ', Cantidad a Devolver ', d.Quantity)
								FROM 
								(
									SELECT IDHCHOJAGASTOQX, ProductId, CodeProduct, sum(Quantity) as Quantity 
									FROM @TableDetail d
									WHERE d.IDHCHOJAGASTOQX IS NOT NULL
									GROUP BY d.IDHCHOJAGASTOQX, ProductId, CodeProduct
								) d
								LEFT JOIN dbo.HCHOJAGASTOQXD hgqxd ON d.IDHCHOJAGASTOQX = hgqxd.IDHCHOJAGASTOQX AND d.ProductId = hgqxd.IDPRODUCTO
								WHERE d.Quantity > ISNULL(hgqxd.CANTIDADENTREGADA - hgqxd.CANTIDADGASTADA - hgqxd.CANTIDADACEPTADADEV, 0) 
								FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

							SELECT	@CodeMessage = '999', 
									@Message = 'No se puede hacer la devolución porque los siguientes productos no tienen suficiente cantidad en inventario físico por paquetes del paciente: ' + CHAR(13) + CHAR(10) + @PhysicalPackagesErrors, 
									@DevolutionId = 0, 
									@StatusResult = 3
							RETURN
						END
					END
				END
				
				-- FIX BUG-39XXX (12/08/2026): @TableDetail se pre-agrega por PharmaceuticalDispensingDetailBatchSerialId
				-- antes de actualizar. Un UPDATE...FROM con mas de una fila de @TableDetail apuntando al mismo
				-- destino (mismo lote/detalle) tiene comportamiento no garantizado en SQL Server: puede aplicar
				-- el incremento/decremento una sola vez o varias, segun el plan de ejecucion. Esto produjo
				-- desincronizacion real entre OutstandingQuantity (tabla hija) y ReturnedQuantity (tabla padre),
				-- y el disparo intermitente de CK_PharmaceuticalDispensingDetail para distintos pacientes.
				-- NOTA: no se envuelve en transaccion local (el llamador ya puede tener una transaccion ambiente
				-- abierta -- @@TRANCOUNT=1 al entrar -- y un ROLLBACK aqui revertiria tambien la del llamador).
				DECLARE @AggBatchSerial TABLE (PharmaceuticalDispensingDetailBatchSerialId INT PRIMARY KEY, TotalQuantity INT)
				INSERT INTO @AggBatchSerial (PharmaceuticalDispensingDetailBatchSerialId, TotalQuantity)
				SELECT td.PharmaceuticalDispensingDetailBatchSerialId, SUM(td.Quantity)
				FROM @TableDetail td
				GROUP BY td.PharmaceuticalDispensingDetailBatchSerialId

				update pddb set pddb.OutstandingQuantity -= agg.TotalQuantity
					from Inventory.PharmaceuticalDispensingDetailBatchSerial pddb
					inner join @AggBatchSerial agg on agg.PharmaceuticalDispensingDetailBatchSerialId = pddb.Id

				--- Aumento la cantidad devuelta o retornada en la dispensacion
				update pdd set pdd.ReturnedQuantity += aggDetail.TotalQuantity
					from Inventory.PharmaceuticalDispensingDetail pdd
					inner join
					(
						select pddb.PharmaceuticalDispensingDetailId, SUM(agg.TotalQuantity) AS TotalQuantity
						from @AggBatchSerial agg
						inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddb on pddb.Id = agg.PharmaceuticalDispensingDetailBatchSerialId
						group by pddb.PharmaceuticalDispensingDetailId
					) aggDetail on aggDetail.PharmaceuticalDispensingDetailId = pdd.Id

				Declare @PatientThirdPartyId int = (Select Id From Common.ThirdParty With(Nolock) Where Nit = @CodePacient)
				if @PatientThirdPartyId Is Null begin
					SELECT	@CodeMessage = '999', 
							@Message = 'El paciente '+ @CodePacient +' no existe como Tercero en Indigo Vie', 
							@DevolutionId = 0, 
							@StatusResult = 3
					return
				end	
				
				---Recalculo los folios que estan activos
				declare @IdRevenueControlDetailTmp int
				declare @TableMessageUpdateRevenue table(StatusResult bit, MessageResult varchar(max))
				Set @Rows = 1
				Set @RowId = 1

				Declare @tmpServiceOrderDetailDistribution Table(RowId Int Identity(1,1) Primary Key Clustered, Id Int, RevenueControlDetailId Int)
				Delete From @tmpServiceOrderDetailDistribution
				Insert Into @tmpServiceOrderDetailDistribution
				select distinct sodd.Id, sodd.RevenueControlDetailId 
				From @TableServiceOrderDetailDistributionId tdd 
				Inner Join Billing.ServiceOrderDetailDistribution sodd With(Nolock) on sodd.Id = tdd.id

				While @Rows > 0
				begin
					Select Top 1 @RowId = RowId, @IdRevenueControlDetailTmp = RevenueControlDetailId From @tmpServiceOrderDetailDistribution Where RowId >= @RowId Order By RowId
					Set @Rows = @@ROWCOUNT
					If @Rows = 0 
						Break

					Insert Into @TableMessageUpdateRevenue
					exec [Billing].[SP_UpdateRevenueControlDetailValues] @IdRevenueControlDetailTmp, @OperatingUnitId
		
					Set @RowId += 1
				End

				set @MessageReturn = 'Se confirmo correctamente la Devolucion ' + @CodeDevolution
				
				-- Productos que afecten inventario
				IF EXISTS
				(
					SELECT 1
					From @TableDetail As td 
					Inner Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs With(Nolock) on pddbs.Id = td.PharmaceuticalDispensingDetailBatchSerialId 
					Inner Join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) on pdd.Id = pddbs.PharmaceuticalDispensingDetailId
					Inner join Inventory.PharmaceuticalDispensing pd with(nolock) ON pdd.PharmaceuticalDispensingId = pd.Id
					Inner join Inventory.Warehouse w with(nolock) ON pdd.WarehouseId = w.Id					
					WHERE w.VirtualStore = @Cero AND pd.AffectInventory = @Uno
				)
				BEGIN
					--- Ahora aumento en el inventario fisico (con el mismo valor con el cual se dispenso)
					declare @KardexXml xml = (
						Select @PatientThirdPartyId As ThirdPartyId, 
							ip.Id As ProductId, 
							ph.BatchSerialId, 
							1 As MovementType, 
							@WarehouseId As WarehouseId, 
							Sum(td.Quantity) As Quantity, 
							pdd.AverageCost As Value, 
							1 As AffectAverageCost 
						From @TableDetail As td 
						Inner Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs With(Nolock) on pddbs.Id = td.PharmaceuticalDispensingDetailBatchSerialId 
						Inner Join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) on pdd.Id = pddbs.PharmaceuticalDispensingDetailId
						Inner join Inventory.PharmaceuticalDispensing pd with(nolock) ON pdd.PharmaceuticalDispensingId = pd.Id
						Inner join Inventory.Warehouse w with(nolock) ON pdd.WarehouseId = w.Id
						Inner Join Inventory.PhysicalInventory ph With(Nolock) on ph.Id = pddbs.PhysicalInventoryId
						Inner Join Inventory.InventoryProduct ip With(Nolock) on ip.Id = pdd.ProductId
						WHERE w.VirtualStore = @Cero AND pd.AffectInventory = @Uno and pddbs.PhysicalInventoryCustodyId is NULL
						Group By ip.Id, ph.BatchSerialId, pdd.AverageCost
						For Xml Path('Kardex'), Elements
					)

					IF @KardexXml IS NOT NULL
					BEGIN
					    declare @TableResultKardex table(CodeMessage varchar(20), Message varchar(1000), [Status] tinyint)
						insert into @TableResultKardex
						exec [Inventory].[SP_SavePhysicalInventoryKardex] @KardexXml, @IdDevolution, @CodeDevolution, 'PharmaceuticalDispensingDevolution', @User
						if Exists (select CodeMessage from @TableResultKardex where [Status] = @tres) begin
							SELECT	@CodeMessage = CodeMessage, 
									@Message = Message, 
									@DevolutionId = 0, 
									@StatusResult = Status
							from @TableResultKardex
							return
						end
					END

					declare @KardexXmlCustody xml = (
						Select @PatientThirdPartyId As ThirdPartyId, 
							ip.Id As ProductId, 
							ph.BatchSerialId, 
							1 As MovementType, 
							@WarehouseId As WarehouseId, 
							Sum(td.Quantity) As Quantity, 
							pdd.AverageCost As Value, 
							1 As AffectAverageCost 
						From @TableDetail As td 
						Inner Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs With(Nolock) on pddbs.Id = td.PharmaceuticalDispensingDetailBatchSerialId 
						Inner Join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) on pdd.Id = pddbs.PharmaceuticalDispensingDetailId
						Inner join Inventory.PharmaceuticalDispensing pd with(nolock) ON pdd.PharmaceuticalDispensingId = pd.Id
						Inner join Inventory.Warehouse w with(nolock) ON pdd.WarehouseId = w.Id
						Inner Join Inventory.PhysicalInventoryCustody ph With(Nolock) on ph.Id = pddbs.PhysicalInventoryCustodyId
						Inner Join Inventory.InventoryProduct ip With(Nolock) on ip.Id = pdd.ProductId
						WHERE w.VirtualStore = @Cero AND pd.AffectInventory = @Uno and pddbs.PhysicalInventoryCustodyId is not NULL
						Group By ip.Id, ph.BatchSerialId, pdd.AverageCost
						For Xml Path('Kardex'), Elements
					)

					IF @KardexXmlCustody IS NOT NULL
					BEGIN
						declare @RevenueControlId as INT
						SELECT top 1 @RevenueControlId = RevenueControl.Id FROM Billing.RevenueControl WHERE AdmissionNumber = @AdmissionNumber and PatientCode = @CodePacient
						declare @TableResultKardexCustody table(CodeMessage varchar(20), Message varchar(1000), [Status] tinyint)
						insert into @TableResultKardexCustody
						exec [Inventory].[SP_SavePhysicalInventoryCustodyKardexCustody] @KardexXmlCustody, @RevenueControlId, @IdDevolution, @CodeDevolution, 'PharmaceuticalDispensingDevolution', @User
						if Exists (select CodeMessage from @TableResultKardexCustody where [Status] = @tres) begin
							SELECT	@CodeMessage = CodeMessage, 
									@Message = Message, 
									@DevolutionId = 0, 
									@StatusResult = Status
							from @TableResultKardexCustody
							return
						end
					END

					--- Actualizo las remisiones de inventario en consignación si las hubiera
					declare @RemissionXml xml = (
						Select td.Id As EntityDetailId, 
							@OperatingUnitId As OperatingUnitId, 
							pdd.FunctionalUnitId, 
							pdd.ProductId As ProductId, 
							ph.BatchSerialId, 
							1 As MovementType, 
							pdd.WarehouseId As WarehouseId, 
							td.Quantity, 
							pdd.AverageCost As Value, 
							pdd.PharmaceuticalDispensingId As OriginId
						From Inventory.PharmaceuticalDispensingDevolutionDetail As td With(Nolock)
						Inner Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs With(Nolock) On pddbs.Id = td.PharmaceuticalDispensingDetailBatchSerialId 
						Inner Join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) On pdd.Id = pddbs.PharmaceuticalDispensingDetailId
						Inner join Inventory.PharmaceuticalDispensing pd with(nolock) ON pdd.PharmaceuticalDispensingId = pd.Id
						Inner Join Inventory.PhysicalInventory ph With(Nolock) On ph.Id = pddbs.PhysicalInventoryId
						Inner Join Inventory.InventoryProduct ip With(Nolock) On ip.Id = pdd.ProductId
						Inner Join Inventory.Warehouse w With(Nolock) On pdd.WarehouseId = w.Id						
						Where w.WarehouseConsignment = @Uno AND pd.AffectInventory = @Uno And td.PharmaceuticalDispensingDevolutionId = @IdDevolution
						For Xml Path('Remission'), Elements
					)
				
					IF @RemissionXml Is Not Null BEGIN
						DECLARE @MessageReturnRemission AS VARCHAR(MAX)
						EXEC [Inventory].[SP_UpdateTheQuantityProductUsedInConsignmentInventoryRemission] @RemissionXml, @IdDevolution, 
							@CodeDevolution, 'PharmaceuticalDispensingDevolution', @User, @MessageReturnRemission OUTPUT
						IF (@MessageReturnRemission IS NOT NULL AND @MessageReturnRemission <> '') BEGIN
							SELECT	@CodeMessage = '999', 
									@Message = @MessageReturnRemission, 
									@DevolutionId = 0, 
									@StatusResult = 3
							return
						END
					END

					IF EXISTS
					(
						SELECT 1 
						FROM @TableDetail td 
						Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs With(Nolock) on td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id 
						Join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) on pddbs.PharmaceuticalDispensingDetailId = pdd.Id
						Join Inventory.Warehouse w With(Nolock) on pdd.WarehouseId = w.Id
						WHERE pddbs.PhysicalInventoryCustodyId IS NULL AND w.ControlStore = @Cero
					)
					BEGIN
						/******* CREAMOS EL XML PARA GENERAR EL COMPROBANTE CONTABLE *******/
						declare @TableJournalVoucher table(Id int, Consecutive bigint, LegalBookId int, 
							AccountingMovementId int, IdJournalVoucher int, VoucherDate DATETIME, 
							Imported bit, Status tinyint, Detail varchar(500), EntityCode varchar(20), 
							EntityId int, EntityName varchar(250), IsClosedYear bit)

						Declare @IdLegalBook Int = (Select Id From GeneralLedger.LegalBook With(Nolock) where OfficialBook = @Uno)
						if @IdLegalBook Is Null begin
							SELECT	@CodeMessage = '999', 
									@Message = 'No existe un libro oficial en el modulo de Contabilidad', 
									@DevolutionId = 0, 
									@StatusResult = 3
							return
						end				
				
						---Inserto la cabecera del comprobante contable
						Insert Into @TableJournalVoucher (Id, Consecutive, LegalBookId, AccountingMovementId, IdJournalVoucher, VoucherDate, Imported, [Status], Detail, EntityCode, EntityId, EntityName, IsClosedYear)
						values(0,0,@IdLegalBook, 0, @IdJournalVoucherType, @DocumentDate, 0, 2, 'Devolucion de Dispensación ' + @CodeDevolution, @CodeDevolution, @IdDevolution, 'PharmaceuticalDispensingDevolution', 0)
						--- Valido que los productos de los detalles tengan grupo

						Declare @StringProductNoGroup Varchar(Max) = (select ip.Code + ', ' from @TableDetail td 
							Inner Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs With(Nolock) on td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id 
							Inner Join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) on pdd.Id = pddbs.PharmaceuticalDispensingDetailId 
							Inner Join Inventory.InventoryProduct ip With(Nolock) on ip.Id = pdd.ProductId 
							Where ProductGroupId Is Null and pddbs.PhysicalInventoryCustodyId IS NULL For Xml Path(''))
						if @StringProductNoGroup Is Not Null begin
							SELECT	@CodeMessage = '999', 
									@Message = 'No se puede realizar la devolución debido a que los siguientes productos no poseen grupos: ' + @StringProductNoGroup, 
									@DevolutionId = 0, 
									@StatusResult = 3
							return
						end

						---- Ahora valido que la unidad funcional exista en los parametros para poder sacar la cuenta del costo
						Declare @StringFunctional varchar(max) = (
							SELECT DISTINCT f.Code + ' ' + f.[Name] + ', ' 
							FROM @TableDetail td 
							JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs With(Nolock) on td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id 
							JOIN Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) on pdd.Id = pddbs.PharmaceuticalDispensingDetailId 
							JOIN Payroll.FunctionalUnit f With(Nolock) on f.Id = pdd.FunctionalUnitId 
							JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ip.Id = pdd.ProductId
							JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
							LEFT JOIN Inventory.SettingInventoryFunctionalUnit sifu WITH (NOLOCK) ON @AssociateCostMainAccount = 1 AND sifu.SettingInventoryId = @SettingInventoryId AND pdd.FunctionalUnitId = sifu.FunctionalUnitId
							LEFT JOIN Inventory.ProductGroupFunctionalUnit pgfu WITH (NOLOCK) ON @AssociateCostMainAccount = 2 AND ip.ProductGroupId = pgfu.ProductGroupId AND pdd.FunctionalUnitId = pgfu.FunctionalUnitId
							WHERE pddbs.PhysicalInventoryCustodyId IS NULL AND ISNULL(sifu.Id, pgfu.Id) IS NULL
							For Xml Path('')
						)
						if @StringFunctional Is Not Null begin
							SELECT	@CodeMessage = '999', 
									@Message = 'Las siguientes unidades funcionales no tienen parametrizada la cuenta del costo en parametros de Inventarios: ' + @StringFunctional, 
									@DevolutionId = 0, 
									@StatusResult = 3
							return
						end

						Declare @IdThirdPartyHealthAdministrator int = (
							select h.ThirdPartyId 
							from [Contract].HealthAdministrator h With(Nolock)
							Inner Join @tmpADINGRESO ing On ing.GENCONENTITY = h.Id
						)

						If @IdThirdPartyHealthAdministrator Is Null Begin
							SELECT	@CodeMessage = '999', 
									@Message = 'La entidad administradora asociada al ingreso no esta homologada en Indigo Vie', 
									@DevolutionId = 0, 
									@StatusResult = 3
							return
						End

						Declare @TableJournalVoucherDetail Table
						(
							Id Int, 
							IdMainAccount Int, 
							IdThirdParty Int null, 
							IdCostCenter Int null,
							DebitValue Decimal(18, 2), 
							CreditValue Decimal(18, 2), 
							Detail Varchar(500), 
							IdRetention Int,
							RetentionRate Decimal(5,2), 
							BaseValue Decimal(18, 0), 
							BillingValue Decimal(18, 2)
						)
				
						/**** DETALLE DEBITO ****/
						/*** IMPORTANTE! Las devoluciones se realizan al mismo valor por el cual se dispensaron ***/
						---- (inventario de almacenes en consignación)
						Insert Into @TableJournalVoucherDetail
							Select 
								0, 
								ma.Id, 
								Case ma.HandlesThirdParty When 1 Then s.IdThirdParty Else Null End,
								Case ma.HandlesCostCenter When 1 Then Case @AssociateCostCenter When 1 Then f.CostCenterId When 2 Then pg.CostCenterId When 3 Then w.CostCenterId End Else Null End, 
								Round(pdd.AverageCost * td.Quantity, 2),
								0, 
								'Generado desde la Devolución de Dispensacion ' + @CodeDevolution,
								Null, 
								Null, 
								Null, 
								Null
							From @TableDetail td 
							Inner Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs With(Nolock) ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id 
							Inner Join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId 
							Inner join Inventory.PharmaceuticalDispensing pd with(nolock) ON pdd.PharmaceuticalDispensingId = pd.Id
							Inner Join Payroll.FunctionalUnit f With(Nolock) ON f.Id = pdd.FunctionalUnitId
							Inner Join Inventory.InventoryProduct ip With(Nolock) ON ip.Id = pdd.ProductId
							Inner Join Inventory.ProductGroup pg With(Nolock) ON ip.ProductGroupId = pg.Id						
							Inner Join GeneralLedger.MainAccounts ma With(Nolock) ON ma.Id = pg.CounterpartCostConsignedInventoryId
							Inner Join Inventory.Warehouse w With(Nolock) ON w.Id = pdd.WarehouseId
							Inner Join Common.Supplier s With(Nolock) ON w.SupplierId = s.Id
							Where w.WarehouseConsignment = @Uno AND pd.AffectInventory = @Uno and pddbs.PhysicalInventoryCustodyId IS NULL
					
						---- (demás inventario)
						Insert Into @TableJournalVoucherDetail
							Select 
								0, 
								ma.Id, 
								CASE ma.HandlesThirdParty WHEN 1 THEN CASE @PharmaceuticalDispensingGetThirdParty WHEN 1 THEN @IdThirdPartyPacient ELSE @PharmaceuticalDispensingThirdPartyId END ELSE null END,
								CASE ma.HandlesCostCenter WHEN 1 THEN CASE @AssociateCostCenter WHEN 1 THEN f.CostCenterId WHEN 2 THEN pg.CostCenterId WHEN 3 THEN w.CostCenterId END ELSE null END, 
								ROUND(pdd.AverageCost * td.Quantity, 2),
								0, 
								'Generado desde la Devolución de Dispensacion ' + @CodeDevolution,
								null, 
								null, 
								null, 
								null
							From @TableDetail td 
							Inner Join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs With(Nolock) ON td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id 
							Inner Join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId 
							Inner join Inventory.PharmaceuticalDispensing pd with(nolock) ON pdd.PharmaceuticalDispensingId = pd.Id
							Inner Join Payroll.FunctionalUnit f With(Nolock) ON f.Id = pdd.FunctionalUnitId
							Inner Join Inventory.InventoryProduct ip With(Nolock) ON ip.Id = pdd.ProductId
							Inner Join Inventory.ProductGroup pg With(Nolock) ON ip.ProductGroupId = pg.Id
							Inner Join Payments.AccountPayableConcepts apc With(Nolock) ON apc.Id = pg.InventoryAccountPayableConceptId
							Inner Join GeneralLedger.MainAccounts ma With(Nolock) ON ma.Id = apc.IdAccount
							Inner Join Inventory.Warehouse w With(Nolock) ON w.Id = pdd.WarehouseId
							Where w.VirtualStore = @Cero AND w.ControlStore = @Cero AND w.WarehouseConsignment <> @Uno AND pd.AffectInventory = @Uno and pddbs.PhysicalInventoryCustodyId IS NULL
				
						/**** DETALLE CREDITO ****/
						insert into @TableJournalVoucherDetail
							select 
								0, 
								ma.Id, 
								case ma.HandlesThirdParty when 1 then case cg.CareGroupType when 1 then h.ThirdPartyId when 3 then @IdThirdPartyPacient else @IdThirdPartyHealthAdministrator end else null end as ThirdPartyId,
								case ma.HandlesCostCenter when 1 then case @AssociateCostCenter when 1 then f.CostCenterId when 2 then pg.CostCenterId when 3 then w.CostCenterId end else null end as CostCenterId, 
								0 as DebitValue, 
								ROUND(pdd.AverageCost * td.Quantity,2) as CreditValue, 
								'Generado desde la Devolución de Dispensacion ' + @CodeDevolution as Detail, 
								null, 
								null, 
								null, 
								null
							from @TableDetail td 
							inner join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs on td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
							inner join Inventory.PharmaceuticalDispensingDetail pdd With(Nolock) on pdd.Id = pddbs.PharmaceuticalDispensingDetailId
							Inner join Inventory.PharmaceuticalDispensing pd with(nolock) ON pdd.PharmaceuticalDispensingId = pd.Id
							inner join Inventory.Warehouse w With(Nolock) on w.Id = pdd.WarehouseId	
							inner join Payroll.FunctionalUnit f With(Nolock) on f.Id = pdd.FunctionalUnitId	
							inner join Inventory.InventoryProduct ip With(Nolock) on ip.Id = pdd.ProductId
							inner join Inventory.ProductGroup pg With(Nolock) on pg.Id = ip.ProductGroupId
							inner join Contract.CareGroup cg With(Nolock) on cg.Id = pdd.CareGroupId
							left join Contract.[Contract] c With(Nolock) on c.Id = cg.ContractId
							left join Contract.HealthAdministrator h With(Nolock) on h.Id = c.HealthAdministratorId	
							left join Inventory.SettingInventoryFunctionalUnit sifu With(Nolock) on @AssociateCostMainAccount = 1 AND sifu.SettingInventoryId = @SettingInventoryId AND pdd.FunctionalUnitId = sifu.FunctionalUnitId
							left join Inventory.ProductGroupFunctionalUnit pgfu With(Nolock) on @AssociateCostMainAccount = 2 AND pg.Id = pgfu.ProductGroupId AND pdd.FunctionalUnitId = pgfu.FunctionalUnitId
							left join GeneralLedger.MainAccounts ma With(Nolock) on ma.Id = ISNULL(sifu.CostAccountId, pgfu.CostAccountId)
							Where w.VirtualStore = @Cero AND w.ControlStore = @Cero AND pd.AffectInventory = @Uno and pddbs.PhysicalInventoryCustodyId IS NULL

						/******* Mando a ejecutar el Store Procedure de Cuentas Contables ***/
						declare @JournalVoucherXml xml = (
							Select * from @TableJournalVoucher as JournalVoucher 
							Inner join @TableJournalVoucherDetail as JournalVoucherDetail on JournalVoucher.Id = JournalVoucherDetail.Id 
							For Xml Auto, Elements
						)
						Declare @TableResultJournal table(CodeMessage varchar(20), Message varchar(1000), IdJournalVoucher int)
						Insert Into @TableResultJournal
						Exec GeneralLedger.SP_CreateAndValidateJournalVoucherMovement @JournalVoucherXml, @User
						If Exists (select 1 from @TableResultJournal where CodeMessage <> 0) begin
							SELECT	@CodeMessage = CodeMessage, 
									@Message = Message, 
									@DevolutionId = 0, 
									@StatusResult = 3
							from @TableResultJournal
							return
						end

						set @MessageReturn = @MessageReturn + ', ' + (select [Message] from @TableResultJournal where CodeMessage = 0)
					END
				END
				
				--- Elimino el registro de la tabla de control
				Delete from Inventory.InventoryControlDocument where DocumentNumber = @CodeDevolution and DocumentType = @DocumentType11
				--- Actualizo la auditoria basica
				update Inventory.PharmaceuticalDispensingDevolution set ConfirmationUser = @User, ConfirmationDate = [Common].[GETDATE]() where Id = @IdDevolution
				
				/******************** hago las operaciones necesarias para cuando se hace desde dashboard *****************/
				if(@IsDashBoard = 1) begin
				
					declare @historyTypeName varchar(200)
					if @DevolutionOrigin = '1' begin
						set @historyTypeName = 'Aceptación devolutivo farmacia, origen traslado hospitalización - usuario: ' + @UserName
					end
					else if @DevolutionOrigin = '3' begin
						set @historyTypeName = 'Aceptación devolutivo farmacia, origen suspención medicamentos (Enfermería) - usuario: ' + @UserName
					end
					else if @DevolutionOrigin = '5' or @DevolutionOrigin = '6' or @DevolutionOrigin = '7' begin
						set @historyTypeName = 'Aceptación devolutivo hoja gasto quirúrgica, (Enfermería) - usuario: ' + @UserName
					end
					else begin
						IF @EntityName = 'PharmaceuticalDispensingTransfer'
						BEGIN
							SELECT @historyTypeName = CONCAT('Traslado dispensación por ingreso ', @EntityCode, ': Ingreso origen ', @AdmissionNumber, ' - ingreso destino ', @AdmissionNumberDestination, ' - usuario: ', @UserName)
						END
						ELSE
						BEGIN
							set @historyTypeName = 'Aceptación devolutivo farmacia, origen egreso hospitalización - usuario: ' + @UserName
						END
					end

					update fp set 
						CANACTPRO -= case when dpc.Id is null then td.Quantity else CANACTPRO - td.Quantity end
					from 
					(
						SELECT ISNULL(FunctionUnitCode, @FunctionUnitCode) FunctionUnitCode, CodeProduct, sum(Quantity) as Quantity 
						FROM @TableDetail
						WHERE @DevolutionOrigin not in ('5','6', '7') --Se exclute devolutivos de cirugia, pues estos no afectan fisipro al dispensar
						GROUP BY ISNULL(FunctionUnitCode, @FunctionUnitCode), CodeProduct
					) td 
					inner join dbo.HCFISIPRO fp on NUMINGRES = @AdmissionNumber and CODCENATE = @CareCenterCode and UFUCODIGO = td.FunctionUnitCode and CODPRODUC = td.CodeProduct
					left join MedicalHistory.DetailPhysicalCUM dpc (nolock) on dpc.IDHCFISIPRO = fp.ID

					---- Inserto en el Kardex de Crystal
					Insert Into dbo.HCKARDPAC(
						NUMCONSEC
						,IPCODPACI
						,NUMINGRES
						,CODCENATE
						,UFUCODIGO
						,CODPROSAL
						,CODPRODUC
						,CANPRODUCT
						,TIPREGIST
						,HCSOLINSN
						,FECREGKAR
						,TIPORIREG
						,DESMOVPRO)
					Select ISNULL(ConsecutiveKardex, NEWID())
						, @CodePacient
						, @AdmissionNumber
						, @CareCenterCode
						, ISNULL(FunctionUnitCode, @FunctionUnitCode)
						, OrderedHealthProfessionalCode
						, CodeProduct, td.Quantity, '2', '',@GetDateTime, 18
						, concat(@historyTypeName, iif(COALESCE(ph.BatchSerialId, phc.BatchSerialId) is null, '', ', lote: ' + bs.BatchCode))
					From @TableDetail td
					join Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs (nolock) on td.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
					left join Inventory.PhysicalInventory ph (nolock) on pddbs.PhysicalInventoryId = ph.Id
					left join Inventory.PhysicalInventoryCustody phc (nolock) on pddbs.PhysicalInventoryCustodyId = phc.Id
					left join Inventory.BatchSerial bs (nolock) on bs.Id = COALESCE(ph.BatchSerialId, phc.BatchSerialId)

					/***SEGMENTO AUDITORIA MedicalHistory - ACEPTACIÓN DEVOLUTIVO PARA TODOS LOS ALMACENES INCLUYENDO CUSTODIA***/
					-- Se evalua si es mezcla o líquido para cambiar el ProccesType a 2
					-- Esta validación ya se hizo antes pero se reinicia @ProccesType para asegurar el valor correcto
					SET @ProccesType = 1 -- Reiniciar valor por defecto
					IF EXISTS
							(
							SELECT 1
								FROM dbo.HCINFCONC Medicamento 
								JOIN dbo.HCINFLIQA CabeceraMezcla ON Medicamento.CODCONCEC = CabeceraMezcla.CODCONCEC_ORIGEN OR Medicamento.CODCONCEC = CabeceraMezcla.CODCONCEC 
								JOIN @tmpADINGRESO adin ON adin.NUMINGRES = Medicamento.NUMINGRES AND adin.IPCODPACI = Medicamento.IPCODPACI
								JOIN @TableDetail td ON td.CodeProduct = Medicamento.CODPRODUC
								WHERE CabeceraMezcla.PREESTADO = 1 
							UNION 
							SELECT 1
								FROM dbo.HCINFLIQD Diluyente
								JOIN dbo.HCINFLIQA CabeceraMezcla ON Diluyente.CODCONCEC = CabeceraMezcla.CODCONCEC_ORIGEN OR Diluyente.CODCONCEC = CabeceraMezcla.CODCONCEC 
								JOIN @tmpADINGRESO adin ON adin.NUMINGRES = Diluyente.NUMINGRES AND adin.IPCODPACI = Diluyente.IPCODPACI
								JOIN @TableDetail td ON td.CodeProduct = Diluyente.CODPRODUC
								WHERE CabeceraMezcla.PREESTADO = 1
							)
					BEGIN
						SET @ProccesType = 2
					END

					-- Insertar trazabilidad para TODOS los almacenes (incluyendo Custodia)
					-- Esta inserción se ejecuta siempre que haya productos ATC en la devolución
					IF EXISTS (SELECT 1 FROM Inventory.ATC atc WITH(NOLOCK) 
								JOIN @TableDetail temp ON atc.Code = temp.CodeProduct) BEGIN
												
						INSERT INTO MedicalHistory.TraceabilityDrugs(
							[ProductCode],
							[NUMINGRES],
							[ProfessionalCode],
							[RegistrationDate],
							[Action],
							[UFUCODIGO],
							[IdSourceTable],
							[SourceTable],
							[ProccesType])                
						SELECT atc.Code,
							@AdmissionNumber,
							@User,
							@GetDateTime,
							18, -- Farmacia - Aceptación de devolutivo
							ISNULL(td.FunctionUnitCode, @FunctionUnitCode),
							@ConsecutiveCrystal,
							'HCDEVMEDC',
							@ProccesType
						FROM Inventory.ATC atc WITH(NOLOCK) 
						JOIN @TableDetail td ON atc.Code = td.CodeProduct
						JOIN HCDEVMEDC hcc WITH(NOLOCK) ON hcc.CODCONCEC = @ConsecutiveCrystal AND hcc.IDHCHOJAGASTOQX IS NULL
						GROUP BY atc.Code, ISNULL(td.FunctionUnitCode, @FunctionUnitCode)
					END
					/******************************************/

					--- Actualizo los detalles de la devolucion de crystal
					Update dbo.HCDEVMEDD 
					set PROESTADO = case 
										when (CANPENDIE - td.Quantity) = 0 then '2'
										WHEN FP.CANACTPRO = 0 then '2'
										else PROESTADO
									end,
						CANDEVOLV += td.Quantity,
						CANPENDIE -= td.Quantity
					From (
						Select CodeProduct, Sum(Quantity) As Quantity, HCDEVMEDDId From @TableDetail Group By CodeProduct, HCDEVMEDDId
					) td 
					Inner Join dbo.HCDEVMEDD hcd With(Nolock) on hcd.CODCONCEC = @ConsecutiveCrystal And hcd.CODPRODUC = td.CodeProduct AND TD.HCDEVMEDDId = hcd.Id
					Left Join dbo.HCFISIPRO FP on FP.NUMINGRES = @AdmissionNumber AND FP.CODPRODUC = td.CodeProduct AND fp.UFUCODIGO = hcd.UFUCODIGO 
					

					---- Actualizamos el returned de DetailPhysicalCUM, ya que este se tiene en cuenta en el kardex del EHR
					Update MedicalHistory.DetailPhysicalCUM 
						Set ReturnedQuantity += td.Quantity
						From (
							Select  dpc.ID, TD.Quantity
							From (
								Select CodeProduct, Sum(Quantity) As Quantity, HCDEVMEDDId From @TableDetail Group By CodeProduct, HCDEVMEDDId
							) td 
							Inner Join dbo.HCDEVMEDD hcd With(Nolock) on hcd.CODCONCEC = @ConsecutiveCrystal And hcd.CODPRODUC = td.CodeProduct AND TD.HCDEVMEDDId = hcd.Id
							lEFT JOIN MedicalHistory.DetailPhysicalCUM  dpc With (Nolock) on hcd.IdDetailPhysicalCUM = dpc.ID 
							WHERE dpc.ReturnedQuantity = 0 
						) td
						Where MedicalHistory.DetailPhysicalCUM.Id = td.Id;

					--actualizamos datos de la Hoja Quirugica
					if EXISTS (SELECT 1 FROM @TableDetail WHERE ISNULL(IDHCHOJAGASTOQX, @IDHCHOJAGASTOQX) > 0)
					begin										
						Update hcd
							set CANTIDADACEPTADADEV += td.Quantity, 
								CANTIDADDEVOLVER -= td.Quantity
						From dbo.HCHOJAGASTOQXD hcd
						JOIN
						(
							Select	ISNULL(td.IDHCHOJAGASTOQX, @IDHCHOJAGASTOQX) IDHCHOJAGASTOQX, 
									td.CodeProduct, 
									Sum(td.Quantity) As Quantity,
									case dc.ORIDEVMED when 5 then 1 when 6 then 2 when 7 then 3 else 0 end as RequestType
							From @TableDetail td
							inner join HCDEVMEDC dc on dc.CODCONCEC = td.EntityId
							Group By ISNULL(td.IDHCHOJAGASTOQX, @IDHCHOJAGASTOQX), CodeProduct, dc.ORIDEVMED
						) td ON hcd.IDHCHOJAGASTOQX = td.IDHCHOJAGASTOQX And hcd.CODPRODUC = td.CodeProduct					
						WHERE hcd.RequestType = td.RequestType
											
						--actualizar tambien el estado de la orden cuando devuelve el total del devolutivo						
						Update hcd
							set StatusOrder = iif((hcd.CANTIDADENTREGADA - hcd.CANTIDADGASTADA - hcd.CANTIDADACEPTADADEV) > 0, 1, 2)
						From dbo.HCHOJAGASTOQXD hcd
						JOIN
						(
							Select	ISNULL(IDHCHOJAGASTOQX, @IDHCHOJAGASTOQX) IDHCHOJAGASTOQX, 
									CodeProduct, 
									Sum(Quantity) As Quantity 
							From @TableDetail 
							Group By ISNULL(IDHCHOJAGASTOQX, @IDHCHOJAGASTOQX), CodeProduct
						) td ON hcd.IDHCHOJAGASTOQX = td.IDHCHOJAGASTOQX And hcd.CODPRODUC = td.CodeProduct

						--3 - Devolucion parcial aceptada (cuando se acepta una parte)
						--4 - Confirmada (cuando se acepta todo devolutivo y/o cuando se gasto todo)
						update hc
							set ESTADO = IIF(EXISTS(
										SELECT 1 
										FROM dbo.HCHOJAGASTOQXD hcd 
										WHERE hcd.IDHCHOJAGASTOQX = hc.ID 
											AND (hcd.CANTIDADENTREGADA - hcd.CANTIDADGASTADA - hcd.CANTIDADACEPTADADEV) > 0
									), 3, 4)
						FROM dbo.HCHOJAGASTOQX  hc
						JOIN
						(
							Select DISTINCT	ISNULL(IDHCHOJAGASTOQX, @IDHCHOJAGASTOQX) IDHCHOJAGASTOQX
							From @TableDetail 
							Group By ISNULL(IDHCHOJAGASTOQX, @IDHCHOJAGASTOQX), CodeProduct
						) td ON hc.ID = td.IDHCHOJAGASTOQX
					end

					--consulto si la devolucion tiene detalles con cantidades
					Declare @strUno Varchar(1) = '1', @strDos Varchar(1) = '2', @strTres varchar(1) = '3'
					if ISNULL(@EntityName, '') <> 'PharmaceuticalDispensingTransfer' AND Not Exists (Select CODCONCEC from dbo.HCDEVMEDD With(Nolock) Where CODCONCEC = @ConsecutiveCrystal and CANPENDIE > @cero and PROESTADO IN (@strUno)) begin
						--si la cantidad es pendinte es 0 actualizo el estado de la cabecera
						update dbo.HCDEVMEDC set DEVESTADO = '2' where CODCONCEC = @ConsecutiveCrystal
						-- Ahora se hace el proceso de liberacion de camas
							
							if @DevolutionOrigin = '1' begin --- Si es un traslado de cama
							
							    Declare @CantidadEstancias as Int  = (SELECT COUNT(*) FROM CHREGESTA WHERE REGESTADO = 1 AND IPCODPACI = @CodePacient AND NUMINGRES = @AdmissionNumber)

								Declare @MinFecini DateTime = (
									select MIN(FECINIEST) from dbo.CHREGESTA With(Nolock) 
									where NUMINGRES = @AdmissionNumber and REGESTADO = @Uno
								)
								Declare @MaxFecini DateTime = (
									select MAX(FECINIEST) from dbo.CHREGESTA With(Nolock) 
									where NUMINGRES = @AdmissionNumber and REGESTADO = @Uno
								)
								set @IdStayOrigin  = (
									select ID from dbo.CHREGESTA  With(Nolock)
									where NUMINGRES = @AdmissionNumber And REGESTADO = @Uno And FECINIEST = @MinFecini
								)
								set @IdStayDestination = (
									select ID from dbo.CHREGESTA  With(Nolock)
									where NUMINGRES = @AdmissionNumber And REGESTADO = @Uno and FECINIEST = @MaxFecini
								)

								--- Actualizo la estancia de origen
								Declare @__Feciniest DateTime
								If @IdStayOrigin <> @IdStayDestination Begin
									Select @__Feciniest = FECINIEST from dbo.CHREGESTA where ID = @IdStayDestination
									update dbo.CHREGESTA set 
										FECFINEST = @__Feciniest, 
										REGESTADO = 2, 
										REGDIAEST = DATEDIFF(DAY, FECINIEST, @__Feciniest) 
									where ID = @IdStayOrigin
								End
								Else Begin
									Select Top 1 @__Feciniest = FECEGRESO 
									From dbo.CHREGEGRE WHERE NUMINGRES = @AdmissionNumber						
									If @__Feciniest Is Not Null
										update dbo.CHREGESTA set 
											FECFINEST = @__Feciniest, 
											REGESTADO = 2, 
											REGDIAEST = DATEDIFF(DAY, FECINIEST, @__Feciniest) 
										where ID = @IdStayOrigin											
									Else
									--- Se cierra la instancia en CHREGESTA solo en caso de que el paciente tenga más de una instancia
									IF @CantidadEstancias  >= 2  BEGIN
										update dbo.CHREGESTA set REGESTADO = 2 where ID = @IdStayOrigin
									END
								End
								
								--- Actualizo la cama de origen para liberarla
								--- Solo en caso de que el paciente tenga más de una instancia
								IF @CantidadEstancias  >= 2  BEGIN
									update dbo.CHCAMASHO set 
										ESTADCAMA = 1, 
										CAMTRAMED = 0, 
										CODCONCEC = null, 
										CODAISLAM = null, 
										CAMTIPANO = 0, 
										CAMDEVMED = 0,
										BedType = null, 
										TypeTransfer = null
									from dbo.CHREGESTA re 
									inner join dbo.CHCAMASHO ca With(Nolock) on re.CODICAMAS = ca.CODICAMAS 
									where re.ID = @IdStayOrigin

									--- Actualizo la cama de destino
									update dbo.CHCAMASHO set CODCONCEC = null, BedType = null, TypeTransfer = null 
									from dbo.CHREGESTA re 
									inner join dbo.CHCAMASHO ca With(Nolock) on re.CODICAMAS = ca.CODICAMAS 
									where re.ID = @IdStayDestination
								end else begin
									update dbo.CHCAMASHO set 										
										CAMDEVMED = 0,
										BedType = null, 
										TypeTransfer = null
									from dbo.CHREGESTA re 
									inner join dbo.CHCAMASHO ca With(Nolock) on re.CODICAMAS = ca.CODICAMAS 
									where re.ID = @IdStayOrigin
								end

								--- Actualizo el ingreso para colocarle la cama actual del paciente y la unidad funcional
								select @UfuCodigoDestination = uf.UFUCODIGO, 
									@CodiCamaDestination = ca.CODICAMAS 
								From dbo.CHREGESTA re 
								inner join dbo.CHCAMASHO ca on re.CODICAMAS = ca.CODICAMAS 
								inner join dbo.INUNIFUNC uf on uf.UFUCODIGO = ca.UFUCODIGO 
								where re.ID = @IdStayDestination

								update dbo.ADINGRESO set 
									UFUAACTHOS = @UfuCodigoDestination, 
									UFUACTPAC = @UfuCodigoDestination, 
									CODCAMACT = @CodiCamaDestination 
								where NUMINGRES = @AdmissionNumber

							end
							else if @DevolutionOrigin = '2' begin --- Si es Egreso de cama

								---Se modifica esta seccion donde al aceptar una devolucion se libera la cama y se deja el estado de la cama sin rechazo 
								update dbo.CHCAMASHO set ESTADCAMA = 1, CAMTRAMED=0, CODCONCEC = null, CODAISLAM = null, CAMTIPANO = 0, CAMDEVMED = 0, CAMRECDEV = 0, BedType = null, TypeTransfer = null
								from dbo.CHREGESTA re 
								inner join dbo.CHCAMASHO ca on re.CODICAMAS = ca.CODICAMAS where re.NUMINGRES = @AdmissionNumber and re.REGESTADO = @Uno

								if Not Exists (select NUMINGRES from dbo.CHREGEGRE With(Nolock) Where NUMINGRES = @AdmissionNumber and FECEGRESO is not null) begin
									SELECT	@CodeMessage = '999', 
											@Message = 'La fecha del egreso del paciente no existe', 
											@DevolutionId = 0, 
											@StatusResult = 3
									return
								end
								set @FechaEgresoPaciente = (select FECEGRESO from dbo.CHREGEGRE With(Nolock) Where NUMINGRES = @AdmissionNumber and FECEGRESO is not null)
								--- Actualizo el estado de la estancia
								update dbo.CHREGESTA set REGESTADO = 2, FECFINEST = @FechaEgresoPaciente, REGDIAEST = DATEDIFF(DAY, FECINIEST, @FechaEgresoPaciente) where NUMINGRES = @AdmissionNumber and REGESTADO = 1

							end
					end
				end --- Fin proceso de dash board

				/*Guardamos las readecuaciones bajo la logica de que si existe un registro de RequestPDS y la bandera IsReadjustment is false, 
				 significa que previamente ya se hizo una readecuación, por consiguiente no se debe insertar un nuevo registro y solo actualizar 
				 el status a 0 (para que caiga a concepto técnico de readecuaciones)
				 */
				 ------------------------------------------------------------------------------------
				DECLARE @RequestPackageDetailStatusRows INT,
						@RequestPackageDetailStatusId INT

				SET @RequestPackageDetailStatusRows = 1
				SET	@RequestPackageDetailStatusId = 0

				WHILE @RequestPackageDetailStatusRows > = 1
				BEGIN

					SELECT TOP 1
						@RequestPackageDetailStatusId = rpds.Id
					FROM @TableDetail td
					JOIN HCDEVMEDD medd 
						on td.EntityName = 'HCDEVMEDD' And td.EntityId = medd.CODCONCEC And td.CodeProduct = medd.CODPRODUC And td.HCDEVMEDDId = medd.Id
					JOIN MedicalHistory.DetailPhysicalCUM dpc on medd.IdDetailPhysicalCUM = dpc.Id
					JOIN MixingStation.RequestPackageDetailStatus rpds on dpc.GroupingCodeDose = rpds.GroupingCodeDose
					WHERE rpds.Id > @RequestPackageDetailStatusId
					ORDER BY rpds.Id

					SET @RequestPackageDetailStatusRows = @@ROWCOUNT
					IF @RequestPackageDetailStatusRows = 0 BREAK

					IF NOT EXISTS
					(
						SELECT 1
						FROM MixingStation.Readjustments r
						WHERE r.RequestPackageDetailStatusId = @RequestPackageDetailStatusId
							AND r.IsReadjustment = 0
					)
					BEGIN

						INSERT INTO [MixingStation].[Readjustments]
							([EntityId]
							,[EntityName]
							,[RequestPackageDetailStatusId]
							,[BatchCode]
							,[SendTo]
							,[CreationUser]
							,[CreationDate]
							,[ModificationUser]
							,[ModificationDate]
							,[Status]
							,[TechnicalConceptDate]
							,[ExpiratedDate]
							,[Temperature]
							,[TechnicalConcept])				
						SELECT @IdDevolution
							, 'PharmaceuticalDispensingDevolution'
							, rpds.Id
							, rpds.BatchCode
							, 0 as SendTo
							, @User as [User]
							, common.GETDATE()
							, null
							, null
							, 0
							, null
							, null
							, null
							, null
						FROM MixingStation.RequestPackageDetailStatus rpds 
						WHERE rpds.Id = @RequestPackageDetailStatusId
					END
					ELSE
					BEGIN
						UPDATE r 
							SET EntityId = @IdDevolution,
								SendTo = 0,
								Status = 0
						FROM MixingStation.Readjustments r
						WHERE r.RequestPackageDetailStatusId = @RequestPackageDetailStatusId
							AND r.IsReadjustment = 0
					END --Fin condición
					
				END --Fin While
				------------------------------------------------------------------------------------
				--select * from @TableDetail td
			end --- Fin solo si se va a confirmar
			else begin
				set @MessageReturn = 'Se guardo correctamente la Devolucion ' + @CodeDevolution
			end

			declare @UserNameCreation varchar(200) =''
			if (@IdDevolution>0 )begin
				declare @userCode varchar(20)
				select @userCode = CreationUser  from PharmaceuticalDispensingDevolution where id = @IdDevolution 
					
				select @UserNameCreation = ' *USERINDIGO* '+ u.UserCode + ' - '+ p.Fullname 
				from [Security].[User] u 
				inner join [Security].Person p on u.IdPerson = p.Id					
				where u.UserCode = @userCode
			end
			---- Falta devolver el mesaje con todo lo que creo
			SELECT	@CodeMessage = '0', 
					@Message = IIF(ISNULL(@EntityName, '') <> 'PharmaceuticalDispensingTransfer', @MessageReturn+@UserNameCreation, CONCAT('Se guardó y confirmó la devolución de dispensación ', @CodeDevolution)), 
					@DevolutionId = @IdDevolution, 
					@StatusResult = 1
		end --- Fin del if que valida si el Xml de Devoluciones viene lleno		
	end try
	begin catch
		SELECT	@CodeMessage = '999', 
				@Message = 'Ocurrio un error al generar la devolucion: ' + ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(10)), 
				@DevolutionId = 0, 
				@StatusResult = 3
	end catch	
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera y confirma devoluciones de medicamentos y dispositivos médicos en el módulo de farmacia hospitalaria. Procesa dos flujos principales: la anulación de devoluciones previas (revertiendo cantidades y estados en HCDEVMEDD) y la creación de nuevas devoluciones a partir de dispensaciones farmacéuticas (PharmaceuticalDispensing y PharmaceuticalDispensingDetail), afectando el inventario de la bodega y los registros de kardex. Valida que el ingreso del paciente no esté facturado ni cerrado (consultando CHREGESTA y el estado del ingreso), recupera la entidad pagadora desde las órdenes de servicio (ServiceOrder), y registra la trazabilidad del usuario que ejecuta la operación (Security.User / Security.Person). Retorna el identificador de la devolución generada, un código de mensaje y el estado del resultado del proceso.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePharmaceuticalDevolution_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePharmaceuticalDevolution_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa la confirmación, guardado o anulación de una devolución farmacéutica de dispensación, sincronizando inventario, kardex, folios de facturación, comprobante contable, hoja de gasto quirúrgica, liberación de camas y trazabilidad clínica.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso (NUMINGRES) debe existir en dbo.ADINGRESO y no estar en estado ''F'' (facturado) ni ''C'' (cerrado); Si el ingreso está bloqueado (''B'') y el tipo de bloqueo de facturación es 3 (Farmacia y Facturación), no se permite la operación; El paciente del ingreso debe existir como tercero (Common.ThirdParty) en Indigo Vie; Debe existir parametrización Inventory.SettingInventory para la unidad operativa; Para confirmar (Status=2), el periodo de inventario (Year/Month) debe coincidir con la fecha del documento; El usuario debe tener permiso sobre el almacén (Inventory.WarehouseUser) cuando proviene del dashboard; En anulaciones, todo registro del XML debe tener motivo de anulación (IdHCMOANULB no nulo/vacío); Debe existir un libro oficial en GeneralLedger.LegalBook para generar comprobante contable; La entidad administradora del ingreso debe estar homologada en Contract.HealthAdministrator; Los almacenes de las dispensaciones deben coincidir con el almacén seleccionado y ser del mismo tipo de consignación; Las cantidades a devolver no pueden superar la cantidad pendiente (CANPENDIE) ni la cantidad física disponible (HCFISIPRO/HCHOJAGASTOQXD)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDevolution_Output';
-- GO
