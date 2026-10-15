

-- ====================================================================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 14/01/2019
-- Description:	Store procedure que se encarga de reversar el proceso de RIAS, se elimina el registro futuro y se nulean los campos que se asignan cuando se inscribe
-- ====================================================================================================================================================================
CREATE PROCEDURE [dbo].[SP_RIASAnnulment]
	@Xml as xml,
	@PatientCode as varchar(25),
	@UserCode as varchar(20),

	@CodeMessage varchar(20) output, 
	@Message varchar(max) output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON

	--Tabla temporal para obtener los datos del xml
	declare @TableXml table(CupsCode varchar(20), RIASCupsId int, Processed bit)
	
	begin try
		
		--Se insertan los registros que vienen del xml
		insert into @TableXml
		select 
			t.x.value('CupsCode[1]','varchar(20)') as CupsCode,
			t.x.value('RIASCupsId[1]','int') as RIASCupsId,
			0 as Processed
		from @Xml.nodes('/RIASForPatient') t(x)
				
		--Se obtiene la cantidad de registros que se van a iterar
		declare @Count int = (select count(*) from @TableXml x where x.Processed = 0)

		--Se recorren los cups que se eliminaron de la rejilla y que manejen rias
		while @Count > 0
		begin
			--Variables para obtener la información del xml
			declare @CupsCode varchar(20), @RIASCupsId int
			
			--Se otienen los campos necesarios de la tabla temporal xml
			select top 1 @CupsCode = ta.CupsCode, @RIASCupsId = ta.RIASCupsId
			From @TableXml ta
			Where ta.Processed = 0
			
			--Se elimina el registro de la actividad futura
			delete from dbo.RIASCUPSPACIENTE where IPCODPACI = @PatientCode and IDRIASCUPS = @RIASCupsId and CODSERIPS = @CupsCode and ESTADO = 1

			--Se obtiene el id del registro de la inscripción
			declare @InscriptionId int = (select top 1 ID 
			from dbo.RIASCUPSPACIENTE 
			where IPCODPACI = @PatientCode and IDRIASCUPS = @RIASCupsId and CODSERIPS = @CupsCode and ESTADO = 2
			order by ID desc)

			--Se actualizan los campos que se realizó en la inscripción
			update dbo.RIASCUPSPACIENTE set ESTADO = 1, NUMINGRES = null, FECHAREALIZACION = null, IDDETALLEORDENSERVICIO = null, VALORCUPS = null, CODPROSAL = null where ID = @InscriptionId

			--Se actualiza el estado Processed para evitar que siga en el loop
			update @TableXml set Processed = 1 where CupsCode = @CupsCode and RIASCupsId = @RIASCupsId

			--Se disminuye el contador
			set @Count -= 1
		end
			
		set @CodeMessage = '0'
		set @Message = 'Se realizó el proceso de anulación correctamente'
		return		
	end try
	begin catch
		set @CodeMessage = '999'
		set @Message = (select ERROR_MESSAGE())
		return
	end catch

END