CREATE TABLE [dbo].[ADINGRESO] (
    [NUMINGRES]                   CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [TIPOINGRE]                   INT                                                                              NOT NULL,
    [IINGREPOR]                   INT                                                                              NOT NULL,
    [ITIPORIES]                   INT                                                                              NOT NULL,
    [ICAUSAING]                   INT                                                                              NOT NULL,
    [CODENTIDA]                   CHAR (9)                                                                         NOT NULL,
    [CODCONTRA]                   CHAR (6)                                                                         NULL,
    [CODPANATE]                   CHAR (2)                                                                         NULL,
    [IFECHAING]                   DATETIME                                                                         NOT NULL,
    [ILIQUIDAC]                   INT                                                                              NOT NULL,
    [ICONTROLI]                   CHAR (15)                                                                        NOT NULL,
    [CODCENATE]                   CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                   CHAR (10)                                                                        NOT NULL,
    [IAUTORIZA]                   CHAR (15)                                                                        NULL,
    [IESTADOIN]                   CHAR (1)                                                                         NOT NULL,
    [IINGRESOA]                   CHAR (10)                                                                        NULL,
    [ISOATVALO]                   NUMERIC (18, 2)                                                                  NULL,
    [ISALCODIG]                   CHAR (3)                                                                         NULL,
    [INUMERORE]                   CHAR (15)                                                                        NULL,
    [IFECHAREM]                   DATETIME                                                                         NULL,
    [IAUTORREM]                   CHAR (15)                                                                        NULL,
    [DEPMUNCOD]                   CHAR (5)                                                                         NULL,
    [AIPSREMIS]                   CHAR (100)                                                                       NULL,
    [IOBSERVAC]                   VARCHAR (3000)                                                                   NULL,
    [IJUSTIFIC]                   CHAR (254)                                                                       NULL,
    [IREINGRES]                   INT                                                                              NOT NULL,
    [UFUINGMED]                   CHAR (10)                                                                        NULL,
    [CODPROING]                   CHAR (20)                                                                        NULL,
    [UFUEGRMED]                   CHAR (10)                                                                        NULL,
    [CODPROEGR]                   CHAR (20)                                                                        NULL,
    [UFUINGHOS]                   CHAR (10)                                                                        NULL,
    [UFUEGRHOS]                   CHAR (10)                                                                        NULL,
    [CODESPTRA]                   CHAR (3)                                                                         NULL,
    [TIPOPROFE]                   CHAR (2)                                                                         NULL,
    [UFUAACTMED]                  CHAR (10)                                                                        NULL,
    [UFUAACTHOS]                  CHAR (10)                                                                        NULL,
    [CODCAMACT]                   INT                                                                              NULL,
    [CODDIAING]                   CHAR (4)                                                                         NULL,
    [CODDIAEGR]                   CHAR (4)                                                                         NULL,
    [UFUACTPAC]                   CHAR (10)                                                                        NOT NULL,
    [CODUSUCRE]                   CHAR (20)                                                                        NULL,
    [FECREGCRE]                   DATETIME                                                                         NULL,
    [CODUSUMOD]                   CHAR (20)                                                                        NULL,
    [FECREGMOD]                   DATETIME                                                                         NULL,
    [CODUSUANU]                   CHAR (20)                                                                        NULL,
    [FECREGANU]                   DATETIME                                                                         NULL,
    [NUMINGREI]                   CHAR (10)                                                                        NULL,
    [CODICAMHO]                   CHAR (10)                                                                        NULL,
    [FECHOSPIT]                   DATETIME                                                                         NULL,
    [INDAUDFOR]                   NUMERIC (18)                                                                     NOT NULL,
    [IPRNOMBRE]                   CHAR (80)                                                                        NULL,
    [IPCODACTR]                   CHAR (15)                                                                        NULL,
    [IPEXPEDIC]                   CHAR (40)                                                                        NULL,
    [FECACTRAN]                   DATETIME                                                                         NULL,
    [HORACIDEN]                   CHAR (5)                                                                         NULL,
    [IPTELEFON]                   CHAR (15) MASKED WITH (FUNCTION = 'partial(0, "Phone_Ofuscado", 0)')             NULL,
    [OBSERACIT]                   VARCHAR (3000)                                                                   NULL,
    [OBSERAREM]                   VARCHAR (250)                                                                    NULL,
    [INGRECEXT]                   BIT                                                                              NULL,
    [PACATENDI]                   BIT                                                                              NULL,
    [ESCADOWNT]                   CHAR (1)                                                                         NULL,
    [ESCARASS]                    CHAR (1)                                                                         NULL,
    [ESCNORPAC]                   CHAR (1)                                                                         NULL,
    [SERSUSCEP]                   BIT                                                                              NULL,
    [ESCVASPAC]                   CHAR (1)                                                                         NULL,
    [ESCAPAPAC]                   CHAR (1)                                                                         NULL,
    [VIVESOLO]                    BIT                                                                              NULL,
    [NUMTRIAGEI]                  CHAR (20)                                                                        NULL,
    [GENCAREGROUP]                INT                                                                              NULL,
    [DESTINOPAC]                  TINYINT                                                                          NULL,
    [GENCONENTITY]                INT                                                                              NULL,
    [REMTRANSPOR]                 INT                                                                              NULL,
    [REMINGRES]                   INT                                                                              NULL,
    [REMACMED]                    BIT                                                                              NULL,
    [REMACAENF]                   BIT                                                                              NULL,
    [REMACENFJEF]                 BIT                                                                              NULL,
    [REMACOTRO]                   BIT                                                                              NULL,
    [REMACNINSAL]                 BIT                                                                              NULL,
    [REMACFAMILI]                 BIT                                                                              NULL,
    [REMPARACLI]                  BIT                                                                              NULL,
    [REMINFOPARA]                 VARCHAR (200)                                                                    NULL,
    [REMINCOPRINI]                INT                                                                              NULL,
    [REMFUNCREG]                  CHAR (20)                                                                        NULL,
    [TRATAESPECIA]                INT                                                                              NULL,
    [CREADOAUTOMA]                INT                                                                              NULL,
    [FECHEGRESO]                  DATETIME                                                                         NULL,
    [NUMINGREHOSP]                NCHAR (10)                                                                       NULL,
    [REQTRIAGE]                   BIT                                                                              NULL,
    [PRIMERLLA]                   BIT                                                                              NULL,
    [SEGUNDLLA]                   BIT                                                                              NULL,
    [TERCERLLA]                   BIT                                                                              NULL,
    [FECPRILLA]                   DATETIME                                                                         NULL,
    [FECSEGLLA]                   DATETIME                                                                         NULL,
    [FECTERLLA]                   DATETIME                                                                         NULL,
    [OBVAUSENT]                   VARCHAR (2000)                                                                   NULL,
    [FECAUSENT]                   DATETIME                                                                         NULL,
    [PROAUSENT]                   CHAR (20)                                                                        NULL,
    [CODTIPPAC]                   INT                                                                              NULL,
    [ESCABIERI]                   CHAR (1)                                                                         NULL,
    [GENULTLIQUI]                 DATE                                                                             NULL,
    [REINGRESOHOSP]               TINYINT                                                                          NULL,
    [PACIENTESITIOQX]             BIT                                                                              NULL,
    [CUPSPENDFAC]                 BIT                                                                              NULL,
    [FECHA]                       DATETIME                                                                         NULL,
    [LINEA]                       VARCHAR (80)                                                                     NULL,
    [ENTIDAD]                     VARCHAR (80)                                                                     NULL,
    [REGIMEN]                     VARCHAR (80)                                                                     NULL,
    [TIPOAFILIADO]                VARCHAR (80)                                                                     NULL,
    [ESTADO]                      VARCHAR (80)                                                                     NULL,
    [CONFIRMADOERP]               VARCHAR (80)                                                                     NULL,
    [REPORTAIPS]                  VARCHAR (80)                                                                     NULL,
    [SOLRESHEMO]                  BIT                                                                              CONSTRAINT [DF_ADINGRESO_SOLRESHEMO] DEFAULT ((0)) NOT NULL,
    [ESTADOMEDICAMENTOSFARMACE]   INT                                                                              NULL,
    [DEPCODIGOACC]                CHAR (2)                                                                         NULL,
    [MUNCODIGOACC]                CHAR (5)                                                                         NULL,
    [DIRECCIONACC]                VARCHAR (255)                                                                    NULL,
    [ZONAEVENTOCC]                TINYINT                                                                          NULL,
    [IdHealthPurposes]            INT                                                                              NULL,
    [IdUbication]                 INT                                                                              NULL,
    [IdEntryRoutesHealthServices] INT                                                                              NULL,
    [IdAdmissionModalities]       INT                                                                              NULL,
    [IdAdmissionType]             INT                                                                              NULL,
    [IDADTIPOIDENTIFICA]          INT                                                                              NULL,
    [Policy]                      VARCHAR (30)                                                                     NULL,
    CONSTRAINT [PK_ADINGRESO__NUMINGRES] PRIMARY KEY CLUSTERED ([NUMINGRES] ASC),
    CONSTRAINT [FK_ADINGRESO_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]) ON UPDATE CASCADE,
    CONSTRAINT [FK_ADINGRESO_AdmissionModalities] FOREIGN KEY ([IdAdmissionModalities]) REFERENCES [Admissions].[AdmissionModalities] ([Id]),
    CONSTRAINT [FK_ADINGRESO_ADTIPOIDENTIFICA_IDADTIPOIDENTIFICA] FOREIGN KEY ([IDADTIPOIDENTIFICA]) REFERENCES [dbo].[ADTIPOIDENTIFICA] ([ID]),
    CONSTRAINT [FK_ADINGRESO_ENTRYROUTESHEALTHSERVICES] FOREIGN KEY ([IdEntryRoutesHealthServices]) REFERENCES [dbo].[EntryRoutesHealthServices] ([Id]),
    CONSTRAINT [FK_ADINGRESO_INDEPARTA] FOREIGN KEY ([DEPCODIGOACC]) REFERENCES [dbo].[INDEPARTA] ([depcodigo]),
    CONSTRAINT [FK_ADINGRESO_INMunicip] FOREIGN KEY ([DEPMUNCOD]) REFERENCES [dbo].[INMUNICIP] ([DEPMUNCOD]) ON UPDATE CASCADE,
    CONSTRAINT [FK_ADINGRESO_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ADINGRESO_INPROFSAL] FOREIGN KEY ([PROAUSENT]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_ADINGRESO_INSalamin] FOREIGN KEY ([ISALCODIG]) REFERENCES [dbo].[INSALARIM] ([ISALCODIG]) ON UPDATE CASCADE,
    CONSTRAINT [FK_ADINGRESO_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]) ON UPDATE CASCADE,
    CONSTRAINT [FK_ADINGRESO_INUNIFUNC1] FOREIGN KEY ([UFUACTPAC]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_ADINGRESO_RCINCPRNIV] FOREIGN KEY ([REMINCOPRINI]) REFERENCES [dbo].[RCINCPRNIV] ([Id]),
    CONSTRAINT [FK_ADINGRESO_RCMEDING] FOREIGN KEY ([REMINGRES]) REFERENCES [dbo].[RCMEDING] ([Id]),
    CONSTRAINT [FK_ADINGRESO_RCMEDTRANS] FOREIGN KEY ([REMTRANSPOR]) REFERENCES [dbo].[RCMEDTRANS] ([Id]),
    CONSTRAINT [FK_ADINGRESO_SEGusuaru] FOREIGN KEY ([CODUSUCRE]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_ADINGRESO_SEGusuaru1] FOREIGN KEY ([CODUSUMOD]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [fk_IdAdmissionType] FOREIGN KEY ([IdAdmissionType]) REFERENCES [Admissions].[AdmissionType] ([Id])
);


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_ADcenaten];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_AdmissionModalities];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_ENTRYROUTESHEALTHSERVICES];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_INDEPARTA];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_INMunicip];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_INPacient];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_INPROFSAL];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_INSalamin];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_INUNIFUNC];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_INUNIFUNC1];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_RCINCPRNIV];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_RCMEDING];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_RCMEDTRANS];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_SEGusuaru];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [FK_ADINGRESO_SEGusuaru1];


GO
ALTER TABLE [dbo].[ADINGRESO] NOCHECK CONSTRAINT [fk_IdAdmissionType];


GO
ALTER TABLE [dbo].[ADINGRESO] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_SERSUSCEP_FECHEGRESO_IPCODPACI_IFECHAING_CODESPTRA_UFUACTPAC_ESCADOWNT_ESCARASS_ESCNORPAC_ESCVASPAC_ESCAPAPAC]
    ON [dbo].[ADINGRESO]([SERSUSCEP] ASC, [FECHEGRESO] ASC)
    INCLUDE([IPCODPACI], [IFECHAING], [CODESPTRA], [UFUACTPAC], [ESCADOWNT], [ESCARASS], [ESCNORPAC], [ESCVASPAC], [ESCAPAPAC]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_IESTADOIN_IPCODPACI_CODENTIDA_CODCAMACT_UFUACTPAC_CODTIPPAC]
    ON [dbo].[ADINGRESO]([IESTADOIN] ASC)
    INCLUDE([IPCODPACI], [CODENTIDA], [CODCAMACT], [UFUACTPAC], [CODTIPPAC]);


GO
CREATE NONCLUSTERED INDEX [IDX_IESTADOIN]
    ON [dbo].[ADINGRESO]([IESTADOIN] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_CODCENATE_UFUCODIGO_UFUEGRMED_IESTADOIN]
    ON [dbo].[ADINGRESO]([CODCENATE] ASC, [UFUCODIGO] ASC, [UFUEGRMED] ASC, [IESTADOIN] ASC)
    INCLUDE([IPCODPACI], [VIVESOLO], [CODTIPPAC]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_IESTADOIN_CODCAMACT_CODESPTRA_CODTIPPAC_UFUACTPAC_VIVESOLO]
    ON [dbo].[ADINGRESO]([IESTADOIN] ASC)
    INCLUDE([CODCAMACT], [CODESPTRA], [CODTIPPAC], [UFUACTPAC], [VIVESOLO]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_GENCONENTITY]
    ON [dbo].[ADINGRESO]([GENCONENTITY] ASC)
    INCLUDE([IPCODPACI], [CODPANATE], [CODCENATE], [UFUCODIGO], [IAUTORIZA], [CODICAMHO], [IPTELEFON]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__IPCODPACI]
    ON [dbo].[ADINGRESO]([IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__CODDIAEGR__NUMINGRES__INC__IFECHAING]
    ON [dbo].[ADINGRESO]([CODDIAEGR] ASC, [NUMINGRES] ASC)
    INCLUDE([IFECHAING]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__IESTADOIN__INC__CODCENATE__CODICAMHO__CODPANATE__IAUTORIZA__IPCODPACI__IPTELEFON__NUMINGRES__UFUCODIGO]
    ON [dbo].[ADINGRESO]([IESTADOIN] ASC)
    INCLUDE([NUMINGRES], [IPCODPACI], [CODPANATE], [CODCENATE], [UFUCODIGO], [IAUTORIZA], [CODICAMHO], [IPTELEFON]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__UFUCODIGO__IESTADOIN__INC__CODCENATE__CODICAMHO__CODPANATE__IAUTORIZA__IPCODPACI__IPTELEFON__NUMINGRES]
    ON [dbo].[ADINGRESO]([UFUCODIGO] ASC, [IESTADOIN] ASC)
    INCLUDE([NUMINGRES], [IPCODPACI], [CODPANATE], [CODCENATE], [IAUTORIZA], [CODICAMHO], [IPTELEFON]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__IFECHAING__IPCODPACI__CODCENATE__NUMINGRES__UFUCODIGO__INC__AIPSREMIS__CODCAMACT__CODCONTRA__CODDIAEGR__CODDIAING_]
    ON [dbo].[ADINGRESO]([IFECHAING] ASC, [IPCODPACI] ASC, [CODCENATE] ASC, [NUMINGRES] ASC, [UFUCODIGO] ASC)
    INCLUDE([AIPSREMIS], [CODCAMACT], [CODCONTRA], [CODDIAEGR], [CODDIAING], [CODENTIDA], [CODESPTRA], [CODICAMHO], [CODPANATE], [CODPROEGR], [CODPROING], [CODUSUANU], [CODUSUCRE], [CODUSUMOD], [DEPMUNCOD], [FECACTRAN], [FECHOSPIT], [FECREGANU], [FECREGCRE], [FECREGMOD], [HORACIDEN], [IAUTORIZA], [IAUTORREM], [ICAUSAING], [ICONTROLI], [IESTADOIN], [IFECHAREM], [IINGREPOR], [IINGRESOA], [IJUSTIFIC], [ILIQUIDAC], [INDAUDFOR], [INGRECEXT], [INUMERORE], [IOBSERVAC], [IPCODACTR], [IPEXPEDIC], [IPRNOMBRE], [IPTELEFON], [IREINGRES], [ISALCODIG], [ISOATVALO], [ITIPORIES], [NUMINGREI], [OBSERACIT], [OBSERAREM], [PACATENDI], [TIPOINGRE], [TIPOPROFE], [UFUAACTHOS], [UFUAACTMED], [UFUACTPAC], [UFUEGRHOS], [UFUEGRMED], [UFUINGHOS], [UFUINGMED]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO__CODCENATE__IESTADOIN__NUMINGRES__INC__IFECHAING]
    ON [dbo].[ADINGRESO]([CODCENATE] ASC, [IESTADOIN] ASC, [NUMINGRES] ASC)
    INCLUDE([IFECHAING]);


GO
CREATE NONCLUSTERED INDEX [UX_ADINGRESO_GENCONENTITY]
    ON [dbo].[ADINGRESO]([GENCONENTITY] ASC)
    INCLUDE([IPCODPACI], [TIPOINGRE], [IINGREPOR], [ITIPORIES], [ICAUSAING], [CODPANATE], [IFECHAING], [ILIQUIDAC], [CODCENATE], [UFUCODIGO], [IAUTORIZA], [IESTADOIN], [CODICAMHO], [IPRNOMBRE], [IPTELEFON], [GENCAREGROUP]);


GO
CREATE NONCLUSTERED INDEX [IX_ADINGRESO_IESTADOIN_CODENTIDA_CODTIPPAC_VIVESOLO]
    ON [dbo].[ADINGRESO]([IESTADOIN] ASC)
    INCLUDE([CODENTIDA], [CODTIPPAC], [VIVESOLO]);


GO

CREATE TRIGGER [dbo].[HospitalCima_Softland_Admission] 
   ON [dbo].[ADINGRESO]
   AFTER  INSERT,DELETE,UPDATE
AS 
BEGIN
	
	SET NOCOUNT ON;

	
	DECLARE @action as  int 
	declare @dataid as varchar(200)

	if exists(select NUMINGRES from inserted ) and exists(select NUMINGRES from deleted) begin
		set @action = 2 --actualizando
		set @dataid = (select top 1 NUMINGRES from inserted)
	end else if exists(select NUMINGRES from inserted ) and not exists(select NUMINGRES from deleted) begin
		set @action = 1 --insertando
		set @dataid = (select top 1 NUMINGRES from inserted)
	end else if not exists(select NUMINGRES from inserted ) and exists(select NUMINGRES from deleted) begin
		set @action = 3 --eliminando
		set @dataid = (select top 1 NUMINGRES from deleted)
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
			   (@dataid,1,@action,GETDATE(),0,NULL)
END

-----------------------------------------------------------------------------------------------------------------------------------


/****** Object:  Trigger [dbo].[HospitalCima_Softland_Patient]    Script Date: 8/24/2021 8:37:34 AM ******/
SET ANSI_NULLS ON
GO
DISABLE TRIGGER [dbo].[HospitalCima_Softland_Admission]
    ON [dbo].[ADINGRESO];


GO
create TRIGGER [dbo].[tgg_CambiaEstadoIngresoCerradoBebes] 
   ON  [dbo].[ADINGRESO]
   AFTER INSERT
AS 
BEGIN

	SET NOCOUNT ON;

	--update ADINGRESO set IESTADOIN = ' ' where IESTADOIN = 'C' AND NUMINGRES in (select NUMINGRES from inserted where CREADOAUTOMA = 1 ) 

	update ADINGRESO set IESTADOIN = ' ' from ADINGRESO i inner join inserted t on i.NUMINGRES = t.NUMINGRES where t.CREADOAUTOMA = 1 and t.IESTADOIN = 'C'

END
GO
DISABLE TRIGGER [dbo].[tgg_CambiaEstadoIngresoCerradoBebes]
    ON [dbo].[ADINGRESO];


GO
	CREATE TRIGGER [dbo].[tgg_ActualizarCampoIdAdmissionType] 
   ON  [dbo].[ADINGRESO]
   AFTER INSERT, UPDATE
AS 
BEGIN

	SET NOCOUNT ON;

	update ADINGRESO set IdAdmissionType = 1 from ADINGRESO i inner join inserted t on i.NUMINGRES = t.NUMINGRES where t.IdAdmissionType is null
	update ADINGRESO set IdAdmissionModalities = 1 from ADINGRESO i inner join inserted t on i.NUMINGRES = t.NUMINGRES where t.IdAdmissionModalities is null
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la poliza del grupo de atencion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'Policy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'se relaciona con la tabla tipo de ingreso Admission.AdmissionType', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdAdmissionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id de modalidades de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdAdmissionModalities';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda elId de Ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IdUbication';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Zona Evento:  1 - Urbana  2 - Rural  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ZONAEVENTOCC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que me especifica como esta el estado de los medicamentos de la atención farmaceutica.    - Cuando el medico modifique la prescripción medica o agregue un nuevo medicamento este campo estara en 1 que significa que esta modificado.    - Cuando el Quimico desde el dashboard de Atención farmaceutica verifique estos medicamentos este campo pasara a 0     ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ESTADOMEDICAMENTOSFARMACE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Solicitud de reservas de hemoconponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'SOLRESHEMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo solo se llena para ingresos ambulatorios.    True: Cuando existen CUPS realizados durante una HC de CE o una Nota administrativa que no han sido facturados    False: cuando no hay pendiente CUPS por facturar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CUPSPENDFAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica que el paciente ya esta en sitio para la realizacion de la cirugia.    este campo se actualiza desde control servicios ambulatorio de VIE ERP  al momento de facturar la programacion de cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'PACIENTESITIOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que permite Identificar si se definio como reingreso o no.  0- no definido  1-Definido como reingreso = SI  2-Definido como reingreso = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'REINGRESOHOSP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Paciente-Población:  1: Maternas  2: Menores de 5 Años  3: Adultos Mayores  4: Discapacitados  5: Poblacion General', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-Normal  2-Renal  3-Oncologico - Quimioterapia  4-Oncologico - Radioterapia  5-Oncologico - Braquiterapia  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TRATAESPECIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina el Destino del paciente cuando el ingreso es ambulatorio.  1 - Pacientes con Salida y órdenes médicas  2 - Pacientes con órdenes medicas sin definir estancia  3 - Pacientes con orden de estancia  4 - Paciente con orden salida parcial (se contempla logica de salida parcial. HCHISPACA  = INDICAPAC = 17 - salida parcial)    Campo que se registra apartir del destino del paciente segun el tablero   de la Historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'DESTINOPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad Funcional Actual del Paciente:  Tomada de la ultima actividad historia clinica/hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUACTPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gestion Hospitalaria:  Especifica el Codigo de la Cama Actual del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODCAMACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gestion Hospitalaria: Especifica la Unidad Funcional Actual del Paciente  Se obtiene de la cama actual del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUAACTHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Historias Clinicas: Especifica la Unidad Funcional Actual del Paciente  Se obtiene de la ultima historia clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUAACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Profesional   NULL=No Existe;  1=Medico General;  2=Medico Especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TIPOPROFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Historias Clinicas: Codigo de la Especialidad Tratante  Opcion valida solo para unidades funcionales de tipo Hospitalizacion - Se actualiza con Interconsultas. Muestra la especialidad acutal tratante, no se tiene opcion para guardar historico de especialidades tratantes, opcionalmente se puede obtener de HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'CODESPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gestion Hospitalaria: Especifica la Unidad Funcional donde se Egresa el Paciente  Nota: Se obtiene cuando se egresa de la  cama.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUEGRHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Gestion Hospitalaria: Especifica la Unidad Funcional donde se Hospitaliza el Paciente  Nota: Se obtiene cuando se asigna cama por primera vez para el ingreso.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUINGHOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Historia Clinica: Especifica la Unidad Funcional por donde Egresa el Paciente  Nota: Se Obtiene de la Historia Clinica donde se da Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUEGRMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Historias Clinicas: Especifica la Unidad Funcional por donde Ingresa el Paciente  Corresponde a la unidad funcional donde se realiza la historia de ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'UFUINGMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si es un Reingreso a urgencias  1-> Si  2-> No definido  0 -> No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IREINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica el Estado del Ingreso: '' '' = Sin Confirmar Hoja de Trabajo  ''F'' = Confirmada Hoja de Trabajo   ''A'' = Anulado  ''C'' = Cerrado  ''P'' = Parcial; Solo cuando es Integración con Vie queda un estado en ''P''  ''B'' = ingreso bloqueado, estado del ERP que hace este tipo de bloqueos en liquidación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IESTADOIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica si Liqiuida Valores a Cargo del Paciente:  1 = Copago  2 = Cuota Moderadora  3 = No Aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ILIQUIDAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica el Tipo de Riesgo:  1=Enfermedad General y Maternidad   2=Accidente de Transito   3=Catastrofe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'ITIPORIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica por donde ingreso el paciente:  1 = Urgencias   2 = Consulta Externa   3 = Nacido Hospital   4 = Remitido   5 = Hospitalización de Urgencias ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'IINGREPOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Ingreso:  1 = Ambulatorio  2 = Hospitalario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'TIPOINGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso    Nota: Codigo 00000001', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADINGRESO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';

