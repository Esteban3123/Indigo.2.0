CREATE TABLE [dbo].[HCHOJAMED] (
    [CONSECUTI]      NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]      VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]      CHAR (10)                                                                        NOT NULL,
    [CODCENATE]      CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]      CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]      CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [CODPROAPL]      CHAR (20)                                                                        NULL,
    [CODPRODUC]      CHAR (20)                                                                        NOT NULL,
    [FECINITRA]      DATETIME                                                                         NOT NULL,
    [FECPROAPL]      DATETIME                                                                         NOT NULL,
    [FECAPLMED]      DATETIME                                                                         NULL,
    [FECREGSIS]      DATETIME                                                                         CONSTRAINT [DF_HCENF_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [CODVIAADM]      CHAR (20)                                                                        NOT NULL,
    [DOSISPROD]      NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]      CHAR (30)                                                                        NULL,
    [FRECUENCI]      INT                                                                              NULL,
    [UNIFRECUE]      CHAR (1)                                                                         NULL,
    [DURACIDOS]      CHAR (20)                                                                        NOT NULL,
    [VALDURFIJ]      INT                                                                              NULL,
    [UNIDURFIJ]      CHAR (1)                                                                         NULL,
    [FECFINDOS]      DATETIME                                                                         NULL,
    [MEDESTADO]      CHAR (1)                                                                         NOT NULL,
    [CABESTADO]      CHAR (1)                                                                         NOT NULL,
    [INDAPLMED]      VARCHAR (MAX)                                                                    NULL,
    [MOTSUSMED]      VARCHAR (2000)                                                                   NULL,
    [CODUSUSUS]      CHAR (20)                                                                        NULL,
    [CANDESCON]      INT                                                                              NULL,
    [FORMUMANU]      BIT                                                                              NOT NULL,
    [DESADMINI]      VARCHAR (MAX)                                                                    NULL,
    [CODTIPEST]      CHAR (3)                                                                         NULL,
    [CONSECPRESCRA]  NUMERIC (18)                                                                     NULL,
    [MEDICACUSTODIA] BIT                                                                              NULL,
    [IDAUTORIZACION] NUMERIC (18)                                                                     NULL,
    [IDHCORDQUIMIO]  INT                                                                              NULL,
    [IDHCORDCICLOSD] INT                                                                              NULL,
    [IDCITA]         INT                                                                              NULL,
    [FORMAPRESCRIBE] TINYINT                                                                          NULL,
    [ITEMDOSIS]      TINYINT                                                                          NULL,
    CONSTRAINT [PK_HCENF] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCHOJAMED_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCHOJAMED_AGASICITA] FOREIGN KEY ([IDCITA]) REFERENCES [dbo].[AGASICITA] ([CODAUTONU]),
    CONSTRAINT [FK_HCHOJAMED_HCORDQUIMIO] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID]),
    CONSTRAINT [FK_HCHOJAMED_HCQUICICLOS] FOREIGN KEY ([IDHCORDCICLOSD]) REFERENCES [EHR].[HCORDCICLOSD] ([ID]),
    CONSTRAINT [FK_HCHOJAMED_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCHOJAMED] NOCHECK CONSTRAINT [FK_HCHOJAMED_ADINGRESO];


GO
ALTER TABLE [dbo].[HCHOJAMED] NOCHECK CONSTRAINT [FK_HCHOJAMED_AGASICITA];


GO
ALTER TABLE [dbo].[HCHOJAMED] NOCHECK CONSTRAINT [FK_HCHOJAMED_HCORDQUIMIO];


GO
ALTER TABLE [dbo].[HCHOJAMED] NOCHECK CONSTRAINT [FK_HCHOJAMED_HCQUICICLOS];


GO
ALTER TABLE [dbo].[HCHOJAMED] NOCHECK CONSTRAINT [FK_HCHOJAMED_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_HCHOJAMED_CODPRODUC_IDHCORDCICLOSD_MEDESTADO_NUMINGRES]
    ON [dbo].[HCHOJAMED]([CODPRODUC] ASC, [IDHCORDCICLOSD] ASC)
    INCLUDE([MEDESTADO], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_HCHOJAMED_IDHCORDQUIMIO_MEDESTADO]
    ON [dbo].[HCHOJAMED]([IDHCORDQUIMIO] ASC)
    INCLUDE([MEDESTADO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Medicamento  1: Activo  2: Terminado  3: Suspendido por modficacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CABESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Medicamento  1: Pendiente  2: Aplicado  3: Suspendido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MEDESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';

