CREATE TABLE [dbo].[HCHISPACA] (
    [IDETIPHIS]                            CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]                            NCHAR (10)                                                                       NOT NULL,
    [CONSFOLIO]                            CHAR (10)                                                                        NULL,
    [IPCODPACI]                            VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]                            CHAR (10)                                                                        NOT NULL,
    [CODCENATE]                            CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                            CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                            CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHISPAC]                            DATETIME                                                                         NOT NULL,
    [TIPHISPAC]                            CHAR (2)                                                                         NOT NULL,
    [INDICAPAC]                            CHAR (2)                                                                         NOT NULL,
    [INDICAMED]                            VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Instructions_Ofuscado", 0)')  NULL,
    [TRAINTUNI]                            BIT                                                                              NOT NULL,
    [MOTTRAINT]                            VARCHAR (2000)                                                                   NULL,
    [ESTHISPAC]                            CHAR (1)                                                                         NOT NULL,
    [CODDIAGNO]                            CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [DATSUBJET]                            VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "DataSubjetive_Ofuscado", 0)') NULL,
    [DATOBJETI]                            VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "DataObjetive_Ofuscado", 0)')  NULL,
    [DATPRONOS]                            VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "DataForecast_Ofuscado", 0)')  NULL,
    [DATTRATAM]                            VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "DataTreatment_Ofuscado", 0)') NULL,
    [INDAUDFOR]                            NUMERIC (18)                                                                     NOT NULL,
    [CODUSUARI]                            CHAR (20)                                                                        NULL,
    [FECVISREG]                            DATETIME                                                                         NULL,
    [MOSEPICRI]                            BIT                                                                              NULL,
    [ESTAFOLIO]                            BIT                                                                              NULL,
    [CODESPTRA]                            CHAR (3)                                                                         NULL,
    [GENCONEXT]                            BIT                                                                              NULL,
    [PLAINDMED]                            VARCHAR (MAX)                                                                    NULL,
    [HCIRENAL]                             INT                                                                              NULL,
    [HCIRENALPERI]                         INT                                                                              NULL,
    [ID]                                   INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDMODELOHC]                           INT                                                                              NULL,
    [GENSERVICEORDER]                      INT                                                                              NULL,
    [CONCILIACIONMED]                      BIT                                                                              NULL,
    [JUNTAMEDICA]                          BIT                                                                              CONSTRAINT [DF__HCHISPACA__JUNTA__0A6EE669] DEFAULT ((0)) NULL,
    [RECORDANESTESIA]                      BIT                                                                              NULL,
    [REAPERTURAHC]                         BIT                                                                              CONSTRAINT [DF_HCHISPACA_REAPERTURAHC] DEFAULT ((0)) NULL,
    [HCTELEFONICA]                         BIT                                                                              CONSTRAINT [DF_HCHISPACA_HCTELEFONICA] DEFAULT ((0)) NULL,
    [MEDSOLHCTELEF]                        CHAR (20)                                                                        NULL,
    [ESPMEDSOLHCTELEF]                     CHAR (3)                                                                         NULL,
    [CAUSAEXTERNA]                         INT                                                                              NULL,
    [FINALIDAD]                            INT                                                                              NULL,
    [HCOTROSPROC]                          INT                                                                              NULL,
    [AssistedConsultationProfessional]     CHAR (20)                                                                        NULL,
    [Reformulation]                        BIT                                                                              NULL,
    [PendingValidationHCTelephone]         BIT                                                                              NULL,
    [DateValidationHCTelephone]            DATETIME                                                                         NULL,
    [INDICATION]                           INT                                                                              NULL,
    [StoryType]                            INT                                                                              CONSTRAINT [DF_HCHISPACA_StoryType] DEFAULT ((0)) NULL,
    [ReportCreatedID]                      VARCHAR (255)                                                                    NULL,
    [OmitsElectronicSignature]             INT                                                                              NULL,
    [ReasonOmitsElectronicSignature]       CHAR (4)                                                                         NULL,
    [ObservationsOmitsElectronicSignature] VARCHAR (200)                                                                    NULL,
    [IdClinicalHistoryFormats]             INT                                                                              NULL,
    [ReportVersionType]                    INT                                                                              CONSTRAINT [DF_HCHISPACA_ReportVersionType] DEFAULT ((2)) NULL,
    CONSTRAINT [PK_HCHISPACA] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC),
    CONSTRAINT [FK_CODPROSAL_INPROFSAL] FOREIGN KEY ([AssistedConsultationProfessional]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCHISPACA_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCHISPACA_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCHISPACA_HCHISPACA] FOREIGN KEY ([NUMEFOLIO], [IPCODPACI]) REFERENCES [dbo].[HCHISPACA] ([NUMEFOLIO], [IPCODPACI]),
    CONSTRAINT [FK_HCHISPACA_HCMOANULB] FOREIGN KEY ([ReasonOmitsElectronicSignature]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCHISPACA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCHISPACA_INPacient] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCHISPACA_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCHISPACA_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCHISPACA_PRMODELOHC] FOREIGN KEY ([IDMODELOHC]) REFERENCES [dbo].[PRMODELOHC] ([ID]),
    CONSTRAINT [IX_ID_HCHISPACA] UNIQUE NONCLUSTERED ([ID] ASC)
);


GO
ALTER TABLE [dbo].[HCHISPACA] NOCHECK CONSTRAINT [FK_HCHISPACA_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCHISPACA] NOCHECK CONSTRAINT [FK_HCHISPACA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCHISPACA] NOCHECK CONSTRAINT [FK_HCHISPACA_PRMODELOHC];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACA].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACA].[INDICAMED]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACA].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACA].[DATSUBJET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACA].[DATOBJETI]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACA].[DATPRONOS]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCHISPACA].[DATTRATAM]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');




GO
ALTER TABLE [dbo].[HCHISPACA] NOCHECK CONSTRAINT [FK_HCHISPACA_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCHISPACA] NOCHECK CONSTRAINT [FK_HCHISPACA_INPROFSAL];


GO
ALTER TABLE [dbo].[HCHISPACA] NOCHECK CONSTRAINT [FK_HCHISPACA_PRMODELOHC];



GO
ALTER TABLE [dbo].[HCHISPACA] NOCHECK CONSTRAINT [FK_HCHISPACA_INPROFSAL];


GO



GO
ALTER TABLE [dbo].[HCHISPACA] NOCHECK CONSTRAINT [FK_HCHISPACA_PRMODELOHC];



GO
ALTER TABLE [dbo].[HCHISPACA] NOCHECK CONSTRAINT [FK_HCHISPACA_INDIAGNOS];


GO



GO
ALTER TABLE [dbo].[HCHISPACA] NOCHECK CONSTRAINT [FK_HCHISPACA_INPROFSAL];


GO



GO
ALTER TABLE [dbo].[HCHISPACA] NOCHECK CONSTRAINT [FK_HCHISPACA_PRMODELOHC];


GO
CREATE NONCLUSTERED INDEX [idx_ReportCreatedID]
    ON [dbo].[HCHISPACA]([ReportCreatedID] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_HCHISPACA_6_1837249600__K9_K4_K5]
    ON [dbo].[HCHISPACA]([FECHISPAC] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCHISPACA_NUMINGRES_INDICAPAC]
    ON [dbo].[HCHISPACA]([NUMINGRES] ASC)
    INCLUDE([INDICAPAC]);


GO
CREATE NONCLUSTERED INDEX [IX_HCHISPACA_IPCODPACI_TIPHISPAC]
    ON [dbo].[HCHISPACA]([IPCODPACI] ASC, [TIPHISPAC] ASC)
    INCLUDE([CODCENATE], [CODDIAGNO], [CODESPTRA], [CODPROSAL], [CONSFOLIO], [DATOBJETI], [DATPRONOS], [DATSUBJET], [DATTRATAM], [ESTAFOLIO], [FECHISPAC], [HCOTROSPROC], [HCTELEFONICA], [IDMODELOHC], [NUMINGRES], [RECORDANESTESIA], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCHISPACA_IPCODPACI_TIPHISPAC_CODCENATE_CODDIAGNO_CODESPTRA_CODPROSAL_CONSFOLIO_DATOBJETI_DATPRONOS_DATSUBJET]
    ON [dbo].[HCHISPACA]([IPCODPACI] ASC, [TIPHISPAC] ASC)
    INCLUDE([CODCENATE], [CODDIAGNO], [CODESPTRA], [CODPROSAL], [CONSFOLIO], [DATOBJETI], [DATPRONOS], [DATSUBJET], [DATTRATAM], [ESTAFOLIO], [FECHISPAC], [GENCONEXT], [HCOTROSPROC], [HCTELEFONICA], [IDETIPHIS], [IDMODELOHC], [JUNTAMEDICA], [NUMINGRES], [RECORDANESTESIA], [StoryType], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCHISPACA_1]
    ON [dbo].[HCHISPACA]([IPCODPACI] ASC, [NUMINGRES] ASC, [TIPHISPAC] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_HCHISPACA_6_1837249600__K6_K9_K1_K8_K4_K16_K2_18]
    ON [dbo].[HCHISPACA]([CODCENATE] ASC, [FECHISPAC] ASC, [IDETIPHIS] ASC, [CODPROSAL] ASC, [IPCODPACI] ASC, [CODDIAGNO] ASC, [NUMEFOLIO] ASC)
    INCLUDE([DATOBJETI]);


GO
ALTER INDEX [_dta_index_HCHISPACA_6_1837249600__K6_K9_K1_K8_K4_K16_K2_18]
    ON [dbo].[HCHISPACA] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCHISPACA_2]
    ON [dbo].[HCHISPACA]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC);


GO
CREATE NONCLUSTERED INDEX [HCHISPACA_IPCODPACI]
    ON [dbo].[HCHISPACA]([IPCODPACI] ASC)
    INCLUDE([FECHISPAC], [CODESPTRA]);


GO
CREATE NONCLUSTERED INDEX [IX_HCHISPACA]
    ON [dbo].[HCHISPACA]([CODCENATE] ASC, [CODPROSAL] ASC, [FECHISPAC] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC, [UFUCODIGO] ASC);


GO
CREATE NONCLUSTERED INDEX [HistoriaClinica]
    ON [dbo].[HCHISPACA]([INDICAPAC] ASC)
    INCLUDE([NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_Historia_1]
    ON [dbo].[HCHISPACA]([TIPHISPAC] ASC, [UFUCODIGO] ASC)
    INCLUDE([CODPROSAL], [IPCODPACI], [NUMEFOLIO], [NUMINGRES]);

GO
  CREATE INDEX IX_HCHISPACA_RIPS
  ON HCHISPACA (CODCENATE, TIPHISPAC, FECHISPAC, UFUCODIGO)
  INCLUDE (NUMEFOLIO, NUMINGRES, IPCODPACI, INDICAPAC, CODDIAGNO)

  GO
  CREATE INDEX IX_HCHISPACA_ByIngreso
  ON HCHISPACA (NUMINGRES)
  INCLUDE (CODCENATE, FECHISPAC, TIPHISPAC, UFUCODIGO, NUMEFOLIO, IPCODPACI, INDICAPAC);
GO

GO
CREATE NONCLUSTERED INDEX [IX_HCHISPACA_DashboardMedicalOrders_RequestDate]
    ON [dbo].[HCHISPACA]([FECHISPAC] DESC, [NUMINGRES] ASC, [NUMEFOLIO] ASC, [IPCODPACI] ASC)
    INCLUDE([CODPROSAL], [TIPHISPAC], [INDICAMED])
    WHERE [FECHISPAC] >= '20260102';

GO
CREATE  trigger [dbo].[DetectarIngresosCruzados]
on [dbo].[HCHISPACA]
for insert,update as
begin
    declare @Ingreso as varchar(10) = (select  NUMINGRES  from inserted)
	declare @Paciente as varchar(10) = (select IPCODPACI   from inserted)
	declare @ExisteIngresoPaciente int
	declare @ExisteRecienNacido  int

			select @ExisteIngresoPaciente = count(*) from ADINGRESO where NUMINGRES = @Ingreso AND IPCODPACI  = @Paciente 
			select @ExisteRecienNacido = count(*) from HCINGRESORECNAC where (NUMINGRES = @Ingreso or NUMINGRESHIJO = @Ingreso) AND (IPCODPACI = @Paciente  or IPCODPACIHIJO = @Paciente )

			if @ExisteIngresoPaciente = 0 and @ExisteRecienNacido = 0 begin
                  raiserror ('Contacte administrador sistemas: El numero de ingreso no pertenece al paciente',10,1)
                  rollback transaction
            end
end
GO
DISABLE TRIGGER [dbo].[DetectarIngresosCruzados]
    ON [dbo].[HCHISPACA];


GO
CREATE TRIGGER [dbo].[tgrFechaFutura_HC]
   ON [dbo].[HCHISPACA]
   AFTER INSERT,UPDATE
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

    if exists(select 1 from inserted where CODCENATE IN ('13034','13032') AND cast(FECHISPAC as date) > cast(GETDATE() as date) )begin
			 /* throw 51000,'Estamos detectando un error en el sistema mediante un trigger, La fecha de HC no puede ser mayor a la actual, por favor comunicarse con el administrador del sistema.', 1
			 return*/
	       raiserror ('Contacte administrador sistemas: Estamos detectando un error en el sistema mediante un trigger: HCHISPACA, La fecha de HC no puede ser mayor a la actual',10,1)
           rollback transaction
	end
END
GO
CREATE TRIGGER [dbo].[Especialidad_NULL_HC]
   ON [dbo].[HCHISPACA]
   AFTER INSERT,UPDATE
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

if exists(select 1 from inserted where CODESPTRA is null )begin
		update hc set CODESPTRA = prof.CODESPEC1
		from HCHISPACA hc
		inner join INPROFSAL prof on prof.CODPROSAL = hc.CODPROSAL
		where hc.ID in (select ID from inserted)
end
END
GO
DISABLE TRIGGER [dbo].[Especialidad_NULL_HC]
    ON [dbo].[HCHISPACA];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de versión del reporte de historia clínica: 1=Histórico (Indigo.Software), 2=Defecto (Indigo.Reports, almacenado por defecto); NULL o 2 muestra reporte defecto, valor 1 obligatorio para leer reporte histórico. INT, constraint default 2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ReportVersionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 = Reporte histórico (definición ubicada en Indigo.Software\Definición de reportes)

2 = Reporte defecto (definición ubicada en Indigo.Reports, se guarda este valor por defecto en la tabla)

NOTA:
Valores NULL o 2 => deben mostrar el reporte defecto.
Para leer el reporte histórico la columna debe estar obligatoriamente en 1.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ReportVersionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ReportVersionType';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del formato de historia clínica hospitalaria, referencia a tabla ClinicalHistoryFormats para definir estructura y plantilla de documento. INT, clave foránea.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Id del formato hospitalario tabla: ClinicalHistoryFormats', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'IdClinicalHistoryFormats';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y justificación de la omisión de firma electrónica del paciente y/o acudiente en la historia clínica. VARCHAR(200), dato sensible PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ObservationsOmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion la cual es el porque SE omite  la firma electrónica paciente y/o acudiente.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ObservationsOmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ObservationsOmitsElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del motivo de omisión de firma electrónica (paciente/acudiente), referencia tabla HCMOANULB. CHAR(4), FK HCMOANULB, dato PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ReasonOmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla: HCMOANULB en la cual se guarda el id del motivo del porque SE omite  la firma electrónica paciente y/o acudiente.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ReasonOmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ReasonOmitsElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de omisión de firma electrónica: 0=Sin selección, 1=Firma manuscrita paciente, 2=Omitir firma paciente/acudiente. INT, dato sensible.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'OmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Omitir firma electrónica (0. Sin selección, 1. Firma del paciente manuscrita, 2. Omitir firma de paciente o acudiente)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'OmitsElectronicSignature';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'OmitsElectronicSignature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del documento PDF generado por API Snorlax en MongoDB para la historia clínica. VARCHAR(255), clave externa mongo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ReportCreatedID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta columna guarda el id de un registro creado en mongo por el API Snorlax que genera los PDF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ReportCreatedID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ReportCreatedID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de nota clínica: 0=Indefinido, 1=Farm, 2=Evolución visual, 3=Evolución, 4=Seguimiento, 5=Ingreso, 6=Nota rápida, 7=Junta médica, 8=Apoyo, 9=Otros procedimientos, 10=Indicaciones telefónicas ingreso, 11=Indicaciones telefónicas nota, 12=Informe quirúrgico ingreso, 13=Informe QX nota, 14=Informe QX, 15=Record anestesia ingreso, 16=Record anestesia nota, 17=Consulta preanestesia ingreso, 18=Consulta preanestesia nota, 19=Referencia, 20=Parto, 21=RN, 22=Extramural. INT, constraint default 0.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'StoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'-- Sin_Definir = 0  
-- Indicaciones_Atencion_Farmaceutica = 1  
-- Evolucion_Valoracion_Salud_Visual = 2 ''''rptHCEvolucion   
-- Evolucion = 3 ''''rptHCEvolucion   
-- Evolucion_Valoracion_Seguimiento = 4 ''''rptHCEvolucion   
-- Ingreso = 5 ''''rptHCIngreso  
-- Nota_Evolucion_Rapida = 6 ''''rptHCNotas  
-- Junta_Medica = 7 ''''rptHCNotas  
-- Nota_Servicios_Apoyo = 8 ''''rptHCServiciosApoyo  
-- Nota_Otros_Procedimientos = 9 ''''rptHCNotas  
-- Ingreso_Indicaciones_Telefonicas = 10 ''''rptHCNotas  
-- Nota_Indicaciones_Telefonicas = 11 ''''rptHCNotas  
-- Ingreso_Informe_Quirurgico = 12 ''''rptHCIngreso  
-- Nota_Informe_QX = 13 ''''rptHCNotas  
-- Informe_QX = 14 ''''rptHCNotas  
-- Ingreso_Record_Anestesia = 15 ''''rptHCIngreso  
-- Nota_Record_Anestesia = 16 ''''rptHCNotas  
-- Ingreso_Consulta_Pre_Anestesia = 17 ''''rptHCPreAnestesia  
-- Nota_Consulta_Pre_Anestesia = 18 ''''rptHCPreAnestesia  
-- Referencia = 19 ''''rptHCNotas  
-- Atencion_Parto = 20 ''''rptHCAtencionParto  
-- Recien_Nacido = 21 ''''rptHCRecienNacido  
-- Extramural = 22 ''''rptHCExtramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'StoryType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'StoryType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vía de indicación médica: 1=Telefónica, 2=Verbal, 3=Escrita, 4=Resultado crítico. INT, indicador de procedencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'INDICATION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1: Telefonica, 2: Verbal, 3:Escrita, 4:Resultado Crítico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'INDICATION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'INDICATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de firma/validación de historia telefónica por médico especialista o profesional de salud solicitante. DATETIME, auditoria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DateValidationHCTelephone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha firma de la HC telefonica por parte del medico especialista', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DateValidationHCTelephone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DateValidationHCTelephone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de pendencia de firma en HC telefónica: 0=Falta validación del médico que llamó, 1=Ya validada por especialista. BIT, control de flujo de autorización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'PendingValidationHCTelephone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pendiente de la Firma HC telefonicas, esto es para identificar que la HC telefonica falta por validacion del medico especialita o de quien llamo.    0 - FALTA POR LA VALIDACION DEL MEDICO QUE LLAMO  1 - YA VALIDO      ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'PendingValidationHCTelephone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'PendingValidationHCTelephone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de reformulación del plan de manejo en intrahospitalarias: SI/NO seleccionado por médico. BIT, aplicable solo HC intrahospitalarias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'Reformulation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me indica lo seleccionado por el medico en el plan manejo, esto para HC intrahospitalarias.    Reformulación SI  Reformulación NO    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'Reformulation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'Reformulation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud que realizó HC asistida (paciente agendado con otro profesional). CHAR(20), FK INPROFSAL, PII profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'AssistedConsultationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional de la consulta asistida     Este codigo de profesional es la persona que hizo la HC a un paciente PERO que ese paciente estaba agendando a otro profesional de la salud en este caso a un medio general.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'AssistedConsultationProfessional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'AssistedConsultationProfessional';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si historia clínica corresponde a categoría ''''Otros Procedimientos'''' (APD/CAPD, procedimientos diversos). INT, clasificador de tipo HC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'HCOTROSPROC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me identifica si la HC es una HC Otros Procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'HCOTROSPROC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'HCOTROSPROC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Propósito clínico de la atención: 1=Parto, 2=RN, 3=Planif.familiar, 4=Alterac.crecimiento <10a, 5=Alterac.desarrollo joven, 6=Alterac.embarazo, 7=Alterac.adulto, 8=Alterac.agudeza visual, 9=Enferm.profesional, 10=N.aplica, 11=Causa externa, 12=Diagnóstico, 13=Terapéutico, 14=Protección específica, 15=Detección temprana general, 16=Detección temprana laboral. INT, RIPS/facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'FINALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Finalidad:  1.Parto  2.Recien Nacido  3.Planificacion Familiar  4.Alteracion Crecimiento Menor 10 Años  5.Alteracion Desarrollo Joven  6.Alteracion Embarazo  7.Alteracion Adulto  8.Alteracion Agudeza Visual  9.Enfermedad Profesional  10.No Aplica  11.Causa Externa  12.Diagnóstico  13.Terapéutico  14.Protección especifica  15.Detección temprana de enfermedad general  16.Detección temprana de enfermedad laboral', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'FINALIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'FINALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación causa externa para eventos/lesiones: 1=Combate, 2=Enferm.profesional, 3=Enferm.general adulto, 4=Enferm.general pediatría, 5=Odontología, 6=Accidente tránsito, 7=Catastrófico, 8=Quemados, 9=Maternidad, 10=Accidente laboral, 11=Cirugía programada. INT, RIPS/ARL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CAUSAEXTERNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Causa Externa =   1.Heridos en Combate   2.Enfermedad Profesional  3.Enfermedad General Adulto  4.Enfermedad General Pediatría  5.Odontología  6.Accidente de Transito  7.Evento Catastrofico  8.Quemados  9.Maternidad  10.Accidente Laboral  11.Cirugia Programada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CAUSAEXTERNA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CAUSAEXTERNA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código especialidad del médico solicitante de HC telefónica (realizada por enfermero con consentimiento del solicitante). CHAR(3), dato profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ESPMEDSOLHCTELEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especialidad del Medico que solicita la HC telefonica, este medico por medio de una llamada solicita servicios y una enfermera en la clinica hace la HC desde el usuario de ella con el consentimiento del medico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ESPMEDSOLHCTELEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ESPMEDSOLHCTELEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del médico solicitante de HC telefónica (solicita servicios remotamente, enfermero documenta en HC). CHAR(20), FK INPROFSAL, PII profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'MEDSOLHCTELEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medico que solicita la HC telefonica, este medico por medio de una llamada solicita servicios y una enfermera en la clinica hace la HC desde el usuario de ella con el consentimiento del medico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'MEDSOLHCTELEF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'MEDSOLHCTELEF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si historia es HC telefónica (consulta remota documentada por enfermero): 1=Sí. BIT, clasificador tipo consulta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'HCTELEFONICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Me identifica si la HC es una HC telefonica  1 - Si    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'HCTELEFONICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'HCTELEFONICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si HC pertenece a reapertura de historia clínica cerrada previamente (permite continuidad de atención). BIT, constraint default 0, control flujo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'REAPERTURAHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si esta Historia clinica es perteneciente de una reapertura de Historia Clinica.   Ejmplo:     Hacen la Historia 1 (Le dan Salida)  Luego Hacen la reapertura  Hacen la Historia 2 (Queda en esta nueva columna en True)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'REAPERTURAHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'REAPERTURAHC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si folio corresponde a registro de anestesia (Record de Anestesia = RA). BIT, clasificador tipo nota.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'RECORDANESTESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica si el folio es de un record de Anestesia. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'RECORDANESTESIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'RECORDANESTESIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si historia fue creada en contexto de junta médica (evaluación multidisciplinaria). BIT, constraint default 0, evento clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'JUNTAMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si la historia fue creada en una junta medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'JUNTAMEDICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'JUNTAMEDICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si se realizó conciliación medicamentosa por folio: true=Sí, false=No. BIT, proceso seguridad medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CONCILIACIONMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'conciliacion medicamentosa por folio  ¿Se realizo la conciliación medicamentosa?  true : si  false: no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CONCILIACIONMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CONCILIACIONMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de orden de servicios asociada a la historia clínica, generada en sistema de órdenes. INT, FK referencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la  Id de la orden de servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único auto-incrementado de registro en tabla HCHISPACA, clave surrogate. INT IDENTITY, constraint unique.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador tipo de diálisis peritoneal en HC Unidad Renal: 1=APD (manual), 2=CAPD (automatizada). INT, procedimiento renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'HCIRENALPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HCI Renal - Unidad Renal  Campo que se registra cuando el HCI sea de tipo Peritoneal.  1 -APD Diálisis Peritoneal Manual  2 - CAPD Diálisis Peritoneal Automatizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'HCIRENALPERI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'HCIRENALPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador tipo de HC Unidad Renal: 1=Predálisis, 2=Peritoneal, 3=Hemodiálisis. INT, clasificador procedimiento renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'HCIRENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'HCI Renal - Unidad Renal  1 - Predialisis  2 - Peritoneal  3 - Hemodiálisis ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'HCIRENAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'HCIRENAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Plantilla de indicaciones médicas generales predefinida para la consulta/ingreso. VARCHAR(MAX), contenido médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'PLAINDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Plantilla de Indicaciones medicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'PLAINDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'PLAINDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si historia fue generada desde consulta externa como control (mostrar en dashboard médicos). BIT, clasificador origen HC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'GENCONEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historia Generada desde consulta externa siendo control para que salga como cabecera en el dashboard medicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'GENCONEXT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'GENCONEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código especialidad tratante actual en historias de hospitalización, actualizado por interconsultas; solo hospitalizacion, no histórico. CHAR(3), FK INESPTRA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historias Clinicas: Codigo de la Especialidad Tratante  Opcion valida solo para unidades funcionales de tipo Hospitalizacion - Se actualiza con Interconsultas. Muestra la especialidad acutal tratante, no se tiene opcion para guardar historico de especialidades tratantes, opcionalmente se puede obtener de HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODESPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODESPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del folio: 1=Activo, 0=Inactivo; indica disponibilidad para nuevas anotaciones. BIT, control acceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ESTAFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Folio Activo=1;Inactivo=0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ESTAFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ESTAFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si folio debe mostrarse en epicrisis (resumen final hospitalización). BIT, filtro reporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Mostrar en  Epicrisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'MOSEPICRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de visualización/lectura del registro de historia clínica por usuario. DATETIME, auditoría de acceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'FECVISREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Visualizacion del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'FECVISREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'FECVISREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que visualizó el registro de historia clínica. CHAR(20), PII auditoría, trazabilidad.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del usuario que visualizo el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de destino paciente post-atención: 1=Orden hospitalización, 2=Urgencias, 3=Observación, 4=Cirugía, 5=Remisión, 6=Morgue, 7=Consulta externa, 8=Salida. NUMERIC(18), decisión clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido de datos del tratamiento prescrito, capturado en sección Tratamiento de historia clínica SOAP. VARCHAR(MAX), nota clínica enmascarada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DATTRATAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que especifica los datos correspondientes al tratamiento capturada en la Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DATTRATAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DATTRATAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido de datos del pronóstico clínico, capturado en sección Pronóstico de historia clínica SOAP. VARCHAR(MAX), nota clínica enmascarada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DATPRONOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que especifica los datos correspondientes al pronostico capturada en la Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DATPRONOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DATPRONOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido de datos de evaluación objetiva (signos vitales, examen físico), sección Objetivo SOAP. VARCHAR(MAX), nota clínica enmascarada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DATOBJETI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que especifica los datos correspondientes a la evaluacion Objetiva capturada en la Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DATOBJETI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DATOBJETI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contenido de datos de evaluación subjetiva (síntomas, queja principal), sección Subjetivo SOAP. VARCHAR(MAX), nota clínica enmascarada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DATSUBJET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que especifica los datos correspondientes a la evaluacion Subjetiva capturada en la Historia Clinica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DATSUBJET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'DATSUBJET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico principal (CIE-10 mapeado), referencia tabla INDIAGNOS. CHAR(4), FK INDIAGNOS, código diagnóstico enmascarado PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de historia tipo Ingreso: A=Abierta (permite edición), C=Cerrada (finalizada). CHAR(1), control ciclo vida HC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ESTHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valido solo para historias de Tipo Ingreso y define si la historia esta cerrada o abierta  A: Abierta  C: Cerrada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ESTHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'ESTHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del motivo de traslado interno entre unidades funcionales. VARCHAR(2000), nota traslado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Motivo del traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'MOTTRAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador tipo de traslado: true=Traslado interno (misma IPS, indicación 3-4), false=Traslado a otra IPS. BIT, destino traslado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si se ha realizado un Traslado Interno a otra Unidad, Cuando en las Indicaciones Medicas la enumeracion es 3 o 4.  True: Traslado Interno  False: Traslado a otra IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'TRAINTUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones médicas generales al paciente (órdenes de medicamentos, cuidados, restricciones). VARCHAR(MAX), instrucciones enmascaradas PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones Medicas Generales al Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'INDICAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'INDICAMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Destino/destino del paciente post-evaluación: 1=Urgencias, 2=Observación urgencias, 3=Hospitalización dif unidad, 4=UCI adulto, 5=UCI pediátrica, 6=UCI neonatal, 7=Consulta externa, 8=Cirugía, 9=Hospitalización domiciliaria, 10=Referencia externa, 11=Morgue, 12=Salida, 13=Continúa unidad, 14=En tratamiento, 15=Retiro voluntario, 16=Fuga, 17=Salida parcial, 18=Estancia madre, 19=Cuidado intermedio, 20=Unidad básica, 21=Hospitalización pediatría. CHAR(2), decisión destino.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define el destino del paciente  1. Trasladar a Urgencias: Solo consulta externa  2. Trasladar a Observacion Urgencias: solo Urgencias  3. Trasladar a Hospitalizacion: Dif Misma Unidad  4. Trasladar a  UCI Adulto: Dif Misma Unidad  5. Trasladar a UCI Pediatrica: Dif Misma Unidad  6. Trasladar a UCI Neonatal: Dif Misma Unidad  7. Trasladar a Consulta Externa: Dif Misma Unidad  8. Trasladar a  Cirugia: Dif Misma Unidad  9. Hospitalizacion en Casa  10. Referencia  11. Morgue  12. Salida  13. Continua en la Unidad  14.Paciente en Tratamiento  15. Retiro Voluntario  16. Fuga  17.Salida Parcial  18.Estancia Con la Madre  19.U.Cuidado Intermedio  20.U.Basica  21.Hospitalización Pediatría', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'INDICAPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'INDICAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de historia clínica: I=Ingreso, E=Evolución, O=Otros modelos apoyo, N=Notas, PT=Partograma, NF=Nota farmacéutica, V=Valoración seguimiento, F=Preanestesia, JM=Junta médica, T=Ingreso unidad diferente, P=Parto, B=Recién nacido. CHAR(2), clasificador estructura HC.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'TIPHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si la historia es de Ingreso o Evolucion  I: Ingreso  E: Evolucion  O: Otros modelos de apoyo  N: Notas  PT:Partograma  NF: Nota Farmaceutica  V: Valoración de Seguimiento  F:Consulta Preanestesia
JM - Junta Medica
T - Historia de Ingreso en unidad diferente a la inicial
P - historia de partos
B - historia de recien nacido
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'TIPHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'TIPHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación/documentación de la historia clínica del paciente. DATETIME, auditoria temporal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Historia Clinica Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'FECHISPAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'FECHISPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de salud (médico, enfermero, especialista) que documentó la historia clínica. VARCHAR(25), FK INPROFSAL, PII profesional enmascarado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional donde se generó la HC (hospitalización, urgencias, consulta externa, UCI). CHAR(10), FK INUNIFUNC, ubicación clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro/institución de atención donde se realizó la HC (clínica, hospital, centro médico). CHAR(10), FK ADCENATEN, institución prestadora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de ingreso/admisión del paciente a la institución, vinculado a HC. CHAR(10), FK ADINGRESO, clave evento hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de identificación del paciente (cédula, pasaporte, carné extranjero), PII principal. VARCHAR(25), FK INPACIENT, identificación enmascarada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo secuencial de evoluciones dentro de una HC de Ingreso del paciente. CHAR(10), numerador de notas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de Evoluciones de la Historia Clinica de Ingreso del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'CONSFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de folio (historia clínica), identificador primario de la HC. NCHAR(10), PK parte, clave historia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre/identificador interno del tipo de historia clínica en sistema Indigo Vie. CHAR(9), clasificador tipo estructura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historias clínicas de pacientes: registra cada folio o nota clínica generada durante una atención, incluyendo datos subjetivos, objetivos, diagnóstico, tratamiento, indicaciones médicas, pronóstico y estado del documento, asociado al paciente, ingreso, profesional de salud y unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del modelo o plantilla de historia clínica utilizado para generar el folio, permite relacionar el registro con el formato estructurado de HC configurado en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHISPACA', @level2type = N'COLUMN', @level2name = N'IDMODELOHC';

GO
CREATE NONCLUSTERED INDEX [IX_HCHISPACA_NUMINGRES]
    ON [dbo].[HCHISPACA]([NUMINGRES] ASC)
    INCLUDE([INDICAPAC]);
