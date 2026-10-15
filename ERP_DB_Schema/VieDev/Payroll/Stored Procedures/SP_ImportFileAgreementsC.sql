




-- =============================================
-- Author:		Daniel Eduardo Arevalo
-- Create date: 07/09/2017
-- Description:	Procedimiento que se encarga de importar el archivo de excel y validarlo
-- =============================================
CREATE PROCEDURE [Payroll].[SP_ImportFileAgreementsC]
	@XmlObject as Xml
AS
BEGIN
	
	--Tabla para almacenar los items del listado que viene en el xml
	declare @TableXmlObject table(Id int IDENTITY PRIMARY KEY, CountFields int, StatusField int, MessageField varchar(max), Nit varchar(50), NitName varchar(200), EmployeeId int, 
	PayrollDate varchar(20), ConceptCode varchar(5), ConceptId int, NoveltyType varchar(1), QuoteValue numeric(18,0), NoveltyBalance numeric(18,0), NitCompany varchar(50),InternalCode varchar(50) )

	--Id del empleado
	declare @EmployeeId as int

	--begin transaction
	Begin try
	
		insert into @TableXmlObject
		select 
		t.x.value('CountFields[1]','int') as CountFields,
		t.x.value('StatusField[1]','int') as StatusField,
		t.x.value('MessageField[1]','varchar(100)') as MessageField,
		t.x.value('Nit[1]','varchar(50)') as Nit,		
		t.x.value('NitName[1]','varchar(200)') as NitName,
		t.x.value('EmployeeId[1]','int') as EmployeeId,
		t.x.value('PayrollDate[1]','varchar(20)') as PayrollDate, --varchar(20)		
		t.x.value('ConceptCode[1]','varchar(5)') as ConceptCode,
		t.x.value('ConceptId[1]','int') as ConceptId,
		t.x.value('NoveltyType[1]','varchar(1)') as NoveltyType,
		t.x.value('QuoteValue[1]','numeric(18,0)') as QuoteValue,
		t.x.value('NoveltyBalance[1]','numeric(18,0)') as NoveltyBalance,
		t.x.value('NitCompany[1]','varchar(50)') as NitCompany,
		t.x.value('InternalCode[1]','varchar(50)') as InternalCode
		from @XmlObject.nodes('/Data/Row') t(x)
		--SELECT * FROM @TableXmlObject
		--Se declara el contador de posiciones para enviar en los mensajes de error
		declare @Position as int = 0

		--Id del registro
		declare @Id as int

		--Cantidad de columnas de cada registro
		declare @CountFields as int
		
		--Nit del tercero
		declare @Nit as varchar(50)

		--Nit del tercero
		declare @NitName as varchar(200)

		--Fecha de la Cuota
		declare @PayrollDate as varchar(20) --Date

		-- Código del Concepto
		declare @ConceptCode as varchar(4)

		-- Id del Concepto
		declare @ConceptId int

		-- Tipo de Novedad
		declare @NoveltyType varchar(1)

		-- Valor de la Cuota del Crédito
		declare @QuoteValue numeric(18,0)

		-- Valor del Balance del Saldo
		declare @NoveltyBalance numeric(18,0)
		
		-- Nit del Empresa
		declare @NitCompany VARCHAR(50)
		--Codigo Interno 
		declare @Internalcode VARCHAR(50)

		--Se recorre el cursor
		declare InfoItem Cursor For Select [Id], [CountFields], [Nit], [PayrollDate], [ConceptCode], NoveltyType, QuoteValue, NoveltyBalance, NitCompany,[InternalCode] From @TableXmlObject

		Open InfoItem

		Fetch Next From InfoItem Into @Id, @CountFields, @Nit, @PayrollDate, @ConceptCode, @NoveltyType, @QuoteValue, @NoveltyBalance, @NitCompany,@Internalcode

		While @@fetch_status = 0
		Begin
		
			--Se incrementa la posicion
			set @Position = @Position + 1
			
			--Se valida que cada registro tenga la estructura requerida
			if @CountFields <> 8
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El registro ' + convert(varchar(3),@Position) + ' no tiene la estructura requerida'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End		

			--Se valida que el campo del nit del tercero no este vacio
			if LEN(@Nit) = 0 And LEN(@Internalcode) =0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La cédula  y el Código Interno del registro ' + convert(varchar(3),@Position) + ' estan vacios'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End
			
			update @TableXmlObject set InternalCode = @Internalcode where id=@Id
			--Se valida que el nit del tercero exista o el codigo interno
			if (select COUNT(*) from Common.ThirdParty tp
				LEFT JOIN Payroll.Employee e on tp.id = e.ThirdPartyId
				LEFT JOIN Payroll.Employee E2 ON E.InternalCode = E2.InternalCode
			where TP.Nit = LTRIM(RTRIM(@Nit)) OR E2.InternalCode = LTRIM(RTRIM(@Internalcode))) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La cédula del registro ' + convert(varchar(3),@Position) + ' no existe como tercero en la BD'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End
			--Print concat('employeeid= ',@EmployeeId)
			--Se actualiza el nombre y nit del tercero
			update @TableXmlObject set NitName = (select IIF(RTRIM(LTRIM(Nit))='',LTRIM(RTRIM(@Internalcode)),RTRIM(LTRIM(Nit) ) + ' - ' + RTRIM(LTRIM([Name]))) from Common.ThirdParty tp
				LEFT JOIN Payroll.Employee e on tp.id = e.ThirdPartyId where Nit = @Nit OR e.InternalCode = @Internalcode) where Id = @Id
			
			--Se valida si el tercero o su codigo interno existe como empleado
			if (select COUNT(*) from Payroll.Employee where ThirdPartyId = (select e.ThirdPartyId from Common.ThirdParty tp 
																			LEFT JOIN Payroll.Employee e on tp.id = e.ThirdPartyId
																			LEFT JOIN Payroll.Employee E2 ON E.InternalCode = E2.InternalCode
																			where tp.Nit = LTRIM(RTRIM(@Nit))OR E2.InternalCode = LTRIM(RTRIM(@Internalcode)))) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La cédula del registro ' + convert(varchar(3),@Position) + ' no existe como empleado en la BD'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End
		

			--Se asigna el id del empleado
			set @EmployeeId = (select Id from Payroll.Employee where ThirdPartyId = (select E2.ThirdPartyId from Common.ThirdParty tp
																					LEFT JOIN Payroll.Employee e on tp.id = e.ThirdPartyId
																					LEFT JOIN Payroll.Employee E2 ON E.InternalCode = E2.InternalCode
																					where Nit = RTRIM(LTRIM(@Nit))OR E2.InternalCode = LTRIM(RTRIM(@Internalcode))))
			
			
			--Se valida que el empleado no haya sido agregado con el mismo mes y año 
			if (select COUNT(*) from @TableXmlObject where EmployeeId > 0 and EmployeeId = @EmployeeId and MONTH(PayrollDate) = MONTH(@PayrollDate) and YEAR(PayrollDate) = YEAR(@PayrollDate) and ConceptCode = @ConceptCode) > 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El empleado del registro ' + convert(varchar(3),@Position) + ' ya existe en la lista con el mismo mes y año, y mismo concepto'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End
				print concat('employeeid= ', @EmployeeId)
			--Se actualiza el id del empleado
			update @TableXmlObject set EmployeeId = @EmployeeId where Id = @Id
			
			--Se valida que el empleado tenga contrato valido y activo
			if	(select COUNT(*)
				from Payroll.Employee e
				inner join Payroll.[Contract] c on c.EmployeeId = e.Id
				where e.Id = @EmployeeId and c.Valid = 1 and c.[Status] = 1) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El empleado del registro ' + convert(varchar(3),@Position) + ' no tiene contrato asociado o está retirado'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			--Se valida que la fecha de nómina no este vacio
			if LEN(@PayrollDate) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La fecha nómina del registro ' + convert(varchar(3),@Position) + ' está vacía'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End


			--Se valida que la fecha de nómina sea una fecha
			if ISDATE(@PayrollDate) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La fecha de nómina del registro ' + convert(varchar(3),@Position) + ' no tiene formato de fecha'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End
			

			update @TableXmlObject set PayrollDate = @PayrollDate where Id = @Id
			
			if LEN(@ConceptCode) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El Concepto del Registro ' + convert(varchar(3),@Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			if	(select COUNT(*)
				from Payroll.Concept where Code = @ConceptCode) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El concepto del registro ' + convert(varchar(3),@Position) + ' no existe'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			if LEN(@ConceptCode) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El Concepto del registro ' + convert(varchar(3),@Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			if	(select COUNT(*)
				from Payroll.Concept where Code = @ConceptCode AND ConceptClass = '041') = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El concepto del registro ' + convert(varchar(3),@Position) + ' no tiene la Clase de Concepto Indicada (Convenios)'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			update @TableXmlObject set ConceptCode = @ConceptCode where Id = @Id
			
			if LEN(@NoveltyType) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El Tipo de Novedad del registro ' + convert(varchar(3),@Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End


			if LEN(@NoveltyType) > 1
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El Valor del Tipo de Novedad del registro ' + convert(varchar(3),@Position) + ' es mayor al recibido'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			update @TableXmlObject set NoveltyType = @NoveltyType where Id = @Id

			if LEN(@QuoteValue) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El Valor de la Cuota del registro ' + convert(varchar(3),@Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			if @QuoteValue <= 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El Valor de la Cuota del registro ' + convert(varchar(3),@Position) + ' es menor o igual a cero'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			update @TableXmlObject set QuoteValue = @QuoteValue where Id = @Id

			if LEN(@NoveltyBalance) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El Valor del Saldo del registro ' + convert(varchar(3),@Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			if @NoveltyBalance <= 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El Valor del Saldo del registro ' + convert(varchar(3),@Position) + ' es menor o igual a cero'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			update @TableXmlObject set NoveltyBalance = @NoveltyBalance where Id = @Id

			if LEN(@NitCompany) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'El Valor del Saldo del registro ' + convert(varchar(3),@Position) + ' está vacío'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			if	(select COUNT(*)
				from Payroll.Company c
				inner join Common.ThirdParty TP on TP.Id = c.ThirdPartyId
				where TP.Nit = @NitCompany) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La Empresa del registro ' + convert(varchar(3),@Position) + ' no está creada como Empresa en Compañía'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			if	(select COUNT(*)
				from Common.ThirdParty
				where Nit = @NitCompany) = 0
			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La Empresa del registro ' + convert(varchar(3),@Position) + ' no está creada como Terceros'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End

			update @TableXmlObject set NitCompany = @NitCompany where Id = @Id

			update @TableXmlObject set InternalCode = @Internalcode where id=@Id

			
			--Se actualiza los campos con que se necesitan para armar el objeto en el formulario
			update @TableXmlObject set StatusField = 1, MessageField = 'OK'
			where Id = @Id
			

			NextFetch:
			Fetch Next From InfoItem Into @Id, @CountFields, @Nit, @PayrollDate, @ConceptCode, @NoveltyType, @QuoteValue, @NoveltyBalance, @NitCompany,@Internalcode
			continue

		End

		Close InfoItem
		Deallocate InfoItem

		--Se retorna la tabla
		select * from @TableXmlObject
		
	end try
	begin catch

		--rollback transaction
		select * from @TableXmlObject

	end catch

END