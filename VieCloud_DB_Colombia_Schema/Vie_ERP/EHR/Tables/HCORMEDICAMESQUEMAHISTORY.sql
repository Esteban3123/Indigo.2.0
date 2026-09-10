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
ALTER TABLE [EHR].[HCORMEDICAMESQUEMAHISTORY] NOCHECK CONSTRAINT [FK_HCQUIMEDICAM_HCORMEDICAMESQUEMAHISTORY];




GO
ALTER TABLE [EHR].[HCORMEDICAMESQUEMAHISTORY] NOCHECK CONSTRAINT [FK_HCORMEDICAMESQUEMAHISTORY_HCFARMEPC];


GO



GO



GO



GO



GO



GO
ALTER TABLE [EHR].[HCORMEDICAMESQUEMAHISTORY] NOCHECK CONSTRAINT [FK_HCQUIMEDICAM_HCORMEDICAMESQUEMAHISTORY];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del medicamento en ciclo oncológico: 1=Original desde ordenamiento, 2=Adicionado, 3=Modificado, 4=Eliminado. INT, auditoría de cambios en esquema quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de la cabecera de los medicamentos del ciclo (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se modificó el registro del medicamento en el ciclo. Auditoria de cambios en prescripción oncológica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que se modifica la cabecera de los medicamentos del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula/identificación (CHAR 20, PII Identification_Ofuscado) del profesional sanitario que realizó la última modificación del medicamento en ciclo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que modifica la cabecera de los medicamentos del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción texto (VARCHAR 8000) de las instrucciones clínicas de administración del medicamento oncológico en el esquema, vía, dosis y observaciones.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DESCRIPADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la administración del medicamento en el esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DESCRIPADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DESCRIPADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prescripción oncológica: 1=Estándar, 2=Frecuencia. INT. Define modalidad de receta quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TypePrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Estandar   2 - Frecuencia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TypePrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TypePrescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de días de administración en formato rango (ej: 1,2,3,4,5-10) o listado (ej: 1,2,3,4,5,6,7,8,9,10). VARCHAR MAX, planificación del ciclo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta columna es un Identificador de los dias.  Ejemplo:  Rango dias [1,2,3,4,5-10]  SIN Rango dias [1,2,3,4,5,6,7,8,9,10]', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) del último evento registrado en la trazabilidad del trámite de medicamento. Auditoría de movimientos en solicitud farmacia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del último evento registrado al trámite', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID (INT) de la cabecera/solicitud de trámite en farmacia. Puede ser NULL si solicitud fue cancelada. Trazabilidad de pedidos.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del trámite, pueda que no tenga eventos relacionados y esto se da cuando se cancela una solicitud', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo (NUMERIC 18, FK→HCFARMEPC) del registro en farmacia. Solo para medicamentos administrados en casa solicitados desde dashboard quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla de farmacia, esto solo se registra a los medicamentos que son en casa y que fueron solicitados a farmacia desde el dashboard quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador BIT (Booleano) que registra si el medicamento ya fue solicitado a farmacia. Aplica solo para administración en domicilio/casa.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'SOLFARMACIAREALIZADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me Identifica si el Medicamento ya fue solicitado a Farmacia, OJO esto aplica solo para los medicamentos que se Administran en Casa.  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'SOLFARMACIAREALIZADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'SOLFARMACIAREALIZADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica PBS (VARCHAR 2000) requerida para medicamentos oncológicos con restricción PBS. Soporte para glosa/cobertura.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio (CHAR 10) del medicamento en el esquema. Identificador administrativo de la prescripción en ciclo quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de folio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación (INT, FK) con tabla HCORDCICLOS. Vincula el medicamento al ciclo específico de tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de ciclos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de diluyente (INT, default=1) usado para preparar el medicamento. Parámetro farmacotécnico de reconstitución.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen final (NUMERIC 18,2) en mL del medicamento después de dilución. Cálculo de administración en quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumne final', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diluyente (CHAR 20, FK→IHLISTPRO). Identificación del producto usado para reconstituir el medicamento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrucciones de administración (VARCHAR MAX) clínicas y técnicas: velocidad infusión, precauciones, monitoreo durante quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Instrucciones de Administracion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice secuencial (INT: 1,2,3...) del medicamento dentro del ciclo. Orden de administración en esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'INDICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice, 1,2,3,4...etc', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'INDICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'INDICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de factor (INT) farmacotécnico o de prescripción. Clasificación del medicamento en esquema quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Factor', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad de medida (VARCHAR 20, FK→INUNIMEDI): mg, mL, unidades. Unidad en que se expresa la dosis del medicamento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo unidad de medida', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis (NUMERIC 18,2) del medicamento en unidades de medida. Cantidad prescrita por día/ciclo en esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código vía de administración (VARCHAR 20, FK→HCVIAADMI): IV, IM, SC, VO, etc. Ruta clínica de medicamento en quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la via de administracion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto/medicamento (CHAR 20, FK→IHLISTPRO). Identificador del fármaco oncológico en catálogo farmacológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día de administración (VARCHAR MAX): número secuencial (1,2,3,4,5...) dentro del ciclo de tratamiento. Cronograma de dosis.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dia 1,2,3,4,5..etc', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ciclo (INT) de quimioterapia. Identifica el período repetido de tratamiento oncológico del paciente.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de clasificación ATC (Anatomical Therapeutic Chemical, INT). Referencia farmacológica internacional del medicamento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id atc entity', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'ATCEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación (INT, FK→HCORDQUIMIO) con la orden maestra de quimioterapia. Vincula medicamento al protocolo completo de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'relacion con la tabla cabecera de la orden de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación (INT, FK→Schemes) con esquema oncológico. Identifica el protocolo/régimen de quimioterapia al cual pertenece el medicamento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con en el esquema oncologico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo (INT, FK) de HCORMEDICAMESQUEMA. Referencia a registro padre del medicamento en esquema de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORMEDICAMESQUEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla HCORMEDICAMESQUEMA', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORMEDICAMESQUEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORMEDICAMESQUEMA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo/Primary Key (INT IDENTITY) del registro histórico. Identificador único en tabla HCORMEDICAMESQUEMAHISTORY para auditoría y trazabilidad.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historial de cambios y versiones del esquema de medicamentos oncológicos (quimioterapia) asociados a órdenes médicas. Registra cada modificación realizada sobre los esquemas de medicación por ciclo y día, permitiendo trazabilidad de las prescripciones quimioterapéuticas.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMAHISTORY';
