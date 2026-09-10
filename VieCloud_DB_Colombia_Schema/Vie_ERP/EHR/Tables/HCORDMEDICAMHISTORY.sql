CREATE TABLE [EHR].[HCORDMEDICAMHISTORY] (
    [ID]                       INT             IDENTITY (1, 1) NOT NULL,
    [IDHCORDMEDICAM]           INT             NOT NULL,
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
    [CANTIDADDILU]             INT             NULL,
    [MEDICAMENTOENCASA]        INT             NULL,
    [HORAFRECUDIA]             DATETIME        NULL,
    [ProfessionalModification] CHAR (20)       NULL,
    [DateModification]         DATETIME        NULL,
    [State]                    INT             NOT NULL,
    CONSTRAINT [PK_HCORDMEDICAMHISTORY] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORDMEDICAMHISTORY_HCORDMEDICAM1] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED]),
    CONSTRAINT [FK_HCORDMEDICAMHISTORY_HCORDMEDICAM2] FOREIGN KEY ([CODDILUYENTE]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCORDMEDICAMHISTORY_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCORDMEDICAMHISTORY_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id])
);


GO
ALTER TABLE [EHR].[HCORDMEDICAMHISTORY] NOCHECK CONSTRAINT [FK_HCORDMEDICAMHISTORY_HCORDMEDICAM2];




GO



GO
ALTER TABLE [EHR].[HCORDMEDICAMHISTORY] NOCHECK CONSTRAINT [FK_HCORDMEDICAMHISTORY_HCORDMEDICAM2];


GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del detalle medicamentoso del ciclo de quimioterapia (1: Ordenado original, 2: Adicionado, 3: Modificado, 4: Eliminado). INT. Auditoria de cambios.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del detalle de los medicamentos del ciclo (1: Con origen desde el ordenamiento, 2: Adicionado, 3: Modificado, 4: Eliminado)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de modificación del detalle medicamentoso en el ciclo. DATETIME. Trazabilidad de cambios en prescripción.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha en que que se modifica el detalle de los medicamentos del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DateModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cédula/identificación del profesional de salud que modifica el detalle medicamentoso. CHAR(20). PII. Responsable del cambio.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Profesional que modifica el detalle de los medicamentos del ciclo', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'ProfessionalModification';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha-hora de administración del medicamento con frecuencia diaria (ej: 10:00, 13:00, 18:00). DATETIME. Solo se completa si el medicamento se aplica múltiples veces en el mismo día.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'HORAFRECUDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo que me va contener la Fecha de la frecuencia:    Ejemplo: (10:00 - 13:00 - 18:00)    Dia 1 hora 10:00  Dia 1 hora 13:00  Dia 1 hora 18:00      Este campo viene diligenciado solo para los medicamentos que son marcados con frecuencia es decir que en el mismo dia se aplica varias veces el medicamento.  ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'HORAFRECUDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'HORAFRECUDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el medicamento se administra en domicilio del paciente (1: Sí, 2: No). INT. Atención domiciliaria.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOENCASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Se administra en casa?    1 - Si   2 - No ', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOENCASA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'MEDICAMENTOENCASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la cantidad de diluyente (1: Solicitado, 2: Administrado). INT. Control de preparación farmacéutica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Solicitado  2 - Administrado', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CANTIDADDILU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Volumen final en mililitros (mL) después de diluir el medicamento. NUMERIC(18,2). Reconstitución de fármacos.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda Volumen final', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'VOLUMENFINAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto diluyente/disolvente farmacéutico. CHAR(20). FK a IHLISTPRO. Solución base para reconstitución.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Codigo del diluyente', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODDILUYENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrucciones de administración del medicamento: técnica, precauciones, monitoreo. VARCHAR(MAX). Guía clínica para enfermería.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la instrucion adminitrar', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis teórica calculada del fármaco según esquema oncológico. NUMERIC(18,2). Valor de referencia previo a ajustes.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISTEORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la Dosis teorica', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISTEORICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISTEORICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Índice secuencial del medicamento dentro del ciclo de quimioterapia. INT. Orden de aplicación.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'INDICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Indice medicamentos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'INDICE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'INDICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del medicamento administradas por día. INT. Frecuencia diaria de dosificación.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del medicamento por dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Factor de cálculo de dosis (1: Superficie corporal, 2: IMC, 3: Peso, 4: Sin factor). INT. Base de dosimetría oncológica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1-Superficie corporal total  2-Indice de masa corporal  3-Peso  4-Sin factor', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad de medida (mg, mL, UI, etc.). VARCHAR(20). FK a INUNIMEDI. Dimensión farmacéutica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración o dosis del producto ordenado en la prescripción. NUMERIC(18,2). Potencia farmacéutica del fármaco.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concetración ó Dosis del producto ordenada', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la vía de administración (IV, IM, SC, PO, etc.). VARCHAR(20). Ruta de entrada del medicamento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto/medicamento en catálogo farmacéutico. CHAR(20). FK a IHLISTPRO. Identificador de fármaco.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el código del producto', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del día dentro del ciclo de quimioterapia (1-21, etc.). INT. Secuencia temporal del tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Dia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'DIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del ciclo de quimioterapia (1er ciclo, 2do ciclo, etc.). INT. Identificador de sesión oncológica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Ciclo  medicamentos', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'CICLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la clasificación ATC (Anatomical Therapeutic Chemical) del fármaco. INT. FK a EHR.ATCEntity. Estandarización farmacológica.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de VIE ATCEntity', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'ATCEntityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'ATCEntityId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del ordenamiento/esquema de quimioterapia padre. INT. FK a HCORDQUIMIO. Relación con plan oncológico.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del orden esquema de quimioterapia', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDQUIMIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del esquema terapéutico de quimioterapia. INT. FK a EHR.Schemes. Protocolo de tratamiento.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Id de esquemas', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID consecutivo de la orden medicamentosa padre. INT. FK a HCORDMEDICAM. Relación con detalle de prescripción.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDMEDICAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla HCORDMEDICAM', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDMEDICAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'IDHCORDMEDICAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de historial medicamentoso del ciclo. INT IDENTITY. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historial de líneas de medicamentos en órdenes médicas de quimioterapia. Registra cada medicamento prescrito por ciclo y día dentro de un esquema de tratamiento oncológico, incluyendo dosis, vía de administración, diluyente y modificaciones realizadas por el profesional.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCORDMEDICAMHISTORY';
