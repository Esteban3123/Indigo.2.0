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
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [Fk_ADINGRESO_NUMINGRES];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_AMBORDLABO_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_AMBORDLABO_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_AMBORDLABO_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_AMBORDLABO_INPROFSAL];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_ADcenaten];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_INPROFSAL];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [FK_HCORDLABO_RIASCUPS];


GO
ALTER TABLE [dbo].[HCORDLABO] NOCHECK CONSTRAINT [Fk_INPACIENT_IPCODPACI];


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
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_UFUCODIGO_EXMREASIT_IDETIPHIS_NUMEFOLIO_IPCODPACI_NUMINGRES_CODCENATE_CODPROSAL_FECORDMED]
    ON [dbo].[HCORDLABO]([UFUCODIGO] ASC, [EXMREASIT] ASC)
    INCLUDE([IDETIPHIS], [NUMEFOLIO], [IPCODPACI], [NUMINGRES], [CODCENATE], [CODPROSAL], [FECORDMED], [CODSERIPS], [CANSERIPS], [OBSSERIPS], [PRISERIPS], [ESTSERIPS], [CODDIAGNO], [SERREAINT], [ESTALELAB], [FECRECMUE], [CONCURRE], [CODMOTIVOMUESTRANOCONFORME], [FECHASUGE], [IDDESCRIPCIONRELACIONADA]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORDLABO_UFUCODIGO_CUPSEntityPanelId_CANSERIPS_CODPROINT_CODPROSAL_CODSERIPS_ESTSERIPS_FECHARESULT_FECHASUGE_FECORDMED]
    ON [dbo].[HCORDLABO]([UFUCODIGO] ASC, [CUPSEntityPanelId] ASC)
    INCLUDE([CANSERIPS], [CODPROINT], [CODPROSAL], [CODSERIPS], [ESTSERIPS], [FECHARESULT], [FECHASUGE], [FECORDMED], [FECRECMUE], [GENSERVICEORDER], [IDDESCRIPCIONRELACIONADA], [INTERPRET], [IPCODPACI], [MANEXTPRO], [NUMEFOLIO], [NUMFOLINT], [NUMINGRES], [OBSSERIPS], [PROFRESULT], [USURECMUE]);


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla ServiceOrderDetail (Id)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IdServerOrderDetail';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo creado para paneles, logica solo por ahora a internacional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CUPSEntityPanelId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'campo que se diligencia si la interfaz de laboratorio se realiza por los canales de mirth connect, solo aplica para registros que en el momento de lectura del canal estan en estado 2 : Muestra recolectada     0 o NULL: - Sin Sincronizar  1: Sincronizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si el laboratoio es de microbiologia:  lo envia la interfaz y si es microbiologia es porque recibe resultados   preliminares.  0 - No es microbiologia  1 - Si es microbiologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'MICROBIOLOGIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del trámite, pueda que no tenga eventos relacionados y esto se da cuando se cancela una solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del último evento registrado al trámite', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para guardar la fecha sugerida para la toma del laboratorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECHASUGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciòn de la correlacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSERVACIONCORRELA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si se maneja correlación :   1-Si   2-No   3-Sin especificar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CORRELACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuando campo TIPO FACTURACION sea 1 guardamos  el IdRiasCups a la que aplica de lo contrario queda NULL  Relacion con la tabla RIASCUPS.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDRIASCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RIAS - Determina como se debe facturar el servicio:  1 - Rias  2 - Consulta Externa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'TIPOFACTURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'observacion de la muestra no conforme (laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSERMUESTRANOCONFORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'codigo del motivo de la muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODMOTIVOMUESTRANOCONFORME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de resultado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECHARESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'espere resultado del laboratorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESPERESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Resultado del profesional (laboratorio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'PROFRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESULTADO DE Microalbuminuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESMICROALBU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado  Fosforo (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESFOSFORO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Fosforo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECFOSFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado Albumina Serica (Longitud 5) (Unidades: g/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESALBUSERICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Albumina Serica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECALBUSERICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado PTH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESPTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha PTH', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECPTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado LDL (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESLDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha LDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECLDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de Colesterol Total (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESCOLESTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Colesterol Total', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECCOLESTOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado de la Creatinuria (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESCREATINUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creatinuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECCREATINUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado HDL  (Longitud 5) (Unidades: mg/dl)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESHDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE Baciloscopia de Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESBASDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Baciloscopia de Diagnóstico: 1. Negativa, 2. Positiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESBASDIAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE RESULTADO DE HDL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECHDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE RESULTADO DE Microalbuminuria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECMICROALBU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha resultado hemoglobina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESHEMGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemoglobina Glicosilada: Valor mínimo 5 y máximo 20, permitir decimales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESHEMGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA DE Creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESCREATININA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resultado Creatinina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESCREATININA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESULTADO DE GLISEMIA BASAL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECGLISBASAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA Hemoglobina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESHEMOGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemoglobina: Valor mínimo 1.5 y máximo 20', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESHEMOGLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TSH Neonatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESTSHNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TSH Neonatal: 1. Normal, 2. Anormal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESTSHNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESULTADO Elisa para VIH: 1. Negativo, 2. Positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESELIVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Elisa para VIH: 1. Negativo, 2. Positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESELIVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA RESULTADO Serología para Sífilis:', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESSERSIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESULTADO Serología para Sífilis: 1. No Reactiva, 2. Reactiva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESSERSIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FECHA RESULTADO Antígeno de Superficie Hepatitis B en Gestantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRESANTSUPH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESULTADO Antígeno de Superficie Hepatitis B en Gestantes: 1. Negativo, 2. Positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'RESANTSUPH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id de la orden de servicio en la cual quedo incluido el servicio de laboratorio, Este campo se llena cuando se genera la orden de servicio desde el formulario de Control de Cuenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'GENSERVICEORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si el examen se realiza en sitio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'EXMREASIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Usuario Que recolecta la muestra.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'USURECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concurrencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CONCURRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del Archivo Adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NOMARCLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Recoleccion de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECRECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Alerta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESTALELAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el Servicio Realiza Interfaz', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'SERREAINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Folio donde se Interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NUMFOLINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Medico que interpreta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODPROINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Interpretacion de Resultados (paraclinicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'INTERPRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Diagnostico Principal Relacionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este Producto Corresponde a un plan de Manejo Externo?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Servicio IPS  1: Solicitado  2: Muestra Recolectada  3: Resultado Entregado  4: Examen Interpretado  5: Remitido  6: Anulado  7: Extramural  8:Muestra Recolectada Parcialmente  9: Muestra No Conforme', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'ESTSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad del Servicio Solicitado  1: Urgente  2: Rutina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'PRISERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'OBSSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CANSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORDLABO', @level2type = N'COLUMN', @level2name = N'AUTO';

