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