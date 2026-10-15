

-- =============================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 08/05/2017
-- Description:	Procedimiento que se encarga de guardar en las tablas de Liquidation y LiquidationDetail de Payroll
-- =============================================
CREATE PROCEDURE [Payroll].[SP_SaveAgreementsCMassive] 
	@XmlObject as Xml,
	@CodeUser as varchar(20)
AS
BEGIN

	--Tabla que almacena los errores o aciertos de cada item que se recorre para poder devolverlos
	declare @TableReturn table(Id int IDENTITY PRIMARY KEY, CodeResult int, MessageResult varchar(max))

	--Tabla para almacenar los items del listado que viene en el xml
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY, Nit varchar(50),InternalCode varchar(50), ConceptCode varchar(5), PaidDate date, AgreementsTypeCode varchar(20), NoveltyType varchar(1), QuoteValue numeric(18,0), AgreementsBalance numeric(18,0), NitCompany varchar(50))

	Begin try
	
		insert into @TableXmlObject
		select 
		t.x.value('Nit[1]','varchar(50)') as Nit,
		t.x.value('InternalCode[1]','varchar(50)') as InternalCode,
		t.x.value('ConceptCode[1]','varchar(5)') as ConceptCode,
		t.x.value('PaidDate[1]','VARCHAR(10)') as PayrollDate,
		t.x.value('AgreementsTypeCode[1]','varchar(20)') as AgreementsTypeCode,
		t.x.value('NoveltyType[1]','varchar(1)') as NoveltyType,
		t.x.value('QuoteValue[1]','numeric(18,0)') as QuoteValue,
		t.x.value('AgreementsBalance[1]','numeric(18,0)') as AgreementsBalance,
		t.x.value('NitCompany[1]','varchar(50)') as NitCompany
		from @XmlObject.nodes('/Data/Row') t(x)
		
		--Id del registro
		declare @Id as int
				
		--Nit del tercero
		declare @Nit as varchar(50)

		-- Código del Concepto
		declare @ConceptCode as varchar(5)

		-- Código del Tipo del Convenio
		declare @AgreementsTypeCode as varchar(20)

		--Fecha de nómina
		declare @PayrollDate as date

		--Tipos de Novedades
		declare @NoveltyType as varchar(1)

		--Valor de las Cuotas
		declare @QuoteValue as numeric(18,0)
	
		--Saldo del Préstamo
		declare @AgreementsBalance as numeric(18,0)

		-- Nit de la Compañía
		declare @NitCompany as varchar(50)
		
		--Codigo interno del empelado
		declare @InternalCode as varchar(50)

		--Se recorre el cursor
		declare InfoItem Cursor For Select [Id], [Nit],[InternalCode], ConceptCode, PaidDate, AgreementsTypeCode, NoveltyType, QuoteValue, AgreementsBalance, NitCompany From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @Id, @Nit,@InternalCode, @ConceptCode, @PayrollDate, @AgreementsTypeCode, @NoveltyType, @QuoteValue, @AgreementsBalance, @NitCompany

		While @@fetch_status = 0
		Begin

			DECLARE @NoveltyTypeNumber TINYINT;
			SET @NoveltyTypeNumber = CASE WHEN UPPER(@NoveltyType) = 'V' THEN 2 ELSE 1 END;

			--Se valida que los días trabajados sea mayor a cero
			if @QuoteValue <= 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque el valor de la cuota está en cero')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End
			PRINT CONCAT('NIT: ', @Nit,' INTERNALCODE: ',@InternalCode)
			DECLARE @CantidadEmpleado integer = 0
			SELECT @CantidadEmpleado =COUNT(*)
											FROM Payroll.Employee E
											LEFT JOIN Payroll.Contract C ON E.Id = C.EmployeeId
											LEFT JOIN Common.ThirdParty TP ON TP.Id = E.ThirdPartyId
											LEFT JOIN Payroll.Employee E2 ON E.InternalCode = E2.InternalCode
											WHERE (TP.Nit = @Nit OR E2.InternalCode = @InternalCode) 
											  AND C.Valid = 1 
											  AND C.Status = 1;
			--COUNT(*) FROM Payroll.Employee E, Common.ThirdParty TP, Payroll.Contract C 
			--WHERE E.Id = C.EmployeeId and TP.Id = E.ThirdPartyId and TP.Nit = @Nit AND C.Valid = 1 AND C.Status = 1

			IF @CantidadEmpleado <= 0 BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque no existe o no tiene Contrato Activo')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			END

			DECLARE @CantidadNitCompania integer = 0
			SELECT @CantidadNitCompania = COUNT(*) FROM Common.ThirdParty where Nit = @NitCompany

			IF @CantidadNitCompania <= 0 BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque NO EXISTE una Empresa con ese Nit')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			END

			DECLARE @CantidadCompaniaEmpresa integer = 0
			SELECT @CantidadCompaniaEmpresa = COUNT(*) FROM Common.ThirdParty TP, Payroll.Company C where TP.Nit = @NitCompany AND C.ThirdPartyId = TP.Id and C.AgreementType = 1

			IF @CantidadCompaniaEmpresa <= 0 BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque la Empresa no está creada en Compañías ni tiene Tipo Convenios')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			END

			DECLARE @CantidadConcepto integer = 0
			SELECT @CantidadConcepto = COUNT(*) FROM Payroll.Concept where Code = @ConceptCode

			IF @CantidadConcepto <= 0 BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque el Concepto No Existe')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			END

			DECLARE @CantidadConceptoConvenio integer = 0
			SELECT @CantidadConceptoConvenio = COUNT(*) FROM Payroll.Concept where Code = @ConceptCode and ConceptClass = '041'

			IF @CantidadConceptoConvenio <= 0 BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque el Concepto SI Existe, pero no tiene la Clase Seleccionada como Convenios')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			END

			DECLARE @GroupId int
			DECLARE @EmployeeId int
			DECLARE @CompanyId int
			DECLARE @ConceptId int
			DECLARE @KindsAgreementsId int

			SELECT TOP 1 @KindsAgreementsId = Id FROM Payroll.KindsAgreements

			-- Averiguo los datos del Empleado
				SELECT 
					@GroupId = C.GroupId, 
					@EmployeeId = E.Id
				FROM Payroll.Employee E
				LEFT JOIN Payroll.Contract C ON E.Id = C.EmployeeId
				LEFT JOIN Common.ThirdParty TP ON E.ThirdPartyId = TP.Id
				LEFT JOIN Payroll.Employee E2 ON E.InternalCode = E2.InternalCode
				WHERE (TP.Nit = @Nit OR E2.InternalCode = @InternalCode) --LINEA PARA OBTENER EL CODIGO INTERNO DESDE EMPLOYEE
				  AND C.Valid = 1 
				  AND C.[Status] = 1;

			-- Averiguo los Datos del Concepto 
			SELECT  @ConceptId = Id FROM Payroll.Concept where Code = @ConceptCode

			-- Averiguo los Datos de la Empresa
			SELECT @CompanyId = C.Id FROM Payroll.Company C, Common.ThirdParty TP WHERE TP.Id = C.ThirdPartyId and TP.Nit = @NitCompany

			DECLARE @ConvenioYaCreado integer = 0
			SELECT @ConvenioYaCreado = COUNT(*) FROM Payroll.AgreementsC where ConceptId = @ConceptId and EmployeeId = @EmployeeId AND StartingDate = @PayrollDate and CompanyId = @CompanyId

			IF @ConvenioYaCreado > 0 BEGIN
				--Se actualiza los campos con el estado en false y el mensaje de error
				insert into @TableReturn(CodeResult, MessageResult) values(999, 'No se puede guardar el empleado con cédula ' + @Nit + ' porque existe un convenio ya creado para esa Fecha, el mismo Concepto y la misma Empresa')
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			END

			DECLARE @ConvenioExistente integer = 0
			DECLARE @IdAgreements integer = 0
			Declare @tmpConsecutive Table(Consecutive Decimal(18, 0))
			SELECT @ConvenioExistente = COUNT(*) FROM Payroll.AgreementsC where EmployeeId = @EmployeeId AND ConceptId = @ConceptId AND [State] = 2 and KindsAgreementsId = @KindsAgreementsId and StartingDate < @PayrollDate

			PRINT '@ConvenioExistente --> ' + convert(varchar(5),@ConvenioExistente)

			IF @ConvenioExistente > 0 BEGIN
				-- El Convenio ya existe, entonces se ingresa el Detalle 
				SELECT @IdAgreements = Id FROM Payroll.AgreementsC where EmployeeId = @EmployeeId AND ConceptId = @ConceptId AND [State] = 2

				DECLARE @CountVacation int = 0
				SELECT @CountVacation = count(*) FROM Payroll.AgreementsC where Id = @IdAgreements and PaidVacation = 1

				IF @CountVacation > 0 BEGIN
					--El Convenio Existe, y se pagó por vacaciones, entonces lo que hay que hacer es inactivar este y crear uno nuevo con los nuevos datos
					-- Finalizo el que había
					UPDATE Payroll.AgreementsC SET State = '4' where Id = @IdAgreements
					-- Inserto uno nuevo
					DECLARE @ConsecutiveNew int = 0
					delete from @tmpConsecutive
					UPDATE Common.Consecutive SET NumberConsecutive += 1 OUTPUT INSERTED.NumberConsecutive into	@tmpConsecutive WHERE [Description] = 'CONVENIOS'
					
					select top 1 @ConsecutiveNew = Consecutive from @tmpConsecutive
					
					INSERT INTO Payroll.AgreementsC(Consecutive, GroupId, EmployeeId, CompanyId, ConceptId, KindsAgreementsId, Comments, LiquidationType, TermType, AgreementValue, NumberShares, [State], StartingDate, CurrentBalance)
					VALUES(@ConsecutiveNew, @GroupId, @EmployeeId, @CompanyId, @ConceptId, @KindsAgreementsId, 'CONVENIO CREADO POR ARCHIVO PLANO EL DÍA ' + CONVERT(VARCHAR(20),[Common].[GETDATE]()), @NoveltyTypeNumber, @NoveltyTypeNumber,  @QuoteValue,  IIF(@NoveltyTypeNumber = 1,1,0), 2, @PayrollDate, @QuoteValue)
					
					INSERT INTO Payroll.AgreementsD(AgreementsCId, ShareValuePaid, DatePayment, TypePayment, StateShare)
					VALUES(SCOPE_IDENTITY(), @QuoteValue, @PayrollDate, 3, 'Pago Registrado por Archivo Plano, subido el día ' + CONVERT(varchar(10),[Common].[GETDATE]()))									   

				END ELSE BEGIN
					INSERT INTO Payroll.AgreementsD(AgreementsCId, ShareValuePaid, DatePayment, TypePayment, StateShare)
					VALUES(@IdAgreements, @QuoteValue, @PayrollDate, 3, 'Pago Registrado por Archivo Plano, subido el día ' + CONVERT(varchar(10),GETDATE()))	
				END
				
			END ELSE BEGIN
				-- El Convenio no existe, entonces se crea desde cero, cabecera y detalle
				DECLARE @IdAgreementsC int
				DECLARE @Consecutive int = 0

				select @Consecutive = NumberConsecutive from Common.Consecutive WHERE [Description] = 'CONVENIOS'

				SET @Consecutive = @Consecutive + 1

				IF EXISTS
				(
					SELECT 1
					FROM Payroll.AgreementsC
					WHERE Consecutive = @Consecutive
				)
				BEGIN 
					INSERT INTO @TableReturn(CodeResult, MessageResult) VALUES(999, 'Ya existe un consecutivo '+ CAST(@Consecutive AS VARCHAR(10)) + ' en convenios')
					select * from @TableReturn
					RETURN
				END

				INSERT INTO Payroll.AgreementsC(Consecutive, GroupId, EmployeeId, CompanyId, ConceptId, KindsAgreementsId, Comments, LiquidationType, TermType, AgreementValue, NumberShares, [State], StartingDate, CurrentBalance)
				SELECT @Consecutive, @GroupId, @EmployeeId, @CompanyId, @ConceptId, @KindsAgreementsId, 'CONVENIO CREADO POR ARCHIVO PLANO EL DÍA ' + CONVERT(VARCHAR(20),GETDATE()), @NoveltyTypeNumber, @NoveltyTypeNumber,  @QuoteValue,  IIF(@NoveltyTypeNumber = 1,1,0), 2, @PayrollDate, @QuoteValue

				INSERT INTO Payroll.AgreementsD(AgreementsCId, ShareValuePaid, DatePayment, TypePayment, StateShare)
				VALUES(SCOPE_IDENTITY(), @QuoteValue, @PayrollDate, 3, 'Pago Registrado por Archivo Plano, subido el día ' + CONVERT(varchar(12),GETDATE()))

				-- Actualizo el consecutivo en la Tabla de Comunes
				UPDATE Common.Consecutive SET NumberConsecutive = @Consecutive WHERE [Description] = 'CONVENIOS'

			END

			--Se agrega a la tabla que retorno el ok
			insert into @TableReturn(CodeResult, MessageResult) values(0, 'El empleado con cédula o código interno ' + IIF(@Nit='',@InternalCode,@Nit) + ' se guardó correctamente')

			NextFetch:
			--Se pasa a la siguiente posicion del cursor
			Fetch Next From InfoItem Into @Id, @Nit,@InternalCode, @ConceptCode, @PayrollDate, @AgreementsTypeCode, @NoveltyType, @QuoteValue, @AgreementsBalance, @NitCompany
			continue

		End

		Close InfoItem
		Deallocate InfoItem
		
		select * from @TableReturn
		
	end try
	begin catch
		delete from @TableReturn
		insert into @TableReturn(CodeResult, MessageResult) values(888, CAST(ERROR_MESSAGE()  as varchar(100)) + ' Linea : ' +CAST(ERROR_LINE() AS VARCHAR(10)))
		select * from @TableReturn
	end catch

END