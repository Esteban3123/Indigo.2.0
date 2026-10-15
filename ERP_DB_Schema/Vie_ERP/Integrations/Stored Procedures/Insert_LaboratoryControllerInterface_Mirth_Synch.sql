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