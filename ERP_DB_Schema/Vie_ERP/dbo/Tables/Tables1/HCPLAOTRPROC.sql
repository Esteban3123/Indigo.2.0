CREATE TABLE [dbo].[HCPLAOTRPROC] (
    [ID]                INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCHISPACA]       INT                                                                              NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]         CHAR (10)                                                                        NOT NULL,
    [NUMEFOLIO]         NCHAR (10)                                                                       NOT NULL,
    [CODCENATE]         CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]         CHAR (10)                                                                        NOT NULL,
    [FECHAREGISTRO]     DATETIME                                                                         NOT NULL,
    [CODPROSAL]         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [NOMBREACOM]        VARCHAR (100)                                                                    NULL,
    [PARENTESCO]        INT                                                                              NULL,
    [MEDICO]            VARCHAR (100)                                                                    NULL,
    [ESPECIALIDAD]      CHAR (3)                                                                         NULL,
    [SEDACION]          BIT                                                                              NULL,
    [LOCAL]             BIT                                                                              NULL,
    [GENERAL]           BIT                                                                              NULL,
    [SINANESTESIA]      BIT                                                                              NULL,
    [HORAINICIO]        DATETIME                                                                         NULL,
    [HORAFINAL]         DATETIME                                                                         NULL,
    [ANESTESIOLOGO]     CHAR (20)                                                                        NULL,
    [DATOSCLINICOS]     VARCHAR (MAX)                                                                    NOT NULL,
    [AMBITO]            INT                                                                              NOT NULL,
    [ANALISIS]          VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Analysis_Ofuscado", 0)')      NOT NULL,
    [RESULCOLONOSCOPIA] INT                                                                              NULL,
    [ACOMPANANTE]       INT                                                                              NOT NULL,
    [PLANTILLA]         VARCHAR (MAX)                                                                    NULL,
    CONSTRAINT [PK_HCPLAOTRPROC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPLAOTRPROC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCPLAOTRPROC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCPLAOTRPROC] NOCHECK CONSTRAINT [FK_HCPLAOTRPROC_ADINGRESO];


GO
ALTER TABLE [dbo].[HCPLAOTRPROC] NOCHECK CONSTRAINT [FK_HCPLAOTRPROC_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda la plantilla de otros procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'PLANTILLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'viene con acompañante:  0-NO  1-SI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ACOMPANANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Se guarda el resultado de la colonoscopia (0. no aplica, 2. Hallazgos compatibles con pólipos hiperplásicos, 3. Hallazgos sugestivos de proceso preneoplásico , 4. Hallazgos sugestivos de proceso neoplásico, 5. Colonoscopia normal, 6. Otros hallazgos, 21. Riesgo no evaluado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'RESULCOLONOSCOPIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Analisis y Conclusiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ANALISIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ambito de realizacion: 1.Ambulatorio 2.Hospitalario 3.Urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'AMBITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Datos Clinicos Relevantes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'DATOSCLINICOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la especialidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ANESTESIOLOGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora Final (columna obsolota por refactoring)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'HORAFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora de inicio (columna obsolota por refactoring)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'HORAINICIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de anestesia: Sin Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'SINANESTESIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de anestesia: General', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'GENERAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de anestesia: Local', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'LOCAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de anestesia: Sedacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'SEDACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de especialidad (columna obsolota por refactoring)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ESPECIALIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medico que ordena el procedimiento (columna obsolota por refactoring)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'MEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parentesco del acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'PARENTESCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del acompañante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'NOMBREACOM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id tabla HCHISPACA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autonumerico cabecera de HC Otros Procedimientos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPLAOTRPROC', @level2type = N'COLUMN', @level2name = N'ID';

