
-- =============================================
-- Author:		
-- Create date: <Create Date,,>
-- Description:	Realiza Insercion en la tabla de interfaz lectura Hyruko
-- =============================================
ALTER TRIGGER [dbo].[InterfazHyruko]
   ON  [dbo].[AMBORDIMA]
   for INSERT 
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @AUTO AS varchar(50)  = (SELECT AUTO FROM INSERTED) 
	DECLARE @Cedula AS varchar(50)  = (SELECT IPCODPACI  FROM INSERTED) 
	DECLARE @Ingreso AS varchar(50)  = (SELECT NUMINGRES   FROM INSERTED) 
	DECLARE @CodServicio as varchar(20) = (SELECT CODSERIPS   FROM INSERTED)
	DECLARE @CenAtencion as varchar(10) = (SELECT CODCENATE FROM inserted)
	DECLARE @UFuncional as varchar(10) = (SELECT UFUCODIGO FROM INSERTED)
	declare @FechaOrden as date = (SELECT FECORDMED FROM INSERTED)
	declare @HoraOrden as time(7) = (SELECT FECORDMED FROM INSERTED)
	declare @Modalidad as varchar(2) 
	declare @NomServicio as varchar(256) 
	declare @UFuncionalVIE as int
	declare @CCostoVIE as int
	declare @NombreEPS as varchar(256)
	declare @NitEPS as varchar(11)

	declare @InterfazHyruko as bit = (select INTERFAZHK FROM HCPARPACS where CODCENATE = @CenAtencion)

   select @Modalidad = RTRIM(TIPMODALI), @NomServicio =  left(DESSERIPS,256) from HCINTESER P INNER JOIN INCUPSIPS S ON S.CODSERIPS =P.CODSERIPS WHERE P.CODSERIPS = @CodServicio

   if @InterfazHyruko = 1 And @Modalidad is not null begin
	
		select @UFuncionalVIE =  Id, @CCostoVIE = CostCenterId from VIE33.Payroll.FunctionalUnit where Code = @UFuncional

		select @NombreEPS = E.Name, @NitEPS = left(T.Nit,11) from ADINGRESO  P inner join  VIE33.Contract.HealthAdministrator E ON E.Id = P.GENCONENTITY INNER JOIN 
		VIE33.Common.ThirdParty T ON T.Id = E.ThirdPartyId where p.NUMINGRES = @Ingreso 

		INSERT INTO [dbo].[HKLECTURA]
			   (--[numero_solicitud]
			   --,
			   [tipo_doc]
			   ,[documento]
			   ,[primer_nombre]
			   ,[segundo_nombre]
			   ,[primer_apellido]
			   ,[segundo_apellido]
			   ,[telefono]
			   ,[celular]
			   ,[direccion]
			   ,[correo]
			   ,[sexo]
			   ,[rh_sanguineo]
			   ,[Fecha_nacimiento]
			   ,[zona_residencial]
			   ,[codigo_departamento]
			   ,[nombre_departamento]
			   ,[codigo_municipio]
			   ,[nombre_municipio]
			   ,[cups]
			   ,[nombre_cups]
			   ,[modalidad]
			   ,[codigo_interno_servicio]
			   ,[centro_costo_solicitante]
			   ,[centro_costo_responde]
			   ,[codigo_medico]
			   ,[nombre_medico]
			   ,[especialidad_solicitante]
			   ,[via_ingreso]
			   ,[campo_urgencia]
			   ,[codigo_cie_10]
			   ,[descripción_cie_10]
			   ,[nombre_eps]
			   ,[nit_eps]
			   ,[fecha_solicitud]
			   ,[hora_solicitud]
			   ,[observacion]
			   ,[justificacion]
			   ,[lectura]
			   ,[prioridad]
			   ,[estado_his]
			   ,[cancelacion_his]
			   ,[auto_imagen_his])


			   select	--@AUTO as numero_solicitud,
					CASE IPTIPODOC 
						when 1 then 'CC'
						when 2 then 'CE'
						when 3 then 'TI'
						when 4 then 'RC'
						when 5 then 'PA'
					END as tipo_doc, rtrim(ltrim(P.IPCODPACI)) as documento,rtrim(ltrim(IPPRINOMB)) as primer_nombre ,rtrim(ltrim(IPSEGNOMB)) as segundo_nombre ,
					rtrim(ltrim(IPPRIAPEL)) as primer_apellido,rtrim(ltrim(IPSEGAPEL)) as segundo_apellido, 
					case when rtrim(ltrim(P.IPTELEFON)) = '' or P.IPTELEFON is null then '1' else rtrim(ltrim(P.IPTELEFON)) end as telefono, 
					rtrim(ltrim(IPTELMOVI)) celular,rtrim(ltrim(IPDIRECCI)) as direccion,rtrim(ltrim(CORELEPAC)) as correo,
					CASE  IPSEXOPAC 
						 when 1 then 'M' 		
						 when 2 then 'F' 	
					END as sexo,
					rtrim(ltrim(IPGRUPSAN))+''+rtrim(ltrim(IPRHSANGR)) as rh_sanguineo,
					IPFECNACI as Fecha_nacimiento,
					'U' AS zona_residencial,
					rtrim(ltrim(D.DEPCODIGO))  as codigo_departamento,
					rtrim(ltrim(D.nomdepart))  as  nombre_departamento,
					rtrim(ltrim(M.MUNCODIGO))   as codigo_municipio,
					rtrim(ltrim(M.MUNNOMBRE))  as nombre_municipio,
					@CodServicio as cups,
					@NomServicio as nombre_cups,
					@Modalidad as modalidad,			   
					@UFuncionalVIE as codigo_interno_servicio,
					@CCostoVIE as centro_costo_solicitante,
					@CCostoVIE as centro_costo_responde,
					'' as codigo_medico,
					'' as nombre_medico,
					'' as especialidad_solicitante,
					I.TIPOINGRE as via_ingreso,
					'N' as campo_urgencia,
					'' as codigo_cie_10,
					'' as descripción_cie_10,
					@NombreEPS as nombre_eps,
					@NitEPS as nit_eps,
					@FechaOrden as fecha_solicitud,
					@HoraOrden as hora_solicitud,
					'' as observacion,
					'' as justificacion,
					0 as lectura,
					1 as prioridad,
					'' as estado_his,
					0 as cancelacion_his,
					@AUTO as numero_solicitud
			   from INPACIENT P inner join INUBICACI U on P.AUUBICACI =  U.AUUBICACI 
			   INNER JOIN  INMUNICIP  M on M.DEPMUNCOD = U.DEPMUNCOD
			   INNER JOIN INDEPARTA  D on D.depcodigo = M.DEPCODIGO 
			   INNER JOIN ADINGRESO I on I.IPCODPACI = P.IPCODPACI 
			   where P.IPCODPACI = @Cedula  and I.NUMINGRES = @Ingreso
   end

  


END
GO