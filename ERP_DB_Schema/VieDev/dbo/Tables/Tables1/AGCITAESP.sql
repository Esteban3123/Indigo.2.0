CREATE TABLE [dbo].[AGCITAESP] (
    [CODAUTONU]   INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]   CHAR (10)                                                                        NOT NULL,
    [FECHACITA]   DATE                                                                             NOT NULL,
    [IPCODPACI]   VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODESPECI]   CHAR (3)                                                                         NOT NULL,
    [CODACTMED]   CHAR (3)                                                                         NOT NULL,
    [CODPROSAL]   CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [FECREGSIS]   DATETIME                                                                         NOT NULL,
    [CITAASIGN]   BIT                                                                              NOT NULL,
    [OBSERVACI]   VARCHAR (MAX)                                                                    NULL,
    [CODUSUASI]   CHAR (20)                                                                        NULL,
    [TEMP]        NCHAR (1)                                                                        NULL,
    [ESTADO]      INT                                                                              NULL,
    [IDCITA]      INT                                                                              NULL,
    [CANCELUSU]   CHAR (20)                                                                        NULL,
    [FECHCANCELA] DATETIME                                                                         NULL,
    [CODCAUCAN]   CHAR (3)                                                                         NULL,
    [OBSCAUCAN]   VARCHAR (200)                                                                    NULL,
    [CODSERIPS]   CHAR (20)                                                                        NULL,
    CONSTRAINT [PK_AGCITAESP] PRIMARY KEY CLUSTERED ([CODAUTONU] ASC),
    CONSTRAINT [FK_AGCITAESP_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AGCITAESP_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGCITAESP_AGASICITA] FOREIGN KEY ([IDCITA]) REFERENCES [dbo].[AGASICITA] ([CODAUTONU]),
    CONSTRAINT [FK_AGCITAESP_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AGCITAESP_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_AGCITAESP_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL])
);


GO
ALTER TABLE [dbo].[AGCITAESP] NOCHECK CONSTRAINT [FK_AGCITAESP_ADCENATEN];


GO
ALTER TABLE [dbo].[AGCITAESP] NOCHECK CONSTRAINT [FK_AGCITAESP_AGACTIMED];


GO
ALTER TABLE [dbo].[AGCITAESP] NOCHECK CONSTRAINT [FK_AGCITAESP_AGASICITA];


GO
ALTER TABLE [dbo].[AGCITAESP] NOCHECK CONSTRAINT [FK_AGCITAESP_INESPECIA];


GO
ALTER TABLE [dbo].[AGCITAESP] NOCHECK CONSTRAINT [FK_AGCITAESP_INPACIENT];


GO
ALTER TABLE [dbo].[AGCITAESP] NOCHECK CONSTRAINT [FK_AGCITAESP_INPROFSAL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el codigo del servicio relacionado a la actividad (CODACTIMED) seleccionado desde el formulario modal de demanda insatisfecha en el formulario de asignacion de citas de consulta exterana ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - En Espera  2 - Asignada  3 - Cancelada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCITAESP', @level2type = N'COLUMN', @level2name = N'ESTADO';

