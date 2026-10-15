CREATE TABLE [dbo].[ADRADICACIONQX] (
    [ID]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMRADICACION]            INT                                                                              NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [ESTADO]                   INT                                                                              NOT NULL,
    [FECHARADIC]               DATETIME                                                                         NOT NULL,
    [QXPRINCIPAL]              CHAR (20)                                                                        NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODESPECI]                CHAR (3)                                                                         NOT NULL,
    [PRIORIDAD]                INT                                                                              NOT NULL,
    [YAPROGRAMOQX]             BIT                                                                              NOT NULL,
    [GENCAREGROUP]             INT                                                                              NULL,
    [GENCONENTITY]             INT                                                                              NULL,
    [FECHACONFIRMACION]        DATETIME                                                                         NULL,
    [FECHAREGISTRO]            DATETIME                                                                         NOT NULL,
    [GQ_CODANULACION]          CHAR (3)                                                                         NULL,
    [GQ_MOTIVOANULACION]       VARCHAR (500)                                                                    NULL,
    [GQ_FECHAANULACION]        DATETIME                                                                         NULL,
    [GQ_USUARIOANULACION]      CHAR (20)                                                                        NULL,
    [USUARIOCONFIRMACION]      CHAR (20)                                                                        NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [CODDIAGNO]                CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    CONSTRAINT [PK_ADRADICACIONQX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADRADICACIONQX_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_ADRADICACIONQX_INCUPSIPS] FOREIGN KEY ([QXPRINCIPAL]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_ADRADICACIONQX_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_ADRADICACIONQX_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_ADRADICACIONQX_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_ADRADICACIONQX_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[ADRADICACIONQX] NOCHECK CONSTRAINT [FK_ADRADICACIONQX_ADCENATEN];


GO
ALTER TABLE [dbo].[ADRADICACIONQX] NOCHECK CONSTRAINT [FK_ADRADICACIONQX_INCUPSIPS];


GO
ALTER TABLE [dbo].[ADRADICACIONQX] NOCHECK CONSTRAINT [FK_ADRADICACIONQX_INDIAGNOS];


GO
ALTER TABLE [dbo].[ADRADICACIONQX] NOCHECK CONSTRAINT [FK_ADRADICACIONQX_INESPECIA];


GO
ALTER TABLE [dbo].[ADRADICACIONQX] NOCHECK CONSTRAINT [FK_ADRADICACIONQX_INPACIENT];


GO
ALTER TABLE [dbo].[ADRADICACIONQX] NOCHECK CONSTRAINT [FK_ADRADICACIONQX_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda programo cirugia 0. no 1. si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'YAPROGRAMOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Emergencia = 1    Urgencia = 2     Normal = 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'PRIORIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Radicado   2 - Confirmado   3 - Anulado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'ESTADO';

