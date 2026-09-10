
CREATE VIEW [dbo].[IND_AD_Paciente]
AS
SELECT        A.IPCODPACI AS CEDULA, 
                         CASE IPTIPODOC WHEN 1 THEN 'CC' WHEN 2 THEN 'CE' WHEN 3 THEN 'TI' WHEN 4 THEN 'RC' WHEN 5 THEN 'PA' WHEN 6 THEN 'AS' WHEN 7 THEN 'MS' WHEN
                          8 THEN 'NU' END AS TIPODOCUMENTO, A.CODIGONIT AS Tercero, A.IPEXPEDIC AS CIUDADEXP, A.IPNOMCOMP AS PACIENTE, 
                         RTRIM(LTRIM(dbo.Edad(CONVERT(varchar, A.IPFECNACI, 105), CONVERT(varchar, GETDATE(), 105)))) AS Edad, 'NO APLICA' AS EMPRESA, 
                         CASE IPTIPOPAC WHEN 1 THEN 'CONTRIBUTIVO' WHEN 2 THEN 'SUBSIDIADO' WHEN 3 THEN 'VINCULADO' WHEN 4 THEN 'PARTICULAR' WHEN 5 THEN 'OTRO'
                          WHEN 6 THEN 'DESPLAZADO REG. CONTRIBUTIVO' WHEN 7 THEN 'DESPLAZADO REG. SUBSIDIADO' WHEN 8 THEN 'DESPLAZADO NO ASEGURADO' END AS
                          TIPOPACIENTE, 
                         CASE IPTIPOAFI WHEN 0 THEN 'NO APLICA' WHEN 1 THEN 'COTIZANTE' WHEN 2 THEN 'BENEFICIARIO' WHEN 3 THEN 'ADICIONAL' WHEN 4 THEN 'JUB/RETIRADO'
                          WHEN 5 THEN 'PENSIONADO' END AS TIPOAFILIACION, 
                         CASE CAPACIPAG WHEN 0 THEN 'NO APLICA' WHEN 1 THEN 'SI' WHEN 2 THEN 'NO' WHEN 3 THEN 'DESPLAZADO' END AS CAPACIDADPAGO, 
                         B.NOMENTIDA AS ENTIDAD, A.CCCONTRAT + '-' + A.CPPLANBEN AS CODCONTRATO, C.DESCONTRA AS CONTRATO, D.UBINOMBRE AS UBICACION, 
                         E.NIVDESCRI AS NIVEL, A.IPDIRECCI AS DIRECCION, A.IPTELEFON AS TELEFONO, A.IPTELMOVI AS MOVIL, A.IPFECNACI, F.desactivi, 
                         CASE IPSEXOPAC WHEN 1 THEN 'MASCULINO' WHEN 2 THEN 'FEMENINO' END AS SEXO, 
                         CASE IPESTADOC WHEN 1 THEN 'SOLTERO' WHEN 2 THEN 'CASADO' WHEN 3 THEN 'VIUDO' WHEN 4 THEN 'UNION LIBRE' WHEN 5 THEN 'SEPARADO/DIV'
                          END AS ESTADOCIVIL, I.NIVEDESCRI AS NIVEL_EDUCATIVO, J.CREDDESCRI AS Creencia, 
                         CASE TIPCOBSAL WHEN 1 THEN 'CONTRIBUTIVO' WHEN 2 THEN 'SUBTOTAL' WHEN 3 THEN 'SUBPARCIAL' WHEN 4 THEN 'CON SISBEN' WHEN 5 THEN 'SIN SISBEN'
                          WHEN 6 THEN 'DESPLAZADOS' WHEN 7 THEN 'PLAN DE SALUD ADICIONAL' WHEN 8 THEN 'OTROS' END AS COBERTURA, 
                         CASE GRUPCODIGO WHEN 1 THEN 'CARCELARIOS' WHEN 2 THEN 'DESPLAZADOS' WHEN 3 THEN 'MIGRANTES' WHEN 4 THEN 'GESTANTES' WHEN 5 THEN
                          'HABITANTES DE LA CALLE' WHEN 4 THEN 'OTRO' END AS GrupoEspecial, CASE DISCCODIGO WHEN 2 THEN 'NO' WHEN 1 THEN 'SI' END AS Discapacidad,
                          A.CORELEPAC AS CORREO, G.DESGRUPET AS GRUPOETNICO, A.IPESTRATO AS ESTRATO, 
                         CASE ESTADOPAC WHEN 1 THEN 'ACTIVO' WHEN 2 THEN 'INACTIVO' END AS ESTADOPACIENTE, A.OBSERVACI AS OBSERVACION, 
                         H1.NOMUSUARI AS EMP_CREA, A.FECREGCRE AS FECHA_CREA, H2.NOMUSUARI AS EMP_MODI, A.FECREGMOD AS FECHA_MODIFICA
FROM            dbo.INPACIENT AS A INNER JOIN
                         dbo.INENTIDAD AS B ON B.CODENTIDA = A.CODENTIDA INNER JOIN
                         dbo.COCONTRAT AS C ON C.CODCONTRA = A.CCCONTRAT INNER JOIN
                         dbo.INUBICACI AS D ON D.AUUBICACI = A.AUUBICACI INNER JOIN
                         dbo.ADNIVELES AS E ON E.NIVCODIGO = A.NIVCODIGO INNER JOIN
                         dbo.ADACTIVID AS F ON F.codactivi = A.CODACTIVI LEFT OUTER JOIN
                         dbo.ADGRUETNI AS G ON G.CODGRUPOE = A.CODGRUPOE LEFT OUTER JOIN
                         dbo.SEGusuaru AS H1 ON H1.CODUSUARI = A.CODUSUCRE LEFT OUTER JOIN
                         dbo.SEGusuaru AS H2 ON H2.CODUSUARI = A.CODUSUMOD LEFT OUTER JOIN
                         dbo.ADNIVELED AS I ON I.NIVECODIGO = A.NIVECODIGO LEFT OUTER JOIN
                         dbo.ADCREDO AS J ON J.CREDCODIGO = A.CREDCODIGO
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista del perfil maestro de pacientes registrados en el sistema, que consolida en una sola consulta toda la información demográfica, de identificación y de afiliación de cada paciente. Integra datos de la tabla de pacientes (INPACIENT) con catálogos de entidades aseguradoras (EPS/ARS), contratos vigentes, ubicación geográfica, nivel socioeconómico, actividad de admisión, grupo étnico, nivel educativo, creencia o credencial, y usuarios que crearon o modificaron el registro. Expone en lenguaje legible campos como cédula o documento de identidad, tipo de documento (CC, TI, CE, PA, etc.), nombre completo del paciente, edad calculada, tipo de paciente (contributivo, subsidiado, particular, desplazado, etc.), tipo de afiliación (cotizante, beneficiario, pensionado), capacidad de pago, cobertura de salud, sexo, estado civil, estrato socioeconómico, dirección, teléfono, correo electrónico, grupo especial (gestantes, carcelarios, migrantes, etc.), discapacidad y estado del paciente (activo/inactivo). Se utiliza como fuente principal para reportería, búsqueda y consulta del perfil sociodemográfico y de aseguramiento de los pacientes en Indigo Vie Cloud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_Paciente';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'IND_AD_Paciente';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de consulta que consolida la información demográfica, administrativa y de afiliación de los pacientes, decodificando catálogos internos a descripciones legibles para reportes/indicadores.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_Paciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El paciente debe tener entidad, contrato, ubicación, nivel y actividad válidos (INNER JOIN), de lo contrario no aparece en la vista.; Los códigos de catálogo (tipo documento, tipo paciente, sexo, estado civil, etc.) deben corresponder a los valores esperados; en caso contrario el campo decodificado retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_Paciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se requieren obligatoriamente entidad, contrato, ubicación, nivel y actividad asociados al paciente para incluirlo en el resultado.; Grupo étnico, usuarios de creación/modificación, nivel educativo y credo son opcionales (LEFT JOIN) y pueden venir nulos.; El código de contrato expuesto se construye concatenando contrato y plan de beneficios separados por ''-''.; La empresa siempre se reporta como ''NO APLICA'' (campo fijo).; La edad se calcula a partir de la fecha de nacimiento contra la fecha actual mediante la función dbo.Edad.; El valor 4 de GRUPCODIGO está duplicado en el CASE (GESTANTES y OTRO), por lo que ''OTRO'' nunca se devuelve.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_Paciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Tipo de documento; Tipo de paciente (régimen); Tipo de afiliación; Capacidad de pago; Entidad aseguradora; Contrato y plan de beneficios; Ubicación geográfica; Nivel de atención; Cobertura en salud; Grupo especial (carcelarios, desplazados, migrantes, gestantes, habitantes de calle); Discapacidad; Grupo étnico; Estrato; Estado civil; Nivel educativo; Credo/Creencia; Auditoría de creación y modificación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_Paciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.IND_AD_Paciente: Devuelve un registro por paciente con datos básicos, afiliación, contrato, ubicación y auditoría, decodificando códigos numéricos a etiquetas de negocio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_Paciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC ∈ {1..8} → Traduce a CC, CE, TI, RC, PA, AS, MS, NU respectivamente else NULL; si IPTIPOPAC ∈ {1..8} → Clasifica al paciente como CONTRIBUTIVO, SUBSIDIADO, VINCULADO, PARTICULAR, OTRO o variantes de DESPLAZADO else NULL; si IPTIPOAFI ∈ {0..5} → Clasifica afiliación como NO APLICA, COTIZANTE, BENEFICIARIO, ADICIONAL, JUB/RETIRADO o PENSIONADO else NULL; si CAPACIPAG ∈ {0..3} → Determina capacidad de pago: NO APLICA, SI, NO o DESPLAZADO else NULL; si TIPCOBSAL ∈ {1..8} → Asigna tipo de cobertura en salud (CONTRIBUTIVO, SUBTOTAL, SUBPARCIAL, CON/SIN SISBEN, DESPLAZADOS, PLAN ADICIONAL, OTROS) else NULL; si GRUPCODIGO ∈ {1..5} → Clasifica grupo especial (CARCELARIOS, DESPLAZADOS, MIGRANTES, GESTANTES, HABITANTES DE LA CALLE) else NULL; si DISCCODIGO = 1 o 2 → Marca Discapacidad como SI o NO else NULL; si ESTADOPAC = 1 o 2 → Marca paciente como ACTIVO o INACTIVO else NULL; si IPSEXOPAC = 1 o 2 → Asigna sexo MASCULINO o FEMENINO else NULL; si IPESTADOC ∈ {1..5} → Asigna estado civil SOLTERO/CASADO/VIUDO/UNION LIBRE/SEPARADO/DIV else NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_Paciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Edad', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_Paciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.INPACIENT; dbo.INENTIDAD; dbo.COCONTRAT; dbo.INUBICACI; dbo.ADNIVELES; dbo.ADACTIVID; dbo.ADGRUETNI; dbo.SEGusuaru; dbo.ADNIVELED; dbo.ADCREDO', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_Paciente';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'IND_AD_Paciente';
GO
