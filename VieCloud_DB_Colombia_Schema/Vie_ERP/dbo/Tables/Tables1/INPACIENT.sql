CREATE TABLE [dbo].[INPACIENT] (
    [IPCODPACI]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IPTIPODOC]           INT                                                                              NOT NULL,
    [CODIGONIT]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Nit_Ofuscado", 0)')            NOT NULL,
    [IPEXPEDIC]           CHAR (40)                                                                        NOT NULL,
    [IPPRIAPEL]           VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')  NULL,
    [IPSEGAPEL]           VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)') NULL,
    [IPPRINOMB]           VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')     NULL,
    [IPSEGNOMB]           VARCHAR (100) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')    NULL,
    [IPNOMCOMP]           CHAR (405) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')             NOT NULL,
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
    [CODACTIVI]           CHAR (5)                                                                         NULL,
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
    [PoblacionPAPSIVI]    BIT                                                                              CONSTRAINT [DF_PACIENTES_PoblacionPAPSIVI] DEFAULT ((0)) NOT NULL,
    [EthnicCommunity]     VARCHAR (100)                                                                    NULL,
    [OncologyCarePathway] INT                                                                              NULL, 
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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[CODIGONIT]
    WITH (LABEL = 'Confidential - Financial', INFORMATION_TYPE = 'Financial');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[IPPRIAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[IPSEGAPEL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[IPPRINOMB]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[IPSEGNOMB]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[IPNOMCOMP]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Name');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[IPDIRECCI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Address');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[IPTELEFON]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[IPTELMOVI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[IPFECNACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Date of Birth');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[CORELEPAC]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'Contact Info');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[NUMCARPET]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[INPACIENT].[IDENTMAMA]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');




GO
ALTER TABLE [dbo].[INPACIENT] NOCHECK CONSTRAINT [FK_GenderIdentity];


GO

CREATE NONCLUSTERED INDEX [IX_INPACIENT_IPTIPODOC]
    ON [dbo].[INPACIENT]([IPTIPODOC] ASC)
    INCLUDE([IPPRIAPEL], [IPSEGAPEL], [IPPRINOMB], [IPSEGNOMB], [IPNOMCOMP], [IPTIPOAFI], [NIVCODIGO], [IPDIRECCI], [IPTELEFON], [IPTELMOVI], [IPFECNACI], [CORELEPAC]);


GO
ALTER INDEX [IX_INPACIENT_IPTIPODOC]
    ON [dbo].[INPACIENT] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_INPACIENT__AUUBICACI__INC__CODGRUPOE__IPCODPACI__IPFECNACI__IPSEXOPAC__IPTIPOPAC]
    ON [dbo].[INPACIENT]([AUUBICACI] ASC)
    INCLUDE([IPCODPACI], [IPTIPOPAC], [IPFECNACI], [IPSEXOPAC], [CODGRUPOE]);


GO
CREATE NONCLUSTERED INDEX [IX_INPACIENT_NUMCARPET_IPCODPACI]
    ON [dbo].[INPACIENT]([NUMCARPET] ASC, [IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_INPACIENT__IPCODPACI__CODENTIDA__INC__IPDIRECCI__IPESTADOC__IPEXPEDIC__IPFECNACI__IPNOMCOMP__IPSEXOPAC__IPTELEFON__IPTELMOVI_]
    ON [dbo].[INPACIENT]([IPCODPACI] ASC, [CODENTIDA] ASC)
    INCLUDE([IPTIPODOC], [IPEXPEDIC], [IPNOMCOMP], [IPDIRECCI], [IPTELEFON], [IPTELMOVI], [IPFECNACI], [IPSEXOPAC], [IPESTADOC]);


GO
CREATE NONCLUSTERED INDEX [IX_INPACIENT__IPCODPACI__INC__IPEXPEDIC__IPNOMCOMP__IPTIPODOC]
    ON [dbo].[INPACIENT]([IPCODPACI] ASC)
    INCLUDE([IPEXPEDIC], [IPNOMCOMP], [IPTIPODOC]);


GO



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
			;throw 51000,'Estamos detectando un error en el sistema mediante un trigger, La identificación del paciente esta quedado nula, por favor comunicarse con el administrador del sistema para poder continuar.', 1
end

END
GO

CREATE TRIGGER [dbo].[HospitalCima_Softland_Patient] 
   ON [dbo].[INPACIENT]
   AFTER  INSERT,DELETE,UPDATE
AS 
BEGIN
	
	SET NOCOUNT ON;

	
	DECLARE @action as  int 
	declare @dataid as varchar(200)

	if exists(select ID from inserted ) and exists(select ID from deleted) begin
		set @action = 2 --actualizando
		set @dataid = (select top 1 ID from inserted)
	end else if exists(select ID from inserted ) and not exists(select ID from deleted) begin
		set @action = 1 --insertando
		set @dataid = (select top 1 ID from inserted)
	end else if not exists(select ID from inserted ) and exists(select ID from deleted) begin
		set @action = 3 --eliminando
		set @dataid = (select top 1 ID from deleted)
	end

	
	if @dataid is null begin
		return
	end

	insert into [integrations].[cimahospital_softland_synch]
			   ([dataid]
			   ,[type]
			   ,[action]
			   ,[transactiondate]
			   ,[state]
			   ,[errormessage])
		 values
			   (@dataid,2,@action,GETDATE(),0,NULL)



END
-----------------------------------------------------------------------------------------------------------------------------------

/****** Object:  Trigger [dbo].[HospitalCima_Softland_LaboratoyOrder]    Script Date: 8/24/2021 8:37:34 AM ******/
SET ANSI_NULLS ON
GO
DISABLE TRIGGER [dbo].[HospitalCima_Softland_Patient]
    ON [dbo].[INPACIENT];


GO
CREATE TRIGGER [dbo].[tgr_validateTypeDocumente] 
   ON [dbo].[INPACIENT]
   AFTER  INSERT
AS 
BEGIN
	
	SET NOCOUNT ON;

	
	DECLARE @identificacion as  varchar(25) =  (select top 1 IPCODPACI from inserted)
	declare @tipo as varchar(200) = (select top 1 IPTIPODOC from inserted)

	if @tipo = 16 and  len(@identificacion) <> 9 begin
	    ;THROW 51000,'CF - Cédula Física : debe tener 9 caracteres',1
		--RAISERROR ('CF - Cédula Física : debe tener 9 caracteres', -- Message text.  
  --             16, -- Severity.  
  --             1 -- State.  
  --             );  
			   return
	end
	

	if @tipo = 17 and  len(@identificacion) <> 10 begin
	  ;THROW 51000,'CJ - Cédula Jurídica : debe tener 10 caracteres',1
		--RAISERROR ('CJ - Cédula Jurídica : debe tener 10 caracteres', -- Message text.  
  --             16, -- Severity.  
  --             1 -- State.  
  --             );  
			   return
	end

	if @tipo = 18 and   len(@identificacion)<> 12 begin
	 ;THROW 51000,'DM - Dimex : debe tener 12 caracteres',1
		--RAISERROR ('DM - Dimex : debe tener 12 caracteres', -- Message text.  
  --             16, -- Severity.  
  --             1 -- State.  
  --             );  
		--	   return
	end


	if @tipo = 19 and  len(@identificacion) <> 10 begin
	 ;THROW 51000,'NI - Nite : debe tener 10 caracteres',1
		--RAISERROR ('NI - Nite : debe tener 10 caracteres', -- Message text.  
  --             16, -- Severity.  
  --             1 -- State.  
  --             );  
			   return
	end

	if @tipo = 20 and  len(@identificacion) <> 10 begin
	   ;THROW 51000,'OT - Otro : debe tener 10 caracteres',1
		--RAISERROR ('OT - Otro : debe tener 10 caracteres', -- Message text.  
  --             16, -- Severity.  
  --             1 -- State.  
  --             );  
			   return
	end


END
GO
DISABLE TRIGGER [dbo].[tgr_validateTypeDocumente]
    ON [dbo].[INPACIENT];


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidad de género del paciente (INT, FK→GenderTypes). Valores: 1=Masculino, 2=Femenino, 3=Transgenerista, 8=Otro, 9=No sabe/No informa/No aplica. Desde 26-12-2023 relaciona tabla maestra de identidad de género.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IdGenderIdentity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'* Despues del: 26-12-2023   Campo que se relaciona con una tabla de un formulario maestro de Identidad de genero.       * Antede del: 26-12-2023     Identidad de Género:  1- Masculino  2- Femenino  3- Transgenerista  8- Otro  9- No sabe/No informa/No aplica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IdGenderIdentity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IdGenderIdentity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bandera (BIT) que marca si el paciente se creó desde agendamiento con datos básicos iniciales (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PACIENTEDATOSBASICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando el paciente se crea desde agendamiento con datos basicos se marca como 1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PACIENTEDATOSBASICO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PACIENTEDATOSBASICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que marca si el paciente está en estado de investigación, diligenciado desde formulario de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PACIEINVESTIGACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Paciente en Investigacion, este campo se diligencia desde el formulario de pacientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PACIEINVESTIGACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PACIEINVESTIGACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del país del paciente (INT, FK→Países VIE), relaciona país de nacimiento o residencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IDPAIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me relaciona el ID pais con la tabla de Paises en VIE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IDPAIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IDPAIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único de la tabla INPACIENT (INT IDENTITY, PK alternativa, índice único).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación o comentario relativo a no registro de identificación de madre (VARCHAR 200, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IDENTOBSERVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion o comentario de no registro de identificacion de madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IDENTOBSERVAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IDENTOBSERVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la madre para pacientes menores a 18 años (VARCHAR 25, PII Ofuscado, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IDENTMAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de la Madre para paciente menores a 18 años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IDENTMAMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IDENTMAMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo del paciente registrado en información médica general (CHAR 1): H=Hombre, M=Mujer, I=Intersexual (nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo del paciente cuando se va a guardar en el Page de "Informacion Medica General":  Hombre (H)  Mujer (M)  Intersexual (I)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEXO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Peso del paciente en gramos (INT, nullable), dato antropométrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Peso del Paciente en gramos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PESO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otra identidad de género distinta a las opciones estándar (VARCHAR 100, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPIDENTSEXOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otra Identidad de Genero', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPIDENTSEXOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPIDENTSEXOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de otra orientación sexual no clasificada en opciones predefinidas (VARCHAR 100, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPORIENTSEXOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otra Orientacion Sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPORIENTSEXOTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPORIENTSEXOTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidad sexual del paciente (TINYINT): guarda clasificación de identidad sexual (nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPIDENTSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la identidad sexual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPIDENTSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPIDENTSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Orientación sexual del paciente (TINYINT). Valores: 1=Homosexual, 2=Heterosexual, 3=Bisexual, 8=Otro, 9=No sabe/No informa/No aplica (nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPORIENTSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Orientacion Sexual:  1- Homosexual  2- Heterosexual  3- Bisexual  8- Otro  9- No sabe/No informa/No aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPORIENTSEXUAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPORIENTSEXUAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ciudad de expedición del documento de identificación (INT, FK→Ciudades VIE, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'GENEXPEDITIONCITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla de ciudades de vie para capturar el lugar de expedicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'GENEXPEDITIONCITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'GENEXPEDITIONCITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Entidad Administradora de Servicios de Salud (EAPB) asignada al contrato (INT, nullable), se completa solo si grupo de atención es EAPB sin contrato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la Entidad Administradora del contrato, este campo solo se llena si el Grupo de atencion que seleccione es de EAPB Sin Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo de atención/cuidado del paciente en base de datos GENESIS (INT, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id del grupo de atencion de la base de datos de GENESIS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el paciente reside en zona apartada/rural (BIT, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'ZONAPARTADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Zona apartada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'ZONAPARTADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'ZONAPARTADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo étnico del paciente (CHAR 3, nullable): afrodescendiente, indígena, raizal, palenquero, otro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'GRUPCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del grupo etnico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'GRUPCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'GRUPCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del nivel socioeconómico del paciente (CHAR 3, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'NIVECODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del nivel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'NIVECODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'NIVECODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del idioma hablado por el paciente (CHAR 3, nullable): español, lengua originaria, otro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IDICODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del idioma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IDICODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IDICODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de discapacidad del paciente (CHAR 3, nullable): auditiva, visual, cognitiva, motriz, psicosocial, múltiple.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'DISCCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la discapacidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'DISCCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'DISCCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de creencia religiosa del paciente (CHAR 3, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CREDCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la creencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CREDCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CREDCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estrato socioeconómico del paciente (INT 1-6, nullable), dato para análisis de vulnerabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPESTRATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPESTRATO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPESTRATO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de última modificación del registro del paciente (DATETIME, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'FECREGMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Modificacion del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'FECREGMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'FECREGMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó última modificación del registro (CHAR 20, nullable), trazabilidad de cambios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que modifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODUSUMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación del registro del paciente en el sistema (DATETIME, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'FECREGCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creacion del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'FECREGCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'FECREGCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro del paciente (CHAR 20, nullable), trazabilidad de origen.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que crea', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODUSUCRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de carpeta o historia clínica del paciente (VARCHAR 25, PII Ofuscado, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'NUMCARPET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero carpeta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'NUMCARPET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'NUMCARPET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Huella dactilar del paciente en formato binario (VARBINARY MAX, nullable), dato biométrico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PACIEHUELL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Huella Dactilar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PACIEHUELL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PACIEHUELL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fotografía del paciente en formato binario (VARBINARY MAX, nullable), documento visual.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PACIEFOTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Foto del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PACIEFOTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PACIEFOTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría/trazabilidad (NUMERIC 18), registro de cambios y validaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o comentarios adicionales sobre el paciente (VARCHAR 250, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones adicionales al paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del paciente (BIT): 1=Activo, 0=Inactivo (incluye fallecido).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'ESTADOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Paciente -Cuando Muere queda en Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'ESTADOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'ESTADOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del grupo étnico al que pertenece el paciente (CHAR 3, FK→ADGRUETNI, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODGRUPOE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Grupo Etnico al cual pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODGRUPOE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODGRUPOE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del paciente (VARCHAR MAX, PII Email Ofuscado, nullable), contacto digital.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CORELEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo Electronico del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CORELEPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CORELEPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de cobertura en salud según Resolución 3047 (CHAR 1): 1=Contributivo, 2=Subsidiado Total, 3=Subsidiado Parcial, 4=Pobre sin SISBEN, 5=Pobre sin Asegurar, 6=Desplazado, 7=Plan Adicional, 8=Otros.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'TIPCOBSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Cobertura en Salud - Res.3047  1: Contributivo  2: Subsidiado Total  3: Subsidiado Parcial  4: Poblacion Pobre sin Asegurar con SISBEN  5: Poblacion Pobre sin Asegurar sin SISBEN  6: Desplazados  7: Plan de Salud Adicional  8: Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'TIPCOBSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'TIPCOBSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor RH de la sangre del paciente (CHAR 1): +=Positivo, -=Negativo (nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPRHSANGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RH:  +  -', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPRHSANGR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPRHSANGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Grupo sanguíneo del paciente (CHAR 2): A, B, AB, O (nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPGRUPSAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Grupo Sanguineo:  A  B  AB  O', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPGRUPSAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPGRUPSAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado civil del paciente (INT): 1=Soltero, 2=Casado, 3=Viudo, 4=Unión libre, 5=Separado/Divorciado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPESTADOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Estado Civil del Paciente:  1=Soltero (a)  2=Casado (a)  3=Viudo (a)  4=Union libre  5=separado(a)/Div', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPESTADOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPESTADOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sexo/Género del paciente (INT): 1=Masculino, 2=Femenino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sexo del Paciente:  1=Masculino   2=Femenino     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEXOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de ocupación o actividad económica del paciente (CHAR 5, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODACTIVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Actividad que realiza el Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODACTIVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODACTIVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de nacimiento del paciente (DATETIME, PII Ofuscado), dato demográfico crítico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPFECNACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Nacimiento del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPFECNACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPFECNACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono móvil/celular del paciente (VARCHAR MAX, PII Phone Ofuscado), contacto primario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTELMOVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico Movíl del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTELMOVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTELMOVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono fijo del paciente (VARCHAR MAX, PII Phone Ofuscado, nullable), contacto alternativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTELEFON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero Telefonico Fijo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTELEFON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTELEFON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección del domicilio del paciente (VARCHAR MAX, PII Ofuscado), ubicación física para correspondencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPDIRECCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPDIRECCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPDIRECCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del nivel socioeconómico/estrato del paciente (CHAR 2, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'NIVCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Nivel o Estrato del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'NIVCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'NIVCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de ubicación del paciente (CHAR 20, nullable): barrio, vereda o zona rural identificada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'AUUBICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Ubicacion del Paciente, en esta opcion se determina el codigo del Barrio o Ubicacion rural.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'AUUBICACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'AUUBICACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del plan de beneficios de salud del paciente (CHAR 2, nullable): cobertura específica contratada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CPPLANBEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Plan de Beneficios del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CPPLANBEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CPPLANBEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato de aseguramiento del paciente (CHAR 6, nullable), vínculo con entidad de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CCCONTRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Contrato del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CCCONTRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CCCONTRAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad aseguradora/administradora del paciente (CHAR 9, nullable): EPS, ARS, otra entidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad a la que pertenece el paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Capacidad de pago del paciente (INT): 0=No aplica, 1=Sí/100% Paciente, 2=No/Cuota recuperación, 3=Desplazado/100% Entidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CAPACIPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si el paciente tiene capacidad de pago  0: No Aplica  1: Si / 100% Paciente    2: No / Cuota Recuperacion Paciente    3: Desplazado / 100% Entidad  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CAPACIPAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CAPACIPAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de afiliación al sistema de salud (INT): 0=No aplica, 1=Cotizante, 2=Beneficiario, 3=Adicional, 4=Jubilado/Retirado, 5=Pensionado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTIPOAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Afiliacion del Paciente:  0: No Aplica  1: Cotizante  2: Beneficiario  3: Adicional  4: Jub/Retirado  5: Pensionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTIPOAFI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTIPOAFI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de paciente según régimen/vinculación (INT): 1=Contributivo, 2=Subsidiado, 3=No afiliado/Vinculado, 4=Particular, 9=Especial/Excepción, 10=Personas privadas libertad, 11-13=Asegurados ARL/SOAT/Planes voluntarios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTIPOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Tipo de Paciente:  1-Contributivo  2-Subsidiado  3-No afiliado --- Antes se llamaba Vinculado ** PBI11755 Refactoring formulario pacientes ** ---  4-Particular  5-Otro --- Se elimina de la lista desplegable del formulario ** PBI11755 Refactoring formulario pacientes ** ---  6-Desplazado Reg. Contributivo --- Se elimina de la lista desplegable del formulario ** PBI11755 Refactoring formulario pacientes ** ---  7-Desplazado Reg. Subsidiado --- Se elimina de la lista desplegable del formulario ** PBI11755 Refactoring formulario pacientes ** ---  8-Desplazado No Asegurado --- Se elimina de la lista desplegable del formulario ** PBI11755 Refactoring formulario pacientes ** ---  9- Especial o excepción --- ** Se agrega por PBI 11755 Refactoring formulario pacientes ** ---  10- Personas privadas de la libertad a cargo del Fondo Nacional de Salud --- ** Se agrega por PBI 11755 Refactoring formulario pacientes ** ---  11-Tomador / amparado ARL --- ** Se agrega por PBI 11755 REfactoring formulario pacientes ** ---  12- Tomador / amparado SOAT --- ** Se agrega por PBI 11755 Refactoring formulario pacientes ** ---  13- Tomador / amparado planes voluntarios de salud --- ** Se agrega por PBI 11755 Refactoring formulario pacientes ** ---', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTIPOPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTIPOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la empresa/empleador del paciente (CHAR 5, nullable), para afiliados contributivos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODEMPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Empresa Laboral del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODEMPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODEMPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del paciente (CHAR 250, PII Name Ofuscado): concatenación de apellidos y nombres.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Completo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPNOMCOMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre del paciente (VARCHAR 100, PII FirstName Ofuscado, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEGNOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre del paciente (VARCHAR 100, PII FirstName Ofuscado, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPPRINOMB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido del paciente (VARCHAR 100, PII SecondSurname Ofuscado, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo Nombre del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPSEGAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido del paciente (VARCHAR 100, PII FirstSurname Ofuscado, nullable).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer Apellido del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPPRIAPEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lugar o ciudad de expedición del documento de identificación (CHAR 40), referencia geográfica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPEXPEDIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lugar de Expedicion del Documento de Identificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPEXPEDIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPEXPEDIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de Identificación Tributaria (NIT) del paciente (VARCHAR 25, PII Nit Ofuscado), generado por interfaz contable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Identificacion tributaria - Este es el numero que genera la interfaz contable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODIGONIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'CODIGONIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de documento de identificación (INT): 1=CC Cédula ciudadanía, 2=CE Cédula extranjería, 3=TI Tarjeta identidad, 4=RC Registro civil, 5=PA Pasaporte, 6=AS Adulto sin ID, 7=MS Menor sin ID, 8=NU Número único, 9=CN Nacido vivo, 10=CD Carnet diplomático, 11=SC Salvoconducto, 12=PEP Permiso especial, 13=PT Permiso temporal, 14=DE Documento extranjero, 15=SI Sin ID.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Documento:   1= CC - Cédula de Ciudadanía   2= CE - Cédula de Extranjería   3= TI - Tarjeta de Identidad   4= RC - Registro Civil   5= PA - Pasaporte   6= AS - Adulto Sin Identificación   7= MS - Menor Sin Identificación   8= NU - Número único de identificación personal   9= CN - Certificado de Nacido Vivo   10= CD - Carnet Diplomático (Aplica para extranjeros)  11= SC - Salvoconducto (Aplica para extranjeros)  12= PE - Permiso Especial de Permanencia (Aplica para extranjeros)  13= PT - Permiso Temporal de Permanencia   14= DE - Documento Extranjero   15= SI - Sin Identificación    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPTIPODOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (VARCHAR 25, PII Identification_Ofuscado, PK): identificación/cédula/documento paciente, equivalente a cédula, número de identificación, expediente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si paciente pertenece a PAPSIVI (Programa Atención Psicosocial Salud Integral Víctimas Conflicto Armado) (BIT, default=0).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PoblacionPAPSIVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el paciente pertenece al Programa de Atención Psicosocial y Salud Integral a Víctimas del Conflicto Armado (PAPSIVI)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PoblacionPAPSIVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'PoblacionPAPSIVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Comunidad étnica del paciente cuando aplica (VARCHAR 100, nullable): indígena, afrodescendiente, raizal, palenquero, gitano, otra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'EthnicCommunity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que guarda la comunidad Etnica en caso de tener ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'EthnicCommunity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'EthnicCommunity';


GO
CREATE NONCLUSTERED INDEX [IX_INPACIENT_SP_Reporte]
    ON [dbo].[INPACIENT]([IPCODPACI] ASC)
    INCLUDE([IPNOMCOMP], [IPTELMOVI], [IPTELEFON], [CODENTIDA], [GENCONENTITY]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Maestro de pacientes del sistema. Contiene los datos demográficos, de identificación, contacto, afiliación y características clínicas de cada paciente registrado en la institución de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT';

GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ruta oncológica del paciente cuando aplica (INT, nullable): 1 = Incidente, 2 = Prevalente, 3 = Alta sospecha, 4 = Benigno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'OncologyCarePathway';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el campo "Ruta oncológica": 1 = Incidente, 2 = Prevalente, 3 = Alta sospecha, 4 = Benigno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'OncologyCarePathway';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INPACIENT', @level2type = N'COLUMN', @level2name = N'OncologyCarePathway';