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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnóstico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que realiza la confirmacion de Radicacion de QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'USUARIOCONFIRMACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario quien Anulo Radicacion de QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'GQ_USUARIOANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la Anulacion del Radicacion de QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'GQ_FECHAANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de Anulacion del Radicacion de QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'GQ_MOTIVOANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Anulacion del Radicacion de QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'GQ_CODANULACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha que registro Radicacion de QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha que realiza la confirmacion de Radicación de QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'FECHACONFIRMACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Entidad ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'GENCONENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grupo de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'GENCAREGROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda programo cirugia 0. no 1. si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'YAPROGRAMOQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Emergencia = 1    Urgencia = 2     Normal = 3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'PRIORIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Professional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo centro Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'procedimiento quirúrgico Principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'QXPRINCIPAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha cual registro Radicacón', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'FECHARADIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Radicado   2 - Confirmado   3 - Anulado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificación del paciente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero Radicacion genera automatico desde radicacion cirugias ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'NUMRADICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQX', @level2type = N'COLUMN', @level2name = N'ID';

