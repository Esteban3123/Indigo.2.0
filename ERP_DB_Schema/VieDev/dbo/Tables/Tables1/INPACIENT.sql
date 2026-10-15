CREATE TABLE [dbo].[INPACIENT] (
    [IPCODPACI]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IPTIPODOC]           INT                                                                              NOT NULL,
    [CODIGONIT]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)')            NOT NULL,
    [IPEXPEDIC]           CHAR (40)                                                                        NOT NULL,
    [IPPRIAPEL]           VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')  NULL,
    [IPSEGAPEL]           VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)') NULL,
    [IPPRINOMB]           VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')     NULL,
    [IPSEGNOMB]           VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')    NULL,
    [IPNOMCOMP]           CHAR (250) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')             NOT NULL,
    [CODEMPRES]           CHAR (5)                                                                         NULL,
    [IPTIPOPAC]           INT                                                                              NOT NULL,
    [IPTIPOAFI]           INT                                                                              NOT NULL,
    [CAPACIPAG]           INT                                                                              NOT NULL,
    [CODENTIDA]           CHAR (9)                                                                         NULL,
    [CCCONTRAT]           CHAR (6)                                                                         NULL,
    [CPPLANBEN]           CHAR (2)                                                                         NULL,
    [AUUBICACI]           CHAR (20)                                                                        NULL,
    [NIVCODIGO]           CHAR (2)                                                                         NULL,
    [IPDIRECCI]           VARCHAR (MAX) MASKED WITH (FUNCTION = 'default()')                               NOT NULL,
    [IPTELEFON]           VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')         NOT NULL,
    [IPTELMOVI]           VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "CellPhone_Ofuscado", 0)')     NOT NULL,
    [IPFECNACI]           DATETIME MASKED WITH (FUNCTION = 'default()')                                    NOT NULL,
    [CODACTIVI]           CHAR (4)                                                                         NULL,
    [IPSEXOPAC]           INT                                                                              NOT NULL,
    [IPESTADOC]           INT                                                                              NOT NULL,
    [IPGRUPSAN]           CHAR (2)                                                                         NULL,
    [IPRHSANGR]           CHAR (1)                                                                         NULL,
    [TIPCOBSAL]           CHAR (1)                                                                         NOT NULL,
    [CORELEPAC]           CHAR (50) MASKED WITH (FUNCTION = 'email()')                                     NULL,
    [CODGRUPOE]           CHAR (3)                                                                         NULL,
    [ESTADOPAC]           BIT                                                                              NOT NULL,
    [OBSERVACI]           VARCHAR (250)                                                                    NULL,
    [INDAUDFOR]           NUMERIC (18)                                                                     NOT NULL,
    [PACIEFOTO]           VARBINARY (MAX)                                                                  NULL,
    [PACIEHUELL]          VARBINARY (MAX)                                                                  NULL,
    [NUMCARPET]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [CODUSUCRE]           CHAR (20)                                                                        NULL,
    [FECREGCRE]           DATETIME                                                                         NULL,
    [CODUSUMOD]           CHAR (20)                                                                        NULL,
    [FECREGMOD]           DATETIME                                                                         NULL,
    [IPESTRATO]           INT                                                                              NULL,
    [CREDCODIGO]          CHAR (3)                                                                         NULL,
    [DISCCODIGO]          CHAR (3)                                                                         NULL,
    [IDICODIGO]           CHAR (3)                                                                         NULL,
    [NIVECODIGO]          CHAR (3)                                                                         NULL,
    [GRUPCODIGO]          CHAR (3)                                                                         NULL,
    [ZONAPARTADA]         BIT                                                                              NULL,
    [GENCAREGROUP]        INT                                                                              NULL,
    [GENCONENTITY]        INT                                                                              NULL,
    [GENEXPEDITIONCITY]   INT                                                                              NULL,
    [IPORIENTSEXUAL]      TINYINT                                                                          NULL,
    [IPIDENTSEXUAL]       TINYINT                                                                          NULL,
    [IPORIENTSEXOTRO]     VARCHAR (100)                                                                    NULL,
    [IPIDENTSEXOTRO]      VARCHAR (100)                                                                    NULL,
    [PESO]                INT                                                                              NULL,
    [IPSEXO]              CHAR (1)                                                                         NULL,
    [IDENTMAMA]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [IDENTOBSERVAC]       VARCHAR (200)                                                                    NULL,
    [ID]                  INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPAIS]              INT                                                                              NULL,
    [PACIEINVESTIGACION]  BIT                                                                              NULL,
    [PACIENTEDATOSBASICO] BIT                                                                              NULL,
    [IdGenderIdentity]    INT                                                                              NULL,
    CONSTRAINT [PK_INPacient] PRIMARY KEY CLUSTERED ([IPCODPACI] ASC),
    CONSTRAINT [FK_GenderIdentity] FOREIGN KEY ([IdGenderIdentity]) REFERENCES [Admissions].[GenderTypes] ([Id]),
    CONSTRAINT [FK_INPacient_ADactivid] FOREIGN KEY ([CODACTIVI]) REFERENCES [dbo].[ADACTIVID] ([codactivi]),
    CONSTRAINT [FK_INPacient_ADEmpresa] FOREIGN KEY ([CODEMPRES]) REFERENCES [dbo].[ADEMPRESA] ([CODEMPRES]),
    CONSTRAINT [FK_INPACIENT_ADGRUETNI] FOREIGN KEY ([CODGRUPOE]) REFERENCES [dbo].[ADGRUETNI] ([CODGRUPOE]),
    CONSTRAINT [IX_ID_INPACIENT] UNIQUE NONCLUSTERED ([ID] ASC)
);


GO
ALTER TABLE [dbo].[INPACIENT] NOCHECK CONSTRAINT [FK_GenderIdentity];


GO
ALTER TABLE [dbo].[INPACIENT] NOCHECK CONSTRAINT [FK_INPacient_ADactivid];


GO
ALTER TABLE [dbo].[INPACIENT] NOCHECK CONSTRAINT [FK_INPacient_ADEmpresa];


GO
ALTER TABLE [dbo].[INPACIENT] NOCHECK CONSTRAINT [FK_INPACIENT_ADGRUETNI];


GO
CREATE NONCLUSTERED INDEX [IX_INPACIENT_IPTIPODOC]
    ON [dbo].[INPACIENT]([IPTIPODOC] ASC)
    INCLUDE([IPPRIAPEL], [IPSEGAPEL], [IPPRINOMB], [IPSEGNOMB], [IPNOMCOMP], [IPTIPOAFI], [NIVCODIGO], [IPDIRECCI], [IPTELEFON], [IPTELMOVI], [IPFECNACI], [CORELEPAC]);


GO
CREATE NONCLUSTERED INDEX [IX_INPACIENT__AUUBICACI__INC__CODGRUPOE__IPCODPACI__IPFECNACI__IPSEXOPAC__IPTIPOPAC]
    ON [dbo].[INPACIENT]([AUUBICACI] ASC)
    INCLUDE([IPCODPACI], [IPTIPOPAC], [IPFECNACI], [IPSEXOPAC], [CODGRUPOE]);


GO
CREATE NONCLUSTERED INDEX [IX_INPACIENT__IPCODPACI__CODENTIDA__INC__IPDIRECCI__IPESTADOC__IPEXPEDIC__IPFECNACI__IPNOMCOMP__IPSEXOPAC__IPTELEFON__IPTELMOVI_]
    ON [dbo].[INPACIENT]([IPCODPACI] ASC, [CODENTIDA] ASC)
    INCLUDE([IPTIPODOC], [IPEXPEDIC], [IPNOMCOMP], [IPDIRECCI], [IPTELEFON], [IPTELMOVI], [IPFECNACI], [IPSEXOPAC], [IPESTADOC]);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_INPACIENT__IPCODPACI]
    ON [dbo].[INPACIENT]([IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_INPACIENT__IPCODPACI__IPEXPEDIC__IPNOMCOMP__INC__IPDIRECCI__IPESTADOC__IPFECNACI__IPSEXOPAC__IPTELEFON__IPTELMOVI__IPTIPODOC]
    ON [dbo].[INPACIENT]([IPCODPACI] ASC, [IPEXPEDIC] ASC, [IPNOMCOMP] ASC)
    INCLUDE([IPTIPODOC], [IPDIRECCI], [IPTELEFON], [IPTELMOVI], [IPFECNACI], [IPSEXOPAC], [IPESTADOC]);


GO
CREATE TRIGGER [dbo].[AgregarUbicacionAutomatica]
   ON [dbo].[INPACIENT]
    AFTER INSERT,UPDATE
AS 
BEGIN

	declare @ID as INT = (select  ID  from inserted)

SET NOCOUNT ON;

	
	update INPACIENT SET AUUBICACI = (select TOP 1 AUUBICACI from INUBICACI where UBINOMBRE LIKE '%BOGOTA%')
	where Id = @ID AND AUUBICACI IS NULL OR  AUUBICACI = ''
	

END
GO
CREATE TRIGGER [dbo].[Paciente_IPCODPACI_NULL]
   ON [dbo].[INPACIENT]
   AFTER INSERT,UPDATE
AS
BEGIN

SET NOCOUNT ON;

  if exists(select 1 from inserted where IPCODPACI = '' )begin
			throw 51000,'Estamos detectando un error en el sistema mediante un trigger, La identificación del paciente esta quedado nula, por favor comunicarse con el administrador del sistema para poder continuar.', 1
end

END
GO
CREATE TRIGGER [dbo].[UbicacionPacienteNula]
   ON [dbo].[INPACIENT]
   AFTER INSERT,UPDATE
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

if exists(select 1 from inserted where AUUBICACI is null ) begin
		update INPACIENT set AUUBICACI = (select top 1 AUUBICACI from INUBICACI) where INPACIENT.ID in (select ID from inserted)
	end
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '* Despues del: 26-12-2023   Campo que se relaciona con una tabla de un formulario maestro de Identidad de genero.       * Antede del: 26-12-2023     Identidad de Género:  1- Masculino  2- Femenino  3- Transgenerista  8- Otro  9- No sabe/No informa/No aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IdGenderIdentity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo del paciente cuando se va a guardar en el Page de "Informacion Medica General":  Hombre (H)  Mujer (M)  Intersexual (I)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la identidad sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPIDENTSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Orientacion Sexual:  1- Homosexual  2- Heterosexual  3- Bisexual  8- Otro  9- No sabe/No informa/No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPORIENTSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Cobertura en Salud - Res.3047  1: Contributivo  2: Subsidiado Total  3: Subsidiado Parcial  4: Poblacion Pobre sin Asegurar con SISBEN  5: Poblacion Pobre sin Asegurar sin SISBEN  6: Desplazados  7: Plan de Salud Adicional  8: Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'TIPCOBSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RH:  +  -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPRHSANGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo Sanguineo:  A  B  AB  O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPGRUPSAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Estado Civil del Paciente:  1=Soltero (a)  2=Casado (a)  3=Viudo (a)  4=Union libre  5=separado(a)/Div', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPESTADOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo del Paciente:  1=Masculino   2=Femenino     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el paciente tiene capacidad de pago  0: No Aplica  1: Si / 100% Paciente    2: No / Cuota Recuperacion Paciente    3: Desplazado / 100% Entidad  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CAPACIPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Afiliacion del Paciente:  0: No Aplica  1: Cotizante  2: Beneficiario  3: Adicional  4: Jub/Retirado  5: Pensionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTIPOAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Paciente:  1-Contributivo  2-Subsidiado  3-No afiliado --- Antes se llamaba Vinculado ** PBI11755 Refactoring formulario pacientes ** ---  4-Particular  5-Otro --- Se elimina de la lista desplegable del formulario ** PBI11755 Refactoring formulario pacientes ** ---  6-Desplazado Reg. Contributivo --- Se elimina de la lista desplegable del formulario ** PBI11755 Refactoring formulario pacientes ** ---  7-Desplazado Reg. Subsidiado --- Se elimina de la lista desplegable del formulario ** PBI11755 Refactoring formulario pacientes ** ---  8-Desplazado No Asegurado --- Se elimina de la lista desplegable del formulario ** PBI11755 Refactoring formulario pacientes ** ---  9- Especial o excepción --- ** Se agrega por PBI 11755 Refactoring formulario pacientes ** ---  10- Personas privadas de la libertad a cargo del Fondo Nacional de Salud --- ** Se agrega por PBI 11755 Refactoring formulario pacientes ** ---  11-Tomador / amparado ARL --- ** Se agrega por PBI 11755 REfactoring formulario pacientes ** ---  12- Tomador / amparado SOAT --- ** Se agrega por PBI 11755 Refactoring formulario pacientes ** ---  13- Tomador / amparado planes voluntarios de salud --- ** Se agrega por PBI 11755 Refactoring formulario pacientes ** ---', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTIPOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Documento:   1= CC - Cédula de Ciudadanía   2= CE - Cédula de Extranjería   3= TI - Tarjeta de Identidad   4= RC - Registro Civil   5= PA - Pasaporte   6= AS - Adulto Sin Identificación   7= MS - Menor Sin Identificación   8= NU - Número único de identificación personal   9= CN - Certificado de Nacido Vivo   10= CD - Carnet Diplomático (Aplica para extranjeros)  11= SC - Salvoconducto (Aplica para extranjeros)  12= PE - Permiso Especial de Permanencia (Aplica para extranjeros)  13= PT - Permiso Temporal de Permanencia   14= DE - Documento Extranjero   15= SI - Sin Identificación    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';

