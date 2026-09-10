
CREATE PROCEDURE [dbo].[SPREP_HC_Generales_LaboratorioCodigoAzul]
(
@CodigoPaciente Varchar(25),
@NumeroIngreso Char(10),
@NumeroCodigoAzul nChar(10)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
          
SELECT RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',C.NUMCODAZU AS 'CONSECUTIVO DEL CODIGO AZUL',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCORDLABO A With(Nolock)
INNER JOIN INCUPSIPS B With(Nolock) ON A.CODSERIPS=B.CODSERIPS 
INNER JOIN dbo.HCCODAZUC C ON C.NUMCODAZU=A.NUMEFOLIO AND A.IPCODPACI = C.IPCODPACI AND A.NUMINGRES = C.NUMINGRES
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO IN(@NumeroCodigoAzul) AND MANEXTPRO = '0' AND IDETIPHIS='CODIGOAZU' 

UNION 

SELECT RTRIM(DESSERIPS) AS 'DESCRIPCION DEL SERVICIO',C.NUMCODAZU AS 'CONSECUTIVO DEL CODIGO AZUL',A.CODCENATE,A.UFUCODIGO,A.IPCODPACI
FROM HCORDIMAG A  With(Nolock)
INNER JOIN INCUPSIPS B With(Nolock) ON A.CODSERIPS=B.CODSERIPS 
INNER JOIN dbo.HCCODAZUC C ON C.NUMCODAZU=A.NUMEFOLIO AND A.IPCODPACI = C.IPCODPACI AND A.NUMINGRES = C.NUMINGRES
WHERE A.IPCODPACI=@CodigoPaciente AND A.NUMINGRES=@NumeroIngreso AND A.NUMEFOLIO IN (@NumeroCodigoAzul) AND MANEXTPRO = '0' AND IDETIPHIS='CODIGOAZU' ORDER BY C.NUMCODAZU DESC
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Recupera los exámenes de laboratorio clínico y las imágenes diagnósticas que fueron solicitadas bajo una alerta de Código Azul (emergencia crítica) para un paciente y un ingreso específicos. Combina las órdenes de laboratorio (HCORDLABO) y las órdenes de imágenes (HCORDIMAG) con el catálogo de servicios CUPS/IPS (INCUPSIPS) para obtener la descripción legible de cada examen, cruzándolas además con el registro del Código Azul (HCCODAZUC) para confirmar que las órdenes pertenecen a dicha alerta. Se utiliza en la reportería de historia clínica para visualizar todos los estudios diagnósticos ordenados durante un evento de Código Azul de un paciente en un ingreso determinado, filtrando únicamente órdenes internas (no externas al profesional) clasificadas con el tipo ''CODIGOAZU''.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_LaboratorioCodigoAzul';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPREP_HC_Generales_LaboratorioCodigoAzul';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta los servicios de laboratorio e imagenología solicitados a un paciente en el contexto de un evento de Código Azul específico durante un ingreso hospitalario.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Deben existir órdenes de laboratorio o imagenología asociadas al folio del Código Azul para el paciente e ingreso indicados.; Debe existir un registro en HCCODAZUC que vincule el folio (NUMCODAZU) con el paciente e ingreso.; Las órdenes deben estar marcadas como tipo de historia ''CODIGOAZU'' y no como procedimiento extendido (MANEXTPRO=''0'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes con MANEXTPRO=''0'' (no externas/procesadas).; Solo se incluyen órdenes con IDETIPHIS=''CODIGOAZU'', garantizando que pertenecen al contexto de Código Azul.; El folio de la orden (NUMEFOLIO) debe coincidir con el consecutivo del Código Azul (NUMCODAZU) para el mismo paciente e ingreso.; UNION elimina duplicados entre laboratorio e imagenología.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Código Azul; Paciente; Ingreso hospitalario; Orden de laboratorio; Orden de imagenología; Servicio CUPS; Centro de atención; Unidad funcional; Historia clínica', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve la unión de servicios de laboratorio (HCORDLABO) e imagenología (HCORDIMAG) cuyo NUMEFOLIO coincide con el código azul, ordenado por NUMCODAZU descendente.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDLABO; dbo.INCUPSIPS; dbo.HCCODAZUC; dbo.HCORDIMAG', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzul';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPREP_HC_Generales_LaboratorioCodigoAzul';
-- GO
