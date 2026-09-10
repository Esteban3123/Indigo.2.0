CREATE TABLE [dbo].[HCFICHA305] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [TIERESIACT]          INT           NULL,
    [HISTOCIRU]           BIT           NULL,
    [CUALOJO1]            INT           NULL,
    [PARPOJODER]          INT           NULL,
    [PARPOJOIZQ]          INT           NULL,
    [CICAMUCO]            BIT           NULL,
    [CUALOJO2]            INT           NULL,
    [DEPIPARPSUP]         BIT           NULL,
    [CUALOJO3]            INT           NULL,
    [EVIDENCIA1]          INT           NULL,
    [DEPIPARPINF]         BIT           NULL,
    [CUALOJO4]            INT           NULL,
    [EVIDENCIA2]          INT           NULL,
    [PESTCONT1]           BIT           NULL,
    [CUALOJO5]            INT           NULL,
    [EVIDENCIA3]          INT           NULL,
    [PESTCONT2]           BIT           NULL,
    [CUALOJO6]            INT           NULL,
    [EVIDENCIA4]          INT           NULL,
    [PESTTOCCORN]         BIT           NULL,
    [CUALOJO7]            INT           NULL,
    [EVIDENCIA5]          INT           NULL,
    [OPACORN]             INT           NULL,
    [ENGROPARP]           INT           NULL,
    [PESTMAL]             INT           NULL,
    [SENSCUERP]           INT           NULL,
    [FOTOFOB]             INT           NULL,
    [MEDIOJODER]          VARCHAR (50)  NULL,
    [MEDIOJOIZQ]          VARCHAR (50)  NULL,
    [EVIDENCIA11]         INT           NULL,
    [EVIDENCIA22]         INT           NULL,
    [EVIDENCIA33]         INT           NULL,
    [EVIDENCIA44]         INT           NULL,
    [EVIDENCIA55]         INT           NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA305] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA305_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA305_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA305] NOCHECK CONSTRAINT [CK_HCFICHA305_JSON];




GO
ALTER TABLE [dbo].[HCFICHA305] NOCHECK CONSTRAINT [CK_HCFICHA305_JSON];


GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacenamiento en formato JSON de estructura de ficha clínica oftalmológica, validado con CHECK CONSTRAINT isjson()', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda las nuevas tablas ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'JSON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número o identificador de versión del registro de ficha oftalmológica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la VERSION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'VERSION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evidencia de afectación ojo izquierdo (INT: 1=Sin evidencia, 2=1-5 pestañas, 3=6-10 pestañas, 4=11-20 pestañas, 5=Más de 20 pestañas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA55';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evidencia* (Ojo Izquierdo)  1=No hay Evidencia2=De 1 a 5 Pestañas3=De 6 a 10 Pestañas4=De 11 a 20 Pestañas5=Más de 20 Pestañas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA55';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA55';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evidencia de afectación ojo derecho (INT: 1=Sin evidencia, 2=1-5 pestañas, 3=6-10 pestañas, 4=11-20 pestañas, 5=Más de 20 pestañas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA44';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evidencia* (Ojo Derecho)  1=No hay Evidencia2=De 1 a 5 Pestañas3=De 6 a 10 Pestañas4=De 11 a 20 Pestañas5=Más de 20 Pestañas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA44';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA44';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evidencia de afectación ojo izquierdo (INT: 1=Sin evidencia, 2=1-5 pestañas, 3=6-10 pestañas, 4=11-20 pestañas, 5=Más de 20 pestañas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA33';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evidencia* (Ojo Izquierdo)  1=No hay Evidencia2=De 1 a 5 Pestañas3=De 6 a 10 Pestañas4=De 11 a 20 Pestañas5=Más de 20 Pestañas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA33';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA33';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evidencia de afectación ojo derecho (INT: 1=Sin evidencia, 2=1-5 pestañas, 3=6-10 pestañas, 4=11-20 pestañas, 5=Más de 20 pestañas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evidencia* (Ojo Derecho)  1=No hay Evidencia2=De 1 a 5 Pestañas3=De 6 a 10 Pestañas4=De 11 a 20 Pestañas5=Más de 20 Pestañas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA22';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA22';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evidencia de afectación ojo izquierdo (INT: 1=Sin evidencia, 2=1-5 pestañas, 3=6-10 pestañas, 4=11-20 pestañas, 5=Más de 20 pestañas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evidencia* (Ojo Izquierdo)  1=No hay Evidencia2=De 1 a 5 Pestañas3=De 6 a 10 Pestañas4=De 11 a 20 Pestañas5=Más de 20 Pestañas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA11';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA11';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medida o valor de agudeza visual ojo izquierdo (VARCHAR 50, ej: 20/20, 0.5)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'MEDIOJOIZQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medida Agudeza Visual Ojo Izquierdo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'MEDIOJOIZQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'MEDIOJOIZQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medida o valor de agudeza visual ojo derecho (VARCHAR 50, ej: 20/20, 0.5)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'MEDIOJODER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Medida Agudeza Visual Ojo Derecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'MEDIOJODER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'MEDIOJODER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sensibilidad a luz fotofobia (INT: 1=Ojo derecho, 2=Ojo izquierdo, 3=Ambos ojos, 4=No presenta)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'FOTOFOB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fotofobia  1= derecho2=izquierdo3=Ambos4=No presenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'FOTOFOB';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'FOTOFOB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sensación de cuerpo extraño ocular (INT: 1=Ojo derecho, 2=Ojo izquierdo, 3=Ambos ojos, 4=No presenta)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'SENSCUERP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sensación de Cuerpo Extraño  1= derecho2=izquierdo3=Ambos4=No presenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'SENSCUERP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'SENSCUERP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pestañas mal posicionadas, malposición de pestañas (INT: 1=Ojo derecho, 2=Ojo izquierdo, 3=Ambos ojos, 4=No presenta)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PESTMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pestaña mal Posicionadas  1= derecho2=izquierdo3=Ambos4=No presenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PESTMAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PESTMAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Engrosamiento del párpado (INT: 1=Ojo derecho, 2=Ojo izquierdo, 3=Ambos ojos, 4=No presenta)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'ENGROPARP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Engrosamiento del Párpado  1= derecho2=izquierdo3=Ambos4=No presenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'ENGROPARP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'ENGROPARP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Presencia de opacidad corneal (INT: 1=Ojo derecho, 2=Ojo izquierdo, 3=Ambos ojos, 4=No presenta)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'OPACORN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hay Opacidad Corneal  1= derecho2=izquierdo3=Ambos  4=No presenta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'OPACORN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'OPACORN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evidencia de afectación ojo izquierdo, recuento de pestañas (INT: 1=Sin evidencia, 2=1-5, 3=6-10, 4=11-20, 5=Más de 20 pestañas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evidencia* (Ojo Izquierdo)  1=No hay Evidencia  2=De 1 a 5 Pestañas  3=De 6 a 10 Pestañas  4=De 11 a 20 Pestañas  5=Más de 20 Pestañas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo afectado, especificación de localización (INT: 1=Ojo derecho, 2=Ojo izquierdo, 3=Ambos ojos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual otro ojo  1= derecho  2=izquierdo  3=Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO7';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pestaña tocando o en contacto con córnea (BIT: true=sí, false=no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PESTTOCCORN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hay Pestaña Tocando la Córnea  True = si   false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PESTTOCCORN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PESTTOCCORN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evidencia de afectación ojo derecho, recuento de pestañas (INT: 1=Sin evidencia, 2=1-5, 3=6-10, 4=11-20, 5=Más de 20 pestañas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evidencia* (Ojo Derecho)    1=No hay Evidencia  2=De 1 a 5 Pestañas  3=De 6 a 10 Pestañas  4=De 11 a 20 Pestañas  5=Más de 20 Pestañas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo afectado, especificación de localización (INT: 1=Ojo derecho, 2=Ojo izquierdo, 3=Ambos ojos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cual otro ojo  1= derecho  2=izquierdo  3=Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO6';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pestañas en contacto con globo ocular, párpado inferior (BIT: true=sí, false=no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PESTCONT2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pestañas en Contacto con el Globo Ocular Párpado Inferior  true = si   False= no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PESTCONT2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PESTCONT2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evidencia de afectación ojo izquierdo, recuento de pestañas (INT: 1=Sin evidencia, 2=1-5, 3=6-10, 4=11-20, 5=Más de 20 pestañas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evidencia* (Ojo Izquierdo)    1=No hay Evidencia  2=De 1 a 5 Pestañas  3=De 6 a 10 Pestañas  4=De 11 a 20 Pestañas  5=Más de 20 Pestañas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo afectado, especificación de localización (INT: 1=Ojo derecho, 2=Ojo izquierdo, 3=Ambos ojos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cual otro ojo  1= derecho  2=izquierdo  3=Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO5';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pestañas en contacto con globo ocular, párpado superior (BIT: true=sí, false=no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PESTCONT1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Pestañas en Contacto con el Globo Ocular en Párpado Superior  true = si   False= no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PESTCONT1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PESTCONT1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evidencia de afectación ojo derecho, recuento de pestañas (INT: 1=Sin evidencia, 2=1-5, 3=6-10, 4=11-20, 5=Más de 20 pestañas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evidencia* (Ojo Derecho)    1=No hay Evidencia  2=De 1 a 5 Pestañas  3=De 6 a 10 Pestañas  4=De 11 a 20 Pestañas  5=Más de 20 Pestañas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo afectado, especificación de localización (INT: 1=Ojo derecho, 2=Ojo izquierdo, 3=Ambos ojos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cual otro ojo  1= derecho2=izquierdo3=Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO4';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Depilación del párpado inferior realizada (BIT: true=sí, false=no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'DEPIPARPINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Depilación Párpado Inferior  True = sifalse = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'DEPIPARPINF';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'DEPIPARPINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Evidencia de afectación ojo derecho, recuento de pestañas (INT: 1=Sin evidencia, 2=1-5, 3=6-10, 4=11-20, 5=Más de 20 pestañas)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Evidencia* (Ojo Derecho)  1=No hay Evidencia  2=De 1 a 5 Pestañas  3=De 6 a 10 Pestañas  4=De 11 a 20 Pestañas  5=Más de 20 Pestañas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'EVIDENCIA1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo afectado, especificación de localización (INT: 1=Ojo derecho, 2=Ojo izquierdo, 3=Ambos ojos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En Cúal Ojo   1= derecho2=izquierdo3=Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO3';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Depilación del párpado superior realizada (BIT: true=sí, false=no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'DEPIPARPSUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Depilación Párpado Superior  True = si  false = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'DEPIPARPSUP';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'DEPIPARPSUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo afectado, especificación de localización (INT: 1=Ojo derecho, 2=Ojo izquierdo, 3=Ambos ojos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En Cúal Ojo  1= derecho  2=izquierdo  3=Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO2';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cicatrices en mucosa tarsal observadas (BIT: true=sí, false=no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CICAMUCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cicatrices en Mucosa Tarsal  True = si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CICAMUCO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CICAMUCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Párpado ojo izquierdo afectado (INT: 1=Párpado superior, 2=Párpado inferior, 3=Ambos párpados)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PARPOJOIZQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Párpado Ojo Izquierdo  1=parpado superior2=parpado inferior3=ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PARPOJOIZQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PARPOJOIZQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Párpado ojo derecho afectado (INT: 1=Párpado superior, 2=Párpado inferior, 3=Ambos párpados)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PARPOJODER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Párpado Ojo Derecho  1=parpado superior  2=parpado inferior  3=ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PARPOJODER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'PARPOJODER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ojo afectado, especificación de localización (INT: 1=Ojo derecho, 2=Ojo izquierdo, 3=Ambos ojos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'En Cúal Ojo  1=Derecho  2=Izquierdo  3=Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO1';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CUALOJO1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Historia o antecedente de cirugía de triquiasis realizada (BIT: true=sí, false=no)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'HISTOCIRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Historia de Cirugía de Triquiasis   true = si  false=no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'HISTOCIRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'HISTOCIRU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiempo en residencia actual del paciente (INT: 1=Menor de 5 años, 2=Entre 5 y 15 años, 3=15 años o más)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'TIERESIACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tiempo en Residencia Actual  1 = Menor de 5 Años  2 = Entre 5 y 15 Años  3 = 15 y Más', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'TIERESIACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'TIERESIACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico oftalmológico, referencia a diagnóstico (CHAR 4, ej: triquiasis)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de ficha de notificación relacionada, FK a HCFICHANOTIFICACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de ficha Notificacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único de registro, clave primaria clustered', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ficha clínica 305 de notificación oftalmológica: registra los hallazgos del examen ocular por paciente, incluyendo diagnóstico, antecedentes quirúrgicos, afecciones de párpados, córnea, mucosa, pestañas y sensaciones visuales (fotofobia, cuerpo extraño), tanto para ojo derecho como izquierdo, con evidencias asociadas y datos en formato JSON.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA305';
