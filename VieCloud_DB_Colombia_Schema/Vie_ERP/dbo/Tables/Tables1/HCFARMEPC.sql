CREATE TABLE [dbo].[HCFARMEPC] (
    [CODCONCEC]           NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]           CHAR (9)                                                                         NULL,
    [NUMEFOLIO]           NCHAR (10)                                                                       NULL,
    [FECHAORDE]           DATETIME                                                                         NOT NULL,
    [CODPROSAL]           CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [IPCODPACI]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]           CHAR (10)                                                                        NOT NULL,
    [CODCENATE]           CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]           CHAR (10)                                                                        NOT NULL,
    [CODCONCEP]           CHAR (10)                                                                        NULL,
    [CODCONCES]           CHAR (10)                                                                        NULL,
    [ORDESTADO]           CHAR (1)                                                                         NOT NULL,
    [CODBODEGA]           VARCHAR (20)                                                                     NOT NULL,
    [CODCENCOS]           CHAR (14)                                                                        NOT NULL,
    [ORDTRANUE]           CHAR (1)                                                                         NOT NULL,
    [JUSANULAC]           CHAR (250)                                                                       NULL,
    [MEDICAMENTOVALIDADO] BIT                                                                              NULL,
    [IDAGEPROGQX]         INT                                                                              NULL,
    [TIPOSOLICITUD]       INT                                                                              NULL,
    [TIPOSOLQX]           INT                                                                              NULL,
    [ORDENQUIMIO]         BIT                                                                              NULL,
    [IDCITA]              INT                                                                              NULL,
    [IDHCORDPRON]         INT                                                                              NULL,
    [USUAREGISTRO]        CHAR (20)                                                                        NULL,
    [ConfirmationStatus]  TINYINT                                                                          CONSTRAINT [DF__HCFARMEPC__Confi__6B1636A1] DEFAULT ((2)) NULL,
    [MedicalOrderType]    TINYINT                                                                          CONSTRAINT [DF__HCFARMEPC__Medic__4A6A3B44] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_HCFARMEPC] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCFARMEPC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCFARMEPC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCFARMEPC_HCORDPRON] FOREIGN KEY ([IDHCORDPRON]) REFERENCES [dbo].[HCORDPRON] ([AUTO]),
    CONSTRAINT [FK_HCFARMEPC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCFARMEPC_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCFARMEPC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [Fk_UserRegister] FOREIGN KEY ([USUAREGISTRO]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCFARMEPC].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCFARMEPC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPC_IDAGEPROGQX_IPCODPACI_NUMINGRES]
    ON [dbo].[HCFARMEPC]([IDAGEPROGQX] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC);


GO
CREATE NONCLUSTERED INDEX [HCFARMEPC_ORDESTADO_IDAGEPROGQX_CODCENATE]
    ON [dbo].[HCFARMEPC]([ORDESTADO] ASC, [IDAGEPROGQX] ASC, [CODCENATE] ASC)
    INCLUDE([CODPROSAL], [IPCODPACI], [NUMINGRES], [UFUCODIGO], [CODBODEGA], [ORDTRANUE], [TIPOSOLICITUD], [ORDENQUIMIO], [MedicalOrderType]);


GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPC_ORDESTADO_CODBODEGA_CODCENATE]
    ON [dbo].[HCFARMEPC]([ORDESTADO] ASC, [CODBODEGA] ASC, [CODCENATE] ASC);


GO
CREATE NONCLUSTERED INDEX [IDX_HISFarmacia]
    ON [dbo].[HCFARMEPC]([UFUCODIGO] ASC, [ORDESTADO] ASC, [CODCENATE] ASC)
    INCLUDE([CODBODEGA], [CODCENCOS], [CODCONCEC], [CODCONCEP], [CODCONCES], [CODPROSAL], [FECHAORDE], [IDETIPHIS], [IPCODPACI], [NUMEFOLIO], [NUMINGRES], [ORDTRANUE]);


GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPEC_CODCENATE_ORDESTADO_CODBODEGA_CODCENCOS_CODCONCEC_CODCONCEP_CODCONCES_CODPROSAL_FECHAORDE_IDETIPHIS_IPCODPACI]
    ON [dbo].[HCFARMEPC]([CODCENATE] ASC, [ORDESTADO] ASC)
    INCLUDE([CODBODEGA], [CODCENCOS], [CODCONCEC], [CODCONCEP], [CODCONCES], [CODPROSAL], [FECHAORDE], [IDETIPHIS], [IPCODPACI], [NUMEFOLIO], [NUMINGRES], [ORDTRANUE], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [xl_INDICE_SOPORTEcac]
    ON [dbo].[HCFARMEPC]([IDHCORDPRON] ASC)
    INCLUDE([CODPROSAL], [IPCODPACI], [NUMINGRES], [ORDESTADO]);


GO
CREATE NONCLUSTERED INDEX [IDX_Farmacia]
    ON [dbo].[HCFARMEPC]([ORDESTADO] ASC, [CODCENATE] ASC)
    INCLUDE([CODBODEGA], [CODCENCOS], [CODCONCEC], [CODCONCEP], [CODCONCES], [CODPROSAL], [FECHAORDE], [IDETIPHIS], [IPCODPACI], [NUMEFOLIO], [NUMINGRES], [ORDTRANUE], [UFUCODIGO]);


GO
ALTER INDEX [IDX_Farmacia]
    ON [dbo].[HCFARMEPC] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPC_ORDESTADO_CODBODEGA_CODCENATE_FECHAORDE]
    ON [dbo].[HCFARMEPC]([ORDESTADO] ASC, [CODBODEGA] ASC, [CODCENATE] ASC)
    INCLUDE([FECHAORDE]);


GO
ALTER INDEX [IX_HCFARMEPC_ORDESTADO_CODBODEGA_CODCENATE_FECHAORDE]
    ON [dbo].[HCFARMEPC] DISABLE;




GO
CREATE NONCLUSTERED INDEX [nci_wi_HCFARMEPC_88EFBCD79A5A4C0583F615C48D6D88F3]
    ON [dbo].[HCFARMEPC]([ORDESTADO] ASC, [IDAGEPROGQX] ASC, [CODCENATE] ASC, [ORDTRANUE] ASC)
    INCLUDE([CODBODEGA], [CODCENCOS], [CODCONCEP], [CODCONCES], [CODPROSAL], [FECHAORDE], [IDETIPHIS], [IPCODPACI], [MEDICAMENTOVALIDADO], [NUMEFOLIO], [NUMINGRES], [ORDENQUIMIO], [TIPOSOLICITUD], [UFUCODIGO]);


GO
ALTER INDEX [nci_wi_HCFARMEPC_88EFBCD79A5A4C0583F615C48D6D88F3]
    ON [dbo].[HCFARMEPC] DISABLE;




GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPC_FECHAORDE_IDHCORDPRON_NUMINGRES_ORDESTADO]
    ON [dbo].[HCFARMEPC]([FECHAORDE] ASC, [IDHCORDPRON] ASC)
    INCLUDE([NUMINGRES], [ORDESTADO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prescripción médica (TINYINT, default=1): 1/null=Estándar, 2=PRN (Pro renata, según sea necesario). Determina el régimen de administración del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'MedicalOrderType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de preescripción medica:  1/null - Preescripción estandar  2 - Preescripción PRN (Pro renata) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'MedicalOrderType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'MedicalOrderType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de confirmación de cita quimioterapia (TINYINT, default=2): 1=Pendiente confirmar asistencia, 2=Confirmado medicamento. Usado por enfermería y central de mezclas para preparación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ConfirmationStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de confirmación de cita de quimioterapia para saber si enfermería o central de mezclas realizan la preparación: 1. Pendiente confirmar asistencia 2. Confirmado medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ConfirmationStatus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ConfirmationStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que registró la orden farmacéutica (CHAR 20, FK→SEGusuaru.CODUSUARI). Auditoría del profesional de la salud que creó el registro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'USUAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el usuario quien registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'USUAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'USUAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de procedimiento no quirúrgico relacionado (INT, FK→HCORDPRON.AUTO). Vincula órdenes farmacéuticas con procedimientos de historia clínica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que se relaciona con la tabla de Procedimintos NoQx (HCORDPRON)  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de cita de quimioterapia (INT). Referencia a la programación oncológica del paciente para seguimiento de administración.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID cita de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDCITA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de orden oncológica/quimioterapia (BIT). Marca si la orden es de medicamentos quimioterápicos confirmados en cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ORDENQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si la orden es de quimioterapia (cuando se confirma la cita de quimioterapia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ORDENQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ORDENQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del paquete o familia de origen al dispensar (INT). Clasificación administrativa del kit/paquete de medicamentos dispensados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'TIPOSOLQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID el paquete Origen Cuando se dispensa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'TIPOSOLQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'TIPOSOLQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de solicitud farmacéutica (INT): 1=IntraHospitalario, 2=Extramural, 3=Mixto. Define si medicamentos se usan dentro o fuera del centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'TIPOSOLICITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - IntraHospitalario --solicitud farmacia solo intraHospitalario  2 - Extramural --solicitud farmacia solo ExtraMural  3 - Mixto (''''solicitud farmacia mixta - Med IntraHospitalario y Med Extramural)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'TIPOSOLICITUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'TIPOSOLICITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de programación quirúrgica (INT). Referencia a tabla de programaciones de cirugía para medicamentos preoperatorios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Tabla donde se guardan las programaiones de cirugía ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de validación farmacéutica (BIT). Confirma si el medicamento fue verificado y autorizado para dispensación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOVALIDADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medicamento validado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOVALIDADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOVALIDADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación de anulación de orden (CHAR 250). Razón documentada por la que se canceló la prescripción farmacéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'JUSANULAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificacion de la anulacion de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'JUSANULAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'JUSANULAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tratamiento farmacológico (CHAR 1): 1=Nuevo, 2=Antiguo, 3=Código Azul. Clasifica si es primera prescripción, continuación o urgencia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ORDTRANUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identifica  el tipo de Orden:  1. Tratamiento Nuevo  2. Tratamiento Antiguo  3. Codigo Azul', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ORDTRANUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ORDTRANUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de costos (CHAR 14, FK→tabla de costos). Asignación contable/presupuestaria del gasto farmacéutico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ceontro de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCENCOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de bodega/almacén farmacéutico (VARCHAR 20). Ubicación física donde se almacenan y dispensan medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODBODEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Bodega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODBODEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODBODEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la orden farmacéutica (CHAR 1): 1=Pendiente, 2=Entregada, 3=Anulada. Si=Anulada, verificar tabla Inventory.ReasonCancellationOfRequest.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ORDESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la Orden:  1. Pendiente  2. Entregada  3. Anulada : si esta en este estado debe estar tambien en [Inventory].[ReasonCancellationOfRequest]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ORDESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ORDESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo de insumos/medicamentos (CHAR 10). Identificador secuencial del registro de medicamentos dispensados.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCONCES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo de Insumos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCONCES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCONCES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código consecutivo de prescripciones (CHAR 10). Identificador secuencial de la orden médica de medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCONCEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo de prescripciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCONCEP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCONCEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (CHAR 10, FK→INUNIFUNC.UFUCODIGO). Servicio o departamento solicitante (urgencia, hospitalización, ambulatorio).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/sede (CHAR 10, FK→ADCENATEN.CODCENATE). Instalación donde se atiende al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/admission (CHAR 10, FK→ADINGRESO.NUMINGRES). Identificador único del episodio de atención del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente (VARCHAR 25, FK→INPACIENT.IPCODPACI, masked=Identification_Ofuscado). PII: cédula/documento/identificación del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional de la salud (CHAR 20, FK→INPROFSAL.CODPROSAL, masked=Identification_Ofuscado). PII: médico/prescriptor de la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la orden farmacéutica (DATETIME). Timestamp de creación de la prescripción médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'FECHAORDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'FECHAORDE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'FECHAORDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (NCHAR 10). Identificador de documento/comprobante de la orden.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre interno del tipo de historia clínica (CHAR 9). Clasificación interna del formato de registro médico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del consecutivo (NUMERIC 18, PK, IDENTITY). Identificador único autoincremental de cada orden farmacéutica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';


GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPC_IDCITA]
    ON [dbo].[HCFARMEPC]([IDCITA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Encabezado de órdenes de medicamentos (farmacia) generadas en historia clínica. Registra cada orden médica de despacho farmacéutico vinculada a un paciente, ingreso, profesional de salud, centro de atención y bodega, incluyendo su estado, tipo de solicitud y si corresponde a quimioterapia o cirugía.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC';

GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPC_NUMINGRES]
    ON [dbo].[HCFARMEPC]([NUMINGRES] ASC)
    INCLUDE([ORDESTADO], [CODBODEGA], [IDAGEPROGQX], [ORDENQUIMIO]);

GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPC_CODCONCEC]
    ON [dbo].[HCFARMEPC]([CODCONCEC] ASC)
    INCLUDE([ORDESTADO]);
