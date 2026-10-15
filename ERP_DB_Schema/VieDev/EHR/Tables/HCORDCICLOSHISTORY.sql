CREATE TABLE [EHR].[HCORDCICLOSHISTORY] (
    [Id]                       INT             IDENTITY (1, 1) NOT NULL,
    [IdHCORDCICLOS]            INT             NOT NULL,
    [SchemesId]                INT             NOT NULL,
    [IDHCORDQUIMIO]            INT             NOT NULL,
    [CICLO]                    INT             NOT NULL,
    [ESTADO]                   INT             NOT NULL,
    [IPCODPACI]                VARCHAR (25)    NOT NULL,
    [NUMINGRES]                CHAR (10)       NOT NULL,
    [NUMEFOLIO]                CHAR (10)       NOT NULL,
    [TALLA]                    NUMERIC (4, 1)  NOT NULL,
    [PESO]                     NUMERIC (18, 3) NOT NULL,
    [IMC]                      NUMERIC (18, 2) NOT NULL,
    [SCT]                      NUMERIC (18, 2) NOT NULL,
    [CODCENATE]                CHAR (10)       NOT NULL,
    [UFUCODIGO]                CHAR (10)       NOT NULL,
    [CODPROSAL]                CHAR (20)       NOT NULL,
    [CODESPECI]                CHAR (3)        NOT NULL,
    [FECHAREGISTRO]            DATETIME        NOT NULL,
    [CODSERIPS]                CHAR (20)       NULL,
    [IDDESCRIPCIONRELACIONADA] INT             NULL,
    [IDHCORDPRON]              INT             NOT NULL,
    [IDHCMOANULB]              CHAR (4)        NULL,
    [OBSERVACIONMOD]           VARCHAR (300)   NULL,
    [Environment]              INT             NULL,
    [ProfessionalModification] CHAR (20)       NULL,
    [DateModification]         DATETIME        NULL,
    [State]                    INT             NOT NULL,
    CONSTRAINT [PK_HCORDCICLOSHISTORY] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_HCMOANULB] FOREIGN KEY ([IDHCMOANULB]) REFERENCES [dbo].[HCMOANULB] ([CODMOTANU]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_HCORDQUIMIO] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_HCORDCICLOSHISTORY_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_ADCENATEN];


GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_ADINGRESO];


GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_HCMOANULB];


GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_HCORDQUIMIO];


GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_INCUPSIPS];


GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_INESPECIA];


GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_INPACIENT];


GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_INPROFSAL];


GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_INUNIFUNC];


GO
ALTER TABLE [EHR].[HCORDCICLOSHISTORY] NOCHECK CONSTRAINT [FK_HCORDCICLOSHISTORY_Schemes];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la cabecera del ciclo (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se modifica la cabecera del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional que modifica la cabecera del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1. Ambulatorio   2. Hospitalario   3.Mixta (Ambulatoria y Hospitalario) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'Environment';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observacion del motivo de la modificacion del esquema oncologico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'OBSERVACIONMOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del motivo de la modificacion del esquema oncologico  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCMOANULB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Identificador de la tabla HCORDPRON', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDPRON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id Descripción relacionada del servicio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Servicio asociado al esquema oncologico  o Código del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Fecha de registro', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo de la especialidad', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código del profesional', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo unidad funcional', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el código centro de atención', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Superficie Corporal Total', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'SCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Índice de masa corporal', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IMC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Peso', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'PESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Talla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'TALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Numero de folio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Código paciente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 - Solicitado sin cita  2 - Solicitado con cita     3 - Ciclo finalizado', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Numero de ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la orden de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guardo el Id de los Esquemas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID Autonumerico de la tabla HCORDCICLOS', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'IdHCORDCICLOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDCICLOSHISTORY', @level2type = N'COLUMN', @level2name = N'Id';

