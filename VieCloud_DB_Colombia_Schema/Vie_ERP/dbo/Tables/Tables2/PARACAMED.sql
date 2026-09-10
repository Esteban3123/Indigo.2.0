CREATE TABLE [dbo].[PARACAMED] (
    [ID]            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]     CHAR (10)       NOT NULL,
    [CODPRODUC]     CHAR (20)       NOT NULL,
    [CODVIAADM]     VARCHAR (20)    NULL,
    [CODFORMED]     CHAR (3)        NULL,
    [DOSISPROD]     NUMERIC (18, 2) NULL,
    [CODUNIMED]     CHAR (3)        NULL,
    [FRECUENCI]     INT             NULL,
    [UNIFRECUE]     CHAR (1)        NULL,
    [DURACIDOS]     CHAR (20)       NULL,
    [CANPEDPRO]     INT             NOT NULL,
    [JUSTIFICACION] VARCHAR (MAX)   NULL,
    [VALDURFIJ]     INT             NULL,
    [UNIDURFIJ]     CHAR (1)        NULL,
    [INDAPLMED]     VARCHAR (MAX)   NULL,
    [CARGAUTO]      INT             NOT NULL,
    [DOSISPRFN]     NUMERIC (18, 2) NULL,
    [CODUNIMFN]     CHAR (3)        NULL,
    [DESADMINI]     VARCHAR (MAX)   NULL,
    [TIPFORMED]     CHAR (1)        NULL,
    [TOTPROUNI]     NUMERIC (18, 2) NULL,
    CONSTRAINT [PK_PARACAMED] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PARACAMED_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_PARACAMED_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Total del Producto por Unidad de Administración. NUMERIC(18,2). Cantidad acumulada de sustancia activa por dosis unitaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Total del producto por unidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'TOTPROUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Formulación: 1=Peso (mg, g), 2=Volumen (mL), 3=Peso-Volumen, 4=Unidad de Administración (comprimidos, ampolletas). CHAR(1).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Formulacion del medicamento:  1 Peso  2 Volumen  3 Peso-Volumen  4 Unidad de Administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'TIPFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'TIPFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de la Administración del medicamento (dosis manual, instrucciones paso a paso). VARCHAR(MAX). Guía operativa para el profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Administracion del medicamento (Dosis Manual)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DESADMINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DESADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad de Medida final tras conversiones (g, mg, mcg). CHAR(3). Unidad estándar después de ajuste de escala.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para almacenar la Unidad Medida final cuando hay conversiones entre (Gramos , Miligramos , Microgramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODUNIMFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis final después de conversiones entre unidades (Gramos, Miligramos, Microgramos). NUMERIC(18,2). Valor normalizado tras cálculos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para almacenar la dosis final cuando hay conversiones entre (Gramos , Miligramos , Microgramos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DOSISPRFN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cargar automáticamente en recién nacidos en la primera historia clínica. INT (booleano: 0/1). Indica si se carga por defecto en neonatos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CARGAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cargar automaticamente en recien la primera historia nacidos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CARGAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CARGAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicaciones de Aplicación del Medicamento (instrucciones clínicas, restricciones, condiciones). VARCHAR(MAX). Orientación al paciente/profesional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indicaciones de Aplicacion de Medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'INDAPLMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'INDAPLMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de la Duración Fija: 1=Minutos, 2=Horas, 3=Días. CHAR(1). Escala temporal del tratamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la duracion fija:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'UNIDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la Duración Fija (número). INT. Tiempo de tratamiento con valor numérico fijo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de la duracion fija', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'VALDURFIJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Justificación clínica o administrativa de la prescripción. VARCHAR(MAX). Campo libre para motivo, diagnóstico asociado o aclaraciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'JUSTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Justificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'JUSTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'JUSTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad Pedida del Producto (número de unidades solicitadas). INT. Volumen total de medicamento requerido para la prescripción.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad Pedida del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CANPEDPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración de la dosis en tratamientos continuos o fijos. CHAR(20). Especifica cuánto tiempo se mantiene el esquema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion de la Dosis   Fija   tratamientos continuo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DURACIDOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DURACIDOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de la Frecuencia: 1=Minutos, 2=Horas, 3=Días. CHAR(1). Define el período entre dosis.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad del Valor de la Frecuencia:  1: Minutos  2: Horas  3: Dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'UNIFRECUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia de administración (número de veces). INT. Se complementa con UNIFRECUE para especificar cada cuánto tiempo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'FRECUENCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'FRECUENCI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Unidad de Medida de la dosis (mg, g, mcg, mL, UI, etc.). CHAR(3). Define la escala de DOSISPROD.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad de Medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del producto (cantidad administrada por toma). NUMERIC(18,2). Valor numérico de la potencia prescrita.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'DOSISPROD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Forma de Presentación del medicamento (comprimido, inyección, suspensión, crema, etc.). CHAR(3).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Forma de presentacion del medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODFORMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Vía de Administración (oral, intravenosa, intramuscular, tópica, etc.). VARCHAR(20), FK a HCVIAADMI. Indica cómo se administra el fármaco.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Via de Administracion Comun', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Producto (medicamento, sustancia, insumo). CHAR(20). Referencia al catálogo farmacéutico del ERP.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención. CHAR(10), FK a ADCENATEN. Identifica la sede/unidad funcional donde se prescribe el medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY), consecutivo de la tabla PARACAMED. INT, clave primaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de medicamentos por centro de atención: define las condiciones de prescripción de cada producto/medicamento, incluyendo vía de administración, forma farmacéutica, dosis, frecuencia, duración del tratamiento y cantidad a pedir. Sirve como configuración base para la prescripción médica automatizada.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PARACAMED';
