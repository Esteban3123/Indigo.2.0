

CREATE PROCEDURE [MedicalAdmissions].[PatientData] (
    @CodigoPaciente VARCHAR(25),
	@IdTraslado INT
  ) AS BEGIN
SET
  NOCOUNT ON;
  DECLARE @NEWID UNIQUEIDENTIFIER = Newid(), @TipoEvento AS VARCHAR(50) = 'Actualizacion paciente', @FechaEvento as varchar(20) = FORMAT(common.GETDATE(), 'dd/MM/yyyy HH:mm:ss');

  SELECT @NEWID AS Id, @TipoEvento AS EventType, @CodigoPaciente AS AggregateId, @FechaEvento AS FechaEvento,
  (
	 SELECT 
		'Admisiones' AS 'Evento.Origen', 
		@TipoEvento As 'Evento.TipoEvento',
		@FechaEvento AS 'Evento.FechaEvento',
		@NEWID AS 'Evento.IdEvento',
		SUBSTRING(DB_NAME(),7,3) AS 'Evento.CodeBD',
	
		--===================================
		-- Datos del paciente
		--===================================
		ISNULL(CTA.PreviousPatientCode, '') AS 'PacienteAnterior.CodigoAnterior',
		ISNULL(CTA.PreviousPatientName, '') AS 'PacienteAnterior.NombreAnterior',
		RTRIM(ISNULL(OG.NOMBRE, '')) AS 'PacienteAnterior.TipoDocumento',
	
		IIF(ESTADOPAC = 1, 'Vivo', 'Fallecido') AS 'Paciente.Estado',	
		RTRIM(G.SIGLA)	   AS 'Paciente.TipoDocumentoSiglas',
		RTRIM(G.NOMBRE)	   AS 'Paciente.TipoDocumento',
		RTRIM(A.IPCODPACI) AS 'Paciente.CodigoPaciente',
		RTRIM(A.IPNOMCOMP) AS 'Paciente.NombreCompleto',
		RTRIM(A.IPPRINOMB) AS 'Paciente.PrimerNombre',
		RTRIM(A.IPSEGNOMB) AS 'Paciente.SegundoNombre',
		RTRIM(A.IPPRIAPEL) AS 'Paciente.PrimerApellido',
		RTRIM(A.IPSEGAPEL) AS 'Paciente.SegundoApellido',
		RTRIM(ISNULL(A.IPGRUPSAN, '')) AS 'Paciente.GrupoSanguineo',
		RTRIM(ISNULL(A.IPRHSANGR, '')) AS 'Paciente.RH',
	
		CASE A.IPSEXOPAC 
			WHEN 1 THEN 'Masculino' 
			WHEN 2 THEN 'Femenino' 
			ELSE NULL 
		END AS 'Paciente.Sexo',
	
		RTRIM(GT.Name)	   AS 'Paciente.IdentidadGenero',	
		RTRIM(FORMAT(A.IPFECNACI, 'dd/MM/yyyy')) AS 'Paciente.FechaNacimiento',	
		IIF(GE.DESGRUPET IS NOT NULL, RTRIM(GE.DESGRUPET),'') AS 'Paciente.GrupoEtnico',
		IIF(A.EthnicCommunity IS NOT NULL, RTRIM(A.EthnicCommunity), '') AS 'Paciente.ComunidadEtnica',
	
		dbo.Patient_Country(A.IPCODPACI) AS 'Paciente.InformacionContacto.Pais',
		dbo.patient_department(A.IPCODPACI) AS 'Paciente.InformacionContacto.Departamento',
		dbo.patient_municipality(A.IPCODPACI) AS 'Paciente.InformacionContacto.Municipio',
		dbo.Only_Patient_Address(A.IPCODPACI) AS 'Paciente.InformacionContacto.Direccion',
		RTRIM(ISNULL(A.IPTELEFON, '')) AS 'Paciente.InformacionContacto.Telefono',		
		RTRIM(ISNULL(A.IPTELMOVI, '')) AS 'Paciente.InformacionContacto.Movil',
		RTRIM(ISNULL(A.CORELEPAC, '')) AS 'Paciente.InformacionContacto.Correo',
	
		CASE A.IPTIPOPAC 
		    WHEN 1 THEN 'Contributivo'
		    WHEN 2 THEN 'Subsidiado'
		    WHEN 3 THEN 'No afiliado'
		    WHEN 4 THEN 'Particular'
		    WHEN 5 THEN 'Otro'
		    WHEN 6 THEN 'Desplazado Reg. Contributivo'
		    WHEN 7 THEN 'Desplazado Reg. Subsidiado'
		    WHEN 8 THEN 'Desplazado No Asegurado'
		    WHEN 9 THEN 'Especial o excepción'
		    WHEN 10 THEN 'Personas privadas de la libertad a cargo del Fondo Nacional de Salud'
		    WHEN 11 THEN 'Tomador / amparado ARL'
		    WHEN 12 THEN 'Tomador / amparado SOAT'
		    WHEN 13 THEN 'Tomador / amparado planes voluntarios de salud'
		    ELSE ''
		END AS 'Paciente.Afiliacion.TipoPaciente',
		CASE A.CAPACIPAG WHEN 1 THEN 'Total paciente' WHEN 2 THEN 'Cuota recuperacion' WHEN 3 THEN 'Total entidad' END AS 'Paciente.Afiliacion.CapacidadPago',
		CASE A.IPTIPOAFI WHEN 1 THEN 'Cotizante' WHEN 2 THEN 'Beneficiario' WHEN 3 THEN 'Adicional' WHEN 4 THEN 'Jubilado / retirado' WHEN 5 THEN 'Pensionado' ELSE '' END AS 'Paciente.Afiliacion.TipoAfiliacion',
		CASE TIPCOBSAL WHEN 1 THEN 'Contributivo' WHEN 2 THEN 'Subsidiado total' WHEN 3 THEN 'Subsidio parcial' WHEN 4 THEN 'Población pobre sin asegurar con Sisbén' WHEN 5 THEN 'Población pobre sin asegurar sin Sisbén' WHEN 6 THEN 'Desplazados' WHEN 7 THEN 'Plan de salud adicional' WHEN 8 THEN 'Otro' ELSE '' END AS 'Paciente.Afiliacion.Cobertura3047',
		RTRIM(ISNULL(CG.Name, '')) AS 'Paciente.Afiliacion.GrupoAtencion',
		RTRIM(ISNULL(NIV.NIVDESCRI, '')) AS 'Paciente.Afiliacion.Nivel',
		HA.Name AS 'Paciente.Afiliacion.EntidadAdministradora'	
	FROM INPACIENT A
	INNER JOIN ADTIPOIDENTIFICA G ON G.CODIGO = A.IPTIPODOC
	LEFT JOIN Admissions.GenderTypes GT ON GT.Id = A.IdGenderIdentity
	LEFT JOIN ADGRUETNI GE ON GE.CODGRUPOE = A.CODGRUPOE
	LEFT JOIN Contract.CareGroup CG ON CG.Id = A.GENCAREGROUP
	LEFT JOIN Contract.HealthAdministrator HA ON HA.Id = A.GENCONENTITY
	LEFT JOIN ADNIVELES NIV ON NIV.NIVCODIGO = A.NIVCODIGO
	OUTER APPLY (SELECT TOP 1 * FROM Admissions.CodeTransferAudit CTA WHERE CTA.Id = @IdTraslado ORDER BY CTA.Id DESC) AS CTA
	LEFT JOIN ADTIPOIDENTIFICA OG ON OG.CODIGO = CTA.PreviousIdentificationType
	WHERE IPCODPACI = @CodigoPaciente
	FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
  ) AS Payload
  
END
GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Procedimiento que construye un evento de tipo "Actualización de paciente" en formato JSON, consolidando los datos demográficos, de contacto, afiliación y cobertura del paciente identificado por su código. Incluye información de auditoría de traslado de código (datos del paciente anterior) cuando aplica. El payload resultante está orientado a la publicación o integración de eventos entre sistemas, incorporando un identificador único de evento, fecha y código de base de datos.', @level0type=N'SCHEMA', @level0name=N'MedicalAdmissions', @level1type=N'PROCEDURE', @level1name=N'PatientData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'MedicalAdmissions', @level1type=N'PROCEDURE', @level1name=N'PatientData';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye un evento JSON de ''Actualización paciente'' con la información demográfica, de contacto, afiliación y datos de traslado de código, listo para ser publicado por el módulo de Admisiones.', @level0type=N'SCHEMA', @level0name=N'MedicalAdmissions', @level1type=N'PROCEDURE', @level1name=N'PatientData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en INPACIENT cuyo IPCODPACI coincida con el código recibido; de lo contrario el resultado vendrá sin Payload.; Los catálogos referenciados (ADTIPOIDENTIFICA, ADGRUETNI, ADNIVELES, GenderTypes, CareGroup, HealthAdministrator) deben estar poblados para que los descriptivos no salgan vacíos.; Si se desea recuperar datos del código anterior del paciente, debe existir el registro en Admissions.CodeTransferAudit con el Id de traslado indicado.', @level0type=N'SCHEMA', @level0name=N'MedicalAdmissions', @level1type=N'PROCEDURE', @level1name=N'PatientData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El payload se serializa como JSON único (FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) por paciente.; El IdEvento es un UNIQUEIDENTIFIER nuevo generado en cada ejecución.; El TipoEvento siempre es ''Actualizacion paciente'' y el Origen siempre es ''Admisiones''.; La FechaEvento se formatea como ''dd/MM/yyyy HH:mm:ss'' usando common.GETDATE().; El CodeBD se deriva siempre de los caracteres 7 a 9 del nombre de la BD actual.; La fecha de nacimiento siempre se entrega en formato ''dd/MM/yyyy''.; Los campos de texto se entregan con RTRIM y los nullables con ISNULL a cadena vacía, garantizando que nunca se retornen NULL en los campos de contacto, documento previo, grupo de atención, nivel, etc.; Solo se retorna información del paciente cuyo IPCODPACI coincide exactamente con el código recibido.; Los datos del paciente anterior provienen del último registro (TOP 1 ORDER BY Id DESC) de CodeTransferAudit que coincida con el IdTraslado dado; si no existe, se entregan vacíos.', @level0type=N'SCHEMA', @level0name=N'MedicalAdmissions', @level1type=N'PROCEDURE', @level1name=N'PatientData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de documento; Identidad de género; Grupo étnico; Comunidad étnica; Grupo sanguíneo y RH; Afiliación al SGSSS; Capacidad de pago; Cobertura Resolución 3047; Grupo de atención; Nivel; Entidad administradora de salud (EPS/ARS); Auditoría de traslado/fusión de código de paciente; Estado vital (vivo/fallecido)', @level0type=N'SCHEMA', @level0name=N'MedicalAdmissions', @level1type=N'PROCEDURE', @level1name=N'PatientData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un resultset con (Id, EventType=''Actualizacion paciente'', AggregateId=código de paciente, FechaEvento, Payload JSON) consolidando datos del paciente y su traslado de código.', @level0type=N'SCHEMA', @level0name=N'MedicalAdmissions', @level1type=N'PROCEDURE', @level1name=N'PatientData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ESTADOPAC = 1 → Estado del paciente se reporta como ''Vivo'' else Se reporta como ''Fallecido''; si A.IPSEXOPAC IN (1,2) → Sexo se traduce a ''Masculino'' (1) o ''Femenino'' (2) else Sexo queda NULL; si A.IPTIPOPAC entre 1..13 → Se mapea a la etiqueta de tipo de afiliación correspondiente (Contributivo, Subsidiado, No afiliado, Particular, Desplazados, PPL, ARL, SOAT, planes voluntarios, etc.) else Se devuelve cadena vacía; si A.CAPACIPAG IN (1,2,3) → Se traduce a ''Total paciente'', ''Cuota recuperacion'' o ''Total entidad'' else NULL; si A.IPTIPOAFI entre 1..5 → Se traduce a tipo de afiliación (Cotizante/Beneficiario/Adicional/Jubilado/Pensionado) else Cadena vacía; si TIPCOBSAL entre 1..8 → Se traduce a la cobertura Resolución 3047 (Contributivo, Subsidiado total/parcial, Población pobre con/sin Sisbén, Desplazados, Plan adicional, Otro) else Cadena vacía; si GE.DESGRUPET IS NOT NULL → Se reporta el grupo étnico else Se devuelve cadena vacía; si A.EthnicCommunity IS NOT NULL → Se reporta la comunidad étnica else Cadena vacía', @level0type=N'SCHEMA', @level0name=N'MedicalAdmissions', @level1type=N'PROCEDURE', @level1name=N'PatientData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.ADTIPOIDENTIFICA; Admissions.GenderTypes; dbo.ADGRUETNI; Contract.CareGroup; Contract.HealthAdministrator; dbo.ADNIVELES; Admissions.CodeTransferAudit', @level0type=N'SCHEMA', @level0name=N'MedicalAdmissions', @level1type=N'PROCEDURE', @level1name=N'PatientData';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MedicalAdmissions', @level1type=N'PROCEDURE', @level1name=N'PatientData';
-- GO
