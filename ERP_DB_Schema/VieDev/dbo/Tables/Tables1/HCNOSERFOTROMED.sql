CREATE TABLE [dbo].[HCNOSERFOTROMED] (
    [ID]                  INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDETIPHIS]           CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]           NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]           VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]           CHAR (10)                                                                        NOT NULL,
    [CODCENATE]           CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]           CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]           CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECHAORDE]           DATETIME                                                                         NOT NULL,
    [CODPRODUC]           CHAR (20)                                                                        NOT NULL,
    [ADMINISTRACION]      VARCHAR (200)                                                                    NOT NULL,
    [TIPODURACION]        VARCHAR (30)                                                                     NOT NULL,
    [CANTIDAD]            INT                                                                              NOT NULL,
    [OBSERVACIONES]       VARCHAR (200)                                                                    NOT NULL,
    [PREESTADO]           INT                                                                              NOT NULL,
    [FECHACREACION]       DATETIME                                                                         NOT NULL,
    [USUARIOCREACION]     CHAR (20)                                                                        NOT NULL,
    [FECHAMODIFICACION]   DATETIME                                                                         NULL,
    [USUARIOMODIFICACION] CHAR (20)                                                                        NULL,
    [RejectionDate]       DATETIME                                                                         NULL,
    [RejectedUser]        CHAR (20)                                                                        NULL,
    [Justification]       VARCHAR (200)                                                                    NULL,
    [FolioPrescription]   VARCHAR (10)                                                                     NULL,
    CONSTRAINT [PK_HCNOSERFOTROMED] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCNOSERFOTROMED_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCNOSERFOTROMED_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCNOSERFOTROMED_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCNOSERFOTROMED_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCNOSERFOTROMED_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCNOSERFOTROMED_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCNOSERFOTROMED] NOCHECK CONSTRAINT [FK_HCNOSERFOTROMED_ADCENATEN];


GO
ALTER TABLE [dbo].[HCNOSERFOTROMED] NOCHECK CONSTRAINT [FK_HCNOSERFOTROMED_ADINGRESO];


GO
ALTER TABLE [dbo].[HCNOSERFOTROMED] NOCHECK CONSTRAINT [FK_HCNOSERFOTROMED_IHLISTPRO];


GO
ALTER TABLE [dbo].[HCNOSERFOTROMED] NOCHECK CONSTRAINT [FK_HCNOSERFOTROMED_INPACIENT];


GO
ALTER TABLE [dbo].[HCNOSERFOTROMED] NOCHECK CONSTRAINT [FK_HCNOSERFOTROMED_INPROFSAL];


GO
ALTER TABLE [dbo].[HCNOSERFOTROMED] NOCHECK CONSTRAINT [FK_HCNOSERFOTROMED_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Folio en el que fue prescrito el medicamento sugerido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FolioPrescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificacion del rechazo del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'Justification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que rechazo el medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'RejectedUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que fue rechazado por el medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'RejectionDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado -->   1: nuevo o solicitado   2 : eliminado o suspendido ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'PREESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'OBSERVACIONES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad / Dia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Duración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'TIPODURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'ADMINISTRACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'FECHAORDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Centro de Atención', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Número de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID o Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCNOSERFOTROMED', @level2type = N'COLUMN', @level2name = N'ID';

