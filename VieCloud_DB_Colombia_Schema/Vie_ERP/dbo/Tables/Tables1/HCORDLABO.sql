CREATE TABLE [dbo].[HCORDLABO] (
    [AUTO]                          INT                                                                               IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]                     CHAR (9)                                                                          NOT NULL,
    [NUMEFOLIO]                     NCHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')  NOT NULL,
    [NUMINGRES]                     CHAR (10)                                                                         NOT NULL,
    [CODCENATE]                     CHAR (10)                                                                         NOT NULL,
    [UFUCODIGO]                     CHAR (10)                                                                         NOT NULL,
    [CODPROSAL]                     CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')     NOT NULL,
    [FECORDMED]                     DATETIME                                                                          NOT NULL,
    [CODSERIPS]                     CHAR (20)                                                                         NOT NULL,
    [CANSERIPS]                     INT                                                                               NOT NULL,
    [OBSSERIPS]                     VARCHAR (2000)                                                                    NULL,
    [PRISERIPS]                     CHAR (1)                                                                          NOT NULL,
    [ESTSERIPS]                     CHAR (1)                                                                          NULL,
    [MANEXTPRO]                     BIT                                                                               NOT NULL,
    [CODDIAGNO]                     CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')      NULL,
    [INTERPRET]                     VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Interpretation_Ofuscado", 0)') NULL,
    [CODPROINT]                     CHAR (20)                                                                         NULL,
    [NUMFOLINT]                     NCHAR (10)                                                                        NULL,
    [SERREAINT]                     BIT                                                                               NOT NULL,
    [INDAUDFOR]                     NUMERIC (18)                                                                      NOT NULL,
    [ESTALELAB]                     BIT                                                                               NOT NULL,
    [FECRECMUE]                     DATETIME                                                                          NULL,
    [NOMARCLAB]                     CHAR (250)                                                                        NULL,
    [CONCURRE]                      ROWVERSION                                                                        NULL,
    [USURECMUE]                     CHAR (20)                                                                         NULL,
    [EXMREASIT]                     BIT                                                                               CONSTRAINT [DF_HCORDLABO_EXMREASIT] DEFAULT ((0)) NOT NULL,
    [GENSERVICEORDER]               INT                                                                               NULL,
    [RESANTSUPH]                    TINYINT MASKED WITH (FUNCTION = 'default()')                                      NULL,
    [FECRESANTSUPH]                 DATETIME                                                                          NULL,
    [RESSERSIF]                     TINYINT MASKED WITH (FUNCTION = 'default()')                                      NULL,
    [FECRESSERSIF]                  DATETIME                                                                          NULL,
    [RESELIVIH]                     TINYINT                                                                           NULL,
    [FECRESELIVIH]                  DATETIME MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [RESTSHNEO]                     TINYINT                                                                           NULL,
    [FECRESTSHNEO]                  DATETIME                                                                          NULL,
    [RESHEMOGLO]                    DECIMAL (18, 2)                                                                   NULL,
    [FECRESHEMOGLO]                 DATETIME                                                                          NULL,
    [FECGLISBASAL]                  DATETIME MASKED WITH (FUNCTION = 'default()')                                     NULL,
    [RESCREATININA]                 DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECRESCREATININA]              DATETIME                                                                          NULL,
    [RESHEMGLO]                     DECIMAL (18, 2)                                                                   NULL,
    [FECRESHEMGLO]                  DATETIME                                                                          NULL,
    [FECMICROALBU]                  DATETIME                                                                          NULL,
    [FECHDL]                        DATETIME                                                                          NULL,
    [RESBASDIAG]                    TINYINT                                                                           NULL,
    [FECRESBASDIAG]                 DATETIME                                                                          NULL,
    [RESHDL]                        DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECCREATINUR]                  DATETIME                                                                          NULL,
    [RESCREATINUR]                  DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECCOLESTOTAL]                 DATETIME                                                                          NULL,
    [RESCOLESTOTAL]                 DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECLDL]                        DATETIME                                                                          NULL,
    [RESLDL]                        DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECPTH]                        DATETIME                                                                          NULL,
    [RESPTH]                        DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECALBUSERICA]                 DATETIME                                                                          NULL,
    [RESALBUSERICA]                 DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [FECFOSFOR]                     DATETIME                                                                          NULL,
    [RESFOSFORO]                    DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [RESMICROALBU]                  DECIMAL (18, 2) MASKED WITH (FUNCTION = 'default()')                              NULL,
    [PROFRESULT]                    NCHAR (20)                                                                        NULL,
    [ESPERESULT]                    CHAR (3)                                                                          NULL,
    [FECHARESULT]                   DATETIME                                                                          NULL,
    [CODMOTIVOMUESTRANOCONFORME]    CHAR (4)                                                                          NULL,
    [OBSERMUESTRANOCONFORME]        VARCHAR (MAX)                                                                     NULL,
    [TIPOFACTURACION]               INT                                                                               NULL,
    [IDRIASCUPS]                    INT                                                                               NULL,
    [CORRELACION]                   TINYINT                                                                           CONSTRAINT [DF_CORRELACION] DEFAULT ((2)) NOT NULL,
    [OBSERVACIONCORRELA]            VARCHAR (4000)                                                                    NULL,
    [FECHASUGE]                     DATETIME                                                                          NULL,
    [IDDESCRIPCIONRELACIONADA]      INT                                                                               NULL,
    [TraceabilityPaperworkEventsId] INT                                                                               NULL,
    [TraceabilityPaperworkId]       INT                                                                               NULL,
    [MICROBIOLOGIA]                 BIT                                                                               NULL,
    [SYNCMIRTH]                     BIT                                                                               NULL,
    [CUPSEntityPanelId]             INT                                                                               NULL,
    [IdServerOrderDetail]           INT                                                                               NULL,
    [Principal] BIT NULL,
    [AuthorizationEventId]          INT            NULL,
    CONSTRAINT [PK_HCORDLABO] PRIMARY KEY CLUSTERED ([AUTO] ASC),
    CONSTRAINT [Fk_ADINGRESO_NUMINGRES] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_AMBORDLABO_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AMBORDLABO_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AMBORDLABO_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_AMBORDLABO_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDLABO_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDLABO_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCORDLABO_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORDLABO_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDLABO_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORDLABO_RIASCUPS] FOREIGN KEY ([IDRIASCUPS]) REFERENCES [dbo].[RIASCUPS] ([ID]),
    CONSTRAINT [FK_IdServerOrderDetail_HCORDLABO] FOREIGN KEY ([IdServerOrderDetail]) REFERENCES [Billing].[ServiceOrderDetail] ([Id]),
    CONSTRAINT [Fk_INPACIENT_IPCODPACI] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_AMBORDLABO_INPROFSAL];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_INPROFSAL];


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[CODDIAGNO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[INTERPRET]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[RESANTSUPH]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[RESSERSIF]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[FECRESELIVIH]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[FECGLISBASAL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[RESCREATININA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[RESHDL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[RESCREATINUR]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[RESCOLESTOTAL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[RESLDL]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[RESPTH]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[RESALBUSERICA]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[RESFOSFORO]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCORDLABO].[RESMICROALBU]
    WITH (LABEL = 'Sensitive - Health', INFORMATION_TYPE = 'Health');



GO
CREATE NONCLUSTERED INDEX [IDX_HCORDLABO_ESTSERIPS]
    ON [dbo].[HCORDLABO]([ESTSERIPS] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_CODCENATE_ESTSERIPS_CODPROSAL_CODSERIPS_FECORDMED_IPCODPACI_NOMARCLAB_NUMEFOLIO_NUMINGRES]
    ON [dbo].[HCORDLABO]([CODCENATE] ASC, [ESTSERIPS] ASC)
    INCLUDE([CODPROSAL], [CODSERIPS], [FECORDMED], [IPCODPACI], [NOMARCLAB], [NUMEFOLIO], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_ESTSERIPS_UFUCODIGO_CODSERIPS]
    ON [dbo].[HCORDLABO]([ESTSERIPS] ASC, [UFUCODIGO] ASC, [CODSERIPS] ASC)
    INCLUDE([IPCODPACI], [NUMEFOLIO], [NUMINGRES]);


GO
ALTER INDEX [IX_HCORDLABO_ESTSERIPS_UFUCODIGO_CODSERIPS]
    ON [dbo].[HCORDLABO] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_CODSERIPS_MANEXTPRO_IDDESCRIPCIONRELACIONADA]
    ON [dbo].[HCORDLABO]([CODSERIPS] ASC, [MANEXTPRO] ASC, [IDDESCRIPCIONRELACIONADA] ASC)
    INCLUDE([NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODCENATE], [UFUCODIGO], [CODPROSAL], [FECORDMED], [CANSERIPS], [OBSSERIPS], [CODDIAGNO], [TraceabilityPaperworkEventsId], [TraceabilityPaperworkId]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_NUMINGRES_CUPSEntityPanelId_CANSERIPS_CODPROINT_CODPROSAL_CODSERIPS_ESTSERIPS_FECHARESULT_FECHASUGE_FECORDMED_GENSE]
    ON [dbo].[HCORDLABO]([NUMINGRES] ASC, [CUPSEntityPanelId] ASC)
    INCLUDE([CANSERIPS], [CODPROINT], [CODPROSAL], [CODSERIPS], [ESTSERIPS], [FECHARESULT], [FECHASUGE], [FECORDMED], [GENSERVICEORDER], [IDDESCRIPCIONRELACIONADA], [INTERPRET], [IPCODPACI], [MANEXTPRO], [NUMEFOLIO], [NUMFOLINT], [OBSSERIPS], [PROFRESULT], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_CODSERIPS_FECRECMUE_SYNCMIRTH]
    ON [dbo].[HCORDLABO]([CODSERIPS] ASC, [FECRECMUE] ASC, [SYNCMIRTH] ASC)
    INCLUDE([NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODCENATE], [UFUCODIGO], [CODPROSAL], [FECORDMED], [CANSERIPS], [OBSSERIPS], [PRISERIPS], [ESTSERIPS], [CODDIAGNO], [USURECMUE], [IDDESCRIPCIONRELACIONADA]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO]
    ON [dbo].[HCORDLABO]([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [MANEXTPRO] ASC, [IDDESCRIPCIONRELACIONADA] ASC);


GO
CREATE NONCLUSTERED INDEX [_dta_index_HCORDLABO_6_377820458__K4_K5_K10]
    ON [dbo].[HCORDLABO]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC);


GO
CREATE NONCLUSTERED INDEX [Ix_OrdenesLaboratorio]
    ON [dbo].[HCORDLABO]([IPCODPACI] ASC, [NUMINGRES] ASC)
    INCLUDE([CODSERIPS], [FECORDMED], [INTERPRET], [MANEXTPRO], [NUMEFOLIO], [NUMFOLINT]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_DashboardMedicalOrders_CareCenter_RequestDate]
    ON [dbo].[HCORDLABO]([CODCENATE] ASC, [FECORDMED] DESC)
    INCLUDE([AUTO], [UFUCODIGO], [NUMINGRES], [NUMEFOLIO], [IPCODPACI], [CODPROSAL], [CANSERIPS], [CODSERIPS], [IDDESCRIPCIONRELACIONADA], [OBSSERIPS])
    WHERE [MANEXTPRO] = 1 AND [ESTSERIPS] <> '6' AND [FECORDMED] >= '20260102';


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_UFUCODIGO_EXMREASIT_IDETIPHIS_NUMEFOLIO_IPCODPACI_NUMINGRES_CODCENATE_CODPROSAL_FECORDMED]
    ON [dbo].[HCORDLABO]([UFUCODIGO] ASC, [EXMREASIT] ASC)
    INCLUDE([IDETIPHIS], [NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODCENATE], [CODPROSAL], [FECORDMED], [CODSERIPS], [CANSERIPS], [OBSSERIPS], [PRISERIPS], [ESTSERIPS], [CODDIAGNO], [SERREAINT], [ESTALELAB], [FECRECMUE], [CONCURRE], [CODMOTIVOMUESTRANOCONFORME], [FECHASUGE], [IDDESCRIPCIONRELACIONADA]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_UFUCODIGO_CUPSEntityPanelId_CANSERIPS_CODPROINT_CODPROSAL_CODSERIPS_ESTSERIPS_FECHARESULT_FECHASUGE_FECORDMED]
    ON [dbo].[HCORDLABO]([UFUCODIGO] ASC, [CUPSEntityPanelId] ASC)
    INCLUDE([CANSERIPS], [CODPROINT], [CODPROSAL], [CODSERIPS], [ESTSERIPS], [FECHARESULT], [FECHASUGE], [FECORDMED], [FECRECMUE], [GENSERVICEORDER], [IDDESCRIPCIONRELACIONADA], [INTERPRET], [IPCODPACI], [MANEXTPRO], [NUMEFOLIO], [NUMFOLINT], [NUMINGRES], [OBSSERIPS], [PROFRESULT], [USURECMUE]);


GO
ALTER INDEX [IX_HCORDLABO_UFUCODIGO_CUPSEntityPanelId_CANSERIPS_CODPROINT_CODPROSAL_CODSERIPS_ESTSERIPS_FECHARESULT_FECHASUGE_FECORDMED]
    ON [dbo].[HCORDLABO] DISABLE;

GO

CREATE NONCLUSTERED INDEX IX_HCORDLABO_Paciente_Ingreso_Estado
ON dbo.HCORDLABO
(
    IPCODPACI,
    NUMINGRES,
    ESTSERIPS,
    MANEXTPRO
)
INCLUDE
(
    AUTO,
    CODSERIPS
);


GO

CREATE TRIGGER [dbo].[HospitalCima_Softland_LaboratoryOrder] 
   ON [dbo].[HCORDLABO]
   AFTER  INSERT,DELETE,UPDATE
AS 
BEGIN
	
	SET NOCOUNT ON;

	
	DECLARE @action as  int 
	declare @dataid as varchar(200)

	if exists(select AUTO from inserted ) and exists(select AUTO from deleted) begin
		set @action = 2 --actualizando
		set @dataid = (select top 1 AUTO from inserted)
	end else if exists(select AUTO from inserted ) and not exists(select AUTO from deleted) begin
		set @action = 1 --insertando
		set @dataid = (select top 1 AUTO from inserted)
	end else if not exists(select AUTO from inserted ) and exists(select AUTO from deleted) begin
		set @action = 3 --eliminando
		set @dataid = (select top 1 AUTO from deleted)
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
			   (@dataid,3,@action,GETDATE(),0,NULL)

END

--------------------------------------------------------------------------------------------------------------------
/****** Object:  Trigger [dbo].[HospitalCima_Softland_ImageOrder]    Script Date: 8/24/2021 8:37:34 AM ******/
SET ANSI_NULLS ON
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a Billing.ServiceOrderDetail(Id). Referencia a detalle de orden de servicio en la cual quedó incluido el laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relación con la tabla ServiceOrderDetail (Id)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. ID de panel CUPS, creado para lógica internacional. Campo de gestión de paneles de servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CUPSEntityPanelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo creado para paneles, logica solo por ahora a internacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CUPSEntityPanelId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CUPSEntityPanelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Sincronización Mirth Connect: 0/NULL=Sin sincronizar, 1=Sincronizado. Solo aplica cuando estado servicio IPS=2 (Muestra recolectada) y se realiza por interfaz Mirth.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'campo que se diligencia si la interfaz de laboratorio se realiza por los canales de mirth connect, solo aplica para registros que en el momento de lectura del canal estan en estado 2 : Muestra recolectada     0 o NULL: - Sin Sincronizar  1: Sincronizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indica si laboratorio es microbiología (1=Sí, 0=No). Se envía por interfaz; si es microbiología recibe resultados preliminares.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si el laboratoio es de microbiologia:  lo envia la interfaz y si es microbiologia es porque recibe resultados   preliminares.  0 - No es microbiologia  1 - Si es microbiologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a trámite/paperwork. ID de cabecera del trámite, puede ser NULL si se cancela la solicitud sin eventos relacionados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del trámite, pueda que no tenga eventos relacionados y esto se da cuando se cancela una solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. ID del último evento registrado al trámite de la orden de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del último evento registrado al trámite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a VIE ERP contract.CUPSEntityContractDescriptions. ID de descripción de relación contractual del servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha sugerida para la toma/recolección del laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECHASUGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para guardar la fecha sugerida para la toma del laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECHASUGE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECHASUGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(4000). Observaciones sobre la correlación o relación de resultados del laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciòn de la correlacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Manejo de correlación: 1=Sí, 2=No, 3=Sin especificar. Default=2.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CORRELACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Determina si se maneja correlación :   1-Si   2-No   3-Sin especificar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CORRELACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CORRELACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT, FK a RIASCUPS(ID). ID RIAS a la cual aplica cuando TIPOFACTURACION=1, NULL si no aplica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Tipo de facturación del servicio: 1=RIAS, 2=Consulta Externa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX). Observaciones del laboratorio respecto a muestra no conforme o rechazada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'observacion de la muestra no conforme (laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4). Código del motivo de rechazo de muestra no conforme.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del motivo de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha en que se registraron/emitieron los resultados del laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECHARESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECHARESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(3). Estado de espera de resultado: indica si hay resultados pendientes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESPERESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'espere resultado del laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESPERESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESPERESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NCHAR(20), FK a profesional. Código del profesional (médico/laboratorista) que valida/firma resultados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'PROFRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado del profesional (laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'PROFRESULT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'PROFRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Resultado de microalbuminuria en mg/dL, indicador renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RESULTADO DE Microalbuminuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESMICROALBU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Resultado de fósforo en mg/dL, marcador metabólico óseo-mineral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESFOSFORO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado  Fosforo (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESFOSFORO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESFOSFORO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado del examen de fósforo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECFOSFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Fosforo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECFOSFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECFOSFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Resultado de albúmina sérica en g/dL, marcador nutricional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado Albumina Serica (Longitud 5) (Unidades: g/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESALBUSERICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de albúmina sérica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Albumina Serica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECALBUSERICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECALBUSERICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Resultado de hormona paratiroidea (PTH), marcador calcio-fósforo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado PTH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESPTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de PTH.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha PTH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECPTH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECPTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Resultado de colesterol LDL en mg/dL, perfil lipídico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado LDL (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESLDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de LDL.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha LDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECLDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECLDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Resultado de colesterol total en mg/dL, perfil lipídico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de Colesterol Total (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESCOLESTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de colesterol total.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Colesterol Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECCOLESTOTAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECCOLESTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Resultado de creatinina urinaria en mg/dL, evaluación renal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado de la Creatinuria (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESCREATINUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de creatinuria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creatinuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECCREATINUR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECCREATINUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Resultado de colesterol HDL en mg/dL, perfil lipídico protector.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado HDL  (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESHDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de baciloscopia de diagnóstico TB.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA DE Baciloscopia de Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESBASDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Resultado baciloscopia diagnóstica TB: 1=Negativa, 2=Positiva.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Baciloscopia de Diagnóstico: 1. Negativa, 2. Positiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESBASDIAG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESBASDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de HDL (colesterol protector).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA DE RESULTADO DE HDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECHDL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECHDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de microalbuminuria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA DE RESULTADO DE Microalbuminuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECMICROALBU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECMICROALBU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de hemoglobina glicosilada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha resultado hemoglobina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESHEMGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Resultado hemoglobina glicosilada (HbA1c), control glucémico. Rango 1.5-20.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hemoglobina Glicosilada: Valor mínimo 5 y máximo 20, permitir decimales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESHEMGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESHEMGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de creatinina sérica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA DE Creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESCREATININA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Resultado creatinina sérica, marcador función renal, PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Resultado Creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESCREATININA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESCREATININA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de glisemia (glucosa) basal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECGLISBASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RESULTADO DE GLISEMIA BASAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECGLISBASAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECGLISBASAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de hemoglobina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA Hemoglobina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESHEMOGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DECIMAL(18,2). Resultado hemoglobina en g/dL, cuantificación eritrocitos. Rango 1.5-20.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hemoglobina: Valor mínimo 1.5 y máximo 20', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESHEMOGLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESHEMOGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de TSH neonatal (screening metabólico).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TSH Neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESTSHNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Resultado TSH neonatal: 1=Normal, 2=Anormal. Screening congénito.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'TSH Neonatal: 1. Normal, 2. Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESTSHNEO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESTSHNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de serología ELISA VIH.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RESULTADO Elisa para VIH: 1. Negativo, 2. Positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESELIVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Resultado ELISA VIH: 1=Negativo, 2=Positivo. Prueba diagnóstica VIH, PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Elisa para VIH: 1. Negativo, 2. Positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESELIVIH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESELIVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de serología sífilis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA RESULTADO Serología para Sífilis:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESSERSIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Resultado serología sífilis: 1=No Reactiva, 2=Reactiva. ETS screening.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RESULTADO Serología para Sífilis: 1. No Reactiva, 2. Reactiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESSERSIF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESSERSIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de ejecución/resultado de antígeno superficie hepatitis B en gestantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'FECHA RESULTADO Antígeno de Superficie Hepatitis B en Gestantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESANTSUPH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TINYINT. Resultado HBsAg en gestante: 1=Negativo, 2=Positivo. Prevención transmisión vertical, PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RESULTADO Antígeno de Superficie Hepatitis B en Gestantes: 1. Negativo, 2. Positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESANTSUPH';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESANTSUPH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. ID de orden de servicio que agrupa este laboratorio, generada desde Control de Cuenta.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de laboratorio, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Define si examen se realiza en sitio (1) o es extramural (0).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'EXMREASIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Define si el examen se realiza en sitio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'EXMREASIT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'EXMREASIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20). Código del usuario/profesional que realiza recolección de muestra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Usuario Que recolecta la muestra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'USURECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'USURECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIMESTAMP. Control de concurrencia para sincronización de registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CONCURRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(250). Nombre del archivo adjunto o documento asociado al laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de recolección/toma de la muestra biológica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Recoleccion de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRECMUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Estado de alerta laboral: 1=Con alerta, 0=Sin alerta. Indicador para seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESTALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESTALELAB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESTALELAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NUMERIC(18,0). Código de auditoría/seguimiento de la orden de laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Especifica si servicio realiza interfaz electrónica (1=Sí, 0=No) con sistemas externos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si el Servicio Realiza Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'SERREAINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'SERREAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NCHAR(10). Número de folio donde se registra interpretación de paraclinicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Folio donde se Interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20). Código del médico/profesional que interpreta resultados paraclinicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Medico que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODPROINT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(MAX). Interpretación clínica de resultados paraclinicos, análisis profesional, PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Interpretacion de Resultados (paraclinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'INTERPRET';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(4), FK a INDIAGNOS(CODDIAGNO). Código diagnóstico principal relacionado, PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico Principal Relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'BIT. Indica si producto/servicio corresponde a plan de manejo externo (1=Sí, 0=No).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(1). Estado servicio IPS: 1=Solicitado, 2=Muestra recolectada, 3=Resultado entregado, 4=Examen interpretado, 5=Remitido, 6=Anulado, 7=Extramural, 8=Muestra parcial, 9=No conforme.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado Servicio IPS  
1: Solicitado  
2: Muestra Recolectada  
3: Resultado Entregado  
4: Examen Interpretado  
5: Remitido  
6: Anulado  
7: Extramural  
8:Muestra Recolectada Parcialmente  
9: Muestra No Conforme

', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(1). Prioridad servicio: 1=Urgente, 2=Rutina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(2000). Observaciones del servicio de laboratorio, hallazgos relevantes.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT. Cantidad de servicios/exámenes solicitados en esta orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CANSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(20), FK a INCUPSIPS(CODSERIPS). Código único CUPS-IPS del procedimiento/servicio laboral.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DATETIME. Fecha de solicitud/orden médica del laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECORDMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(25), FK a INPROFSAL(CODPROSAL). Código del profesional solicitante, PII.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10), FK a INUNIFUNC(UFUCODIGO). Código de unidad funcional que solicita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10), FK a ADCENATEN(CODCENATE). Código del centro de atención/punto de servicio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(10), FK a ADINGRESO(NUMINGRES). Número de ingreso/atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VARCHAR(25). Código/ID del paciente (cédula, identificación), PII ofuscado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NCHAR(10). Número de folio o expediente de la historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHAR(9). Nombre interno del tipo de historia clínica (EHR, ambulatoria, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INT IDENTITY. ID autoincrementable, clave primaria de HCORDLABO.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'AUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_Paciente_UF_CodSer]
    ON [dbo].[HCORDLABO]([IPCODPACI] ASC, [UFUCODIGO] ASC, [CODSERIPS] ASC)
    INCLUDE([AUTO], [NUMEFOLIO], [FECORDMED], [IDETIPHIS], [SERREAINT]);


GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Indica cual es el laboratorio principal de la atención - Solo aplica para unidad de urgencias',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'HCORDLABO',
    @level2type = N'COLUMN',
    @level2name = N'Principal'


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes médicas de laboratorio clínico registradas en la historia clínica del paciente. Contiene cada examen de laboratorio solicitado por un profesional de salud durante un ingreso o atención, incluyendo el estado de la orden, resultados de exámenes específicos (hemoglobina, creatinina, glucosa basal, colesterol, HDL, LDL, PTH, microalbuminuria, entre otros), recepción de muestra y trazabilidad del proceso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del último evento de autorización con estado Autorizado registrado para esta orden. Actualizado automáticamente en la misma transacción al guardar el evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'AuthorizationEventId';
