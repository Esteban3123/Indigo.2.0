CREATE PROCEDURE [dbo].[ConsultarAutorizacionesSuceptibles]
 @paciente varchar(25),
 @ingreso varchar(30)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT 
  CODSERIPS, 
  SERSUSCEP, 
  TIPOSERIPS, 
  CASE A.TIPOSERIPS when 1 then (
    SELECT TOP(1)
      case ESTSERIPS when 1 then 'Solicitado' when 2 then 'Muestra recolectada' when 3 then 'Resultado entregado' when 4 then 'Exámen interpretado' when 5 then 'Remitido' when 6 then 'Anulado' when 7 then 'Extramural' when 8 then 'Muestra recolectada parcialmente' end as Estado 
    from 
      dbo.HCORDLABO 
    where 
      NUMINGRES = @ingreso 
      And IPCODPACI = @paciente
      And NUMEFOLIO = A.NUMEFOLIO 
      And CODSERIPS = A.CODSERIPS
  ) when 2 then (
    SELECT TOP(1)
      case ESTSERIPS when 1 then 'Solicitado' when 2 then 'Muestra recolectada' when 3 then 'Resultado entregado' when 4 then 'Exámen interpretado' when 5 then 'Remitido' when 6 then 'Anulado' when 7 then 'Extramural' end as Estado 
    from 
      dbo.HCORDPATO 
    where 
      NUMINGRES = @ingreso 
      AND IPCODPACI = @paciente
      AND NUMEFOLIO = A.NUMEFOLIO 
      AND CODSERIPS = A.CODSERIPS
  ) when 3 then (
    SELECT TOP(1)
      case ESTSERIPS when 1 then 'Solicitado' when 2 then 'Estudio realizado' when 3 then 'Imagen procesada' when 4 then 'Estudio interpretado' when 5 then 'Remitido' when 6 then 'Anulado' when 7 then 'Extramural' end as Estado 
    from 
      dbo.HCORDIMAG 
    where 
      NUMINGRES = @ingreso 
      And IPCODPACI = @paciente 
      And NUMEFOLIO = A.NUMEFOLIO
      And CODSERIPS = A.CODSERIPS
  ) when 4 then (
    SELECT TOP(1)
      case ESTSERIPS when 1 then 'Ordenado' when 2 then 'Completado' when 3 then 'Interpretado' when 4 then 'Sin interfaz' when 5 then 'Anulado' end as Estado 
    from 
      dbo.HCORDPRON 
    where 
      NUMINGRES = @ingreso 
      And IPCODPACI = @paciente
      And NUMEFOLIO = A.NUMEFOLIO 
      And CODSERIPS = A.CODSERIPS
  ) when 5 then (
    SELECT TOP(1)
      case ESTSERIPS when 1 then 'Solicitado' when 2 then 'Sala programada' when 3 then 'Cancelado' when 4 then 'Resultado Revisado' when 5 then 'Anulado' WHEN 6 THEN 'Programado no realizado' end as Estado 
    from 
      dbo.HCORDPROQ 
    where 
      NUMINGRES = @ingreso 
      And IPCODPACI = @paciente
      And NUMEFOLIO = A.NUMEFOLIO 
      And CODSERIPS = A.CODSERIPS
  ) when 6 then 'Informe qx' END as 'ESTADO' 
FROM 
  dbo.ADAUTOSER A 
WHERE 
  IPCODPACI = @paciente
  AND NUMINGRES = @ingreso 
  AND SERSUSCEP = 1
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consulta las autorizaciones de servicios susceptibles de facturación para un paciente y número de ingreso específicos. Para cada autorización pendiente registrada en ADAUTOSER, determina el estado actual del servicio según su tipo: laboratorio clínico (HCORDLABO), patología (HCORDPATO), imágenes diagnósticas (HCORDIMAG), procedimientos no quirúrgicos (HCORDPRON) o procedimientos quirúrgicos (HCORDPROQ). Devuelve el código de servicio CUPS, el indicador de susceptibilidad y el estado legible del servicio (por ejemplo: Solicitado, Resultado entregado, Anulado), permitiendo verificar qué servicios autorizados ya tienen ejecución registrada en la historia clínica y cuáles están pendientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ConsultarAutorizacionesSuceptibles';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'ConsultarAutorizacionesSuceptibles';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los servicios autorizados marcados como susceptibles para un paciente e ingreso, mostrando el estado actual de cada servicio según el tipo de orden clínica asociada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ConsultarAutorizacionesSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el ingreso y paciente en ADAUTOSER con servicios marcados SERSUSCEP = 1; Para resolver el estado, debe existir orden correspondiente en la tabla del tipo de servicio (HCORDLABO/HCORDPATO/HCORDIMAG/HCORDPRON/HCORDPROQ) ligada por NUMEFOLIO y CODSERIPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ConsultarAutorizacionesSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran servicios con SERSUSCEP = 1 (susceptibles); El cruce con la orden clínica se hace siempre por la combinación paciente, ingreso, folio y código de servicio (CODSERIPS); Cada tipo de servicio (TIPOSERIPS) tiene su propio dominio de estados y su propia tabla de origen; El estado se obtiene del primer registro coincidente (TOP 1) por orden; TIPOSERIPS distinto a 1-6 produce estado nulo', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ConsultarAutorizacionesSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso hospitalario; Autorización de servicios; Servicio susceptible; Orden de laboratorio; Orden de patología; Orden de imágenes diagnósticas; Orden de procedimientos; Orden quirúrgica; Estado de la orden clínica; Folio clínico; CUPS/Servicio IPS', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ConsultarAutorizacionesSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.ADAUTOSER: Devuelve servicios autorizados susceptibles cuando SERSUSCEP = 1 para el paciente e ingreso indicados, traduciendo TIPOSERIPS y ESTSERIPS a una etiqueta de estado legible.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ConsultarAutorizacionesSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOSERIPS = 1 (laboratorio) → Consulta HCORDLABO y mapea ESTSERIPS a estados: 1=Solicitado, 2=Muestra recolectada, 3=Resultado entregado, 4=Exámen interpretado, 5=Remitido, 6=Anulado, 7=Extramural, 8=Muestra recolectada parcialmente; si TIPOSERIPS = 2 (patología) → Consulta HCORDPATO y mapea ESTSERIPS a estados: 1=Solicitado, 2=Muestra recolectada, 3=Resultado entregado, 4=Exámen interpretado, 5=Remitido, 6=Anulado, 7=Extramural; si TIPOSERIPS = 3 (imágenes) → Consulta HCORDIMAG y mapea ESTSERIPS a estados: 1=Solicitado, 2=Estudio realizado, 3=Imagen procesada, 4=Estudio interpretado, 5=Remitido, 6=Anulado, 7=Extramural; si TIPOSERIPS = 4 (procedimientos/servicios) → Consulta HCORDPRON y mapea ESTSERIPS a estados: 1=Ordenado, 2=Completado, 3=Interpretado, 4=Sin interfaz, 5=Anulado; si TIPOSERIPS = 5 (procedimientos quirúrgicos) → Consulta HCORDPROQ y mapea ESTSERIPS a estados: 1=Solicitado, 2=Sala programada, 3=Cancelado, 4=Resultado Revisado, 5=Anulado, 6=Programado no realizado; si TIPOSERIPS = 6 → Devuelve la etiqueta literal ''Informe qx'' sin consultar otra tabla', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ConsultarAutorizacionesSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADAUTOSER; dbo.HCORDLABO; dbo.HCORDPATO; dbo.HCORDIMAG; dbo.HCORDPRON; dbo.HCORDPROQ', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ConsultarAutorizacionesSuceptibles';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'ConsultarAutorizacionesSuceptibles';
-- GO
