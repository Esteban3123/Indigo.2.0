-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_AD_TrasladoCodigo]
	@PacienteAnterior  varchar(25),
	@PacienteNuevo  varchar(25),
	@TipoIdentificacion AS VARCHAR(5)
AS
BEGIN

  --reviso que no este bloqueado nindgun documento por el numero de la cama de los pacientes
  if (SELECT count(NUMDOCUME) FROM dbo.INDOCUMEN WHERE NUMDOCUME IN (SELECT CODICAMAS FROM dbo.CHREGESTA WHERE IPCODPACI IN (@PacienteAnterior,@PacienteNuevo) AND REGESTADO='1')) > 0 begin
       select 999 as CodigoMensaje, 'Uno de los Pacientes Se le esta Registrando Historia Clinica Por lo tanto No se Puede Ejecutar el Traslado de Codigos' as Mensaje 
	   return
  end

   --reviso que no este bloqueado ningun documento por el numero de ingreso de los pacientes
  if (SELECT count(NUMDOCUME) FROM dbo.INDOCUMEN WHERE NUMDOCUME IN (SELECT NUMINGRES FROM dbo.ADINGRESO WHERE IPCODPACI IN (@PacienteAnterior,@PacienteNuevo) AND IESTADOIN='')) > 0 begin
       select 999 as CodigoMensaje, 'Uno de los Pacientes Se le esta Registrando Historia Clinica Por lo tanto No se Puede Ejecutar el Traslado de Codigos' as Mensaje 
	   return
  end

     -- reviso que no este bloqueado ningun documento por el numero de Numero de identificacion de los pacientes
  if (SELECT count(NUMDOCUME) FROM dbo.INDOCUMEN WHERE NUMDOCUME IN (@PacienteAnterior,@PacienteNuevo) ) > 0 begin
       select 999 as CodigoMensaje, 'Uno de los Pacientes Se le esta Registrando Historia Clinica Por lo tanto No se Puede Ejecutar el Traslado de Codigos' as Mensaje 
	   return
  end

  --si el paciente nuevo no existe lo creo apartir de la informacion del anteriro
  if (SELECT count(IPCODPACI) FROM dbo.INPACIENT WHERE IPCODPACI=@pacientenuevo) = 0  begin
     
	  INSERT INTO dbo.INPACIENT (IPCODPACI, IPTIPODOC, CODIGONIT, IPEXPEDIC, IPPRIAPEL, IPSEGAPEL, IPPRINOMB, IPSEGNOMB, IPNOMCOMP, CODEMPRES, IPTIPOPAC, IPTIPOAFI,CAPACIPAG, CODENTIDA, CCCONTRAT, CPPLANBEN, 
	  AUUBICACI, NIVCODIGO, IPDIRECCI, IPTELEFON, IPTELMOVI, IPFECNACI, CODACTIVI, IPSEXOPAC, IPESTADOC,IPGRUPSAN, IPRHSANGR, TIPCOBSAL, CORELEPAC,CODGRUPOE,ESTADOPAC, NUMCARPET,INDAUDFOR) 
	  SELECT @pacientenuevo, IPTIPODOC, CODIGONIT, IPEXPEDIC, IPPRIAPEL, IPSEGAPEL, IPPRINOMB, IPSEGNOMB, IPNOMCOMP, CODEMPRES, IPTIPOPAC, IPTIPOAFI,CAPACIPAG, CODENTIDA, CCCONTRAT, CPPLANBEN, 
	  AUUBICACI, NIVCODIGO, IPDIRECCI, IPTELEFON, IPTELMOVI, IPFECNACI, CODACTIVI, IPSEXOPAC, IPESTADOC,IPGRUPSAN, IPRHSANGR, TIPCOBSAL, CORELEPAC,CODGRUPOE,ESTADOPAC, NUMCARPET,INDAUDFOR
	  FROM dbo.INPACIENT WHERE IPCODPACI=@PacienteAnterior 

  end

  --actualizamos tipo doc.
  if @TipoIdentificacion <> '' begin
		UPDATE dbo.INPACIENT SET IPTIPODOC=@TipoIdentificacion  WHERE IPCODPACI=@PacienteNuevo 
  end

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que realiza el traslado o fusión del código de identificación de un paciente (cédula anterior) hacia un nuevo código de identificación, utilizado cuando se detecta que un mismo paciente está registrado con dos documentos distintos en el sistema. Antes de ejecutar el traslado, verifica que ninguno de los dos pacientes tenga documentos bloqueados por registro activo de historia clínica (revisando camas en CHREGESTA, números de ingreso en ADINGRESO e identificaciones directas en INDOCUMEN), para garantizar que no haya historia clínica en proceso que pueda corromperse. Si el paciente con el nuevo código aún no existe en el maestro de pacientes (INPACIENT), lo crea automáticamente copiando todos los datos demográficos, de afiliación y contrato del paciente anterior. Opcionalmente actualiza el tipo de documento de identificación del paciente destino según el parámetro recibido.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_TrasladoCodigo';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SP_AD_TrasladoCodigo';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Trasladar/migrar la información maestra de un paciente a un nuevo código de identificación, creando el nuevo registro a partir del anterior y actualizando opcionalmente su tipo de documento, siempre que ningún documento clínico esté bloqueado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_TrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Ningún documento en INDOCUMEN debe estar asociado al código de cama (CODICAMAS) de un registro de estancia activo (REGESTADO=''1'') de cualquiera de los dos pacientes.; Ningún documento en INDOCUMEN debe estar asociado al número de ingreso (NUMINGRES) de un ingreso con IESTADOIN='''' de cualquiera de los dos pacientes.; Ningún documento en INDOCUMEN debe coincidir con los códigos de identificación del paciente anterior o nuevo.; El paciente anterior debe existir en INPACIENT para poder copiar su información al nuevo.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_TrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'No se ejecuta ningún traslado si algún documento clínico está bloqueado/abierto para alguno de los pacientes implicados.; El paciente nuevo siempre queda existente en INPACIENT al finalizar exitosamente (creado o preexistente).; Solo se sobrescribe el tipo de documento cuando se proporciona un valor no vacío.; La información del paciente nuevo recién creado se basa íntegramente en la del paciente anterior, salvo el código de paciente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_TrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Traslado/cambio de código de identificación; Tipo de identificación; Historia clínica; Ingreso hospitalario; Cama; Estancia activa; Bloqueo de documentos clínicos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_TrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] dbo.INPACIENT: Cuando el paciente nuevo no existe (COUNT=0 en INPACIENT por IPCODPACI), se crea copiando todos los datos demográficos y administrativos del paciente anterior, asignándole el nuevo código.; [UPDATE] dbo.INPACIENT: Cuando @TipoIdentificacion es distinto de cadena vacía, se actualiza IPTIPODOC del paciente nuevo con dicho valor.; [RETURN_RESULT] (resultset): Si existe bloqueo por cama, ingreso o identificación en INDOCUMEN, retorna CodigoMensaje=999 con mensaje indicando que se está registrando historia clínica y aborta la ejecución.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_TrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen documentos en INDOCUMEN cuyo NUMDOCUME coincide con CODICAMAS de estancias activas (REGESTADO=''1'') de los pacientes → Retorna mensaje 999 y termina sin trasladar else Continúa con la siguiente validación; si Existen documentos en INDOCUMEN cuyo NUMDOCUME coincide con NUMINGRES de ingresos con IESTADOIN='''' de los pacientes → Retorna mensaje 999 y termina sin trasladar else Continúa con la siguiente validación; si Existen documentos en INDOCUMEN cuyo NUMDOCUME coincide con los códigos de paciente anterior o nuevo → Retorna mensaje 999 y termina sin trasladar else Continúa con la creación/actualización; si El paciente nuevo no existe en INPACIENT → Inserta el nuevo paciente clonando los datos del paciente anterior else Omite la inserción; si @TipoIdentificacion <> '''' → Actualiza IPTIPODOC del paciente nuevo else No modifica el tipo de documento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_TrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INDOCUMEN; dbo.CHREGESTA; dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_TrasladoCodigo';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_AD_TrasladoCodigo';
-- GO
