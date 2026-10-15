CREATE TABLE [dbo].[HCJUNOPMH] (
    [CODCONCEC]            CHAR (10)                                                                        NOT NULL,
    [CODPRONOP]            CHAR (20)                                                                        NOT NULL,
    [CODPRODUC]            CHAR (20)                                                                        NOT NULL,
    [IDETIPHIS]            CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]            NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]            VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]            CHAR (10)                                                                        NOT NULL,
    [CODCENATE]            CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]            CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]            CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED]            DATETIME                                                                         NOT NULL,
    [CODVIAADM]            CHAR (3)                                                                         NOT NULL,
    [CODFORMED]            VARCHAR (20)                                                                     NULL,
    [DOSISPROD]            NUMERIC (18, 2)                                                                  NULL,
    [CODUNIMED]            CHAR (3)                                                                         NULL,
    [FRECUENCI]            INT                                                                              NULL,
    [UNIFRECUE]            CHAR (1)                                                                         NULL,
    [TIPFORMED]            CHAR (3)                                                                         NULL,
    [DURACIDOS]            CHAR (20)                                                                        NOT NULL,
    [VALDURFIJ]            INT                                                                              NULL,
    [UNIDURFIJ]            CHAR (1)                                                                         NULL,
    [CANPEDPRO]            INT                                                                              NOT NULL,
    [CODDIAGNO]            CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NOT NULL,
    [DIASTRATA]            INT                                                                              NULL,
    [MEDNOMEJO]            BIT                                                                              NULL,
    [MEDREADVE]            BIT                                                                              NULL,
    [MEDINTOLE]            BIT                                                                              NULL,
    [MEDNOINDI]            BIT                                                                              NULL,
    [MEDOBSERV]            VARCHAR (500)                                                                    NULL,
    [INDAUDFOR]            NUMERIC (18)                                                                     NOT NULL,
    [IDESQUEMAONC]         INT                                                                              NULL,
    [IDHCORDQUIMIO]        INT                                                                              NULL,
    [IDHCORMEDICAMESQUEMA] INT                                                                              NULL,
    CONSTRAINT [PK_HCJUNOPMH] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODPRONOP] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCJUNOPMH_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCJUNOPMH_HCORDQUIMIO] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID]),
    CONSTRAINT [FK_HCJUNOPMH_IDHCORMEDICAMESQUEMA] FOREIGN KEY ([IDHCORMEDICAMESQUEMA]) REFERENCES [EHR].[HCORMEDICAMESQUEMA] ([ID]),
    CONSTRAINT [FK_HCJUNOPMH_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCJUNOPMH_Schemes] FOREIGN KEY ([IDESQUEMAONC]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_ADINGRESO];


GO
ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_HCORDQUIMIO];


GO
ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_IDHCORMEDICAMESQUEMA];


GO
ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_INPACIENT];


GO
ALTER TABLE [dbo].[HCJUNOPMH] NOCHECK CONSTRAINT [FK_HCJUNOPMH_Schemes];


GO
CREATE NONCLUSTERED INDEX [IX_HCJUNOPMH_IDHCORDQUIMIO]
    ON [dbo].[HCJUNOPMH]([IDHCORDQUIMIO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCJUNOPMH_IDESQUEMAONC]
    ON [dbo].[HCJUNOPMH]([IDESQUEMAONC] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCJUNOPMH_HCORMEDICAMESQUEMA]
    ON [dbo].[HCJUNOPMH]([IDHCORMEDICAMESQUEMA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la orden de medicamento autorizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDHCORMEDICAMESQUEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la orden de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de esquema oncologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDOBSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento No Indicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDNOINDI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Intolerancia al Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDINTOLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medicamento Produjo Reaccion Adversa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDREADVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Con el Medicamento No Mejora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'MEDNOMEJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de tratamiento del medicamento No POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'DIASTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diagnostico Principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Pedida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duracion del Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'FRECUENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Forma de presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Medicamento Homologo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Medicamento No POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODPRONOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concecutivo del Formato No POS    Nota: Codigo 00000017', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPMH', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

