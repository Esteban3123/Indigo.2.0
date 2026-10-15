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
      And NUMEFOLIO = @ingreso
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