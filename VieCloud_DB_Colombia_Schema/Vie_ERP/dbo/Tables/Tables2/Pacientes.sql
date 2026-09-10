CREATE TABLE [dbo].[Pacientes] (
    [IPCODPACI]         CHAR (15)     NOT NULL,
    [IPTIPODOC]         INT           NOT NULL,
    [CODIGONIT]         CHAR (15)     NOT NULL,
    [IPEXPEDIC]         CHAR (40)     NULL,
    [IPPRIAPEL]         CHAR (20)     NULL,
    [IPSEGAPEL]         CHAR (20)     NULL,
    [IPPRINOMB]         CHAR (20)     NULL,
    [IPSEGNOMB]         CHAR (20)     NULL,
    [IPNOMCOMP]         CHAR (250)    NOT NULL,
    [CODEMPRES]         CHAR (5)      NULL,
    [IPTIPOPAC]         INT           NULL,
    [IPTIPOAFI]         INT           NULL,
    [CAPACIPAG]         INT           NULL,
    [CODENTIDA]         CHAR (9)      NULL,
    [CCCONTRAT]         CHAR (6)      NULL,
    [CPPLANBEN]         CHAR (2)      NULL,
    [AUUBICACI]         CHAR (20)     NULL,
    [NIVCODIGO]         CHAR (2)      NULL,
    [IPDIRECCI]         VARCHAR (MAX) NULL,
    [IPTELEFON]         VARCHAR (MAX) NULL,
    [IPTELMOVI]         VARCHAR (MAX) NULL,
    [IPFECNACI]         DATETIME      NULL,
    [CODACTIVI]         CHAR (4)      NULL,
    [IPSEXOPAC]         INT           NULL,
    [IPESTADOC]         INT           NULL,
    [IPGRUPSAN]         CHAR (2)      NULL,
    [IPRHSANGR]         CHAR (1)      NULL,
    [TIPCOBSAL]         CHAR (1)      NULL,
    [CORELEPAC]         CHAR (50)     NULL,
    [CODGRUPOE]         CHAR (3)      NULL,
    [ESTADOPAC]         BIT           NULL,
    [OBSERVACI]         VARCHAR (250) NULL,
    [INDAUDFOR]         NUMERIC (18)  NULL,
    [PACIEFOTO]         VARCHAR (MAX) NULL,
    [PACIEHUELL]        VARCHAR (MAX) NULL,
    [NUMCARPET]         VARCHAR (15)  NULL,
    [CODUSUCRE]         CHAR (20)     NULL,
    [FECREGCRE]         DATETIME      NULL,
    [CODUSUMOD]         CHAR (20)     NULL,
    [FECREGMOD]         DATETIME      NULL,
    [IPESTRATO]         INT           NULL,
    [CREDCODIGO]        CHAR (3)      NULL,
    [DISCCODIGO]        CHAR (3)      NULL,
    [IDICODIGO]         CHAR (3)      NULL,
    [NIVECODIGO]        CHAR (3)      NULL,
    [GRUPCODIGO]        CHAR (3)      NULL,
    [ZONAPARTADA]       BIT           NULL,
    [GENCAREGROUP]      BIT           NULL,
    [GENCONENTITY]      BIT           NULL,
    [GENEXPEDITIONCITY] BIT           NULL,
    [IPORIENTSEXUAL]    TINYINT       NULL,
    [IPIDENTSEXUAL]     TINYINT       NULL,
    [IPORIENTSEXOTRO]   VARCHAR (100) NULL,
    [IPIDENTSEXOTRO]    VARCHAR (100) NULL,
    [PESO]              INT           NULL,
    [IPSEXO]            CHAR (1)      NULL,
    [IDENTMAMA]         CHAR (15)     NULL,
    [IDENTOBSERVAC]     VARCHAR (200) NULL,
    [ID]                INT           NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observacion o comentario de no registro de identificacion de madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IDENTOBSERVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion de la Madre para paciente menores a 18 años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IDENTMAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genero del paciente Hombre (H), Mujer (M), Intersexual (I) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPSEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'PESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otra Identidad de Genero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPIDENTSEXOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otra Orientacion Sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPORIENTSEXOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orientacion Sexual:  1- Homosexual  2- Heterosexual  3- Bisexual  8- Otro  9- No sabe/No informa/No aplica (Identidad sexual)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPIDENTSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orientacion Sexual:  1- Homosexual  2- Heterosexual  3- Bisexual  8- Otro  9- No sabe/No informa/No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPORIENTSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la tabla de ciudades de vie para capturar el lugar de expedicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'GENEXPEDITIONCITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el id de la Entidad Administradora del contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el id del grupo de atencion de la base de datos de GENESIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Zona apartada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'ZONAPARTADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del grupo etnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'GRUPCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo nivel educativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'NIVECODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del idioma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IDICODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la discapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'DISCCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la creencia religiosa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'CREDCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPESTRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Modificacion del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'FECREGMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Creacion del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'FECREGCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del usuario que crea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número carpeta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'NUMCARPET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Huella dactilar del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'PACIEHUELL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Foto del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'PACIEFOTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del paciente  - cuando muere queda en inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'ESTADOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Grupo Etnico al cual pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'CODGRUPOE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo Electronico del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'CORELEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Cobertura en Salud - Res.3047  1: Contributivo  2: Subsidiado Total  3: Subsidiado Parcial  4: Poblacion Pobre sin Asegurar con SISBEN  5: Poblacion Pobre sin Asegurar sin SISBEN  6: Desplazados  7: Plan de Salud Adicional  8: Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'TIPCOBSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'RH:  +  -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPRHSANGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo Sanguineo:  A  B  AB  O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPGRUPSAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Estado Civil del Paciente:  1=Soltero (a)  2=Casado (a)  3=Viudo (a)  4=Union libre  5=separado(a)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPESTADOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo del Paciente:  1=Masculino   2=Femenino     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la Actividad que realiza el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'CODACTIVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de Nacimiento del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPFECNACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero Telefonico Movíl del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPTELMOVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero Telefonico Fijo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPTELEFON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPDIRECCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Nivel o Estrato del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'NIVCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la Ubicacion y municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'AUUBICACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Plan de Beneficios del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'CPPLANBEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Contrato del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'CCCONTRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la Entidad a la que pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina si el paciente tiene capacidad de pago  0: No Aplica  1: Si / 100% Paciente    2: No / Cuota Recuperacion Paciente    3: Desplazado / 100% Entidad ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'CAPACIPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Tipo de Afiliacion del Paciente:  0: No Aplica  1: Cotizante  2: Beneficiario  3: Adicional  4: Jub/Retirado  5: Pensionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPTIPOAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Tipo de Paciente:  1-Contributivo  2-Subsidiado  3-No afiliado  4-Particular  5-Otro  6-Desplazado Reg. Contributivo 7-Desplazado Reg. Subsidiado  8-Desplazado No Asegurado  9- Especial o excepción  10- Personas privadas de la libertad a cargo del Fondo Nacional de Salud 11-Tomador / amparado ARL 12- Tomador / amparado SOAT 13- Tomador / amparado planes voluntarios de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPTIPOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la Empresa Laboral del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'CODEMPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar de Expedicion del Documento de Identificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPEXPEDIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nit Institucion Prestadora de Servicios de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento paciente  1=Cédula de Ciudadanía  2=Cédula de Extranjería  3=Tarjeta de Identidad  4=Registro Civil  5=Pasporte  6=Adulto Sin Identificación  7=Menor Sin Identificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Pacientes', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla maestra de demografía de pacientes del HIS. Almacena identificación (tipo, número, lugar de expedición), nombre completo, datos de contacto, fecha de nacimiento, sexo, género, orientación sexual e identidad de género. Registra además información clínica básica (grupo sanguíneo, RH, peso), afiliación en salud (tipo de cobertura según Res. 3047, entidad, contrato, plan de beneficios), variables socioeconómicas (estrato, etnia, idioma, discapacidad, creencia religiosa, nivel educativo) y trazabilidad de creación/modificación de registros.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Pacientes';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Pacientes';
GO
