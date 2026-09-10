-- =============================================
-- Author:		Rafael Patiño
-- Create date: 07-06-2023
-- Description:	Sp que genera los registro en la tabla de control de interfaz (aplica solo para laboratorio por el momento)
-- Cliente HOMI (Sp ejecutado por canal de mirth para que los laboratorios pasen lab core una vez el medico ordene)
-- =============================================
CREATE PROCEDURE [Integrations].[Insert_LaboratoryControllerInterface_Mirth_Synch]
(
    @Identificacion as varchar(25),
	@Ingreso as varchar(10),
	@Ambito as varchar(3),
	@NumeroFolio as varchar(10),
	@IdOrdenesLaboratorio as varchar(4000)
)
AS
BEGIN
    SET NOCOUNT ON
	--variable 
	declare @IdGenerado as int

	--iniciamos transaccion
	begin tran saveLaboratoryInterfaz
	begin try 

	
	--insert cabecera
	insert into INTERCABE
	select @Identificacion,@Ingreso,iif(@NumeroFolio = '',null,@NumeroFolio) 
	
	--tomamos id generado
	set @IdGenerado = SCOPE_IDENTITY() 

	--si es intrahospitalario o ambulatorio
	if @Ambito = 'INT' begin
		insert into INTERDETA
		SELECT @IdGenerado, b.AUTO ,b.CODSERIPS,b.NUMEFOLIO,'INT',1  
		FROM dbo.SplitString(@IdOrdenesLaboratorio) a 
		inner join HCORDLABO b on convert(int,a.Value) = b.AUTO 

		update HCORDLABO set ESTSERIPS = 2, FECRECMUE = Common.GETDATE()
		where AUTO in (select convert(int,value) FROM dbo.SplitString(@IdOrdenesLaboratorio))

	end else if @Ambito ='AMB' begin
		insert into INTERDETA
		SELECT @IdGenerado, b.AUTO ,b.CODSERIPS,NULL,'AMB',1  
		FROM dbo.SplitString(@IdOrdenesLaboratorio) a 
		inner join AMBORDLAB b on convert(int,a.Value) = b.AUTO 

		update AMBORDLAB set ESTSERIPS = 2, FECRECMUE = Common.GETDATE()
		where AUTO in (select convert(int,value) FROM dbo.SplitString(@IdOrdenesLaboratorio))
	end 
	
	--confirmamos transaccion
	commit tran saveLaboratoryInterfaz
	select @IdGenerado as CodeMessage, 'Se guardo correctamente'  Message 

	end try
	begin catch
	 --controlamos error en catch y devolvemos transaccion	
	  rollback tran saveLaboratoryInterfaz
	  select '999' as CodeMessage, ERROR_MESSAGE() + ', Linea: ' + cast(ERROR_LINE() as varchar(20)) Message
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de integración ejecutado por el canal Mirth Connect para registrar y sincronizar órdenes de laboratorio hacia el sistema LIS (Lab Core) en el cliente HOMI. Recibe la identificación del paciente, el número de ingreso, el ámbito de atención (intrahospitalario INT o ambulatorio AMB) y una lista de IDs de órdenes de laboratorio; crea la cabecera de interconsulta en INTERCABE y el detalle de cada servicio CUPS en INTERDETA. Según el ámbito, actualiza el estado de las órdenes en HCORDLABO (hospitalario) o AMBORDLAB (ambulatorio) marcándolas como enviadas al laboratorio (estado 2) y registrando la fecha de recepción de muestra. Devuelve el identificador generado o el mensaje de error en caso de fallo, garantizando consistencia mediante una transacción explícita.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'PROCEDURE', @level1name = N'Insert_LaboratoryControllerInterface_Mirth_Synch';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'PROCEDURE', @level1name = N'Insert_LaboratoryControllerInterface_Mirth_Synch';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Registra en las tablas de control de interfaz la cabecera y detalle de las órdenes de laboratorio enviadas a LabCore, y marca las órdenes origen como enviadas según el ámbito (hospitalario o ambulatorio).', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'Insert_LaboratoryControllerInterface_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El listado de Ids de órdenes debe ser parseable a entero por dbo.SplitString; El ámbito debe ser ''INT'' o ''AMB''; otros valores no producen inserción de detalle ni actualización; Los AUTO referenciados deben existir en HCORDLABO (INT) o AMBORDLAB (AMB) para ser incluidos vía inner join; Debe existir la función Common.GETDATE() y dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'Insert_LaboratoryControllerInterface_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la operación se ejecuta bajo una transacción nombrada (saveLaboratoryInterfaz); ante cualquier error se hace rollback completo; Las órdenes enviadas siempre quedan con ESTSERIPS = 2 y FECRECMUE con la fecha del servidor (Common.GETDATE()); Los detalles de INTERDETA siempre se vinculan al Id de cabecera generado por SCOPE_IDENTITY(); Solo se procesan órdenes cuyo AUTO exista en la tabla de origen correspondiente al ámbito (inner join filtra inexistentes); El procedimiento siempre devuelve un resultset con CodeMessage y Message (éxito con id, o ''999'' con mensaje de error y línea)', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'Insert_LaboratoryControllerInterface_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de laboratorio; Interfaz de laboratorio (LabCore); Ámbito intrahospitalario; Ámbito ambulatorio; Recepción de muestra; CUPS (servicios); Folio; Ingreso del paciente', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'Insert_LaboratoryControllerInterface_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.INTERCABE: Siempre se inserta una cabecera con identificación, ingreso y folio (NULL si el folio recibido es cadena vacía); [INSERT] dbo.INTERDETA: Cuando ámbito = ''INT'', inserta un detalle por cada orden encontrada en HCORDLABO con tipo ''INT'', estado 1 y folio de la orden; [INSERT] dbo.INTERDETA: Cuando ámbito = ''AMB'', inserta un detalle por cada orden encontrada en AMBORDLAB con tipo ''AMB'', estado 1 y folio NULL; [UPDATE] dbo.HCORDLABO: Cuando ámbito = ''INT'', actualiza ESTSERIPS = 2 y FECRECMUE = Common.GETDATE() para los AUTO recibidos en la lista; [UPDATE] dbo.AMBORDLAB: Cuando ámbito = ''AMB'', actualiza ESTSERIPS = 2 y FECRECMUE = Common.GETDATE() para los AUTO recibidos en la lista; [RETURN_RESULT] (resultset): En éxito devuelve el Id generado como CodeMessage y ''Se guardo correctamente''; en error devuelve ''999'' con ERROR_MESSAGE() y la línea', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'Insert_LaboratoryControllerInterface_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Ámbito = ''INT'' (intrahospitalario) → Inserta detalle desde HCORDLABO con número de folio y actualiza HCORDLABO marcando estado 2 y fecha de recepción de muestra else Si ámbito = ''AMB'' (ambulatorio), inserta detalle desde AMBORDLAB con folio NULL y actualiza AMBORDLAB con estado 2 y fecha de recepción; si NumeroFolio es cadena vacía '''' → Se inserta NULL en la cabecera en lugar del folio else Se inserta el folio recibido', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'Insert_LaboratoryControllerInterface_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'Insert_LaboratoryControllerInterface_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.AMBORDLAB; dbo.SplitString', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'Insert_LaboratoryControllerInterface_Mirth_Synch';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'Insert_LaboratoryControllerInterface_Mirth_Synch';
-- GO
