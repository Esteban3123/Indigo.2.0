CREATE TABLE [dbo].[HCRADORDEN] (
    [ID]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCORDPRON]              INT                                                                              NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODDIAGNO]                CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [FECHAORDEN]               DATETIME                                                                         NOT NULL,
    [ESTADO]                   TINYINT                                                                          NOT NULL,
    [FECHAREGISTRO]            DATETIME                                                                         NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [CODESPECI]                CHAR (3)                                                                         NOT NULL,
    [FECHAPROSIMULA]           DATETIME                                                                         NULL,
    [USUARIOPROSIMULA]         CHAR (20)                                                                        NULL,
    [FECHASIMULA]              DATETIME                                                                         NULL,
    [USUARIOSIMULA]            CHAR (20)                                                                        NULL,
    [FECHAPLANEA]              DATETIME                                                                         NULL,
    [USUARIOPLANEA]            CHAR (20)                                                                        NULL,
    [NOREFIEREFECHAPLANEA]     BIT                                                                              NULL,
    [USUARIONOREFIEREPLANEA]   CHAR (20)                                                                        NULL,
    [CITABRAQUIASIGNADA]       BIT                                                                              NULL,
    [IDHCMOANULB]              CHAR (4)                                                                         NULL,
    [USUARIOANULACION]         VARCHAR (20)                                                                     NULL,
    [FECHAANULACION]           DATETIME                                                                         NULL,
    [OBSERVANULACION]          VARCHAR (MAX)                                                                    NULL,
    [CODCENATEULTIMO]          CHAR (10)                                                                        NULL,
    [FECHACONTORNEO]           DATETIME                                                                         NULL,
    [USUARIOCONTORNEO]         CHAR (20)                                                                        NULL,
    [NOREFIEREFECHACONTOR]     BIT                                                                              NULL,
    [USUARIONOREFIERECONTOR]   CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_HCRADORDEN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADORDEN_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADORDEN_ADCENATEN1] FOREIGN KEY ([CODCENATEULTIMO]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADORDEN_HCMOANULB] FOREIGN KEY ([IDHCMOANULB]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCRADORDEN_HCORDPRON] FOREIGN KEY ([IDHCORDPRON]) REFERENCES [dbo].[HCORDPRON] ([AUTO]),
    CONSTRAINT [FK_HCRADORDEN_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCRADORDEN_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCRADORDEN_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCRADORDEN_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCRADORDEN_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[HCRADORDEN] NOCHECK CONSTRAINT [FK_HCRADORDEN_ADCENATEN];


GO
ALTER TABLE [dbo].[HCRADORDEN] NOCHECK CONSTRAINT [FK_HCRADORDEN_ADCENATEN1];


GO
ALTER TABLE [dbo].[HCRADORDEN] NOCHECK CONSTRAINT [FK_HCRADORDEN_HCMOANULB];


GO
ALTER TABLE [dbo].[HCRADORDEN] NOCHECK CONSTRAINT [FK_HCRADORDEN_HCORDPRON];


GO
ALTER TABLE [dbo].[HCRADORDEN] NOCHECK CONSTRAINT [FK_HCRADORDEN_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCRADORDEN] NOCHECK CONSTRAINT [FK_HCRADORDEN_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCRADORDEN] NOCHECK CONSTRAINT [FK_HCRADORDEN_INESPECIA];


GO
ALTER TABLE [dbo].[HCRADORDEN] NOCHECK CONSTRAINT [FK_HCRADORDEN_INPACIENT];


GO
ALTER TABLE [dbo].[HCRADORDEN] NOCHECK CONSTRAINT [FK_HCRADORDEN_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de No refiere Contorneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIONOREFIERECONTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No refiere fecha de contorneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'NOREFIEREFECHACONTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Contorneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOCONTORNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de contorneo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHACONTORNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta columna Identifica el Ultimo centro de atencion en el cual el paciente a estado, por ejemplo:   La simulacion la hacen en el Centro A  La planeacion la hacen en el Centro B  La aplicacion de la radio la hacen en el Centro C    En esta columna se almacena el Ultimo utilizado.   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODCENATEULTIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'OBSERVANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que hace la anulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo anulacion, campo relacionado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Para las ordenes de Braquiterapia, indica si ya se asigno la primer cita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CITABRAQUIASIGNADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario de No refiere Planeación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIONOREFIEREPLANEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'No refiere fecha de planeacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'NOREFIEREFECHAPLANEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Planeación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOPLANEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de planeación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAPLANEA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Real Simulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOSIMULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Real de la Simulacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHASIMULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario programo la Simulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'USUARIOPROSIMULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha que seleccionan como programacion de la Simulación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAPROSIMULA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creacion registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Solicitado  2 - Simulacion Programada (aplica solo para radioterapias)  3 - Planeacion Programada  (aplica solo para radioterapias)  4 - Planeacion Confirmada   (aplica solo para radioterapias)  5 - Finalizado    6 - Anulado   7 - Completado   8 - Braquiterapia Iniciada ( cuando se ha aplicado la primer dosis desde  dashboard de Braquiterapia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'FECHAORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificacion de paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo centro atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID relacion con ordenes procedimientos NO QX  (HCORDPRON)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'ID';

