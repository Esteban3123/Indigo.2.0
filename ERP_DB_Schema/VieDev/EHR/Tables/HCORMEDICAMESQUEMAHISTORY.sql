CREATE TABLE [EHR].[HCORMEDICAMESQUEMAHISTORY] (
    [ID]                            INT             IDENTITY (1, 1) NOT NULL,
    [IDHCORMEDICAMESQUEMA]          INT             NOT NULL,
    [SchemesId]                     INT             NOT NULL,
    [IDHCORDQUIMIO]                 INT             NOT NULL,
    [ATCEntityId]                   INT             NULL,
    [CICLO]                         INT             NOT NULL,
    [DIA]                           VARCHAR (MAX)   NOT NULL,
    [CODPRODUC]                     CHAR (20)       NOT NULL,
    [CODVIAADM]                     VARCHAR (20)    NOT NULL,
    [DOSIS]                         NUMERIC (18, 2) NOT NULL,
    [CODUNIMED]                     VARCHAR (20)    NOT NULL,
    [TIPOFACTOR]                    INT             NOT NULL,
    [INDICE]                        INT             NOT NULL,
    [INSTRUADMINIS]                 VARCHAR (MAX)   NULL,
    [CODDILUYENTE]                  CHAR (20)       NULL,
    [VOLUMENFINAL]                  NUMERIC (18, 2) NULL,
    [CANTIDADDILU]                  INT             CONSTRAINT [DF_HCORMEDICAMESQUEMAH_ESTADOCICLO] DEFAULT ((1)) NULL,
    [IDHCORDCICLOS]                 INT             NULL,
    [NUMEFOLIO]                     CHAR (10)       NULL,
    [JUSTIFICACIONPBS]              VARCHAR (2000)  NULL,
    [SOLFARMACIAREALIZADA]          BIT             NULL,
    [IDHCFARMEPC]                   NUMERIC (18)    NULL,
    [TraceabilityPaperworkId]       INT             NULL,
    [TraceabilityPaperworkEventsId] INT             NULL,
    [DESCRIPCIONDIA]                VARCHAR (MAX)   NULL,
    [TypePrescription]              INT             CONSTRAINT [DF_HCORMEDICAMESQUEMAH_TypePrescription] DEFAULT ((1)) NULL,
    [DESCRIPADMIN]                  VARCHAR (8000)  NULL,
    [ProfessionalModification]      CHAR (20)       NULL,
    [DateModification]              DATETIME        NULL,
    [State]                         INT             NOT NULL,
    CONSTRAINT [PK_HCORMEDICAMESQUEMAHISTORY] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_HCFARMEPC] FOREIGN KEY ([IDHCFARMEPC]) REFERENCES [dbo].[HCFARMEPC] ([CODCONCEC]),
    CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_HCORMEDICAMESQUEMA] FOREIGN KEY ([ID]) REFERENCES [EHR].[HCORMEDICAMESQUEMAHISTORY] ([ID]),
    CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_IHLISTPRO2] FOREIGN KEY ([CODDILUYENTE]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_INUNIMEDI] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED]),
    CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id]),
    CONSTRAINT [FK_HCQUIMEDICAM_HCORMEDICAMESQUEMAHISTORY] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID])
);


GO
ALTER TABLE [EHR].[HCORMEDICAMESQUEMAHISTORY] NOCHECK CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_HCFARMEPC];


GO
ALTER TABLE [EHR].[HCORMEDICAMESQUEMAHISTORY] NOCHECK CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_HCORMEDICAMESQUEMA];


GO
ALTER TABLE [EHR].[HCORMEDICAMESQUEMAHISTORY] NOCHECK CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_HCVIAADMI];


GO
ALTER TABLE [EHR].[HCORMEDICAMESQUEMAHISTORY] NOCHECK CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_IHLISTPRO2];


GO
ALTER TABLE [EHR].[HCORMEDICAMESQUEMAHISTORY] NOCHECK CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_INUNIMEDI];


GO
ALTER TABLE [EHR].[HCORMEDICAMESQUEMAHISTORY] NOCHECK CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_Schemes];


GO
ALTER TABLE [EHR].[HCORMEDICAMESQUEMAHISTORY] NOCHECK CONSTRAINT [FK_HCQUIMEDICAM_HCORMEDICAMESQUEMAHISTORY];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la cabecera de los medicamentos del ciclo (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha en que se modifica la cabecera de los medicamentos del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Profesional que modifica la cabecera de los medicamentos del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripcion de la administración del medicamento en el esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DESCRIPADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1 - Estandar   2 - Frecuencia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TypePrescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Esta columna es un Identificador de los dias.  Ejemplo:  Rango dias [1,2,3,4,5-10]  SIN Rango dias [1,2,3,4,5,6,7,8,9,10]', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del último evento registrado al trámite', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera del trámite, pueda que no tenga eventos relacionados y esto se da cuando se cancela una solicitud', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de la tabla de farmacia, esto solo se registra a los medicamentos que son en casa y que fueron solicitados a farmacia desde el dashboard quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo que me Identifica si el Medicamento ya fue solicitado a Farmacia, OJO esto aplica solo para los medicamentos que se Administran en Casa.  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'SOLFARMACIAREALIZADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero de folio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relacion con la tabla de ciclos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumne final', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrucciones de Administracion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indice, 1,2,3,4...etc', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'INDICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo Factor', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo unidad de medida', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la via de administracion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dia 1,2,3,4,5..etc', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Numero del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id atc entity', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'ATCEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'relacion con la tabla cabecera de la orden de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relacion con en el esquema oncologico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de la tabla HCORMEDICAMESQUEMA', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORMEDICAMESQUEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'ID';

