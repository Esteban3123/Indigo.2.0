CREATE TABLE [dbo].[RIASXPACIENTE] (
    [ID]                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRIAS]            INT                                                                              NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECHAINCRIPCION]   DATETIME                                                                         NULL,
    [ESTADO]            INT                                                                              NOT NULL,
    [ORIGENINSCRIPCION] INT                                                                              NOT NULL,
    [USUARIO]           CHAR (20)                                                                        NOT NULL,
    [JUSTIFICACIONEGRE] VARCHAR (MAX)                                                                    NULL,
    [USUARIOEGRE]       CHAR (20)                                                                        NULL,
    [CODMOTIVOEGRE]     CHAR (4)                                                                         NULL,
    [FECHAEGRE]         DATETIME                                                                         NULL,
    CONSTRAINT [PK_RIASXPACIENTE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RIASXPACIENTE_HCMOANULB] FOREIGN KEY ([CODMOTIVOEGRE]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_RIASXPACIENTE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_RIASXPACIENTE_RIAS] FOREIGN KEY ([IDRIAS]) REFERENCES [dbo].[RIAS] ([ID])
);


GO
ALTER TABLE [dbo].[RIASXPACIENTE] NOCHECK CONSTRAINT [FK_RIASXPACIENTE_HCMOANULB];


GO
ALTER TABLE [dbo].[RIASXPACIENTE] NOCHECK CONSTRAINT [FK_RIASXPACIENTE_INPACIENT];


GO
ALTER TABLE [dbo].[RIASXPACIENTE] NOCHECK CONSTRAINT [FK_RIASXPACIENTE_RIAS];


GO
CREATE NONCLUSTERED INDEX [IX_RIASXPACIENTE_IPCODPACI]
    ON [dbo].[RIASXPACIENTE]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de egreso de la rias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAEGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo del motivo por el cual egresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'CODMOTIVOEGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo usuario que egreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'USUARIOEGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Razón por la cual se egresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONEGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que realizo el registro   (usuario que inscribio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'USUARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Proceso que inscribio:  1 - Dashboard RIAS  2 - Orden de servicios  3 - Agendamiento  4 - Desde la Historia Clinica (Formulario demanda Inducida)  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'ORIGENINSCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1-No Inscrito   2-Inscrito   3-Egresado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inscrición a la RIAS ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'FECHAINCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de RIAS, es el id de la rias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'IDRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASXPACIENTE', @level2type = N'COLUMN', @level2name = N'ID';

