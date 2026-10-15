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
CREATE NONCLUSTERED INDEX [IX_Calendario]
    ON [dbo].[HCHOJAMED]([IPCODPACI] ASC, [NUMINGRES] ASC, [CABESTADO] ASC, [FECPROAPL] ASC)
    INCLUDE([CODPRODUC], [CODPROSAL], [CODUNIMED], [CODVIAADM], [CONSECUTI], [DOSISPROD], [DURACIDOS], [FECAPLMED], [FECINITRA], [FRECUENCI], [INDAPLMED], [MEDESTADO], [UNIDURFIJ], [UNIFRECUE], [VALDURFIJ]);


GO
CREATE NONCLUSTERED INDEX [IX_AplicacionesPendientes]
    ON [dbo].[HCHOJAMED]([CODCENATE] ASC, [UFUCODIGO] ASC, [MEDESTADO] ASC, [CABESTADO] ASC, [FECPROAPL] ASC)
    INCLUDE([CODPRODUC], [CODUNIMED], [CONSECUTI], [DOSISPROD], [DURACIDOS], [IPCODPACI], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_HCHOJAMED_CODPRODUC_IDHCORDCICLOSD_MEDESTADO_NUMINGRES]
    ON [dbo].[HCHOJAMED]([CODPRODUC] ASC, [IDHCORDCICLOSD] ASC)
    INCLUDE([MEDESTADO], [NUMINGRES]);


GO
CREATE NONCLUSTERED INDEX [IX_HCHOJAMED_IDHCORDQUIMIO_MEDESTADO]
    ON [dbo].[HCHOJAMED]([IDHCORDQUIMIO] ASC)
    INCLUDE([MEDESTADO]);


GO
CREATE NONCLUSTERED INDEX [IX_Medicamentos]
    ON [dbo].[HCHOJAMED]([IPCODPACI] ASC, [NUMINGRES] ASC, [MEDESTADO] ASC, [FECAPLMED] ASC)
    INCLUDE([CANDESCON], [CODPROAPL], [CODPRODUC], [CODTIPEST], [CODUNIMED], [CODVIAADM], [DOSISPROD], [DURACIDOS], [FRECUENCI], [UFUCODIGO], [UNIFRECUE]);


GO
CREATE NONCLUSTERED INDEX [XL_perfilFarmacoterapeutico]
    ON [dbo].[HCHOJAMED]([NUMINGRES] ASC, [CODPRODUC] ASC, [FECAPLMED] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el item de la dosis personalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'ITEMDOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Forma de prescripción: 0:Estandar, 1:Personalizada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FORMAPRESCRIBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la cita de Quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDCITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del ciclo de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOSD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del esquema de quimioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relación con la tabla ADAUTSERC ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IDAUTORIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que me identifica si el medicamento es de custodia, es decir si el medicamento es suministrado ya por el paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MEDICACUSTODIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'consecutivo de prescripcion medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CONSECPRESCRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del tipo de estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Administracion del medicamento (Dosis Manual)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'DESADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulacion Manual', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FORMUMANU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad a Descontar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CANDESCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario que suspende', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODUSUSUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo de Suspension', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MOTSUSMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicaciones de Aplicacion de Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Medicamento  1: Activo  2: Terminado  3: Suspendido por modficacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CABESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Medicamento  1: Pendiente  2: Aplicado  3: Suspendido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'MEDESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Final de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECFINDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duracion de la Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FRECUENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha/Hora del servidor para el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha/Hora real de aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha/Hora Programada de aplicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECPROAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha incial de Tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'FECINITRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional que Aplica / Suspende', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODPROAPL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Profesional de la Salud que prescribe', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutiov Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOJAMED', @level2type = N'COLUMN', @level2name = N'CONSECUTI';

