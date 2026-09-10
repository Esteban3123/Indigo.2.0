

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
			
			--Se actualiza el nombre y nit del tercero
			update @TableXmlObject set NitName = (
			select TOP 1 LTRIM(RTRIM(tp.Nit)) + ' - ' + LTRIM(RTRIM(tp.[Name])) 
			from Common.ThirdParty tp
			INNER JOIN Payroll.Employee e on tp.id = e.ThirdPartyId
			where (LEN(@Nit) > 0 AND tp.Nit = @Nit) 
			OR (LEN(@Nit) = 0 AND e.InternalCode = @Internalcode)
			) where Id = @Id
			
			
			--Se valida si el tercero o su codigo interno existe como empleado
			if (select COUNT(*) from Common.ThirdParty tp
						INNER JOIN Payroll.Employee e on tp.id = e.ThirdPartyId
						where (LEN(@Nit) > 0 AND tp.Nit = LTRIM(RTRIM(@Nit)))
						OR (LEN(@Nit) = 0 AND LEN(@Internalcode) > 0 AND e.InternalCode = LTRIM(RTRIM(@Internalcode)))) = 0

			Begin
				--Se actualiza los campos con el estado en false y el mensaje de error
				update @TableXmlObject set StatusField = 0, MessageField = 'La cédula del registro ' + convert(varchar(3),@Position) + ' no existe como empleado en la BD'
				where Id = @Id			
				--Se pasa a la siguiente posicion del cursor
				GoTo NextFetch
			End
		

			--Se asigna el id del empleado
			set @EmployeeId = (
				SELECT TOP 1 e.Id 
				FROM Common.ThirdParty tp
				INNER JOIN Payroll.Employee e on tp.id = e.ThirdPartyId
				WHERE tp.Nit = LTRIM(RTRIM(@Nit))
    
				UNION ALL
    
				SELECT TOP 1 e.Id 
				FROM Payroll.Employee e
				WHERE LEN(LTRIM(RTRIM(@Nit))) = 0 
				  AND e.InternalCode = LTRIM(RTRIM(@Internalcode))
				  AND LEN(LTRIM(RTRIM(@Internalcode))) > 0
			)
			
			
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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importa y valida un archivo de Excel (enviado como XML) que contiene novedades de convenios o créditos de nómina para empleados. Recorre cada fila del XML, verificando que el NIT o código interno del empleado exista en la tabla de terceros (Common.ThirdParty) y en la tabla de empleados (Payroll.Employee), y que cada registro tenga la estructura de columnas requerida. Para cada fila válida asigna el identificador del empleado y enriquece el registro con el nombre del tercero; si encuentra errores de estructura, cédula vacía o empleado inexistente, marca la fila con estado fallido y un mensaje descriptivo. El resultado es un conjunto de registros procesados con indicadores de éxito o error, usado para la carga masiva de descuentos de convenios (como libranzas o créditos) en el módulo de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ImportFileAgreementsC';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'PROCEDURE', @level1name = N'SP_ImportFileAgreementsC';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida fila a fila un archivo (en XML) de novedades de convenios para nómina, verificando estructura, existencia de tercero/empleado, contrato vigente, concepto de clase Convenios (041), fecha, valores y empresa, y devuelve el resultado marcado como OK o con mensaje de error.', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileAgreementsC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe seguir la estructura /Data/Row con los nodos CountFields, StatusField, MessageField, Nit, NitName, EmployeeId, PayrollDate, ConceptCode, ConceptId, NoveltyType, QuoteValue, NoveltyBalance, NitCompany e InternalCode; Cada fila debe traer 8 campos (CountFields=8) para considerarse estructuralmente válida; Las tablas Common.ThirdParty, Payroll.Employee, Payroll.Contract, Payroll.Concept y Payroll.Company deben estar pobladas con los datos maestros referenciados (terceros, empleados con contrato vigente, conceptos de clase ''041'' y empresas)', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileAgreementsC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila procesada termina con StatusField=1 y MessageField=''OK'', o con StatusField=0 y un mensaje describiendo el primer error encontrado; Una fila válida exige exactamente 8 columnas (CountFields=8); Solo se aceptan conceptos cuya ConceptClass sea ''041'' (Convenios); QuoteValue y NoveltyBalance deben ser estrictamente mayores que cero; El empleado debe tener al menos un contrato con Valid=1 y Status=1; No se permiten dos filas para el mismo empleado, mismo mes/año de PayrollDate y mismo ConceptCode dentro del mismo lote; El procedimiento no escribe en tablas físicas: solo construye y devuelve una tabla en memoria con resultados de validación; Ante cualquier excepción (CATCH), se devuelve igualmente el contenido validado parcialmente sin propagar el error', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileAgreementsC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nómina; Empleado; Contrato laboral; Tercero; Concepto de nómina; Clase de concepto Convenios (041); Tipo de novedad; Cuota; Saldo de novedad; Empresa empleadora; Carga masiva desde Excel/XML; Libranzas/Créditos/Convenios', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileAgreementsC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si CountFields <> 8 → Marca StatusField=0 con mensaje ''no tiene la estructura requerida'' y omite el resto de validaciones de la fila; si LEN(@Nit)=0 AND LEN(@InternalCode)=0 → Marca la fila como inválida (''cédula y código interno vacíos'') y salta a la siguiente; si No existe el Nit en Common.ThirdParty ni el InternalCode en Payroll.Employee → Marca la fila como inválida (''no existe como tercero en la BD''); si El tercero/código interno no corresponde a un Payroll.Employee → Marca la fila como inválida (''no existe como empleado en la BD''); si Ya existe en el lote (@TableXmlObject) otra fila con mismo EmployeeId, mismo mes/año de PayrollDate y mismo ConceptCode → Marca duplicado en la lista con mismo mes/año y concepto; si El empleado no tiene un Payroll.Contract con Valid=1 y Status=1 → Marca la fila como inválida (''no tiene contrato asociado o está retirado''); si PayrollDate vacío o ISDATE(@PayrollDate)=0 → Marca la fila como inválida por fecha vacía o sin formato de fecha; si ConceptCode vacío o no existe en Payroll.Concept → Marca la fila como inválida (concepto vacío o inexistente); si Concepto existe pero Payroll.Concept.ConceptClass <> ''041'' → Marca la fila como inválida (''no tiene la Clase de Concepto Indicada (Convenios)''); si NoveltyType vacío o LEN > 1 → Marca la fila como inválida (tipo de novedad vacío o mayor al recibido); si QuoteValue vacío o <= 0 → Marca la fila como inválida (valor de cuota vacío o menor/igual a cero); si NoveltyBalance vacío o <= 0 → Marca la fila como inválida (valor del saldo vacío o menor/igual a cero); si NitCompany vacío, o no existe Payroll.Company asociada a un ThirdParty con ese Nit, o no existe en Common.ThirdParty → Marca la fila como inválida (empresa no creada como Empresa/Tercero); si Todas las validaciones anteriores pasan → Marca la fila con StatusField=1 y MessageField=''OK''', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileAgreementsC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Common.ThirdParty; Payroll.Employee; Payroll.Contract; Payroll.Concept; Payroll.Company', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileAgreementsC';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Payroll', @level1type=N'PROCEDURE', @level1name=N'SP_ImportFileAgreementsC';
-- GO
