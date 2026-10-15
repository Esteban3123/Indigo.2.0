CREATE TABLE [dbo].[PADASIGNACION] (
    [ID]                 INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PADCONTROLID]       INT           NULL,
    [TIPOATENCION]       TINYINT       NOT NULL,
    [AUTORIZACION]       VARCHAR (15)  NOT NULL,
    [NUMEROINGRESO]      CHAR (10)     NOT NULL,
    [ATENCIONAUTORIZA]   SMALLINT      CONSTRAINT [DF_PADASIGNACION_ATENCIONAUTORIZA] DEFAULT ((0)) NOT NULL,
    [ENFERMERIAAUTORIZA] SMALLINT      CONSTRAINT [DF_PADASIGNACION_ENFERMERIAAUTORIZA] DEFAULT ((0)) NOT NULL,
    [TERAPIAAUTORIZA]    SMALLINT      CONSTRAINT [DF_PADASIGNACION_TERAPIAAUTORIZA] DEFAULT ((0)) NOT NULL,
    [PSICOLOGIAAUTORIZA] SMALLINT      CONSTRAINT [DF_PADASIGNACION_PSICOLOGIAAUTORIZA] DEFAULT ((0)) NOT NULL,
    [ESTADO]             TINYINT       NOT NULL,
    [FECHAREGISTRO]      DATETIME      NOT NULL,
    [FECHAEGRESO]        DATETIME      NULL,
    [OBSERVACION]        VARCHAR (500) NULL,
    [ATENCIONREALIZA]    SMALLINT      CONSTRAINT [DF_PADASIGNACION_ATENCIONREALIZA] DEFAULT ((0)) NOT NULL,
    [ENFERMERIAREALIZA]  SMALLINT      CONSTRAINT [DF_PADASIGNACION_ENFERMERIAREALIZA] DEFAULT ((0)) NOT NULL,
    [TERAPIAREALIZA]     SMALLINT      CONSTRAINT [DF_PADASIGNACION_TERAPIAREALIZA] DEFAULT ((0)) NOT NULL,
    [PSICOLOGIAREALIZA]  SMALLINT      CONSTRAINT [DF_PADASIGNACION_PSICOLOGIAREALIZA] DEFAULT ((0)) NOT NULL,
    [MOTIVOEGRESO]       CHAR (4)      NULL,
    [USUARIOREGISTRA]    CHAR (20)     NOT NULL,
    [USUARIOEGRESO]      CHAR (20)     NULL,
    [NUMEROINGRESOORG]   CHAR (10)     NULL,
    [CODDIAGNO]          CHAR (4)      NULL,
    CONSTRAINT [PK_PADASIGNACION] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PADASIGNACION_ADINGRESO] FOREIGN KEY ([NUMEROINGRESO]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_PADASIGNACION_HCMOANULB] FOREIGN KEY ([MOTIVOEGRESO]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_PADASIGNACION_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_PADASIGNACION_PADCONTROL] FOREIGN KEY ([PADCONTROLID]) REFERENCES [dbo].[PADCONTROL] ([ID])
);


GO
ALTER TABLE [dbo].[PADASIGNACION] NOCHECK CONSTRAINT [FK_PADASIGNACION_ADINGRESO];


GO
ALTER TABLE [dbo].[PADASIGNACION] NOCHECK CONSTRAINT [FK_PADASIGNACION_HCMOANULB];


GO
ALTER TABLE [dbo].[PADASIGNACION] NOCHECK CONSTRAINT [FK_PADASIGNACION_INDIAGNOS];


GO
ALTER TABLE [dbo].[PADASIGNACION] NOCHECK CONSTRAINT [FK_PADASIGNACION_PADCONTROL];


GO
CREATE NONCLUSTERED INDEX [IX_PADASIGNACION_MOTIVOEGRESO]
    ON [dbo].[PADASIGNACION]([MOTIVOEGRESO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PADASIGNACION_NUMEROINGRESO]
    ON [dbo].[PADASIGNACION]([NUMEROINGRESO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PADASIGNACION_PADCONTROLID]
    ON [dbo].[PADASIGNACION]([PADCONTROLID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de ingreso en la solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'NUMEROINGRESOORG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario quien realiza el egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'USUARIOEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario quien realiza la asignación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de motivo de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'MOTIVOEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de atenciones de psicología realizadas en el momento de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'PSICOLOGIAREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de terapias realizadas en el momento de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'TERAPIAREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de atenciones de enfermeria realizadas en el momento de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ENFERMERIAREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de atenciones médicas realizadas en el momento de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ATENCIONREALIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'FECHAEGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado, 1:Asignada, 2:Egresado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de atenciones de psicología autorizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'PSICOLOGIAAUTORIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de terapias autorizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'TERAPIAAUTORIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de atenciones de enfermería autorizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ENFERMERIAAUTORIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de atenciones médicas autorizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ATENCIONAUTORIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'NUMEROINGRESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de autorización', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'AUTORIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de atencion, 1:Atencion domiciliaria, 2:Hospitalización en casa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'TIPOATENCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de control PAD', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'PADCONTROLID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADASIGNACION', @level2type = N'COLUMN', @level2name = N'ID';

