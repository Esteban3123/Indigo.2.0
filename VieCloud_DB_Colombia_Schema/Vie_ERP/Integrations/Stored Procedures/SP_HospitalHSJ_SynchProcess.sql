-- =============================================
-- Author:		Rafael Patiño
-- Create date: 29-06-2023
-- Description:	Sp que genera los registro en la tabla de paciente, ingreso y control de consulta externa
-- Cliente San Jose Colombia (Sp ejecutado por canal de mirth local en San jose interfaz servinte)
-- =============================================
CREATE PROCEDURE [Integrations].[SP_HospitalHSJ_SynchProcess]
(
   @Json NVARCHAR(MAX)
)
AS
BEGIN
    SET NOCOUNT ON

	  	--DECLARE @JsonPrueba NVARCHAR(MAX)
 --   SET @JsonPrueba = N'[
	--				  {
	--					"id": 3446986,
	--					"NRO_CARGO": 3446986,
	--					"ID_CITA_SERVINTE": 3446986,
	--					"TIPO_DOCUMENTO": "CC",
	--					"NUMERO_DOCUMENTO": 3446986,
	--					"NRO_UNICO_ID_SERVINTE": "1000000254",
	--					"PRIMER_NOMBRE": "EMILIA",
	--					"SEGUNDO_NOMBRE": "MILENA",
	--					"PRIMER_APELLIDO": "LANCHEROS",
	--					"SEGUNDO_APELLIDO": "MAYORGA",
	--					"FECHA_NACIMEINTO": "1990-04-24T00:00:00",
	--					"TELEFONO": "3225197142",
	--					"DIRECCION": "CLL 73 SUR 78 C 12",
	--					"SEXO": "F",
	--					"COD_RESPONSABLE": "CCFC50",
	--					"CEDULA_MEDICO": "1079183992",
	--					"COD_ESPECIALIDAD": "021",
	--					"SERVICIO_CUPS": "010101",
	--					"FECHA_HORA_CITA": "2023-06-30 10:00:00",
	--					"DURACION": 20,
	--					"TIPO_CITA": 1,
	--					"AUTORIZACION": 101213,
	--				    "TIPOCITASERVINTEC": 'C'
	--				  }
	--				]';

	--variables 
	declare @IdCita as varchar(25)
	declare @FechaProceso as datetime = [Common].[GETDATE]()
	DECLARE @COMENTARIOINTEGRACION AS VARCHAR(25) = 'INTEGRACION MIRTH'
	set @IdCita = (SELECT top 1 ID_CITA_SERVINTE FROM OPENJSON(@Json, N'$')WITH (ID_CITA_SERVINTE VARCHAR(200) N'$.ID_CITA_SERVINTE'))
	

	begin try 

 

	   declare @tablaCitas as table( id int identity(1,1)
								,NRO_CARGO VARCHAR(200)
								,ID_CITA_SERVINTE VARCHAR(200) 
								,TIPO_DOCUMENTO VARCHAR(200) 
								,NUMERO_DOCUMENTO VARCHAR(200) 
								,NRO_UNICO_ID_SERVINTE VARCHAR(200) 
								,PRIMER_NOMBRE VARCHAR(200) 
								,SEGUNDO_NOMBRE VARCHAR(200) 
								,PRIMER_APELLIDO VARCHAR(200) 
								,SEGUNDO_APELLIDO VARCHAR(200) 
								,FECHA_NACIMEINTO DATETIME 
								,TELEFONO VARCHAR(200) 
								,DIRECCION VARCHAR(200)
								,SEXO VARCHAR(200)
								,COD_RESPONSABLE VARCHAR(200) 
								,CEDULA_MEDICO VARCHAR(200) 
								,COD_ESPECIALIDAD VARCHAR(200) 
								,SERVICIO_CUPS VARCHAR(200) 
								,FECHA_HORA_CITA DATETIME 
								,DURACION VARCHAR(200) 
								,TIPO_CITA VARCHAR(200) 
								,AUTORIZACION VARCHAR(200)
								,CODIGO_CONSULTORIO VARCHAR(200)
								,ES_POS_OPERATORIO VARCHAR(200)
								,TIPOCITA_SERVINTEC VARCHAR(200))
								
    --tabla para los datos del paciente nuevo
	--declare @tablaPacienteNuevos as table( id int identity(1,1), NUMERO_DOCUMENTO varchar(25), TIPODOCUMENTO varchar(20))

	--cargamos tablas apartir del json   
    INSERT INTO @tablaCitas 
		SELECT 
		    NRO_CARGO 
			,ID_CITA_SERVINTE 
			,TIPO_DOCUMENTO 
			,NUMERO_DOCUMENTO 
			,NRO_UNICO_ID_SERVINTE 
			,PRIMER_NOMBRE 
			,SEGUNDO_NOMBRE 
			,PRIMER_APELLIDO 
			,SEGUNDO_APELLIDO 
			,FECHA_NACIMEINTO 
			,TELEFONO 
			,DIRECCION 
			,SEXO 
			,COD_RESPONSABLE 
			,CEDULA_MEDICO 
			,COD_ESPECIALIDAD 
			,SERVICIO_CUPS 
			,FECHA_HORA_CITA 
			,DURACION 
			,TIPO_CITA 
			,AUTORIZACION
			,CODIGO_CONSULTORIO
			,ES_POS_OPERATORIO
			,TIPOCITA_SERVINTEC
		FROM OPENJSON(@Json, N'$') WITH (
			 NRO_CARGO VARCHAR(200) N'$.NRO_CARGO'
			,ID_CITA_SERVINTE VARCHAR(200) N'$.ID_CITA_SERVINTE'
			,TIPO_DOCUMENTO VARCHAR(200) N'$.TIPO_DOCUMENTO'
			,NUMERO_DOCUMENTO VARCHAR(200) N'$.NUMERO_DOCUMENTO'
			,NRO_UNICO_ID_SERVINTE VARCHAR(200) N'$.NRO_UNICO_ID_SERVINTE'
			,PRIMER_NOMBRE VARCHAR(200) N'$.PRIMER_NOMBRE'
			,SEGUNDO_NOMBRE VARCHAR(200) N'$.SEGUNDO_NOMBRE'
			,PRIMER_APELLIDO VARCHAR(200) N'$.PRIMER_APELLIDO'
			,SEGUNDO_APELLIDO VARCHAR(200) N'$.SEGUNDO_APELLIDO'
			,FECHA_NACIMEINTO DATETIME N'$.FECHA_NACIMEINTO'
			,TELEFONO VARCHAR(200) N'$.TELEFONO'
			,DIRECCION VARCHAR(200) N'$.DIRECCION'
			,SEXO VARCHAR(200) N'$.SEXO'
			,COD_RESPONSABLE VARCHAR(200) N'$.COD_RESPONSABLE'
			,CEDULA_MEDICO VARCHAR(200) N'$.CEDULA_MEDICO'
			,COD_ESPECIALIDAD VARCHAR(200) N'$.COD_ESPECIALIDAD'
			,SERVICIO_CUPS VARCHAR(200) N'$.SERVICIO_CUPS'
			,FECHA_HORA_CITA DATETIME N'$.FECHA_HORA_CITA'
			,DURACION VARCHAR(200) N'$.DURACION'
			,TIPO_CITA VARCHAR(200) N'$.TIPO_CITA'
			,AUTORIZACION VARCHAR(200) N'$.AUTORIZACION'
			,CODIGO_CONSULTORIO VARCHAR(200) N'$.CODIGO_CONSULTORIO'
			,ES_POS_OPERATORIO VARCHAR(200) N'$.ES_POS_OPERATORIO'
			,TIPOCITA_SERVINTEC VARCHAR(200) N'$.TIPOCITA_SERVINTEC'			
			) AS CitasData;

			--select * from @tablaCitas 
			
			declare @CodigoEntidad as varchar(100) = (select top 1 ent.code from @tablaCitas a left join
									integrations.HospitalHSJ_Entidades H on a.COD_RESPONSABLE = h.LegacyCode left join
									Contract.HealthAdministrator ent on ent.Code = H.IndigoCode)

	        declare @IdCareGroup as int = (select top 1 isnull(care.Id,12) as IdCareGroup
									from Contract.HealthAdministrator e left join 
									Contract.Contract c on c.HealthAdministratorId = e.id left join 
									Contract.CareGroup care on care.ContractId = C.Id 
									 where e.Code = @CodigoEntidad)

			declare @validacion  Nvarchar(max)=''
			--actualizamos la entidad PARTICULAR al codigo 999 e indigo
			update @tablaCitas set COD_RESPONSABLE = '999' where COD_RESPONSABLE = 'PARTICULAR'

	

			--select @IdCareGroup, @CodigoEntidad
	
			/***************Validamos Consultorios**********************************/
			if not exists(select * from Integrations.HospitalHSJ_Consultorios E inner join	@tablaCitas a  on E.LegacyCode =a.CODIGO_CONSULTORIO)
			BEGIN
				select @validacion = concat(@validacion, STRING_AGG(a.CODIGO_CONSULTORIO,',')) from @tablaCitas a
				insert into Integrations.HospitalHSJ_LogMirths
				select @IdCita,
					   @FechaProceso,
					  'Los siguientes consultorios no existe en la tabla homologos - Integrations.HospitalHSJ_Consultorios' + char(13) + char(10) + @validacion Message,
					   @Json
			    select 999 as CodeMessage, 'Los siguientes consultorios no existe en la tabla homologos - Integrations.HospitalHSJ_Consultorios' + char(13) + char(10) + @validacion Message
				set @validacion = ''
				return
			END

			
			select @validacion = concat(@validacion, STRING_AGG(H.legacyName,',')) from @tablaCitas a inner join 
				Integrations.HospitalHSJ_Consultorios h on a.CODIGO_CONSULTORIO = h.LegacyCode 
            where Not Exists (select 1 from dbo.AGCONSULT ENT where ENT.CODIGOCON = h.IndigoCode )
			
			if @validacion <> ''			
			begin
				insert into Integrations.HospitalHSJ_LogMirths
				select @IdCita,
					   @FechaProceso,
					  'Los siguientes consultorios no estan homologadas: ' + char(13) + char(10) + @validacion Message,
					   @Json
				select 999 as CodeMessage, 'Los siguientes consultorios no estan homologadas: ' + char(13) + char(10) + @validacion Message
                set @validacion = ''
				return
			end

			/***************Validamos Entidades**********************************/
			if not exists(select * from Integrations.HospitalHSJ_Entidades E inner join	@tablaCitas a  on E.LegacyCode =a.COD_RESPONSABLE)
			BEGIN
				select @validacion = concat(@validacion, STRING_AGG(a.COD_RESPONSABLE,',')) from @tablaCitas a
				insert into Integrations.HospitalHSJ_LogMirths
				select @IdCita,
					   @FechaProceso,
					  'Las siguientes entidades no existe en la tabla homologos - Integrations.HospitalHSJ_Entidades' + char(13) + char(10) + @validacion Message,
					   @Json
			    select 999 as CodeMessage, 'Las siguientes entidades no existe en la tabla homologos - Integrations.HospitalHSJ_Entidades' + char(13) + char(10) + @validacion Message
				set @validacion = ''
				return
			END

			
			select @validacion = concat(@validacion, STRING_AGG(H.legacyName,',')) from @tablaCitas a inner join 
				Integrations.HospitalHSJ_Entidades h on a.COD_RESPONSABLE = h.LegacyCode 
            where Not Exists (select 1 from contract.HealthAdministrator ENT where ENT.Code = h.IndigoCode )
			
			if @validacion <> ''			
			begin
				insert into Integrations.HospitalHSJ_LogMirths
				select @IdCita,
					   @FechaProceso,
					  'Las siguientes entidades no estan homologadas: ' + char(13) + char(10) + @validacion Message,
					   @Json
				select 999 as CodeMessage, 'Las siguientes entidades no estan homologadas: ' + char(13) + char(10) + @validacion Message
                set @validacion = ''
				return
			end
			/***************FIN Validamos Entidades**********************************/

			/***************Validamos Profesionales**********************************/
			if not exists(select * from Integrations.HospitalHSJ_Profesionales E inner join	@tablaCitas a  on E.LegacyCode =a.CEDULA_MEDICO)
			BEGIN
				select @validacion = concat(@validacion, STRING_AGG(a.CEDULA_MEDICO,',')) from @tablaCitas a
				insert into Integrations.HospitalHSJ_LogMirths
				select @IdCita,
					   @FechaProceso,
					  'Los siguintes medicos no existe en la tabla homologos - Integrations.HospitalHSJ_Profesionales: ' + char(13) + char(10) + @validacion Message,
					   @Json
			    select 999 as CodeMessage, 'Los siguintes medicos no existe en la tabla homologos - Integrations.HospitalHSJ_Profesionales: ' + char(13) + char(10) + @validacion Message
				set @validacion = ''
				return
			END

			select @validacion = concat(@validacion, STRING_AGG(H.legacyName,',')) from @tablaCitas a inner join 
				Integrations.HospitalHSJ_Profesionales h on a.CEDULA_MEDICO = h.LegacyCode 
            where Not Exists (select 1 from INPROFSAL P where P.CODPROSAL = h.IndigoCode )
			
			if @validacion <> ''			
			begin
				insert into Integrations.HospitalHSJ_LogMirths
				select @IdCita,
					   @FechaProceso,
					  'Los siguientes profesionales no estan homologados: ' + char(13) + char(10) + @validacion Message,
					   @Json
				select 999 as CodeMessage, 'Los siguientes profesionales no estan homologados: ' + char(13) + char(10) + @validacion Message
				set @validacion = ''
				return
			end
			/***************FIN Validamos Profesionales**********************************/

			/***************Validamos Especialidades**********************************/
			if not exists(select * from Integrations.HospitalHSJ_Especialidades E inner join	@tablaCitas a  on E.LegacyCode =a.COD_ESPECIALIDAD)
			BEGIN
				select @validacion = concat(@validacion, STRING_AGG(a.COD_ESPECIALIDAD,',')) from @tablaCitas a
				insert into Integrations.HospitalHSJ_LogMirths
				select @IdCita,
					   @FechaProceso,
					  'Las siguientes especialidades no existe en la tabla homologos - Integrations.HospitalHSJ_Especialidades' + char(13) + char(10) + @validacion Message,
					   @Json
			    select 999 as CodeMessage, 'Las siguientes especialidades no existe en la tabla homologos - Integrations.HospitalHSJ_Especialidades' + char(13) + char(10) + @validacion Message
				set @validacion = ''
				return
			END

			select @validacion = concat(@validacion, STRING_AGG(H.legacyName,',')) from @tablaCitas a inner join 
				Integrations.HospitalHSJ_Especialidades h on a.COD_ESPECIALIDAD = h.LegacyCode 
            where Not Exists (select 1 from INESPECIA ESP where ESP.CODESPECI = h.IndigoCode )
			
			if @validacion <> ''			
			begin
				insert into Integrations.HospitalHSJ_LogMirths
				select @IdCita,
					   @FechaProceso,
					  'Los siguientes Especialidades no estan homologados: ' + char(13) + char(10) + @validacion Message,
					   @Json
				select 999 as CodeMessage, 'Los siguientes Especialidades no estan homologados: ' + char(13) + char(10) + @validacion Message
                set @validacion = ''
				return
			end
			/***************FIN Validamos Especialidades**********************************/

			--si pasa todas la validacicones iniciamos transaccion
			--begin tran saveInterfaz

			--Insertamos pacientes que no estan en indigo y si viene en la cita
			--insert into @tablaPacienteNuevos
			--select a.NUMERO_DOCUMENTO, a.TIPO_DOCUMENTO from @tablaCitas a
   --         where Not Exists (select 1 from INPACIENT p where  p.IPCODPACI = a.NUMERO_DOCUMENTO )

			declare @IdentificacionCrear varchar(25) = (select top 1 NUMERO_DOCUMENTO from @tablaCitas)

			--select a.NUMERO_DOCUMENTO, a.TIPO_DOCUMENTO from @tablaCitas a
   --         where Not Exists (select 1 from INPACIENT p inner join 
			--								ADTIPOIDENTIFICA t on p.IPTIPODOC = t.Codigo 
			--							where  p.IPCODPACI = a.NUMERO_DOCUMENTO and t.SIGLA = a.TIPO_DOCUMENTO )

			update @tablaCitas set DIRECCION ='BOGOTA' where DIRECCION is null or DIRECCION = '' or DIRECCION = ' '

			IF not exists(select 1 from DBO.INPACIENT where IPCODPACI = @IdentificacionCrear) 
			BEGIN
						print 'crear nuevo paciente'
					
						INSERT DBO.INPACIENT (IPCODPACI, IPTIPODOC, CODIGONIT, IPEXPEDIC,
									IPPRIAPEL, IPSEGAPEL, IPPRINOMB, IPSEGNOMB,
									IPNOMCOMP, CODEMPRES, IPTIPOPAC, IPTIPOAFI,
									CAPACIPAG, CODENTIDA, CCCONTRAT, CPPLANBEN, 
									AUUBICACI, NIVCODIGO, IPDIRECCI, IPTELEFON,
									IPTELMOVI, IPFECNACI, CODACTIVI, IPSEXOPAC,
									IPESTADOC, IPGRUPSAN, IPRHSANGR, TIPCOBSAL,
									CORELEPAC, CODGRUPOE, ESTADOPAC, OBSERVACI,
									INDAUDFOR, PACIEFOTO, PACIEHUELL, NUMCARPET,
									CODUSUCRE, FECREGCRE, CODUSUMOD, FECREGMOD,
									IPESTRATO, CREDCODIGO, DISCCODIGO, IDICODIGO,
									NIVECODIGO, GRUPCODIGO, ZONAPARTADA, GENCAREGROUP, 
									GENCONENTITY, GENEXPEDITIONCITY, IPORIENTSEXUAL, IPIDENTSEXUAL,
									IPORIENTSEXOTRO, IPIDENTSEXOTRO, PESO, IPSEXO,
									IDENTMAMA, IDENTOBSERVAC, IDPAIS, PACIEINVESTIGACION, 
                                    PACIENTEDATOSBASICO)
							SELECT 
									  CAST(
										a.NUMERO_DOCUMENTO AS VARCHAR(25)
									  ), 
									  CASE a.TIPO_DOCUMENTO WHEN 'CC' THEN '1' WHEN 'CE' THEN '2' WHEN 'CN' THEN '9' WHEN 'PE' THEN '12' WHEN 'PT' THEN '13' WHEN 'RC' THEN '4' WHEN 'TI' THEN '3' ELSE '15' END TIPO, 
									  replicate('0',(18 - len(a.NUMERO_DOCUMENTO))) + convert(varchar, a.NUMERO_DOCUMENTO), 
									  '' as LugarExpedicion, 
									  SUBSTRING (a.PRIMER_APELLIDO, 0, 20 ), 
									  SUBSTRING (a.SEGUNDO_APELLIDO, 0, 20 ), 
									  SUBSTRING(a.PRIMER_NOMBRE, 0 , 20), 
									  a.SEGUNDO_NOMBRE, 
									  RTRIM(a.PRIMER_NOMBRE) + ' ' + RTRIM(a.SEGUNDO_NOMBRE) + ' ' + RTRIM(a.PRIMER_APELLIDO) + ' ' + RTRIM(a.SEGUNDO_APELLIDO), 
									  '9999' as CodigoEmpresaLabora, 
									  '5' as TIpoPaciente, --otros
									  '0' as TipoAfiliad ,  --no aplica 
									  '0' as CapacidadPago,  --no aplica 
									  Ent.code as Entidad, 
									  NULL, 
									  NULL, 
									  '11001001100' as Ubicacion, 
									  '04' as NivelCodigo, 
									  isnull(a.DIRECCION,'BOGOTA'), 
									  a.TELEFONO, 
									  a.TELEFONO, 
									  a.FECHA_NACIMEINTO, 
									  '9999' as CodigoActividad, 
									  ISNULL(CASE a.SEXO WHEN 'F' THEN '2' WHEN 'M' THEN '1' END,1) SEXO, 
									  '1', 
									  NULL, 
									  NULL, 
									  '8', --otros
									  '', 
									  NULL, 
									  '1' as EstadoPac, 
									  @COMENTARIOINTEGRACION as Obsrevacion, 
									  '0' as INDAUDFOR, 
									  NULL, 
									  NULL, 
									  a.NUMERO_DOCUMENTO as Numcarpeta, 
									  '3' as UsuarioCreacion, 
									  @FechaProceso, 
									  NULL, 
									  NULL, 
									  0 as Estrato, 
									  NULL, 
									  NULL, 
									  NULL, 
									  NULL, 
									  NULL, 
									  0 as ZONAAPARTADA, 
									  isnull(@IdCareGroup,'12') as GrupoAtencion, 
									  Ent.id as Entidad, 
									  '149' as CiudadExpedicion, 
									  NULL, 
									  NULL, 
									  NULL, 
									  NULL, 
									  NULL, 
									  NULL, 
									  NULL, 
									  '', 
									  '1' as IdPais, 
									  '0', 
									  '0'
								FROM @tablaCitas a inner join
									 integrations.HospitalHSJ_Entidades H on a.COD_RESPONSABLE = h.LegacyCode inner join
									 Contract.HealthAdministrator ent on ent.Code = H.IndigoCode where NUMERO_DOCUMENTO = @IdentificacionCrear 					
									 
			END  --fin crear nuevo paciente
			ELSE 
			BEGIN --si existe paciente,actualimos datos paciente
				print 'actualizapaciente'
				update inpacient 
				set 
					IPPRIAPEL = SUBSTRING (a.PRIMER_APELLIDO, 0, 20 ), 
					IPSEGAPEL = SUBSTRING (a.SEGUNDO_APELLIDO, 0, 20 ),
					IPPRINOMB = SUBSTRING (a.PRIMER_NOMBRE, 0, 20 ), 
					IPSEGNOMB = a.SEGUNDO_NOMBRE,
					IPNOMCOMP = RTRIM(a.PRIMER_NOMBRE) + ' ' + RTRIM(a.SEGUNDO_NOMBRE) + ' ' + RTRIM(a.PRIMER_APELLIDO) + ' ' + RTRIM(a.SEGUNDO_APELLIDO) ,
					CODENTIDA = Ent.Code ,					
					GENCONENTITY = Ent.Id,
					GENCAREGROUP =isnull(@IdCareGroup,'12'),
					NIVECODIGO = isnull(NIVECODIGO,'04'), --otros
					IPTIPOPAC = isnull(IPTIPOPAC,5), --otros
					TIPCOBSAL = isnull(TIPCOBSAL,8), --otros
					IPDIRECCI = a.DIRECCION 
				from @tablaCitas a inner join INPACIENT p on a.NUMERO_DOCUMENTO = p.IPCODPACI  inner join 
					Integrations.HospitalHSJ_Entidades H on a.COD_RESPONSABLE = h.LegacyCode inner join 
					Contract.HealthAdministrator ent on ent.Code = H.IndigoCode where IPCODPACI =  @IdentificacionCrear 
			END
		
			
				----CREAR PERSON-----
			IF not exists (select 1 from Common.Person where IdentificationNumber = @IdentificacionCrear)
			BEGIN
				INSERT Common.Person
				SELECT A.NUMERO_DOCUMENTO IdentificationNumber,CASE A.TIPO_DOCUMENTO WHEN 'CC' THEN 0 WHEN 'CE' THEN 1 WHEN 'TI' THEN 2 WHEN 'RC' THEN 3 WHEN 'PA' THEN 4 WHEN 'PE' THEN 12 WHEN 'CN' THEN 9
				WHEN 'PT' THEN 12 ELSE 0 END IdentificationType, 149 IdentificacionCityId,NULL IdentificationExpeditionDate,NULL MilitaryCardId,NULL MilitaryCardNumber,
				PRIMER_NOMBRE FirstName,SEGUNDO_NOMBRE SecondName,PRIMER_APELLIDO FirstLastName,SEGUNDO_APELLIDO SecondLastName,FECHA_NACIMEINTO BirthDate,149 BirthCityId,
				NULL DeathDate,CASE SEXO WHEN 'F' THEN 2 WHEN 'M' THEN '1' END  Gender,NULL BloodGroup,NULL RH,NULL Fingerprint,NULL SonNumber,NULL Dependents,NULL MaritalStatus,1 State,
				NULL HousingType,NULL SocioEconomicStatus,NULL CigaretteConsumption,NULL SportPractice,NULL EthnicGroupId,NULL ReligiousBeliefsId,NULL Weight,NULL ShirtSize,NULL PantSize,NULL ShoeSize, NULL as identificationTypeId
				FROM  @tablaCitas a where NUMERO_DOCUMENTO = @IdentificacionCrear 
			END	
			ELSE 
			BEGIN
				update  Common.Person 
				set 
					FirstLastName = a.PRIMER_APELLIDO, 
					SecondLastName = a.SEGUNDO_APELLIDO,
					SecondName = a.SEGUNDO_NOMBRE,
					FirstName = a.PRIMER_NOMBRE 
				from @tablaCitas a inner join Common.Person p on a.NUMERO_DOCUMENTO = p.IdentificationNumber  
				where p.IdentificationNumber =  @IdentificacionCrear
			END
		

			-------
			------CREAR TERCERO----
			IF not exists (select 1 from Common.ThirdParty where Nit = @IdentificacionCrear)
			BEGIN
				INSERT  Common.ThirdParty 
				SELECT  per.id PersonId,a.NUMERO_DOCUMENTO Nit,0 DigitVerification,PRIMER_NOMBRE + ' ' + SEGUNDO_NOMBRE + ' ' + PRIMER_APELLIDO + ' ' + SEGUNDO_APELLIDO  Name,1 PersonType,
				0 RetentionType,0 ContributionType,0 StateEnterpriseType, NULL IVARetentionAccountPayableConceptId,0 Ica,0 IcaPercentage,0 IcaTop,0 IcaTopValue,NULL EntityCode,
				NULL EconomicActivityId,1 Class,NULL DigitalSignature, NULL CodeCIIU, 1 State,GETDATE() CreationDate,999 UserId,0 HandlesBranchOffice,NULL CodeDivipola,NULL IVARetentionConceptId,
				0 ElectronicBiller,NULL CreationUser,NULL ModificationUser,NULL ModificationDate
				FROM @tablaCitas a inner join
						Common.Person per on per.IdentificationNumber =a.NUMERO_DOCUMENTO
			END
			ELSE 
			BEGIN
				update  Common.ThirdParty
				set 
					Name = a.PRIMER_NOMBRE + ' ' + a.SEGUNDO_NOMBRE + ' ' + a.PRIMER_APELLIDO + ' ' + a.SEGUNDO_APELLIDO					
				from @tablaCitas a inner join Common.ThirdParty p on a.NUMERO_DOCUMENTO = p.Nit  
				where p.Nit =  @IdentificacionCrear
			END

		--select count(1) from @tablaCitas 

		--iteramos las citas para crear ingreso y control servicio ambulatorio
		declare @Contador int = 0, @cantidadRegistro Int
		set @cantidadRegistro = (select count(1) from @tablaCitas)
		declare @IdConsecu varchar(8) = '00000001'
		declare @CodigoCentroAtencion as varchar(10) ='001'
		declare @CodigoUnidadFuncional as varchar(10) ='2000'
		declare @Ingreso as decimal(18,0)

		print 'numero ingreso y cita insertar ' + convert(varchar,@cantidadRegistro)

		WHILE @Contador < @cantidadRegistro
		BEGIN
			 SET @Contador  += 1
			 print '@Contador: ' + convert(varchar,@Contador)

			 /*Update dbo.INCONSECU Set @Ingreso = CONNUMACT += 1  where IDCONSECU = @IdConsecu

			 INSERT INTO dbo.ADINGRESO
							(NUMINGRES,
							IPCODPACI,
							TIPOINGRE,
							IINGREPOR,
							ITIPORIES,
							ICAUSAING,
							CODENTIDA,
							IFECHAING,
							ILIQUIDAC,
							ICONTROLI,
							CODCENATE,
							UFUCODIGO,
							IESTADOIN,
							IREINGRES,
							UFUACTPAC,
							GENCAREGROUP,
							GENCONENTITY,
							CODUSUCRE,
							FECREGCRE,
							PACIENTESITIOQX,
							INDAUDFOR,
							IOBSERVAC,
							CODTIPPAC)
						SELECT
							@Ingreso AS NUMINGRES,
							a.NUMERO_DOCUMENTO AS IPCODPACI,
							1 TIPOINGRE,
							2 IINGREPOR,
							1 ITIPORIES,
							29 ICAUSAING,
							Ent.Code,
							@FechaProceso IFECHAING,
							1 ILIQUIDAC,
							'' ICONTROLI,
							@CodigoCentroAtencion as CODCENATE,
							@CodigoUnidadFuncional as UFUCODIGO,
							'C' IESTADOIN,
							0 IREINGRES,
							@CodigoUnidadFuncional UFUACTPAC,
							isnull(@IdCareGroup,'12') as CareGroupId,
							Ent.Id as HealthAdministratorId,
							3 AS CODUSUCRE,
							@FechaProceso,
							0 AS PACIENTESITIOQX,
							0 AS INDAUDFOR,
							@COMENTARIOINTEGRACION,
							3 -- poblacion general
						FROM @tablaCitas a inner join
						integrations.HospitalHSJ_Entidades H on a.COD_RESPONSABLE = h.LegacyCode inner join
						Contract.HealthAdministrator ent on ent.Code = H.IndigoCode inner join
						dbo.INPACIENT p on p.IPCODPACI = a.NUMERO_DOCUMENTO 
						where a.Id = @Contador
						*/
			declare @CodigoCups as varchar(25)
			select top 1 @CodigoCups=  a.SERVICIO_CUPS from @tablaCitas a where a.Id = @Contador
			declare @codigoActividad as varchar(25) = (select  top 1 a.CODACTMED  from AGACTMEDD a inner join AGACTIMED d on a.CODACTMED = d.CODACTMED where a.CODSERIPS = @CodigoCups and  d.ACTIVICON in (0,2) AND d.ESTADOACT =1)
			if @codigoActividad is null begin set @codigoActividad = '999' end 

			declare @TIpoSolicitud as varchar(25) =  (select top 1 SERIPSDASHAMBU = case isnull(SERIPSDASHAMBU,1) 
														when 1 then 1
														when 2 then 1 --Otros
														when 3 then 2 --apoyo DX
														when 4 then 1
														when 5 then 1
														when 6 then 1
														when 7 then 1
														when 8 then 1
														when 9 then 2
														when 10 then 1
														when 11 then 1
														when 12 then 2 
														ELSE 1 END from INCUPSIPS where CODSERIPS =@CodigoCups)
			
			INSERT INTO [dbo].[AGASICITA]
					   ([CODESPECI],[CODCENATE]
					   ,[IPCODPACI],[CODPROSAL]
					   ,[FECHORAIN],[FECHORAFI]
					   ,[CODIGOCON],[CODACTMED]
					   ,[CODTIPSOL],[CODTIPCIT]
					   ,[CODESTCIT],[CITAEXTRA]
					   ,[OBSERVACI],[ESTENVSMS]
					   ,[CODUSUASI],[FECREGSIS]
					   ,[OBSCITPRE],[FECINICIT]
					   ,[FECPROCT],[FECITADES]
					   ,[GENGENERATESO],[TIPSOLICITU]
					   ,[IDSALA],[IDEQUIPOTRA]
					   ,[CANCELUSU],[FECHCANCELA]
					   ,[CODCAUCAN],[OBSCAUCAN]
					   ,[CODSERIPS],[RELCITAINGRE]
					   ,[NUMINGRES],[CODDIAGNO]
					   ,[TIPTRATAMIENTO],[IDTURNOSALA]
					   ,[FASE],[OTRAFASE]
					   ,[CICLO],[IDHCRADESQUEMAS]
					   ,[IDRIASCUPS],[FECHAOFERTADA]
					   ,[CODCAUINA],[OBSCAUINA]
					   ,[CODUSUINA],[FECHAINA]
					   ,[CONFASIST],[GENCAREGROUP]
					   ,[GENCONENTITY],[NUMAUTORI]
					   ,[TIPSERIPS],[DESMOTANU]
					   ,[IDDESCRIPCIONRELACIONADA],[IDHCORDCICLOSD]
					   ,[CONFIRMQUIMIO],[CODUSUARIOREPRO]
					   ,[CODMOTIVOREPRO],[JUTIFICACIONREPRO]
					   ,[COMENTARIOREPRO],[FECHAREPRO]
					   ,[USUCONFIRMQUIMIO],[IDHCRADORDEN]
					   ,[MODALIDAD],[CONFIRMCITA]
					   ,[USUCONFIRM],[FECHACONFIRM]
					   ,[NUMINGRESCONFIRM],[IDCITAPADRE]
					   ,[IDAGENDA],[TIPOVISADO]
					   ,[OBSVISADO],[TraceabilityPaperworkId],[IdCitaSync])
				SELECT		
					   iif(PRO.CODPROSAL = '79147571','430', ESP.CODESPECI)
					   ,@CodigoCentroAtencion
					   ,a.NUMERO_DOCUMENTO
					   ,PRO.CODPROSAL
					   ,a.FECHA_HORA_CITA
					   ,dateadd(minute,convert(int,isnull(a.duracion,0)),a.FECHA_HORA_CITA) as FechaFinal
					   ,con.CODIGOCON
					   ,@codigoActividad as CODACTMED
					   ,'0' as CODTIPSOL ---presencial
					   ,case a.TIPO_CITA when 'P' then 0 when 'C' then 1 when 'O' then 2 when 'H' then 2 END as CODTIPCIT 
					   ,'0' as CODESTCIT --asignada
					   ,0 as CITAEXTRA
					   ,@COMENTARIOINTEGRACION
					   ,0 as ESTENVSMS
					   ,3 as CODUSUASI
					   ,@fechaProceso
					   ,NULL as OBSCITPRE
					   ,NULL as FECINICIT
					   ,NULL as FECPROCT
					   ,a.FECHA_HORA_CITA as FECITADES
					   ,0 as GENGENERATESO
					   ,@TIpoSolicitud as TIPSOLICITU --cita medica
					   ,NULL as IDSALA
					   ,null as IDEQUIPOTRA
					   ,null as CANCELUSU
					   ,null as FECHCANCELA
					   ,null as CODCAUCAN
					   ,null as OBSCAUCAN
					   ,a.SERVICIO_CUPS
					   ,null as RELCITAINGRE
					   ,null as Ingreso --@Ingreso
					   ,null CODDIAGNO
					   ,null as TIPTRATAMIENTO
					   ,null as IDTURNOSALA
					   ,null as FASE
					   ,null as OTRAFASE
					   ,null as CICLO
					   ,null as IDHCRADESQUEMAS
					   ,null as IDRIASCUPS
					   ,a.FECHA_HORA_CITA
					   ,null as CODCAUINA
					   ,null as OBSCAUINA
					   ,null as CODUSUINA
					   ,null as FECHAINA
					   ,null as CONFASIST
					   ,isnull(@IdCareGroup,'12') as careGroupId
					   ,ent.Id
					   ,null as NUMAUTORI
					   ,null as TIPSERIPS
					   ,null as DESMOTANU
					   ,null as IDDESCRIPCIONRELACIONADA
					   ,null as IDHCORDCICLOSD
					   ,null as CONFIRMQUIMIO
					   ,null as CODUSUARIOREPRO
					   ,null as CODMOTIVOREPRO
					   ,null as JUTIFICACIONREPRO
					   ,null as COMENTARIOREPRO
					   ,null as FECHAREPRO
					   ,null as USUCONFIRMQUIMIO
					   ,null as IDHCRADORDEN
					   ,0 as MODALIDAD
					   ,null as CONFIRMCITA
					   ,null as USUCONFIRM
					   ,null as FECHACONFIRM
					   ,null as NUMINGRESCONFIRM
					   ,null as IDCITAPADRE
					   ,null as IDAGENDA
					   ,null as TIPOVISADO
					   ,null as OBSVISADO
					   ,null as TraceabilityPaperworkId
					   ,a.ID_CITA_SERVINTE
		   FROM @tablaCitas a inner join
					dbo.INPACIENT p on p.IPCODPACI = a.NUMERO_DOCUMENTO inner join
					
					integrations.HospitalHSJ_Entidades H on a.COD_RESPONSABLE = h.LegacyCode inner join
					Contract.HealthAdministrator ent on ent.Code = H.IndigoCode inner join
					
					integrations.HospitalHSJ_Especialidades HE on a.COD_ESPECIALIDAD = HE.LegacyCode inner join
					dbo.INESPECIA ESP on Esp.CODESPECI =  HE.IndigoCode inner join

					integrations.HospitalHSJ_Profesionales HP on a.CEDULA_MEDICO = HP.LegacyCode inner join
					dbo.INPROFSAL PRO on PRO.CODPROSAL =  HP.IndigoCode inner join

					integrations.HospitalHSJ_Consultorios HC on a.CODIGO_CONSULTORIO = HC.LegacyCode inner join
					dbo.AGCONSULT con on con.CODIGOCON =  HC.IndigoCode
				WHERE a.Id = @Contador
	
			
			/*declare @IdCitaIndigo as int =  scope_identity()

			INSERT INTO dbo.ADCONCOEX(
						IPCODPACI,
						IPFECHACO,
						IPFECHCIT,
						CONESTADO,
						IPNOMCOMP,
						CODENTIDA,
						CODCENATE,
						UFUCODIGO,
						CODTIPCON,
						CODPROSAL,
						NUMINGRES,
						NUMCONCIT,
						INDAUDFOR,
						AUTESTADO,
						CODESPECI,
						LIQUIDAR,
						PRIMERLLA,
						SEGUNDLLA,
						TERCERLLA,
						GENCAREGROUP,
						GENCONENTITY,
						GENINVOICE,
						GENINVOICEID,
						IDDESCRIPCIONRELACIONADA,
						FECREGSIS,
						IdCitaSync,
						CodigoConsultorio)
				SELECT
					a.NUMERO_DOCUMENTO,
					a.FECHA_HORA_CITA,
					a.FECHA_HORA_CITA,
					1 as Estado, --CASE a.ES_POS_OPERATORIO when 0 then 7 else 1 END AS CONESTADO,    --estado cita en espera activacion 2 canal mirth (para las citas post operatorio ya deben quedar activas = 1)
					p.IPNOMCOMP,
					ent.Code,
					@CodigoCentroAtencion,
					@CodigoUnidadFuncional,
					case a.TIPO_CITA when 'P' then 1 when 'C' then 2 when 'O' then 3 when 'H' then 3 END as CODTIPCON,  --1 as CODTIPCON, --primera vez
					PRO.CODPROSAL,
					@Ingreso,
					@IdCitaIndigo as NUMCONCIT,
					0,
					null,
					ESP.CODESPECI,
					1,
					0,
					0,
					0,
					isnull(@IdCareGroup,'12') as careGroupId,
					ent.Id,
					null InvoiceNumber,
					null InvoiceId,
					null CUPSEntityContractDescriptionId,
					@FechaProceso,
					a.ID_CITA_SERVINTE,
					CON.CODIGOCON 
				FROM @tablaCitas a inner join
					dbo.INPACIENT p on p.IPCODPACI = a.NUMERO_DOCUMENTO inner join
					
					integrations.HospitalHSJ_Entidades H on a.COD_RESPONSABLE = h.LegacyCode inner join
					Contract.HealthAdministrator ent on ent.Code = H.IndigoCode inner join
					
					integrations.HospitalHSJ_Especialidades HE on a.COD_ESPECIALIDAD = HE.LegacyCode inner join
					dbo.INESPECIA ESP on Esp.CODESPECI =  HE.IndigoCode inner join

					integrations.HospitalHSJ_Profesionales HP on a.CEDULA_MEDICO = HP.LegacyCode inner join
					dbo.INPROFSAL PRO on PRO.CODPROSAL =  HP.IndigoCode inner join

					integrations.HospitalHSJ_Consultorios HC on a.CODIGO_CONSULTORIO = HC.LegacyCode inner join
					dbo.AGCONSULT con on con.CODIGOCON =  HC.IndigoCode
				WHERE a.Id = @Contador

				
				--para las citas de gastroenterologia y de tipo P realizamos registro en tabla de otros procedimientos
				 INSERT INTO [dbo].[AMBORDOTROSPRO](
					[IDCITA] 
					,[ESTADO] 
					,[NUMINGRES] 
					,[IPCODPACI]
					,[CODCENATE]
					,[UFUCODIGO]
					,[FECHAREG] 
					,[CODSERIPS]
					,[CANTIDAD] 
					,[IDDESCRIPCIONRELACIONADA] 
					,[GENCAREGROUP] 
					,[GENCONENTITY] 
					,[GENINVOICE] 
					,[GENINVOICEID] 
					,[GENSERVICEORDER]
					,[CODPROSAL] 
					,[CODESPECI] 
					,[CODCENATEPRO] 
					,[UFUCODIGOPRO] 
					,[CODUSUPRO]
					,[FECHAPRO] 
					,[INTERPRETACION] 
					,[CODPROSALINT]
					,[NUMEFOLIOINT] 
					,[MEDICOREALI] 
					,[CODESPREALI] 
					,[FECHAREALI] 
					,[CORRELACION]
					,[OBSERVACIONCORRELA] 
					,[OBSERVACION]
					,[IDAREAPRO]
					,[NOMARCPAT] 
					,[PRILLAMADO] 
					,[SEGLLAMADO] 
					,[TERLLAMADO] 
					,[FECPRILLAMADO] 
					,[FECSEGILLAMADO]
					,[FECTERLLAMADO] 
					,[PROFPRIMERLLAMADO] 
					,[PROFSEGLLAMADO]
					,[PROFTERLLAMADO] 
					,[OBVAUSENT] 
					,[PROAUSENT]
					,[FECAUSENT] 
					)
					SELECT 
					 @IdCitaIndigo 
					,1 as ESTADO
					,@Ingreso as NUMINGRES
					,a.NUMERO_DOCUMENTO as IPCODPACI
					,@CodigoCentroAtencion as [CODCENATE]
					,@CodigoUnidadFuncional as [UFUCODIGO]
					,@FechaProceso 
					,a.SERVICIO_CUPS
					,1 as [CANTIDAD] 
					,(select Id from contract.ContractDescriptions where code = a.SERVICIO_CUPS) as [IDDESCRIPCIONRELACIONADA] 
					,isnull(@IdCareGroup,'12') as [GENCAREGROUP] 
					,ent.Id as [GENCONENTITY] 
					,null as [GENINVOICE] 
					,null as [GENINVOICEID] 
					,null as [GENSERVICEORDER]
					,PRO.CODPROSAL as [CODPROSAL] 
					,ESP.CODESPECI as [CODESPECI] 
					,@CodigoCentroAtencion as [CODCENATEPRO] 
					,@CodigoUnidadFuncional as [UFUCODIGOPRO] 
					,3 as [CODUSUPRO]
					,@FechaProceso as [FECHAPRO] 
					,null as [INTERPRETACION] 
					,null as [CODPROSALINT]
					,null as [NUMEFOLIOINT] 
					,null as [MEDICOREALI] 
					,null as [CODESPREALI] 
					,null as [FECHAREALI] 
					,null as [CORRELACION]
					,null as [OBSERVACIONCORRELA] 
					,null as [OBSERVACION]
					,14 as [IDAREAPRO] --id area otros procedimiento - gastro
					,null as [NOMARCPAT] 
					,0 as [PRILLAMADO] 
					,0 as [SEGLLAMADO] 
					,0 as [TERLLAMADO] 
					,null as [FECPRILLAMADO] 
					,null as [FECSEGILLAMADO]
					,null as [FECTERLLAMADO] 
					,null as [PROFPRIMERLLAMADO] 
					,null as [PROFSEGLLAMADO]
					,null as [PROFTERLLAMADO] 
					,null as [OBVAUSENT] 
					,null as [PROAUSENT]
					,null as [FECAUSENT] 
					FROM @tablaCitas a inner join
					dbo.INPACIENT p on p.IPCODPACI = a.NUMERO_DOCUMENTO inner join
					
					integrations.HospitalHSJ_Entidades H on a.COD_RESPONSABLE = h.LegacyCode inner join
					Contract.HealthAdministrator ent on ent.Code = H.IndigoCode inner join
					
					integrations.HospitalHSJ_Especialidades HE on a.COD_ESPECIALIDAD = HE.LegacyCode inner join
					dbo.INESPECIA ESP on Esp.CODESPECI =  HE.IndigoCode inner join

					integrations.HospitalHSJ_Profesionales HP on a.CEDULA_MEDICO = HP.LegacyCode inner join
					dbo.INPROFSAL PRO on PRO.CODPROSAL =  HP.IndigoCode inner join

					integrations.HospitalHSJ_Consultorios HC on a.CODIGO_CONSULTORIO = HC.LegacyCode inner join
					dbo.AGCONSULT con on con.CODIGOCON =  HC.IndigoCode
					where a.Id = @Contador AND a.TIPOCITA_SERVINTEC = 'P' AND a.COD_ESPECIALIDAD = '310' 

					*/

				--insertamos control de registros
				insert into [Integrations].[HospitalHSJ_Synch]
				select a.ID_CITA_SERVINTE,Common.GETDATE(),a.FECHA_HORA_CITA,@Json,1 from @tablaCitas a WHERE a.Id = @Contador

				

		END -- FIN WHILE

	--confirmamos transaccion
	--commit tran saveInterfaz
	select 1 as CodeMessage, 'Se guardo correctamente'  Message 

	end try
	begin catch
	 --controlamos error en catch y devolvemos transaccion	
	 -- rollback tran saveInterfaz

	  insert into Integrations.HospitalHSJ_LogMirths
	  select @IdCita,common.GETDATE(),ERROR_MESSAGE() + ', Linea: ' + cast(ERROR_LINE() as varchar(20)) + char(13) + char(10), @Json  
	  
	  select '999' as CodeMessage, ERROR_MESSAGE() + ', Linea: ' + cast(ERROR_LINE() as varchar(20)) Message
	end catch

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de integración con el sistema legado Servinte del Hospital San José Colombia, ejecutado mediante el canal de mensajería Mirth. Recibe un JSON con los datos de una o varias citas médicas (paciente, documento de identidad, responsable de pago, médico, servicio CUPS, fecha y hora de la cita, consultorio, entre otros) y los sincroniza con Indigo Vie Cloud: crea o actualiza registros de paciente, ingreso y control de consulta externa. Durante el proceso valida la existencia de consultorios homologados, resuelve el código de la entidad pagadora (EPS/responsable) y su grupo de atención (CareGroup) dentro de los contratos vigentes, y en caso de errores de validación registra el incidente en una bitácora de log (HospitalHSJ_LogMirths) para trazabilidad. Actúa como puente entre la agenda generada en Servinte y la historia clínica/admisiones de Indigo, garantizando que cada cita quede correctamente asociada a su contrato, entidad y grupo de atención.', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'PROCEDURE', @level1name = N'SP_HospitalHSJ_SynchProcess';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'PROCEDURE', @level1name = N'SP_HospitalHSJ_SynchProcess';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Procesa un JSON de citas provenientes de la interfaz Mirth/Servinte del Hospital San José: valida homologaciones, crea o actualiza paciente, persona y tercero, y registra la cita de consulta externa junto con su control de sincronización.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'SP_HospitalHSJ_SynchProcess';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El parámetro JSON debe contener al menos un registro con ID_CITA_SERVINTE y los campos esperados de cita.; Los códigos de consultorio, entidad responsable, profesional (cédula médico) y especialidad deben existir en sus tablas de homologación de Integrations.HospitalHSJ_* y a su vez estar homologados a registros existentes en AGCONSULT, HealthAdministrator, INPROFSAL e INESPECIA respectivamente.; Debe existir al menos un contrato y CareGroup asociado a la entidad, o se asume el CareGroup por defecto 12.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'SP_HospitalHSJ_SynchProcess';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las entidades con código ''PARTICULAR'' se normalizan al código ''999'' antes de procesar.; Si la dirección llega nula, vacía o en blanco se reemplaza por ''BOGOTA''.; Cualquier validación fallida (consultorio, entidad, profesional o especialidad no homologados) registra un log en HospitalHSJ_LogMirths, devuelve CodeMessage=999 y aborta sin insertar paciente, persona, tercero ni cita.; El usuario de creación/integración siempre se registra como 3 con observación ''INTEGRACION MIRTH''.; Se procesa solo el primer NUMERO_DOCUMENTO del JSON para la creación/actualización de paciente, persona y tercero.; Si no se encuentra IdCareGroup asociado al contrato de la entidad, se usa por defecto el valor 12.; El mapeo de TIPO_DOCUMENTO se realiza con tablas fijas distintas para INPACIENT (CC=1, CE=2, CN=9, PE=12, PT=13, RC=4, TI=3, otros=15) y para Common.Person (CC=0, CE=1, TI=2, RC=3, PA=4, PE=12, CN=9, PT=12, otros=0).; El género se mapea F=2 y M=1 (con valor por defecto 1 en INPACIENT cuando es nulo).; Para el profesional con cédula ''79147571'' la especialidad de la cita se fuerza a ''430''.; Las citas se insertan siempre con centro de atención ''001'' y unidad funcional ''2000''.; Si el CUPS no tiene actividad médica activa asociada, se usa el código de actividad ''999''.; El TipoSolicitud se deriva de SERIPSDASHAMBU: valores 3, 9 y 12 se mapean a 2 (apoyo diagnóstico) y el resto a 1 (cita médica).; El éxito final devuelve CodeMessage=1; cualquier error capturado devuelve CodeMessage=999 con el mensaje y línea del error.', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'SP_HospitalHSJ_SynchProcess';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Cita médica; Consulta externa; Especialidad médica; Profesional de la salud; Consultorio; Entidad responsable de pago / Administradora de salud; Grupo de atención (CareGroup); Tipo de documento de identidad; Autorización; CUPS / Servicio; Integración Mirth con sistema Servinte', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'SP_HospitalHSJ_SynchProcess';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Integrations.HospitalHSJ_Entidades; Contract.HealthAdministrator; Contract.Contract; Contract.CareGroup; Integrations.HospitalHSJ_Consultorios; dbo.AGCONSULT; Integrations.HospitalHSJ_Profesionales; dbo.INPROFSAL; Integrations.HospitalHSJ_Especialidades; dbo.INESPECIA; dbo.INPACIENT; Common.Person; Common.ThirdParty; dbo.AGACTMEDD; dbo.AGACTIMED; dbo.INCUPSIPS', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'SP_HospitalHSJ_SynchProcess';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Integrations', @level1type=N'PROCEDURE', @level1name=N'SP_HospitalHSJ_SynchProcess';
-- GO
