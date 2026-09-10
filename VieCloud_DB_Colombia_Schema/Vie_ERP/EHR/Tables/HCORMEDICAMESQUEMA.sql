CREATE TABLE [EHR].[HCORMEDICAMESQUEMA] (
    [ID]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
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
    [CANTIDADDILU]                  INT             CONSTRAINT [DF_HCORMEDICAMESQUEMA_ESTADOCICLO] DEFAULT ((1)) NULL,
    [IDHCORDCICLOS]                 INT             NULL,
    [NUMEFOLIO]                     CHAR (10)       NULL,
    [JUSTIFICACIONPBS]              VARCHAR (2000)  NULL,
    [SOLFARMACIAREALIZADA]          BIT             NULL,
    [IDHCFARMEPC]                   NUMERIC (18)    NULL,
    [TraceabilityPaperworkId]       INT             NULL,
    [TraceabilityPaperworkEventsId] INT             NULL,
    [DESCRIPCIONDIA]                VARCHAR (MAX)   NULL,
    [TypePrescription]              INT             CONSTRAINT [DF_HCORMEDICAMESQUEMA_TypePrescription] DEFAULT ((1)) NULL,
    [DESCRIPADMIN]                  VARCHAR (8000)  NULL,
    [ProfessionalModification]      CHAR (20)       NULL,
    [DateModification]              DATETIME        NULL,
    [State]                         INT             CONSTRAINT [DF_HCORMEDICAMESQUEMA_State] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_HCORMEDICAMESQUEMA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORMEDICAMESQUEMA_HCFARMEPC] FOREIGN KEY ([IDHCFARMEPC]) REFERENCES [dbo].[HCFARMEPC] ([CODCONCEC]),
    CONSTRAINT [FK_HCORMEDICAMESQUEMA_HCORMEDICAMESQUEMA] FOREIGN KEY ([ID]) REFERENCES [EHR].[HCORMEDICAMESQUEMA] ([ID]),
    CONSTRAINT [FK_HCORMEDICAMESQUEMA_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_HCORMEDICAMESQUEMA_IHLISTPRO2] FOREIGN KEY ([CODDILUYENTE]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCORMEDICAMESQUEMA_INUNIMEDI] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED]),
    CONSTRAINT [FK_HCORMEDICAMESQUEMA_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id]),
    CONSTRAINT [FK_HCQUIMEDICAM_HCORMEDICAMESQUEMA] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID])
);




GO



GO



GO



GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IndiceMedicamentosQuimio]
    ON [EHR].[HCORMEDICAMESQUEMA]([IDHCORDQUIMIO] ASC, [CICLO] ASC, [CODPRODUC] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del ciclo medicamentoso (1=Ordenado, 2=Adicionado, 3=Modificado, 4=Eliminado). INT, auditoría de cambios en esquema oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del ciclo (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de modificación del registro de medicamento en esquema. DATETIME, trazabilidad de cambios en prescripción quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion del registro (Medicamento esquema) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código/identificación del profesional que modificó el medicamento en esquema. CHAR(20), auditoría sanitaria, equivalente a médico/oncólogo responsable.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realiza la modificacion del registro (medicamento esquema) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción detallada de instrucciones de administración del medicamento en esquema oncológico. VARCHAR(8000), complementa vía, dosis y diluyentes.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DESCRIPADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de la administración del medicamento en el esquema', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DESCRIPADMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DESCRIPADMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de prescripción medicamentosa (1=Estándar, 2=Frecuencia variable). INT, determina patrón de aplicación en ciclo quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'TypePrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Estandar   2 - Frecuencia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'TypePrescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'TypePrescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Patrón de días de aplicación en ciclo (ej: rango [1,2,3,4,5-10] o listado [1,2,3,4,5,6,7,8,9,10]). VARCHAR(MAX), planificación temporal oncológica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Esta columna es un Identificador de los dias.  Ejemplo:  Rango dias [1,2,3,4,5-10]  SIN Rango dias [1,2,3,4,5,6,7,8,9,10]', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DESCRIPCIONDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del último evento registrado en trámite/solicitud del medicamento. INT, referencia a auditoría de gestión administrativa.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del último evento registrado al trámite', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkEventsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de cabecera del trámite/solicitud. INT, agrupa eventos; puede ser nulo si solicitud cancelada, trazabilidad integral.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del trámite, pueda que no tenga eventos relacionados y esto se da cuando se cancela una solicitud', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'TraceabilityPaperworkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de farmacia para medicamentos administrados en domicilio solicitados desde módulo quimioterapia. NUMERIC(18), FK a HCFARMEPC.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla de farmacia, esto solo se registra a los medicamentos que son en casa y que fueron solicitados a farmacia desde el dashboard quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) si medicamento en casa ya fue solicitado a farmacia. Aplica solo medicamentos domiciliarios, control gestión suministro.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'SOLFARMACIAREALIZADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me Identifica si el Medicamento ya fue solicitado a Farmacia, OJO esto aplica solo para los medicamentos que se Administran en Casa.  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'SOLFARMACIAREALIZADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'SOLFARMACIAREALIZADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica PBS (Formulario de Beneficios) para autorización medicamento. VARCHAR(2000), evidencia regulatoria y cobertura.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación clínica PBS', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'JUSTIFICACIONPBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio administrativo del medicamento en esquema. CHAR(10), identificador de trámite documental.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de folio', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con tabla de ciclos oncológicos. INT, FK a ciclos del tratamiento quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de ciclos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'IDHCORDCICLOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de diluyente a usar para preparación del medicamento. INT, default=1, soporte preparación farmacéutica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen final de disolución del medicamento después dilución. NUMERIC(18,2), cálculo de concentración final.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Volumne final', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto diluyente (suero fisiológico, agua destilada, etc.). CHAR(20), FK a IHLISTPRO.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrucciones detalladas de administración del medicamento (velocidad infusión, precauciones). VARCHAR(MAX), protocolo seguridad.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Instrucciones de Administracion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice secuencial del medicamento en esquema (1,2,3,4...). INT, orden de administración dentro ciclo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'INDICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indice, 1,2,3,4...etc', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'INDICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'INDICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de factor/coeficiente para cálculo de dosis. INT, parámetro farmacométrico personalizado.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo Factor', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad de medida (mg, mL, unidad, etc.). VARCHAR(20), FK a INUNIMEDI.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo unidad de medida', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad/dosis de medicamento a administrar. NUMERIC(18,2), valor numérico con precisión farmacéutica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DOSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DOSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de vía de administración (IV, IM, oral, intratecal, etc.). VARCHAR(20), FK a HCVIAADMI.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la via de administracion', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del medicamento/producto oncológico en catálogo. CHAR(20), identificador de fármaco prescrito.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día de aplicación en ciclo (1,2,3,4,5,etc.). VARCHAR(MAX), patrón temporal de administración.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dia 1,2,3,4,5..etc', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial del ciclo de quimioterapia (1=primer ciclo, 2=segundo, etc.). INT, periodización tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de entidad ATC (Clasificación Anatómica Terapéutica Química). INT, clasificación farmacológica internacional.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id atc entity', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'ATCEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con orden cabecera de quimioterapia. INT, FK a HCORDQUIMIO, rastreo a prescripción padre.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'relacion con la tabla cabecera de la orden de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con esquema oncológico estándar. INT, FK a Schemes, vinculación a protocolo terapéutico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con en el esquema oncologico', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del medicamento en esquema oncológico. INT IDENTITY, secuencia autoincrementable.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de medicamentos por esquema de quimioterapia: cada fila representa un medicamento prescrito dentro de un ciclo y día específico de un protocolo oncológico, incluyendo dosis, vía de administración, diluyente y justificación de cobertura PBS.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORMEDICAMESQUEMA';
