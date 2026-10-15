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
CREATE NONCLUSTERED INDEX [IX_HCJUNOPOM]
    ON [dbo].[HCJUNOPOM]([IPCODPACI] ASC, [NUMINGRES] ASC, [NUMEFOLIO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCJUNOPOM_HCORMEDICAMESQUEMA]
    ON [dbo].[HCJUNOPOM]([IDHCORMEDICAMESQUEMA] ASC);


GO
CREATE NONCLUSTERED INDEX [UX_HCJUNOPOM_NUMINGRES_CODPRODUC_CODMINSALUD]
    ON [dbo].[HCJUNOPOM]([NUMINGRES] ASC, [CODPRODUC] ASC, [CODMINSALUD] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Me identifica si el registro se hace por medio de una prescripcion extramural o no.     0 - No es extramural   1 - Si es extramural ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'EXTRAMURAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la orden de medicamento autorizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'IDHCORMEDICAMESQUEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la orden de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de esquema oncologico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'IDESQUEMAONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la administración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'DESCADMINMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define si se puede o no relacionar el medicamento MIPRES a otra orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'ORDUNICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del principio activo en MIPRES', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'DESCMEDPRINACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prescripción en formato json', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'PRESCRIPCIONJSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha en que se guardo el motivo por el cual no se agrego una prescripcion de MINSALUD. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'FECMOTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del motivo por el que no se registro la justificación de un medicamento no POS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'DESMOTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del motivo por el que no se registro la justificación de un medicamento no POS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'CODMOTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero de Prescripción que generó el software de MINSALUD cuando registró la orden del servicio o tecnología NO POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'CODMINSALUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicacion Paciente:  1: Orden de Hospitalizacion  2: Urgencias  3: Dejar en Observacion  4: Cirugia  5: Remitir  6: Morgue  7: Remitir a Consulta Externa  8: Salida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Administracion del Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'DESADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'El uso corresponde a la indicación registrada en el Invima ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'USOCORINV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Es un medicamento de experimentación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'MEDEXPERI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Es un medicamento utilizado en el pais', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'MEDUTIPAI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Se han agotado las posibilidades terapéuticas existentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'AGOPOSTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Existe Riesgo Inminente para la Vida del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'RIEINMPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Explicacion de Razones y Ventajas del uso de este Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'RAZVENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Posibles Efectos Adversos que se deriven del uso de este medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'EFEADVMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Resumen de la Historia Clinica y Justificacion del Medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'RESJUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indi', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'INDTERAPR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'DIASTRATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'DOSISDIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico principal o razon principal por la solicitud del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duracion del Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'FRECUENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Forma de presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Solicitud de la Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Interno Tipo Historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concecutivo del Formato No POS    Nota: Codigo 00000017', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUNOPOM', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

