CREATE TABLE [dbo].[HCJUNOPOM] (
    [CODCONCEC]            CHAR (10)                                                                        NOT NULL,
    [IDETIPHIS]            CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]            NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]            VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]            CHAR (10)                                                                        NOT NULL,
    [CODCENATE]            CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]            CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]            CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED]            DATETIME                                                                         NOT NULL,
    [CODPRODUC]            CHAR (20)                                                                        NOT NULL,
    [CODVIAADM]            CHAR (3)                                                                         NOT NULL,
    [CODFORMED]            VARCHAR (20)                                                                     NOT NULL,
    [DOSISPROD]            NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]            CHAR (3)                                                                         NULL,
    [FRECUENCI]            INT                                                                              NULL,
    [UNIFRECUE]            CHAR (1)                                                                         NULL,
    [TIPFORMED]            CHAR (1)                                                                         NOT NULL,
    [DURACIDOS]            CHAR (20)                                                                        NOT NULL,
    [VALDURFIJ]            INT                                                                              NULL,
    [UNIDURFIJ]            CHAR (1)                                                                         NULL,
    [CANPEDPRO]            INT                                                                              NOT NULL,
    [CODDIAGNO]            CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [DOSISDIAS]            CHAR (500)                                                                       NULL,
    [DIASTRATA]            INT                                                                              NULL,
    [INDTERAPR]            VARCHAR (MAX)                                                                    NULL,
    [RESJUSMED]            VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "SummaryHC_Ofuscado", 0)')     NULL,
    [EFEADVMED]            VARCHAR (MAX)                                                                    NULL,
    [RAZVENMED]            VARCHAR (MAX)                                                                    NULL,
    [RIEINMPAC]            BIT                                                                              NOT NULL,
    [AGOPOSTER]            BIT                                                                              NULL,
    [MEDUTIPAI]            BIT                                                                              NULL,
    [MEDEXPERI]            BIT                                                                              NULL,
    [USOCORINV]            BIT                                                                              NULL,
    [DESADMINI]            VARCHAR (MAX)                                                                    NULL,
    [INDAUDFOR]            NUMERIC (18)                                                                     NOT NULL,
    [CODMINSALUD]          VARCHAR (30)                                                                     NULL,
    [CODMOTIVO]            VARCHAR (4)                                                                      NULL,
    [DESMOTIVO]            VARCHAR (MAX)                                                                    NULL,
    [FECMOTIVO]            DATETIME                                                                         NULL,
    [PRESCRIPCIONJSON]     VARCHAR (8000)                                                                   NULL,
    [DESCMEDPRINACT]       VARCHAR (1500)                                                                   NULL,
    [ORDUNICA]             BIT                                                                              NULL,
    [DESCADMINMIPRES]      VARCHAR (200)                                                                    NULL,
    [IDESQUEMAONC]         INT                                                                              NULL,
    [IDHCORDQUIMIO]        INT                                                                              NULL,
    [IDHCORMEDICAMESQUEMA] INT                                                                              NULL,
    [EXTRAMURAL]           BIT                                                                              CONSTRAINT [DF_HCJUNOPOM_EXTRAMURAL] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_HCJUNOPOS] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCJUNOPOM_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCJUNOPOM_HCORDQUIMIO] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID]),
    CONSTRAINT [FK_HCJUNOPOM_IDHCORMEDICAMESQUEMA] FOREIGN KEY ([IDHCORMEDICAMESQUEMA]) REFERENCES [EHR].[HCORMEDICAMESQUEMA] ([ID]),
    CONSTRAINT [FK_HCJUNOPOM_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCJUNOPOM_Schemes] FOREIGN KEY ([IDESQUEMAONC]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ALTER TABLE [dbo].[HCJUNOPOM] NOCHECK CONSTRAINT [FK_HCJUNOPOM_ADINGRESO];


GO
ALTER TABLE [dbo].[HCJUNOPOM] NOCHECK CONSTRAINT [FK_HCJUNOPOM_HCORDQUIMIO];


GO
ALTER TABLE [dbo].[HCJUNOPOM] NOCHECK CONSTRAINT [FK_HCJUNOPOM_IDHCORMEDICAMESQUEMA];


GO
ALTER TABLE [dbo].[HCJUNOPOM] NOCHECK CONSTRAINT [FK_HCJUNOPOM_INPACIENT];


GO
ALTER TABLE [dbo].[HCJUNOPOM] NOCHECK CONSTRAINT [FK_HCJUNOPOM_Schemes];


GO
CREATE NONCLUSTERED INDEX [UX_HCJUNOPOM_NUMINGRES_CODPRODUC_CODMINSALUD]
    ON [dbo].[HCJUNOPOM]([NUMINGRES] ASC, [CODPRODUC] ASC, [CODMINSALUD] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Me identifica si el registro se hace por medio de una prescripcion extramural o no.     0 - No es extramural   1 - Si es extramural ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'EXTRAMURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'DESCADMINMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concecutivo del Formato No POS    Nota: Codigo 00000017', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

