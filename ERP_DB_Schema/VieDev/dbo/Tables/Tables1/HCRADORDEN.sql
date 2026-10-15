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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Esta columna Identifica el Ultimo centro de atencion en el cual el paciente a estado, por ejemplo:   La simulacion la hacen en el Centro A  La planeacion la hacen en el Centro B  La aplicacion de la radio la hacen en el Centro C    En esta columna se almacena el Ultimo utilizado.   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'CODCENATEULTIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Solicitado  2 - Simulacion Programada (aplica solo para radioterapias)  3 - Planeacion Programada  (aplica solo para radioterapias)  4 - Planeacion Confirmada   (aplica solo para radioterapias)  5 - Finalizado    6 - Anulado   7 - Completado   8 - Braquiterapia Iniciada ( cuando se ha aplicado la primer dosis desde  dashboard de Braquiterapia)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADORDEN', @level2type = N'COLUMN', @level2name = N'ESTADO';

