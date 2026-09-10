CREATE TABLE [EHR].[HCORDMEDICAM] (
    [ID]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesId]                INT             NOT NULL,
    [IDHCORDQUIMIO]            INT             NOT NULL,
    [ATCEntityId]              INT             NULL,
    [CICLO]                    INT             NOT NULL,
    [DIA]                      INT             NOT NULL,
    [CODPRODUC]                CHAR (20)       NOT NULL,
    [CODVIAADM]                VARCHAR (20)    NOT NULL,
    [DOSISPROD]                NUMERIC (18, 2) NOT NULL,
    [CODUNIMED]                VARCHAR (20)    NOT NULL,
    [TIPOFACTOR]               INT             NOT NULL,
    [CANTIDAD]                 INT             NOT NULL,
    [INDICE]                   INT             NOT NULL,
    [DOSISTEORICA]             NUMERIC (18, 2) NOT NULL,
    [INSTRUADMINIS]            VARCHAR (MAX)   NULL,
    [CODDILUYENTE]             CHAR (20)       NULL,
    [VOLUMENFINAL]             NUMERIC (18, 2) NULL,
    [CANTIDADDILU]             INT             CONSTRAINT [DF_HCORDMEDICAM_ESTADOCICLO] DEFAULT ((1)) NULL,
    [MEDICAMENTOENCASA]        INT             NULL,
    [HORAFRECUDIA]             DATETIME        NULL,
    [ProfessionalModification] CHAR (20)       NULL,
    [DateModification]         DATETIME        NULL,
    [State]                    INT             CONSTRAINT [DF_HCORDMEDICAM_State] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_HCORDMEDICAM] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDMEDICAM_HCORDMEDICAM] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM]),
    CONSTRAINT [FK_HCORDMEDICAM_HCORDMEDICAM1] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED]),
    CONSTRAINT [FK_HCORDMEDICAM_HCORDMEDICAM2] FOREIGN KEY ([CODDILUYENTE]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCORDMEDICAM_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCORDMEDICAM_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id]),
    CONSTRAINT [FK_HCQUIMEDICAM_HCORDMEDICAM] FOREIGN KEY ([IDHCORDQUIMIO]) REFERENCES [EHR].[HCORDQUIMIO] ([ID])
);


GO
ALTER TABLE [EHR].[HCORDMEDICAM] NOCHECK CONSTRAINT [FK_HCORDMEDICAM_HCORDMEDICAM2];




GO



GO



GO
ALTER TABLE [EHR].[HCORDMEDICAM] NOCHECK CONSTRAINT [FK_HCORDMEDICAM_HCORDMEDICAM2];


GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IndiceMedicamentosQuimio]
    ON [EHR].[HCORDMEDICAM]([IDHCORDQUIMIO] ASC, [CICLO] ASC, [CODPRODUC] ASC, [DOSISPROD] ASC)
    INCLUDE([DIA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del medicamento en la orden (1: Original del ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado). Indica auditoría de cambios en prescripción.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del medicamento (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación del registro del medicamento. Timestamp de cambio en la prescripción farmacológica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de modificacion del registro (medicamento) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del profesional de salud (médico, farmacéutico, enfermero) que realizó la modificación del medicamento. PII - Ofuscado.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que realiza la modificacion del registro (medicamento) ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de administración programada del medicamento. Usado cuando el fármaco se aplica múltiples veces en el mismo día (ej: 10:00, 13:00, 18:00). Frecuencia diaria de dosis.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'HORAFRECUDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me va contener la Fecha de la frecuencia:    Ejemplo: (10:00 - 13:00 - 18:00)    Dia 1 hora 10:00  Dia 1 hora 13:00  Dia 1 hora 18:00      Este campo viene diligenciado solo para los medicamentos que son marcados con frecuencia es decir que en el mismo dia se aplica varias veces el medicamento.  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'HORAFRECUDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'HORAFRECUDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de dispensación domiciliaria: 1=Sí se administra en casa, 2=No. Medicamento para autocuidado o atención ambulatoria en domicilio.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOENCASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se administra en casa?    1 - Si   2 - No ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOENCASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOENCASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la cantidad de diluyente: 1=Solicitado, 2=Administrado. Trazabilidad de dispensación del diluyente en quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Solicitado  2 - Administrado', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen total (en mL u unidad) del medicamento después de dilución. Calculado en preparación farmacéutica de infusiones.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Volumen final', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto diluyente (suero fisiológico, agua destilada, etc.). FK a IHLISTPRO. Insumo de preparación.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Codigo del diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrucciones clínicas de administración del medicamento (vía, velocidad, precauciones, observaciones). Campo descriptivo de praxis.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la instrucion adminitrar', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis calculada teóricamente según factor (superficie corporal, IMC, peso). Base para validación contra dosis administrada real.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'DOSISTEORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Dosis teorica', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'DOSISTEORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'DOSISTEORICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice o número secuencial de medicamentos dentro del ciclo de quimioterapia. Orden de aplicación en esquema.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'INDICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Indice medicamentos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'INDICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'INDICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades (comprimidos, ampollas, frascos) del medicamento por día. Volumen diario de dispensación.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del medicamento por dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de cálculo de dosis: 1=Superficie corporal total (m²), 2=Índice de masa corporal (IMC), 3=Peso (kg), 4=Sin factor. Parámetro antropométrico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Superficie corporal total  2-Indice de masa corporal  3-Peso  4-Sin factor', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad de medida del medicamento (mg, mL, UI, g, mcg). FK a INUNIMEDI. Dimensión de concentración.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración o dosis del producto ordenada por el médico (ej: 500 mg, 10 mL). Cantidad prescrita por unidad.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concetración ó Dosis del producto ordenada', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la vía de administración (IV, IM, VO, SC, tópica, inhalada, etc.). FK a HCVIAADMI. Ruta farmacológica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del producto medicamento en el catálogo de farmacotecnia. FK a IHLISTPRO. Identificador de fármaco.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código del producto', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del día dentro del ciclo de quimioterapia en que se administra el medicamento. Secuencia temporal.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ciclo de quimioterapia. Agrupa medicamentos administrados en un período repetitivo de tratamiento oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Ciclo  medicamentos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con tabla ATCEntity de VIE. Clasificación farmacoterapéutica ATC del medicamento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de VIE ATCEntity', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'ATCEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de la orden maestra de esquema de quimioterapia. FK a HCORDQUIMIO. Nexo con el plan oncológico completo.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del orden esquema de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del esquema de medicamentos (protocolo o pauta predefinida). FK a Schemes. Referencia a plantilla de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id de esquemas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria) del detalle de medicamento en la línea de orden. Consecutivo de registro.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de medicamentos ordenados en un esquema de quimioterapia. Registra cada medicamento del ciclo de tratamiento oncológico con su dosis, vía de administración, diluyente y volumen final, permitiendo hacer seguimiento de la orden médica de quimioterapia por ciclo y día.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAM';
