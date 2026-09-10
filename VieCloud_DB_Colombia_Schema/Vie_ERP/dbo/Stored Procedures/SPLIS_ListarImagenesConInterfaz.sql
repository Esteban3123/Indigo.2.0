

CREATE PROCEDURE [dbo].[SPLIS_ListarImagenesConInterfaz]
AS
BEGIN
	SET NOCOUNT ON;
     SELECT  A.CODSERIPS,A.IPCODPACI,C.IPNOMCOMP,C.IPPRIAPEL,C.IPSEGAPEL,C.IPFECNACI,C.IPSEXOPAC,B.TIPMODALI,D.DESSERIPS, E.NOMSERPAC,A.IDETIPHIS,A.CODCENATE,A.UFUCODIGO,A.NUMEFOLIO,A.NUMINGRES,A.CODPROSAL
     FROM  dbo.HCORDIMAG AS A INNER JOIN
   dbo.HCINTESER as B ON A.CODSERIPS=B.CODSERIPS AND A.CODCENATE=B.CODCENATE INNER JOIN
   dbo.INPACIENT AS C ON A.IPCODPACI= C.IPCODPACI INNER JOIN 
   dbo.INCUPSIPS AS D ON A.CODSERIPS=D.CODSERIPS INNER JOIN
   dbo.HCPARPACS AS E ON A.CODCENATE=E.CODCENATE
        where A.ESTSERIPS='2' and A.SERREAINT='True' and A.REALINOTIF='False'
UNION
		   SELECT  A.CODSERIPS,A.IPCODPACI,C.IPNOMCOMP,C.IPPRIAPEL,C.IPSEGAPEL,C.IPFECNACI,C.IPSEXOPAC,B.TIPMODALI,D.DESSERIPS, E.NOMSERPAC,'' AS IDETIPHIS,A.CODCENATE,A.UFUCODIGO,'' AS NUMEFOLIO,A.NUMINGRES,A.CODPROSAL
     FROM  dbo.AMBORDIMA AS A INNER JOIN
   dbo.HCINTESER as B ON A.CODSERIPS=B.CODSERIPS AND A.CODCENATE=B.CODCENATE INNER JOIN
   dbo.INPACIENT AS C ON A.IPCODPACI= C.IPCODPACI INNER JOIN 
   dbo.INCUPSIPS AS D ON A.CODSERIPS=D.CODSERIPS INNER JOIN
   dbo.HCPARPACS AS E ON A.CODCENATE=E.CODCENATE
        where A.ESTSERIPS='2' and A.SERREAINT = 1 and A.REALINOTIF='False'
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista las órdenes de imágenes diagnósticas (radiología, ecografías, tomografías, resonancias, etc.) que están listas para ser enviadas a la interfaz con el sistema PACS, pero que aún no han sido notificadas. Combina órdenes provenientes de historia clínica (HCORDIMAG) y de atención ambulatoria (AMBORDIMA), filtrando solo aquellas con estado aprobado, marcadas para integración con interfaz y pendientes de notificación. Para cada orden retorna datos del paciente (cédula, nombre completo, fecha de nacimiento, sexo), el servicio CUPS solicitado, la modalidad de imagen, el centro de atención, la unidad funcional, el número de ingreso, el profesional solicitante y el nombre del servicio PACS configurado en el centro. Se usa para alimentar la cola de envío al sistema de imágenes diagnósticas (PACS/RIS) y garantizar que cada estudio sea notificado a la interfaz correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarImagenesConInterfaz';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPLIS_ListarImagenesConInterfaz';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las órdenes de imágenes diagnósticas (hospitalarias y ambulatorias) que están pendientes de ser notificadas a la interfaz externa de imágenes, junto con datos del paciente, servicio y centro de atención.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesConInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir correspondencia entre servicio y centro en la tabla de configuración de interfaces de servicios; El paciente referenciado debe existir en el maestro de pacientes; El servicio debe estar parametrizado en el catálogo CUPS/IPS; El centro de atención debe tener parámetros de integración configurados', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesConInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen órdenes en estado ''2'' (servicio confirmado/listo); Solo se incluyen órdenes marcadas como sujetas a realización vía interfaz; Se excluyen órdenes que ya fueron notificadas a la interfaz (REALINOTIF=''False''); El flag SERREAINT se evalúa como cadena ''True'' en el flujo hospitalario y como entero 1 en el flujo ambulatorio; Las órdenes ambulatorias no aportan identificador de tipo de historia ni número de folio (se devuelven vacíos); Por uso de UNION se eliminan duplicados entre órdenes hospitalarias y ambulatorias', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesConInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Órdenes de imágenes diagnósticas; Hospitalización; Atención ambulatoria; Paciente; Servicio CUPS/IPS; Modalidad de imagen; Centro de atención; Interfaz de integración de imágenes; Notificación a interfaz', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesConInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Retorna las órdenes de imagen hospitalarias cuando ESTSERIPS=''2'' (estado del servicio) y SERREAINT=''True'' (servicio sujeto a interfaz) y REALINOTIF=''False'' (aún no notificada a la interfaz); [RETURN_RESULT] resultset: Une (UNION) las órdenes de imagen ambulatorias cuando ESTSERIPS=''2'' y SERREAINT=1 y REALINOTIF=''False'', dejando IDETIPHIS y NUMEFOLIO vacíos por no aplicar a ambulatorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesConInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCORDIMAG; dbo.HCINTESER; dbo.INPACIENT; dbo.INCUPSIPS; dbo.HCPARPACS; dbo.AMBORDIMA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesConInterfaz';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPLIS_ListarImagenesConInterfaz';
-- GO
