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
ALTER TABLE [dbo].[HCFARMEPC] NOCHECK CONSTRAINT [FK_HCFARMEPC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCFARMEPC] NOCHECK CONSTRAINT [FK_HCFARMEPC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCFARMEPC] NOCHECK CONSTRAINT [FK_HCFARMEPC_HCORDPRON];


GO
ALTER TABLE [dbo].[HCFARMEPC] NOCHECK CONSTRAINT [FK_HCFARMEPC_INPACIENT];


GO
ALTER TABLE [dbo].[HCFARMEPC] NOCHECK CONSTRAINT [FK_HCFARMEPC_INPROFSAL];


GO
ALTER TABLE [dbo].[HCFARMEPC] NOCHECK CONSTRAINT [FK_HCFARMEPC_INUNIFUNC];


GO
ALTER TABLE [dbo].[HCFARMEPC] NOCHECK CONSTRAINT [Fk_UserRegister];


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
CREATE NONCLUSTERED INDEX [IX_HCFARMEPC_ORDESTADO_CODBODEGA_CODCENATE_FECHAORDE]
    ON [dbo].[HCFARMEPC]([ORDESTADO] ASC, [CODBODEGA] ASC, [CODCENATE] ASC)
    INCLUDE([FECHAORDE]);


GO
CREATE NONCLUSTERED INDEX [nci_wi_HCFARMEPC_88EFBCD79A5A4C0583F615C48D6D88F3]
    ON [dbo].[HCFARMEPC]([ORDESTADO] ASC, [IDAGEPROGQX] ASC, [CODCENATE] ASC, [ORDTRANUE] ASC)
    INCLUDE([CODBODEGA], [CODCENCOS], [CODCONCEP], [CODCONCES], [CODPROSAL], [FECHAORDE], [IDETIPHIS], [IPCODPACI], [MEDICAMENTOVALIDADO], [NUMEFOLIO], [NUMINGRES], [ORDENQUIMIO], [TIPOSOLICITUD], [UFUCODIGO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCFARMEPC_FECHAORDE_IDHCORDPRON_NUMINGRES_ORDESTADO]
    ON [dbo].[HCFARMEPC]([FECHAORDE] ASC, [IDHCORDPRON] ASC)
    INCLUDE([NUMINGRES], [ORDESTADO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de preescripción medica:  1/null - Preescripción estandar  2 - Preescripción PRN (Pro renata) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'MedicalOrderType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de confirmación de cita de quimioterapia para saber si enfermería o central de mezclas realizan la preparación: 1. Pendiente confirmar asistencia 2. Confirmado medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ConfirmationStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el usuario quien registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'USUAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que se relaciona con la tabla de Procedimintos NoQx (HCORDPRON)  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID cita de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si la orden es de quimioterapia (cuando se confirma la cita de quimioterapia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ORDENQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID el paquete Origen Cuando se dispensa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'TIPOSOLQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - IntraHospitalario --solicitud farmacia solo intraHospitalario  2 - Extramural --solicitud farmacia solo ExtraMural  3 - Mixto (''solicitud farmacia mixta - Med IntraHospitalario y Med Extramural)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'TIPOSOLICITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Tabla donde se guardan las programaiones de cirugía ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDAGEPROGQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento validado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOVALIDADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Justificacion de la anulacion de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'JUSANULAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica  el tipo de Orden:  1. Tratamiento Nuevo  2. Tratamiento Antiguo  3. Codigo Azul', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ORDTRANUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ceontro de Costos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCENCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Bodega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODBODEGA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la Orden:  1. Pendiente  2. Entregada  3. Anulada : si esta en este estado debe estar tambien en [Inventory].[ReasonCancellationOfRequest]', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'ORDESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo de Insumos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCONCES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo de prescripciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCONCEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'FECHAORDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFARMEPC', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

